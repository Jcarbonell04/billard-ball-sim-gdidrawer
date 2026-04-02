using GDIDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        }

        /// <summary>
        /// populate _listBalls with random balls and a cue ball
        /// </summary>
        public void MakeBalls(int numBalls)
        {

        }

        /// <summary>
        /// draw all the balls on the Canvas and handle the cue ball aim when mouse is idle
        /// </summary>
        public void ShowTable()
        {

        }

        private void Canvas_MouseMoveScaled(Point mouse)
        {
            _currentLocation = new Vector2(mouse.X, mouse.Y);

            // only redraw aiming line if balls are not moving/idle
            if (!Running)
                ShowTable();
        }

        private void Canvas_MouseLeftClickScaled(Point mouse)
        {
            if (_cueBall == null) return;

            // reset hits for new shot
            foreach (var b in _listBalls)
                b.ResetHits();

            // calculate from cue ball to mouse click
            Vector2 direction = new Vector2(mouse.X, mouse.Y) - _cueBall.Center;

            if (direction.LengthSquared() < 0.01f) return; // avoid zero length

            direction = Vector2.Normalize(direction) * 40f; // constant shot speed

            _cueBall.SetVelocity(direction); // start simulation
        }

    }
}
