using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Data.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Collections.Generic;

namespace BLL
{
    public class ReportPrintService
    {
        /// <summary>
        /// 打印报表列表
        /// </summary>
        /// <returns></returns>
        public static ListItem[] PrintReport()
        {
            ListItem[] lis = new ListItem[5];
            lis[0] = new ListItem("百万工时安全统计月报表", BLL.Const.Information_MillionsMonthlyReportId);
            lis[1] = new ListItem("职工伤亡事故原因分析报表", BLL.Const.Information_AccidentCauseReportId);
            lis[2] = new ListItem("安全生产数据季报", BLL.Const.Information_SafetyQuarterlyReportId);
            lis[3] = new ListItem("应急演练开展情况季报表", BLL.Const.Information_DrillConductedQuarterlyReportId);
            lis[4] = new ListItem("应急演练工作计划半年报表", BLL.Const.Information_DrillPlanHalfYearReportId);
            return lis;
        }
    }
}
