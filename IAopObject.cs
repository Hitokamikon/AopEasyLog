using System.ComponentModel;

namespace AopEasyLog
{
    /// <summary>
    /// AOP物体
    /// </summary>
    public interface IAopObject : INotifyPropertyChanged
    {
        #region 属性

        /// <summary>
        /// Aop Id
        /// </summary>
        /// <remarks>
        /// 一个类的所有物体应该都有一个唯一Id
        /// </remarks>
        int AopId { get; }

        /// <summary>
        /// 显示名称
        /// </summary>
        string AopDisplayName { get; }

        #endregion
    }
}
