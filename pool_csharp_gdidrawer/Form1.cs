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

        public Form1()
        {
            InitializeComponent();

            // Setup timer
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();

            // Create drawer
            drawer = new CDrawer(800, 600);

            // Create balls
            MakeBalls();
        }

        private void MakeBalls()
        {
            balls.Clear();

            // Create regular balls (no overlap)
            for (int i = 0; i < 10; i++)
            {
                Ball b;
                do
                {
                    b = new Ball(drawer, Color.Red);
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
            cue.SetVelocity(new Vector2(20, 12));

            balls.Add(cue);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (drawer == null) return;

            drawer.Clear();

            foreach (var b in balls)
            {
                b.Move(drawer, balls);
                b.Show(drawer);
            }

            drawer.Render();
        }
    }
}