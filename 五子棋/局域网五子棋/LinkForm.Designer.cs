namespace Gobang
{
    partial class LinkForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIp = new System.Windows.Forms.TextBox();
            this.btnInvitation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对战方IP：";
            // 
            // tbIp
            // 
            this.tbIp.Location = new System.Drawing.Point(83, 21);
            this.tbIp.Name = "tbIp";
            this.tbIp.Size = new System.Drawing.Size(143, 21);
            this.tbIp.TabIndex = 2;
            this.tbIp.Text = "192.168.112.";
            this.tbIp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIp_KeyPress);
            // 
            // btnInvitation
            // 
            this.btnInvitation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInvitation.Location = new System.Drawing.Point(72, 54);
            this.btnInvitation.Name = "btnInvitation";
            this.btnInvitation.Size = new System.Drawing.Size(105, 37);
            this.btnInvitation.TabIndex = 3;
            this.btnInvitation.Text = "邀请对仗";
            this.btnInvitation.UseVisualStyleBackColor = true;
            this.btnInvitation.Click += new System.EventHandler(this.btnInvitation_Click);
            // 
            // LinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 102);
            this.Controls.Add(this.btnInvitation);
            this.Controls.Add(this.tbIp);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkForm";
            this.Text = "连接对战方";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIp;
        private System.Windows.Forms.Button btnInvitation;
    }
}