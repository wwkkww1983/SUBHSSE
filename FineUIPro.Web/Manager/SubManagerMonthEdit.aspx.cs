using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class SubManagerMonthEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SubManagerMonthId
        {
            get
            {
                return (string)ViewState["SubManagerMonthId"];
            }
            set
            {
                ViewState["SubManagerMonthId"] = value;
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
                this.SubManagerMonthId = Request.Params["SubManagerMonthId"];
                if (!string.IsNullOrEmpty(this.SubManagerMonthId))
                {
                    Model.Manager_SubManagerMonth subManagerMonth = BLL.SubManagerMonthService.GetSubManagerMonthById(this.SubManagerMonthId);
                    if (subManagerMonth != null)
                    {
                        this.ProjectId = subManagerMonth.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtSubManagerMonthCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SubManagerMonthId);
                        this.txtSubManagerMonthName.Text = subManagerMonth.SubManagerMonthName;
                        if (!string.IsNullOrEmpty(subManagerMonth.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = subManagerMonth.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", subManagerMonth.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(subManagerMonth.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.SubManagerMonthMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtSubManagerMonthCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.SubManagerMonthMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.SubManagerMonthMenuId;
                this.ctlAuditFlow.DataId = this.SubManagerMonthId;
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
            Model.Manager_SubManagerMonth subManagerMonth = new Model.Manager_SubManagerMonth
            {
                ProjectId = this.ProjectId,
                SubManagerMonthCode = this.txtSubManagerMonthCode.Text.Trim(),
                SubManagerMonthName = this.txtSubManagerMonthName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                subManagerMonth.CompileMan = this.drpCompileMan.SelectedValue;
            }
            subManagerMonth.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            subManagerMonth.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            subManagerMonth.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                subManagerMonth.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.SubManagerMonthId))
            {
                subManagerMonth.SubManagerMonthId = this.SubManagerMonthId;
                BLL.SubManagerMonthService.UpdateSubManagerMonth(subManagerMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, subManagerMonth.SubManagerMonthCode, subManagerMonth.SubManagerMonthId, BLL.Const.SubManagerMonthMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SubManagerMonthId = SQLHelper.GetNewID(typeof(Model.Manager_SubManagerMonth));
                subManagerMonth.SubManagerMonthId = this.SubManagerMonthId;
                BLL.SubManagerMonthService.AddSubManagerMonth(subManagerMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, subManagerMonth.SubManagerMonthCode, subManagerMonth.SubManagerMonthId, BLL.Const.SubManagerMonthMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.SubManagerMonthMenuId, this.SubManagerMonthId, (type == BLL.Const.BtnSubmit ? true : false), subManagerMonth.SubManagerMonthName, "../Manager/SubManagerMonthView.aspx?SubManagerMonthId={0}");
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
            if (string.IsNullOrEmpty(this.SubManagerMonthId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubManagerMonthAttachUrl&menuId={1}", SubManagerMonthId, BLL.Const.SubManagerMonthMenuId)));
        }
        #endregion
    }
}