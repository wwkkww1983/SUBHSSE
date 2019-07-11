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
    public partial class MonthReportCEdit8 : PageBase
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
        /// 9.1 危险源动态识别及控制集合
        /// </summary>
        private static List<Model.Manager_Month_HazardC> hazards = new List<Model.Manager_Month_HazardC>();

        /// <summary>
        /// 9.2 HSSE培训集合
        /// </summary>
        private static List<Model.Manager_Month_TrainC> trains = new List<Model.Manager_Month_TrainC>();

        /// <summary>
        /// 9.3 HSSE检查集合
        /// </summary>
        private static List<Model.Manager_Month_CheckC> checks = new List<Model.Manager_Month_CheckC>();

        /// <summary>
        /// 9.4 HSSE会议集合
        /// </summary>
        private static List<Model.Manager_Month_MeetingC> meetings = new List<Model.Manager_Month_MeetingC>();

        /// <summary>
        /// 9.5 HSSE活动集合
        /// </summary>
        private static List<Model.Manager_Month_ActivitiesC> activitiess = new List<Model.Manager_Month_ActivitiesC>();

        /// <summary>
        /// 9.6.1 应急预案修编集合
        /// </summary>
        private static List<Model.Manager_Month_EmergencyPlanC> emergencyPlans = new List<Model.Manager_Month_EmergencyPlanC>();

        /// <summary>
        /// 9.6.2 应急演练活动集合
        /// </summary>
        private static List<Model.Manager_Month_EmergencyExercisesC> emergencyExercisess = new List<Model.Manager_Month_EmergencyExercisesC>();

        /// <summary>
        /// 9.7 HSE费用投入计划集合
        /// </summary>
        private static List<Model.Manager_Month_CostInvestmentPlanC> costInvestmentPlans = new List<Model.Manager_Month_CostInvestmentPlanC>();

        /// <summary>
        /// 9.8 HSE管理文件/方案修编计划集合
        /// </summary>
        private static List<Model.Manager_Month_ManageDocPlanC> manageDocPlans = new List<Model.Manager_Month_ManageDocPlanC>();

        /// <summary>
        /// 9.9 其他HSE工作计划
        /// </summary>
        private static List<Model.Manager_Month_OtherWorkPlanC> otherWorkPlans = new List<Model.Manager_Month_OtherWorkPlanC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hazards.Clear();
                trains.Clear();
                checks.Clear();
                meetings.Clear();
                activitiess.Clear();
                emergencyPlans.Clear();
                emergencyExercisess.Clear();
                costInvestmentPlans.Clear();
                manageDocPlans.Clear();
                otherWorkPlans.Clear();
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
                    this.txtNextEmergencyResponse.Text = monthReport.NextEmergencyResponse;
                    Model.SUBHSSEDB db = Funs.DB;
                    //9.1 危险源动态识别及控制
                    hazards = (from x in db.Manager_Month_HazardC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazards.Count > 0)
                    {
                        this.gvHazard.DataSource = hazards;
                        this.gvHazard.DataBind();
                    }
                    //9.3 HSSE检查
                    checks = (from x in db.Manager_Month_CheckC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checks.Count > 0)
                    {
                        this.gvCheck.DataSource = checks;
                        this.gvCheck.DataBind();
                    }
                    //9.8 HSE管理文件/方案修编计划
                    manageDocPlans = (from x in db.Manager_Month_ManageDocPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (manageDocPlans.Count > 0)
                    {
                        this.gvManageDocPlan.DataSource = manageDocPlans;
                        this.gvManageDocPlan.DataBind();
                    }
                    //9.9其他HSE工作计划
                    otherWorkPlans = (from x in db.Manager_Month_OtherWorkPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherWorkPlans.Count > 0)
                    {
                        this.gvOtherWorkPlan.DataSource = otherWorkPlans;
                        this.gvOtherWorkPlan.DataBind();
                    }
                }
            }
        }
        #endregion

        #region 危险源动态识别及控制
        /// <summary>
        /// 增加危险源动态识别及控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewHazard_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthHazardList();
            Model.Manager_Month_HazardC hazardSort = new Model.Manager_Month_HazardC
            {
                HazardId = SQLHelper.GetNewID(typeof(Model.Manager_Month_HazardC))
            };
            hazards.Add(hazardSort);
            this.gvHazard.DataSource = hazards;
            this.gvHazard.DataBind();
        }

        /// <summary>
        /// 检查并保存危险源动态识别及控制集合
        /// </summary>
        private void jerqueSaveMonthHazardList()
        {
            hazards.Clear();
            JArray mergedData = gvHazard.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_HazardC hazardSort = new Model.Manager_Month_HazardC
                {
                    HazardId = this.gvHazard.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    WorkArea = values.Value<string>("WorkArea").ToString(),
                    Subcontractor = values.Value<string>("Subcontractor").ToString(),
                    DangerousSource = values.Value<string>("DangerousSource").ToString(),
                    ControlMeasures = values.Value<string>("ControlMeasures").ToString()
                };
                hazards.Add(hazardSort);
            }
        }

        protected void gvHazard_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthHazardList();
            string rowID = this.gvHazard.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in hazards)
                {
                    if (item.HazardId == rowID)
                    {
                        hazards.Remove(item);
                        break;
                    }
                }
                gvHazard.DataSource = hazards;
                gvHazard.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE检查
        /// <summary>
        /// 增加HSE检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewCheck_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthCheckList();
            Model.Manager_Month_CheckC checkSort = new Model.Manager_Month_CheckC
            {
                CheckId = SQLHelper.GetNewID(typeof(Model.Manager_Month_CheckC))
            };
            checks.Add(checkSort);
            this.gvCheck.DataSource = checks;
            this.gvCheck.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE检查集合
        /// </summary>
        private void jerqueSaveMonthCheckList()
        {
            checks.Clear();
            JArray mergedData = gvCheck.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_CheckC checkSort = new Model.Manager_Month_CheckC
                {
                    CheckId = this.gvCheck.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    CheckType = values.Value<string>("CheckType").ToString(),
                    Inspectors = values.Value<string>("Inspectors").ToString(),
                    CheckDate = values.Value<string>("CheckDate").ToString(),
                    Remark = values.Value<string>("Remark").ToString()
                };
                checks.Add(checkSort);
            }
        }

        protected void gvCheck_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthCheckList();
            string rowID = this.gvCheck.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in checks)
                {
                    if (item.CheckId == rowID)
                    {
                        checks.Remove(item);
                        break;
                    }
                }
                gvCheck.DataSource = checks;
                gvCheck.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE管理文件/方案修编计划
        /// <summary>
        /// 增加HSE管理文件/方案修编计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewManageDocPlan_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthManageDocPlanList();
            Model.Manager_Month_ManageDocPlanC manageDocPlanSort = new Model.Manager_Month_ManageDocPlanC
            {
                ManageDocPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ManageDocPlanC))
            };
            manageDocPlans.Add(manageDocPlanSort);
            this.gvManageDocPlan.DataSource = manageDocPlans;
            this.gvManageDocPlan.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE管理文件/方案修编计划集合
        /// </summary>
        private void jerqueSaveMonthManageDocPlanList()
        {
            manageDocPlans.Clear();
            JArray mergedData = gvManageDocPlan.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_ManageDocPlanC manageDocPlanSort = new Model.Manager_Month_ManageDocPlanC
                {
                    ManageDocPlanId = this.gvManageDocPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ManageDocPlanName = values.Value<string>("ManageDocPlanName").ToString(),
                    CompileMan = values.Value<string>("CompileMan").ToString(),
                    CompileDate = values.Value<string>("CompileDate").ToString()
                };
                manageDocPlans.Add(manageDocPlanSort);
            }
        }

        protected void gvManageDocPlan_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthManageDocPlanList();
            string rowID = this.gvManageDocPlan.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in manageDocPlans)
                {
                    if (item.ManageDocPlanId == rowID)
                    {
                        manageDocPlans.Remove(item);
                        break;
                    }
                }
                gvManageDocPlan.DataSource = manageDocPlans;
                gvManageDocPlan.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 其他HSE工作计划
        /// <summary>
        /// 增加其他HSE工作计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewOtherWorkPlan_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthOtherWorkPlanList();
            Model.Manager_Month_OtherWorkPlanC otherWorkPlanSort = new Model.Manager_Month_OtherWorkPlanC
            {
                OtherWorkPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherWorkPlanC))
            };
            otherWorkPlans.Add(otherWorkPlanSort);
            this.gvOtherWorkPlan.DataSource = otherWorkPlans;
            this.gvOtherWorkPlan.DataBind();
        }

        /// <summary>
        /// 检查并保存其他HSE工作计划集合
        /// </summary>
        private void jerqueSaveMonthOtherWorkPlanList()
        {
            otherWorkPlans.Clear();
            JArray mergedData = gvOtherWorkPlan.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_OtherWorkPlanC otherWorkPlanSort = new Model.Manager_Month_OtherWorkPlanC
                {
                    OtherWorkPlanId = this.gvOtherWorkPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    WorkContent = values.Value<string>("WorkContent").ToString()
                };
                otherWorkPlans.Add(otherWorkPlanSort);
            }
        }

        protected void gvOtherWorkPlan_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthOtherWorkPlanList();
            string rowID = this.gvOtherWorkPlan.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in otherWorkPlans)
                {
                    if (item.OtherWorkPlanId == rowID)
                    {
                        otherWorkPlans.Remove(item);
                        break;
                    }
                }
                gvOtherWorkPlan.DataSource = otherWorkPlans;
                gvOtherWorkPlan.DataBind();
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
                oldMonthReport.NextEmergencyResponse = this.txtNextEmergencyResponse.Text.Trim();
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperateHazardListSort(MonthReportId);
                OperateCheckListSort(MonthReportId);
                OperateManageDocPlanSort(MonthReportId);
                OperateOtherWorkPlanSort(MonthReportId);
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
                monthReport.NextEmergencyResponse = this.txtNextEmergencyResponse.Text.Trim();
                BLL.MonthReportCService.AddMonthReport(monthReport);
                OperateHazardListSort(MonthReportId);
                OperateCheckListSort(MonthReportId);
                OperateManageDocPlanSort(MonthReportId);
                OperateOtherWorkPlanSort(MonthReportId);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 9.1 危险源动态识别及控制
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateHazardListSort(string monthReportId)
        {
            BLL.HazardCService.DeleteHazardByMonthReportId(monthReportId);
            jerqueSaveMonthHazardList();
            foreach (Model.Manager_Month_HazardC hazard in hazards)
            {
                hazard.MonthReportId = monthReportId;
                BLL.HazardCService.AddHazard(hazard);
            }
        }

        /// <summary>
        /// 9.3 HSSE检查
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateCheckListSort(string monthReportId)
        {
            BLL.CheckCService.DeleteCheckByMonthReportId(monthReportId);
            jerqueSaveMonthCheckList();
            foreach (Model.Manager_Month_CheckC check in checks)
            {
                check.MonthReportId = monthReportId;
                BLL.CheckCService.AddCheck(check);
            }
        }

        /// <summary>
        /// 9.8 HSE管理文件/方案修编计划
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateManageDocPlanSort(string monthReportId)
        {
            BLL.ManageDocPlanCService.DeleteManageDocPlanByMonthReportId(monthReportId);
            jerqueSaveMonthManageDocPlanList();
            foreach (Model.Manager_Month_ManageDocPlanC item in manageDocPlans)
            {
                item.MonthReportId = monthReportId;
                BLL.ManageDocPlanCService.AddManageDocPlan(item);
            }
        }

        /// <summary>
        /// 9.9 其他HSE工作计划
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateOtherWorkPlanSort(string monthReportId)
        {
            BLL.OtherWorkPlanCService.DeleteOtherWorkPlanByMonthReportId(monthReportId);
            jerqueSaveMonthOtherWorkPlanList();
            foreach (Model.Manager_Month_OtherWorkPlanC item in otherWorkPlans)
            {
                item.MonthReportId = monthReportId;
                BLL.OtherWorkPlanCService.AddOtherWorkPlan(item);
            }
        }
        #endregion
    }
}