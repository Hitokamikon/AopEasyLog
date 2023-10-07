using Rougamo.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AopEasyLog
{
    /// <summary>
    /// 成员信息记录
    /// </summary>
    public class MemberRecords
    {
        #region 字段

        /// <summary>
        /// 文件的路径
        /// </summary>
        readonly string folder;

        #endregion

        #region 属性

        /// <summary>
        /// 成员信息
        /// </summary>
        public MemberInfo MemberInfo { get; private set; }

        /// <summary>
        /// Debug记录集合
        /// </summary>
        public List<DebugRecord> DebugRecords { get; private set; } = new List<DebugRecord>();

        /// <summary>
        /// Info记录集合
        /// </summary>
        public List<InfoRecord> InfoRecords { get; private set; } = new List<InfoRecord>();

        /// <summary>
        /// 警告记录集合
        /// </summary>
        public List<WarnRecord> WarnRecords { get; private set; } = new List<WarnRecord>();

        /// <summary>
        /// 异常记录集合
        /// </summary>
        public List<ExceptionRecord> ExceptionRecords { get; private set; } = new List<ExceptionRecord>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">方法上下文</param>
        public MemberRecords(MethodContext context)
        {
            MemberInfo = context.Method;
            folder = $"{AopHelper.Path}/MethodInvoke/{context.TargetType.FullName}";
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        #endregion

        #region 方法

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="target">目标物体</param>
        /// <param name="invokeTime">调用次数</param>
        /// <param name="paras">参数</param>
        /// <param name="stackTrace">堆栈</param>
        /// <returns>记录的时间</returns>
        public DateTime BeginInvoke(IAopObject target, int invokeTime , List<string> paras , StackTrace stackTrace)
        {
            DateTime dateTime = DateTime.Now;
            string folder = this.folder;
            StreamWriter streamWriter = null;
            if (target != null)
            {
                folder = $"{this.folder}/{target.AopId}";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                if(!string.IsNullOrEmpty(target.AopDisplayName))
                {
                    try
                    {
                        streamWriter = new StreamWriter($"{folder}/AopDisplayName.txt");
                        streamWriter.Write(target.AopDisplayName);
                        streamWriter.Flush();
                    }
                    finally
                    {
                        streamWriter?.Close();
                    }
                }
            }
            string path = $"{folder}/[{invokeTime}][{MemberInfo.Name}]开始执行[{dateTime:yyyy.MM.dd HH.mm.ss fff}].txt";

            try
            {
                streamWriter = new StreamWriter(path);
                streamWriter.WriteLine($"参数[{paras.Count}]:");
                foreach(var para in paras)
                {
                    streamWriter.WriteLine($"{para.Length}|{para}");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("堆栈:");
                foreach(var trace in stackTrace.GetFrames())
                {
                    streamWriter.Write(trace);
                }

                streamWriter.Flush();
            }
            catch (Exception)
            {
                //ConsoleHelper.Warn(ex.Message);
            }
            finally
            {
                streamWriter?.Close();
            }

            return dateTime;
        }

        /// <summary>
        /// 结束调用
        /// </summary>
        /// <param name="target">目标物体</param>
        /// <param name="invokeTime">调用次数</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="logs">函数体内部打印列表</param>
        public void EndInvoke(IAopObject target, int invokeTime, string returnValue, List<string> logs)
        {
            if (string.IsNullOrEmpty(returnValue)) return;
            DateTime dateTime = DateTime.Now;
            string folder = this.folder;
            StreamWriter streamWriter = null;
            if (target != null)
            {
                folder = $"{this.folder}/{target.AopId}";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
            }
            string path = $"{folder}/[{invokeTime}][{MemberInfo.Name}]结束执行[{dateTime:yyyy.MM.dd HH.mm.ss fff}].txt";

            try
            {
                streamWriter = new StreamWriter(path);
                if(!string.IsNullOrEmpty(returnValue))
                {
                    streamWriter.WriteLine($"返回[{returnValue.Length}]:");
                    streamWriter.WriteLine(returnValue);
                }

                if(logs != null && logs.Count > 0)
                {
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("函数内部打印:");
                    foreach(var log in logs) 
                    {
                        streamWriter.WriteLine(log);
                    }
                }

                streamWriter.Flush();
            }
            catch (Exception)
            {
                //ConsoleHelper.Warn(ex.Message);
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="exception">异常</param>
        public void Exception(Exception exception)
        {
            var exceptionRecord = new ExceptionRecord(exception);
            ExceptionRecords.Add(exceptionRecord);

            StreamWriter streamWriter = null;
            try
            {
                //streamWriter = new StreamWriter(exceptionFilePath, true);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                streamWriter.WriteLine(exceptionRecord.ToString());
                streamWriter.Flush();
            }
            catch (Exception)
            {
                //ConsoleHelper.Warn(ex.Message);
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        #endregion
    }
}
