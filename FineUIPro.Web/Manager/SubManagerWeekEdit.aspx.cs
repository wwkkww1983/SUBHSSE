using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class SubSubManagerWeekEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SubManagerWeekId
        {
            get
            {
                return (string)ViewState["SubManagerWeekId"];
            }
            set
            {
                ViewState["SubManagerWeekId"] = value;
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
                this.SubManagerWeekId = Request.Params["SubManagerWeekId"];
                if (!string.IsNullOrEmpty(this.SubManagerWeekId))
                {
                    Model.Manager_SubManagerWeek subManagerWeek = BLL.SubManagerWeekService.GetSubManagerWeekById(this.SubManagerWeekId);
                    if (subManagerWeek != null)
                    {
                        this.ProjectId = subManagerWeek.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtSubManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SubManagerWeekId);
                        this.txtSubManagerWeekName.Text = subManagerWeek.SubManagerWeekName;
                        if (!string.IsNullOrEmpty(subManagerWeek.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = subManagerWeek.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", subManagerWeek.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(subManagerWeek.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.SubManagerWeekMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtSubManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.SubManagerWeekMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.SubManagerWeekMenuId;
                this.ctlAuditFlow.DataId = this.SubManagerWeekId;
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
            Model.Manager_SubManagerWeek subManagerWeek = new Model.Manager_SubManagerWeek
            {
                ProjectId = this.ProjectId,
                SubManagerWeekCode = this.txtSubManagerWeekCode.Text.Trim(),
                SubManagerWeekName = this.txtSubManagerWeekName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                subManagerWeek.CompileMan = this.drpCompileMan.SelectedValue;
            }
            subManagerWeek.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            subManagerWeek.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            subManagerWeek.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                subManagerWeek.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.SubManagerWeekId))
            {
                subManagerWeek.SubManagerWeekId = this.SubManagerWeekId;
                BLL.SubManagerWeekService.UpdateSubManagerWeek(subManagerWeek);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改分包商周报");
            }
            else
            {
                this.SubManagerWeekId = SQLHelper.GetNewID(typeof(Model.Manager_SubManagerWeek));
                subManagerWeek.SubManagerWeekId = this.SubManagerWeekId;
                BLL.SubManagerWeekService.AddSubManagerWeek(subManagerWeek);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加分包商周报");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.SubManagerWeekMenuId, this.SubManagerWeekId, (type == BLL.Const.BtnSubmit ? true : false), subManagerWeek.SubManagerWeekName, "../Manager/SubManagerWeekView.aspx?SubManagerWeekId={0}");
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
            if (string.IsNullOrEmpty(this.SubManagerWeekId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubManagerWeekAttachUrl&menuId={1}", SubManagerWeekId, BLL.Const.SubManagerWeekMenuId)));
        }
        #endregion
    }
}