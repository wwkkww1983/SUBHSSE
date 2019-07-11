using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentStatisticsEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentStatisticsId
        {
            get
            {
                return (string)ViewState["AccidentStatisticsId"];
            }
            set
            {
                ViewState["AccidentStatisticsId"] = value;
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

                this.AccidentStatisticsId = Request.Params["AccidentStatisticsId"];
                var accidentStatistics = BLL.AccidentStatisticsService.GetAccidentStatisticsById(this.AccidentStatisticsId);
                if (accidentStatistics != null)
                {
                    this.txtPerson.Text = accidentStatistics.Person;
                    if (accidentStatistics.CompileDate != null)
                    {
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentStatistics.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(accidentStatistics.UnitId))
                    {
                        this.ddlUnitId.SelectedValue = accidentStatistics.UnitId;
                    }
                    if (!string.IsNullOrEmpty(accidentStatistics.ProjectId))
                    {
                        this.ddlProjectId.SelectedValue = accidentStatistics.ProjectId;
                    }
                    this.txtTreatment.Text = accidentStatistics.Treatment;
                }
                else
                {
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ServerAccidentStatisticsMenuId;
                this.ctlAuditFlow.DataId = this.AccidentStatisticsId;
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
            SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 数据保存方法
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.ProjectAccident_AccidentStatistics accidentStatistics = new Model.ProjectAccident_AccidentStatistics();
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
            accidentStatistics.Treatment = this.txtTreatment.Text.Trim();
            accidentStatistics.CompileDate = Funs.GetNewDateTime(this.dpkCompileDate.Text.Trim());
            if (this.ddlProjectId.SelectedValue != BLL.Const._Null)
            {
                accidentStatistics.ProjectId = this.ddlProjectId.SelectedValue;
            }
            if (this.ddlUnitId.SelectedValue != BLL.Const._Null)
            {
                accidentStatistics.UnitId = this.ddlUnitId.SelectedValue;
            }
            accidentStatistics.Person = this.txtPerson.Text.Trim();

            accidentStatistics.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                accidentStatistics.States = this.ctlAuditFlow.NextStep;
            }

            if (string.IsNullOrEmpty(this.AccidentStatisticsId))
            {
                this.AccidentStatisticsId = accidentStatistics.AccidentStatisticsId = SQLHelper.GetNewID(typeof(Model.ProjectAccident_AccidentStatistics));
                BLL.AccidentStatisticsService.AddAccidentStatistics(accidentStatistics);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerAccidentStatisticsMenuId, BLL.Const.BtnModify);
            }
            else
            {
                accidentStatistics.AccidentStatisticsId = this.AccidentStatisticsId;
                BLL.AccidentStatisticsService.UpdateAccidentStatistics(accidentStatistics);
                BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ServerAccidentStatisticsMenuId, BLL.Const.BtnAdd);
            }

            //保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.CurrUser.LoginProjectId, BLL.Const.ServerAccidentStatisticsMenuId, this.AccidentStatisticsId, (type == BLL.Const.BtnSubmit ? true : false), this.ddlProjectId.SelectedItem.Text, "../ProjectAccident/AccidentStatisticsView.aspx?AccidentStatisticsId={0}");
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerAccidentStatisticsMenuId);
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

            if (string.IsNullOrEmpty(this.AccidentStatisticsId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectAccident&menuId={1}", this.AccidentStatisticsId, BLL.Const.ServerAccidentStatisticsMenuId)));
        }
    }
}