using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SubExpenseCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取所有相关分包商HSE费用投入
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_SubExpenseC> GetSubExpenseByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_SubExpenseC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加分包商HSE费用投入
        /// </summary>
        /// <param name="newSubExpense"></param>
        public static void AddSubExpense(Model.Manager_Month_SubExpenseC subExpense)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_SubExpenseC newSubExpense = new Model.Manager_Month_SubExpenseC
            {
                SubExpenseId = SQLHelper.GetNewID(typeof(Model.Manager_Month_SubExpenseC)),
                MonthReportId = subExpense.MonthReportId,
                SubUnit = subExpense.SubUnit,
                CostMonth = subExpense.CostMonth,
                CostYear = subExpense.CostYear,
                SortIndex = subExpense.SortIndex
            };
            db.Manager_Month_SubExpenseC.InsertOnSubmit(newSubExpense);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除分包商HSE费用投入
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteSubExpenseByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_SubExpenseC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_SubExpenseC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
