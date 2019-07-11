using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataPlanEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级企业安全管理资料项
        /// </summary>
        public string SafetyDataPlanId
        {
            get
            {
                return (string)ViewState["SafetyDataPlanId"];
            }
            set
            {
                ViewState["SafetyDataPlanId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 企业安全管理资料编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafetyDataPlanId = Request.Params["SafetyDataPlanId"];
                if (!string.IsNullOrEmpty(this.SafetyDataPlanId))
                {
                    var SafetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(this.SafetyDataPlanId);
                    if (SafetyDataPlan != null)
                    {
                        var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(SafetyDataPlan.SafetyDataId);
                        if (safetyData != null)
                        {
                            this.txtTitle.Text = safetyData.Title;
                            this.txtCode.Text = safetyData.Code;
                        }
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataPlan.RealStartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataPlan.RealEndDate);
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataPlan.CheckDate);
                        this.txtReminderDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataPlan.ReminderDate);
                        this.txtShouldScore.Text = SafetyDataPlan.ShouldScore.ToString();
                        //this.txtRealScore.Text = SafetyDataPlan.RealScore.ToString();
                        this.txtRemark.Text = SafetyDataPlan.Remark;
                    }                                         
                }                
            }
        }
                
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var newSafetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(this.SafetyDataPlanId);
            if (newSafetyDataPlan != null)
            {
                newSafetyDataPlan.RealStartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
                newSafetyDataPlan.RealEndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
                newSafetyDataPlan.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
                newSafetyDataPlan.ReminderDate = Funs.GetNewDateTime(this.txtReminderDate.Text.Trim());
                newSafetyDataPlan.ShouldScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                if (newSafetyDataPlan.RealScore.HasValue)
                {
                    newSafetyDataPlan.RealScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                }
                newSafetyDataPlan.Remark = this.txtRemark.Text.Trim();
                BLL.SafetyDataPlanService.UpdateSafetyDataPlan(newSafetyDataPlan);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtCheckDate.Text, newSafetyDataPlan.SafetyDataPlanId, BLL.Const.ServerSafetyDataPlanMenuId, BLL.Const.BtnModify);
            }
                        
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}