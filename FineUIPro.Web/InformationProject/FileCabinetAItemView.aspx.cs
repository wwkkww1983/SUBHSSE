using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetAItemView : PageBase
    {
        /// <summary>
        /// 文件明细id
        /// </summary>
        public string FileCabinetAItemId
        {
            get
            {
                return (string)ViewState["FileCabinetAItemId"];
            }
            set
            {
                ViewState["FileCabinetAItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string FileCabinetAId
        {
            get
            {
                return (string)ViewState["FileCabinetAId"];
            }
            set
            {
                ViewState["FileCabinetAId"] = value;
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
                this.FileCabinetAId = Request.Params["FileCabinetAId"];
                this.FileCabinetAItemId = Request.Params["FileCabinetAItemId"];
                if (!string.IsNullOrEmpty(this.FileCabinetAItemId))
                {
                    var FileCabinetAItem = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(this.FileCabinetAItemId);
                    if (FileCabinetAItem != null)
                    {
                        this.FileCabinetAId = FileCabinetAItem.FileCabinetAId;
                        ///读取编号
                        this.txtCode.Text = FileCabinetAItem.Code;
                        this.txtTitle.Text = FileCabinetAItem.Title;
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCabinetAItem.FileContent);
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
            if (!string.IsNullOrEmpty(this.FileCabinetAItemId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/FileCabinetAAttachUrl&menuId={1}&type=-1", this.FileCabinetAItemId, BLL.Const.ProjectFileCabinetAMenuId)));
            }            
        }
        #endregion
    }
}