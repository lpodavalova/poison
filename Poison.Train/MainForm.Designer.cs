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
            this.bt_Run = new System.Windows.Forms.Button();
            this.tb_GeneratingAvgTimeStart = new System.Windows.Forms.TextBox();
            this.lb_GeneratingAvgTimeStart = new System.Windows.Forms.Label();
            this.lb_Step = new System.Windows.Forms.Label();
            this.tb_Step = new System.Windows.Forms.TextBox();
            this.pb_Chart2 = new System.Windows.Forms.PictureBox();
            this.lb_GeneratingAvgTimeStop = new System.Windows.Forms.Label();
            this.tb_GeneratingAvgTimeStop = new System.Windows.Forms.TextBox();
            this.pb_Chart1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Run
            // 
            this.bt_Run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Run.Location = new System.Drawing.Point(12, 88);
            this.bt_Run.Name = "bt_Run";
            this.bt_Run.Size = new System.Drawing.Size(896, 23);
            this.bt_Run.TabIndex = 1;
            this.bt_Run.Text = "Запуск!";
            this.bt_Run.UseVisualStyleBackColor = true;
            this.bt_Run.Click += new System.EventHandler(this.bt_Run_Click);
            // 
            // tb_GeneratingAvgTimeStart
            // 
            this.tb_GeneratingAvgTimeStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_GeneratingAvgTimeStart.Location = new System.Drawing.Point(269, 6);
            this.tb_GeneratingAvgTimeStart.Name = "tb_GeneratingAvgTimeStart";
            this.tb_GeneratingAvgTimeStart.Size = new System.Drawing.Size(639, 20);
            this.tb_GeneratingAvgTimeStart.TabIndex = 2;
            // 
            // lb_GeneratingAvgTimeStart
            // 
            this.lb_GeneratingAvgTimeStart.AutoSize = true;
            this.lb_GeneratingAvgTimeStart.Location = new System.Drawing.Point(12, 9);
            this.lb_GeneratingAvgTimeStart.Name = "lb_GeneratingAvgTimeStart";
            this.lb_GeneratingAvgTimeStart.Size = new System.Drawing.Size(251, 13);
            this.lb_GeneratingAvgTimeStart.TabIndex = 3;
            this.lb_GeneratingAvgTimeStart.Text = "Начальный средний интервал между поездами:";
            // 
            // lb_Step
            // 
            this.lb_Step.AutoSize = true;
            this.lb_Step.Location = new System.Drawing.Point(192, 61);
            this.lb_Step.Name = "lb_Step";
            this.lb_Step.Size = new System.Drawing.Size(71, 13);
            this.lb_Step.TabIndex = 4;
            this.lb_Step.Text = "Длина шага:";
            // 
            // tb_Step
            // 
            this.tb_Step.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Step.Location = new System.Drawing.Point(269, 58);
            this.tb_Step.Name = "tb_Step";
            this.tb_Step.Size = new System.Drawing.Size(639, 20);
            this.tb_Step.TabIndex = 5;
            // 
            // pb_Chart2
            // 
            this.pb_Chart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Chart2.Location = new System.Drawing.Point(12, 411);
            this.pb_Chart2.Name = "pb_Chart2";
            this.pb_Chart2.Size = new System.Drawing.Size(896, 327);
            this.pb_Chart2.TabIndex = 7;
            this.pb_Chart2.TabStop = false;
            // 
            // lb_GeneratingAvgTimeStop
            // 
            this.lb_GeneratingAvgTimeStop.AutoSize = true;
            this.lb_GeneratingAvgTimeStop.Location = new System.Drawing.Point(19, 35);
            this.lb_GeneratingAvgTimeStop.Name = "lb_GeneratingAvgTimeStop";
            this.lb_GeneratingAvgTimeStop.Size = new System.Drawing.Size(244, 13);
            this.lb_GeneratingAvgTimeStop.TabIndex = 8;
            this.lb_GeneratingAvgTimeStop.Text = "Конечный средний интервал между поездами:";
            // 
            // tb_GeneratingAvgTimeStop
            // 
            this.tb_GeneratingAvgTimeStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_GeneratingAvgTimeStop.Location = new System.Drawing.Point(269, 32);
            this.tb_GeneratingAvgTimeStop.Name = "tb_GeneratingAvgTimeStop";
            this.tb_GeneratingAvgTimeStop.Size = new System.Drawing.Size(639, 20);
            this.tb_GeneratingAvgTimeStop.TabIndex = 9;
            // 
            // pb_Chart1
            // 
            this.pb_Chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Chart1.Location = new System.Drawing.Point(12, 117);
            this.pb_Chart1.Name = "pb_Chart1";
            this.pb_Chart1.Size = new System.Drawing.Size(896, 288);
            this.pb_Chart1.TabIndex = 6;
            this.pb_Chart1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 750);
            this.Controls.Add(this.tb_GeneratingAvgTimeStop);
            this.Controls.Add(this.lb_GeneratingAvgTimeStop);
            this.Controls.Add(this.pb_Chart2);
            this.Controls.Add(this.pb_Chart1);
            this.Controls.Add(this.tb_Step);
            this.Controls.Add(this.lb_Step);
            this.Controls.Add(this.lb_GeneratingAvgTimeStart);
            this.Controls.Add(this.tb_GeneratingAvgTimeStart);
            this.Controls.Add(this.bt_Run);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Пример модели: эффективность движения поездов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Run;
        private System.Windows.Forms.TextBox tb_GeneratingAvgTimeStart;
        private System.Windows.Forms.Label lb_GeneratingAvgTimeStart;
        private System.Windows.Forms.Label lb_Step;
        private System.Windows.Forms.TextBox tb_Step;
        private System.Windows.Forms.PictureBox pb_Chart2;
        private System.Windows.Forms.Label lb_GeneratingAvgTimeStop;
        private System.Windows.Forms.TextBox tb_GeneratingAvgTimeStop;
        private System.Windows.Forms.PictureBox pb_Chart1;
    }
}

