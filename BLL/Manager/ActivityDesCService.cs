using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ActivityDesCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取相关所有活动情况说明信息
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_ActivityDesC> GetActivityDesByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_ActivityDesC where x.MonthReportId == monthReportId select x).ToList();
        }

        /// <summary>
        /// 添加活动情况说明信息
        /// </summary>
        /// <param name="des"></param>
        public static void AddActivityDes(Model.Manager_Month_ActivityDesC des)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_ActivityDesC newDes = new Model.Manager_Month_ActivityDesC
            {
                ActivityDesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ActivityDesC)),
                MonthReportId = des.MonthReportId,
                ActivityTheme = des.ActivityTheme,
                ActivityDate = des.ActivityDate,
                UnitName = des.UnitName,
                EffectDes = des.EffectDes,
                SortIndex = des.SortIndex
            };
            db.Manager_Month_ActivityDesC.InsertOnSubmit(newDes);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关活动情况说明
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteActivityDesByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_ActivityDesC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_ActivityDesC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
