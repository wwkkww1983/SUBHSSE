using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Information
{
    public partial class MillionsMonthlyReportSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 报表主键Id
        /// </summary>
        public string MillionsMonthlyReportId
        {
            get
            {
                return (string)ViewState["MillionsMonthlyReportId"];
            }
            set
            {
                ViewState["MillionsMonthlyReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Information_MillionsMonthlyReportItem> items = new List<Model.Information_MillionsMonthlyReportItem>();
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                items.Clear();
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, ConstValue.Group_0009, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, false);
                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;
                string unitId = Request.QueryString["UnitId"];
                string year = Request.QueryString["Year"];
                string months = Request.QueryString["Months"];
                MillionsMonthlyReportId = Request.QueryString["MillionsMonthlyReportId"];
                if (!String.IsNullOrEmpty(MillionsMonthlyReportId))
                {
                    items = BLL.MillionsMonthlyReportItemService.GetItemsNoSum(MillionsMonthlyReportId);
                    //int i = items.Count * 10;
                    //int count = items.Count;
                    //if (items.Count < 10)
                    //{
                    //    for (int j = 0; j < (10 - count); j++)
                    //    {
                    //        i += 10;
                    //        Model.Information_MillionsMonthlyReportItem newItem = new Information_MillionsMonthlyReportItem
                    //        {
                    //            MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                    //            Affiliation = string.Empty,
                    //            Name = string.Empty,
                    //            SortIndex = i
                    //        };
                    //        items.Add(newItem);
                    //    }
                    //}
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                    Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(MillionsMonthlyReportId);
                    if (report != null)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        this.btnCopy.Hidden = true;
                        if (report.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                        else
                        {
                            if (report.HandleMan == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId)
                            {
                                this.btnSave.Hidden = false;
                                this.btnSubmit.Hidden = false;
                            }
                        }
                        if (report.UpState == BLL.Const.UpState_3)
                        {
                            this.btnSave.Hidden = true;
                            this.btnUpdata.Hidden = true;
                        }
                        drpMonth.SelectedValue = report.Month.ToString();
                        drpYear.SelectedValue = report.Year.ToString();
                        drpUnit.SelectedValue = report.UnitId;
                        if (report.FillingDate != null)
                        {
                            txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", report.FillingDate);
                        }
                        txtDutyPerson.Text = report.DutyPerson;
                        if (report.RecordableIncidentRate != null)
                        {
                            txtRecordableIncidentRate.Text = report.RecordableIncidentRate.ToString();
                        }
                        if (report.LostTimeRate != null)
                        {
                            txtLostTimeRate.Text = report.LostTimeRate.ToString();
                        }
                        if (report.LostTimeInjuryRate != null)
                        {
                            txtLostTimeInjuryRate.Text = report.LostTimeInjuryRate.ToString();
                        }
                        if (report.DeathAccidentFrequency != null)
                        {
                            txtDeathAccidentFrequency.Text = report.DeathAccidentFrequency.ToString();
                        }
                        if (report.AccidentMortality != null)
                        {
                            txtAccidentMortality.Text = report.AccidentMortality.ToString();
                        }
                    }
                }
                else
                {
                    this.btnCopy.Hidden = false;
                    drpMonth.SelectedValue = months;
                    drpYear.SelectedValue = year;
                    txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    txtDutyPerson.Text = this.CurrUser.UserName;
                    //增加明细集合
                    GetNewItems(year, months);
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                    this.txtRecordableIncidentRate.Text = "0";
                    this.txtLostTimeRate.Text = "0";
                    this.txtLostTimeInjuryRate.Text = "0";
                    this.txtDeathAccidentFrequency.Text = "0";
                    this.txtAccidentMortality.Text = "0";
                }
            }
        }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(MillionsMonthlyReportId);
            if (report.HandleMan == this.CurrUser.UserId)
            {
                this.btnSave.Hidden = false;
                this.btnSubmit.Hidden = false;
            }
            else
            {
                this.btnSave.Hidden = true;
                this.btnSubmit.Hidden = true;
            }
        }
        #endregion

        #region 保存、提交、上报
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void Save(string type)
        {
            //string millionsMonthlyReportId = Request.QueryString["MillionsMonthlyReportId"];
            Model.Information_MillionsMonthlyReport report = new Information_MillionsMonthlyReport
            {
                UnitId = drpUnit.SelectedValue,
                Year = Funs.GetNewIntOrZero(drpYear.SelectedValue),
                Month = Funs.GetNewIntOrZero(drpMonth.SelectedValue)
            };
            if (!string.IsNullOrEmpty(txtFillingDate.Text.Trim()))
            {
                report.FillingDate = Convert.ToDateTime(txtFillingDate.Text.Trim());
            }
            report.DutyPerson = txtDutyPerson.Text.Trim();
            if (!string.IsNullOrEmpty(txtRecordableIncidentRate.Text.Trim()))
            {
                report.RecordableIncidentRate = Convert.ToDecimal(txtRecordableIncidentRate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLostTimeRate.Text.Trim()))
            {
                report.LostTimeRate = Convert.ToDecimal(txtLostTimeRate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLostTimeInjuryRate.Text.Trim()))
            {
                report.LostTimeInjuryRate = Convert.ToDecimal(txtLostTimeInjuryRate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDeathAccidentFrequency.Text.Trim()))
            {
                report.DeathAccidentFrequency = Convert.ToDecimal(txtDeathAccidentFrequency.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAccidentMortality.Text.Trim()))
            {
                report.AccidentMortality = Convert.ToDecimal(txtAccidentMortality.Text.Trim());
            }

            if (String.IsNullOrEmpty(MillionsMonthlyReportId))
            {
                Model.Information_MillionsMonthlyReport old = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdDate(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
                if (old == null)
                {
                    report.MillionsMonthlyReportId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReport));
                    report.UpState = BLL.Const.UpState_2;
                    report.FillingMan = this.CurrUser.UserName;
                    report.HandleState = BLL.Const.HandleState_1;
                    report.HandleMan = this.CurrUser.UserId;
                    BLL.MillionsMonthlyReportService.AddMillionsMonthlyReport(report);
                    BLL.LogService.AddSys_Log(this.CurrUser, report.Year.ToString() + "-" + report.Month.ToString(), report.MillionsMonthlyReportId, BLL.Const.MillionsMonthlyReportMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    ShowNotify("该月份记录已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Model.Information_MillionsMonthlyReport oldReport = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(MillionsMonthlyReportId);
                if (oldReport != null)
                {
                    report.HandleMan = oldReport.HandleMan;
                    report.HandleState = oldReport.HandleState;
                }
                report.MillionsMonthlyReportId = MillionsMonthlyReportId;
                report.UpState = BLL.Const.UpState_2;
                BLL.MillionsMonthlyReportService.UpdateMillionsMonthlyReport(report);
                BLL.LogService.AddSys_Log(this.CurrUser, report.Year.ToString()+"-"+report.Month.ToString(),report.MillionsMonthlyReportId,BLL.Const.MillionsMonthlyReportMenuId,BLL.Const.BtnModify );
            }
            MillionsMonthlyReportId = report.MillionsMonthlyReportId;
            BLL.MillionsMonthlyReportItemService.DeleteMillionsMonthlyReportItemByMillionsMonthlyReportId(report.MillionsMonthlyReportId);
            GetItems(report.MillionsMonthlyReportId);
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    item.Affiliation = System.Web.HttpUtility.HtmlDecode(item.Affiliation);
                    item.Name = System.Web.HttpUtility.HtmlDecode(item.Name);
                    BLL.MillionsMonthlyReportItemService.AddMillionsMonthlyReportItem(item);
                }
            }
            if (type == "updata")     //保存并上报
            {
                Update(report.MillionsMonthlyReportId);
            }
            if (type == "submit")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReportSubmit.aspx?Type=MillionsMonthlyReport&Id={0}", report.MillionsMonthlyReportId, "编辑 - ")));
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            //if (type != "submit")
            //{
            //    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save("add");
        }

        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            Save("updata");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save("submit");
        }
        #endregion

        #region 数据同步
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="millionsMonthlyReportId"></param>
        private void Update(string millionsMonthlyReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertInformation_MillionsMonthlyReportTableCompleted += new EventHandler<HSSEService.DataInsertInformation_MillionsMonthlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_MillionsMonthlyReportTableCompleted);
            var report = from x in Funs.DB.Information_MillionsMonthlyReport
                         where x.MillionsMonthlyReportId == millionsMonthlyReportId && x.UpState == BLL.Const.UpState_2
                         select new HSSEService.Information_MillionsMonthlyReport
                         {
                             MillionsMonthlyReportId = x.MillionsMonthlyReportId,
                             UnitId = x.UnitId,
                             Year = x.Year,
                             Month = x.Month,
                             FillingMan = x.FillingMan,
                             FillingDate = x.FillingDate,
                             DutyPerson = x.DutyPerson,
                             RecordableIncidentRate = x.RecordableIncidentRate,
                             LostTimeRate = x.LostTimeRate,
                             LostTimeInjuryRate = x.LostTimeInjuryRate,
                             DeathAccidentFrequency = x.DeathAccidentFrequency,
                             AccidentMortality = x.AccidentMortality,
                         };

            var reportItem = from x in Funs.DB.Information_MillionsMonthlyReportItem
                             where x.MillionsMonthlyReportId == millionsMonthlyReportId
                             select new HSSEService.Information_MillionsMonthlyReportItem
                             {
                                 MillionsMonthlyReportItemId = x.MillionsMonthlyReportItemId,
                                 MillionsMonthlyReportId = x.MillionsMonthlyReportId,
                                 SortIndex = x.SortIndex,
                                 Affiliation = x.Affiliation,
                                 Name = x.Name,
                                 PostPersonNum = x.PostPersonNum,
                                 SnapPersonNum = x.SnapPersonNum,
                                 ContractorNum = x.ContractorNum,
                                 SumPersonNum = x.SumPersonNum,
                                 TotalWorkNum = x.TotalWorkNum,
                                 SeriousInjuriesNum = x.SeriousInjuriesNum,
                                 SeriousInjuriesPersonNum = x.SeriousInjuriesPersonNum,
                                 SeriousInjuriesLossHour = x.SeriousInjuriesLossHour,
                                 MinorAccidentNum = x.MinorAccidentNum,
                                 MinorAccidentPersonNum = x.MinorAccidentPersonNum,
                                 MinorAccidentLossHour = x.MinorAccidentLossHour,
                                 OtherAccidentNum = x.OtherAccidentNum,
                                 OtherAccidentPersonNum = x.OtherAccidentPersonNum,
                                 OtherAccidentLossHour = x.OtherAccidentLossHour,
                                 RestrictedWorkPersonNum = x.RestrictedWorkPersonNum,
                                 RestrictedWorkLossHour = x.RestrictedWorkLossHour,
                                 MedicalTreatmentPersonNum = x.MedicalTreatmentPersonNum,
                                 MedicalTreatmentLossHour = x.MedicalTreatmentLossHour,
                                 FireNum = x.FireNum,
                                 ExplosionNum = x.ExplosionNum,
                                 TrafficNum = x.TrafficNum,
                                 EquipmentNum = x.EquipmentNum,
                                 QualityNum = x.QualityNum,
                                 OtherNum = x.OtherNum,
                                 FirstAidDressingsNum = x.FirstAidDressingsNum,
                                 AttemptedEventNum = x.AttemptedEventNum,
                                 LossDayNum = x.LossDayNum,
                             };
            poxy.DataInsertInformation_MillionsMonthlyReportTableAsync(report.ToList(), reportItem.ToList());
        }

        #region 百万工时安全统计月报表
        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_MillionsMonthlyReportTableCompleted(object sender, HSSEService.DataInsertInformation_MillionsMonthlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.MillionsMonthlyReportService.UpdateMillionsMonthlyReport(report);
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.MillionsMonthlyReportMenuId, item);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_1 && x.YearId == report.Year.ToString() && x.MonthId == report.Month.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【百万工时安全统计月报表】上传到服务器" + idList.Count.ToString() + "条数据；",null,BLL.Const.MillionsMonthlyReportMenuId,BLL.Const.BtnUploadResources);
            }
            else
            {                
                BLL.LogService.AddSys_Log(this.CurrUser, "【百万工时安全统计月报表】上传到服务器失败；", null, BLL.Const.MillionsMonthlyReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
        #endregion
        
        #region 增加本月明细
        private void GetItems(string millionsMonthlyReportId)
        {
            decimal TotalWorkNumSum = 0;
            int PostPersonNumSum = 0, SnapPersonNumSum = 0, ContractorNumSum = 0, SumPersonNumSum = 0, SeriousInjuriesNumSum = 0, SeriousInjuriesPersonNumSum = 0, SeriousInjuriesLossHourSum = 0, MinorAccidentNumSum = 0,
                           MinorAccidentPersonNumSum = 0, MinorAccidentLossHourSum = 0, OtherAccidentNumSum = 0, OtherAccidentPersonNumSum = 0, OtherAccidentLossHourSum = 0, RestrictedWorkPersonNumSum = 0, RestrictedWorkLossHourSum = 0, MedicalTreatmentPersonNumSum = 0, MedicalTreatmentLossHourSum = 0,
                           FireNumSum = 0, ExplosionNumSum = 0, TrafficNumSum = 0, EquipmentNumSum = 0, QualityNumSum = 0, OtherNumSum = 0, FirstAidDressingsNumSum = 0, AttemptedEventNumSum = 0, LossDayNumSum = 0;
            items.Clear();
            int i = 10;           
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                int SumPersonNum = 0;
                Model.Information_MillionsMonthlyReportItem item = new Information_MillionsMonthlyReportItem();
                if (values["MillionsMonthlyReportItemId"].ToString() != "")
                {
                    item.MillionsMonthlyReportItemId = values.Value<string>("MillionsMonthlyReportItemId");
                }
                item.MillionsMonthlyReportId = millionsMonthlyReportId;
                item.SortIndex = i;
                if (!string.IsNullOrEmpty(values["Affiliation"].ToString()))
                {
                    item.Affiliation = values.Value<string>("Affiliation");
                }
                if (!string.IsNullOrEmpty(values["Name"].ToString()))
                {
                    item.Name = values.Value<string>("Name");
                }
                if (!string.IsNullOrEmpty(values["PostPersonNum"].ToString()))
                {
                    item.PostPersonNum = values.Value<int>("PostPersonNum");
                    SumPersonNum += values.Value<int>("PostPersonNum");
                    PostPersonNumSum += values.Value<int>("PostPersonNum");
                    SumPersonNumSum += values.Value<int>("PostPersonNum");
                }
                else
                {
                    item.PostPersonNum = 0;
                }
                if (!string.IsNullOrEmpty(values["SnapPersonNum"].ToString()))
                {
                    item.SnapPersonNum = values.Value<int>("SnapPersonNum");
                    SumPersonNum += values.Value<int>("SnapPersonNum");
                    SnapPersonNumSum += values.Value<int>("SnapPersonNum");
                    SumPersonNumSum += values.Value<int>("SnapPersonNum");
                }
                else
                {
                    item.SnapPersonNum = 0;
                }
                if (!string.IsNullOrEmpty(values["ContractorNum"].ToString()))
                {
                    item.ContractorNum = values.Value<int>("ContractorNum");
                    SumPersonNum += values.Value<int>("ContractorNum");
                    ContractorNumSum += values.Value<int>("ContractorNum");
                    SumPersonNumSum += values.Value<int>("ContractorNum");
                }
                else
                {
                    item.ContractorNum = 0;
                }
                if (SumPersonNum != 0)
                {
                    item.SumPersonNum = SumPersonNum;          //获取每条明细记录员工总数合计值
                }
                if (!string.IsNullOrEmpty(values["TotalWorkNum"].ToString()))
                {
                    item.TotalWorkNum = values.Value<decimal>("TotalWorkNum");
                    TotalWorkNumSum += values.Value<decimal>("TotalWorkNum");
                }
                else
                {
                    item.TotalWorkNum = 0;
                }
                if (!string.IsNullOrEmpty(values["SeriousInjuriesNum"].ToString()))
                {
                    item.SeriousInjuriesNum = values.Value<int>("SeriousInjuriesNum");
                    SeriousInjuriesNumSum += values.Value<int>("SeriousInjuriesNum");
                }
                else
                {
                    item.SeriousInjuriesNum = 0;
                }
                if (!string.IsNullOrEmpty(values["SeriousInjuriesPersonNum"].ToString()))
                {
                    item.SeriousInjuriesPersonNum = values.Value<int>("SeriousInjuriesPersonNum");
                    SeriousInjuriesPersonNumSum += values.Value<int>("SeriousInjuriesPersonNum");
                }
                else
                {
                    item.SeriousInjuriesPersonNum = 0;
                }
                if (values["SeriousInjuriesLossHour"].ToString() != "")
                {
                    item.SeriousInjuriesLossHour = values.Value<int>("SeriousInjuriesLossHour");
                    SeriousInjuriesLossHourSum += values.Value<int>("SeriousInjuriesLossHour");
                }
                else
                {
                    item.SeriousInjuriesLossHour = 0;
                }
                if (values["MinorAccidentNum"].ToString() != "")
                {
                    item.MinorAccidentNum = values.Value<int>("MinorAccidentNum");
                    MinorAccidentNumSum += values.Value<int>("MinorAccidentNum");
                }
                else
                {
                    item.MinorAccidentNum = 0;
                }
                if (values["MinorAccidentPersonNum"].ToString() != "")
                {
                    item.MinorAccidentPersonNum = values.Value<int>("MinorAccidentPersonNum");
                    MinorAccidentPersonNumSum += values.Value<int>("MinorAccidentPersonNum");
                }
                if (values["MinorAccidentLossHour"].ToString() != "")
                {
                    item.MinorAccidentLossHour = values.Value<int>("MinorAccidentLossHour");
                    MinorAccidentLossHourSum += values.Value<int>("MinorAccidentLossHour");
                }
                else
                {
                    item.MinorAccidentLossHour = 0;
                }
                if (values["OtherAccidentNum"].ToString() != "")
                {
                    item.OtherAccidentNum = values.Value<int>("OtherAccidentNum");
                    OtherAccidentNumSum += values.Value<int>("OtherAccidentNum");
                }
                if (values["OtherAccidentPersonNum"].ToString() != "")
                {
                    item.OtherAccidentPersonNum = values.Value<int>("OtherAccidentPersonNum");
                    OtherAccidentPersonNumSum += values.Value<int>("OtherAccidentPersonNum");
                }
                else
                {
                    item.OtherAccidentPersonNum = 0;
                }
                if (values["OtherAccidentLossHour"].ToString() != "")
                {
                    item.OtherAccidentLossHour = values.Value<int>("OtherAccidentLossHour");
                    OtherAccidentLossHourSum += values.Value<int>("OtherAccidentLossHour");
                }
                else
                {
                    item.OtherAccidentLossHour = 0;
                }
                if (values["RestrictedWorkPersonNum"].ToString() != "")
                {
                    item.RestrictedWorkPersonNum = values.Value<int>("RestrictedWorkPersonNum");
                    RestrictedWorkPersonNumSum += values.Value<int>("RestrictedWorkPersonNum");
                }
                else
                {
                    item.RestrictedWorkPersonNum = 0;
                }
                if (values["RestrictedWorkLossHour"].ToString() != "")
                {
                    item.RestrictedWorkLossHour = values.Value<int>("RestrictedWorkLossHour");
                    RestrictedWorkLossHourSum += values.Value<int>("RestrictedWorkLossHour");
                }
                else
                {
                    item.RestrictedWorkLossHour = 0;
                }
                if (values["MedicalTreatmentPersonNum"].ToString() != "")
                {
                    item.MedicalTreatmentPersonNum = values.Value<int>("MedicalTreatmentPersonNum");
                    MedicalTreatmentPersonNumSum += values.Value<int>("MedicalTreatmentPersonNum");
                }
                else
                {
                    item.MedicalTreatmentPersonNum = 0;
                }
                if (values["MedicalTreatmentLossHour"].ToString() != "")
                {
                    item.MedicalTreatmentLossHour = values.Value<int>("MedicalTreatmentLossHour");
                    MedicalTreatmentLossHourSum += values.Value<int>("MedicalTreatmentLossHour");
                }
                else
                {
                    item.MedicalTreatmentLossHour = 0;
                }
                if (values["FireNum"].ToString() != "")
                {
                    item.FireNum = values.Value<int>("FireNum");
                    FireNumSum += values.Value<int>("FireNum");
                }
                else
                {
                    item.FireNum = 0;
                }
                if (values["ExplosionNum"].ToString() != "")
                {
                    item.ExplosionNum = values.Value<int>("ExplosionNum");
                    ExplosionNumSum += values.Value<int>("ExplosionNum");
                }
                else
                {
                    item.ExplosionNum = 0;
                }
                if (values["TrafficNum"].ToString() != "")
                {
                    item.TrafficNum = values.Value<int>("TrafficNum");
                    TrafficNumSum += values.Value<int>("TrafficNum");
                }
                else
                {
                    item.TrafficNum = 0;
                }
                if (values["EquipmentNum"].ToString() != "")
                {
                    item.EquipmentNum = values.Value<int>("EquipmentNum");
                    EquipmentNumSum += values.Value<int>("EquipmentNum");
                }
                else
                {
                    item.EquipmentNum = 0;
                }
                if (values["QualityNum"].ToString() != "")
                {
                    item.QualityNum = values.Value<int>("QualityNum");
                    QualityNumSum += values.Value<int>("QualityNum");
                }
                else
                {
                    item.QualityNum = 0;
                }
                if (values["OtherNum"].ToString() != "")
                {
                    item.OtherNum = values.Value<int>("OtherNum");
                    OtherNumSum += values.Value<int>("OtherNum");
                }
                else
                {
                    item.OtherNum = 0;
                }
                if (values["FirstAidDressingsNum"].ToString() != "")
                {
                    item.FirstAidDressingsNum = values.Value<int>("FirstAidDressingsNum");
                    FirstAidDressingsNumSum += values.Value<int>("FirstAidDressingsNum");
                }
                else
                {
                    item.FirstAidDressingsNum = 0;
                }
                if (values["AttemptedEventNum"].ToString() != "")
                {
                    item.AttemptedEventNum = values.Value<int>("AttemptedEventNum");
                    AttemptedEventNumSum += values.Value<int>("AttemptedEventNum");
                }
                else
                {
                    item.AttemptedEventNum = 0;
                }
                if (values["LossDayNum"].ToString() != "")
                {
                    item.LossDayNum = values.Value<int>("LossDayNum");
                    LossDayNumSum += values.Value<int>("LossDayNum");
                }
                else
                {
                    item.LossDayNum = 0;
                }
                items.Add(item);
                i += 10;
            }

            Model.Information_MillionsMonthlyReportItem totalItem = new Information_MillionsMonthlyReportItem
            {
                MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                MillionsMonthlyReportId = millionsMonthlyReportId,
                SortIndex = i + 10,
                Affiliation = "本月合计",
                Name = "本月合计",
                PostPersonNum = PostPersonNumSum,
                SnapPersonNum = SnapPersonNumSum,
                ContractorNum = ContractorNumSum,
                SumPersonNum = SumPersonNumSum,
                TotalWorkNum = TotalWorkNumSum,
                SeriousInjuriesNum = SeriousInjuriesNumSum,
                SeriousInjuriesPersonNum = SeriousInjuriesPersonNumSum,
                SeriousInjuriesLossHour = SeriousInjuriesLossHourSum,
                MinorAccidentNum = MinorAccidentNumSum,
                MinorAccidentPersonNum = MinorAccidentPersonNumSum,
                MinorAccidentLossHour = MinorAccidentLossHourSum,
                OtherAccidentNum = OtherAccidentNumSum,
                OtherAccidentPersonNum = OtherAccidentPersonNumSum,
                OtherAccidentLossHour = OtherAccidentLossHourSum,
                RestrictedWorkPersonNum = RestrictedWorkPersonNumSum,
                RestrictedWorkLossHour = RestrictedWorkLossHourSum,
                MedicalTreatmentPersonNum = MedicalTreatmentPersonNumSum,
                MedicalTreatmentLossHour = MedicalTreatmentLossHourSum,
                FireNum = FireNumSum,
                ExplosionNum = ExplosionNumSum,
                TrafficNum = TrafficNumSum,
                EquipmentNum = EquipmentNumSum,
                QualityNum = QualityNumSum,
                OtherNum = OtherNumSum,
                FirstAidDressingsNum = FirstAidDressingsNumSum,
                AttemptedEventNum = AttemptedEventNumSum,
                LossDayNum = LossDayNumSum
            };
            items.Add(totalItem);
        }
        #endregion

        #region Grid行点击事件
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            GetItems(string.Empty);
            Model.Information_MillionsMonthlyReportItem totalItem = items.FirstOrDefault(x => x.Affiliation == "本月合计");
            items.Remove(totalItem);
            if (e.CommandName == "Add")
            {
                Model.Information_MillionsMonthlyReportItem oldItem = items.FirstOrDefault(x => x.MillionsMonthlyReportItemId == rowID);
                Model.Information_MillionsMonthlyReportItem newItem = new Information_MillionsMonthlyReportItem
                {
                    MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem))
                };
                if (oldItem != null)
                {
                    newItem.SortIndex = oldItem.SortIndex + 1;
                    newItem.Affiliation = oldItem.Affiliation;
                }
                else
                {
                    newItem.SortIndex = 0;
                }
                items.Add(newItem);
                items = items.OrderBy(x => x.SortIndex).ToList();
                Grid1.DataSource = items;
                Grid1.DataBind();
            }
            if (e.CommandName == "Delete")
            {
                foreach (var item in items)
                {
                    if (item.MillionsMonthlyReportItemId == rowID)
                    {
                        items.Remove(item);
                        break;
                    }
                }
                Grid1.DataSource = items;
                Grid1.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取明细
        private void GetNewItems(string year, string months)
        {
            //获取项目集合
            List<Model.InformationProject_MillionsMonthlyReport> millionsMonthlyReports = (from x in Funs.DB.InformationProject_MillionsMonthlyReport where x.Year.ToString() == year && x.Month.ToString() == months && x.States == BLL.Const.State_2 select x).ToList();
            List<string> projectIds = millionsMonthlyReports.Select(x => x.ProjectId).ToList();
            //增加明细集合
            Model.Information_MillionsMonthlyReportItem item1 = new Information_MillionsMonthlyReportItem
            {
                MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                Affiliation = "机关后勤服务",
                Name = "总部",
                SortIndex = 10,
                PostPersonNum = 0,
                SnapPersonNum = 0,
                ContractorNum = 0,
                SumPersonNum = 0,
                TotalWorkNum = 0,
                SeriousInjuriesNum = 0,
                SeriousInjuriesPersonNum = 0,
                SeriousInjuriesLossHour = 0,
                MinorAccidentNum = 0,
                MinorAccidentPersonNum = 0,
                MinorAccidentLossHour = 0,
                OtherAccidentNum = 0,
                OtherAccidentPersonNum = 0,
                OtherAccidentLossHour = 0,
                RestrictedWorkPersonNum = 0,
                RestrictedWorkLossHour = 0,
                MedicalTreatmentPersonNum = 0,
                MedicalTreatmentLossHour = 0,
                FireNum = 0,
                ExplosionNum = 0,
                TrafficNum = 0,
                EquipmentNum = 0,
                QualityNum = 0,
                OtherNum = 0,
                FirstAidDressingsNum = 0,
                AttemptedEventNum = 0,
                LossDayNum = 0,
            };

            items.Add(item1);
            Model.Information_MillionsMonthlyReportItem item2 = new Information_MillionsMonthlyReportItem
            {
                MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                Affiliation = "机关后勤服务",
                Name = "二级单位",
                SortIndex = 20,
                PostPersonNum = 0,
                SnapPersonNum = 0,
                ContractorNum = 0,
                SumPersonNum = 0,
                TotalWorkNum = 0,
                SeriousInjuriesNum = 0,
                SeriousInjuriesPersonNum = 0,
                SeriousInjuriesLossHour = 0,
                MinorAccidentNum = 0,
                MinorAccidentPersonNum = 0,
                MinorAccidentLossHour = 0,
                OtherAccidentNum = 0,
                OtherAccidentPersonNum = 0,
                OtherAccidentLossHour = 0,
                RestrictedWorkPersonNum = 0,
                RestrictedWorkLossHour = 0,
                MedicalTreatmentPersonNum = 0,
                MedicalTreatmentLossHour = 0,
                FireNum = 0,
                ExplosionNum = 0,
                TrafficNum = 0,
                EquipmentNum = 0,
                QualityNum = 0,
                OtherNum = 0,
                FirstAidDressingsNum = 0,
                AttemptedEventNum = 0,
                LossDayNum = 0,
            };
            items.Add(item2);
            var projects = (from x in Funs.DB.Base_Project
                           where projectIds.Contains(x.ProjectId)
                           select x).ToList();
            var maiUnit=BLL.CommonService.GetIsThisUnit();
            if (maiUnit != null && maiUnit.UnitId == BLL.Const.UnitId_14)
            {
                projects = BLL.ProjectService.GetProjectWorkList();
            }
            int i = 20;
            foreach (var p in projects)
            {
                i += 10;
                Model.Information_MillionsMonthlyReportItem item = new Information_MillionsMonthlyReportItem
                {
                    MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                    Affiliation = "项目现场",
                    Name = p.ProjectName,
                    SortIndex = i
                };

                if (!string.IsNullOrEmpty(p.UnitId))
                {
                    var name = BLL.UnitService.GetUnitNameByUnitId(p.UnitId);
                    if (!string.IsNullOrEmpty(name))
                    {                       
                        item.Affiliation = name;
                        item.Name = "[" + p.ProjectCode + "]" + p.ProjectName;
                    }
                }

                Model.InformationProject_MillionsMonthlyReport report = millionsMonthlyReports.FirstOrDefault(x => x.ProjectId == p.ProjectId);
                if (report != null)
                {
                    item.PostPersonNum = report.PostPersonNum;
                    item.SnapPersonNum = report.SnapPersonNum;
                    item.ContractorNum = report.ContractorNum;
                    item.SumPersonNum = report.SumPersonNum;
                    item.TotalWorkNum = report.TotalWorkNum;
                    item.SeriousInjuriesNum = report.SeriousInjuriesNum;
                    item.SeriousInjuriesPersonNum = report.SeriousInjuriesPersonNum;
                    item.SeriousInjuriesLossHour = report.SeriousInjuriesLossHour;
                    item.MinorAccidentNum = report.MinorAccidentNum;
                    item.MinorAccidentPersonNum = report.MinorAccidentPersonNum;
                    item.MinorAccidentLossHour = report.MinorAccidentLossHour;
                    item.OtherAccidentNum = report.OtherAccidentNum;
                    item.OtherAccidentPersonNum = report.OtherAccidentPersonNum;
                    item.OtherAccidentLossHour = report.OtherAccidentLossHour;
                    item.RestrictedWorkPersonNum = report.RestrictedWorkPersonNum;
                    item.RestrictedWorkLossHour = report.RestrictedWorkLossHour;
                    item.MedicalTreatmentPersonNum = report.MedicalTreatmentPersonNum;
                    item.MedicalTreatmentLossHour = report.MedicalTreatmentLossHour;
                    item.FireNum = report.FireNum;
                    item.ExplosionNum = report.ExplosionNum;
                    item.TrafficNum = report.TrafficNum;
                    item.EquipmentNum = report.EquipmentNum;
                    item.QualityNum = report.QualityNum;
                    item.OtherNum = report.OtherNum;
                    item.FirstAidDressingsNum = report.FirstAidDressingsNum;
                    item.AttemptedEventNum = report.AttemptedEventNum;
                    item.LossDayNum = report.LossDayNum;
                }
                else
                {
                    item.PostPersonNum = 0;
                    item.SnapPersonNum = 0;
                    item.ContractorNum = 0;
                    item.SumPersonNum = 0;
                    item.TotalWorkNum = 0;
                    item.SeriousInjuriesNum = 0;
                    item.SeriousInjuriesPersonNum = 0;
                    item.SeriousInjuriesLossHour = 0;
                    item.MinorAccidentNum = 0;
                    item.MinorAccidentPersonNum = 0;
                    item.MinorAccidentLossHour = 0;
                    item.OtherAccidentNum = 0;
                    item.OtherAccidentPersonNum = 0;
                    item.OtherAccidentLossHour = 0;
                    item.RestrictedWorkPersonNum = 0;
                    item.RestrictedWorkLossHour = 0;
                    item.MedicalTreatmentPersonNum = 0;
                    item.MedicalTreatmentLossHour = 0;
                    item.FireNum = 0;
                    item.ExplosionNum = 0;
                    item.TrafficNum = 0;
                    item.EquipmentNum = 0;
                    item.QualityNum = 0;
                    item.OtherNum = 0;
                    item.FirstAidDressingsNum = 0;
                    item.AttemptedEventNum = 0;
                    item.LossDayNum = 0;
                }
                items.Add(item);
            }            
        }
        #endregion

        #region 单位下拉选择事件
        /// <summary>
        /// 单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            items.Clear();
            if (drpUnit.SelectedValue != BLL.Const._Null)
            {
                //GetNewItems();
            }
            Grid1.DataSource = items;
            Grid1.DataBind();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.MillionsMonthlyReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnCopy.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnSaveUp))
                //{
                //    this.btnUpdata.Hidden = false;
                //}
                if (buttonList.Contains(BLL.Const.BtnSubmit))
                {
                    this.btnSubmit.Hidden = false;
                    //this.btnCopy.Hidden = false;
                }
            }
        }
        #endregion

        #region 复制上个月数据
        /// <summary>
        /// 复制上个月的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            DateTime? nowDate = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue);
            if (nowDate.HasValue)
            {
                DateTime showDate = new DateTime();
                showDate = nowDate.Value.AddMonths(-1);
                Model.Information_MillionsMonthlyReport millionsMonthlyReport = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(this.drpUnit.SelectedValue, showDate.Year, showDate.Month);
                if (millionsMonthlyReport != null)
                {
                    Model.Information_MillionsMonthlyReport newMillionsMonthlyReport = new Information_MillionsMonthlyReport();
                    this.MillionsMonthlyReportId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReport));
                    newMillionsMonthlyReport.MillionsMonthlyReportId = this.MillionsMonthlyReportId;
                    newMillionsMonthlyReport.UnitId = this.drpUnit.SelectedValue;
                    newMillionsMonthlyReport.Year = Convert.ToInt32(this.drpYear.SelectedValue);
                    newMillionsMonthlyReport.Month = Convert.ToInt32(this.drpMonth.SelectedValue);
                    newMillionsMonthlyReport.FillingMan = this.CurrUser.UserName;
                    newMillionsMonthlyReport.FillingDate = DateTime.Now;
                    newMillionsMonthlyReport.DutyPerson = this.CurrUser.UserName;
                    newMillionsMonthlyReport.RecordableIncidentRate = millionsMonthlyReport.RecordableIncidentRate;
                    newMillionsMonthlyReport.LostTimeRate = millionsMonthlyReport.LostTimeRate;
                    newMillionsMonthlyReport.LostTimeInjuryRate = millionsMonthlyReport.LostTimeInjuryRate;
                    newMillionsMonthlyReport.DeathAccidentFrequency = millionsMonthlyReport.DeathAccidentFrequency;
                    newMillionsMonthlyReport.AccidentMortality = millionsMonthlyReport.AccidentMortality;
                    newMillionsMonthlyReport.UpState = BLL.Const.UpState_2;
                    newMillionsMonthlyReport.HandleState = BLL.Const.HandleState_1;
                    newMillionsMonthlyReport.HandleMan = this.CurrUser.UserId;
                    BLL.MillionsMonthlyReportService.AddMillionsMonthlyReport(newMillionsMonthlyReport);

                    items = BLL.MillionsMonthlyReportItemService.GetItems(millionsMonthlyReport.MillionsMonthlyReportId);
                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            if (item.Affiliation != "本月合计" || item.Name != "本月合计")
                            {
                                Model.Information_MillionsMonthlyReportItem newItem = new Information_MillionsMonthlyReportItem
                                {
                                    MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                                    MillionsMonthlyReportId = this.MillionsMonthlyReportId,
                                    SortIndex = item.SortIndex,
                                    Affiliation = item.Affiliation,
                                    Name = item.Name,
                                    PostPersonNum = item.PostPersonNum,
                                    SnapPersonNum = item.SnapPersonNum,
                                    ContractorNum = item.ContractorNum,
                                    SumPersonNum = item.SumPersonNum,
                                    TotalWorkNum = item.TotalWorkNum,
                                    SeriousInjuriesNum = item.SeriousInjuriesNum,
                                    SeriousInjuriesPersonNum = item.SeriousInjuriesPersonNum,
                                    SeriousInjuriesLossHour = item.SeriousInjuriesLossHour,
                                    MinorAccidentNum = item.MinorAccidentNum,
                                    MinorAccidentPersonNum = item.MinorAccidentPersonNum,
                                    MinorAccidentLossHour = item.MinorAccidentLossHour,
                                    OtherAccidentNum = item.OtherAccidentNum,
                                    OtherAccidentPersonNum = item.OtherAccidentPersonNum,
                                    OtherAccidentLossHour = item.OtherAccidentLossHour,
                                    RestrictedWorkPersonNum = item.RestrictedWorkPersonNum,
                                    RestrictedWorkLossHour = item.RestrictedWorkLossHour,
                                    MedicalTreatmentPersonNum = item.MedicalTreatmentPersonNum,
                                    MedicalTreatmentLossHour = item.MedicalTreatmentLossHour,
                                    FireNum = item.FireNum,
                                    ExplosionNum = item.ExplosionNum,
                                    TrafficNum = item.TrafficNum,
                                    EquipmentNum = item.EquipmentNum,
                                    QualityNum = item.QualityNum,
                                    OtherNum = item.OtherNum,
                                    FirstAidDressingsNum = item.FirstAidDressingsNum,
                                    AttemptedEventNum = item.AttemptedEventNum,
                                    LossDayNum = item.LossDayNum
                                };
                                BLL.MillionsMonthlyReportItemService.AddMillionsMonthlyReportItem(newItem);
                            }
                        }
                    }

                    GetValues(newMillionsMonthlyReport.MillionsMonthlyReportId);
                }
            }
        }

        /// <summary>
        /// 获取复制的值绑定到文本中
        /// </summary>
        private void GetValues(string millionsMonthlyReportId)
        {
            var report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(millionsMonthlyReportId);
            if (report != null)
            {
                drpMonth.SelectedValue = report.Month.ToString();
                drpYear.SelectedValue = report.Year.ToString();
                drpUnit.SelectedValue = report.UnitId;
                if (report.FillingDate != null)
                {
                    txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", report.FillingDate);
                }
                txtDutyPerson.Text = report.DutyPerson;
                if (report.RecordableIncidentRate != null)
                {
                    txtRecordableIncidentRate.Text = report.RecordableIncidentRate.ToString();
                }
                if (report.LostTimeRate != null)
                {
                    txtLostTimeRate.Text = report.LostTimeRate.ToString();
                }
                if (report.LostTimeInjuryRate != null)
                {
                    txtLostTimeInjuryRate.Text = report.LostTimeInjuryRate.ToString();
                }
                if (report.DeathAccidentFrequency != null)
                {
                    txtDeathAccidentFrequency.Text = report.DeathAccidentFrequency.ToString();
                }
                if (report.AccidentMortality != null)
                {
                    txtAccidentMortality.Text = report.AccidentMortality.ToString();
                }
                items = BLL.MillionsMonthlyReportItemService.GetItems(millionsMonthlyReportId);
                this.Grid1.DataSource = items;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}