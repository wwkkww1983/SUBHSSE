using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目安全组织体系
    /// </summary>
    public static class SafetySystemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全组织体系
        /// </summary>
        /// <param name="SafetySystemId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetySystem GetSafetySystemById(string SafetySystemId)
        {
            return Funs.DB.SecuritySystem_SafetySystem.FirstOrDefault(e => e.SafetySystemId == SafetySystemId);
        }

        /// <summary>
        /// 根据上级Id获取安全组织体系集合
        /// </summary>
        /// <param name="supSafetySystemId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetySystem GetSafetySystemByProjectId(string projectId, string unitId)
        {
            return Funs.DB.SecuritySystem_SafetySystem.FirstOrDefault(e => e.ProjectId == projectId && e.UnitId == unitId);
        }

        /// <summary>
        /// 添加安全组织体系
        /// </summary>
        /// <param name="safetySystem"></param>
        public static void AddSafetySystem(Model.SecuritySystem_SafetySystem safetySystem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetySystem newSafetySystem = new Model.SecuritySystem_SafetySystem
            {
                SafetySystemId = safetySystem.SafetySystemId,
                ProjectId = safetySystem.ProjectId,
                Remark = safetySystem.Remark,
                AttachUrl = safetySystem.AttachUrl,
                SeeFile = safetySystem.SeeFile,
                UnitId = safetySystem.UnitId
            };
            db.SecuritySystem_SafetySystem.InsertOnSubmit(newSafetySystem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全组织体系
        /// </summary>
        /// <param name="safetySystem"></param>
        public static void UpdateSafetySystem(Model.SecuritySystem_SafetySystem safetySystem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetySystem newSafetySystem = db.SecuritySystem_SafetySystem.FirstOrDefault(e => e.SafetySystemId == safetySystem.SafetySystemId);
            if (newSafetySystem != null)
            {
                newSafetySystem.Remark = safetySystem.Remark;
                newSafetySystem.AttachUrl = safetySystem.AttachUrl;
                newSafetySystem.SeeFile = safetySystem.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全组织体系
        /// </summary>
        /// <param name="SafetySystemId"></param>
        public static void DeleteSafetySystem(string SafetySystemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetySystem safetySystem = db.SecuritySystem_SafetySystem.FirstOrDefault(e => e.SafetySystemId == SafetySystemId);
            if (safetySystem != null)
            {
                if (!string.IsNullOrEmpty(safetySystem.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, safetySystem.AttachUrl);//删除附件
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(safetySystem.SafetySystemId);

                db.SecuritySystem_SafetySystem.DeleteOnSubmit(safetySystem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全组织体系
        /// </summary>
        /// <param name="SafetySystemId"></param>
        public static void DeleteSafetySystemByProjectid(string projectid)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetySystem safetySystem = db.SecuritySystem_SafetySystem.FirstOrDefault(e => e.ProjectId == projectid);
            if (safetySystem != null)
            {
                if (!string.IsNullOrEmpty(safetySystem.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, safetySystem.AttachUrl);//删除附件
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(safetySystem.SafetySystemId);

                db.SecuritySystem_SafetySystem.DeleteOnSubmit(safetySystem);
                db.SubmitChanges();
            }
        }
    }
}