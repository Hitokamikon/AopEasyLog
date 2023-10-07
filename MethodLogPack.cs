using System.Collections.Generic;

namespace AopEasyLog
{
    /// <summary>
    /// 方法Log打印包
    /// </summary>
    public class MethodLogPack
    {
        #region 属性

        /// <summary>
        /// Debug列表
        /// </summary>
        public List<string> Debugs { get; private set; } = new List<string>();

        /// <summary>
        /// Info列表
        /// </summary>
        public List<string> Infos { get; private set; } = new List<string>();

        /// <summary>
        /// Warn列表
        /// </summary>
        public List<string> Warns { get; private set; } = new List<string>();

        #endregion

        #region 方法

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="debug">Debug内容</param>
        public void Debug(string debug)
        {
            Debugs.Add(debug);
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="info">Info内容</param>
        public void Info(string info)
        {
            Infos.Add(info);
        }

        #endregion
    }
}
