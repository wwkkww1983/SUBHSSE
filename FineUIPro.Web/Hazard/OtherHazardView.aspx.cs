using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Hazard
{
    public partial class OtherHazardView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string OtherHazardId
        {
            get
            {
                return (string)ViewState["OtherHazardId"];
            }
            set
            {
                ViewState["OtherHazardId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.OtherHazardId = Request.Params["OtherHazardId"];
                var otherHazard = BLL.Hazard_OtherHazardService.GetOtherHazardByOtherHazardId(this.OtherHazardId);
                if (otherHazard != null)
                {
                    this.txtOtherHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.OtherHazardId);
                    this.txtOtherHazardName.Text = otherHazard.OtherHazardName;
                    Model.Sys_User compileMan = BLL.UserService.GetUserByUserId(otherHazard.CompileMan);
                    if (compileMan != null)
                    {
                        this.txtCompileMan.Text = compileMan.UserName;
                    }
                    if (otherHazard.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", otherHazard.CompileDate);
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtOtherHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.OtherHazardMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                    this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.OtherHazardMenuId;
                this.ctlAuditFlow.DataId = this.OtherHazardId;
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
            if (!string.IsNullOrEmpty(this.OtherHazardId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherHazard&menuId={1}&type=-1", this.OtherHazardId, BLL.Const.OtherHazardMenuId)));
            }
            
        }
        #endregion
    }
}