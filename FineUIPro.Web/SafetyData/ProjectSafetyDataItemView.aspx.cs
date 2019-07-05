using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataItemView : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string SafetyDataItemId
        {
            get
            {
                return (string)ViewState["SafetyDataItemId"];
            }
            set
            {
                ViewState["SafetyDataItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string SafetyDataId
        {
            get
            {
                return (string)ViewState["SafetyDataId"];
            }
            set
            {
                ViewState["SafetyDataId"] = value;
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
                this.SafetyDataId = Request.Params["SafetyDataId"];
                this.SafetyDataItemId = Request.Params["SafetyDataItemId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(this.SafetyDataItemId))
                {
                    var projectSafetyDataItem = BLL.SafetyDataItemService.GetSafetyDataItemByID(this.SafetyDataItemId);
                    if (projectSafetyDataItem != null)
                    {
                        this.ProjectId = projectSafetyDataItem.ProjectId;
                        this.SafetyDataId = projectSafetyDataItem.SafetyDataId;
                        ///读取编号
                        this.txtCode.Text = projectSafetyDataItem.Code;   ///编号是上级编号 + 流水号                        
                        this.txtTitle.Text = projectSafetyDataItem.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataItem.CompileDate);
                        this.txtSubmitDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataItem.SubmitDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(projectSafetyDataItem.FileContent);
                    }
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
            if (!string.IsNullOrEmpty(this.SafetyDataItemId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafetyDataAttachUrl&menuId={1}", this.SafetyDataItemId, BLL.Const.ProjectSafetyDataMenuId)));    
            }
        }
        #endregion
    }
}