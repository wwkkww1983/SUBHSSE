using System;
using BLL;
using Model;

namespace FineUIPro.Web.Exchange
{
    public partial class ContentSave : PageBase
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
                this.drpContentType.DataTextField = "ContentTypeName";
                drpContentType.DataValueField = "ContentTypeId";
                drpContentType.DataSource = BLL.ContentTypeService.GetContentTypeLists();
                drpContentType.DataBind();
                Funs.FineUIPleaseSelect(this.drpContentType);
                this.ContentId = Request.QueryString["ContentId"];
                if (!String.IsNullOrEmpty(this.ContentId))
                {
                    var q = BLL.ContentService.GetContentById(this.ContentId);
                    if (q != null)
                    {
                        txtTheme.Text = q.Theme;
                        drpContentType.SelectedValue = q.ContentTypeId;
                        txtContents.Text = q.Contents;
                        Model.Sys_User u = BLL.UserService.GetUserByUserId(q.CompileMan);
                        if (u != null)
                        {
                            txtCompileMan.Text = u.UserName;
                            hdCompileMan.Text = u.UserId;
                        }
                        if (q.CompileDate != null)
                        {
                            txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }                      
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    hdCompileMan.Text = this.CurrUser.UserId;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            this.DataSave();
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());

        }

        private void DataSave()
        {
            Model.Exchange_Content content = new Exchange_Content
            {
                Theme = txtTheme.Text.Trim()
            };
            if (drpContentType.SelectedValue != BLL.Const._Null)
            {
                content.ContentTypeId = drpContentType.SelectedValue;
            }
            content.Contents = txtContents.Text.Trim();
            content.CompileMan = hdCompileMan.Text.Trim();
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                content.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            if (String.IsNullOrEmpty(this.ContentId))
            {
                this.ContentId = SQLHelper.GetNewID(typeof(Model.Exchange_Content));
                content.ContentId = this.ContentId;
                BLL.ContentService.AddContent(content);
                BLL.LogService.AddSys_Log(this.CurrUser, content.Theme, content.ContentId, BLL.Const.ContentMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                content.ContentId = this.ContentId;
                BLL.ContentService.UpdateContent(content);
                BLL.LogService.AddSys_Log(this.CurrUser, content.Theme, content.ContentId, BLL.Const.ContentMenuId, BLL.Const.BtnModify);
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
            if (string.IsNullOrEmpty(this.ContentId))
            {
                DataSave();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Exchange&menuId=" + BLL.Const.ContentMenuId, this.ContentId)));
        }
        #endregion
    }
}