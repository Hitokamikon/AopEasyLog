using Rougamo;
using Rougamo.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
[assembly: IgnoreMo]

namespace AopEasyLog
{
    /// <summary>
    /// 打印的Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Class)]
    public class LogAttribute : MoAttribute
    {
        #region 字段

        /// <summary>
        /// 调用次数查询字典
        /// </summary>
        static readonly Dictionary<MethodBase, int> invokeTimesDic = new Dictionary<MethodBase, int>();

        #endregion

        #region 属性 

        /// <summary>
        /// Log等级
        /// </summary>
        /// <remarks>
        /// 等级越高越容易打印
        /// </remarks>
        public int LogLevel { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logLevel">Log等级</param>
        public LogAttribute(int logLevel = 1)
        {
            LogLevel = logLevel;
        }


        #endregion

        #region 方法

        /// <summary>
        /// 当方法开始执行
        /// </summary>
        /// <param name="context">上下文</param>
        public override void OnEntry(MethodContext context)
        {
            var parameterInfos = context.Method.GetParameters();
            ILogger logger = null;
            if (parameterInfos.Length > 0)
            {
                for(int i = 0; i < parameterInfos.Length; i++)
                {
                    var parameterInfo = parameterInfos[i];
                    if(parameterInfo.DefaultValue == null && parameterInfo.ParameterType == typeof(ILogger))
                    {
                        context.RewriteArguments = true;
                        if (logger == null)
                            logger = new MethodLogger(context);
                        context.Arguments[i] = logger;
                    }
                }
            }
            base.OnEntry(context);
            if (context.Method.Name.StartsWith("get_")) return;
            if (context.Method.Name.StartsWith("add_")) return;
            if (context.Method.Name.StartsWith("remove_")) return;
            int times = 1;
            if (invokeTimesDic.ContainsKey(context.Method))
            {
                times = invokeTimesDic[context.Method] + 1;
                invokeTimesDic[context.Method] = times;
            }
            else
                invokeTimesDic.Add(context.Method, times);
            
            if (!AopHelper.IsOn) return;
            if (AopHelper.LogLevel > LogLevel) return;
            List<string> paras = new List<string>();
            for (int i = 0; i < context.Arguments.Length; i++)
            {
                paras.Add(context.Arguments[i].ToString());
            }
            AopHelper.BeginInvoke(context, context.Target as IAopObject, times, paras);
        }

        /// <summary>
        /// 当方法结束执行
        /// </summary>
        /// <param name="context">上下文</param>
        public override void OnExit(MethodContext context)
        {
            base.OnExit(context);

            if (!AopHelper.IsOn) return;
            if (AopHelper.LogLevel > LogLevel) return;
            if (!invokeTimesDic.ContainsKey(context.Method)) return;
            int invokeTimes = invokeTimesDic[context.Method];

            List<string> debugs = null;
            if (context.Datas.Contains("Debug"))
            {
                debugs = context.Datas["Debug"] as List<string>;
            }

            AopHelper.EndInvoke(context, context.Target as IAopObject, invokeTimes, context.ReturnValue?.ToString(), debugs);

            invokeTimesDic.Remove(context.Method);
        }

        #endregion

    }
}
