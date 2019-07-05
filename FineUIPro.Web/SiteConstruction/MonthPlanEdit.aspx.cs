using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SiteConstruction
{
    public partial class MonthPlanEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthPlanId
        {
            get
            {
                return (string)ViewState["MonthPlanId"];
            }
            set
            {
                ViewState["MonthPlanId"] = value;
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
                ///加载单位
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnit.Enabled = false;
                }

                this.MonthPlanId = Request.Params["MonthPlanId"];
                string unitId = this.CurrUser.UnitId;
                if (!string.IsNullOrEmpty(this.MonthPlanId))
                {
                    Model.SiteConstruction_MonthPlan MonthPlan = BLL.MonthPlanService.GetMonthPlanById(this.MonthPlanId);
                    if (MonthPlan != null)
                    {
                        this.ProjectId = MonthPlan.ProjectId;
                        unitId = MonthPlan.UnitId;
                        this.txtJobContent.Text = MonthPlan.JobContent;
                        this.txtMonths.Text = string.Format("{0:yyyy-MM}", MonthPlan.Months);
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", MonthPlan.CompileDate);
                        if (!string.IsNullOrEmpty(MonthPlan.UnitId))
                        {
                            this.drpUnit.SelectedValue = MonthPlan.UnitId;
                        }
                        //this.txtSeeFile.Text = HttpUtility.HtmlDecode(MonthPlan.SeeFile);                                               
                    }
                }
                else
                {
                    this.txtMonths.Text = string.Format("{0:yyyy-MM}", DateTime.Now);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtJobContent.Text = string.Format("{0:yyyy-MM}", DateTime.Now) + "工作计划：";
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectMonthPlanMenuId;
                this.ctlAuditFlow.DataId = this.MonthPlanId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = unitId;
            }
        }
        #endregion

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
            Model.SiteConstruction_MonthPlan newMonthPlan = new Model.SiteConstruction_MonthPlan
            {
                ProjectId = this.ProjectId
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                newMonthPlan.UnitId = this.drpUnit.SelectedValue;
            }
            newMonthPlan.Months = Funs.GetNewDateTimeOrNow(this.txtMonths.Text.Trim());
          
            newMonthPlan.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            newMonthPlan.JobContent = this.txtJobContent.Text.Trim();
            //newMonthPlan.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            ////单据状态
            newMonthPlan.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                newMonthPlan.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.MonthPlanId))
            {
                newMonthPlan.MonthPlanId = this.MonthPlanId;
                BLL.MonthPlanService.UpdateMonthPlan(newMonthPlan);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改现场动态", newMonthPlan.MonthPlanId);
            }
            else
            {
                this.MonthPlanId = SQLHelper.GetNewID(typeof(Model.SiteConstruction_MonthPlan));
                newMonthPlan.CompileMan = this.CurrUser.UserId;
                newMonthPlan.MonthPlanId = this.MonthPlanId;
                BLL.MonthPlanService.AddMonthPlan(newMonthPlan);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加现场动态", newMonthPlan.MonthPlanId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectMonthPlanMenuId, this.MonthPlanId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCompileDate.Text.Trim(), "../SiteConstruction/MonthPlanView.aspx?MonthPlanId={0}");
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
            if (string.IsNullOrEmpty(this.MonthPlanId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthPlanAttachUrl&menuId={1}", MonthPlanId, BLL.Const.ProjectMonthPlanMenuId)));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.txtCompileDate.Text))
            {
                var co = Funs.DB.SiteConstruction_MonthPlan.FirstOrDefault(x => x.UnitId == this.drpUnit.SelectedValue && (x.CompileDate.Value.AddDays(1) > Funs.GetNewDateTime(this.txtCompileDate.Text) && x.CompileDate.Value.AddDays(-1) < Funs.GetNewDateTime(this.txtCompileDate.Text)));
                if (co != null)
                {
                    ShowNotify("该单位本月月度计划已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
    }
}