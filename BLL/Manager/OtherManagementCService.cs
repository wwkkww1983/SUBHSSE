using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class OtherManagementCService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSE现场其他管理情况信息
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_OtherManagementC> GetOtherManagementByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_OtherManagementC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加HSE现场其他管理情况信息
        /// </summary>
        /// <param name="otherManagement"></param>
        public static void AddOtherManagement(Model.Manager_Month_OtherManagementC otherManagement)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_OtherManagementC newOtherManagement = new Model.Manager_Month_OtherManagementC
            {
                OtherManagementId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherManagementC)),
                MonthReportId = otherManagement.MonthReportId,
                ManagementDes = otherManagement.ManagementDes,
                SortIndex = otherManagement.SortIndex
            };
            db.Manager_Month_OtherManagementC.InsertOnSubmit(newOtherManagement);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除HSE现场其他管理情况信息
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteOtherManagementByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_OtherManagementC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_OtherManagementC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
