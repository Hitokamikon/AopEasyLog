using Rougamo.Context;
using System.Collections.Generic;

namespace AopEasyLog
{
    /// <summary>
    /// 方法内的打印器
    /// </summary>
    public class MethodLogger : ILogger
    {
        #region 属性

        /// <summary>
        /// 上下文
        /// </summary>
        public MethodContext Context { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">上下文</param>
        public MethodLogger(MethodContext context)
        {
            Context = context;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 打印Debug信息
        /// </summary>
        /// <param name="debug">Debug信息</param>
        public void Debug(string debug)
        {
            if (Context.Datas.Contains("Debug"))
            {
                if (Context.Datas["Debug"] is List<string> debugs)
                {
                    debugs.Add(debug);
                }
            }
            else
            {
                List<string> debugs = new List<string>();
                Context.Datas.Add("Debug", debugs);
                debugs.Add(debug);
            }
        }

        /// <summary>
        /// 打印Info信息
        /// </summary>
        /// <param name="info">Info信息</param>
        public void Info(string info)
        {
            if (Context.Datas.Contains("Info"))
            {
                if (Context.Datas["Info"] is List<string> infos)
                {
                    infos.Add(info);
                }
            }
            else
            {
                List<string> infos = new List<string>();
                Context.Datas.Add("Info", infos);
                infos.Add(info);
            }
        }

        /// <summary>
        /// 打印警告信息
        /// </summary>
        /// <param name="warning">警告信息</param>
        public void Warn(string warning)
        {
            if (Context.Datas.Contains("Warn"))
            {
                if (Context.Datas["Warn"] is List<string> warns)
                {
                    warns.Add(warning);
                }
            }
            else
            {
                List<string> warns = new List<string>();
                Context.Datas.Add("Warn", warns);
                warns.Add(warning);
            }
        }

        #endregion
    }
}
