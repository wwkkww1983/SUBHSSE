using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 考试计划项
    /// </summary>
    public class TestPlanItem
    {
        /// <summary>
        /// 考试计划ID
        /// </summary>
        public string TestPlanId
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PlanCode
        {
            get;
            set;
        }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 计划人
        /// </summary>
        public string PlanManId
        {
            get;
            set;
        }
        /// <summary>
        /// 计划人姓名
        /// </summary>
        public string PlanManName
        {
            get;
            set;
        }
        /// <summary>
        /// 制定时间
        /// </summary>
        public string PlanDate
        {
            get;
            set;
        }
        /// <summary>
        /// 扫码开始时间
        /// </summary>
        public string TestStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 扫码结束时间
        /// </summary>
        public string TestEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 考试时长
        /// </summary>
        public int Duration
        {
            get;
            set;
        }
        /// <summary>
        /// 总分数
        /// </summary>
        public int TotalScore
        {
            get;
            set;
        }
        /// <summary>
        /// 考题数量
        /// </summary>
        public int QuestionCount
        {
            get;
            set;
        }
        /// <summary>
        /// 考试地点
        /// </summary>
        public string TestPalce
        {
            get;
            set;
        }

        /// <summary>
        /// 考试单位ID
        /// </summary>
        public string UnitIds
        {
            get;
            set;
        }
        /// <summary>
        /// 考试单位名称
        /// </summary>
        public string UnitNames
        {
            get;
            set;
        }
        /// <summary>
        /// 考试岗位ID
        /// </summary>
        public string WorkPostIds
        {
            get;
            set;
        }
        /// <summary>
        /// 考试岗位名称
        /// </summary>
        public string WorkPostNames
        {
            get;
            set;
        }
        /// <summary>
        /// 考试二维码
        /// </summary>
        public string QRCodeUrl
        {
            get;
            set;
        }       
        /// <summary>
        /// 状态
        /// </summary>
        public string States
        {
            get;
            set;
        }
    }
}
