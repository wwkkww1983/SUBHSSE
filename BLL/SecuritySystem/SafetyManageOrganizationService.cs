using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 项目安全管理组织机构
    /// </summary>
    public static class SafetyManageOrganizationService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全管理组织机构
        /// </summary>
        /// <param name="SafetyManageOrganizationId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetyManageOrganization GetSafetyManageOrganizationById(string SafetyManageOrganizationId)
        {
            return Funs.DB.SecuritySystem_SafetyManageOrganization.FirstOrDefault(e => e.SafetyManageOrganizationId == SafetyManageOrganizationId);
        }

        /// <summary>
        /// 根据上级Id获取安全管理组织机构集合
        /// </summary>
        /// <param name="supSafetyManageOrganizationId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetyManageOrganization GetSafetyManageOrganizationByProjectId(string projectId,string unitId)
        {
            return Funs.DB.SecuritySystem_SafetyManageOrganization.FirstOrDefault(e => e.ProjectId == projectId && e.UnitId == unitId);
        }

        /// <summary>
        /// 添加安全管理组织机构
        /// </summary>
        /// <param name="SafetyManageOrganization"></param>
        public static void AddSafetyManageOrganization(Model.SecuritySystem_SafetyManageOrganization SafetyManageOrganization)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyManageOrganization newSafetyManageOrganization = new Model.SecuritySystem_SafetyManageOrganization
            {
                SafetyManageOrganizationId = SafetyManageOrganization.SafetyManageOrganizationId,
                ProjectId = SafetyManageOrganization.ProjectId,
                UnitId = SafetyManageOrganization.UnitId,
                Remark = SafetyManageOrganization.Remark,
                AttachUrl = SafetyManageOrganization.AttachUrl,
                SeeFile = SafetyManageOrganization.SeeFile
            };
            db.SecuritySystem_SafetyManageOrganization.InsertOnSubmit(newSafetyManageOrganization);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全管理组织机构
        /// </summary>
        /// <param name="SafetyManageOrganization"></param>
        public static void UpdateSafetyManageOrganization(Model.SecuritySystem_SafetyManageOrganization SafetyManageOrganization)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyManageOrganization newSafetyManageOrganization = db.SecuritySystem_SafetyManageOrganization.FirstOrDefault(e => e.SafetyManageOrganizationId == SafetyManageOrganization.SafetyManageOrganizationId);
            if (newSafetyManageOrganization != null)
            {
                newSafetyManageOrganization.Remark = SafetyManageOrganization.Remark;
                newSafetyManageOrganization.AttachUrl = SafetyManageOrganization.AttachUrl;
                newSafetyManageOrganization.SeeFile = SafetyManageOrganization.SeeFile;
                newSafetyManageOrganization.UnitId = SafetyManageOrganization.UnitId;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全管理组织机构
        /// </summary>
        /// <param name="SafetyManageOrganizationId"></param>
        public static void DeleteSafetyManageOrganization(string SafetyManageOrganizationId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyManageOrganization SafetyManageOrganization = db.SecuritySystem_SafetyManageOrganization.FirstOrDefault(e => e.SafetyManageOrganizationId == SafetyManageOrganizationId);
            if (SafetyManageOrganization != null)
            {
                if (!string.IsNullOrEmpty(SafetyManageOrganization.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, SafetyManageOrganization.AttachUrl);//删除附件
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(SafetyManageOrganization.SafetyManageOrganizationId);

                db.SecuritySystem_SafetyManageOrganization.DeleteOnSubmit(SafetyManageOrganization);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全管理组织机构
        /// </summary>
        /// <param name="SafetyManageOrganizationId"></param>
        public static void DeleteSafetyManageOrganizationByProjectid(string projectid)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyManageOrganization SafetyManageOrganization = db.SecuritySystem_SafetyManageOrganization.FirstOrDefault(e => e.ProjectId == projectid);
            if (SafetyManageOrganization != null)
            {
                if (!string.IsNullOrEmpty(SafetyManageOrganization.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, SafetyManageOrganization.AttachUrl);//删除附件
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(SafetyManageOrganization.SafetyManageOrganizationId);

                db.SecuritySystem_SafetyManageOrganization.DeleteOnSubmit(SafetyManageOrganization);
                db.SubmitChanges();
            }
        }
    }
}