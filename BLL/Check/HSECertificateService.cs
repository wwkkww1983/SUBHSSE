using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 获奖证书或奖杯
    /// </summary>
    public static class HSECertificateService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取获奖证书或奖杯
        /// </summary>
        /// <param name="hSECertificateId"></param>
        /// <returns></returns>
        public static Model.Check_HSECertificate GetHSECertificateById(string hSECertificateId)
        {
            return Funs.DB.Check_HSECertificate.FirstOrDefault(e => e.HSECertificateId == hSECertificateId);
        }

        /// <summary>
        /// 添加获奖证书或奖杯
        /// </summary>
        /// <param name="hSECertificate"></param>
        public static void AddHSECertificate(Model.Check_HSECertificate hSECertificate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_HSECertificate newHSECertificate = new Model.Check_HSECertificate
            {
                HSECertificateId = hSECertificate.HSECertificateId,
                ProjectId = hSECertificate.ProjectId,
                HSECertificateCode = hSECertificate.HSECertificateCode,
                HSECertificateName = hSECertificate.HSECertificateName,
                AttachUrl = hSECertificate.AttachUrl,
                CompileMan = hSECertificate.CompileMan,
                CompileDate = hSECertificate.CompileDate,
                States = hSECertificate.States
            };
            db.Check_HSECertificate.InsertOnSubmit(newHSECertificate);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.ProjectHSECertificateMenuId, hSECertificate.ProjectId, null, hSECertificate.HSECertificateId, hSECertificate.CompileDate);
        }

        /// <summary>
        /// 修改获奖证书或奖杯
        /// </summary>
        /// <param name="hSECertificate"></param>
        public static void UpdateHSECertificate(Model.Check_HSECertificate hSECertificate)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_HSECertificate newHSECertificate = db.Check_HSECertificate.FirstOrDefault(e => e.HSECertificateId == hSECertificate.HSECertificateId);
            if (newHSECertificate != null)
            {
                //newHSECertificate.ProjectId = hSECertificate.ProjectId;
                newHSECertificate.HSECertificateCode = hSECertificate.HSECertificateCode;
                newHSECertificate.HSECertificateName = hSECertificate.HSECertificateName;
                newHSECertificate.AttachUrl = hSECertificate.AttachUrl;
                newHSECertificate.CompileMan = hSECertificate.CompileMan;
                newHSECertificate.CompileDate = hSECertificate.CompileDate;
                newHSECertificate.States = hSECertificate.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除获奖证书或奖杯
        /// </summary>
        /// <param name="hSECertificateId"></param>
        public static void DeleteHSECertificateById(string hSECertificateId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_HSECertificate hseCertificate = db.Check_HSECertificate.FirstOrDefault(e => e.HSECertificateId == hSECertificateId);
            if (hseCertificate != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(hSECertificateId);
                CommonService.DeleteFlowOperateByID(hSECertificateId);
                db.Check_HSECertificate.DeleteOnSubmit(hseCertificate);
                db.SubmitChanges();
            }
        }
    }
}
