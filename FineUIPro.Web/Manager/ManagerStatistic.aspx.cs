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
    public partial class ManagerStatistic : PageBase
    {
        #region 定义项
        private static DateTime startTime;

        private static DateTime endTime;
        #endregion

        #region
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
        }
        #endregion

        #region
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnStatistic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) || string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                Alert.ShowInTop("请选择开始日期和结束日期！", MessageBoxIcon.Warning);
                return;
            }
            startTime = Convert.ToDateTime(this.txtStartTime.Text.Trim());
            endTime = Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1);
            if (startTime > endTime)
            {
                Alert.ShowInTop("开始日期不能大于结束日期！", MessageBoxIcon.Warning);
                return;
            }
            //1.项目信息
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            if (project != null)
            {
                this.txtProjectCode.Text = project.ProjectCode;
                this.txtProjectName.Text = project.ProjectName;
                ///项目经理
                var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.RoleId.Contains(BLL.Const.ProjectManager));
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

            this.txtNoStartDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
            this.txtNoEndDate.Text = this.txtEndTime.Text.Trim();

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
            this.txtHseManhours.Text = this.txtManhours.Text.Trim();
            this.txtSafetyManhours.Text = this.txtHseManhours.Text.Trim();
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
                }
                else
                {
                    if (startTime >= newTime)
                    {
                        this.txtHseManhours.Text = this.txtManhours.Text.Trim();
                    }
                    else
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
                    }
                }
            }
            else
            {
                this.txtHseManhours.Text = this.txtManhours.Text.Trim();
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
                this.txtSafetyManhours.Text = this.txtHseManhours.Text.Trim();
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

            GetMonth();
        }
        #endregion

        #region 获取各模块明细数据
        /// <summary>
        /// 获取各模块明细数据
        /// </summary>
        private void GetMonth()
        {
            GetManhoursSort();
            GetAccidentSort();
            GetSolution();
            GetHseCost();
            GetTrainSort();
            GetMeetingSort();
            GetCheckSort();
            GetIncentiveSort();
        }
        #endregion

        #region 明细显示统计
        #region 3.项目施工现场人工时分类统计
        /// <summary>
        ///  3.项目施工现场人工时分类统计
        /// </summary>
        private void GetManhoursSort()
        {
            int sumPersonTotal = 0;
            int sumPersonWorkTimeTotal = 0;
            List<Model.Manager_ManhoursSortB> manhoursSortBs = new List<Model.Manager_ManhoursSortB>();
            //获取当期人工时日报
            List<Model.SitePerson_DayReport> dayReports = BLL.SitePerson_DayReportService.GetDayReportsByCompileDate(startTime, endTime, this.CurrUser.LoginProjectId);
            //获取单位集合
            var unitIds = (from x in Funs.DB.Project_ProjectUnit
                           where x.ProjectId == this.CurrUser.LoginProjectId
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
                if (dayReports.Count > 0)
                {
                    decimal persontotal = Convert.ToDecimal(Math.Round(personNum / dayReports.Count, 2));
                    if (persontotal.ToString().IndexOf(".") > 0)
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
                //累计值
                sumPersonTotal += manhoursSort.PersonTotal ?? 0;
                sumPersonWorkTimeTotal += manhoursSort.ManhoursTotal ?? 0;
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
            this.txtNumber11.Text = "0";
            this.txtNumber11.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum11.Text = "0";
            this.txtPersonNum11.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours11.Text = "0";
            this.txtLoseHours11.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney11.Text = "0";
            this.txtLoseMoney11.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType11, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType12 = "2";   //重伤事故
            this.txtNumber12.Text = "0";
            this.txtNumber12.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum12.Text = "0";
            this.txtPersonNum12.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours12.Text = "0";
            this.txtLoseHours12.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney12.Text = "0";
            this.txtLoseMoney12.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType12, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType13 = "3";  //轻伤事故
            this.txtNumber13.Text = "0";
            this.txtNumber13.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum13.Text = "0";
            this.txtPersonNum13.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours13.Text = "0";
            this.txtLoseHours13.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney13.Text = "0";
            this.txtLoseMoney13.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType13, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType14 = "1";   //工作受限事故
            this.txtNumber14.Text = "0";
            this.txtNumber14.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum14.Text = "0";
            this.txtPersonNum14.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours14.Text = "0";
            this.txtLoseHours14.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney14.Text = "0";
            this.txtLoseMoney14.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType14, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType15 = "2";  //医疗处理
            this.txtNumber15.Text = "0";
            this.txtNumber15.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum15.Text = "0";
            this.txtPersonNum15.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours15.Text = "0";
            this.txtLoseHours15.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney15.Text = "0";
            this.txtLoseMoney15.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType15, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType16 = "3";   //现场处置（急救）
            this.txtNumber16.Text = "0";
            this.txtNumber16.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum16.Text = "0";
            this.txtPersonNum16.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours16.Text = "0";
            this.txtLoseHours16.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney16.Text = "0";
            this.txtLoseMoney16.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType16, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType21 = "4";  //未遂事故
            this.txtNumber21.Text = "0";
            this.txtNumber21.Text = BLL.AccidentReportOtherService.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum21.Text = "0";
            this.txtPersonNum21.Text = BLL.AccidentReportOtherService.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours21.Text = "0";
            this.txtLoseHours21.Text = BLL.AccidentReportOtherService.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney21.Text = "0";
            this.txtLoseMoney21.Text = BLL.AccidentReportOtherService.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType21, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType22 = "4";  //火灾事故
            this.txtNumber22.Text = "0";
            this.txtNumber22.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum22.Text = "0";
            this.txtPersonNum22.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours22.Text = "0";
            this.txtLoseHours22.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney22.Text = "0";
            this.txtLoseMoney22.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType22, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType23 = "5";   //爆炸事故
            this.txtNumber23.Text = "0";
            this.txtNumber23.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum23.Text = "0";
            this.txtPersonNum23.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours23.Text = "0";
            this.txtLoseHours23.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney23.Text = "0";
            this.txtLoseMoney23.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType23, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType24 = "6";   //道路交通事故
            this.txtNumber24.Text = "0";
            this.txtNumber24.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum24.Text = "0";
            this.txtPersonNum24.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours24.Text = "0";
            this.txtLoseHours24.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney24.Text = "0";
            this.txtLoseMoney24.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType24, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType25 = "7";  //机械设备事故
            this.txtNumber25.Text = "0";
            this.txtNumber25.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum25.Text = "0";
            this.txtPersonNum25.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours25.Text = "0";
            this.txtLoseHours25.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney25.Text = "0";
            this.txtLoseMoney25.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType25, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType26 = "8";  //环境污染事故
            this.txtNumber26.Text = "0";
            this.txtNumber26.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum26.Text = "0";
            this.txtPersonNum26.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours26.Text = "0";
            this.txtLoseHours26.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney26.Text = "0";
            this.txtLoseMoney26.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType26, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType27 = "9";  //职业病
            this.txtNumber27.Text = "0";
            this.txtNumber27.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum27.Text = "0";
            this.txtPersonNum27.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours27.Text = "0";
            this.txtLoseHours27.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney27.Text = "0";
            this.txtLoseMoney27.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType27, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType28 = "10";  //生产事故
            this.txtNumber28.Text = "0";
            this.txtNumber28.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum28.Text = "0";
            this.txtPersonNum28.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours28.Text = "0";
            this.txtLoseHours28.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney28.Text = "0";
            this.txtLoseMoney28.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType28, this.CurrUser.LoginProjectId).ToString("N0");

            string accidentType29 = "11";   //其它事故
            this.txtNumber29.Text = "0";
            this.txtNumber29.Text = BLL.AccidentReport2Service.GetCountByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString();
            this.txtPersonNum29.Text = "0";
            this.txtPersonNum29.Text = BLL.AccidentReport2Service.GetPersonNumByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString();
            this.txtLoseHours29.Text = "0";
            this.txtLoseHours29.Text = BLL.AccidentReport2Service.GetSumLoseWorkTimeByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString("N0");
            this.txtLoseMoney29.Text = "0";
            this.txtLoseMoney29.Text = BLL.AccidentReport2Service.GetSumLosMoneyByAccidentTimeAndAccidentType(startTime, endTime, accidentType29, this.CurrUser.LoginProjectId).ToString("N0");

            //事故数据
            List<Model.Accident_AccidentReport> accidentReports = BLL.AccidentReport2Service.GetAccidentReportsByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReportOther> accidentReportOthers = BLL.AccidentReportOtherService.GetAccidentReportOthersByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReport> recordAccidentReports = BLL.AccidentReport2Service.GetRecordAccidentReportsByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            List<Model.Accident_AccidentReportOther> recordAccidentReportOthers = BLL.AccidentReportOtherService.GetRecordAccidentReportOthersByAccidentTime(startTime, endTime, this.CurrUser.LoginProjectId);
            int countA = recordAccidentReports.Count + recordAccidentReportOthers.Count;
            this.txtAccidentNum.Text = countA.ToString();
            int sumManhours = Convert.ToInt32(this.txtManhours.Text.Replace(",", "").Trim());
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
        }
        #endregion

        #region 6.危大工程施工方案数量统计
        /// <summary>
        ///  6.危大工程施工方案数量统计
        /// </summary>
        private void GetSolution()
        {
            this.txtIsArgumentLargerHazardNun.Text = (from x in Funs.DB.Solution_LargerHazard where x.ProjectId == this.CurrUser.LoginProjectId && x.IsArgument == true && x.RecordTime >= startTime && x.RecordTime < endTime && x.States == BLL.Const.State_2 select x).Count().ToString();
            this.txtLargerHazardNum.Text = (from x in Funs.DB.Solution_LargerHazard where x.ProjectId == this.CurrUser.LoginProjectId && x.RecordTime >= startTime && x.RecordTime < endTime && x.States == BLL.Const.State_2 select x).Count().ToString();
        }
        #endregion

        #region 7.项目安全生产及文明施工措施费统计汇总表
        /// <summary>
        ///  7.项目安全生产及文明施工措施费统计汇总表
        /// </summary>
        private void GetHseCost()
        {
            decimal totalPlanCostA = 0, totalPlanCostB = 0, totalRealCostA = 0, totalRealCostB = 0, totalRealCostAB = 0;
            List<Model.Manager_HseCostB> hseCostBs = new List<Model.Manager_HseCostB>();
            List<string> unitIds = (from x in Funs.DB.Project_ProjectUnit
                                    where x.ProjectId == this.CurrUser.LoginProjectId
                                    select x.UnitId).ToList();
            foreach (var unitId in unitIds)
            {
                Model.Manager_HseCostB hseCost = new Model.Manager_HseCostB
                {
                    HseCostId = SQLHelper.GetNewID(typeof(Model.Manager_HseCostB)),
                    UnitId = unitId
                };
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
                hseCost.RealCostB = BLL.CostSmallDetailItemService.GetCostDetailsByUnitIdAndCostType(this.CurrUser.LoginProjectId, unitId, startTime, endTime, "B");
                hseCost.RealCostAB = hseCost.RealCostA + hseCost.RealCostB;
                hseCostBs.Add(hseCost);
                totalPlanCostA += hseCost.PlanCostA ?? 0;
                totalPlanCostB += hseCost.PlanCostB ?? 0;
                totalRealCostA += hseCost.RealCostA ?? 0;
                totalRealCostB += hseCost.RealCostB ?? 0;
                totalRealCostAB += hseCost.RealCostAB ?? 0;
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
                summary.Add("RealCostB", totalRealCostB);
                summary.Add("RealCostAB", totalRealCostAB);
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
            int sumTrainPersonNumber = 0;
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
                trainSort.TrainPersonNumber = (from x in trainings
                                               join y in Funs.DB.EduTrain_TrainRecordDetail
                                               on x.TrainingId equals y.TrainingId
                                               select y).Count();
                trainSorts.Add(trainSort);
                sumTrainNumber += trainSort.TrainNumber ?? 0;
                sumTrainPersonNumber += trainSort.TrainPersonNumber ?? 0;
            }
            this.GridTrainSort.DataSource = trainSorts;
            this.GridTrainSort.DataBind();
            if (this.GridTrainSort.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("TrainType", "合计：");
                summary.Add("TrainNumber", sumTrainNumber);
                summary.Add("TrainPersonNumber", sumTrainPersonNumber);
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
            this.txtMeetingNumber1.Text = BLL.WeekMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtMeetingPersonNumber1.Text = BLL.WeekMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();

            this.txtMeetingNumber2.Text = BLL.MonthMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtMeetingPersonNumber2.Text = BLL.MonthMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();

            this.txtMeetingNumber3.Text = BLL.SpecialMeetingService.GetCountByTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            this.txtMeetingPersonNumber3.Text = BLL.SpecialMeetingService.GetSumAttentPersonNumByMeetingDate(startTime, endTime, this.CurrUser.LoginProjectId).ToString();

            this.txtAllMeetingNumber.Text = (Convert.ToInt32(this.txtMeetingNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingNumber3.Text.Trim())).ToString();
            this.txtAllMeetingPersonNumber.Text = (Convert.ToInt32(this.txtMeetingPersonNumber1.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber2.Text.Trim()) + Convert.ToInt32(this.txtMeetingPersonNumber3.Text.Trim())).ToString();
        }
        #endregion

        #region 10.项目施工现场HSE检查情况统计
        /// <summary>
        ///  10.项目施工现场HSE检查情况统计
        /// </summary>
        private void GetCheckSort()
        {
            this.txtCheckNumber1.Text = BLL.Check_CheckDayService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            int totalViolationNumber1 = (from x in Funs.DB.Check_CheckDay
                                         join y in Funs.DB.Check_CheckDayDetail
                                         on x.CheckDayId equals y.CheckDayId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2
                                         select y).Count();
            this.txtViolationNumber1.Text = totalViolationNumber1.ToString();

            this.txtCheckNumber2.Text = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "0").ToString();
            int totalViolationNumber2 = (from x in Funs.DB.Check_CheckColligation
                                         join y in Funs.DB.Check_CheckColligationDetail
                                         on x.CheckColligationId equals y.CheckColligationId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 && x.CheckType == "0" 
                                         select y).Count();
            this.txtViolationNumber2.Text = totalViolationNumber2.ToString();

            this.txtCheckNumber3.Text = BLL.Check_CheckColligationService.GetCountByCheckTimeAndCheckType(startTime, endTime, this.CurrUser.LoginProjectId, "1").ToString();
            int totalViolationNumber3 = (from x in Funs.DB.Check_CheckColligation
                                         join y in Funs.DB.Check_CheckColligationDetail
                                         on x.CheckColligationId equals y.CheckColligationId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 && x.CheckType == "1"
                                         select y).Count();
            this.txtViolationNumber3.Text = totalViolationNumber3.ToString();

            this.txtCheckNumber4.Text = BLL.Check_CheckSpecialService.GetCountByCheckTime(startTime, endTime, this.CurrUser.LoginProjectId).ToString();
            int totalViolationNumber4 = (from x in Funs.DB.Check_CheckSpecial
                                         join y in Funs.DB.Check_CheckSpecialDetail
                                         on x.CheckSpecialId equals y.CheckSpecialId
                                         where x.CheckTime >= startTime && x.CheckTime < endTime && x.ProjectId == this.CurrUser.LoginProjectId && x.States == BLL.Const.State_2 
                                         select y).Count();
            this.txtViolationNumber4.Text = totalViolationNumber4.ToString();

            this.txtAllCheckNumber.Text = (Convert.ToInt32(this.txtCheckNumber1.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber2.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber3.Text.Trim()) + Convert.ToInt32(this.txtCheckNumber4.Text.Trim())).ToString();
            this.txtAllViolationNumber.Text = (totalViolationNumber1 + totalViolationNumber2 + totalViolationNumber3 + totalViolationNumber4).ToString();
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

            string incentiveType12 = "2";
            this.txtIncentiveMoney2.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType12, this.CurrUser.LoginProjectId));

            string incentiveType13 = "3";
            this.txtIncentiveMoney3.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType13, this.CurrUser.LoginProjectId));

            string incentiveType14 = "4";
            this.txtIncentiveMoney4.Text = string.Format("{0:N2}", BLL.IncentiveNoticeService.GetSumMoneyByTimeAndType(startTime, endTime, incentiveType14, this.CurrUser.LoginProjectId));

            this.txtIncentiveMoney5.Text = string.Format("{0:N2}", BLL.PunishNoticeService.GetSumMoneyByTime(startTime, endTime, this.CurrUser.LoginProjectId));

            this.txtIncentiveNumber1.Text = "0";
            this.txtIncentiveNumber1.Text = BLL.ViolationPersonService.GetViolationPersonNum(startTime, endTime, "1", this.CurrUser.LoginProjectId).ToString();

            this.txtIncentiveNumber2.Text = "0";
            this.txtIncentiveNumber2.Text = BLL.ViolationPersonService.GetViolationPersonNum(startTime, endTime, "2", this.CurrUser.LoginProjectId).ToString();
        }
        #endregion
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
                var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == monthReportId.ToString() && x.RoleId.Contains(BLL.Const.ProjectManager));
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