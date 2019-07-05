using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SpManagerTotalMonthSafetyDataDItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            get;
            set;
        }

       /// <summary>
       /// 项目名称
       /// </summary>
        public string ProjectName
       {
           get;
           set;
       }
       /// <summary>
        /// 排列序号
        /// </summary>
        public int SortIndex
        {
            get;
            set;
        } 
       /// <summary>
        /// 现场人数
       /// </summary>
        public string ThisUnitPersonNum
       {
           get;
           set;
       }

       /// <summary>
       /// 现场HSE管理人数
       /// </summary>
        public string ThisUnitHSEPersonNum
       {
           get;
           set;
       }

        /// <summary>
        /// 分包商现场人数
        /// </summary>
        public string SubUnitPersonNum
        {
            get;
            set;
        }

        /// <summary>
        /// 分包商HSE管理人数
        /// </summary>
        public string SubUnitHSEPersonNum
        {
            get;
            set;
        }

        /// <summary>
        /// 人工时数
        /// </summary>
        public string ManHours
        {
            get;
            set;
        }

        /// <summary>
        /// 安全生产人工时数
        /// </summary>
        public string HSEManHours
        {
            get;
            set;
        }
    }
}
