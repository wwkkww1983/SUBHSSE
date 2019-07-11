using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Resources
{
    public partial class ResourcesDataEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 资料主键
        /// </summary>
        public string ResourcesDataId
        {
            get
            {
                return (string)ViewState["ResourcesDataId"];
            }
            set
            {
                ViewState["ResourcesDataId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 资料编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ResourcesDataId = Request.Params["ResourcesDataId"];
                if (!string.IsNullOrEmpty(this.ResourcesDataId))
                {
                    var ResourcesData = BLL.ResourcesDataService.GetResourcesDataByResourcesDataId(this.ResourcesDataId);
                    if (ResourcesData != null)
                    {
                        this.txtFileCode.Text = ResourcesData.FileCode;
                        this.txtFileName.Text = ResourcesData.FileName;
                        this.txtRemark.Text = ResourcesData.Remark;

                        this.txtFileContent.Text = HttpUtility.HtmlDecode(ResourcesData.FileContent);
                    }
                }
            }
        }


        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.Resources_ResourcesData newResourcesData = new Model.Resources_ResourcesData
            {
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),

                FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text.Trim())
            };
            if (string.IsNullOrEmpty(this.ResourcesDataId))
            {
                this.ResourcesDataId = SQLHelper.GetNewID(typeof(Model.Resources_ResourcesData));
                newResourcesData.ResourcesDataId = this.ResourcesDataId; 
                BLL.ResourcesDataService.AddResourcesData(newResourcesData);
                BLL.LogService.AddSys_Log(this.CurrUser, newResourcesData.FileCode, newResourcesData.ResourcesDataId, BLL.Const.ResourcesDataMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newResourcesData.ResourcesDataId = this.ResourcesDataId;
                BLL.ResourcesDataService.UpdateResourcesData(newResourcesData);
                BLL.LogService.AddSys_Log(this.CurrUser, newResourcesData.FileCode, newResourcesData.ResourcesDataId, BLL.Const.ResourcesDataMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            if (string.IsNullOrEmpty(this.ResourcesDataId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ResourcesDataAttachUrl&menuId={1}", this.ResourcesDataId, BLL.Const.ResourcesDataMenuId)));
        }
        #endregion
    }
}