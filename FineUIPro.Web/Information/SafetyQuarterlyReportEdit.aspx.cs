using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.Information
{
    public partial class SafetyQuarterlyReportEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyQuarterlyReportId
        {
            get
            {
                return (string)ViewState["SafetyQuarterlyReportId"];
            }
            set
            {
                ViewState["SafetyQuarterlyReportId"] = value;
            }
        }

        /// <summary>
        /// 安全专职人员附件路径
        /// </summary>
        public string FullTimeManAttachUrl
        {
            get
            {
                return (string)ViewState["FullTimeManAttachUrl"];
            }
            set
            {
                ViewState["FullTimeManAttachUrl"] = value;
            }
        }

        /// <summary>
        /// 项目经理人员附件路径
        /// </summary>
        public string PMManAttachUrl
        {
            get
            {
                return (string)ViewState["PMManAttachUrl"];
            }
            set
            {
                ViewState["PMManAttachUrl"] = value;
            }
        }
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
                this.ddlUnitId.DataTextField = "UnitName";
                this.ddlUnitId.DataValueField = "UnitId";
                this.ddlUnitId.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                this.ddlUnitId.DataBind();

                this.ddlYearId.DataTextField = "ConstText";
                ddlYearId.DataValueField = "ConstValue";
                ddlYearId.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                ddlYearId.DataBind();

                this.ddlQuarter.DataTextField = "ConstText";
                ddlQuarter.DataValueField = "ConstValue";
                ddlQuarter.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0011);
                ddlQuarter.DataBind();

                this.ddlUnitId.Readonly = true;
                string unitId = Request.Params["UnitId"];
                string year = Request.QueryString["Year"];
                string quarter = Request.QueryString["Quarter"];
                this.SafetyQuarterlyReportId = Request.Params["SafetyQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.SafetyQuarterlyReportId))
                {
                    var safetyQuarterlyReport = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(this.SafetyQuarterlyReportId);
                    if (safetyQuarterlyReport != null)
                    {
                        this.btnCopy.Hidden = true;
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        if (safetyQuarterlyReport.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                        else
                        {
                            if (safetyQuarterlyReport.HandleMan == this.CurrUser.UserId)
                            {
                                this.btnSave.Hidden = false;
                                this.btnSubmit.Hidden = false;
                            }
                        }
                        if (safetyQuarterlyReport.UpState == BLL.Const.UpState_3)  //已上报
                        {
                            this.btnSave.Hidden = true;
                            this.btnUpdata.Hidden = true;
                        }
                        #region 赋值
                        if (!string.IsNullOrEmpty(safetyQuarterlyReport.UnitId))
                        {
                            this.ddlUnitId.SelectedValue = safetyQuarterlyReport.UnitId;
                        }
                        this.ddlYearId.SelectedValue = safetyQuarterlyReport.YearId.ToString();
                        this.ddlQuarter.SelectedValue = safetyQuarterlyReport.Quarters.ToString();
                        if (safetyQuarterlyReport.TotalInWorkHours != null)
                        {
                            this.txtTotalInWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalInWorkHours);
                        }
                        this.txtTotalInWorkHoursRemark.Text = safetyQuarterlyReport.TotalInWorkHoursRemark;
                        if (safetyQuarterlyReport.TotalOutWorkHours != null)
                        {
                            this.txtTotalOutWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalOutWorkHours);
                        }
                        this.txtTotalOutWorkHoursRemark.Text = safetyQuarterlyReport.TotalOutWorkHoursRemark;
                        if (safetyQuarterlyReport.WorkHoursLossRate != null)
                        {
                            this.txtWorkHoursLossRate.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursLossRate);
                        }
                        this.txtWorkHoursLossRateRemark.Text = safetyQuarterlyReport.WorkHoursLossRateRemark;
                        if (safetyQuarterlyReport.WorkHoursAccuracy != null)
                        {
                            this.txtWorkHoursAccuracy.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursAccuracy);
                        }
                        this.txtWorkHoursAccuracyRemark.Text = safetyQuarterlyReport.WorkHoursAccuracyRemark;
                        if (safetyQuarterlyReport.MainBusinessIncome != null)
                        {
                            this.txtMainBusinessIncome.Text = Convert.ToString(safetyQuarterlyReport.MainBusinessIncome);
                        }
                        this.txtMainBusinessIncomeRemark.Text = safetyQuarterlyReport.MainBusinessIncomeRemark;
                        if (safetyQuarterlyReport.ConstructionRevenue != null)
                        {
                            this.txtConstructionRevenue.Text = Convert.ToString(safetyQuarterlyReport.ConstructionRevenue);
                        }
                        this.txtConstructionRevenueRemark.Text = safetyQuarterlyReport.ConstructionRevenueRemark;
                        if (safetyQuarterlyReport.UnitTimeIncome != null)
                        {
                            this.txtUnitTimeIncome.Text = Convert.ToString(safetyQuarterlyReport.UnitTimeIncome);
                        }
                        this.txtUnitTimeIncomeRemark.Text = safetyQuarterlyReport.UnitTimeIncomeRemark;
                        if (safetyQuarterlyReport.BillionsOutputMortality != null)
                        {
                            this.txtBillionsOutputMortality.Text = Convert.ToString(safetyQuarterlyReport.BillionsOutputMortality);
                        }
                        this.txtBillionsOutputMortalityRemark.Text = safetyQuarterlyReport.BillionsOutputMortalityRemark;
                        if (safetyQuarterlyReport.MajorFireAccident != null)
                        {
                            this.txtMajorFireAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorFireAccident);
                        }
                        this.txtMajorFireAccidentRemark.Text = safetyQuarterlyReport.MajorFireAccidentRemark;
                        if (safetyQuarterlyReport.MajorEquipAccident != null)
                        {
                            this.txtMajorEquipAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorEquipAccident);
                        }
                        this.txtMajorEquipAccidentRemark.Text = safetyQuarterlyReport.MajorEquipAccidentRemark;
                        if (safetyQuarterlyReport.AccidentFrequency != null)
                        {
                            this.txtAccidentFrequency.Text = Convert.ToString(safetyQuarterlyReport.AccidentFrequency);
                        }
                        this.txtAccidentFrequencyRemark.Text = safetyQuarterlyReport.AccidentFrequencyRemark;
                        if (safetyQuarterlyReport.SeriousInjuryAccident != null)
                        {
                            this.txtSeriousInjuryAccident.Text = Convert.ToString(safetyQuarterlyReport.SeriousInjuryAccident);
                        }
                        this.txtSeriousInjuryAccidentRemark.Text = safetyQuarterlyReport.SeriousInjuryAccidentRemark;
                        if (safetyQuarterlyReport.FireAccident != null)
                        {
                            this.txtFireAccident.Text = Convert.ToString(safetyQuarterlyReport.FireAccident);
                        }
                        this.txtFireAccidentRemark.Text = safetyQuarterlyReport.FireAccidentRemark;
                        if (safetyQuarterlyReport.EquipmentAccident != null)
                        {
                            this.txtEquipmentAccident.Text = Convert.ToString(safetyQuarterlyReport.EquipmentAccident);
                        }
                        this.txtEquipmentAccidentRemark.Text = safetyQuarterlyReport.EquipmentAccidentRemark;
                        if (safetyQuarterlyReport.PoisoningAndInjuries != null)
                        {
                            this.txtPoisoningAndInjuries.Text = Convert.ToString(safetyQuarterlyReport.PoisoningAndInjuries);
                        }
                        this.txtPoisoningAndInjuriesRemark.Text = safetyQuarterlyReport.PoisoningAndInjuriesRemark;
                        if (safetyQuarterlyReport.ProductionSafetyInTotal != null)
                        {
                            this.txtProductionSafetyInTotal.Text = Convert.ToString(safetyQuarterlyReport.ProductionSafetyInTotal);
                        }
                        this.txtProductionSafetyInTotalRemark.Text = safetyQuarterlyReport.ProductionSafetyInTotalRemark;
                        if (safetyQuarterlyReport.ProtectionInput != null)
                        {
                            this.txtProtectionInput.Text = Convert.ToString(safetyQuarterlyReport.ProtectionInput);
                        }
                        this.txtProtectionInputRemark.Text = safetyQuarterlyReport.ProtectionInputRemark;
                        if (safetyQuarterlyReport.LaboAndHealthIn != null)
                        {
                            this.txtLaboAndHealthIn.Text = Convert.ToString(safetyQuarterlyReport.LaboAndHealthIn);
                        }
                        this.txtLaboAndHealthInRemark.Text = safetyQuarterlyReport.LaborAndHealthInRemark;
                        if (safetyQuarterlyReport.TechnologyProgressIn != null)
                        {
                            this.txtTechnologyProgressIn.Text = Convert.ToString(safetyQuarterlyReport.TechnologyProgressIn);
                        }
                        this.txtTechnologyProgressInRemark.Text = safetyQuarterlyReport.TechnologyProgressInRemark;
                        if (safetyQuarterlyReport.EducationTrainIn != null)
                        {
                            this.txtEducationTrainIn.Text = Convert.ToString(safetyQuarterlyReport.EducationTrainIn);
                        }
                        this.txtEducationTrainInRemark.Text = safetyQuarterlyReport.EducationTrainInRemark;
                        if (safetyQuarterlyReport.ProjectCostRate != null)
                        {
                            this.txtProjectCostRate.Text = Convert.ToString(safetyQuarterlyReport.ProjectCostRate);
                        }
                        this.txtProjectCostRateRemark.Text = safetyQuarterlyReport.ProjectCostRateRemark;
                        if (safetyQuarterlyReport.ProductionInput != null)
                        {
                            this.txtProductionInput.Text = Convert.ToString(safetyQuarterlyReport.ProductionInput);
                        }
                        this.txtProductionInputRemark.Text = safetyQuarterlyReport.ProductionInputRemark;
                        if (safetyQuarterlyReport.Revenue != null)
                        {
                            this.txtRevenue.Text = Convert.ToString(safetyQuarterlyReport.Revenue);
                        }
                        this.txtRevenueRemark.Text = safetyQuarterlyReport.RevenueRemark;
                        if (safetyQuarterlyReport.FullTimeMan != null)
                        {
                            this.txtFullTimeMan.Text = Convert.ToString(safetyQuarterlyReport.FullTimeMan);
                        }
                        this.txtFullTimeManRemark.Text = safetyQuarterlyReport.FullTimeManRemark;
                        if (!string.IsNullOrEmpty(safetyQuarterlyReport.FullTimeManAttachUrl))
                        {
                            this.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                            this.lbFullTimeManAttachUrl.Text = safetyQuarterlyReport.FullTimeManAttachUrl.Substring(safetyQuarterlyReport.FullTimeManAttachUrl.IndexOf("~") + 1);
                        }
                        if (safetyQuarterlyReport.PMMan != null)
                        {
                            this.txtPMMan.Text = Convert.ToString(safetyQuarterlyReport.PMMan);
                        }
                        this.txtPMManRemark.Text = safetyQuarterlyReport.PMManRemark;
                        if (!string.IsNullOrEmpty(safetyQuarterlyReport.PMManAttachUrl))
                        {
                            this.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                            this.lbPMManAttachUrl.Text = safetyQuarterlyReport.PMManAttachUrl.Substring(safetyQuarterlyReport.PMManAttachUrl.IndexOf("~") + 1);
                        }
                        if (safetyQuarterlyReport.CorporateDirectorEdu != null)
                        {
                            this.txtCorporateDirectorEdu.Text = Convert.ToString(safetyQuarterlyReport.CorporateDirectorEdu);
                        }
                        this.txtCorporateDirectorEduRemark.Text = safetyQuarterlyReport.CorporateDirectorEduRemark;
                        if (safetyQuarterlyReport.ProjectLeaderEdu != null)
                        {
                            this.txtProjectLeaderEdu.Text = Convert.ToString(safetyQuarterlyReport.ProjectLeaderEdu);
                        }
                        this.txtProjectLeaderEduRemark.Text = safetyQuarterlyReport.ProjectLeaderEduRemark;
                        if (safetyQuarterlyReport.FullTimeEdu != null)
                        {
                            this.txtFullTimeEdu.Text = Convert.ToString(safetyQuarterlyReport.FullTimeEdu);
                        }
                        this.txtFullTimeEduRemark.Text = safetyQuarterlyReport.FullTimeEduRemark;
                        if (safetyQuarterlyReport.ThreeKidsEduRate != null)
                        {
                            this.txtThreeKidsEduRate.Text = Convert.ToString(safetyQuarterlyReport.ThreeKidsEduRate);
                        }
                        this.txtThreeKidsEduRateRemark.Text = safetyQuarterlyReport.ThreeKidsEduRateRemark;
                        if (safetyQuarterlyReport.UplinReportRate != null)
                        {
                            this.txtUplinReportRate.Text = Convert.ToString(safetyQuarterlyReport.UplinReportRate);
                        }
                        this.txtUplinReportRateRemark.Text = safetyQuarterlyReport.UplinReportRateRemark;
                        this.txtRemark.Text = safetyQuarterlyReport.Remarks;
                        if (safetyQuarterlyReport.KeyEquipmentTotal != null)
                        {
                            this.txtKeyEquipmentTotal.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentTotal);
                        }
                        this.txtKeyEquipmentTotalRemark.Text = safetyQuarterlyReport.KeyEquipmentTotalRemark;
                        if (safetyQuarterlyReport.KeyEquipmentReportCount != null)
                        {
                            this.txtKeyEquipmentReportCount.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentReportCount);
                        }
                        this.txtKeyEquipmentReportCountRemark.Text = safetyQuarterlyReport.KeyEquipmentReportCountRemark;
                        if (safetyQuarterlyReport.ChemicalAreaProjectCount != null)
                        {
                            this.txtChemicalAreaProjectCount.Text = Convert.ToString(safetyQuarterlyReport.ChemicalAreaProjectCount);
                        }
                        this.txtChemicalAreaProjectCountRemark.Text = safetyQuarterlyReport.ChemicalAreaProjectCountRemark;
                        if (safetyQuarterlyReport.HarmfulMediumCoverCount != null)
                        {
                            this.txtHarmfulMediumCoverCount.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverCount);
                        }
                        this.txtHarmfulMediumCoverCountRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverCountRemark;
                        if (safetyQuarterlyReport.HarmfulMediumCoverRate != null)
                        {
                            this.txtHarmfulMediumCoverRate.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverRate);
                        }
                        this.txtHarmfulMediumCoverRateRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverRateRemark;
                        #endregion
                    }
                }
                else
                {
                    this.btnCopy.Hidden = false;
                    this.ddlUnitId.SelectedValue = unitId;
                    this.ddlYearId.SelectedValue = year;
                    //int quarters = Funs.GetNowQuarterlyByTime(Convert.ToDateTime(DateTime.Now));
                    //if (quarters != null)
                    //{
                    this.ddlQuarter.SelectedValue = quarter;
                    //}
                    //获取项目报告集合
                    List<Model.InformationProject_SafetyQuarterlyReport> safetyQuarterlyReports = (from x in Funs.DB.InformationProject_SafetyQuarterlyReport where x.YearId.ToString() == year && x.Quarters.ToString() == quarter && x.States == BLL.Const.State_2 select x).ToList();
                    if (safetyQuarterlyReports.Count > 0)
                    {
                        decimal sumPersonWorkTimeTotal = 0;
                        sumPersonWorkTimeTotal=safetyQuarterlyReports.Sum(x => x.TotalInWorkHours ?? 0);
                        this.txtTotalInWorkHours.Text = sumPersonWorkTimeTotal.ToString();
                        this.txtTotalOutWorkHours.Text = safetyQuarterlyReports.Sum(x => x.TotalOutWorkHours ?? 0).ToString();
                        decimal totalOutWorkHours = Funs.GetNewDecimalOrZero(this.txtTotalOutWorkHours.Text.Trim());
                        if (sumPersonWorkTimeTotal != 0)
                        {
                            this.txtWorkHoursLossRate.Text = decimal.Round((totalOutWorkHours * 1000000 / sumPersonWorkTimeTotal), 2).ToString();
                        }
                        else
                        {
                            this.txtWorkHoursLossRate.Text = "0";
                        }
                        this.txtMainBusinessIncome.Text = safetyQuarterlyReports.Sum(x => x.MainBusinessIncome ?? 0).ToString();
                        this.txtConstructionRevenue.Text = safetyQuarterlyReports.Sum(x => x.ConstructionRevenue ?? 0).ToString();
                        this.txtUnitTimeIncome.Text = safetyQuarterlyReports.Sum(x => x.UnitTimeIncome ?? 0).ToString();
                        this.txtMajorFireAccident.Text = safetyQuarterlyReports.Sum(x => x.MajorFireAccident ?? 0).ToString();
                        this.txtMajorEquipAccident.Text = safetyQuarterlyReports.Sum(x => x.MajorEquipAccident ?? 0).ToString();
                        this.txtSeriousInjuryAccident.Text = safetyQuarterlyReports.Sum(x => x.SeriousInjuryAccident ?? 0).ToString();
                        this.txtFireAccident.Text = safetyQuarterlyReports.Sum(x => x.FireAccident ?? 0).ToString();
                        this.txtEquipmentAccident.Text = safetyQuarterlyReports.Sum(x => x.EquipmentAccident ?? 0).ToString();
                        this.txtPoisoningAndInjuries.Text = safetyQuarterlyReports.Sum(x => x.PoisoningAndInjuries ?? 0).ToString();
                        this.txtProductionSafetyInTotal.Text = safetyQuarterlyReports.Sum(x => x.ProductionSafetyInTotal ?? 0).ToString();
                        this.txtProtectionInput.Text = safetyQuarterlyReports.Sum(x => x.ProtectionInput ?? 0).ToString();
                        this.txtLaboAndHealthIn.Text = safetyQuarterlyReports.Sum(x => x.LaboAndHealthIn ?? 0).ToString();
                        this.txtTechnologyProgressIn.Text = safetyQuarterlyReports.Sum(x => x.TechnologyProgressIn ?? 0).ToString();
                        this.txtEducationTrainIn.Text = safetyQuarterlyReports.Sum(x => x.EducationTrainIn ?? 0).ToString();
                        this.txtProductionInput.Text = safetyQuarterlyReports.Sum(x => x.ProductionInput ?? 0).ToString();
                        this.txtFullTimeMan.Text = safetyQuarterlyReports.Sum(x => x.FullTimeMan ?? 0).ToString();
                        this.txtPMMan.Text = safetyQuarterlyReports.Sum(x => x.PMMan ?? 0).ToString();
                        this.txtCorporateDirectorEdu.Text = safetyQuarterlyReports.Sum(x => x.CorporateDirectorEdu ?? 0).ToString();
                        this.txtProjectLeaderEdu.Text = safetyQuarterlyReports.Sum(x => x.ProjectLeaderEdu ?? 0).ToString();
                        this.txtFullTimeEdu.Text = safetyQuarterlyReports.Sum(x => x.FullTimeEdu ?? 0).ToString();
                        this.txtKeyEquipmentTotal.Text = safetyQuarterlyReports.Sum(x => x.KeyEquipmentTotal ?? 0).ToString();
                        this.txtKeyEquipmentReportCount.Text = safetyQuarterlyReports.Sum(x => x.KeyEquipmentReportCount ?? 0).ToString();
                        this.txtChemicalAreaProjectCount.Text = safetyQuarterlyReports.Sum(x => x.ChemicalAreaProjectCount ?? 0).ToString();
                        this.txtHarmfulMediumCoverCount.Text = safetyQuarterlyReports.Sum(x => x.HarmfulMediumCoverCount ?? 0).ToString();
                    }
                }
                var unit = BLL.UnitService.GetUnitByUnitId(this.ddlUnitId.SelectedValue);
                if (unit != null)
                {
                    if (!string.IsNullOrEmpty(unit.UnitTypeId))
                    {
                        var unitType = BLL.UnitTypeService.GetUnitTypeById(unit.UnitTypeId);
                        if (unitType != null)
                        {
                            if (unitType.UnitTypeName.Contains("施工"))
                            {
                                this.txtConstructionRevenue.Hidden = true;
                                this.txtConstructionRevenueRemark.Hidden = true;
                                this.txtKeyEquipmentTotal.Hidden = false;
                                this.txtKeyEquipmentTotalRemark.Hidden = false;
                                this.txtKeyEquipmentReportCount.Hidden = false;
                                this.txtKeyEquipmentReportCountRemark.Hidden = false;
                                this.txtChemicalAreaProjectCount.Hidden = false;
                                this.txtChemicalAreaProjectCountRemark.Hidden = false;
                                this.txtHarmfulMediumCoverCount.Hidden = false;
                                this.txtHarmfulMediumCoverCountRemark.Hidden = false;
                                this.txtHarmfulMediumCoverRate.Hidden = false;
                                this.txtHarmfulMediumCoverRateRemark.Hidden = false;
                            }
                            else
                            {
                                this.txtConstructionRevenue.Hidden = false;
                                this.txtConstructionRevenueRemark.Hidden = false;
                                this.txtKeyEquipmentTotal.Hidden = true;
                                this.txtKeyEquipmentTotalRemark.Hidden = true;
                                this.txtKeyEquipmentReportCount.Hidden = true;
                                this.txtKeyEquipmentReportCountRemark.Hidden = true;
                                this.txtChemicalAreaProjectCount.Hidden = true;
                                this.txtChemicalAreaProjectCountRemark.Hidden = true;
                                this.txtHarmfulMediumCoverCount.Hidden = true;
                                this.txtHarmfulMediumCoverCountRemark.Hidden = true;
                                this.txtHarmfulMediumCoverRate.Hidden = true;
                                this.txtHarmfulMediumCoverRateRemark.Hidden = true;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        private void Save(string type)
        {
            Model.Information_SafetyQuarterlyReport safetyQuarterlyReport = new Model.Information_SafetyQuarterlyReport();
            if (this.ddlUnitId.SelectedValue != "null")
            {
                safetyQuarterlyReport.UnitId = this.ddlUnitId.SelectedValue;
            }
            else
            {
                ShowNotify("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlYearId.SelectedValue != BLL.Const._Null)
            {
                safetyQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            }
            else
            {
                ShowNotify("请选择年度！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlQuarter.SelectedValue != BLL.Const._Null)
            {
                safetyQuarterlyReport.Quarters = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
            }
            else
            {
                ShowNotify("请选择季度！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtTotalInWorkHours.Text))
            {
                try
                {
                    safetyQuarterlyReport.TotalInWorkHours = Convert.ToInt32(this.txtTotalInWorkHours.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【总投入工时数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.TotalInWorkHoursRemark = this.txtTotalInWorkHoursRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtTotalOutWorkHours.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.TotalOutWorkHours = Convert.ToInt32(this.txtTotalOutWorkHours.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【总损失工时数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.TotalOutWorkHoursRemark = this.txtTotalOutWorkHoursRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtWorkHoursLossRate.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.WorkHoursLossRate = Convert.ToDecimal(this.txtWorkHoursLossRate.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【百万工时损失率】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.WorkHoursLossRateRemark = this.txtWorkHoursLossRateRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtWorkHoursAccuracy.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.WorkHoursAccuracy = Convert.ToDecimal(this.txtWorkHoursAccuracy.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【工时统计准确率】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.WorkHoursAccuracyRemark = this.txtWorkHoursAccuracyRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtMainBusinessIncome.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.MainBusinessIncome = Convert.ToDecimal(this.txtMainBusinessIncome.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【主营业务收入/亿元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.MainBusinessIncomeRemark = this.txtMainBusinessIncomeRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtConstructionRevenue.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ConstructionRevenue = Convert.ToDecimal(this.txtConstructionRevenue.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【施工收入/亿元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ConstructionRevenueRemark = this.txtConstructionRevenueRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtUnitTimeIncome.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.UnitTimeIncome = Convert.ToDecimal(this.txtUnitTimeIncome.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【单位工时收入/元】必须是数字！", MessageBoxIcon.Warning);
                }
            }
            safetyQuarterlyReport.UnitTimeIncomeRemark = this.txtUnitTimeIncomeRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtBillionsOutputMortality.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.BillionsOutputMortality = Convert.ToDecimal(this.txtBillionsOutputMortality.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【百亿产值死亡率(%)】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.BillionsOutputMortalityRemark = this.txtBillionsOutputMortalityRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtMajorFireAccident.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.MajorFireAccident = Convert.ToInt32(this.txtMajorFireAccident.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【重大火灾事故报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.MajorFireAccidentRemark = this.txtMajorFireAccidentRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtMajorEquipAccident.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.MajorEquipAccident = Convert.ToInt32(this.txtMajorEquipAccident.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【重大机械设备事故报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.MajorEquipAccidentRemark = this.txtMajorEquipAccidentRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtAccidentFrequency.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.AccidentFrequency = Convert.ToDecimal(this.txtAccidentFrequency.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【事故发生频率（占总收入之比）】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.AccidentFrequencyRemark = this.txtAccidentFrequencyRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtSeriousInjuryAccident.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.SeriousInjuryAccident = Convert.ToInt32(this.txtSeriousInjuryAccident.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【重伤以上事故报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.SeriousInjuryAccidentRemark = this.txtSeriousInjuryAccidentRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtFireAccident.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.FireAccident = Convert.ToInt32(this.txtFireAccident.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【火灾事故统计报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.FireAccidentRemark = this.txtFireAccidentRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtEquipmentAccident.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.EquipmentAccident = Convert.ToInt32(this.txtEquipmentAccident.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【装备事故统计报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.EquipmentAccidentRemark = this.txtEquipmentAccidentRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtPoisoningAndInjuries.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.PoisoningAndInjuries = Convert.ToInt32(this.txtPoisoningAndInjuries.Text);
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【中毒及职业伤害报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.PoisoningAndInjuriesRemark = this.txtPoisoningAndInjuriesRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtProductionSafetyInTotal.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ProductionSafetyInTotal = Convert.ToInt32(this.txtProductionSafetyInTotal.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全生产投入总额/元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ProductionSafetyInTotalRemark = this.txtProductionSafetyInTotalRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtProtectionInput.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ProtectionInput = Convert.ToDecimal(this.txtProtectionInput.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全防护投入/元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ProtectionInputRemark = this.txtProtectionInputRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtLaboAndHealthIn.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.LaboAndHealthIn = Convert.ToDecimal(this.txtLaboAndHealthIn.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【劳动保护及职业健康投入/元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.LaborAndHealthInRemark = this.txtLaboAndHealthInRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtTechnologyProgressIn.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.TechnologyProgressIn = Convert.ToDecimal(this.txtTechnologyProgressIn.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全技术进步投入/元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }

            }
            safetyQuarterlyReport.TechnologyProgressInRemark = this.txtTechnologyProgressInRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtEducationTrainIn.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.EducationTrainIn = Convert.ToDecimal(this.txtEducationTrainIn.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全教育培训投入/元】必须是数字!", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.EducationTrainInRemark = this.txtEducationTrainInRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtProjectCostRate.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ProjectCostRate = Convert.ToDecimal(this.txtProjectCostRate.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确,【工程造价占比（%）】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ProjectCostRateRemark = this.txtProjectCostRateRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtProductionInput.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ProductionInput = Convert.ToDecimal(this.txtProductionInput.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【百万工时安全生产投入额/万元】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ProductionInputRemark = this.txtProductionInputRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtRevenue.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.Revenue = Convert.ToDecimal(this.txtRevenue.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全生产投入占施工收入之比（%）】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.RevenueRemark = this.txtRevenueRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtFullTimeMan.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.FullTimeMan = Convert.ToInt32(this.txtFullTimeMan.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全专职人员总数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.FullTimeManRemark = this.txtFullTimeManRemark.Text;
            safetyQuarterlyReport.FullTimeManAttachUrl = this.FullTimeManAttachUrl;
            if (!string.IsNullOrEmpty(this.txtPMMan.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.PMMan = Convert.ToInt32(this.txtPMMan.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【项目经理人员总数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.PMManRemark = this.txtPMManRemark.Text.Trim();
            safetyQuarterlyReport.PMManAttachUrl = this.PMManAttachUrl;
            if (!string.IsNullOrEmpty(this.txtCorporateDirectorEdu.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.CorporateDirectorEdu = Convert.ToInt32(this.txtCorporateDirectorEdu.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【企业负责人安全生产继续教育数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.CorporateDirectorEduRemark = this.txtCorporateDirectorEduRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtProjectLeaderEdu.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ProjectLeaderEdu = Convert.ToInt32(this.txtProjectLeaderEdu.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【项目负责人安全生产继续教育数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ProjectLeaderEduRemark = this.txtProjectLeaderEduRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtFullTimeEdu.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.FullTimeEdu = Convert.ToInt32(this.txtFullTimeEdu.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全专职人员安全生产继续教育数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.FullTimeEduRemark = this.txtFullTimeEduRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtThreeKidsEduRate.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ThreeKidsEduRate = Convert.ToDecimal(this.txtThreeKidsEduRate.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【安全生产三类人员继续教育覆盖率】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ThreeKidsEduRateRemark = this.txtThreeKidsEduRateRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtUplinReportRate.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.UplinReportRate = Convert.ToDecimal(this.txtUplinReportRate.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【上行报告(施工现场安全生产动态季报、专项活动总结上报、生产事故按时限上报)履行率】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.UplinReportRateRemark = this.txtUplinReportRateRemark.Text.Trim();
            safetyQuarterlyReport.Remarks = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtKeyEquipmentTotal.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.KeyEquipmentTotal = Convert.ToInt32(this.txtKeyEquipmentTotal.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【重点装备总数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.KeyEquipmentTotalRemark = this.txtKeyEquipmentTotalRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtKeyEquipmentReportCount.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.KeyEquipmentReportCount = Convert.ToInt32(this.txtKeyEquipmentReportCount.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【重点装备安全控制检查报告数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.KeyEquipmentReportCountRemark = this.txtKeyEquipmentReportCountRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtChemicalAreaProjectCount.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.ChemicalAreaProjectCount = Convert.ToInt32(this.txtChemicalAreaProjectCount.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【化工界区施工作业项目数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.ChemicalAreaProjectCountRemark = this.txtChemicalAreaProjectCountRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtHarmfulMediumCoverCount.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.HarmfulMediumCoverCount = Convert.ToInt32(this.txtHarmfulMediumCoverCount.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【化工界区施工作业有害介质检测复测覆盖数】必须是整数！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.HarmfulMediumCoverCountRemark = this.txtHarmfulMediumCoverCountRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtHarmfulMediumCoverRate.Text.Trim()))
            {
                try
                {
                    safetyQuarterlyReport.HarmfulMediumCoverRate = Convert.ToDecimal(this.txtHarmfulMediumCoverRate.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入的格式不正确，【施工作业安全技术交底覆盖率（%）】必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            safetyQuarterlyReport.HarmfulMediumCoverRateRemark = this.txtHarmfulMediumCoverRateRemark.Text.Trim();
            if (string.IsNullOrEmpty(this.SafetyQuarterlyReportId))
            {
                var s = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitYearQuarters(this.ddlUnitId.SelectedValue, Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue), Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue));
                if (s != null)
                {
                    ShowNotify("该单位的该年度的该季度安全生产数据季报已经存在，不能重复编制！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    this.SafetyQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.Information_SafetyQuarterlyReport));
                    safetyQuarterlyReport.SafetyQuarterlyReportId = this.SafetyQuarterlyReportId;
                    safetyQuarterlyReport.CompileMan = this.CurrUser.UserName;
                    safetyQuarterlyReport.UpState = BLL.Const.UpState_2;
                    safetyQuarterlyReport.HandleMan = this.CurrUser.UserId;
                    safetyQuarterlyReport.HandleState = BLL.Const.HandleState_1;
                    BLL.SafetyQuarterlyReportService.AddSafetyQuarterlyReport(safetyQuarterlyReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, (this.ddlYearId.SelectedText + "-" + this.ddlQuarter.SelectedText), safetyQuarterlyReport.SafetyQuarterlyReportId,BLL.Const.SafetyQuarterlyReportMenuId,BLL.Const.BtnAdd);
                }
            }
            else
            {
                Model.Information_SafetyQuarterlyReport oldReport = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(this.SafetyQuarterlyReportId);
                if (oldReport != null)
                {
                    safetyQuarterlyReport.HandleMan = oldReport.HandleMan;
                    safetyQuarterlyReport.HandleState = oldReport.HandleState;
                }
                safetyQuarterlyReport.SafetyQuarterlyReportId = this.SafetyQuarterlyReportId;
                safetyQuarterlyReport.UpState = BLL.Const.UpState_2;
                BLL.SafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(safetyQuarterlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, (this.ddlYearId.SelectedText + "-" + this.ddlQuarter.SelectedText), safetyQuarterlyReport.SafetyQuarterlyReportId, BLL.Const.SafetyQuarterlyReportMenuId, BLL.Const.BtnModify);
            }
            if (type == "updata")     //保存并上报
            {
                Update(safetyQuarterlyReport.SafetyQuarterlyReportId);
            }
            if (type == "submit")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReportSubmit.aspx?Type=SafetyQuarterlyReport&Id={0}", safetyQuarterlyReport.SafetyQuarterlyReportId, "编辑 - ")));
            }
            if (type != "submit")
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region 上传到集团公司
        private void Update(string safetyQuarterlyReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertInformation_SafetyQuarterlyReportTableCompleted += new EventHandler<HSSEService.DataInsertInformation_SafetyQuarterlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_SafetyQuarterlyReportTableCompleted);
            var report = from x in Funs.DB.Information_SafetyQuarterlyReport
                         where x.SafetyQuarterlyReportId == safetyQuarterlyReportId && x.UpState == BLL.Const.UpState_2
                         select new HSSEService.Information_SafetyQuarterlyReport
                         {
                             SafetyQuarterlyReportId = x.SafetyQuarterlyReportId,
                             UnitId = x.UnitId,
                             YearId = x.YearId,
                             Quarters = x.Quarters,
                             TotalInWorkHours = x.TotalInWorkHours,
                             TotalInWorkHoursRemark = x.TotalInWorkHoursRemark,
                             TotalOutWorkHours = x.TotalOutWorkHours,
                             TotalOutWorkHoursRemark = x.TotalOutWorkHoursRemark,
                             WorkHoursLossRate = x.WorkHoursLossRate,
                             WorkHoursLossRateRemark = x.WorkHoursLossRateRemark,
                             WorkHoursAccuracy = x.WorkHoursAccuracy,
                             WorkHoursAccuracyRemark = x.WorkHoursAccuracyRemark,
                             MainBusinessIncome = x.MainBusinessIncome,
                             MainBusinessIncomeRemark = x.MainBusinessIncomeRemark,
                             ConstructionRevenue = x.ConstructionRevenue,
                             ConstructionRevenueRemark = x.ConstructionRevenueRemark,
                             UnitTimeIncome = x.UnitTimeIncome,
                             UnitTimeIncomeRemark = x.UnitTimeIncomeRemark,
                             BillionsOutputMortality = x.BillionsOutputMortality,
                             BillionsOutputMortalityRemark = x.BillionsOutputMortalityRemark,
                             MajorFireAccident = x.MajorFireAccident,
                             MajorFireAccidentRemark = x.MajorFireAccidentRemark,
                             MajorEquipAccident = x.MajorEquipAccident,
                             MajorEquipAccidentRemark = x.MajorEquipAccidentRemark,
                             AccidentFrequency = x.AccidentFrequency,
                             AccidentFrequencyRemark = x.AccidentFrequencyRemark,
                             SeriousInjuryAccident = x.SeriousInjuryAccident,
                             SeriousInjuryAccidentRemark = x.SeriousInjuryAccidentRemark,
                             FireAccident = x.FireAccident,
                             FireAccidentRemark = x.FireAccidentRemark,
                             EquipmentAccident = x.EquipmentAccident,
                             EquipmentAccidentRemark = x.EquipmentAccidentRemark,
                             PoisoningAndInjuries = x.PoisoningAndInjuries,
                             PoisoningAndInjuriesRemark = x.PoisoningAndInjuriesRemark,
                             ProductionSafetyInTotal = x.ProductionSafetyInTotal,
                             ProductionSafetyInTotalRemark = x.ProductionSafetyInTotalRemark,
                             ProtectionInput = x.ProtectionInput,
                             ProtectionInputRemark = x.ProtectionInputRemark,
                             LaboAndHealthIn = x.LaboAndHealthIn,
                             LaborAndHealthInRemark = x.LaborAndHealthInRemark,
                             TechnologyProgressIn = x.TechnologyProgressIn,
                             TechnologyProgressInRemark = x.TechnologyProgressInRemark,
                             EducationTrainIn = x.EducationTrainIn,
                             EducationTrainInRemark = x.EducationTrainInRemark,
                             ProjectCostRate = x.ProjectCostRate,
                             ProjectCostRateRemark = x.ProjectCostRateRemark,
                             ProductionInput = x.ProductionInput,
                             ProductionInputRemark = x.ProductionInputRemark,
                             Revenue = x.Revenue,
                             RevenueRemark = x.RevenueRemark,
                             FullTimeMan = x.FullTimeMan,
                             FullTimeManRemark = x.FullTimeManRemark,
                             FullTimeManAttachUrl = x.FullTimeManAttachUrl,
                             PMMan = x.PMMan,
                             PMManRemark = x.PMManRemark,
                             PMManAttachUrl = x.PMManAttachUrl,
                             CorporateDirectorEdu = x.CorporateDirectorEdu,
                             CorporateDirectorEduRemark = x.CorporateDirectorEduRemark,
                             ProjectLeaderEdu = x.ProjectLeaderEdu,
                             ProjectLeaderEduRemark = x.ProjectLeaderEduRemark,
                             FullTimeEdu = x.FullTimeEdu,
                             FullTimeEduRemark = x.FullTimeEduRemark,
                             ThreeKidsEduRate = x.ThreeKidsEduRate,
                             ThreeKidsEduRateRemark = x.ThreeKidsEduRateRemark,
                             UplinReportRate = x.UplinReportRate,
                             UplinReportRateRemark = x.UplinReportRateRemark,
                             Remarks = x.Remarks,
                             CompileMan = x.CompileMan,
                             ////附件转为字节传送
                             FullTimeManAttachUrlFileContext = FileStructService.GetFileStructByAttachUrl(x.FullTimeManAttachUrl),
                             PMManAttachUrlFileContext = FileStructService.GetFileStructByAttachUrl(x.PMManAttachUrl),
                             KeyEquipmentTotal = x.KeyEquipmentTotal,
                             KeyEquipmentTotalRemark = x.KeyEquipmentTotalRemark,
                             KeyEquipmentReportCount = x.KeyEquipmentReportCount,
                             KeyEquipmentReportCountRemark = x.KeyEquipmentReportCountRemark,
                             ChemicalAreaProjectCount = x.ChemicalAreaProjectCount,
                             ChemicalAreaProjectCountRemark = x.ChemicalAreaProjectCountRemark,
                             HarmfulMediumCoverCount = x.HarmfulMediumCoverCount,
                             HarmfulMediumCoverCountRemark = x.HarmfulMediumCoverCountRemark,
                             HarmfulMediumCoverRate = x.HarmfulMediumCoverRate,
                             HarmfulMediumCoverRateRemark = x.HarmfulMediumCoverRateRemark
                         };


            poxy.DataInsertInformation_SafetyQuarterlyReportTableAsync(report.ToList());
        }

        #region 安全生产数据季报
        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_SafetyQuarterlyReportTableCompleted(object sender, HSSEService.DataInsertInformation_SafetyQuarterlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.SafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(report);
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.SafetyQuarterlyReportMenuId, item);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_3 && x.YearId == report.YearId.ToString() && x.QuarterId == report.Quarters.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【安全生产数据季报】上传到服务器" + idList.Count.ToString() + "条数据；",null, BLL.Const.SafetyQuarterlyReportMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {                
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全生产数据季报】上传到服务器失败；", null, BLL.Const.SafetyQuarterlyReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
        #endregion

        #region 上传附件
        /// <summary>
        /// 上传安全专职人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpFullTimeManAttachUrl_Click(object sender, EventArgs e)
        {
            if (fuFullTimeManAttachUrl.HasFile)
            {
                this.lbFullTimeManAttachUrl.Text = fuFullTimeManAttachUrl.ShortFileName;
                if (ValidateFileTypes(this.lbFullTimeManAttachUrl.Text))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.FullTimeManAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuFullTimeManAttachUrl, this.FullTimeManAttachUrl, UploadFileService.SafetyQuarterlyReportFilePath);
                if (string.IsNullOrEmpty(this.FullTimeManAttachUrl))
                {
                    ShowNotify("文件名已经存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ShowNotify("文件上传成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("上传文件不存在！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除安全专职人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteFullTimeManAttachUrl_Click(object sender, EventArgs e)
        {
            this.fuFullTimeManAttachUrl.Reset();
            this.lbFullTimeManAttachUrl.Text = string.Empty;
            this.FullTimeManAttachUrl = string.Empty;
        }

        /// <summary>
        /// 查看安全专职人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeeFullTimeManAttachUrl_Click(object sender, EventArgs e)
        {
            string filePath = BLL.Funs.RootPath + this.FullTimeManAttachUrl;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                Response.Clear();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(filePath, 0, fileSize);
                Response.Flush();
                Response.Close();
                this.SimpleForm1.Reset();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('模板不存在，请联系管理员！')", true);
            }
        }

        /// <summary>
        /// 上传项目经理人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpPMManAttachUrl_Click(object sender, EventArgs e)
        {
            if (fuPMManAttachUrl.HasFile)
            {
                this.lbPMManAttachUrl.Text = fuPMManAttachUrl.ShortFileName;
                if (ValidateFileTypes(this.lbPMManAttachUrl.Text))
                {
                    ShowNotify("无效的文件类型！");
                    return;
                }
                this.PMManAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuPMManAttachUrl, this.PMManAttachUrl, UploadFileService.SafetyQuarterlyReportFilePath);
                if (string.IsNullOrEmpty(this.PMManAttachUrl))
                {
                    ShowNotify("文件名已经存在！");
                    return;
                }
                else
                {
                    ShowNotify("文件上传成功！");
                }
            }
            else
            {
                ShowNotify("上传文件不存在！");
            }
        }

        /// <summary>
        /// 删除项目经理人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeletePMManAttachUrl_Click(object sender, EventArgs e)
        {
            this.fuPMManAttachUrl.Reset();
            this.lbPMManAttachUrl.Text = string.Empty;
            this.PMManAttachUrl = string.Empty;
        }

        /// <summary>
        /// 查看项目经理人员名单附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeePMManAttachUrl_Click(object sender, EventArgs e)
        {
            string filePath = BLL.Funs.RootPath + this.PMManAttachUrl;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                Response.Clear();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(filePath, 0, fileSize);
                Response.Flush();
                Response.Close();
                this.SimpleForm1.Reset();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('附件不存在！')", true);
            }
        }
        #endregion
       
        #region 关闭办理流程窗口
        /// <summary>
        /// 关闭办理流程窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(this.SafetyQuarterlyReportId);
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

        #region 复制上个季度数据
        /// <summary>
        /// 复制上个季度数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            int lastYear = 0, lastQuarter = 0;
            int year = Convert.ToInt32(this.ddlYearId.SelectedValue);
            int quarter = Convert.ToInt32(this.ddlQuarter.SelectedValue);
            if (quarter == 1)
            {
                lastYear = year - 1;
                lastQuarter = 4;
            }
            else
            {
                lastYear = year;
                lastQuarter = quarter - 1;
            }
            Model.Information_SafetyQuarterlyReport safetyQuarterlyReport = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(this.ddlUnitId.SelectedValue, lastYear, lastQuarter);
            if (safetyQuarterlyReport != null)
            {
                Model.Information_SafetyQuarterlyReport newSafetyQuarterlyReport = new Model.Information_SafetyQuarterlyReport();
                this.SafetyQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.Information_SafetyQuarterlyReport));
                newSafetyQuarterlyReport.SafetyQuarterlyReportId = this.SafetyQuarterlyReportId;
                newSafetyQuarterlyReport.UnitId = this.ddlUnitId.SelectedValue;
                newSafetyQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
                newSafetyQuarterlyReport.Quarters = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
                newSafetyQuarterlyReport.TotalInWorkHours = safetyQuarterlyReport.TotalInWorkHours;
                newSafetyQuarterlyReport.TotalInWorkHoursRemark = safetyQuarterlyReport.TotalInWorkHoursRemark;
                newSafetyQuarterlyReport.TotalOutWorkHours = safetyQuarterlyReport.TotalOutWorkHours;
                newSafetyQuarterlyReport.TotalOutWorkHoursRemark = safetyQuarterlyReport.TotalOutWorkHoursRemark;
                newSafetyQuarterlyReport.WorkHoursLossRate = safetyQuarterlyReport.WorkHoursLossRate;
                newSafetyQuarterlyReport.WorkHoursLossRateRemark = safetyQuarterlyReport.WorkHoursLossRateRemark;
                newSafetyQuarterlyReport.WorkHoursAccuracy = safetyQuarterlyReport.WorkHoursAccuracy;
                newSafetyQuarterlyReport.WorkHoursAccuracyRemark = safetyQuarterlyReport.WorkHoursAccuracyRemark;
                newSafetyQuarterlyReport.MainBusinessIncome = safetyQuarterlyReport.MainBusinessIncome;
                newSafetyQuarterlyReport.MainBusinessIncomeRemark = safetyQuarterlyReport.MainBusinessIncomeRemark;
                newSafetyQuarterlyReport.ConstructionRevenue = safetyQuarterlyReport.ConstructionRevenue;
                newSafetyQuarterlyReport.ConstructionRevenueRemark = safetyQuarterlyReport.ConstructionRevenueRemark;
                newSafetyQuarterlyReport.UnitTimeIncome = safetyQuarterlyReport.UnitTimeIncome;
                newSafetyQuarterlyReport.UnitTimeIncomeRemark = safetyQuarterlyReport.UnitTimeIncomeRemark;
                newSafetyQuarterlyReport.BillionsOutputMortality = safetyQuarterlyReport.BillionsOutputMortality;
                newSafetyQuarterlyReport.BillionsOutputMortalityRemark = safetyQuarterlyReport.BillionsOutputMortalityRemark;
                newSafetyQuarterlyReport.MajorFireAccident = safetyQuarterlyReport.MajorFireAccident;
                newSafetyQuarterlyReport.MajorFireAccidentRemark = safetyQuarterlyReport.MajorFireAccidentRemark;
                newSafetyQuarterlyReport.MajorEquipAccident = safetyQuarterlyReport.MajorEquipAccident;
                newSafetyQuarterlyReport.MajorEquipAccidentRemark = safetyQuarterlyReport.MajorEquipAccidentRemark;
                newSafetyQuarterlyReport.AccidentFrequency = safetyQuarterlyReport.AccidentFrequency;
                newSafetyQuarterlyReport.AccidentFrequencyRemark = safetyQuarterlyReport.AccidentFrequencyRemark;
                newSafetyQuarterlyReport.SeriousInjuryAccident = safetyQuarterlyReport.SeriousInjuryAccident;
                newSafetyQuarterlyReport.SeriousInjuryAccidentRemark = safetyQuarterlyReport.SeriousInjuryAccidentRemark;
                newSafetyQuarterlyReport.FireAccident = safetyQuarterlyReport.FireAccident;
                newSafetyQuarterlyReport.FireAccidentRemark = safetyQuarterlyReport.FireAccidentRemark;
                newSafetyQuarterlyReport.EquipmentAccident = safetyQuarterlyReport.EquipmentAccident;
                newSafetyQuarterlyReport.EquipmentAccidentRemark = safetyQuarterlyReport.EquipmentAccidentRemark;
                newSafetyQuarterlyReport.PoisoningAndInjuries = safetyQuarterlyReport.PoisoningAndInjuries;
                newSafetyQuarterlyReport.PoisoningAndInjuriesRemark = safetyQuarterlyReport.PoisoningAndInjuriesRemark;
                newSafetyQuarterlyReport.ProductionSafetyInTotal = safetyQuarterlyReport.ProductionSafetyInTotal;
                newSafetyQuarterlyReport.ProductionSafetyInTotalRemark = safetyQuarterlyReport.ProductionSafetyInTotalRemark;
                newSafetyQuarterlyReport.ProtectionInput = safetyQuarterlyReport.ProtectionInput;
                newSafetyQuarterlyReport.ProtectionInputRemark = safetyQuarterlyReport.ProtectionInputRemark;
                newSafetyQuarterlyReport.LaboAndHealthIn = safetyQuarterlyReport.LaboAndHealthIn;
                newSafetyQuarterlyReport.LaborAndHealthInRemark = safetyQuarterlyReport.LaborAndHealthInRemark;
                newSafetyQuarterlyReport.TechnologyProgressIn = safetyQuarterlyReport.TechnologyProgressIn;
                newSafetyQuarterlyReport.TechnologyProgressInRemark = safetyQuarterlyReport.TechnologyProgressInRemark;
                newSafetyQuarterlyReport.EducationTrainIn = safetyQuarterlyReport.EducationTrainIn;
                newSafetyQuarterlyReport.EducationTrainInRemark = safetyQuarterlyReport.EducationTrainInRemark;
                newSafetyQuarterlyReport.ProjectCostRate = safetyQuarterlyReport.ProjectCostRate;
                newSafetyQuarterlyReport.ProjectCostRateRemark = safetyQuarterlyReport.ProjectCostRateRemark;
                newSafetyQuarterlyReport.ProductionInput = safetyQuarterlyReport.ProductionInput;
                newSafetyQuarterlyReport.ProductionInputRemark = safetyQuarterlyReport.ProductionInputRemark;
                newSafetyQuarterlyReport.Revenue = safetyQuarterlyReport.Revenue;
                newSafetyQuarterlyReport.RevenueRemark = safetyQuarterlyReport.RevenueRemark;
                newSafetyQuarterlyReport.FullTimeMan = safetyQuarterlyReport.FullTimeMan;
                newSafetyQuarterlyReport.FullTimeManRemark = safetyQuarterlyReport.FullTimeManRemark;
                newSafetyQuarterlyReport.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                newSafetyQuarterlyReport.PMMan = safetyQuarterlyReport.PMMan;
                newSafetyQuarterlyReport.PMManRemark = safetyQuarterlyReport.PMManRemark;
                newSafetyQuarterlyReport.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                newSafetyQuarterlyReport.CorporateDirectorEdu = safetyQuarterlyReport.CorporateDirectorEdu;
                newSafetyQuarterlyReport.CorporateDirectorEduRemark = safetyQuarterlyReport.CorporateDirectorEduRemark;
                newSafetyQuarterlyReport.ProjectLeaderEdu = safetyQuarterlyReport.ProjectLeaderEdu;
                newSafetyQuarterlyReport.ProjectLeaderEduRemark = safetyQuarterlyReport.ProjectLeaderEduRemark;
                newSafetyQuarterlyReport.FullTimeEdu = safetyQuarterlyReport.FullTimeEdu;
                newSafetyQuarterlyReport.FullTimeEduRemark = safetyQuarterlyReport.FullTimeEduRemark;
                newSafetyQuarterlyReport.ThreeKidsEduRate = safetyQuarterlyReport.ThreeKidsEduRate;
                newSafetyQuarterlyReport.ThreeKidsEduRateRemark = safetyQuarterlyReport.ThreeKidsEduRateRemark;
                newSafetyQuarterlyReport.UplinReportRate = safetyQuarterlyReport.UplinReportRate;
                newSafetyQuarterlyReport.UplinReportRateRemark = safetyQuarterlyReport.UplinReportRateRemark;
                newSafetyQuarterlyReport.Remarks = safetyQuarterlyReport.Remarks;
                newSafetyQuarterlyReport.FillingDate = DateTime.Now;
                newSafetyQuarterlyReport.CompileMan = this.CurrUser.UserName;
                newSafetyQuarterlyReport.UpState = BLL.Const.UpState_2;
                newSafetyQuarterlyReport.HandleMan = this.CurrUser.UserId;
                newSafetyQuarterlyReport.HandleState = BLL.Const.HandleState_1;
                newSafetyQuarterlyReport.KeyEquipmentTotal = safetyQuarterlyReport.KeyEquipmentTotal;
                newSafetyQuarterlyReport.KeyEquipmentTotalRemark = safetyQuarterlyReport.KeyEquipmentTotalRemark;
                newSafetyQuarterlyReport.KeyEquipmentReportCount = safetyQuarterlyReport.KeyEquipmentReportCount;
                newSafetyQuarterlyReport.KeyEquipmentReportCountRemark = safetyQuarterlyReport.KeyEquipmentReportCountRemark;
                newSafetyQuarterlyReport.ChemicalAreaProjectCount = safetyQuarterlyReport.ChemicalAreaProjectCount;
                newSafetyQuarterlyReport.ChemicalAreaProjectCountRemark = safetyQuarterlyReport.ChemicalAreaProjectCountRemark;
                newSafetyQuarterlyReport.HarmfulMediumCoverCount = safetyQuarterlyReport.HarmfulMediumCoverCount;
                newSafetyQuarterlyReport.HarmfulMediumCoverCountRemark = safetyQuarterlyReport.HarmfulMediumCoverCountRemark;
                newSafetyQuarterlyReport.HarmfulMediumCoverRate = safetyQuarterlyReport.HarmfulMediumCoverRate;
                newSafetyQuarterlyReport.HarmfulMediumCoverRateRemark = safetyQuarterlyReport.HarmfulMediumCoverRateRemark;
                BLL.SafetyQuarterlyReportService.AddSafetyQuarterlyReport(newSafetyQuarterlyReport);

                GetValues(newSafetyQuarterlyReport.SafetyQuarterlyReportId);
            }
        }

        /// <summary>
        /// 赋值
        /// </summary> 
        private void GetValues(string safetyQuarterlyReportId)
        {
            var safetyQuarterlyReport = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(safetyQuarterlyReportId);
            if (safetyQuarterlyReport != null)
            {
                if (safetyQuarterlyReport.TotalInWorkHours != null)
                {
                    this.txtTotalInWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalInWorkHours);
                }
                this.txtTotalInWorkHoursRemark.Text = safetyQuarterlyReport.TotalInWorkHoursRemark;
                if (safetyQuarterlyReport.TotalOutWorkHours != null)
                {
                    this.txtTotalOutWorkHours.Text = Convert.ToString(safetyQuarterlyReport.TotalOutWorkHours);
                }
                this.txtTotalOutWorkHoursRemark.Text = safetyQuarterlyReport.TotalOutWorkHoursRemark;
                if (safetyQuarterlyReport.WorkHoursLossRate != null)
                {
                    this.txtWorkHoursLossRate.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursLossRate);
                }
                this.txtWorkHoursLossRateRemark.Text = safetyQuarterlyReport.WorkHoursLossRateRemark;
                if (safetyQuarterlyReport.WorkHoursAccuracy != null)
                {
                    this.txtWorkHoursAccuracy.Text = Convert.ToString(safetyQuarterlyReport.WorkHoursAccuracy);
                }
                this.txtWorkHoursAccuracyRemark.Text = safetyQuarterlyReport.WorkHoursAccuracyRemark;
                if (safetyQuarterlyReport.MainBusinessIncome != null)
                {
                    this.txtMainBusinessIncome.Text = Convert.ToString(safetyQuarterlyReport.MainBusinessIncome);
                }
                this.txtMainBusinessIncomeRemark.Text = safetyQuarterlyReport.MainBusinessIncomeRemark;
                if (safetyQuarterlyReport.ConstructionRevenue != null)
                {
                    this.txtConstructionRevenue.Text = Convert.ToString(safetyQuarterlyReport.ConstructionRevenue);
                }
                this.txtConstructionRevenueRemark.Text = safetyQuarterlyReport.ConstructionRevenueRemark;
                if (safetyQuarterlyReport.UnitTimeIncome != null)
                {
                    this.txtUnitTimeIncome.Text = Convert.ToString(safetyQuarterlyReport.UnitTimeIncome);
                }
                this.txtUnitTimeIncomeRemark.Text = safetyQuarterlyReport.UnitTimeIncomeRemark;
                if (safetyQuarterlyReport.BillionsOutputMortality != null)
                {
                    this.txtBillionsOutputMortality.Text = Convert.ToString(safetyQuarterlyReport.BillionsOutputMortality);
                }
                this.txtBillionsOutputMortalityRemark.Text = safetyQuarterlyReport.BillionsOutputMortalityRemark;
                if (safetyQuarterlyReport.MajorFireAccident != null)
                {
                    this.txtMajorFireAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorFireAccident);
                }
                this.txtMajorFireAccidentRemark.Text = safetyQuarterlyReport.MajorFireAccidentRemark;
                if (safetyQuarterlyReport.MajorEquipAccident != null)
                {
                    this.txtMajorEquipAccident.Text = Convert.ToString(safetyQuarterlyReport.MajorEquipAccident);
                }
                this.txtMajorEquipAccidentRemark.Text = safetyQuarterlyReport.MajorEquipAccidentRemark;
                if (safetyQuarterlyReport.AccidentFrequency != null)
                {
                    this.txtAccidentFrequency.Text = Convert.ToString(safetyQuarterlyReport.AccidentFrequency);
                }
                this.txtAccidentFrequencyRemark.Text = safetyQuarterlyReport.AccidentFrequencyRemark;
                if (safetyQuarterlyReport.SeriousInjuryAccident != null)
                {
                    this.txtSeriousInjuryAccident.Text = Convert.ToString(safetyQuarterlyReport.SeriousInjuryAccident);
                }
                this.txtSeriousInjuryAccidentRemark.Text = safetyQuarterlyReport.SeriousInjuryAccidentRemark;
                if (safetyQuarterlyReport.FireAccident != null)
                {
                    this.txtFireAccident.Text = Convert.ToString(safetyQuarterlyReport.FireAccident);
                }
                this.txtFireAccidentRemark.Text = safetyQuarterlyReport.FireAccidentRemark;
                if (safetyQuarterlyReport.EquipmentAccident != null)
                {
                    this.txtEquipmentAccident.Text = Convert.ToString(safetyQuarterlyReport.EquipmentAccident);
                }
                this.txtEquipmentAccidentRemark.Text = safetyQuarterlyReport.EquipmentAccidentRemark;
                if (safetyQuarterlyReport.PoisoningAndInjuries != null)
                {
                    this.txtPoisoningAndInjuries.Text = Convert.ToString(safetyQuarterlyReport.PoisoningAndInjuries);
                }
                this.txtPoisoningAndInjuriesRemark.Text = safetyQuarterlyReport.PoisoningAndInjuriesRemark;
                if (safetyQuarterlyReport.ProductionSafetyInTotal != null)
                {
                    this.txtProductionSafetyInTotal.Text = Convert.ToString(safetyQuarterlyReport.ProductionSafetyInTotal);
                }
                this.txtProductionSafetyInTotalRemark.Text = safetyQuarterlyReport.ProductionSafetyInTotalRemark;
                if (safetyQuarterlyReport.ProtectionInput != null)
                {
                    this.txtProtectionInput.Text = Convert.ToString(safetyQuarterlyReport.ProtectionInput);
                }
                this.txtProtectionInputRemark.Text = safetyQuarterlyReport.ProtectionInputRemark;
                if (safetyQuarterlyReport.LaboAndHealthIn != null)
                {
                    this.txtLaboAndHealthIn.Text = Convert.ToString(safetyQuarterlyReport.LaboAndHealthIn);
                }
                this.txtLaboAndHealthInRemark.Text = safetyQuarterlyReport.LaborAndHealthInRemark;
                if (safetyQuarterlyReport.TechnologyProgressIn != null)
                {
                    this.txtTechnologyProgressIn.Text = Convert.ToString(safetyQuarterlyReport.TechnologyProgressIn);
                }
                this.txtTechnologyProgressInRemark.Text = safetyQuarterlyReport.TechnologyProgressInRemark;
                if (safetyQuarterlyReport.EducationTrainIn != null)
                {
                    this.txtEducationTrainIn.Text = Convert.ToString(safetyQuarterlyReport.EducationTrainIn);
                }
                this.txtEducationTrainInRemark.Text = safetyQuarterlyReport.EducationTrainInRemark;
                if (safetyQuarterlyReport.ProjectCostRate != null)
                {
                    this.txtProjectCostRate.Text = Convert.ToString(safetyQuarterlyReport.ProjectCostRate);
                }
                this.txtProjectCostRateRemark.Text = safetyQuarterlyReport.ProjectCostRateRemark;
                if (safetyQuarterlyReport.ProductionInput != null)
                {
                    this.txtProductionInput.Text = Convert.ToString(safetyQuarterlyReport.ProductionInput);
                }
                this.txtProductionInputRemark.Text = safetyQuarterlyReport.ProductionInputRemark;
                if (safetyQuarterlyReport.Revenue != null)
                {
                    this.txtRevenue.Text = Convert.ToString(safetyQuarterlyReport.Revenue);
                }
                this.txtRevenueRemark.Text = safetyQuarterlyReport.RevenueRemark;
                if (safetyQuarterlyReport.FullTimeMan != null)
                {
                    this.txtFullTimeMan.Text = Convert.ToString(safetyQuarterlyReport.FullTimeMan);
                }
                this.txtFullTimeManRemark.Text = safetyQuarterlyReport.FullTimeManRemark;
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.FullTimeManAttachUrl))
                {
                    this.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                    this.lbFullTimeManAttachUrl.Text = safetyQuarterlyReport.FullTimeManAttachUrl.Substring(safetyQuarterlyReport.FullTimeManAttachUrl.IndexOf("~") + 1);
                }
                if (safetyQuarterlyReport.PMMan != null)
                {
                    this.txtPMMan.Text = Convert.ToString(safetyQuarterlyReport.PMMan);
                }
                this.txtPMManRemark.Text = safetyQuarterlyReport.PMManRemark;
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.PMManAttachUrl))
                {
                    this.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                    this.lbPMManAttachUrl.Text = safetyQuarterlyReport.PMManAttachUrl.Substring(safetyQuarterlyReport.PMManAttachUrl.IndexOf("~") + 1);
                }
                if (safetyQuarterlyReport.CorporateDirectorEdu != null)
                {
                    this.txtCorporateDirectorEdu.Text = Convert.ToString(safetyQuarterlyReport.CorporateDirectorEdu);
                }
                this.txtCorporateDirectorEduRemark.Text = safetyQuarterlyReport.CorporateDirectorEduRemark;
                if (safetyQuarterlyReport.ProjectLeaderEdu != null)
                {
                    this.txtProjectLeaderEdu.Text = Convert.ToString(safetyQuarterlyReport.ProjectLeaderEdu);
                }
                this.txtProjectLeaderEduRemark.Text = safetyQuarterlyReport.ProjectLeaderEduRemark;
                if (safetyQuarterlyReport.FullTimeEdu != null)
                {
                    this.txtFullTimeEdu.Text = Convert.ToString(safetyQuarterlyReport.FullTimeEdu);
                }
                this.txtFullTimeEduRemark.Text = safetyQuarterlyReport.FullTimeEduRemark;
                if (safetyQuarterlyReport.ThreeKidsEduRate != null)
                {
                    this.txtThreeKidsEduRate.Text = Convert.ToString(safetyQuarterlyReport.ThreeKidsEduRate);
                }
                this.txtThreeKidsEduRateRemark.Text = safetyQuarterlyReport.ThreeKidsEduRateRemark;
                if (safetyQuarterlyReport.UplinReportRate != null)
                {
                    this.txtUplinReportRate.Text = Convert.ToString(safetyQuarterlyReport.UplinReportRate);
                }
                this.txtUplinReportRateRemark.Text = safetyQuarterlyReport.UplinReportRateRemark;
                this.txtRemark.Text = safetyQuarterlyReport.Remarks;
                if (safetyQuarterlyReport.KeyEquipmentTotal != null)
                {
                    this.txtKeyEquipmentTotal.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentTotal);
                }
                this.txtKeyEquipmentTotalRemark.Text = safetyQuarterlyReport.KeyEquipmentTotalRemark;
                if (safetyQuarterlyReport.KeyEquipmentReportCount != null)
                {
                    this.txtKeyEquipmentReportCount.Text = Convert.ToString(safetyQuarterlyReport.KeyEquipmentReportCount);
                }
                this.txtKeyEquipmentReportCountRemark.Text = safetyQuarterlyReport.KeyEquipmentReportCountRemark;
                if (safetyQuarterlyReport.ChemicalAreaProjectCount != null)
                {
                    this.txtChemicalAreaProjectCount.Text = Convert.ToString(safetyQuarterlyReport.ChemicalAreaProjectCount);
                }
                this.txtChemicalAreaProjectCountRemark.Text = safetyQuarterlyReport.ChemicalAreaProjectCountRemark;
                if (safetyQuarterlyReport.HarmfulMediumCoverCount != null)
                {
                    this.txtHarmfulMediumCoverCount.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverCount);
                }
                this.txtHarmfulMediumCoverCountRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverCountRemark;
                if (safetyQuarterlyReport.HarmfulMediumCoverRate != null)
                {
                    this.txtHarmfulMediumCoverRate.Text = Convert.ToString(safetyQuarterlyReport.HarmfulMediumCoverRate);
                }
                this.txtHarmfulMediumCoverRateRemark.Text = safetyQuarterlyReport.HarmfulMediumCoverRateRemark;
            }
        }
        #endregion
    }
}