using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 采购供货厂家管理
    /// </summary>
    public static class InUnitService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取采购供货厂家管理
        /// </summary>
        /// <param name="inUnitId"></param>
        /// <returns></returns>
        public static Model.QualityAudit_InUnit GetInUnitById(string inUnitId)
        {
            return Funs.DB.QualityAudit_InUnit.FirstOrDefault(e => e.InUnitId == inUnitId);
        }

        /// <summary>
        /// 添加采购供货厂家管理
        /// </summary>
        /// <param name="inUnit"></param>
        public static void AddInUnit(Model.QualityAudit_InUnit inUnit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_InUnit newInUnit = new Model.QualityAudit_InUnit
            {
                InUnitId = inUnit.InUnitId,
                ProjectId = inUnit.ProjectId,
                InUnitCode = inUnit.InUnitCode,
                ManufacturerName = inUnit.ManufacturerName,
                HSEMan = inUnit.HSEMan,
                HeadTel = inUnit.HeadTel,
                InDate = inUnit.InDate,
                PersonCount = inUnit.PersonCount,
                TrainNum = inUnit.TrainNum,
                OutDate = inUnit.OutDate,
                BadgesIssued = inUnit.BadgesIssued,
                JobTicketValidity = inUnit.JobTicketValidity,
                TrainRecordsUrl = inUnit.TrainRecordsUrl,
                PlanUrl = inUnit.PlanUrl,
                TemporaryPersonUrl = inUnit.TemporaryPersonUrl,
                InPersonTrainUrl = inUnit.InPersonTrainUrl,
                Accommodation = inUnit.Accommodation,
                OperationTicket = inUnit.OperationTicket,
                LaborSituation = inUnit.LaborSituation,
                CompileMan = inUnit.CompileMan,
                CompileDate = inUnit.CompileDate,
                HSEAgreementUrl = inUnit.HSEAgreementUrl
            };
            db.QualityAudit_InUnit.InsertOnSubmit(newInUnit);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.InUnitMenuId, inUnit.ProjectId, null, inUnit.InUnitId, inUnit.CompileDate);
        }

        /// <summary>
        /// 修改采购供货厂家管理
        /// </summary>
        /// <param name="inUnit"></param>
        public static void UpdateInUnit(Model.QualityAudit_InUnit inUnit)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_InUnit newInUnit = db.QualityAudit_InUnit.FirstOrDefault(e => e.InUnitId == inUnit.InUnitId);
            if (newInUnit != null)
            {
                newInUnit.ProjectId = inUnit.ProjectId;
                newInUnit.InUnitCode = inUnit.InUnitCode;
                newInUnit.ManufacturerName = inUnit.ManufacturerName;
                newInUnit.HSEMan = inUnit.HSEMan;
                newInUnit.HeadTel = inUnit.HeadTel;
                newInUnit.InDate = inUnit.InDate;
                newInUnit.PersonCount = inUnit.PersonCount;
                newInUnit.TrainNum = inUnit.TrainNum;
                newInUnit.OutDate = inUnit.OutDate;
                newInUnit.BadgesIssued = inUnit.BadgesIssued;
                newInUnit.JobTicketValidity = inUnit.JobTicketValidity;
                newInUnit.TrainRecordsUrl = inUnit.TrainRecordsUrl;
                newInUnit.PlanUrl = inUnit.PlanUrl;
                newInUnit.TemporaryPersonUrl = inUnit.TemporaryPersonUrl;
                newInUnit.InPersonTrainUrl = inUnit.InPersonTrainUrl;
                newInUnit.Accommodation = inUnit.Accommodation;
                newInUnit.OperationTicket = inUnit.OperationTicket;
                newInUnit.LaborSituation = inUnit.LaborSituation;
                newInUnit.CompileMan = inUnit.CompileMan;
                newInUnit.CompileDate = inUnit.CompileDate;
                newInUnit.HSEAgreementUrl = inUnit.HSEAgreementUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除采购供货厂家管理
        /// </summary>
        /// <param name="inUnitId"></param>
        public static void DeleteInUnitById(string inUnitId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.QualityAudit_InUnit inUnit = db.QualityAudit_InUnit.FirstOrDefault(e => e.InUnitId == inUnitId);
            if (inUnit!=null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(inUnitId);
                if (!string.IsNullOrEmpty(inUnit.TrainRecordsUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, inUnit.TrainRecordsUrl);
                }
                if (!string.IsNullOrEmpty(inUnit.PlanUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, inUnit.PlanUrl);
                }
                if (!string.IsNullOrEmpty(inUnit.TemporaryPersonUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, inUnit.TemporaryPersonUrl);
                }
                if (!string.IsNullOrEmpty(inUnit.InPersonTrainUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, inUnit.InPersonTrainUrl);
                }
                if (!string.IsNullOrEmpty(inUnit.HSEAgreementUrl))
                {
                    UploadFileService.DeleteFile(Funs.RootPath, inUnit.HSEAgreementUrl);
                }
                db.QualityAudit_InUnit.DeleteOnSubmit(inUnit);
                db.SubmitChanges();
            }
        }
    }
}
