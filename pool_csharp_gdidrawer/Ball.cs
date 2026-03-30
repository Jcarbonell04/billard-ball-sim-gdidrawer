using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics; // For Vector2
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

            // Stop if almost zero
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

            // Move
            _center += _velocity;

            // Ball collisions
            foreach (var other in balls)
            {
                if (other == this) continue;
                if (this.Equals(other))
                {
                    ProcessCollision(other);
                }
            }
        }

        // Simple collision response
        private void ProcessCollision(Ball other)
        {
            Vector2 normal = Vector2.Normalize(other._center - this._center);
            Vector2 relativeVelocity = this._velocity - other._velocity;
            float velAlongNormal = Vector2.Dot(relativeVelocity, normal);

            if (velAlongNormal > 0) return;

            float restitution = 1.0f; // Perfectly elastic
            float j = -(1 + restitution) * velAlongNormal / 2; // divide by 2 for equal mass

            Vector2 impulse = j * normal;
            this._velocity += impulse;
            other._velocity -= impulse;

            this.Hits++;
            this.TotalHits++;
            other.TotalHits++;
        }

        // Draw ball
        public void Show(CDrawer drawer)
        {
            // Draw main circle
            drawer.AddEllipse(_center, Radius, BallColor);

            // Cue ball highlight
            if (BallColor == Color.White)
            {
                drawer.AddEllipse(_center, Radius, Color.Transparent, 2, CueBorderColor);
            }

            // Draw text
            drawer.AddCenteredText(_center, ToString(), Color.Black);
        }

        // Overrides
        public override bool Equals(object obj)
        {
            if (obj is Ball other)
            {
                float distance = Vector2.Distance(_center, other._center);
                return distance < (this.Radius + other.Radius);
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

