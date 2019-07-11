using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class ProjectSafetyDataEItemView : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string SafetyDataEItemId
        {
            get
            {
                return (string)ViewState["SafetyDataEItemId"];
            }
            set
            {
                ViewState["SafetyDataEItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string SafetyDataEId
        {
            get
            {
                return (string)ViewState["SafetyDataEId"];
            }
            set
            {
                ViewState["SafetyDataEId"] = value;
            }
        }

        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafetyDataEId = Request.Params["SafetyDataEId"];
                this.SafetyDataEItemId = Request.Params["SafetyDataEItemId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
               
                if (!string.IsNullOrEmpty(this.SafetyDataEItemId))
                {
                    var projectSafetyDataEItem = BLL.SafetyDataEItemService.GetSafetyDataEItemByID(this.SafetyDataEItemId);
                    if (projectSafetyDataEItem != null)
                    {
                        this.ProjectId = projectSafetyDataEItem.ProjectId;
                        this.SafetyDataEId = projectSafetyDataEItem.SafetyDataEId;
                        ///读取编号
                        this.txtCode.Text = projectSafetyDataEItem.Code;   ///编号是上级编号 + 流水号                        
                        this.txtTitle.Text = projectSafetyDataEItem.Title;                        
                        this.txtSubmitDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataEItem.SubmitDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(projectSafetyDataEItem.FileContent);
                    }
                }

                var getSafetyDataPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEIdProjectId(this.SafetyDataEId, this.ProjectId);
                if (getSafetyDataPlan != null)
                {
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.CheckDate);
                }
            }
        }
        
        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SafetyDataEItemId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafetyDataEAttachUrl&menuId={1}&type=-1", this.SafetyDataEItemId, BLL.Const.ProjectSafetyDataEMenuId)));    
            }
        }
        #endregion
    }
}