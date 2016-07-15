using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Gobang
{
    public partial class LocalIPForm : Form
    {
        public LocalIPForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 邀请对仗按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvitation_Click(object sender, EventArgs e)
        {
            try
            {
                Config.LocalIP = IPAddress.Parse(tbIp.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("请输入正确的IP地址");
            }
        }

        /// <summary>
        /// 输入框按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbIp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnInvitation_Click(this, new EventArgs());
            }
        }
    }
}
