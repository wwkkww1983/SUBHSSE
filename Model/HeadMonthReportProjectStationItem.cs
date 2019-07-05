using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class HeadMonthReportProjectStationItem
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        public string ProjectCode
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
        /// 项目经理
        /// </summary>
        public string ProjectManager
        {
            get;
            set;
        }

        /// <summary>
        /// 项目类型
        /// </summary>
        public string ProjectType
        {
            get;
            set;
        }

        /// <summary>
        /// 项目开工日期
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 项目竣工日期
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 完成工时(当月)
        /// </summary>
        public string Manhours
        {
            get;
            set;
        }

        /// <summary>
        /// 完成工时(累计)
        /// </summary>
        public string TotalManhours
        {
            get;
            set;
        }

        /// <summary>
        /// 项目安全工时(当月)
        /// </summary>
        public string HseManhours
        {
            get;
            set;
        }

        /// <summary>
        /// 项目安全工时(累计)
        /// </summary>
        public string TotalHseManhours
        {
            get;
            set;
        }

        /// <summary>
        /// 当月员工总数
        /// </summary>
        public string TotalManNum
        {
            get;
            set;
        }

        /// <summary>
        /// 事故起数(当月)
        /// </summary>
        public string AccidentNum
        {
            get;
            set;
        }

        /// <summary>
        /// 事故起数(累计)
        /// </summary>
        public string AccidentTotalNum
        {
            get;
            set;
        }

        /// <summary>
        /// 损失工时(当月)
        /// </summary>
        public string LoseHours
        {
            get;
            set;
        }

        /// <summary>
        /// 损失工时(累计)
        /// </summary>
        public string TotalLoseHours
        {
            get;
            set;
        }

        /// <summary>
        /// 可记录事故率
        /// </summary>
        public string AvgA
        {
            get;
            set;
        }

        /// <summary>
        /// 损失工时事故率
        /// </summary>
        public string AvgB
        {
            get;
            set;
        }

        /// <summary>
        /// 事故伤害严重率
        /// </summary>
        public string AvgC
        {
            get;
            set;
        }

        /// <summary>
        /// 危险性较大的清单数
        /// </summary>
        public string HazardNum
        {
            get;
            set;
        }

        /// <summary>
        /// 需要专家论证的危险性较大的清单数
        /// </summary>
        public string IsArgumentHazardNum
        {
            get;
            set;
        }

        /// <summary>
        /// 安全生产费计划额
        /// </summary>
        public string PlanCostA
        {
            get;
            set;
        }

        /// <summary>
        /// 文明施工措施费计划额
        /// </summary>
        public string PlanCostB
        {
            get;
            set;
        }

        /// <summary>
        /// A-安全生产合计(当期)
        /// </summary>
        public string RealCostA
        {
            get;
            set;
        }

        /// <summary>
        /// A-安全生产合计(当年合计)
        /// </summary>
        public string RealCostYA
        {
            get;
            set;
        }

        /// <summary>
        /// A-安全生产合计(项目累计)
        /// </summary>
        public string RealCostPA
        {
            get;
            set;
        }

        /// <summary>
        /// B-文明施工合计(当期)
        /// </summary>
        public string RealCostB
        {
            get;
            set;
        }

        /// <summary>
        /// B-文明施工合计(当年合计)
        /// </summary>
        public string RealCostYB
        {
            get;
            set;
        }

        /// <summary>
        /// B-文明施工合计(项目累计)
        /// </summary>
        public string RealCostPB
        {
            get;
            set;
        }

        /// <summary>
        /// A+B合计(当期)
        /// </summary>
        public string RealCostAB
        {
            get;
            set;
        }

        /// <summary>
        /// A+B合计(当年合计)
        /// </summary>
        public string RealCostYAB
        {
            get;
            set;
        }

        /// <summary>
        /// A+B合计(项目累计)
        /// </summary>
        public string RealCostPAB
        {
            get;
            set;
        }
    }
}
