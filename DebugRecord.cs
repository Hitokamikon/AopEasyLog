namespace AopEasyLog
{
    /// <summary>
    /// Debug记录
    /// </summary>
    public class DebugRecord : Record
    {
        #region 属性

        /// <summary>
        /// Debug
        /// </summary>
        public string Debug { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="debug">Debug</param>
        public DebugRecord(string debug)
        {
            Debug = debug;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重写转字符串方法
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => $"[{DateTime:yyyy.MM.dd HH:mm:ss fff}] {Debug}";

        #endregion
    }
}
