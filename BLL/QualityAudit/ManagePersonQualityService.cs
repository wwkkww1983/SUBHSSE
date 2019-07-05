using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理人员资质
    /// </summary>
    public static class ManagePersonQualityService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取管理人员资质
        /// </summary>
        /// <param name="ManagePersonQualityId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_ManagePersonQuality GetManagePersonQualityByPersonId(string personId)
        {
            return Funs.DB.QualityAudit_ManagePersonQuality.FirstOrDefault(e => e.PersonId == personId);
        }

        /// <summary>
        /// 添加管理人员资质
        /// </summary>
        /// <param name="ManagePersonQuality"></param>
        public static void AddManagePersonQuality(Model.QualityAudit_ManagePersonQuality ManagePersonQuality)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_ManagePersonQuality newManagePersonQuality = new Model.QualityAudit_ManagePersonQuality
            {
                ManagePersonQualityId = ManagePersonQuality.ManagePersonQualityId,
                PersonId = ManagePersonQuality.PersonId,
                CertificateNo = ManagePersonQuality.CertificateNo,
                CertificateName = ManagePersonQuality.CertificateName,
                Grade = ManagePersonQuality.Grade,
                SendUnit = ManagePersonQuality.SendUnit,
                SendDate = ManagePersonQuality.SendDate,
                LimitDate = ManagePersonQuality.LimitDate,
                LateCheckDate = ManagePersonQuality.LateCheckDate,
                ApprovalPerson = ManagePersonQuality.ApprovalPerson,
                Remark = ManagePersonQuality.Remark,
                CompileMan = ManagePersonQuality.CompileMan,
                CompileDate = ManagePersonQuality.CompileDate,
                AuditDate = ManagePersonQuality.AuditDate
            };
            db.QualityAudit_ManagePersonQuality.InsertOnSubmit(newManagePersonQuality);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改管理人员资质
        /// </summary>
        /// <param name="ManagePersonQuality"></param>
        public static void UpdateManagePersonQuality(Model.QualityAudit_ManagePersonQuality ManagePersonQuality)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_ManagePersonQuality newManagePersonQuality = db.QualityAudit_ManagePersonQuality.FirstOrDefault(e => e.ManagePersonQualityId == ManagePersonQuality.ManagePersonQualityId);
            if (newManagePersonQuality != null)
            {
                newManagePersonQuality.CertificateNo = ManagePersonQuality.CertificateNo;
                newManagePersonQuality.CertificateName = ManagePersonQuality.CertificateName;
                newManagePersonQuality.Grade = ManagePersonQuality.Grade;
                newManagePersonQuality.SendUnit = ManagePersonQuality.SendUnit;
                newManagePersonQuality.SendDate = ManagePersonQuality.SendDate;
                newManagePersonQuality.LimitDate = ManagePersonQuality.LimitDate;
                newManagePersonQuality.LateCheckDate = ManagePersonQuality.LateCheckDate;
                newManagePersonQuality.ApprovalPerson = ManagePersonQuality.ApprovalPerson;
                newManagePersonQuality.Remark = ManagePersonQuality.Remark;
                newManagePersonQuality.CompileMan = ManagePersonQuality.CompileMan;
                newManagePersonQuality.CompileDate = ManagePersonQuality.CompileDate;
                newManagePersonQuality.AuditDate = ManagePersonQuality.AuditDate;
                db.SubmitChanges();
            }
        }
    }
}
