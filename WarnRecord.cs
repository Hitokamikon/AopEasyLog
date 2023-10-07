namespace AopEasyLog
{
    /// <summary>
    /// 警告记录
    /// </summary>
    public class WarnRecord : Record
    {
        #region 属性

        /// <summary>
        /// 警告
        /// </summary>
        public string Warn { get; private set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 警告记录
        /// </summary>
        /// <param name="warn">警告</param>
        public WarnRecord(string warn)
        {
            Warn = warn;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重写转字符串方法
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => $"[{DateTime:yyyy.MM.dd HH:mm:ss fff}] {Warn}";

        #endregion
    }
}
