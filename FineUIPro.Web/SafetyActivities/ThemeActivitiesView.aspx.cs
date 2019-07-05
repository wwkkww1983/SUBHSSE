using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class ThemeActivitiesView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ThemeActivitiesId
        {
            get
            {
                return (string)ViewState["ThemeActivitiesId"];
            }
            set
            {
                ViewState["ThemeActivitiesId"] = value;
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
                this.ThemeActivitiesId = Request.Params["ThemeActivitiesId"];
                if (!string.IsNullOrEmpty(this.ThemeActivitiesId))
                {
                    Model.SafetyActivities_ThemeActivities ThemeActivities = BLL.ThemeActivitiesService.GetThemeActivitiesById(this.ThemeActivitiesId);
                    if (ThemeActivities != null)
                    {                        
                        this.txtTitle.Text = ThemeActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ThemeActivities.CompileDate);
                        this.txtRemark.Text = ThemeActivities.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(ThemeActivities.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(ThemeActivities.CompileMan);
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
            if (!string.IsNullOrEmpty(this.ThemeActivitiesId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ThemeActivitiesAttachUrl&menuId={1}&type=-1", ThemeActivitiesId, BLL.Const.ProjectThemeActivitiesMenuId)));
            }
        }
        #endregion
    }
}