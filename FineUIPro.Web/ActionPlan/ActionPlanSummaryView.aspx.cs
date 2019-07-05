using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ActionPlan
{
    public partial class ActionPlanSummaryView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ActionPlanSummaryId
        {
            get
            {
                return (string)ViewState["ActionPlanSummaryId"];
            }
            set
            {
                ViewState["ActionPlanSummaryId"] = value;
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
              
                this.ActionPlanSummaryId = Request.Params["ActionPlanSummaryId"];
                if (!string.IsNullOrEmpty(this.ActionPlanSummaryId))
                {
                    Model.ActionPlan_ActionPlanSummary ActionPlanSummary = BLL.ActionPlanSummaryService.GetActionPlanSummaryById(this.ActionPlanSummaryId);
                    if (ActionPlanSummary!=null)
                    {
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ActionPlanSummaryId);                        
                        this.txtName.Text = ActionPlanSummary.Name;
                        this.txtUnit.Text = BLL.UnitService.GetUnitNameByUnitId(ActionPlanSummary.UnitId);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(ActionPlanSummary.CompileMan);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ActionPlanSummary.CompileDate);
                        this.txtContents.Text = HttpUtility.HtmlDecode(ActionPlanSummary.Contents);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectActionPlanSummaryMenuId;
                this.ctlAuditFlow.DataId = this.ActionPlanSummaryId;
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
            if (!string.IsNullOrEmpty(this.ActionPlanSummaryId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanSummaryAttachUrl&menuId={1}&type=-1", this.ActionPlanSummaryId, BLL.Const.ProjectActionPlanSummaryMenuId)));
            }
        }
        #endregion
    }
}