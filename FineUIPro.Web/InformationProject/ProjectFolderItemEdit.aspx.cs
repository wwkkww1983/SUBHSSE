using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ProjectFolderItemEdit : PageBase
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

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            if (!string.IsNullOrEmpty(this.txtTitle.Text))
            {
                Model.InformationProject_ProjectFolderItem newProjectFolderItem = new Model.InformationProject_ProjectFolderItem
                {
                    ProjectFolderId = this.ProjectFolderId,
                    Title = this.txtTitle.Text.Trim(),
                    Code = this.txtCode.Text.Trim(),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text),
                    CompileDate = System.DateTime.Now,
                    CompileMan = this.CurrUser.UserId
                };
                if (string.IsNullOrEmpty(this.ProjectFolderItemId))
                {
                    this.ProjectFolderItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_ProjectFolderItem));
                    newProjectFolderItem.ProjectFolderItemId = this.ProjectFolderItemId;
                    BLL.ProjectFolderItemService.AddProjectFolderItem(newProjectFolderItem);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加项目文件", newProjectFolderItem.Code);
                }
                else
                {
                    newProjectFolderItem.ProjectFolderItemId = this.ProjectFolderItemId;
                    BLL.ProjectFolderItemService.UpdateProjectFolderItem(newProjectFolderItem);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改项目文件", newProjectFolderItem.Code);
                }

                if (isClose)
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                Alert.ShowInParent("文件标题不能为空！");
                return;
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
            if (string.IsNullOrEmpty(this.ProjectFolderItemId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectFolderAttachUrl&menuId={1}", this.ProjectFolderItemId, BLL.Const.ProjectFolderMenuId)));
        }
        #endregion
    }
}