using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SpManagerTotalMonthItem
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
        /// 项目编号
        /// </summary>
        public string ProjectCode
        {
            get;
            set;
        }

       /// <summary>
       /// 存在隐患
       /// </summary>
        public string ExistenceHiddenDanger
       {
           get;
           set;
       }

       /// <summary>
       /// 整改措施
       /// </summary>
        public string CorrectiveActions
       {
           get;
           set;
       }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        public string PlanCompletedDate
        {
            get;
            set;
        }

        /// <summary>
        /// 责任人
        /// </summary>
        public string ResponsiMan
        {
            get;
            set;
        }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public string ActualCompledDate
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
