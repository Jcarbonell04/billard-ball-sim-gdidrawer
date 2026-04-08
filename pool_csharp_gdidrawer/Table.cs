// Table.cs - Manages the pool table, balls, canvas, and aiming
// Handles rendering, ball creation, and shot execution
// Author: Jaedyn Carbonell

using GDIDrawer;
using System.Numerics;

namespace pool_csharp_gdidrawer
{
    internal class Table
    {
        // DATA MEMBERS
        private List<Ball> _listBalls = new List<Ball>();

        private Vector2 _currentLocation = Vector2.Zero; // current mouse location

        private Ball _cueBall = null;

        public CDrawer Canvas { get; private set; } = null;

        public List<Ball> Balls
        {
            get
            {
                return new List<Ball>(_listBalls);
            }
        }
        public bool Running 
        {
            get
            {
                foreach (Ball b in _listBalls)
                {
                    // game is running if velocity is grester than 0
                    if (b.Velocity.LengthSquared() > 0.01f) 
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        ///  Single default, explicit, but does nothing.
        /// </summary>
        public Table()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="numBall"></param>
        public void MakeTable(int width, int height, int numBall)
        {
            // close existing canvas if any
            if (Canvas != null)
                Canvas.Close();

            // create new drawer
            Canvas = new CDrawer(width, height, false);
            Canvas.RedundaMouse = true; // odd name
            Canvas.BBColour = Color.Green;

            // bind events
            Canvas.MouseMoveScaled += Canvas_MouseMoveScaled;
            Canvas.MouseLeftClickScaled += Canvas_MouseLeftClickScaled;

            // create balls
            MakeBalls(numBall);

            // initial draw
            ShowTable();
        }

        /// <summary>
        /// populate _listBalls with random balls and a cue ball
        /// </summary>
        public void MakeBalls(int numBalls)
        {
            _listBalls.Clear();

            if (!Running)
            {
                // regular balls (no overlap)
                for (int i = 0; i < numBalls; i++)
                {
                    Ball b;
                    do
                    {
                        b = new Ball(Canvas, RandColor.GetColor());
                    }
                    while (_listBalls.Exists(existing => existing.Equals(b)));

                    _listBalls.Add(b);
                }

                // cue ball
                Ball cueBall;
                int attempts = 0;

                do
                {
                    cueBall = new Ball(Canvas);
                    attempts++;
                }
                while (_listBalls.Exists(existing => existing.Equals(cueBall)) && attempts < 1000);

                _cueBall = cueBall;
                _listBalls.Add(cueBall);
            }
        }

        /// <summary>
        /// draw all the balls on the Canvas and handle the cue ball aim when mouse is idle
        /// </summary>
        public void ShowTable()
        {
            if (Canvas == null)
                return;

            Canvas.Clear();

            foreach (Ball b in _listBalls)
            {
                b.Show(Canvas);
                b.Move(Canvas, _listBalls);
            }

            // only draw when balls have stopped
            if (!Running && _cueBall != null)
            {
                Canvas.AddLine
                (
                    (int)_cueBall.Center.X,
                    (int)_cueBall.Center.Y,
                    (int)_currentLocation.X,
                    (int)_currentLocation.Y,
                    Color.Yellow
                );

                Vector2 dir = Vector2.Normalize(_currentLocation - _cueBall.Center);
                Vector2 perp = new Vector2(-dir.Y, dir.X);

                // mult dir by 15 to go back along the line from the tip
                // mult perp by 5 to offset sideways for the wing

                // AddLine(x tip, y tip, x wing end, y wing end, colour) 
                Canvas.AddLine((int)_currentLocation.X, 
                               (int)_currentLocation.Y,
                               (int)(_currentLocation.X - dir.X * 15 + perp.X * 5),
                               (int)(_currentLocation.Y - dir.Y * 15 + perp.Y * 5), 
                               Color.Red);

                Canvas.AddLine((int)_currentLocation.X, 
                               (int)_currentLocation.Y,
                               (int)(_currentLocation.X - dir.X * 15 - perp.X * 5),
                               (int)(_currentLocation.Y - dir.Y * 15 - perp.Y * 5), 
                               Color.Red);

            }

            Canvas.Render();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="canvas"></param>
        private void Canvas_MouseMoveScaled(Point mousePos, CDrawer canvas)
        {
            // update field member
            _currentLocation = new Vector2(mousePos.X, mousePos.Y);

            // only redraw aiming line if balls are not moving/idle
            if (!Running)
                ShowTable();
        }

        private void Canvas_MouseLeftClickScaled(Point mousePos, CDrawer canvas)
        {
            if (_cueBall == null) return;

            // reset hits for new shot
            foreach (var b in _listBalls)
                b.ResetHits();

            // calculate from cue ball to mouse click
            //Vector2 direction = new Vector2(mousePos.X, mousePos.Y) - _cueBall.Center;

            Vector2 direction = _currentLocation - _cueBall.Center;

            if (direction.LengthSquared() < 0.01f) 
                return; // avoid zero length, for normalization

            // constant shot speed
            //direction = Vector2.Normalize(direction) * 40f; 

            // variable shot power
            float strength = direction.Length() / 15f;
            direction = Vector2.Normalize(direction) * strength;

            // apply velocity
            _cueBall.SetVelocity(direction); 
        }
    }
}
