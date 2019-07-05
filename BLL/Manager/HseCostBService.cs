using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HseCostBService
    {
        /// <summary>
        /// 根据月报告主键获取所有月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_HseCostB> GetHseCostsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_HseCostB where x.MonthReportId == monthReportId orderby x.UnitId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键和单位Id获取所有月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <param name="unitId">单位Id</param>
        /// <returns></returns>
        public static Model.Manager_HseCostB GetHseCostsByMonthReportIdAndUnitId(string monthReportId, string unitId)
        {
            return (from x in Funs.DB.Manager_HseCostB where x.MonthReportId == monthReportId && x.UnitId == unitId select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据月报告主键获取年度累计实际支出
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        //public static decimal? GetSumYearRealCostByMonthReportId(string monthReportId, string unitId)
        //{
        //    return (from x in Funs.DB.Manager_HseCostB where x.MonthReportId == monthReportId && x.UnitId == unitId select x.YearRealCost).FirstOrDefault();
        //}

        /// <summary>
        /// 根据月报告主键获取总累计实际支出
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        //public static decimal? GetSumTotalRealCostByMonthReportId(string monthReportId, string unitId)
        //{
        //    return (from x in Funs.DB.Manager_HseCostB where x.MonthReportId == monthReportId && x.UnitId == unitId select x.TotalRealCost).FirstOrDefault();
        //}

        /// <summary>
        /// 增加月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="hseCost">月报告HSE技术措施费用实体</param>
        public static void AddHseCost(Model.Manager_HseCostB hseCost)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HseCostB));
            Model.Manager_HseCostB newHseCost = new Model.Manager_HseCostB
            {
                HseCostId = newKeyID,
                MonthReportId = hseCost.MonthReportId,
                UnitId = hseCost.UnitId,
                PlanCostA = hseCost.PlanCostA,
                PlanCostB = hseCost.PlanCostB,
                RealCostA = hseCost.RealCostA,
                ProjectRealCostA = hseCost.ProjectRealCostA,
                RealCostB = hseCost.RealCostB,
                ProjectRealCostB = hseCost.ProjectRealCostB,
                RealCostAB = hseCost.RealCostAB,
                ProjectRealCostAB = hseCost.ProjectRealCostAB
            };

            db.Manager_HseCostB.InsertOnSubmit(newHseCost);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteHseCostsByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_HseCostB where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_HseCostB.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
