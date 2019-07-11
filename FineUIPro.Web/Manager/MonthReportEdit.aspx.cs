using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportEdit : PageBase
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
                else
                {
                    ///当前时间
                    endTime = System.DateTime.Now.AddDays(1 - System.DateTime.Now.Day).AddDays(-1); 
                    ///当月第一天
                    startTime = System.DateTime.Now.AddMonths(-1).AddDays(1 - System.DateTime.Now.AddMonths(-1).Day);

                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtMonthReportStartDate.Text = string.Format("{0:yyyy-MM-dd}", startTime);
                    this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", endTime);
                    this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", startTime);
                    //this.txtYear.Text = System.DateTime.Now.Year.ToString();
                    //this.txtMonth.Text = System.DateTime.Now.Month.ToString();
                    this.txtReportMan.Text = this.CurrUser.UserName;
                    ///  得到项目总体数据统计
                    this.GetAllProjectData();
                    ///  得到项目安全人工时
                    this.GetAllManhoursData();
                    ///  得到本月机具设备投入情况
                    this.GetEquipmentQualityData();
                    ////安全费用
                    this.GetMonthSafetyCostData();
                    ///  得到教育与培训情况统计
                    this.GetTrainSort();
                    ///  得到会议情况统计
                    this.GetMeetingSort();
                    ///  得到HSE检查情况统计
                    this.GetCheckSort();
                    ///  得到事故分类统计
                    this.GetAccidentSort();
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
           // Grid2.CommitChanges();
            int monthNum = 0;
            int totalNum = 0;
            foreach (JObject mergedRow in Grid2.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["AccidentNumber01"] != null)
                {
                    monthNum += values.Value<int>("AccidentNumber01");
                }
                if (values["AccidentNumber02"] != null)
                {
                    totalNum += values.Value<int>("AccidentNumber02");
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

        /// <summary>
        /// 验证月份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtReportMonths_TextChanged(object sender, EventArgs e)
        {
            DateTime? reportMonths = Funs.GetNewDateTime(this.txtReportMonths.Text + "-01");
            if (!reportMonths.HasValue)
            {
                ShowNotify("月报月份不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.MonthReportService.GetMonthReportsByReportMonthsIDProejctID(Funs.GetNewDateTimeOrNow(this.txtReportMonths.Text + "-01"), this.MonthReportId, this.ProjectId) != null)
            {
                ShowNotify("当前月份已存在月报！", MessageBoxIcon.Warning);
                return;
            }

            startTime = reportMonths.Value;
            endTime = reportMonths.Value.AddDays(1 - reportMonths.Value.Day).AddMonths(1).AddDays(-1);
            this.txtMonthReportStartDate.Text = string.Format("{0:yyyy-MM-dd}", startTime);
            this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", endTime);
            this.TextBox_TextChanged(null, null);
        }

        #region 页面时间刷新事件
        /// <summary>
        /// 页面时间刷新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtMonthReportDate.Text) && !string.IsNullOrEmpty(this.txtMonthReportStartDate.Text))
            {
                ///当前时间
                endTime = Funs.GetNewDateTime(this.txtMonthReportDate.Text).Value;
                ///当月
                startTime = Funs.GetNewDateTime(this.txtMonthReportStartDate.Text).Value;

                //this.txtYear.Text = Funs.GetNewDateTime(this.txtMonthReportDate.Text).Value.Year.ToString();
                //this.txtMonth.Text = Funs.GetNewDateTime(this.txtMonthReportDate.Text).Value.Month.ToString();
                this.txtReportMan.Text = this.CurrUser.UserName;
                ///  得到项目总体数据统计
                this.GetAllProjectData();
                ///  得到项目安全人工时
                this.GetAllManhoursData();
                ///  得到本月机具设备投入情况
                this.GetEquipmentQualityData();
                ////安全费用
                this.GetMonthSafetyCostData();
                ///  得到教育与培训情况统计
                this.GetTrainSort();
                ///  得到会议情况统计
                this.GetMeetingSort();
                ///  得到HSE检查情况统计
                this.GetCheckSort();
                ///  得到事故分类统计
                this.GetAccidentSort();

                Alert.ShowInTop("数据刷新成功！", MessageBoxIcon.Success);

            }
            else
            {
                Alert.ShowInTop("日期不能为空！", MessageBoxIcon.Warning);
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

        #region 明细显示统计
        #region 项目总体数据统计
        /// <summary>
        ///  得到项目总体数据统计
        /// </summary>
        private void GetAllProjectData()
        {
            ///总人数集合
            var allPerson = from x in Funs.DB.SitePerson_Person where x.ProjectId == this.ProjectId && x.IsUsed == true && (x.InTime < endTime || !x.InTime.HasValue) select x;
            int allPersonCount = allPerson.Count(); ///总人数

            ///管理人员集合
            var glAllPerson = from x in Funs.DB.SitePerson_Person
                              join y in Funs.DB.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                              where x.ProjectId == this.ProjectId && x.IsUsed == true && (y.PostType == BLL.Const.PostType_1 || y.PostType == BLL.Const.PostType_4) && (x.InTime < endTime || !x.InTime.HasValue)
                              select x;
            int glAllPersonCount = glAllPerson.Count(); ///管理人员人数

            ///安全专职人员集合
            var hsseAllPerson = from x in Funs.DB.SitePerson_Person
                                join y in Funs.DB.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                                where x.ProjectId == this.ProjectId && x.IsUsed == true && y.IsHsse == true && (x.InTime < endTime || !x.InTime.HasValue)
                                select x;
            int hsseAllPersonCout = hsseAllPerson.Count();///安全专职人员人数

            ///单位作业人员集合
            var zyAllPerson = from x in Funs.DB.SitePerson_Person
                              join y in Funs.DB.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                              where x.ProjectId == this.ProjectId && x.IsUsed == true && (y.PostType == BLL.Const.PostType_2 || y.PostType == BLL.Const.PostType_3) && (x.InTime < endTime || !x.InTime.HasValue)
                              select x;
            int zyAllPersonCount = zyAllPerson.Count(); ///单位作业人员人数

            string allProjectData = "截至到" + this.txtMonthReportDate.Text + "，本装置项目员工总人数" + allPersonCount.ToString() + "人，其中管理人员总数" + glAllPersonCount.ToString() + "人。\r\n";

            ////总包单位人员总数
            var mainUnit = from x in Funs.DB.Base_Unit
                           join y in Funs.DB.Project_ProjectUnit
                           on x.UnitId equals y.UnitId
                           where y.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_1
                           select x;     //1为总包
            if (mainUnit.Count() > 0)
            {
                foreach (var item in mainUnit)
                {
                    int mainUnitglCount = glAllPerson.Where(x => x.UnitId == item.UnitId).Count();
                    int mainUnithsseCount = hsseAllPerson.Where(x => x.UnitId == item.UnitId).Count();
                    allProjectData += item.UnitName + "管理人员总数" + mainUnitglCount.ToString() + "人，专职安全人员共" + mainUnithsseCount.ToString() + " 人。\r\n";
                }
            }

            ////分包单位人员总数
            var subUnit = from x in Funs.DB.Base_Unit
                          join y in Funs.DB.Project_ProjectUnit
                          on x.UnitId equals y.UnitId
                          where y.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                          select x;     //2为施工分包
            if (subUnit.Count() > 0)
            {
                foreach (var item in subUnit)
                {
                    int subUnitglCount = glAllPerson.Where(x => x.UnitId == item.UnitId).Count();
                    int subUnithsseCount = hsseAllPerson.Where(x => x.UnitId == item.UnitId).Count();
                    int subUnitzyCount = zyAllPerson.Where(x => x.UnitId == item.UnitId).Count();
                    allProjectData += item.UnitName + "管理人员总数" + subUnitglCount.ToString() + "人，专职安全人员共" + subUnithsseCount.ToString() + " 人，施工单位作业人员总数" + subUnitzyCount.ToString() + "人。\r\n";
                }
            }

            this.txtAllProjectData.Text = allProjectData;
        }
        #endregion

        #region 得到项目安全人工时
        /// <summary>
        ///  得到项目安全人工时
        /// </summary>
        private void GetAllManhoursData()
        {
            var pro = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
            DateTime proStarTime = System.DateTime.Now; ///项目开始时间
            if (pro != null && pro.StartDate.HasValue)
            {
                proStarTime = pro.StartDate.Value;
            }

            ////人工时主表
            decimal totalWorkTime = 0;

            //按月报取数
            var monthReport = from x in Funs.DB.SitePerson_MonthReport
                              where x.ProjectId == this.ProjectId && x.CompileDate >= proStarTime && x.CompileDate < endTime
                              select x;
            foreach (var monthReportItem in monthReport)
            {
                var dayReportDetail = from x in Funs.DB.SitePerson_MonthReportDetail where x.MonthReportId == monthReportItem.MonthReportId select x;
                if (dayReportDetail.Count() > 0)
                {
                    foreach (var dayReportDetailItem in dayReportDetail)
                    {
                        decimal itemTime = dayReportDetailItem.PersonWorkTime.HasValue ? dayReportDetailItem.PersonWorkTime.Value : 0;
                        totalWorkTime += itemTime;
                    }
                }
            }
            //当月
            decimal monthWorkTime = 0;
            DateTime reportMonth = Funs.GetNewDateTime(txtReportMonths.Text + "-01").Value;
            var monthDayReport = from x in monthReport where x.CompileDate.Value.Month == reportMonth.Month && x.CompileDate.Value.Year == reportMonth.Year select x;
            foreach (var monthItem in monthDayReport)
            {
                var monthItemDetail = from x in Funs.DB.SitePerson_MonthReportDetail where x.MonthReportId == monthItem.MonthReportId select x;
                if (monthItemDetail.Count() > 0)
                {
                    foreach (var monthReportDetailItem in monthItemDetail)
                    {
                        decimal itemTime = monthReportDetailItem.PersonWorkTime.HasValue ? monthReportDetailItem.PersonWorkTime.Value : 0;
                        monthWorkTime += itemTime;
                    }
                }
            }

            //按日报取数
            //var dayReport = from x in Funs.DB.SitePerson_DayReport
            //                where x.ProjectId == this.ProjectId && x.CompileDate >= proStarTime && x.CompileDate < endTime
            //                select x;
            //foreach (var dayReportItem in dayReport)
            //{
            //    var dayReportDetail = from x in Funs.DB.SitePerson_DayReportDetail where x.DayReportId == dayReportItem.DayReportId select x;
            //    if (dayReportDetail.Count() > 0)
            //    {
            //        foreach (var dayReportDetailItem in dayReportDetail)
            //        {
            //            decimal itemTime = dayReportDetailItem.PersonWorkTime.HasValue ? dayReportDetailItem.PersonWorkTime.Value : 0;
            //            totalWorkTime += itemTime;
            //        }
            //    }
            //}
            ////当月
            //decimal monthWorkTime = 0;
            //var monthDayReport = from x in dayReport where x.CompileDate >= startTime && x.CompileDate <= endTime select x;
            //foreach (var dayItem in monthDayReport)
            //{
            //    var dayItemDetail = from x in Funs.DB.SitePerson_DayReportDetail where x.DayReportId == dayItem.DayReportId select x;
            //    if (dayItemDetail.Count() > 0)
            //    {
            //        foreach (var dayReportDetailItem in dayItemDetail)
            //        {
            //            decimal itemTime = dayReportDetailItem.PersonWorkTime.HasValue ? dayReportDetailItem.PersonWorkTime.Value : 0;
            //            monthWorkTime += itemTime;
            //        }
            //    }
            //}

            string allManhoursData = "项目自" + proStarTime.ToLongDateString().ToString() + "至" + endTime.ToLongDateString().ToString() + "，累计完成" + totalWorkTime.ToString() + "安全人工时无可记录事故，其中本月完成" + monthWorkTime.ToString() + "安全人工时。";
            this.txtAllManhoursData.Text = allManhoursData;
        }
        #endregion

        #region 得到教育与培训情况统计
        /// <summary>
        ///  得到教育与培训情况统计
        /// </summary>
        private void GetTrainSort()
        {
            var trainTypes = BLL.TrainTypeService.GetTrainTypeList();
            ////总培训
            var totelTraining = from x in Funs.DB.EduTrain_TrainRecord where x.ProjectId == this.ProjectId && (x.TrainStartDate < endTime) select x;
            List<Model.Manager_TrainSort> trainSorts = new List<Model.Manager_TrainSort>();
            foreach (var item in trainTypes)
            {
                var type = BLL.TrainTypeService.GetTrainTypeById(item.TrainTypeId);
                 if (type != null)
                 {
                    Model.Manager_TrainSort trainSort = new Model.Manager_TrainSort
                    {
                        TrainSortId = SQLHelper.GetNewID(typeof(Model.Manager_TrainSort)),
                        TrainTypeName = type.TrainTypeName
                    };
                    //对应培训类型的培训记录集合
                    var training = from x in totelTraining where x.TrainTypeId == item.TrainTypeId select x;
                     //对应培训类型的当月记录集合
                     var monthTraining = from x in training where x.TrainStartDate >= startTime && x.TrainStartDate <= endTime select x;
                     trainSort.TrainNumber11 = monthTraining.Count();
                     trainSort.TrainNumber12 = training.Count();
                     trainSort.TrainNumber13 = (from x in Funs.DB.EduTrain_TrainRecord
                                                join y in Funs.DB.EduTrain_TrainRecordDetail
                                                on x.TrainingId equals y.TrainingId
                                                where x.ProjectId == this.ProjectId && x.TrainTypeId == item.TrainTypeId && x.TrainStartDate >= startTime && x.TrainStartDate <= endTime
                                                select y).Count();
                     trainSort.TrainNumber14 = (from x in Funs.DB.EduTrain_TrainRecord
                                                join y in Funs.DB.EduTrain_TrainRecordDetail
                                           on x.TrainingId equals y.TrainingId
                                                where x.ProjectId == this.ProjectId && x.TrainTypeId == item.TrainTypeId
                                                  && x.TrainStartDate <= endTime
                                                select y).Count();
                     trainSorts.Add(trainSort);
                 }
            }
            this.Grid1.DataSource = trainSorts;
            this.Grid1.DataBind();
        }
        #endregion

        #region 得到会议情况统计
        /// <summary>
        ///  得到会议情况统计
        /// </summary>
        private void GetMeetingSort()
        {
            ////总会议
            var totelWeekMeets = from x in Funs.DB.Meeting_WeekMeeting where x.ProjectId == this.ProjectId && x.WeekMeetingDate < endTime select x;
            var totelMonthMeets = from x in Funs.DB.Meeting_MonthMeeting where x.ProjectId == this.ProjectId && x.MonthMeetingDate < endTime select x;
            var totelSpecialMeets = from x in Funs.DB.Meeting_SpecialMeeting where x.ProjectId == this.ProjectId && x.SpecialMeetingDate < endTime select x;
            ////当前时间会议
            var nowWeekMeets = from x in totelWeekMeets where x.WeekMeetingDate >= startTime && x.WeekMeetingDate < endTime select x;
            var nowMonthMeets = from x in totelMonthMeets where x.MonthMeetingDate >= startTime && x.MonthMeetingDate < endTime select x;
            var nowSpecialMeets = from x in totelSpecialMeets where x.SpecialMeetingDate >= startTime && x.SpecialMeetingDate < endTime select x;

            this.txtMeetingNumber01.Text = nowWeekMeets.Count().ToString();
            this.txtMeetingNumber11.Text = totelWeekMeets.Count().ToString();

            this.txtMeetingNumber02.Text = nowMonthMeets.Count().ToString();
            this.txtMeetingNumber12.Text = totelMonthMeets.Count().ToString();

            this.txtMeetingNumber03.Text = nowSpecialMeets.Count().ToString();
            this.txtMeetingNumber13.Text = totelSpecialMeets.Count().ToString();
        }
        #endregion

        #region 得到HSE检查情况统计
        /// <summary>
        ///  得到HSE检查情况统计
        /// </summary>
        private void GetCheckSort()
        {
            ///日检查累计次数
            var checkDayTolet = from x in Funs.DB.Check_CheckDay where x.ProjectId == this.ProjectId && x.CheckTime < endTime select x;
            ///本月日检查次数
            var checkDayMonth = from x in checkDayTolet where x.CheckTime >= startTime && x.CheckTime < endTime select x;
            ///本月日检查违章数量
            var checkDayMonthCount = from x in Funs.DB.Check_CheckDayDetail
                                     join y in checkDayMonth on x.CheckDayId equals y.CheckDayId
                                     select x;
            ///日检查累计数量
            var checkDayToletCount = from x in Funs.DB.Check_CheckDayDetail
                                     join y in checkDayTolet on x.CheckDayId equals y.CheckDayId
                                     select x;
            this.txtCheckNumber01.Text = checkDayMonth.Count().ToString();
            this.txtCheckNumber02.Text = checkDayTolet.Count().ToString();
            this.txtCheckNumber03.Text = checkDayMonthCount.Count().ToString();
            this.txtCheckNumber04.Text = checkDayToletCount.Count().ToString();

            ///专项查累计次数
            var checkSpecialTolet = from x in Funs.DB.Check_CheckSpecial where x.ProjectId == this.ProjectId && x.CheckTime < endTime select x;
            ///本月专项检查次数
            var checkSpecialMonth = from x in checkSpecialTolet where x.CheckTime >= startTime && x.CheckTime < endTime select x;
            ///本月专项违章数量
            var checkSpecialMonthCount = from x in Funs.DB.Check_CheckSpecialDetail
                                         join y in checkSpecialMonth on x.CheckSpecialId equals y.CheckSpecialId
                                         select x;
            ///专项累计数量
            var checkSpecialToletCount = from x in Funs.DB.Check_CheckSpecialDetail
                                         join y in checkSpecialTolet on x.CheckSpecialId equals y.CheckSpecialId
                                         select x;
            this.txtCheckNumber11.Text = checkSpecialMonth.Count().ToString();
            this.txtCheckNumber12.Text = checkSpecialTolet.Count().ToString();
            this.txtCheckNumber13.Text = checkSpecialMonthCount.Count().ToString();
            this.txtCheckNumber14.Text = checkSpecialToletCount.Count().ToString();

            ///综合大检查累计次数
            var checkColligationTolet = from x in Funs.DB.Check_CheckColligation where x.ProjectId == this.ProjectId && x.CheckTime < endTime select x;
            ///本月综合大检查次数
            var checkColligationMonth = from x in checkColligationTolet where x.CheckTime >= startTime && x.CheckTime < endTime select x;
            ///本月综合大检查违章数量
            var checkColligationMonthCount = from x in Funs.DB.Check_CheckColligationDetail
                                             join y in checkColligationMonth on x.CheckColligationId equals y.CheckColligationId
                                             select x;
            ///综合大检查累计数量
            var checkColligationToletCount = from x in Funs.DB.Check_CheckColligationDetail
                                             join y in checkColligationTolet on x.CheckColligationId equals y.CheckColligationId
                                             select x;
            this.txtCheckNumber21.Text = checkColligationMonth.Count().ToString();
            this.txtCheckNumber22.Text = checkColligationTolet.Count().ToString();
            this.txtCheckNumber23.Text = checkColligationMonthCount.Count().ToString();
            this.txtCheckNumber24.Text = checkColligationToletCount.Count().ToString();
        }
        #endregion

        #region 得到事故分类统计
        /// <summary>
        ///  得到事故分类统计
        /// </summary>
        private void GetAccidentSort()
        {
            var accidentTypes = BLL.AccidentTypeService.GetAccidentTypeList();
            ////总培训
            var totelAccidents = from x in Funs.DB.Accident_AccidentPersonRecord where x.ProjectId == this.ProjectId && x.AccidentDate < endTime select x;
            List<Model.Manager_AccidentSort> accidentSorts = new List<Model.Manager_AccidentSort>();
            foreach (var item in accidentTypes)
            {
                Model.Manager_AccidentSort accidentSort = new Model.Manager_AccidentSort
                {
                    AccidentSortId = SQLHelper.GetNewID(typeof(Model.Manager_AccidentSort)),
                    AccidentTypeId = item.AccidentTypeId
                };
                //对应培训类型的培训记录集合
                var accidents = from x in totelAccidents where x.AccidentTypeId == item.AccidentTypeId select x;
                //对应培训类型的当月记录集合
                var monthAccidents = from x in accidents where x.AccidentDate >= startTime && x.AccidentDate < endTime select x;
                accidentSort.AccidentNumber01 = monthAccidents.Count();
                accidentSort.AccidentNumber02 = accidents.Count();
                accidentSorts.Add(accidentSort);
            }
            this.Grid2.DataSource = accidentSorts;
            this.Grid2.DataBind();
        }
        #endregion

        #region 本月机具设备投入情况
        /// <summary>
        ///  得到本月机具设备投入情况
        /// </summary>
        private void GetEquipmentQualityData()
        {
            ///特种机具设备集合
            var allSpecialEquipment = from x in Funs.DB.QualityAudit_EquipmentQuality
                                      where x.ProjectId == this.ProjectId && ((x.OutDate.HasValue && x.OutDate > endTime) || !x.OutDate.HasValue)
                                      && (!x.InDate.HasValue || (x.InDate < endTime))
                                      select x;
            ///一般机具设备集合
            var allGeneralEquipment = from x in Funs.DB.QualityAudit_GeneralEquipmentQuality
                                      where x.ProjectId == this.ProjectId && x.IsQualified == true && x.CompileDate < endTime
                                      select x;
            if (allSpecialEquipment.Count() > 0)
            {
                int? allCount = allGeneralEquipment.Sum(x => x.EquipmentCount);
                int allEquipmentCount = (allCount.HasValue ? allCount.Value : 0) + allSpecialEquipment.Count(); ///总设备
                var tzSpecialEquipment = allSpecialEquipment.Count();
                int ybAllEquipment = (allCount.HasValue ? allCount.Value : 0);
                string allProjectData = "截至到" + this.txtMonthReportDate.Text + "，本装置项目总机具设备数：" + allEquipmentCount.ToString() + "台，其中特种机具设备：" + tzSpecialEquipment.ToString() + "台，一般机具设备：" + ybAllEquipment.ToString() + "台。\r\n";

                ////总包单位
                var mainUnit = from x in Funs.DB.Base_Unit
                               join y in Funs.DB.Project_ProjectUnit
                               on x.UnitId equals y.UnitId
                               where y.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_1
                               select x;     //1为总包
                if (mainUnit.Count() > 0)
                {
                    foreach (var item in mainUnit)
                    {
                        var mainUnitAllSpecialEquipment = allSpecialEquipment.Where(x => x.UnitId == item.UnitId);
                        var mainUnitAllGeneralEquipment = allGeneralEquipment.Where(x => x.UnitId == item.UnitId);
                        if (mainUnitAllSpecialEquipment.Count() > 0)
                        {
                            allProjectData += item.UnitName + ":";
                            allProjectData += "特种设备：" + mainUnitAllSpecialEquipment.Count().ToString() + "台，";
                            allProjectData += "。\r\n";
                        }
                        if (mainUnitAllGeneralEquipment.Count() > 0)
                        {
                            allProjectData += item.UnitName + ":";
                            allProjectData += "一般设备：" + mainUnitAllGeneralEquipment.Sum(x => x.EquipmentCount).ToString() + "台，";
                            allProjectData += "。\r\n";
                        }
                    }
                }

                //////分包单位机具            
                var subUnit = from x in Funs.DB.Base_Unit
                              join y in Funs.DB.Project_ProjectUnit
                              on x.UnitId equals y.UnitId
                              where y.ProjectId == this.ProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                              select x;     //2为施工分包
                if (subUnit.Count() > 0)
                {
                    foreach (var item in subUnit)
                    {
                        var subUnitAllSpecialEquipment = allSpecialEquipment.Where(x => x.UnitId == item.UnitId);
                        var subUnitAllGeneralEquipment = allGeneralEquipment.Where(x => x.UnitId == item.UnitId);
                        if (subUnitAllSpecialEquipment.Count() > 0)
                        {
                            allProjectData += item.UnitName + ":";
                            allProjectData += "特种设备：" + subUnitAllSpecialEquipment.Count().ToString() + "台，";
                            allProjectData += "。\r\n";
                        }
                        if (subUnitAllGeneralEquipment.Count() > 0)
                        {
                            allProjectData += item.UnitName + ":";
                            allProjectData += "一般设备：" + subUnitAllGeneralEquipment.Sum(x => x.EquipmentCount).ToString() + "台，";
                            allProjectData += "。\r\n";
                        }
                    }
                }    

                this.txtEquipmentQualityData.Text = allProjectData;
            }
        }
        #endregion

        #region 本月安全费用情况
        /// <summary>
        ///  本月安全费用情况
        /// </summary>
        private void GetMonthSafetyCostData()
        {
            ///累计集合
            var allCosts = from x in Funs.DB.CostGoods_CostSmallDetail
                           join y in Funs.DB.CostGoods_CostSmallDetailItem
                           on x.CostSmallDetailId equals y.CostSmallDetailId
                           where x.ProjectId == this.ProjectId && x.Months <= endTime
                           select y;
            ///当月集合
            var monthCosts = from x in Funs.DB.CostGoods_CostSmallDetail
                             join y in Funs.DB.CostGoods_CostSmallDetailItem
                             on x.CostSmallDetailId equals y.CostSmallDetailId
                             where x.ProjectId == this.ProjectId && x.Months <= endTime && x.Months >= startTime
                             select y;

            this.txtThisMonthSafetyCost.Text = monthCosts.Sum(x => x.CostMoney).ToString();
            this.txtTotalSafetyCost.Text = allCosts.Sum(x => x.CostMoney).ToString();
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
            if (!Funs.GetNewDateTime(this.txtReportMonths.Text + "-01").HasValue)
            {
                ShowNotify("月报月份不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.MonthReportService.GetMonthReportsByReportMonthsIDProejctID(Funs.GetNewDateTimeOrNow(this.txtReportMonths.Text + "-01"), this.MonthReportId, this.ProjectId) != null)
            {
                ShowNotify("当前月份已存在月报！", MessageBoxIcon.Warning);
                return;
            }

            Model.Manager_MonthReport monthReport = new Model.Manager_MonthReport
            {
                MonthReportCode = this.txtMonthReportCode.Text,
                ProjectId = this.ProjectId,
                Months = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                MonthReportStartDate = Funs.GetNewDateTime(this.txtMonthReportStartDate.Text),
                MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                ReportMonths = Funs.GetNewDateTime(this.txtReportMonths.Text + "-01"),
                ReportMan = this.CurrUser.UserId,
                AllProjectData = this.txtAllProjectData.Text,
                AllManhoursData = this.txtAllManhoursData.Text,

                ThisMonthKeyPoints = this.txtThisMonthKeyPoints.Text.Trim(),
                ThisMonthSafetyCost = Funs.GetNewDecimalOrZero(this.txtThisMonthSafetyCost.Text.Trim()),
                TotalSafetyCost = Funs.GetNewDecimalOrZero(this.txtTotalSafetyCost.Text.Trim()),
                ThisMonthSafetyActivity = this.txtThisMonthSafetyActivity.Text.Trim(),
                NextMonthWorkFocus = this.txtNextMonthWorkFocus.Text.Trim(),
                EquipmentQualityData = this.txtEquipmentQualityData.Text
            };

            if (!string.IsNullOrEmpty(this.MonthReportId))
            {
                monthReport.MonthReportId = this.MonthReportId;
                BLL.MonthReportService.UpdateMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthMenuId, BLL.Const.BtnModify);
            }
            else
            {
                monthReport.MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport));
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthMenuId, BLL.Const.BtnAdd);
            }

            ///保存教育与培训情况统计
            this.SaveTrainSort();
            /// 保存会议情况统计
            this.SaveMeetingSort();
            /// 保存HSE检查情况统计
            this.SaveCheckSort();
            /// 保存事故分类统计
            this.SaveAccidentSort();
            /// 保存安全奖惩情况统计
            this.SaveIncentiveSort();
            /// 保存其它情况统计
            this.SaveHseCost();
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
        #region 保存教育与培训情况统计
        /// <summary>
        /// 保存教育与培训情况统计
        /// </summary>
        private void SaveTrainSort()
        {
            BLL.TrainSortService.DeleteTrainSortsByMonthReportId(this.MonthReportId);
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                Model.Manager_TrainSort trainSort = new Model.Manager_TrainSort
                {
                    TrainSortId = this.Grid1.Rows[i].DataKeys[0].ToString(),
                    MonthReportId = this.MonthReportId,
                    TrainTypeName = this.Grid1.Rows[i].DataKeys[1].ToString(),
                    TrainNumber11 = Funs.GetNewInt(this.Grid1.Rows[i].Values[1].ToString()),
                    TrainNumber12 = Funs.GetNewInt(this.Grid1.Rows[i].Values[2].ToString()),
                    TrainNumber13 = Funs.GetNewInt(this.Grid1.Rows[i].Values[3].ToString()),
                    TrainNumber14 = Funs.GetNewInt(this.Grid1.Rows[i].Values[4].ToString())
                };
                BLL.TrainSortService.AddTrainSort(trainSort);
            }
        }
        #endregion

        #region 保存会议情况统计
        /// <summary>
        /// 保存会议情况统计
        /// </summary>
        private void SaveMeetingSort()
        {
            BLL.MeetingSortService.DeleteMeetingSortsByMonthReportId(this.MonthReportId);
            Model.Manager_MeetingSort newMeetingSort = new Model.Manager_MeetingSort
            {
                MonthReportId = this.MonthReportId,
                MeetingNumber01 = Funs.GetNewInt(this.txtMeetingNumber01.Text),
                MeetingNumber02 = Funs.GetNewInt(this.txtMeetingNumber02.Text),
                MeetingNumber03 = Funs.GetNewInt(this.txtMeetingNumber03.Text),
                MeetingNumber04 = Funs.GetNewInt(this.txtMeetingNumber04.Text),

                MeetingNumber11 = Funs.GetNewInt(this.txtMeetingNumber11.Text),
                MeetingNumber12 = Funs.GetNewInt(this.txtMeetingNumber12.Text),
                MeetingNumber13 = Funs.GetNewInt(this.txtMeetingNumber13.Text),
                MeetingNumber14 = Funs.GetNewInt(this.txtMeetingNumber14.Text)
            };
            BLL.MeetingSortService.AddMeetingSort(newMeetingSort);
        }
        #endregion

        #region 保存HSE检查情况统计
        /// <summary>
        /// 保存HSE检查情况统计
        /// </summary>
        private void SaveCheckSort()
        {
            BLL.CheckSortService.DeleteCheckSortsByMonthReportId(this.MonthReportId);
            Model.Manager_CheckSort newCheckSort = new Model.Manager_CheckSort
            {
                MonthReportId = this.MonthReportId,
                CheckNumber01 = Funs.GetNewInt(this.txtCheckNumber01.Text),
                CheckNumber02 = Funs.GetNewInt(this.txtCheckNumber02.Text),
                CheckNumber03 = Funs.GetNewInt(this.txtCheckNumber03.Text),
                CheckNumber04 = Funs.GetNewInt(this.txtCheckNumber04.Text),
                CheckNumber11 = Funs.GetNewInt(this.txtCheckNumber11.Text),
                CheckNumber12 = Funs.GetNewInt(this.txtCheckNumber12.Text),
                CheckNumber13 = Funs.GetNewInt(this.txtCheckNumber13.Text),
                CheckNumber14 = Funs.GetNewInt(this.txtCheckNumber14.Text),
                CheckNumber21 = Funs.GetNewInt(this.txtCheckNumber21.Text),
                CheckNumber22 = Funs.GetNewInt(this.txtCheckNumber22.Text),
                CheckNumber23 = Funs.GetNewInt(this.txtCheckNumber23.Text),
                CheckNumber24 = Funs.GetNewInt(this.txtCheckNumber24.Text),
                CheckNumber31 = Funs.GetNewInt(this.txtCheckNumber31.Text),
                CheckNumber32 = Funs.GetNewInt(this.txtCheckNumber32.Text),
                CheckNumber33 = Funs.GetNewInt(this.txtCheckNumber33.Text),
                CheckNumber34 = Funs.GetNewInt(this.txtCheckNumber34.Text)
            };
            BLL.CheckSortService.AddCheckSort(newCheckSort);
        }
        #endregion

        #region 保存事故分类统计
        /// <summary>
        /// 保存事故分类统计
        /// </summary>
        private void SaveAccidentSort()
        {
            BLL.AccidentSortService.DeleteAccidentSortsByMonthReportId(this.MonthReportId);
            //for (int i = 0; i < this.Grid2.Rows.Count; i++)
            //{
            //    Model.Manager_AccidentSort accidentSort = new Model.Manager_AccidentSort();
            //    accidentSort.AccidentSortId = this.Grid2.Rows[i].DataKeys[0].ToString();
            //    accidentSort.MonthReportId = this.MonthReportId;
            //    accidentSort.AccidentTypeId = this.Grid2.Rows[i].DataKeys[1].ToString();
            //    accidentSort.AccidentNumber01 = Funs.GetNewInt(this.Grid2.Rows[i].Values[1].ToString());
            //    accidentSort.AccidentNumber02 = Funs.GetNewInt(this.Grid2.Rows[i].Values[2].ToString());
            //    BLL.AccidentSortService.AddAccidentSort(accidentSort);
            //}

            foreach (JObject mergedRow in Grid2.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.Manager_AccidentSort accidentSort = new Model.Manager_AccidentSort
                {
                    MonthReportId = this.MonthReportId
                };
                if (values["AccidentSortId"] != null)
                {
                    accidentSort.AccidentSortId  += values.Value<string>("AccidentSortId");
                }
                if (values["AccidentTypeId"] != null)
                {
                    accidentSort.AccidentTypeId += values.Value<string>("AccidentTypeId");
                }                
                if (values["AccidentNumber01"] != null)
                {
                    accidentSort.AccidentNumber01= values.Value<int>("AccidentNumber01");
                }
                if (values["AccidentNumber02"] != null)
                {
                    accidentSort.AccidentNumber02 = values.Value<int>("AccidentNumber02");
                }

                if (!string.IsNullOrEmpty(accidentSort.AccidentSortId))
                {
                    BLL.AccidentSortService.AddAccidentSort(accidentSort);
                }
            }
        }
        #endregion

        #region 保存安全奖惩情况统计
        /// <summary>
        /// 保存安全奖惩情况统计
        /// </summary>
        private void SaveIncentiveSort()
        {
            BLL.IncentiveSortService.DeleteIncentiveSortsByMonthReportId(this.MonthReportId);
            Model.Manager_IncentiveSort newIncentiveSort = new Model.Manager_IncentiveSort
            {
                MonthReportId = this.MonthReportId,
                IncentiveNumber01 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber01.Text),
                IncentiveNumber02 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber02.Text),
                IncentiveNumber03 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber03.Text),
                IncentiveNumber04 = Funs.GetNewInt(this.txtIncentiveNumber04.Text),
                IncentiveNumber05 = Funs.GetNewInt(this.txtIncentiveNumber05.Text),
                IncentiveNumber06 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber06.Text),
                IncentiveNumber07 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber07.Text),

                IncentiveNumber11 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber11.Text),
                IncentiveNumber12 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber12.Text),
                IncentiveNumber13 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber13.Text),
                IncentiveNumber14 = Funs.GetNewInt(this.txtIncentiveNumber14.Text),
                IncentiveNumber15 = Funs.GetNewInt(this.txtIncentiveNumber15.Text),
                IncentiveNumber16 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber16.Text),
                IncentiveNumber17 = Funs.GetNewDecimalOrZero(this.txtIncentiveNumber17.Text)
            };

            BLL.IncentiveSortService.AddIncentiveSort(newIncentiveSort);
        }
        #endregion

        #region 保存其它情况统计
        /// <summary>
        /// 保存其它情况统计
        /// </summary>
        private void SaveHseCost()
        {
            BLL.HseCostService.DeleteHseSortsByMonthReportId(this.MonthReportId);
            Model.Manager_HseCost newHseCost = new Model.Manager_HseCost
            {
                MonthReportId = this.MonthReportId,
                HseNumber01 = Funs.GetNewInt(this.txtHseNumber01.Text),
                HseNumber02 = Funs.GetNewInt(this.txtHseNumber02.Text),
                HseNumber03 = Funs.GetNewInt(this.txtHseNumber03.Text),
                HseNumber04 = Funs.GetNewInt(this.txtHseNumber04.Text),
                HseNumber05 = Funs.GetNewInt(this.txtHseNumber05.Text),
                HseNumber06 = Funs.GetNewInt(this.txtHseNumber06.Text),
                HseNumber07 = Funs.GetNewInt(this.txtHseNumber07.Text),
                HseNumber08 = Funs.GetNewInt(this.txtHseNumber08.Text),
                HseNumber09 = Funs.GetNewInt(this.txtHseNumber09.Text),
                HseNumber00 = Funs.GetNewInt(this.txtHseNumber00.Text),
                HseNumber10 = Funs.GetNewDecimalOrZero(this.txtHseNumber10.Text),
                HseNumber11 = Funs.GetNewDecimalOrZero(this.txtHseNumber11.Text),
                SpecialNumber = Funs.GetNewInt(this.txtSpecialNumber.Text)
            };
            //newHseCost.HseNumber12 = Funs.GetNewDecimal(this.txtHseNumber12.Text);
            //newHseCost.HseNumber13 = Funs.GetNewDecimal(this.txtHseNumber13.Text);
            //newHseCost.HseNumber14 = Funs.GetNewDecimal(this.txtHseNumber14.Text);

            BLL.HseCostService.AddHseSort(newHseCost);
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
            if (!Funs.GetNewDateTime(this.txtReportMonths.Text + "-01").HasValue)
            {
                ShowNotify("月报月份不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.MonthReportService.GetMonthReportsByReportMonthsIDProejctID(Funs.GetNewDateTimeOrNow(this.txtReportMonths.Text + "-01"), this.MonthReportId, this.ProjectId) != null)
            {
                ShowNotify("当前月份已存在月报！", MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(this.MonthReportId))
            {
                Model.Manager_MonthReport monthReport = new Model.Manager_MonthReport
                {
                    MonthReportCode = this.txtMonthReportCode.Text,
                    ProjectId = this.ProjectId,
                    Months = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    MonthReportStartDate = Funs.GetNewDateTime(this.txtMonthReportStartDate.Text),
                    MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    ReportMonths = Funs.GetNewDateTime(this.txtReportMonths.Text + "-01"),
                    ReportMan = this.CurrUser.UserId,
                    AllProjectData = this.txtAllProjectData.Text,
                    AllManhoursData = this.txtAllManhoursData.Text,

                    ThisMonthKeyPoints = this.txtThisMonthKeyPoints.Text.Trim(),
                    ThisMonthSafetyCost = Funs.GetNewDecimalOrZero(this.txtThisMonthSafetyCost.Text.Trim()),
                    TotalSafetyCost = Funs.GetNewDecimalOrZero(this.txtTotalSafetyCost.Text.Trim()),
                    ThisMonthSafetyActivity = this.txtThisMonthSafetyActivity.Text.Trim(),
                    NextMonthWorkFocus = this.txtNextMonthWorkFocus.Text.Trim(),
                    EquipmentQualityData = this.txtEquipmentQualityData.Text,

                    MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport))
                };
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId,BLL.Const.ProjectManagerMonthMenuId,BLL.Const.BtnAdd);
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ProjectManagerMonthMenuId)));
        }
        #endregion
    }
}