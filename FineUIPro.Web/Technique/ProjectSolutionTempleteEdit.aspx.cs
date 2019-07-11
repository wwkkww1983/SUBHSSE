using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class ProjectSolutionTempleteEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string TempleteId
        {
            get
            {
                return (string)ViewState["TempleteId"];
            }
            set
            {
                ViewState["TempleteId"] = value;
            }
        }

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
                this.drpTempleteType.DataValueField = "SolutionTempleteTypeCode";
                this.drpTempleteType.DataTextField = "SolutionTempleteTypeName";
                this.drpTempleteType.DataSource = BLL.SolutionTempleteTypeService.GetSolutionTempleteType();
                this.drpTempleteType.DataBind();
                Funs.FineUIPleaseSelect(this.drpTempleteType);
                this.drpCompileMan.DataValueField = "UserId";
                this.drpCompileMan.DataTextField = "UserName";
                this.drpCompileMan.DataSource = BLL.UserService.GetUserList();
                this.drpCompileMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpCompileMan);
                this.TempleteId = Request.Params["TempleteId"];
                if (!string.IsNullOrEmpty(this.TempleteId))
                {
                    Model.Technique_ProjectSolutionTemplete projectSolutionTemplete = BLL.ProjectSolutionTempleteService.GetProjectSolutionTempleteById(this.TempleteId);
                    if (projectSolutionTemplete != null)
                    {
                        this.txtTempleteCode.Text = projectSolutionTemplete.TempleteCode;
                        this.txtTempleteName.Text = projectSolutionTemplete.TempleteName;
                        if (!string.IsNullOrEmpty(projectSolutionTemplete.TempleteType))
                        {
                            this.drpTempleteType.SelectedValue = projectSolutionTemplete.TempleteType;
                        }
                        if (!string.IsNullOrEmpty(projectSolutionTemplete.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = projectSolutionTemplete.CompileMan;
                        }
                        if (projectSolutionTemplete.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", projectSolutionTemplete.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(projectSolutionTemplete.FileContents);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectSolutionTempleteMenuId, this.CurrUser.LoginProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
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
            Model.Technique_ProjectSolutionTemplete projectSolutionTemplete = new Model.Technique_ProjectSolutionTemplete
            {
                TempleteCode = this.txtTempleteCode.Text.Trim(),
                TempleteName = this.txtTempleteName.Text.Trim()
            };
            if (this.drpTempleteType.SelectedValue != BLL.Const._Null)
            {
                projectSolutionTemplete.TempleteType = this.drpTempleteType.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择类型", MessageBoxIcon.Warning);
                return;
            }
            projectSolutionTemplete.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                projectSolutionTemplete.CompileMan = this.drpCompileMan.SelectedValue;
            }
            projectSolutionTemplete.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.TempleteId))
            {
                projectSolutionTemplete.TempleteId = this.TempleteId;
                BLL.ProjectSolutionTempleteService.UpdateProjectSolutionTemplete(projectSolutionTemplete);
                BLL.LogService.AddSys_Log(this.CurrUser, projectSolutionTemplete.TempleteCode, projectSolutionTemplete.TempleteId,BLL.Const.ProjectSolutionTempleteMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.TempleteId = SQLHelper.GetNewID(typeof(Model.Technique_ProjectSolutionTemplete));
                projectSolutionTemplete.TempleteId = this.TempleteId;
                BLL.ProjectSolutionTempleteService.AddProjectSolutionTemplete(projectSolutionTemplete);
                BLL.LogService.AddSys_Log(this.CurrUser, projectSolutionTemplete.TempleteCode, projectSolutionTemplete.TempleteId, BLL.Const.ProjectSolutionTempleteMenuId, BLL.Const.BtnAdd);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
    }
}