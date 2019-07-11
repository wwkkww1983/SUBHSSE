using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 企业安全文件上报
    /// </summary>
    public static class SubUnitReportService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取企业安全文件上报信息
        /// </summary>
        /// <param name="subUnitReportId"></param>
        /// <returns></returns>
        public static Model.Supervise_SubUnitReport GetSubUnitReportById(string subUnitReportId)
        {
            return Funs.DB.Supervise_SubUnitReport.FirstOrDefault(e => e.SubUnitReportId == subUnitReportId);
        }

        /// <summary>
        /// 是否未上报
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-已上报，false-未上报</returns>
        public static bool IsUpLoadSubUnitReport(string subUnitReportId)
        {
            bool isUpLoad = true;
            var SubUnitReport = BLL.SubUnitReportService.GetSubUnitReportById(subUnitReportId);
            if (SubUnitReport != null)
            {
                if (SubUnitReport.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Supervise_SubUnitReportItem.FirstOrDefault(x => x.SubUnitReportId == subUnitReportId && x.UpState == Const.UpState_3);
                    if (detailCout != null)
                    {
                        isUpLoad = false;
                    }
                }
                else
                {
                    isUpLoad = false;
                }
            }

            return isUpLoad;
        }
    }
}
