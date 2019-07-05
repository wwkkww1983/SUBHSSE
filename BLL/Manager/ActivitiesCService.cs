using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class ActivitiesCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSSE活动
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_ActivitiesC> GetActivitiesByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_ActivitiesC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加HSSE活动
        /// </summary>
        /// <param name="activities"></param>
        public static void AddActivities(Model.Manager_Month_ActivitiesC activities)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_ActivitiesC newActivities = new Model.Manager_Month_ActivitiesC
            {
                ActivitiesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ActivitiesC)),
                MonthReportId = activities.MonthReportId,
                ActivitiesTitle = activities.ActivitiesTitle,
                ActivitiesDate = activities.ActivitiesDate,
                Unit = activities.Unit,
                Remark = activities.Remark,
                SortIndex = activities.SortIndex
            };
            db.Manager_Month_ActivitiesC.InsertOnSubmit(newActivities);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关的HSE活动
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteActivitiesByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_ActivitiesC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_ActivitiesC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
