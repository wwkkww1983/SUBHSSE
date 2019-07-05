using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应急响应记录/评价
    /// </summary>
    public static class EmergencyResponseRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应急响应记录/评价
        /// </summary>
        /// <param name="EmergencyResponseRecordId"></param>
        /// <returns></returns>
        public static Model.Emergency_EmergencyResponseRecord GetEmergencyResponseRecordById(string fileId)
        {
            return Funs.DB.Emergency_EmergencyResponseRecord.FirstOrDefault(e => e.FileId == fileId);
        }

        /// <summary>
        /// 添加应急响应记录/评价
        /// </summary>
        /// <param name="EmergencyResponseRecord"></param>
        public static void AddEmergencyResponseRecord(Model.Emergency_EmergencyResponseRecord EmergencyResponseRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Emergency_EmergencyResponseRecord newEmergencyResponseRecord = new Model.Emergency_EmergencyResponseRecord
            {
                FileId = EmergencyResponseRecord.FileId,
                ProjectId = EmergencyResponseRecord.ProjectId,
                FileCode = EmergencyResponseRecord.FileCode,
                FileName = EmergencyResponseRecord.FileName,
                UnitId = EmergencyResponseRecord.UnitId,
                FileContent = EmergencyResponseRecord.FileContent,
                CompileMan = EmergencyResponseRecord.CompileMan,
                CompileDate = EmergencyResponseRecord.CompileDate,
                AttachUrl = EmergencyResponseRecord.AttachUrl,
                States = EmergencyResponseRecord.States
            };
            db.Emergency_EmergencyResponseRecord.InsertOnSubmit(newEmergencyResponseRecord);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectEmergencyResponseRecordMenuId, EmergencyResponseRecord.ProjectId, null, EmergencyResponseRecord.FileId, EmergencyResponseRecord.CompileDate);
        }

        /// <summary>
        /// 修改应急响应记录/评价
        /// </summary>
        /// <param name="EmergencyResponseRecord"></param>
        public static void UpdateEmergencyResponseRecord(Model.Emergency_EmergencyResponseRecord EmergencyResponseRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Emergency_EmergencyResponseRecord newEmergencyResponseRecord = db.Emergency_EmergencyResponseRecord.FirstOrDefault(e => e.FileId == EmergencyResponseRecord.FileId);
            if (newEmergencyResponseRecord != null)
            {
                newEmergencyResponseRecord.FileCode = EmergencyResponseRecord.FileCode;
                newEmergencyResponseRecord.FileName = EmergencyResponseRecord.FileName;
                newEmergencyResponseRecord.UnitId = EmergencyResponseRecord.UnitId;
                newEmergencyResponseRecord.FileContent = EmergencyResponseRecord.FileContent;
                newEmergencyResponseRecord.CompileMan = EmergencyResponseRecord.CompileMan;
                newEmergencyResponseRecord.CompileDate = EmergencyResponseRecord.CompileDate;
                newEmergencyResponseRecord.AttachUrl = EmergencyResponseRecord.AttachUrl;
                newEmergencyResponseRecord.States = EmergencyResponseRecord.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应急响应记录/评价
        /// </summary>
        /// <param name="FileId"></param>
        public static void DeleteEmergencyResponseRecordById(string FileId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Emergency_EmergencyResponseRecord EmergencyResponseRecord = db.Emergency_EmergencyResponseRecord.FirstOrDefault(e => e.FileId == FileId);
            if (EmergencyResponseRecord != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(EmergencyResponseRecord.FileId);
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(EmergencyResponseRecord.FileId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(EmergencyResponseRecord.FileId);
                db.Emergency_EmergencyResponseRecord.DeleteOnSubmit(EmergencyResponseRecord);
                db.SubmitChanges();
            }
        }
    }
}
