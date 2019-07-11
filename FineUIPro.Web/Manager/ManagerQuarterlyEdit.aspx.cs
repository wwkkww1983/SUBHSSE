using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerQuarterlyEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerQuarterlyId
        {
            get
            {
                return (string)ViewState["ManagerQuarterlyId"];
            }
            set
            {
                ViewState["ManagerQuarterlyId"] = value;
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
                this.ManagerQuarterlyId = Request.Params["ManagerQuarterlyId"];
                if (!string.IsNullOrEmpty(this.ManagerQuarterlyId))
                {
                    Model.Manager_ManagerQuarterly managerQuarterly = BLL.ManagerQuarterlyService.GetManagerQuarterlyById(this.ManagerQuarterlyId);
                    if (managerQuarterly != null)
                    {
                        this.ProjectId = managerQuarterly.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtManagerQuarterlyCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerQuarterlyId);
                        this.txtManagerQuarterlyName.Text = managerQuarterly.ManagerQuarterlyName;
                        if (!string.IsNullOrEmpty(managerQuarterly.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = managerQuarterly.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerQuarterly.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerQuarterly.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectManagerQuarterlyMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtManagerQuarterlyCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerQuarterlyMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerQuarterlyMenuId;
                this.ctlAuditFlow.DataId = this.ManagerQuarterlyId;
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
            Model.Manager_ManagerQuarterly managerQuarterly = new Model.Manager_ManagerQuarterly
            {
                ProjectId = this.ProjectId,
                ManagerQuarterlyCode = this.txtManagerQuarterlyCode.Text.Trim(),
                ManagerQuarterlyName = this.txtManagerQuarterlyName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                managerQuarterly.CompileMan = this.drpCompileMan.SelectedValue;
            }
            managerQuarterly.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            managerQuarterly.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            managerQuarterly.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                managerQuarterly.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ManagerQuarterlyId))
            {
                managerQuarterly.ManagerQuarterlyId = this.ManagerQuarterlyId;
                BLL.ManagerQuarterlyService.UpdateManagerQuarterly(managerQuarterly);
                BLL.LogService.AddSys_Log(this.CurrUser, managerQuarterly.ManagerQuarterlyCode, managerQuarterly.ManagerQuarterlyId, BLL.Const.ProjectManagerQuarterlyMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ManagerQuarterlyId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerQuarterly));
                managerQuarterly.ManagerQuarterlyId = this.ManagerQuarterlyId;
                BLL.ManagerQuarterlyService.AddManagerQuarterly(managerQuarterly);
                BLL.LogService.AddSys_Log(this.CurrUser, managerQuarterly.ManagerQuarterlyCode, managerQuarterly.ManagerQuarterlyId, BLL.Const.ProjectManagerQuarterlyMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectManagerQuarterlyMenuId, this.ManagerQuarterlyId, (type == BLL.Const.BtnSubmit ? true : false), managerQuarterly.ManagerQuarterlyName, "../Manager/ManagerQuarterlyView.aspx?ManagerQuarterlyId={0}");
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
            if (string.IsNullOrEmpty(this.ManagerQuarterlyId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerQuarterlyAttachUrl&menuId={1}", ManagerQuarterlyId, BLL.Const.ProjectManagerQuarterlyMenuId)));
        }
        #endregion
    }
}