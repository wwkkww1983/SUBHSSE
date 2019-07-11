using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.SecuritySystem
{
    public partial class SafetyManageOrganization : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
        /// 主键
        /// </summary>
        public string SafetyManageOrganizationId
        {
            get
            {
                return (string)ViewState["SafetyManageOrganizationId"];
            }
            set
            {
                ViewState["SafetyManageOrganizationId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
                this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.Enabled = false;
                }

                var SafetyManageOrganization = BLL.SafetyManageOrganizationService.GetSafetyManageOrganizationByProjectId(this.ProjectId, this.drpUnit.SelectedValue);
                if (SafetyManageOrganization != null)
                {
                    this.SafetyManageOrganizationId = SafetyManageOrganization.SafetyManageOrganizationId;
                    this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyManageOrganization.SeeFile);
                }
                else
                {
                    this.SafetyManageOrganizationId = string.Empty;
                    this.txtSeeFile.Text = string.Empty;
                }
            }
        }

        #region 删除
        /// <summary>
        /// 删除明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.LogService.AddSys_Log(this.CurrUser, "删除安全管理组织机构", null, BLL.Const.ProjectSafetyManageOrganizationMenuId, BLL.Const.BtnDelete);
            BLL.SafetyManageOrganizationService.DeleteSafetyManageOrganizationByProjectid(this.ProjectId);
            ShowNotify("删除成功！", MessageBoxIcon.Success);
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectSafetyManageOrganizationMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion
        
        /// <summary>
        ///  保存按钮事件
        /// </summary>
        /// <param name="isClose"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
            ShowNotify("数据保存成功!", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveData()
        {
            Model.SecuritySystem_SafetyManageOrganization newSafetyManageOrganization = new Model.SecuritySystem_SafetyManageOrganization
            {
                SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text),
                ProjectId = this.ProjectId
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                newSafetyManageOrganization.UnitId = this.drpUnit.SelectedValue;
            }
            if (!string.IsNullOrEmpty(newSafetyManageOrganization.ProjectId) && this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                if (string.IsNullOrEmpty(this.SafetyManageOrganizationId))
                {
                    this.SafetyManageOrganizationId = newSafetyManageOrganization.SafetyManageOrganizationId = SQLHelper.GetNewID(typeof(Model.SecuritySystem_SafetyManageOrganization));
                    newSafetyManageOrganization.SafetyManageOrganizationId = this.SafetyManageOrganizationId;
                    BLL.SafetyManageOrganizationService.AddSafetyManageOrganization(newSafetyManageOrganization);
                    BLL.LogService.AddSys_Log(this.CurrUser, this.drpUnit.SelectedText, newSafetyManageOrganization.SafetyManageOrganizationId, BLL.Const.ProjectSafetyManageOrganizationMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    newSafetyManageOrganization.SafetyManageOrganizationId = this.SafetyManageOrganizationId;
                    BLL.SafetyManageOrganizationService.UpdateSafetyManageOrganization(newSafetyManageOrganization);
                    BLL.LogService.AddSys_Log(this.CurrUser, this.drpUnit.SelectedText, newSafetyManageOrganization.SafetyManageOrganizationId, BLL.Const.ProjectSafetyManageOrganizationMenuId, BLL.Const.BtnModify);
                }
            }
            else
            {
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(this.SafetyManageOrganizationId))
            {
                this.SaveData();
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyManageOrganizationAttachUrl&menuId={1}", this.SafetyManageOrganizationId, BLL.Const.ProjectSafetyManageOrganizationMenuId)));
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var SafetyManageOrganization = BLL.SafetyManageOrganizationService.GetSafetyManageOrganizationByProjectId(this.ProjectId, this.drpUnit.SelectedValue);
            if (SafetyManageOrganization != null)
            {
                this.SafetyManageOrganizationId = SafetyManageOrganization.SafetyManageOrganizationId;
                this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyManageOrganization.SeeFile);
            }
            else
            {
                this.SafetyManageOrganizationId = string.Empty;
                this.txtSeeFile.Text = string.Empty;
            }
        }
        #endregion
    }
}