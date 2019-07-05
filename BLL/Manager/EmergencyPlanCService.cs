using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class EmergencyPlanCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取应急预案修编
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_EmergencyPlanC> GetEmergencyPlanByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_EmergencyPlanC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加应急预案修编
        /// </summary>
        /// <param name="plan"></param>
        public static void AddEmergencyPlan(Model.Manager_Month_EmergencyPlanC plan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_EmergencyPlanC newPlan = new Model.Manager_Month_EmergencyPlanC
            {
                EmergencyPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_EmergencyPlanC)),
                MonthReportId = plan.MonthReportId,
                EmergencyName = plan.EmergencyName,
                CompileMan = plan.CompileMan,
                CompileDate = plan.CompileDate,
                Remark = plan.Remark,
                SortIndex = plan.SortIndex
            };
            db.Manager_Month_EmergencyPlanC.InsertOnSubmit(newPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关应急预案修编
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteEmergencyPlanByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_EmergencyPlanC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_EmergencyPlanC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
