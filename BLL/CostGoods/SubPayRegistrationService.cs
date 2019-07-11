using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商安全费用投入登记
    /// </summary>
    public static class SubPayRegistrationService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取分包商安全费用投入登记
        /// </summary>
        /// <param name="subPayRegistrationId"></param>
        /// <returns></returns>
        public static Model.CostGoods_SubPayRegistration GetSubPayRegistrationById(string subPayRegistrationId)
        {
            return Funs.DB.CostGoods_SubPayRegistration.FirstOrDefault(e => e.SubPayRegistrationId == subPayRegistrationId);
        }

        /// <summary>
        /// 获取当年费用小计
        /// </summary>
        /// <param name="subPaydate"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_SubPayRegistration> GetSubPayRegistrationByYear(string projectId, DateTime subPaydate)
        {
            return (from x in db.CostGoods_SubPayRegistration where x.ProjectId == projectId && x.PayDate.Value.Year == subPaydate.Year select x).ToList();
        }

        /// <summary>
        /// 获取项目费用小计
        /// </summary>
        /// <param name="subPaydate"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_SubPayRegistration> GetSubPayRegistrationTotal(string projectId)
        {
            return (from x in db.CostGoods_SubPayRegistration where x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 添加分包商安全费用投入登记
        /// </summary>
        /// <param name="subPayRegistration"></param>
        public static void AddSubPayRegistration(Model.CostGoods_SubPayRegistration subPayRegistration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_SubPayRegistration newSubPayRegistration = new Model.CostGoods_SubPayRegistration();
            newSubPayRegistration.SubPayRegistrationId = subPayRegistration.SubPayRegistrationId;
            newSubPayRegistration.ProjectId = subPayRegistration.ProjectId;
            newSubPayRegistration.UnitId = subPayRegistration.UnitId;
            newSubPayRegistration.PayDate = subPayRegistration.PayDate;
            newSubPayRegistration.ContractNum = subPayRegistration.ContractNum;
            newSubPayRegistration.State = subPayRegistration.State;
            newSubPayRegistration.CompileMan = subPayRegistration.CompileMan;
            newSubPayRegistration.CompileDate = subPayRegistration.CompileDate;
            newSubPayRegistration.SMonthType1 = subPayRegistration.SMonthType1;
            newSubPayRegistration.SMainApproveType1 = subPayRegistration.SMainApproveType1;
            newSubPayRegistration.SMonthType2 = subPayRegistration.SMonthType2;
            newSubPayRegistration.SMainApproveType2 = subPayRegistration.SMainApproveType2;
            newSubPayRegistration.SMonthType3 = subPayRegistration.SMonthType3;
            newSubPayRegistration.SMainApproveType3 = subPayRegistration.SMainApproveType3;
            newSubPayRegistration.SMonthType4 = subPayRegistration.SMonthType4;
            newSubPayRegistration.SMainApproveType4 = subPayRegistration.SMainApproveType4;
            newSubPayRegistration.SMonthType5 = subPayRegistration.SMonthType5;
            newSubPayRegistration.SMainApproveType5 = subPayRegistration.SMainApproveType5;
            newSubPayRegistration.SMonthType6 = subPayRegistration.SMonthType6;
            newSubPayRegistration.SMainApproveType6 = subPayRegistration.SMainApproveType6;
            newSubPayRegistration.SMonthType7 = subPayRegistration.SMonthType7;
            newSubPayRegistration.SMainApproveType7 = subPayRegistration.SMainApproveType7;
            newSubPayRegistration.SMonthType8 = subPayRegistration.SMonthType8;
            newSubPayRegistration.SMainApproveType8 = subPayRegistration.SMainApproveType8;
            newSubPayRegistration.SMonthType9 = subPayRegistration.SMonthType9;
            newSubPayRegistration.SMainApproveType9 = subPayRegistration.SMainApproveType9;
            newSubPayRegistration.SMonthType10 = subPayRegistration.SMonthType10;
            newSubPayRegistration.SMainApproveType10 = subPayRegistration.SMainApproveType10;
            newSubPayRegistration.SMonthType11 = subPayRegistration.SMonthType11;
            newSubPayRegistration.SMainApproveType11 = subPayRegistration.SMainApproveType11;
            newSubPayRegistration.SMonthType12 = subPayRegistration.SMonthType12;
            newSubPayRegistration.SMainApproveType12 = subPayRegistration.SMainApproveType12;
            newSubPayRegistration.SMonthType13 = subPayRegistration.SMonthType13;
            newSubPayRegistration.SMainApproveType13 = subPayRegistration.SMainApproveType13;
            newSubPayRegistration.SMonthType14 = subPayRegistration.SMonthType14;
            newSubPayRegistration.SMainApproveType14 = subPayRegistration.SMainApproveType14;
            newSubPayRegistration.SMonthType15 = subPayRegistration.SMonthType15;
            newSubPayRegistration.SMainApproveType15 = subPayRegistration.SMainApproveType15;
            newSubPayRegistration.SMonthType16 = subPayRegistration.SMonthType16;
            newSubPayRegistration.SMainApproveType16 = subPayRegistration.SMainApproveType16;
            newSubPayRegistration.SMonthType17 = subPayRegistration.SMonthType17;
            newSubPayRegistration.SMainApproveType17 = subPayRegistration.SMainApproveType17;
            newSubPayRegistration.SMonthType18 = subPayRegistration.SMonthType18;
            newSubPayRegistration.SMainApproveType18 = subPayRegistration.SMainApproveType18;
            newSubPayRegistration.SMonthType19 = subPayRegistration.SMonthType19;
            newSubPayRegistration.SMainApproveType19 = subPayRegistration.SMainApproveType19;
            newSubPayRegistration.SMonthType20 = subPayRegistration.SMonthType20;
            newSubPayRegistration.SMainApproveType20 = subPayRegistration.SMainApproveType20;
            newSubPayRegistration.SMonthType21 = subPayRegistration.SMonthType21;
            newSubPayRegistration.SMainApproveType21 = subPayRegistration.SMainApproveType21;
            newSubPayRegistration.SMonthType22 = subPayRegistration.SMonthType22;
            newSubPayRegistration.SMainApproveType22 = subPayRegistration.SMainApproveType22;
            newSubPayRegistration.SMonthType23 = subPayRegistration.SMonthType23;
            newSubPayRegistration.SMainApproveType23 = subPayRegistration.SMainApproveType23;
            newSubPayRegistration.SMonthType24 = subPayRegistration.SMonthType24;
            newSubPayRegistration.SMainApproveType24 = subPayRegistration.SMainApproveType24;
            newSubPayRegistration.SMonthType25 = subPayRegistration.SMonthType25;
            newSubPayRegistration.SMainApproveType25 = subPayRegistration.SMainApproveType25;
            newSubPayRegistration.SMonthType26 = subPayRegistration.SMonthType26;
            newSubPayRegistration.SMainApproveType26 = subPayRegistration.SMainApproveType26;
            newSubPayRegistration.SMonthType27 = subPayRegistration.SMonthType27;
            newSubPayRegistration.SMainApproveType27 = subPayRegistration.SMainApproveType27;
            newSubPayRegistration.SMonthType28 = subPayRegistration.SMonthType28;
            newSubPayRegistration.SMainApproveType28 = subPayRegistration.SMainApproveType28;
            newSubPayRegistration.SMonthType29 = subPayRegistration.SMonthType29;
            newSubPayRegistration.SMainApproveType29 = subPayRegistration.SMainApproveType29;
            db.CostGoods_SubPayRegistration.InsertOnSubmit(newSubPayRegistration);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分包商安全费用投入登记
        /// </summary>
        /// <param name="subPayRegistration"></param>
        public static void UpdateSubPayRegistration(Model.CostGoods_SubPayRegistration subPayRegistration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_SubPayRegistration newSubPayRegistration = db.CostGoods_SubPayRegistration.FirstOrDefault(e => e.SubPayRegistrationId == subPayRegistration.SubPayRegistrationId);
            if (newSubPayRegistration != null)
            {
                newSubPayRegistration.ProjectId = subPayRegistration.ProjectId;
                newSubPayRegistration.UnitId = subPayRegistration.UnitId;
                newSubPayRegistration.PayDate = subPayRegistration.PayDate;
                newSubPayRegistration.ContractNum = subPayRegistration.ContractNum;
                newSubPayRegistration.State = subPayRegistration.State;
                newSubPayRegistration.SMonthType1 = subPayRegistration.SMonthType1;
                newSubPayRegistration.SMainApproveType1 = subPayRegistration.SMainApproveType1;
                newSubPayRegistration.SMonthType2 = subPayRegistration.SMonthType2;
                newSubPayRegistration.SMainApproveType2 = subPayRegistration.SMainApproveType2;
                newSubPayRegistration.SMonthType3 = subPayRegistration.SMonthType3;
                newSubPayRegistration.SMainApproveType3 = subPayRegistration.SMainApproveType3;
                newSubPayRegistration.SMonthType4 = subPayRegistration.SMonthType4;
                newSubPayRegistration.SMainApproveType4 = subPayRegistration.SMainApproveType4;
                newSubPayRegistration.SMonthType5 = subPayRegistration.SMonthType5;
                newSubPayRegistration.SMainApproveType5 = subPayRegistration.SMainApproveType5;
                newSubPayRegistration.SMonthType6 = subPayRegistration.SMonthType6;
                newSubPayRegistration.SMainApproveType6 = subPayRegistration.SMainApproveType6;
                newSubPayRegistration.SMonthType7 = subPayRegistration.SMonthType7;
                newSubPayRegistration.SMainApproveType7 = subPayRegistration.SMainApproveType7;
                newSubPayRegistration.SMonthType8 = subPayRegistration.SMonthType8;
                newSubPayRegistration.SMainApproveType8 = subPayRegistration.SMainApproveType8;
                newSubPayRegistration.SMonthType9 = subPayRegistration.SMonthType9;
                newSubPayRegistration.SMainApproveType9 = subPayRegistration.SMainApproveType9;
                newSubPayRegistration.SMonthType10 = subPayRegistration.SMonthType10;
                newSubPayRegistration.SMainApproveType10 = subPayRegistration.SMainApproveType10;
                newSubPayRegistration.SMonthType11 = subPayRegistration.SMonthType11;
                newSubPayRegistration.SMainApproveType11 = subPayRegistration.SMainApproveType11;
                newSubPayRegistration.SMonthType12 = subPayRegistration.SMonthType12;
                newSubPayRegistration.SMainApproveType12 = subPayRegistration.SMainApproveType12;
                newSubPayRegistration.SMonthType13 = subPayRegistration.SMonthType13;
                newSubPayRegistration.SMainApproveType13 = subPayRegistration.SMainApproveType13;
                newSubPayRegistration.SMonthType14 = subPayRegistration.SMonthType14;
                newSubPayRegistration.SMainApproveType14 = subPayRegistration.SMainApproveType14;
                newSubPayRegistration.SMonthType15 = subPayRegistration.SMonthType15;
                newSubPayRegistration.SMainApproveType15 = subPayRegistration.SMainApproveType15;
                newSubPayRegistration.SMonthType16 = subPayRegistration.SMonthType16;
                newSubPayRegistration.SMainApproveType16 = subPayRegistration.SMainApproveType16;
                newSubPayRegistration.SMonthType17 = subPayRegistration.SMonthType17;
                newSubPayRegistration.SMainApproveType17 = subPayRegistration.SMainApproveType17;
                newSubPayRegistration.SMonthType18 = subPayRegistration.SMonthType18;
                newSubPayRegistration.SMainApproveType18 = subPayRegistration.SMainApproveType18;
                newSubPayRegistration.SMonthType19 = subPayRegistration.SMonthType19;
                newSubPayRegistration.SMainApproveType19 = subPayRegistration.SMainApproveType19;
                newSubPayRegistration.SMonthType20 = subPayRegistration.SMonthType20;
                newSubPayRegistration.SMainApproveType20 = subPayRegistration.SMainApproveType20;
                newSubPayRegistration.SMonthType21 = subPayRegistration.SMonthType21;
                newSubPayRegistration.SMainApproveType21 = subPayRegistration.SMainApproveType21;
                newSubPayRegistration.SMonthType22 = subPayRegistration.SMonthType22;
                newSubPayRegistration.SMainApproveType22 = subPayRegistration.SMainApproveType22;
                newSubPayRegistration.SMonthType23 = subPayRegistration.SMonthType23;
                newSubPayRegistration.SMainApproveType23 = subPayRegistration.SMainApproveType23;
                newSubPayRegistration.SMonthType24 = subPayRegistration.SMonthType24;
                newSubPayRegistration.SMainApproveType24 = subPayRegistration.SMainApproveType24;
                newSubPayRegistration.SMonthType25 = subPayRegistration.SMonthType25;
                newSubPayRegistration.SMainApproveType25 = subPayRegistration.SMainApproveType25;
                newSubPayRegistration.SMonthType26 = subPayRegistration.SMonthType26;
                newSubPayRegistration.SMainApproveType26 = subPayRegistration.SMainApproveType26;
                newSubPayRegistration.SMonthType27 = subPayRegistration.SMonthType27;
                newSubPayRegistration.SMainApproveType27 = subPayRegistration.SMainApproveType27;
                newSubPayRegistration.SMonthType28 = subPayRegistration.SMonthType28;
                newSubPayRegistration.SMainApproveType28 = subPayRegistration.SMainApproveType28;
                newSubPayRegistration.SMonthType29 = subPayRegistration.SMonthType29;
                newSubPayRegistration.SMainApproveType29 = subPayRegistration.SMainApproveType29;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商安全费用投入登记
        /// </summary>
        /// <param name="subPayRegistrationId"></param>
        public static void DeleteSubPayRegistrationById(string subPayRegistrationId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_SubPayRegistration subPayRegistration = db.CostGoods_SubPayRegistration.FirstOrDefault(e => e.SubPayRegistrationId == subPayRegistrationId);
            if (subPayRegistration != null)
            {
                CommonService.DeleteFlowOperateByID(subPayRegistrationId);
                db.CostGoods_SubPayRegistration.DeleteOnSubmit(subPayRegistration);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据时间、项目获取分包商安全费用投入登记信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_SubPayRegistration> GetSubPayRegistrationByPayDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in db.CostGoods_SubPayRegistration where x.PayDate >= startTime && x.PayDate <= endTime && x.ProjectId == projectId select x).ToList();
        }

        public static decimal? GetSubPayRegistrationByUnitId(string unitId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.CostGoods_SubPayRegistration
                     where x.UnitId == unitId && x.PayDate >= startTime && x.PayDate < endTime
                     select x).ToList();
            if (q.Count > 0)
            {
                return q.Sum(e => (e.SMainApproveType1 + e.SMainApproveType2 + e.SMainApproveType3 + e.SMainApproveType4 + e.SMainApproveType5 + e.SMainApproveType6 + e.SMainApproveType7 + e.SMainApproveType8 + e.SMainApproveType9 + e.SMainApproveType10 + e.SMainApproveType11 + e.SMainApproveType12 + e.SMainApproveType13 + e.SMainApproveType14 + e.SMainApproveType15 + e.SMainApproveType16 + e.SMainApproveType17 + e.SMainApproveType18 + e.SMainApproveType19 + e.SMainApproveType20 + e.SMainApproveType21 + e.SMainApproveType22 + e.SMainApproveType23 + e.SMainApproveType24 + e.SMainApproveType25 + e.SMainApproveType26 + e.SMainApproveType27 + e.SMainApproveType28 + e.SMainApproveType29));
             }
            return null;
        }
    }
}
