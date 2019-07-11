using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportBEdit : PageBase
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
                var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
                //1.项目信息
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.txtProjectCode.Text = project.ProjectCode;
                    this.txtProjectName.Text = project.ProjectName;
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.ProjectId && x.RoleId == BLL.Const.ProjectManager);
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
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(MonthReportId);
                    if (monthReport != null)
                    {
                        this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        this.txtMonthReportCode.Text = monthReport.MonthReportCode;
                        if (monthReport.Months != null)
                        {
                            this.txtMonths.Text = string.Format("{0:yyyy-MM}", monthReport.Months);
                        }
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                        this.txtAccidentReview.Text = monthReport.AccidentReview;

                        this.txtHseActiveReview.Text = monthReport.HseActiveReview;
                        this.txtHseActiveKey.Text = monthReport.HseActiveKey;

                        startTime = Convert.ToDateTime(monthReport.Months);
                        endTime = startTime.AddMonths(1);
                        if (monthReport.NoEndDate != null)
                        {
                            this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.NoEndDate);
                        }
                        else
                        {
                            this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", endTime.AddDays(-1));
                        }
                    }
                }
                else
                {
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthBMenuId, this.ProjectId, this.CurrUser.UnitId);
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int day = DateTime.Now.Day;
                    if (day > (!string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 4))
                    {
                        this.txtMonths.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        startTime = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + "1");
                        endTime = startTime.AddMonths(1);
                        this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.Date);
                    }
                    else
                    {
                        if (month == 1)
                        {
                            this.txtMonths.Text = (DateTime.Now.Year - 1).ToString() + "-12"; ;
                        }
                        else
                        {
                            this.txtMonths.Text = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month - 1).ToString();
                        }

                        this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        startTime = Convert.ToDateTime(this.txtMonths.Text + "-" + "1");
                        endTime = startTime.AddMonths(1);
                        this.txtNoEndDate.Text = string.Format("{0:yyyy-MM-dd}", endTime.AddDays(-1));
                    }
                }
                int? sumTotalPanhours = 0;
                List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.CurrUser.LoginProjectId);
                if (dayReports.Count > 0)
                {
                    foreach (var dayReport in dayReports)
                    {
                        sumTotalPanhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                else
                {
                    sumTotalPanhours = 0;
                }
                this.txtManhours.Text = (sumTotalPanhours ?? 0).ToString("N0");
                if (sumTotalPanhours == null)
                {
                    sumTotalPanhours = 0;
                }
                // 冻结时间
                int freezeDay = !string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 5;
                Model.Manager_MonthReportB lastMonthReport = BLL.MonthReportBService.GetLastMonthReportByDate(Convert.ToDateTime(this.txtMonthReportDate.Text.Trim()), freezeDay, this.CurrUser.LoginProjectId);

                if (lastMonthReport != null)
                {
                    this.txtSumManhours.Text = (BLL.MonthReportBService.GetMonthReportByMonthReportId(lastMonthReport.MonthReportId).TotalManhours + sumTotalPanhours).Value.ToString("N0");
                    LastMonthReportId = lastMonthReport.MonthReportId;
                }
                else
                {
                    this.txtSumManhours.Text = (sumTotalPanhours ?? 0).ToString("N0");
                    LastMonthReportId = string.Empty;
                }
                //安全人工时 
                //轻重死（含待定）事故的最近时间
                Model.Accident_AccidentReport maxAccident = BLL.AccidentReport2Service.GetMaxAccidentTimeReport(endTime, this.CurrUser.LoginProjectId);
                DateTime? maxAccidentTime = null;
                if (maxAccident != null)
                {
                    maxAccidentTime = maxAccident.AccidentDate;
                }
                if (maxAccidentTime != null)
                {
                    DateTime newTime = Convert.ToDateTime(maxAccidentTime);
                    if (newTime.AddDays(1).Year > newTime.Year || newTime.AddDays(1).Month > newTime.Month)
                    {
                        this.txtHseManhours.Text = "0";
                        this.txtSumHseManhours.Text = "0";
                    }
                    else
                    {
                        if (startTime >= newTime)  //事故时间在月报开始时间之前
                        {
                            this.txtHseManhours.Text = this.txtManhours.Text.Trim();

                            if (lastMonthReport != null)
                            {
                                this.txtSumHseManhours.Text = (lastMonthReport.TotalHseManhours + sumTotalPanhours).Value.ToString("N0");
                            }
                            else
                            {
                                this.txtSumHseManhours.Text = (sumTotalPanhours ?? 0).ToString("N0");
                            }
                        }
                        else    //事故时间在月报开始时间之后
                        {
                            int? sumHseManhours2 = 0;
                            List<Model.SitePerson_DayReport> newDayReports2 = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(newTime.AddDays(1), endTime, this.CurrUser.LoginProjectId);
                            if (newDayReports2.Count > 0)
                            {
                                foreach (var dayReport in newDayReports2)
                                {
                                    sumHseManhours2 += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                                }
                            }
                            else
                            {
                                sumHseManhours2 = 0;
                            }
                            this.txtHseManhours.Text = (sumHseManhours2 ?? 0).ToString("N0");
                            this.txtSumHseManhours.Text = (sumHseManhours2 ?? 0).ToString("N0");
                        }
                        //Model.Manager_ResetManHours oldResetManHorus = BLL.ResetManHoursService.GetResetManHoursByAccidentReportId(maxAccident.AccidentReportId);
                        //if (oldResetManHorus == null)
                        //{
                        //    Model.Manager_ResetManHours newResetManHorus = new Model.Manager_ResetManHours();
                        //    newResetManHorus.ProjectId = this.CurrUser.LoginProjectId;
                        //    newResetManHorus.AccidentTypeId = maxAccident.AccidentTypeId;
                        //    newResetManHorus.Abstract = maxAccident.Abstract;
                        //    newResetManHorus.AccidentDate = maxAccident.AccidentDate;                  
                        //    //之前累计安全人工时指的是发生轻重死事故时，公司当时的累计安全人工时。
                        //    int sumHseManhours3 = 0;
                        //    Model.Manager_HeadMonthReportB headMonthReport = BLL.HeadMonthReportBService.GetLastHeadMonthReportByMonths(startTime);
                        //    if (headMonthReport != null)   
                        //    {
                        //        sumHseManhours3 = headMonthReport.AllSumTotalHseManhours ?? 0;   //上月公司月报累计安全人工时
                        //    }
                        //    List<Model.SitePerson_DayReport> newDayReports3 = (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= startTime && x.CompileDate < newTime && x.States == BLL.Const.State_2 select x).ToList();
                        //    if (newDayReports3.Count > 0)
                        //    {
                        //        foreach (var dayReport in newDayReports3)
                        //        {
                        //            sumHseManhours3 += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                        //        }
                        //    }
                        //    newResetManHorus.BeforeManHours = sumHseManhours3;
                        //    newResetManHorus.ProjectManager = BLL.ProjectService.GetProjectManagerName(this.CurrUser.LoginProjectId);
                        //    newResetManHorus.HSSEManager = BLL.ProjectService.GetHSSEManagerName(this.CurrUser.LoginProjectId);
                        //    newResetManHorus.AccidentReportId = maxAccident.AccidentReportId;
                        //    BLL.ResetManHoursService.AddResetManHours(newResetManHorus);
                        //}
                    }
                }
                else
                {
                    this.txtHseManhours.Text = this.txtManhours.Text.Trim();
                    this.txtSumHseManhours.Text = this.txtSumManhours.Text.Trim();
                }
                DateTime? noStartDate = BLL.AccidentReportOtherService.GetLastNoStartAccidentReportOther(this.CurrUser.LoginProjectId);
                if (noStartDate == null)
                {
                    if (maxAccidentTime != null)
                    {
                        noStartDate = maxAccidentTime;
                    }
                    else
                    {
                        noStartDate = project.StartDate;
                    }
                }
                else
                {
                    if (maxAccidentTime != null)
                    {
                        if (maxAccidentTime > noStartDate)
                        {
                            noStartDate = maxAccidentTime;
                        }
                    }
                }
                this.txtNoStartDate.Text = string.Format("{0:yyyy-MM-dd}", noStartDate);

                int? sumNewPanhours = 0;
                if (noStartDate == project.StartDate || startTime >= noStartDate)
                {
                    if (lastMonthReport != null)
                    {
                        int? safe = BLL.MonthReportBService.GetMonthReportByMonthReportId(lastMonthReport.MonthReportId).SafetyManhours;
                        this.txtSafetyManhours.Text = ((safe != null ? safe.Value : 0) + Convert.ToInt32(this.txtHseManhours.Text.Replace(",", ""))).ToString("N0");
                    }
                    else
                    {
                        this.txtSafetyManhours.Text = this.txtHseManhours.Text.Trim();
                    }
                }
                else if (startTime < noStartDate)
                {
                    List<Model.SitePerson_DayReport> newSafetyDayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, Convert.ToDateTime(noStartDate.Value.Date.AddDays(1)), this.CurrUser.LoginProjectId);
                    if (newSafetyDayReports.Count > 0)
                    {
                        foreach (var dayReport in newSafetyDayReports)
                        {
                            sumNewPanhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                        }
                    }
                    this.txtSafetyManhours.Text = (sumTotalPanhours - sumNewPanhours).Value.ToString("N0");
                }

                this.txtIsArgumentLargerHazardNun.Text = (from x in Funs.DB.Solution_LargerHazard where x.ProjectId == this.CurrUser.LoginProjectId && x.IsArgument == true && x.RecordTime >= startTime && x.RecordTime < endTime && x.States == BLL.Const.State_2 select x).Count().ToString();
                this.txtLargerHazardNum.Text = (from x in Funs.DB.Solution_LargerHazard where x.ProjectId == this.CurrUser.LoginProjectId && x.RecordTime >= startTime && x.RecordTime < endTime && x.States == BLL.Const.State_2 select x).Count().ToString();
                if (lastMonthReport != null)
                {
                    this.txtTotalIsArgumentLargerHazardNun.Text = ((lastMonthReport.TotalIsArgumentLargerHazardNun ?? 0) + Funs.GetNewIntOrZero(this.txtIsArgumentLargerHazardNun.Text.Trim())).ToString();
                    this.txtTotalLargerHazardNum.Text = ((lastMonthReport.TotalLargerHazardNun ?? 0) + Funs.GetNewIntOrZero(this.txtLargerHazardNum.Text.Trim())).ToString();
                }
                else
                {
                    this.txtTotalIsArgumentLargerHazardNun.Text = this.txtIsArgumentLargerHazardNun.Text.Trim();
                    this.txtTotalLargerHazardNum.Text = this.txtLargerHazardNum.Text.Trim();
                }
                GetMonth();
            }
        }

        /// <summary>
        /// 显示月报告
        /// </summary>
        private void GetMonth()
        {
            GetManhoursSort();
            GetAccidentSort();
            GetHseCost();
            GetTrainSort();
            GetMeetingSort();
            GetCheckSort();
            GetIncentiveSort();
        }

        #region 明细显示统计
        #region 3.项目施工现场人工时分类统计
        /// <summary>
        ///  3.项目施工现场人工时分类统计
        /// </summary>
        private void GetManhoursSort()
        {
            int sumPersonTotal = 0;
            int sumPersonWorkTimeTotal = 0;
            int sumTotalPersonWorkTime = 0;
            List<Model.Manager_ManhoursSortB> manhoursSortBs = new List<Model.Manager_ManhoursSortB>();
            //获取当期人工时日报
            List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.CurrUser.LoginProjectId);
            //获取单位集合
            var unitIds = (from x in Funs.DB.Project_ProjectUnit
                           where x.ProjectId == this.CurrUser.LoginProjectId && (x.UnitType == "1" || x.UnitType == "2")
                           select x.UnitId).ToList();
            foreach (var unitId in unitIds)
            {
                Model.Manager_ManhoursSortB manhoursSort = new Model.Manager_ManhoursSortB
                {
                    ManhoursSortId = SQLHelper.GetNewID(typeof(Model.Manager_ManhoursSortB)),
                    UnitId = unitId
                };
                //员工总数
                decimal personNum = (from x in dayReports
                                     join y in Funs.DB.SitePerson_DayReportDetail
                                  on x.DayReportId equals y.DayReportId
                                     where y.UnitId == unitId
                                     select y.RealPersonNum ?? 0).Sum();
                int count = BLL.SitePerson_DayReportService.GetDayReportsByCompileDateAndUnitId(startTime, endTime, this.CurrUser.LoginProjectId, unitId).Count();
                if (count > 0)
                {
                    decimal persontotal = Convert.ToDecimal(Math.Round(personNum / count, 2));
                    if (persontotal.ToString().IndexOf(".") > 0 && persontotal.ToString().Substring(persontotal.ToString().IndexOf("."), persontotal.ToString().Length - persontotal.ToString().IndexOf(".")) != ".00")
                    {
                        string personint = persontotal.ToString().Substring(0, persontotal.ToString().IndexOf("."));
                        manhoursSort.PersonTotal = Convert.ToInt32(personint) + 1;
                    }
                    else
                    {
                        manhoursSort.PersonTotal = Convert.ToInt32(persontotal);
                    }
                }
                //完成人工时（当月）
                decimal personWorkTimeTotal = (from x in dayReports
                                               join y in Funs.DB.SitePerson_DayReportDetail
                                            on x.DayReportId equals y.DayReportId
                                               where y.UnitId == unitId
                                               select y.PersonWorkTime ?? 0).Sum();
                manhoursSort.ManhoursTotal = Convert.ToInt32(personWorkTimeTotal);
                //完成人工时（累计）
                Model.Manager_ManhoursSortB latManhoursSort = BLL.ManhoursSortBService.GetSumManhoursTotalByMonthReportIdAndUnitId(LastMonthReportId, unitId);
                if (latManhoursSort != null)
                {
                    manhoursSort.TotalManhoursTotal = Convert.ToInt32(latManhoursSort.TotalManhoursTotal) + manhoursSort.ManhoursTotal ?? 0;
                }
                else
                {
                    manhoursSort.TotalManhoursTotal = manhoursSort.ManhoursTotal ?? 0;
                }
                //累计值
                sumPersonTotal += manhoursSort.PersonTotal ?? 0;
                sumPersonWorkTimeTotal += manhoursSort.ManhoursTotal ?? 0;
                sumTotalPersonWorkTime += manhoursSort.TotalManhoursTotal ?? 0;
                manhoursSortBs.Add(manhoursSort);
            }
            this.GridManhoursSort.DataSource = manhoursSortBs;
            this.GridManhoursSort.DataBind();
            if (this.GridManhoursSort.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("UnitId", "合计：");
                summary.Add("PersonTotal", sumPersonTotal);
                summary.Add("ManhoursTotal", sumPersonWorkTimeTotal);
                summary.Add("TotalManhoursTotal", sumTotalPersonWorkTime);
                this.GridManhoursSort.SummaryData = summary;
            }
            else
            {
                this.GridManhoursSort.SummaryData = null;
            }
        }
        #endregion

        #region 4.项目施工现场事故分类统计
        /// <summary>
        ///  4.项目施工现场事故分类统计
        /// </summary>
        private void GetAccidentSort()
        {
            string accidentType11 = "1";   //死亡事故
            Model.Manager_AccidentSortB accidentSort = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType11.Text);
            this.txtNumber11.Text = "0";
            this.txtNumber11.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber11.Text = accidentSort != null ? ((accidentSort.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber11.Text)).ToString() : Convert.ToInt32(this.txtNumber11.Text).ToString("N0");
            this.txtPersonNum11.Text = "0";
            this.txtPersonNum11.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum11.Text = accidentSort != null ? ((accidentSort.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum11.Text)).ToString() : Convert.ToInt32(this.txtPersonNum11.Text).ToString("N0");
            this.txtLoseHours11.Text = "0";
            this.txtLoseHours11.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours11.Text = accidentSort != null ? ((accidentSort.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours11.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours11.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney11.Text = "0";
            this.txtLoseMoney11.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney11.Text = accidentSort != null ? ((accidentSort.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney11.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney11.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType12 = "2";   //重伤事故
            Model.Manager_AccidentSortB accidentSort2 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType12.Text);
            this.txtNumber12.Text = "0";
            this.txtNumber12.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber12.Text = accidentSort2 != null ? ((accidentSort2.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber12.Text)).ToString() : Convert.ToInt32(this.txtNumber12.Text).ToString("N0");
            this.txtPersonNum12.Text = "0";
            this.txtPersonNum12.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum12.Text = accidentSort2 != null ? ((accidentSort2.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum12.Text)).ToString() : Convert.ToInt32(this.txtPersonNum12.Text).ToString("N0");
            this.txtLoseHours12.Text = "0";
            this.txtLoseHours12.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours12.Text = accidentSort2 != null ? ((accidentSort2.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours12.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours12.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney12.Text = "0";
            this.txtLoseMoney12.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney12.Text = accidentSort2 != null ? ((accidentSort2.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney12.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney12.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType13 = "3";  //轻伤事故
            Model.Manager_AccidentSortB accidentSort3 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType13.Text);
            this.txtNumber13.Text = "0";
            this.txtNumber13.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber13.Text = accidentSort3 != null ? ((accidentSort3.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber13.Text)).ToString() : Convert.ToInt32(this.txtNumber13.Text).ToString("N0");
            this.txtPersonNum13.Text = "0";
            this.txtPersonNum13.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum13.Text = accidentSort3 != null ? ((accidentSort3.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum13.Text)).ToString() : Convert.ToInt32(this.txtPersonNum13.Text).ToString("N0");
            this.txtLoseHours13.Text = "0";
            this.txtLoseHours13.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours13.Text = accidentSort3 != null ? ((accidentSort3.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours13.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours13.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney13.Text = "0";
            this.txtLoseMoney13.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney13.Text = accidentSort3 != null ? ((accidentSort3.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney13.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney13.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType14 = "1";   //工作受限事故
            Model.Manager_AccidentSortB accidentSort4 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType14.Text);
            this.txtNumber14.Text = "0";
            this.txtNumber14.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber14.Text = accidentSort4 != null ? ((accidentSort4.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber14.Text)).ToString() : Convert.ToInt32(this.txtNumber14.Text).ToString("N0");
            this.txtPersonNum14.Text = "0";
            this.txtPersonNum14.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum14.Text = accidentSort4 != null ? ((accidentSort4.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum14.Text)).ToString() : Convert.ToInt32(this.txtPersonNum14.Text).ToString("N0");
            this.txtLoseHours14.Text = "0";
            this.txtLoseHours14.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours14.Text = accidentSort4 != null ? ((accidentSort4.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours14.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours14.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney14.Text = "0";
            this.txtLoseMoney14.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney14.Text = accidentSort4 != null ? ((accidentSort4.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney14.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney14.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType15 = "2";  //医疗处理
            Model.Manager_AccidentSortB accidentSort5 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType15.Text);
            this.txtNumber15.Text = "0";
            this.txtNumber15.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber15.Text = accidentSort5 != null ? ((accidentSort5.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber15.Text)).ToString() : Convert.ToInt32(this.txtNumber15.Text).ToString("N0");
            this.txtPersonNum15.Text = "0";
            this.txtPersonNum15.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum15.Text = accidentSort5 != null ? ((accidentSort5.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum15.Text)).ToString() : Convert.ToInt32(this.txtPersonNum15.Text).ToString("N0");
            this.txtLoseHours15.Text = "0";
            this.txtLoseHours15.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours15.Text = accidentSort5 != null ? ((accidentSort5.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours15.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours15.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney15.Text = "0";
            this.txtLoseMoney15.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney15.Text = accidentSort5 != null ? ((accidentSort5.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney15.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney15.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType16 = "3";   //现场处置（急救）
            Model.Manager_AccidentSortB accidentSort6 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType16.Text);
            this.txtNumber16.Text = "0";
            this.txtNumber16.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber16.Text = accidentSort6 != null ? ((accidentSort6.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber16.Text)).ToString() : Convert.ToInt32(this.txtNumber16.Text).ToString("N0");
            this.txtPersonNum16.Text = "0";
            this.txtPersonNum16.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum16.Text = accidentSort6 != null ? ((accidentSort6.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum16.Text)).ToString() : Convert.ToInt32(this.txtPersonNum16.Text).ToString("N0");
            this.txtLoseHours16.Text = "0";
            this.txtLoseHours16.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours16.Text = accidentSort6 != null ? ((accidentSort6.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours16.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours16.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney16.Text = "0";
            this.txtLoseMoney16.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney16.Text = accidentSort6 != null ? ((accidentSort6.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney16.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney16.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType21 = "4";  //未遂事故
            Model.Manager_AccidentSortB accidentSort21 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType21.Text);
            this.txtNumber21.Text = "0";
            this.txtNumber21.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber21.Text = accidentSort21 != null ? ((accidentSort21.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber21.Text)).ToString() : Convert.ToInt32(this.txtNumber21.Text).ToString("N0");
            this.txtPersonNum21.Text = "0";
            this.txtPersonNum21.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum21.Text = accidentSort21 != null ? ((accidentSort21.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum21.Text)).ToString() : Convert.ToInt32(this.txtPersonNum21.Text).ToString("N0");
            this.txtLoseHours21.Text = "0";
            this.txtLoseHours21.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours21.Text = accidentSort21 != null ? ((accidentSort21.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours21.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours21.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney21.Text = "0";
            this.txtLoseMoney21.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney21.Text = accidentSort21 != null ? ((accidentSort21.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney21.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney21.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType22 = "4";  //火灾事故
            Model.Manager_AccidentSortB accidentSort22 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType22.Text);
            this.txtNumber22.Text = "0";
            this.txtNumber22.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber22.Text = accidentSort22 != null ? ((accidentSort22.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber22.Text)).ToString() : Convert.ToInt32(this.txtNumber22.Text).ToString("N0");
            this.txtPersonNum22.Text = "0";
            this.txtPersonNum22.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum22.Text = accidentSort22 != null ? ((accidentSort22.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum22.Text)).ToString() : Convert.ToInt32(this.txtPersonNum22.Text).ToString("N0");
            this.txtLoseHours22.Text = "0";
            this.txtLoseHours22.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours22.Text = accidentSort22 != null ? ((accidentSort22.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours22.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours22.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney22.Text = "0";
            this.txtLoseMoney22.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney22.Text = accidentSort22 != null ? ((accidentSort22.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney22.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney22.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType23 = "5";   //爆炸事故
            Model.Manager_AccidentSortB accidentSort23 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType23.Text);
            this.txtNumber23.Text = "0";
            this.txtNumber23.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber23.Text = accidentSort23 != null ? ((accidentSort23.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber23.Text)).ToString() : Convert.ToInt32(this.txtNumber23.Text).ToString("N0");
            this.txtPersonNum23.Text = "0";
            this.txtPersonNum23.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum23.Text = accidentSort23 != null ? ((accidentSort23.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum23.Text)).ToString() : Convert.ToInt32(this.txtPersonNum23.Text).ToString("N0");
            this.txtLoseHours23.Text = "0";
            this.txtLoseHours23.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours23.Text = accidentSort23 != null ? ((accidentSort23.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours23.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours23.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney23.Text = "0";
            this.txtLoseMoney23.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney23.Text = accidentSort23 != null ? ((accidentSort23.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney23.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney23.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType24 = "6";   //道路交通事故
            Model.Manager_AccidentSortB accidentSort24 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType24.Text);
            this.txtNumber24.Text = "0";
            this.txtNumber24.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber24.Text = accidentSort24 != null ? ((accidentSort24.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber24.Text)).ToString() : Convert.ToInt32(this.txtNumber24.Text).ToString("N0");
            this.txtPersonNum24.Text = "0";
            this.txtPersonNum24.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum24.Text = accidentSort24 != null ? ((accidentSort24.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum24.Text)).ToString() : Convert.ToInt32(this.txtPersonNum24.Text).ToString("N0");
            this.txtLoseHours24.Text = "0";
            this.txtLoseHours24.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours24.Text = accidentSort24 != null ? ((accidentSort24.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours24.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours24.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney24.Text = "0";
            this.txtLoseMoney24.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney24.Text = accidentSort24 != null ? ((accidentSort24.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney24.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney24.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType25 = "7";  //机械设备事故
            Model.Manager_AccidentSortB accidentSort25 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType25.Text);
            this.txtNumber25.Text = "0";
            this.txtNumber25.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber25.Text = accidentSort25 != null ? ((accidentSort25.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber25.Text)).ToString() : Convert.ToInt32(this.txtNumber25.Text).ToString("N0");
            this.txtPersonNum25.Text = "0";
            this.txtPersonNum25.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum25.Text = accidentSort25 != null ? ((accidentSort25.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum25.Text)).ToString() : Convert.ToInt32(this.txtPersonNum25.Text).ToString("N0");
            this.txtLoseHours25.Text = "0";
            this.txtLoseHours25.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours25.Text = accidentSort25 != null ? ((accidentSort25.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours25.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours25.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney25.Text = "0";
            this.txtLoseMoney25.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney25.Text = accidentSort25 != null ? ((accidentSort25.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney25.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney25.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType26 = "8";  //环境污染事故
            Model.Manager_AccidentSortB accidentSort26 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType26.Text);
            this.txtNumber26.Text = "0";
            this.txtNumber26.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber26.Text = accidentSort26 != null ? ((accidentSort26.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber26.Text)).ToString() : Convert.ToInt32(this.txtNumber26.Text).ToString("N0");
            this.txtPersonNum26.Text = "0";
            this.txtPersonNum26.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum26.Text = accidentSort26 != null ? ((accidentSort26.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum26.Text)).ToString() : Convert.ToInt32(this.txtPersonNum26.Text).ToString("N0");
            this.txtLoseHours26.Text = "0";
            this.txtLoseHours26.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours26.Text = accidentSort26 != null ? ((accidentSort26.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours26.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours26.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney26.Text = "0";
            this.txtLoseMoney26.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney26.Text = accidentSort26 != null ? ((accidentSort26.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney26.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney26.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType27 = "9";  //职业病
            Model.Manager_AccidentSortB accidentSort27 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType27.Text);
            this.txtNumber27.Text = "0";
            this.txtNumber27.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber27.Text = accidentSort27 != null ? ((accidentSort27.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber27.Text)).ToString() : Convert.ToInt32(this.txtNumber27.Text).ToString("N0");
            this.txtPersonNum27.Text = "0";
            this.txtPersonNum27.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum27.Text = accidentSort27 != null ? ((accidentSort27.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum27.Text)).ToString() : Convert.ToInt32(this.txtPersonNum27.Text).ToString("N0");
            this.txtLoseHours27.Text = "0";
            this.txtLoseHours27.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours27.Text = accidentSort27 != null ? ((accidentSort27.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours27.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours27.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney27.Text = "0";
            this.txtLoseMoney27.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney27.Text = accidentSort27 != null ? ((accidentSort27.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney27.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney27.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType28 = "10";  //生产事故
            Model.Manager_AccidentSortB accidentSort28 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType28.Text);
            this.txtNumber28.Text = "0";
            this.txtNumber28.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber28.Text = accidentSort28 != null ? ((accidentSort28.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber28.Text)).ToString() : Convert.ToInt32(this.txtNumber28.Text).ToString("N0");
            this.txtPersonNum28.Text = "0";
            this.txtPersonNum28.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum28.Text = accidentSort28 != null ? ((accidentSort28.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum28.Text)).ToString() : Convert.ToInt32(this.txtPersonNum28.Text).ToString("N0");
            this.txtLoseHours28.Text = "0";
            this.txtLoseHours28.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours28.Text = accidentSort28 != null ? ((accidentSort28.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours28.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours28.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney28.Text = "0";
            this.txtLoseMoney28.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney28.Text = accidentSort28 != null ? ((accidentSort28.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney28.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney28.Text.Replace(",", "").Trim()).ToString("N0");

            string accidentType29 = "11";   //其它事故
            Model.Manager_AccidentSortB accidentSort29 = BLL.AccidentSortBService.GetAccidentSortsByMonthReportIdAndAccidentType(LastMonthReportId, this.lblAccidentType29.Text);
            this.txtNumber29.Text = "0";
            this.txtNumber29.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString();
            this.txtSumNumber29.Text = accidentSort29 != null ? ((accidentSort29.TotalNum ?? 0) + Convert.ToInt32(this.txtNumber29.Text)).ToString() : Convert.ToInt32(this.txtNumber29.Text).ToString("N0");
            this.txtPersonNum29.Text = "0";
            this.txtPersonNum29.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString();
            this.txtSumPersonNum29.Text = accidentSort29 != null ? ((accidentSort29.TotalPersonNum ?? 0) + Convert.ToInt32(this.txtPersonNum29.Text)).ToString() : Convert.ToInt32(this.txtPersonNum29.Text).ToString("N0");
            this.txtLoseHours29.Text = "0";
            this.txtLoseHours29.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseHours29.Text = accidentSort29 != null ? ((accidentSort29.TotalLoseHours ?? 0) + Convert.ToInt32(this.txtLoseHours29.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseHours29.Text.Replace(",", "").Trim()).ToString("N0");
            this.txtLoseMoney29.Text = "0";
            this.txtLoseMoney29.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtSumLoseMoney29.Text = accidentSort29 != null ? ((accidentSort29.TotalLoseMoney ?? 0) + Convert.ToInt32(this.txtLoseMoney29.Text.Replace(",", "").Trim())).ToString("N0") : Convert.ToInt32(this.txtLoseMoney29.Text.Replace(",", "").Trim()).ToString("N0");
            //事故数据
            List<Model.Accident_AccidentReport> accidentReports = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReportOther> accidentReportOthers = BLL.AccidentReportOtherService.GetAccidentReportOthersByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReport> recordAccidentReports = BLL.AccidentReport2Service.GetRecordAccidentReportsByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReportOther> recordAccidentReportOthers = BLL.AccidentReportOtherService.GetRecordAccidentReportOthersByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            int countA = recordAccidentReports.Count + recordAccidentReportOthers.Count;
            this.txtAccidentNum.Text = countA.ToString();
            int sumManhours = Convert.ToInt32(this.txtSumManhours.Text.Replace(",", "").Trim());
            //百万工时总可记录事故率：当期的医院处置、工作受限事故、轻伤、重伤、死亡事故总次数*一百万/当期累计总工时数
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
                this.txtAccidentRateE.Text = decimal.Round((Convert.ToDecimal(countE) * 1000000 / sumManhours), 2).ToString();
            }
            else
            {
                this.txtAccidentRateE.Text = "0";
            }
            //事故台账
            List<Model.Manager_AccidentDetailSortB> accidentDetailSorts = new List<Model.Manager_AccidentDetailSortB>();
            foreach (var item in accidentReports)
            {
                Model.Manager_AccidentDetailSortB accidentDetailSort = new Model.Manager_AccidentDetailSortB
                {
                    AccidentDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_AccidentDetailSortB)),
                    MonthReportId = this.CurrUser.LoginProjectId,
                    Abstract = item.Abstract
                };
                Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentReportRegistration).FirstOrDefault(x => x.ConstValue == item.AccidentTypeId);
                if (c != null)
                {
                    accidentDetailSort.AccidentType = c.ConstText;
                }
                accidentDetailSort.PeopleNum = item.PeopleNum;
                accidentDetailSort.WorkingHoursLoss = item.WorkingHoursLoss;
                accidentDetailSort.EconomicLoss = item.EconomicLoss;
                accidentDetailSort.AccidentDate = item.AccidentDate;
                accidentDetailSorts.Add(accidentDetailSort);
            }
            foreach (var item in accidentReportOthers)
            {
                Model.Manager_AccidentDetailSortB accidentDetailSort = new Model.Manager_AccidentDetailSortB
                {
                    AccidentDetailSortId = SQLHelper.GetNewID(typeof(Model.Manager_AccidentDetailSortB)),
                    MonthReportId = this.CurrUser.LoginProjectId,
                    Abstract = item.Abstract
                };
                Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentInvestigationProcessingReport).FirstOrDefault(x => x.ConstValue == item.AccidentTypeId);
                if (c != null)
                {
                    accidentDetailSort.AccidentType = c.ConstText;
                }
                accidentDetailSort.PeopleNum = item.PeopleNum;
                accidentDetailSort.WorkingHoursLoss = item.WorkingHoursLoss;
                accidentDetailSort.EconomicLoss = item.EconomicLoss;
                accidentDetailSort.AccidentDate = item.AccidentDate;
                accidentDetailSorts.Add(accidentDetailSort);
            }
            this.GridAccidentDetailSort.DataSource = accidentDetailSorts;
            this.GridAccidentDetailSort.DataBind();
            string review = string.Empty;
            foreach (var item in accidentReports)
            {
                if (item.IsNotConfirm == true)
                {
                    review += item.Abstract + "(待定):" + item.ProcessDescription + "\r\n";
                }
                else
                {
                    review += item.Abstract + ":" + item.ProcessDescription + "\r\n";
                }
            }
            foreach (var item in accidentReportOthers)
            {
                review += item.Abstract + ":" + item.ProcessDescription + "\r\n";
            }
            this.txtAccidentReview.Text = review;
        }
        #endregion

        #region 7.项目安全生产及文明施工措施费统计汇总表
        /// <summary>
        ///  7.项目安全生产及文明施工措施费统计汇总表
        /// </summary>
        private void GetHseCost()
        {
            decimal totalPlanCostA = 0, totalPlanCostB = 0, totalRealCostA = 0, totalProjectRealCostA = 0, totalRealCostB = 0, totalProjectRealCostB = 0, totalRealCostAB = 0, totalProjectRealCostAB = 0;
            List<Model.Manager_HseCostB> hseCostBs = new List<Model.Manager_HseCostB>();
            List<string> unitIds = (from x in Funs.DB.Project_ProjectUnit
                                    where x.ProjectId == this.CurrUser.LoginProjectId && (x.UnitType == "1" || x.UnitType == "2")
                                    select x.UnitId).ToList();
            foreach (var unitId in unitIds)
            {
                Model.Manager_HseCostB hseCost = new Model.Manager_HseCostB
                {
                    HseCostId = SQLHelper.GetNewID(typeof(Model.Manager_HseCostB)),
                    UnitId = unitId
                };
                Model.Manager_HseCostB lastHseCost = BLL.HseCostBService.GetHseCostsByMonthReportIdAndUnitId(LastMonthReportId, unitId);  //获取本单位上月的记录
                Model.Project_ProjectUnit projectUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, unitId);
                if (projectUnit != null)
                {
                    if (projectUnit.PlanCostA != null)
                    {
                        hseCost.PlanCostA = projectUnit.PlanCostA;
                    }
                    if (projectUnit.PlanCostB != null)
                    {
                        hseCost.PlanCostB = projectUnit.PlanCostB;
                    }
                }
                hseCost.RealCostA = BLL.CostSmallDetailItemService.GetCostDetailsByUnitIdAndCostType(this.CurrUser.LoginProjectId, unitId, startTime, endTime, "A");
                if (lastHseCost != null)
                {
                    hseCost.ProjectRealCostA = (lastHseCost.ProjectRealCostA ?? 0) + hseCost.RealCostA;
                }
                else
                {
                    hseCost.ProjectRealCostA = hseCost.RealCostA;
                }
                hseCost.RealCostB = BLL.CostSmallDetailItemService.GetCostDetailsByUnitIdAndCostType(this.CurrUser.LoginProjectId, unitId, startTime, endTime, "B");
                if (lastHseCost != null)
                {
                    hseCost.ProjectRealCostB = (lastHseCost.ProjectRealCostB ?? 0) + hseCost.RealCostB;
                }
                else
                {
                    hseCost.ProjectRealCostB = hseCost.RealCostB;
                }
                hseCost.RealCostAB = hseCost.RealCostA + hseCost.RealCostB;
                if (lastHseCost != null)
                {
                    hseCost.ProjectRealCostAB = (lastHseCost.ProjectRealCostAB ?? 0) + hseCost.RealCostAB;
                }
                else
                {
                    hseCost.ProjectRealCostAB = hseCost.RealCostAB;
                }
                hseCostBs.Add(hseCost);
                totalPlanCostA += hseCost.PlanCostA ?? 0;
                totalPlanCostB += hseCost.PlanCostB ?? 0;
                totalRealCostA += hseCost.RealCostA ?? 0;
                totalProjectRealCostA += hseCost.ProjectRealCostA ?? 0;
                totalRealCostB += hseCost.RealCostB ?? 0;
                totalProjectRealCostB += hseCost.ProjectRealCostB ?? 0;
                totalRealCostAB += hseCost.RealCostAB ?? 0;
                totalProjectRealCostAB += hseCost.ProjectRealCostAB ?? 0;
            }
            this.GridHSECostSort.DataSource = hseCostBs;
            this.GridHSECostSort.DataBind();
            if (this.GridHSECostSort.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("UnitId", "合计：");
                summary.Add("PlanCostA", totalPlanCostA);
                summary.Add("PlanCostB", totalPlanCostB);
                summary.Add("RealCostA", totalRealCostA);
                summary.Add("ProjectRealCostA", totalProjectRealCostA);
                summary.Add("RealCostB", totalRealCostB);
                summary.Add("ProjectRealCostB", totalProjectRealCostB);
                summary.Add("RealCostAB", totalRealCostAB);
                summary.Add("ProjectRealCostAB", totalProjectRealCostAB);
                this.GridHSECostSort.SummaryData = summary;
            }
            else
            {
                this.GridHSECostSort.SummaryData = null;
            }
        }
        #endregion

        #region 8.项目施工现场HSE培训情况统计
        /// <summary>
        ///  8.项目施工现场HSE培训情况统计
        /// </summary>
        private void GetTrainSort()
        {
            int sumTrainNumber = 0;
            int sumTotalTrainNum = 0;
            int sumTrainPersonNumber = 0;
            int sumTotalTrainPersonNum = 0;
            var trainTypes = BLL.TrainTypeService.GetTrainTypeList();
            List<Model.Manager_TrainSortB> trainSorts = new List<Model.Manager_TrainSortB>();
            //当月记录集合
            var monthTrainings = from x in Funs.DB.EduTrain_TrainRecord where x.ProjectId == this.CurrUser.LoginProjectId && x.TrainStartDate >= startTime && x.TrainStartDate < endTime && x.States == BLL.Const.State_2 select x;
            foreach (var item in trainTypes)
            {
                Model.Manager_TrainSortB trainSort = new Model.Manager_TrainSortB
                {
                    TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort)),
                    TrainType = item.TrainTypeName
                };
                var trainings = from x in monthTrainings where x.TrainTypeId == item.TrainTypeId select x;
                trainSort.TrainNumber = trainings.Count();
                Model.Manager_TrainSortB lastTrainSort = BLL.TrainSortBService.GetTrainSortByMonthReportIdAndTrainType(LastMonthReportId, item.TrainTypeName);
                trainSort.TotalTrainNum = lastTrainSort != null ? (lastTrainSort.TotalTrainNum + trainSort.TrainNumber) : trainSort.TrainNumber;
                trainSort.TrainPersonNumber = (from x in trainings
                                               join y in Funs.DB.EduTrain_TrainRecordDetail
                                               on x.TrainingId equals y.TrainingId
                                               select y).Count();
                trainSort.TotalTrainPersonNum = lastTrainSort != null ? (lastTrainSort.TotalTrainPersonNum + trainSort.TrainPersonNumber) : trainSort.TrainPersonNumber;
                trainSorts.Add(trainSort);
                sumTrainNumber += trainSort.TrainNumber ?? 0;
                sumTotalTrainNum += trainSort.TotalTrainNum ?? 0;
                sumTrainPersonNumber += trainSort.TrainPersonNumber ?? 0;
                sumTotalTrainPersonNum += trainSort.TotalTrainPersonNum ?? 0;
            }
            this.GridTrainSort.DataSource = trainSorts;
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

        #region 9.项目施工现场HSE会议情况统计
        /// <summary>
        ///  9.项目施工现场HSE会议情况统计
        /// </summary>
        private void GetMeetingSort()
        {
            Model.Manager_MeetingSortB meetingSort1 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(LastMonthReportId, this.lblMeetingType1.Text);
            this.txtMeetingNumber1.Text = BLL.WeekMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingNumber1.Text = meetingSort1 != null ? (meetingSort1.TotalMeetingNum + Convert.ToInt32(this.txtMeetingNumber1.Text)).ToString() : Convert.ToInt32(this.txtMeetingNumber1.Text).ToString("N0");
            this.txtMeetingPersonNumber1.Text = BLL.WeekMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingPersonNumber1.Text = meetingSort1 != null ? (meetingSort1.TotalMeetingPersonNum + Convert.ToInt32(this.txtMeetingPersonNumber1.Text)).ToString() : Convert.ToInt32(this.txtMeetingPersonNumber1.Text).ToString("N0");

            Model.Manager_MeetingSortB meetingSort2 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(LastMonthReportId, this.lblMeetingType2.Text);
            this.txtMeetingNumber2.Text = BLL.MonthMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingNumber2.Text = meetingSort2 != null ? (meetingSort2.TotalMeetingNum + Convert.ToInt32(this.txtMeetingNumber2.Text)).ToString() : Convert.ToInt32(this.txtMeetingNumber2.Text).ToString("N0");
            this.txtMeetingPersonNumber2.Text = BLL.MonthMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingPersonNumber2.Text = meetingSort2 != null ? (meetingSort2.TotalMeetingPersonNum + Convert.ToInt32(this.txtMeetingPersonNumber2.Text)).ToString() : Convert.ToInt32(this.txtMeetingPersonNumber2.Text).ToString("N0");

            Model.Manager_MeetingSortB meetingSort3 = BLL.MeetingSortBService.GetMeetingSortsByMonthReportIdAndMeetingType(LastMonthReportId, this.lblMeetingType3.Text);
            this.txtMeetingNumber3.Text = BLL.SpecialMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingNumber3.Text = meetingSort3 != null ? (meetingSort3.TotalMeetingNum + Convert.ToInt32(this.txtMeetingNumber3.Text)).ToString() : Convert.ToInt32(this.txtMeetingNumber3.Text).ToString("N0");
            this.txtMeetingPersonNumber3.Text = BLL.SpecialMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumMeetingPersonNumber3.Text = meetingSort3 != null ? (meetingSort3.TotalMeetingPersonNum + Convert.ToInt32(this.txtMeetingPersonNumber3.Text)).ToString() : Convert.ToInt32(this.txtMeetingPersonNumber3.Text).ToString("N0");

            this.txtAllMeetingNumber.Text = (Convert.ToInt32(this.txtMeetingNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber3.Text.Trim())).ToString();
            this.txtAllMeetingPersonNumber.Text = (Convert.ToInt32(this.txtMeetingPersonNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingNumber.Text = (Convert.ToInt32(this.txtSumMeetingNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingNumber3.Text.Trim())).ToString();
            this.txtAllSumMeetingPersonNumber.Text = (Convert.ToInt32(this.txtSumMeetingPersonNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingPersonNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumMeetingPersonNumber3.Text.Trim())).ToString();
        }
        #endregion

        #region 10.项目施工现场HSE检查情况统计
        /// <summary>
        ///  10.项目施工现场HSE检查情况统计
        /// </summary>
        private void GetCheckSort()
        {
            Model.Manager_CheckSortB checkSort1 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(LastMonthReportId, this.lblCheckType1.Text);
            this.txtCheckNumber1.Text = BLL.Check_CheckDayService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumCheckNumber1.Text = checkSort1 != null ? (checkSort1.TotalCheckNum + Convert.ToInt32(this.txtCheckNumber1.Text)).ToString() : this.txtCheckNumber1.Text.ToString();
            int totalViolationNumber1 = (from x in Funs.DB.Check_CheckDay
                                         join y in Funs.DB.Check_CheckDayDetail
                                         on x.CheckDayId equals y.CheckDayId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                         select y).Count();
            this.txtViolationNumber1.Text = totalViolationNumber1.ToString();
            this.txtSumViolationNumber1.Text = checkSort1 != null ? (checkSort1.TotalViolationNum + Convert.ToInt32(this.txtViolationNumber1.Text)).ToString() : this.txtViolationNumber1.Text.ToString();

            Model.Manager_CheckSortB checkSort2 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(LastMonthReportId, this.lblCheckType2.Text);
            this.txtCheckNumber2.Text = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "0").ToString();
            this.txtSumCheckNumber2.Text = checkSort2 != null ? (checkSort2.TotalCheckNum + Convert.ToInt32(this.txtCheckNumber2.Text)).ToString() : this.txtCheckNumber2.Text.ToString();
            int totalViolationNumber2 = (from x in Funs.DB.Check_CheckColligation
                                         join y in Funs.DB.Check_CheckColligationDetail
                                         on x.CheckColligationId equals y.CheckColligationId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 && x.CheckType == "0"
                                         select y).Count();
            this.txtViolationNumber2.Text = totalViolationNumber2.ToString();
            this.txtSumViolationNumber2.Text = checkSort2 != null ? (checkSort2.TotalViolationNum + Convert.ToInt32(this.txtViolationNumber2.Text)).ToString() : this.txtViolationNumber2.Text.ToString();

            Model.Manager_CheckSortB checkSort3 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(LastMonthReportId, this.lblCheckType3.Text);
            this.txtCheckNumber3.Text = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "1").ToString();
            this.txtSumCheckNumber3.Text = checkSort3 != null ? (checkSort3.TotalCheckNum + Convert.ToInt32(this.txtCheckNumber3.Text)).ToString() : this.txtCheckNumber3.Text.ToString();
            int totalViolationNumber3 = (from x in Funs.DB.Check_CheckColligation
                                         join y in Funs.DB.Check_CheckColligationDetail
                                         on x.CheckColligationId equals y.CheckColligationId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 && x.CheckType == "1"
                                         select y).Count();
            this.txtViolationNumber3.Text = totalViolationNumber3.ToString();
            this.txtSumViolationNumber3.Text = checkSort3 != null ? (checkSort3.TotalViolationNum + Convert.ToInt32(this.txtViolationNumber3.Text)).ToString() : this.txtViolationNumber3.Text.ToString();

            Model.Manager_CheckSortB checkSort4 = BLL.CheckSortBService.GetCheckSortByMonthReportIdAndCheckType(LastMonthReportId, this.lblCheckType4.Text);
            this.txtCheckNumber4.Text = BLL.Check_CheckSpecialService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtSumCheckNumber4.Text = checkSort4 != null ? (checkSort4.TotalCheckNum + Convert.ToInt32(this.txtCheckNumber4.Text)).ToString() : this.txtCheckNumber4.Text.ToString();
            int totalViolationNumber4 = (from x in Funs.DB.Check_CheckSpecial
                                         join y in Funs.DB.Check_CheckSpecialDetail
                                         on x.CheckSpecialId equals y.CheckSpecialId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                         select y).Count();
            this.txtViolationNumber4.Text = totalViolationNumber4.ToString();
            this.txtSumViolationNumber4.Text = checkSort4 != null ? (checkSort4.TotalViolationNum + Convert.ToInt32(this.txtViolationNumber4.Text)).ToString() : this.txtViolationNumber4.Text.ToString();

            this.txtAllCheckNumber.Text = (Convert.ToInt32(this.txtCheckNumber1.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber2.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber3.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber4.Text.Trim())).ToString();
            this.txtAllSumCheckNumber.Text = (Convert.ToInt32(this.txtSumCheckNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber3.Text.Trim()) + Convert.ToInt32(this.txtSumCheckNumber4.Text.Trim())).ToString();
            this.txtAllViolationNumber.Text = (totalViolationNumber1 + totalViolationNumber2 + totalViolationNumber3 + totalViolationNumber4).ToString();
            this.txtAllSumViolationNumber.Text = (Convert.ToInt32(this.txtSumViolationNumber1.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber2.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber3.Text.Trim()) + Convert.ToInt32(this.txtSumViolationNumber4.Text.Trim())).ToString();
        }
        #endregion

        #region 11.项目施工现场HSE奖惩情况统计
        /// <summary>
        ///  11.项目施工现场HSE奖惩情况统计
        /// </summary>
        private void GetIncentiveSort()
        {
            string incentiveType11 = "1";
            this.txtIncentiveMoney1.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType11, this.CurrUser.LoginProjectId));
            Model.Manager_IncentiveSortB incentiveSort1 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(LastMonthReportId, this.lblIncentiveType11.Text);
            this.txtSumIncentiveMoney1.Text = incentiveSort1 != null ? string.Format("{0:N2}", (incentiveSort1.TotalIncentiveMoney + Convert.ToDecimal(this.txtIncentiveMoney1.Text))) : this.txtIncentiveMoney1.Text;

            string incentiveType12 = "2";
            this.txtIncentiveMoney2.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType12, this.CurrUser.LoginProjectId));
            Model.Manager_IncentiveSortB incentiveSort2 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(LastMonthReportId, this.lblIncentiveType12.Text);
            this.txtSumIncentiveMoney2.Text = incentiveSort2 != null ? string.Format("{0:N2}", (incentiveSort2.TotalIncentiveMoney + Convert.ToDecimal(this.txtIncentiveMoney2.Text))) : this.txtIncentiveMoney2.Text;

            string incentiveType13 = "3";
            this.txtIncentiveMoney3.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType13, this.CurrUser.LoginProjectId));
            Model.Manager_IncentiveSortB incentiveSort3 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(LastMonthReportId, this.lblIncentiveType13.Text);
            this.txtSumIncentiveMoney3.Text = incentiveSort3 != null ? string.Format("{0:N2}", (incentiveSort3.TotalIncentiveMoney + Convert.ToDecimal(this.txtIncentiveMoney3.Text))) : this.txtIncentiveMoney3.Text;

            string incentiveType14 = "4";
            this.txtIncentiveMoney4.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType14, this.CurrUser.LoginProjectId));
            Model.Manager_IncentiveSortB incentiveSort4 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(LastMonthReportId, this.lblIncentiveType14.Text);
            this.txtSumIncentiveMoney4.Text = incentiveSort4 != null ? string.Format("{0:N2}", (incentiveSort4.TotalIncentiveMoney + Convert.ToDecimal(this.txtIncentiveMoney4.Text))) : this.txtIncentiveMoney4.Text;

            this.txtIncentiveMoney5.Text = string.Format("{0:N2}", BLL.PunishNoticeService.GetSumMoneyByTime(startTime, endTime, this.CurrUser.LoginProjectId));
            Model.Manager_IncentiveSortB incentiveSort5 = BLL.IncentiveSortBService.GetIncentiveSortByMonthReportIdAndIncentiveType(LastMonthReportId, this.lblIncentiveType15.Text);
            this.txtSumIncentiveMoney5.Text = incentiveSort5 != null ? string.Format("{0:N2}", (incentiveSort5.TotalIncentiveMoney + Convert.ToDecimal(this.txtIncentiveMoney5.Text))) : this.txtIncentiveMoney5.Text;

            this.txtIncentiveNumber1.Text = "0";
            this.txtIncentiveNumber1.Text = BLL.ViolationPersonService.GetViolationPersonNum(startTime, endTime, "1", this.CurrUser.LoginProjectId).ToString();
            int? sumIncentiveNumber1 = BLL.IncentiveSortBService.GetSumIncentiveNumberByMonthReportId(LastMonthReportId, "通 报 批 评 （人/次）");
            this.txtSumIncentiveNumber1.Text = sumIncentiveNumber1 != null ? (sumIncentiveNumber1.Value + Convert.ToInt32(this.txtIncentiveNumber1.Text)).ToString() : this.txtIncentiveNumber1.Text;

            this.txtIncentiveNumber2.Text = "0";
            this.txtIncentiveNumber2.Text = BLL.ViolationPersonService.GetViolationPersonNum(startTime, endTime, "2", this.CurrUser.LoginProjectId).ToString();
            int? sumIncentiveNumber2 = BLL.IncentiveSortBService.GetSumIncentiveNumberByMonthReportId(LastMonthReportId, "开 除 （人/次）");
            this.txtSumIncentiveNumber2.Text = sumIncentiveNumber2 != null ? (sumIncentiveNumber2.Value + Convert.ToInt32(this.txtIncentiveNumber2.Text)).ToString() : this.txtIncentiveNumber2.Text;
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
            if (string.IsNullOrEmpty(MonthReportId))
            {
                // 冻结时间
                var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
                if (sysSet != null)
                {
                    int freezeDay = !string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 5;
                    Model.Manager_MonthReportB lastMonthReport = BLL.MonthReportBService.GetLastMonthReportByDate(DateTime.Now, freezeDay, this.CurrUser.LoginProjectId);
                    if (lastMonthReport != null)
                    {
                        DateTime months = Convert.ToDateTime(lastMonthReport.Months);
                        string date1 = months.Date.Year.ToString() + "-" + months.Date.Month.ToString();
                        string date2 = this.txtMonths.Text.Trim();
                        if (date1 == date2)
                        {
                            Alert.ShowInTop("该月份月报已经存在，不能重复添加！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            Model.Manager_MonthReportB monthReport = new Model.Manager_MonthReportB
            {
                MonthReportCode = this.txtMonthReportCode.Text,
                ProjectId = this.ProjectId,
                Months = Funs.GetNewDateTime(this.txtMonths.Text + "-1"),
                MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                ReportMan = this.CurrUser.UserId
            };
            if (!string.IsNullOrEmpty(this.txtManhours.Text.Trim()))
            {
                monthReport.Manhours = Convert.ToInt32(this.txtManhours.Text.Replace(",", "").Trim());
            }
            else
            {
                monthReport.Manhours = null;
            }
            if (!string.IsNullOrEmpty(this.txtSumManhours.Text.Trim()))
            {
                monthReport.TotalManhours = Convert.ToInt32(this.txtSumManhours.Text.Replace(",", "").Trim());
            }
            else
            {
                monthReport.TotalManhours = null;
            }
            if (!string.IsNullOrEmpty(this.txtHseManhours.Text.Trim()))
            {
                monthReport.HseManhours = Convert.ToInt32(this.txtHseManhours.Text.Replace(",", "").Trim());
            }
            else
            {
                monthReport.HseManhours = null;
            }
            if (!string.IsNullOrEmpty(this.txtSumHseManhours.Text.Trim()))
            {
                monthReport.TotalHseManhours = Convert.ToInt32(this.txtSumHseManhours.Text.Replace(",", "").Trim());
            }
            else
            {
                monthReport.TotalHseManhours = null;
            }
            if (!string.IsNullOrEmpty(this.txtNoStartDate.Text.Trim()))
            {
                monthReport.NoStartDate = Convert.ToDateTime(this.txtNoStartDate.Text.Trim());
            }
            else
            {
                monthReport.NoStartDate = null;
            }
            if (!string.IsNullOrEmpty(this.txtNoEndDate.Text.Trim()))
            {
                monthReport.NoEndDate = Convert.ToDateTime(this.txtNoEndDate.Text.Trim());
            }
            else
            {
                monthReport.NoEndDate = null;
            }
            monthReport.SafetyManhours = Convert.ToInt32(this.txtSafetyManhours.Text.Replace(",", "").Trim());
            monthReport.TotalManNum = Funs.GetNewIntOrZero(this.GridManhoursSort.SummaryData.GetValue("PersonTotal").ToString());
            monthReport.AccidentReview = this.txtAccidentReview.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtAccidentNum.Text.Trim()))
            {
                monthReport.AccidentNum = Convert.ToInt32(this.txtAccidentNum.Text.Trim());
            }
            else
            {
                monthReport.AccidentNum = null;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentRateA.Text.Trim()))
            {
                monthReport.AccidentRateA = Convert.ToDecimal(this.txtAccidentRateA.Text.Trim());
            }
            else
            {
                monthReport.AccidentRateA = null;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentRateB.Text.Trim()))
            {
                monthReport.AccidentRateB = Convert.ToDecimal(this.txtAccidentRateB.Text.Trim());
            }
            else
            {
                monthReport.AccidentRateB = null;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentRateC.Text.Trim()))
            {
                monthReport.AccidentRateC = Convert.ToDecimal(this.txtAccidentRateC.Text.Trim());
            }
            else
            {
                monthReport.AccidentRateC = null;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentRateD.Text.Trim()))
            {
                monthReport.AccidentRateD = Convert.ToDecimal(this.txtAccidentRateD.Text.Trim());
            }
            else
            {
                monthReport.AccidentRateD = null;
            }
            if (!string.IsNullOrEmpty(this.txtAccidentRateE.Text.Trim()))
            {
                monthReport.AccidentRateE = Convert.ToDecimal(this.txtAccidentRateE.Text.Trim());
            }
            else
            {
                monthReport.AccidentRateE = null;
            }
            monthReport.HseActiveReview = this.txtHseActiveReview.Text.Trim();
            monthReport.HseActiveKey = this.txtHseActiveKey.Text.Trim();
            monthReport.LargerHazardNun = Funs.GetNewIntOrZero(this.txtLargerHazardNum.Text.Trim());
            monthReport.TotalLargerHazardNun = Funs.GetNewIntOrZero(this.txtTotalLargerHazardNum.Text.Trim());
            monthReport.IsArgumentLargerHazardNun = Funs.GetNewIntOrZero(this.txtIsArgumentLargerHazardNun.Text.Trim());
            monthReport.TotalIsArgumentLargerHazardNun = Funs.GetNewIntOrZero(this.txtTotalIsArgumentLargerHazardNun.Text.Trim());
            Model.Manager_CostAnalyse costAnalyse = new Model.Manager_CostAnalyse();
            //当月总费用
            decimal cost = Funs.GetNewDecimalOrZero(this.GridHSECostSort.SummaryData.GetValue("RealCostAB").ToString());
            if (!string.IsNullOrEmpty(this.MonthReportId))
            {
                monthReport.MonthReportId = this.MonthReportId;
                BLL.MonthReportBService.UpdateMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthBMenuId, BLL.Const.BtnModify);
                if (BLL.CostAnalyseService.getCostAnalyseByMonths(monthReport.Months, this.CurrUser.LoginProjectId) != null)
                {
                    costAnalyse = BLL.CostAnalyseService.getCostAnalyseByMonths(monthReport.Months, this.CurrUser.LoginProjectId);
                    if (monthReport.Manhours == 0 || monthReport.Manhours == null)
                    {
                        costAnalyse.Analyse = 0;
                        costAnalyse.Manhours = 0;
                    }
                    else
                    {
                        costAnalyse.Analyse = cost / monthReport.Manhours;
                        costAnalyse.Manhours = monthReport.Manhours;
                    }
                    costAnalyse.TotalRealCostMoney = cost;
                    BLL.CostAnalyseService.UpdateCostAnalyse(costAnalyse);
                }
                else
                {
                    costAnalyse.Months = monthReport.Months;
                    costAnalyse.ProjectId = this.CurrUser.LoginProjectId;
                    if (monthReport.Manhours == 0 || monthReport.Manhours == null)
                    {
                        costAnalyse.Analyse = 0;
                        costAnalyse.Manhours = 0;
                    }
                    else
                    {
                        costAnalyse.Analyse = cost / monthReport.Manhours;
                        costAnalyse.Manhours = monthReport.Manhours;
                    }
                    costAnalyse.TotalRealCostMoney = cost;
                    if (BLL.CostAnalyseService.getCostAnalyseByMonths(monthReport.Months, this.CurrUser.LoginProjectId) == null)
                    {
                        BLL.CostAnalyseService.AddCostAnalyse(costAnalyse);
                    }
                }
            }
            else
            {
                monthReport.MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport));
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportBService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthBMenuId, BLL.Const.BtnAdd);
                costAnalyse.Months = monthReport.Months;
                costAnalyse.ProjectId = this.CurrUser.LoginProjectId;
                if (monthReport.Manhours == 0 || monthReport.Manhours == null)
                {
                    costAnalyse.Analyse = 0;
                    costAnalyse.Manhours = 0;
                }
                else
                {
                    costAnalyse.Analyse = cost / monthReport.Manhours;
                    costAnalyse.Manhours = monthReport.Manhours;
                }
                costAnalyse.TotalRealCostMoney = cost;
                if (BLL.CostAnalyseService.getCostAnalyseByMonths(monthReport.Months, this.CurrUser.LoginProjectId) == null)
                {
                    BLL.CostAnalyseService.AddCostAnalyse(costAnalyse);
                }
                //删除未上报月报信息
                Model.ManagementReport_ReportRemind reportRemind = (from x in Funs.DB.ManagementReport_ReportRemind
                                                                    where x.ProjectId == this.ProjectId && x.Months == monthReport.Months && x.ReportName == "管理月报"
                                                                    select x).FirstOrDefault();
                if (reportRemind != null)
                {
                    BLL.ReportRemindService.DeleteReportRemindByReportRemind(reportRemind);
                }
            }
            ///保存人工时情况统计
            this.SaveManhoursSort();
            /// 保存事故分类统计
            this.SaveAccidentSort();
            /// 保存事故台账统计
            this.SaveAccidentDetailSort();
            ///保存费用情况统计
            this.SaveHseCostSort();
            ///保存教育与培训情况统计
            this.SaveTrainSort();
            /// 保存会议情况统计
            this.SaveMeetingSort();
            /// 保存HSE检查情况统计
            this.SaveCheckSort();
            /// 保存安全奖惩情况统计
            this.SaveIncentiveSort();
            //生成费用汇总表
            Model.TC_CostStatistic costStatistic = BLL.CostStatisticService.GetCostStatisticByMonthsAndProjectId(startTime, this.CurrUser.LoginProjectId);
            if (costStatistic != null)
            {
                BLL.CostStatisticDetailService.DeleteCostStatisticDetailByCostStatisticCode(costStatistic.CostStatisticCode);
                BLL.CostStatisticService.DeleteCostStatisticByCostStatisticCode(costStatistic.CostStatisticCode);
            }
            var units = from x in Funs.DB.Project_ProjectUnit
                        where x.ProjectId == this.CurrUser.LoginProjectId && (x.UnitType == "1" || x.UnitType == "2")
                        select x;     //1为总包，2为施工分包
            DateTime yearStartTime;
            DateTime projectStartTime;
            yearStartTime = Convert.ToDateTime(startTime.Year + "-01" + "-01");
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            if (project != null)
            {
                if (project.StartDate != null)
                {
                    projectStartTime = Convert.ToDateTime(project.StartDate);
                }
                else
                {
                    projectStartTime = Convert.ToDateTime("2000-01-01");
                }
            }
            else
            {
                projectStartTime = Convert.ToDateTime("2000-01-01");
            }
            Model.TC_CostStatistic newCostStatistic = new Model.TC_CostStatistic();
            string costStatisticCodePerfix = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectCode + "-CM01-FYHZ-";
            string costStatisticCode = BLL.SQLHelper.RunProcNewId("SpGetNewNumber", "TC_CostStatistic", "CostStatisticCode", this.CurrUser.LoginProjectId, costStatisticCodePerfix);
            newCostStatistic.CostStatisticCode = costStatisticCode;
            newCostStatistic.ProjectId = this.CurrUser.LoginProjectId;
            newCostStatistic.Months = startTime;
            newCostStatistic.CompileMan = this.CurrUser.UserId;
            newCostStatistic.CompileDate = DateTime.Now;
            BLL.CostStatisticService.AddCostStatistic(newCostStatistic);
            Model.TC_CostStatistic lastCostStatistic = BLL.CostStatisticService.GetLastCostStatisticByMonthsAndProjectId(startTime, this.CurrUser.LoginProjectId);
            foreach (Model.Project_ProjectUnit unit in units)
            {
                if (unit != null)
                {
                    List<Model.CostGoods_CostSmallDetailItem> projectDetails = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, unit.UnitId, projectStartTime, endTime);
                    if (projectDetails.Count > 0)
                    {
                        List<Model.CostGoods_CostSmallDetailItem> details = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, unit.UnitId, startTime, endTime);
                        List<Model.CostGoods_CostSmallDetailItem> yearDetails = BLL.CostSmallDetailItemService.GetCostDetailsByUnitId(this.CurrUser.LoginProjectId, unit.UnitId, yearStartTime, endTime);
                        Model.TC_CostStatisticDetail lastDetail = null;
                        if (lastCostStatistic != null)
                        {
                            lastDetail = BLL.CostStatisticDetailService.GetCostStatisticDetailByUnitId(unit.UnitId, lastCostStatistic.CostStatisticCode);
                        }
                        Model.TC_CostStatisticDetail detail = new Model.TC_CostStatisticDetail
                        {
                            CostStatisticCode = costStatisticCode,
                            UnitId = unit.UnitId,
                            A1 = (from x in details where x.CostType == "A1" select x.CostMoney ?? 0).Sum()
                        };
                        if (lastDetail == null)
                        {
                            detail.YA1 = (from x in yearDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA1 = lastDetail.YA1 + detail.A1;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA1 = (from x in projectDetails where x.CostType == "A1" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA1 = lastDetail.PA1 + detail.A1;
                        }
                        detail.A2 = (from x in details where x.CostType == "A2" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YA2 = (from x in yearDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA2 = lastDetail.YA2 + detail.A2;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA2 = (from x in projectDetails where x.CostType == "A2" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA2 = lastDetail.PA2 + detail.A2;
                        }
                        detail.A3 = (from x in details where x.CostType == "A3" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YA3 = (from x in yearDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA3 = lastDetail.YA3 + detail.A3;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA3 = (from x in projectDetails where x.CostType == "A3" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA3 = lastDetail.PA3 + detail.A3;
                        }
                        detail.A4 = (from x in details where x.CostType == "A4" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YA4 = (from x in yearDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA4 = lastDetail.YA4 + detail.A4;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA4 = (from x in projectDetails where x.CostType == "A4" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA4 = lastDetail.PA4 + detail.A4;
                        }
                        detail.A5 = (from x in details where x.CostType == "A5" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YA5 = (from x in yearDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA5 = lastDetail.YA5 + detail.A5;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA5 = (from x in projectDetails where x.CostType == "A5" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA5 = lastDetail.PA5 + detail.A5;
                        }
                        detail.A6 = (from x in details where x.CostType == "A6" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YA6 = (from x in yearDetails where x.CostType == "A6" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YA6 = lastDetail.YA6 + detail.A6;
                        }
                        if (lastDetail == null)
                        {
                            detail.PA6 = (from x in projectDetails where x.CostType == "A6" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PA6 = lastDetail.PA6 + detail.A6;
                        }
                        detail.A = detail.A1 + detail.A2 + detail.A3 + detail.A4 + detail.A5 + detail.A6;
                        detail.YA = detail.YA1 + detail.YA2 + detail.YA3 + detail.YA4 + detail.YA5 + detail.YA6;
                        detail.PA = detail.PA1 + detail.PA2 + detail.PA3 + detail.PA4 + detail.PA5 + detail.PA6;
                        detail.B1 = (from x in details where x.CostType == "B1" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YB1 = (from x in yearDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YB1 = lastDetail.YB1 + detail.B1;
                        }
                        if (lastDetail == null)
                        {
                            detail.PB1 = (from x in projectDetails where x.CostType == "B1" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PB1 = lastDetail.PB1 + detail.B1;
                        }
                        detail.B2 = (from x in details where x.CostType == "B2" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YB2 = (from x in yearDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YB2 = lastDetail.YB2 + detail.B2;
                        }
                        if (lastDetail == null)
                        {
                            detail.PB2 = (from x in projectDetails where x.CostType == "B2" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PB2 = lastDetail.PB2 + detail.B2;
                        }
                        detail.B3 = (from x in details where x.CostType == "B3" select x.CostMoney ?? 0).Sum();
                        if (lastDetail == null)
                        {
                            detail.YB3 = (from x in yearDetails where x.CostType == "B3" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.YB3 = lastDetail.YB3 + detail.B3;
                        }
                        if (lastDetail == null)
                        {
                            detail.PB3 = (from x in projectDetails where x.CostType == "B3" select x.CostMoney ?? 0).Sum();
                        }
                        else
                        {
                            detail.PB3 = lastDetail.PB3 + detail.B3;
                        }
                        detail.B = detail.B1 + detail.B2 + detail.B3;
                        detail.YB = detail.YB1 + detail.YB2 + detail.YB3;
                        detail.PB = detail.PB1 + detail.PB2 + detail.PB3;
                        detail.AB = detail.A + detail.B;
                        detail.YAB = detail.YA + detail.YB;
                        detail.PAB = detail.PA + detail.PB;
                        BLL.CostStatisticDetailService.AddCostStatisticDetail(detail);
                    }
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取培训类型
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertTrainType(object TrainTypeId)
        {
            string name = string.Empty;
            if (TrainTypeId != null)
            {
                string trainTypeId = TrainTypeId.ToString().Trim();
                Model.Base_TrainType trainType = BLL.TrainTypeService.GetTrainTypeById(trainTypeId);
                if (trainType != null)
                {
                    name = trainType.TrainTypeName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取事故类型
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertAccidentType(object AccidentTypeId)
        {
            string name = string.Empty;
            if (AccidentTypeId != null)
            {
                string accidentTypeId = AccidentTypeId.ToString().Trim();
                Model.Base_AccidentType accidentType = BLL.AccidentTypeService.GetAccidentTypeById(accidentTypeId);
                if (accidentType != null)
                {
                    name = accidentType.AccidentTypeName;
                }
            }
            return name;
        }
        #endregion

        #region 保存列表明细
        #region 保存人工时情况统计
        /// <summary>
        /// 保存人工时情况统计
        /// </summary>
        private void SaveManhoursSort()
        {
            BLL.ManhoursSortBService.DeleteManhoursSortsByMonthReportId(this.MonthReportId);
            for (int i = 0; i < this.GridManhoursSort.Rows.Count; i++)
            {
                Model.Manager_ManhoursSortB manhoursSort = new Model.Manager_ManhoursSortB
                {
                    ManhoursSortId = this.GridManhoursSort.Rows[i].DataKeys[0].ToString(),
                    MonthReportId = this.MonthReportId,
                    UnitId = this.GridManhoursSort.Rows[i].DataKeys[1].ToString(),
                    PersonTotal = Funs.GetNewInt(this.GridManhoursSort.Rows[i].Values[2].ToString()),
                    ManhoursTotal = Funs.GetNewInt(this.GridManhoursSort.Rows[i].Values[3].ToString()),
                    TotalManhoursTotal = Funs.GetNewInt(this.GridManhoursSort.Rows[i].Values[4].ToString())
                };
                BLL.ManhoursSortBService.AddManhoursSort(manhoursSort);
            }
        }
        #endregion

        #region 保存事故分类统计
        /// <summary>
        /// 保存事故分类统计
        /// </summary>
        private void SaveAccidentSort()
        {
            BLL.AccidentSortBService.DeleteAccidentSortsByMonthReportId(this.MonthReportId);
            Model.Manager_AccidentSortB accidentSort11 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType11.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber11.Text.Trim()))
            {
                accidentSort11.Number = Convert.ToInt32(this.txtNumber11.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber11.Text.Trim()))
            {
                accidentSort11.TotalNum = Convert.ToInt32(this.txtSumNumber11.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum11.Text.Trim()))
            {
                accidentSort11.PersonNum = Convert.ToInt32(this.txtPersonNum11.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum11.Text.Trim()))
            {
                accidentSort11.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum11.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours11.Text.Trim()))
            {
                accidentSort11.LoseHours = Convert.ToInt32(this.txtLoseHours11.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours11.Text.Trim()))
            {
                accidentSort11.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours11.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney11.Text.Trim()))
            {
                accidentSort11.LoseMoney = Convert.ToInt32(this.txtLoseMoney11.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney11.Text.Trim()))
            {
                accidentSort11.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney11.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort11);

            Model.Manager_AccidentSortB accidentSort12 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType12.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber12.Text.Trim()))
            {
                accidentSort12.Number = Convert.ToInt32(this.txtNumber12.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber12.Text.Trim()))
            {
                accidentSort12.TotalNum = Convert.ToInt32(this.txtSumNumber12.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum12.Text.Trim()))
            {
                accidentSort12.PersonNum = Convert.ToInt32(this.txtPersonNum12.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum12.Text.Trim()))
            {
                accidentSort12.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum12.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours12.Text.Trim()))
            {
                accidentSort12.LoseHours = Convert.ToInt32(this.txtLoseHours12.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours12.Text.Trim()))
            {
                accidentSort12.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours12.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney12.Text.Trim()))
            {
                accidentSort12.LoseMoney = Convert.ToInt32(this.txtLoseMoney12.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney12.Text.Trim()))
            {
                accidentSort12.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney12.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort12);

            Model.Manager_AccidentSortB accidentSort13 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType13.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber13.Text.Trim()))
            {
                accidentSort13.Number = Convert.ToInt32(this.txtNumber13.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber13.Text.Trim()))
            {
                accidentSort13.TotalNum = Convert.ToInt32(this.txtSumNumber13.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum13.Text.Trim()))
            {
                accidentSort13.PersonNum = Convert.ToInt32(this.txtPersonNum13.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum13.Text.Trim()))
            {
                accidentSort13.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum13.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours13.Text.Trim()))
            {
                accidentSort13.LoseHours = Convert.ToInt32(this.txtLoseHours13.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours13.Text.Trim()))
            {
                accidentSort13.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours13.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney13.Text.Trim()))
            {
                accidentSort13.LoseMoney = Convert.ToInt32(this.txtLoseMoney13.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney13.Text.Trim()))
            {
                accidentSort13.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney13.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort13);

            Model.Manager_AccidentSortB accidentSort14 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType14.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber14.Text.Trim()))
            {
                accidentSort14.Number = Convert.ToInt32(this.txtNumber14.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber14.Text.Trim()))
            {
                accidentSort14.TotalNum = Convert.ToInt32(this.txtSumNumber14.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum14.Text.Trim()))
            {
                accidentSort14.PersonNum = Convert.ToInt32(this.txtPersonNum14.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum14.Text.Trim()))
            {
                accidentSort14.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum14.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours14.Text.Trim()))
            {
                accidentSort14.LoseHours = Convert.ToInt32(this.txtLoseHours14.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours14.Text.Trim()))
            {
                accidentSort14.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours14.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney14.Text.Trim()))
            {
                accidentSort14.LoseMoney = Convert.ToInt32(this.txtLoseMoney14.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney14.Text.Trim()))
            {
                accidentSort14.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney14.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort14);

            Model.Manager_AccidentSortB accidentSort15 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType15.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber15.Text.Trim()))
            {
                accidentSort15.Number = Convert.ToInt32(this.txtNumber15.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber15.Text.Trim()))
            {
                accidentSort15.TotalNum = Convert.ToInt32(this.txtSumNumber15.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum15.Text.Trim()))
            {
                accidentSort15.PersonNum = Convert.ToInt32(this.txtPersonNum15.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum15.Text.Trim()))
            {
                accidentSort15.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum15.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours15.Text.Trim()))
            {
                accidentSort15.LoseHours = Convert.ToInt32(this.txtLoseHours15.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours15.Text.Trim()))
            {
                accidentSort15.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours15.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney15.Text.Trim()))
            {
                accidentSort15.LoseMoney = Convert.ToInt32(this.txtLoseMoney15.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney15.Text.Trim()))
            {
                accidentSort15.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney15.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort15);

            Model.Manager_AccidentSortB accidentSort16 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType16.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber16.Text.Trim()))
            {
                accidentSort16.Number = Convert.ToInt32(this.txtNumber16.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber16.Text.Trim()))
            {
                accidentSort16.TotalNum = Convert.ToInt32(this.txtSumNumber16.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum16.Text.Trim()))
            {
                accidentSort16.PersonNum = Convert.ToInt32(this.txtPersonNum16.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum16.Text.Trim()))
            {
                accidentSort16.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum16.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours16.Text.Trim()))
            {
                accidentSort16.LoseHours = Convert.ToInt32(this.txtLoseHours16.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours16.Text.Trim()))
            {
                accidentSort16.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours16.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney16.Text.Trim()))
            {
                accidentSort16.LoseMoney = Convert.ToInt32(this.txtLoseMoney16.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney16.Text.Trim()))
            {
                accidentSort16.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney16.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort16);

            Model.Manager_AccidentSortB accidentSort21 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType21.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber21.Text.Trim()))
            {
                accidentSort21.Number = Convert.ToInt32(this.txtNumber21.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber21.Text.Trim()))
            {
                accidentSort21.TotalNum = Convert.ToInt32(this.txtSumNumber21.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum21.Text.Trim()))
            {
                accidentSort21.PersonNum = Convert.ToInt32(this.txtPersonNum21.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum21.Text.Trim()))
            {
                accidentSort21.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum21.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours21.Text.Trim()))
            {
                accidentSort21.LoseHours = Convert.ToInt32(this.txtLoseHours21.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours21.Text.Trim()))
            {
                accidentSort21.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours21.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney21.Text.Trim()))
            {
                accidentSort21.LoseMoney = Convert.ToInt32(this.txtLoseMoney21.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney21.Text.Trim()))
            {
                accidentSort21.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney21.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort21);

            Model.Manager_AccidentSortB accidentSort22 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType22.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber22.Text.Trim()))
            {
                accidentSort22.Number = Convert.ToInt32(this.txtNumber22.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber22.Text.Trim()))
            {
                accidentSort22.TotalNum = Convert.ToInt32(this.txtSumNumber22.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum22.Text.Trim()))
            {
                accidentSort22.PersonNum = Convert.ToInt32(this.txtPersonNum22.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum22.Text.Trim()))
            {
                accidentSort22.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum22.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours22.Text.Trim()))
            {
                accidentSort22.LoseHours = Convert.ToInt32(this.txtLoseHours22.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours22.Text.Trim()))
            {
                accidentSort22.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours22.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney22.Text.Trim()))
            {
                accidentSort22.LoseMoney = Convert.ToInt32(this.txtLoseMoney22.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney22.Text.Trim()))
            {
                accidentSort22.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney22.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort22);

            Model.Manager_AccidentSortB accidentSort23 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType23.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber23.Text.Trim()))
            {
                accidentSort23.Number = Convert.ToInt32(this.txtNumber23.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber23.Text.Trim()))
            {
                accidentSort23.TotalNum = Convert.ToInt32(this.txtSumNumber23.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum23.Text.Trim()))
            {
                accidentSort23.PersonNum = Convert.ToInt32(this.txtPersonNum23.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum23.Text.Trim()))
            {
                accidentSort23.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum23.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours23.Text.Trim()))
            {
                accidentSort23.LoseHours = Convert.ToInt32(this.txtLoseHours23.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours23.Text.Trim()))
            {
                accidentSort23.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours23.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney23.Text.Trim()))
            {
                accidentSort23.LoseMoney = Convert.ToInt32(this.txtLoseMoney23.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney23.Text.Trim()))
            {
                accidentSort23.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney23.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort23);

            Model.Manager_AccidentSortB accidentSort24 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType24.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber24.Text.Trim()))
            {
                accidentSort24.Number = Convert.ToInt32(this.txtNumber24.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber24.Text.Trim()))
            {
                accidentSort24.TotalNum = Convert.ToInt32(this.txtSumNumber24.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum24.Text.Trim()))
            {
                accidentSort24.PersonNum = Convert.ToInt32(this.txtPersonNum24.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum24.Text.Trim()))
            {
                accidentSort24.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum24.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours24.Text.Trim()))
            {
                accidentSort24.LoseHours = Convert.ToInt32(this.txtLoseHours24.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours24.Text.Trim()))
            {
                accidentSort24.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours24.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney24.Text.Trim()))
            {
                accidentSort24.LoseMoney = Convert.ToInt32(this.txtLoseMoney24.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney24.Text.Trim()))
            {
                accidentSort24.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney24.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort24);

            Model.Manager_AccidentSortB accidentSort25 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType25.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber25.Text.Trim()))
            {
                accidentSort25.Number = Convert.ToInt32(this.txtNumber25.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber25.Text.Trim()))
            {
                accidentSort25.TotalNum = Convert.ToInt32(this.txtSumNumber25.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum25.Text.Trim()))
            {
                accidentSort25.PersonNum = Convert.ToInt32(this.txtPersonNum25.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum25.Text.Trim()))
            {
                accidentSort25.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum25.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours25.Text.Trim()))
            {
                accidentSort25.LoseHours = Convert.ToInt32(this.txtLoseHours25.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours25.Text.Trim()))
            {
                accidentSort25.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours25.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney25.Text.Trim()))
            {
                accidentSort25.LoseMoney = Convert.ToInt32(this.txtLoseMoney25.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney25.Text.Trim()))
            {
                accidentSort25.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney25.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort25);

            Model.Manager_AccidentSortB accidentSort26 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType26.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber26.Text.Trim()))
            {
                accidentSort26.Number = Convert.ToInt32(this.txtNumber26.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber26.Text.Trim()))
            {
                accidentSort26.TotalNum = Convert.ToInt32(this.txtSumNumber26.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum26.Text.Trim()))
            {
                accidentSort26.PersonNum = Convert.ToInt32(this.txtPersonNum26.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum26.Text.Trim()))
            {
                accidentSort26.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum26.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours26.Text.Trim()))
            {
                accidentSort26.LoseHours = Convert.ToInt32(this.txtLoseHours26.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours26.Text.Trim()))
            {
                accidentSort26.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours26.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney26.Text.Trim()))
            {
                accidentSort26.LoseMoney = Convert.ToInt32(this.txtLoseMoney26.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney26.Text.Trim()))
            {
                accidentSort26.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney26.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort26);

            Model.Manager_AccidentSortB accidentSort27 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType27.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber27.Text.Trim()))
            {
                accidentSort27.Number = Convert.ToInt32(this.txtNumber27.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber27.Text.Trim()))
            {
                accidentSort27.TotalNum = Convert.ToInt32(this.txtSumNumber27.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum27.Text.Trim()))
            {
                accidentSort27.PersonNum = Convert.ToInt32(this.txtPersonNum27.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum27.Text.Trim()))
            {
                accidentSort27.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum27.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours27.Text.Trim()))
            {
                accidentSort27.LoseHours = Convert.ToInt32(this.txtLoseHours27.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours27.Text.Trim()))
            {
                accidentSort27.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours27.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney27.Text.Trim()))
            {
                accidentSort27.LoseMoney = Convert.ToInt32(this.txtLoseMoney27.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney27.Text.Trim()))
            {
                accidentSort27.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney27.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort27);

            Model.Manager_AccidentSortB accidentSort28 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType28.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber28.Text.Trim()))
            {
                accidentSort28.Number = Convert.ToInt32(this.txtNumber28.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber28.Text.Trim()))
            {
                accidentSort28.TotalNum = Convert.ToInt32(this.txtSumNumber28.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum28.Text.Trim()))
            {
                accidentSort28.PersonNum = Convert.ToInt32(this.txtPersonNum28.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum28.Text.Trim()))
            {
                accidentSort28.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum28.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours28.Text.Trim()))
            {
                accidentSort28.LoseHours = Convert.ToInt32(this.txtLoseHours28.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours28.Text.Trim()))
            {
                accidentSort28.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours28.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney28.Text.Trim()))
            {
                accidentSort28.LoseMoney = Convert.ToInt32(this.txtLoseMoney28.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney28.Text.Trim()))
            {
                accidentSort28.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney28.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort28);

            Model.Manager_AccidentSortB accidentSort29 = new Model.Manager_AccidentSortB
            {
                MonthReportId = this.MonthReportId,
                AccidentType = this.lblAccidentType29.Text.Trim(),
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtNumber29.Text.Trim()))
            {
                accidentSort29.Number = Convert.ToInt32(this.txtNumber29.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumNumber29.Text.Trim()))
            {
                accidentSort29.TotalNum = Convert.ToInt32(this.txtSumNumber29.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtPersonNum29.Text.Trim()))
            {
                accidentSort29.PersonNum = Convert.ToInt32(this.txtPersonNum29.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumPersonNum29.Text.Trim()))
            {
                accidentSort29.TotalPersonNum = Convert.ToInt32(this.txtSumPersonNum29.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseHours29.Text.Trim()))
            {
                accidentSort29.LoseHours = Convert.ToInt32(this.txtLoseHours29.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseHours29.Text.Trim()))
            {
                accidentSort29.TotalLoseHours = Convert.ToInt32(this.txtSumLoseHours29.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtLoseMoney29.Text.Trim()))
            {
                accidentSort29.LoseMoney = Convert.ToInt32(this.txtLoseMoney29.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumLoseMoney29.Text.Trim()))
            {
                accidentSort29.TotalLoseMoney = Convert.ToInt32(this.txtSumLoseMoney29.Text.Replace(",", "").Trim());
            }
            BLL.AccidentSortBService.AddAccidentSort(accidentSort29);
        }
        #endregion

        #region 保存事故台账统计
        /// <summary>
        /// 保存事故台账统计
        /// </summary>
        private void SaveAccidentDetailSort()
        {
            BLL.AccidentDetailSortBService.DeleteAccidentDetailSortsByMonthReportId(this.MonthReportId);
            for (int i = 0; i < this.GridAccidentDetailSort.Rows.Count; i++)
            {
                Model.Manager_AccidentDetailSortB accidentDetailSort = new Model.Manager_AccidentDetailSortB
                {
                    AccidentDetailSortId = this.GridAccidentDetailSort.Rows[i].DataKeys[0].ToString(),
                    MonthReportId = this.MonthReportId,
                    Abstract = this.GridAccidentDetailSort.Rows[i].Values[4].ToString(),
                    AccidentType = this.GridAccidentDetailSort.Rows[i].Values[5].ToString(),
                    PeopleNum = Funs.GetNewIntOrZero(this.GridAccidentDetailSort.Rows[i].Values[6].ToString()),
                    WorkingHoursLoss = Funs.GetNewDecimalOrZero(this.GridAccidentDetailSort.Rows[i].Values[7].ToString()),
                    EconomicLoss = Funs.GetNewDecimalOrZero(this.GridAccidentDetailSort.Rows[i].Values[8].ToString()),
                    AccidentDate = Funs.GetNewDateTime(this.GridAccidentDetailSort.Rows[i].Values[9].ToString())
                };
                BLL.AccidentDetailSortBService.AddAccidentDetailSort(accidentDetailSort);
            }
        }
        #endregion

        #region 保存费用情况统计
        /// <summary>
        /// 保存费用情况统计
        /// </summary>
        private void SaveHseCostSort()
        {
            BLL.HseCostBService.DeleteHseCostsByMonthReportId(this.MonthReportId);
            for (int i = 0; i < this.GridHSECostSort.Rows.Count; i++)
            {
                Model.Manager_HseCostB hseCostSort = new Model.Manager_HseCostB
                {
                    HseCostId = this.GridHSECostSort.Rows[i].DataKeys[0].ToString(),
                    MonthReportId = this.MonthReportId,
                    UnitId = this.GridHSECostSort.Rows[i].DataKeys[1].ToString(),
                    PlanCostA = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[2].ToString()),
                    PlanCostB = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[3].ToString()),
                    RealCostA = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[6].ToString()),
                    ProjectRealCostA = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[7].ToString()),
                    RealCostB = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[9].ToString()),
                    ProjectRealCostB = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[10].ToString()),
                    RealCostAB = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[12].ToString()),
                    ProjectRealCostAB = Funs.GetNewDecimalOrZero(this.GridHSECostSort.Rows[i].Values[13].ToString())
                };
                BLL.HseCostBService.AddHseCost(hseCostSort);
            }
        }
        #endregion

        #region 保存教育与培训情况统计
        /// <summary>
        /// 保存教育与培训情况统计
        /// </summary>
        private void SaveTrainSort()
        {
            BLL.TrainSortBService.DeleteTrainSortsByMonthReportId(this.MonthReportId);
            for (int i = 0; i < this.GridTrainSort.Rows.Count; i++)
            {
                Model.Manager_TrainSortB trainSort = new Model.Manager_TrainSortB
                {
                    TrainSortId = this.GridTrainSort.Rows[i].DataKeys[0].ToString(),
                    MonthReportId = this.MonthReportId,
                    TrainType = this.GridTrainSort.Rows[i].Values[0].ToString(),
                    TrainNumber = Funs.GetNewInt(this.GridTrainSort.Rows[i].Values[1].ToString()),
                    TotalTrainNum = Funs.GetNewInt(this.GridTrainSort.Rows[i].Values[2].ToString()),
                    TrainPersonNumber = Funs.GetNewInt(this.GridTrainSort.Rows[i].Values[3].ToString()),
                    TotalTrainPersonNum = Funs.GetNewInt(this.GridTrainSort.Rows[i].Values[4].ToString())
                };
                BLL.TrainSortBService.AddTrainSort(trainSort);
            }
        }
        #endregion

        #region 保存会议情况统计
        /// <summary>
        /// 保存会议情况统计
        /// </summary>
        private void SaveMeetingSort()
        {
            BLL.MeetingSortBService.DeleteMeetingSortsByMonthReportId(this.MonthReportId);
            Model.Manager_MeetingSortB newMeetingSort1 = new Model.Manager_MeetingSortB
            {
                MonthReportId = this.MonthReportId,
                MeetingType = this.lblMeetingType1.Text.Trim(),
                MeetingNumber = Funs.GetNewInt(this.txtMeetingNumber1.Text),
                TotalMeetingNum = Funs.GetNewInt(this.txtSumMeetingNumber1.Text),
                MeetingPersonNumber = Funs.GetNewInt(this.txtMeetingPersonNumber1.Text),
                TotalMeetingPersonNum = Funs.GetNewInt(this.txtSumMeetingPersonNumber1.Text)
            };
            BLL.MeetingSortBService.AddMeetingSort(newMeetingSort1);

            Model.Manager_MeetingSortB newMeetingSort2 = new Model.Manager_MeetingSortB
            {
                MonthReportId = this.MonthReportId,
                MeetingType = this.lblMeetingType2.Text.Trim(),
                MeetingNumber = Funs.GetNewInt(this.txtMeetingNumber2.Text),
                TotalMeetingNum = Funs.GetNewInt(this.txtSumMeetingNumber2.Text),
                MeetingPersonNumber = Funs.GetNewInt(this.txtMeetingPersonNumber2.Text),
                TotalMeetingPersonNum = Funs.GetNewInt(this.txtSumMeetingPersonNumber2.Text)
            };
            BLL.MeetingSortBService.AddMeetingSort(newMeetingSort2);

            Model.Manager_MeetingSortB newMeetingSort3 = new Model.Manager_MeetingSortB
            {
                MonthReportId = this.MonthReportId,
                MeetingType = this.lblMeetingType3.Text.Trim(),
                MeetingNumber = Funs.GetNewInt(this.txtMeetingNumber3.Text),
                TotalMeetingNum = Funs.GetNewInt(this.txtSumMeetingNumber3.Text),
                MeetingPersonNumber = Funs.GetNewInt(this.txtMeetingPersonNumber3.Text),
                TotalMeetingPersonNum = Funs.GetNewInt(this.txtSumMeetingPersonNumber3.Text)
            };
            BLL.MeetingSortBService.AddMeetingSort(newMeetingSort3);
        }
        #endregion

        #region 保存HSE检查情况统计
        /// <summary>
        /// 保存HSE检查情况统计
        /// </summary>
        private void SaveCheckSort()
        {
            BLL.CheckSortBService.DeleteCheckSortsByMonthReportId(this.MonthReportId);
            Model.Manager_CheckSortB newCheckSort1 = new Model.Manager_CheckSortB
            {
                MonthReportId = this.MonthReportId,
                CheckType = this.lblCheckType1.Text,
                CheckNumber = Funs.GetNewInt(this.txtCheckNumber1.Text),
                TotalCheckNum = Funs.GetNewInt(this.txtSumCheckNumber1.Text),
                ViolationNumber = Funs.GetNewInt(this.txtViolationNumber1.Text),
                TotalViolationNum = Funs.GetNewInt(this.txtSumViolationNumber1.Text)
            };
            BLL.CheckSortBService.AddCheckSort(newCheckSort1);

            Model.Manager_CheckSortB newCheckSort2 = new Model.Manager_CheckSortB
            {
                MonthReportId = this.MonthReportId,
                CheckType = this.lblCheckType2.Text,
                CheckNumber = Funs.GetNewInt(this.txtCheckNumber2.Text),
                TotalCheckNum = Funs.GetNewInt(this.txtSumCheckNumber2.Text),
                ViolationNumber = Funs.GetNewInt(this.txtViolationNumber2.Text),
                TotalViolationNum = Funs.GetNewInt(this.txtSumViolationNumber2.Text)
            };
            BLL.CheckSortBService.AddCheckSort(newCheckSort2);

            Model.Manager_CheckSortB newCheckSort3 = new Model.Manager_CheckSortB
            {
                MonthReportId = this.MonthReportId,
                CheckType = this.lblCheckType3.Text,
                CheckNumber = Funs.GetNewInt(this.txtCheckNumber3.Text),
                TotalCheckNum = Funs.GetNewInt(this.txtSumCheckNumber3.Text),
                ViolationNumber = Funs.GetNewInt(this.txtViolationNumber3.Text),
                TotalViolationNum = Funs.GetNewInt(this.txtSumViolationNumber3.Text)
            };
            BLL.CheckSortBService.AddCheckSort(newCheckSort3);

            Model.Manager_CheckSortB newCheckSort4 = new Model.Manager_CheckSortB
            {
                MonthReportId = this.MonthReportId,
                CheckType = this.lblCheckType4.Text,
                CheckNumber = Funs.GetNewInt(this.txtCheckNumber4.Text),
                TotalCheckNum = Funs.GetNewInt(this.txtSumCheckNumber4.Text),
                ViolationNumber = Funs.GetNewInt(this.txtViolationNumber4.Text),
                TotalViolationNum = Funs.GetNewInt(this.txtSumViolationNumber4.Text)
            };
            BLL.CheckSortBService.AddCheckSort(newCheckSort4);
        }
        #endregion

        #region 保存安全奖惩情况统计
        /// <summary>
        /// 保存安全奖惩情况统计
        /// </summary>
        private void SaveIncentiveSort()
        {
            BLL.IncentiveSortBService.DeleteIncentiveSortsByMonthReportId(this.MonthReportId);
            Model.Manager_IncentiveSortB newIncentiveSort1 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType11.Text.Trim(),
                BigType = "1",
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveMoney1.Text.Trim()))
            {
                newIncentiveSort1.IncentiveMoney = Convert.ToDecimal(this.txtIncentiveMoney1.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveMoney1.Text.Trim()))
            {
                newIncentiveSort1.TotalIncentiveMoney = Convert.ToDecimal(this.txtSumIncentiveMoney1.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort1);

            Model.Manager_IncentiveSortB newIncentiveSort2 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType12.Text.Trim(),
                BigType = "1",
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveMoney2.Text.Trim()))
            {
                newIncentiveSort2.IncentiveMoney = Convert.ToDecimal(this.txtIncentiveMoney2.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveMoney2.Text.Trim()))
            {
                newIncentiveSort2.TotalIncentiveMoney = Convert.ToDecimal(this.txtSumIncentiveMoney2.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort2);

            Model.Manager_IncentiveSortB newIncentiveSort3 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType13.Text.Trim(),
                BigType = "1",
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveMoney3.Text.Trim()))
            {
                newIncentiveSort3.IncentiveMoney = Convert.ToDecimal(this.txtIncentiveMoney3.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveMoney3.Text.Trim()))
            {
                newIncentiveSort3.TotalIncentiveMoney = Convert.ToDecimal(this.txtSumIncentiveMoney3.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort3);

            Model.Manager_IncentiveSortB newIncentiveSort4 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType14.Text.Trim(),
                BigType = "1",
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveMoney4.Text.Trim()))
            {
                newIncentiveSort4.IncentiveMoney = Convert.ToDecimal(this.txtIncentiveMoney4.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveMoney4.Text.Trim()))
            {
                newIncentiveSort4.TotalIncentiveMoney = Convert.ToDecimal(this.txtSumIncentiveMoney4.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort4);

            Model.Manager_IncentiveSortB newIncentiveSort5 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType15.Text.Trim(),
                BigType = "2",
                TypeFlag = "1"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveMoney5.Text.Trim()))
            {
                newIncentiveSort5.IncentiveMoney = Convert.ToDecimal(this.txtIncentiveMoney5.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveMoney5.Text.Trim()))
            {
                newIncentiveSort5.TotalIncentiveMoney = Convert.ToDecimal(this.txtSumIncentiveMoney5.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort5);

            Model.Manager_IncentiveSortB newIncentiveSort6 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType21.Text.Trim(),
                BigType = "2",
                TypeFlag = "2"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveNumber1.Text.Trim()))
            {
                newIncentiveSort6.IncentiveNumber = Funs.GetNewIntOrZero(this.txtIncentiveNumber1.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveNumber1.Text.Trim()))
            {
                newIncentiveSort6.TotalIncentiveNumber = Funs.GetNewIntOrZero(this.txtSumIncentiveNumber1.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort6);

            Model.Manager_IncentiveSortB newIncentiveSort7 = new Model.Manager_IncentiveSortB
            {
                MonthReportId = this.MonthReportId,
                IncentiveType = this.lblIncentiveType22.Text.Trim(),
                BigType = "2",
                TypeFlag = "2"
            };
            if (!string.IsNullOrEmpty(this.txtIncentiveNumber2.Text.Trim()))
            {
                newIncentiveSort7.IncentiveNumber = Funs.GetNewIntOrZero(this.txtIncentiveNumber2.Text.Replace(",", "").Trim());
            }
            if (!string.IsNullOrEmpty(this.txtSumIncentiveNumber2.Text.Trim()))
            {
                newIncentiveSort7.TotalIncentiveNumber = Funs.GetNewIntOrZero(this.txtSumIncentiveNumber2.Text.Replace(",", "").Trim());
            }
            BLL.IncentiveSortBService.AddIncentiveSort(newIncentiveSort7);
        }
        #endregion
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
                // 冻结时间
                var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
                if (sysSet != null)
                {
                    int freezeDay = !string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 5;
                    Model.Manager_MonthReportB lastMonthReport = BLL.MonthReportBService.GetLastMonthReportByDate(DateTime.Now, freezeDay, this.CurrUser.LoginProjectId);
                    if (lastMonthReport != null)
                    {
                        DateTime months = Convert.ToDateTime(lastMonthReport.Months);
                        string date1 = months.Date.Year.ToString() + "-" + months.Date.Month.ToString();
                        string date2 = this.txtMonths.Text.Trim();
                        if (date1 == date2)
                        {
                            Alert.ShowInTop("该月份月报已经存在，不能重复添加！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                Model.Manager_MonthReportB monthReport = new Model.Manager_MonthReportB
                {
                    MonthReportCode = this.txtMonthReportCode.Text,
                    ProjectId = this.ProjectId,
                    Months = Funs.GetNewDateTime(this.txtMonths.Text + "-1"),
                    MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    ReportMan = this.CurrUser.UserId,
                    MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport))
                };
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportBService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthBMenuId, BLL.Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ProjectManagerMonthBMenuId)));
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
            string unitName = string.Empty;
            if (unitId != null)
            {
                Model.Project_ProjectUnit projectUnit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, unitId.ToString());
                if (projectUnit.OutTime != null && projectUnit.OutTime.Value <= DateTime.Now)   //离场单位
                {
                    unitName = BLL.UnitService.GetUnitNameByUnitId(unitId.ToString()) + "(退场)";
                }
                else      //在场单位
                {
                    unitName = BLL.UnitService.GetUnitNameByUnitId(unitId.ToString());
                }
            }
            return unitName;
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
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(monthReportId.ToString());
                if (project != null)
                {
                    return project.ProjectCode;
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
                return BLL.ProjectService.GetProjectNameByProjectId(monthReportId.ToString());
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
                ///项目经理
                var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == monthReportId.ToString() && x.RoleId == BLL.Const.ProjectManager);
                if (m != null)
                {
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                    if (user != null)
                    {
                        return user.UserName;
                    }
                }
            }
            return "";
        }
        #endregion
    }
}