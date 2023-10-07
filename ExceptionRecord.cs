using System;
using System.Diagnostics;

namespace AopEasyLog
{
    /// <summary>
    /// 异常记录
    /// </summary>
    public class ExceptionRecord : Record
    {
        #region 属性

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">异常</param>
        public ExceptionRecord(Exception exception)
        {
            Exception = exception;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重写转字符串方法
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => $"[{DateTime:yyyy.MM.dd HH:mm:ss fff}] {Exception.Message} + {Exception.StackTrace}";

        #endregion
    }
}
