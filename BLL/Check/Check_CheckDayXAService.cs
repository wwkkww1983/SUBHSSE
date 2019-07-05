using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 日常巡检（新奥）
    /// </summary>
    public static class Check_CheckDayXAService
    {
        /// <summary>
        /// 根据日常巡检ID获取日常巡检信息
        /// </summary>
        /// <param name="CheckDayName"></param>
        /// <returns></returns>
        public static Model.Check_CheckDayXA GetCheckDayByCheckDayId(string checkDayId)
        {
            return Funs.DB.Check_CheckDayXA.FirstOrDefault(e => e.CheckDayId == checkDayId);
        }

        /// <summary>
        /// 根据时间段获取日常巡检信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckDayXA where x.CheckDate >= startTime && x.CheckDate < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 添加安全日常巡检
        /// </summary>
        /// <param name="checkDay"></param>
        public static void AddCheckDay(Model.Check_CheckDayXA checkDay)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckDayXA newCheckDay = new Model.Check_CheckDayXA
            {
                CheckDayId = checkDay.CheckDayId,
                CheckDayCode = checkDay.CheckDayCode,
                ProjectId = checkDay.ProjectId,
                NotOKNum = checkDay.NotOKNum,
                DutyUnitIds = checkDay.DutyUnitIds,
                DutyTeamGroupIds = checkDay.DutyTeamGroupIds,
                WorkAreaIds = checkDay.WorkAreaIds,
                Unqualified = checkDay.Unqualified,
                CompileMan = checkDay.CompileMan,
                CompileUnit = checkDay.CompileUnit,
                CheckDate = checkDay.CheckDate,
                HandleStation = checkDay.HandleStation,
                IsOK = checkDay.IsOK,
                States = checkDay.States
            };
            db.Check_CheckDayXA.InsertOnSubmit(newCheckDay);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckDayXAMenuId, checkDay.ProjectId, null, checkDay.CheckDayId, checkDay.CheckDate);
        }

        /// <summary>
        /// 修改安全日常巡检
        /// </summary>
        /// <param name="checkDay"></param>
        public static void UpdateCheckDay(Model.Check_CheckDayXA checkDay)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckDayXA newCheckDay = db.Check_CheckDayXA.FirstOrDefault(e => e.CheckDayId == checkDay.CheckDayId);
            if (newCheckDay != null)
            {
                newCheckDay.CheckDayCode = checkDay.CheckDayCode;
                newCheckDay.NotOKNum = checkDay.NotOKNum;
                newCheckDay.DutyUnitIds = checkDay.DutyUnitIds;
                newCheckDay.DutyTeamGroupIds = checkDay.DutyTeamGroupIds;
                newCheckDay.WorkAreaIds = checkDay.WorkAreaIds;
                newCheckDay.Unqualified = checkDay.Unqualified;
                newCheckDay.CheckDate = checkDay.CheckDate;
                newCheckDay.HandleStation = checkDay.HandleStation;
                newCheckDay.IsOK = checkDay.IsOK;
                newCheckDay.States = checkDay.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据日常巡检ID删除对应日常巡检记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckDay(string checkDayId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckDayXA where x.CheckDayId == checkDayId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.CheckDayId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckDayId);
                BLL.CommonService.DeleteAttachFileById(q.CheckDayId + "1");  //整改情况附件
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(q.CheckDayId);
                db.Check_CheckDayXA.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
