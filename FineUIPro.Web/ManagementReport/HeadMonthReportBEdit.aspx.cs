using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ManagementReport
{
    public partial class HeadMonthReportBEdit : PageBase
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

        private static DateTime startTime;

        private static DateTime endTime;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.MonthReportId = Request.Params["HeadMonthReportId"];
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_HeadMonthReportB headMonthReport = BLL.HeadMonthReportBService.GetHeadMonthReportByHeadMonthReportId(this.MonthReportId);
                    if (headMonthReport != null)
                    {
                        this.txtMonthReportCode.Text = headMonthReport.MonthReportCode;
                        this.txtReportUnitName.Text = headMonthReport.ReportUnitName;
                        this.txtReportMan.Text = headMonthReport.ReportMan;
                        if (headMonthReport.ReportDate != null)
                        {
                            this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", headMonthReport.ReportDate);
                        }
                        if (headMonthReport.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", headMonthReport.Months);
                        }
                        startTime = Convert.ToDateTime(headMonthReport.Months);
                        endTime = startTime.AddMonths(1);
                        this.txtCheckMan.Text = headMonthReport.CheckMan;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.Params["months"]))
                    {
                        this.txtMonths.Text = string.Format("{0:yyyy-MM}", Convert.ToDateTime(Request.Params["months"]));
                    }
                    startTime = Convert.ToDateTime(Request.Params["months"]);
                    endTime = startTime.AddMonths(1);
                    List<Model.Base_Unit> units = BLL.UnitService.GetThisUnitDropDownList();
                    if (units.Count > 0)
                    {
                        this.txtReportUnitName.Text = units[0].UnitName;
                    }
                    this.txtReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtReportMan.Text = this.CurrUser.UserName;
                }
                // 获取项目当月的月报
                List<Model.Manager_MonthReportB> monthReports = new List<Model.Manager_MonthReportB>();//BLL.MonthReportService.GetMonthReportsByMonths(startTime);

                // 获取项目最近的月报
                List<Model.Manager_MonthReportB> lastMonthReports = new List<Model.Manager_MonthReportB>();
                List<Model.Base_Project> projects = BLL.ProjectService.GetAllProjectDropDownList();

                foreach (Model.Base_Project project in projects)
                {
                    Model.Manager_MonthReportB x = BLL.MonthReportBService.GetMonthReportsByMonthsAndProjectId(startTime, project.ProjectId);
                    if (x != null)
                    {
                        monthReports.Add(x);
                    }

                    else
                    {
                        Model.Manager_MonthReportB n = BLL.MonthReportBService.GetLateMonthReportByMonths(startTime, project.ProjectId);
                        if (n != null)
                        {
                            lastMonthReports.Add(n);
                        }
                    }
                }
                GetMonth(monthReports, lastMonthReports);
            }
        }
        #endregion

        #region 项目状态发生变化
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjectStation(this.rblState.SelectedValue);
        }
        #endregion

        #region 获取各项目月报汇总
        /// <summary>
        /// 获取各项目月报汇总
        /// </summary>
        /// <param name="monthReports"></param>
        /// <param name="lastReports"></param>
        private void GetMonth(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            GetProjectStation(this.rblState.SelectedValue);
            GetAccidentSort(monthReports, lastReports);
            GetTrainSort(monthReports, lastReports);
            GetMeetingSort(monthReports, lastReports);
            GetCheckSort(monthReports, lastReports);
            GetIncentiveSort(monthReports, lastReports);
        }

        private bool IsShow(string projectId)
        {
            bool b = false;
            if (this.rblState.SelectedValue == "1")   //施工中
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportsByMonthsAndProjectId(startTime, projectId);
                if (monthReport != null)
                {
                    b = true;
                }
                else    //项目当月未生成月报且完工，但有数据 
                {
                    Model.Base_ProjectSate projectSate = (from x in Funs.DB.Base_ProjectSate
                                                          where x.ProjectId == projectId
                                                          orderby x.CompileDate descending
                                                          select x).FirstOrDefault();
                    if (projectSate != null)
                    {
                        if (projectSate.CompileDate >= startTime && projectSate.CompileDate < endTime)
                        {
                            b = true;
                        }
                    }
                }
            }
            return b;
        }

        #region 获取各项目情况
        /// <summary>
        /// 获取各项目情况
        /// </summary>
        private void GetProjectStation(string state)
        {
            int sumManhours = 0, sumTotalManhours = 0, sumHseManhours = 0, sumTotalHseManhours = 0, sumTotalManNum = 0,
                sumAccidentNum = 0, sumAccidentTotalNum = 0, sumLoseHours = 0, sumTotalLoseHours = 0, sumHazardNum = 0,
                sumIsArgumentHazardNum = 0, allManhours = 0, allTotalManhours = 0, allHseManhours = 0, allTotalHseManhours = 0, allTotalManNum = 0,
                allAccidentNum = 0, allAccidentTotalNum = 0, allLoseHours = 0, allTotalLoseHours = 0, allHazardNum = 0,
                allIsArgumentHazardNum = 0;
            decimal sumAvgA = 0, sumAvgB = 0, sumAvgC = 0, sumPlanCostA = 0, sumPlanCostB = 0, sumRealCostA = 0, sumRealCostYA = 0,
                sumRealCostPA = 0, sumRealCostB = 0, sumRealCostYB = 0, sumRealCostPB = 0, sumRealCostAB = 0, sumRealCostYAB = 0, sumRealCostPAB = 0,
                allAvgA = 0, allAvgB = 0, allAvgC = 0, allPlanCostA = 0, allPlanCostB = 0, allRealCostA = 0, allRealCostYA = 0,
                allRealCostPA = 0, allRealCostB = 0, allRealCostYB = 0, allRealCostPB = 0, allRealCostAB = 0, allRealCostYAB = 0, allRealCostPAB = 0;
            List<Model.HeadMonthReportProjectStationItem> items = new List<Model.HeadMonthReportProjectStationItem>();
            List<Model.Base_Project> allProjects = BLL.ProjectService.GetAllProjectDropDownList();
            List<Model.Base_Project> projects = new List<Model.Base_Project>();
            if (state == "0")   //全部
            {
                projects = BLL.ProjectService.GetAllProjectDropDownList();
            }
            else
            {
                projects = BLL.ProjectService.GetProjectDropDownListByState(this.rblState.SelectedValue);
            }
            //获取当月所有项目发生损失工时事故的最后时间
            Model.Accident_AccidentReport maxAccident = BLL.AccidentReport2Service.GetMaxAccidentTimeReportByTime(startTime, endTime);
            DateTime? maxAccidentTime = null;
            if (maxAccident != null)
            {
                maxAccidentTime = maxAccident.AccidentDate;
            }
            allProjects = allProjects.OrderBy(x => x.ProjectCode).ToList();
            projects = projects.OrderBy(x => x.ProjectCode).ToList();
            Model.Manager_HeadMonthReportB LastHeadMonthReport = BLL.HeadMonthReportBService.GetLastHeadMonthReportByMonths(startTime);
            if (maxAccidentTime == null)
            {
                if (LastHeadMonthReport != null)
                {
                    if (LastHeadMonthReport.AllSumTotalHseManhours != null)
                    {
                        sumTotalHseManhours = LastHeadMonthReport.AllSumTotalHseManhours ?? 0;    //上月月报累计值不为空时，累计值初始为上月累计值
                        allTotalHseManhours = LastHeadMonthReport.AllSumTotalHseManhours ?? 0;
                    }
                }
            }
            List<string> projectIds = projects.Select(x => x.ProjectId).ToList();
            List<string> allProjectIds = allProjects.Select(x => x.ProjectId).ToList();
            foreach (Model.Base_Project project in allProjects)
            {
                List<Model.TC_CostStatisticDetail> costStatisticDetails = null;
                // 当月的月报
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportsByMonthsAndProjectId(startTime, project.ProjectId);
                // 最近的月报
                Model.Manager_MonthReportB lastMonthReport = BLL.MonthReportBService.GetLateMonthReportByMonths(startTime, project.ProjectId);
                //if (monthReport != null || lastMonthReport != null)
                //{
                if (projectIds.Contains(project.ProjectId) || IsShow(project.ProjectId))   //选中状态的项目
                {
                    Model.HeadMonthReportProjectStationItem item = new Model.HeadMonthReportProjectStationItem
                    {
                        ProjectCode = project.ProjectCode,
                        ProjectName = project.ProjectName,
                        ProjectManager = BLL.ProjectService.GetProjectManagerName(project.ProjectId)
                    };
                    if (!string.IsNullOrEmpty(project.ProjectType))
                    {
                        Model.Sys_Const c = BLL.ConstValue.drpConstItemList(ConstValue.Group_ProjectType).FirstOrDefault(x => x.ConstValue == project.ProjectType);
                        if (c != null)
                        {
                            item.ProjectType = c.ConstText;
                        }
                    }
                    item.StartDate = project.StartDate;
                    item.EndDate = project.EndDate;
                    if (monthReport != null)
                    {
                        item.Manhours = (monthReport.Manhours ?? 0).ToString();
                        sumManhours += monthReport.Manhours ?? 0;
                        allManhours += monthReport.Manhours ?? 0;
                        item.TotalManhours = (monthReport.TotalManhours ?? 0).ToString();
                        sumTotalManhours += monthReport.TotalManhours ?? 0;
                        allTotalManhours += monthReport.TotalManhours ?? 0;
                        item.HseManhours = (monthReport.HseManhours ?? 0).ToString();
                        item.TotalHseManhours = (monthReport.TotalHseManhours ?? 0).ToString();
                        if (maxAccidentTime != null)  //当月发生事故
                        {
                            int hseManhours = 0;
                            List<Model.SitePerson_DayReport> newDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(Convert.ToDateTime(maxAccidentTime).AddDays(1), endTime, project.ProjectId);
                            if (newDayReports.Count > 0)
                            {
                                foreach (var dayReport in newDayReports)
                                {
                                    hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                }
                            }
                            sumHseManhours += hseManhours;
                            allHseManhours += hseManhours;
                            sumTotalHseManhours += hseManhours;
                            allTotalHseManhours += hseManhours;
                        }
                        else
                        {
                            sumHseManhours += monthReport.HseManhours ?? 0;
                            allHseManhours += monthReport.HseManhours ?? 0;
                            sumTotalHseManhours += monthReport.HseManhours ?? 0;
                            allTotalHseManhours += monthReport.HseManhours ?? 0;
                        }
                        item.TotalManNum = (monthReport.TotalManNum ?? 0).ToString();
                        sumTotalManNum += monthReport.TotalManNum ?? 0;
                        allTotalManNum += monthReport.TotalManNum ?? 0;
                        var q = BLL.AccidentSortBService.GetAccidentSortsByMonthReportId(monthReport.MonthReportId);
                        item.AccidentNum = (from x in q select x.Number ?? 0).Sum().ToString();
                        sumAccidentNum += Funs.GetNewIntOrZero(item.AccidentNum);
                        allAccidentNum += Funs.GetNewIntOrZero(item.AccidentNum);
                        item.AccidentTotalNum = (from x in q select x.TotalNum ?? 0).Sum().ToString();
                        sumAccidentTotalNum += Funs.GetNewIntOrZero(item.AccidentTotalNum);
                        allAccidentTotalNum += Funs.GetNewIntOrZero(item.AccidentTotalNum);
                        item.LoseHours = (from x in q select x.LoseHours ?? 0).Sum().ToString();
                        sumLoseHours += Funs.GetNewIntOrZero(item.LoseHours);
                        allLoseHours += Funs.GetNewIntOrZero(item.LoseHours);
                        item.TotalLoseHours = (from x in q select x.TotalLoseHours ?? 0).Sum().ToString();
                        sumTotalLoseHours += Funs.GetNewIntOrZero(item.TotalLoseHours);
                        allTotalLoseHours += Funs.GetNewIntOrZero(item.TotalLoseHours);
                        item.AvgA = (monthReport.AccidentRateA ?? 0).ToString();
                        item.AvgB = (monthReport.AccidentRateB ?? 0).ToString();
                        item.AvgC = (monthReport.AccidentRateC ?? 0).ToString();
                        item.HazardNum = (monthReport.TotalLargerHazardNun ?? 0).ToString();
                        sumHazardNum += monthReport.TotalLargerHazardNun ?? 0;
                        allHazardNum += monthReport.TotalLargerHazardNun ?? 0;
                        item.IsArgumentHazardNum = (monthReport.TotalIsArgumentLargerHazardNun ?? 0).ToString();
                        sumIsArgumentHazardNum += monthReport.TotalIsArgumentLargerHazardNun ?? 0;
                        allIsArgumentHazardNum += monthReport.TotalIsArgumentLargerHazardNun ?? 0;
                        DateTime yearStartTime = Convert.ToDateTime(startTime.Year + "-01" + "-01");
                        List<Model.Manager_HseCostB> yearCosts = (from x in Funs.DB.Manager_HseCostB
                                                                  join y in Funs.DB.Manager_MonthReportB
                                                                  on x.MonthReportId equals y.MonthReportId
                                                                  where y.ProjectId == project.ProjectId && y.Months.Value.Year == yearStartTime.Year
                                                                  select x).ToList();
                        List<Model.Manager_HseCostB> monthCosts = BLL.HseCostBService.GetHseCostsByMonthReportId(monthReport.MonthReportId);
                        item.PlanCostA = monthCosts.Sum(x => x.PlanCostA ?? 0).ToString();
                        sumPlanCostA += Funs.GetNewDecimalOrZero(item.PlanCostA);
                        allPlanCostA += Funs.GetNewDecimalOrZero(item.PlanCostA);
                        item.PlanCostB = monthCosts.Sum(x => x.PlanCostB ?? 0).ToString();
                        sumPlanCostB += Funs.GetNewDecimalOrZero(item.PlanCostB);
                        allPlanCostB += Funs.GetNewDecimalOrZero(item.PlanCostB);
                        Model.TC_CostStatistic costStatistic = BLL.CostStatisticService.GetCostStatisticByMonthsAndProjectId(Convert.ToDateTime(monthReport.Months), project.ProjectId);
                        if (costStatistic != null)
                        {
                            costStatisticDetails = BLL.CostStatisticDetailService.GetCostStatisticDetailsByCostStatisticCode(costStatistic.CostStatisticCode);
                            if (costStatisticDetails.Count > 0)
                            {
                                item.RealCostA = (costStatisticDetails.Sum(x => x.A) ?? 0).ToString();
                                sumRealCostA += Funs.GetNewDecimalOrZero(item.RealCostA);
                                allRealCostA += Funs.GetNewDecimalOrZero(item.RealCostA);
                                item.RealCostYA = (costStatisticDetails.Sum(x => x.YA) ?? 0).ToString();
                                sumRealCostYA += Funs.GetNewDecimalOrZero(item.RealCostYA);
                                allRealCostYA += Funs.GetNewDecimalOrZero(item.RealCostYA);
                                item.RealCostPA = (costStatisticDetails.Sum(x => x.PA) ?? 0).ToString();
                                sumRealCostPA += Funs.GetNewDecimalOrZero(item.RealCostPA);
                                allRealCostPA += Funs.GetNewDecimalOrZero(item.RealCostPA);
                                item.RealCostB = (costStatisticDetails.Sum(x => x.B) ?? 0).ToString();
                                sumRealCostB += Funs.GetNewDecimalOrZero(item.RealCostB);
                                allRealCostB += Funs.GetNewDecimalOrZero(item.RealCostB);
                                item.RealCostYB = (costStatisticDetails.Sum(x => x.YB) ?? 0).ToString();
                                sumRealCostYB += Funs.GetNewDecimalOrZero(item.RealCostYB);
                                allRealCostYB += Funs.GetNewDecimalOrZero(item.RealCostYB);
                                item.RealCostPB = (costStatisticDetails.Sum(x => x.PB) ?? 0).ToString();
                                sumRealCostPB += Funs.GetNewDecimalOrZero(item.RealCostPB);
                                allRealCostPB += Funs.GetNewDecimalOrZero(item.RealCostPB);
                                item.RealCostAB = (costStatisticDetails.Sum(x => x.AB) ?? 0).ToString();
                                sumRealCostAB += Funs.GetNewDecimalOrZero(item.RealCostAB);
                                allRealCostAB += Funs.GetNewDecimalOrZero(item.RealCostAB);
                                item.RealCostYAB = (costStatisticDetails.Sum(x => x.YAB) ?? 0).ToString();
                                sumRealCostYAB += Funs.GetNewDecimalOrZero(item.RealCostYAB);
                                allRealCostYAB += Funs.GetNewDecimalOrZero(item.RealCostYAB);
                                item.RealCostPAB = (costStatisticDetails.Sum(x => x.PAB) ?? 0).ToString();
                                sumRealCostPAB += Funs.GetNewDecimalOrZero(item.RealCostPAB);
                                allRealCostPAB += Funs.GetNewDecimalOrZero(item.RealCostPAB);
                            }
                            else
                            {
                                item.RealCostA = "0";
                                item.RealCostYA = "0";
                                item.RealCostPA = "0";
                                item.RealCostB = "0";
                                item.RealCostYB = "0";
                                item.RealCostPB = "0";
                                item.RealCostAB = "0";
                                item.RealCostYAB = "0";
                                item.RealCostPAB = "0";
                            }
                        }
                        //item.RealCostA = monthCosts.Sum(x => x.RealCostA ?? 0).ToString();
                        //sumRealCostA += Funs.GetNewDecimal(item.RealCostA);
                        //item.RealCostYA = yearCosts.Sum(x => x.RealCostA ?? 0).ToString();
                        //sumRealCostYA += Funs.GetNewDecimal(item.RealCostYA);
                        //item.RealCostPA = monthCosts.Sum(x => x.ProjectRealCostA ?? 0).ToString();
                        //sumRealCostPA += Funs.GetNewDecimal(item.RealCostPA);
                        //item.RealCostB = monthCosts.Sum(x => x.RealCostB ?? 0).ToString();
                        //sumRealCostB += Funs.GetNewDecimal(item.RealCostB);
                        //item.RealCostYB = yearCosts.Sum(x => x.RealCostB ?? 0).ToString();
                        //sumRealCostYB += Funs.GetNewDecimal(item.RealCostYB);
                        //item.RealCostPB = monthCosts.Sum(x => x.ProjectRealCostB ?? 0).ToString();
                        //sumRealCostPB += Funs.GetNewDecimal(item.RealCostPB);
                        //item.RealCostAB = monthCosts.Sum(x => x.RealCostAB ?? 0).ToString();
                        //sumRealCostAB += Funs.GetNewDecimal(item.RealCostAB);
                        //item.RealCostYAB = yearCosts.Sum(x => x.RealCostAB ?? 0).ToString();
                        //sumRealCostYAB += Funs.GetNewDecimal(item.RealCostYAB);
                        //item.RealCostPAB = monthCosts.Sum(x => x.ProjectRealCostAB ?? 0).ToString();
                        //sumRealCostPAB += Funs.GetNewDecimal(item.RealCostPAB);
                    }
                    else
                    {
                        if (lastMonthReport != null)
                        {
                            item.Manhours = "0";
                            item.TotalManhours = (lastMonthReport.TotalManhours ?? 0).ToString();
                            sumTotalManhours += lastMonthReport.TotalManhours ?? 0;
                            allTotalManhours += lastMonthReport.TotalManhours ?? 0;
                            item.HseManhours = "0";
                            item.TotalHseManhours = (lastMonthReport.TotalHseManhours ?? 0).ToString();
                            if (maxAccidentTime != null)  //当月发生事故
                            {
                                int hseManhours = 0;
                                List<Model.SitePerson_DayReport> newDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(Convert.ToDateTime(maxAccidentTime).AddDays(1), endTime, project.ProjectId);
                                if (newDayReports.Count > 0)
                                {
                                    foreach (var dayReport in newDayReports)
                                    {
                                        hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                    }
                                }
                                sumTotalHseManhours += hseManhours;
                                allTotalHseManhours += hseManhours;
                            }
                            item.TotalManNum = "0";
                            item.AccidentNum = "0";
                            var q = BLL.AccidentSortBService.GetAccidentSortsByMonthReportId(lastMonthReport.MonthReportId);
                            item.AccidentTotalNum = (from x in q select x.TotalNum ?? 0).Sum().ToString();
                            sumAccidentTotalNum += Funs.GetNewIntOrZero(item.AccidentTotalNum);
                            allAccidentTotalNum += Funs.GetNewIntOrZero(item.AccidentTotalNum);
                            item.LoseHours = "0";
                            sumLoseHours += Funs.GetNewIntOrZero(item.LoseHours);
                            allLoseHours += Funs.GetNewIntOrZero(item.LoseHours);
                            item.TotalLoseHours = (from x in q select x.TotalLoseHours ?? 0).Sum().ToString();
                            sumTotalLoseHours += Funs.GetNewIntOrZero(item.TotalLoseHours);
                            allTotalLoseHours += Funs.GetNewIntOrZero(item.TotalLoseHours);
                            item.AvgA = "0";
                            item.AvgB = "0";
                            item.AvgC = "0";
                            item.HazardNum = (lastMonthReport.TotalLargerHazardNun ?? 0).ToString();
                            sumHazardNum += lastMonthReport.TotalLargerHazardNun ?? 0;
                            allHazardNum += lastMonthReport.TotalLargerHazardNun ?? 0;
                            item.IsArgumentHazardNum = (lastMonthReport.TotalIsArgumentLargerHazardNun ?? 0).ToString();
                            sumIsArgumentHazardNum += lastMonthReport.TotalIsArgumentLargerHazardNun ?? 0;
                            allIsArgumentHazardNum += lastMonthReport.TotalIsArgumentLargerHazardNun ?? 0;
                            DateTime yearStartTime = Convert.ToDateTime(startTime.Year + "-01" + "-01");
                            List<Model.Manager_HseCostB> yearCosts = (from x in Funs.DB.Manager_HseCostB
                                                                      join y in Funs.DB.Manager_MonthReportB
                                                                      on x.MonthReportId equals y.MonthReportId
                                                                      where y.ProjectId == project.ProjectId && y.Months.Value.Year == yearStartTime.Year
                                                                      select x).ToList();
                            List<Model.Manager_HseCostB> monthCosts = BLL.HseCostBService.GetHseCostsByMonthReportId(lastMonthReport.MonthReportId);
                            item.PlanCostA = monthCosts.Sum(x => x.PlanCostA ?? 0).ToString();
                            sumPlanCostA += Funs.GetNewDecimalOrZero(item.PlanCostA);
                            allPlanCostA += Funs.GetNewDecimalOrZero(item.PlanCostA);
                            item.PlanCostB = monthCosts.Sum(x => x.PlanCostB ?? 0).ToString();
                            sumPlanCostB += Funs.GetNewDecimalOrZero(item.PlanCostB);
                            allPlanCostB += Funs.GetNewDecimalOrZero(item.PlanCostB);
                            Model.TC_CostStatistic costStatistic = BLL.CostStatisticService.GetCostStatisticByMonthsAndProjectId(Convert.ToDateTime(lastMonthReport.Months), project.ProjectId);
                            if (costStatistic != null)
                            {
                                costStatisticDetails = BLL.CostStatisticDetailService.GetCostStatisticDetailsByCostStatisticCode(costStatistic.CostStatisticCode);
                                if (costStatisticDetails.Count > 0)
                                {
                                    item.RealCostA = (costStatisticDetails.Sum(x => x.A) ?? 0).ToString();
                                    sumRealCostA += Funs.GetNewDecimalOrZero(item.RealCostA);
                                    allRealCostA += Funs.GetNewDecimalOrZero(item.RealCostA);
                                    item.RealCostYA = (costStatisticDetails.Sum(x => x.YA) ?? 0).ToString();
                                    sumRealCostYA += Funs.GetNewDecimalOrZero(item.RealCostYA);
                                    allRealCostYA += Funs.GetNewDecimalOrZero(item.RealCostYA);
                                    item.RealCostPA = (costStatisticDetails.Sum(x => x.PA) ?? 0).ToString();
                                    sumRealCostPA += Funs.GetNewDecimalOrZero(item.RealCostPA);
                                    allRealCostPA += Funs.GetNewDecimalOrZero(item.RealCostPA);
                                    item.RealCostB = (costStatisticDetails.Sum(x => x.B) ?? 0).ToString();
                                    sumRealCostB += Funs.GetNewDecimalOrZero(item.RealCostB);
                                    allRealCostB += Funs.GetNewDecimalOrZero(item.RealCostB);
                                    item.RealCostYB = (costStatisticDetails.Sum(x => x.YB) ?? 0).ToString();
                                    sumRealCostYB += Funs.GetNewDecimalOrZero(item.RealCostYB);
                                    allRealCostYB += Funs.GetNewDecimalOrZero(item.RealCostYB);
                                    item.RealCostPB = (costStatisticDetails.Sum(x => x.PB) ?? 0).ToString();
                                    sumRealCostPB += Funs.GetNewDecimalOrZero(item.RealCostPB);
                                    allRealCostPB += Funs.GetNewDecimalOrZero(item.RealCostPB);
                                    item.RealCostAB = (costStatisticDetails.Sum(x => x.AB) ?? 0).ToString();
                                    sumRealCostAB += Funs.GetNewDecimalOrZero(item.RealCostAB);
                                    allRealCostAB += Funs.GetNewDecimalOrZero(item.RealCostAB);
                                    item.RealCostYAB = (costStatisticDetails.Sum(x => x.YAB) ?? 0).ToString();
                                    sumRealCostYAB += Funs.GetNewDecimalOrZero(item.RealCostYAB);
                                    allRealCostYAB += Funs.GetNewDecimalOrZero(item.RealCostYAB);
                                    item.RealCostPAB = (costStatisticDetails.Sum(x => x.PAB) ?? 0).ToString();
                                    sumRealCostPAB += Funs.GetNewDecimalOrZero(item.RealCostPAB);
                                    allRealCostPAB += Funs.GetNewDecimalOrZero(item.RealCostPAB);
                                }
                                else
                                {
                                    item.RealCostA = "0";
                                    item.RealCostYA = "0";
                                    item.RealCostPA = "0";
                                    item.RealCostB = "0";
                                    item.RealCostYB = "0";
                                    item.RealCostPB = "0";
                                    item.RealCostAB = "0";
                                    item.RealCostYAB = "0";
                                    item.RealCostPAB = "0";
                                }
                            }
                            else
                            {
                                item.RealCostA = "0";
                                item.RealCostYA = "0";
                                item.RealCostPA = "0";
                                item.RealCostB = "0";
                                item.RealCostYB = "0";
                                item.RealCostPB = "0";
                                item.RealCostAB = "0";
                                item.RealCostYAB = "0";
                                item.RealCostPAB = "0";
                            }
                            //item.RealCostYA = yearCosts.Sum(x => x.RealCostA ?? 0).ToString();
                            //sumRealCostYA += Funs.GetNewDecimal(item.RealCostYA);
                            //item.RealCostPA = monthCosts.Sum(x => x.ProjectRealCostA ?? 0).ToString();
                            //sumRealCostPA += Funs.GetNewDecimal(item.RealCostPA);
                            //item.RealCostYB = yearCosts.Sum(x => x.RealCostB ?? 0).ToString();
                            //sumRealCostYB += Funs.GetNewDecimal(item.RealCostYB);
                            //item.RealCostPB = monthCosts.Sum(x => x.ProjectRealCostB ?? 0).ToString();
                            //sumRealCostPB += Funs.GetNewDecimal(item.RealCostPB);
                            //item.RealCostYAB = yearCosts.Sum(x => x.RealCostAB ?? 0).ToString();
                            //sumRealCostYAB += Funs.GetNewDecimal(item.RealCostYAB);
                            //item.RealCostPAB = monthCosts.Sum(x => x.ProjectRealCostAB ?? 0).ToString();
                            //sumRealCostPAB += Funs.GetNewDecimal(item.RealCostPAB);
                        }
                    }
                    items.Add(item);
                }
                else
                {
                    if (monthReport != null)
                    {
                        allManhours += monthReport.Manhours ?? 0;
                        allTotalManhours += monthReport.TotalManhours ?? 0;
                        if (maxAccidentTime != null)  //当月发生事故
                        {
                            int hseManhours = 0;
                            List<Model.SitePerson_DayReport> newDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(Convert.ToDateTime(maxAccidentTime).AddDays(1), endTime, project.ProjectId);
                            if (newDayReports.Count > 0)
                            {
                                foreach (var dayReport in newDayReports)
                                {
                                    hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                }
                            }
                            allHseManhours += hseManhours;
                            allTotalHseManhours += hseManhours;
                        }
                        else
                        {
                            allHseManhours += monthReport.HseManhours ?? 0;
                            allTotalHseManhours += monthReport.HseManhours ?? 0;
                        }
                        allTotalManNum += monthReport.TotalManNum ?? 0;
                        allAccidentNum += monthReport.AccidentNum ?? 0;
                        var q = BLL.AccidentSortBService.GetAccidentSortsByMonthReportId(monthReport.MonthReportId);
                        allAccidentTotalNum += (from x in q select x.TotalNum ?? 0).Sum();
                        allLoseHours += (from x in q select x.LoseHours ?? 0).Sum();
                        allTotalLoseHours += (from x in q select x.TotalLoseHours ?? 0).Sum();
                        allHazardNum += monthReport.TotalLargerHazardNun ?? 0;
                        allIsArgumentHazardNum += monthReport.TotalIsArgumentLargerHazardNun ?? 0;
                        DateTime yearStartTime = Convert.ToDateTime(startTime.Year + "-01" + "-01");
                        List<Model.Manager_HseCostB> yearCosts = (from x in Funs.DB.Manager_HseCostB
                                                                  join y in Funs.DB.Manager_MonthReportB
                                                                  on x.MonthReportId equals y.MonthReportId
                                                                  where y.ProjectId == project.ProjectId && y.Months.Value.Year == yearStartTime.Year
                                                                  select x).ToList();
                        List<Model.Manager_HseCostB> monthCosts = BLL.HseCostBService.GetHseCostsByMonthReportId(monthReport.MonthReportId);
                        allPlanCostA += monthCosts.Sum(x => x.PlanCostA ?? 0);
                        allPlanCostB += monthCosts.Sum(x => x.PlanCostB ?? 0);
                        Model.TC_CostStatistic costStatistic = BLL.CostStatisticService.GetCostStatisticByMonthsAndProjectId(Convert.ToDateTime(monthReport.Months), project.ProjectId);
                        if (costStatistic != null)
                        {
                            costStatisticDetails = BLL.CostStatisticDetailService.GetCostStatisticDetailsByCostStatisticCode(costStatistic.CostStatisticCode);
                            if (costStatisticDetails.Count > 0)
                            {
                                allRealCostA += costStatisticDetails.Sum(x => x.A) ?? 0;
                                allRealCostYA += costStatisticDetails.Sum(x => x.YA) ?? 0;
                                allRealCostPA += costStatisticDetails.Sum(x => x.PA) ?? 0;
                                allRealCostB += costStatisticDetails.Sum(x => x.B) ?? 0;
                                allRealCostYB += costStatisticDetails.Sum(x => x.YB) ?? 0;
                                allRealCostPB += costStatisticDetails.Sum(x => x.PB) ?? 0;
                                allRealCostAB += costStatisticDetails.Sum(x => x.AB) ?? 0;
                                allRealCostYAB += costStatisticDetails.Sum(x => x.YAB) ?? 0;
                                allRealCostPAB += costStatisticDetails.Sum(x => x.PAB) ?? 0;
                            }
                        }
                    }
                    else
                    {
                        if (lastMonthReport != null)
                        {
                            allTotalManhours += lastMonthReport.TotalManhours ?? 0;
                            if (maxAccidentTime != null)  //当月发生事故
                            {
                                int hseManhours = 0;
                                List<Model.SitePerson_DayReport> newDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(Convert.ToDateTime(maxAccidentTime).AddDays(1), endTime, project.ProjectId);
                                if (newDayReports.Count > 0)
                                {
                                    foreach (var dayReport in newDayReports)
                                    {
                                        hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                    }
                                }
                                allTotalHseManhours += hseManhours;
                            }
                            var q = BLL.AccidentSortBService.GetAccidentSortsByMonthReportId(lastMonthReport.MonthReportId);
                            allAccidentTotalNum += (from x in q select x.TotalNum ?? 0).Sum();
                            allTotalLoseHours += (from x in q select x.TotalLoseHours ?? 0).Sum();
                            allHazardNum += lastMonthReport.TotalLargerHazardNun ?? 0;
                            allIsArgumentHazardNum += lastMonthReport.TotalIsArgumentLargerHazardNun ?? 0;
                            DateTime yearStartTime = Convert.ToDateTime(startTime.Year + "-01" + "-01");
                            List<Model.Manager_HseCostB> yearCosts = (from x in Funs.DB.Manager_HseCostB
                                                                      join y in Funs.DB.Manager_MonthReportB
                                                                      on x.MonthReportId equals y.MonthReportId
                                                                      where y.ProjectId == project.ProjectId && y.Months.Value.Year == yearStartTime.Year
                                                                      select x).ToList();
                            List<Model.Manager_HseCostB> monthCosts = BLL.HseCostBService.GetHseCostsByMonthReportId(lastMonthReport.MonthReportId);
                            allPlanCostA += monthCosts.Sum(x => x.PlanCostA ?? 0);
                            allPlanCostB += monthCosts.Sum(x => x.PlanCostB ?? 0);
                            Model.TC_CostStatistic costStatistic = BLL.CostStatisticService.GetCostStatisticByMonthsAndProjectId(Convert.ToDateTime(lastMonthReport.Months), project.ProjectId);
                            if (costStatistic != null)
                            {
                                costStatisticDetails = BLL.CostStatisticDetailService.GetCostStatisticDetailsByCostStatisticCode(costStatistic.CostStatisticCode);
                                if (costStatisticDetails.Count > 0)
                                {
                                    allRealCostA += costStatisticDetails.Sum(x => x.A) ?? 0;
                                    allRealCostYA += costStatisticDetails.Sum(x => x.YA) ?? 0;
                                    allRealCostPA += costStatisticDetails.Sum(x => x.PA) ?? 0;
                                    allRealCostB += costStatisticDetails.Sum(x => x.B) ?? 0;
                                    allRealCostYB += costStatisticDetails.Sum(x => x.YB) ?? 0;
                                    allRealCostPB += costStatisticDetails.Sum(x => x.PB) ?? 0;
                                    allRealCostAB += costStatisticDetails.Sum(x => x.AB) ?? 0;
                                    allRealCostYAB += costStatisticDetails.Sum(x => x.YAB) ?? 0;
                                    allRealCostPAB += costStatisticDetails.Sum(x => x.PAB) ?? 0;
                                }
                            }
                        }
                    }
                }
                //}
            }

            var q1 = (from x in Funs.DB.Manager_AccidentSortB
                      join y in Funs.DB.Manager_MonthReportB on x.MonthReportId equals y.MonthReportId
                      where projectIds.Contains(y.ProjectId) && (x.AccidentType.Contains("死 亡 事 故") || x.AccidentType.Contains("轻 伤 事 故") || x.AccidentType.Contains("重 伤 事 故") || x.AccidentType.Contains("医 院 处 置") || x.AccidentType.Contains("工 作 受 限 事 故"))
                      && y.Months <= startTime
                      select x).Distinct().ToList();
            decimal a = (from x in q1 select x.TotalNum ?? 0).Sum();
            if (sumTotalManhours != 0)
            {
                sumAvgA = decimal.Round(Convert.ToDecimal(a * 1000000 / Convert.ToInt32(sumTotalManhours)), 2);
            }
            var q2 = (from x in Funs.DB.Manager_AccidentSortB
                      join y in Funs.DB.Manager_MonthReportB on x.MonthReportId equals y.MonthReportId
                      where projectIds.Contains(y.ProjectId) && (x.AccidentType.Contains("死 亡 事 故") || x.AccidentType.Contains("轻 伤 事 故") || x.AccidentType.Contains("重 伤 事 故"))
                      && y.Months <= startTime
                      select x).Distinct().ToList();
            decimal b = (from x in q2 select x.TotalNum ?? 0).Sum();
            if (sumTotalManhours != 0)
            {
                sumAvgB = decimal.Round(Convert.ToDecimal(b * 1000000 / Convert.ToInt32(sumTotalManhours)), 2);
            }
            decimal c1 = (from x in q2 select x.TotalLoseHours ?? 0).Sum();
            if (sumTotalManhours != 0)
            {
                sumAvgC = decimal.Round(Convert.ToDecimal(c1 * 1000000 / Convert.ToDecimal(sumTotalManhours)), 2);
            }

            var aq1 = (from x in Funs.DB.Manager_AccidentSortB
                       join y in Funs.DB.Manager_MonthReportB on x.MonthReportId equals y.MonthReportId
                       where allProjectIds.Contains(y.ProjectId) && (x.AccidentType.Contains("死 亡 事 故") || x.AccidentType.Contains("轻 伤 事 故") || x.AccidentType.Contains("重 伤 事 故") || x.AccidentType.Contains("医 院 处 置") || x.AccidentType.Contains("工 作 受 限 事 故"))
                       && y.Months <= startTime
                       select x).Distinct().ToList();
            decimal aa = (from x in aq1 select x.TotalNum ?? 0).Sum();
            if (allTotalManhours != 0)
            {
                allAvgA = decimal.Round(Convert.ToDecimal(aa * 1000000 / Convert.ToInt32(allTotalManhours)), 2);
            }
            var aq2 = (from x in Funs.DB.Manager_AccidentSortB
                       join y in Funs.DB.Manager_MonthReportB on x.MonthReportId equals y.MonthReportId
                       where allProjectIds.Contains(y.ProjectId) && (x.AccidentType.Contains("死 亡 事 故") || x.AccidentType.Contains("轻 伤 事 故") || x.AccidentType.Contains("重 伤 事 故"))
                      && y.Months <= startTime
                       select x).Distinct().ToList();
            decimal bb = (from x in aq2 select x.TotalNum ?? 0).Sum();
            if (allTotalManhours != 0)
            {
                allAvgB = decimal.Round(Convert.ToDecimal(bb * 1000000 / Convert.ToInt32(allTotalManhours)), 2);
            }
            decimal cc1 = (from x in aq2 select x.TotalLoseHours ?? 0).Sum();
            if (allTotalManhours != 0)
            {
                allAvgC = decimal.Round(Convert.ToDecimal(cc1 * 1000000 / Convert.ToDecimal(allTotalManhours)), 2);
            }
            Model.HeadMonthReportProjectStationItem totalItem = new Model.HeadMonthReportProjectStationItem
            {
                ProjectCode = "_",
                ProjectName = "当前类型项目合计：",
                Manhours = sumManhours.ToString(),
                TotalManhours = sumTotalManhours.ToString(),
                HseManhours = sumHseManhours.ToString(),
                TotalHseManhours = sumTotalHseManhours.ToString(),
                TotalManNum = sumTotalManNum.ToString(),
                AccidentNum = sumAccidentNum.ToString(),
                AccidentTotalNum = sumAccidentTotalNum.ToString(),
                LoseHours = sumLoseHours.ToString(),
                TotalLoseHours = sumTotalLoseHours.ToString(),
                AvgA = sumAvgA.ToString(),
                AvgB = sumAvgB.ToString(),
                AvgC = sumAvgC.ToString(),
                HazardNum = sumHazardNum.ToString(),
                IsArgumentHazardNum = sumIsArgumentHazardNum.ToString(),
                PlanCostA = sumPlanCostA.ToString(),
                PlanCostB = sumPlanCostB.ToString(),
                RealCostA = sumRealCostA.ToString(),
                RealCostYA = sumRealCostYA.ToString(),
                RealCostPA = sumRealCostPA.ToString(),
                RealCostB = sumRealCostB.ToString(),
                RealCostYB = sumRealCostYB.ToString(),
                RealCostPB = sumRealCostPB.ToString(),
                RealCostAB = sumRealCostAB.ToString(),
                RealCostYAB = sumRealCostYAB.ToString(),
                RealCostPAB = sumRealCostPAB.ToString()
            };
            items.Add(totalItem);
            this.GridProjectStation.DataSource = items;
            this.GridProjectStation.DataBind();
            Model.Manager_HeadMonthReportB headMonthReport = BLL.HeadMonthReportBService.GetHeadMonthReportByMonths(startTime);
            if (headMonthReport != null && !string.IsNullOrEmpty(headMonthReport.CheckMan))
            {
                allHseManhours = headMonthReport.AllSumHseManhours ?? 0;
                allTotalHseManhours = headMonthReport.AllSumTotalHseManhours ?? 0;
            }
            if (this.GridProjectStation.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("ProjectName", "公司所有项目合计：");
                summary.Add("Manhours", allManhours);
                summary.Add("TotalManhours", allTotalManhours);
                summary.Add("HseManhours", allHseManhours);
                summary.Add("TotalHseManhours", allTotalHseManhours);
                summary.Add("TotalManNum", allTotalManNum);
                summary.Add("AccidentNum", allAccidentNum);
                summary.Add("AccidentTotalNum", allAccidentTotalNum);
                summary.Add("LoseHours", allLoseHours);
                summary.Add("TotalLoseHours", allTotalLoseHours);
                summary.Add("AvgA", allAvgA);
                summary.Add("AvgB", allAvgB);
                summary.Add("AvgC", allAvgC);
                summary.Add("HazardNum", allHazardNum);
                summary.Add("IsArgumentHazardNum", allIsArgumentHazardNum);
                summary.Add("PlanCostA", allPlanCostA);
                summary.Add("PlanCostB", allPlanCostB);
                summary.Add("RealCostA", allRealCostA);
                summary.Add("RealCostYA", allRealCostYA);
                summary.Add("RealCostPA", allRealCostPA);
                summary.Add("RealCostB", allRealCostB);
                summary.Add("RealCostYB", allRealCostYB);
                summary.Add("RealCostPB", allRealCostPB);
                summary.Add("RealCostAB", allRealCostAB);
                summary.Add("RealCostYAB", allRealCostYAB);
                summary.Add("RealCostPAB", allRealCostPAB);
                this.GridProjectStation.SummaryData = summary;
            }
            else
            {
                this.GridProjectStation.SummaryData = null;
            }
        }
        #endregion

        #region 获取事故情况
        /// <summary>
        /// 获取事故情况
        /// </summary>
        private void GetAccidentSort(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            List<string> ids = monthReports.Select(x => x.MonthReportId).ToList();
            List<string> lastIds = lastReports.Select(x => x.MonthReportId).ToList();
            List<Model.Manager_AccidentSortB> accidentSorts1 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType11.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts1 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType11.Text);
            this.txtNumber11.Text = accidentSorts1.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber11.Text = (accidentSorts1.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts1.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum11.Text = accidentSorts1.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum11.Text = (accidentSorts1.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts1.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours11.Text = accidentSorts1.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours11.Text = (accidentSorts1.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts1.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney11.Text = accidentSorts1.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney11.Text = (accidentSorts1.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts1.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts2 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType12.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts2 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType12.Text);
            this.txtNumber12.Text = accidentSorts2.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber12.Text = (accidentSorts2.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts2.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum12.Text = accidentSorts2.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum12.Text = (accidentSorts2.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts2.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours12.Text = accidentSorts2.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours12.Text = (accidentSorts2.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts2.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney12.Text = accidentSorts2.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney12.Text = (accidentSorts2.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts2.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts3 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType13.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts3 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType13.Text);
            this.txtNumber13.Text = accidentSorts3.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber13.Text = (accidentSorts3.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts3.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum13.Text = accidentSorts3.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum13.Text = (accidentSorts3.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts3.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours13.Text = accidentSorts3.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours13.Text = (accidentSorts3.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts3.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney13.Text = accidentSorts3.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney13.Text = (accidentSorts3.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts3.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts4 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType14.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts4 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType14.Text);
            this.txtNumber14.Text = accidentSorts4.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber14.Text = (accidentSorts4.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts4.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum14.Text = accidentSorts4.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum14.Text = (accidentSorts4.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts4.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours14.Text = accidentSorts4.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours14.Text = (accidentSorts4.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts4.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney14.Text = accidentSorts4.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney14.Text = (accidentSorts4.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts4.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts5 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType15.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts5 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType15.Text);
            this.txtNumber15.Text = accidentSorts5.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber15.Text = (accidentSorts5.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts5.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum15.Text = accidentSorts5.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum15.Text = (accidentSorts5.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts5.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours15.Text = accidentSorts5.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours15.Text = (accidentSorts5.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts5.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney15.Text = accidentSorts5.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney15.Text = (accidentSorts5.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts5.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts6 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType16.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts6 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType16.Text);
            this.txtNumber16.Text = accidentSorts6.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber16.Text = (accidentSorts6.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts6.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum16.Text = accidentSorts6.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum16.Text = (accidentSorts6.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts6.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours16.Text = accidentSorts6.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours16.Text = (accidentSorts6.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts6.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney16.Text = accidentSorts6.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney16.Text = (accidentSorts6.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts6.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts21 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType21.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts21 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType21.Text);
            this.txtNumber21.Text = accidentSorts21.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber21.Text = (accidentSorts21.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts21.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum21.Text = accidentSorts21.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum21.Text = (accidentSorts21.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts21.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours21.Text = accidentSorts21.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours21.Text = (accidentSorts21.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts21.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney21.Text = accidentSorts21.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney21.Text = (accidentSorts21.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts21.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts22 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType22.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts22 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType22.Text);
            this.txtNumber22.Text = accidentSorts22.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber22.Text = (accidentSorts22.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts22.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum22.Text = accidentSorts22.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum22.Text = (accidentSorts22.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts22.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours22.Text = accidentSorts22.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours22.Text = (accidentSorts22.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts22.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney22.Text = accidentSorts22.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney22.Text = (accidentSorts22.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts22.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts23 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType23.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts23 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType23.Text);
            this.txtNumber23.Text = accidentSorts23.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber23.Text = (accidentSorts23.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts23.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum23.Text = accidentSorts23.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum23.Text = (accidentSorts23.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts23.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours23.Text = accidentSorts23.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours23.Text = (accidentSorts23.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts23.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney23.Text = accidentSorts23.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney23.Text = (accidentSorts23.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts23.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts24 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType24.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts24 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType24.Text);
            this.txtNumber24.Text = accidentSorts24.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber24.Text = (accidentSorts24.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts24.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum24.Text = accidentSorts24.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum24.Text = (accidentSorts24.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts24.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours24.Text = accidentSorts24.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours24.Text = (accidentSorts24.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts24.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney24.Text = accidentSorts24.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney24.Text = (accidentSorts24.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts24.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts25 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType25.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts25 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType25.Text);
            this.txtNumber25.Text = accidentSorts25.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber25.Text = (accidentSorts25.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts25.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum25.Text = accidentSorts25.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum25.Text = (accidentSorts25.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts25.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours25.Text = accidentSorts25.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours25.Text = (accidentSorts25.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts25.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney25.Text = accidentSorts25.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney25.Text = (accidentSorts25.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts25.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts26 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType26.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts26 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType26.Text);
            this.txtNumber26.Text = accidentSorts26.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber26.Text = (accidentSorts26.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts26.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum26.Text = accidentSorts26.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum26.Text = (accidentSorts26.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts26.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours26.Text = accidentSorts26.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours26.Text = (accidentSorts26.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts26.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney26.Text = accidentSorts26.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney26.Text = (accidentSorts26.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts26.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts27 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType27.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts27 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType27.Text);
            this.txtNumber27.Text = accidentSorts27.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber27.Text = (accidentSorts27.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts27.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum27.Text = accidentSorts27.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum27.Text = (accidentSorts27.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts27.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours27.Text = accidentSorts27.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours27.Text = (accidentSorts27.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts27.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney27.Text = accidentSorts27.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney27.Text = (accidentSorts27.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts27.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts28 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType28.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts28 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType28.Text);
            this.txtNumber28.Text = accidentSorts28.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber28.Text = (accidentSorts28.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts28.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum28.Text = accidentSorts28.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum28.Text = (accidentSorts28.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts28.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours28.Text = accidentSorts28.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours28.Text = (accidentSorts28.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts28.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney28.Text = accidentSorts28.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney28.Text = (accidentSorts28.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts28.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            List<Model.Manager_AccidentSortB> accidentSorts29 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(ids, this.lblAccidentType29.Text);
            List<Model.Manager_AccidentSortB> lastAccidentSorts29 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdsAndAccidentType(lastIds, this.lblAccidentType29.Text);
            this.txtNumber29.Text = accidentSorts29.Sum(x => x.Number ?? 0).ToString();
            this.txtSumNumber29.Text = (accidentSorts29.Sum(x => x.TotalNum ?? 0) + lastAccidentSorts29.Sum(x => x.TotalNum ?? 0)).ToString();
            this.txtPersonNum29.Text = accidentSorts29.Sum(x => x.PersonNum ?? 0).ToString();
            this.txtSumPersonNum29.Text = (accidentSorts29.Sum(x => x.TotalPersonNum ?? 0) + lastAccidentSorts29.Sum(x => x.TotalPersonNum ?? 0)).ToString();
            this.txtLoseHours29.Text = accidentSorts29.Sum(x => x.LoseHours ?? 0).ToString();
            this.txtSumLoseHours29.Text = (accidentSorts29.Sum(x => x.TotalLoseHours ?? 0) + lastAccidentSorts29.Sum(x => x.TotalLoseHours ?? 0)).ToString();
            this.txtLoseMoney29.Text = accidentSorts29.Sum(x => x.LoseMoney ?? 0).ToString();
            this.txtSumLoseMoney29.Text = (accidentSorts29.Sum(x => x.TotalLoseMoney ?? 0) + lastAccidentSorts29.Sum(x => x.TotalLoseMoney ?? 0)).ToString();

            //事故数据
            List<Model.Accident_AccidentReport> accidentReports = (from x in Funs.DB.Accident_AccidentReport where x.AccidentDate >= startTime && x.AccidentDate < endTime select x).ToList();
            List<Model.Accident_AccidentReportOther> accidentReportOthers = (from x in Funs.DB.Accident_AccidentReportOther where x.AccidentDate >= startTime && x.AccidentDate < endTime select x).ToList();
            this.txtAccidentNum.Text = ((from x in accidentReports where x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3" select x).Count()
                + (from x in accidentReportOthers where x.AccidentTypeId == "1" || x.AccidentTypeId == "2" select x).Count()).ToString();
            int sumManhours = 0;
            if (this.GridProjectStation.SummaryData != null)
            {
                sumManhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("TotalManhours").ToString());
            }
            //百万工时总可记录事故率：当期的医院处置、工作受限事故、轻伤、重伤、死亡事故总次数*一百万/当期累计总工时数
            int countA = (from x in accidentReports where x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3" select x).Count()
                + (from x in accidentReportOthers where x.AccidentTypeId == "1" || x.AccidentTypeId == "2" select x).Count();
            if (sumManhours != 0)
            {
                this.txtAccidentRateA.Text = decimal.Round((Convert.ToDecimal(countA) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateA.Text = "0";
            }
            //百万工时损失工时率：当期总损失工时数*一百万/当期累计总工时数
            decimal countB = (from x in accidentReports select x.WorkingHoursLoss ?? 0).Sum()
                + (from x in accidentReportOthers select x.WorkingHoursLoss ?? 0).Sum();
            if (sumManhours != 0)
            {
                this.txtAccidentRateB.Text = decimal.Round((Convert.ToDecimal(countB) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateB.Text = "0";
            }
            //百万工时损失工时伤害事故率：当期的轻伤、重伤、死亡事故总次数*一百万/当期累计总工时数
            int countC = (from x in accidentReports where x.AccidentTypeId == "1" || x.AccidentTypeId == "2" || x.AccidentTypeId == "3" select x).Count();
            if (sumManhours != 0)
            {
                this.txtAccidentRateC.Text = decimal.Round((Convert.ToDecimal(countC) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateC.Text = "0";
            }
            //百万工时死亡事故频率：当期死亡事故总次数*一百万/当期累计总工时数
            int countD = (from x in accidentReports where x.AccidentTypeId == "1" select x).Count();
            if (sumManhours != 0)
            {
                this.txtAccidentRateD.Text = decimal.Round((Convert.ToDecimal(countD) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateD.Text = "0";
            }
            //百万工时事故死亡率：当期死亡总人数*一百万/当期累计总工时数
            int countE = (from x in accidentReports where x.AccidentTypeId == "1" select x.PeopleNum ?? 0).Sum();
            if (sumManhours != 0)
            {
                this.txtAccidentRateE.Text = decimal.Round((Convert.ToDecimal(countD) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateE.Text = "0";
            }

            List<Model.Manager_AccidentDetailSortB> details = new List<Model.Manager_AccidentDetailSortB>();
            foreach (var item in monthReports)
            {
                details.AddRange(BLL.AccidentDetailSortBService.GetAccidentDetailSortsByMonthReportId(item.MonthReportId));
            }
            this.GridAccidentDetailSort.DataSource = details;
            this.GridAccidentDetailSort.DataBind();
        }
        #endregion

        #region 获取培训情况
        /// <summary>
        /// 获取培训情况
        /// </summary>
        private void GetTrainSort(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            int sumTrainNumber = 0;
            int sumTotalTrainNum = 0;
            int sumTrainPersonNumber = 0;
            int sumTotalTrainPersonNum = 0;
            var trainTypes = BLL.TrainTypeService.GetTrainTypeList();
            List<Model.Manager_TrainSortB> totalTrainSorts = new List<Model.Manager_TrainSortB>();
            List<Model.Manager_TrainSortB> trainSorts = new List<Model.Manager_TrainSortB>();
            List<Model.Manager_TrainSortB> lastTrainSorts = new List<Model.Manager_TrainSortB>();
            foreach (var item in monthReports)
            {
                trainSorts.AddRange(BLL.TrainSortBService.GetTrainSortsByMonthReportId(item.MonthReportId));
            }
            foreach (var item in lastReports)
            {
                lastTrainSorts.AddRange(BLL.TrainSortBService.GetTrainSortsByMonthReportId(item.MonthReportId));
            }
            foreach (var item in trainTypes)
            {
                Model.Manager_TrainSortB trainSort = new Model.Manager_TrainSortB
                {
                    TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort)),
                    TrainType = item.TrainTypeName,
                    TrainNumber = trainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TrainNumber ?? 0),
                    TotalTrainNum = trainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TotalTrainNum ?? 0) + lastTrainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TotalTrainNum ?? 0),
                    TrainPersonNumber = trainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TrainPersonNumber ?? 0),
                    TotalTrainPersonNum = trainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TotalTrainPersonNum ?? 0) + lastTrainSorts.Where(x => x.TrainType == item.TrainTypeName).Sum(x => x.TotalTrainPersonNum ?? 0)
                };
                totalTrainSorts.Add(trainSort);
                sumTrainNumber += trainSort.TrainNumber ?? 0;
                sumTotalTrainNum += trainSort.TotalTrainNum ?? 0;
                sumTrainPersonNumber += trainSort.TrainPersonNumber ?? 0;
                sumTotalTrainPersonNum += trainSort.TotalTrainPersonNum ?? 0;
            }
            this.GridTrainSort.DataSource = totalTrainSorts;
            this.GridTrainSort.DataBind();
            if (this.GridTrainSort.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("TrainType", "合计：");
                summary.Add("TrainNumber", sumTrainNumber);
                summary.Add("TotalTrainNum", sumTotalTrainNum);
                summary.Add("TrainPersonNumber", sumTrainPersonNumber);
                summary.Add("TotalTrainPersonNum", sumTotalTrainPersonNum);
                this.GridTrainSort.SummaryData = summary;
            }
            else
            {
                this.GridTrainSort.SummaryData = null;
            }
        }
        #endregion

        #region 获取会议情况
        /// <summary>
        /// 获取会议情况
        /// </summary>
        private void GetMeetingSort(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            List<Model.Manager_MeetingSortB> meetingSorts1 = new List<Model.Manager_MeetingSortB>();
            List<Model.Manager_MeetingSortB> meetingSorts2 = new List<Model.Manager_MeetingSortB>();
            List<Model.Manager_MeetingSortB> meetingSorts3 = new List<Model.Manager_MeetingSortB>();
            List<Model.Manager_MeetingSortB> lastMeetingSorts1 = new List<Model.Manager_MeetingSortB>();
            List<Model.Manager_MeetingSortB> lastMeetingSorts2 = new List<Model.Manager_MeetingSortB>();
            List<Model.Manager_MeetingSortB> lastMeetingSorts3 = new List<Model.Manager_MeetingSortB>();
            foreach (var item in monthReports)
            {
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType1.Text) != null)
                {
                    meetingSorts1.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType1.Text));
                }
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType2.Text) != null)
                {
                    meetingSorts2.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType2.Text));
                }
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType3.Text) != null)
                {
                    meetingSorts3.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType3.Text));
                }
            }
            foreach (var item in lastReports)
            {
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType1.Text) != null)
                {
                    lastMeetingSorts1.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType1.Text));
                }
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType2.Text) != null)
                {
                    lastMeetingSorts2.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType2.Text));
                }
                if (BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType3.Text) != null)
                {
                    lastMeetingSorts3.Add(BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(item.MonthReportId, this.lblMeetingType3.Text));
                }
            }

            this.txtMeetingNumber1.Text = meetingSorts1.Sum(x => x.MeetingNumber ?? 0).ToString();
            this.txtSumMeetingNumber1.Text = (meetingSorts1.Sum(x => x.TotalMeetingNum ?? 0) + lastMeetingSorts1.Sum(x => x.TotalMeetingNum ?? 0)).ToString();
            this.txtMeetingPersonNumber1.Text = meetingSorts1.Sum(x => x.MeetingPersonNumber ?? 0).ToString();
            this.txtSumMeetingPersonNumber1.Text = (meetingSorts1.Sum(x => x.TotalMeetingPersonNum ?? 0) + lastMeetingSorts1.Sum(x => x.TotalMeetingPersonNum ?? 0)).ToString();

            this.txtMeetingNumber2.Text = meetingSorts2.Sum(x => x.MeetingNumber ?? 0).ToString();
            this.txtSumMeetingNumber2.Text = (meetingSorts2.Sum(x => x.TotalMeetingNum ?? 0) + lastMeetingSorts2.Sum(x => x.TotalMeetingNum ?? 0)).ToString();
            this.txtMeetingPersonNumber2.Text = meetingSorts2.Sum(x => x.MeetingPersonNumber ?? 0).ToString();
            this.txtSumMeetingPersonNumber2.Text = (meetingSorts2.Sum(x => x.TotalMeetingPersonNum ?? 0) + lastMeetingSorts2.Sum(x => x.TotalMeetingPersonNum ?? 0)).ToString();

            this.txtMeetingNumber3.Text = meetingSorts3.Sum(x => x.MeetingNumber ?? 0).ToString();
            this.txtSumMeetingNumber3.Text = (meetingSorts3.Sum(x => x.TotalMeetingNum ?? 0) + lastMeetingSorts3.Sum(x => x.TotalMeetingNum ?? 0)).ToString();
            this.txtMeetingPersonNumber3.Text = meetingSorts3.Sum(x => x.MeetingPersonNumber ?? 0).ToString();
            this.txtSumMeetingPersonNumber3.Text = (meetingSorts3.Sum(x => x.TotalMeetingPersonNum ?? 0) + lastMeetingSorts3.Sum(x => x.TotalMeetingPersonNum ?? 0)).ToString();

            this.txtAllMeetingNumber.Text = (Convert.ToInt32(this.txtMeetingNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber3.Text.Trim())).ToString();
            this.txtAllMeetingPersonNumber.Text = (Convert.ToInt32(this.txtMeetingPersonNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingNumber.Text = (Convert.ToInt32(this.txtSumMeetingNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingPersonNumber.Text = (Convert.ToInt32(this.txtSumMeetingPersonNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingPersonNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingPersonNumber3.Text.Trim())).ToString();
        }
        #endregion

        #region 获取检查情况
        /// <summary>
        /// 获取检查情况
        /// </summary>
        private void GetCheckSort(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            List<Model.Manager_CheckSortB> checkSorts1 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> checkSorts2 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> checkSorts3 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> checkSorts4 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> lastCheckSorts1 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> lastCheckSorts2 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> lastCheckSorts3 = new List<Model.Manager_CheckSortB>();
            List<Model.Manager_CheckSortB> lastCheckSorts4 = new List<Model.Manager_CheckSortB>();
            foreach (var item in monthReports)
            {
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType1.Text) != null)
                {
                    checkSorts1.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType1.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType2.Text) != null)
                {
                    checkSorts2.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType2.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType3.Text) != null)
                {
                    checkSorts3.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType3.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType4.Text) != null)
                {
                    checkSorts4.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType4.Text));
                }
            }
            foreach (var item in lastReports)
            {
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType1.Text) != null)
                {
                    lastCheckSorts1.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType1.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType2.Text) != null)
                {
                    lastCheckSorts2.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType2.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType3.Text) != null)
                {
                    lastCheckSorts3.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType3.Text));
                }
                if (BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType4.Text) != null)
                {
                    lastCheckSorts4.Add(BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(item.MonthReportId, this.lblCheckType4.Text));
                }
            }

            this.txtCheckNumber1.Text = checkSorts1.Sum(x => x.CheckNumber ?? 0).ToString();
            this.txtSumCheckNumber1.Text = (checkSorts1.Sum(x => x.TotalCheckNum ?? 0) + lastCheckSorts1.Sum(x => x.TotalCheckNum ?? 0)).ToString();
            this.txtViolationNumber1.Text = checkSorts1.Sum(x => x.ViolationNumber ?? 0).ToString();
            this.txtSumViolationNumber1.Text = (checkSorts1.Sum(x => x.TotalViolationNum ?? 0) + lastCheckSorts1.Sum(x => x.TotalViolationNum ?? 0)).ToString();

            this.txtCheckNumber2.Text = checkSorts2.Sum(x => x.CheckNumber ?? 0).ToString();
            this.txtSumCheckNumber2.Text = (checkSorts2.Sum(x => x.TotalCheckNum ?? 0) + lastCheckSorts2.Sum(x => x.TotalCheckNum ?? 0)).ToString();
            this.txtViolationNumber2.Text = checkSorts2.Sum(x => x.ViolationNumber ?? 0).ToString();
            this.txtSumViolationNumber2.Text = (checkSorts2.Sum(x => x.TotalViolationNum ?? 0) + lastCheckSorts2.Sum(x => x.TotalViolationNum ?? 0)).ToString();

            this.txtCheckNumber3.Text = checkSorts3.Sum(x => x.CheckNumber ?? 0).ToString();
            this.txtSumCheckNumber3.Text = (checkSorts3.Sum(x => x.TotalCheckNum ?? 0) + lastCheckSorts3.Sum(x => x.TotalCheckNum ?? 0)).ToString();
            this.txtViolationNumber3.Text = checkSorts3.Sum(x => x.ViolationNumber ?? 0).ToString();
            this.txtSumViolationNumber3.Text = (checkSorts3.Sum(x => x.TotalViolationNum ?? 0) + lastCheckSorts3.Sum(x => x.TotalViolationNum ?? 0)).ToString();

            this.txtCheckNumber4.Text = checkSorts4.Sum(x => x.CheckNumber ?? 0).ToString();
            this.txtSumCheckNumber4.Text = (checkSorts4.Sum(x => x.TotalCheckNum ?? 0) + lastCheckSorts4.Sum(x => x.TotalCheckNum ?? 0)).ToString();
            this.txtViolationNumber4.Text = checkSorts4.Sum(x => x.ViolationNumber ?? 0).ToString();
            this.txtSumViolationNumber4.Text = (checkSorts4.Sum(x => x.TotalViolationNum ?? 0) + lastCheckSorts4.Sum(x => x.TotalViolationNum ?? 0)).ToString();

            this.txtAllCheckNumber.Text = (Convert.ToInt32(this.txtCheckNumber1.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber2.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber3.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber4.Text.Trim())).ToString();
            this.txtAllSumCheckNumber.Text = (Convert.ToInt32(this.txtSumCheckNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber3.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber4.Text.Trim())).ToString();
            this.txtAllViolationNumber.Text = (Convert.ToInt32(this.txtViolationNumber1.Text.Trim()) + Convert.ToInt32(this.txtViolationNumber2.Text.Trim()) + Convert.ToInt32(this.txtViolationNumber3.Text.Trim()) + Convert.ToInt32(this.txtViolationNumber4.Text.Trim())).ToString();
            this.txtAllSumViolationNumber.Text = (Convert.ToInt32(this.txtSumViolationNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber3.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber4.Text.Trim())).ToString();
        }
        #endregion

        #region 获取奖惩情况
        /// <summary>
        /// 获取奖惩情况
        /// </summary>
        private void GetIncentiveSort(List<Model.Manager_MonthReportB> monthReports, List<Model.Manager_MonthReportB> lastReports)
        {
            List<Model.Manager_IncentiveSortB> incentiveSorts1 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts2 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts3 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts4 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts5 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts6 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> incentiveSorts7 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts1 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts2 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts3 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts4 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts5 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts6 = new List<Model.Manager_IncentiveSortB>();
            List<Model.Manager_IncentiveSortB> lastIncentiveSorts7 = new List<Model.Manager_IncentiveSortB>();
            foreach (var item in monthReports)
            {
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType11.Text) != null)
                {
                    incentiveSorts1.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType11.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType12.Text) != null)
                {
                    incentiveSorts2.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType12.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType13.Text) != null)
                {
                    incentiveSorts3.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType13.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType14.Text) != null)
                {
                    incentiveSorts4.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType14.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType15.Text) != null)
                {
                    incentiveSorts5.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType15.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "通 报 批 评 （人/次）") != null)
                {
                    incentiveSorts6.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "通 报 批 评 （人/次）"));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "开 除 （人/次）") != null)
                {
                    incentiveSorts7.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "开 除 （人/次）"));
                }
            }
            foreach (var item in lastReports)
            {
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType11.Text) != null)
                {
                    lastIncentiveSorts1.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType11.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType12.Text) != null)
                {
                    lastIncentiveSorts2.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType12.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType13.Text) != null)
                {
                    lastIncentiveSorts3.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType13.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType14.Text) != null)
                {
                    lastIncentiveSorts4.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType14.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType15.Text) != null)
                {
                    lastIncentiveSorts5.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, this.lblIncentiveType15.Text));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "通 报 批 评 （人/次）") != null)
                {
                    lastIncentiveSorts6.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "通 报 批 评 （人/次）"));
                }
                if (BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "开 除 （人/次）") != null)
                {
                    lastIncentiveSorts7.Add(BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(item.MonthReportId, "开 除 （人/次）"));
                }
            }

            this.txtIncentiveMoney1.Text = incentiveSorts1.Sum(x => x.IncentiveMoney ?? 0).ToString("N2");
            this.txtSumIncentiveMoney1.Text = (incentiveSorts1.Sum(x => x.TotalIncentiveMoney ?? 0) + lastIncentiveSorts1.Sum(x => x.TotalIncentiveMoney ?? 0)).ToString("N2");

            this.txtIncentiveMoney2.Text = incentiveSorts2.Sum(x => x.IncentiveMoney ?? 0).ToString("N2");
            this.txtSumIncentiveMoney2.Text = (incentiveSorts2.Sum(x => x.TotalIncentiveMoney ?? 0) + lastIncentiveSorts2.Sum(x => x.TotalIncentiveMoney ?? 0)).ToString("N2");

            this.txtIncentiveMoney3.Text = incentiveSorts3.Sum(x => x.IncentiveMoney ?? 0).ToString("N2");
            this.txtSumIncentiveMoney3.Text = (incentiveSorts3.Sum(x => x.TotalIncentiveMoney ?? 0) + lastIncentiveSorts3.Sum(x => x.TotalIncentiveMoney ?? 0)).ToString("N2");

            this.txtIncentiveMoney4.Text = incentiveSorts4.Sum(x => x.IncentiveMoney ?? 0).ToString("N2");
            this.txtSumIncentiveMoney4.Text = (incentiveSorts4.Sum(x => x.TotalIncentiveMoney ?? 0) + lastIncentiveSorts4.Sum(x => x.TotalIncentiveMoney ?? 0)).ToString("N2");

            this.txtIncentiveMoney5.Text = incentiveSorts5.Sum(x => x.IncentiveMoney ?? 0).ToString("N2");
            this.txtSumIncentiveMoney5.Text = (incentiveSorts5.Sum(x => x.TotalIncentiveMoney ?? 0) + lastIncentiveSorts5.Sum(x => x.TotalIncentiveMoney ?? 0)).ToString("N2");

            this.txtIncentiveNumber1.Text = incentiveSorts6.Sum(x => x.IncentiveNumber ?? 0).ToString();
            this.txtSumIncentiveNumber1.Text = (incentiveSorts6.Sum(x => x.TotalIncentiveNumber ?? 0) + lastIncentiveSorts6.Sum(x => x.TotalIncentiveNumber ?? 0)).ToString();

            this.txtIncentiveNumber2.Text = incentiveSorts7.Sum(x => x.IncentiveNumber ?? 0).ToString();
            this.txtSumIncentiveNumber2.Text = (incentiveSorts7.Sum(x => x.TotalIncentiveNumber ?? 0) + lastIncentiveSorts7.Sum(x => x.TotalIncentiveNumber ?? 0)).ToString();
        }
        #endregion

        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Manager_HeadMonthReportB headMonthReport = new Model.Manager_HeadMonthReportB
            {
                MonthReportCode = this.txtMonthReportCode.Text,
                Months = Convert.ToDateTime(this.txtMonths.Text + "-1"),
                ReportUnitName = this.txtReportUnitName.Text.Trim(),
                AllSumHseManhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("HseManhours").ToString()),
                AllSumTotalHseManhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("TotalHseManhours").ToString())
            };
            if (!string.IsNullOrEmpty(this.txtReportDate.Text.Trim()))
            {
                headMonthReport.ReportDate = Convert.ToDateTime(this.txtReportDate.Text.Trim());
            }
            else
            {
                headMonthReport.ReportDate = null;
            }
            headMonthReport.ReportMan = this.txtReportMan.Text.Trim();
            headMonthReport.CheckMan = this.txtCheckMan.Text.Trim();

            Model.Manager_CostAnalyse costAnalyse = new Model.Manager_CostAnalyse();
            //当月总费用
            decimal cost = Funs.GetNewDecimalOrZero(this.GridProjectStation.SummaryData.GetValue("RealCostAB").ToString());
            int manhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("Manhours").ToString());
            if (!string.IsNullOrEmpty(MonthReportId))
            {
                headMonthReport.HeadMonthReportId = MonthReportId;
                BLL.HeadMonthReportBService.UpdateHeadMonthReport(headMonthReport);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改本部管理月报", headMonthReport.HeadMonthReportId);
                costAnalyse = BLL.CostAnalyseService.getCostAnalyseByMonths(headMonthReport.Months, null);
                if (costAnalyse != null)
                {
                    if (manhours == 0)
                    {
                        costAnalyse.Analyse = 0;
                        costAnalyse.Manhours = 0;
                    }
                    else
                    {
                        costAnalyse.Analyse = cost / manhours;
                        costAnalyse.Manhours = manhours;
                    }
                    costAnalyse.TotalRealCostMoney = cost;
                    BLL.CostAnalyseService.UpdateCostAnalyse(costAnalyse);
                }
                else
                {
                    costAnalyse = new Model.Manager_CostAnalyse
                    {
                        Months = headMonthReport.Months
                    };
                    if (manhours == 0)
                    {
                        costAnalyse.Analyse = 0;
                        costAnalyse.Manhours = 0;
                    }
                    else
                    {
                        costAnalyse.Analyse = cost / manhours;
                        costAnalyse.Manhours = manhours;
                    }
                    costAnalyse.TotalRealCostMoney = cost;
                    BLL.CostAnalyseService.AddCostAnalyse(costAnalyse);
                }
            }
            else
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HeadMonthReportB));
                headMonthReport.HeadMonthReportId = newKeyID;
                BLL.HeadMonthReportBService.AddHeadMonthReport(headMonthReport);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "增加本部管理月报", headMonthReport.HeadMonthReportId);
                costAnalyse.Months = headMonthReport.Months;
                if (manhours == 0)
                {
                    costAnalyse.Analyse = 0;
                    costAnalyse.Manhours = 0;
                }
                else
                {
                    costAnalyse.Analyse = cost / manhours;
                    costAnalyse.Manhours = manhours;
                }
                costAnalyse.TotalRealCostMoney = cost;
                BLL.CostAnalyseService.AddCostAnalyse(costAnalyse);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MonthReportId))
            {
                Model.Manager_HeadMonthReportB headMonthReport = new Model.Manager_HeadMonthReportB
                {
                    Months = Convert.ToDateTime(this.txtMonths.Text + "-1"),
                    ReportUnitName = this.txtReportUnitName.Text.Trim(),
                    AllSumHseManhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("HseManhours").ToString()),
                    AllSumTotalHseManhours = Funs.GetNewIntOrZero(this.GridProjectStation.SummaryData.GetValue("TotalHseManhours").ToString())
                };
                if (!string.IsNullOrEmpty(this.txtReportDate.Text.Trim()))
                {
                    headMonthReport.ReportDate = Convert.ToDateTime(this.txtReportDate.Text.Trim());
                }
                else
                {
                    headMonthReport.ReportDate = null;
                }
                headMonthReport.ReportMan = this.txtReportMan.Text.Trim();
                headMonthReport.CheckMan = this.txtCheckMan.Text.Trim();

                //Model.TC_CostAnalyse costAnalyse = new Model.TC_CostAnalyse();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_HeadMonthReportB));
                headMonthReport.HeadMonthReportId = newKeyID;
                BLL.HeadMonthReportBService.AddHeadMonthReport(headMonthReport);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "增加本部管理月报", headMonthReport.HeadMonthReportId);
                //costAnalyse.Months = headMonthReport.Months;
                //if (allSumManhours == 0)
                //{
                //    costAnalyse.Analyse = 0;
                //    costAnalyse.Manhours = 0;
                //}
                //else
                //{
                //    costAnalyse.Analyse = allSumRealCostAB / allSumManhours;
                //    costAnalyse.Manhours = allSumManhours;
                //}
                //costAnalyse.TotalRealCostMoney = allSumRealCostAB;
                //BLL.CostAnalyseService.AddCostAnalyse(costAnalyse);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ServerMonthReportBMenuId)));
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
    }
}