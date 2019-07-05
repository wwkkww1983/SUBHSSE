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
    public partial class MonthReportCEdit6 : PageBase
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
                    GetFiveExpenseList(); //五环HSE费用投入
                }
                else
                {
                    GetFiveExpenseList(); //五环HSE费用投入
                }
            }
        }
        #endregion

        #region 五环费用投入
        /// <summary>
        /// 五环费用投入
        /// </summary>
        private void GetFiveExpenseList()
        {
            decimal? sMonthType1 = 0, sMonthType2 = 0, sMonthType3 = 0, sMonthType4 = 0, sMonthType5 = 0, sMonthType6 = 0;
            decimal? tMonthType1 = 0, tMonthType2 = 0;
            List<Model.CostGoods_PayRegistration> payRegistrations = BLL.PayRegistrationService.GetPayRegistrationByPayDate(startTime, endTime, this.ProjectId);
            if (payRegistrations != null)
            {
                foreach (var item in payRegistrations)
                {
                    sMonthType1 += item.SMonthType1_1 + item.SMonthType1_2 + item.SMonthType1_3 + item.SMonthType1_4 + item.SMonthType1_5 + item.SMonthType1_6 + item.SMonthType1_7 + item.SMonthType1_8 + item.SMonthType1_9 + item.SMonthType1_10 + item.SMonthType1_11 + item.SMonthType1_12 + item.SMonthType1_13 + item.SMonthType1_14 + item.SMonthType1_15 + item.SMonthType1_16;
                    sMonthType2 += item.SMonthType2_1 + item.SMonthType2_2 + item.SMonthType2_3 + item.SMonthType2_4;
                    sMonthType3 += item.SMonthType3_1 + item.SMonthType3_2 + item.SMonthType3_3 + item.SMonthType3_4 + item.SMonthType3_5 + item.SMonthType3_6;
                    sMonthType4 += item.SMonthType4_1 + item.SMonthType4_2 + item.SMonthType4_3 + item.SMonthType4_4 + item.SMonthType4_5 + item.SMonthType4_6 + item.SMonthType4_7 + item.SMonthType4_8 + item.SMonthType4_9 + item.SMonthType4_10 + item.SMonthType4_11 + item.SMonthType4_12 + item.SMonthType4_13 + item.SMonthType4_14 + item.SMonthType4_15 + item.SMonthType4_16 + item.SMonthType4_17 + item.SMonthType4_18 + item.SMonthType4_19 + item.SMonthType4_20 + item.SMonthType4_21 + item.SMonthType4_22 + item.SMonthType4_23 + item.SMonthType4_24 + item.SMonthType4_25 + item.SMonthType4_26 + item.SMonthType4_27 + item.SMonthType4_28 + item.SMonthType4_29 + item.SMonthType4_30 + item.SMonthType4_31 + item.SMonthType4_32 + item.SMonthType4_33 + item.SMonthType4_34 + item.SMonthType4_35 + item.SMonthType4_35 + item.SMonthType4_36 + item.SMonthType4_37 + item.SMonthType4_38 + item.SMonthType4_39 + item.SMonthType4_40;
                    sMonthType5 += item.SMonthType5_1 + item.SMonthType5_2 + item.SMonthType5_3 + item.SMonthType5_4 + item.SMonthType5_5 + item.SMonthType5_6;
                    sMonthType6 += item.SMonthType6_1 + item.SMonthType6_2 + item.SMonthType6_3;
                    tMonthType1 += item.TMonthType1_1 + item.TMonthType1_2 + item.TMonthType1_3 + item.TMonthType1_4 + item.TMonthType1_5 + item.TMonthType1_6 + item.TMonthType1_7 + item.TMonthType1_8 + item.TMonthType1_9 + item.TMonthType1_10 + item.TMonthType1_11;
                    tMonthType2 += item.TMonthType2_1 + item.TMonthType2_2 + item.TMonthType2_3 + item.TMonthType2_4 + item.TMonthType2_5 + item.TMonthType2_6 + item.TMonthType2_7 + item.TMonthType2_8 + item.TMonthType2_9;
                }
                this.txtSMonthType1.Text = sMonthType1.ToString();
                this.txtSMonthType2.Text = sMonthType2.ToString();
                this.txtSMonthType3.Text = sMonthType3.ToString();
                this.txtSMonthType4.Text = sMonthType4.ToString();
                this.txtSMonthType5.Text = sMonthType5.ToString();
                this.txtSMonthType6.Text = sMonthType6.ToString();
                this.txtTMonthType1.Text = tMonthType1.ToString();
                this.txtTMonthType2.Text = tMonthType2.ToString();
            }
            decimal? yearSMonthType1 = 0, yearSMonthType2 = 0, yearSMonthType3 = 0, yearSMonthType4 = 0, yearSMonthType5 = 0, yearSMonthType6 = 0;
            decimal? yearTMonthType1 = 0, yearTMonthType2 = 0;
            Model.Manager_MonthReportC mc = BLL.MonthReportCService.GetMonthReportByMonthReportId(Request.Params["monthReportId"]);
            DateTime? monthReportDate = null;
            if (mc != null)
            {
                monthReportDate = mc.MonthReportDate;
            }
            else
            {
                monthReportDate = DateTime.Now;
            }
            var yPayRegistrations = BLL.PayRegistrationService.GetPayRegistrationByYear(this.ProjectId, monthReportDate.Value);
            foreach (var item in yPayRegistrations)
            {
                yearSMonthType1 += item.SMonthType1_1 + item.SMonthType1_2 + item.SMonthType1_3 + item.SMonthType1_4 + item.SMonthType1_5 + item.SMonthType1_6 + item.SMonthType1_7 + item.SMonthType1_8 + item.SMonthType1_9 + item.SMonthType1_10 + item.SMonthType1_11 + item.SMonthType1_12 + item.SMonthType1_13 + item.SMonthType1_14 + item.SMonthType1_15 + item.SMonthType1_16;
                yearSMonthType2 += item.SMonthType2_1 + item.SMonthType2_2 + item.SMonthType2_3 + item.SMonthType2_4;
                yearSMonthType3 += item.SMonthType3_1 + item.SMonthType3_2 + item.SMonthType3_3 + item.SMonthType3_4 + item.SMonthType3_5 + item.SMonthType3_6;
                yearSMonthType4 += item.SMonthType4_1 + item.SMonthType4_2 + item.SMonthType4_3 + item.SMonthType4_4 + item.SMonthType4_5 + item.SMonthType4_6 + item.SMonthType4_7 + item.SMonthType4_8 + item.SMonthType4_9 + item.SMonthType4_10 + item.SMonthType4_11 + item.SMonthType4_12 + item.SMonthType4_13 + item.SMonthType4_14 + item.SMonthType4_15 + item.SMonthType4_16 + item.SMonthType4_17 + item.SMonthType4_18 + item.SMonthType4_19 + item.SMonthType4_20 + item.SMonthType4_21 + item.SMonthType4_22 + item.SMonthType4_23 + item.SMonthType4_24 + item.SMonthType4_25 + item.SMonthType4_26 + item.SMonthType4_27 + item.SMonthType4_28 + item.SMonthType4_29 + item.SMonthType4_30 + item.SMonthType4_31 + item.SMonthType4_32 + item.SMonthType4_33 + item.SMonthType4_34 + item.SMonthType4_35 + item.SMonthType4_35 + item.SMonthType4_36 + item.SMonthType4_37 + item.SMonthType4_38 + item.SMonthType4_39 + item.SMonthType4_40;
                yearSMonthType5 += item.SMonthType5_1 + item.SMonthType5_2 + item.SMonthType5_3 + item.SMonthType5_4 + item.SMonthType5_5 + item.SMonthType5_6;
                yearSMonthType6 += item.SMonthType6_1 + item.SMonthType6_2 + item.SMonthType6_3;
                yearTMonthType1 += item.TMonthType1_1 + item.TMonthType1_2 + item.TMonthType1_3 + item.TMonthType1_4 + item.TMonthType1_5 + item.TMonthType1_6 + item.TMonthType1_7 + item.TMonthType1_8 + item.TMonthType1_9 + item.TMonthType1_10 + item.TMonthType1_11;
                yearTMonthType2 += item.TMonthType2_1 + item.TMonthType2_2 + item.TMonthType2_3 + item.TMonthType2_4 + item.TMonthType2_5 + item.TMonthType2_6 + item.TMonthType2_7 + item.TMonthType2_8 + item.TMonthType2_9;

            }
            this.txtYearSMonthType1.Text = yearSMonthType1.ToString();
            this.txtYearSMonthType2.Text = yearSMonthType2.ToString();
            this.txtYearSMonthType3.Text = yearSMonthType3.ToString();
            this.txtYearSMonthType4.Text = yearSMonthType4.ToString();
            this.txtYearSMonthType5.Text = yearSMonthType5.ToString();
            this.txtYearSMonthType6.Text = yearSMonthType6.ToString();
            this.txtYearTMonthType1.Text = yearTMonthType1.ToString();
            this.txtYearTMonthType2.Text = yearTMonthType2.ToString();
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

                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加HSE月报告", monthReport.MonthReportId);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        #endregion
    }
}