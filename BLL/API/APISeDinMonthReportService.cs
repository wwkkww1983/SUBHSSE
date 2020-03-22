using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 赛鼎月报
    /// </summary>
    public static class APISeDinMonthReportService
    {
        #region  获取赛鼎月报详细
        /// <summary>
        ///  获取赛鼎月报详细
        /// </summary>
        /// <param name="MonthReportId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReportItem getSeDinMonthReportById(string MonthReportId)
        {
            var getInfo = from x in Funs.DB.SeDin_MonthReport
                          where x.MonthReportId == MonthReportId
                          select new Model.SeDinMonthReportItem
                          {
                              MonthReportId = x.MonthReportId,
                              ProjectId = x.ProjectId,
                              DueDate = string.Format("{0:yyyy-MM-dd}", x.DueDate),
                              StartDate = string.Format("{0:yyyy-MM-dd}", x.StartDate),
                              EndDate = string.Format("{0:yyyy-MM-dd}", x.EndDate),
                              ReporMonth = string.Format("{0:yyyy-MM}", x.ReporMonth),                              
                              CompileManId = x.CompileManId,
                              CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileManId).UserName,
                              AuditManId = x.AuditManId,
                              AuditManName = Funs.DB.Sys_User.First(u => u.UserId == x.AuditManId).UserName,
                              ApprovalManId = x.ApprovalManId,
                              ApprovalManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApprovalManId).UserName,
                              ThisSummary=x.ThisSummary,
                              NextPlan=x.NextPlan,
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion        

        #region 获取赛鼎月报列表信息
        /// <summary>
        /// 获取赛鼎月报列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.SeDinMonthReportItem> getSeDinMonthReportList(string projectId)
        {
            var getSeDinMonthReport = from x in Funs.DB.SeDin_MonthReport
                                  where x.ProjectId == projectId  
                                  orderby x.ReporMonth descending
                                  select new Model.SeDinMonthReportItem
                                  {
                                      MonthReportId = x.MonthReportId,
                                      ProjectId = x.ProjectId,
                                      DueDate = string.Format("{0:yyyy-MM-dd}", x.DueDate),
                                      StartDate = string.Format("{0:yyyy-MM-dd}", x.StartDate),
                                      EndDate = string.Format("{0:yyyy-MM-dd}", x.EndDate),
                                      ReporMonth = string.Format("{0:yyyy-MM}", x.ReporMonth),
                                      CompileManId = x.CompileManId,
                                      CompileManName = Funs.DB.Sys_User.First(u => u.UserId == x.CompileManId).UserName,
                                      AuditManId = x.AuditManId,
                                      AuditManName = Funs.DB.Sys_User.First(u => u.UserId == x.AuditManId).UserName,
                                      ApprovalManId = x.ApprovalManId,
                                      ApprovalManName = Funs.DB.Sys_User.First(u => u.UserId == x.ApprovalManId).UserName,
                                      ThisSummary = x.ThisSummary,
                                      NextPlan = x.NextPlan,
                                  };
            return getSeDinMonthReport.ToList();
        }
        #endregion        

        #region 保存SeDin_MonthReport
        /// <summary>
        /// 保存SeDin_MonthReport
        /// </summary>
        /// <param name="newItem">处罚通知单</param>
        /// <returns></returns>
        public static void SaveSeDinMonthReport(Model.SeDinMonthReportItem newItem)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.SeDin_MonthReport newSeDinMonthReport = new Model.SeDin_MonthReport
                {
                    MonthReportId = newItem.MonthReportId,
                    ProjectId = newItem.ProjectId,                  
                    DueDate = Funs.GetNewDateTime(newItem.DueDate),
                    StartDate = Funs.GetNewDateTime(newItem.StartDate),
                    EndDate = Funs.GetNewDateTime(newItem.EndDate),
                    ReporMonth = Funs.GetNewDateTime(newItem.ReporMonth),
                    CompileManId = newItem.CompileManId,
                    AuditManId = newItem.AuditManId,
                    ApprovalManId = newItem.ApprovalManId,
                    ThisSummary = System.Web.HttpUtility.HtmlEncode(newItem.ThisSummary),
                    NextPlan = System.Web.HttpUtility.HtmlEncode(newItem.NextPlan),
                };              
                var updateReport = db.SeDin_MonthReport.FirstOrDefault(x => x.MonthReportId == newItem.MonthReportId || (x.ProjectId==newItem.ProjectId && x.ReporMonth ==newSeDinMonthReport.ReporMonth));
                if (updateReport == null)
                {
                    newSeDinMonthReport.MonthReportId = SQLHelper.GetNewID();
                    db.SeDin_MonthReport.InsertOnSubmit(newSeDinMonthReport);
                    db.SubmitChanges();
                    
                }
                else
                {
                    updateReport.DueDate = newSeDinMonthReport.DueDate;
                    updateReport.StartDate = newSeDinMonthReport.StartDate;
                    updateReport.EndDate = newSeDinMonthReport.EndDate;
                    updateReport.CompileManId = newSeDinMonthReport.CompileManId;
                    updateReport.AuditManId = newSeDinMonthReport.AuditManId;
                    updateReport.ApprovalManId = newSeDinMonthReport.ApprovalManId;
                    updateReport.ThisSummary = newSeDinMonthReport.ThisSummary;
                    updateReport.NextPlan = newSeDinMonthReport.NextPlan;
                }               
            }
        }
        #endregion      
        
    }
}
