using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class RegistrationEdit : PageBase
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
                        ///单位
                        BLL.UnitService.InitUnitDropDownList(this.drpResponsibilityUnit, registration.ProjectId, false);
                        //区域
                        BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkArea, registration.ProjectId, false);
                        if (!string.IsNullOrEmpty(registration.WorkAreaId))
                        {
                            this.drpWorkArea.SelectedValue = registration.WorkAreaId;
                        }
                        if (!string.IsNullOrEmpty(registration.ResponsibilityUnitId))
                        {
                            this.drpResponsibilityUnit.SelectedValue = registration.ResponsibilityUnitId;
                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpResponsibilityMan, registration.ProjectId, registration.ResponsibilityUnitId, false);
                            if (!string.IsNullOrEmpty(registration.ResponsibilityManId))
                            {
                                this.drpResponsibilityMan.SelectedValue = registration.ResponsibilityManId;
                            }
                        }

                        this.txtProblemTypes.Text = registration.ProblemTypes;
                        this.txtProblemDescription.Text = registration.ProblemDescription;
                        this.txtTakeSteps.Text = registration.TakeSteps;
                        
                        if (registration.RectificationPeriod.HasValue)
                        {
                            this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationPeriod);
                            this.txtH.Text = registration.RectificationPeriod.Value.Hour.ToString();
                            this.txtM.Text = registration.RectificationPeriod.Value.Minute.ToString();
                        }
                        this.txtCheckManName.Text = registration.CheckManName;
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", registration.CheckTime);                        
                        this.txtStates.Text = registration.States;
                        this.ImageUrl = registration.ImageUrl;
                        this.divImageUrl.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
                        
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpWorkArea.SelectedValue) && !string.IsNullOrEmpty(this.drpResponsibilityUnit.SelectedValue) && !string.IsNullOrEmpty(this.drpResponsibilityMan.SelectedValue))
            {
                var updateRegistration = Funs.DB.Inspection_Registration.FirstOrDefault(x => x.RegistrationId == this.RegistrationId);
                if (updateRegistration != null)
                {
                    updateRegistration.WorkAreaId = this.drpWorkArea.SelectedValue;
                    updateRegistration.ResponsibilityUnitId = this.drpResponsibilityUnit.SelectedValue;
                    updateRegistration.ProblemTypes = this.txtProblemTypes.Text.Trim();
                    updateRegistration.ResponsibilityManId = this.drpResponsibilityMan.SelectedValue;
                    updateRegistration.ProblemDescription = this.txtProblemDescription.Text.Trim();
                    updateRegistration.TakeSteps = this.txtTakeSteps.Text.Trim();
                    string periodString = this.txtRectificationPeriod.Text.Trim() + " " + this.txtH.Text.Trim() + ":" + this.txtM.Text.Trim();
                    updateRegistration.RectificationPeriod = Funs.GetNewDateTime(periodString);
                    Funs.DB.SubmitChanges();
                    BLL.LogService.AddSys_Log(this.CurrUser, periodString, updateRegistration.RegistrationId, BLL.Const.RegisterMenuId, BLL.Const.BtnModify);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
            else
            {
                Alert.ShowInTop("区域、责任单位、责任人为必选项", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpResponsibilityUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpResponsibilityMan.Items.Clear();
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpResponsibilityMan, this.CurrUser.LoginProjectId, this.drpResponsibilityUnit.SelectedValue, false);
            this.drpResponsibilityMan.SelectedIndex = 0;
        }
    }
}