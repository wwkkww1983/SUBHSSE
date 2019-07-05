using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ProjectFolderItemView : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string ProjectFolderItemId
        {
            get
            {
                return (string)ViewState["ProjectFolderItemId"];
            }
            set
            {
                ViewState["ProjectFolderItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string ProjectFolderId
        {
            get
            {
                return (string)ViewState["ProjectFolderId"];
            }
            set
            {
                ViewState["ProjectFolderId"] = value;
            }
        }

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectFolderId = Request.Params["ProjectFolderId"];
                this.ProjectFolderItemId = Request.Params["ProjectFolderItemId"];
                if (!string.IsNullOrEmpty(this.ProjectFolderItemId))
                {
                    var ProjectFolderItem = BLL.ProjectFolderItemService.GetProjectFolderItemByID(this.ProjectFolderItemId);
                    if (ProjectFolderItem != null)
                    {
                        this.ProjectFolderId = ProjectFolderItem.ProjectFolderId;
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ProjectFolderItemId);
                        this.txtTitle.Text = ProjectFolderItem.Title;
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(ProjectFolderItem.FileContent);
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectFolderMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);                    
                }
            }
        }
        
        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ProjectFolderItemId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectFolderAttachUrl&menuId={1}&type=-1", this.ProjectFolderItemId, BLL.Const.ProjectFolderMenuId)));
            }
        }
        #endregion
    }
}