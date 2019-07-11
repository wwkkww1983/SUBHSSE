using BLL;
using System;
using System.Web;

namespace FineUIPro.Web.SecuritySystem
{
    public partial class SafetyInstitutionEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyInstitutionId
        {
            get
            {
                return (string)ViewState["SafetyInstitutionId"];
            }
            set
            {
                ViewState["SafetyInstitutionId"] = value;
            }
        }
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                   
                this.SafetyInstitutionId = Request.Params["SafetyInstitutionId"];
                this.UnitId = Request.Params["UnitId"];
                if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
                {
                    Model.SecuritySystem_SafetyInstitution SafetyInstitution = BLL.SafetyInstitutionService.GetSafetyInstitutionById(this.SafetyInstitutionId);
                    if (SafetyInstitution != null)
                    {
                        this.ProjectId = SafetyInstitution.ProjectId;
                        this.txtTitle.Text = SafetyInstitution.Title;
                        this.txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyInstitution.EffectiveDate);
                        this.txtRemark.Text = SafetyInstitution.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyInstitution.SeeFile);                       
                        this.txtScope.Text = SafetyInstitution.Scope;
                    }
                }
                else
                {                    
                    this.txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectSafetyInstitutionMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    
                    this.txtTitle.Text = this.SimpleForm1.Title;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {           
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.SecuritySystem_SafetyInstitution newSafetyInstitution = new Model.SecuritySystem_SafetyInstitution
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                EffectiveDate = Funs.GetNewDateTime(this.txtEffectiveDate.Text.Trim()),
                Scope = this.txtScope.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text)
            };
            if (!string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
                newSafetyInstitution.SafetyInstitutionId = this.SafetyInstitutionId;
                BLL.SafetyInstitutionService.UpdateSafetyInstitution(newSafetyInstitution);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyInstitution.Title, newSafetyInstitution.SafetyInstitutionId,BLL.Const.ProjectSafetyInstitutionMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.SafetyInstitutionId = SQLHelper.GetNewID(typeof(Model.SecuritySystem_SafetyInstitution));
                newSafetyInstitution.UnitId = this.UnitId;
                newSafetyInstitution.SafetyInstitutionId = this.SafetyInstitutionId;
                BLL.SafetyInstitutionService.AddSafetyInstitution(newSafetyInstitution);
                BLL.LogService.AddSys_Log(this.CurrUser, newSafetyInstitution.Title, newSafetyInstitution.SafetyInstitutionId, BLL.Const.ProjectSafetyInstitutionMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.SafetyInstitutionId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyInstitutionAttachUrl&menuId={1}", SafetyInstitutionId,BLL.Const.ProjectSafetyInstitutionMenuId)));
        }
        #endregion
    }
}