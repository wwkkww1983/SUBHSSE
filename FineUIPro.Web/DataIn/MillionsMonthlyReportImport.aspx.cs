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
    public partial class MillionsMonthlyReportImport : PageBase
    {
        #region 定义变量
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
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 百万工时安全统计月报表集合
        /// </summary>
        private static List<Model.View_DataIn_MillionsMonthlyReport> reports = new List<Model.View_DataIn_MillionsMonthlyReport>();

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
                if (reports != null)
                {
                    reports.Clear();
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
        /// 审核
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
                if (reports != null)
                {
                    reports.Clear();
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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportBar.aspx?FileName={0}", this.hdFileName.Text, "审核 - ")));
            }
            catch (Exception ex)
            {
                ShowNotify("'" + ex.Message + "'", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (errorInfos.Count <= 0)
            {
                if (!string.IsNullOrEmpty(this.hdFileName.Text))
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("MillionsMonthlyReportBarIn.aspx?FileName={0}", this.hdFileName.Text, "导入 - ")));
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int PostPersonNumSum = 0, SnapPersonNumSum = 0, ContractorNumSum = 0, SumPersonNumSum = 0, SeriousInjuriesNumSum = 0, SeriousInjuriesPersonNumSum = 0, SeriousInjuriesLossHourSum = 0, MinorAccidentNumSum = 0, MinorAccidentPersonNumSum = 0, MinorAccidentLossHourSum = 0, OtherAccidentNumSum = 0, OtherAccidentPersonNumSum = 0, OtherAccidentLossHourSum = 0, RestrictedWorkPersonNumSum = 0, RestrictedWorkLossHourSum = 0, MedicalTreatmentPersonNumSum = 0, MedicalTreatmentLossHourSum = 0, FireNumSum = 0, ExplosionNumSum = 0, TrafficNumSum = 0, EquipmentNumSum = 0, QualityNumSum = 0, OtherNumSum = 0, FirstAidDressingsNumSum = 0, AttemptedEventNumSum = 0, LossDayNumSum = 0;
            decimal TotalWorkNumSum=0;
            if (errorInfos.Count <= 0)
            {
                List<Model.View_DataIn_MillionsMonthlyReport> report = new List<Model.View_DataIn_MillionsMonthlyReport>();
                if (Session["reports"] != null)
                {
                    report = Session["reports"] as List<Model.View_DataIn_MillionsMonthlyReport>;
                }

                int a = report.Count();
                for (int i = 0; i < a; i++)
                {
                    MillionsMonthlyReportId = string.Empty;
                    //判断百万工时安全统计月报是否存在
                    var isExist = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(report[i].UnitId, Convert.ToInt32(report[i].Year), Convert.ToInt32(report[i].Month));
                    if (isExist != null)
                    {
                        MillionsMonthlyReportId = isExist.MillionsMonthlyReportId;
                    }
                    else
                    {
                        MillionsMonthlyReportId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReport));
                        Model.Information_MillionsMonthlyReport newReport = new Model.Information_MillionsMonthlyReport
                        {
                            MillionsMonthlyReportId = MillionsMonthlyReportId,
                            UnitId = report[i].UnitId,
                            Year = report[i].Year,
                            Month = report[i].Month,
                            FillingMan = this.CurrUser.UserName,
                            FillingDate = DateTime.Now,
                            DutyPerson = report[i].DutyPerson,
                            RecordableIncidentRate = report[i].RecordableIncidentRate,
                            LostTimeRate = report[i].LostTimeRate,
                            LostTimeInjuryRate = report[i].LostTimeInjuryRate,
                            DeathAccidentFrequency = report[i].DeathAccidentFrequency,
                            AccidentMortality = report[i].AccidentMortality,
                            UpState = BLL.Const.UpState_2,
                            HandleState = BLL.Const.HandleState_1,
                            HandleMan = this.CurrUser.UserId
                        };
                        BLL.MillionsMonthlyReportService.AddMillionsMonthlyReport(newReport);
                    }
                    Model.Information_MillionsMonthlyReportItem newReportItem = new Model.Information_MillionsMonthlyReportItem
                    {
                        MillionsMonthlyReportItemId = report[i].MillionsMonthlyReportItemId,
                        MillionsMonthlyReportId = MillionsMonthlyReportId,
                        Affiliation = report[i].Affiliation,
                        Name = report[i].Name,
                        PostPersonNum = report[i].PostPersonNum,
                        SnapPersonNum = report[i].SnapPersonNum,
                        ContractorNum = report[i].ContractorNum,
                        SumPersonNum = report[i].SumPersonNum,
                        TotalWorkNum = report[i].TotalWorkNum,
                        SeriousInjuriesNum = report[i].SeriousInjuriesNum,
                        SeriousInjuriesPersonNum = report[i].SeriousInjuriesPersonNum,
                        SeriousInjuriesLossHour = report[i].SeriousInjuriesLossHour,
                        MinorAccidentNum = report[i].MinorAccidentNum,
                        MinorAccidentPersonNum = report[i].MinorAccidentPersonNum,
                        MinorAccidentLossHour = report[i].MinorAccidentLossHour,
                        OtherAccidentNum = report[i].OtherAccidentNum,
                        OtherAccidentPersonNum = report[i].OtherAccidentPersonNum,
                        OtherAccidentLossHour = report[i].OtherAccidentLossHour,
                        RestrictedWorkPersonNum = report[i].RestrictedWorkPersonNum,
                        RestrictedWorkLossHour = report[i].RestrictedWorkLossHour,
                        MedicalTreatmentPersonNum = report[i].MedicalTreatmentPersonNum,
                        MedicalTreatmentLossHour = report[i].MedicalTreatmentLossHour,
                        FireNum = report[i].FireNum,
                        ExplosionNum = report[i].ExplosionNum,
                        TrafficNum = report[i].TrafficNum,
                        EquipmentNum = report[i].EquipmentNum,
                        QualityNum = report[i].QualityNum,
                        OtherNum = report[i].OtherNum,
                        FirstAidDressingsNum = report[i].FirstAidDressingsNum,
                        AttemptedEventNum = report[i].AttemptedEventNum,
                        LossDayNum = report[i].LossDayNum
                    };
                    var sortIndexMax = ((from x in Funs.DB.Information_MillionsMonthlyReportItem where x.MillionsMonthlyReportId == MillionsMonthlyReportId select x.SortIndex).Max());
                    if (sortIndexMax != null)
                    {
                        newReportItem.SortIndex = sortIndexMax + 10;
                    }
                    else
                    {
                        newReportItem.SortIndex = i + 10;
                    }

                    PostPersonNumSum += Convert.ToInt32(newReportItem.PostPersonNum);
                    SnapPersonNumSum += Convert.ToInt32(newReportItem.SnapPersonNum);
                    ContractorNumSum += Convert.ToInt32(newReportItem.ContractorNum);
                    SumPersonNumSum += Convert.ToInt32(newReportItem.SumPersonNum);
                    TotalWorkNumSum += Convert.ToDecimal(newReportItem.TotalWorkNum);
                    SeriousInjuriesNumSum += Convert.ToInt32(newReportItem.SeriousInjuriesNum);
                    SeriousInjuriesPersonNumSum += Convert.ToInt32(newReportItem.SeriousInjuriesPersonNum);
                    SeriousInjuriesLossHourSum += Convert.ToInt32(newReportItem.SeriousInjuriesLossHour);
                    MinorAccidentNumSum += Convert.ToInt32(newReportItem.MinorAccidentNum);
                    MinorAccidentPersonNumSum += Convert.ToInt32(newReportItem.MinorAccidentPersonNum);
                    MinorAccidentLossHourSum += Convert.ToInt32(newReportItem.MinorAccidentLossHour);
                    OtherAccidentNumSum += Convert.ToInt32(newReportItem.OtherAccidentNum);
                    OtherAccidentPersonNumSum += Convert.ToInt32(newReportItem.OtherAccidentPersonNum);
                    OtherAccidentLossHourSum += Convert.ToInt32(newReportItem.OtherAccidentLossHour);
                    RestrictedWorkPersonNumSum += Convert.ToInt32(newReportItem.RestrictedWorkPersonNum);
                    RestrictedWorkLossHourSum += Convert.ToInt32(newReportItem.RestrictedWorkLossHour);
                    MedicalTreatmentPersonNumSum += Convert.ToInt32(newReportItem.MedicalTreatmentPersonNum);
                    MedicalTreatmentLossHourSum += Convert.ToInt32(newReportItem.MedicalTreatmentLossHour);
                    FireNumSum += Convert.ToInt32(newReportItem.FireNum);
                    ExplosionNumSum += Convert.ToInt32(newReportItem.ExplosionNum);
                    TrafficNumSum += Convert.ToInt32(newReportItem.TrafficNum);
                    EquipmentNumSum += Convert.ToInt32(newReportItem.EquipmentNum);
                    QualityNumSum += Convert.ToInt32(newReportItem.QualityNum);
                    OtherNumSum += Convert.ToInt32(newReportItem.OtherNum);
                    FirstAidDressingsNumSum += Convert.ToInt32(newReportItem.FirstAidDressingsNum);
                    AttemptedEventNumSum += Convert.ToInt32(newReportItem.AttemptedEventNum);
                    LossDayNumSum += Convert.ToInt32(newReportItem.LossDayNum);

                    BLL.MillionsMonthlyReportItemService.AddMillionsMonthlyReportItem(newReportItem);
                }


                //增加本月合计数
                Model.Information_MillionsMonthlyReportItem totalItem = new Model.Information_MillionsMonthlyReportItem
                {
                    MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem)),
                    MillionsMonthlyReportId = MillionsMonthlyReportId
                };
                var sort = ((from x in Funs.DB.Information_MillionsMonthlyReportItem where x.MillionsMonthlyReportId == MillionsMonthlyReportId select x.SortIndex).Max());
                if (sort != null)
                {
                    totalItem.SortIndex = sort + 10;
                }
                totalItem.Affiliation = "本月合计";
                totalItem.Name = "本月合计";
                totalItem.PostPersonNum = PostPersonNumSum;
                totalItem.SnapPersonNum = SnapPersonNumSum;
                totalItem.ContractorNum = ContractorNumSum;
                totalItem.SumPersonNum = SumPersonNumSum;
                totalItem.TotalWorkNum = TotalWorkNumSum;
                totalItem.SeriousInjuriesNum = SeriousInjuriesNumSum;
                totalItem.SeriousInjuriesPersonNum = SeriousInjuriesPersonNumSum;
                totalItem.SeriousInjuriesLossHour = SeriousInjuriesLossHourSum;
                totalItem.MinorAccidentNum = MinorAccidentNumSum;
                totalItem.MinorAccidentPersonNum = MinorAccidentPersonNumSum;
                totalItem.MinorAccidentLossHour = MinorAccidentLossHourSum;
                totalItem.OtherAccidentNum = OtherAccidentNumSum;
                totalItem.OtherAccidentPersonNum = OtherAccidentPersonNumSum;
                totalItem.OtherAccidentLossHour = OtherAccidentLossHourSum;
                totalItem.RestrictedWorkPersonNum = RestrictedWorkPersonNumSum;
                totalItem.RestrictedWorkLossHour = RestrictedWorkLossHourSum;
                totalItem.MedicalTreatmentPersonNum = MedicalTreatmentPersonNumSum;
                totalItem.MedicalTreatmentLossHour = MedicalTreatmentLossHourSum;
                totalItem.FireNum = FireNumSum;
                totalItem.ExplosionNum = ExplosionNumSum;
                totalItem.TrafficNum = TrafficNumSum;
                totalItem.EquipmentNum = EquipmentNumSum;
                totalItem.QualityNum = QualityNumSum;
                totalItem.OtherNum = OtherNumSum;
                totalItem.FirstAidDressingsNum = FirstAidDressingsNumSum;
                totalItem.AttemptedEventNum = AttemptedEventNumSum;
                totalItem.LossDayNum = LossDayNumSum;
                BLL.MillionsMonthlyReportItemService.AddMillionsMonthlyReportItem(totalItem);

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
            if (Session["reports"] != null)
            {
                reports = Session["reports"] as List<Model.View_DataIn_MillionsMonthlyReport>;
            }
            if (reports.Count > 0)
            {
                this.Grid1.Visible = true;
                //this.Form2.Visible = false;
                this.Grid1.DataSource = reports;
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
        //    if (Session["reports"] != null)
        //    {
        //        reports = Session["reports"] as List<Model.View_DataIn_MillionsMonthlyReport>;
        //    }
        //    if (reports.Count > 0)
        //    {
        //        this.Grid1.Visible = true;
        //        this.Form2.Visible = false;
        //        this.Grid1.DataSource = reports;
        //        this.Grid1.DataBind();
        //    }
        //}
        #endregion 

        #region 下载模板
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
                string uploadfilepath = rootPath + Const.MillionsMonthlyReportTemplateUrl;
                string filePath = Const.MillionsMonthlyReportTemplateUrl;
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
    }
}