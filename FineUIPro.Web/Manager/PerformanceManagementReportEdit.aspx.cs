using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class PerformanceManagementReportEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string PerformanceManagementReportId
        {
            get
            {
                return (string)ViewState["PerformanceManagementReportId"];
            }
            set
            {
                ViewState["PerformanceManagementReportId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.PerformanceManagementReportId = Request.Params["PerformanceManagementReportId"];
                if (!string.IsNullOrEmpty(this.PerformanceManagementReportId))
                {
                    Model.Manager_PerformanceManagementReport performanceManagementReport = BLL.PerformanceManagementReportService.GetPerformanceManagementReportById(this.PerformanceManagementReportId);
                    if (performanceManagementReport != null)
                    {
                        this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", performanceManagementReport.ReportDate);
                        this.txtMonthPerformance1.Text = performanceManagementReport.MonthPerformance1;
                        this.txtMonthPerformance2.Text = performanceManagementReport.MonthPerformance2;
                        this.txtMonthPerformance3.Text = performanceManagementReport.MonthPerformance3;
                        this.txtMonthPerformance4.Text = performanceManagementReport.MonthPerformance4;
                        this.txtMonthPerformance5.Text = performanceManagementReport.MonthPerformance5;
                        this.txtMonthPerformance6.Text = performanceManagementReport.MonthPerformance6;
                        this.txtMonthPerformance7.Text = performanceManagementReport.MonthPerformance7;
                        this.txtMonthPerformance8.Text = performanceManagementReport.MonthPerformance8;
                        this.txtMonthPerformance9.Text = performanceManagementReport.MonthPerformance9;
                        this.txtMonthPerformance10.Text = performanceManagementReport.MonthPerformance10;
                        this.txtMonthPerformance11.Text = performanceManagementReport.MonthPerformance11;
                        this.txtMonthPerformance12.Text = performanceManagementReport.MonthPerformance12;
                        this.txtMonthPerformance13.Text = performanceManagementReport.MonthPerformance13;
                        this.txtMonthPerformance14.Text = performanceManagementReport.MonthPerformance14;
                        this.txtMonthPerformance15.Text = performanceManagementReport.MonthPerformance15;
                        this.txtMonthPerformance16.Text = performanceManagementReport.MonthPerformance16;
                        this.txtMonthPerformance17.Text = performanceManagementReport.MonthPerformance17;
                        this.txtMonthPerformance18.Text = performanceManagementReport.MonthPerformance18;
                        this.txtMonthPerformance19.Text = performanceManagementReport.MonthPerformance19;
                        this.txtPerformanceIndex1.Text = performanceManagementReport.PerformanceIndex1;
                        this.txtPerformanceIndex2.Text = performanceManagementReport.PerformanceIndex2;
                        this.txtPerformanceIndex3.Text = performanceManagementReport.PerformanceIndex3;
                        this.txtPerformanceIndex4.Text = performanceManagementReport.PerformanceIndex4;
                        this.txtPerformanceIndex5.Text = performanceManagementReport.PerformanceIndex5;
                        this.txtPerformanceIndex6.Text = performanceManagementReport.PerformanceIndex6;
                        this.txtPerformanceIndex7.Text = performanceManagementReport.PerformanceIndex7;
                        this.txtPerformanceIndex8.Text = performanceManagementReport.PerformanceIndex8;
                        this.txtPerformanceIndex9.Text = performanceManagementReport.PerformanceIndex9;
                        this.txtPerformanceIndex10.Text = performanceManagementReport.PerformanceIndex10;
                        this.txtPerformanceIndex11.Text = performanceManagementReport.PerformanceIndex11;
                    }
                }
                else
                {
                    this.txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///    保存方法
        /// </summary>
        private void SaveData()
        {
            Model.Manager_PerformanceManagementReport performanceManagementReport = new Model.Manager_PerformanceManagementReport
            {
                ProjectId = this.CurrUser.LoginProjectId,
                ReportDate = Funs.GetNewDateTime(this.txtDate.Text.Trim()),
                MonthPerformance1 = this.txtMonthPerformance1.Text.Trim(),
                MonthPerformance2 = this.txtMonthPerformance2.Text.Trim(),
                MonthPerformance3 = this.txtMonthPerformance3.Text.Trim(),
                MonthPerformance4 = this.txtMonthPerformance4.Text.Trim(),
                MonthPerformance5 = this.txtMonthPerformance5.Text.Trim(),
                MonthPerformance6 = this.txtMonthPerformance6.Text.Trim(),
                MonthPerformance7 = this.txtMonthPerformance7.Text.Trim(),
                MonthPerformance8 = this.txtMonthPerformance8.Text.Trim(),
                MonthPerformance9 = this.txtMonthPerformance9.Text.Trim(),
                MonthPerformance10 = this.txtMonthPerformance10.Text.Trim(),
                MonthPerformance11 = this.txtMonthPerformance11.Text.Trim(),
                MonthPerformance12 = this.txtMonthPerformance12.Text.Trim(),
                MonthPerformance13 = this.txtMonthPerformance13.Text.Trim(),
                MonthPerformance14 = this.txtMonthPerformance14.Text.Trim(),
                MonthPerformance15 = this.txtMonthPerformance15.Text.Trim(),
                MonthPerformance16 = this.txtMonthPerformance16.Text.Trim(),
                MonthPerformance17 = this.txtMonthPerformance17.Text.Trim(),
                MonthPerformance18 = this.txtMonthPerformance18.Text.Trim(),
                MonthPerformance19 = this.txtMonthPerformance19.Text.Trim(),
                PerformanceIndex1 = this.txtPerformanceIndex1.Text.Trim(),
                PerformanceIndex2 = this.txtPerformanceIndex2.Text.Trim(),
                PerformanceIndex3 = this.txtPerformanceIndex3.Text.Trim(),
                PerformanceIndex4 = this.txtPerformanceIndex4.Text.Trim(),
                PerformanceIndex5 = this.txtPerformanceIndex5.Text.Trim(),
                PerformanceIndex6 = this.txtPerformanceIndex6.Text.Trim(),
                PerformanceIndex7 = this.txtPerformanceIndex7.Text.Trim(),
                PerformanceIndex8 = this.txtPerformanceIndex8.Text.Trim(),
                PerformanceIndex9 = this.txtPerformanceIndex9.Text.Trim(),
                PerformanceIndex10 = this.txtPerformanceIndex10.Text.Trim(),
                PerformanceIndex11 = this.txtPerformanceIndex11.Text.Trim(),

            };
            if (!string.IsNullOrEmpty(this.PerformanceManagementReportId))
            {
                performanceManagementReport.PerformanceManagementReportId = this.PerformanceManagementReportId;
                BLL.PerformanceManagementReportService.UpdatePerformanceManagementReport(performanceManagementReport);
                BLL.LogService.AddSys_Log(this.CurrUser, null, performanceManagementReport.PerformanceManagementReportId, BLL.Const.ProjectPerformanceManagementReportMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.PerformanceManagementReportId = SQLHelper.GetNewID(typeof(Model.Manager_PerformanceManagementReport));
                performanceManagementReport.PerformanceManagementReportId = this.PerformanceManagementReportId;
                performanceManagementReport.CompileMan = this.CurrUser.UserId;
                performanceManagementReport.CompileDate = DateTime.Now;
                BLL.PerformanceManagementReportService.AddPerformanceManagementReport(performanceManagementReport);
                BLL.LogService.AddSys_Log(this.CurrUser, null, performanceManagementReport.PerformanceManagementReportId, BLL.Const.ProjectPerformanceManagementReportMenuId, BLL.Const.BtnAdd);
            }
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PerformanceManagementReportAttachUrl&type=-1", this.PerformanceManagementReportId, BLL.Const.ProjectPerformanceManagementReportMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.PerformanceManagementReportId))
                {
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PerformanceManagementReportAttachUrl&menuId={1}", this.PerformanceManagementReportId, BLL.Const.ProjectPerformanceManagementReportMenuId)));
            }
        }
        #endregion
    }
}