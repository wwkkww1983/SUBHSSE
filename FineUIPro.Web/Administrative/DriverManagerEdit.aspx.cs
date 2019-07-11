using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Administrative
{
    public partial class DriverManagerEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string DriverManagerId
        {
            get
            {
                return (string)ViewState["DriverManagerId"];
            }
            set
            {
                ViewState["DriverManagerId"] = value;
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
                this.DriverManagerId = Request.Params["DriverManagerId"];
                if (!string.IsNullOrEmpty(this.DriverManagerId))
                {
                    Model.Administrative_DriverManager DriverManager = BLL.DriverManagerService.GetDriverManagerById(this.DriverManagerId);
                    if (DriverManager != null)
                    {
                        this.ProjectId = DriverManager.ProjectId;
                        this.txtDriverManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.DriverManagerId);
                        this.txtDriverName.Text = DriverManager.DriverName;
                        this.txtDriverCode.Text = DriverManager.DriverCode;
                        this.txtDrivingDate.Text = string.Format("{0:yyyy-MM-dd}", DriverManager.DrivingDate);
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DriverManager.CheckDate);
                        this.txtDriverCarModel.Text = DriverManager.DriverCarModel;
                        this.txtRemark.Text = DriverManager.Remark;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtDriverManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.DriverManagerMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtDrivingDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddYears(1));
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.DriverManagerMenuId;
                this.ctlAuditFlow.DataId = this.DriverManagerId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 保存、提交
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
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Administrative_DriverManager DriverManager = new Model.Administrative_DriverManager
            {
                ProjectId = this.ProjectId,
                DriverManagerCode = this.txtDriverManagerCode.Text.Trim(),
                DriverName = this.txtDriverName.Text.Trim(),
                DriverCode = this.txtDriverCode.Text.Trim(),
                DrivingDate = Funs.GetNewDateTime(this.txtDrivingDate.Text.Trim()),
                CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                DriverCarModel = this.txtDriverCarModel.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
                States = BLL.Const.State_0
            };
            if (type==BLL.Const.BtnSubmit)
            {
                DriverManager.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.DriverManagerId))
            {
                DriverManager.DriverManagerId = this.DriverManagerId;
                BLL.DriverManagerService.UpdateDriverManager(DriverManager);
                BLL.LogService.AddSys_Log(this.CurrUser, DriverManager.DriverManagerCode, DriverManager.DriverManagerId,BLL.Const.DriverManagerMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                this.DriverManagerId = SQLHelper.GetNewID(typeof(Model.Administrative_DriverManager));
                DriverManager.DriverManagerId = this.DriverManagerId;
                BLL.DriverManagerService.AddDriverManager(DriverManager);
                BLL.LogService.AddSys_Log(this.CurrUser, DriverManager.DriverManagerCode, DriverManager.DriverManagerId, BLL.Const.DriverManagerMenuId, BLL.Const.BtnModify);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.DriverManagerMenuId, this.DriverManagerId, (type == BLL.Const.BtnSubmit ? true : false), DriverManager.DriverName, "../Administrative/DriverManagerView.aspx?DriverManagerId={0}");
        }
        #endregion
    }
}