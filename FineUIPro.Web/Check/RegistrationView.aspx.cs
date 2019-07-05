using System;

namespace FineUIPro.Web.Check
{
    public partial class RegistrationView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string RegistrationId
        {
            get
            {
                return (string)ViewState["RegistrationId"];
            }
            set
            {
                ViewState["RegistrationId"] = value;
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)ViewState["ImageUrl"];
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        /// <summary>
        /// 整改后附件路径
        /// </summary>
        public string RectificationImageUrl
        {
            get
            {
                return (string)ViewState["RectificationImageUrl"];
            }
            set
            {
                ViewState["RectificationImageUrl"] = value;
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
                this.RegistrationId = Request.Params["RegistrationId"];
                if (!string.IsNullOrEmpty(this.RegistrationId))
                {
                    Model.View_Inspection_Registration registration = BLL.RegistrationService.GetRegistrationById(this.RegistrationId);
                    if (registration != null)
                    {
                        this.txtWorkAreaName.Text = registration.WorkAreaName;
                        this.txtResponsibilityUnitName.Text = registration.ResponsibilityUnitName;
                        this.txtProblemTypes.Text = registration.ProblemTypes;
                        this.txtProblemDescription.Text = registration.ProblemDescription;
                        this.txtTakeSteps.Text = registration.TakeSteps;
                        this.txtResponsibilityManName.Text = registration.ResponsibilityManName;
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationPeriod);
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);
                        this.txtRectificationTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.RectificationTime);
                        this.txtStates.Text = registration.States;
                        this.ImageUrl = registration.ImageUrl;
                        this.divImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
                        this.RectificationImageUrl = registration.RectificationImageUrl;
                        this.divRectificationImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.RectificationImageUrl);
                    }
                }
            }
        }
        #endregion
    }
}