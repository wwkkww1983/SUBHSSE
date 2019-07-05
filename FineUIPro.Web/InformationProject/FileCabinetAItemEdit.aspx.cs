using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetAItemEdit : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string FileCabinetAItemId
        {
            get
            {
                return (string)ViewState["FileCabinetAItemId"];
            }
            set
            {
                ViewState["FileCabinetAItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string FileCabinetAId
        {
            get
            {
                return (string)ViewState["FileCabinetAId"];
            }
            set
            {
                ViewState["FileCabinetAId"] = value;
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
                this.FileCabinetAId = Request.Params["FileCabinetAId"];
                this.FileCabinetAItemId = Request.Params["FileCabinetAItemId"];
                if (!string.IsNullOrEmpty(this.FileCabinetAItemId))
                {
                    var FileCabinetAItem = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(this.FileCabinetAItemId);
                    if (FileCabinetAItem != null)
                    {
                        this.FileCabinetAId = FileCabinetAItem.FileCabinetAId;
                        ///读取编号
                        this.txtCode.Text = FileCabinetAItem.Code;
                        this.txtTitle.Text = FileCabinetAItem.Title;
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCabinetAItem.FileContent);
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
                Model.InformationProject_FileCabinetAItem newFileCabinetAItem = new Model.InformationProject_FileCabinetAItem
                {
                    FileCabinetAId = this.FileCabinetAId,
                    Title = this.txtTitle.Text.Trim(),
                    Code = this.txtCode.Text.Trim(),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text),
                    CompileDate = System.DateTime.Now,
                    CompileMan = this.CurrUser.UserId
                };
                if (string.IsNullOrEmpty(this.FileCabinetAItemId))
                {
                    newFileCabinetAItem.IsMenu = false;
                    this.FileCabinetAItemId = newFileCabinetAItem.FileCabinetAItemId = SQLHelper.GetNewID(typeof(Model.InformationProject_FileCabinetAItem));
                    BLL.FileCabinetAItemService.AddFileCabinetAItem(newFileCabinetAItem);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加项目文件", newFileCabinetAItem.Code);
                }
                else
                {
                    newFileCabinetAItem.FileCabinetAItemId = this.FileCabinetAItemId;
                    BLL.FileCabinetAItemService.UpdateFileCabinetAItem(newFileCabinetAItem);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改项目文件", newFileCabinetAItem.Code);
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
            if (string.IsNullOrEmpty(this.FileCabinetAItemId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/FileCabinetAAttachUrl&menuId={1}", this.FileCabinetAItemId, BLL.Const.ProjectFileCabinetAMenuId)));
        }
        #endregion
    }
}