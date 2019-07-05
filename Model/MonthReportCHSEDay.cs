using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 月报C（五环月报）项目现场HSE人工日统计情况
    /// </summary>
    public class MonthReportCHSEDay
    {
        /// <summary>
        /// 本月连续安全工作天数
        /// </summary>
        public int MonthHSEDay
        {
            get;
            set;
        }

        /// <summary>
        /// 累计连续安全工作天数
        /// </summary>
        public int SumHSEDay
        {
            get;
            set;
        }

        /// <summary>
        /// 本月HSE人工日
        /// </summary>
        public int MonthHSEWorkDay
        {
            get;
            set;
        }

        /// <summary>
        /// 年度累计HSE人工日
        /// </summary>
        public int YearHSEWorkDay
        {
            get;
            set;
        }

        /// <summary>
        /// 总累计HSE人工日
        /// </summary>
        public int SumHSEWorkDay
        {
            get;
            set;
        }

        /// <summary>
        /// 本月安全人工时（五环）
        /// </summary>
        public int HseManhours
        {
            get;
            set;
        }

        /// <summary>
        /// 本月安全人工时（分包商）
        /// </summary>
        public int SubcontractManHours
        {
            get;
            set;
        }

        /// <summary>
        /// 累计安全人工时
        /// </summary>
        public int TotalHseManhours
        {
            get;
            set;
        }
    }
}
