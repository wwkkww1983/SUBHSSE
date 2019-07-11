using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 施工单位安全费用管理
    /// </summary>
    public static class HSSECostUnitManageService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostUnitManage GetHSSECostUnitManageByHSSECostUnitManageId(string hsseCostUnitManageId)
        {
            return Funs.DB.CostGoods_HSSECostUnitManage.FirstOrDefault(e => e.HSSECostUnitManageId == hsseCostUnitManageId);
        }

        /// <summary>
        /// 根据费用主键获取安全费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostUnitManage GetHSSECostUnitManageByHSSECostManageIdUnitId(string hsseCostManageId,string UnitId)
        {
            return Funs.DB.CostGoods_HSSECostUnitManage.FirstOrDefault(e => e.HSSECostManageId == hsseCostManageId && e.UnitId == UnitId);
        }

        /// <summary>
        /// 根据费用主键获取安全费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_HSSECostUnitManage> GetHSSECostUnitManageListByProjectIdUnitIdMonth(string ProjectId, string UnitId,DateTime? Months)
        {
            var list = (from x in Funs.DB.CostGoods_HSSECostUnitManage
                        join y in Funs.DB.CostGoods_HSSECostManage on x.HSSECostManageId equals y.HSSECostManageId
                        where y.ProjectId == ProjectId && y.Month < Months && x.UnitId == UnitId
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 根据费用主键获取安全费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostUnitManage GetHSSECostUnitManageByProjectIdUnitIdYearMonth(string ProjectId, string UnitId,int year, int month)
        {
            var unitManage = (from x in Funs.DB.CostGoods_HSSECostUnitManage
                        join y in Funs.DB.CostGoods_HSSECostManage on x.HSSECostManageId equals y.HSSECostManageId
                        where x.UnitId == UnitId &&
                          y.ProjectId == ProjectId && y.Month.Value.Year == year && y.Month.Value.Month == month
                        select x).FirstOrDefault();
            return unitManage;
        }

        /// <summary>
        /// 添加安全费用管理
        /// </summary>
        /// <param name="hsseCostUnitManage"></param>
        public static void AddHSSECostUnitManage(Model.CostGoods_HSSECostUnitManage hsseCostUnitManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostUnitManage newHSSECostUnitManage = new Model.CostGoods_HSSECostUnitManage
            {
                HSSECostUnitManageId = hsseCostUnitManage.HSSECostUnitManageId,
                HSSECostManageId = hsseCostUnitManage.HSSECostManageId,
                UnitId = hsseCostUnitManage.UnitId,
                EngineeringCost = hsseCostUnitManage.EngineeringCost,
                Remark1 = hsseCostUnitManage.Remark1,
                SubUnitCost = hsseCostUnitManage.SubUnitCost,
                Remark2 = hsseCostUnitManage.Remark2,
                AuditedSubUnitCost = hsseCostUnitManage.AuditedSubUnitCost,
                Remark3 = hsseCostUnitManage.Remark3,
                CostRatio = hsseCostUnitManage.CostRatio,
                Remark4 = hsseCostUnitManage.Remark4,
                CostA1 = hsseCostUnitManage.CostA1,
                CostA2 = hsseCostUnitManage.CostA2,
                CostA3 = hsseCostUnitManage.CostA3,
                CostA4 = hsseCostUnitManage.CostA4,
                CostA5 = hsseCostUnitManage.CostA5,
                CostA6 = hsseCostUnitManage.CostA6,
                CostA7 = hsseCostUnitManage.CostA7,
                CostA8 = hsseCostUnitManage.CostA8,
                CostB1 = hsseCostUnitManage.CostB1,
                CostB2 = hsseCostUnitManage.CostB2,
                CostC1 = hsseCostUnitManage.CostC1,
                CostD1 = hsseCostUnitManage.CostD1,
                CostD2 = hsseCostUnitManage.CostD2,
                CostD3 = hsseCostUnitManage.CostD3,
                CompileDate = hsseCostUnitManage.CompileDate,
                CompileManId = hsseCostUnitManage.CompileManId,
                States = hsseCostUnitManage.States,
                StateType = hsseCostUnitManage.StateType,
                AuditManId = hsseCostUnitManage.AuditManId,
                RatifiedManId = hsseCostUnitManage.RatifiedManId,
            };

            db.CostGoods_HSSECostUnitManage.InsertOnSubmit(newHSSECostUnitManage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改费用管理
        /// </summary>
        /// <param name="hsseCostUnitManage"></param>
        public static void UpdateHSSECostUnitManage(Model.CostGoods_HSSECostUnitManage hsseCostUnitManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostUnitManage newHSSECostUnitManage = db.CostGoods_HSSECostUnitManage.FirstOrDefault(e => e.HSSECostUnitManageId == hsseCostUnitManage.HSSECostUnitManageId);
            if (newHSSECostUnitManage != null)
            {
                newHSSECostUnitManage.EngineeringCost = hsseCostUnitManage.EngineeringCost;
                newHSSECostUnitManage.Remark1 = hsseCostUnitManage.Remark1;
                newHSSECostUnitManage.SubUnitCost = hsseCostUnitManage.SubUnitCost;
                newHSSECostUnitManage.Remark2 = hsseCostUnitManage.Remark2;
                newHSSECostUnitManage.AuditedSubUnitCost = hsseCostUnitManage.AuditedSubUnitCost;
                newHSSECostUnitManage.Remark3 = hsseCostUnitManage.Remark3;
                newHSSECostUnitManage.CostRatio = hsseCostUnitManage.CostRatio;
                newHSSECostUnitManage.Remark4 = hsseCostUnitManage.Remark4;
                newHSSECostUnitManage.CostA1 = hsseCostUnitManage.CostA1;
                newHSSECostUnitManage.CostA2 = hsseCostUnitManage.CostA2;
                newHSSECostUnitManage.CostA3 = hsseCostUnitManage.CostA3;
                newHSSECostUnitManage.CostA4 = hsseCostUnitManage.CostA4;
                newHSSECostUnitManage.CostA5 = hsseCostUnitManage.CostA5;
                newHSSECostUnitManage.CostA6 = hsseCostUnitManage.CostA6;
                newHSSECostUnitManage.CostA7 = hsseCostUnitManage.CostA7;
                newHSSECostUnitManage.CostA8 = hsseCostUnitManage.CostA8;
                newHSSECostUnitManage.CostB1 = hsseCostUnitManage.CostB1;
                newHSSECostUnitManage.CostB2 = hsseCostUnitManage.CostB2;
                newHSSECostUnitManage.CostC1 = hsseCostUnitManage.CostC1;
                newHSSECostUnitManage.CostD1 = hsseCostUnitManage.CostD1;
                newHSSECostUnitManage.CostD2 = hsseCostUnitManage.CostD2;
                newHSSECostUnitManage.CostD3 = hsseCostUnitManage.CostD3;
                newHSSECostUnitManage.States = hsseCostUnitManage.States;
                newHSSECostUnitManage.StateType = hsseCostUnitManage.StateType;

                newHSSECostUnitManage.AuditCostA1 = hsseCostUnitManage.AuditCostA1;
                newHSSECostUnitManage.AuditCostA2 = hsseCostUnitManage.AuditCostA2;
                newHSSECostUnitManage.AuditCostA3 = hsseCostUnitManage.AuditCostA3;
                newHSSECostUnitManage.AuditCostA4 = hsseCostUnitManage.AuditCostA4;
                newHSSECostUnitManage.AuditCostA5 = hsseCostUnitManage.AuditCostA5;
                newHSSECostUnitManage.AuditCostA6 = hsseCostUnitManage.AuditCostA6;
                newHSSECostUnitManage.AuditCostA7 = hsseCostUnitManage.AuditCostA7;
                newHSSECostUnitManage.AuditCostA8 = hsseCostUnitManage.AuditCostA8;

                newHSSECostUnitManage.RatifiedCostA1 = hsseCostUnitManage.RatifiedCostA1;
                newHSSECostUnitManage.RatifiedCostA2 = hsseCostUnitManage.RatifiedCostA2;
                newHSSECostUnitManage.RatifiedCostA3 = hsseCostUnitManage.RatifiedCostA3;
                newHSSECostUnitManage.RatifiedCostA4 = hsseCostUnitManage.RatifiedCostA4;
                newHSSECostUnitManage.RatifiedCostA5 = hsseCostUnitManage.RatifiedCostA5;
                newHSSECostUnitManage.RatifiedCostA6 = hsseCostUnitManage.RatifiedCostA6;
                newHSSECostUnitManage.RatifiedCostA7 = hsseCostUnitManage.RatifiedCostA7;
                newHSSECostUnitManage.RatifiedCostA8 = hsseCostUnitManage.RatifiedCostA8;

                newHSSECostUnitManage.AuditManId = hsseCostUnitManage.AuditManId;
                newHSSECostUnitManage.RatifiedManId = hsseCostUnitManage.RatifiedManId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据安全费用主键删除施工单位费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        public static void DeleteHSSECostUnitManageByHSSECostManageId(string hsseCostManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var hsseCostUnitManages = from x in db.CostGoods_HSSECostUnitManage where x.HSSECostManageId == hsseCostManageId select x;
            if (hsseCostUnitManages.Count() > 0)
            {
                foreach (var item in hsseCostUnitManages)
                {
                    DeleteHSSECostUnitManageById(item.HSSECostUnitManageId);
                }
            }
        }

        /// <summary>
        /// 根据施工单位费用主键删除施工单位费用管理
        /// </summary>
        /// <param name="hsseCostUnitManageId"></param>
        public static void DeleteHSSECostUnitManageById(string hsseCostUnitManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostUnitManage HSSECostUnitManage = db.CostGoods_HSSECostUnitManage.FirstOrDefault(e => e.HSSECostUnitManageId == hsseCostUnitManageId);
            if (HSSECostUnitManage != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(hsseCostUnitManageId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(hsseCostUnitManageId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(hsseCostUnitManageId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(hsseCostUnitManageId + "#2");
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(hsseCostUnitManageId + "#3");

                HSSECostUnitManageItemService.DeleteHSSECostUnitManageItemByHSSECostUnitManageId(HSSECostUnitManage.HSSECostUnitManageId);               
                db.CostGoods_HSSECostUnitManage.DeleteOnSubmit(HSSECostUnitManage);
                db.SubmitChanges();
            }
        }
    }
}
