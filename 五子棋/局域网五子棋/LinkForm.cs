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
    public partial class LinkForm : Form
    {
        public LinkForm()
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
                Config.ip = IPAddress.Parse(tbIp.Text);
                if (Config.ip.ToString() == Config.LocalIP.ToString())
                {
                    MessageBox.Show("本机请使用双人对仗！");
                    return;
                }
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
