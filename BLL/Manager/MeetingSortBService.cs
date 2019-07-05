using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MeetingSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_MeetingSortB> GetMeetingSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_MeetingSortB where x.MonthReportId == monthReportId select x).ToList();
        }

        public static Model.Manager_MeetingSortB GetMeetingSortsByMonthReportIdAndMeetingType(string monthReportId, string meetingType)
        {
            return (from x in Funs.DB.Manager_MeetingSortB where x.MonthReportId == monthReportId && x.MeetingType == meetingType select x).FirstOrDefault();
        }


        /// <summary>
        /// 根据月报告主键获取总会议次数
        /// </summary>
        /// <param name="meetingType">会议类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumMeetingNumberByMonthReportId(string monthReportId, string meetingType)
        {
            return (from x in Funs.DB.Manager_MeetingSortB where x.MonthReportId == monthReportId && x.MeetingType == meetingType select x.MeetingNumber).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总参加人数
        /// </summary>
        /// <param name="meetingType">会议类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumMeetingPersonNumberByMonthReportId(string monthReportId, string meetingType)
        {
            return (from x in Funs.DB.Manager_MeetingSortB where x.MonthReportId == monthReportId && x.MeetingType == meetingType select x.MeetingPersonNumber).Sum();
        }

        /// <summary>
        /// 增加月报告HSE会议情况信息
        /// </summary>
        /// <param name="meetingSort">月报告HSE会议情况实体</param>
        public static void AddMeetingSort(Model.Manager_MeetingSortB meetingSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MeetingSortB));
            Model.Manager_MeetingSortB newMeetingSort = new Model.Manager_MeetingSortB
            {
                MeetingSortId = newKeyID,
                MonthReportId = meetingSort.MonthReportId,
                MeetingType = meetingSort.MeetingType,
                MeetingNumber = meetingSort.MeetingNumber,
                TotalMeetingNum = meetingSort.TotalMeetingNum,
                MeetingPersonNumber = meetingSort.MeetingPersonNumber,
                TotalMeetingPersonNum = meetingSort.TotalMeetingPersonNum
            };

            db.Manager_MeetingSortB.InsertOnSubmit(newMeetingSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE会议情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteMeetingSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_MeetingSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_MeetingSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
