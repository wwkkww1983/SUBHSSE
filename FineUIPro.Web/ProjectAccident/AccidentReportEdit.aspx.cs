using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentReportEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentReportId
        {
            get
            {
                return (string)ViewState["AccidentReportId"];
            }
            set
            {
                ViewState["AccidentReportId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                BLL.ProjectService.InitProjectDropDownList(ddlProjectId, true);
                //加载单位下拉选项
                BLL.UnitService.InitUnitDropDownList(this.ddlUnitId, this.CurrUser.LoginProjectId, true);

                this.AccidentReportId = Request.Params["AccidentReportId"];
                var accidentReport = BLL.AccidentReportService.GetAccidentReportById(this.AccidentReportId);
                if (accidentReport != null)
                {
                    this.txtWorkArea.Text = accidentReport.WorkArea;
                    if (accidentReport.CompileDate != null)
                    {
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentReport.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(accidentReport.UnitId))
                    {
                        this.ddlUnitId.SelectedValue = accidentReport.UnitId;
                    }
                    if (!string.IsNullOrEmpty(accidentReport.ProjectId))
                    {
                        this.ddlProjectId.SelectedValue = accidentReport.ProjectId;
                    }
                    this.txtAccidentDescription.Text = accidentReport.AccidentDescription;
                    this.txtCasualties.Text = accidentReport.Casualties;
                }
                else
                {
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ServerAccidentReportMenuId;
                this.ctlAuditFlow.DataId = this.AccidentReportId;
                this.ctlAuditFlow.ProjectId = this.CurrUser.LoginProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            Model.ProjectAccident_AccidentReport accidentReport = new Model.ProjectAccident_AccidentReport();
            if (this.ddlProjectId.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.ddlProjectId.SelectedValue))
            {
                ShowNotify("请选择项目");
                return;
            }
            if (this.ddlUnitId.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.ddlUnitId.SelectedValue))
            {
                ShowNotify("请选择单位");
                return;
            }
            accidentReport.WorkArea = this.txtWorkArea.Text.Trim();
            if (!string.IsNullOrEmpty(this.dpkCompileDate.Text.Trim()))
            {
                accidentReport.CompileDate = Convert.ToDateTime(this.dpkCompileDate.Text.Trim());
            }
            if (this.ddlProjectId.SelectedValue != BLL.Const._Null)
            {
                accidentReport.ProjectId = this.ddlProjectId.SelectedValue;
            }
            if (this.ddlUnitId.SelectedValue != BLL.Const._Null)
            {
                accidentReport.UnitId = this.ddlUnitId.SelectedValue;
            }
            accidentReport.AccidentDescription = this.txtAccidentDescription.Text.Trim();
            accidentReport.Casualties = this.txtCasualties.Text.Trim();
            //accidentReport.AttachUrl = this.FullAttachUrl;
            accidentReport.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                accidentReport.States = this.ctlAuditFlow.NextStep;
            }
            if (string.IsNullOrEmpty(this.AccidentReportId))
            {
                this.AccidentReportId = accidentReport.AccidentReportId = SQLHelper.GetNewID(typeof(Model.ProjectAccident_AccidentReport));
                BLL.AccidentReportService.AddAccidentReport(accidentReport);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerAccidentReportMenuId, BLL.Const.BtnModify);
            }
            else
            {

                accidentReport.AccidentReportId = this.AccidentReportId;
                BLL.AccidentReportService.UpdateAccidentReport(accidentReport);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerAccidentReportMenuId, BLL.Const.BtnAdd);
            }

            //保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ServerAccidentReportMenuId, this.AccidentReportId, (type == BLL.Const.BtnSubmit ? true : false), this.ddlProjectId.SelectedItem.Text, "../ProjectAccident/AccidentReportView.aspx?AccidentReportId={0}");
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerAccidentReportMenuId);
            if (buttonList.Count() > 0)
            {
             
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (this.ddlProjectId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择项目");
                return;
            }
            if (this.ddlUnitId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择单位");
                return;
            }
            
            if (string.IsNullOrEmpty(this.AccidentReportId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectAccident&menuId={1}", this.AccidentReportId, BLL.Const.ServerAccidentReportMenuId)));
        }
    }
}