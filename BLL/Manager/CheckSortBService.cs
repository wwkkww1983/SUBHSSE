using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CheckSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_CheckSortB> GetCheckSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_CheckSortB where x.MonthReportId == monthReportId select x).ToList();
        }

        public static Model.Manager_CheckSortB GetCheckSortByMonthReportIdAndCheckType(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortB where x.MonthReportId == monthReportId && x.CheckType == checkType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取总检查次数
        /// </summary>
        /// <param name="checkType">安全检查类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumCheckNumberByMonthReportId(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortB where x.MonthReportId == monthReportId && x.CheckType == checkType select x.CheckNumber).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总违章数量
        /// </summary>
        /// <param name="meetingType">安全检查类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumViolationNumberByMonthReportId(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortB where x.MonthReportId == monthReportId && x.CheckType == checkType select x.ViolationNumber).Sum();
        }

        /// <summary>
        /// 增加月报告HSE检查情况信息
        /// </summary>
        /// <param name="checkSort">月报告HSE检查情况实体</param>
        public static void AddCheckSort(Model.Manager_CheckSortB checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortB));
            Model.Manager_CheckSortB newCheckSort = new Model.Manager_CheckSortB
            {
                CheckSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                CheckType = checkSort.CheckType,
                CheckNumber = checkSort.CheckNumber,
                TotalCheckNum = checkSort.TotalCheckNum,
                ViolationNumber = checkSort.ViolationNumber,
                TotalViolationNum = checkSort.TotalViolationNum
            };

            db.Manager_CheckSortB.InsertOnSubmit(newCheckSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteCheckSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_CheckSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_CheckSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
