using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class MonthlyRatingView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthlyRatingId
        {
            get
            {
                return (string)ViewState["MonthlyRatingId"];
            }
            set
            {
                ViewState["MonthlyRatingId"] = value;
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
                this.MonthlyRatingId = Request.Params["MonthlyRatingId"];
                if (!string.IsNullOrEmpty(this.MonthlyRatingId))
                {
                    Model.SafetyActivities_MonthlyRating MonthlyRating = BLL.MonthlyRatingService.GetMonthlyRatingById(this.MonthlyRatingId);
                    if (MonthlyRating != null)
                    {                        
                        this.txtTitle.Text = MonthlyRating.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", MonthlyRating.CompileDate);
                        this.txtRemark.Text = MonthlyRating.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(MonthlyRating.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(MonthlyRating.CompileMan);
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
            if (!string.IsNullOrEmpty(this.MonthlyRatingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthlyRatingAttachUrl&menuId={1}&type=-1", MonthlyRatingId, BLL.Const.ProjectMonthlyRatingMenuId)));
            }
        }
        #endregion
    }
}