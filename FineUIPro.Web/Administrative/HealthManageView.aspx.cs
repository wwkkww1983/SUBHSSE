using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class HealthManageView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HealthManageId
        {
            get
            {
                return (string)ViewState["HealthManageId"];
            }
            set
            {
                ViewState["HealthManageId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                
                this.HealthManageId = Request.Params["HealthManageId"];
                if (!string.IsNullOrEmpty(this.HealthManageId))
                {
                    Model.Administrative_HealthManage healthManage = BLL.HealthManageService.GetHealthManageById(this.HealthManageId);
                    if (healthManage != null)
                    {
                        if (!string.IsNullOrEmpty(healthManage.PersonId))
                        {
                            var person = BLL.UserService.GetUserByUserId(healthManage.PersonId);
                            if (person!=null)
                            {
                                this.txtPersonName.Text = person.UserName;
                            }
                        }
                        if (healthManage.Age != null)
                        {
                            this.txtAge.Text = Convert.ToString(healthManage.Age);
                        }
                        this.txtBloodtype.Text = healthManage.Bloodtype;
                        this.txtHealthState.Text = healthManage.HealthState;
                        this.txtTaboo.Text = healthManage.Taboo;
                        if (healthManage.CheckTime != null)
                        {
                            this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", healthManage.CheckTime);
                        }
                        this.txtRemark.Text = healthManage.Remark;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HealthManageMenuId;
                this.ctlAuditFlow.DataId = this.HealthManageId;
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
            if (!string.IsNullOrEmpty(this.HealthManageId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HealthManageAttachUrl&menuId={1}", this.HealthManageId, BLL.Const.HealthManageMenuId)));
            }
        }
        #endregion
    }
}