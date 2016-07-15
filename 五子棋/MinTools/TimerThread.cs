using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinTools
{
    /// <summary>
    /// 时钟线程
    /// </summary>
    public class TimerThread : System.Timers.Timer
    {
        /// <summary>
        /// 满足执行条件时执行
        /// </summary>
        public event EventHandler<System.Timers.ElapsedEventArgs> Elapseds;

        /// <summary>
        /// 时钟执行类型枚举
        /// </summary>
        public enum AutoResetTypeEnum
        {
            /// <summary>
            /// 以时间间隔执行
            /// </summary>
            TimeInterval,
            /// <summary>
            /// 执行后间隔指定时间再执行
            /// </summary>
            Linear,
            /// <summary>
            /// 执行一次
            /// </summary>
            Once
        }

        /// <summary>
        /// 时钟执行类型
        /// </summary>
        private AutoResetTypeEnum AutoResetType { get; set; }

        /// <summary>
        /// 线程开关
        /// </summary>
        public bool Switch { get; set; }

        /// <summary>
        /// 初始化时钟线程(线程)
        /// </summary>
        public TimerThread()
        {
            this.Switch = false;
            this.AutoReset = false;
            this.Elapsed += TimerThread_Elapsed;
        }

        /// <summary>
        /// 初始化时钟线程(时钟)
        /// </summary>
        /// <param name="interval">间隔时间</param>
        /// <param name="autoResetType">时钟执行类型</param>
        public TimerThread(int interval, AutoResetTypeEnum autoResetType)
        {
            this.AutoResetType = autoResetType;
            if (this.AutoResetType == AutoResetTypeEnum.TimeInterval)
            {
                this.AutoReset = true;
            }
            else
            {
                this.AutoReset = false;
            }
            this.Interval = interval;
            this.Switch = false;
            this.Elapsed += TimerThread_Elapsed;
        }

        /// <summary>
        /// 初始化时钟线程(时钟,默认间隔时间为1秒)
        /// </summary>
        /// <param name="autoResetType">时钟执行类型</param>
        public TimerThread(AutoResetTypeEnum autoResetType)
        {
            this.AutoResetType = autoResetType;
            if (this.AutoResetType == AutoResetTypeEnum.TimeInterval)
            {
                this.AutoReset = true;
            }
            else
            {
                this.AutoReset = false;
            }
            this.Interval = 1000;
            this.Switch = false;
            this.Elapsed += TimerThread_Elapsed;
        }

        /// <summary>
        /// 时钟执行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerThread_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Elapseds != null)
                Elapseds(sender, e);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            if (this.Switch == false) return;
            if (AutoResetType == AutoResetTypeEnum.Linear)
            {
                System.Threading.Thread.Sleep((int)this.Interval);
                if (this.Switch == false) return;
                this.Start();
            }
        }

        /// <summary>
        /// 开启时钟
        /// </summary>
        public new void Start()
        {
            this.Enabled = this.Switch = true;
        }

        /// <summary>
        /// 开启时钟
        /// </summary>
        /// <param name="interval">间隔时间</param>
        public void Start(int interval)
        {
            this.Interval = interval;
            if (this.Switch == false)
            {
                this.Enabled = this.Switch = true;
            }
        }

        /// <summary>
        /// 关闭时钟(执行完一次后关闭)
        /// </summary>
        public new void Stop()
        {
            this.Enabled = this.Switch = false;
        }
    }
}
