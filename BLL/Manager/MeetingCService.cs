using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class MeetingCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据日报Id获取会议
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_MeetingC> GetMeetingByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_MeetingC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="meeting"></param>
        public static void AddMeeting(Model.Manager_Month_MeetingC meeting)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_MeetingC newMeeting = new Model.Manager_Month_MeetingC
            {
                MeetingId = SQLHelper.GetNewID(typeof(Model.Manager_Month_MeetingC)),
                MonthReportId = meeting.MonthReportId,
                MeetingTitle = meeting.MeetingTitle,
                MeetingDate = meeting.MeetingDate,
                Host = meeting.Host,
                Participants = meeting.Participants,
                SortIndex = meeting.SortIndex
            };
            db.Manager_Month_MeetingC.InsertOnSubmit(newMeeting);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关会议
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteMeetingByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_MeetingC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_MeetingC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
