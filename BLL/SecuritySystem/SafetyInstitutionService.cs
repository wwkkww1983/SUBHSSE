using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全制度
    /// </summary>
    public static class SafetyInstitutionService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全制度信息
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        /// <returns></returns>
        public static Model.SecuritySystem_SafetyInstitution GetSafetyInstitutionById(string safetyInstitutionId)
        {
            return Funs.DB.SecuritySystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitutionId);
        }

        /// <summary>
        /// 添加安全制度
        /// </summary>
        /// <param name="safetyInstitution"></param>
        public static void AddSafetyInstitution(Model.SecuritySystem_SafetyInstitution safetyInstitution)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyInstitution newSafetyInstitution = new Model.SecuritySystem_SafetyInstitution
            {
                SafetyInstitutionId = safetyInstitution.SafetyInstitutionId,
                ProjectId = safetyInstitution.ProjectId,
                Title = safetyInstitution.Title,
                EffectiveDate = safetyInstitution.EffectiveDate,
                Scope = safetyInstitution.Scope,
                Remark = safetyInstitution.Remark,
                AttachUrl = safetyInstitution.AttachUrl,
                SeeFile = safetyInstitution.SeeFile,
                UnitId = safetyInstitution.UnitId
            };

            db.SecuritySystem_SafetyInstitution.InsertOnSubmit(newSafetyInstitution);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="safetyInstitution"></param>
        public static void UpdateSafetyInstitution(Model.SecuritySystem_SafetyInstitution safetyInstitution)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyInstitution newSafetyInstitution = db.SecuritySystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitution.SafetyInstitutionId);
            if (newSafetyInstitution != null)
            {
                newSafetyInstitution.Title = safetyInstitution.Title;
                newSafetyInstitution.EffectiveDate = safetyInstitution.EffectiveDate;
                newSafetyInstitution.Scope = safetyInstitution.Scope;
                newSafetyInstitution.Remark = safetyInstitution.Remark;
                newSafetyInstitution.AttachUrl = safetyInstitution.AttachUrl;
                newSafetyInstitution.SeeFile = safetyInstitution.SeeFile;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="safetyInstitutionId"></param>
        public static void DeleteSafetyInstitutionById(string safetyInstitutionId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SecuritySystem_SafetyInstitution safetyInstitution = db.SecuritySystem_SafetyInstitution.FirstOrDefault(e => e.SafetyInstitutionId == safetyInstitutionId);
            if (safetyInstitution != null)
            {
                if (!string.IsNullOrEmpty(safetyInstitution.AttachUrl))
                {
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, safetyInstitution.AttachUrl);//删除附件
                } 
                db.SecuritySystem_SafetyInstitution.DeleteOnSubmit(safetyInstitution);
                db.SubmitChanges();
            }
        }
    }
}