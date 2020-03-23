using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 赛鼎月报
    /// </summary>
    public static class APISeDinMonthReportService
    {
        #region 获取赛鼎月报列表信息
        /// <summary>
        /// 获取赛鼎月报列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.SeDinMonthReportItem> getSeDinMonthReportList(string projectId, string month)
        {
            var monthD=Funs.GetNewDateTime(month);
            var getSeDinMonthReport = from x in Funs.DB.SeDin_MonthReport
                                      where x.ProjectId == projectId 
                                      && (!monthD.HasValue || (monthD.HasValue && x.ReporMonth.Value.Year== monthD.Value.Year && x.ReporMonth.Value.Month == monthD.Value.Month))
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
        
        #region 获取赛鼎月报初始化页面
        #region  获取赛鼎月报初始化页面 --0、封面
        /// <summary>
        /// 获取赛鼎月报初始化页面 --封面
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReportItem getSeDinMonthReportNullPage0(string projectId)
        {
            var nowDate = System.DateTime.Now;
            Model.SeDinMonthReportItem newItem = new Model.SeDinMonthReportItem
            {
                ProjectId = projectId,
                DueDate = nowDate.Year.ToString() + "-" + nowDate.Month.ToString() + "-05",
                StartDate = nowDate.AddMonths(-2).Year.ToString() + "-" + nowDate.AddMonths(-2).Month.ToString() + "-26",
                EndDate = nowDate.AddMonths(-1).Year.ToString() + "-" + nowDate.AddMonths(-1).Month.ToString() + "-25",
                ReporMonth = nowDate.AddMonths(-1).Year.ToString() + "-" + nowDate.AddMonths(-1).Month.ToString(),
            };
            return newItem;
        }
        #endregion
        #region  获取赛鼎月报初始化页面 --1、项目信息
        /// <summary>
        /// 获取赛鼎月报初始化页面 --封面
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReport1Item getSeDinMonthReportNullPage1(string projectId)
        {
            ///项目经理
            string projectManagerId = string.Empty;
            var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == projectId && x.RoleId == Const.ProjectManager);
            if (m != null)
            {
                projectManagerId = m.UserId;
            }
            ////安全经理
            string hsseManagerId = string.Empty;
            var h = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == projectId && x.RoleId == Const.HSSEManager);
            if (h != null)
            {
                hsseManagerId = h.UserId;
            }

            var newItem = from x in Funs.DB.Base_Project
                          where x.ProjectId == projectId
                          select new Model.SeDinMonthReport1Item
                          {
                              ProjectCode = x.PostCode,
                              ProjectName = x.ProjectName,
                              ProjectType=Funs.DB.Sys_Const.First(y=>y.GroupId == ConstValue.Group_ProjectType && y.ConstValue==x.ProjectType).ConstText,
                              StartDate=string.Format("{0:yyyy-MM-dd}",x.StartDate),
                              EndDate = string.Format("{0:yyyy-MM-dd}", x.EndDate),
                              ProjectManager=UserService.GetUserNameAndTelByUserId(projectManagerId),
                              HsseManager = UserService.GetUserNameAndTelByUserId(hsseManagerId),
                          };
            
            return newItem.FirstOrDefault();
        }
        #endregion
        #region  获取赛鼎月报初始化页面 --2、项目安全工时统计
        /// <summary>
        /// 获取赛鼎月报初始化页面 --封面
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReport2Item getSeDinMonthReportNullPage2(string projectId, string month)
        {
            var newItem = new Model.SeDinMonthReport2Item();
            var monthD = Funs.GetNewDateTime(month);
            var getProject = Funs.DB.Base_Project.FirstOrDefault(x => x.ProjectId == projectId);
            if (getProject != null && monthD.HasValue)
            {
                var getMonthReport = from x in Funs.DB.SitePerson_MonthReport where x.ProjectId == projectId select x;
                if (getMonthReport.Count() > 0)
                {
                    var monthR= getMonthReport.FirstOrDefault(x => x.CompileDate.Value.Year == monthD.Value.Year && x.CompileDate.Value.Month == monthD.Value.Month);
                    if (monthR != null)
                    {
                    }
                }
            }
            return newItem;
        }
        #endregion
        #endregion

        #region 获取赛鼎月报详细
        #region  获取赛鼎月报详细 --0、封面
        /// <summary>
        ///  获取赛鼎月报详细
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReportItem getSeDinMonthReport0ById(string monthReportId)
        {
            var getInfo = from x in Funs.DB.SeDin_MonthReport
                          where x.MonthReportId == monthReportId
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
        #region  获取赛鼎月报详细 --1、项目信息
        /// <summary>
        ///  获取赛鼎月报详细
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReport1Item getSeDinMonthReport1ById(string monthReportId)
        {
            var getInfo = from x in Funs.DB.SeDin_MonthReport1
                          where x.MonthReportId == monthReportId
                          select new Model.SeDinMonthReport1Item
                          {
                              MonthReport1Id=x.MonthReport1Id,
                              MonthReportId = x.MonthReportId,
                              ProjectCode = x.ProjectCode,
                              ProjectName = x.ProjectName,
                              ProjectType =x.ProjectType,
                              StartDate = string.Format("{0:yyyy-MM-dd}", x.StartDate),
                              EndDate = string.Format("{0:yyyy-MM-dd}", x.EndDate),
                              ProjectManager = x.ProjectManager,
                              HsseManager=x.HsseManager,
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion
        #region  获取赛鼎月报详细 --2、项目安全工时统计
        /// <summary>
        ///  获取赛鼎月报详细
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static Model.SeDinMonthReport2Item getSeDinMonthReport2ById(string monthReportId)
        {
            var getInfo = from x in Funs.DB.SeDin_MonthReport2
                          where x.MonthReportId == monthReportId
                          select new Model.SeDinMonthReport2Item
                          {
                              MonthReport2Id = x.MonthReport2Id,
                              MonthReportId = x.MonthReportId,
                              MonthWorkTime = x.MonthWorkTime,
                              YearWorkTime = x.YearWorkTime,
                              ProjectWorkTime = x.ProjectWorkTime,
                              TotalLostTime=x.TotalLostTime,
                              MillionLossRate=x.MillionLossRate,
                              TimeAccuracyRate=x.TimeAccuracyRate,
                              StartDate = string.Format("{0:yyyy-MM-dd}", x.StartDate),
                              EndDate = string.Format("{0:yyyy-MM-dd}", x.EndDate),
                              SafeWorkTime = x.SafeWorkTime,
                          };
            return getInfo.FirstOrDefault();
        }
        #endregion
        #endregion

        #region 保存
        #region 保存 MonthReport0 封面
        /// <summary>
        /// 保存SeDin_MonthReport
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public static string SaveSeDinMonthReport0(Model.SeDinMonthReportItem newItem)
        {
           
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.SeDin_MonthReport newReport = new Model.SeDin_MonthReport
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
                var updateReport = db.SeDin_MonthReport.FirstOrDefault(x => x.MonthReportId == newItem.MonthReportId || (x.ProjectId==newItem.ProjectId && x.ReporMonth ==newReport.ReporMonth));
                if (updateReport == null)
                {
                    newReport.MonthReportId = SQLHelper.GetNewID();
                    db.SeDin_MonthReport.InsertOnSubmit(newReport);
                    db.SubmitChanges();
                    
                }
                else
                {
                    newReport.MonthReportId = updateReport.MonthReportId;
                    updateReport.DueDate = newReport.DueDate;
                    updateReport.StartDate = newReport.StartDate;
                    updateReport.EndDate = newReport.EndDate;
                    updateReport.CompileManId = newReport.CompileManId;
                    updateReport.AuditManId = newReport.AuditManId;
                    updateReport.ApprovalManId = newReport.ApprovalManId;
                    updateReport.ThisSummary = newReport.ThisSummary;
                    updateReport.NextPlan = newReport.NextPlan;
                }

                return newReport.MonthReportId;
            }
        }
        #endregion

        #region 保存 MonthReport1、项目信息
        /// <summary>
        /// 保存SeDin_MonthReport
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public static string SaveSeDinMonthReport1(Model.SeDinMonthReport1Item newItem)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.SeDin_MonthReport1 newReport = new Model.SeDin_MonthReport1
                {
                    MonthReportId = newItem.MonthReportId,
                    MonthReport1Id = newItem.MonthReport1Id,
                    ProjectCode=newItem.ProjectCode,
                    ProjectName = newItem.ProjectName,
                    ProjectType=newItem.ProjectType,
                    StartDate = Funs.GetNewDateTime(newItem.StartDate),
                    EndDate = Funs.GetNewDateTime(newItem.EndDate),
                    ProjectManager = newItem.ProjectManager,
                    HsseManager = newItem.HsseManager,
                };
                var updateReport = db.SeDin_MonthReport1.FirstOrDefault(x => x.MonthReportId == newItem.MonthReportId );
                if (updateReport == null)
                {
                    newReport.MonthReport1Id = SQLHelper.GetNewID();
                    db.SeDin_MonthReport1.InsertOnSubmit(newReport);
                    db.SubmitChanges();
                }
                else
                {
                    newReport.MonthReportId = updateReport.MonthReportId;
                    updateReport.ProjectCode = newReport.ProjectCode;
                    updateReport.ProjectName = newReport.ProjectName;
                    updateReport.ProjectType = newReport.ProjectType;
                    updateReport.StartDate = newReport.StartDate;
                    updateReport.EndDate = newReport.EndDate;
                    updateReport.ProjectManager = newReport.ProjectManager;
                    updateReport.HsseManager = newReport.HsseManager;
                }
                return newReport.MonthReportId;
            }
        }
        #endregion

        #region 保存 MonthReport2、项目安全工时统计
        /// <summary>
        /// 保存SeDin_MonthReport
        /// </summary>
        /// <param name="newItem">赛鼎月报</param>
        /// <returns></returns>
        public static string SaveSeDinMonthReport2(Model.SeDinMonthReport2Item newItem)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.SeDin_MonthReport2 newReport = new Model.SeDin_MonthReport2
                {
                    MonthReportId = newItem.MonthReportId,
                    MonthReport2Id = newItem.MonthReport2Id,
                    MonthWorkTime = newItem.MonthWorkTime,
                    YearWorkTime = newItem.YearWorkTime,
                    ProjectWorkTime = newItem.ProjectWorkTime,
                    TotalLostTime=newItem.TotalLostTime,
                    MillionLossRate = newItem.MillionLossRate,
                    TimeAccuracyRate = newItem.TimeAccuracyRate,
                    StartDate = Funs.GetNewDateTime(newItem.StartDate),
                    EndDate = Funs.GetNewDateTime(newItem.EndDate),
                    SafeWorkTime = newItem.SafeWorkTime,
                };
                var updateReport = db.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == newItem.MonthReportId);
                if (updateReport == null)
                {
                    newReport.MonthReport2Id = SQLHelper.GetNewID();
                    db.SeDin_MonthReport2.InsertOnSubmit(newReport);
                    db.SubmitChanges();
                }
                else
                {
                    newReport.MonthReportId = updateReport.MonthReportId;
                    updateReport.MonthWorkTime = newReport.MonthWorkTime;
                    updateReport.YearWorkTime = newReport.YearWorkTime;
                    updateReport.ProjectWorkTime = newReport.ProjectWorkTime;
                    updateReport.TotalLostTime = newReport.TotalLostTime;
                    updateReport.MillionLossRate = newReport.MillionLossRate;
                    updateReport.TimeAccuracyRate = newReport.TimeAccuracyRate;
                    updateReport.StartDate = newReport.StartDate;
                    updateReport.EndDate = newReport.EndDate;
                    updateReport.SafeWorkTime = newReport.SafeWorkTime;
                   
                }
                return newReport.MonthReportId;
            }
        }
        #endregion
        #endregion
    }
}
