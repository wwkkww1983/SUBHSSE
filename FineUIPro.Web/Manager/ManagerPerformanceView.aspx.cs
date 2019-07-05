using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerPerformanceView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerPerformanceId
        {
            get
            {
                return (string)ViewState["ManagerPerformanceId"];
            }
            set
            {
                ViewState["ManagerPerformanceId"] = value;
            }
        }
        #endregion

        #region 加载
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
                this.ManagerPerformanceId = Request.Params["ManagerPerformanceId"];
                if (!string.IsNullOrEmpty(this.ManagerPerformanceId))
                {
                    Model.Manager_ManagerPerformance managerPerformance = BLL.ManagerPerformanceService.GetManagerPerformanceById(this.ManagerPerformanceId);
                    if (managerPerformance != null)
                    {
                        this.txtManagerPerformanceCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerPerformanceId);
                        this.txtManagerPerformanceName.Text = managerPerformance.ManagerPerformanceName;
                        var users = BLL.UserService.GetUserByUserId(managerPerformance.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerPerformance.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerPerformance.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerPerformanceMenuId;
                this.ctlAuditFlow.DataId = this.ManagerPerformanceId;
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
            if (!string.IsNullOrEmpty(this.ManagerPerformanceId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerPerformanceAttachUrl&menuId={1}&type=-1", ManagerPerformanceId, BLL.Const.ProjectManagerPerformanceMenuId)));
            }

        }
        #endregion
    }
}