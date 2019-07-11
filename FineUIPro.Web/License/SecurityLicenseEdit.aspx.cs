using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class SecurityLicenseEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SecurityLicenseId
        {
            get
            {
                return (string)ViewState["SecurityLicenseId"];
            }
            set
            {
                ViewState["SecurityLicenseId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.SetDrop();
                this.SecurityLicenseId = Request.Params["SecurityLicenseId"];
                if (!string.IsNullOrEmpty(this.SecurityLicenseId))
                {
                    Model.License_SecurityLicense securityLicense = BLL.SecurityLicenseService.GetSecurityLicenseById(this.SecurityLicenseId);
                    if (securityLicense != null)
                    {
                        this.ProjectId = securityLicense.ProjectId;
                        this.SetDrop();
                        this.txtSecurityLicenseCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SecurityLicenseId);
                        this.txtSecurityLicenseName.Text = securityLicense.SecurityLicenseName;
                        this.txtNewProjectName.Text = securityLicense.SecurityLicenseName;
                        this.txtNewProjectPart.Text = securityLicense.NewProjectPart;
                        if (!string.IsNullOrEmpty(securityLicense.UnitId))
                        {
                            this.drpUnitId.SelectedValue = securityLicense.UnitId;
                        }
                        this.txtLimitDate.Text = securityLicense.LimitDate;
                        if (!string.IsNullOrEmpty(securityLicense.SignMan))
                        {
                            this.drpSignMan.SelectedValue = securityLicense.SignMan;
                        }
                        if (securityLicense.SignDate != null)
                        {
                            this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", securityLicense.SignDate);
                        }
                        this.txtConfirmMan.Text = securityLicense.ConfirmMan;
                        this.txtAddMeasureMan.Text = securityLicense.AddMeasureMan;
                        this.txtAddMeasure.Text = securityLicense.AddMeasure;
                        this.txtSecurityLicenseContents.Text = HttpUtility.HtmlDecode(securityLicense.SecurityLicenseContents);
                    }
                }
                else
                {
                    this.drpSignMan.SelectedValue = this.CurrUser.UserId;
                    this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                    var pcodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectSecurityLicenseMenuId, this.ProjectId);
                    if (pcodeTemplateRule != null)
                    {
                        this.txtSecurityLicenseContents.Text = HttpUtility.HtmlDecode(pcodeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtSecurityLicenseCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectSecurityLicenseMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// 下拉框
        /// </summary>
        private void SetDrop()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            BLL.UserService.InitUserDropDownList(this.drpSignMan, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.License_SecurityLicense securityLicense = new Model.License_SecurityLicense
            {
                ProjectId = this.ProjectId,
                SecurityLicenseCode = this.txtSecurityLicenseCode.Text.Trim(),
                SecurityLicenseName = this.txtSecurityLicenseName.Text.Trim(),
                NewProjectName = this.txtNewProjectName.Text.Trim(),
                NewProjectPart = this.txtNewProjectPart.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                securityLicense.UnitId = this.drpUnitId.SelectedValue;
            }
            securityLicense.LimitDate = this.txtLimitDate.Text.Trim();
            if (this.drpSignMan.SelectedValue != BLL.Const._Null)
            {
                securityLicense.SignMan = this.drpSignMan.SelectedValue;
            }
            securityLicense.SignDate = Funs.GetNewDateTime(this.txtSignDate.Text.Trim());
            securityLicense.ConfirmMan = this.txtConfirmMan.Text.Trim();
            securityLicense.AddMeasure = this.txtAddMeasure.Text.Trim();
            securityLicense.AddMeasureMan = this.txtAddMeasureMan.Text.Trim();
            securityLicense.SecurityLicenseContents = HttpUtility.HtmlEncode(this.txtSecurityLicenseContents.Text.Trim());

            if (!string.IsNullOrEmpty(this.SecurityLicenseId))
            {
                securityLicense.SecurityLicenseId = this.SecurityLicenseId;
                BLL.SecurityLicenseService.UpdateSecurityLicense(securityLicense);
                BLL.LogService.AddSys_Log(this.CurrUser, securityLicense.SecurityLicenseCode, securityLicense.SecurityLicenseId, BLL.Const.ProjectSecurityLicenseMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SecurityLicenseId = SQLHelper.GetNewID(typeof(Model.License_SecurityLicense));
                securityLicense.SecurityLicenseId = this.SecurityLicenseId;
                BLL.SecurityLicenseService.AddSecurityLicense(securityLicense);
                BLL.LogService.AddSys_Log(this.CurrUser, securityLicense.SecurityLicenseCode, securityLicense.SecurityLicenseId, BLL.Const.ProjectSecurityLicenseMenuId, BLL.Const.BtnAdd);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SecurityLicenseAttachUrl&type=-1", SecurityLicenseId, BLL.Const.ProjectSecurityLicenseMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.SecurityLicenseId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SecurityLicenseAttachUrl&menuId={1}", SecurityLicenseId, BLL.Const.ProjectSecurityLicenseMenuId)));
            }
        }
        #endregion
    }
}