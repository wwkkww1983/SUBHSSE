using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class OtherActivitiesView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string OtherActivitiesId
        {
            get
            {
                return (string)ViewState["OtherActivitiesId"];
            }
            set
            {
                ViewState["OtherActivitiesId"] = value;
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
                this.OtherActivitiesId = Request.Params["OtherActivitiesId"];
                if (!string.IsNullOrEmpty(this.OtherActivitiesId))
                {
                    Model.SafetyActivities_OtherActivities OtherActivities = BLL.OtherActivitiesService.GetOtherActivitiesById(this.OtherActivitiesId);
                    if (OtherActivities != null)
                    {                        
                        this.txtTitle.Text = OtherActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", OtherActivities.CompileDate);
                        this.txtRemark.Text = OtherActivities.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(OtherActivities.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(OtherActivities.CompileMan);
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
            if (!string.IsNullOrEmpty(this.OtherActivitiesId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherActivitiesAttachUrl&menuId={1}&type=-1", OtherActivitiesId, BLL.Const.ProjectOtherActivitiesMenuId)));
            }
        }
        #endregion
    }
}