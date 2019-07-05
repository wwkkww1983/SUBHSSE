using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ViolationRecordDService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报告主键获取违规记录汇总表
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_ViolationRecordD GetViolationRecordDByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_ViolationRecordD where x.MonthReportId == monthReportId select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加违规记录汇总表信息
        /// </summary>
        /// <param name="violationRecordD">违规记录汇总表实体</param>
        public static void AddViolationRecordD(Model.Manager_ViolationRecordD violationRecordD)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_ViolationRecordD));
            Model.Manager_ViolationRecordD newViolationRecordD = new Model.Manager_ViolationRecordD
            {
                ViolationRecordId = newKeyID,
                MonthReportId = violationRecordD.MonthReportId,
                FileContents = violationRecordD.FileContents
            };

            db.Manager_ViolationRecordD.InsertOnSubmit(newViolationRecordD);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改违规记录汇总表信息
        /// </summary>
        /// <param name="violationRecordD">月报告实体</param>
        public static void UpdateViolationRecordD(Model.Manager_ViolationRecordD violationRecordD)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ViolationRecordD newViolationRecordD = db.Manager_ViolationRecordD.First(e => e.MonthReportId == violationRecordD.MonthReportId);
            newViolationRecordD.FileContents = violationRecordD.FileContents;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报告主键删除违规记录汇总表信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteViolationRecordDByMonthReportId(string monthReportId)
        {
            var q = (from x in db.Manager_ViolationRecordD where x.MonthReportId == monthReportId select x).FirstOrDefault();
            if (q != null)
            {
                db.Manager_ViolationRecordD.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
