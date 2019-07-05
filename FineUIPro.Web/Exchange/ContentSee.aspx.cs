using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.IO;

namespace FineUIPro.Web.Exchange
{
    public partial class ContentSee : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ContentId
        {
            get
            {
                return (string)ViewState["ContentId"];
            }
            set
            {
                ViewState["ContentId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ContentId = Request.QueryString["ContentId"];
                if (!String.IsNullOrEmpty(this.ContentId))
                {
                    var q = BLL.ContentService.GetContentById(this.ContentId);
                    if (q != null)
                    {
                        txtTheme.Text = q.Theme;
                        Model.Exchange_ContentType contentType = BLL.ContentTypeService.GetContentType(q.ContentTypeId);
                        if(contentType!=null)
                        {
                        txtContentType.Text = contentType.ContentTypeName;
                        }
                        txtContents.Text = q.Contents;
                        Model.Sys_User u = BLL.UserService.GetUserByUserId(q.CompileMan);
                        if (u != null)
                        {
                            txtCompileMan.Text = u.UserName;
                        }
                        if (q.CompileDate != null)
                        {
                            txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }                     
                    }
                }
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Exchange&menuId={1}&type=-1", ContentId, BLL.Const.ContentMenuId)));
        }
        #endregion
    }
}