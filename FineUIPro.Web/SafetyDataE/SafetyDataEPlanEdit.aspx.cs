using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataEPlanEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级企业安全管理资料项
        /// </summary>
        public string SafetyDataEPlanId
        {
            get
            {
                return (string)ViewState["SafetyDataEPlanId"];
            }
            set
            {
                ViewState["SafetyDataEPlanId"] = value;
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
                this.SafetyDataEPlanId = Request.Params["SafetyDataEPlanId"];
                if (!string.IsNullOrEmpty(this.SafetyDataEPlanId))
                {
                    var SafetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(this.SafetyDataEPlanId);
                    if (SafetyDataEPlan != null)
                    {
                        var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(SafetyDataEPlan.SafetyDataEId);
                        if (SafetyDataE != null)
                        {
                            this.txtTitle.Text = SafetyDataE.Title;
                            this.txtCode.Text = SafetyDataE.Code;
                        }
                       
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataEPlan.CheckDate);
                        this.txtReminderDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataEPlan.ReminderDate);
                        this.txtShouldScore.Text = SafetyDataEPlan.ShouldScore.ToString();
                        //this.txtRealScore.Text = SafetyDataEPlan.RealScore.ToString();
                        this.txtRemark.Text = SafetyDataEPlan.Remark;
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
            var newSafetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(this.SafetyDataEPlanId);
            if (newSafetyDataEPlan != null)
            {                
                newSafetyDataEPlan.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
                newSafetyDataEPlan.ReminderDate = Funs.GetNewDateTime(this.txtReminderDate.Text.Trim());
                newSafetyDataEPlan.ShouldScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                if (newSafetyDataEPlan.RealScore.HasValue)
                {
                    newSafetyDataEPlan.RealScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                }
                newSafetyDataEPlan.Remark = this.txtRemark.Text.Trim();
                BLL.SafetyDataEPlanService.UpdateSafetyDataEPlan(newSafetyDataEPlan);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtCheckDate.Text, newSafetyDataEPlan.SafetyDataEPlanId, BLL.Const.ServerSafetyDataEPlanMenuId, BLL.Const.BtnModify);
            }
                        
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}