using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ProjectFolderEdit : PageBase
    {
        #region 定义项

        /// <summary>
        /// 上级检查项
        /// </summary>
        public string SupProjectFolderId
        {
            get
            {
                return (string)ViewState["SupProjectFolderId"];
            }
            set
            {
                ViewState["SupProjectFolderId"] = value;
            }
        }

        /// <summary>
        /// 检查项
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
        #endregion

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectFolderId = Request.Params["ProjectFolderId"];
                this.SupProjectFolderId = Request.Params["SupProjectFolderId"];
                if (!string.IsNullOrEmpty(this.ProjectFolderId))
                {
                    var ProjectFolder = BLL.ProjectFolderService.GetProjectFolderByID(this.ProjectFolderId);
                    if (ProjectFolder != null)
                    {
                        this.SupProjectFolderId = ProjectFolder.SupProjectFolderId;
                        this.txtTitle.Text = ProjectFolder.Title;
                        if (ProjectFolder.IsEndLever == true)
                        {
                            this.chkIsEndLevel.Checked = true;
                        }
                        else
                        {
                            chkIsEndLevel.Checked = false;
                        }
                        this.txtCode.Text = ProjectFolder.Code;
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    this.chkIsEndLevel.Enabled = BLL.ProjectFolderService.IsDeleteProjectFolder(this.ProjectFolderId);
                }
            }
        }


        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string staName = this.txtTitle.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.ProjectFolderService.IsExistTitle(this.ProjectFolderId, this.SupProjectFolderId, staName))
                {
                    Model.InformationProject_ProjectFolder newProjectFolder = new Model.InformationProject_ProjectFolder
                    {
                        Title = staName,
                        SupProjectFolderId = this.SupProjectFolderId,
                        Code = this.txtCode.Text.Trim(),
                        ProjectId = this.CurrUser.LoginProjectId,
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked)
                    };
                    if (string.IsNullOrEmpty(this.ProjectFolderId))
                    {
                        newProjectFolder.ProjectFolderId = SQLHelper.GetNewID(typeof(Model.InformationProject_ProjectFolder));
                        BLL.ProjectFolderService.AddProjectFolder(newProjectFolder);
                        BLL.LogService.AddSys_Log(this.CurrUser, newProjectFolder.Code, newProjectFolder.ProjectFolderId, BLL.Const.ProjectFolderMenuId, BLL.Const.BtnAdd);
                    }
                    else
                    {
                        newProjectFolder.ProjectFolderId = this.ProjectFolderId;
                        BLL.ProjectFolderService.UpdateProjectFolder(newProjectFolder);
                        BLL.LogService.AddSys_Log(this.CurrUser, newProjectFolder.Code, newProjectFolder.ProjectFolderId, BLL.Const.ProjectFolderMenuId, BLL.Const.BtnModify);
                    }

                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInParent("项目文件夹名称已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInParent("项目文件夹名称不能为空！");
                return;
            }
        }
    }
}