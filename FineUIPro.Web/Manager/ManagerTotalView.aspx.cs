using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerTotalId
        {
            get
            {
                return (string)ViewState["ManagerTotalId"];
            }
            set
            {
                ViewState["ManagerTotalId"] = value;
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
                this.ManagerTotalId = Request.Params["ManagerTotalId"];
                if (!string.IsNullOrEmpty(this.ManagerTotalId))
                {
                    Model.Manager_ManagerTotal managerTotal = BLL.ManagerTotalService.GetManagerTotalById(this.ManagerTotalId);
                    if (managerTotal != null)
                    {
                        this.txtManagerTotalCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerTotalId);
                        this.txtManagerTotalName.Text = managerTotal.ManagerTotalName;
                        var users = BLL.UserService.GetUserByUserId(managerTotal.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerTotal.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerTotal.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerTotalMenuId;
                this.ctlAuditFlow.DataId = this.ManagerTotalId;
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
            if (!string.IsNullOrEmpty(this.ManagerTotalId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerTotalAttachUrl&menuId={1}&type=-1", ManagerTotalId, BLL.Const.ProjectManagerTotalMenuId)));
            }

        }
        #endregion
    }
}