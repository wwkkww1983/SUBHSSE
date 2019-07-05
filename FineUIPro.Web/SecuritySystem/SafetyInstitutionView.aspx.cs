using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SecuritySystem
{
    public partial class SafetyInstitutionView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyInstitutionId
        {
            get
            {
                return (string)ViewState["SafetyInstitutionId"];
            }
            set
            {
                ViewState["SafetyInstitutionId"] = value;
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
                this.SafetyInstitutionId = Request.Params["SafetyInstitutionId"];
                if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
                {
                    Model.SecuritySystem_SafetyInstitution SafetyInstitution = BLL.SafetyInstitutionService.GetSafetyInstitutionById(this.SafetyInstitutionId);
                    if (SafetyInstitution != null)
                    {                        
                        this.txtTitle.Text = SafetyInstitution.Title;
                        this.txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyInstitution.EffectiveDate);
                        this.txtRemark.Text = SafetyInstitution.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyInstitution.SeeFile);
                        this.txtScope.Text = SafetyInstitution.Scope;
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
            if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyInstitutionAttachUrl&menuId={1}&type=-1", SafetyInstitutionId, BLL.Const.ProjectSafetyInstitutionMenuId)));
            }
        }
        #endregion
    }
}