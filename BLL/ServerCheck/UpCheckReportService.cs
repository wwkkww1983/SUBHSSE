using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 企业上报安全监督报告
    /// </summary>
    public static class UpCheckReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取安全监督检查整改
        /// </summary>
        /// <param name="UpCheckReportId"></param>
        /// <returns></returns>
        public static Model.Supervise_UpCheckReport GetUpCheckReportById(string UpCheckReportId)
        {
            return Funs.DB.Supervise_UpCheckReport.FirstOrDefault(e => e.UpCheckReportId == UpCheckReportId);
        }

        /// <summary>
        /// 添加安全监督检查整改
        /// </summary>
        /// <param name="UpCheckReport"></param>
        public static void AddUpCheckReport(Model.Supervise_UpCheckReport UpCheckReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_UpCheckReport newUpCheckReport = new Model.Supervise_UpCheckReport
            {
                UpCheckReportId = UpCheckReport.UpCheckReportId,
                UnitId = UpCheckReport.UnitId,
                CheckStartTime = UpCheckReport.CheckStartTime,
                CheckEndTime = UpCheckReport.CheckEndTime,
                Values1 = UpCheckReport.Values1,
                Values2 = UpCheckReport.Values2,
                Values3 = UpCheckReport.Values3,
                Values4 = UpCheckReport.Values4,
                Values5 = UpCheckReport.Values5,
                Values6 = UpCheckReport.Values6,
                Values7 = UpCheckReport.Values7,
                CompileDate = UpCheckReport.CompileDate,
                AuditDate = UpCheckReport.AuditDate,
                UpState = UpCheckReport.UpState,
                UpDateTime = UpCheckReport.UpDateTime
            };
            db.Supervise_UpCheckReport.InsertOnSubmit(newUpCheckReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全监督检查整改
        /// </summary>
        /// <param name="UpCheckReport"></param>
        public static void UpdateUpCheckReport(Model.Supervise_UpCheckReport UpCheckReport)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_UpCheckReport newUpCheckReport = db.Supervise_UpCheckReport.FirstOrDefault(e => e.UpCheckReportId == UpCheckReport.UpCheckReportId);
            if (newUpCheckReport != null)
            {
                newUpCheckReport.UnitId = UpCheckReport.UnitId;
                newUpCheckReport.CheckStartTime = UpCheckReport.CheckStartTime;
                newUpCheckReport.CheckEndTime = UpCheckReport.CheckEndTime;
                newUpCheckReport.Values1 = UpCheckReport.Values1;
                newUpCheckReport.Values2 = UpCheckReport.Values2;
                newUpCheckReport.Values3 = UpCheckReport.Values3;
                newUpCheckReport.Values4 = UpCheckReport.Values4;
                newUpCheckReport.Values5 = UpCheckReport.Values5;
                newUpCheckReport.Values6 = UpCheckReport.Values6;
                newUpCheckReport.Values7 = UpCheckReport.Values7;
                newUpCheckReport.CompileDate = UpCheckReport.CompileDate;
                newUpCheckReport.AuditDate = UpCheckReport.AuditDate;
                newUpCheckReport.UpState = UpCheckReport.UpState;
                newUpCheckReport.UpDateTime = UpCheckReport.UpDateTime;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全监督检查整改
        /// </summary>
        /// <param name="UpCheckReportId"></param>
        public static void DeleteUpCheckReportById(string UpCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_UpCheckReport upCheckReport = db.Supervise_UpCheckReport.FirstOrDefault(e => e.UpCheckReportId == UpCheckReportId);
            if (upCheckReport != null)
            {
                db.Supervise_UpCheckReport.DeleteOnSubmit(upCheckReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主表主键删除安全监督检查整改
        /// </summary>
        /// <param name="upCheckReportId"></param>
        public static void DeleteUpCheckReportItemByUpCheckReportId(string upCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var upCheckReportItem = from x in db.Supervise_UpCheckReportItem where x.UpCheckReportId == upCheckReportId select x;
            if (upCheckReportItem.Count() > 0)
            {
                db.Supervise_UpCheckReportItem.DeleteAllOnSubmit(upCheckReportItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主表主键删除安全监督检查整改
        /// </summary>
        /// <param name="upCheckReportId"></param>
        public static void DeleteUpCheckReportItem2ByUpCheckReportId(string upCheckReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var upCheckReportItem2 = from x in db.Supervise_UpCheckReportItem2 where x.UpCheckReportId == upCheckReportId select x;
            if (upCheckReportItem2.Count() > 0)
            {
                db.Supervise_UpCheckReportItem2.DeleteAllOnSubmit(upCheckReportItem2);
                db.SubmitChanges();
            }
        }
    }
}