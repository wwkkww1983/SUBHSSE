using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.Exchange
{
    public partial class ContentTypeSave : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.GetButtonPower();
                string contentTypeId = Request.QueryString["ContentTypeId"];
                if (!String.IsNullOrEmpty(contentTypeId))
                {
                    var q = BLL.ContentTypeService.GetContentType(contentTypeId);
                    if (q != null)
                    {
                        txtContentTypeCode.Text = q.ContentTypeCode;
                        txtContentTypeName.Text = q.ContentTypeName;
                    }
                }
            }
        }

        private void LoadData()
        {

            btnClose.OnClientClick = ActiveWindow.GetHideReference();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Exchange_ContentType contentType = new Exchange_ContentType();
            string contentTypeId = Request.QueryString["ContentTypeId"];

            contentType.ContentTypeCode = txtContentTypeCode.Text.Trim();
            contentType.ContentTypeName = txtContentTypeName.Text.Trim();
            if (String.IsNullOrEmpty(contentTypeId))
            {
                contentTypeId = contentType.ContentTypeId = SQLHelper.GetNewID(typeof(Model.Exchange_ContentType));
                BLL.ContentTypeService.AddContentType(contentType);
                BLL.LogService.AddSys_Log(this.CurrUser, contentType.ContentTypeCode, contentType.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                contentType.ContentTypeId = contentTypeId;
                BLL.ContentTypeService.UpdateContentType(contentType);
                BLL.LogService.AddSys_Log(this.CurrUser, contentType.ContentTypeCode, contentType.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnModify);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(contentTypeId) + ActiveWindow.GetHideReference());

        }

        #region 权限设置
        /// <summary>
        /// 权限按钮设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ContentMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}