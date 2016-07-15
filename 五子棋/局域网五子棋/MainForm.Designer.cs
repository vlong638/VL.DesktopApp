namespace Gobang
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnDouble = new System.Windows.Forms.Button();
            this.btnNetwork = new System.Windows.Forms.Button();
            this.lbTheHand = new System.Windows.Forms.Label();
            this.panCheckerboard = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnDouble
            // 
            this.btnDouble.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDouble.Location = new System.Drawing.Point(653, 590);
            this.btnDouble.Name = "btnDouble";
            this.btnDouble.Size = new System.Drawing.Size(126, 53);
            this.btnDouble.TabIndex = 1;
            this.btnDouble.Text = "双人对仗";
            this.btnDouble.UseVisualStyleBackColor = true;
            this.btnDouble.Click += new System.EventHandler(this.btnDouble_Click);
            // 
            // btnNetwork
            // 
            this.btnNetwork.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNetwork.Location = new System.Drawing.Point(878, 590);
            this.btnNetwork.Name = "btnNetwork";
            this.btnNetwork.Size = new System.Drawing.Size(126, 53);
            this.btnNetwork.TabIndex = 2;
            this.btnNetwork.Text = "联网对仗";
            this.btnNetwork.UseVisualStyleBackColor = true;
            this.btnNetwork.Click += new System.EventHandler(this.btnNetwork_Click);
            // 
            // lbTheHand
            // 
            this.lbTheHand.AutoSize = true;
            this.lbTheHand.BackColor = System.Drawing.Color.Transparent;
            this.lbTheHand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTheHand.ForeColor = System.Drawing.Color.Blue;
            this.lbTheHand.Location = new System.Drawing.Point(668, 12);
            this.lbTheHand.Name = "lbTheHand";
            this.lbTheHand.Size = new System.Drawing.Size(0, 16);
            this.lbTheHand.TabIndex = 3;
            this.lbTheHand.Visible = false;
            // 
            // panCheckerboard
            // 
            this.panCheckerboard.BackColor = System.Drawing.Color.Transparent;
            this.panCheckerboard.Location = new System.Drawing.Point(26, 21);
            this.panCheckerboard.Name = "panCheckerboard";
            this.panCheckerboard.Size = new System.Drawing.Size(610, 610);
            this.panCheckerboard.TabIndex = 4;
            this.panCheckerboard.Click += new System.EventHandler(this.panel1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1016, 655);
            this.Controls.Add(this.panCheckerboard);
            this.Controls.Add(this.lbTheHand);
            this.Controls.Add(this.btnNetwork);
            this.Controls.Add(this.btnDouble);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(1032, 693);
            this.MinimumSize = new System.Drawing.Size(1032, 693);
            this.Name = "MainForm";
            this.Text = "局域网五子棋";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDouble;
        private System.Windows.Forms.Button btnNetwork;
        private System.Windows.Forms.Label lbTheHand;
        private System.Windows.Forms.Panel panCheckerboard;

    }
}

