using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AopEasyLog
{
    /// <summary>
    /// 属性变化记录集合
    /// </summary>
    public class PropertyChangeRecords
    {
        #region 字段

        /// <summary>
        /// 路径
        /// </summary>
        readonly string path;

        #endregion

        #region 属性

        /// <summary>
        /// 属性变化记录列表
        /// </summary>
        public List<PropertyChangeRecord> Records { get; private set; } = new List<PropertyChangeRecord>();

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        /// 所在文件夹目录
        /// </summary>
        public string Folder { get;private set; }

        /// <summary>
        /// 属性的目录
        /// </summary>
        public string PropertyFolder { get; private set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName => PropertyInfo.Name;

        /// <summary>
        /// 目标物体
        /// </summary>
        public object Obj { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyInfo">属性信息</param>
        /// <param name="obj">目标物体</param>
        /// <param name="folder">所在文件夹目录</param>
        public PropertyChangeRecords(PropertyInfo propertyInfo, object obj, string folder)
        {
            Folder = folder;
            PropertyInfo = propertyInfo;
            Obj = obj;
            PropertyFolder = $"{folder}/{propertyInfo.Name}";
            path = $"{PropertyFolder}/初始值.txt";

            if (!Directory.Exists(PropertyFolder))
                Directory.CreateDirectory(PropertyFolder);

            var initValue = propertyInfo.GetValue(obj);

            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(path);
                streamWriter.WriteLine(initValue);
                streamWriter.Flush();
            }
            finally
            {
                streamWriter?.Close();
            }

        }
        

        #endregion

        #region 方法

        /// <summary>
        /// 添加记录
        /// </summary>
        public void AddRecord()
        {
            object value = PropertyInfo.GetValue(Obj, null);
            PropertyChangeRecord propertyChangeRecord = new PropertyChangeRecord(value);
            Records.Add(propertyChangeRecord);

            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter($"{PropertyFolder}/[{DateTime.Now:yyyy.MM.dd HH.mm.ss fff}].txt");
                streamWriter.Write(propertyChangeRecord.ToString());
                streamWriter.Flush();
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        #endregion
    }
}
