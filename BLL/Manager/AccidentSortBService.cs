using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AccidentSortBService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        public static Model.Manager_AccidentSortB GetAccidentSortsByMonthReportIdAndAccidentType(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_AccidentSortB> GetAccidentSortsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where x.MonthReportId == monthReportId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键集合及类型获取所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_AccidentSortB> GetAccidentSortsByMonthReportIdsAndAccidentType(List<string> monthReportIds, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where monthReportIds.Contains(x.MonthReportId) && x.AccidentType == accidentType select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总发生次数
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static int? GetSumNumberByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.Number).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总损失工时
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static int? GetSumLoseHoursByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.LoseHours).Sum();
        }

        /// <summary>
        /// 根据月报告主键获取总损失金额
        /// </summary>
        /// <param name="AccidentType">事故类型</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumLoseMoneyByMonthReportId(string monthReportId, string accidentType)
        {
            return (from x in Funs.DB.Manager_AccidentSortB where x.MonthReportId == monthReportId && x.AccidentType == accidentType select x.LoseMoney).Sum();
        }

        /// <summary>
        /// 增加月报告事故分类信息
        /// </summary>
        /// <param name="accidentSort">月报告事故分类实体</param>
        public static void AddAccidentSort(Model.Manager_AccidentSortB accidentSort)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_AccidentSortB));
            Model.Manager_AccidentSortB newAccidentSort = new Model.Manager_AccidentSortB
            {
                AccidentSortId = newKeyID,
                MonthReportId = accidentSort.MonthReportId,
                AccidentType = accidentSort.AccidentType,
                TypeFlag = accidentSort.TypeFlag,
                Number = accidentSort.Number,
                TotalNum = accidentSort.TotalNum,
                PersonNum = accidentSort.PersonNum,
                TotalPersonNum = accidentSort.TotalPersonNum,
                LoseHours = accidentSort.LoseHours,
                TotalLoseHours = accidentSort.TotalLoseHours,
                LoseMoney = accidentSort.LoseMoney,
                TotalLoseMoney = accidentSort.TotalLoseMoney
            };

            db.Manager_AccidentSortB.InsertOnSubmit(newAccidentSort);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告事故分类信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteAccidentSortsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_AccidentSortB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_AccidentSortB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
