using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class HseCostCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable getListData(string monthReportId)
        {
            return from x in db.Manager_HseCostC
                   where x.MonthReportId == monthReportId
                   orderby x.UnitId
                   select new
                   {
                       UnitName = (from y in db.Base_Unit where y.UnitId == x.UnitId select y.UnitName).First(),
                       x.PlanCost,
                       x.RealCost,
                       x.TotalRealCost//=string.Format("{0:N2}",x.TotalRealCost)              
                   };
        }

        /// <summary>
        /// 根据月报告主键获取所有月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static List<Model.Manager_HseCostC> GetHseCostsByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_HseCostC where x.MonthReportId == monthReportId orderby x.UnitId select x).ToList();
        }

        /// <summary>
        /// 根据月报告主键获取总累计实际支出
        /// </summary>
        /// <param name="unitId">单位主键</param>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static decimal? GetSumTotalRealCostByMonthReportId(string monthReportId, string unitId)
        {
            return (from x in Funs.DB.Manager_HseCostC where x.MonthReportId == monthReportId && x.UnitId == unitId select x.TotalRealCost).FirstOrDefault();
        }

        /// <summary>
        /// 增加月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="hseCost">月报告HSE技术措施费用实体</param>
        public static void AddHseCost(Model.Manager_HseCostC hseCost)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HseCostC));
            Model.Manager_HseCostC newHseCost = new Model.Manager_HseCostC
            {
                HseCostId = newKeyID,
                MonthReportId = hseCost.MonthReportId,
                UnitId = hseCost.UnitId,
                PlanCost = hseCost.PlanCost,
                RealCost = hseCost.RealCost,
                TotalRealCost = hseCost.TotalRealCost
            };

            db.Manager_HseCostC.InsertOnSubmit(newHseCost);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除对应的所有月报告HSE技术措施费用信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteHseCostsByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_HseCostC where x.MonthReportId == monthReportId select x).ToList();
            db.Manager_HseCostC.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
