using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MeetingSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_MeetingSortC> GetMeetingSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_MeetingSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        public static Model.Manager_MeetingSortC GetMeetingSortsByMonthReportIdAndMeetingType(string monthReportId, string meetingType)
        {
            return (from x in Funs.DB.Manager_MeetingSortC where x.MonthReportId == monthReportId && x.MeetingType == meetingType select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加月报告HSE会议情况信息
        /// </summary>
        /// <param name="meetingSort">月报告HSE会议情况实体</param>
        public static void AddMeetingSort(Model.Manager_MeetingSortC meetingSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortC));
            Model.Manager_MeetingSortC newMeetingSort = new Model.Manager_MeetingSortC
            {
                MeetingSortId = newKeyID,
                MonthReportId = meetingSort.MonthReportId,
                MeetingType = meetingSort.MeetingType,
                SortIndex = meetingSort.SortIndex,
                MeetingHours = meetingSort.MeetingHours,
                MeetingHostMan = meetingSort.MeetingHostMan,
                MeetingDate = meetingSort.MeetingDate,
                AttentPerson = meetingSort.AttentPerson,
                MainContent = meetingSort.MainContent
            };

            db.Manager_MeetingSortC.InsertOnSubmit(newMeetingSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMeetingSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_MeetingSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_MeetingSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
