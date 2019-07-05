using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class FireActivitiesView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FireActivitiesId
        {
            get
            {
                return (string)ViewState["FireActivitiesId"];
            }
            set
            {
                ViewState["FireActivitiesId"] = value;
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
                this.FireActivitiesId = Request.Params["FireActivitiesId"];
                if (!string.IsNullOrEmpty(this.FireActivitiesId))
                {
                    Model.SafetyActivities_FireActivities FireActivities = BLL.FireActivitiesService.GetFireActivitiesById(this.FireActivitiesId);
                    if (FireActivities != null)
                    {                        
                        this.txtTitle.Text = FireActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", FireActivities.CompileDate);
                        this.txtRemark.Text = FireActivities.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(FireActivities.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(FireActivities.CompileMan);
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
            if (!string.IsNullOrEmpty(this.FireActivitiesId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/FireActivitiesAttachUrl&menuId={1}&type=-1", FireActivitiesId, BLL.Const.ProjectFireActivitiesMenuId)));
            }
        }
        #endregion
    }
}