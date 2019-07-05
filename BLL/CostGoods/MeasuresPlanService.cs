using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全措施费使用计划
    /// </summary>
    public static class MeasuresPlanService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全措施费使用计划
        /// </summary>
        /// <param name="measuresPlanId"></param>
        /// <returns></returns>
        public static Model.CostGoods_MeasuresPlan GetMeasuresPlanById(string measuresPlanId)
        {
            return Funs.DB.CostGoods_MeasuresPlan.FirstOrDefault(e => e.MeasuresPlanId == measuresPlanId);
        }

        /// <summary>
        /// 添加安全措施费使用计划
        /// </summary>
        /// <param name="measuresPlan"></param>
        public static void AddMeasuresPlan(Model.CostGoods_MeasuresPlan measuresPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_MeasuresPlan newMeasuresPlan = new Model.CostGoods_MeasuresPlan
            {
                MeasuresPlanId = measuresPlan.MeasuresPlanId,
                ProjectId = measuresPlan.ProjectId,
                MeasuresPlanCode = measuresPlan.MeasuresPlanCode,
                UnitId = measuresPlan.UnitId,
                FileContents = measuresPlan.FileContents,
                CompileMan = measuresPlan.CompileMan,
                CompileDate = measuresPlan.CompileDate
            };
            db.CostGoods_MeasuresPlan.InsertOnSubmit(newMeasuresPlan);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectMeasuresPlanMenuId, measuresPlan.ProjectId, measuresPlan.UnitId, measuresPlan.MeasuresPlanId, measuresPlan.CompileDate);
        }

        /// <summary>
        /// 修改安全措施费使用计划
        /// </summary>
        /// <param name="measuresPlan"></param>
        public static void UpdateMeasuresPlan(Model.CostGoods_MeasuresPlan measuresPlan)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_MeasuresPlan newMeasuresPlan = db.CostGoods_MeasuresPlan.FirstOrDefault(e => e.MeasuresPlanId == measuresPlan.MeasuresPlanId);
            if (newMeasuresPlan != null)
            {
                newMeasuresPlan.ProjectId = measuresPlan.ProjectId;
                newMeasuresPlan.MeasuresPlanCode = measuresPlan.MeasuresPlanCode;
                newMeasuresPlan.UnitId = measuresPlan.UnitId;
                newMeasuresPlan.FileContents = measuresPlan.FileContents;
                newMeasuresPlan.CompileMan = measuresPlan.CompileMan;
                newMeasuresPlan.CompileDate = measuresPlan.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全措施费使用计划
        /// </summary>
        /// <param name="measuresPlanId"></param>
        public static void DeleteMeasuresPlanById(string measuresPlanId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_MeasuresPlan measuresPlan = db.CostGoods_MeasuresPlan.FirstOrDefault(e => e.MeasuresPlanId == measuresPlanId);
            if (measuresPlan!=null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(measuresPlanId);
                CommonService.DeleteAttachFileById(measuresPlanId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(measuresPlanId);
                db.CostGoods_MeasuresPlan.DeleteOnSubmit(measuresPlan);
                db.SubmitChanges();
            }
        }
    }
}