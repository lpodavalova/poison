﻿namespace Poison.Train
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
            this.tb_GeneratingAvgTime = new System.Windows.Forms.TextBox();
            this.lb_GeneratingAvgTime = new System.Windows.Forms.Label();
            this.lb_Step = new System.Windows.Forms.Label();
            this.tb_Step = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_Stat
            // 
            this.tb_Stat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Stat.Location = new System.Drawing.Point(12, 87);
            this.tb_Stat.Multiline = true;
            this.tb_Stat.Name = "tb_Stat";
            this.tb_Stat.ReadOnly = true;
            this.tb_Stat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Stat.Size = new System.Drawing.Size(467, 543);
            this.tb_Stat.TabIndex = 0;
            // 
            // bt_Run
            // 
            this.bt_Run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Run.Location = new System.Drawing.Point(12, 58);
            this.bt_Run.Name = "bt_Run";
            this.bt_Run.Size = new System.Drawing.Size(467, 23);
            this.bt_Run.TabIndex = 1;
            this.bt_Run.Text = "Запуск!";
            this.bt_Run.UseVisualStyleBackColor = true;
            this.bt_Run.Click += new System.EventHandler(this.bt_Run_Click);
            // 
            // tb_GeneratingAvgTime
            // 
            this.tb_GeneratingAvgTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_GeneratingAvgTime.Location = new System.Drawing.Point(313, 6);
            this.tb_GeneratingAvgTime.Name = "tb_GeneratingAvgTime";
            this.tb_GeneratingAvgTime.Size = new System.Drawing.Size(166, 20);
            this.tb_GeneratingAvgTime.TabIndex = 2;
            // 
            // lb_GeneratingAvgTime
            // 
            this.lb_GeneratingAvgTime.AutoSize = true;
            this.lb_GeneratingAvgTime.Location = new System.Drawing.Point(12, 9);
            this.lb_GeneratingAvgTime.Name = "lb_GeneratingAvgTime";
            this.lb_GeneratingAvgTime.Size = new System.Drawing.Size(295, 13);
            this.lb_GeneratingAvgTime.TabIndex = 3;
            this.lb_GeneratingAvgTime.Text = "Начальное значение времени подхода поезда к участку:";
            // 
            // lb_Step
            // 
            this.lb_Step.AutoSize = true;
            this.lb_Step.Location = new System.Drawing.Point(277, 35);
            this.lb_Step.Name = "lb_Step";
            this.lb_Step.Size = new System.Drawing.Size(30, 13);
            this.lb_Step.TabIndex = 4;
            this.lb_Step.Text = "Шаг:";
            // 
            // tb_Step
            // 
            this.tb_Step.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Step.Location = new System.Drawing.Point(313, 32);
            this.tb_Step.Name = "tb_Step";
            this.tb_Step.Size = new System.Drawing.Size(166, 20);
            this.tb_Step.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 642);
            this.Controls.Add(this.tb_Step);
            this.Controls.Add(this.lb_Step);
            this.Controls.Add(this.lb_GeneratingAvgTime);
            this.Controls.Add(this.tb_GeneratingAvgTime);
            this.Controls.Add(this.bt_Run);
            this.Controls.Add(this.tb_Stat);
            this.Name = "MainForm";
            this.Text = "Train Model Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Stat;
        private System.Windows.Forms.Button bt_Run;
        private System.Windows.Forms.TextBox tb_GeneratingAvgTime;
        private System.Windows.Forms.Label lb_GeneratingAvgTime;
        private System.Windows.Forms.Label lb_Step;
        private System.Windows.Forms.TextBox tb_Step;
    }
}

