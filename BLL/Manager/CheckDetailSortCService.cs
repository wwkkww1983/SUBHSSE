using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class CheckDetailSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE检查明细情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_CheckDetailSortC> GetCheckDetailSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_CheckDetailSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        public static Model.Manager_CheckDetailSortC GetCheckDetailSortByMonthReportIdAndCheckType(string monthReportId, string checkType)
        {
            return (from x in Funs.DB.Manager_CheckDetailSortC where x.MonthReportId == monthReportId && x.CheckType == checkType select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加月报告HSE检查明细情况信息
        /// </summary>
        /// <param name="checkSort">月报告HSE检查明细情况实体</param>
        public static void AddCheckDetailSort(Model.Manager_CheckDetailSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_CheckDetailSortC));
            Model.Manager_CheckDetailSortC newCheckDetailSort = new Model.Manager_CheckDetailSortC
            {
                CheckDetailSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                CheckType = checkSort.CheckType,
                CheckTime = checkSort.CheckTime,
                JoinUnit = checkSort.JoinUnit,
                StateDef = checkSort.StateDef
            };

            db.Manager_CheckDetailSortC.InsertOnSubmit(newCheckDetailSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE检查明细情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteCheckDetailSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_CheckDetailSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_CheckDetailSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
