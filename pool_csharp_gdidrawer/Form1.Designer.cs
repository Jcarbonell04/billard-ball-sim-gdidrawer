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
            SuspendLayout();
            // 
            // UI_NewTable
            // 
            UI_NewTable.Location = new Point(12, 12);
            UI_NewTable.Name = "UI_NewTable";
            UI_NewTable.Size = new Size(117, 34);
            UI_NewTable.TabIndex = 0;
            UI_NewTable.Text = "New Table [10]";
            UI_NewTable.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(UI_NewTable);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button UI_NewTable;
    }
}
