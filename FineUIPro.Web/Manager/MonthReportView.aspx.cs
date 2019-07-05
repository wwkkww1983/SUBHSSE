using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportView : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(this.MonthReportId))
                {
                    Model.Manager_MonthReport monthReport = BLL.MonthReportService.GetMonthReportByMonthReportId(this.MonthReportId);
                    if (monthReport != null)
                    {
                        this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                        this.ProjectId = monthReport.ProjectId;
                        //if (monthReport.Months.HasValue)
                        //{
                        //    this.txtYear.Text = monthReport.Months.Value.Year.ToString();
                        //    this.txtMonth.Text = monthReport.Months.Value.Month.ToString();
                        //}
                        if (monthReport.ReportMonths.HasValue)
                        {
                            this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", monthReport.ReportMonths);
                        }
                        if (monthReport.MonthReportDate.HasValue)
                        {
                            this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.MonthReportDate);
                        }

                        if (monthReport.MonthReportStartDate.HasValue)
                        {
                            this.txtMonthReportStartDate.Text = string.Format("{0:yyyy-MM-dd}", monthReport.MonthReportStartDate);
                        }
                        this.txtReportMan.Text = BLL.UserService.GetUserNameByUserId(monthReport.ReportMan);

                        this.txtAllProjectData.Text = monthReport.AllProjectData;
                        this.txtAllManhoursData.Text = monthReport.AllManhoursData;

                        this.txtThisMonthKeyPoints.Text = monthReport.ThisMonthKeyPoints;
                        if (monthReport.ThisMonthSafetyCost != null)
                        {
                            this.txtThisMonthSafetyCost.Text = monthReport.ThisMonthSafetyCost.ToString();
                        }
                        if (monthReport.TotalSafetyCost != null)
                        {
                            this.txtTotalSafetyCost.Text = monthReport.TotalSafetyCost.ToString();
                        }
                        this.txtThisMonthSafetyActivity.Text = monthReport.ThisMonthSafetyActivity;
                        this.txtNextMonthWorkFocus.Text = monthReport.NextMonthWorkFocus;
                        this.txtEquipmentQualityData.Text = monthReport.EquipmentQualityData;

                        ///当前时间
                        endTime = (Funs.GetNewDateTime(this.txtMonthReportDate.Text).HasValue ? Funs.GetNewDateTime(this.txtMonthReportDate.Text).Value : System.DateTime.Now);
                        ///当月第一天
                        startTime = (Funs.GetNewDateTime(this.txtMonthReportStartDate.Text).HasValue ? Funs.GetNewDateTime(this.txtMonthReportStartDate.Text).Value : new DateTime(endTime.Year, endTime.Month, 1));

                        this.GetInitData();
                        OutputSummaryData();
                    }
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
            int monthNum = 0;
            int totalNum = 0;
            for (int i = 0; i < this.Grid2.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.Grid2.Rows[i].Values[1].ToString()))
                {
                    monthNum += Funs.GetNewIntOrZero(this.Grid2.Rows[i].Values[1].ToString());
                }
                if (!string.IsNullOrEmpty(this.Grid2.Rows[i].Values[2].ToString()))
                {
                    totalNum += Funs.GetNewIntOrZero(this.Grid2.Rows[i].Values[2].ToString());
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
        private void GetInitData()
        {
            var trainSorts = from x in Funs.DB.Manager_TrainSort where x.MonthReportId == this.MonthReportId select x;
            this.Grid1.DataSource = trainSorts;
            this.Grid1.DataBind();

            var meetingSort = Funs.DB.Manager_MeetingSort.FirstOrDefault(x => x.MonthReportId == this.MonthReportId);
            if (meetingSort != null)
            {
                this.txtMeetingNumber01.Text = meetingSort.MeetingNumber01.ToString();
                this.txtMeetingNumber02.Text = meetingSort.MeetingNumber02.ToString();
                this.txtMeetingNumber03.Text = meetingSort.MeetingNumber03.ToString();
                this.txtMeetingNumber04.Text = meetingSort.MeetingNumber04.ToString();
                this.txtMeetingNumber11.Text = meetingSort.MeetingNumber11.ToString();
                this.txtMeetingNumber12.Text = meetingSort.MeetingNumber12.ToString();
                this.txtMeetingNumber13.Text = meetingSort.MeetingNumber13.ToString();
                this.txtMeetingNumber14.Text = meetingSort.MeetingNumber14.ToString();
            }

            var checkSort = Funs.DB.Manager_CheckSort.FirstOrDefault(x => x.MonthReportId == this.MonthReportId);
            if (checkSort != null)
            {
                this.txtCheckNumber01.Text = checkSort.CheckNumber01.ToString();
                this.txtCheckNumber02.Text = checkSort.CheckNumber02.ToString();
                this.txtCheckNumber03.Text = checkSort.CheckNumber03.ToString();
                this.txtCheckNumber04.Text = checkSort.CheckNumber04.ToString();

                this.txtCheckNumber11.Text = checkSort.CheckNumber11.ToString();
                this.txtCheckNumber12.Text = checkSort.CheckNumber12.ToString();
                this.txtCheckNumber13.Text = checkSort.CheckNumber13.ToString();
                this.txtCheckNumber14.Text = checkSort.CheckNumber14.ToString();

                this.txtCheckNumber21.Text = checkSort.CheckNumber21.ToString();
                this.txtCheckNumber22.Text = checkSort.CheckNumber22.ToString();
                this.txtCheckNumber23.Text = checkSort.CheckNumber23.ToString();
                this.txtCheckNumber24.Text = checkSort.CheckNumber24.ToString();

                this.txtCheckNumber31.Text = checkSort.CheckNumber31.ToString();
                this.txtCheckNumber32.Text = checkSort.CheckNumber32.ToString();
                this.txtCheckNumber33.Text = checkSort.CheckNumber33.ToString();
                this.txtCheckNumber34.Text = checkSort.CheckNumber34.ToString();
            }

            var accidentSorts = from x in Funs.DB.Manager_AccidentSort where x.MonthReportId == this.MonthReportId select x;
            this.Grid2.DataSource = accidentSorts;
            this.Grid2.DataBind();

            var IncentiveSort = Funs.DB.Manager_IncentiveSort.FirstOrDefault(x => x.MonthReportId == this.MonthReportId);
            if (IncentiveSort != null)
            {
                this.txtIncentiveNumber01.Text = IncentiveSort.IncentiveNumber01.ToString();
                this.txtIncentiveNumber02.Text = IncentiveSort.IncentiveNumber02.ToString();
                this.txtIncentiveNumber03.Text = IncentiveSort.IncentiveNumber03.ToString();
                this.txtIncentiveNumber04.Text = IncentiveSort.IncentiveNumber04.ToString();
                this.txtIncentiveNumber05.Text = IncentiveSort.IncentiveNumber05.ToString();
                this.txtIncentiveNumber06.Text = IncentiveSort.IncentiveNumber06.ToString();
                this.txtIncentiveNumber07.Text = IncentiveSort.IncentiveNumber07.ToString();
                this.txtIncentiveNumber11.Text = IncentiveSort.IncentiveNumber11.ToString();
                this.txtIncentiveNumber12.Text = IncentiveSort.IncentiveNumber12.ToString();
                this.txtIncentiveNumber13.Text = IncentiveSort.IncentiveNumber13.ToString();
                this.txtIncentiveNumber14.Text = IncentiveSort.IncentiveNumber14.ToString();
                this.txtIncentiveNumber15.Text = IncentiveSort.IncentiveNumber15.ToString();
                this.txtIncentiveNumber16.Text = IncentiveSort.IncentiveNumber16.ToString();
                this.txtIncentiveNumber17.Text = IncentiveSort.IncentiveNumber17.ToString();
            }

            var HseSort = Funs.DB.Manager_HseCost.FirstOrDefault(x => x.MonthReportId == this.MonthReportId);
            if (HseSort != null)
            {
                this.txtHseNumber01.Text = HseSort.HseNumber01.ToString();
                this.txtHseNumber02.Text = HseSort.HseNumber02.ToString();
                this.txtHseNumber03.Text = HseSort.HseNumber03.ToString();
                this.txtHseNumber04.Text = HseSort.HseNumber04.ToString();
                this.txtHseNumber05.Text = HseSort.HseNumber05.ToString();
                this.txtHseNumber06.Text = HseSort.HseNumber06.ToString();
                this.txtHseNumber07.Text = HseSort.HseNumber07.ToString();
                this.txtHseNumber08.Text = HseSort.HseNumber08.ToString();
                this.txtHseNumber09.Text = HseSort.HseNumber09.ToString();
                this.txtHseNumber00.Text = HseSort.HseNumber00.ToString();
                this.txtHseNumber10.Text = HseSort.HseNumber10.ToString();
                this.txtHseNumber11.Text = HseSort.HseNumber11.ToString();
                this.txtSpecialNumber.Text = HseSort.SpecialNumber.ToString();
            }
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

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.MonthReportId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}&type=-1", this.MonthReportId, BLL.Const.ProjectManagerMonthMenuId)));
            }
        }
        #endregion
    }
}