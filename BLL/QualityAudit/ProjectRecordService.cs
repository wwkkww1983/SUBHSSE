using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目协议记录
    /// </summary>
    public static class ProjectRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取项目协议记录
        /// </summary>
        /// <param name="projectRecordId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_ProjectRecord GetProjectRecordById(string projectRecordId)
        {
            return Funs.DB.QualityAudit_ProjectRecord.FirstOrDefault(e => e.ProjectRecordId == projectRecordId);
        }

        /// <summary>
        /// 添加项目协议记录
        /// </summary>
        /// <param name="projectRecord"></param>
        public static void AddProjectRecord(Model.QualityAudit_ProjectRecord projectRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_ProjectRecord newProjectRecord = new Model.QualityAudit_ProjectRecord
            {
                ProjectRecordId = projectRecord.ProjectRecordId,
                UnitId = projectRecord.UnitId,
                ProjectId = projectRecord.ProjectId,
                ProjectRecordCode = projectRecord.ProjectRecordCode,
                ProjectRecordName = projectRecord.ProjectRecordName,
                Remark = projectRecord.Remark,
                CompileMan = projectRecord.CompileMan,
                CompileDate = projectRecord.CompileDate
            };
            db.QualityAudit_ProjectRecord.InsertOnSubmit(newProjectRecord);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectRecordMenuId, projectRecord.ProjectId, null, projectRecord.ProjectRecordId, projectRecord.CompileDate);
        }

        /// <summary>
        /// 修改项目协议记录
        /// </summary>
        /// <param name="projectRecord"></param>
        public static void UpdateProjectRecord(Model.QualityAudit_ProjectRecord projectRecord)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_ProjectRecord newProjectRecord = db.QualityAudit_ProjectRecord.FirstOrDefault(e => e.ProjectRecordId == projectRecord.ProjectRecordId);
            if (newProjectRecord != null)
            {
                newProjectRecord.ProjectId = projectRecord.ProjectId;
                newProjectRecord.ProjectRecordCode = projectRecord.ProjectRecordCode;
                newProjectRecord.ProjectRecordName = projectRecord.ProjectRecordName;
                newProjectRecord.Remark = projectRecord.Remark;
                newProjectRecord.CompileMan = projectRecord.CompileMan;
                newProjectRecord.CompileDate = projectRecord.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除项目协议记录
        /// </summary>
        /// <param name="projectRecordId"></param>
        public static void DeleteProjectRecordById(string projectRecordId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_ProjectRecord projectRecord = db.QualityAudit_ProjectRecord.FirstOrDefault(e => e.ProjectRecordId == projectRecordId);
            if (projectRecord != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(projectRecordId);
                CommonService.DeleteAttachFileById(projectRecordId);
                db.QualityAudit_ProjectRecord.DeleteOnSubmit(projectRecord);
                db.SubmitChanges();
            }
        }
    }
}