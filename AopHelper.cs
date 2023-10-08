using Rougamo;
using Rougamo.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AopEasyLog
{
    /// <summary>
    /// AOP助手
    /// </summary>
    static public class AopHelper
    {
        #region 属性

        /// <summary>
        /// 路径
        /// </summary>
        static public string Path { get; private set; }

        /// <summary>
        /// 写文件队列
        /// </summary>
        static internal ConcurrentQueue<WriteFile> WriteFiles { get; private set; } = new ConcurrentQueue<WriteFile>();

        /// <summary>
        /// Log类型
        /// </summary>
        //static public LogType LogType { get; set; } = LogType.Console | LogType.Debug;

        /// <summary>
        /// 成员信息记录查询字典
        /// </summary>
        static public Dictionary<MemberInfo, MemberRecords> MemberRecordsDic { get; private set; } = new Dictionary<MemberInfo, MemberRecords>();

        static bool isOn = true;

        /// <summary>
        /// 是否开启AOP功能
        /// </summary>
        static public bool IsOn 
        {
            get => isOn;
            set
            {
                if(isOn != value)
                {
                    isOn = value;
                    OnSwitched?.Invoke(null , EventArgs.Empty);
                }
            }
        }

        static int logLevel = 1;

        /// <summary>
        /// 打印等级
        /// </summary>
        static public int LogLevel
        {
            get => logLevel;
            set
            {
                if(logLevel != value)
                {
                    logLevel = value;
                    OnLogLevelChanged?.Invoke(null , EventArgs.Empty);
                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 当开启状态改变
        /// </summary>
        static public event EventHandler OnSwitched;

        /// <summary>
        /// 当打印等级改变
        /// </summary>
        static public event EventHandler OnLogLevelChanged;

        /// <summary>
        /// 当打印Debug
        /// </summary>
        static public event EventHandler<string> OnDebug;

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        [IgnoreMo]
        static public void Init()
        {
            Path = "AOP/" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss fff");
            Directory.CreateDirectory(Path);
            Directory.CreateDirectory($"{Path}/MethodInvoke");
            Directory.CreateDirectory($"{Path}/PropertyChange");

            Task.Run(() => 
            {
                while(true)
                {
                    int count = 0;
                    while(WriteFiles.Count > 0 && count < 10)
                    {
                        count++;
                        if(WriteFiles.TryDequeue(out var file))
                        {
                            file?.DoWrite();
                        }
                    }
                    Thread.Sleep(10);
                }
            });
        }

        /// <summary>
        /// 开始调用
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="target">目标物体</param>
        /// <param name="invokeTime">调用的次数</param>
        /// <param name="paras">参数列表</param>
        /// <returns>调用的时间</returns>
        [IgnoreMo]
        static public DateTime BeginInvoke(MethodContext context, IAopObject target , int invokeTime , List<string> paras)
        {
            MemberRecords memberRecords;
            if (MemberRecordsDic.ContainsKey(context.Method))
                memberRecords = MemberRecordsDic[context.Method];
            else
            {
                memberRecords = new MemberRecords(context);
                MemberRecordsDic[context.Method] = memberRecords;
            }
            DateTime dateTime = memberRecords.BeginInvoke(target, invokeTime, paras, new StackTrace(true));


            string debug = $"开始调用方法[{context.Method.Name}]";

            Debug(debug);

            return dateTime;
        }

        /// <summary>
        /// 结束调用
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="target">目标物体</param>
        /// <param name="invokeTime">调用的次数</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="logs">内部打印</param>
        [IgnoreMo]
        static public void EndInvoke(MethodContext context, IAopObject target, int invokeTime, string returnValue, List<string> logs)
        {
            if (MemberRecordsDic.ContainsKey(context.Method))
            {
                MemberRecordsDic[context.Method].EndInvoke(target, invokeTime, returnValue, logs);
            }
            string debug = $"结束调用方法[{context.Method.Name}] ， 返回值:{returnValue}";

            Debug(debug);
        }

        /// <summary>
        /// 打印堆栈
        /// </summary>
        /// <param name="streamWriter">文件写入器</param>
        /// <param name="stackTrace">堆栈</param>
        [IgnoreMo]
        static public void PrintStackTrace(StreamWriter streamWriter , StackTrace stackTrace)
        {
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                streamWriter.Write(stackTrace.GetFrame(i));
            }
            streamWriter.WriteLine();
        }

        /// <summary>
        /// Debug打印
        /// </summary>
        /// <param name="debug">debug内容</param>
        [IgnoreMo]
        static public void Debug(string debug)
        {
            OnDebug?.Invoke(null, debug);
        }

        #endregion
    }
}
