using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 危险观察登记
    /// </summary>
    public static class Hazard_RegistrationService
    {
        /// <summary>
        /// 根据危险观察登记ID获取危险观察登记信息
        /// </summary>
        /// <param name="RegistrationName"></param>
        /// <returns></returns>
        public static Model.Hazard_Registration GetRegistrationByRegistrationId(string registrationId)
        {
            return Funs.DB.Hazard_Registration.FirstOrDefault(e => e.RegistrationId == registrationId);
        }

        /// <summary>
        /// 根据项目主键和开始、结束时间获得危险观察登记的数量
        /// </summary>
        /// <param name="projectId">项目主键</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetHazardRegisterCountByProjectIdAndDate(string projectId, DateTime startTime, DateTime endTime)
        {
            var q = (from x in Funs.DB.Hazard_Registration where x.ProjectId == projectId && x.CheckTime >= startTime && x.CheckTime <= endTime select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 添加安全危险观察登记
        /// </summary>
        /// <param name="registration"></param>
        public static void AddRegistration(Model.Hazard_Registration registration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_Registration newRegistration = new Model.Hazard_Registration
            {
                RegistrationId = registration.RegistrationId,
                ProjectId = registration.ProjectId,
                WorkAreaId = registration.WorkAreaId,
                ProblemDescription = registration.ProblemDescription,
                ProblemTypes = registration.ProblemTypes,
                TakeSteps = registration.TakeSteps,
                ResponsibilityUnitId = registration.ResponsibilityUnitId,
                ResponsibilityManId = registration.ResponsibilityManId,
                RectificationPeriod = registration.RectificationPeriod,
                CheckManId = registration.CheckManId,
                CheckTime = registration.CheckTime,
                ImageUrl = registration.ImageUrl,
                RectificationImageUrl = registration.RectificationImageUrl,
                States = registration.States,
                RectificationTime = registration.RectificationTime,
                State = registration.State,
                HazardCode = registration.HazardCode,
                DefectsType = registration.DefectsType,
                MayLeadAccidents = registration.MayLeadAccidents,
                IsEffective = registration.IsEffective
            };

            db.Hazard_Registration.InsertOnSubmit(newRegistration);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.HazardRegisterMenuId, registration.ProjectId, null, registration.RegistrationId, registration.CheckTime);
        }

        /// <summary>
        /// 修改安全危险观察登记
        /// </summary>
        /// <param name="registration"></param>
        public static void UpdateRegistration(Model.Hazard_Registration registration)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Hazard_Registration newRegistration = db.Hazard_Registration.FirstOrDefault(e => e.RegistrationId == registration.RegistrationId);
            if (newRegistration != null)
            {
                newRegistration.WorkAreaId = registration.WorkAreaId;
                newRegistration.ProblemDescription = registration.ProblemDescription;
                newRegistration.ProblemTypes = registration.ProblemTypes;
                newRegistration.TakeSteps = registration.TakeSteps;
                newRegistration.ResponsibilityUnitId = registration.ResponsibilityUnitId;
                newRegistration.ResponsibilityManId = registration.ResponsibilityManId;
                newRegistration.RectificationPeriod = registration.RectificationPeriod;
                newRegistration.CheckTime = registration.CheckTime;
                newRegistration.ImageUrl = registration.ImageUrl;
                newRegistration.RectificationImageUrl = registration.RectificationImageUrl;
                newRegistration.States = registration.States;
                newRegistration.RectificationTime = registration.RectificationTime;
                newRegistration.State = registration.State;
                newRegistration.HazardCode = registration.HazardCode;
                newRegistration.DefectsType = registration.DefectsType;
                newRegistration.MayLeadAccidents = registration.MayLeadAccidents;
                newRegistration.IsEffective = registration.IsEffective;
                newRegistration.RectificationRemark = registration.RectificationRemark;
                newRegistration.ConfirmMan = registration.ConfirmMan;
                newRegistration.ConfirmDate = registration.ConfirmDate;
                newRegistration.HandleIdea = registration.HandleIdea;
                db.SubmitChanges();

                if (string.IsNullOrEmpty(BLL.CodeRecordsService.ReturnCodeByDataId(registration.RegistrationId)))
                {
                    ////增加一条编码记录
                    BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.HazardRegisterMenuId, registration.ProjectId, null, registration.RegistrationId, registration.CheckTime);
                }                
            }
        }

        /// <summary>
        /// 根据危险观察登记ID删除对应危险观察登记记录信息
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteRegistration(string registrationId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Hazard_Registration where x.RegistrationId == registrationId select x).FirstOrDefault();
            if (q != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(q.RegistrationId);
                ////删除附件表
                //BLL.CommonService.DeleteAttachFileById(q.RegistrationId);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, q.ImageUrl);
                BLL.UploadFileService.DeleteFile(Funs.RootPath, q.RectificationImageUrl);

                db.Hazard_Registration.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
