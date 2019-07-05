using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 安全监督检查整改
    /// </summary>
    public static class SuperviseCheckRectifyService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查整改
        /// </summary>
        /// <param name="superviseCheckRectifyId"></param>
        /// <returns></returns>
        public static Model.Supervise_SuperviseCheckRectify GetSuperviseCheckRectifyById(string superviseCheckRectifyId)
        {
            return Funs.DB.Supervise_SuperviseCheckRectify.FirstOrDefault(e => e.SuperviseCheckRectifyId == superviseCheckRectifyId);
        }

        /// <summary>
        /// 根据检查报告id获取整改
        /// </summary>
        /// <param name="superviseCheckReportId"></param>
        /// <returns></returns>
        public static Model.Supervise_SuperviseCheckRectify GetSuperviseCheckRectifyBySuperviseCheckReportId(string superviseCheckReportId)
        {
            return Funs.DB.Supervise_SuperviseCheckRectify.FirstOrDefault(e => e.SuperviseCheckReportId == superviseCheckReportId);
        }

        /// <summary>
        /// 添加安全监督检查整改
        /// </summary>
        /// <param name="SuperviseCheckRectify"></param>
        public static void AddSuperviseCheckRectify(Model.Supervise_SuperviseCheckRectify superviseCheckRectify)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckRectify newSuperviseCheckRectify = new Model.Supervise_SuperviseCheckRectify
            {
                SuperviseCheckRectifyId = superviseCheckRectify.SuperviseCheckRectifyId,
                SuperviseCheckRectifyCode = superviseCheckRectify.SuperviseCheckRectifyCode,
                ProjectId = superviseCheckRectify.ProjectId,
                UnitId = superviseCheckRectify.UnitId,
                CheckDate = superviseCheckRectify.CheckDate,
                IssueMan = superviseCheckRectify.IssueMan,
                IssueDate = superviseCheckRectify.IssueDate,
                SuperviseCheckReportId = superviseCheckRectify.SuperviseCheckReportId,
                HandleState = superviseCheckRectify.HandleState
            };
            db.Supervise_SuperviseCheckRectify.InsertOnSubmit(newSuperviseCheckRectify);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全监督检查整改
        /// </summary>
        /// <param name="superviseCheckRectify"></param>
        public static void UpdateSuperviseCheckRectify(Model.Supervise_SuperviseCheckRectify superviseCheckRectify)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckRectify newSuperviseCheckRectify = db.Supervise_SuperviseCheckRectify.FirstOrDefault(e => e.SuperviseCheckRectifyId == superviseCheckRectify.SuperviseCheckRectifyId);
            if (newSuperviseCheckRectify != null)
            {
                newSuperviseCheckRectify.SuperviseCheckRectifyCode = superviseCheckRectify.SuperviseCheckRectifyCode;
                newSuperviseCheckRectify.ProjectId = superviseCheckRectify.ProjectId;
                newSuperviseCheckRectify.UnitId = superviseCheckRectify.UnitId;
                newSuperviseCheckRectify.CheckDate = superviseCheckRectify.CheckDate;
                newSuperviseCheckRectify.IssueMan = superviseCheckRectify.IssueMan;
                newSuperviseCheckRectify.IssueDate = superviseCheckRectify.IssueDate;
                newSuperviseCheckRectify.SuperviseCheckReportId = superviseCheckRectify.SuperviseCheckReportId;
                newSuperviseCheckRectify.HandleState = superviseCheckRectify.HandleState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全监督检查整改
        /// </summary>
        /// <param name="superviseCheckRectifyId"></param>
        public static void DeleteSuperviseCheckRectifyById(string superviseCheckRectifyId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SuperviseCheckRectify superviseCheckRectify = db.Supervise_SuperviseCheckRectify.FirstOrDefault(e => e.SuperviseCheckRectifyId == superviseCheckRectifyId);
            if (superviseCheckRectify != null)
            {                
                var superviseCheckRectifys = from x in db.Supervise_SuperviseCheckRectify where x.SuperviseCheckReportId == superviseCheckRectify.SuperviseCheckReportId select x;
                if (superviseCheckRectifys.Count() == 1)
                {
                    var report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(superviseCheckRectify.SuperviseCheckReportId);
                    if (report != null)
                    {  
                        report.IsIssued = null;  //已下发
                        BLL.SuperviseCheckReportService.UpdateSuperviseCheckReport(report);
                    }
                }

                db.Supervise_SuperviseCheckRectify.DeleteOnSubmit(superviseCheckRectify);
                db.SubmitChanges();
            }
        }
    }
}