using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 实施计划
    /// </summary>
    public static class ActionPlanSummaryService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取实施计划
        /// </summary>
        /// <param name="ActionPlanSummaryId"></param>
        /// <returns></returns>
        public static Model.ActionPlan_ActionPlanSummary GetActionPlanSummaryById(string ActionPlanSummaryId)
        {
            return Funs.DB.ActionPlan_ActionPlanSummary.FirstOrDefault(e => e.ActionPlanSummaryId == ActionPlanSummaryId);
        }

        /// <summary>
        /// 根据日期获取实施计划集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>实施计划集合</returns>
        public static List<Model.ActionPlan_ActionPlanSummary> GetActionPlanSummarysByDate(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.ActionPlan_ActionPlanSummary where x.CompileDate >= startTime && x.CompileDate <= endTime && x.ProjectId == projectId orderby x.CompileDate select x).ToList();
        }

        /// <summary>
        /// 添加实施计划
        /// </summary>
        /// <param name="ActionPlanSummary"></param>
        public static void AddActionPlanSummary(Model.ActionPlan_ActionPlanSummary ActionPlanSummary)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanSummary newActionPlanSummary = new Model.ActionPlan_ActionPlanSummary
            {
                ActionPlanSummaryId = ActionPlanSummary.ActionPlanSummaryId,
                ProjectId = ActionPlanSummary.ProjectId,
                UnitId = ActionPlanSummary.UnitId,
                Code = ActionPlanSummary.Code,
                Name = ActionPlanSummary.Name,
                Contents = ActionPlanSummary.Contents,
                CompileMan = ActionPlanSummary.CompileMan,
                CompileDate = ActionPlanSummary.CompileDate,
                States = ActionPlanSummary.States
            };
            db.ActionPlan_ActionPlanSummary.InsertOnSubmit(newActionPlanSummary);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectActionPlanSummaryMenuId, newActionPlanSummary.ProjectId, null, newActionPlanSummary.ActionPlanSummaryId, newActionPlanSummary.CompileDate);
        }

        /// <summary>
        /// 修改实施计划
        /// </summary>
        /// <param name="ActionPlanSummary"></param>
        public static void UpdateActionPlanSummary(Model.ActionPlan_ActionPlanSummary ActionPlanSummary)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanSummary newActionPlanSummary = db.ActionPlan_ActionPlanSummary.FirstOrDefault(e => e.ActionPlanSummaryId == ActionPlanSummary.ActionPlanSummaryId);
            if (newActionPlanSummary != null)
            {
                //newActionPlanSummary.ProjectId = ActionPlanSummary.ProjectId;
                newActionPlanSummary.UnitId = ActionPlanSummary.UnitId;
                newActionPlanSummary.Code = ActionPlanSummary.Code;
                newActionPlanSummary.Name = ActionPlanSummary.Name;
                newActionPlanSummary.Contents = ActionPlanSummary.Contents;
                newActionPlanSummary.CompileMan = ActionPlanSummary.CompileMan;
                newActionPlanSummary.CompileDate = ActionPlanSummary.CompileDate;
                newActionPlanSummary.States = ActionPlanSummary.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除实施计划
        /// </summary>
        /// <param name="ActionPlanSummaryId"></param>
        public static void DeleteActionPlanSummaryById(string ActionPlanSummaryId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ActionPlan_ActionPlanSummary ActionPlanSummary = db.ActionPlan_ActionPlanSummary.FirstOrDefault(e => e.ActionPlanSummaryId == ActionPlanSummaryId);
            if (ActionPlanSummary != null)
            {
                BLL.CommonService.DeleteAttachFileById(ActionPlanSummaryId);
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(ActionPlanSummary.ActionPlanSummaryId);
                ////删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(ActionPlanSummary.ActionPlanSummaryId);
                db.ActionPlan_ActionPlanSummary.DeleteOnSubmit(ActionPlanSummary);
                db.SubmitChanges();
            }
        }
    }
}
