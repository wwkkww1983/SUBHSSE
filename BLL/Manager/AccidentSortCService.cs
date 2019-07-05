using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AccidentSortCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        public static Model.Manager_AccidentSortC GetAccidentSortsByMonthReportIdAndAccidentType(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortC where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_AccidentSortC> GetAccidentSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_AccidentSortC where x.MonthReportId == monthReportId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总发生次数
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static int? GetSumNumberByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortC where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.Number).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总损失工时
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static int? GetSumLoseHoursByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortC where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.LoseHours).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总损失金额
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumLoseMoneyByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortC where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.LoseMoney).Sum();
        }

        /// <summary>
        /// 增加月报告事故分类信息
        /// </summary>
        /// <param name="accidentSort">月报告事故分类实体</param>
        public static void AddAccidentSort(Model.Manager_AccidentSortC accidentSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_AccidentSortC));
            Model.Manager_AccidentSortC newAccidentSort = new Model.Manager_AccidentSortC
            {
                AccidentSortId = newKeyID,
                MonthReportId = accidentSort.MonthReportId,
                AccidentType = accidentSort.AccidentType,
                TypeFlag = accidentSort.TypeFlag,
                Number = accidentSort.Number,
                TotalNum = accidentSort.TotalNum,
                LoseHours = accidentSort.LoseHours,
                TotalLoseHours = accidentSort.TotalLoseHours,
                LoseMoney = accidentSort.LoseMoney,
                TotalLoseMoney = accidentSort.TotalLoseMoney
            };

            db.Manager_AccidentSortC.InsertOnSubmit(newAccidentSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteAccidentSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_AccidentSortC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_AccidentSortC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
