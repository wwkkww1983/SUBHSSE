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
    public partial class MonthReportBView : PageBase
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
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MonthReportId = Request.Params["monthReportId"];
                #region 1．项目信息
                //1.项目信息
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.txtProjectCode.Text = project.ProjectCode;
                    this.txtProjectName.Text = project.ProjectName;
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.RoleId == BLL.Const.ProjectManager);
                    if (m != null)
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                        if (user != null)
                        {
                            this.txtProjectManager.Text = user.UserName;
                        }
                    }
                    if (!string.IsNullOrEmpty(project.ProjectType))
                    {
                        Model.Sys_Const c = BLL.ConstValue.drpConstItemList(ConstValue.Group_ProjectType).FirstOrDefault(x => x.ConstValue == project.ProjectType);
                        if (c != null)
                        {
                            this.txtProjectType.Text = c.ConstText;
                        }
                    }
                    this.txtProjectStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    this.txtProjectEndDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    this.txtProjectAddress.Text = project.ProjectAddress;
                }
                #endregion
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(MonthReportId);
                    if (monthReport != null)
                    {
                        this.lblMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        if (monthReport.Months != null)
                        {
                            string y = monthReport.Months.Value.Year.ToString();
                            string m =monthReport.Months.Value.Month.ToString();
                            this.lblMonths.Text = y + "年" + m + "月";
                        }
                        if (monthReport.MonthReportDate != null)
                        {
                            this.lblMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.MonthReportDate);
                        }
                        if (!string.IsNullOrEmpty(monthReport.ReportMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(monthReport.ReportMan);
                            if (user != null)
                            {
                                this.lblReportMan.Text = user.UserName;
                            }
                        }
                        #region 2.项目施工现场HSE业绩统计
                        this.txtManhours.Text = (monthReport.Manhours ?? 0).ToString("###,###");
                        this.txtSumManhours.Text = (monthReport.TotalManhours ?? 0).ToString("###,###");
                        this.txtHseManhours.Text = (monthReport.HseManhours ?? 0).ToString("###,###");
                        this.txtSumHseManhours.Text = (monthReport.TotalHseManhours ?? 0).ToString("N0");
                        if (monthReport.NoStartDate != null)
                        {
                            this.txtNoStartDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.NoStartDate);
                        }
                        if (monthReport.NoEndDate != null)
                        {
                            this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.NoEndDate);
                        }
                        else
                        {
                            this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(monthReport.Months).AddMonths(1).AddDays(-1));
                        }
                        this.txtSafetyManhours.Text = (monthReport.SafetyManhours ?? 0).ToString("###,###");
                        #endregion
                        #region 3．项目施工现场人工时分类统计
                        List<Model.Manager_ManhoursSortB> manhoursSorts = BLL.ManhoursSortBService.GetManhoursSortsByMonthReportId(MonthReportId);
                        int? sumPerson = (from x in manhoursSorts select x.PersonTotal).Sum();
                        int? sumManhours = (from x in manhoursSorts select x.ManhoursTotal).Sum();
                        int? totalSumManhours = (from x in manhoursSorts select x.TotalManhoursTotal).Sum();
                        this.GridManhoursSort.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.GridManhoursSort.Columns[0].FooterText = "合计";
                        this.GridManhoursSort.Columns[1].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.GridManhoursSort.Columns[1].FooterText = (sumPerson ?? 0).ToString("N0");
                        this.GridManhoursSort.Columns[2].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.GridManhoursSort.Columns[2].FooterText = (sumManhours ?? 0).ToString("N0");
                        this.GridManhoursSort.Columns[3].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.GridManhoursSort.Columns[3].FooterText = (totalSumManhours ?? 0).ToString("N0");
                        this.GridManhoursSort.DataSourceID = null;
                        this.GridManhoursSort.DataSource = manhoursSorts;
                        this.GridManhoursSort.DataBind();
                        #endregion
                        #region 4．项目施工现场事故分类统计
                        GetAccidentSort();
                        this.txtAccidentNum.Text = monthReport.AccidentNum.ToString();
                        this.txtAccidentRateA.Text = monthReport.AccidentRateA.ToString();
                        this.txtAccidentRateB.Text = monthReport.AccidentRateB.ToString();
                        this.txtAccidentRateC.Text = monthReport.AccidentRateC.ToString();
                        this.txtAccidentRateD.Text = monthReport.AccidentRateD.ToString();
                        this.txtAccidentRateE.Text = monthReport.AccidentRateE.ToString();
                        //事故
                        List<Model.Manager_AccidentDetailSortB> accidentDetailSorts = BLL.AccidentDetailSortBService.GetAccidentDetailSortsByMonthReportId(MonthReportId);
                        this.GridAccidentDetailSort.DataSource = accidentDetailSorts;
                        this.GridAccidentDetailSort.DataBind();
                        #endregion
                        this.txtAccidentReview.Text = monthReport.AccidentReview;//5.事故综述
                        #region 6.危大工程施工方案数量统计
                        this.txtLargerHazardNum.Text = monthReport.LargerHazardNun.ToString();
                        this.txtTotalLargerHazardNum.Text = (monthReport.TotalLargerHazardNun ?? 0).ToString();
                        this.txtIsArgumentLargerHazardNun.Text = monthReport.IsArgumentLargerHazardNun.ToString();
                        this.txtTotalIsArgumentLargerHazardNun.Text = (monthReport.TotalIsArgumentLargerHazardNun ?? 0).ToString();
                        #endregion
                        #region 7.项目安全生产及文明施工措施费统计汇总表
                        //费用
                        List<Model.Manager_HseCostB> hseCosts = BLL.HseCostBService.GetHseCostsByMonthReportId(MonthReportId);
                        decimal? sumPlanCostA = (from x in hseCosts select x.PlanCostA).Sum();
                        decimal? sumPlanCostB = (from x in hseCosts select x.PlanCostB).Sum();
                        decimal? sumRealCostA = (from x in hseCosts select x.RealCostA).Sum();
                        decimal? sumProjectRealCostA = (from x in hseCosts select x.ProjectRealCostA).Sum();
                        decimal? sumRealCostB = (from x in hseCosts select x.RealCostB).Sum();
                        decimal? sumProjectRealCostB = (from x in hseCosts select x.ProjectRealCostB).Sum();
                        decimal? sumRealCostAB = (from x in hseCosts select x.RealCostAB).Sum();
                        decimal? sumProjectRealCostAB = (from x in hseCosts select x.ProjectRealCostAB).Sum();
                        this.gvHSECostSort.DataSource = hseCosts;
                        this.gvHSECostSort.DataBind();
                        #endregion
                        #region 8.项目施工现场HSE培训情况统计
                        //培训
                        List<Model.Manager_TrainSortB> trainSorts = BLL.TrainSortBService.GetTrainSortsByMonthReportId(this.MonthReportId);
                        int? sumTrainNum = (from x in trainSorts select x.TrainNumber).Sum();
                        int? sumTotalTrainNum = (from x in trainSorts select x.TotalTrainNum).Sum();
                        int? sumPersonNum = (from x in trainSorts select x.TrainPersonNumber).Sum();
                        int? sumTotalPersonNum = (from x in trainSorts select x.TotalTrainPersonNum).Sum();
                        this.gvTrainSort.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.gvTrainSort.Columns[0].FooterText = "合计";                       
                        this.gvTrainSort.Columns[1].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.gvTrainSort.Columns[1].FooterText = (sumTrainNum ?? 0).ToString("N0");
                        this.gvTrainSort.Columns[2].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.gvTrainSort.Columns[2].FooterText = (sumTotalTrainNum ?? 0).ToString("N0");
                        this.gvTrainSort.Columns[3].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.gvTrainSort.Columns[3].FooterText = (sumPersonNum ?? 0).ToString("N0");
                        this.gvTrainSort.Columns[4].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
                        this.gvTrainSort.Columns[4].FooterText = (sumTotalPersonNum ?? 0).ToString("N0");
                        this.gvTrainSort.DataSourceID = null;
                        this.gvTrainSort.DataSource = trainSorts;
                        this.gvTrainSort.DataBind();
                        #endregion
                        GetMeetingSort();// 9.项目施工现场HSE会议情况统计
                        GetCheckSort();//10.项目施工现场HSE检查情况统计
                        GetIncentiveSort();//11.项目施工现场HSE奖惩情况统计
                        this.txtHseActiveReview.Text = monthReport.HseActiveReview;
                        this.txtHseActiveKey.Text = monthReport.HseActiveKey;
                    }
                }
            }
        }

        #region 4.项目施工现场事故分类统计
        /// <summary>
        ///  4.项目施工现场事故分类统计
        /// </summary>
        private void GetAccidentSort()
        {
            Model.Manager_AccidentSortB accidentSort1 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType11.Text);
            if (accidentSort1 != null)
            {
                this.txtNumber11.Text = accidentSort1.Number.ToString();
                this.txtSumNumber11.Text = (accidentSort1.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum11.Text = accidentSort1.PersonNum.ToString();
                this.txtSumPersonNum11.Text = accidentSort1.TotalPersonNum.ToString();
                this.txtLoseHours11.Text = (accidentSort1.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours11.Text = (accidentSort1.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney11.Text = (accidentSort1.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney11.Text = (accidentSort1.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort2 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType12.Text);
            if (accidentSort1 != null)
            {
                this.txtNumber12.Text = accidentSort2.Number.ToString();
                this.txtSumNumber12.Text = (accidentSort2.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum12.Text = accidentSort2.PersonNum.ToString();
                this.txtSumPersonNum12.Text = accidentSort2.TotalPersonNum.ToString();
                this.txtLoseHours12.Text = (accidentSort2.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours12.Text = (accidentSort2.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney12.Text = (accidentSort2.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney12.Text = (accidentSort2.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort3 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType13.Text);
            if (accidentSort3 != null)
            {
                this.txtNumber13.Text = accidentSort3.Number.ToString();
                this.txtSumNumber13.Text = (accidentSort3.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum13.Text = accidentSort3.PersonNum.ToString();
                this.txtSumPersonNum13.Text = accidentSort3.TotalPersonNum.ToString();
                this.txtLoseHours13.Text = (accidentSort3.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours13.Text = (accidentSort3.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney13.Text = (accidentSort3.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney13.Text = (accidentSort3.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort4 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType14.Text);
            if (accidentSort4 != null)
            {
                this.txtNumber14.Text = accidentSort4.Number.ToString();
                this.txtSumNumber14.Text = (accidentSort4.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum14.Text = accidentSort4.PersonNum.ToString();
                this.txtSumPersonNum14.Text = accidentSort4.TotalPersonNum.ToString();
                this.txtLoseHours14.Text = (accidentSort4.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours14.Text = (accidentSort4.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney14.Text = (accidentSort4.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney14.Text = (accidentSort4.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort5 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType15.Text);
            if (accidentSort5 != null)
            {
                this.txtNumber15.Text = accidentSort5.Number.ToString();
                this.txtSumNumber15.Text = (accidentSort5.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum15.Text = accidentSort5.PersonNum.ToString();
                this.txtSumPersonNum15.Text = accidentSort5.TotalPersonNum.ToString();
                this.txtLoseHours15.Text = (accidentSort5.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours15.Text = (accidentSort5.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney15.Text = (accidentSort5.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney15.Text = (accidentSort5.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort6 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType16.Text);
            if (accidentSort6 != null)
            {
                this.txtNumber16.Text = accidentSort6.Number.ToString();
                this.txtSumNumber16.Text = (accidentSort6.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum16.Text = accidentSort6.PersonNum.ToString();
                this.txtSumPersonNum16.Text = accidentSort6.TotalPersonNum.ToString();
                this.txtLoseHours16.Text = (accidentSort6.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours16.Text = (accidentSort6.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney16.Text = (accidentSort6.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney16.Text = (accidentSort6.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort21 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType21.Text);
            if (accidentSort21 != null)
            {
                this.txtNumber21.Text = accidentSort21.Number.ToString();
                this.txtSumNumber21.Text = (accidentSort21.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum21.Text = accidentSort21.PersonNum.ToString();
                this.txtSumPersonNum21.Text = accidentSort21.TotalPersonNum.ToString();
                this.txtLoseHours21.Text = (accidentSort21.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours21.Text = (accidentSort21.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney21.Text = (accidentSort21.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney21.Text = (accidentSort21.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort22 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType22.Text);
            if (accidentSort22 != null)
            {
                this.txtNumber22.Text = accidentSort22.Number.ToString();
                this.txtSumNumber22.Text = (accidentSort22.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum22.Text = accidentSort22.PersonNum.ToString();
                this.txtSumPersonNum22.Text = accidentSort22.TotalPersonNum.ToString();
                this.txtLoseHours22.Text = (accidentSort22.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours22.Text = (accidentSort22.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney22.Text = (accidentSort22.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney22.Text = (accidentSort22.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort23 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType23.Text);
            if (accidentSort23 != null)
            {
                this.txtNumber23.Text = accidentSort23.Number.ToString();
                this.txtSumNumber23.Text = (accidentSort23.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum23.Text = accidentSort23.PersonNum.ToString();
                this.txtSumPersonNum23.Text = accidentSort23.TotalPersonNum.ToString();
                this.txtLoseHours23.Text = (accidentSort23.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours23.Text = (accidentSort23.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney23.Text = (accidentSort23.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney23.Text = (accidentSort23.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort24 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType24.Text);
            if (accidentSort24 != null)
            {
                this.txtNumber24.Text = accidentSort24.Number.ToString();
                this.txtSumNumber24.Text = (accidentSort24.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum24.Text = accidentSort24.PersonNum.ToString();
                this.txtSumPersonNum24.Text = accidentSort24.TotalPersonNum.ToString();
                this.txtLoseHours24.Text = (accidentSort24.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours24.Text = (accidentSort24.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney24.Text = (accidentSort24.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney24.Text = (accidentSort24.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort25 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType25.Text);
            if (accidentSort25 != null)
            {
                this.txtNumber25.Text = accidentSort25.Number.ToString();
                this.txtSumNumber25.Text = (accidentSort25.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum25.Text = accidentSort25.PersonNum.ToString();
                this.txtSumPersonNum25.Text = accidentSort25.TotalPersonNum.ToString();
                this.txtLoseHours25.Text = (accidentSort25.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours25.Text = (accidentSort25.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney25.Text = (accidentSort25.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney25.Text = (accidentSort25.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort26 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType26.Text);
            if (accidentSort26 != null)
            {
                this.txtNumber26.Text = accidentSort26.Number.ToString();
                this.txtSumNumber26.Text = (accidentSort26.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum26.Text = accidentSort26.PersonNum.ToString();
                this.txtSumPersonNum26.Text = accidentSort26.TotalPersonNum.ToString();
                this.txtLoseHours26.Text = (accidentSort26.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours26.Text = (accidentSort26.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney26.Text = (accidentSort26.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney26.Text = (accidentSort26.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort27 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType27.Text);
            if (accidentSort27 != null)
            {
                this.txtNumber27.Text = accidentSort27.Number.ToString();
                this.txtSumNumber27.Text = (accidentSort27.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum27.Text = accidentSort27.PersonNum.ToString();
                this.txtSumPersonNum27.Text = accidentSort27.TotalPersonNum.ToString();
                this.txtLoseHours27.Text = (accidentSort27.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours27.Text = (accidentSort27.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney27.Text = (accidentSort27.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney27.Text = (accidentSort27.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort28 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType28.Text);
            if (accidentSort28 != null)
            {
                this.txtNumber28.Text = accidentSort28.Number.ToString();
                this.txtSumNumber28.Text = (accidentSort28.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum28.Text = accidentSort28.PersonNum.ToString();
                this.txtSumPersonNum28.Text = accidentSort28.TotalPersonNum.ToString();
                this.txtLoseHours28.Text = (accidentSort28.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours28.Text = (accidentSort28.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney28.Text = (accidentSort28.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney28.Text = (accidentSort28.TotalLoseMoney ?? 0).ToString("N0");
            }

            Model.Manager_AccidentSortB accidentSort29 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(MonthReportId, this.lblAccidentType29.Text);
            if (accidentSort29 != null)
            {
                this.txtNumber29.Text = accidentSort29.Number.ToString();
                this.txtSumNumber29.Text = (accidentSort29.TotalNum ?? 0).ToString("N0");
                this.txtPersonNum29.Text = accidentSort29.PersonNum.ToString();
                this.txtSumPersonNum29.Text = accidentSort29.TotalPersonNum.ToString();
                this.txtLoseHours29.Text = (accidentSort29.LoseHours ?? 0).ToString("N0");
                this.txtSumLoseHours29.Text = (accidentSort29.TotalLoseHours ?? 0).ToString("N0");
                this.txtLoseMoney29.Text = (accidentSort29.LoseMoney ?? 0).ToString("N0");
                this.txtSumLoseMoney29.Text = (accidentSort29.TotalLoseMoney ?? 0).ToString("N0");
            }

            //事故台账
        }
        #endregion

        #region 9.项目施工现场HSE会议情况统计
        /// <summary>
        ///  9.项目施工现场HSE会议情况统计
        /// </summary>
        private void GetMeetingSort()
        {
            Model.Manager_MeetingSortB meetingSort1 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(MonthReportId, this.lblMeetingType1.Text);
            if (meetingSort1 != null)
            {
                this.txtMeetingNumber1.Text = meetingSort1.MeetingNumber.ToString();
                this.txtSumMeetingNumber1.Text = meetingSort1.TotalMeetingNum.ToString();
                this.txtMeetingPersonNumber1.Text = meetingSort1.MeetingPersonNumber.ToString();
                this.txtSumMeetingPersonNumber1.Text = meetingSort1.TotalMeetingPersonNum.ToString();
            }

            Model.Manager_MeetingSortB meetingSort2 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(MonthReportId, this.lblMeetingType2.Text);
            if (meetingSort2 != null)
            {
                this.txtMeetingNumber2.Text = meetingSort2.MeetingNumber.ToString();
                this.txtSumMeetingNumber2.Text = meetingSort2.TotalMeetingNum.ToString();
                this.txtMeetingPersonNumber2.Text = meetingSort2.MeetingPersonNumber.ToString();
                this.txtSumMeetingPersonNumber2.Text = meetingSort2.TotalMeetingPersonNum.ToString();
            }

            Model.Manager_MeetingSortB meetingSort3 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(MonthReportId, this.lblMeetingType3.Text);
            if (meetingSort3 != null)
            {
                this.txtMeetingNumber3.Text = meetingSort3.MeetingNumber.ToString();
                this.txtSumMeetingNumber3.Text = meetingSort3.TotalMeetingNum.ToString();
                this.txtMeetingPersonNumber3.Text = meetingSort3.MeetingPersonNumber.ToString();
                this.txtSumMeetingPersonNumber3.Text = meetingSort3.TotalMeetingPersonNum.ToString();
            }
            this.txtAllMeetingNumber.Text = (Funs.GetNewIntOrZero(this.txtMeetingNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtMeetingNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtMeetingNumber3.Text.Trim())).ToString();
            this.txtAllMeetingPersonNumber.Text = (Funs.GetNewIntOrZero(this.txtMeetingPersonNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtMeetingPersonNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtMeetingPersonNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingNumber.Text = (Funs.GetNewIntOrZero(this.txtSumMeetingNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumMeetingNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumMeetingNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingPersonNumber.Text = (Funs.GetNewIntOrZero(this.txtSumMeetingPersonNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumMeetingPersonNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumMeetingPersonNumber3.Text.Trim())).ToString();
        }
        #endregion

        #region 10.项目施工现场HSE检查情况统计
        /// <summary>
        ///  10.项目施工现场HSE检查情况统计
        /// </summary>
        private void GetCheckSort()
        {
            Model.Manager_CheckSortB checkSort1 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(MonthReportId, this.lblCheckType1.Text);
            if (checkSort1 != null)
            {
                this.txtCheckNumber1.Text = checkSort1.CheckNumber.ToString();
                this.txtSumCheckNumber1.Text = checkSort1.TotalCheckNum.ToString();
                this.txtViolationNumber1.Text = checkSort1.ViolationNumber.ToString();
                this.txtSumViolationNumber1.Text = checkSort1.TotalViolationNum.ToString();
            }

            Model.Manager_CheckSortB checkSort2 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(MonthReportId, this.lblCheckType2.Text);
            if (checkSort2 != null)
            {
                this.txtCheckNumber2.Text = checkSort2.CheckNumber.ToString();
                this.txtSumCheckNumber2.Text = checkSort2.TotalCheckNum.ToString();
                this.txtViolationNumber2.Text = checkSort2.ViolationNumber.ToString();
                this.txtSumViolationNumber2.Text = checkSort2.TotalViolationNum.ToString();
            }

            Model.Manager_CheckSortB checkSort3 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(MonthReportId, this.lblCheckType3.Text);
            if (checkSort3 != null)
            {
                this.txtCheckNumber3.Text = checkSort3.CheckNumber.ToString();
                this.txtSumCheckNumber3.Text = checkSort3.TotalCheckNum.ToString();
                this.txtViolationNumber3.Text = checkSort3.ViolationNumber.ToString();
                this.txtSumViolationNumber3.Text = checkSort3.TotalViolationNum.ToString();
            }

            Model.Manager_CheckSortB checkSort4 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(MonthReportId, this.lblCheckType4.Text);
            if (checkSort4 != null)
            {
                this.txtCheckNumber4.Text = checkSort4.CheckNumber.ToString();
                this.txtSumCheckNumber4.Text = checkSort4.TotalCheckNum.ToString();
                this.txtViolationNumber4.Text = checkSort4.ViolationNumber.ToString();
                this.txtSumViolationNumber4.Text = checkSort4.TotalViolationNum.ToString();
            }

            this.txtAllCheckNumber.Text = (Funs.GetNewIntOrZero(this.txtCheckNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtCheckNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtCheckNumber3.Text.Trim()) + Funs.GetNewIntOrZero(this.txtCheckNumber4.Text.Trim())).ToString();
            this.txtAllSumCheckNumber.Text = (Funs.GetNewIntOrZero(this.txtSumCheckNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumCheckNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumCheckNumber3.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumCheckNumber4.Text.Trim())).ToString();
            this.txtAllViolationNumber.Text = (Funs.GetNewIntOrZero(this.txtViolationNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtViolationNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtViolationNumber3.Text.Trim()) + Funs.GetNewIntOrZero(this.txtViolationNumber4.Text.Trim())).ToString();
            this.txtAllSumViolationNumber.Text = (Funs.GetNewIntOrZero(this.txtSumViolationNumber1.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumViolationNumber2.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumViolationNumber3.Text.Trim()) + Funs.GetNewIntOrZero(this.txtSumViolationNumber4.Text.Trim())).ToString();
        }
        #endregion

        #region 11.项目施工现场HSE奖惩情况统计
        /// <summary>
        ///  11.项目施工现场HSE奖惩情况统计
        /// </summary>
        private void GetIncentiveSort()
        {
            Model.Manager_IncentiveSortB incentiveSort1 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, this.lblIncentiveType11.Text);
            if (incentiveSort1 != null)
            {
                this.txtIncentiveMoney1.Text = string.Format("{0:N2}", incentiveSort1.IncentiveMoney);
                this.txtSumIncentiveMoney1.Text = string.Format("{0:N2}", incentiveSort1.TotalIncentiveMoney);
            }

            Model.Manager_IncentiveSortB incentiveSort2 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, this.lblIncentiveType12.Text);
            if (incentiveSort2 != null)
            {
                this.txtIncentiveMoney2.Text = string.Format("{0:N2}", incentiveSort2.IncentiveMoney);
                this.txtSumIncentiveMoney2.Text = string.Format("{0:N2}", incentiveSort2.TotalIncentiveMoney);
            }

            Model.Manager_IncentiveSortB incentiveSort3 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, this.lblIncentiveType13.Text);
            if (incentiveSort3 != null)
            {
                this.txtIncentiveMoney3.Text = string.Format("{0:N2}", incentiveSort3.IncentiveMoney);
                this.txtSumIncentiveMoney3.Text = string.Format("{0:N2}", incentiveSort3.TotalIncentiveMoney);
            }

            Model.Manager_IncentiveSortB incentiveSort4 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, this.lblIncentiveType14.Text);
            if (incentiveSort4 != null)
            {
                this.txtIncentiveMoney4.Text = string.Format("{0:N2}", incentiveSort4.IncentiveMoney);
                this.txtSumIncentiveMoney4.Text = string.Format("{0:N2}", incentiveSort4.TotalIncentiveMoney);
            }

            Model.Manager_IncentiveSortB incentiveSort5 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, this.lblIncentiveType15.Text);
            if (incentiveSort5 != null)
            {
                this.txtIncentiveMoney5.Text = string.Format("{0:N2}", incentiveSort5.IncentiveMoney);
                this.txtSumIncentiveMoney5.Text = string.Format("{0:N2}", incentiveSort5.TotalIncentiveMoney);
            }

            Model.Manager_IncentiveSortB incentiveSort6 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, "通 报 批 评 （人/次）");
            if (incentiveSort6 != null)
            {
                this.txtIncentiveNumber1.Text = incentiveSort6.IncentiveNumber.ToString();
                this.txtSumIncentiveNumber1.Text = incentiveSort6.TotalIncentiveNumber.ToString();
            }

            Model.Manager_IncentiveSortB incentiveSort7 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(MonthReportId, "开 除 （人/次）");
            if (incentiveSort7 != null)
            {
                this.txtIncentiveNumber2.Text = incentiveSort7.IncentiveNumber.ToString();
                this.txtSumIncentiveNumber2.Text = incentiveSort7.TotalIncentiveNumber.ToString();
            }
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换单位名称
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object unitId)
        {
            if (unitId != null)
            {
                return BLL.UnitService.GetUnitNameByUnitId(unitId.ToString());
            }
            return "";
        }

        /// <summary>
        /// 转换项目代码
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectCode(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(monthReport.ProjectId);
                    if (project != null)
                    {
                        return project.ProjectCode;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 转换项目名称
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectName(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    return BLL.ProjectService.GetProjectNameByProjectId(monthReport.ProjectId);
                }
            }
            return "";
        }

        /// <summary>
        /// 转换项目经理
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectManagerName(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId && x.RoleId == BLL.Const.ProjectManager);
                    if (m != null)
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                        if (user != null)
                        {
                            return user.UserName;
                        }
                    }
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

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("管理月报TCC" + filename, System.Text.Encoding.UTF8) + ".xls");
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

        #region 多表头
        /// <summary>
        /// 多表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvHSECostSort_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:

                    //总表头
                    TableCellCollection tcHeader = e.Row.Cells;
                    tcHeader.Clear();
                    //第一行
                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[0].Attributes.Add("rowspan", "3");
                    tcHeader[0].Text = "公司名称";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[1].Attributes.Add("rowspan", "3");
                    tcHeader[1].Text = "安全生产费计划额（总额）";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[2].Attributes.Add("rowspan", "3");
                    tcHeader[2].Text = "文明施工措施费计划额（总额）";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[3].Attributes.Add("colspan", "6");
                    tcHeader[3].Text = "实际支出</th></tr><tr>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[4].Attributes.Add("colspan", "2");
                    tcHeader[4].Text = "A-安全生产合计";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[5].Attributes.Add("colspan", "2");
                    tcHeader[5].Text = "B-文明施工合计";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[6].Attributes.Add("colspan", "2");
                    tcHeader[6].Text = "A+B合计</th></tr><tr>";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[7].Text = "当期";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[8].Text = "项目累计";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[9].Text = "当期";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[10].Text = "项目累计";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[11].Text = "当期";

                    tcHeader.Add(new TableHeaderCell());
                    tcHeader[12].Text = "项目累计";

                    break;
            }
        }
        #endregion
    }
}