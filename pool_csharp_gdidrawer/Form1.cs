using GDIDrawer;
using System.Numerics;
using Timer = System.Windows.Forms.Timer; // avoid ambiguity 

namespace pool_csharp_gdidrawer
{
    public partial class Form1 : Form
    {
        private Table _table;        // use your Table class
        private Timer UI_Timer = new Timer();

        public Form1()
        {
            InitializeComponent();

            // setup timer
            UI_Timer.Interval = 30;
            UI_Timer.Tick += Timer_Tick;
            UI_Timer.Start();

            _table = new Table();
            _table.MakeTable(800, 600, 10);  // width, height, number of balls
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_table != null)
                _table.ShowTable();  // Table handles moving balls, aiming line, rendering
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Activate();

            if (_table != null && _table.Canvas != null)
                _table.Canvas.Position = new Point(this.Right + 100, this.Top);
        }
    }
}