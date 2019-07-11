using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 专项检查
    /// </summary>
    public static class Check_CheckSpecialService
    {
        /// <summary>
        /// 根据专项检查ID获取专项检查信息
        /// </summary>
        /// <param name="CheckSpecialName"></param>
        /// <returns></returns>
        public static Model.Check_CheckSpecial GetCheckSpecialByCheckSpecialId(string checkSpecialId)
        {
            return Funs.DB.Check_CheckSpecial.FirstOrDefault(e => e.CheckSpecialId == checkSpecialId);
        }

        /// <summary>
        /// 根据时间段获取专项检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取专项检查集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>专项检查集合</returns>
        public static List<Model.Check_CheckSpecial> GetListByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 根据时间段获取已完成的专项检查整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的专项检查整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckSpecial
                    join y in Funs.DB.Check_CheckSpecialDetail on x.CheckSpecialId equals y.CheckSpecialId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId && y.CompleteStatus != null && y.CompleteStatus == true
                    select y).Count();
        }

        /// <summary>
        /// 添加安全专项检查
        /// </summary>
        /// <param name="checkSpecial"></param>
        public static void AddCheckSpecial(Model.Check_CheckSpecial checkSpecial)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckSpecial newCheckSpecial = new Model.Check_CheckSpecial
            {
                CheckSpecialId = checkSpecial.CheckSpecialId,
                CheckSpecialCode = checkSpecial.CheckSpecialCode,
                ProjectId = checkSpecial.ProjectId,
                CheckPerson = checkSpecial.CheckPerson,
                CheckTime = checkSpecial.CheckTime,
                ScanUrl = checkSpecial.ScanUrl,
                DaySummary = checkSpecial.DaySummary,
                PartInUnits = checkSpecial.PartInUnits,
                PartInPersons = checkSpecial.PartInPersons,
                PartInPersonIds = checkSpecial.PartInPersonIds,
                PartInPersonNames=checkSpecial.PartInPersonNames,
                CheckAreas = checkSpecial.CheckAreas,
                States = checkSpecial.States,
                CompileMan = checkSpecial.CompileMan,
                CheckType = checkSpecial.CheckType
            };
            db.Check_CheckSpecial.InsertOnSubmit(newCheckSpecial);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckSpecialMenuId, checkSpecial.ProjectId, null, checkSpecial.CheckSpecialId, checkSpecial.CheckTime);

            if (!string.IsNullOrEmpty(newCheckSpecial.PartInPersonIds))
            { 
            }
        }

        /// <summary>
        /// 修改安全专项检查
        /// </summary>
        /// <param name="checkSpecial"></param>
        public static void UpdateCheckSpecial(Model.Check_CheckSpecial checkSpecial)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckSpecial newCheckSpecial = db.Check_CheckSpecial.FirstOrDefault(e => e.CheckSpecialId == checkSpecial.CheckSpecialId);
            if (newCheckSpecial != null)
            {
                newCheckSpecial.CheckSpecialCode = checkSpecial.CheckSpecialCode;
                //newCheckSpecial.ProjectId = checkSpecial.ProjectId;
                newCheckSpecial.CheckPerson = checkSpecial.CheckPerson;
                newCheckSpecial.CheckTime = checkSpecial.CheckTime;
                newCheckSpecial.ScanUrl = checkSpecial.ScanUrl;
                newCheckSpecial.DaySummary = checkSpecial.DaySummary;
                newCheckSpecial.PartInUnits = checkSpecial.PartInUnits;
                newCheckSpecial.PartInPersons = checkSpecial.PartInPersons;
                newCheckSpecial.PartInPersonIds = checkSpecial.PartInPersonIds;
                newCheckSpecial.PartInPersonNames = checkSpecial.PartInPersonNames;
                newCheckSpecial.CheckAreas = checkSpecial.CheckAreas;
                newCheckSpecial.States = checkSpecial.States;
                newCheckSpecial.CheckType = checkSpecial.CheckType;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据专项检查ID删除对应专项检查记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckSpecial(string checkSpecialId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckSpecial where x.CheckSpecialId == checkSpecialId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.CheckSpecialId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckSpecialId);
                ///删除工程师日志收集记录
                BLL.HSSELogService.CollectHSSELog(q.ProjectId, q.CheckPerson, q.CheckTime, "21", "专项检查", Const.BtnDelete, 1);
                if (!string.IsNullOrEmpty(q.PartInPersonIds))
                {
                    List<string> partInPersonIds = Funs.GetStrListByStr(q.PartInPersonIds, ',');
                    foreach (var item in partInPersonIds)
                    {
                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item, q.CheckTime, "21", "专项检查", Const.BtnDelete, 1);
                    }
                }
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(q.CheckSpecialId);
                db.Check_CheckSpecial.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
