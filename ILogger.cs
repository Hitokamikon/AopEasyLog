namespace AopEasyLog
{
    /// <summary>
    /// 打印器接口
    /// </summary>
    public interface ILogger
    {
        #region 方法

        /// <summary>
        /// 打印Debug信息
        /// </summary>
        /// <param name="debug">Debug信息</param>
        void Debug(string debug);

        /// <summary>
        /// 打印Info信息
        /// </summary>
        /// <param name="info">Info信息</param>
        void Info(string info);

        /// <summary>
        /// 打印警告信息
        /// </summary>
        /// <param name="warning">警告信息</param>
        void Warn(string warning);

        #endregion
    }
}
