using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{

    /// <summary>
    /// 事故快报表
    /// </summary>
    public static class AccidentReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取事故快报
        /// </summary>
        /// <param name="accidentReportId"></param>
        /// <returns></returns>
        public static Model.ProjectAccident_AccidentReport GetAccidentReportById(string accidentReportId)
        {
            return Funs.DB.ProjectAccident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReportId);
        }

        /// <summary>
        /// 添加事故快报
        /// </summary>
        /// <param name="accidentReport"></param>
        public static void AddAccidentReport(Model.ProjectAccident_AccidentReport accidentReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentReport newAccidentReport = new Model.ProjectAccident_AccidentReport
            {
                AccidentReportId = accidentReport.AccidentReportId,
                ProjectId = accidentReport.ProjectId,
                UnitId = accidentReport.UnitId,
                WorkArea = accidentReport.WorkArea,
                CompileDate = accidentReport.CompileDate,
                AccidentDescription = accidentReport.AccidentDescription,
                Casualties = accidentReport.Casualties,
                AttachUrl = accidentReport.AttachUrl,
                States = accidentReport.States
            };
            db.ProjectAccident_AccidentReport.InsertOnSubmit(newAccidentReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改事故快报
        /// </summary>
        /// <param name="accidentReport"></param>
        public static void UpdateAccidentReport(Model.ProjectAccident_AccidentReport accidentReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentReport newAccidentReport = db.ProjectAccident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReport.AccidentReportId);
            if (newAccidentReport != null)
            {
                newAccidentReport.ProjectId = accidentReport.ProjectId;
                newAccidentReport.UnitId = accidentReport.UnitId;
                newAccidentReport.WorkArea = accidentReport.WorkArea;
                newAccidentReport.CompileDate = accidentReport.CompileDate;
                newAccidentReport.AccidentDescription = accidentReport.AccidentDescription;
                newAccidentReport.Casualties = accidentReport.Casualties;
                newAccidentReport.AttachUrl = accidentReport.AttachUrl;
                newAccidentReport.States = accidentReport.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除事故快报
        /// </summary>
        /// <param name="accidentReportId"></param>
        public static void DeleteAccidentReportById(string accidentReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.ProjectAccident_AccidentReport accidentReport = db.ProjectAccident_AccidentReport.FirstOrDefault(e => e.AccidentReportId == accidentReportId);
            if (accidentReport != null)
            {
                if (!string.IsNullOrEmpty(accidentReport.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, accidentReport.AttachUrl);//删除附件
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(accidentReport.AccidentReportId);
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(accidentReport.AccidentReportId);
                ////删除流程表
                BLL.CommonService.DeleteFlowOperateByID(accidentReport.AccidentReportId);

                db.ProjectAccident_AccidentReport.DeleteOnSubmit(accidentReport);
                db.SubmitChanges();
            }
        }
    }
}