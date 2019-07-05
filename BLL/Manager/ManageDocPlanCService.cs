using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ManageDocPlanCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSE管理文件/方案修编计划
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_ManageDocPlanC> GetManageDocPlanByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_ManageDocPlanC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        ///添加HSE管理文件/方案修编计划
        /// </summary>
        /// <param name="manageDocPlan"></param>
        public static void AddManageDocPlan(Model.Manager_Month_ManageDocPlanC manageDocPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_ManageDocPlanC newManageDocPlan = new Model.Manager_Month_ManageDocPlanC
            {
                ManageDocPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ManageDocPlanC)),
                MonthReportId = manageDocPlan.MonthReportId,
                ManageDocPlanName = manageDocPlan.ManageDocPlanName,
                CompileMan = manageDocPlan.CompileMan,
                CompileDate = manageDocPlan.CompileDate,
                SortIndex = manageDocPlan.SortIndex
            };
            db.Manager_Month_ManageDocPlanC.InsertOnSubmit(newManageDocPlan);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关HSE管理文件/方案修编计划
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteManageDocPlanByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_ManageDocPlanC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_ManageDocPlanC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
