using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BLL;

namespace FineUIPro.Web.Hazard
{
    public partial class RegistrationEdit : PageBase
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
        /// 项目主键
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.RegistrationId = Request.Params["RegistrationId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.InitDropDownList();
                Funs.FineUIPleaseSelect(this.drpWorkArea);
                Funs.FineUIPleaseSelect(this.drpResponsibilityMan);
                if (!string.IsNullOrEmpty(RegistrationId))
                {
                    Model.Hazard_Registration registration = BLL.Hazard_RegistrationService.GetRegistrationByRegistrationId(RegistrationId);
                    if (registration != null)
                    {
                        this.ProjectId = registration.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtRegistrationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RegistrationId);
                        this.txtCheckMan.Text = BLL.UserService.GetUserNameByUserId(registration.CheckManId);
                        this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", registration.CheckTime);
                        if (!string.IsNullOrEmpty(registration.ResponsibilityUnitId))
                        {
                            this.drpUnit.SelectedValue = registration.ResponsibilityUnitId;
                        
                            BLL.WorkAreaService.InitWorkAreaProjetcUnitDropDownList(this.drpWorkArea, this.ProjectId, registration.ResponsibilityUnitId, true);
                            if (!string.IsNullOrEmpty(registration.WorkAreaId))
                            {
                                this.drpWorkArea.SelectedValue = registration.WorkAreaId;
                            }

                            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpResponsibilityMan, this.ProjectId, registration.ResponsibilityUnitId, true);
                            if (!string.IsNullOrEmpty(registration.ResponsibilityManId))
                            {
                                this.drpResponsibilityMan.SelectedValue = registration.ResponsibilityManId;
                            }
                        }

                        this.txtProblemTypes.Text = registration.ProblemTypes;
                        this.txtProblemDescription.Text = registration.ProblemDescription;
                        if (registration.RectificationPeriod != null)
                        {
                            this.txtRectificationPeriod.Text = string.Format("{0:yyyy-MM-dd}", registration.RectificationPeriod);
                        }
                        this.ImageUrl = registration.ImageUrl;
                        this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
                        this.txtTakeSteps.Text = registration.TakeSteps;
                        this.txtHazardCode.Text = registration.HazardCode;
                        this.txtDefectsType.Text = registration.DefectsType;
                        this.txtMayLeadAccidents.Text = registration.MayLeadAccidents;
                        if (registration.IsEffective == true)
                        {
                            this.rblIsEffective.SelectedValue = "True";
                        }
                        else
                        {
                            this.rblIsEffective.SelectedValue = "False";
                        }

                        if(string.IsNullOrEmpty(this.txtRegistrationCode.Text.Trim()))
                        {
                            this.txtRegistrationCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.HazardRegisterMenuId, this.ProjectId, registration.ResponsibilityUnitId);
                        }
                    }
                }
                else
                {
                    this.txtCheckMan.Text = this.CurrUser.UserName;
                    this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HazardRegisterMenuId;
                this.ctlAuditFlow.DataId = this.RegistrationId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
            BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsEffective, ConstValue.Group_0001, "True");
        }

        #region  单位变化事件
        /// <summary>
        /// 单位变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRegistrationCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.HazardRegisterMenuId, this.ProjectId, this.drpUnit.SelectedValue);
            BLL.WorkAreaService.InitWorkAreaProjetcUnitDropDownList(this.drpWorkArea, this.ProjectId, this.drpUnit.SelectedValue, true);
            this.drpWorkArea.SelectedValue = BLL.Const._Null;
         
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpResponsibilityMan, this.ProjectId, this.drpUnit.SelectedValue, true);
            this.drpResponsibilityMan.SelectedValue = BLL.Const._Null;
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 整改前附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile1_Click(object sender, EventArgs e)
        {
            if (btnFile1.HasFile)
            {
                this.ImageUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile1, this.ImageUrl, UploadFileService.RegistrationFilePath);
                this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.ImageUrl);
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
            BLL.UploadFileService.DeleteFile(Funs.RootPath, this.ImageUrl);
            this.ImageUrl = string.Empty;
            this.btnFile1.Reset();
            this.divFile1.InnerHtml = string.Empty;
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
            if (this.drpWorkArea.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检区域！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择责任单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpResponsibilityMan.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择责任人！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
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
            if (this.drpWorkArea.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检区域！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择责任单位！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpResponsibilityMan.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择责任人！", MessageBoxIcon.Warning);
                return;
            }
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
            Model.Hazard_Registration registration = new Model.Hazard_Registration
            {
                ProjectId = this.ProjectId,
                WorkAreaId = this.drpWorkArea.SelectedValue,
                CheckTime = Funs.GetNewDateTime(this.txtCheckTime.Text.Trim()),
                ResponsibilityUnitId = this.drpUnit.SelectedValue,
                ResponsibilityManId = this.drpResponsibilityMan.SelectedValue,
                ProblemTypes = this.txtProblemTypes.Text.Trim(),
                ProblemDescription = this.txtProblemDescription.Text.Trim(),
                RectificationPeriod = Funs.GetNewDateTime(this.txtRectificationPeriod.Text.Trim()),
                ImageUrl = this.ImageUrl,
                TakeSteps = this.txtTakeSteps.Text.Trim(),
                HazardCode = this.txtHazardCode.Text.Trim(),
                DefectsType = this.txtDefectsType.Text.Trim(),
                MayLeadAccidents = this.txtMayLeadAccidents.Text.Trim(),
                IsEffective = Convert.ToBoolean(this.rblIsEffective.SelectedValue),
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                registration.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.RegistrationId))
            {
                Model.Hazard_Registration registration1 = BLL.Hazard_RegistrationService.GetRegistrationByRegistrationId(this.RegistrationId);
                registration.State = registration1.State;
                registration.RegistrationId = this.RegistrationId;
                BLL.Hazard_RegistrationService.UpdateRegistration(registration);
                BLL.LogService.AddSys_Log(this.CurrUser, registration.HazardCode, registration.RegistrationId,BLL.Const.RegisterMenuId,BLL.Const.BtnModify);
            }
            else
            {
                registration.RegistrationId = SQLHelper.GetNewID(typeof(Model.Hazard_Registration));
                registration.CheckManId = this.CurrUser.UserId;
                registration.State = BLL.Const.RegistrationState_0;
                this.RegistrationId = registration.RegistrationId;
                BLL.Hazard_RegistrationService.AddRegistration(registration);
                BLL.LogService.AddSys_Log(this.CurrUser, registration.HazardCode, registration.RegistrationId, BLL.Const.RegisterMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.HazardRegisterMenuId, this.RegistrationId, (type == BLL.Const.BtnSubmit ? true : false), this.txtProblemTypes.Text.Trim(), "../Hazard/RegistrationView.aspx?RegistrationId={0}");
        }

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetSaveStateReference(this.hdSelect.ClientID)
                      + Window2.GetShowReference(String.Format("ShowHazardList.aspx")));
        }
        #endregion

        #region 关闭弹出窗口，重新加载树
        /// <summary>
        /// 关闭弹出窗口，重新加载树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.hdSelect.Text.Trim()))
            {
                string[] strs = this.hdSelect.Text.Trim().Split(',');
                this.txtHazardCode.Text = strs[0];
                this.txtDefectsType.Text = strs[1];
                this.txtMayLeadAccidents.Text = strs[2];
                if (string.IsNullOrEmpty(this.txtProblemTypes.Text.Trim()))
                {
                    this.txtProblemTypes.Text = this.txtDefectsType.Text;
                }
            }
        }
        #endregion
    }
}