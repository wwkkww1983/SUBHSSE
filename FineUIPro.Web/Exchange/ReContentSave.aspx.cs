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
    public partial class ReContentSave : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.GetButtonPower();
                string reContentId = Request.QueryString["ReContentId"];
                if (!String.IsNullOrEmpty(reContentId))
                {
                    var q = BLL.ReContentService.GetReContentById(reContentId);
                    if (q != null)
                    {
                        txtContents.Text = q.Contents;
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
            Model.Exchange_ReContent reContent = new Exchange_ReContent();
            string reContentId = Request.QueryString["ReContentId"];

            reContent.Contents = txtContents.Text.Trim();
            reContent.CompileMan = this.CurrUser.UserId;
            reContent.CompileDate = DateTime.Now;
            if (String.IsNullOrEmpty(reContentId))
            {
                reContent.ReContentId = SQLHelper.GetNewID(typeof(Model.Exchange_ReContent));
                reContent.ContentId = Request.Params["ContentId"];
                BLL.ReContentService.AddReContent(reContent);
                BLL.LogService.AddSys_Log(this.CurrUser,  "增加回帖信息", reContent.ReContentId,BLL.Const.ContentMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                Model.Exchange_ReContent reContent1 = BLL.ReContentService.GetReContentById(reContentId);
                reContent.ReContentId = reContentId;
                reContent.ContentId = reContent1.ContentId;
                BLL.ReContentService.UpdateReContent(reContent);
                BLL.LogService.AddSys_Log(this.CurrUser, "增加回帖信息", reContent.ReContentId, BLL.Const.ContentMenuId, BLL.Const.BtnModify);
            }
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());

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