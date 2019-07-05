using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class HealthView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HealthId
        {
            get
            {
                return (string)ViewState["HealthId"];
            }
            set
            {
                ViewState["HealthId"] = value;
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
                this.HealthId = Request.Params["HealthId"];
                if (!string.IsNullOrEmpty(this.HealthId))
                {
                    Model.Manager_Health health = BLL.HealthService.GetHealthById(this.HealthId);
                    if (health != null)
                    {
                        this.txtHealthCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HealthId);
                        this.txtHealthName.Text = health.HealthName;
                        var users = BLL.UserService.GetUserByUserId(health.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", health.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(health.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HealthMenuId;
                this.ctlAuditFlow.DataId = this.HealthId;
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
            if (!string.IsNullOrEmpty(this.HealthId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HealthAttachUrl&menuId={1}&type=-1", HealthId, BLL.Const.HealthMenuId)));
            }

        }
        #endregion
    }
}