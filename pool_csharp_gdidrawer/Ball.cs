using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics; 
using GDIDrawer;

namespace pool_csharp_gdidrawer
{
    public class Ball : IComparable<Ball>
    {
        // Static Members
        public static float Friction { get; set; } = 0.991f;
        private static Random rnd = new Random();

        // Instance Members
        private Vector2 _center;
        private Vector2 _velocity;

        public int Radius { get; private set; }
        public int Hits { get; private set; }
        public int TotalHits { get; private set; }
        public Color BallColor { get; private set; }

        public Vector2 Center => _center;
        public Vector2 Velocity => _velocity;

        private readonly Color CueBorderColor = Color.Yellow;

        // Constructors
        // Cue ball
        /// <summary>
        /// creates a instance of cue ball
        /// </summary>
        /// <param name="drawer"></param>
        public Ball(CDrawer drawer)
        {
            Radius = 30;
            BallColor = Color.White;
            _center = new Vector2(rnd.Next(Radius, drawer.ScaledWidth - Radius),rnd.Next(Radius, drawer.ScaledHeight - Radius));
        }

        // Regular ball
        /// <summary>
        /// Set the BallColor. Randomize a Radius between 20 and 50
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="color"></param>
        public Ball(CDrawer drawer, Color color)
        {
            BallColor = color;
            Radius = rnd.Next(20, 51);
            _center = new Vector2(rnd.Next(Radius, drawer.ScaledWidth - Radius), rnd.Next(Radius, drawer.ScaledHeight - Radius));
        }

        // Methods
        /// <summary>
        /// resets the hit count to 0
        /// </summary>
        public void ResetHits() => Hits = 0;

        /// <summary>
        /// assign _velocity to the supplied argument
        /// </summary>
        /// <param name="v"></param>
        public void SetVelocity(Vector2 v) => _velocity = v;

        /// <summary>
        /// slow things down by multipling the velocity with the friction, 
        /// </summary>
        /// <param name="drawer">CDrawer to draw on</param>
        /// <param name="balls">ball list to show animation</param>
        public void Move(CDrawer drawer, List<Ball> balls)
        {
            // apply friction
            _velocity *= Friction;

            // stop if velocity is near
            if (_velocity.LengthSquared() < 0.1f)
            {
                _velocity = Vector2.Zero;
                return;
            }

            // Wall collisions
            if (_center.X - Radius < 0 && _velocity.X < 0) _velocity.X *= -1;
            if (_center.X + Radius > drawer.ScaledWidth && _velocity.X > 0) _velocity.X *= -1;
            if (_center.Y - Radius < 0 && _velocity.Y < 0) _velocity.Y *= -1;
            if (_center.Y + Radius > drawer.ScaledHeight && _velocity.Y > 0) _velocity.Y *= -1;

            // move ball
            _center += _velocity;

            // ball collisions
            foreach (Ball other in balls)
            {
                if (other == this)
                {
                    continue;
                }

                float dist = Vector2.Distance(_center, other._center);
                float minDist = this.Radius + other.Radius;

                if (dist < minDist)
                {
                    ProcessCollision(other);
                }
            }
        }

        /// <summary>
        /// this bounces the overlapping iteration ball with itself
        /// </summary>
        /// <param name="tar"></param>
        private void ProcessCollision(Ball tar)
        {
            Vector2 dist = tar._center - _center; // Get Collision Vector
            Vector2 myNorm = Vector2.Normalize(tar._center - _center); // Normalize for invoking ball
            Vector2 targetNorm = Vector2.Normalize(_center - tar._center); // Normalize for target ball

            // Calculate Radius weighted velocity vector
            //Vector2 CMVelocity = Vector2.Add(Vector2.Multiply((float)_iRadius / (_iRadius + tar._iRadius), _v), Vector2.Multiply((float)tar._iRadius / (_iRadius + tar._iRadius), tar._v));
            Vector2 CMVelocity = (_velocity * ((float)Radius / (Radius + tar.Radius)) + (tar._velocity * ((float)tar.Radius / (Radius + tar.Radius))));

            // Process invoking ball
            _velocity -= CMVelocity;// Vector2.Subtract(_v, CMVelocity);
            _velocity = Vector2.Reflect(_velocity, myNorm); // perform "bounce"
            _velocity += CMVelocity;// Vector2.Add(_v, CMVelocity);
            Hits++;
            TotalHits++;

            // Process target ball
            tar._velocity -= CMVelocity;
            tar._velocity = Vector2.Reflect(tar._velocity, targetNorm); // perform bounce
            tar._velocity += CMVelocity;// Vector2.Add(tar._v, CMVelocity);
            tar.Hits++;
            tar.TotalHits++;

            // "Fix" collision if balls overlapped - could apply weighted adjustment shift between both balls
            //       but here we just move the target ball over on collision vector so it doesn't overlap
            //tar._center = Vector2.Add(tar._center, Vector2.Multiply((float)((_iRadius + tar._iRadius - dist.Length()) / (_iRadius + tar._iRadius)), dist));
            tar._center += dist * (float)((Radius + tar.Radius - dist.Length()) / (Radius + tar.Radius));
        }

       /// <summary>
       /// Draw balls to a specifed drawer
       /// </summary>
       /// <param name="drawer"></param>
        public void Show(CDrawer drawer)
        {
            // draw filled ball (diameter = Radius * 2)
            drawer.AddCenteredEllipse( (int)_center.X, (int)_center.Y, Radius * 2, Radius * 2, BallColor);

            // if cue ball, draw yellow border
            if (BallColor == Color.White)
            {
                drawer.AddCenteredEllipse((int)_center.X, (int)_center.Y, Radius * 2, Radius * 2, null,               // no fill
                                                                                                  2,                  // border thickness
                                                                                                  CueBorderColor      // border color
                );
            }

            // create rectangle for centered text
            Rectangle textRect = new Rectangle((int)(_center.X - Radius), (int)(_center.Y - Radius), Radius * 2, Radius * 2);

            // add radius and collision count text in the ball
            drawer.AddText(ToString(), 10, textRect, Color.Black);
        }

        /// <summary>
        /// finding if the t balls collide
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Ball other)
            {
                float distance = Vector2.Distance(_center, other._center);
                return distance < (this.Radius + other.Radius + 1f); // add tiny margin
            }
            return false;
        }

        /// <summary>
        /// override for equals implementation
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => 1;

        /// <summary>
        /// formatted string
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Radius} : {Hits}";

        // SORTING 
        /// <summary>
        /// override default to sort by desc radius
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Ball other)
        {
            return other.Radius.CompareTo(this.Radius); // Descending
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CompareHits(Ball a, Ball b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return b.Hits.CompareTo(a.Hits);
        }

        /// <summary>
        /// Compares two balls based on their total number of collisions (TotalHits).
        /// This is used for sorting a list of balls in descending order of total hits.
        /// </summary>
        /// <param name="a">The first ball to compare</param>
        /// <param name="b">The second ball to compare</param>
        /// <returns>
        /// -1 if a should come before b, 1 if a should come after b, 0 if equal.
        /// </returns>
        public static int CompareTotalHits(Ball a, Ball b)
        {
            if (a == null && b == null) 
                return 0; // equal
            if (a == null) 
                return 1; // a after b
            if (b == null) 
                return -1;// a befpre b
            // compare total hits in desc
            return b.TotalHits.CompareTo(a.TotalHits);
        }
    }
}

