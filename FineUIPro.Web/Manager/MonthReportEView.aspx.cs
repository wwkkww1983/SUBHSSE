using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportEView : PageBase
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
        /// 月报告查主键
        /// </summary>
        public string LastMonthReportId
        {
            get
            {
                return (string)ViewState["LastMonthReportId"];
            }
            set
            {
                ViewState["LastMonthReportId"] = value;
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
        #endregion

        private static DateTime startTime;

        private static DateTime endTime;

        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MonthReportId = Request.Params["monthReportId"];
                this.txtReportMan.Text = this.CurrUser.UserName;
                this.ProjectId = this.CurrUser.LoginProjectId;
                //1.项目情况
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.txtProjectName.Text = project.ProjectName;
                    string startDate = string.Empty, endDate = string.Empty;
                    if (project.StartDate != null)
                    {
                        startDate = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    }
                    if (project.EndDate != null)
                    {
                        endDate = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    }
                    this.txtStartEndDate.Text = startDate + "/" + endDate;
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId.Contains(BLL.Const.ProjectManager));
                    if (m != null)
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                        if (user != null)
                        {
                            this.txtProjectManagerName.Text = user.UserName;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_MonthReportE monthReport = BLL.MonthReportEService.GetMonthReportByMonthReportId(MonthReportId);
                    if (monthReport != null)
                    {
                        startTime = Convert.ToDateTime(monthReport.Months);
                        endTime = startTime.AddMonths(1);
                        this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        this.txtMonthReportCode.Text = monthReport.MonthReportCode;
                        if (monthReport.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", monthReport.Months);
                        }
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        this.txtCountryCities.Text = monthReport.CountryCities;
                        this.txtContractType.Text = monthReport.ContractType;
                        if (monthReport.ContractAmount != null)
                        {
                            this.txtContractAmount.Text = monthReport.ContractAmount.ToString();
                        }
                        this.txtThisMajorWork.Text = monthReport.ThisMajorWork;
                        this.txtNextMajorWork.Text = monthReport.NextMajorWork;
                        if (monthReport.ThisIncome != null)
                        {
                            this.txtThisIncome.Text = monthReport.ThisIncome.ToString();
                        }
                        if (monthReport.YearIncome != null)
                        {
                            this.txtYearIncome.Text = monthReport.YearIncome.ToString();
                        }
                        if (monthReport.TotalIncome != null)
                        {
                            this.txtTotalIncome.Text = monthReport.TotalIncome.ToString();
                        }
                        this.txtThisImageProgress.Text = monthReport.ThisImageProgress;
                        this.txtYearImageProgress.Text = monthReport.YearImageProgress;
                        this.txtTotalImageProgress.Text = monthReport.TotalImageProgress;
                        if (monthReport.YearPersonNum != null)
                        {
                            this.txtYearPersonNum.Text = monthReport.YearPersonNum.ToString();
                        }
                        if (monthReport.TotalPersonNum != null)
                        {
                            this.txtTotalPersonNum.Text = monthReport.TotalPersonNum.ToString();
                        }
                        if (monthReport.ThisForeignPersonNum != null)
                        {
                            this.txtThisForeignPersonNum.Text = monthReport.ThisForeignPersonNum.ToString();
                        }
                        if (monthReport.YearForeignPersonNum != null)
                        {
                            this.txtYearForeignPersonNum.Text = monthReport.YearForeignPersonNum.ToString();
                        }
                        if (monthReport.TotalForeignPersonNum != null)
                        {
                            this.txtTotalForeignPersonNum.Text = monthReport.TotalForeignPersonNum.ToString();
                        }
                        this.txtProjectManagerPhone.Text = monthReport.ProjectManagerPhone;
                        this.txtHSEManagerName.Text = monthReport.HSEManagerName;
                        this.txtHSEManagerPhone.Text = monthReport.HSEManagerPhone;
                        //本月现场员工总数
                        if (monthReport.ThisPersonNum != null)
                        {
                            this.txtThisPersonNum.Text = monthReport.ThisPersonNum.ToString();
                        }
                        //本月HSSE教育培训（人/次）
                        if (monthReport.ThisTrainPersonNum != null)
                        {
                            this.txtThisTrainPersonNum.Text = monthReport.ThisTrainPersonNum.ToString();
                        }
                        //本年累计HSSE教育培训（人/次）
                        if (monthReport.YearTrainPersonNum != null)
                        {
                            this.txtYearTrainPersonNum.Text = monthReport.YearTrainPersonNum.ToString();
                        }
                        //自开工累计HSSE教育培训（人/次）
                        if (monthReport.TotalTrainPersonNum != null)
                        {
                            this.txtTotalTrainPersonNum.Text = monthReport.TotalTrainPersonNum.ToString();
                        }
                        //本月HSSE检查（次）
                        if (monthReport.ThisCheckNum != null)
                        {
                            this.txtThisCheckNum.Text = monthReport.ThisCheckNum.ToString();
                        }
                        //本年累计HSSE检查（次）
                        if (monthReport.YearCheckNum != null)
                        {
                            this.txtYearCheckNum.Text = monthReport.YearCheckNum.ToString();
                        }
                        //自开工累计HSSE检查（次）
                        if (monthReport.TotalCheckNum != null)
                        {
                            this.txtTotalCheckNum.Text = monthReport.TotalCheckNum.ToString();
                        }
                        //本月HSSE隐患排查治理（项）
                        if (monthReport.ThisViolationNum != null)
                        {
                            this.txtThisViolationNum.Text = monthReport.ThisViolationNum.ToString();
                        }
                        //本年累计HSSE隐患排查治理（项）
                        if (monthReport.YearViolationNum != null)
                        {
                            this.txtYearViolationNum.Text = monthReport.YearViolationNum.ToString();
                        }
                        //自开工累计HSSE隐患排查治理（项）
                        if (monthReport.TotalViolationNum != null)
                        {
                            this.txtTotalViolationNum.Text = monthReport.TotalViolationNum.ToString();
                        }
                        //本月HSSE投入
                        if (monthReport.ThisInvestment != null)
                        {
                            this.txtThisInvestment.Text = monthReport.ThisInvestment.ToString();
                        }
                        //本年累计HSSE投入
                        if (monthReport.YearInvestment != null)
                        {
                            this.txtYearInvestment.Text = monthReport.YearInvestment.ToString();
                        }
                        //自开工累计HSSE投入
                        if (monthReport.TotalInvestment != null)
                        {
                            this.txtTotalInvestment.Text = monthReport.TotalInvestment.ToString();
                        }
                        //本月HSSE奖励
                        if (monthReport.ThisReward != null)
                        {
                            this.txtThisReward.Text = monthReport.ThisReward.ToString();
                        }
                        //本年累计HSSE奖励
                        if (monthReport.YearReward != null)
                        {
                            this.txtYearReward.Text = monthReport.YearReward.ToString();
                        }
                        //自开工累计HSSE奖励
                        if (monthReport.TotalReward != null)
                        {
                            this.txtTotalReward.Text = monthReport.TotalReward.ToString();
                        }
                        //本月HSSE处罚
                        if (monthReport.ThisPunish != null)
                        {
                            this.txtThisPunish.Text = monthReport.ThisPunish.ToString();
                        }
                        //本年累计HSSE处罚
                        if (monthReport.YearPunish != null)
                        {
                            this.txtYearPunish.Text = monthReport.YearPunish.ToString();
                        }
                        //自开工累计HSSE处罚
                        if (monthReport.TotalPunish != null)
                        {
                            this.txtTotalPunish.Text = monthReport.TotalPunish.ToString();
                        }
                        //本月HSSE应急演练（次）
                        if (monthReport.ThisEmergencyDrillNum != null)
                        {
                            this.txtThisEmergencyDrillNum.Text = monthReport.ThisEmergencyDrillNum.ToString();
                        }
                        //本年累计HSSE应急演练（次）
                        if (monthReport.YearEmergencyDrillNum != null)
                        {
                            this.txtYearEmergencyDrillNum.Text = monthReport.YearEmergencyDrillNum.ToString();
                        }
                        //自开工累计HSSE应急演练（次）
                        if (monthReport.TotalEmergencyDrillNum != null)
                        {
                            this.txtTotalEmergencyDrillNum.Text = monthReport.TotalEmergencyDrillNum.ToString();
                        }
                        //HSE工时
                        if (monthReport.ThisHSEManhours != null)
                        {
                            this.txtThisHSEManhours.Text = (monthReport.ThisHSEManhours ?? 0).ToString("N0");
                        }
                        if (monthReport.YearHSEManhours != null)
                        {
                            this.txtYearHSEManhours.Text = (monthReport.YearHSEManhours ?? 0).ToString("N0");
                        }
                        if (monthReport.TotalHSEManhours != null)
                        {
                            this.txtTotalHSEManhours.Text = (monthReport.TotalHSEManhours ?? 0).ToString("N0");
                        }
                        //本月可记录HSE事件
                        if (monthReport.ThisRecordEvent != null)
                        {
                            this.txtThisRecordEvent.Text = monthReport.ThisRecordEvent.ToString(); 
                        }
                        //本年累计可记录HSE事件
                        if (monthReport.YearRecordEvent != null)
                        {
                            this.txtYearRecordEvent.Text = monthReport.YearRecordEvent.ToString(); 
                        }
                        //自开工累计可记录HSE事件
                        if (monthReport.TotalRecordEvent != null)
                        {
                            this.txtTotalRecordEvent.Text = monthReport.TotalRecordEvent.ToString();
                        }
                        //本月不可记录HSE事件
                        if (monthReport.ThisNoRecordEvent != null)
                        {
                            this.txtThisNoRecordEvent.Text = monthReport.ThisNoRecordEvent.ToString();
                        }
                        //本年累计不可记录HSE事件
                        if (monthReport.YearNoRecordEvent != null)
                        {
                            this.txtYearNoRecordEvent.Text = monthReport.YearNoRecordEvent.ToString();
                        }
                        //自开工累计不可记录HSE事件
                        if (monthReport.TotalNoRecordEvent != null)
                        {
                            this.txtTotalNoRecordEvent.Text = monthReport.TotalNoRecordEvent.ToString();
                        }
                    }
                }
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}&type=-1", this.MonthReportId, BLL.Const.ProjectManagerMonthEMenuId)));
        }
        #endregion
    }
}