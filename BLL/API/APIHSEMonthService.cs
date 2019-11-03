using System.Collections.Generic;
using System.Linq;
using System;

namespace BLL
{
    /// <summary>
    /// HSE管理月报
    /// </summary>
    public static class APIHSEMonthService
    {
        #region 获取HSE管理月报信息
        /// <summary>
        /// 获取HSE管理月报信息
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static Model.HSEMonthItem getHSEMonth(string monthReportId)
        {
            var getDataItem = (from x in Funs.DB.Manager_MonthReport
                              where x.MonthReportId == monthReportId
                              select new Model.HSEMonthItem
                              {
                                  MonthReportId = x.MonthReportId,
                                  ProjectId = x.ProjectId,
                                  ReportManId=x.ReportMan,
                                  ReportManName =Funs.DB.Sys_User.First(u=>u.UserId ==x.ReportMan).UserName,
                                  ReportMonths = string.Format("{0:yyyy-MM}", x.ReportMonths),
                                  ReportStartDate = string.Format("{0:yyyy-MM}", x.MonthReportStartDate),
                                  ReportEndDate = string.Format("{0:yyyy-MM}", x.MonthReportDate),
                                  AllManhoursData = x.AllManhoursData,
                              }).FirstOrDefault();
            return getDataItem;
        }
        #endregion        

        #region 获取HSE管理月报列表信息
        /// <summary>
        /// 获取HSE管理月报列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="reportMonths"></param>
        /// <returns></returns>
        public static List<Model.HSEMonthItem> getHSEMonthList(string projectId, string reportMonths)
        {
            DateTime? getMonthDate = Funs.GetNewDateTime(reportMonths);
            var getDataList = from x in Funs.DB.Manager_MonthReport
                              where x.ProjectId == projectId 
                              && (reportMonths == null || (x.ReportMonths.Value.Year == getMonthDate.Value.Year && x.ReportMonths.Value.Month == getMonthDate.Value.Month))
                              orderby x.ReportMonths descending
                              select new Model.HSEMonthItem
                              {
                                  MonthReportId = x.MonthReportId,
                                  ProjectId = x.ProjectId,
                                  ReportManId = x.ReportMan,
                                  ReportManName = Funs.DB.Sys_User.First(u => u.UserId == x.ReportMan).UserName,
                                  ReportMonths = string.Format("{0:yyyy-MM}", x.ReportMonths),
                                  ReportStartDate = string.Format("{0:yyyy-MM}", x.MonthReportStartDate),
                                  ReportEndDate = string.Format("{0:yyyy-MM}", x.MonthReportDate),
                                  AllManhoursData = x.AllManhoursData,
                              };
            return getDataList.ToList();
        }
        #endregion
    }
}
