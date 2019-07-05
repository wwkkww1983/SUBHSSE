using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 季节性/节假日检查
    /// </summary>
    public static class Check_CheckHolidayService
    {
        /// <summary>
        /// 根据季节性/节假日检查ID获取季节性/节假日检查信息
        /// </summary>
        /// <param name="CheckHolidayName"></param>
        /// <returns></returns>
        public static Model.Check_CheckHoliday GetCheckHolidayByCheckHolidayId(string checkHolidayId)
        {
            return Funs.DB.Check_CheckHoliday.FirstOrDefault(e => e.CheckHolidayId == checkHolidayId);
        }

        /// <summary>
        /// 根据时间段获取季节性/节假日检查信息集合
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static int GetCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckHoliday where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == projectId && x.States == BLL.Const.State_2 select x).Count();
        }

        /// <summary>
        /// 根据时间段获取已完成的季节性/节假日检查整改数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="projectId">项目号</param>
        /// <returns>已完成的季节性/节假日检查整改数量</returns>
        public static int GetIsOKViolationCountByCheckTime(DateTime startTime, DateTime endTime, string projectId)
        {
            return (from x in Funs.DB.Check_CheckHoliday
                    join y in Funs.DB.Check_CheckHolidayDetail on x.CheckHolidayId equals y.CheckHolidayId
                    where x.CheckTime >= startTime && x.CheckTime <= endTime && x.ProjectId == projectId
                    select y).Count();
        }

        /// <summary>
        /// 添加安全季节性/节假日检查
        /// </summary>
        /// <param name="checkHoliday"></param>
        public static void AddCheckHoliday(Model.Check_CheckHoliday checkHoliday)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckHoliday newCheckHoliday = new Model.Check_CheckHoliday
            {
                CheckHolidayId = checkHoliday.CheckHolidayId,
                CheckHolidayCode = checkHoliday.CheckHolidayCode,
                ProjectId = checkHoliday.ProjectId,
                Area = checkHoliday.Area,
                CheckTime = checkHoliday.CheckTime,
                ThisUnitId = checkHoliday.ThisUnitId,
                MainUnitPerson = checkHoliday.MainUnitPerson,
                SubUnits = checkHoliday.SubUnits,
                SubUnitPerson = checkHoliday.SubUnitPerson,
                MainUnitDeputy = checkHoliday.MainUnitDeputy,
                SubUnitDeputy = checkHoliday.SubUnitDeputy,
                MainUnitDeputyDate = checkHoliday.MainUnitDeputyDate,
                SubUnitDeputyDate = checkHoliday.SubUnitDeputyDate,
                AttachUrl = checkHoliday.AttachUrl,
                IsCompleted = checkHoliday.IsCompleted,
                States = checkHoliday.States,
                CompileMan = checkHoliday.CompileMan
            };
            db.Check_CheckHoliday.InsertOnSubmit(newCheckHoliday);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectCheckHolidayMenuId, checkHoliday.ProjectId, null, checkHoliday.CheckHolidayId, checkHoliday.CheckTime);
        }

        /// <summary>
        /// 修改安全季节性/节假日检查
        /// </summary>
        /// <param name="checkHoliday"></param>
        public static void UpdateCheckHoliday(Model.Check_CheckHoliday checkHoliday)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckHoliday newCheckHoliday = db.Check_CheckHoliday.FirstOrDefault(e => e.CheckHolidayId == checkHoliday.CheckHolidayId);
            if (newCheckHoliday != null)
            {
                newCheckHoliday.Area = checkHoliday.Area;
                newCheckHoliday.CheckTime = checkHoliday.CheckTime;
                newCheckHoliday.ThisUnitId = checkHoliday.ThisUnitId;
                newCheckHoliday.MainUnitPerson = checkHoliday.MainUnitPerson;
                newCheckHoliday.SubUnits = checkHoliday.SubUnits;
                newCheckHoliday.SubUnitPerson = checkHoliday.SubUnitPerson;
                newCheckHoliday.MainUnitDeputy = checkHoliday.MainUnitDeputy;
                newCheckHoliday.SubUnitDeputy = checkHoliday.SubUnitDeputy;
                newCheckHoliday.MainUnitDeputyDate = checkHoliday.MainUnitDeputyDate;
                newCheckHoliday.SubUnitDeputyDate = checkHoliday.SubUnitDeputyDate;
                newCheckHoliday.AttachUrl = checkHoliday.AttachUrl;
                newCheckHoliday.IsCompleted = checkHoliday.IsCompleted;
                newCheckHoliday.States = checkHoliday.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据季节性/节假日检查ID删除对应季节性/节假日检查记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteCheckHoliday(string checkHolidayId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckHoliday where x.CheckHolidayId == checkHolidayId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.CheckHolidayId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckHolidayId);
                ///删除工程师日志收集记录
                if (!string.IsNullOrEmpty(q.MainUnitPerson))
                {
                    List<string> mainUnitPersonIds = Funs.GetStrListByStr(q.MainUnitPerson, ',');
                    foreach (var item in mainUnitPersonIds)
                    {
                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item, q.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnDelete, 1);
                    }
                }
                if (!string.IsNullOrEmpty(q.SubUnitPerson))
                {
                    List<string> subUnitPersonIds = Funs.GetStrListByStr(q.SubUnitPerson, ',');
                    foreach (var item in subUnitPersonIds)
                    {
                        BLL.HSSELogService.CollectHSSELog(q.ProjectId, item, q.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnDelete, 1);
                    }
                }
                BLL.CommonService.DeleteFlowOperateByID(q.CheckHolidayId);
                db.Check_CheckHoliday.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
