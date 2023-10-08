using System.IO;

namespace AopEasyLog
{
    internal class CreateDir : Operation
    {
        #region 属性

        /// <summary>
        /// 路径
        /// </summary>
        internal string Dir { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dir">路径</param>
        public CreateDir(string dir)
        {
            Dir = dir;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 执行
        /// </summary>
        internal override void Do()
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }

        #endregion
    }
}
