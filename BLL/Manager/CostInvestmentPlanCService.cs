using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class CostInvestmentPlanCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSE费用投入计划
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_CostInvestmentPlanC> GetCostInvestmentPlanByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_CostInvestmentPlanC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加HSE费用投入计划
        /// </summary>
        /// <param name="plan"></param>
        public static void AddCostInvestmentPlan(Model.Manager_Month_CostInvestmentPlanC plan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_CostInvestmentPlanC newP = new Model.Manager_Month_CostInvestmentPlanC
            {
                CostInvestmentPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_CostInvestmentPlanC)),
                MonthReportId = plan.MonthReportId,
                InvestmentProject = plan.InvestmentProject,
                MainPlanCost = plan.MainPlanCost,
                SubPlanCost = plan.SubPlanCost,
                SortIndex = plan.SortIndex
            };
            db.Manager_Month_CostInvestmentPlanC.InsertOnSubmit(newP);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所以费用投入计划
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteCostInvestmentPlanByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_CostInvestmentPlanC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_CostInvestmentPlanC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        public static ListItem[] GetCostInvestmentPlanList()
        {
            ListItem[] lis = new ListItem[7];
            lis[0] = new ListItem("基础管理");
            lis[1] = new ListItem("安全技术");
            lis[2] = new ListItem("职业健康");
            lis[3] = new ListItem("安全防护");
            lis[4] = new ListItem("化工试车");
            lis[5] = new ListItem("教育培训");
            lis[6] = new ListItem("文明施工环境保护");
            return lis;
        }
    }
}
