using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class CheckCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSSE检查
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_CheckC> GetCheckByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_CheckC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加HSE检查
        /// </summary>
        /// <param name="check"></param>
        public static void AddCheck(Model.Manager_Month_CheckC check)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_CheckC newCheck = new Model.Manager_Month_CheckC
            {
                CheckId = SQLHelper.GetNewID(typeof(Model.Manager_Month_CheckC)),
                MonthReportId = check.MonthReportId,
                CheckType = check.CheckType,
                Inspectors = check.Inspectors,
                CheckDate = check.CheckDate,
                Remark = check.Remark,
                SortIndex = check.SortIndex
            };
            db.Manager_Month_CheckC.InsertOnSubmit(newCheck);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除Hse检查
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteCheckByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_CheckC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_CheckC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
