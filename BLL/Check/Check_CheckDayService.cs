using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 日常巡检
    /// </summary>
    public static class Check_CheckDayService
    {
        /// <summary>
        /// 根据日常巡检ID获取日常巡检信息
        /// </summary>
        /// <param name="CheckDayName"></param>
        /// <returns></returns>
        public static Model.Check_CheckDay GetCheckDayByCheckDayId(string checkDayId)
        {
            return Funs.DB.Check_CheckDay.FirstOrDefault(e => e.CheckDayId == checkDayId);
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
            return (from x in Funs.DB.Check_CheckDay where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取已完成的日常巡检整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的日常巡检整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckDay
                    join y in Funs.DB.Check_CheckDayDetail on x.CheckDayId equals y.CheckDayId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId && y.CompleteStatus != null && y.CompleteStatus == true
                    select y).Count();
        }

        /// <summary>
        /// 添加安全日常巡检
        /// </summary>
        /// <param name="checkDay"></param>
        public static void AddCheckDay(Model.Check_CheckDay checkDay)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckDay newCheckDay = new Model.Check_CheckDay
            {
                CheckDayId = checkDay.CheckDayId,
                CheckDayCode = checkDay.CheckDayCode,
                ProjectId = checkDay.ProjectId,
                WeatherId = checkDay.WeatherId,
                CheckPerson = checkDay.CheckPerson,
                CheckTime = checkDay.CheckTime,
                DaySummary = checkDay.DaySummary,
                ScanUrl = checkDay.ScanUrl,
                States = checkDay.States,
                CompileMan = checkDay.CompileMan
            };
            db.Check_CheckDay.InsertOnSubmit(newCheckDay);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckDayMenuId, checkDay.ProjectId, null, checkDay.CheckDayId, checkDay.CheckTime);
        }

        /// <summary>
        /// 修改安全日常巡检
        /// </summary>
        /// <param name="checkDay"></param>
        public static void UpdateCheckDay(Model.Check_CheckDay checkDay)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckDay newCheckDay = db.Check_CheckDay.FirstOrDefault(e => e.CheckDayId == checkDay.CheckDayId);
            if (newCheckDay != null)
            {
                newCheckDay.CheckDayCode = checkDay.CheckDayCode;
                newCheckDay.WeatherId = checkDay.WeatherId;
                newCheckDay.CheckPerson = checkDay.CheckPerson;
                newCheckDay.CheckTime = checkDay.CheckTime;
                newCheckDay.DaySummary = checkDay.DaySummary;
                newCheckDay.ScanUrl = checkDay.ScanUrl;
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
            var checkDay = (from x in db.Check_CheckDay where x.CheckDayId == checkDayId select x).FirstOrDefault();
            if (checkDay != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(checkDay.CheckDayId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(checkDay.CheckDayId);
                ///删除工程师日志收集记录
                BLL.HSSELogService.CollectHSSELog(checkDay.ProjectId, checkDay.CheckPerson, checkDay.CheckTime, "21", "日常巡检", Const.BtnDelete, 1);
                BLL.CommonService.DeleteFlowOperateByID(checkDay.CheckDayId); 
                db.Check_CheckDay.DeleteOnSubmit(checkDay);
                db.SubmitChanges();
            }
        }
    }
}
