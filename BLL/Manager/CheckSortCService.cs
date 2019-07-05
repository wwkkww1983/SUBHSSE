using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CheckSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_CheckSortC> GetCheckSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_CheckSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        public static Model.Manager_CheckSortC GetCheckSortByMonthReportIdAndCheckType(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortC where x.MonthReportId == monthReportId && x.CheckType == checkType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取总检查次数
        /// </summary>
        /// <param name="checkType">安全检查类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumCheckNumberByMonthReportId(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortC where x.MonthReportId == monthReportId && x.CheckType == checkType select x.CheckNumber).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总违章数量
        /// </summary>
        /// <param name="meetingType">安全检查类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumViolationNumberByMonthReportId(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckSortC where x.MonthReportId == monthReportId && x.CheckType == checkType select x.ViolationNumber).Sum();
        }

        /// <summary>
        /// 增加月报告HSE检查情况信息
        /// </summary>
        /// <param name="checkSort">月报告HSE检查情况实体</param>
        public static void AddCheckSort(Model.Manager_CheckSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_CheckSortC));
            Model.Manager_CheckSortC newCheckSort = new Model.Manager_CheckSortC
            {
                CheckSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                CheckType = checkSort.CheckType,
                CheckNumber = checkSort.CheckNumber,
                YearCheckNum = checkSort.YearCheckNum,
                TotalCheckNum = checkSort.TotalCheckNum,
                ViolationNumber = checkSort.ViolationNumber,
                YearViolationNum = checkSort.YearViolationNum
            };

            db.Manager_CheckSortC.InsertOnSubmit(newCheckSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE检查情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteCheckSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_CheckSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_CheckSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
