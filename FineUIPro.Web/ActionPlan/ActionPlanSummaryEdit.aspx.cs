using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ActionPlan
{
    public partial class ActionPlanSummaryEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ActionPlanSummaryId
        {
            get
            {
                return (string)ViewState["ActionPlanSummaryId"];
            }
            set
            {
                ViewState["ActionPlanSummaryId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;

                this.InitDropDownList();
                this.ActionPlanSummaryId = Request.Params["ActionPlanSummaryId"];
                if (!string.IsNullOrEmpty(this.ActionPlanSummaryId))
                {
                    Model.ActionPlan_ActionPlanSummary ActionPlanSummary = BLL.ActionPlanSummaryService.GetActionPlanSummaryById(this.ActionPlanSummaryId);
                    if (ActionPlanSummary!=null)
                    {
                        this.ProjectId = ActionPlanSummary.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }  
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ActionPlanSummaryId);                        
                        this.txtName.Text = ActionPlanSummary.Name;
                       
                        if (!string.IsNullOrEmpty(ActionPlanSummary.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = ActionPlanSummary.CompileMan;
                        }
                        if (!string.IsNullOrEmpty(ActionPlanSummary.UnitId))
                        {
                            this.drpUnit.SelectedValue = ActionPlanSummary.UnitId;
                        }
                        if (ActionPlanSummary.CompileDate!=null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ActionPlanSummary.CompileDate);
                        }
                        this.txtContents.Text = HttpUtility.HtmlDecode(ActionPlanSummary.Contents);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectActionPlanSummaryMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectActionPlanSummaryMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectActionPlanSummaryMenuId;
                this.ctlAuditFlow.DataId = this.ActionPlanSummaryId;
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
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            ///加载单位
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                this.drpUnit.Enabled = false;
            }
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
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.ActionPlan_ActionPlanSummary ActionPlanSummary = new Model.ActionPlan_ActionPlanSummary
            {
                ProjectId = this.ProjectId,
                Code = this.txtCode.Text.Trim(),
                Name = this.txtName.Text.Trim(),
                Contents = HttpUtility.HtmlEncode(this.txtContents.Text)
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                ActionPlanSummary.CompileMan = this.drpCompileMan.SelectedValue;
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                ActionPlanSummary.UnitId = this.drpUnit.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                ActionPlanSummary.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            }
            ActionPlanSummary.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                ActionPlanSummary.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ActionPlanSummaryId))
            {
                ActionPlanSummary.ActionPlanSummaryId = this.ActionPlanSummaryId;
                BLL.ActionPlanSummaryService.UpdateActionPlanSummary(ActionPlanSummary);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改实施计划总结");
            }
            else
            {
                this.ActionPlanSummaryId = SQLHelper.GetNewID(typeof(Model.ActionPlan_ActionPlanSummary));
                ActionPlanSummary.ActionPlanSummaryId = this.ActionPlanSummaryId;
                BLL.ActionPlanSummaryService.AddActionPlanSummary(ActionPlanSummary);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加实施计划总结");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectActionPlanSummaryMenuId, this.ActionPlanSummaryId, (type == BLL.Const.BtnSubmit ? true : false), ActionPlanSummary.Name, "../ActionPlan/ActionPlanSummaryView.aspx?ActionPlanSummaryId={0}");
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
            if (string.IsNullOrEmpty(this.ActionPlanSummaryId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ActionPlanSummaryAttachUrl&menuId={1}", this.ActionPlanSummaryId, BLL.Const.ProjectActionPlanSummaryMenuId)));
        }
        #endregion
    }
}