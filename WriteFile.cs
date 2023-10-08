using System.IO;

namespace AopEasyLog
{
    /// <summary>
    /// 写文件
    /// </summary>
    internal class WriteFile : Operation
    {
        #region 属性

        /// <summary>
        /// 文件路径
        /// </summary>
        internal string Path { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        internal string Content { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">内容</param>
        internal WriteFile(string path, string content)
        {
            Path = path;
            Content = content;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 执行
        /// </summary>
        internal override void Do()
        {
            File.WriteAllText(Path, Content);
        }

        #endregion
    }
}
