namespace AutoUpdater
{
    partial class Updater
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
            this.tips = new System.Windows.Forms.Label();
            this.total_process = new System.Windows.Forms.ProgressBar();
            this.current_process = new System.Windows.Forms.ProgressBar();
            this.label_now_download = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tips
            // 
            this.tips.AutoSize = true;
            this.tips.BackColor = System.Drawing.Color.Transparent;
            this.tips.Font = new System.Drawing.Font("微软雅黑", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tips.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tips.Location = new System.Drawing.Point(316, 379);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(603, 52);
            this.tips.TabIndex = 9;
            this.tips.Text = "系统正在连接服务器，请稍后......";
            this.tips.SizeChanged += new System.EventHandler(this.tips_SizeChanged);
            this.tips.Click += new System.EventHandler(this.tips_Click);
            // 
            // total_process
            // 
            this.total_process.Location = new System.Drawing.Point(308, 466);
            this.total_process.MarqueeAnimationSpeed = 50;
            this.total_process.Name = "total_process";
            this.total_process.Size = new System.Drawing.Size(661, 35);
            this.total_process.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.total_process.TabIndex = 8;
            // 
            // current_process
            // 
            this.current_process.Location = new System.Drawing.Point(308, 529);
            this.current_process.Name = "current_process";
            this.current_process.Size = new System.Drawing.Size(661, 35);
            this.current_process.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.current_process.TabIndex = 10;
            this.current_process.Visible = false;
            // 
            // label_now_download
            // 
            this.label_now_download.AutoSize = true;
            this.label_now_download.BackColor = System.Drawing.Color.Transparent;
            this.label_now_download.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_now_download.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label_now_download.Location = new System.Drawing.Point(587, 591);
            this.label_now_download.Name = "label_now_download";
            this.label_now_download.Size = new System.Drawing.Size(30, 21);
            this.label_now_download.TabIndex = 11;
            this.label_now_download.Text = ".....";
            this.label_now_download.Visible = false;
            this.label_now_download.TextChanged += new System.EventHandler(this.label_now_download_TextChanged);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 733);
            this.Controls.Add(this.label_now_download);
            this.Controls.Add(this.current_process);
            this.Controls.Add(this.tips);
            this.Controls.Add(this.total_process);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Updater_Load);
            this.Shown += new System.EventHandler(this.AutoUpdate_Shown);
            this.SizeChanged += new System.EventHandler(this.AutoUpdate_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tips;
        private System.Windows.Forms.ProgressBar total_process;
        private System.Windows.Forms.ProgressBar current_process;
        private System.Windows.Forms.Label label_now_download;
    }
}