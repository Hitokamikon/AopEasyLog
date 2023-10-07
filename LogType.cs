using System;

namespace AopEasyLog
{
    /// <summary>
    /// 打印类型
    /// </summary>
    [Flags]
    public enum LogType
    {
        /// <summary>
        /// 控制台输出
        /// </summary>
        Console = 1,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 2,
    }
}
