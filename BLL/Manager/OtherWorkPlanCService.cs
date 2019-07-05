using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class OtherWorkPlanCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取所有相关其他HSE工作计划
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_OtherWorkPlanC> GetOtherWorkPlanByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_OtherWorkPlanC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加其他HSE工作计划
        /// </summary>
        /// <param name="otherWorkPlan"></param>
        public static void AddOtherWorkPlan(Model.Manager_Month_OtherWorkPlanC otherWorkPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_OtherWorkPlanC newP = new Model.Manager_Month_OtherWorkPlanC
            {
                OtherWorkPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherWorkPlanC)),
                MonthReportId = otherWorkPlan.MonthReportId,
                WorkContent = otherWorkPlan.WorkContent,
                SortIndex = otherWorkPlan.SortIndex
            };
            db.Manager_Month_OtherWorkPlanC.InsertOnSubmit(newP);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关其他HSE工作计划
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteOtherWorkPlanByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_OtherWorkPlanC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_OtherWorkPlanC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
