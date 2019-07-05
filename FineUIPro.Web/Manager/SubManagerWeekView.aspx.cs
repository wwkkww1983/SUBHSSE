using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class SubSubManagerWeekView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SubManagerWeekId
        {
            get
            {
                return (string)ViewState["SubManagerWeekId"];
            }
            set
            {
                ViewState["SubManagerWeekId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SubManagerWeekId = Request.Params["SubManagerWeekId"];
                if (!string.IsNullOrEmpty(this.SubManagerWeekId))
                {
                    Model.Manager_SubManagerWeek subManagerWeek = BLL.SubManagerWeekService.GetSubManagerWeekById(this.SubManagerWeekId);
                    if (subManagerWeek != null)
                    {
                        this.txtSubManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SubManagerWeekId);
                        this.txtSubManagerWeekName.Text = subManagerWeek.SubManagerWeekName;
                        var users = BLL.UserService.GetUserByUserId(subManagerWeek.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", subManagerWeek.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(subManagerWeek.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.SubManagerWeekMenuId;
                this.ctlAuditFlow.DataId = this.SubManagerWeekId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SubManagerWeekId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubManagerWeekAttachUrl&menuId={1}&type=-1", SubManagerWeekId, BLL.Const.SubManagerWeekMenuId)));
            }

        }
        #endregion
    }
}