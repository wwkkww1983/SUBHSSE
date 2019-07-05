using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.DataIn
{
    public partial class SafetyQuarterlyReportImport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 安全生产数据季报表集合
        /// </summary>
        private static List<Model.Information_SafetyQuarterlyReport> safetyQuarterlyReports = new List<Model.Information_SafetyQuarterlyReport>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static List<Model.ErrorInfo> errorInfos = new List<Model.ErrorInfo>();
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
                this.hdFileName.Text = string.Empty;
                this.hdCheckResult.Text = string.Empty;
                if (safetyQuarterlyReports != null)
                {
                    safetyQuarterlyReports.Clear();
                }
                if (errorInfos != null)
                {
                    errorInfos.Clear();
                }
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuAttachUrl.HasFile == false)
                {
                    ShowNotify("请您选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                string IsXls = Path.GetExtension(this.fuAttachUrl.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    ShowNotify("只可以选择Excel文件！", MessageBoxIcon.Warning);
                    return;
                }
                if (safetyQuarterlyReports != null)
                {
                    safetyQuarterlyReports.Clear();
                }
                if (errorInfos != null)
                {
                    errorInfos.Clear();
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                this.hdFileName.Text = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdFileName.Text;
                this.fuAttachUrl.PostedFile.SaveAs(filePath);
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyQuarterlyReportBar.aspx?FileName={0}", this.hdFileName.Text, "审核 - ")));
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (errorInfos.Count <= 0)
            {
                if (!string.IsNullOrEmpty(this.hdFileName.Text))
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafetyQuarterlyReportBarIn.aspx?FileName={0}", this.hdFileName.Text, "导入 - ")));
                }
                else
                {
                    ShowNotify("请先审核要导入的文件！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (errorInfos.Count <= 0)
            {
                if (Session["safetyQuarterlyReports"] != null)
                {
                    safetyQuarterlyReports = Session["safetyQuarterlyReports"] as List<Model.Information_SafetyQuarterlyReport>;
                }
                int b = safetyQuarterlyReports.Count();
                int c = b;
                for (int i = 0; i < c; i++)
                {
                    Model.Information_SafetyQuarterlyReport report = new Model.Information_SafetyQuarterlyReport
                    {
                        UnitId = safetyQuarterlyReports[i].UnitId,
                        YearId = safetyQuarterlyReports[i].YearId,
                        Quarters = safetyQuarterlyReports[i].Quarters,
                        TotalInWorkHours = safetyQuarterlyReports[i].TotalInWorkHours,
                        TotalInWorkHoursRemark = safetyQuarterlyReports[i].TotalInWorkHoursRemark,
                        TotalOutWorkHours = safetyQuarterlyReports[i].TotalOutWorkHours,
                        TotalOutWorkHoursRemark = safetyQuarterlyReports[i].TotalOutWorkHoursRemark,
                        WorkHoursLossRate = safetyQuarterlyReports[i].WorkHoursLossRate,
                        WorkHoursLossRateRemark = safetyQuarterlyReports[i].WorkHoursLossRateRemark,
                        WorkHoursAccuracy = safetyQuarterlyReports[i].WorkHoursAccuracy,
                        WorkHoursAccuracyRemark = safetyQuarterlyReports[i].WorkHoursAccuracyRemark,
                        MainBusinessIncome = safetyQuarterlyReports[i].MainBusinessIncome,
                        MainBusinessIncomeRemark = safetyQuarterlyReports[i].MainBusinessIncomeRemark,
                        ConstructionRevenue = safetyQuarterlyReports[i].ConstructionRevenue,
                        ConstructionRevenueRemark = safetyQuarterlyReports[i].ConstructionRevenueRemark,
                        UnitTimeIncome = safetyQuarterlyReports[i].UnitTimeIncome,
                        UnitTimeIncomeRemark = safetyQuarterlyReports[i].UnitTimeIncomeRemark,
                        BillionsOutputMortality = safetyQuarterlyReports[i].BillionsOutputMortality,
                        BillionsOutputMortalityRemark = safetyQuarterlyReports[i].BillionsOutputMortalityRemark,
                        MajorFireAccident = safetyQuarterlyReports[i].MajorFireAccident,
                        MajorFireAccidentRemark = safetyQuarterlyReports[i].MajorFireAccidentRemark,
                        MajorEquipAccident = safetyQuarterlyReports[i].MajorEquipAccident,
                        MajorEquipAccidentRemark = safetyQuarterlyReports[i].MajorEquipAccidentRemark,
                        AccidentFrequency = safetyQuarterlyReports[i].AccidentFrequency,
                        AccidentFrequencyRemark = safetyQuarterlyReports[i].AccidentFrequencyRemark,
                        SeriousInjuryAccident = safetyQuarterlyReports[i].SeriousInjuryAccident,
                        SeriousInjuryAccidentRemark = safetyQuarterlyReports[i].SeriousInjuryAccidentRemark,
                        FireAccident = safetyQuarterlyReports[i].FireAccident,
                        FireAccidentRemark = safetyQuarterlyReports[i].FireAccidentRemark,
                        EquipmentAccident = safetyQuarterlyReports[i].EquipmentAccident,
                        EquipmentAccidentRemark = safetyQuarterlyReports[i].EquipmentAccidentRemark,
                        PoisoningAndInjuries = safetyQuarterlyReports[i].PoisoningAndInjuries,
                        PoisoningAndInjuriesRemark = safetyQuarterlyReports[i].PoisoningAndInjuriesRemark,
                        ProductionSafetyInTotal = safetyQuarterlyReports[i].ProductionSafetyInTotal,
                        ProductionSafetyInTotalRemark = safetyQuarterlyReports[i].ProductionSafetyInTotalRemark,
                        ProtectionInput = safetyQuarterlyReports[i].ProtectionInput,
                        ProtectionInputRemark = safetyQuarterlyReports[i].ProtectionInputRemark,
                        LaboAndHealthIn = safetyQuarterlyReports[i].LaboAndHealthIn,
                        LaborAndHealthInRemark = safetyQuarterlyReports[i].LaborAndHealthInRemark,
                        TechnologyProgressIn = safetyQuarterlyReports[i].TechnologyProgressIn,
                        TechnologyProgressInRemark = safetyQuarterlyReports[i].TechnologyProgressInRemark,
                        EducationTrainIn = safetyQuarterlyReports[i].EducationTrainIn,
                        EducationTrainInRemark = safetyQuarterlyReports[i].EducationTrainInRemark,
                        ProjectCostRate = safetyQuarterlyReports[i].ProjectCostRate,
                        ProjectCostRateRemark = safetyQuarterlyReports[i].ProjectCostRateRemark,
                        ProductionInput = safetyQuarterlyReports[i].ProductionInput,
                        ProductionInputRemark = safetyQuarterlyReports[i].ProductionInputRemark,
                        Revenue = safetyQuarterlyReports[i].Revenue,
                        RevenueRemark = safetyQuarterlyReports[i].RevenueRemark,
                        FullTimeMan = safetyQuarterlyReports[i].FullTimeMan,
                        FullTimeManRemark = safetyQuarterlyReports[i].FullTimeManRemark,
                        PMMan = safetyQuarterlyReports[i].PMMan,
                        PMManRemark = safetyQuarterlyReports[i].PMManRemark,
                        CorporateDirectorEdu = safetyQuarterlyReports[i].CorporateDirectorEdu,
                        CorporateDirectorEduRemark = safetyQuarterlyReports[i].CorporateDirectorEduRemark,
                        ProjectLeaderEdu = safetyQuarterlyReports[i].ProjectLeaderEdu,
                        ProjectLeaderEduRemark = safetyQuarterlyReports[i].ProjectLeaderEduRemark,
                        FullTimeEdu = safetyQuarterlyReports[i].FullTimeEdu,
                        FullTimeEduRemark = safetyQuarterlyReports[i].FullTimeEduRemark,
                        ThreeKidsEduRate = safetyQuarterlyReports[i].ThreeKidsEduRate,
                        ThreeKidsEduRateRemark = safetyQuarterlyReports[i].ThreeKidsEduRateRemark,
                        UplinReportRate = safetyQuarterlyReports[i].UplinReportRate,
                        UplinReportRateRemark = safetyQuarterlyReports[i].UplinReportRateRemark,
                        Remarks = safetyQuarterlyReports[i].Remarks,
                        KeyEquipmentTotal = safetyQuarterlyReports[i].KeyEquipmentTotal,
                        KeyEquipmentTotalRemark = safetyQuarterlyReports[i].KeyEquipmentTotalRemark,
                        KeyEquipmentReportCount = safetyQuarterlyReports[i].KeyEquipmentReportCount,
                        KeyEquipmentReportCountRemark = safetyQuarterlyReports[i].KeyEquipmentReportCountRemark,
                        ChemicalAreaProjectCount = safetyQuarterlyReports[i].ChemicalAreaProjectCount,
                        ChemicalAreaProjectCountRemark = safetyQuarterlyReports[i].ChemicalAreaProjectCountRemark,
                        HarmfulMediumCoverCount = safetyQuarterlyReports[i].HarmfulMediumCoverCount,
                        HarmfulMediumCoverCountRemark = safetyQuarterlyReports[i].HarmfulMediumCoverCountRemark,
                        HarmfulMediumCoverRate = safetyQuarterlyReports[i].HarmfulMediumCoverRate,
                        HarmfulMediumCoverRateRemark = safetyQuarterlyReports[i].HarmfulMediumCoverRateRemark,
                        CompileMan = this.CurrUser.UserName,
                        UpState = BLL.Const.UpState_2,
                        HandleMan = this.CurrUser.UserId,
                        HandleState = BLL.Const.HandleState_1
                    };
                    BLL.SafetyQuarterlyReportService.AddSafetyQuarterlyReport(safetyQuarterlyReports[i]);
                } 
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdFileName.Text;
                if (filePath != string.Empty && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);//删除上传的XLS文件
                }
                ShowNotify("导入成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("请先将错误数据修正，再重新导入保存！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 导出错误提示
        /// <summary>
        /// 导出错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            //string strFileName = DateTime.Now.ToString("yyyyMMdd-hhmmss");
            //System.Web.HttpContext HC = System.Web.HttpContext.Current;
            //HC.Response.Clear();
            //HC.Response.Buffer = true;
            //HC.Response.ContentEncoding = System.Text.Encoding.UTF8;//设置输出流为简体中文

            ////---导出为Excel文件
            //HC.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + ".xls");
            //HC.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。

            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //this.gvErrorInfo.RenderControl(htw);
            //HC.Response.Write(sw.ToString());
            //HC.Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭审核弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //errorInfos.Clear();
            //if (!string.IsNullOrEmpty(this.hdCheckResult.Text.Trim()))
            //{
            //    string result = this.hdCheckResult.Text.Trim();
            //    List<string> errorInfoList = result.Split('|').ToList();
            //    foreach (var item in errorInfoList)
            //    {
            //        string[] errors = item.Split(',');
            //        Model.ErrorInfo errorInfo = new Model.ErrorInfo();
            //        errorInfo.Row = Convert.ToInt32(errors[0]);
            //        errorInfo.Column = errors[1];
            //        errorInfo.Reason = errors[2];
            //        errorInfos.Add(errorInfo);
            //    }
            //    if (errorInfos.Count > 0)
            //    {
            //        this.Grid1.Visible = false;
            //        this.Form2.Visible = true;
            //        this.gvErrorInfo.DataSource = errorInfos;
            //        this.gvErrorInfo.DataBind();
            //    }
            //}
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["safetyQuarterlyReports"] != null)
            {
                safetyQuarterlyReports = Session["safetyQuarterlyReports"] as List<Model.Information_SafetyQuarterlyReport>;
            }
            if (safetyQuarterlyReports.Count > 0)
            {
                this.Grid1.Visible = true;
                //this.Form2.Visible = false;
                this.Grid1.DataSource = safetyQuarterlyReports;
                this.Grid1.DataBind();
            }
        }

        ///// <summary>
        ///// 关闭保存导入数据窗口
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Window3_Close(object sender, WindowCloseEventArgs e)
        //{
        //    if (Session["safetyQuarterlyReports"] != null)
        //    {
        //        safetyQuarterlyReports = Session["safetyQuarterlyReports"] as List<Model.Information_SafetyQuarterlyReport>;
        //    }
        //    if (safetyQuarterlyReports.Count > 0)
        //    {
        //        this.Grid1.Visible = true;
        //        this.Form2.Visible = false;
        //        this.Grid1.DataSource = safetyQuarterlyReports;
        //        this.Grid1.DataBind();
        //    }
        //}
        #endregion 

        #region 转换字符串
        /// <summary>
        /// 转换单位
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        protected string ConvertUnit(object u)
        {
            string unitName = string.Empty;
            if (u != null)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(u.ToString());
                if (unit != null)
                {
                    unitName = unit.UnitName;
                }
            }
            return unitName;
        }
        #endregion

        #region 下载导入模板
        /// <summary>
        /// 下载模板按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("确定下载导入模板吗？", String.Empty, MessageBoxIcon.Question, PageManager1.GetCustomEventReference(false, "Confirm_OK"), PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                string rootPath = Server.MapPath("~/");
                string uploadfilepath = rootPath + Const.SafetyQuarterlyReportTemplateUrl;
                string filePath = Const.SafetyQuarterlyReportTemplateUrl;
                string fileName = Path.GetFileName(filePath);
                FileInfo info = new FileInfo(uploadfilepath);
                long fileSize = info.Length;
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "excel/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Length", fileSize.ToString().Trim());
                Response.TransmitFile(uploadfilepath, 0, fileSize);
                Response.End();
            }
        }
        #endregion
    }
}