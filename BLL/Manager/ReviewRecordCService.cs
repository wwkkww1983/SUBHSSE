using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ReviewRecordCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取详细审查记录
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_ReviewRecordC> GetReviewRecordByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_ReviewRecordC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加详细审查记录
        /// </summary>
        /// <param name="newReviewRecord"></param>
        public static void AddReviewRecord(Model.Manager_Month_ReviewRecordC reviewRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_ReviewRecordC newReviewRecord = new Model.Manager_Month_ReviewRecordC
            {
                ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC)),
                MonthReportId = reviewRecord.MonthReportId,
                ReviewRecordName = reviewRecord.ReviewRecordName,
                ReviewMan = reviewRecord.ReviewMan,
                ReviewDate = reviewRecord.ReviewDate,
                SortIndex = reviewRecord.SortIndex
            };
            db.Manager_Month_ReviewRecordC.InsertOnSubmit(newReviewRecord);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关详细审查记录
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteReviewRecordByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_ReviewRecordC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_ReviewRecordC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
