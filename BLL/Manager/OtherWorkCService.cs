using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class OtherWorkCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报ID获取其他工作情况列表
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_OtherWorkC> GetOtherWorkByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_OtherWorkC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加其他工作情况
        /// </summary>
        /// <param name="otherWork"></param>
        public static void AddOtherWork(Model.Manager_Month_OtherWorkC otherWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_OtherWorkC newOtherWork = new Model.Manager_Month_OtherWorkC
            {
                OtherWorkId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherWorkC)),
                MonthReportId = otherWork.MonthReportId,
                WorkContentDes = otherWork.WorkContentDes,
                SortIndex = otherWork.SortIndex
            };
            db.Manager_Month_OtherWorkC.InsertOnSubmit(newOtherWork);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关的其他工作情况
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteOtherWorkByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_OtherWorkC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_OtherWorkC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
