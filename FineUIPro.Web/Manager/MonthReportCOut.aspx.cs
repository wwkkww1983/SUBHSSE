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
    public partial class MonthReportCOut : PageBase
    {
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

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonthReportId(MonthReportId);
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                Model.Base_Unit mainUnit = BLL.UnitService.GetThisUnitDropDownList()[0];
                if (monthReport != null)
                {
                    DateTime thisMonth = Convert.ToDateTime(monthReport.Months);
                    DateTime lastMonth = thisMonth.AddMonths(-1);
                    this.lbReportDate.Text = "报告期： " + lastMonth.Year.ToString() + "年 " + lastMonth.Month.ToString() + "月  26日  至  " + thisMonth.Month.ToString() + "月25日";
                    this.lbReportCode.Text = "顺序号： " + monthReport.MonthReportCode;
                    Model.Sys_User reportMan = BLL.UserService.GetUserByUserId(monthReport.ReportMan);
                    if (reportMan != null)
                    {
                        this.lblReportMan.Text = reportMan.UserName;
                    }
                    if (monthReport.MonthReportDate != null)
                    {
                        this.lblMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.MonthReportDate);
                    }
                    this.lbProjectName.Text = project.ProjectName;
                    if (mainUnit != null)
                    {
                        this.lblMainUnitName.Text = mainUnit.UnitName;
                    }
                    this.lblProjectAddress.Text = project.ProjectAddress;
                    this.lblProjectCode.Text = project.ProjectCode;
                    this.lblContractNo.Text = project.ContractNo;  //合同号
                    if (!string.IsNullOrEmpty(project.ProjectType))
                    {
                        Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_ProjectType).FirstOrDefault(x => x.ConstValue == project.ProjectType);
                        if (c != null)
                        {
                            this.lblProjectType.Text = c.ConstText;
                        }
                    }
                    this.lblWorkRange.Text = project.WorkRange;    //工程范围
                    if (project.Duration != null)
                    {
                        this.lblDuration.Text = project.Duration.ToString();      //工期（月）
                    }
                    if (project.StartDate != null)
                    {
                        this.lblStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    }
                    if (project.EndDate != null)
                    {
                        this.lblEndDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    }
                    Model.SUBHSSEDB db = Funs.DB;
                    var q = from x in db.Manager_PersonSortC
                            where x.MonthReportId == MonthReportId
                            select x;
                    if (q.Count() > 0)
                    {
                        //人力投入情况
                        List<Model.Manager_PersonSortC> sorts = (from x in db.Manager_PersonSortC
                                                                 join y in db.Project_ProjectUnit
                                                                 on x.UnitId equals y.UnitId
                                                                 where x.MonthReportId == MonthReportId && y.ProjectId == this.ProjectId
                                                                 orderby y.UnitType
                                                                 select x).Distinct().ToList();
                        List<Model.Manager_PersonSortC> personSorts = new List<Model.Manager_PersonSortC>();
                        var units = from x in db.Project_ProjectUnit
                                    where x.ProjectId == this.ProjectId && (x.UnitType == "1" || x.UnitType == "2")
                                    orderby x.UnitType
                                    select x;     //1为总包，2为施工分包
                        foreach (var unit in units)
                        {
                            Model.Manager_PersonSortC personSort = sorts.FirstOrDefault(x => x.UnitId == unit.UnitId);
                            if (personSort != null)
                            {
                                personSorts.Add(personSort);
                            }
                        }
                        Model.Manager_PersonSortC personSortTotal = new Model.Manager_PersonSortC();
                        personSortTotal.UnitId = "Total";
                        if (personSorts.Count > 0)
                        {
                            personSortTotal.SumPersonNum = (from x in personSorts select x.SumPersonNum ?? 0).Sum();
                            personSortTotal.HSEPersonNum = (from x in personSorts select x.HSEPersonNum ?? 0).Sum();
                        }
                        else
                        {
                            personSortTotal.SumPersonNum = 0;
                            personSortTotal.HSEPersonNum = 0;
                        }
                        personSorts.Add(personSortTotal);
                        this.gvPersonSort.DataSource = personSorts;
                        this.gvPersonSort.DataBind();
                    }
                    this.lbMonthHSEDay.Text = (monthReport.MonthHSEDay ?? 0).ToString();
                    this.lbSumHSEDay.Text = (monthReport.SumHSEDay ?? 0).ToString();
                    this.lbMonthHSEWorkDay.Text = (monthReport.MonthHSEWorkDay ?? 0).ToString();
                    this.lbYearHSEWorkDay.Text = (monthReport.YearHSEWorkDay ?? 0).ToString();
                    this.lbSumHSEWorkDay.Text = (monthReport.SumHSEWorkDay ?? 0).ToString();
                    this.lbHseManhours.Text = (monthReport.HseManhours ?? 0).ToString();
                    this.lbSubcontractManHours.Text = (monthReport.SubcontractManHours ?? 0).ToString();
                    this.lbTotalHseManhours.Text = (monthReport.TotalHseManhours ?? 0).ToString();
                    this.txtMainActivitiesDef.Text = monthReport.MainActivitiesDef;
                    //危险源情况
                    var hazardSorts = (from x in db.Manager_HazardSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazardSorts.Count > 0)   //保存过数据
                    {
                        this.gvHazardSort.DataSource = hazardSorts;
                        this.gvHazardSort.DataBind();
                    }
                    //培训情况
                    var trainSorts = (from x in db.Manager_TrainSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (trainSorts.Count > 0)
                    {
                        this.gvTrainSort.DataSource = trainSorts;
                        this.gvTrainSort.DataBind();
                    }
                    //培训活动情况
                    var trainActivitySorts = (from x in db.Manager_TrainActivitySortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (trainActivitySorts.Count > 0)
                    {
                        this.gvTrainActivitySort.DataSource = trainActivitySorts;
                        this.gvTrainActivitySort.DataBind();
                    }
                    //检查情况
                    var checkSorts = (from x in db.Manager_CheckSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checkSorts.Count > 0)
                    {
                        this.gvCheckSort.DataSource = checkSorts;
                        this.gvCheckSort.DataBind();
                    }
                    //检查明细情况
                    var checkDetailSorts = (from x in db.Manager_CheckDetailSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checkDetailSorts.Count > 0)
                    {
                        this.gvCheckDetailSort.DataSource = checkDetailSorts;
                        this.gvCheckDetailSort.DataBind();
                    }
                    this.txtMeetingNum.Text = (monthReport.MeetingNum ?? 0).ToString();
                    this.txtYearMeetingNum.Text = (monthReport.YearMeetingNum ?? 0).ToString();
                    //会议情况
                    var meetingSorts = (from x in db.Manager_MeetingSortC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (meetingSorts.Count > 0)
                    {
                        this.gvMeetingSort.DataSource = meetingSorts;
                        this.gvMeetingSort.DataBind();
                    }
                    this.txtComplexEmergencyNum.Text = (monthReport.ComplexEmergencyNum ?? 0).ToString();
                    this.txtYearComplexEmergencyNum.Text = (monthReport.YearComplexEmergencyNum ?? 0).ToString();
                    this.txtTotalComplexEmergencyNum.Text = (monthReport.TotalComplexEmergencyNum ?? 0).ToString();
                    this.txtSpecialEmergencyNum.Text = (monthReport.SpecialEmergencyNum ?? 0).ToString();
                    this.txtYearSpecialEmergencyNum.Text = (monthReport.YearSpecialEmergencyNum ?? 0).ToString();
                    this.txtTotalSpecialEmergencyNum.Text = (monthReport.TotalSpecialEmergencyNum ?? 0).ToString();
                    this.txtDrillRecordNum.Text = (monthReport.DrillRecordNum ?? 0).ToString();
                    this.txtYearDrillRecordNum.Text = (monthReport.YearDrillRecordNum ?? 0).ToString();
                    this.txtTotalDrillRecordNum.Text = (monthReport.TotalDrillRecordNum ?? 0).ToString();
                    this.txtEmergencyManagementWorkDef.Text = monthReport.EmergencyManagementWorkDef;
                    this.txtLicenseRemark.Text = monthReport.LicenseRemark;
                    this.txtEquipmentRemark.Text = monthReport.EquipmentRemark;
                    //HSE奖励情况
                    var rewardSorts = (from x in db.Manager_IncentiveSortC where x.MonthReportId == MonthReportId && x.BigType == "1" orderby x.SortIndex select x).ToList();
                    if (rewardSorts.Count > 0)
                    {
                        this.gvRewardSort.DataSource = rewardSorts;
                        this.gvRewardSort.DataBind();
                    }
                    this.txtRewardNum.Text = (monthReport.RewardNum ?? 0).ToString();
                    this.txtYearRewardNum.Text = (monthReport.YearRewardNum ?? 0).ToString();
                    this.txtRewardMoney.Text = (monthReport.RewardMoney ?? 0).ToString();
                    this.txtYearRewardMoney.Text = (monthReport.YearRewardMoney ?? 0).ToString();
                    //HSE处罚情况
                    var punishSorts = (from x in db.Manager_IncentiveSortC where x.MonthReportId == MonthReportId && x.BigType == "2" orderby x.SortIndex select x).ToList();
                    if (punishSorts.Count > 0)
                    {
                        this.gvPunishSort.DataSource = punishSorts;
                        this.gvPunishSort.DataBind();
                    }
                    this.txtPunishNum.Text = (monthReport.PunishNum ?? 0).ToString();
                    this.txtYearPunishNum.Text = (monthReport.YearPunishNum ?? 0).ToString();
                    this.txtPunishMoney.Text = (monthReport.PunishMoney ?? 0).ToString();
                    this.txtYearPunishMoney.Text = (monthReport.YearPunishMoney ?? 0).ToString();
                    //4.10 HSE现场其他管理情况
                    var otherManagements = (from x in db.Manager_Month_OtherManagementC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherManagements.Count > 0)
                    {
                        this.gvOtherManagement.DataSource = otherManagements;
                        this.gvOtherManagement.DataBind();
                    }
                    //5.1.2 本月文件、方案修编情况说明
                    var plans = (from x in db.Manager_Month_PlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (plans.Count > 0)
                    {
                        this.gvMonthPlan.DataSource = plans;
                        this.gvMonthPlan.DataBind();
                    }
                    //5.2.2 详细审查记录
                    var reviewRecords = (from x in db.Manager_Month_ReviewRecordC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (reviewRecords.Count > 0)
                    {
                        this.gvReviewRecord.DataSource = reviewRecords;
                        this.gvReviewRecord.DataBind();
                    }
                    this.nbMainCost1.Text = (monthReport.MainCost1 ?? 0).ToString();
                    this.nbMainProjectCost1.Text = (monthReport.MainProjectCost1 ?? 0).ToString();
                    this.nbSubCost1.Text = (monthReport.SubCost1 ?? 0).ToString();
                    this.nbSubProjectCost1.Text = (monthReport.SubProjectCost1 ?? 0).ToString();
                    this.nbMainCost2.Text = (monthReport.MainCost2 ?? 0).ToString();
                    this.nbMainProjectCost2.Text = (monthReport.MainProjectCost2 ?? 0).ToString();
                    this.nbSubCost2.Text = (monthReport.SubCost2 ?? 0).ToString();
                    this.nbSubProjectCost2.Text = (monthReport.SubProjectCost2 ?? 0).ToString();
                    this.nbMainCost3.Text = (monthReport.MainCost3 ?? 0).ToString();
                    this.nbMainProjectCost3.Text = (monthReport.MainProjectCost3 ?? 0).ToString();
                    this.nbSubCost3.Text = (monthReport.SubCost3 ?? 0).ToString();
                    this.nbSubProjectCost3.Text = (monthReport.SubProjectCost3 ?? 0).ToString();
                    this.nbMainCost4.Text = (monthReport.MainCost4 ?? 0).ToString();
                    this.nbMainProjectCost4.Text = (monthReport.MainProjectCost4 ?? 0).ToString();
                    this.nbSubCost4.Text = (monthReport.SubCost4 ?? 0).ToString();
                    this.nbSubProjectCost4.Text = (monthReport.SubProjectCost4 ?? 0).ToString();
                    this.nbMainCost5.Text = (monthReport.MainCost5 ?? 0).ToString();
                    this.nbMainProjectCost5.Text = (monthReport.MainProjectCost5 ?? 0).ToString();
                    this.nbSubCost5.Text = (monthReport.SubCost5 ?? 0).ToString();
                    this.nbSubProjectCost5.Text = (monthReport.SubProjectCost5 ?? 0).ToString();
                    this.nbMainCost6.Text = (monthReport.MainCost6 ?? 0).ToString();
                    this.nbMainProjectCost6.Text = (monthReport.MainProjectCost6 ?? 0).ToString();
                    this.nbSubCost6.Text = (monthReport.SubCost6 ?? 0).ToString();
                    this.nbSubProjectCost6.Text = (monthReport.SubProjectCost6 ?? 0).ToString();
                    this.nbMainCost7.Text = (monthReport.MainCost7 ?? 0).ToString();
                    this.nbMainProjectCost7.Text = (monthReport.MainProjectCost7 ?? 0).ToString();
                    this.nbSubCost7.Text = (monthReport.SubCost7 ?? 0).ToString();
                    this.nbSubProjectCost7.Text = (monthReport.SubProjectCost7 ?? 0).ToString();
                    this.nbMainCost.Text = (monthReport.MainCost ?? 0).ToString();
                    this.nbMainProjectCost.Text = (monthReport.MainProjectCost ?? 0).ToString();
                    this.nbSubCost.Text = (monthReport.SubCost ?? 0).ToString();
                    this.nbSubProjectCost.Text = (monthReport.SubProjectCost ?? 0).ToString();
                    this.nbJianAnCost.Text = (monthReport.JianAnCost ?? 0).ToString();
                    this.nbJianAnProjectCost.Text = (monthReport.JianAnProjectCost ?? 0).ToString();
                    //7.1 管理绩效数据统计(表一)
                    var accidentDesciptions = (from x in db.Manager_Month_AccidentDesciptionC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (accidentDesciptions.Count > 0)
                    {
                        this.gvAccidentDesciption.DataSource = accidentDesciptions;
                        this.gvAccidentDesciption.DataBind();
                    }
                    //7.2 管理绩效数据统计（表二）
                    var AccidentDesciptionItems = (from x in db.Manager_Month_AccidentDesciptionItemC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (AccidentDesciptionItems.Count > 0)
                    {
                        this.gvAccidentDesciptionItem.DataSource = AccidentDesciptionItems;
                        this.gvAccidentDesciptionItem.DataBind();
                    }
                    this.txtAccidentDes.Text = monthReport.AccidentDes;
                    //9.1 危险源动态识别及控制
                    var hazards = (from x in db.Manager_Month_HazardC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (hazards.Count > 0)
                    {
                        this.gvHazard.DataSource = hazards;
                        this.gvHazard.DataBind();
                    }
                    //9.3 HSSE检查
                    var checks = (from x in db.Manager_Month_CheckC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (checks.Count > 0)
                    {
                        this.gvCheck.DataSource = checks;
                        this.gvCheck.DataBind();
                    }
                    this.txtNextEmergencyResponse.Text = monthReport.NextEmergencyResponse;
                    //9.8 HSE管理文件/方案修编计划
                    var manageDocPlans = (from x in db.Manager_Month_ManageDocPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (manageDocPlans.Count > 0)
                    {
                        this.gvManageDocPlan.DataSource = manageDocPlans;
                        this.gvManageDocPlan.DataBind();
                    }
                    //9.9其他HSE工作计划
                    var otherWorkPlans = (from x in db.Manager_Month_OtherWorkPlanC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherWorkPlans.Count > 0)
                    {
                        this.gvOtherWorkPlan.DataSource = otherWorkPlans;
                        this.gvOtherWorkPlan.DataBind();
                    }
                    this.txtQuestion.Text = monthReport.Question;
                    //this.txtPhotoContents.Text = HttpUtility.HtmlDecode(monthReport.PhotoContents);
                }
            }
        }

        #region 转换字符串
        /// <summary>
        /// 把单位Id转换为单位名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object UnitId)
        {
            if (UnitId != null)
            {
                Model.Base_Unit u = BLL.UnitService.GetUnitByUnitId(UnitId.ToString());
                if (u != null)
                {
                    return u.UnitName;
                }
                else
                {
                    return "合计：";
                }
            }
            return "";
        }


        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("管理月报" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.Table1.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}