using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 其他危险源辨识文件
    /// </summary>
    public static class Hazard_OtherHazardService
    {
        /// <summary>
        /// 根据其他危险源辨识文件ID获取其他危险源辨识文件信息
        /// </summary>
        /// <param name="OtherHazardName"></param>
        /// <returns></returns>
        public static Model.Hazard_OtherHazard GetOtherHazardByOtherHazardId(string registrationId)
        {
            return Funs.DB.Hazard_OtherHazard.FirstOrDefault(e => e.OtherHazardId == registrationId);
        }

        /// <summary>
        /// 根据项目主键和开始、结束时间获得其他危险源辨识的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetOtherHazardCountByProjectIdAndDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.Hazard_OtherHazard where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加安全其他危险源辨识文件
        /// </summary>
        /// <param name="registration"></param>
        public static void AddOtherHazard(Model.Hazard_OtherHazard registration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_OtherHazard newOtherHazard = new Model.Hazard_OtherHazard
            {
                OtherHazardId = registration.OtherHazardId,
                ProjectId = registration.ProjectId,
                OtherHazardCode = registration.OtherHazardCode,
                OtherHazardName = registration.OtherHazardName,
                AttachUrl = registration.AttachUrl,
                CompileMan = registration.CompileMan,
                CompileDate = registration.CompileDate,
                States = registration.States
            };

            db.Hazard_OtherHazard.InsertOnSubmit(newOtherHazard);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.OtherHazardMenuId, registration.ProjectId, null, registration.OtherHazardId, registration.CompileDate);
        }

        /// <summary>
        /// 修改安全其他危险源辨识文件
        /// </summary>
        /// <param name="registration"></param>
        public static void UpdateOtherHazard(Model.Hazard_OtherHazard registration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_OtherHazard newOtherHazard = db.Hazard_OtherHazard.FirstOrDefault(e => e.OtherHazardId == registration.OtherHazardId);
            if (newOtherHazard != null)
            {
                newOtherHazard.OtherHazardCode = registration.OtherHazardCode;
                newOtherHazard.OtherHazardName = registration.OtherHazardName;
                newOtherHazard.AttachUrl = registration.AttachUrl;
                newOtherHazard.CompileDate = registration.CompileDate;
                newOtherHazard.States = registration.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据其他危险源辨识文件ID删除对应其他危险源辨识文件记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteOtherHazard(string registrationId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Hazard_OtherHazard where x.OtherHazardId == registrationId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.OtherHazardId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.OtherHazardId);

                db.Hazard_OtherHazard.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
