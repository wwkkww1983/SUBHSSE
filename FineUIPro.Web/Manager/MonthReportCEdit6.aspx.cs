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
                else
                {
                    GetFiveExpenseList(); //费用投入
                }
            }
        }
        #endregion

        #region 费用投入
        /// <summary>
        /// 费用投入
        /// </summary>
        private void GetFiveExpenseList()
        {
            Model.Manager_MonthReportC mr = BLL.MonthReportCService.GetLastMonthReportByDate(endTime, this.ProjectId);
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
                    //tMonthType2 += item.TMonthType2_1 + item.TMonthType2_2 + item.TMonthType2_3 + item.TMonthType2_4 + item.TMonthType2_5 + item.TMonthType2_6 + item.TMonthType2_7 + item.TMonthType2_8 + item.TMonthType2_9;
                }
                this.nbMainCost1.Text = sMonthType1.ToString();
                this.nbMainCost2.Text = sMonthType2.ToString();
                this.nbMainCost3.Text = sMonthType3.ToString();
                this.nbMainCost4.Text = sMonthType4.ToString();
                this.nbMainCost5.Text = sMonthType5.ToString();
                this.nbMainCost6.Text = sMonthType6.ToString();
                this.nbMainCost7.Text = tMonthType1.ToString();
                this.nbMainCost.Text = (sMonthType1 + sMonthType2 + sMonthType3 + sMonthType4 + sMonthType5 + sMonthType6 + tMonthType1).ToString();
            }
            if (mr != null)
            {
                this.nbMainProjectCost1.Text = ((mr.MainProjectCost1 ?? 0) + sMonthType1).ToString();
                this.nbMainProjectCost2.Text = ((mr.MainProjectCost2 ?? 0) + sMonthType2).ToString();
                this.nbMainProjectCost3.Text = ((mr.MainProjectCost3 ?? 0) + sMonthType3).ToString();
                this.nbMainProjectCost4.Text = ((mr.MainProjectCost4 ?? 0) + sMonthType4).ToString();
                this.nbMainProjectCost5.Text = ((mr.MainProjectCost5 ?? 0) + sMonthType5).ToString();
                this.nbMainProjectCost6.Text = ((mr.MainProjectCost6 ?? 0) + sMonthType6).ToString();
                this.nbMainProjectCost7.Text = ((mr.MainProjectCost7 ?? 0) + tMonthType1).ToString();
                this.nbMainProjectCost.Text = ((mr.MainProjectCost ?? 0) + (sMonthType1 + sMonthType2 + sMonthType3 + sMonthType4 + sMonthType5 + sMonthType6 + tMonthType1)).ToString();
            }
            else
            {

                this.nbMainProjectCost1.Text = sMonthType1.ToString();
                this.nbMainProjectCost2.Text = sMonthType2.ToString();
                this.nbMainProjectCost3.Text = sMonthType3.ToString();
                this.nbMainProjectCost4.Text = sMonthType4.ToString();
                this.nbMainProjectCost5.Text = sMonthType5.ToString();
                this.nbMainProjectCost6.Text = sMonthType6.ToString();
                this.nbMainProjectCost7.Text = tMonthType1.ToString();
                this.nbMainProjectCost.Text =  (sMonthType1 + sMonthType2 + sMonthType3 + sMonthType4 + sMonthType5 + sMonthType6 + tMonthType1).ToString();
            }

            decimal? subMonthType1 = 0, subMonthType2 = 0, subMonthType3 = 0, subMonthType4 = 0, subMonthType5 = 0, subMonthType6 = 0, subMonthType7 = 0;
            List<Model.CostGoods_SubPayRegistration> subPayRegistrations = BLL.SubPayRegistrationService.GetSubPayRegistrationByPayDate(startTime, endTime, this.ProjectId);
            if (subPayRegistrations != null)
            {
                foreach (var item in subPayRegistrations)
                {
                    subMonthType1 += item.SMainApproveType1 + item.SMainApproveType2 + item.SMainApproveType3 + item.SMainApproveType4 + item.SMainApproveType5;
                    subMonthType2 += item.SMainApproveType6;
                    subMonthType3 += item.SMainApproveType7;
                    subMonthType4 += item.SMainApproveType8 + item.SMainApproveType9 + item.SMainApproveType10 + item.SMainApproveType11 + item.SMainApproveType12 + item.SMainApproveType13 + item.SMainApproveType14 + item.SMainApproveType15 + item.SMainApproveType16 + item.SMainApproveType17 + item.SMainApproveType18 + item.SMainApproveType19 + item.SMainApproveType20 + item.SMainApproveType21;
                    subMonthType5 += item.SMainApproveType22 + item.SMainApproveType23 + item.SMainApproveType24 + item.SMainApproveType25 + item.SMainApproveType26 + item.SMainApproveType27;
                    subMonthType6 += item.SMainApproveType28;
                    subMonthType6 += item.SMainApproveType29;
                }
                this.nbSubCost1.Text = subMonthType1.ToString();
                this.nbSubCost2.Text = subMonthType2.ToString();
                this.nbSubCost3.Text = subMonthType3.ToString();
                this.nbSubCost4.Text = subMonthType4.ToString();
                this.nbSubCost5.Text = subMonthType5.ToString();
                this.nbSubCost6.Text = subMonthType6.ToString();
                this.nbSubCost7.Text = subMonthType7.ToString();
                this.nbSubCost.Text = (subMonthType1 + subMonthType2 + subMonthType3 + subMonthType4 + subMonthType5 + subMonthType6 + subMonthType7).ToString();
            }
            if (mr != null)
            {
                this.nbSubProjectCost1.Text = ((mr.SubProjectCost1 ?? 0) + subMonthType1).ToString();
                this.nbSubProjectCost2.Text = ((mr.SubProjectCost2 ?? 0) + subMonthType2).ToString();
                this.nbSubProjectCost3.Text = ((mr.SubProjectCost3 ?? 0) + subMonthType3).ToString();
                this.nbSubProjectCost4.Text = ((mr.SubProjectCost4 ?? 0) + subMonthType4).ToString();
                this.nbSubProjectCost5.Text = ((mr.SubProjectCost5 ?? 0) + subMonthType5).ToString();
                this.nbSubProjectCost6.Text = ((mr.SubProjectCost6 ?? 0) + subMonthType6).ToString();
                this.nbSubProjectCost7.Text = ((mr.SubProjectCost7 ?? 0) + subMonthType7).ToString();
                this.nbSubProjectCost.Text = ((mr.SubProjectCost ?? 0) + (subMonthType1 + subMonthType2 + subMonthType3 + subMonthType4 + subMonthType5 + subMonthType6 + subMonthType7)).ToString();
                this.nbJianAnProjectCost.Text = (mr.JianAnProjectCost ?? 0).ToString();
            }
            else
            {
                this.nbSubProjectCost1.Text = subMonthType1.ToString();
                this.nbSubProjectCost2.Text = subMonthType2.ToString();
                this.nbSubProjectCost3.Text = subMonthType3.ToString();
                this.nbSubProjectCost4.Text = subMonthType4.ToString();
                this.nbSubProjectCost5.Text = subMonthType5.ToString();
                this.nbSubProjectCost6.Text = subMonthType6.ToString();
                this.nbSubProjectCost7.Text = subMonthType7.ToString();
                this.nbSubProjectCost.Text = (subMonthType1 + subMonthType2 + subMonthType3 + subMonthType4 + subMonthType5 + subMonthType6 + subMonthType7).ToString();
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
                oldMonthReport.MainCost1 = Funs.GetNewDecimalOrZero(this.nbMainCost1.Text.Trim());
                oldMonthReport.MainProjectCost1 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost1.Text.Trim());
                oldMonthReport.SubCost1 = Funs.GetNewDecimalOrZero(this.nbSubCost1.Text.Trim());
                oldMonthReport.SubProjectCost1 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost1.Text.Trim());
                oldMonthReport.MainCost2 = Funs.GetNewDecimalOrZero(this.nbMainCost2.Text.Trim());
                oldMonthReport.MainProjectCost2 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost2.Text.Trim());
                oldMonthReport.SubCost2 = Funs.GetNewDecimalOrZero(this.nbSubCost2.Text.Trim());
                oldMonthReport.SubProjectCost2 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost2.Text.Trim());
                oldMonthReport.MainCost3 = Funs.GetNewDecimalOrZero(this.nbMainCost3.Text.Trim());
                oldMonthReport.MainProjectCost3 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost3.Text.Trim());
                oldMonthReport.SubCost3 = Funs.GetNewDecimalOrZero(this.nbSubCost3.Text.Trim());
                oldMonthReport.SubProjectCost3 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost3.Text.Trim());
                oldMonthReport.MainCost4 = Funs.GetNewDecimalOrZero(this.nbMainCost4.Text.Trim());
                oldMonthReport.MainProjectCost4 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost4.Text.Trim());
                oldMonthReport.SubCost4 = Funs.GetNewDecimalOrZero(this.nbSubCost4.Text.Trim());
                oldMonthReport.SubProjectCost4 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost4.Text.Trim());
                oldMonthReport.MainCost5 = Funs.GetNewDecimalOrZero(this.nbMainCost5.Text.Trim());
                oldMonthReport.MainProjectCost5 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost5.Text.Trim());
                oldMonthReport.SubCost5 = Funs.GetNewDecimalOrZero(this.nbSubCost5.Text.Trim());
                oldMonthReport.SubProjectCost5 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost5.Text.Trim());
                oldMonthReport.MainCost6 = Funs.GetNewDecimalOrZero(this.nbMainCost6.Text.Trim());
                oldMonthReport.MainProjectCost6 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost6.Text.Trim());
                oldMonthReport.SubCost6 = Funs.GetNewDecimalOrZero(this.nbSubCost6.Text.Trim());
                oldMonthReport.SubProjectCost6 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost6.Text.Trim());
                oldMonthReport.MainCost7 = Funs.GetNewDecimalOrZero(this.nbMainCost7.Text.Trim());
                oldMonthReport.MainProjectCost7 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost7.Text.Trim());
                oldMonthReport.SubCost7 = Funs.GetNewDecimalOrZero(this.nbSubCost7.Text.Trim());
                oldMonthReport.SubProjectCost7 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost7.Text.Trim());
                oldMonthReport.MainCost = Funs.GetNewDecimalOrZero(this.nbMainCost.Text.Trim());
                oldMonthReport.MainProjectCost = Funs.GetNewDecimalOrZero(this.nbMainProjectCost.Text.Trim());
                oldMonthReport.SubCost = Funs.GetNewDecimalOrZero(this.nbSubCost.Text.Trim());
                oldMonthReport.SubProjectCost = Funs.GetNewDecimalOrZero(this.nbSubProjectCost.Text.Trim());
                oldMonthReport.JianAnCost = Funs.GetNewDecimalOrZero(this.nbJianAnCost.Text.Trim());
                oldMonthReport.JianAnProjectCost = Funs.GetNewDecimalOrZero(this.nbJianAnProjectCost.Text.Trim());
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);

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
                monthReport.MainCost1 = Funs.GetNewDecimalOrZero(this.nbMainCost1.Text.Trim());
                monthReport.MainProjectCost1 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost1.Text.Trim());
                monthReport.SubCost1 = Funs.GetNewDecimalOrZero(this.nbSubCost1.Text.Trim());
                monthReport.SubProjectCost1 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost1.Text.Trim());
                monthReport.MainCost2 = Funs.GetNewDecimalOrZero(this.nbMainCost2.Text.Trim());
                monthReport.MainProjectCost2 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost2.Text.Trim());
                monthReport.SubCost2 = Funs.GetNewDecimalOrZero(this.nbSubCost2.Text.Trim());
                monthReport.SubProjectCost2 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost2.Text.Trim());
                monthReport.MainCost3 = Funs.GetNewDecimalOrZero(this.nbMainCost3.Text.Trim());
                monthReport.MainProjectCost3 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost3.Text.Trim());
                monthReport.SubCost3 = Funs.GetNewDecimalOrZero(this.nbSubCost3.Text.Trim());
                monthReport.SubProjectCost3 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost3.Text.Trim());
                monthReport.MainCost4 = Funs.GetNewDecimalOrZero(this.nbMainCost4.Text.Trim());
                monthReport.MainProjectCost4 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost4.Text.Trim());
                monthReport.SubCost4 = Funs.GetNewDecimalOrZero(this.nbSubCost4.Text.Trim());
                monthReport.SubProjectCost4 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost4.Text.Trim());
                monthReport.MainCost5 = Funs.GetNewDecimalOrZero(this.nbMainCost5.Text.Trim());
                monthReport.MainProjectCost5 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost5.Text.Trim());
                monthReport.SubCost5 = Funs.GetNewDecimalOrZero(this.nbSubCost5.Text.Trim());
                monthReport.SubProjectCost5 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost5.Text.Trim());
                monthReport.MainCost6 = Funs.GetNewDecimalOrZero(this.nbMainCost6.Text.Trim());
                monthReport.MainProjectCost6 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost6.Text.Trim());
                monthReport.SubCost6 = Funs.GetNewDecimalOrZero(this.nbSubCost6.Text.Trim());
                monthReport.SubProjectCost6 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost6.Text.Trim());
                monthReport.MainCost7 = Funs.GetNewDecimalOrZero(this.nbMainCost7.Text.Trim());
                monthReport.MainProjectCost7 = Funs.GetNewDecimalOrZero(this.nbMainProjectCost7.Text.Trim());
                monthReport.SubCost7 = Funs.GetNewDecimalOrZero(this.nbSubCost7.Text.Trim());
                monthReport.SubProjectCost7 = Funs.GetNewDecimalOrZero(this.nbSubProjectCost7.Text.Trim());
                monthReport.MainCost = Funs.GetNewDecimalOrZero(this.nbMainCost.Text.Trim());
                monthReport.MainProjectCost = Funs.GetNewDecimalOrZero(this.nbMainProjectCost.Text.Trim());
                monthReport.SubCost = Funs.GetNewDecimalOrZero(this.nbSubCost.Text.Trim());
                monthReport.SubProjectCost = Funs.GetNewDecimalOrZero(this.nbSubProjectCost.Text.Trim());
                monthReport.JianAnCost = Funs.GetNewDecimalOrZero(this.nbJianAnCost.Text.Trim());
                monthReport.JianAnProjectCost = Funs.GetNewDecimalOrZero(this.nbJianAnProjectCost.Text.Trim());
                BLL.MonthReportCService.AddMonthReport(monthReport);

                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }


        #endregion
    }
}