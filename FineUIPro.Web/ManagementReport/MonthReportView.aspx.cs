using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ManagementReport
{
    public partial class MonthReportView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 月份
        /// </summary>
        public string ReportMonths
        {
            get
            {
                return (string)ViewState["ReportMonths"];
            }
            set
            {
                ViewState["ReportMonths"] = value;
            }
        }
        #endregion

        //private static DateTime startTime;
        //private static DateTime endTime;

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
                this.ReportMonths = Request.Params["ReportMonths"];
                if (!string.IsNullOrEmpty(this.ReportMonths))
                {
                    DateTime? reportMonths = Funs.GetNewDateTime(this.ReportMonths);
                    this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", reportMonths);
                    List<Model.Manager_MonthReport> monthReports = BLL.MonthReportService.GetMonthReportsByReportMonths(reportMonths);
                    string allProjectData = string.Empty;
                    string allManhoursData = string.Empty;
                    string thisMonthKeyPoints = string.Empty;
                    string thisMonthSafetyActivity = string.Empty;
                    string nextMonthWorkFocus = string.Empty;
                    string equipmentQualityData = string.Empty;
                    decimal thisMonthSafetyCost = 0;
                    decimal totalSafetyCost = 0;
                    foreach (var monthReport in monthReports)
                    {
                        Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(monthReport.ProjectId);
                        allProjectData += project.ProjectName + "：\r\n" + monthReport.AllProjectData + "\r\n";
                        allManhoursData += project.ProjectName + "：\r\n" + monthReport.AllManhoursData + "\r\n";
                        thisMonthKeyPoints += project.ProjectName + "：\r\n" + monthReport.ThisMonthKeyPoints + "\r\n";
                        thisMonthSafetyActivity += project.ProjectName + "：\r\n" + monthReport.ThisMonthSafetyActivity + "\r\n";
                        nextMonthWorkFocus += project.ProjectName + "：\r\n" + monthReport.NextMonthWorkFocus + "\r\n";
                        equipmentQualityData += project.ProjectName + "：\r\n" + monthReport.EquipmentQualityData + "\r\n";
                        thisMonthSafetyCost += monthReport.ThisMonthSafetyCost ?? 0;
                        totalSafetyCost += monthReport.TotalSafetyCost ?? 0;
                    }
                    this.txtAllProjectData.Text = allProjectData;
                    this.txtAllManhoursData.Text = allManhoursData;
                    this.txtThisMonthKeyPoints.Text = thisMonthKeyPoints;
                    this.txtThisMonthSafetyCost.Text = thisMonthSafetyCost.ToString();
                    this.txtTotalSafetyCost.Text = totalSafetyCost.ToString();
                    this.txtThisMonthSafetyActivity.Text = thisMonthSafetyActivity;
                    this.txtNextMonthWorkFocus.Text = nextMonthWorkFocus;
                    this.txtEquipmentQualityData.Text = equipmentQualityData;
                    this.GetInitData(monthReports);
                    OutputSummaryData();
                }
            }
            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    // 页面要求重新计算合计行的值
                    OutputSummaryData();
                }
            }
        }

        #region 合计事故数
        /// <summary>
        /// 合计事故数
        /// </summary>
        private void OutputSummaryData()
        {
            Grid2.CommitChanges();
            decimal monthNum = 0;
            decimal totalNum = 0;
            for (int i = 0; i < this.Grid2.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.Grid2.Rows[i].Values[1].ToString()))
                {
                    monthNum += Convert.ToInt32(this.Grid2.Rows[i].Values[1].ToString());
                }
                if (!string.IsNullOrEmpty(this.Grid2.Rows[i].Values[2].ToString()))
                {
                    totalNum += Convert.ToInt32(this.Grid2.Rows[i].Values[2].ToString());
                }
            }
            if (this.Grid2.Rows.Count > 1)
            {
                JObject summary = new JObject();
                summary.Add("TrainTypeId", "合计：");
                summary.Add("AccidentNumber01", monthNum);
                summary.Add("AccidentNumber02", totalNum);

                Grid2.SummaryData = summary;
            }
            else
            {
                Grid2.SummaryData = null;
            }
        }
        #endregion

        #region 得到已存数据
        private void GetInitData(List<Model.Manager_MonthReport> monthReports)
        {
            List<string> monthReportIds = monthReports.Select(x => x.MonthReportId).ToList();
            List<Model.Manager_TrainSort> newTrainSorts = new List<Model.Manager_TrainSort>();
            var trainSorts = from x in Funs.DB.Manager_TrainSort where monthReportIds.Contains(x.MonthReportId) select x;
            if (trainSorts.Count() > 0)
            {
                var trainType = trainSorts.Select(x => x.TrainTypeName).Distinct();
                foreach (var item in trainType)
                {
                    Model.Manager_TrainSort trainSort = new Model.Manager_TrainSort
                    {
                        TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort)),
                        TrainTypeName = item,
                        TrainNumber11 = (from x in trainSorts where x.TrainTypeName == item select x).Sum(x => x.TrainNumber11),
                        TrainNumber12 = (from x in trainSorts where x.TrainTypeName == item select x).Sum(x => x.TrainNumber12),
                        TrainNumber13 = (from x in trainSorts where x.TrainTypeName == item select x).Sum(x => x.TrainNumber13),
                        TrainNumber14 = (from x in trainSorts where x.TrainTypeName == item select x).Sum(x => x.TrainNumber14)
                    };
                    newTrainSorts.Add(trainSort);
                }
            }

            this.Grid1.DataSource = newTrainSorts;
            this.Grid1.DataBind();

            var meetingSort = from x in Funs.DB.Manager_MeetingSort where monthReportIds.Contains(x.MonthReportId) select x;
            if (meetingSort != null)
            {
                this.txtMeetingNumber01.Text = meetingSort.Sum(x => x.MeetingNumber01).ToString();
                this.txtMeetingNumber02.Text = meetingSort.Sum(x => x.MeetingNumber02).ToString();
                this.txtMeetingNumber03.Text = meetingSort.Sum(x => x.MeetingNumber03).ToString();
                this.txtMeetingNumber04.Text = meetingSort.Sum(x => x.MeetingNumber04).ToString();
                this.txtMeetingNumber11.Text = meetingSort.Sum(x => x.MeetingNumber11).ToString();
                this.txtMeetingNumber12.Text = meetingSort.Sum(x => x.MeetingNumber12).ToString();
                this.txtMeetingNumber13.Text = meetingSort.Sum(x => x.MeetingNumber13).ToString();
                this.txtMeetingNumber14.Text = meetingSort.Sum(x => x.MeetingNumber14).ToString();
            }

            var checkSort = from x in Funs.DB.Manager_CheckSort where monthReportIds.Contains(x.MonthReportId) select x;
            if (checkSort != null)
            {
                this.txtCheckNumber01.Text = checkSort.Sum(x => x.CheckNumber01).ToString();
                this.txtCheckNumber02.Text = checkSort.Sum(x => x.CheckNumber02).ToString();
                this.txtCheckNumber03.Text = checkSort.Sum(x => x.CheckNumber03).ToString();
                this.txtCheckNumber04.Text = checkSort.Sum(x => x.CheckNumber04).ToString();

                this.txtCheckNumber11.Text = checkSort.Sum(x => x.CheckNumber11).ToString();
                this.txtCheckNumber12.Text = checkSort.Sum(x => x.CheckNumber12).ToString();
                this.txtCheckNumber13.Text = checkSort.Sum(x => x.CheckNumber13).ToString();
                this.txtCheckNumber14.Text = checkSort.Sum(x => x.CheckNumber14).ToString();

                this.txtCheckNumber21.Text = checkSort.Sum(x => x.CheckNumber21).ToString();
                this.txtCheckNumber22.Text = checkSort.Sum(x => x.CheckNumber22).ToString();
                this.txtCheckNumber23.Text = checkSort.Sum(x => x.CheckNumber23).ToString();
                this.txtCheckNumber24.Text = checkSort.Sum(x => x.CheckNumber24).ToString();

                this.txtCheckNumber31.Text = checkSort.Sum(x => x.CheckNumber31).ToString();
                this.txtCheckNumber32.Text = checkSort.Sum(x => x.CheckNumber32).ToString();
                this.txtCheckNumber33.Text = checkSort.Sum(x => x.CheckNumber33).ToString();
                this.txtCheckNumber34.Text = checkSort.Sum(x => x.CheckNumber34).ToString();
            }

            var accidentSorts = from x in Funs.DB.Manager_AccidentSort where monthReportIds.Contains(x.MonthReportId) select x;
            var accidentTypes = BLL.AccidentTypeService.GetAccidentTypeList();
            List<Model.Manager_AccidentSort> newAccidentSorts = new List<Model.Manager_AccidentSort>();
            foreach (var accidentType in accidentTypes)
            {
                Model.Manager_AccidentSort accidentSort = new Model.Manager_AccidentSort
                {
                    AccidentSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort)),
                    AccidentTypeId = accidentType.AccidentTypeId,
                    AccidentNumber01 = (from x in accidentSorts where x.AccidentTypeId == accidentType.AccidentTypeId select x).Sum(x => x.AccidentNumber01),
                    AccidentNumber02 = (from x in accidentSorts where x.AccidentTypeId == accidentType.AccidentTypeId select x).Sum(x => x.AccidentNumber02)
                };
                newAccidentSorts.Add(accidentSort);
            }
            this.Grid2.DataSource = newAccidentSorts;
            this.Grid2.DataBind();

            var IncentiveSort = from x in Funs.DB.Manager_IncentiveSort where monthReportIds.Contains(x.MonthReportId) select x;
            if (IncentiveSort != null)
            {
                this.txtIncentiveNumber01.Text = IncentiveSort.Sum(x => x.IncentiveNumber01).ToString();
                this.txtIncentiveNumber02.Text = IncentiveSort.Sum(x => x.IncentiveNumber02).ToString();
                this.txtIncentiveNumber03.Text = IncentiveSort.Sum(x => x.IncentiveNumber03).ToString();
                this.txtIncentiveNumber04.Text = IncentiveSort.Sum(x => x.IncentiveNumber04).ToString();
                this.txtIncentiveNumber05.Text = IncentiveSort.Sum(x => x.IncentiveNumber05).ToString();
                this.txtIncentiveNumber06.Text = IncentiveSort.Sum(x => x.IncentiveNumber06).ToString();
                this.txtIncentiveNumber07.Text = IncentiveSort.Sum(x => x.IncentiveNumber07).ToString();
                this.txtIncentiveNumber11.Text = IncentiveSort.Sum(x => x.IncentiveNumber11).ToString();
                this.txtIncentiveNumber12.Text = IncentiveSort.Sum(x => x.IncentiveNumber12).ToString();
                this.txtIncentiveNumber13.Text = IncentiveSort.Sum(x => x.IncentiveNumber13).ToString();
                this.txtIncentiveNumber14.Text = IncentiveSort.Sum(x => x.IncentiveNumber14).ToString();
                this.txtIncentiveNumber15.Text = IncentiveSort.Sum(x => x.IncentiveNumber15).ToString();
                this.txtIncentiveNumber16.Text = IncentiveSort.Sum(x => x.IncentiveNumber16).ToString();
                this.txtIncentiveNumber17.Text = IncentiveSort.Sum(x => x.IncentiveNumber17).ToString();
            }

            var HseSort = from x in Funs.DB.Manager_HseCost where monthReportIds.Contains(x.MonthReportId) select x;
            if (HseSort != null)
            {
                this.txtHseNumber01.Text = HseSort.Sum(x => x.HseNumber01).ToString();
                this.txtHseNumber02.Text = HseSort.Sum(x => x.HseNumber02).ToString();
                this.txtHseNumber03.Text = HseSort.Sum(x => x.HseNumber03).ToString();
                this.txtHseNumber04.Text = HseSort.Sum(x => x.HseNumber04).ToString();
                this.txtHseNumber05.Text = HseSort.Sum(x => x.HseNumber05).ToString();
                this.txtHseNumber06.Text = HseSort.Sum(x => x.HseNumber06).ToString();
                this.txtHseNumber07.Text = HseSort.Sum(x => x.HseNumber07).ToString();
                this.txtHseNumber08.Text = HseSort.Sum(x => x.HseNumber08).ToString();
                this.txtHseNumber09.Text = HseSort.Sum(x => x.HseNumber09).ToString();
                this.txtHseNumber00.Text = HseSort.Sum(x => x.HseNumber00).ToString();
                //this.txtHseNumber10.Text = HseSort.HseNumber10.ToString();
                //this.txtHseNumber11.Text = HseSort.HseNumber11.ToString();
                this.txtSpecialNumber.Text = HseSort.Sum(x => x.SpecialNumber).ToString();
            }
        }
        #endregion

        #region 格式化字符串
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
    }
}