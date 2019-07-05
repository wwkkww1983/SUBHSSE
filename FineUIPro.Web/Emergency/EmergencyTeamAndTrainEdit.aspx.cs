using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Emergency
{
    public partial class EmergencyTeamAndTrainEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = BLL.EmergencyTeamAndTrainService.GetEmergencyTeamAndTrainById(this.FileId);
                    if (EmergencyTeamAndTrain != null)
                    {
                        this.ProjectId = EmergencyTeamAndTrain.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = EmergencyTeamAndTrain.FileName;

                        if (!string.IsNullOrEmpty(EmergencyTeamAndTrain.UnitId))
                        {
                            this.drpUnit.SelectedValue = EmergencyTeamAndTrain.UnitId;
                        }
                        if (!string.IsNullOrEmpty(EmergencyTeamAndTrain.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = EmergencyTeamAndTrain.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", EmergencyTeamAndTrain.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(EmergencyTeamAndTrain.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var FileCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.ProjectId);
                    if (FileCodeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCodeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEmergencyTeamAndTrainMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
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
            Model.Emergency_EmergencyTeamAndTrain EmergencyTeamAndTrain = new Model.Emergency_EmergencyTeamAndTrain
            {
                ProjectId = this.ProjectId,
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim()
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                EmergencyTeamAndTrain.UnitId = this.drpUnit.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                EmergencyTeamAndTrain.CompileMan = this.drpCompileMan.SelectedValue;
            }
            EmergencyTeamAndTrain.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            EmergencyTeamAndTrain.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            EmergencyTeamAndTrain.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                EmergencyTeamAndTrain.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                EmergencyTeamAndTrain.FileId = this.FileId;
                BLL.EmergencyTeamAndTrainService.UpdateEmergencyTeamAndTrain(EmergencyTeamAndTrain);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改应急队伍/培训", EmergencyTeamAndTrain.FileId);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.Emergency_EmergencyTeamAndTrain));
                EmergencyTeamAndTrain.FileId = this.FileId;
                BLL.EmergencyTeamAndTrainService.AddEmergencyTeamAndTrain(EmergencyTeamAndTrain);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加应急队伍/培训", EmergencyTeamAndTrain.FileId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), EmergencyTeamAndTrain.FileName, "../Emergency/EmergencyTeamAndTrainView.aspx?FileId={0}");
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
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EmergencyTeamAndTrainAttachUrl&menuId={1}", FileId, BLL.Const.ProjectEmergencyTeamAndTrainMenuId)));
        }
        #endregion
    }
}