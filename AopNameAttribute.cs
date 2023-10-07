using System;

namespace AopEasyLog
{
    /// <summary>
    /// AOP名称特性
    /// </summary>
    public class AopNameAttribute : Attribute
    {
        #region 属性

        /// <summary>
        /// AOP名称
        /// </summary>
        public string Name { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">AOP名称</param>
        public AopNameAttribute(string name)
        {
            Name = name;
        }

        #endregion
    }
}
