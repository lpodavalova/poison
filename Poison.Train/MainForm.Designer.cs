namespace Poison.Train
{
    partial class MainForm
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
            this.tb_Stat = new System.Windows.Forms.TextBox();
            this.bt_Run = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_Stat
            // 
            this.tb_Stat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Stat.Location = new System.Drawing.Point(12, 41);
            this.tb_Stat.Multiline = true;
            this.tb_Stat.Name = "tb_Stat";
            this.tb_Stat.ReadOnly = true;
            this.tb_Stat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Stat.Size = new System.Drawing.Size(467, 589);
            this.tb_Stat.TabIndex = 0;
            // 
            // bt_Run
            // 
            this.bt_Run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Run.Location = new System.Drawing.Point(12, 12);
            this.bt_Run.Name = "bt_Run";
            this.bt_Run.Size = new System.Drawing.Size(467, 23);
            this.bt_Run.TabIndex = 1;
            this.bt_Run.Text = "Run";
            this.bt_Run.UseVisualStyleBackColor = true;
            this.bt_Run.Click += new System.EventHandler(this.bt_Run_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 642);
            this.Controls.Add(this.bt_Run);
            this.Controls.Add(this.tb_Stat);
            this.Name = "MainForm";
            this.Text = "Train Model Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Stat;
        private System.Windows.Forms.Button bt_Run;
    }
}

