using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.DataIn
{
    public partial class AccidentCauseReportImport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 职工伤亡事故原因分析表明细
        /// </summary>
        private static List<Model.View_DataIn_AccidentCauseReport> reports = new List<Model.View_DataIn_AccidentCauseReport>();

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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCauseReportBar.aspx?FileName={0}", this.hdFileName.Text, "审核 - ")));
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
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportBarIn.aspx?FileName={0}", this.hdFileName.Text, "导入 - ")));
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
            if (errorInfos.Count <= 0)
            {
                List<Model.View_DataIn_AccidentCauseReport> report = new List<Model.View_DataIn_AccidentCauseReport>();
                if (Session["reports"] != null)
                {
                    report = Session["reports"] as List<Model.View_DataIn_AccidentCauseReport>;
                }

                int a = report.Count();
                for (int i = 0; i < a; i++)
                {
                    string accidentCauseReportId = string.Empty;
                    //判断职工伤亡事故原因分析是否存在
                    var isExist = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(report[i].UnitId, Convert.ToInt32(report[i].Year), Convert.ToInt32(report[i].Month));
                    if (isExist != null)
                    {
                        accidentCauseReportId = isExist.AccidentCauseReportId;
                    }
                    else
                    {
                        accidentCauseReportId = SQLHelper.GetNewID(typeof(Model.Information_AccidentCauseReport));
                        Model.Information_AccidentCauseReport newReport = new Model.Information_AccidentCauseReport
                        {
                            AccidentCauseReportId = accidentCauseReportId,
                            UnitId = report[i].UnitId,
                            AccidentCauseReportCode = report[i].AccidentCauseReportCode,
                            Year = report[i].Year,
                            Month = report[i].Month,
                            DeathAccident = report[i].DeathAccident,
                            DeathToll = report[i].DeathToll,
                            InjuredAccident = report[i].InjuredAccident,
                            InjuredToll = report[i].InjuredToll,
                            MinorWoundAccident = report[i].MinorWoundAccident,
                            MinorWoundToll = report[i].MinorWoundToll,
                            AverageTotalHours = report[i].AverageTotalHours,
                            AverageManHours = report[i].AverageManHours,
                            TotalLossMan = report[i].TotalLossMan,
                            LastMonthLossHoursTotal = report[i].LastMonthLossHoursTotal,
                            KnockOffTotal = report[i].KnockOffTotal,
                            DirectLoss = report[i].DirectLoss,
                            IndirectLosses = report[i].IndirectLosses,
                            TotalLoss = report[i].TotalLoss,
                            TotalLossTime = report[i].TotalLossTime,
                            FillCompanyPersonCharge = report[i].FillCompanyPersonCharge,
                            TabPeople = this.CurrUser.UserName,
                            AuditPerson = this.CurrUser.UserName,
                            FillingDate = DateTime.Now,
                            UpState = BLL.Const.UpState_2,
                            HandleState = BLL.Const.HandleState_1,
                            HandleMan = this.CurrUser.UserId
                        };
                        BLL.AccidentCauseReportService.AddAccidentCauseReport(newReport);
                    }
                    Model.Information_AccidentCauseReportItem newReportItem = new Model.Information_AccidentCauseReportItem
                    {
                        AccidentCauseReportItemId = report[i].AccidentCauseReportItemId,
                        AccidentCauseReportId = accidentCauseReportId,
                        AccidentType = report[i].AccidentType,
                        Death1 = report[i].Death1,
                        Injuries1 = report[i].Injuries1,
                        MinorInjuries1 = report[i].MinorInjuries1,
                        Death2 = report[i].Death2,
                        Injuries2 = report[i].Injuries2,
                        MinorInjuries2 = report[i].MinorInjuries2,
                        Death3 = report[i].Death3,
                        Injuries3 = report[i].Injuries3,
                        MinorInjuries3 = report[i].MinorInjuries3,
                        Death4 = report[i].Death4,
                        Injuries4 = report[i].Injuries4,
                        MinorInjuries4 = report[i].MinorInjuries4,
                        Death5 = report[i].Death5,
                        Injuries5 = report[i].Injuries5,
                        MinorInjuries5 = report[i].MinorInjuries5,
                        Death6 = report[i].Death6,
                        Injuries6 = report[i].Injuries6,
                        MinorInjuries6 = report[i].MinorInjuries6,
                        Death7 = report[i].Death7,
                        Injuries7 = report[i].Injuries7,
                        MinorInjuries7 = report[i].MinorInjuries7,
                        Death8 = report[i].Death8,
                        Injuries8 = report[i].Injuries8,
                        MinorInjuries8 = report[i].MinorInjuries8,
                        Death9 = report[i].Death9,
                        Injuries9 = report[i].Injuries9,
                        MinorInjuries9 = report[i].MinorInjuries9,
                        Death10 = report[i].Death10,
                        Injuries10 = report[i].Injuries10,
                        MinorInjuries10 = report[i].MinorInjuries10,
                        Death11 = report[i].Death11,
                        Injuries11 = report[i].Injuries11,
                        MinorInjuries11 = report[i].MinorInjuries11,
                        TotalDeath = report[i].TotalDeath,
                        TotalInjuries = report[i].TotalInjuries,
                        TotalMinorInjuries = report[i].TotalMinorInjuries
                    };
                    BLL.AccidentCauseReportItemService.AddAccidentCauseReportItem(newReportItem);
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
            //if (Session["errorInfos"] != null)
            //{
            //    this.hdCheckResult.Text = Session["errorInfos"].ToString();
            //}
            //else
            //{
            //    this.hdCheckResult.Text = string.Empty;
            //    this.Grid1.Hidden = false;
            //    this.gvErrorInfo.Hidden = true;
            //}
            //if (!string.IsNullOrEmpty(this.hdCheckResult.Text.Trim()))
            //{
            //    string result = this.hdCheckResult.Text.Trim();
            //    List<string> errorInfoList = result.Split('|').ToList();
            //    foreach (var item in errorInfoList)
            //    {
            //        string[] errors = item.Split(',');
            //        Model.ErrorInfo errorInfo = new Model.ErrorInfo();
            //        errorInfo.Row = errors[0];
            //        errorInfo.Column = errors[1];
            //        errorInfo.Reason = errors[2];
            //        errorInfos.Add(errorInfo);
            //    }
            //    if (errorInfos.Count > 0)
            //    {
            //        this.Grid1.Hidden = true;
            //        this.gvErrorInfo.Hidden = false;
            //        //this.btnOut.Hidden = false;
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
                reports = Session["reports"] as List<Model.View_DataIn_AccidentCauseReport>;
            }
            if (reports.Count > 0)
            {
                this.Grid1.Hidden = false;
                //this.gvErrorInfo.Hidden = true;
                //this.btnOut.Hidden = true;
                this.Grid1.DataSource = reports;
                this.Grid1.DataBind();
            }
        }

        ///// <summary>
        ///// 关闭保存弹出窗口
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Window3_Close(object sender, WindowCloseEventArgs e)
        //{
        //    if (Session["reports"] != null)
        //    {
        //        reports = Session["reports"] as List<Model.View_DataIn_AccidentCauseReport>;
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
                string uploadfilepath = rootPath + Const.AccidentCauseReportTemplateUrl;
                string filePath = Const.AccidentCauseReportTemplateUrl;
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