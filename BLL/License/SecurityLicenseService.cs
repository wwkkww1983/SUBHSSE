using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 新开项目作业许可证
    /// </summary>
    public static class SecurityLicenseService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 根据主键获取新开项目作业许可证
        /// </summary>
        /// <param name="securityLicenseId"></param>
        /// <returns></returns>
        public static Model.License_SecurityLicense GetSecurityLicenseById(string securityLicenseId)
        {
            return Funs.DB.License_SecurityLicense.FirstOrDefault(e => e.SecurityLicenseId == securityLicenseId);
        }

        /// <summary>
        /// 添加新开项目作业许可证
        /// </summary>
        /// <param name="securityLicense"></param>
        public static void AddSecurityLicense(Model.License_SecurityLicense securityLicense)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_SecurityLicense newSecurityLicense = new Model.License_SecurityLicense
            {
                SecurityLicenseId = securityLicense.SecurityLicenseId,
                ProjectId = securityLicense.ProjectId,
                SecurityLicenseCode = securityLicense.SecurityLicenseCode,
                SecurityLicenseName = securityLicense.SecurityLicenseName,
                NewProjectName = securityLicense.NewProjectName,
                NewProjectPart = securityLicense.NewProjectPart,
                UnitId = securityLicense.UnitId,
                LimitDate = securityLicense.LimitDate,
                SignMan = securityLicense.SignMan,
                SignDate = securityLicense.SignDate,
                ConfirmMan = securityLicense.ConfirmMan,
                AddMeasure = securityLicense.AddMeasure,
                AddMeasureMan = securityLicense.AddMeasureMan,
                SecurityLicenseContents = securityLicense.SecurityLicenseContents
            };
            db.License_SecurityLicense.InsertOnSubmit(newSecurityLicense);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectSecurityLicenseMenuId, securityLicense.ProjectId, null, securityLicense.SecurityLicenseId, securityLicense.SignDate);
        }

        /// <summary>
        /// 修改新开项目作业许可证
        /// </summary>
        /// <param name="securityLicense"></param>
        public static void UpdateSecurityLicense(Model.License_SecurityLicense securityLicense)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_SecurityLicense newSecurityLicense = db.License_SecurityLicense.FirstOrDefault(e => e.SecurityLicenseId == securityLicense.SecurityLicenseId);
            if (newSecurityLicense != null)
            {
                newSecurityLicense.ProjectId = securityLicense.ProjectId;
                newSecurityLicense.SecurityLicenseCode = securityLicense.SecurityLicenseCode;
                newSecurityLicense.SecurityLicenseName = securityLicense.SecurityLicenseName;
                newSecurityLicense.NewProjectName = securityLicense.NewProjectName;
                newSecurityLicense.NewProjectPart = securityLicense.NewProjectPart;
                newSecurityLicense.UnitId = securityLicense.UnitId;
                newSecurityLicense.LimitDate = securityLicense.LimitDate;
                newSecurityLicense.SignMan = securityLicense.SignMan;
                newSecurityLicense.SignDate = securityLicense.SignDate;
                newSecurityLicense.ConfirmMan = securityLicense.ConfirmMan;
                newSecurityLicense.AddMeasure = securityLicense.AddMeasure;
                newSecurityLicense.AddMeasureMan = securityLicense.AddMeasureMan;
                newSecurityLicense.SecurityLicenseContents = securityLicense.SecurityLicenseContents;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除新开项目作业许可证
        /// </summary>
        /// <param name="securityLicenseId"></param>
        public static void DeleteSecurityLicenseById(string securityLicenseId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.License_SecurityLicense securityLicense = db.License_SecurityLicense.FirstOrDefault(e => e.SecurityLicenseId == securityLicenseId);
            if (securityLicense != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(securityLicenseId);
                BLL.CommonService.DeleteAttachFileById(securityLicenseId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(securityLicenseId);  ////删除审核流程表
                db.License_SecurityLicense.DeleteOnSubmit(securityLicense);
                db.SubmitChanges();
            }
        }
    }
}