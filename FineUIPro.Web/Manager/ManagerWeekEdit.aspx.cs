using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerWeekEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerWeekId
        {
            get
            {
                return (string)ViewState["ManagerWeekId"];
            }
            set
            {
                ViewState["ManagerWeekId"] = value;
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
                this.ManagerWeekId = Request.Params["ManagerWeekId"];
                if (!string.IsNullOrEmpty(this.ManagerWeekId))
                {
                    Model.Manager_ManagerWeek managerWeek = BLL.ManagerWeekService.GetManagerWeekById(this.ManagerWeekId);
                    if (managerWeek != null)
                    {
                        this.ProjectId = managerWeek.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerWeekId);
                        this.txtManagerWeekName.Text = managerWeek.ManagerWeekName;
                        if (!string.IsNullOrEmpty(managerWeek.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = managerWeek.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerWeek.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerWeek.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectManagerWeekMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerWeekMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerWeekMenuId;
                this.ctlAuditFlow.DataId = this.ManagerWeekId;
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
            Model.Manager_ManagerWeek managerWeek = new Model.Manager_ManagerWeek
            {
                ProjectId = this.ProjectId,
                ManagerWeekCode = this.txtManagerWeekCode.Text.Trim(),
                ManagerWeekName = this.txtManagerWeekName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                managerWeek.CompileMan = this.drpCompileMan.SelectedValue;
            }
            managerWeek.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            managerWeek.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            managerWeek.States = BLL.Const.State_0; 
            if (type == BLL.Const.BtnSubmit)
            {
                managerWeek.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ManagerWeekId))
            {
                managerWeek.ManagerWeekId = this.ManagerWeekId;
                BLL.ManagerWeekService.UpdateManagerWeek(managerWeek);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改管理周报");
            }
            else
            {
                this.ManagerWeekId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerWeek));
                managerWeek.ManagerWeekId = this.ManagerWeekId;
                BLL.ManagerWeekService.AddManagerWeek(managerWeek);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加管理周报");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectManagerWeekMenuId, this.ManagerWeekId, (type == BLL.Const.BtnSubmit ? true : false), managerWeek.ManagerWeekName, "../Manager/ManagerWeekView.aspx?ManagerWeekId={0}");
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
            if (string.IsNullOrEmpty(this.ManagerWeekId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerWeekAttachUrl&menuId={1}", ManagerWeekId, BLL.Const.ProjectManagerWeekMenuId)));
        }
        #endregion
    }
}