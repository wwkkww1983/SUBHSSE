using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class SubManagerMonthView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SubManagerMonthId
        {
            get
            {
                return (string)ViewState["SubManagerMonthId"];
            }
            set
            {
                ViewState["SubManagerMonthId"] = value;
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
                this.SubManagerMonthId = Request.Params["SubManagerMonthId"];
                if (!string.IsNullOrEmpty(this.SubManagerMonthId))
                {
                    Model.Manager_SubManagerMonth subManagerMonth = BLL.SubManagerMonthService.GetSubManagerMonthById(this.SubManagerMonthId);
                    if (subManagerMonth != null)
                    {
                        this.txtSubManagerMonthCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SubManagerMonthId);
                        this.txtSubManagerMonthName.Text = subManagerMonth.SubManagerMonthName;
                        var users = BLL.UserService.GetUserByUserId(subManagerMonth.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", subManagerMonth.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(subManagerMonth.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.SubManagerMonthMenuId;
                this.ctlAuditFlow.DataId = this.SubManagerMonthId;
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
            if (!string.IsNullOrEmpty(this.SubManagerMonthId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubManagerMonthAttachUrl&menuId={1}&type=-1", SubManagerMonthId, BLL.Const.SubManagerMonthMenuId)));
            }

        }
        #endregion
    }
}