namespace ProfilerViewer
{
    partial class Viewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TXT_Profiler = new System.Windows.Forms.TextBox();
            this.CHK_Pause = new System.Windows.Forms.CheckBox();
            this.CHK_Grids = new System.Windows.Forms.CheckBox();
            this.CHK_Blocks = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TXT_Profiler
            // 
            this.TXT_Profiler.Location = new System.Drawing.Point(13, 12);
            this.TXT_Profiler.Multiline = true;
            this.TXT_Profiler.Name = "TXT_Profiler";
            this.TXT_Profiler.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXT_Profiler.Size = new System.Drawing.Size(522, 359);
            this.TXT_Profiler.TabIndex = 0;
            // 
            // CHK_Pause
            // 
            this.CHK_Pause.AutoSize = true;
            this.CHK_Pause.Location = new System.Drawing.Point(12, 377);
            this.CHK_Pause.Name = "CHK_Pause";
            this.CHK_Pause.Size = new System.Drawing.Size(56, 17);
            this.CHK_Pause.TabIndex = 1;
            this.CHK_Pause.Text = "Pause";
            this.CHK_Pause.UseVisualStyleBackColor = true;
            this.CHK_Pause.CheckedChanged += new System.EventHandler(this.CHK_Pause_CheckedChanged);
            // 
            // CHK_Grids
            // 
            this.CHK_Grids.AutoSize = true;
            this.CHK_Grids.Location = new System.Drawing.Point(74, 377);
            this.CHK_Grids.Name = "CHK_Grids";
            this.CHK_Grids.Size = new System.Drawing.Size(82, 17);
            this.CHK_Grids.TabIndex = 2;
            this.CHK_Grids.Text = "Profile Grids";
            this.CHK_Grids.UseVisualStyleBackColor = true;
            this.CHK_Grids.CheckedChanged += new System.EventHandler(this.CHK_Grids_CheckedChanged);
            // 
            // CHK_Blocks
            // 
            this.CHK_Blocks.AutoSize = true;
            this.CHK_Blocks.Location = new System.Drawing.Point(162, 377);
            this.CHK_Blocks.Name = "CHK_Blocks";
            this.CHK_Blocks.Size = new System.Drawing.Size(90, 17);
            this.CHK_Blocks.TabIndex = 3;
            this.CHK_Blocks.Text = "Profile Blocks";
            this.CHK_Blocks.UseVisualStyleBackColor = true;
            this.CHK_Blocks.CheckedChanged += new System.EventHandler(this.CHK_Blocks_CheckedChanged);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 406);
            this.Controls.Add(this.CHK_Blocks);
            this.Controls.Add(this.CHK_Grids);
            this.Controls.Add(this.CHK_Pause);
            this.Controls.Add(this.TXT_Profiler);
            this.Name = "Viewer";
            this.Text = "ProfilerViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_Profiler;
        private System.Windows.Forms.CheckBox CHK_Pause;
        private System.Windows.Forms.CheckBox CHK_Grids;
        private System.Windows.Forms.CheckBox CHK_Blocks;
    }
}