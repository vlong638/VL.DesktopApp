using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gobang
{
    /// <summary>
    /// 棋子类
    /// </summary>
    public class Piece : PictureBox
    {
        /// <summary>
        /// 颜色属性依赖字段
        /// </summary>
        PColor color;
        /// <summary>
        /// 棋子颜色
        /// </summary>
        public PColor Color
        {
            set
            {
                color = value;
                if (value == PColor.White)
                {
                    this.Image = global::Gobang.Properties.Resources.White;
                }
                else
                {
                    this.Image = global::Gobang.Properties.Resources.Black;
                }
            }
            get
            {
                return color;//返回棋子颜色
            }
        }

        /// <summary>
        /// 棋子坐标
        /// </summary>
        public Point Coordinate { get; set; }

        /// <summary>
        /// 初始化棋子类
        /// </summary>
        public Piece(PColor pColor, System.Drawing.Point point)
        {
            Color = pColor;
            Size = new System.Drawing.Size(33, 33);
            BackColor = System.Drawing.Color.Transparent;
            Location = new Point(point.X * Config.xConst - Config.xOffset, point.Y * Config.yConst - Config.yOffset);
            Coordinate = point;
            SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        }
    }
}
