using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerTotalId
        {
            get
            {
                return (string)ViewState["ManagerTotalId"];
            }
            set
            {
                ViewState["ManagerTotalId"] = value;
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
                this.ManagerTotalId = Request.Params["ManagerTotalId"];
                if (!string.IsNullOrEmpty(this.ManagerTotalId))
                {
                    Model.Manager_ManagerTotal managerTotal = BLL.ManagerTotalService.GetManagerTotalById(this.ManagerTotalId);
                    if (managerTotal != null)
                    {
                        this.ProjectId = managerTotal.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtManagerTotalCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerTotalId);
                        this.txtManagerTotalName.Text = managerTotal.ManagerTotalName;
                        if (!string.IsNullOrEmpty(managerTotal.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = managerTotal.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerTotal.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerTotal.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectManagerTotalMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtManagerTotalCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerTotalMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerTotalMenuId;
                this.ctlAuditFlow.DataId = this.ManagerTotalId;
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
            Model.Manager_ManagerTotal managerTotal = new Model.Manager_ManagerTotal
            {
                ProjectId = this.ProjectId,
                ManagerTotalCode = this.txtManagerTotalCode.Text.Trim(),
                ManagerTotalName = this.txtManagerTotalName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                managerTotal.CompileMan = this.drpCompileMan.SelectedValue;
            }
            managerTotal.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            managerTotal.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            managerTotal.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                managerTotal.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ManagerTotalId))
            {
                managerTotal.ManagerTotalId = this.ManagerTotalId;
                BLL.ManagerTotalService.UpdateManagerTotal(managerTotal);
                BLL.LogService.AddSys_Log(this.CurrUser, managerTotal.ManagerTotalCode, managerTotal.ManagerTotalId, BLL.Const.ProjectManagerTotalMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ManagerTotalId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotal));
                managerTotal.ManagerTotalId = this.ManagerTotalId;
                BLL.ManagerTotalService.AddManagerTotal(managerTotal);
                BLL.LogService.AddSys_Log(this.CurrUser, managerTotal.ManagerTotalCode, managerTotal.ManagerTotalId, BLL.Const.ProjectManagerTotalMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectManagerTotalMenuId, this.ManagerTotalId, (type == BLL.Const.BtnSubmit ? true : false), managerTotal.ManagerTotalName, "../Manager/ManagerTotalView.aspx?ManagerTotalId={0}");
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
            if (string.IsNullOrEmpty(this.ManagerTotalId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerTotalAttachUrl&menuId={1}", ManagerTotalId, BLL.Const.ProjectManagerTotalMenuId)));
        }
        #endregion
    }
}