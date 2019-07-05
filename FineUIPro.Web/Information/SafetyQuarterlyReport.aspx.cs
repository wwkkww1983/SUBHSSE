using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.IO;

namespace FineUIPro.Web.Information
{
    public partial class SafetyQuarterlyReport : PageBase
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
        /// 附件路径
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
                this.drpQuarter.DataTextField = "ConstText";
                drpQuarter.DataValueField = "ConstValue";
                drpQuarter.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0011);
                drpQuarter.DataBind();
                this.drpYear.DataTextField = "ConstText";
                drpYear.DataValueField = "ConstValue";
                drpYear.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                drpYear.DataBind();
                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;
                DateTime showDate = System.DateTime.Now.AddMonths(-3);
                this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                drpYear.SelectedValue = showDate.Year.ToString();
                GetValue();
            }
        }
        #endregion

        #region 清空Label
        /// <summary>
        /// 清空文本框
        /// </summary>
        private void SetEmpty()
        {
            this.SimpleForm1.Title = string.Empty;
            lblUnitName.Text = string.Empty;
            lblYearId.Text = string.Empty;
            lblQuarters.Text = string.Empty;
            lblHandleMan.Text = string.Empty;
           // this.txtValue.Text = "无数据";
            this.txtValue.Text = HttpUtility.HtmlDecode("无数据");
            this.SimpleForm1.Title = "安全生产数据季报";

            this.fAttach1.Hidden = true;
            this.lbAttachUrl1.Text = string.Empty;
            this.fAttach2.Hidden = true;
            this.lbAttachUrl2.Text = string.Empty;
        }
        #endregion

        #region 获取记录值
        private void GetValue()
        {
            this.SetEmpty();
            int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
            int quarter = Funs.GetNewIntOrZero(drpQuarter.SelectedValue);
            Model.View_Information_SafetyQuarterlyReport safetyQuarterlyReport = Funs.DB.View_Information_SafetyQuarterlyReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.Quarters == quarter && e.YearId == year);
            if (safetyQuarterlyReport != null)
            {
                string upState = string.Empty;
                if (safetyQuarterlyReport.UpState == BLL.Const.UpState_3)
                {
                    upState = "(已上报)";
                }
                else
                {
                    upState = "(未上报)";
                }
                this.SimpleForm1.Title = "安全生产数据季报" + upState;
                this.lblUnitName.Text = safetyQuarterlyReport.UnitName;
                this.lblYearId.Text = safetyQuarterlyReport.YearId.ToString();
                this.lblQuarters.Text = safetyQuarterlyReport.QuartersStr;

                this.GetTxetValue(safetyQuarterlyReport);
                if (safetyQuarterlyReport.HandleState == BLL.Const.HandleState_1 || safetyQuarterlyReport.UpState == BLL.Const.UpState_3)
                {
                    this.lblHandleMan.Hidden = true;
                }
                else
                {
                    this.lblHandleMan.Hidden = false;
                    this.lblHandleMan.Text = "下一步办理人：" + safetyQuarterlyReport.UserName;
                }

                this.fAttach1.Hidden = false;
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.FullTimeManAttachUrl))
                {
                    this.FullTimeManAttachUrl = safetyQuarterlyReport.FullTimeManAttachUrl;
                    this.lbAttachUrl1.Text = safetyQuarterlyReport.FullTimeManAttachUrl.Substring(safetyQuarterlyReport.FullTimeManAttachUrl.IndexOf("~") + 1);
                }

                this.fAttach2.Hidden = false;
                if (!string.IsNullOrEmpty(safetyQuarterlyReport.PMManAttachUrl))
                {
                    this.PMManAttachUrl = safetyQuarterlyReport.PMManAttachUrl;
                    this.lbAttachUrl2.Text = safetyQuarterlyReport.PMManAttachUrl.Substring(safetyQuarterlyReport.PMManAttachUrl.IndexOf("~") + 1);
                }
            }
            else
            {
                SetEmpty();
            }
            this.GetButtonPower();
        }
        
        /// <summary>
        /// 得到值
        /// </summary>
        /// <param name="safetyQuarterlyReport"></param>
        private void GetTxetValue(Model.View_Information_SafetyQuarterlyReport safetyQuarterlyReport)
        {
            this.SafetyQuarterlyReportId = safetyQuarterlyReport.SafetyQuarterlyReportId;
            var unit = BLL.UnitService.GetUnitByUnitId(safetyQuarterlyReport.UnitId);
            string unitTypeName = string.Empty;
            if (unit != null)
            {
                var unitType = BLL.UnitTypeService.GetUnitTypeById(unit.UnitTypeId);
                if (unitType != null)
                {
                    unitTypeName = unitType.UnitTypeName;
                }

            }
            else
            {
                return;
            }
            string textvalue = string.Empty;
            if (!string.IsNullOrEmpty(unitTypeName) && unitTypeName.Contains("施工"))
            {
               textvalue = ("     总投入工时数：" + safetyQuarterlyReport.TotalInWorkHours).PadRight(103, ' ') + "备注：" + safetyQuarterlyReport.TotalInWorkHoursRemark + "\r\n\r\n"
    + ("     总损失工时数：" + safetyQuarterlyReport.TotalOutWorkHours).PadRight(103, ' ') + "备注：" + safetyQuarterlyReport.TotalOutWorkHoursRemark + "\r\n\r\n"
    + ("     百万工时损失率：" + safetyQuarterlyReport.WorkHoursLossRate).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.WorkHoursLossRateRemark + "\r\n\r\n"
    + ("     工时统计准确率：" + safetyQuarterlyReport.WorkHoursAccuracy).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.WorkHoursAccuracyRemark + "\r\n\r\n"
    + ("     主营业务收入/亿元：" + safetyQuarterlyReport.MainBusinessIncome).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.MainBusinessIncomeRemark + "\r\n\r\n"
    + ("     单位工时收入/元：" + safetyQuarterlyReport.UnitTimeIncome).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.UnitTimeIncomeRemark + "\r\n\r\n"
    + ("     百亿产值死亡率：" + safetyQuarterlyReport.BillionsOutputMortality).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.BillionsOutputMortalityRemark + "\r\n\r\n"
    + ("     重大火灾事故报告数：" + safetyQuarterlyReport.MajorFireAccident).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.MajorFireAccidentRemark + "\r\n\r\n"
    + ("     重大机械设备事故报告数：" + safetyQuarterlyReport.MajorEquipAccident).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.MajorEquipAccidentRemark + "\r\n\r\n"
    + ("     事故发生频率（占总收入之比）：" + safetyQuarterlyReport.AccidentFrequency).PadRight(98, ' ') + "备注：" + safetyQuarterlyReport.AccidentFrequencyRemark + "\r\n\r\n"
    + ("     重伤以上事故报告数：" + safetyQuarterlyReport.SeriousInjuryAccident).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.SeriousInjuryAccidentRemark + "\r\n\r\n"
    + ("     火灾事故统计报告数：" + safetyQuarterlyReport.FireAccident).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.FireAccidentRemark + "\r\n\r\n"
    + ("     装备事故统计报告数：" + safetyQuarterlyReport.EquipmentAccident).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.EquipmentAccidentRemark + "\r\n\r\n"
    + ("     中毒及职业伤害报告数：" + safetyQuarterlyReport.PoisoningAndInjuries).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.PoisoningAndInjuriesRemark + "\r\n\r\n"
    + ("     安全生产投入总额/元：" + safetyQuarterlyReport.ProductionSafetyInTotal).PadRight(101, ' ') + "备注：" + safetyQuarterlyReport.ProductionSafetyInTotalRemark + "\r\n\r\n"
    + ("     安全防护投入/元：" + safetyQuarterlyReport.ProtectionInput).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.ProtectionInputRemark + "\r\n\r\n"
    + ("     劳动保护及职业健康投入/元：" + safetyQuarterlyReport.LaboAndHealthIn).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.LaborAndHealthInRemark + "\r\n\r\n"
    + ("     安全技术进步投入/元：" + safetyQuarterlyReport.TechnologyProgressIn).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.TechnologyProgressInRemark + "\r\n\r\n"
    + ("     安全教育培训投入/元：" + safetyQuarterlyReport.EducationTrainIn).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.EducationTrainInRemark + "\r\n\r\n"
    + ("     工程造价占比：" + safetyQuarterlyReport.ProjectCostRate).PadRight(105, ' ') + "备注：" + safetyQuarterlyReport.ProjectCostRateRemark + "\r\n\r\n"
    + ("     百万工时安全生产投入额/万元：" + safetyQuarterlyReport.ProductionInput).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.ProductionInputRemark + "\r\n\r\n"
    + ("     安全生产投入占施工收入之比：" + safetyQuarterlyReport.Revenue).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.RevenueRemark + "\r\n\r\n"
    + ("     安全专职人员总数（附名单）：" + safetyQuarterlyReport.FullTimeMan).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.FullTimeManRemark + "\r\n\r\n"
    + ("     项目经理人员总数（附名单）：" + safetyQuarterlyReport.PMMan).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.PMManRemark + "\r\n\r\n"
    + ("     企业负责人安全生产继续教育数：" + safetyQuarterlyReport.CorporateDirectorEdu).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.CorporateDirectorEduRemark + "\r\n\r\n"
    + ("     项目负责人安全生产继续教育数：" + safetyQuarterlyReport.ProjectLeaderEdu).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.ProjectLeaderEduRemark + "\r\n\r\n"
    + ("     安全专职人员安全生产继续教育数：" + safetyQuarterlyReport.FullTimeEdu).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.FullTimeEduRemark + "\r\n\r\n"
    + ("     安全生产三类人员继续教育覆盖率：" + safetyQuarterlyReport.ThreeKidsEduRate).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.ThreeKidsEduRateRemark + "\r\n\r\n"
    + ("     上行报告(施工现场安全生产动态季报、专项活动总结上报、生产事故按时限上报)履行率：" + safetyQuarterlyReport.UplinReportRate).PadRight(84, ' ') + "备注：" + safetyQuarterlyReport.UplinReportRateRemark + "\r\n\r\n"
    + ("     重点装备总数：" + safetyQuarterlyReport.KeyEquipmentTotal).PadRight(106, ' ') + "备注：" + safetyQuarterlyReport.KeyEquipmentTotalRemark + "\r\n\r\n"
    + ("     重点装备安全控制检查报告数：" + safetyQuarterlyReport.KeyEquipmentReportCount).PadRight(102, ' ') + "备注：" + safetyQuarterlyReport.KeyEquipmentReportCountRemark + "\r\n\r\n"
    + ("     化工界区施工作业项目数：" + safetyQuarterlyReport.ChemicalAreaProjectCount).PadRight(103, ' ') + "备注：" + safetyQuarterlyReport.ChemicalAreaProjectCountRemark + "\r\n\r\n"
    + ("     化工界区施工作业有害介质检测复测覆盖数：" + safetyQuarterlyReport.HarmfulMediumCoverCount).PadRight(98, ' ') + "备注：" + safetyQuarterlyReport.HarmfulMediumCoverCountRemark + "\r\n\r\n"
    + ("     施工作业安全技术交底覆盖率（%）：" + safetyQuarterlyReport.HarmfulMediumCoverRate).PadRight(100, ' ') + "备注：" + safetyQuarterlyReport.HarmfulMediumCoverRateRemark + "\r\n\r\n"
    + ("     备注：" + safetyQuarterlyReport.Remarks).PadRight(100, ' ');              
            }
            else
            {
                textvalue = ("     总投入工时数：" + safetyQuarterlyReport.TotalInWorkHours).PadRight(103, ' ') + "备注：" + safetyQuarterlyReport.TotalInWorkHoursRemark + "\r\n\r\n"
    + ("     总损失工时数：" + safetyQuarterlyReport.TotalOutWorkHours).PadRight(103, ' ') + "备注：" +  safetyQuarterlyReport.TotalOutWorkHoursRemark + "\r\n\r\n"
    + ("     百万工时损失率：" + safetyQuarterlyReport.WorkHoursLossRate).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.WorkHoursLossRateRemark + "\r\n\r\n"
    + ("     工时统计准确率：" + safetyQuarterlyReport.WorkHoursAccuracy).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.WorkHoursAccuracyRemark + "\r\n\r\n"
    + ("     主营业务收入/亿元：" + safetyQuarterlyReport.MainBusinessIncome).PadRight(101, ' ') + "备注：" +  safetyQuarterlyReport.MainBusinessIncomeRemark + "\r\n\r\n"
    + ("     施工收入/亿元：" + safetyQuarterlyReport.ConstructionRevenue).PadRight(103, ' ') + "备注：" +  safetyQuarterlyReport.ConstructionRevenueRemark + "\r\n\r\n"
    + ("     单位工时收入/元：" + safetyQuarterlyReport.UnitTimeIncome).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.UnitTimeIncomeRemark + "\r\n\r\n"
    + ("     百亿产值死亡率：" + safetyQuarterlyReport.BillionsOutputMortality).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.BillionsOutputMortalityRemark + "\r\n\r\n"
    + ("     重大火灾事故报告数：" + safetyQuarterlyReport.MajorFireAccident).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.MajorFireAccidentRemark + "\r\n\r\n"
    + ("     重大机械设备事故报告数：" + safetyQuarterlyReport.MajorEquipAccident).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.MajorEquipAccidentRemark + "\r\n\r\n"
    + ("     事故发生频率（占总收入之比）：" + safetyQuarterlyReport.AccidentFrequency).PadRight(97, ' ') + "备注：" +  safetyQuarterlyReport.AccidentFrequencyRemark + "\r\n\r\n"
    + ("     重伤以上事故报告数：" + safetyQuarterlyReport.SeriousInjuryAccident).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.SeriousInjuryAccidentRemark + "\r\n\r\n"
    + ("     火灾事故统计报告数：" + safetyQuarterlyReport.FireAccident).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.FireAccidentRemark + "\r\n\r\n"
    + ("     装备事故统计报告数：" + safetyQuarterlyReport.EquipmentAccident).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.EquipmentAccidentRemark + "\r\n\r\n"
    + ("     中毒及职业伤害报告数：" + safetyQuarterlyReport.PoisoningAndInjuries).PadRight(99, ' ') + "备注：" +  safetyQuarterlyReport.PoisoningAndInjuriesRemark + "\r\n\r\n"
    + ("     安全生产投入总额/元：" + safetyQuarterlyReport.ProductionSafetyInTotal).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.ProductionSafetyInTotalRemark + "\r\n\r\n"
    + ("     安全防护投入/元：" + safetyQuarterlyReport.ProtectionInput).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.ProtectionInputRemark + "\r\n\r\n"
    + ("     劳动保护及职业健康投入/元：" + safetyQuarterlyReport.LaboAndHealthIn).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.LaborAndHealthInRemark + "\r\n\r\n"
    + ("     安全技术进步投入/元：" + safetyQuarterlyReport.TechnologyProgressIn).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.TechnologyProgressInRemark + "\r\n\r\n"
    + ("     安全教育培训投入/元：" + safetyQuarterlyReport.EducationTrainIn).PadRight(100, ' ') + "备注：" +  safetyQuarterlyReport.EducationTrainInRemark + "\r\n\r\n"
    + ("     工程造价占比：" + safetyQuarterlyReport.ProjectCostRate).PadRight(102, ' ') + "备注：" +  safetyQuarterlyReport.ProjectCostRateRemark + "\r\n\r\n"
    + ("     百万工时安全生产投入额/万元：" + safetyQuarterlyReport.ProductionInput).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.ProductionInputRemark + "\r\n\r\n"
    + ("     安全生产投入占施工收入之比：" + safetyQuarterlyReport.Revenue).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.RevenueRemark + "\r\n\r\n"
    + ("     安全专职人员总数（附名单）：" + safetyQuarterlyReport.FullTimeMan).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.FullTimeManRemark + "\r\n\r\n"
    + ("     项目经理人员总数（附名单）：" + safetyQuarterlyReport.PMMan).PadRight(98, ' ') + "备注：" +  safetyQuarterlyReport.PMManRemark + "\r\n\r\n"
    + ("     企业负责人安全生产继续教育数：" + safetyQuarterlyReport.CorporateDirectorEdu).PadRight(97, ' ') + "备注：" +  safetyQuarterlyReport.CorporateDirectorEduRemark + "\r\n\r\n"
    + ("     项目负责人安全生产继续教育数：" + safetyQuarterlyReport.ProjectLeaderEdu).PadRight(97, ' ') + "备注：" +  safetyQuarterlyReport.ProjectLeaderEduRemark + "\r\n\r\n"
    + ("     安全专职人员安全生产继续教育数：" + safetyQuarterlyReport.FullTimeEdu).PadRight(96, ' ') + "备注：" +  safetyQuarterlyReport.FullTimeEduRemark + "\r\n\r\n"
    + ("     安全生产三类人员继续教育覆盖率：" + safetyQuarterlyReport.ThreeKidsEduRate).PadRight(96, ' ') + "备注：" +  safetyQuarterlyReport.ThreeKidsEduRateRemark + "\r\n\r\n"
    + ("     上行报告(施工现场安全生产动态季报、专项活动总结上报、生产事故按时限上报)履行率：" + safetyQuarterlyReport.UplinReportRate).PadRight(81, ' ') + "备注：" +  safetyQuarterlyReport.UplinReportRateRemark + "\r\n\r\n"
    + ("     备注：" + safetyQuarterlyReport.Remarks).PadRight(100, ' ');
               
            }

            this.txtValue.Text = textvalue; 
        }
        #endregion

        #region 增加、修改、删除、审核、审批、上报、导入事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyQuarterlyReportEdit.aspx?UnitId={0}&&Year={1}&&Quarter={2}", this.CurrUser.UnitId, this.drpYear.SelectedValue, this.drpQuarter.SelectedValue, "编辑 - ")));
        }

        /// <summary>
        /// 弹出编辑框
        /// </summary>
        private void ShowEdit()
        {
            Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report == null)
            {
                Alert.ShowInTop("所选时间无报表记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyQuarterlyReportEdit.aspx?SafetyQuarterlyReportId={0}", report.SafetyQuarterlyReportId, "编辑 - ")));
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }


        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit1_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit2_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安全生产数据季报", (this.lblYearId.Text + "-" + this.lblQuarters.Text));
                BLL.ProjectDataFlowSetService.DeleteFlowSetByDataId(report.SafetyQuarterlyReportId);
                BLL.SafetyQuarterlyReportService.DeleteSafetyQuarterlyReportById(report.SafetyQuarterlyReportId);
                
                SetEmpty();
                this.btnNew.Hidden = false;
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../DataIn/SafetyQuarterlyReportImport.aspx", "导入 - ")));
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭编辑弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }
        
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }

        /// <summary>
        /// 关闭查看审批信息弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {

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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SafetyQuarterlyReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnPrint))
                {
                    this.btnPrint.Hidden = false;
                }
                int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
                int quarter = Funs.GetNewIntOrZero(drpQuarter.SelectedValue);
                var report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(this.drpUnit.SelectedValue, year, quarter);
                this.btnAudit1.Hidden = true;
                this.btnAudit2.Hidden = true;
                this.btnUpdata.Hidden = true;
                if (report != null)
                {
                    this.btnNew.Hidden = true;
                    if (report.HandleMan == this.CurrUser.UserId)   //当前人是下一步办理入
                    {
                        if (report.HandleState == BLL.Const.HandleState_2)
                        {
                            this.btnAudit1.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_3)
                        {
                            this.btnAudit2.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region 单位下拉框联动事件
        /// <summary>
        /// 单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetValue();
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.Information_SafetyQuarterlyReportId, report.SafetyQuarterlyReportId, "", "打印 - ")));
            }
        }
        #endregion

        #region 季度向前/向后
        /// <summary>
        /// 前一季度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBulletLeft_Click(object sender, EventArgs e)
        {
            SetMonthChange("-");
        }

        /// <summary>
        /// 后一季度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulletRight_Click(object sender, EventArgs e)
        {
            SetMonthChange("+");
        }

        /// <summary>
        /// 季度加减变化
        /// </summary>
        /// <param name="type"></param>
        private void SetMonthChange(string type)
        {
            DateTime? nowDate = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + (Funs.GetNewIntOrZero(this.drpQuarter.SelectedValue) * 3).ToString());
            if (nowDate.HasValue)
            {
                DateTime showDate = new DateTime();
                if (type == "+")
                {
                    showDate = nowDate.Value.AddMonths(3);
                }
                else
                {
                    showDate = nowDate.Value.AddMonths(-3);
                }

                this.drpYear.SelectedValue = showDate.Year.ToString();
                this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                ///值变化
                GetValue();
            }
        }
        #endregion

        #region 查看审批信息
        /// <summary>
        /// 查看审批信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("ReportAuditSee.aspx?Id={0}", report.SafetyQuarterlyReportId, "查看 - ")));
            }
            else
            {
                ShowNotify("所选月份无记录！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 附件查看
        /// <summary>
        /// 查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee1_Click(object sender, EventArgs e)
        {
            this.ShowFullAttch(this.FullTimeManAttachUrl);
        }

        /// <summary>
        /// 查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee2_Click(object sender, EventArgs e)
        {
            this.ShowFullAttch(this.PMManAttachUrl);
        }

        /// <summary>
        /// 附件显示
        /// </summary>
        /// <param name="url"></param>
        private void ShowFullAttch(string url)
        {
            string filePath = BLL.Funs.RootPath + url;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {                                
                long fileSize = info.Length;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                System.Web.HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('附件不存在！')", true);
            }
        }
        #endregion       

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            string result = this.txtValue.Text.Trim();
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Replace("<br>", "\r\n");

                string name = this.drpYear.SelectedText + "年" + this.drpQuarter.SelectedText + "安全生产数据季报";
                // 非AJAX回发
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8) + ".txt");
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(result);
                Response.End();
            }
            else
            {
                Alert.ShowInTop("数据为空，无法导出", MessageBoxIcon.Warning);
                return;
            }       
        }
        #endregion
    }
}