using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PlanCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取本月文件、方案修编情况说明
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_PlanC> GetPlanByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_PlanC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加本月文件、方案修编情况说明
        /// </summary>
        /// <param name="newPlan"></param>
        public static void AddPlan(Model.Manager_Month_PlanC plan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_PlanC newPlan = new Model.Manager_Month_PlanC
            {
                PlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_PlanC)),
                MonthReportId = plan.MonthReportId,
                PlanName = plan.PlanName,
                CompileMan = plan.CompileMan,
                CompileDate = plan.CompileDate,
                SortIndex = plan.SortIndex
            };
            db.Manager_Month_PlanC.InsertOnSubmit(newPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除本月文件、方案修编情况说明
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeletePlanByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_PlanC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_PlanC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
