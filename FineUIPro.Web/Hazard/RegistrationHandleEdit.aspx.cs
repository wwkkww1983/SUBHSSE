using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class RegistrationHandleEdit : PageBase
    {
        #region  定义项
        /// <summary>
        /// 危险观察登记主键
        /// </summary>
        public string RegistrationId
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
        /// 附件路径
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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                RegistrationId = Request.Params["RegistrationId"];
                if (!string.IsNullOrEmpty(RegistrationId))
                {
                    Model.Hazard_Registration registration = BLL.Hazard_RegistrationService.GetRegistrationByRegistrationId(RegistrationId);

                    this.txtRegistrationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RegistrationId);
                    Model.ProjectData_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(registration.WorkAreaId);
                    if (workArea != null)
                    {
                        this.txtWorkArea.Text = workArea.WorkAreaName;
                    }
                    Model.Sys_User checkMan = BLL.UserService.GetUserByUserId(registration.CheckManId);
                    if (checkMan != null)
                    {
                        this.txtCheckMan.Text = checkMan.UserName;
                    }
                    if (registration.CheckTime != null)
                    {
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", registration.CheckTime);
                    }
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(registration.ResponsibilityUnitId);
                    if (unit != null)
                    {
                        this.txtUnit.Text = unit.UnitName;
                    }
                    this.txtProblemTypes.Text = registration.ProblemTypes;
                    this.txtProblemDescription.Text = registration.ProblemDescription;
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(registration.ResponsibilityManId);
                    if (user != null)
                    {
                        this.txtResponsibilityMan.Text = user.UserName;
                    }
                    if (registration.RectificationPeriod != null)
                    {
                        this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationPeriod);
                    }
                    this.ImageUrl = registration.ImageUrl;
                    this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
                    this.txtTakeSteps.Text = registration.TakeSteps;
                    this.txtIsEffective.Text = registration.IsEffective == true ? "是" : "否";
                    this.txtHazardCode.Text = registration.HazardCode;
                    this.txtDefectsType.Text = registration.DefectsType;
                    this.txtMayLeadAccidents.Text = registration.MayLeadAccidents;
                    //整改
                    if (registration.RectificationTime != null)
                    {
                        this.txtRectificationTime.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationTime);
                    }
                    this.txtRectificationRemark.Text = registration.RectificationRemark;
                    this.RectificationImageUrl = registration.RectificationImageUrl;
                    this.divFile2.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.RectificationImageUrl);
                    //确认
                    if (registration.State == BLL.Const.RegistrationState_1)
                    {
                        this.tr1.Hidden = false;
                        this.tr2.Hidden = false;
                        if (string.IsNullOrEmpty(registration.ConfirmMan))   //已整改
                        {
                            this.txtConfirmMan.Text = this.CurrUser.UserName;
                            this.hdConfirmMan.Text = this.CurrUser.UserId;
                            this.txtConfirmDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        }
                        Model.Sys_User confirmMan = BLL.UserService.GetUserByUserId(registration.ConfirmMan);
                        if (confirmMan != null)
                        {
                            this.hdConfirmMan.Text = confirmMan.UserId;
                            this.txtConfirmMan.Text = confirmMan.UserName;
                        }
                        if (registration.ConfirmDate != null)
                        {
                            this.txtConfirmDate.Text = string.Format("{0:yyyy-MM-dd}", registration.ConfirmDate);
                        }
                        if (!string.IsNullOrEmpty(registration.HandleIdea))
                        {
                            this.tr3.Hidden = false;
                            this.txtHandleIdea.Text = registration.HandleIdea;
                        }
                    }
                    else if (registration.State == BLL.Const.RegistrationState_3)  //重新整改
                    {
                        if (!string.IsNullOrEmpty(registration.HandleIdea))
                        {
                            this.tr3.Hidden = false;
                            this.txtHandleIdea.Text = registration.HandleIdea;
                            this.txtHandleIdea.Readonly = true;
                        }
                    }
                }
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 整改后附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile2_Click(object sender, EventArgs e)
        {
            if (btnFile2.HasFile)
            {
                this.RectificationImageUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile2, this.RectificationImageUrl, UploadFileService.RegistrationFilePath);
                this.divFile2.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.RectificationImageUrl);
            }
        }
        #endregion

        #region 清空附件
        /// <summary>
        /// 清空附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            BLL.UploadFileService.DeleteFile(Funs.RootPath, this.RectificationImageUrl);
            this.ImageUrl = string.Empty;
            this.btnFile2.Reset();
            this.divFile2.InnerHtml = string.Empty;
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Hazard_Registration registration = BLL.Hazard_RegistrationService.GetRegistrationByRegistrationId(this.RegistrationId);
            registration.RectificationTime = Funs.GetNewDateTime(this.txtRectificationTime.Text.Trim());
            registration.RectificationImageUrl = this.RectificationImageUrl;
            registration.RectificationRemark = this.txtRectificationRemark.Text.Trim();
            registration.ConfirmMan = this.hdConfirmMan.Text;
            registration.ConfirmDate = Funs.GetNewDateTime(this.txtConfirmDate.Text.Trim());
            registration.HandleIdea = this.txtHandleIdea.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                if (registration.State == BLL.Const.RegistrationState_0)
                {
                    registration.State = BLL.Const.RegistrationState_1;
                }
                else if (registration.State == BLL.Const.RegistrationState_1)
                {
                    registration.State = this.rblState.SelectedValue;
                }
                else if (registration.State == BLL.Const.RegistrationState_3)
                {
                    registration.State = BLL.Const.RegistrationState_1;
                    registration.HandleIdea = string.Empty;
                }
            }
            registration.RegistrationId = this.RegistrationId;
            BLL.Hazard_RegistrationService.UpdateRegistration(registration);
            BLL.LogService.AddSys_Log(this.CurrUser, registration.HazardCode, registration.RegistrationId, BLL.Const.ProjectPayRegistrationMenuId,BLL.Const.BtnModify);
        }

        /// <summary>
        /// 处理结果变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblState.SelectedValue == BLL.Const.RegistrationState_2)
            {
                this.tr3.Hidden = true;
            }
            else
            {
                this.tr3.Hidden = false;
            }
        }
    }
}