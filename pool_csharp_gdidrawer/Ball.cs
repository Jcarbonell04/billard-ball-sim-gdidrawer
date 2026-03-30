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
        public Ball(CDrawer drawer)
        {
            Radius = 30;
            BallColor = Color.White;
            _center = new Vector2(
                rnd.Next(Radius, drawer.ScaledWidth - Radius),
                rnd.Next(Radius, drawer.ScaledHeight - Radius)
            );
        }

        // Regular ball
        public Ball(CDrawer drawer, Color color)
        {
            BallColor = color;
            Radius = rnd.Next(20, 51);
            _center = new Vector2(
                rnd.Next(Radius, drawer.ScaledWidth - Radius),
                rnd.Next(Radius, drawer.ScaledHeight - Radius)
            );
        }

        // Methods
        public void ResetHits() => Hits = 0;

        public void SetVelocity(Vector2 v) => _velocity = v;

        public void Move(CDrawer drawer, List<Ball> balls)
        {
            // Apply friction
            _velocity *= Friction;

            // Stop if velocity is almost zero
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

            // Move ball
            _center += _velocity;

            // Ball collisions
            foreach (var other in balls)
            {
                if (other == this) continue;

                float dist = Vector2.Distance(_center, other._center);
                float minDist = this.Radius + other.Radius;

                if (dist < minDist)
                {
                    ProcessCollision(other, dist, minDist);
                }
            }
        }

        private void ProcessCollision(Ball other, float distance, float minDistance)
        {
            // direction along which the collision occurs
            Vector2 normal = Vector2.Normalize(other._center - this._center);

            // how fast this ball is moving compared to the other
            Vector2 relativeVelocity = this._velocity - other._velocity;

            // how much of the velocity is directed toward anoter ball
            float velAlongNormal = Vector2.Dot(relativeVelocity, normal);

            // if the balls are moving away from each other, do nothing
            if (velAlongNormal > 0)
            {
                return; // no collision response needed
            }

            // elastic collision or like no energy lost
            float bounceFactor = 1.0f;

            // IMPULSE MAG
            // Divide by 2 because both balls have equal mass and react equally
            float j = -(1 + bounceFactor) * velAlongNormal / 2f;

            // Convert scalar impulse to vector along the collision normal
            Vector2 impulse = j * normal;

            // this ball gets pushed along impulse
            this._velocity += impulse;
            // the other ball gets pushed opposite to impulse
            other._velocity -= impulse;

            // INCREMENT COLLSION COUNTERS

            // Hits: number of collisions this ball has experienced this round
            this.Hits++;
            other.Hits++;
            // TotalHits: number of collisions this ball has experienced across all rounds
            this.TotalHits++;
            other.TotalHits++;

            // INCREASE OVERLAP AND PREVENT BALLS FROM STICKING TO EACHOTHER
            float overlap = minDistance - distance; // how much the balls are overlapping
            if (overlap > 0)
            {
                // Move each ball half the overlap distance along the collision normal
                Vector2 separation = normal * (overlap / 2f);
                this._center -= separation;  // move this ball away
                other._center += separation; // move other ball away
            }
        }

        // Draw ball
        public void Show(CDrawer drawer)
        {
            // Draw filled ball (diameter = Radius * 2)
            drawer.AddCenteredEllipse(
                (int)_center.X,
                (int)_center.Y,
                Radius * 2,
                Radius * 2,
                BallColor
            );

            // If cue ball, draw yellow border
            if (BallColor == Color.White)
            {
                drawer.AddCenteredEllipse(
                    (int)_center.X,
                    (int)_center.Y,
                    Radius * 2,
                    Radius * 2,
                    null,               // no fill
                    2,                  // border thickness
                    CueBorderColor      // border color
                );
            }

            // Create rectangle for centered text
            Rectangle textRect = new Rectangle(
                (int)(_center.X - Radius),
                (int)(_center.Y - Radius),
                Radius * 2,
                Radius * 2
            );

            // Draw "Radius : Hits" in center
            drawer.AddText(ToString(), 10, textRect, Color.Black);
        }

        public override bool Equals(object obj)
        {
            if (obj is Ball other)
            {
                float distance = Vector2.Distance(_center, other._center);
                return distance < (this.Radius + other.Radius + 1f); // add tiny margin
            }
            return false;
        }

        public override int GetHashCode() => 1;

        public override string ToString() => $"{Radius} : {Hits}";

        // Sorting
        public int CompareTo(Ball other) => other.Radius.CompareTo(this.Radius); // Descending
        public static int CompareHits(Ball a, Ball b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return b.Hits.CompareTo(a.Hits);
        }
        public static int CompareTotalHits(Ball a, Ball b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return b.TotalHits.CompareTo(a.TotalHits);
        }
    }
}

