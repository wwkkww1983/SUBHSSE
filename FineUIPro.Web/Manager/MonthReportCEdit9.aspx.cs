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
    public partial class MonthReportCEdit9 : PageBase
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
                    Model.SUBHSSEDB db = Funs.DB;
                    //9.1 危险源动态识别及控制
                    hazards = (from x in db.Manager_Month_HazardC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazards.Count > 0)
                    {
                        this.gvHazard.DataSource = hazards;
                        this.gvHazard.DataBind();
                    }
                    //9.2 HSSE培训
                    trains = (from x in db.Manager_Month_TrainC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (trains.Count > 0)
                    {
                        this.gvTrain.DataSource = trains;
                        this.gvTrain.DataBind();
                    }
                    //9.3 HSSE检查
                    checks = (from x in db.Manager_Month_CheckC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checks.Count > 0)
                    {
                        this.gvCheck.DataSource = checks;
                        this.gvCheck.DataBind();
                    }
                    //9.4 HSSE会议
                    meetings = (from x in db.Manager_Month_MeetingC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (meetings.Count > 0)
                    {
                        this.gvMeeting.DataSource = meetings;
                        this.gvMeeting.DataBind();
                    }
                    //9.5 HSSE活动
                    activitiess = (from x in db.Manager_Month_ActivitiesC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (activitiess.Count > 0)
                    {
                        this.gvActivities.DataSource = activitiess;
                        this.gvActivities.DataBind();
                    }
                    //9.6.1 应急预案修编
                    emergencyPlans = (from x in db.Manager_Month_EmergencyPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (emergencyPlans.Count > 0)
                    {
                        this.gvEmergencyPlan.DataSource = emergencyPlans;
                        this.gvEmergencyPlan.DataBind();
                    }
                    // 9.6.2 应急演练活动
                    emergencyExercisess = (from x in db.Manager_Month_EmergencyExercisesC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (emergencyExercisess.Count > 0)
                    {
                        this.gvEmergencyExercises.DataSource = emergencyExercisess;
                        this.gvEmergencyExercises.DataBind();
                    }
                    // 9.7 HSE费用投入计划
                    costInvestmentPlans = (from x in db.Manager_Month_CostInvestmentPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (costInvestmentPlans.Count > 0)
                    {
                        this.gvCostInvestmentPlan.DataSource = costInvestmentPlans;
                        this.gvCostInvestmentPlan.DataBind();
                    }
                    else
                    {
                        GetCostInvestmentPlan();
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
                else
                {
                    GetCostInvestmentPlan();
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
            int rowsCount = this.gvHazard.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_HazardC hazardSort = new Model.Manager_Month_HazardC
                {
                    HazardId = this.gvHazard.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    WorkArea = this.gvHazard.Rows[i].Values[2].ToString(),
                    Subcontractor = this.gvHazard.Rows[i].Values[3].ToString(),
                    DangerousSource = this.gvHazard.Rows[i].Values[4].ToString(),
                    ControlMeasures = this.gvHazard.Rows[i].Values[5].ToString()
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

        #region HSE培训
        /// <summary>
        /// 增加HSE培训
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewTrain_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthTrainList();
            Model.Manager_Month_TrainC trainSort = new Model.Manager_Month_TrainC
            {
                TrainId = SQLHelper.GetNewID(typeof(Model.Manager_Month_TrainC))
            };
            trains.Add(trainSort);
            this.gvTrain.DataSource = trains;
            this.gvTrain.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE培训集合
        /// </summary>
        private void jerqueSaveMonthTrainList()
        {
            trains.Clear();
            int rowsCount = this.gvTrain.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_TrainC trainSort = new Model.Manager_Month_TrainC
                {
                    TrainId = this.gvTrain.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    TrainName = this.gvTrain.Rows[i].Values[2].ToString(),
                    TrainMan = this.gvTrain.Rows[i].Values[3].ToString(),
                    TrainDate = this.gvTrain.Rows[i].Values[4].ToString(),
                    TrainObject = this.gvTrain.Rows[i].Values[5].ToString(),
                    Remark = this.gvTrain.Rows[i].Values[6].ToString()
                };
                trains.Add(trainSort);
            }
        }

        protected void gvTrain_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthTrainList();
            string rowID = this.gvTrain.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in trains)
                {
                    if (item.TrainId == rowID)
                    {
                        trains.Remove(item);
                        break;
                    }
                }
                gvTrain.DataSource = trains;
                gvTrain.DataBind();
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
            int rowsCount = this.gvCheck.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_CheckC checkSort = new Model.Manager_Month_CheckC
                {
                    CheckId = this.gvCheck.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    CheckType = this.gvCheck.Rows[i].Values[2].ToString(),
                    Inspectors = this.gvCheck.Rows[i].Values[3].ToString(),
                    CheckDate = this.gvCheck.Rows[i].Values[4].ToString(),
                    Remark = this.gvCheck.Rows[i].Values[5].ToString()
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

        #region HSE会议
        /// <summary>
        /// 增加HSE会议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewMeeting_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthMeetingList();
            Model.Manager_Month_MeetingC meetingSort = new Model.Manager_Month_MeetingC
            {
                MeetingId = SQLHelper.GetNewID(typeof(Model.Manager_Month_MeetingC))
            };
            meetings.Add(meetingSort);
            this.gvMeeting.DataSource = meetings;
            this.gvMeeting.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE会议集合
        /// </summary>
        private void jerqueSaveMonthMeetingList()
        {
            meetings.Clear();
            int rowsCount = this.gvMeeting.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_MeetingC meetingSort = new Model.Manager_Month_MeetingC
                {
                    MeetingId = this.gvMeeting.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    MeetingTitle = this.gvMeeting.Rows[i].Values[2].ToString(),
                    MeetingDate = this.gvMeeting.Rows[i].Values[3].ToString(),
                    Host = this.gvMeeting.Rows[i].Values[4].ToString(),
                    Participants = this.gvMeeting.Rows[i].Values[5].ToString()
                };
                meetings.Add(meetingSort);
            }
        }

        protected void gvMeeting_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthMeetingList();
            string rowID = this.gvMeeting.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in meetings)
                {
                    if (item.MeetingId == rowID)
                    {
                        meetings.Remove(item);
                        break;
                    }
                }
                gvMeeting.DataSource = meetings;
                gvMeeting.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region HSE活动
        /// <summary>
        /// 增加HSE活动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewActivities_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthActivitiesList();
            Model.Manager_Month_ActivitiesC activitiesSort = new Model.Manager_Month_ActivitiesC
            {
                ActivitiesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_ActivitiesC))
            };
            activitiess.Add(activitiesSort);
            this.gvActivities.DataSource = activitiess;
            this.gvActivities.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE活动集合
        /// </summary>
        private void jerqueSaveMonthActivitiesList()
        {
            activitiess.Clear();
            int rowsCount = this.gvActivities.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_ActivitiesC activitiesSort = new Model.Manager_Month_ActivitiesC
                {
                    ActivitiesId = this.gvActivities.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ActivitiesTitle = this.gvActivities.Rows[i].Values[2].ToString(),
                    ActivitiesDate = this.gvActivities.Rows[i].Values[3].ToString(),
                    Unit = this.gvActivities.Rows[i].Values[4].ToString(),
                    Remark = this.gvActivities.Rows[i].Values[5].ToString()
                };
                activitiess.Add(activitiesSort);
            }
        }

        protected void gvActivities_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthActivitiesList();
            string rowID = this.gvActivities.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in activitiess)
                {
                    if (item.ActivitiesId == rowID)
                    {
                        activitiess.Remove(item);
                        break;
                    }
                }
                gvActivities.DataSource = activitiess;
                gvActivities.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 应急预案修编
        /// <summary>
        /// 增加应急预案修编
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewEmergencyPlan_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthEmergencyPlanList();
            Model.Manager_Month_EmergencyPlanC emergencyPlanSort = new Model.Manager_Month_EmergencyPlanC
            {
                EmergencyPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_EmergencyPlanC))
            };
            emergencyPlans.Add(emergencyPlanSort);
            this.gvEmergencyPlan.DataSource = emergencyPlans;
            this.gvEmergencyPlan.DataBind();
        }

        /// <summary>
        /// 检查并保存应急预案修编集合
        /// </summary>
        private void jerqueSaveMonthEmergencyPlanList()
        {
            emergencyPlans.Clear();
            int rowsCount = this.gvEmergencyPlan.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_EmergencyPlanC emergencyPlanSort = new Model.Manager_Month_EmergencyPlanC
                {
                    EmergencyPlanId = this.gvEmergencyPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    EmergencyName = this.gvEmergencyPlan.Rows[i].Values[2].ToString(),
                    CompileMan = this.gvEmergencyPlan.Rows[i].Values[3].ToString(),
                    CompileDate = this.gvEmergencyPlan.Rows[i].Values[4].ToString(),
                    Remark = this.gvEmergencyPlan.Rows[i].Values[5].ToString()
                };
                emergencyPlans.Add(emergencyPlanSort);
            }
        }

        protected void gvEmergencyPlan_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthEmergencyPlanList();
            string rowID = this.gvEmergencyPlan.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in emergencyPlans)
                {
                    if (item.EmergencyPlanId == rowID)
                    {
                        emergencyPlans.Remove(item);
                        break;
                    }
                }
                gvEmergencyPlan.DataSource = emergencyPlans;
                gvEmergencyPlan.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 应急演练活动
        /// <summary>
        /// 增加应急演练活动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewEmergencyExercises_Click(object sender, EventArgs e)
        {
            jerqueSaveMonthEmergencyExercisesList();
            Model.Manager_Month_EmergencyExercisesC emergencyExercisesSort = new Model.Manager_Month_EmergencyExercisesC
            {
                EmergencyExercisesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_EmergencyExercisesC))
            };
            emergencyExercisess.Add(emergencyExercisesSort);
            this.gvEmergencyExercises.DataSource = emergencyExercisess;
            this.gvEmergencyExercises.DataBind();
        }

        /// <summary>
        /// 检查并保存应急演练活动集合
        /// </summary>
        private void jerqueSaveMonthEmergencyExercisesList()
        {
            emergencyExercisess.Clear();
            int rowsCount = this.gvEmergencyExercises.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_EmergencyExercisesC emergencyExercisesSort = new Model.Manager_Month_EmergencyExercisesC
                {
                    EmergencyExercisesId = this.gvEmergencyExercises.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    DrillContent = this.gvEmergencyExercises.Rows[i].Values[2].ToString(),
                    DrillDate = this.gvEmergencyExercises.Rows[i].Values[3].ToString(),
                    UnitName = this.gvEmergencyExercises.Rows[i].Values[4].ToString(),
                    ParticipantsNum = Funs.GetNewIntOrZero(this.gvEmergencyExercises.Rows[i].Values[5].ToString())
                };
                emergencyExercisess.Add(emergencyExercisesSort);
            }
        }

        protected void gvEmergencyExercises_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveMonthEmergencyExercisesList();
            string rowID = this.gvEmergencyExercises.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in emergencyExercisess)
                {
                    if (item.EmergencyExercisesId == rowID)
                    {
                        emergencyExercisess.Remove(item);
                        break;
                    }
                }
                gvEmergencyExercises.DataSource = emergencyExercisess;
                gvEmergencyExercises.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region  HSE费用投入计划
        /// <summary>
        /// 显示月报告HSE费用投入计划
        /// </summary>
        private void GetCostInvestmentPlan()
        {
            System.Web.UI.WebControls.ListItem[] list = BLL.CostInvestmentPlanCService.GetCostInvestmentPlanList();
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Model.Manager_Month_CostInvestmentPlanC des = new Model.Manager_Month_CostInvestmentPlanC
                    {
                        CostInvestmentPlanId = SQLHelper.GetNewID(typeof(Model.Manager_Month_CostInvestmentPlanC)),
                        InvestmentProject = list[i].Value,
                        MainPlanCost = 0,
                        SubPlanCost = 0
                    };
                    costInvestmentPlans.Add(des);
                }
            }
            this.gvCostInvestmentPlan.DataSource = costInvestmentPlans;
            this.gvCostInvestmentPlan.DataBind();
        }

        /// <summary>
        /// 检查并保存HSE费用投入计划集合
        /// </summary>
        private void jerqueSaveCostInvestmentPlanList()
        {
            costInvestmentPlans.Clear();
            int rowsCount = this.gvCostInvestmentPlan.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_CostInvestmentPlanC costInvestmentPlanSort = new Model.Manager_Month_CostInvestmentPlanC
                {
                    CostInvestmentPlanId = this.gvCostInvestmentPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    InvestmentProject = this.gvCostInvestmentPlan.Rows[i].Values[1].ToString(),
                    MainPlanCost = Funs.GetNewDecimalOrZero(this.gvCostInvestmentPlan.Rows[i].Values[3].ToString()),
                    SubPlanCost = Funs.GetNewDecimalOrZero(this.gvCostInvestmentPlan.Rows[i].Values[4].ToString())
                };
                costInvestmentPlans.Add(costInvestmentPlanSort);
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
            int rowsCount = this.gvManageDocPlan.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_ManageDocPlanC manageDocPlanSort = new Model.Manager_Month_ManageDocPlanC
                {
                    ManageDocPlanId = this.gvManageDocPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    ManageDocPlanName = this.gvManageDocPlan.Rows[i].Values[2].ToString(),
                    CompileMan = this.gvManageDocPlan.Rows[i].Values[3].ToString(),
                    CompileDate = this.gvManageDocPlan.Rows[i].Values[4].ToString()
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
            int rowsCount = this.gvOtherWorkPlan.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_OtherWorkPlanC otherWorkPlanSort = new Model.Manager_Month_OtherWorkPlanC
                {
                    OtherWorkPlanId = this.gvOtherWorkPlan.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    WorkContent = this.gvOtherWorkPlan.Rows[i].Values[2].ToString()
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
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperateHazardListSort(MonthReportId);
                OperateTrainListSort(MonthReportId);
                OperateCheckListSort(MonthReportId);
                OperateMeetingListSort(MonthReportId);
                OperateActivitiesSort(MonthReportId);
                OperateEmergencyPlanSort(MonthReportId);
                OperateEmergencyExercisesSort(MonthReportId);
                OperateCostInvestmentPlanSort(MonthReportId);
                OperateManageDocPlanSort(MonthReportId);
                OperateOtherWorkPlanSort(MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改HSE月报告", MonthReportId);
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
                OperateHazardListSort(MonthReportId);
                OperateTrainListSort(MonthReportId);
                OperateCheckListSort(MonthReportId);
                OperateMeetingListSort(MonthReportId);
                OperateActivitiesSort(MonthReportId);
                OperateEmergencyPlanSort(MonthReportId);
                OperateEmergencyExercisesSort(MonthReportId);
                OperateCostInvestmentPlanSort(MonthReportId);
                OperateManageDocPlanSort(MonthReportId);
                OperateOtherWorkPlanSort(MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加HSE月报告", monthReport.MonthReportId);
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
        /// 9.2 HSSE培训
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateTrainListSort(string monthReportId)
        {
            BLL.TrainCService.DeleteTrainByMonthReportId(monthReportId);
            jerqueSaveMonthTrainList();
            foreach (Model.Manager_Month_TrainC train in trains)
            {
                train.MonthReportId = monthReportId;
                BLL.TrainCService.AddTrain(train);
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
        /// 9.4 HSSE会议
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateMeetingListSort(string monthReportId)
        {
            BLL.MeetingCService.DeleteMeetingByMonthReportId(monthReportId);
            jerqueSaveMonthMeetingList();
            foreach (Model.Manager_Month_MeetingC meeting in meetings)
            {
                meeting.MonthReportId = monthReportId;
                BLL.MeetingCService.AddMeeting(meeting);
            }
        }

        /// <summary>
        /// 9.5 HSSE活动
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateActivitiesSort(string monthReportId)
        {
            BLL.ActivitiesCService.DeleteActivitiesByMonthReportId(monthReportId);
            jerqueSaveMonthActivitiesList();
            foreach (Model.Manager_Month_ActivitiesC activities in activitiess)
            {
                activities.MonthReportId = monthReportId;
                BLL.ActivitiesCService.AddActivities(activities);
            }
        }

        /// <summary>
        /// 9.6.1 应急预案修编
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateEmergencyPlanSort(string monthReportId)
        {
            BLL.EmergencyPlanCService.DeleteEmergencyPlanByMonthReportId(monthReportId);
            jerqueSaveMonthEmergencyPlanList();
            foreach (Model.Manager_Month_EmergencyPlanC plan in emergencyPlans)
            {
                plan.MonthReportId = monthReportId;
                BLL.EmergencyPlanCService.AddEmergencyPlan(plan);
            }
        }

        /// <summary>
        /// 9.6.2 应急演练活动
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateEmergencyExercisesSort(string monthReportId)
        {
            BLL.EmergencyExercisesCService.DeleteEmergencyExercisesByMonthReportId(monthReportId);
            jerqueSaveMonthEmergencyExercisesList();
            foreach (Model.Manager_Month_EmergencyExercisesC exs in emergencyExercisess)
            {
                exs.MonthReportId = monthReportId;
                BLL.EmergencyExercisesCService.AddEmergencyExercises(exs);
            }
        }

        /// <summary>
        /// 9.7 HSE费用投入计划
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateCostInvestmentPlanSort(string monthReportId)
        {
            BLL.CostInvestmentPlanCService.DeleteCostInvestmentPlanByMonthReportId(monthReportId);
            jerqueSaveCostInvestmentPlanList();
            foreach (Model.Manager_Month_CostInvestmentPlanC plan in costInvestmentPlans)
            {
                plan.MonthReportId = monthReportId;
                BLL.CostInvestmentPlanCService.AddCostInvestmentPlan(plan);
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