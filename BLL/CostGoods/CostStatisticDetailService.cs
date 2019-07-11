using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class CostStatisticDetailService
    {
        //public static decimal? GetSumCostMoneyByCostStatisticCode(string costCode)
        //{
        //    return (from x in Funs.DB.TC_CostStatisticDetail where x.CostStatisticCode == costCode select x.CostMoney).Sum();
        //}

        public static List<Model.TC_CostStatisticDetail> GetCostStatisticDetailsByUnitId(string unitId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.TC_CostStatisticDetail
                    join y in Funs.DB.TC_CostStatistic on x.CostStatisticCode equals y.CostStatisticCode
                    where x.UnitId == unitId && y.Months >= startTime && y.Months < endTime
                    select x).Distinct().ToList();
        }
       
        public static Model.TC_CostStatisticDetail GetCostStatisticDetailByUnitId(string unitId, string costStatisticCode)
        {
            return (from x in Funs.DB.TC_CostStatisticDetail where x.CostStatisticCode == costStatisticCode && x.UnitId == unitId select x).FirstOrDefault();
        }

        public static List<Model.TC_CostStatisticDetail> GetCostStatisticDetailsByCostStatisticCode(string costStatisticCode)
        {
            return (from x in Funs.DB.TC_CostStatisticDetail where x.CostStatisticCode == costStatisticCode orderby x.UnitId select x).ToList();
        }

        /// <summary>
        /// 增加费用汇总明细信息
        /// </summary>
        /// <param name="pauseNotice">费用汇总明细实体</param>
        public static void AddCostStatisticDetail(Model.TC_CostStatisticDetail detail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string CostStatisticDetailId = SQLHelper.GetNewID(typeof(Model.TC_CostStatisticDetail));
            Model.TC_CostStatisticDetail newtc_CostDetail = new Model.TC_CostStatisticDetail
            {
                CostStatisticDetailId = CostStatisticDetailId,
                CostStatisticCode = detail.CostStatisticCode,
                UnitId = detail.UnitId,
                A1 = detail.A1,
                YA1 = detail.YA1,
                PA1 = detail.PA1,
                A2 = detail.A2,
                YA2 = detail.YA2,
                PA2 = detail.PA2,
                A3 = detail.A3,
                YA3 = detail.YA3,
                PA3 = detail.PA3,
                A4 = detail.A4,
                YA4 = detail.YA4,
                PA4 = detail.PA4,
                A5 = detail.A5,
                YA5 = detail.YA5,
                PA5 = detail.PA5,
                A6 = detail.A6,
                YA6 = detail.YA6,
                PA6 = detail.PA6,
                A = detail.A,
                YA = detail.YA,
                PA = detail.PA,
                B1 = detail.B1,
                YB1 = detail.YB1,
                PB1 = detail.PB1,
                B2 = detail.B2,
                YB2 = detail.YB2,
                PB2 = detail.PB2,
                B3 = detail.B3,
                YB3 = detail.YB3,
                PB3 = detail.PB3,
                B = detail.B,
                YB = detail.YB,
                PB = detail.PB,
                AB = detail.AB,
                YAB = detail.YAB,
                PAB = detail.PAB
            };
            db.TC_CostStatisticDetail.InsertOnSubmit(newtc_CostDetail);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据费用汇总编号删除对应的费用汇总明细信息
        /// </summary>
        /// <param name="actionPlanCode">施计划编号</param>
        public static void DeleteCostStatisticDetailByCostStatisticCode(string costStatisticCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.TC_CostStatisticDetail where x.CostStatisticCode == costStatisticCode select x).ToList();
            db.TC_CostStatisticDetail.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
