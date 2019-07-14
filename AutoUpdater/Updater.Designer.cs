namespace AutoUpdater
{
    partial class AutoUpdate
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
            this.tips.Location = new System.Drawing.Point(474, 568);
            this.tips.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(897, 78);
            this.tips.TabIndex = 9;
            this.tips.Text = "系统正在连接服务器，请稍后......";
            this.tips.TextChanged += new System.EventHandler(this.label6_TextChanged);
            // 
            // total_process
            // 
            this.total_process.Location = new System.Drawing.Point(462, 699);
            this.total_process.Margin = new System.Windows.Forms.Padding(4);
            this.total_process.MarqueeAnimationSpeed = 50;
            this.total_process.Name = "total_process";
            this.total_process.Size = new System.Drawing.Size(992, 52);
            this.total_process.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.total_process.TabIndex = 8;
            // 
            // current_process
            // 
            this.current_process.Location = new System.Drawing.Point(462, 794);
            this.current_process.Margin = new System.Windows.Forms.Padding(4);
            this.current_process.Name = "current_process";
            this.current_process.Size = new System.Drawing.Size(992, 52);
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
            this.label_now_download.Location = new System.Drawing.Point(881, 887);
            this.label_now_download.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_now_download.Name = "label_now_download";
            this.label_now_download.Size = new System.Drawing.Size(44, 31);
            this.label_now_download.TabIndex = 11;
            this.label_now_download.Text = ".....";
            this.label_now_download.TextChanged += new System.EventHandler(this.label_now_download_TextChanged);
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1106);
            this.Controls.Add(this.label_now_download);
            this.Controls.Add(this.current_process);
            this.Controls.Add(this.tips);
            this.Controls.Add(this.total_process);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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