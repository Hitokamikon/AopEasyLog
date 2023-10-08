namespace AopEasyLog
{
    /// <summary>
    /// Info记录
    /// </summary>
    public class InfoRecord : Record
    {
        #region 属性

        /// <summary>
        /// Info
        /// </summary>
        public string Info { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="info">Info</param>
        public InfoRecord(string info)
        {
            Info = info;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重写转字符串方法
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => $"[{DateTime:yyyy.MM.dd HH:mm:ss fff}] {Info}";

        #endregion
    }
}
