using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 开工前检查
    /// </summary>
    public static class Check_CheckWorkService
    {
        /// <summary>
        /// 根据开工前检查ID获取开工前检查信息
        /// </summary>
        /// <param name="CheckWorkName"></param>
        /// <returns></returns>
        public static Model.Check_CheckWork GetCheckWorkByCheckWorkId(string checkWorkId)
        {
            return Funs.DB.Check_CheckWork.FirstOrDefault(e => e.CheckWorkId == checkWorkId);
        }

        /// <summary>
        /// 根据时间段获取开工前检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckWork where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取已完成的开工前检查整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的开工前检查整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckWork
                    join y in Funs.DB.Check_CheckWorkDetail on x.CheckWorkId equals y.CheckWorkId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId
                    select y).Count();
        }

        /// <summary>
        /// 添加安全开工前检查
        /// </summary>
        /// <param name="checkWork"></param>
        public static void AddCheckWork(Model.Check_CheckWork checkWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckWork newCheckWork = new Model.Check_CheckWork
            {
                CheckWorkId = checkWork.CheckWorkId,
                CheckWorkCode = checkWork.CheckWorkCode,
                ProjectId = checkWork.ProjectId,
                Area = checkWork.Area,
                CheckTime = checkWork.CheckTime,
                ThisUnitId = checkWork.ThisUnitId,
                MainUnitPerson = checkWork.MainUnitPerson,
                SubUnits = checkWork.SubUnits,
                SubUnitPerson = checkWork.SubUnitPerson,
                MainUnitDeputy = checkWork.MainUnitDeputy,
                SubUnitDeputy = checkWork.SubUnitDeputy,
                MainUnitDeputyDate = checkWork.MainUnitDeputyDate,
                SubUnitDeputyDate = checkWork.SubUnitDeputyDate,
                AttachUrl = checkWork.AttachUrl,
                IsCompleted = checkWork.IsCompleted,
                States = checkWork.States,
                CompileMan = checkWork.CompileMan,
                IsAgree = checkWork.IsAgree
            };
            db.Check_CheckWork.InsertOnSubmit(newCheckWork);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckWorkMenuId, checkWork.ProjectId, null, checkWork.CheckWorkId, checkWork.CheckTime);
        }

        /// <summary>
        /// 修改安全开工前检查
        /// </summary>
        /// <param name="checkWork"></param>
        public static void UpdateCheckWork(Model.Check_CheckWork checkWork)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckWork newCheckWork = db.Check_CheckWork.FirstOrDefault(e => e.CheckWorkId == checkWork.CheckWorkId);
            if (newCheckWork != null)
            {
                newCheckWork.Area = checkWork.Area;
                newCheckWork.CheckTime = checkWork.CheckTime;
                newCheckWork.ThisUnitId = checkWork.ThisUnitId;
                newCheckWork.MainUnitPerson = checkWork.MainUnitPerson;
                newCheckWork.SubUnits = checkWork.SubUnits;
                newCheckWork.SubUnitPerson = checkWork.SubUnitPerson;
                newCheckWork.MainUnitDeputy = checkWork.MainUnitDeputy;
                newCheckWork.SubUnitDeputy = checkWork.SubUnitDeputy;
                newCheckWork.MainUnitDeputyDate = checkWork.MainUnitDeputyDate;
                newCheckWork.SubUnitDeputyDate = checkWork.SubUnitDeputyDate;
                newCheckWork.AttachUrl = checkWork.AttachUrl;
                newCheckWork.IsCompleted = checkWork.IsCompleted;
                newCheckWork.States = checkWork.States;
                newCheckWork.IsAgree = checkWork.IsAgree;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据开工前检查ID删除对应开工前检查记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckWork(string checkWorkId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckWork where x.CheckWorkId == checkWorkId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.CheckWorkId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckWorkId);
                ///删除工程师日志收集记录
                if (!string.IsNullOrEmpty(q.MainUnitPerson))
                {
                    List<string> mainUnitPersonIds = Funs.GetStrListByStr(q.MainUnitPerson, ',');
                    foreach (var item in mainUnitPersonIds)
                    {
                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item, q.CheckTime, "21", "开工前HSE检查", Const.BtnDelete, 1);
                    }
                }
                if (!string.IsNullOrEmpty(q.SubUnitPerson))
                {
                    List<string> subUnitPersonIds = Funs.GetStrListByStr(q.SubUnitPerson, ',');
                    foreach (var item in subUnitPersonIds)
                    {
                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item, q.CheckTime, "21", "开工前HSE检查", Const.BtnDelete, 1);
                    }
                }
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(q.CheckWorkId);
                db.Check_CheckWork.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
