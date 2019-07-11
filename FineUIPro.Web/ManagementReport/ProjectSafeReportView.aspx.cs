using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ProjectSafeReportView : PageBase
    {
        #region 自定义变量
        /// <summary>
        /// 安全文件上报主键
        /// </summary>
        public string SafeReportId
        {
            get
            {
                return (string)ViewState["SafeReportId"];
            }
            set
            {
                ViewState["SafeReportId"] = value;
            }
        }
        /// <summary>
        /// 安全文件上报明细主键
        /// </summary>
        public string SafeReportItemId
        {
            get
            {
                return (string)ViewState["SafeReportItemId"];
            }
            set
            {
                ViewState["SafeReportItemId"] = value;
            }
        }
        /// <summary>
        /// 安全文件上报明细主键
        /// </summary>
        public string SafeReportUnitItemId
        {
            get
            {
                return (string)ViewState["SafeReportUnitItemId"];
            }
            set
            {
                ViewState["SafeReportUnitItemId"] = value;
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
            // 表头过滤
            if (!IsPostBack && this.CurrUser != null)
            {
                this.SafeReportItemId = Request.Params["SafeReportItemId"];
                this.SafeReportUnitItemId = Request.Params["SafeReportUnitItemId"];
                var safeReportItem = BLL.SafeReportItemService.GetSafeReportItemBySafeReportItemId(this.SafeReportItemId);
                if (safeReportItem != null)
                {
                    this.txtReportContent.Text = safeReportItem.ReportContent;
                    this.txtReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeReportItem.ReportTime);
                    this.txtUpReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeReportItem.UpReportTime);
                    this.drpReportManId.Text = BLL.UserService.GetUserNameByUserId(safeReportItem.ReportManId);
                    if (safeReportItem.States == BLL.Const.State_2)
                    {
                        this.lbState.Text = "已上报";
                    }
                    this.SafeReportId = safeReportItem.SafeReportId;
                }
                else {
                    var safeUnitReportItem = BLL.SafeReportUnitItemService.GetSafeReportUnitItemBySafeReportUnitItemId(this.SafeReportUnitItemId);
                    if (safeUnitReportItem != null)
                    {
                        this.txtReportContent.Text = safeUnitReportItem.ReportContent;
                        this.txtReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeUnitReportItem.ReportTime);
                        this.txtUpReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeUnitReportItem.UpReportTime);
                        this.drpReportManId.Text = BLL.UserService.GetUserNameByUserId(safeUnitReportItem.ReportManId);
                        if (safeUnitReportItem.States == BLL.Const.State_2)
                        {
                            this.lbState.Text = "已上报";
                        }
                        this.SafeReportId = safeUnitReportItem.SafeReportId;
                    }
                }

                var safeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(this.SafeReportId);
                if (safeReport != null)
                {
                    this.SimpleForm1.Hidden = false;
                    this.txtSafeReportCode.Text = safeReport.SafeReportCode;
                    this.txtSafeReportName.Text = safeReport.SafeReportName;
                    this.txtRequestTime.Text = string.Format("{0:yyyy-MM-dd}", safeReport.RequestTime);
                    this.txtRequirement.Text = safeReport.Requirement;
                }
            }
        }
        #endregion
        
        #region 标准模板附件查看
        /// <summary>
        /// 标准模板附件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTemplateView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SafeReportId))
            {
               PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ServerSafeReportAttachUrl&menuId={1}&type=-1", this.SafeReportId, BLL.Const.ServerSafeReportMenuId)));
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
            if (!string.IsNullOrEmpty(this.SafeReportItemId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafeReportAttachUrl&menuId={1}type=-1", this.SafeReportItemId, BLL.Const.ProjectSafeReportMenuId)));
            }
            else
            {
                if (!string.IsNullOrEmpty(this.SafeReportUnitItemId))
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/UnitSafeReportAttachUrl&menuId={1}type=-1", this.SafeReportUnitItemId, BLL.Const.ServerSafeUnitReportMenuId)));
                }
            }
        }
        #endregion        
    }
}