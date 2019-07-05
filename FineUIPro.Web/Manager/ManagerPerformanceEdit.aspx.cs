using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerPerformanceEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerPerformanceId
        {
            get
            {
                return (string)ViewState["ManagerPerformanceId"];
            }
            set
            {
                ViewState["ManagerPerformanceId"] = value;
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
                this.ManagerPerformanceId = Request.Params["ManagerPerformanceId"];
                if (!string.IsNullOrEmpty(this.ManagerPerformanceId))
                {
                    Model.Manager_ManagerPerformance managerPerformance = BLL.ManagerPerformanceService.GetManagerPerformanceById(this.ManagerPerformanceId);
                    if (managerPerformance != null)
                    {
                        this.ProjectId = managerPerformance.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtManagerPerformanceCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerPerformanceId);
                        this.txtManagerPerformanceName.Text = managerPerformance.ManagerPerformanceName;
                        if (!string.IsNullOrEmpty(managerPerformance.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = managerPerformance.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerPerformance.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerPerformance.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectManagerPerformanceMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtManagerPerformanceCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerPerformanceMenuId, this.ProjectId, this.CurrUser.UnitId);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerPerformanceMenuId;
                this.ctlAuditFlow.DataId = this.ManagerPerformanceId;
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
            Model.Manager_ManagerPerformance managerPerformance = new Model.Manager_ManagerPerformance
            {
                ProjectId = this.ProjectId,
                ManagerPerformanceCode = this.txtManagerPerformanceCode.Text.Trim(),
                ManagerPerformanceName = this.txtManagerPerformanceName.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                managerPerformance.CompileMan = this.drpCompileMan.SelectedValue;
            }
            managerPerformance.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            managerPerformance.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            managerPerformance.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                managerPerformance.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ManagerPerformanceId))
            {
                managerPerformance.ManagerPerformanceId = this.ManagerPerformanceId;
                BLL.ManagerPerformanceService.UpdateManagerPerformance(managerPerformance);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改管理顾客评价");
            }
            else
            {
                this.ManagerPerformanceId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerPerformance));
                managerPerformance.ManagerPerformanceId = this.ManagerPerformanceId;
                BLL.ManagerPerformanceService.AddManagerPerformance(managerPerformance);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加管理顾客评价");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectManagerPerformanceMenuId, this.ManagerPerformanceId, (type == BLL.Const.BtnSubmit ? true : false), managerPerformance.ManagerPerformanceName, "../Manager/ManagerPerformanceView.aspx?ManagerPerformanceId={0}");
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
            if (string.IsNullOrEmpty(this.ManagerPerformanceId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerPerformanceAttachUrl&menuId={1}", ManagerPerformanceId, BLL.Const.ProjectManagerPerformanceMenuId)));
        }
        #endregion
    }
}