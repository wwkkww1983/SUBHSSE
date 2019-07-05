using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全监督检查报告表
    /// </summary>
    public static class SuperviseCheckReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查报告
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        /// <returns></returns>
        public static Model.Supervise_SuperviseCheckReport GetSuperviseCheckReportById(string superviseCheckReportId)
        {
            return Funs.DB.Supervise_SuperviseCheckReport.FirstOrDefault(e => e.SuperviseCheckReportId == superviseCheckReportId);
        }

        /// <summary>
        /// 添加安全监督检查报告
        /// </summary>
        /// <param name="superviseCheckReport"></param>
        public static void AddSuperviseCheckReport(Model.Supervise_SuperviseCheckReport superviseCheckReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckReport newSuperviseCheckReport = new Model.Supervise_SuperviseCheckReport
            {
                SuperviseCheckReportId = superviseCheckReport.SuperviseCheckReportId,
                SuperviseCheckReportCode = superviseCheckReport.SuperviseCheckReportCode,
                CheckDate = superviseCheckReport.CheckDate,
                ProjectId = superviseCheckReport.ProjectId,
                UnitId = superviseCheckReport.UnitId,
                CheckTeam = superviseCheckReport.CheckTeam,
                EvaluationResult = superviseCheckReport.EvaluationResult,
                AttachUrl = superviseCheckReport.AttachUrl,
                IsIssued = superviseCheckReport.IsIssued
            };
            db.Supervise_SuperviseCheckReport.InsertOnSubmit(newSuperviseCheckReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全监督检查报告
        /// </summary>
        /// <param name="superviseCheckReport"></param>
        public static void UpdateSuperviseCheckReport(Model.Supervise_SuperviseCheckReport superviseCheckReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckReport newSuperviseCheckReport = db.Supervise_SuperviseCheckReport.FirstOrDefault(e => e.SuperviseCheckReportId == superviseCheckReport.SuperviseCheckReportId);
            if (newSuperviseCheckReport != null)
            {
                newSuperviseCheckReport.SuperviseCheckReportCode = superviseCheckReport.SuperviseCheckReportCode;
                newSuperviseCheckReport.CheckDate = superviseCheckReport.CheckDate;
                newSuperviseCheckReport.ProjectId = superviseCheckReport.ProjectId;
                newSuperviseCheckReport.UnitId = superviseCheckReport.UnitId;
                newSuperviseCheckReport.CheckTeam = superviseCheckReport.CheckTeam;
                newSuperviseCheckReport.EvaluationResult = superviseCheckReport.EvaluationResult;
                newSuperviseCheckReport.AttachUrl = superviseCheckReport.AttachUrl;
                newSuperviseCheckReport.IsIssued = superviseCheckReport.IsIssued;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全监督检查报告
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        public static void DeleteSuperviseCheckReportById(string superviseCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckReport superviseCheckReport = db.Supervise_SuperviseCheckReport.FirstOrDefault(e => e.SuperviseCheckReportId == superviseCheckReportId);
            if (superviseCheckReport != null)
            {
                if (!string.IsNullOrEmpty(superviseCheckReport.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, superviseCheckReport.AttachUrl);//删除附件
                }
                db.Supervise_SuperviseCheckReport.DeleteOnSubmit(superviseCheckReport);
                db.SubmitChanges();
            }
        }
    }
}