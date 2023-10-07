namespace AopEasyLog
{
    /// <summary>
    /// 属性改变记录
    /// </summary>
    public class PropertyChangeRecord : Record
    {
        #region 属性

        /// <summary>
        /// 属性值
        /// </summary>
        public object Value { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">属性值</param>
        public PropertyChangeRecord(object value)
        {
            Value = value;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重写转字符串方法
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => Value.ToString();

        #endregion
    }
}
