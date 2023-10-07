using System;

namespace AopEasyLog
{
    /// <summary>
    /// 记录
    /// </summary>
    public abstract class Record
    {
        #region 属性

        /// <summary>
        /// 记录的时间
        /// </summary>
        public DateTime DateTime { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Record()
        {
            DateTime = DateTime.Now;
        }

        #endregion
    }
}
