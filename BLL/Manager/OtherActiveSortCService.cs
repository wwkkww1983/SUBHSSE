using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class OtherActiveSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取所有月报告其他HSE管理活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_OtherActiveSortC> GetOtherActiveSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_OtherActiveSortC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 增加月报告其他HSE管理活动信息
        /// </summary>
        /// <param name="checkSort">月报告其他HSE管理活动实体</param>
        public static void AddOtherActiveSort(Model.Manager_OtherActiveSortC checkSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_OtherActiveSortC));
            Model.Manager_OtherActiveSortC newOtherActiveSort = new Model.Manager_OtherActiveSortC
            {
                OtherActiveSortId = newKeyID,
                MonthReportId = checkSort.MonthReportId,
                SortIndex = checkSort.SortIndex,
                ActiveName = checkSort.ActiveName,
                Num = checkSort.Num,
                YearNum = checkSort.YearNum
            };

            db.Manager_OtherActiveSortC.InsertOnSubmit(newOtherActiveSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告其他HSE管理活动信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteOtherActiveSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_OtherActiveSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_OtherActiveSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
