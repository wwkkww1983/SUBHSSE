using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class APIPersonQualityService
    {
        #region 根据identityCard获取人员资质信息
        /// <summary>
        /// 根据identityCard获取人员资质信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static Model.PersonQualityItem getPersonQualityInfo(string type, string dataId)
        {
            Model.PersonQualityItem getQualityItem = new Model.PersonQualityItem();
            if (type == "1")
            {
                ////特种作业人员
                getQualityItem = (from x in Funs.DB.QualityAudit_PersonQuality
                                  join y in Funs.DB.SitePerson_Person on x.PersonId equals y.PersonId
                                  where x.PersonQualityId == dataId
                                  orderby y.CardNo
                                  select new Model.PersonQualityItem
                                  {
                                      PersonQualityId = x.PersonQualityId,
                                      QualityType = type,
                                      PersonId = x.PersonId,
                                      PersonName = y.PersonName,
                                      CardNo = y.CardNo,
                                      IdentityCard = y.IdentityCard,
                                      ProjectId = y.ProjectId,
                                      UnitId = y.UnitId,
                                      UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                      UnitCode = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitCode,
                                      WorkPostId = y.WorkPostId,
                                      WorkPostName = Funs.DB.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                      CertificateId = x.CertificateId,
                                      CertificateName = Funs.DB.Base_Certificate.First(z => z.CertificateId == x.CertificateId).CertificateName,
                                      CertificateNo = x.CertificateNo,
                                      Grade = x.Grade,
                                      SendUnit = x.SendUnit,
                                      SendDate = string.Format("{0:yyyy-MM-dd}", x.SendDate),
                                      LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                      LateCheckDate = string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                      Remark = x.Remark,
                                      CompileMan = x.CompileMan,
                                      CompileManName = Funs.DB.Sys_User.First(z => z.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                      AuditorName = Funs.DB.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                      AuditOpinion = x.AuditOpinion,
                                      States = x.States,
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.PersonQualityId, null),
                                  }).FirstOrDefault();
            }
            //// 安管人员
            else if (type == "2")
            {
                getQualityItem = (from x in Funs.DB.QualityAudit_SafePersonQuality
                                  join y in Funs.DB.SitePerson_Person on x.PersonId equals y.PersonId
                                  where x.SafePersonQualityId == dataId
                                  orderby y.CardNo
                                  select new Model.PersonQualityItem
                                  {
                                      PersonQualityId = x.SafePersonQualityId,
                                      QualityType = type,
                                      PersonId = x.PersonId,
                                      PersonName = y.PersonName,
                                      CardNo = y.CardNo,
                                      IdentityCard = y.IdentityCard,
                                      ProjectId = y.ProjectId,
                                      UnitId = y.UnitId,
                                      UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                      UnitCode = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitCode,
                                      WorkPostId = y.WorkPostId,
                                      WorkPostName = Funs.DB.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                      //CertificateId = x.CertificateId,
                                      CertificateName = x.CertificateName,
                                      CertificateNo = x.CertificateNo,
                                      Grade = x.Grade,
                                      SendUnit = x.SendUnit,
                                      SendDate = string.Format("{0:yyyy-MM-dd}", x.SendDate),
                                      LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                      LateCheckDate = string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                      Remark = x.Remark,
                                      CompileMan = x.CompileMan,
                                      CompileManName = Funs.DB.Sys_User.First(z => z.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                      AuditorName = Funs.DB.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                      AuditOpinion = x.AuditOpinion,
                                      States = x.States,
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.SafePersonQualityId, null),
                                  }).FirstOrDefault();
            }
            else if (type == "3")
            {
                ////特种设备作业人员
                getQualityItem = (from x in Funs.DB.QualityAudit_EquipmentPersonQuality
                                  join y in Funs.DB.SitePerson_Person on x.PersonId equals y.PersonId
                                  where x.EquipmentPersonQualityId == dataId
                                  orderby y.CardNo
                                  select new Model.PersonQualityItem
                                  {
                                      PersonQualityId = x.EquipmentPersonQualityId,
                                      QualityType = type,
                                      PersonId = x.PersonId,
                                      PersonName = y.PersonName,
                                      CardNo = y.CardNo,
                                      IdentityCard = y.IdentityCard,
                                      ProjectId = y.ProjectId,
                                      UnitId = y.UnitId,
                                      UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                      UnitCode = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitCode,
                                      WorkPostId = y.WorkPostId,
                                      WorkPostName = Funs.DB.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                      CertificateId = x.CertificateId,
                                      CertificateName = Funs.DB.Base_Certificate.First(z => z.CertificateId == x.CertificateId).CertificateName,
                                      CertificateNo = x.CertificateNo,
                                      Grade = x.Grade,
                                      SendUnit = x.SendUnit,
                                      SendDate = string.Format("{0:yyyy-MM-dd}", x.SendDate),
                                      LimitDate = string.Format("{0:yyyy-MM-dd}", x.LimitDate),
                                      LateCheckDate = string.Format("{0:yyyy-MM-dd}", x.LateCheckDate),
                                      Remark = x.Remark,
                                      CompileMan = x.CompileMan,
                                      CompileManName = Funs.DB.Sys_User.First(z => z.UserId == x.CompileMan).UserName,
                                      CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                      AuditDate = string.Format("{0:yyyy-MM-dd}", x.AuditDate),
                                      AuditorName = Funs.DB.Sys_User.First(z => z.UserId == x.AuditorId).UserName,
                                      AuditOpinion = x.AuditOpinion,
                                      States = x.States,
                                      AttachUrl = APIUpLoadFileService.getFileUrl(x.EquipmentPersonQualityId, null),
                                  }).FirstOrDefault();
            }

            return getQualityItem;
        }
        #endregion

        #region 根据projectId、unitid获取特岗人员资质信息
        /// <summary>
        /// 根据projectId、unitid获取特岗人员资质信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitId">单位ID</param>
        /// <param name="qualityType">资质类型</param>
        /// <param name="states">0-待提交；1-待审核；2-已审核；-1打回</param>
        /// <param name="unitIdQ">查询单位ID</param>
        /// <returns></returns>
        public static List<Model.PersonQualityItem> getPersonQualityList(string projectId, string unitId, string qualityType, string states, string unitIdQ)
        {
            List<Model.PersonQualityItem> getLists = new List<Model.PersonQualityItem>();
            if (qualityType == "1")
            {
                getLists = (from x in Funs.DB.SitePerson_Person                            
                            join z in Funs.DB.Base_WorkPost on x.WorkPostId equals z.WorkPostId
                            join y in Funs.DB.QualityAudit_PersonQuality on x.PersonId equals  y.PersonId into jonPerson
                             from y in jonPerson.DefaultIfEmpty()
                            where x.ProjectId == projectId && z.PostType == Const.PostType_2
                            orderby x.CardNo
                            select new Model.PersonQualityItem
                            {
                                PersonQualityId = y.PersonQualityId,
                                QualityType = qualityType,
                                PersonId = x.PersonId,
                                PersonName = x.PersonName,
                                CardNo = x.CardNo,
                                IdentityCard = x.IdentityCard,
                                ProjectId = x.ProjectId,
                                UnitId = x.UnitId,
                                UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                CertificateId = y.CertificateId,
                                CertificateName = Funs.DB.Base_Certificate.First(z => z.CertificateId == y.CertificateId).CertificateName,
                                WorkPostId = x.WorkPostId,
                                WorkPostName = z.WorkPostName,
                                CertificateNo = y.CertificateNo,
                                Grade = y.Grade,
                                SendUnit = y.SendUnit,
                                SendDate = string.Format("{0:yyyy-MM-dd}", y.SendDate),
                                LimitDate = string.Format("{0:yyyy-MM-dd}", y.LimitDate),
                                LimitDateD = y.LimitDate,
                                LateCheckDate = string.Format("{0:yyyy-MM-dd}", y.LateCheckDate),
                                ApprovalPerson = y.ApprovalPerson,
                                Remark = y.Remark,
                                CompileMan = y.CompileMan,
                                CompileManName = Funs.DB.Sys_User.First(z => z.UserId == y.CompileMan).UserName,
                                CompileDate = string.Format("{0:yyyy-MM-dd}", y.CompileDate),
                                AuditDate = string.Format("{0:yyyy-MM-dd}", y.AuditDate),
                                AuditorName = Funs.DB.Sys_User.First(z => z.UserId == y.AuditorId).UserName,
                                AuditOpinion = y.AuditOpinion,
                                States = y.States,
                                AttachUrl = APIUpLoadFileService.getFileUrl(y.PersonQualityId, null),
                            }).ToList();
            }
            else if (qualityType == "2")
            {
                getLists = (from x in Funs.DB.SitePerson_Person
                            join z in Funs.DB.Base_WorkPost on x.WorkPostId equals z.WorkPostId
                            join y in Funs.DB.QualityAudit_SafePersonQuality on x.PersonId equals y.PersonId into jonPerson
                            from y in jonPerson.DefaultIfEmpty()
                            where x.ProjectId == projectId && z.IsHsse == true
                            orderby y.LimitDate
                            select new Model.PersonQualityItem
                            {
                                PersonQualityId = y.SafePersonQualityId,
                                QualityType = qualityType,
                                PersonId = x.PersonId,
                                PersonName = x.PersonName,
                                CardNo = x.CardNo,
                                IdentityCard = x.IdentityCard,
                                ProjectId = x.ProjectId,
                                UnitId = x.UnitId,
                                UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                //CertificateId = y.CertificateId,
                                CertificateName = y.CertificateName,
                                WorkPostId = x.WorkPostId,
                                WorkPostName = z.WorkPostName,
                                CertificateNo = y.CertificateNo,
                                Grade = y.Grade,
                                SendUnit = y.SendUnit,
                                SendDate = string.Format("{0:yyyy-MM-dd}", y.SendDate),
                                LimitDate = string.Format("{0:yyyy-MM-dd}", y.LimitDate),
                                LimitDateD = y.LimitDate,
                                LateCheckDate = string.Format("{0:yyyy-MM-dd}", y.LateCheckDate),
                                ApprovalPerson = y.ApprovalPerson,
                                Remark = y.Remark,
                                CompileMan = y.CompileMan,
                                CompileManName = Funs.DB.Sys_User.First(z => z.UserId == y.CompileMan).UserName,
                                CompileDate = string.Format("{0:yyyy-MM-dd}", y.CompileDate),
                                AuditDate = string.Format("{0:yyyy-MM-dd}", y.AuditDate),
                                AuditorName = Funs.DB.Sys_User.First(z => z.UserId == y.AuditorId).UserName,
                                AuditOpinion = y.AuditOpinion,
                                States = y.States,
                                AttachUrl = APIUpLoadFileService.getFileUrl(y.SafePersonQualityId, null),
                            }).ToList();
            }
            else if (qualityType == "3")
            {
                getLists = (from x in Funs.DB.SitePerson_Person
                            join z in Funs.DB.Base_WorkPost on x.WorkPostId equals z.WorkPostId
                            join y in Funs.DB.QualityAudit_EquipmentPersonQuality on x.PersonId equals y.PersonId into jonPerson
                            from y in jonPerson.DefaultIfEmpty()
                            where x.ProjectId == projectId && z.PostType == Const.PostType_5
                            orderby y.LimitDate
                            select new Model.PersonQualityItem
                            {
                                PersonQualityId = y.EquipmentPersonQualityId,
                                QualityType = qualityType,
                                PersonId = x.PersonId,
                                PersonName = x.PersonName,
                                CardNo = x.CardNo,
                                IdentityCard = x.IdentityCard,
                                ProjectId = x.ProjectId,
                                UnitId = x.UnitId,
                                UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == x.UnitId).UnitName,
                                CertificateId = y.CertificateId,
                                CertificateName = Funs.DB.Base_Certificate.First(z => z.CertificateId == y.CertificateId).CertificateName,
                                WorkPostId = x.WorkPostId,
                                WorkPostName = z.WorkPostName,
                                CertificateNo = y.CertificateNo,
                                Grade = y.Grade,
                                SendUnit = y.SendUnit,
                                SendDate = string.Format("{0:yyyy-MM-dd}", y.SendDate),
                                LimitDate = string.Format("{0:yyyy-MM-dd}", y.LimitDate),
                                LimitDateD = y.LimitDate,
                                LateCheckDate = string.Format("{0:yyyy-MM-dd}", y.LateCheckDate),
                                ApprovalPerson = y.ApprovalPerson,
                                Remark = y.Remark,
                                CompileMan = y.CompileMan,
                                CompileManName = Funs.DB.Sys_User.First(z => z.UserId == y.CompileMan).UserName,
                                CompileDate = string.Format("{0:yyyy-MM-dd}", y.CompileDate),
                                AuditDate = string.Format("{0:yyyy-MM-dd}", y.AuditDate),
                                AuditorName = Funs.DB.Sys_User.First(z => z.UserId == y.AuditorId).UserName,
                                AuditOpinion = y.AuditOpinion,
                                States = y.States,
                                AttachUrl = APIUpLoadFileService.getFileUrl(y.EquipmentPersonQualityId, null),
                            }).ToList();
            }

            if (ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(projectId, unitId))
            {
                getLists = getLists.Where(x => x.UnitId == unitId).ToList();
            }
            if (!string.IsNullOrEmpty(unitIdQ))
            {
                getLists = getLists.Where(x => x.UnitId == unitIdQ).ToList();
            }
            if (states == Const.State_0)
            {
                getLists = getLists.Where(x => x.PersonQualityId == null || x.LimitDateD < DateTime.Now.AddMonths(1)).ToList();
            }
            else if (states == Const.State_1)
            {
                getLists = getLists.Where(x => x.States == states).ToList();
            }
            else if (states == Const.State_2)
            {
                getLists = getLists.Where(x => x.States == states && x.LimitDateD >= DateTime.Now.AddMonths(1)).ToList();
            }
            else if (states == Const.State_R)
            {
                getLists = getLists.Where(x => x.States == states).ToList();
            }
            return getLists;
        }
        #endregion

        #region 人员资质信息保存方法
        /// <summary>
        /// 人员资质信息保存方法
        /// </summary>
        /// <param name="personQuality">人员信息</param>
        public static void SavePersonQuality(Model.PersonQualityItem personQuality)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                if (personQuality.QualityType == "1")
                {
                    Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                    {
                        PersonQualityId = personQuality.PersonQualityId,
                        PersonId = personQuality.PersonId,
                        CertificateNo = personQuality.CertificateNo,
                        CertificateName = personQuality.CertificateName,
                        Grade = personQuality.Grade,
                        SendUnit = personQuality.SendUnit,
                        SendDate = Funs.GetNewDateTime(personQuality.SendDate),
                        LimitDate = Funs.GetNewDateTime(personQuality.LimitDate),
                        LateCheckDate = Funs.GetNewDateTime(personQuality.LateCheckDate),
                        ApprovalPerson = personQuality.ApprovalPerson,
                        Remark = personQuality.Remark,
                        CompileDate = Funs.GetNewDateTime(personQuality.CompileDate),
                        AuditDate = Funs.GetNewDateTime(personQuality.AuditDate),
                        AuditOpinion = personQuality.AuditOpinion,
                        States = personQuality.States,
                    };

                    if (!string.IsNullOrEmpty(personQuality.CertificateId))
                    {
                        newPersonQuality.CertificateId = personQuality.CertificateId;
                    }
                    if (!string.IsNullOrEmpty(personQuality.CompileMan))
                    {
                        newPersonQuality.CompileMan = personQuality.CompileMan;
                    }
                    if (!string.IsNullOrEmpty(personQuality.AuditorId))
                    {
                        newPersonQuality.AuditorId = personQuality.AuditorId;
                    }
                    var getPersonQuality = db.QualityAudit_PersonQuality.FirstOrDefault(x => x.PersonQualityId == newPersonQuality.PersonQualityId);
                    if (getPersonQuality == null)
                    {
                        newPersonQuality.PersonQualityId = SQLHelper.GetNewID();
                        newPersonQuality.CompileDate = DateTime.Now;
                        db.QualityAudit_PersonQuality.InsertOnSubmit(newPersonQuality);
                        db.SubmitChanges();
                    }
                    else
                    {
                        newPersonQuality.PersonQualityId = getPersonQuality.PersonQualityId;
                        getPersonQuality.CertificateId = newPersonQuality.CertificateId;
                        getPersonQuality.CertificateNo = newPersonQuality.CertificateNo;
                        getPersonQuality.CertificateName = newPersonQuality.CertificateName;
                        getPersonQuality.Grade = newPersonQuality.Grade;
                        getPersonQuality.SendUnit = newPersonQuality.SendUnit;
                        getPersonQuality.SendDate = newPersonQuality.SendDate;
                        getPersonQuality.LimitDate = newPersonQuality.LimitDate;
                        getPersonQuality.LateCheckDate = newPersonQuality.LateCheckDate;
                        getPersonQuality.Remark = newPersonQuality.Remark;
                        getPersonQuality.AuditDate = newPersonQuality.AuditDate;
                        getPersonQuality.AuditorId = newPersonQuality.AuditorId;
                        db.SubmitChanges();
                    }
                    if (!string.IsNullOrEmpty(newPersonQuality.PersonQualityId))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.PersonQualityMenuId, newPersonQuality.PersonQualityId, personQuality.AttachUrl, "0");
                    }
                }
                else if (personQuality.QualityType == "2")
                {
                    Model.QualityAudit_SafePersonQuality newSafeQuality = new Model.QualityAudit_SafePersonQuality
                    {
                        SafePersonQualityId = personQuality.PersonQualityId,
                        PersonId = personQuality.PersonId,
                        CertificateNo = personQuality.CertificateNo,
                        CertificateName = personQuality.CertificateName,
                        Grade = personQuality.Grade,
                        SendUnit = personQuality.SendUnit,
                        SendDate = Funs.GetNewDateTime(personQuality.SendDate),
                        LimitDate = Funs.GetNewDateTime(personQuality.LimitDate),
                        LateCheckDate = Funs.GetNewDateTime(personQuality.LateCheckDate),
                        ApprovalPerson = personQuality.ApprovalPerson,
                        Remark = personQuality.Remark,
                        CompileDate = Funs.GetNewDateTime(personQuality.CompileDate),
                        AuditDate = Funs.GetNewDateTime(personQuality.AuditDate),
                        AuditOpinion = personQuality.AuditOpinion,
                        States = personQuality.States,
                    };

                    //if (!string.IsNullOrEmpty(personQuality.CertificateId))
                    //{
                    //    newSafeQuality.CertificateId = personQuality.CertificateId;
                    //}
                    if (!string.IsNullOrEmpty(personQuality.CompileMan))
                    {
                        newSafeQuality.CompileMan = personQuality.CompileMan;
                    }
                    if (!string.IsNullOrEmpty(personQuality.AuditorId))
                    {
                        newSafeQuality.AuditorId = personQuality.AuditorId;
                    }
                    var getPersonQuality = db.QualityAudit_SafePersonQuality.FirstOrDefault(x => x.SafePersonQualityId == newSafeQuality.SafePersonQualityId);
                    if (getPersonQuality == null)
                    {
                        newSafeQuality.SafePersonQualityId = SQLHelper.GetNewID();
                        newSafeQuality.CompileDate = DateTime.Now;
                        db.QualityAudit_SafePersonQuality.InsertOnSubmit(newSafeQuality);
                        db.SubmitChanges();
                    }
                    else
                    {
                        newSafeQuality.SafePersonQualityId = getPersonQuality.SafePersonQualityId;
                        //getPersonQuality.CertificateId = newSafeQuality.CertificateId;
                        getPersonQuality.CertificateNo = newSafeQuality.CertificateNo;
                        getPersonQuality.CertificateName = newSafeQuality.CertificateName;
                        getPersonQuality.Grade = newSafeQuality.Grade;
                        getPersonQuality.SendUnit = newSafeQuality.SendUnit;
                        getPersonQuality.SendDate = newSafeQuality.SendDate;
                        getPersonQuality.LimitDate = newSafeQuality.LimitDate;
                        getPersonQuality.LateCheckDate = newSafeQuality.LateCheckDate;
                        getPersonQuality.Remark = newSafeQuality.Remark;
                        getPersonQuality.AuditDate = newSafeQuality.AuditDate;
                        getPersonQuality.AuditorId = newSafeQuality.AuditorId;
                        db.SubmitChanges();
                    }
                    if (!string.IsNullOrEmpty(newSafeQuality.SafePersonQualityId))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.SafePersonQualityMenuId, newSafeQuality.SafePersonQualityId, personQuality.AttachUrl, "0");
                    }
                }
                //// 特种设备作业人员
                if (personQuality.QualityType == "3")
                {
                    Model.QualityAudit_EquipmentPersonQuality newEquipmentPersonQuality = new Model.QualityAudit_EquipmentPersonQuality
                    {
                        EquipmentPersonQualityId = personQuality.PersonQualityId,
                        PersonId = personQuality.PersonId,
                        CertificateNo = personQuality.CertificateNo,
                        CertificateName = personQuality.CertificateName,
                        Grade = personQuality.Grade,
                        SendUnit = personQuality.SendUnit,
                        SendDate = Funs.GetNewDateTime(personQuality.SendDate),
                        LimitDate = Funs.GetNewDateTime(personQuality.LimitDate),
                        LateCheckDate = Funs.GetNewDateTime(personQuality.LateCheckDate),
                        ApprovalPerson = personQuality.ApprovalPerson,
                        Remark = personQuality.Remark,
                        CompileDate = Funs.GetNewDateTime(personQuality.CompileDate),
                        AuditDate = Funs.GetNewDateTime(personQuality.AuditDate),
                        AuditOpinion = personQuality.AuditOpinion,
                        States = personQuality.States,
                    };

                    if (!string.IsNullOrEmpty(personQuality.CertificateId))
                    {
                        newEquipmentPersonQuality.CertificateId = personQuality.CertificateId;
                    }
                    if (!string.IsNullOrEmpty(personQuality.CompileMan))
                    {
                        newEquipmentPersonQuality.CompileMan = personQuality.CompileMan;
                    }
                    if (!string.IsNullOrEmpty(personQuality.AuditorId))
                    {
                        newEquipmentPersonQuality.AuditorId = personQuality.AuditorId;
                    }
                    var getEquipmentPersonQuality = db.QualityAudit_EquipmentPersonQuality.FirstOrDefault(x => x.EquipmentPersonQualityId == newEquipmentPersonQuality.EquipmentPersonQualityId);
                    if (getEquipmentPersonQuality == null)
                    {
                        newEquipmentPersonQuality.EquipmentPersonQualityId = SQLHelper.GetNewID();
                        newEquipmentPersonQuality.CompileDate = DateTime.Now;
                        db.QualityAudit_EquipmentPersonQuality.InsertOnSubmit(newEquipmentPersonQuality);
                        db.SubmitChanges();
                    }
                    else
                    {
                        newEquipmentPersonQuality.EquipmentPersonQualityId = getEquipmentPersonQuality.EquipmentPersonQualityId;
                        getEquipmentPersonQuality.CertificateId = newEquipmentPersonQuality.CertificateId;
                        getEquipmentPersonQuality.CertificateNo = newEquipmentPersonQuality.CertificateNo;
                        getEquipmentPersonQuality.CertificateName = newEquipmentPersonQuality.CertificateName;
                        getEquipmentPersonQuality.Grade = newEquipmentPersonQuality.Grade;
                        getEquipmentPersonQuality.SendUnit = newEquipmentPersonQuality.SendUnit;
                        getEquipmentPersonQuality.SendDate = newEquipmentPersonQuality.SendDate;
                        getEquipmentPersonQuality.LimitDate = newEquipmentPersonQuality.LimitDate;
                        getEquipmentPersonQuality.LateCheckDate = newEquipmentPersonQuality.LateCheckDate;
                        getEquipmentPersonQuality.Remark = newEquipmentPersonQuality.Remark;
                        getEquipmentPersonQuality.AuditDate = newEquipmentPersonQuality.AuditDate;
                        getEquipmentPersonQuality.AuditorId = newEquipmentPersonQuality.AuditorId;
                        db.SubmitChanges();
                    }
                    if (!string.IsNullOrEmpty(newEquipmentPersonQuality.EquipmentPersonQualityId))
                    {
                        APIUpLoadFileService.SaveAttachUrl(Const.EquipmentPersonQualityMenuId, newEquipmentPersonQuality.EquipmentPersonQualityId, personQuality.AttachUrl, "0");
                    }
                }
            }
        }
        #endregion
    }
}
