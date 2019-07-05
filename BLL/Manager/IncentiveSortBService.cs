using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class IncentiveSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        public static Model.Manager_IncentiveSortB GetIncentiveSortByMonthReportIdAndIncentiveType(string monthReportId, string incentiveType)
        {
            return (from x in Funs.DB.Manager_IncentiveSortB where x.MonthReportId == monthReportId && x.IncentiveType == incentiveType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_IncentiveSortB> GetIncentiveSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_IncentiveSortB where x.MonthReportId == monthReportId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总奖惩金额
        /// </summary>
        /// <param name="incentiveType">奖惩类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumIncentiveMoneyByMonthReportId(string monthReportId, string incentiveType)
        {
            return (from x in Funs.DB.Manager_IncentiveSortB where x.MonthReportId == monthReportId && x.IncentiveType == incentiveType select x.IncentiveMoney).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总奖惩次数
        /// </summary>
        /// <param name="incentiveType">奖惩类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static int? GetSumIncentiveNumberByMonthReportId(string monthReportId, string incentiveType)
        {
            return (from x in Funs.DB.Manager_IncentiveSortB where x.MonthReportId == monthReportId && x.IncentiveType == incentiveType select x.TotalIncentiveNumber).FirstOrDefault();
        }

        /// <summary>
        /// 增加月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="incentiveSort">月报告HSE奖惩情况实体</param>
        public static void AddIncentiveSort(Model.Manager_IncentiveSortB incentiveSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_IncentiveSortB));
            Model.Manager_IncentiveSortB newIncentiveSort = new Model.Manager_IncentiveSortB
            {
                IncentiveSortId = newKeyID,
                MonthReportId = incentiveSort.MonthReportId,
                IncentiveType = incentiveSort.IncentiveType,
                BigType = incentiveSort.BigType,
                TypeFlag = incentiveSort.TypeFlag,
                IncentiveMoney = incentiveSort.IncentiveMoney,
                TotalIncentiveMoney = incentiveSort.TotalIncentiveMoney,
                IncentiveNumber = incentiveSort.IncentiveNumber,
                TotalIncentiveNumber = incentiveSort.TotalIncentiveNumber
            };

            db.Manager_IncentiveSortB.InsertOnSubmit(newIncentiveSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE奖惩情况信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteIncentiveSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_IncentiveSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_IncentiveSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
