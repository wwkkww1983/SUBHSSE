using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class HSECertificateView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSECertificateId
        {
            get
            {
                return (string)ViewState["HSECertificateId"];
            }
            set
            {
                ViewState["HSECertificateId"] = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        private string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
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
                this.HSECertificateId = Request.Params["HSECertificateId"];
                if (!string.IsNullOrEmpty(this.HSECertificateId))
                {
                    Model.Check_HSECertificate hseCertificate = BLL.HSECertificateService.GetHSECertificateById(this.HSECertificateId);
                    if (hseCertificate != null)
                    {
                        this.txtHSECertificateCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HSECertificateId);
                        this.txtHSECertificateName.Text = hseCertificate.HSECertificateName;
                        this.AttachUrl = hseCertificate.AttachUrl;
                        //this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHSECertificateMenuId;
                this.ctlAuditFlow.DataId = this.HSECertificateId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.HSECertificateId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSECertificate&menuId=9A034CAD-C7D5-4DE4-9FF5-828D35FFEE28", this.HSECertificateId)));
            }            
        }
        #endregion
    }
}