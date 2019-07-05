using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalFolderEdit : PageBase
    {
        #region 定义项

        /// <summary>
        /// 上级检查项
        /// </summary>
        public string SupPersonalFolderId
        {
            get
            {
                return (string)ViewState["SupPersonalFolderId"];
            }
            set
            {
                ViewState["SupPersonalFolderId"] = value;
            }
        }

        /// <summary>
        /// 检查项
        /// </summary>
        public string PersonalFolderId
        {
            get
            {
                return (string)ViewState["PersonalFolderId"];
            }
            set
            {
                ViewState["PersonalFolderId"] = value;
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
                this.PersonalFolderId = Request.Params["PersonalFolderId"];
                this.SupPersonalFolderId = Request.Params["SupPersonalFolderId"];
                if (!string.IsNullOrEmpty(this.PersonalFolderId))
                {
                    var PersonalFolder = BLL.PersonalFolderService.GetPersonalFolderByID(this.PersonalFolderId);
                    if (PersonalFolder != null)
                    {
                        this.SupPersonalFolderId = PersonalFolder.SupPersonalFolderId;
                        this.txtTitle.Text = PersonalFolder.Title;
                        if (PersonalFolder.IsEndLever == true)
                        {
                            this.chkIsEndLevel.Checked = true;
                        }
                        else
                        {
                            chkIsEndLevel.Checked = false;
                        }
                        this.txtCode.Text = PersonalFolder.Code;
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    this.chkIsEndLevel.Enabled = BLL.PersonalFolderService.IsDeletePersonalFolder(this.PersonalFolderId);
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
                if (!BLL.PersonalFolderService.IsExistTitle(this.PersonalFolderId, this.SupPersonalFolderId, staName))
                {
                    Model.Personal_PersonalFolder newPersonalFolder = new Model.Personal_PersonalFolder
                    {
                        Title = staName,
                        SupPersonalFolderId = this.SupPersonalFolderId,
                        Code = this.txtCode.Text.Trim(),
                        CompileMan = this.CurrUser.UserId,
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked)
                    };
                    if (string.IsNullOrEmpty(this.PersonalFolderId))
                    {
                        newPersonalFolder.PersonalFolderId = SQLHelper.GetNewID(typeof(Model.Personal_PersonalFolder));
                        BLL.PersonalFolderService.AddPersonalFolder(newPersonalFolder);
                        BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加个人文件夹", newPersonalFolder.Code);
                    }
                    else
                    {
                        newPersonalFolder.PersonalFolderId = this.PersonalFolderId;
                        BLL.PersonalFolderService.UpdatePersonalFolder(newPersonalFolder);
                        BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改个人文件夹", newPersonalFolder.Code);
                    }

                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInParent("个人文件夹名称已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInParent("个人文件夹名称不能为空！");
                return;
            }
        }
    }
}