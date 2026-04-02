namespace pool_csharp_gdidrawer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UI_NewTable = new Button();
            UI_Friction_Lbl = new Label();
            UI_SortMode_Gbx = new GroupBox();
            UI_Total_Hits = new RadioButton();
            UI_Hits_Rdo = new RadioButton();
            UI_Radius_Rdo = new RadioButton();
            UI_Display_DGV = new DataGridView();
            label1 = new Label();
            UI_SortMode_Gbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UI_Display_DGV).BeginInit();
            SuspendLayout();
            // 
            // UI_NewTable
            // 
            UI_NewTable.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UI_NewTable.Location = new Point(13, 11);
            UI_NewTable.Margin = new Padding(2);
            UI_NewTable.Name = "UI_NewTable";
            UI_NewTable.Size = new Size(160, 43);
            UI_NewTable.TabIndex = 0;
            UI_NewTable.Text = "New Table [10]";
            UI_NewTable.UseVisualStyleBackColor = true;
            // 
            // UI_Friction_Lbl
            // 
            UI_Friction_Lbl.AutoSize = true;
            UI_Friction_Lbl.BackColor = SystemColors.ActiveCaption;
            UI_Friction_Lbl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UI_Friction_Lbl.Location = new Point(318, 25);
            UI_Friction_Lbl.Margin = new Padding(2, 0, 2, 0);
            UI_Friction_Lbl.Name = "UI_Friction_Lbl";
            UI_Friction_Lbl.Size = new Size(50, 21);
            UI_Friction_Lbl.TabIndex = 1;
            UI_Friction_Lbl.Text = "0.991";
            // 
            // UI_SortMode_Gbx
            // 
            UI_SortMode_Gbx.Controls.Add(UI_Total_Hits);
            UI_SortMode_Gbx.Controls.Add(UI_Hits_Rdo);
            UI_SortMode_Gbx.Controls.Add(UI_Radius_Rdo);
            UI_SortMode_Gbx.Location = new Point(11, 72);
            UI_SortMode_Gbx.Margin = new Padding(2);
            UI_SortMode_Gbx.Name = "UI_SortMode_Gbx";
            UI_SortMode_Gbx.Padding = new Padding(2);
            UI_SortMode_Gbx.Size = new Size(357, 42);
            UI_SortMode_Gbx.TabIndex = 2;
            UI_SortMode_Gbx.TabStop = false;
            UI_SortMode_Gbx.Text = "Sort Mode: ";
            // 
            // UI_Total_Hits
            // 
            UI_Total_Hits.AutoSize = true;
            UI_Total_Hits.Location = new Point(282, 19);
            UI_Total_Hits.Margin = new Padding(2);
            UI_Total_Hits.Name = "UI_Total_Hits";
            UI_Total_Hits.Size = new Size(78, 19);
            UI_Total_Hits.TabIndex = 3;
            UI_Total_Hits.TabStop = true;
            UI_Total_Hits.Text = "Total Hits ";
            UI_Total_Hits.UseVisualStyleBackColor = true;
            // 
            // UI_Hits_Rdo
            // 
            UI_Hits_Rdo.AutoSize = true;
            UI_Hits_Rdo.Location = new Point(143, 19);
            UI_Hits_Rdo.Margin = new Padding(2);
            UI_Hits_Rdo.Name = "UI_Hits_Rdo";
            UI_Hits_Rdo.Size = new Size(46, 19);
            UI_Hits_Rdo.TabIndex = 1;
            UI_Hits_Rdo.TabStop = true;
            UI_Hits_Rdo.Text = "Hits";
            UI_Hits_Rdo.UseVisualStyleBackColor = true;
            // 
            // UI_Radius_Rdo
            // 
            UI_Radius_Rdo.AutoSize = true;
            UI_Radius_Rdo.Location = new Point(2, 16);
            UI_Radius_Rdo.Margin = new Padding(2);
            UI_Radius_Rdo.Name = "UI_Radius_Rdo";
            UI_Radius_Rdo.Size = new Size(60, 19);
            UI_Radius_Rdo.TabIndex = 0;
            UI_Radius_Rdo.TabStop = true;
            UI_Radius_Rdo.Text = "Radius";
            UI_Radius_Rdo.UseVisualStyleBackColor = true;
            // 
            // UI_Display_DGV
            // 
            UI_Display_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UI_Display_DGV.Location = new Point(10, 118);
            UI_Display_DGV.Margin = new Padding(2);
            UI_Display_DGV.Name = "UI_Display_DGV";
            UI_Display_DGV.RowHeadersVisible = false;
            UI_Display_DGV.RowHeadersWidth = 62;
            UI_Display_DGV.Size = new Size(360, 361);
            UI_Display_DGV.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(234, 25);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 21);
            label1.TabIndex = 4;
            label1.Text = "Friction : ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 490);
            Controls.Add(label1);
            Controls.Add(UI_Display_DGV);
            Controls.Add(UI_SortMode_Gbx);
            Controls.Add(UI_Friction_Lbl);
            Controls.Add(UI_NewTable);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Shown += Form1_Shown;
            UI_SortMode_Gbx.ResumeLayout(false);
            UI_SortMode_Gbx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)UI_Display_DGV).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button UI_NewTable;
        private Label UI_Friction_Lbl;
        private GroupBox UI_SortMode_Gbx;
        private RadioButton UI_Total_Hits;
        private RadioButton UI_Hits_Rdo;
        private RadioButton UI_Radius_Rdo;
        private DataGridView UI_Display_DGV;
        private Label label1;
    }
}
