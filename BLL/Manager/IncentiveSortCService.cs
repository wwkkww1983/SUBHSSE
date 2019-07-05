using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class IncentiveSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        public static List<Model.Manager_IncentiveSortC> GetIncentiveSortsByMonthReportIdAndBigType(string monthReportId, string bigType)
        {
            return (from x in Funs.DB.Manager_IncentiveSortC where x.MonthReportId == monthReportId && x.BigType == bigType orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_IncentiveSortC> GetIncentiveSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_IncentiveSortC where x.MonthReportId == monthReportId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总奖惩金额
        /// </summary>
        /// <param name="incentiveType">奖惩类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumIncentiveMoneyByMonthReportId(string monthReportId, string incentiveType)
        {
            return (from x in Funs.DB.Manager_IncentiveSortC where x.MonthReportId == monthReportId && x.IncentiveType == incentiveType select x.IncentiveMoney).Sum();
        }

        /// <summary>
        /// 增加月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="incentiveSort">月报告HSE奖惩情况实体</param>
        public static void AddIncentiveSort(Model.Manager_IncentiveSortC incentiveSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortC));
            Model.Manager_IncentiveSortC newIncentiveSort = new Model.Manager_IncentiveSortC
            {
                IncentiveSortId = newKeyID,
                MonthReportId = incentiveSort.MonthReportId,
                IncentiveType = incentiveSort.IncentiveType,
                BigType = incentiveSort.BigType,
                IncentiveMoney = incentiveSort.IncentiveMoney,
                SortIndex = incentiveSort.SortIndex,
                IncentiveUnit = incentiveSort.IncentiveUnit,
                IncentiveDate = incentiveSort.IncentiveDate,
                IncentiveReason = incentiveSort.IncentiveReason,
                IncentiveBasis = incentiveSort.IncentiveBasis
            };

            db.Manager_IncentiveSortC.InsertOnSubmit(newIncentiveSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteIncentiveSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_IncentiveSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_IncentiveSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
