using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerQuarterlyView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerQuarterlyId
        {
            get
            {
                return (string)ViewState["ManagerQuarterlyId"];
            }
            set
            {
                ViewState["ManagerQuarterlyId"] = value;
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
                this.ManagerQuarterlyId = Request.Params["ManagerQuarterlyId"];
                if (!string.IsNullOrEmpty(this.ManagerQuarterlyId))
                {
                    Model.Manager_ManagerQuarterly managerQuarterly = BLL.ManagerQuarterlyService.GetManagerQuarterlyById(this.ManagerQuarterlyId);
                    if (managerQuarterly != null)
                    {
                        this.txtManagerQuarterlyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerQuarterlyId);
                        this.txtManagerQuarterlyName.Text = managerQuarterly.ManagerQuarterlyName;
                        var users = BLL.UserService.GetUserByUserId(managerQuarterly.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerQuarterly.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerQuarterly.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerQuarterlyMenuId;
                this.ctlAuditFlow.DataId = this.ManagerQuarterlyId;
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
            if (!string.IsNullOrEmpty(this.ManagerQuarterlyId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerQuarterlyAttachUrl&menuId={1}&type=-1", ManagerQuarterlyId, BLL.Const.ProjectManagerQuarterlyMenuId)));
            }

        }
        #endregion
    }
}