using GDIDrawer;
using System.Numerics;
using Timer = System.Windows.Forms.Timer; // avoid ambiguity 

namespace pool_csharp_gdidrawer
{
    public partial class Form1 : Form
    {
        private List<Ball> balls = new List<Ball>();
        private CDrawer drawer;
        private Timer timer = new Timer();

        private int regBalls = 10;

        public Form1()
        {
            InitializeComponent();

            // Setup timer
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Create drawer
            drawer = new CDrawer(800, 600);
            lastPos = drawer.Position;


            // Create balls
            MakeBalls();
        }

        private void MakeBalls()
        {
            balls.Clear();

            // Create regular balls (no overlap)
            for (int i = 0; i < regBalls; i++)
            {
                Ball b;
                do
                {
                    b = new Ball(drawer, RandColor.GetColor());
                }
                while (balls.Exists(existing => existing.Equals(b)));

                balls.Add(b);
            }

            // Create cue ball
            Ball cue;
            do
            {
                cue = new Ball(drawer);
            }
            while (balls.Exists(existing => existing.Equals(cue)));

            // Give cue ball velocity
            //cue.SetVelocity(new Vector2(5, 3));
            cue.SetVelocity(new Vector2(20, 12)); // increased to ensure collison

            balls.Add(cue);
        }


        private Point lastPos;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (drawer == null) return;

            int scaleDown = 20;
            Point currentPos = drawer.Position;
            Point diff = new Point((currentPos.X - lastPos.X) / scaleDown, (currentPos.Y - lastPos.Y) / scaleDown);
            lastPos = currentPos;

            drawer.Clear();


            foreach (var b in balls)
            {
                b.Move(drawer, balls);
                b.Show(drawer);
                b.SetVelocity(new Vector2(b.Velocity.X + diff.X, b.Velocity.Y + diff.Y));
            }

            drawer.Render();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Activate();
            drawer.Position = new Point(this.Right + 100, this.Top);
        }
    }
}