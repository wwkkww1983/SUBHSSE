using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCEdit5 : PageBase
    {
        #region 定义项
        /// <summary>
        /// 月报告查主键
        /// </summary>
        public string MonthReportId
        {
            get
            {
                return (string)ViewState["MonthReportId"];
            }
            set
            {
                ViewState["MonthReportId"] = value;
            }
        }

        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

        private static DateTime projectStartTime;

        #region 定义集合
        /// <summary>
        /// 5.1.2 本月文件、方案修编情况说明集合
        /// </summary>
        private static List<Model.Manager_Month_PlanC> plans = new List<Model.Manager_Month_PlanC>();

        /// <summary>
        /// 5.2.2 详细审查记录集合
        /// </summary>
        private static List<Model.Manager_Month_ReviewRecordC> reviewRecords = new List<Model.Manager_Month_ReviewRecordC>();

        /// <summary>
        /// 5.3 HSSE文件管理集合
        /// </summary>
        private static List<Model.Manager_Month_FileManageC> fileManages = new List<Model.Manager_Month_FileManageC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                plans.Clear();
                reviewRecords.Clear();
                fileManages.Clear();
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                Model.Manager_MonthReportC mr = BLL.MonthReportCService.GetLastMonthReportByDate(endTime, this.ProjectId);
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(ProjectId);
                if (project.StartDate != null)
                {
                    projectStartTime = Convert.ToDateTime(project.StartDate);
                }
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    months = Convert.ToDateTime(monthReport.Months);
                    Model.SUBHSSEDB db = Funs.DB;
                    //5.1.2 本月文件、方案修编情况说明
                    plans = (from x in db.Manager_Month_PlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (plans.Count > 0)
                    {
                        this.gvMonthPlan.DataSource = plans;
                        this.gvMonthPlan.DataBind();
                    }
                    else
                    {
                        GetPlanSort();
                    }
                    //5.2.2 详细审查记录
                    reviewRecords = (from x in db.Manager_Month_ReviewRecordC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (reviewRecords.Count > 0)
                    {
                        this.gvReviewRecord.DataSource = reviewRecords;
                        this.gvReviewRecord.DataBind();
                    }
                    else
                    {
                        GetReviewRecordSort();
                    }
                }
                else
                {
                    //施工HSE实施计划数量
                    //List<Model.ActionPlan_ActionPlanList> actionPlans = BLL.ActionPlanListService.GetActionPlanListsByDate(startTime, endTime, this.ProjectId);
                    //int actionPlanNum = actionPlans.Count;
                    ////HSE管理规定数量
                    //List<Model.ActionPlan_ManagerRule> hSERules = BLL.ActionPlan_ManagerRuleService.GetManagerRuleListsByDate(startTime, endTime, this.ProjectId);
                    //int hSERuleNum = hSERules.Count;
                    //int constructSolutionNum = BLL.ConstructSolutionService.GetConstructSolutionCountByDate(this.ProjectId, startTime, endTime);
                    //int subUnitQualityAuditDetailNum = BLL.SubUnitQualityAuditDetailService.GetCountByDate(this.ProjectId, startTime, endTime);
                    //int equipmentQualityAuditDetailNum = BLL.EquipmentQualityAuditDetailService.GetCountByDate(this.ProjectId, startTime, endTime);
                    //int personQualityAuditDetailNum = BLL.PersonQualityService.GetCountByDate(this.ProjectId, startTime, endTime);
                    //int generalEquipmentQualityNum = BLL.GeneralEquipmentQualityService.GetSumCountByDate(this.ProjectId, startTime, endTime);
                    GetPlanSort();
                    GetReviewRecordSort();
                }
            }
        }
        #endregion

        #region HSE管理文件、方案修编
        /// <summary>
        /// 显示本月文件、方案修编情况说明
        /// </summary>
        private void GetPlanSort()
        {
            List<Model.ActionPlan_ActionPlanList> actionList = BLL.ActionPlanListService.GetActionPlanListsByDate(startTime, endTime, this.ProjectId);
            //施工计划            
            if (actionList.Count > 0)
            {
                foreach (Model.ActionPlan_ActionPlanList item in actionList)
                {
                    Model.Manager_Month_PlanC plan = new Model.Manager_Month_PlanC
                    {
                        PlanId = item.ActionPlanListId,
                        PlanName = item.ActionPlanListName
                    };
                    List<Model.Sys_User> userSelect = (from x in Funs.DB.Sys_User
                                                       join y in Funs.DB.Sys_FlowOperate
                                                       on x.UserId equals y.OperaterId
                                                       join z in Funs.DB.Project_ProjectUser
                                                       on y.OperaterId equals z.UserId
                                                       where (z.RoleId.Contains(BLL.Const.HSSEEngineer) || z.RoleId.Contains(BLL.Const.HSSEManager)) && y.MenuId == BLL.Const.ProjectActionPlanListMenuId && y.DataId == item.ActionPlanListId
                                                       select x).Distinct().ToList();
                    if (userSelect.Count() > 0)
                    {
                        List<Model.Sys_User> users = userSelect.Distinct().ToList();
                        string names = string.Empty;
                        foreach (var user in users)
                        {
                            names += user.UserName + ",";
                        }
                        if (!string.IsNullOrEmpty(names))
                        {
                            names = names.Substring(0, names.LastIndexOf(","));
                        }
                        plan.CompileMan = names;
                    }
                    if (item.CompileDate != null)
                    {
                        plan.CompileDate = string.Format("{0:yyyy-MM-dd}", item.CompileDate);
                    }
                    plans.Add(plan);
                }
            }
            //管理规定
            List<Model.ActionPlan_ManagerRule> hseRuleList = BLL.ActionPlan_ManagerRuleService.GetManagerRuleListsByDate(startTime, endTime, this.ProjectId);
            if (hseRuleList.Count > 0)
            {
                foreach (Model.ActionPlan_ManagerRule rule in hseRuleList)
                {
                    Model.Manager_Month_PlanC plan = new Model.Manager_Month_PlanC
                    {
                        PlanId = rule.ManagerRuleId,
                        PlanName = rule.ManageRuleName
                    };
                    List<Model.Sys_User> users = (from x in Funs.DB.Sys_User
                                                  join y in Funs.DB.Sys_FlowOperate
                                                  on x.UserId equals y.OperaterId
                                                  join z in Funs.DB.Project_ProjectUser
                                                  on y.OperaterId equals z.UserId
                                                  where (z.RoleId.Contains(BLL.Const.HSSEEngineer) || z.RoleId.Contains(BLL.Const.HSSEManager)) && y.MenuId == BLL.Const.ActionPlan_ManagerRuleMenuId && y.DataId == rule.ManagerRuleId
                                                  select x).Distinct().ToList();
                    if (users.Count() > 0)
                    {
                        string names = string.Empty;
                        foreach (var user in users)
                        {
                            names += user.UserName + ",";
                        }
                        if (!string.IsNullOrEmpty(names))
                        {
                            names = names.Substring(0, names.LastIndexOf(","));
                        }
                        plan.CompileMan = names;
                    }
                    if (rule.CompileDate != null)
                    {
                        plan.CompileDate = string.Format("{0:yyyy-MM-dd}", rule.CompileDate);
                    }
                    plans.Add(plan);
                }
            }
            this.gvMonthPlan.DataSource = plans;
            this.gvMonthPlan.DataBind();
        }

        /// <summary>
        /// 增加本月文件、方案修编情况说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewMonthPlan_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthPlanList();
            Model.Manager_Month_PlanC monthPlanSort = new Model.Manager_Month_PlanC
            {
                PlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_PlanC))
            };
            plans.Add(monthPlanSort);
            this.gvMonthPlan.DataSource = plans;
            this.gvMonthPlan.DataBind();
        }

        /// <summary>
        /// 检查并保存其他HSE管理活动集合
        /// </summary>
        private void jerqueSaveMonthPlanList()
        {
            plans.Clear();
            JArray mergedData = gvMonthPlan.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_PlanC monthPlanSort = new Model.Manager_Month_PlanC
                {
                    PlanId = this.gvMonthPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    PlanName = values.Value<string>("PlanName").ToString(),
                    CompileMan = values.Value<string>("CompileMan").ToString(),
                    CompileDate = values.Value<string>("CompileDate").ToString()
                };
                plans.Add(monthPlanSort);
            }
        }

        protected void gvMonthPlan_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthPlanList();
            string rowID = this.gvMonthPlan.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in plans)
                {
                    if (item.PlanId == rowID)
                    {
                        plans.Remove(item);
                        break;
                    }
                }
                gvMonthPlan.DataSource = plans;
                gvMonthPlan.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 审查记录修编情况修编、审核
        /// </summary>
        private void GetReviewRecordSort()
        {
            //施工方案
            List<Model.Solution_ConstructSolution> constructSolution = BLL.ConstructSolutionService.GetConstructSolutionListByDate(this.ProjectId, startTime, endTime);
            if (constructSolution.Count > 0)
            {
                foreach (Model.Solution_ConstructSolution item in constructSolution)
                {
                    Model.Manager_Month_ReviewRecordC reviewRecord = new Model.Manager_Month_ReviewRecordC
                    {
                        ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC)),
                        ReviewRecordName = item.ConstructSolutionName
                    };
                    List<Model.Sys_User> users = (from x in Funs.DB.Sys_User
                                                  join y in Funs.DB.Sys_FlowOperate
                                                  on x.UserId equals y.OperaterId
                                                  join z in Funs.DB.Project_ProjectUser
                                                  on y.OperaterId equals z.UserId
                                                  where (z.RoleId.Contains(BLL.Const.HSSEEngineer) || z.RoleId.Contains(BLL.Const.HSSEManager)) && y.MenuId == BLL.Const.ProjectConstructSolutionMenuId && y.DataId == item.ConstructSolutionId
                                                  select x).Distinct().ToList();
                    if (users.Count() > 0)
                    {
                        string names = string.Empty;
                        foreach (var user in users)
                        {
                            names += user.UserName + ",";
                        }
                        if (!string.IsNullOrEmpty(names))
                        {
                            names = names.Substring(0, names.LastIndexOf(","));
                        }
                        reviewRecord.ReviewMan = names;
                    }

                    if (item.CompileDate != null)
                    {
                        reviewRecord.ReviewDate = string.Format("{0:yyyy-MM-dd}", item.CompileDate);
                    }
                    reviewRecords.Add(reviewRecord);
                }
            }
            //分包商审核记录
            List<Model.QualityAudit_SubUnitQualityAuditDetail> subUnitDetails = BLL.SubUnitQualityAuditDetailService.GetListByDate(this.ProjectId, startTime, endTime);
            if (subUnitDetails.Count > 0)
            {
                foreach (Model.QualityAudit_SubUnitQualityAuditDetail item in subUnitDetails)
                {
                    Model.Manager_Month_ReviewRecordC reviewRecord = new Model.Manager_Month_ReviewRecordC
                    {
                        ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC)),
                        ReviewRecordName = item.AuditContent
                    };
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(item.AuditMan);
                    if (user != null)
                    {
                        reviewRecord.ReviewMan = user.UserName;
                    }
                    if (item.AuditDate != null)
                    {
                        reviewRecord.ReviewDate = string.Format("{0:yyyy-MM-dd}", item.AuditDate);
                    }
                    reviewRecords.Add(reviewRecord);
                }
            }
            //特种设备审核记录
            List<Model.QualityAudit_EquipmentQualityAuditDetail> equipmentDetails = BLL.EquipmentQualityAuditDetailService.GetListByDate(this.ProjectId, startTime, endTime);
            if (equipmentDetails.Count > 0)
            {
                foreach (Model.QualityAudit_EquipmentQualityAuditDetail item in equipmentDetails)
                {
                    Model.Manager_Month_ReviewRecordC reviewRecord = new Model.Manager_Month_ReviewRecordC
                    {
                        ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC)),
                        ReviewRecordName = item.AuditContent
                    };
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(item.AuditMan);
                    if (user != null)
                    {
                        reviewRecord.ReviewMan = user.UserName;
                    }
                    if (item.AuditDate != null)
                    {
                        reviewRecord.ReviewDate = string.Format("{0:yyyy-MM-dd}", item.AuditDate);
                    }
                    reviewRecords.Add(reviewRecord);
                }
            }
            //特岗人员资质
            List<Model.QualityAudit_PersonQuality> personDetails = BLL.PersonQualityService.GetListByDate(this.ProjectId, startTime, endTime);
            if (personDetails.Count > 0)
            {
                foreach (Model.QualityAudit_PersonQuality item in personDetails)
                {
                    Model.Manager_Month_ReviewRecordC reviewRecord = new Model.Manager_Month_ReviewRecordC
                    {
                        ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC))
                    };
                    string personName = string.Empty;
                    Model.SitePerson_Person person = BLL.PersonService.GetPersonById(item.PersonId);
                    if (person != null)
                    {
                        personName = person.PersonName;
                    }
                    reviewRecord.ReviewRecordName = personName + "(" + item.CertificateName + ")";
                    reviewRecord.ReviewMan = item.ApprovalPerson;
                    if (item.AuditDate != null)
                    {
                        reviewRecord.ReviewDate = string.Format("{0:yyyy-MM-dd}", item.AuditDate);
                    }
                    reviewRecords.Add(reviewRecord);
                }
            }
            //一般机具资质
            List<Model.QualityAudit_GeneralEquipmentQuality> generalEquipmentDetails = BLL.GeneralEquipmentQualityService.GetListByDate(this.ProjectId, startTime, endTime);
            if (generalEquipmentDetails.Count > 0)
            {
                foreach (Model.QualityAudit_GeneralEquipmentQuality item in generalEquipmentDetails)
                {
                    Model.Manager_Month_ReviewRecordC reviewRecord = new Model.Manager_Month_ReviewRecordC
                    {
                        ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC))
                    };
                    string generalEquipmentName = string.Empty;
                    Model.Base_SpecialEquipment specialEquipment = BLL.SpecialEquipmentService.GetSpecialEquipmentById(item.SpecialEquipmentId);
                    if (specialEquipment != null)
                    {
                        generalEquipmentName = specialEquipment.SpecialEquipmentName;
                    }
                    reviewRecord.ReviewRecordName = generalEquipmentName + "(" + item.EquipmentCount.ToString() + ")";
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(item.CompileMan);
                    if (user != null)
                    {
                        reviewRecord.ReviewMan = user.UserName;
                    }
                    if (item.InDate != null)
                    {
                        reviewRecord.ReviewDate = string.Format("{0:yyyy-MM-dd}", item.InDate);
                    }
                    reviewRecords.Add(reviewRecord);
                }
            }
            this.gvReviewRecord.DataSource = reviewRecords;
            this.gvReviewRecord.DataBind();
        }

        /// <summary>
        /// 增加本月文件、方案修编情况说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewReviewRecord_Click(object sender, EventArgs e)
        {
            jerqueSaveReviewRecordList();
            Model.Manager_Month_ReviewRecordC reviewRecordSort = new Model.Manager_Month_ReviewRecordC
            {
                ReviewRecordId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ReviewRecordC))
            };
            reviewRecords.Add(reviewRecordSort);
            this.gvReviewRecord.DataSource = reviewRecords;
            this.gvReviewRecord.DataBind();
        }

        /// <summary>
        /// 检查并保存其他HSE管理活动集合
        /// </summary>
        private void jerqueSaveReviewRecordList()
        {
            reviewRecords.Clear();
            JArray mergedData = gvReviewRecord.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_ReviewRecordC reviewRecordSort = new Model.Manager_Month_ReviewRecordC
                {
                    ReviewRecordId = this.gvReviewRecord.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ReviewRecordName = values.Value<string>("ReviewRecordName").ToString(),
                    ReviewMan = values.Value<string>("ReviewMan").ToString(),
                    ReviewDate = values.Value<string>("ReviewDate").ToString()
                };
                reviewRecords.Add(reviewRecordSort);
            }
        }

        protected void gvReviewRecord_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveReviewRecordList();
            string rowID = this.gvReviewRecord.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in reviewRecords)
                {
                    if (item.ReviewRecordId == rowID)
                    {
                        reviewRecords.Remove(item);
                        break;
                    }
                }
                gvReviewRecord.DataSource = reviewRecords;
                gvReviewRecord.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Manager_MonthReportC oldMonthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
            if (oldMonthReport != null)
            {
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperatePlanSort(MonthReportId);
                OperateReviewRecordSort(MonthReportId);
                BLL.LogService.AddSys_Log(this.CurrUser, oldMonthReport.MonthReportCode, oldMonthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnModify);
            }
            else
            {
                Model.Manager_MonthReportC monthReport = new Model.Manager_MonthReportC();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportC));
                monthReport.MonthReportId = newKeyID;
                monthReport.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = newKeyID;
                monthReport.MonthReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthCMenuId, this.ProjectId, this.CurrUser.UnitId);
                monthReport.Months = Funs.GetNewDateTime(Request.Params["months"]);
                monthReport.ReportMan = this.CurrUser.UserId;
                monthReport.MonthReportDate = DateTime.Now;
                BLL.MonthReportCService.AddMonthReport(monthReport);
                OperatePlanSort(MonthReportId);
                OperateReviewRecordSort(MonthReportId);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 5.1.2 本月文件、方案修编情况说明
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperatePlanSort(string monthReportId)
        {
            BLL.PlanCService.DeletePlanByMonthReportId(monthReportId);
            jerqueSaveMonthPlanList();
            foreach (Model.Manager_Month_PlanC plan in plans)
            {
                plan.MonthReportId = monthReportId;
                BLL.PlanCService.AddPlan(plan);
            }
        }

        /// <summary>
        /// 5.2.2 详细审查记录
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateReviewRecordSort(string monthReportId)
        {
            BLL.ReviewRecordCService.DeleteReviewRecordByMonthReportId(monthReportId);
            jerqueSaveReviewRecordList();
            foreach (Model.Manager_Month_ReviewRecordC reviewRecord in reviewRecords)
            {
                reviewRecord.MonthReportId = monthReportId;
                BLL.ReviewRecordCService.AddReviewRecord(reviewRecord);
            }
        }
        #endregion
    }
}