using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace AopEasyLog
{
    /// <summary>
    /// AOP物体
    /// </summary>
    public class AopObject
    {
        #region 属性

        /// <summary>
        /// 物体
        /// </summary>
        public IAopObject Object { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 属性改变集合列表
        /// </summary>
        public List<PropertyChangeRecords> PropertyChangeRecordss { get; private set; } = new List<PropertyChangeRecords>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="obj">物体</param>
        public AopObject(IAopObject obj)
        {
            Object = obj;
            CreateTime = DateTime.Now;
            var propertyInfos = obj.GetType().GetProperties();

            var typeName = obj.GetType().FullName;
            string folder = $"{AopHelper.Path}/PropertyChange/{typeName}/[{obj.AopId}][{CreateTime:yyyy.MM.dd HH.mm.ss fff}]";

            Task.Run(() => {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                foreach (var propertyInfo in propertyInfos)
                {
                    PropertyChangeRecords propertyChangeRecords = new PropertyChangeRecords(propertyInfo, obj, folder);

                    PropertyChangeRecordss.Add(propertyChangeRecords);
                }

                obj.PropertyChanged += Obj_PropertyChanged;
            });
        }

        #endregion

        #region 事件响应

        private void Obj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangeRecords propertyChangeRecords = PropertyChangeRecordss.Find(r=>r.PropertyName == e.PropertyName);
            propertyChangeRecords?.AddRecord();
        }

        #endregion
    }
}
