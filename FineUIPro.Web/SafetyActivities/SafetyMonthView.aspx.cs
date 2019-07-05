using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class SafetyMonthView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyMonthId
        {
            get
            {
                return (string)ViewState["SafetyMonthId"];
            }
            set
            {
                ViewState["SafetyMonthId"] = value;
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
                this.SafetyMonthId = Request.Params["SafetyMonthId"];
                if (!string.IsNullOrEmpty(this.SafetyMonthId))
                {
                    Model.SafetyActivities_SafetyMonth SafetyMonth = BLL.SafetyMonthService.GetSafetyMonthById(this.SafetyMonthId);
                    if (SafetyMonth != null)
                    {                        
                        this.txtTitle.Text = SafetyMonth.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyMonth.CompileDate);
                        this.txtRemark.Text = SafetyMonth.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyMonth.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(SafetyMonth.CompileMan);
                    }
                }                
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
            if (!string.IsNullOrEmpty(this.SafetyMonthId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyMonthAttachUrl&menuId={1}&type=-1", SafetyMonthId, BLL.Const.ProjectSafetyMonthMenuId)));
            }
        }
        #endregion
    }
}