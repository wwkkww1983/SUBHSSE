using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class IncentiveSortService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 增加月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="incentiveSort">月报告HSE奖惩情况实体</param>
        public static void AddIncentiveSort(Model.Manager_IncentiveSort incentiveSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSort));
            Model.Manager_IncentiveSort newIncentiveSort = new Model.Manager_IncentiveSort
            {
                IncentiveSortId = newKeyID,
                MonthReportId = incentiveSort.MonthReportId,
                IncentiveNumber01 = incentiveSort.IncentiveNumber01,
                IncentiveNumber02 = incentiveSort.IncentiveNumber02,
                IncentiveNumber03 = incentiveSort.IncentiveNumber03,
                IncentiveNumber04 = incentiveSort.IncentiveNumber04,
                IncentiveNumber05 = incentiveSort.IncentiveNumber05,
                IncentiveNumber06 = incentiveSort.IncentiveNumber06,
                IncentiveNumber07 = incentiveSort.IncentiveNumber07,
                IncentiveNumber11 = incentiveSort.IncentiveNumber11,
                IncentiveNumber12 = incentiveSort.IncentiveNumber12,
                IncentiveNumber13 = incentiveSort.IncentiveNumber13,
                IncentiveNumber14 = incentiveSort.IncentiveNumber14,
                IncentiveNumber15 = incentiveSort.IncentiveNumber15,
                IncentiveNumber16 = incentiveSort.IncentiveNumber16,
                IncentiveNumber17 = incentiveSort.IncentiveNumber17
            };

            db.Manager_IncentiveSort.InsertOnSubmit(newIncentiveSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteIncentiveSortsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_IncentiveSort where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_IncentiveSort.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
