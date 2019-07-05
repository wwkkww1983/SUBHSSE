using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FiveExpenseCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取五环HSE费用投入
        /// </summary>
        /// <returns></returns>
        public static List<Model.Manager_Month_FiveExpenseC> GetFiveExpenseByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_FiveExpenseC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加五环HSE费用投入
        /// </summary>
        /// <param name="fiveExpense"></param>
        public static void AddFiveExpense(Model.Manager_Month_FiveExpenseC fiveExpense)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_FiveExpenseC newFiveExpense = new Model.Manager_Month_FiveExpenseC
            {
                FiveExpenseId = SQLHelper.GetNewID(typeof(Model.Manager_Month_FiveExpenseC)),
                MonthReportId = fiveExpense.MonthReportId,
                InvestmentProject = fiveExpense.InvestmentProject,
                PlanCostMonth = fiveExpense.PlanCostMonth,
                PlanCostYear = fiveExpense.PlanCostYear,
                ActualCostMonth = fiveExpense.ActualCostMonth,
                ActualCostYear = fiveExpense.ActualCostYear,
                SortIndex = fiveExpense.SortIndex
            };
            db.Manager_Month_FiveExpenseC.InsertOnSubmit(newFiveExpense);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报ID删除所有相关五环HSE费用投入信息
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteFiveExpenseByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_FiveExpenseC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_FiveExpenseC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
