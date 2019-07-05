using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class HealthEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HealthId
        {
            get
            {
                return (string)ViewState["HealthId"];
            }
            set
            {
                ViewState["HealthId"] = value;
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
                this.InitDropDownList();
                this.HealthId = Request.Params["HealthId"];
                if (!string.IsNullOrEmpty(this.HealthId))
                {
                    Model.Manager_Health health = BLL.HealthService.GetHealthById(this.HealthId);
                    if (health != null)
                    {
                        this.ProjectId = health.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtHealthCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HealthId);
                        this.txtHealthName.Text = health.HealthName;
                        if (!string.IsNullOrEmpty(health.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = health.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", health.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(health.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.HealthMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtHealthCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.HealthMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HealthMenuId;
                this.ctlAuditFlow.DataId = this.HealthId;
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
            this.drpCompileMan.DataValueField = "UserId";
            this.drpCompileMan.DataTextField = "UserName";
            this.drpCompileMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.ProjectId);
            this.drpCompileMan.DataBind();
            Funs.FineUIPleaseSelect(this.drpCompileMan);
        }

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
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Manager_Health health = new Model.Manager_Health
            {
                ProjectId = this.ProjectId,
                HealthCode = this.txtHealthCode.Text.Trim(),
                HealthName = this.txtHealthName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                health.CompileMan = this.drpCompileMan.SelectedValue;
            }
            health.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            health.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            health.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                health.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.HealthId))
            {
                health.HealthId = this.HealthId;
                BLL.HealthService.UpdateHealth(health);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改工程现场环境与职业健康月报");
            }
            else
            {
                this.HealthId = SQLHelper.GetNewID(typeof(Model.Manager_Health));
                health.HealthId = this.HealthId;
                BLL.HealthService.AddHealth(health);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加工程现场环境与职业健康月报");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.HealthMenuId, this.HealthId, (type == BLL.Const.BtnSubmit ? true : false), health.HealthName, "../Manager/HealthView.aspx?HealthId={0}");
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
            if (string.IsNullOrEmpty(this.HealthId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HealthAttachUrl&menuId={1}", HealthId, BLL.Const.HealthMenuId)));
        }
        #endregion
    }
}