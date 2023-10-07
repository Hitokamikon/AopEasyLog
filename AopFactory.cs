using Rougamo;
using System;
using System.Collections.Generic;

namespace AopEasyLog
{
    /// <summary>
    /// AOP对象工厂
    /// </summary>
    static public class AopFactory
    {
        #region 字段

        /// <summary>
        /// AOP物体列表查询字典
        /// </summary>
        static readonly Dictionary<Type, List<AopObject>> aopObjectsDic = new Dictionary<Type, List<AopObject>>();

        #endregion

        #region 方法

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">AOP物体的类型</typeparam>
        /// <param name="paras">构造函数的参数</param>
        static public T Create<T>(params object[] paras) where T : IAopObject
        {
            Type type = typeof(T);
            if (Activator.CreateInstance(type, paras) is T t)
            {
                List<AopObject> aopObjects;
                if (aopObjectsDic.ContainsKey(type))
                {
                    aopObjects = aopObjectsDic[type];
                }
                else
                {
                    aopObjects = new List<AopObject>();
                    aopObjectsDic.Add(type, aopObjects);
                }
                var aopObject = new AopObject(t);
                aopObjects.Add(aopObject);

                return t;
            }
            return default;
        }

        #endregion
    }
}
