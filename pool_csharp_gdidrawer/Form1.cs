using GDIDrawer;
using System.Numerics;
using Timer = System.Windows.Forms.Timer; // avoid ambiguity 

namespace pool_csharp_gdidrawer
{
    public partial class Form1 : Form
    {
        // PRIVATE MEMBERS
        private Table _table;        // use your Table class
        private Timer UI_Timer = new Timer();

        private int numBalls = 0;
        private bool shotRunning = false;

        public Form1()
        {
            InitializeComponent();

            this.Shown += Form1_Shown;

            // setup timer
            UI_Timer.Interval = 30;
            UI_Timer.Tick += Timer_Tick;
            UI_Timer.Start();

            _table = new Table();
            _table.MakeTable(800, 600, numBalls);  // width, height, number of balls

            UI_NewTable.Click += UI_NewTable_Click;
            UI_NewTable.MouseWheel += UI_NewTable_MouseWheel;
            UI_NewTable.Text = $"New Table ({numBalls})";

            UI_Friction_Lbl.Click += UI_Friction_Lbl_Click;
            UI_Friction_Lbl.MouseWheel += UI_Friction_Lbl_MouseWheel;
            UI_Friction_Lbl.Text = $"{Ball.Friction:F3}";

            UI_Radius_Rdo.CheckedChanged += SortRadioChanged;
            UI_Hits_Rdo.CheckedChanged += SortRadioChanged;
            UI_Total_Hits.CheckedChanged += SortRadioChanged;

            UI_Display_DGV.DataSource = null;
            UI_Display_DGV.RowHeadersVisible = false;
            UpdateGridView();
        }

        /// <summary>
        /// updates the table each frame and shows stats when a shot ends.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_table != null)
                _table.ShowTable();  // Table handles moving balls, aiming line, rendering

            if (shotRunning && !_table.Running)
            {
                shotRunning = false;
                UpdateGridView();

                List<Ball> balls = _table.Balls;

                // update stats
                int ballsHit = balls.Count(b => b.Hits > 0);
                int totalBalls = balls.Count;

                int totalCollisions = balls.Sum(b => b.TotalHits);

                double percent = totalBalls > 0 ? (ballsHit / (float)totalBalls) * 100f : 0f;

                this.Text = $"Hit %: {percent:F1}% | Total Collisions: {totalCollisions}";
            }

            if (_table.Running)
                shotRunning = true;
        }

        /// <summary>
        /// refreshes and sorts the DataGridView with current ball data.
        /// </summary>
        private void UpdateGridView()
        {
            if (_table == null || UI_Display_DGV == null) 
                return;

            List<Ball> ballsCopy = _table.Balls;

            if (UI_Radius_Rdo.Checked)
                ballsCopy.Sort(); // descending Radius
            else if (UI_Hits_Rdo.Checked)
                ballsCopy.Sort(Ball.CompareHits);
            else if (UI_Total_Hits.Checked)
                ballsCopy.Sort(Ball.CompareTotalHits);

            UI_Display_DGV.DataSource = null;
            UI_Display_DGV.DataSource = ballsCopy;

            // hide columns
            UI_Display_DGV.Columns["BallColor"].Visible = false;
            UI_Display_DGV.Columns["Center"].Visible = false;
            UI_Display_DGV.Columns["Velocity"].Visible = false;

            // color Radius column
            foreach (DataGridViewRow row in UI_Display_DGV.Rows)
            {
                Ball b = row.DataBoundItem as Ball;
                if (b != null)
                    row.Cells["Radius"].Style.BackColor = b.BallColor;
            }

            UI_Display_DGV.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// position the canvas beside the form
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Activate();

            if (_table != null && _table.Canvas != null)
                _table.Canvas.Position = new Point(this.Right + 100, this.Top);
        }

        /// <summary>
        /// edit the num of balls with scroll whell
        /// </summary>
        private void UI_NewTable_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) numBalls++;
            else if (numBalls > 1) numBalls--; // minimum 1 ball

            UI_NewTable.Text = $"New Table ({numBalls})";
        }

        /// <summary>
        /// add new table button event handler
        /// </summary>
        private void UI_NewTable_Click(object sender, EventArgs e)
        {
            if (_table != null && _table.Canvas != null)
                _table.Canvas.Close();

            _table = new Table();
            _table.MakeTable(800, 600, numBalls);

            // position canvas beside form
            if (_table.Canvas != null)
                _table.Canvas.Position = new Point(this.Right + 100, this.Top);

            shotRunning = false;

            UpdateGridView();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //private void UI_Friction_Lbl_Click(object sender, EventArgs e)
        //{
        //    // adjust friction here
        //    Ball.Friction = Math.Clamp(Ball.Friction + 0.001f, 0.900f, 1.000f);
        //    UI_Friction_Lbl.Text = $"{Ball.Friction:F3}";
        //}

        /// <summary>
        /// adujst ball friction with scroll
        /// </summary>
        private void UI_Friction_Lbl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) Ball.Friction += 0.001f;
            else Ball.Friction -= 0.001f;

            Ball.Friction = Math.Clamp(Ball.Friction, 0.900f, 1.0f);
            UI_Friction_Lbl.Text = $"{Ball.Friction:F3}";
        }

        /// <summary>
        /// bind radio event to update grid view
        /// </summary>
        private void SortRadioChanged(object sender, EventArgs e)
        {
            UpdateGridView();
        }
    }
}