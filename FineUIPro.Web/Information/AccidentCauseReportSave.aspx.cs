using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Newtonsoft.Json.Linq;
using System.ServiceModel;

namespace FineUIPro.Web.Information
{
    public partial class AccidentCauseReportSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 报表主键Id
        /// </summary>
        public string AccidentCauseReportId
        {
            get
            {
                return (string)ViewState["AccidentCauseReportId"];
            }
            set
            {
                ViewState["AccidentCauseReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Information_AccidentCauseReportItem> items = new List<Information_AccidentCauseReportItem>();
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
                this.drpMonth.DataTextField = "ConstText";
                drpMonth.DataValueField = "ConstValue";
                drpMonth.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0009);
                drpMonth.DataBind();
                this.drpYear.DataTextField = "ConstText";
                drpYear.DataValueField = "ConstValue";
                drpYear.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                drpYear.DataBind();
                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;
                string year = Request.QueryString["year"];
                string months = Request.QueryString["month"];
                AccidentCauseReportId = Request.QueryString["AccidentCauseReportId"];
                if (!String.IsNullOrEmpty(AccidentCauseReportId))
                {
                    var q = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(AccidentCauseReportId);
                    if (q != null)
                    {
                        this.btnCopy.Hidden = true;
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        if (q.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                        else
                        {
                            if (q.HandleMan == this.CurrUser.UserId)
                            {
                                this.btnSave.Hidden = false;
                                this.btnSubmit.Hidden = false;
                            }
                        }
                        if (q.UpState == BLL.Const.UpState_3)
                        {
                            this.btnSave.Hidden = true;
                            this.btnUpdata.Hidden = true;
                        }
                        drpMonth.SelectedValue = q.Month.ToString();
                        drpYear.SelectedValue = q.Year.ToString();
                        lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
                        lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
                        lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
                        if (DateTime.Now.Month == 1)
                        {
                            lbLastMonth.Text = "(十二月)";
                        }
                        else
                        {
                            int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                            string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (month - 1) select x.ConstText).FirstOrDefault();
                            lbLastMonth.Text = "(" + lastMonth + ")";
                        }
                        drpUnit.SelectedValue = q.UnitId;
                        txtAccidentCauseReportCode.Text = q.AccidentCauseReportCode;
                        if (q.DeathAccident != null)
                        {
                            txtDeathAccident.Text = q.DeathAccident.ToString();
                        }
                        if (q.DeathToll != null)
                        {
                            txtDeathToll.Text = q.DeathToll.ToString();
                        }
                        if (q.InjuredAccident != null)
                        {
                            txtInjuredAccident.Text = q.InjuredAccident.ToString();
                        }
                        if (q.InjuredToll != null)
                        {
                            txtInjuredToll.Text = q.InjuredToll.ToString();
                        }
                        if (q.MinorWoundAccident != null)
                        {
                            txtMinorWoundAccident.Text = q.MinorWoundAccident.ToString();
                        }
                        if (q.MinorWoundToll != null)
                        {
                            txtMinorWoundToll.Text = q.MinorWoundToll.ToString();
                        }
                        if (q.AverageTotalHours != null)
                        {
                            txtAverageTotalHours.Text = q.AverageTotalHours.ToString();
                        }
                        if (q.AverageManHours != null)
                        {
                            txtAverageManHours.Text = q.AverageManHours.ToString();
                        }
                        if (q.TotalLossMan != null)
                        {
                            txtTotalLossMan.Text = q.TotalLossMan.ToString();
                        }
                        if (q.LastMonthLossHoursTotal != null)
                        {
                            txtLastMonthLossHoursTotal.Text = q.LastMonthLossHoursTotal.ToString();
                        }
                        if (q.KnockOffTotal != null)
                        {
                            txtKnockOffTotal.Text = q.KnockOffTotal.ToString();
                        }
                        if (q.DirectLoss != null)
                        {
                            txtDirectLoss.Text = q.DirectLoss.ToString();
                        }
                        if (q.IndirectLosses != null)
                        {
                            txtIndirectLosses.Text = q.IndirectLosses.ToString();
                        }
                        if (q.TotalLoss != null)
                        {
                            txtTotalLoss.Text = q.TotalLoss.ToString();
                        }
                        if (q.TotalLossTime != null)
                        {
                            txtTotalLossTime.Text = q.TotalLossTime.ToString();
                        }
                        items = BLL.AccidentCauseReportItemService.GetItemsNoSum(AccidentCauseReportId);
                        this.Grid1.DataSource = items;
                        this.Grid1.DataBind();
                        txtFillCompanyPersonCharge.Text = q.FillCompanyPersonCharge;
                        if (!string.IsNullOrEmpty(q.TabPeople))
                        {
                            txtTabPeople.Text = q.TabPeople;
                        }
                        else
                        {
                            txtTabPeople.Text = this.CurrUser.UserName;
                        }
                        txtAuditPerson.Text = q.AuditPerson;
                        if (q.FillingDate != null)
                        {
                            txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", q.FillingDate);
                        }
                        else
                        {
                            txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        }
                    }
                }
                else
                {
                    this.btnCopy.Hidden = false;
                    //int month = DateTime.Now.Month;
                    //drpMonth.SelectedValue = drpMonth.SelectedValue = month.ToString();
                    //drpYear.SelectedValue = DateTime.Now.Year.ToString();
                    drpMonth.SelectedValue = months;
                    drpYear.SelectedValue = year;
                    lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
                    lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
                    lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
                    if (DateTime.Now.Month == 1)
                    {
                        lbLastMonth.Text = "(十二月)";
                    }
                    else
                    {
                        string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (Convert.ToInt32(months) - 1) select x.ConstText).FirstOrDefault();
                        lbLastMonth.Text = "(" + lastMonth + ")"; ;
                    }
                    //获取项目报告集合
                    List<Model.InformationProject_AccidentCauseReport> accidentCauseReports = (from x in Funs.DB.InformationProject_AccidentCauseReport where x.Year.ToString() == year && x.Month.ToString() == months && x.States == BLL.Const.State_2 select x).ToList();
                    if (accidentCauseReports.Count > 0)
                    {
                        txtDeathAccident.Text = accidentCauseReports.Sum(x => x.DeathAccident ?? 0).ToString();
                        txtDeathToll.Text = accidentCauseReports.Sum(x => x.DeathToll ?? 0).ToString();
                        txtInjuredAccident.Text = accidentCauseReports.Sum(x => x.InjuredAccident ?? 0).ToString();
                        txtInjuredToll.Text = accidentCauseReports.Sum(x => x.InjuredToll ?? 0).ToString();
                        txtMinorWoundAccident.Text = accidentCauseReports.Sum(x => x.MinorWoundAccident ?? 0).ToString();
                        txtMinorWoundToll.Text = accidentCauseReports.Sum(x => x.MinorWoundToll ?? 0).ToString();
                        txtAverageTotalHours.Text = accidentCauseReports.Sum(x => x.AverageTotalHours ?? 0).ToString();
                        txtAverageManHours.Text = accidentCauseReports.Sum(x => x.AverageManHours ?? 0).ToString();
                        txtTotalLossMan.Text = accidentCauseReports.Sum(x => x.TotalLossMan ?? 0).ToString();
                        txtLastMonthLossHoursTotal.Text = accidentCauseReports.Sum(x => x.LastMonthLossHoursTotal ?? 0).ToString();
                        txtKnockOffTotal.Text = accidentCauseReports.Sum(x => x.KnockOffTotal ?? 0).ToString();
                        txtDirectLoss.Text = accidentCauseReports.Sum(x => x.DirectLoss ?? 0).ToString();
                        txtIndirectLosses.Text = accidentCauseReports.Sum(x => x.IndirectLosses ?? 0).ToString();
                        txtTotalLoss.Text = accidentCauseReports.Sum(x => x.TotalLoss ?? 0).ToString();
                        txtTotalLossTime.Text = accidentCauseReports.Sum(x => x.TotalLossTime ?? 0).ToString();
                    }
                    List<string> accidentCauseReportIds = accidentCauseReports.Select(x => x.AccidentCauseReportId).ToList();
                    List<Model.InformationProject_AccidentCauseReportItem> projectItems = (from x in Funs.DB.InformationProject_AccidentCauseReportItem where accidentCauseReportIds.Contains(x.AccidentCauseReportId) select x).ToList();
                    var accidentTypes = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_0012);
                    foreach (var a in accidentTypes)
                    {
                        if (a.ConstText != "总计")
                        {
                            Model.Information_AccidentCauseReportItem item = new Model.Information_AccidentCauseReportItem
                            {
                                AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.Information_AccidentCauseReportItem)),
                                AccidentType = a.ConstText
                            };
                            if (projectItems.Count > 0)
                            {
                                item.TotalDeath = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.TotalDeath ?? 0);
                                item.TotalInjuries = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.TotalInjuries ?? 0);
                                item.TotalMinorInjuries = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.TotalMinorInjuries ?? 0);
                                item.Death1 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death1 ?? 0);
                                item.Injuries1 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries1 ?? 0);
                                item.MinorInjuries1 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries1 ?? 0);
                                item.Death2 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death2 ?? 0);
                                item.Injuries2 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries2 ?? 0);
                                item.MinorInjuries2 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries2 ?? 0);
                                item.Death3 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death3 ?? 0);
                                item.Injuries3 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries3 ?? 0);
                                item.MinorInjuries3 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries3 ?? 0);
                                item.Death4 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death4 ?? 0);
                                item.Injuries4 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries4 ?? 0);
                                item.MinorInjuries4 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries4 ?? 0);
                                item.Death5 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death5 ?? 0);
                                item.Injuries5 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries5 ?? 0);
                                item.MinorInjuries5 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries5 ?? 0);
                                item.Death6 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death6 ?? 0);
                                item.Injuries6 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries6 ?? 0);
                                item.MinorInjuries6 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries6 ?? 0);
                                item.Death7 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death7 ?? 0);
                                item.Injuries7 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries7 ?? 0);
                                item.MinorInjuries7 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries7 ?? 0);
                                item.Death8 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death8 ?? 0);
                                item.Injuries8 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries8 ?? 0);
                                item.MinorInjuries8 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries8 ?? 0);
                                item.Death9 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death9 ?? 0);
                                item.Injuries9 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries9 ?? 0);
                                item.MinorInjuries9 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries9 ?? 0);
                                item.Death10 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death10 ?? 0);
                                item.Injuries10 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries10 ?? 0);
                                item.MinorInjuries10 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries10 ?? 0);
                                item.Death11 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Death11 ?? 0);
                                item.Injuries11 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.Injuries11 ?? 0);
                                item.MinorInjuries11 = projectItems.Where(x => x.AccidentType == item.AccidentType).Sum(x => x.MinorInjuries11 ?? 0);
                            }
                            items.Add(item);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                    txtTabPeople.Text = this.CurrUser.UserName;
                    txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(AccidentCauseReportId);
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
        /// 保存
        /// </summary>
        /// <param name="type"></param>
        private void Save(string type)
        {
            Model.Information_AccidentCauseReport accidentCauseReport = new Information_AccidentCauseReport
            {
                //string accidentCauseReportId = Request.QueryString["AccidentCauseReportId"];
                UnitId = drpUnit.SelectedValue,
                AccidentCauseReportCode = txtAccidentCauseReportCode.Text.Trim(),
                Year = Funs.GetNewIntOrZero(drpYear.SelectedValue),
                Month = Funs.GetNewIntOrZero(drpMonth.SelectedValue)
            };
            if (!string.IsNullOrEmpty(txtDeathAccident.Text.Trim()))
            {
                accidentCauseReport.DeathAccident = Funs.GetNewIntOrZero(txtDeathAccident.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDeathToll.Text.Trim()))
            {
                accidentCauseReport.DeathToll = Funs.GetNewIntOrZero(txtDeathToll.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtInjuredAccident.Text.Trim()))
            {
                accidentCauseReport.InjuredAccident = Funs.GetNewIntOrZero(txtInjuredAccident.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtInjuredToll.Text.Trim()))
            {
                accidentCauseReport.InjuredToll = Funs.GetNewIntOrZero(txtInjuredToll.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMinorWoundAccident.Text.Trim()))
            {
                accidentCauseReport.MinorWoundAccident = Funs.GetNewIntOrZero(txtMinorWoundAccident.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtMinorWoundToll.Text.Trim()))
            {
                accidentCauseReport.MinorWoundToll = Funs.GetNewIntOrZero(txtMinorWoundToll.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAverageTotalHours.Text.Trim()))
            {
                accidentCauseReport.AverageTotalHours = Funs.GetNewDecimalOrZero(txtAverageTotalHours.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAverageManHours.Text.Trim()))
            {
                accidentCauseReport.AverageManHours = Funs.GetNewIntOrZero(txtAverageManHours.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTotalLossMan.Text.Trim()))
            {
                accidentCauseReport.TotalLossMan = Funs.GetNewIntOrZero(txtTotalLossMan.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLastMonthLossHoursTotal.Text.Trim()))
            {
                accidentCauseReport.LastMonthLossHoursTotal = Funs.GetNewIntOrZero(txtLastMonthLossHoursTotal.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtKnockOffTotal.Text.Trim()))
            {
                accidentCauseReport.KnockOffTotal = Funs.GetNewIntOrZero(txtKnockOffTotal.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDirectLoss.Text.Trim()))
            {
                accidentCauseReport.DirectLoss = Funs.GetNewIntOrZero(txtDirectLoss.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtIndirectLosses.Text.Trim()))
            {
                accidentCauseReport.IndirectLosses = Funs.GetNewIntOrZero(txtIndirectLosses.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTotalLoss.Text.Trim()))
            {
                accidentCauseReport.TotalLoss = Funs.GetNewIntOrZero(txtTotalLoss.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTotalLossTime.Text.Trim()))
            {
                accidentCauseReport.TotalLossTime = Funs.GetNewIntOrZero(txtTotalLossTime.Text.Trim());
            }
            accidentCauseReport.FillCompanyPersonCharge = txtFillCompanyPersonCharge.Text.Trim();
            accidentCauseReport.TabPeople = txtTabPeople.Text.Trim();
            accidentCauseReport.AuditPerson = txtAuditPerson.Text.Trim();
            if (!string.IsNullOrEmpty(txtFillingDate.Text.Trim()))
            {
                accidentCauseReport.FillingDate = Convert.ToDateTime(txtFillingDate.Text.Trim());
            }
            if (String.IsNullOrEmpty(AccidentCauseReportId))
            {
                Model.Information_AccidentCauseReport old = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdDate(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
                if (old == null)
                {
                    this.AccidentCauseReportId = SQLHelper.GetNewID(typeof(Model.Exchange_Content));
                    accidentCauseReport.AccidentCauseReportId = this.AccidentCauseReportId;
                    accidentCauseReport.UpState = BLL.Const.UpState_2;
                    accidentCauseReport.HandleState = BLL.Const.HandleState_1;
                    accidentCauseReport.HandleMan = this.CurrUser.UserId;
                    BLL.AccidentCauseReportService.AddAccidentCauseReport(accidentCauseReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, accidentCauseReport.AccidentCauseReportCode, accidentCauseReport.AccidentCauseReportId, BLL.Const.AccidentCauseReportMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    ShowNotify("该月份记录已存在！");
                    return;
                }
            }
            else
            {
                Model.Information_AccidentCauseReport oldReport = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(AccidentCauseReportId);
                if (oldReport != null)
                {
                    accidentCauseReport.HandleMan = oldReport.HandleMan;
                    accidentCauseReport.HandleState = oldReport.HandleState;
                }
                accidentCauseReport.AccidentCauseReportId = AccidentCauseReportId;
                accidentCauseReport.UpState = BLL.Const.UpState_2;
                BLL.AccidentCauseReportService.UpdateAccidentCauseReport(accidentCauseReport);
                BLL.LogService.AddSys_Log(this.CurrUser, accidentCauseReport.AccidentCauseReportCode, accidentCauseReport.AccidentCauseReportId,BLL.Const.AccidentCauseReportMenuId,BLL.Const.BtnModify);
                BLL.AccidentCauseReportItemService.DeleteAccidentCauseReportItemByAccidentCauseReportId(AccidentCauseReportId);
            }
            AddItems(accidentCauseReport.AccidentCauseReportId);
            if (type == "updata")     //保存并上报
            {
                Update(accidentCauseReport.AccidentCauseReportId);
            }
            if (type == "submit")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReportSubmit.aspx?Type=AccidentCauseReport&Id={0}", accidentCauseReport.AccidentCauseReportId, "编辑 - ")));
            }
            if (type != "submit")
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save("add");
        }

        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            Save("updata");
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save("submit");
        }

        private void AddItems(string accidentCauseReportId)
        {
            int Death1 = 0, Injuries1 = 0, MinorInjuries1 = 0, Death2 = 0, Injuries2 = 0, MinorInjuries2 = 0, Death3 = 0, Injuries3 = 0, MinorInjuries3 = 0,
                           Death4 = 0, Injuries4 = 0, MinorInjuries4 = 0, Death5 = 0, Injuries5 = 0, MinorInjuries5 = 0, Death6 = 0, Injuries6 = 0, MinorInjuries6 = 0,
                           Death7 = 0, Injuries7 = 0, MinorInjuries7 = 0, Death8 = 0, Injuries8 = 0, MinorInjuries8 = 0, Death9 = 0, Injuries9 = 0, MinorInjuries9 = 0,
                           Death10 = 0, Injuries10 = 0, MinorInjuries10 = 0, Death11 = 0, Injuries11 = 0, MinorInjuries11 = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["AccidentType"].ToString() != "")
                {
                    int sumD = 0, sumI = 0, sumM = 0;
                    Model.Information_AccidentCauseReportItem item = items.FirstOrDefault(x => x.AccidentType == values["AccidentType"].ToString());
                    if (values["Death1"].ToString() != "")
                    {
                        item.Death1 = values.Value<int>("Death1");
                        Death1 += values.Value<int>("Death1");
                        sumD += values.Value<int>("Death1");
                    }
                    if (values["Injuries1"].ToString() != "")
                    {
                        item.Injuries1 = values.Value<int>("Injuries1");
                        Injuries1 += values.Value<int>("Injuries1");
                        sumI += values.Value<int>("Injuries1");
                    }
                    if (values["MinorInjuries1"].ToString() != "")
                    {
                        item.MinorInjuries1 = values.Value<int>("MinorInjuries1");
                        MinorInjuries1 += values.Value<int>("MinorInjuries1");
                        sumM += values.Value<int>("MinorInjuries1");
                    }
                    if (values["Death2"].ToString() != "")
                    {
                        item.Death2 = values.Value<int>("Death2");
                        Death2 += values.Value<int>("Death2");
                        sumD += values.Value<int>("Death2");
                    }
                    if (values["Injuries2"].ToString() != "")
                    {
                        item.Injuries2 = values.Value<int>("Injuries2");
                        Injuries2 += values.Value<int>("Injuries2");
                        sumI += values.Value<int>("Injuries2");
                    }
                    if (values["MinorInjuries2"].ToString() != "")
                    {
                        item.MinorInjuries2 = values.Value<int>("MinorInjuries2");
                        MinorInjuries2 += values.Value<int>("MinorInjuries2");
                        sumM += values.Value<int>("MinorInjuries2");
                    }
                    if (values["Death3"].ToString() != "")
                    {
                        item.Death3 = values.Value<int>("Death3");
                        Death3 += values.Value<int>("Death3");
                        sumD += values.Value<int>("Death3");
                    }
                    if (values["Injuries3"].ToString() != "")
                    {
                        item.Injuries3 = values.Value<int>("Injuries3");
                        Injuries3 += values.Value<int>("Injuries3");
                        sumI += values.Value<int>("Injuries3");
                    }
                    if (values["MinorInjuries3"].ToString() != "")
                    {
                        item.MinorInjuries3 = values.Value<int>("MinorInjuries3");
                        MinorInjuries3 += values.Value<int>("MinorInjuries3");
                        sumM += values.Value<int>("MinorInjuries3");
                    }
                    if (values["Death4"].ToString() != "")
                    {
                        item.Death4 = values.Value<int>("Death4");
                        Death4 += values.Value<int>("Death4");
                        sumD += values.Value<int>("Death4");
                    }
                    if (values["Injuries4"].ToString() != "")
                    {
                        item.Injuries4 = values.Value<int>("Injuries4");
                        Injuries4 += values.Value<int>("Injuries4");
                        sumI += values.Value<int>("Injuries4");
                    }
                    if (values["MinorInjuries4"].ToString() != "")
                    {
                        item.MinorInjuries4 = values.Value<int>("MinorInjuries4");
                        MinorInjuries4 += values.Value<int>("MinorInjuries4");
                        sumM += values.Value<int>("MinorInjuries4");
                    }
                    if (values["Death5"].ToString() != "")
                    {
                        item.Death5 = values.Value<int>("Death5");
                        Death5 += values.Value<int>("Death5");
                        sumD += values.Value<int>("Death5");
                    }
                    if (values["Injuries5"].ToString() != "")
                    {
                        item.Injuries5 = values.Value<int>("Injuries5");
                        Injuries5 += values.Value<int>("Injuries5");
                        sumI += values.Value<int>("Injuries5");
                    }
                    if (values["MinorInjuries5"].ToString() != "")
                    {
                        item.MinorInjuries5 = values.Value<int>("MinorInjuries5");
                        MinorInjuries5 += values.Value<int>("MinorInjuries5");
                        sumM += values.Value<int>("MinorInjuries5");
                    }
                    if (values["Death6"].ToString() != "")
                    {
                        item.Death6 = values.Value<int>("Death6");
                        Death6 += values.Value<int>("Death6");
                        sumD += values.Value<int>("Death6");
                    }
                    if (values["Injuries6"].ToString() != "")
                    {
                        item.Injuries6 = values.Value<int>("Injuries6");
                        Injuries6 += values.Value<int>("Injuries6");
                        sumI += values.Value<int>("Injuries6");
                    }
                    if (values["MinorInjuries6"].ToString() != "")
                    {
                        item.MinorInjuries6 = values.Value<int>("MinorInjuries6");
                        MinorInjuries6 += values.Value<int>("MinorInjuries6");
                        sumM += values.Value<int>("MinorInjuries6");
                    }
                    if (values["Death7"].ToString() != "")
                    {
                        item.Death7 = values.Value<int>("Death7");
                        Death7 += values.Value<int>("Death7");
                        sumD += values.Value<int>("Death7");
                    }
                    if (values["Injuries7"].ToString() != "")
                    {
                        item.Injuries7 = values.Value<int>("Injuries7");
                        Injuries7 += values.Value<int>("Injuries7");
                        sumI += values.Value<int>("Injuries7");
                    }
                    if (values["MinorInjuries7"].ToString() != "")
                    {
                        item.MinorInjuries7 = values.Value<int>("MinorInjuries7");
                        MinorInjuries7 += values.Value<int>("MinorInjuries7");
                        sumM += values.Value<int>("MinorInjuries7");
                    }
                    if (values["Death8"].ToString() != "")
                    {
                        item.Death8 = values.Value<int>("Death8");
                        Death8 += values.Value<int>("Death8");
                        sumD += values.Value<int>("Death8");
                    }
                    if (values["Injuries8"].ToString() != "")
                    {
                        item.Injuries8 = values.Value<int>("Injuries8");
                        Injuries8 += values.Value<int>("Injuries8");
                        sumI += values.Value<int>("Injuries8");
                    }
                    if (values["MinorInjuries8"].ToString() != "")
                    {
                        item.MinorInjuries8 = values.Value<int>("MinorInjuries8");
                        MinorInjuries8 += values.Value<int>("MinorInjuries8");
                        sumM += values.Value<int>("MinorInjuries8");
                    }
                    if (values["Death9"].ToString() != "")
                    {
                        item.Death9 = values.Value<int>("Death9");
                        Death9 += values.Value<int>("Death9");
                        sumD += values.Value<int>("Death9");
                    }
                    if (values["Injuries9"].ToString() != "")
                    {
                        item.Injuries9 = values.Value<int>("Injuries9");
                        Injuries9 += values.Value<int>("Injuries9");
                        sumI += values.Value<int>("Injuries9");
                    }
                    if (values["MinorInjuries9"].ToString() != "")
                    {
                        item.MinorInjuries9 = values.Value<int>("MinorInjuries9");
                        MinorInjuries9 += values.Value<int>("MinorInjuries9");
                        sumM += values.Value<int>("MinorInjuries9");
                    }
                    if (values["Death10"].ToString() != "")
                    {
                        item.Death10 = values.Value<int>("Death10");
                        Death10 += values.Value<int>("Death10");
                        sumD += values.Value<int>("Death10");
                    }
                    if (values["Injuries10"].ToString() != "")
                    {
                        item.Injuries10 = values.Value<int>("Injuries10");
                        Injuries10 += values.Value<int>("Injuries10");
                        sumI += values.Value<int>("Injuries10");
                    }
                    if (values["MinorInjuries10"].ToString() != "")
                    {
                        item.MinorInjuries10 = values.Value<int>("MinorInjuries10");
                        MinorInjuries10 += values.Value<int>("MinorInjuries10");
                        sumM += values.Value<int>("MinorInjuries10");
                    }
                    if (values["Death11"].ToString() != "")
                    {
                        item.Death11 = values.Value<int>("Death11");
                        Death11 += values.Value<int>("Death11");
                        sumD += values.Value<int>("Death11");
                    }
                    if (values["Injuries11"].ToString() != "")
                    {
                        item.Injuries11 = values.Value<int>("Injuries11");
                        Injuries11 += values.Value<int>("Injuries11");
                        sumI += values.Value<int>("Injuries11");
                    }
                    if (values["MinorInjuries11"].ToString() != "")
                    {
                        item.MinorInjuries11 = values.Value<int>("MinorInjuries11");
                        MinorInjuries11 += values.Value<int>("MinorInjuries11");
                        sumM += values.Value<int>("MinorInjuries11");
                    }
                    item.TotalDeath = sumD;
                    item.TotalInjuries = sumI;
                    item.TotalMinorInjuries = sumM;
                }
            }
            Model.Information_AccidentCauseReportItem totalItem = new Model.Information_AccidentCauseReportItem
            {
                AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.Information_AccidentCauseReportItem)),
                AccidentType = "总计",
                TotalDeath = Death1 + Death2 + Death3 + Death4 + Death5 + Death6 + Death7 + Death8 + Death9 + Death10 + Death11,
                TotalInjuries = Injuries1 + Injuries2 + Injuries3 + Injuries4 + Injuries5 + Injuries6 + Injuries7 + Injuries8 + Injuries9 + Injuries10 + Injuries11,
                TotalMinorInjuries = MinorInjuries1 + MinorInjuries2 + MinorInjuries3 + MinorInjuries4 + MinorInjuries5 + MinorInjuries6 + MinorInjuries7 + MinorInjuries8 + MinorInjuries9 + MinorInjuries10 + MinorInjuries11,
                Death1 = Death1,
                Death2 = Death2,
                Death3 = Death3,
                Death4 = Death4,
                Death5 = Death5,
                Death6 = Death6,
                Death7 = Death7,
                Death8 = Death8,
                Death9 = Death9,
                Death10 = Death10,
                Death11 = Death11,
                Injuries1 = Injuries1,
                Injuries2 = Injuries2,
                Injuries3 = Injuries3,
                Injuries4 = Injuries4,
                Injuries5 = Injuries5,
                Injuries6 = Injuries6,
                Injuries7 = Injuries7,
                Injuries8 = Injuries8,
                Injuries9 = Injuries9,
                Injuries10 = Injuries10,
                Injuries11 = Injuries11,
                MinorInjuries1 = MinorInjuries1,
                MinorInjuries2 = MinorInjuries2,
                MinorInjuries3 = MinorInjuries3,
                MinorInjuries4 = MinorInjuries4,
                MinorInjuries5 = MinorInjuries5,
                MinorInjuries6 = MinorInjuries6,
                MinorInjuries7 = MinorInjuries7,
                MinorInjuries8 = MinorInjuries8,
                MinorInjuries9 = MinorInjuries9,
                MinorInjuries10 = MinorInjuries10,
                MinorInjuries11 = MinorInjuries11
            };
            items.Add(totalItem);
            foreach (var item in items)
            {
                item.AccidentCauseReportId = accidentCauseReportId;
                BLL.AccidentCauseReportItemService.AddAccidentCauseReportItem(item);
            }
        }
        #endregion

        #region 月份下拉事件
        /// <summary>
        /// 月份下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
            lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
            lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
            if (drpMonth.SelectedValue == "1")
            {
                lbLastMonth.Text = "(十二月)";
            }
            else
            {
                int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (month - 1) select x.ConstText).FirstOrDefault();
                lbLastMonth.Text = "(" + lastMonth + ")";
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCauseReportMenuId);
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
                }
            }
        }
        #endregion

        #region 数据同步
        private void Update(string accidentCauseReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertInformation_AccidentCauseReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_AccidentCauseReportTableCompletedEventArgs>(poxy_DataInsertInformation_AccidentCauseReportTableCompleted);
            var report = from x in Funs.DB.Information_AccidentCauseReport
                         where x.AccidentCauseReportId == accidentCauseReportId 
                         && x.UpState == BLL.Const.UpState_2
                         select new BLL.HSSEService.Information_AccidentCauseReport
                         {
                             AccidentCauseReportId = x.AccidentCauseReportId,
                             UnitId = x.UnitId,
                             AccidentCauseReportCode = x.AccidentCauseReportCode,
                             Year = x.Year,
                             Month = x.Month,
                             DeathAccident = x.DeathAccident,
                             DeathToll = x.DeathToll,
                             InjuredAccident = x.InjuredAccident,
                             InjuredToll = x.InjuredToll,
                             MinorWoundAccident = x.MinorWoundAccident,
                             MinorWoundToll = x.MinorWoundToll,
                             AverageTotalHours = x.AverageTotalHours,
                             AverageManHours = x.AverageManHours,
                             TotalLossMan = x.TotalLossMan,
                             LastMonthLossHoursTotal = x.LastMonthLossHoursTotal,
                             KnockOffTotal = x.KnockOffTotal,
                             DirectLoss = x.DirectLoss,
                             IndirectLosses = x.IndirectLosses,
                             TotalLoss = x.TotalLoss,
                             TotalLossTime = x.TotalLossTime,
                             FillCompanyPersonCharge = x.FillCompanyPersonCharge,
                             TabPeople = x.TabPeople,
                             AuditPerson = x.AuditPerson,
                             FillingDate = x.FillingDate,
                         };

            var reportItem = from x in Funs.DB.Information_AccidentCauseReportItem
                             where x.AccidentCauseReportId == accidentCauseReportId 
                             select new BLL.HSSEService.Information_AccidentCauseReportItem
                             {
                                 AccidentCauseReportItemId = x.AccidentCauseReportItemId,
                                 AccidentCauseReportId = x.AccidentCauseReportId,
                                 AccidentType = x.AccidentType,
                                 TotalDeath = x.TotalDeath,
                                 TotalInjuries = x.TotalInjuries,
                                 TotalMinorInjuries = x.TotalMinorInjuries,
                                 Death1 = x.Death1,
                                 Injuries1 = x.Injuries1,
                                 MinorInjuries1 = x.MinorInjuries1,
                                 Death2 = x.Death2,
                                 Injuries2 = x.Injuries2,
                                 MinorInjuries2 = x.MinorInjuries2,
                                 Death3 = x.Death3,
                                 Injuries3 = x.Injuries3,
                                 MinorInjuries3 = x.MinorInjuries3,
                                 Death4 = x.Death4,
                                 Injuries4 = x.Injuries4,
                                 MinorInjuries4 = x.MinorInjuries4,
                                 Death5 = x.Death5,
                                 Injuries5 = x.Injuries5,
                                 MinorInjuries5 = x.MinorInjuries5,
                                 Death6 = x.Death6,
                                 Injuries6 = x.Injuries6,
                                 MinorInjuries6 = x.MinorInjuries6,
                                 Death7 = x.Death7,
                                 Injuries7 = x.Injuries7,
                                 MinorInjuries7 = x.MinorInjuries7,
                                 Death8 = x.Death8,
                                 Injuries8 = x.Injuries8,
                                 MinorInjuries8 = x.MinorInjuries8,
                                 Death9 = x.Death9,
                                 Injuries9 = x.Injuries9,
                                 MinorInjuries9 = x.MinorInjuries9,
                                 Death10 = x.Death10,
                                 Injuries10 = x.Injuries10,
                                 MinorInjuries10 = x.MinorInjuries10,
                                 Death11 = x.Death11,
                                 Injuries11 = x.Injuries11,
                                 MinorInjuries11 = x.MinorInjuries11,
                             };
            poxy.DataInsertInformation_AccidentCauseReportTableAsync(report.ToList(), reportItem.ToList());
        }

        #region 职工伤亡事故原因分析报表
        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_AccidentCauseReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_AccidentCauseReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.AccidentCauseReportService.UpdateAccidentCauseReport(report);
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.AccidentCauseReportMenuId, item);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_2 && x.YearId == report.Year.ToString() && x.MonthId == report.Month.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【职工伤亡事故原因分析报表】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.AccidentCauseReportMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【职工伤亡事故原因分析报表】上传到服务器失败；", null, BLL.Const.AccidentCauseReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion

        #endregion

        #region 复制上月数据
        /// <summary>
        /// 复制上月报表数据
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
                Model.Information_AccidentCauseReport accidentCauseReport = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(this.drpUnit.SelectedValue, showDate.Year, showDate.Month);
                if (accidentCauseReport != null)
                {
                    Model.Information_AccidentCauseReport newAccidentCauseReport = new Information_AccidentCauseReport();
                    this.AccidentCauseReportId = SQLHelper.GetNewID(typeof(Model.Information_AccidentCauseReport));
                    newAccidentCauseReport.AccidentCauseReportId = this.AccidentCauseReportId;
                    newAccidentCauseReport.UnitId = this.drpUnit.SelectedValue;
                    newAccidentCauseReport.AccidentCauseReportCode = accidentCauseReport.AccidentCauseReportCode;
                    newAccidentCauseReport.Year = Convert.ToInt32(this.drpYear.SelectedValue);
                    newAccidentCauseReport.Month = Convert.ToInt32(this.drpMonth.SelectedValue);
                    newAccidentCauseReport.DeathAccident = accidentCauseReport.DeathAccident;
                    newAccidentCauseReport.DeathToll = accidentCauseReport.DeathToll;
                    newAccidentCauseReport.InjuredAccident = accidentCauseReport.InjuredAccident;
                    newAccidentCauseReport.InjuredToll = accidentCauseReport.InjuredToll;
                    newAccidentCauseReport.MinorWoundAccident = accidentCauseReport.MinorWoundAccident;
                    newAccidentCauseReport.MinorWoundToll = accidentCauseReport.MinorWoundToll;
                    newAccidentCauseReport.AverageTotalHours = accidentCauseReport.AverageTotalHours;
                    newAccidentCauseReport.AverageManHours = accidentCauseReport.AverageManHours;
                    newAccidentCauseReport.TotalLossMan = accidentCauseReport.TotalLossMan;
                    newAccidentCauseReport.LastMonthLossHoursTotal = accidentCauseReport.LastMonthLossHoursTotal;
                    newAccidentCauseReport.KnockOffTotal = accidentCauseReport.KnockOffTotal;
                    newAccidentCauseReport.DirectLoss = accidentCauseReport.DirectLoss;
                    newAccidentCauseReport.IndirectLosses = accidentCauseReport.IndirectLosses;
                    newAccidentCauseReport.TotalLoss = accidentCauseReport.TotalLoss;
                    newAccidentCauseReport.TotalLossTime = accidentCauseReport.TotalLossTime;
                    newAccidentCauseReport.FillCompanyPersonCharge = accidentCauseReport.FillCompanyPersonCharge;
                    newAccidentCauseReport.TabPeople = accidentCauseReport.TabPeople;
                    newAccidentCauseReport.AuditPerson = accidentCauseReport.AuditPerson;
                    newAccidentCauseReport.FillingDate = DateTime.Now;
                    newAccidentCauseReport.UpState = BLL.Const.UpState_2;
                    newAccidentCauseReport.HandleState = BLL.Const.HandleState_1;
                    newAccidentCauseReport.HandleMan = this.CurrUser.UserId;
                    BLL.AccidentCauseReportService.AddAccidentCauseReport(newAccidentCauseReport);

                    items = BLL.AccidentCauseReportItemService.GetItems(accidentCauseReport.AccidentCauseReportId);
                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            Model.Information_AccidentCauseReportItem newItem = new Information_AccidentCauseReportItem
                            {
                                AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.Information_AccidentCauseReportItem)),
                                AccidentCauseReportId = this.AccidentCauseReportId,
                                AccidentType = item.AccidentType,
                                TotalDeath = item.TotalDeath,
                                TotalInjuries = item.TotalInjuries,
                                TotalMinorInjuries = item.TotalMinorInjuries,
                                Death1 = item.Death1,
                                Injuries1 = item.Injuries1,
                                MinorInjuries1 = item.MinorInjuries1,
                                Death2 = item.Death2,
                                Injuries2 = item.Injuries2,
                                MinorInjuries2 = item.MinorInjuries2,
                                Death3 = item.Death3,
                                Injuries3 = item.Injuries3,
                                MinorInjuries3 = item.MinorInjuries3,
                                Death4 = item.Death4,
                                Injuries4 = item.Injuries4,
                                MinorInjuries4 = item.MinorInjuries4,
                                Death5 = item.Death5,
                                Injuries5 = item.Injuries5,
                                MinorInjuries5 = item.MinorInjuries5,
                                Death6 = item.Death6,
                                Injuries6 = item.Injuries6,
                                MinorInjuries6 = item.MinorInjuries6,
                                Death7 = item.Death7,
                                Injuries7 = item.Injuries7,
                                MinorInjuries7 = item.MinorInjuries7,
                                Death8 = item.Death8,
                                Injuries8 = item.Injuries8,
                                MinorInjuries8 = item.MinorInjuries8,
                                Death9 = item.Death9,
                                Injuries9 = item.Injuries9,
                                MinorInjuries9 = item.MinorInjuries9,
                                Death10 = item.Death10,
                                Injuries10 = item.Injuries10,
                                MinorInjuries10 = item.MinorInjuries10,
                                Death11 = item.Death11,
                                Injuries11 = item.Injuries11,
                                MinorInjuries11 = item.MinorInjuries11
                            };
                            BLL.AccidentCauseReportItemService.AddAccidentCauseReportItem(newItem);
                        }
                    }
                    GetValues(newAccidentCauseReport.AccidentCauseReportId);
                }
            }
        }

        /// <summary>
        /// 获取复制的值加载到页面
        /// </summary>
        private void GetValues(string accidentCauseReportId)
        {
            var q = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(accidentCauseReportId);
            if (q != null)
            {
                drpMonth.SelectedValue = q.Month.ToString();
                drpYear.SelectedValue = q.Year.ToString();
                lbMonth1.Text = "(" + drpMonth.SelectedText + ")";
                lbMonth2.Text = "(" + drpMonth.SelectedText + ")";
                lbMonth3.Text = "(" + drpMonth.SelectedText + ")";
                if (DateTime.Now.Month == 1)
                {
                    lbLastMonth.Text = "(十二月)";
                }
                else
                {
                    int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                    string lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (month - 1) select x.ConstText).FirstOrDefault();
                    lbLastMonth.Text = "(" + lastMonth + ")";
                }
                drpUnit.SelectedValue = q.UnitId;
                txtAccidentCauseReportCode.Text = q.AccidentCauseReportCode;
                if (q.DeathAccident != null)
                {
                    txtDeathAccident.Text = q.DeathAccident.ToString();
                }
                if (q.DeathToll != null)
                {
                    txtDeathToll.Text = q.DeathToll.ToString();
                }
                if (q.InjuredAccident != null)
                {
                    txtInjuredAccident.Text = q.InjuredAccident.ToString();
                }
                if (q.InjuredToll != null)
                {
                    txtInjuredToll.Text = q.InjuredToll.ToString();
                }
                if (q.MinorWoundAccident != null)
                {
                    txtMinorWoundAccident.Text = q.MinorWoundAccident.ToString();
                }
                if (q.MinorWoundToll != null)
                {
                    txtMinorWoundToll.Text = q.MinorWoundToll.ToString();
                }
                if (q.AverageTotalHours != null)
                {
                    txtAverageTotalHours.Text = q.AverageTotalHours.ToString();
                }
                if (q.AverageManHours != null)
                {
                    txtAverageManHours.Text = q.AverageManHours.ToString();
                }
                if (q.TotalLossMan != null)
                {
                    txtTotalLossMan.Text = q.TotalLossMan.ToString();
                }
                if (q.LastMonthLossHoursTotal != null)
                {
                    txtLastMonthLossHoursTotal.Text = q.LastMonthLossHoursTotal.ToString();
                }
                if (q.KnockOffTotal != null)
                {
                    txtKnockOffTotal.Text = q.KnockOffTotal.ToString();
                }
                if (q.DirectLoss != null)
                {
                    txtDirectLoss.Text = q.DirectLoss.ToString();
                }
                if (q.IndirectLosses != null)
                {
                    txtIndirectLosses.Text = q.IndirectLosses.ToString();
                }
                if (q.TotalLoss != null)
                {
                    txtTotalLoss.Text = q.TotalLoss.ToString();
                }
                if (q.TotalLossTime != null)
                {
                    txtTotalLossTime.Text = q.TotalLossTime.ToString();
                }
                items = BLL.AccidentCauseReportItemService.GetItemsNoSum(AccidentCauseReportId);
                this.Grid1.DataSource = items;
                this.Grid1.DataBind();
                txtFillCompanyPersonCharge.Text = q.FillCompanyPersonCharge;
                if (!string.IsNullOrEmpty(q.TabPeople))
                {
                    txtTabPeople.Text = q.TabPeople;
                }
                else
                {
                    txtTabPeople.Text = this.CurrUser.UserName;
                }
                txtAuditPerson.Text = q.AuditPerson;
                if (q.FillingDate != null)
                {
                    txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", q.FillingDate);
                }
                else
                {
                    txtFillingDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                items = BLL.AccidentCauseReportItemService.GetItems(accidentCauseReportId);
                this.Grid1.DataSource = items;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}