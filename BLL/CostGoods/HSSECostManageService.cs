using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 安全费用管理
    /// </summary>
    public static class HSSECostManageService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="HSSECostManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostManage GetHSSECostManageByHSSECostManageId(string hsseCostManageId)
        {
            return Funs.DB.CostGoods_HSSECostManage.FirstOrDefault(e => e.HSSECostManageId == hsseCostManageId);
        }

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="HSSECostManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_HSSECostManage GetHSSECostManageByProjectIdMonth(string ProjectId, int year, int month)
        {
            return Funs.DB.CostGoods_HSSECostManage.FirstOrDefault(x => x.ProjectId == ProjectId && x.Month.Value.Year == year && x.Month.Value.Month == month); ;
        }

        /// <summary>
        /// 根据主键获取安全费用管理
        /// </summary>
        /// <param name="HSSECostManageId"></param>
        /// <returns></returns>
        public static List<Model.CostGoods_HSSECostManage> GetListHSSECostManageByProjectIdMonth(string ProjectId, DateTime? Months)
        {
            return (from x in Funs.DB.CostGoods_HSSECostManage
                    where x.ProjectId == ProjectId && x.Month < Months
                    select x).ToList();
        }

        /// <summary>
        /// 添加安全费用管理
        /// </summary>
        /// <param name="hsseCostManage"></param>
        public static void AddHSSECostManage(Model.CostGoods_HSSECostManage hsseCostManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostManage newHSSECostManage = new Model.CostGoods_HSSECostManage
            {
                HSSECostManageId = hsseCostManage.HSSECostManageId,
                ProjectId = hsseCostManage.ProjectId,
                Month = hsseCostManage.Month,
                Code = hsseCostManage.Code,
                ReportDate = hsseCostManage.ReportDate,
                MainIncome = hsseCostManage.MainIncome,
                Remark1 = hsseCostManage.Remark1,
                ConstructionIncome = hsseCostManage.ConstructionIncome,
                Remark2 = hsseCostManage.Remark2,
                SafetyCosts = hsseCostManage.SafetyCosts,
                Remark3 = hsseCostManage.Remark3,
                CompileDate = hsseCostManage.CompileDate,
                CompileManId = hsseCostManage.CompileManId,
            };
            db.CostGoods_HSSECostManage.InsertOnSubmit(newHSSECostManage);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectHSSECostManageMenuId, hsseCostManage.ProjectId, null, hsseCostManage.HSSECostManageId, hsseCostManage.Month);
        }

        /// <summary>
        /// 修改费用管理
        /// </summary>
        /// <param name="hsseCostManage"></param>
        public static void UpdateHSSECostManage(Model.CostGoods_HSSECostManage hsseCostManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostManage newHSSECostManage = db.CostGoods_HSSECostManage.FirstOrDefault(e => e.HSSECostManageId == hsseCostManage.HSSECostManageId);
            if (newHSSECostManage != null)
            {
                //newHSSECostManage.ProjectId = HSSECostManage.ProjectId;
                newHSSECostManage.Month = hsseCostManage.Month;
                newHSSECostManage.Code = hsseCostManage.Code;
                newHSSECostManage.ReportDate = hsseCostManage.ReportDate;
                newHSSECostManage.MainIncome = hsseCostManage.MainIncome;
                newHSSECostManage.Remark1 = hsseCostManage.Remark1;
                newHSSECostManage.ConstructionIncome = hsseCostManage.ConstructionIncome;
                newHSSECostManage.Remark2 = hsseCostManage.Remark2;
                newHSSECostManage.SafetyCosts = hsseCostManage.SafetyCosts;
                newHSSECostManage.Remark3 = hsseCostManage.Remark3;
                newHSSECostManage.States = hsseCostManage.States;
                newHSSECostManage.AuditManId = hsseCostManage.AuditManId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除费用管理
        /// </summary>
        /// <param name="HSSECostManageId"></param>
        public static void DeleteHSSECostManageById(string HSSECostManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_HSSECostManage HSSECostManage = db.CostGoods_HSSECostManage.FirstOrDefault(e => e.HSSECostManageId == HSSECostManageId);
            if (HSSECostManage != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(HSSECostManageId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(HSSECostManageId);//删除流程
                CommonService.DeleteFlowOperateByID(HSSECostManageId);   //删除审核流程
                CommonService.DeleteAttachFileById(HSSECostManageId);//删除附件
                db.CostGoods_HSSECostManage.DeleteOnSubmit(HSSECostManage);
                db.SubmitChanges();
            }
        }
    }
}
