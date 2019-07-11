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
    public partial class MonthReportCView6 : PageBase
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
        /// 6.1 五环HSE费用投入集合
        /// </summary>
        private static List<Model.Manager_Month_FiveExpenseC> fiveExpenses = new List<Model.Manager_Month_FiveExpenseC>();

        /// <summary>
        /// 6.2 分包商HSE费用投入集合
        /// </summary>
        private static List<Model.Manager_Month_SubExpenseC> subExpenses = new List<Model.Manager_Month_SubExpenseC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fiveExpenses.Clear();
                subExpenses.Clear();
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
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
                }
            }
        }
        #endregion
    }
}