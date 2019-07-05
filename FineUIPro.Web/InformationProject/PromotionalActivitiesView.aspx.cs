using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class PromotionalActivitiesView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PromotionalActivitiesId
        {
            get
            {
                return (string)ViewState["PromotionalActivitiesId"];
            }
            set
            {
                ViewState["PromotionalActivitiesId"] = value;
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
                this.PromotionalActivitiesId = Request.Params["PromotionalActivitiesId"];
                if (!string.IsNullOrEmpty(this.PromotionalActivitiesId))
                {
                    Model.InformationProject_PromotionalActivities PromotionalActivities = BLL.PromotionalActivitiesService.GetPromotionalActivitiesById(this.PromotionalActivitiesId);
                    if (PromotionalActivities != null)
                    {
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PromotionalActivitiesId);
                        this.txtTitle.Text = PromotionalActivities.Title;
                        if (PromotionalActivities.ActivitiesDate != null)
                        {
                            this.txtActivitiesDate.Text = string.Format("{0:yyyy-MM-dd}", PromotionalActivities.ActivitiesDate);
                        }
                        var users = BLL.UserService.GetUserByUserId(PromotionalActivities.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }

                        this.txtUnits.Text = PromotionalActivities.UnitNames;
                        this.txtUsers.Text = PromotionalActivities.UserNames;                        
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(PromotionalActivities.MainContent);
                    }
                }
                
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPromotionalActivitiesMenuId;
                this.ctlAuditFlow.DataId = this.PromotionalActivitiesId;
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
            if (!string.IsNullOrEmpty(this.PromotionalActivitiesId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PromotionalActivitiesAttachUrl&menuId={1}&type=-1", PromotionalActivitiesId, BLL.Const.ProjectPromotionalActivitiesMenuId)));
            }
        }
        #endregion
    }
}