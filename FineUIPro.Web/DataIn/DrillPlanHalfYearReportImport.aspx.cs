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
    public partial class DrillPlanHalfYearReportImport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 应急演练工作计划半年报表集合
        /// </summary>
        private static List<Model.View_Information_DrillPlanHalfYearReportItem> reports = new List<Model.View_Information_DrillPlanHalfYearReportItem>();

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
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillPlanHalfYearReportBar.aspx?FileName={0}", this.hdFileName.Text, "审核 - ")));
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
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("DrillPlanHalfYearReportBarIn.aspx?FileName={0}", this.hdFileName.Text, "导入 - ")));
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
                List<Model.View_Information_DrillPlanHalfYearReportItem> report = new List<Model.View_Information_DrillPlanHalfYearReportItem>();
                if (Session["reports"] != null)
                {
                    report = Session["reports"] as List<Model.View_Information_DrillPlanHalfYearReportItem>;
                }

                int a = report.Count();
                for (int i = 0; i < a; i++)
                {
                    string drillPlanHalfYearReportId = string.Empty;
                    //判断应急演练工作计划半年报是否存在

                    var isExist = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdDate(report[i].UnitId, Convert.ToInt32(report[i].YearId), Convert.ToInt32(report[i].HalfYearId));
                    if (isExist != null)
                    {
                        drillPlanHalfYearReportId = isExist.DrillPlanHalfYearReportId;
                    }
                    else
                    {
                        drillPlanHalfYearReportId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReport));
                        Model.Information_DrillPlanHalfYearReport newReport = new Model.Information_DrillPlanHalfYearReport
                        {
                            DrillPlanHalfYearReportId = drillPlanHalfYearReportId,
                            UnitId = report[i].UnitId,
                            YearId = report[i].YearId,
                            HalfYearId = report[i].HalfYearId,
                            Telephone = report[i].Telephone,
                            CompileDate = DateTime.Now,
                            CompileMan = this.CurrUser.UserName,
                            HandleMan = this.CurrUser.UserId,
                            UpState = BLL.Const.UpState_2,
                            HandleState = BLL.Const.HandleState_1
                        };
                        BLL.DrillPlanHalfYearReportService.AddDrillPlanHalfYearReport(newReport);
                    }
                    Model.Information_DrillPlanHalfYearReportItem newReportItem = new Model.Information_DrillPlanHalfYearReportItem
                    {
                        DrillPlanHalfYearReportItemId = report[i].DrillPlanHalfYearReportItemId,
                        DrillPlanHalfYearReportId = drillPlanHalfYearReportId,
                        DrillPlanName = report[i].DrillPlanName,
                        OrganizationUnit = report[i].OrganizationUnit,
                        DrillPlanDate = report[i].DrillPlanDate,
                        AccidentScene = report[i].AccidentScene,
                        ExerciseWay = report[i].ExerciseWay,
                        SortIndex = i
                    };
                    BLL.DrillPlanHalfYearReportItemService.AddDrillPlanHalfYearReportItem(newReportItem);
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
            if (Session["reports"] != null)
            {
                reports = Session["reports"] as List<Model.View_Information_DrillPlanHalfYearReportItem>;
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
        //        reports = Session["reports"] as List<Model.View_Information_DrillPlanHalfYearReportItem>;
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
                string uploadfilepath = rootPath + Const.DrillPlanHalfYearReportTemplateUrl;
                string filePath = Const.DrillPlanHalfYearReportTemplateUrl;
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

        /// <summary>
        /// 上/下半年
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        protected string ConvertHalfYear(object y)
        {
            string year = string.Empty;
            if (y!=null)
            {
                if (y.ToString()=="1")
                {
                    year = "上半年";
                }
                else
                {
                    year = "下半年";
                }
            }
            return year;
        }
        #endregion
    }
}