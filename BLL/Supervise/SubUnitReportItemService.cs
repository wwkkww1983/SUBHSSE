using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 企业安全文件上报
    /// </summary>
    public static class SubUnitReportItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据明细主键获取信息
        /// </summary>
        /// <param name="subUnitReportItemId"></param>
        /// <returns></returns>
        public static Model.Supervise_SubUnitReportItem GetSubUnitReportItemById(string subUnitReportItemId)
        {
            return Funs.DB.Supervise_SubUnitReportItem.FirstOrDefault(e => e.SubUnitReportItemId == subUnitReportItemId);
        }

        /// <summary>
        /// 根据企业安全文件上报Id获取明细信息
        /// </summary>
        /// <param name="subUnitReportId"></param>
        /// <returns></returns>
        public static Model.Supervise_SubUnitReportItem GetSubUnitReportItemBySubUnitReportId(string subUnitReportId)
        {
            return Funs.DB.Supervise_SubUnitReportItem.FirstOrDefault(e => e.SubUnitReportId == subUnitReportId);
        }

        /// <summary>
        /// 修改企业安全文件上报明细信息
        /// </summary>
        /// <param name="subUnitReportItem"></param>
        public static void UpdateSubUnitReportItem(Model.Supervise_SubUnitReportItem subUnitReportItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SubUnitReportItem newSubUnitReportItem = db.Supervise_SubUnitReportItem.FirstOrDefault(e => e.SubUnitReportItemId == subUnitReportItem.SubUnitReportItemId);
            if (newSubUnitReportItem != null)
            {
                newSubUnitReportItem.ReportTitle = subUnitReportItem.ReportTitle;
                newSubUnitReportItem.ReportContent = subUnitReportItem.ReportContent;
                newSubUnitReportItem.AttachUrl = subUnitReportItem.AttachUrl;
                newSubUnitReportItem.ReportDate = subUnitReportItem.ReportDate;
                newSubUnitReportItem.State = subUnitReportItem.State;
                newSubUnitReportItem.UpState = subUnitReportItem.UpState;
                db.SubmitChanges();
            }
        }

    }
}
