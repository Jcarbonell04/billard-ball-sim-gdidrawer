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
            UI_Radius_Rdo = new RadioButton();
            UI_Hits_Rdo = new RadioButton();
            UI_Total_Hits = new RadioButton();
            UI_Display_DGV = new DataGridView();
            UI_SortMode_Gbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)UI_Display_DGV).BeginInit();
            SuspendLayout();
            // 
            // UI_NewTable
            // 
            UI_NewTable.Location = new Point(12, 12);
            UI_NewTable.Name = "UI_NewTable";
            UI_NewTable.Size = new Size(144, 34);
            UI_NewTable.TabIndex = 0;
            UI_NewTable.Text = "New Table [10]";
            UI_NewTable.UseVisualStyleBackColor = true;
            // 
            // UI_Friction_Lbl
            // 
            UI_Friction_Lbl.AutoSize = true;
            UI_Friction_Lbl.Location = new Point(383, 12);
            UI_Friction_Lbl.Name = "UI_Friction_Lbl";
            UI_Friction_Lbl.Size = new Size(128, 25);
            UI_Friction_Lbl.TabIndex = 1;
            UI_Friction_Lbl.Text = "Friction : 0.991";
            // 
            // UI_SortMode_Gbx
            // 
            UI_SortMode_Gbx.Controls.Add(UI_Total_Hits);
            UI_SortMode_Gbx.Controls.Add(UI_Hits_Rdo);
            UI_SortMode_Gbx.Controls.Add(UI_Radius_Rdo);
            UI_SortMode_Gbx.Location = new Point(12, 57);
            UI_SortMode_Gbx.Name = "UI_SortMode_Gbx";
            UI_SortMode_Gbx.Size = new Size(517, 66);
            UI_SortMode_Gbx.TabIndex = 2;
            UI_SortMode_Gbx.TabStop = false;
            UI_SortMode_Gbx.Text = "Sort Mode: ";
            // 
            // UI_Radius_Rdo
            // 
            UI_Radius_Rdo.AutoSize = true;
            UI_Radius_Rdo.Location = new Point(3, 27);
            UI_Radius_Rdo.Name = "UI_Radius_Rdo";
            UI_Radius_Rdo.Size = new Size(90, 29);
            UI_Radius_Rdo.TabIndex = 0;
            UI_Radius_Rdo.TabStop = true;
            UI_Radius_Rdo.Text = "Radius";
            UI_Radius_Rdo.UseVisualStyleBackColor = true;
            // 
            // UI_Hits_Rdo
            // 
            UI_Hits_Rdo.AutoSize = true;
            UI_Hits_Rdo.Location = new Point(204, 31);
            UI_Hits_Rdo.Name = "UI_Hits_Rdo";
            UI_Hits_Rdo.Size = new Size(68, 29);
            UI_Hits_Rdo.TabIndex = 1;
            UI_Hits_Rdo.TabStop = true;
            UI_Hits_Rdo.Text = "Hits";
            UI_Hits_Rdo.UseVisualStyleBackColor = true;
            // 
            // UI_Total_Hits
            // 
            UI_Total_Hits.AutoSize = true;
            UI_Total_Hits.Location = new Point(403, 31);
            UI_Total_Hits.Name = "UI_Total_Hits";
            UI_Total_Hits.Size = new Size(115, 29);
            UI_Total_Hits.TabIndex = 3;
            UI_Total_Hits.TabStop = true;
            UI_Total_Hits.Text = "Total Hits ";
            UI_Total_Hits.UseVisualStyleBackColor = true;
            // 
            // UI_Display_DGV
            // 
            UI_Display_DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UI_Display_DGV.Location = new Point(15, 129);
            UI_Display_DGV.Name = "UI_Display_DGV";
            UI_Display_DGV.RowHeadersVisible = false;
            UI_Display_DGV.RowHeadersWidth = 62;
            UI_Display_DGV.Size = new Size(515, 412);
            UI_Display_DGV.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(541, 565);
            Controls.Add(UI_Display_DGV);
            Controls.Add(UI_SortMode_Gbx);
            Controls.Add(UI_Friction_Lbl);
            Controls.Add(UI_NewTable);
            Name = "Form1";
            Text = "Form1";
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
    }
}
