using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MeetingSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_MeetingSort GetMeetingSortsByMonthReportId(string monthReportId)
        {
            return Funs.DB.Manager_MeetingSort.FirstOrDefault(x => x.MonthReportId == monthReportId);
        }


        /// <summary>
        /// 增加月报告HSE会议情况信息
        /// </summary>
        /// <param name="meetingSort">月报告HSE会议情况实体</param>
        public static void AddMeetingSort(Model.Manager_MeetingSort meetingSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSort));
            Model.Manager_MeetingSort newMeetingSort = new Model.Manager_MeetingSort
            {
                MeetingSortId = newKeyID,
                MonthReportId = meetingSort.MonthReportId,
                MeetingNumber01 = meetingSort.MeetingNumber01,
                MeetingNumber02 = meetingSort.MeetingNumber02,
                MeetingNumber03 = meetingSort.MeetingNumber03,
                MeetingNumber04 = meetingSort.MeetingNumber04,
                MeetingNumber11 = meetingSort.MeetingNumber11,
                MeetingNumber12 = meetingSort.MeetingNumber12,
                MeetingNumber13 = meetingSort.MeetingNumber13,
                MeetingNumber14 = meetingSort.MeetingNumber14
            };

            db.Manager_MeetingSort.InsertOnSubmit(newMeetingSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMeetingSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_MeetingSort where x.MonthReportId == monthReportId select x).ToList();
            if (q.Count() > 0)
            {
                db.Manager_MeetingSort.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
