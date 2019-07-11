using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalFolderItemEdit : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string PersonalFolderItemId
        {
            get
            {
                return (string)ViewState["PersonalFolderItemId"];
            }
            set
            {
                ViewState["PersonalFolderItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
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
                this.PersonalFolderId = Request.Params["PersonalFolderId"];
                this.PersonalFolderItemId = Request.Params["PersonalFolderItemId"];
                if (!string.IsNullOrEmpty(this.PersonalFolderItemId))
                {
                    var personalFolderItem = BLL.PersonalFolderItemService.GetPersonalFolderItemByID(this.PersonalFolderItemId);
                    if (personalFolderItem != null)
                    {
                        this.PersonalFolderId = personalFolderItem.PersonalFolderId;
                        this.txtCode.Text = personalFolderItem.Code;
                        this.txtTitle.Text = personalFolderItem.Title;
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(personalFolderItem.FileContent);
                    }
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
                Model.Personal_PersonalFolderItem newPersonalFolderItem = new Model.Personal_PersonalFolderItem
                {
                    PersonalFolderId = this.PersonalFolderId,
                    Title = this.txtTitle.Text.Trim(),
                    Code = this.txtCode.Text.Trim(),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text),
                    CompileDate = System.DateTime.Now
                };
                if (string.IsNullOrEmpty(this.PersonalFolderItemId))
                {
                    this.PersonalFolderItemId =  SQLHelper.GetNewID(typeof(Model.Personal_PersonalFolderItem));
                    newPersonalFolderItem.PersonalFolderItemId = this.PersonalFolderItemId;
                    BLL.PersonalFolderItemService.AddPersonalFolderItem(newPersonalFolderItem);
                    BLL.LogService.AddSys_Log(this.CurrUser, newPersonalFolderItem.Code, newPersonalFolderItem.PersonalFolderItemId,BLL.Const.PersonalFolderMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    newPersonalFolderItem.PersonalFolderItemId = this.PersonalFolderItemId;
                    BLL.PersonalFolderItemService.UpdatePersonalFolderItem(newPersonalFolderItem);
                    BLL.LogService.AddSys_Log(this.CurrUser, newPersonalFolderItem.Code, newPersonalFolderItem.PersonalFolderItemId, BLL.Const.PersonalFolderMenuId, BLL.Const.BtnModify);
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
            if (string.IsNullOrEmpty(this.PersonalFolderItemId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonalFolderAttachUrl&menuId={1}&type=0", this.PersonalFolderItemId, BLL.Const.PersonalFolderMenuId)));
        }
        #endregion
    }
}