using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataCheckEditEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级企业安全管理资料项
        /// </summary>
        public string SafetyDataCheckItemId
        {
            get
            {
                return (string)ViewState["SafetyDataCheckItemId"];
            }
            set
            {
                ViewState["SafetyDataCheckItemId"] = value;
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
                this.SafetyDataCheckItemId = Request.Params["SafetyDataCheckItemId"];
                if (!string.IsNullOrEmpty(this.SafetyDataCheckItemId))
                {
                    var SafetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(this.SafetyDataCheckItemId);
                    if (SafetyDataCheckItem != null)
                    {
                        var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(SafetyDataCheckItem.SafetyDataId);
                        if (safetyData != null)
                        {
                            this.txtTitle.Text = safetyData.Title;
                            this.txtCode.Text = safetyData.Code;
                        }
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheckItem.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheckItem.EndDate);
                        this.txtReminderDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheckItem.ReminderDate);
                        this.txtShouldScore.Text = SafetyDataCheckItem.ShouldScore.ToString();
                        //this.txtRealScore.Text = SafetyDataCheckItem.RealScore.ToString();
                        this.txtRemark.Text = SafetyDataCheckItem.Remark;
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
            var newSafetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(this.SafetyDataCheckItemId);
            if (newSafetyDataCheckItem != null)
            {
                newSafetyDataCheckItem.StartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
                newSafetyDataCheckItem.EndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
                newSafetyDataCheckItem.ReminderDate = Funs.GetNewDateTime(this.txtReminderDate.Text.Trim());
                newSafetyDataCheckItem.ShouldScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                if (newSafetyDataCheckItem.RealScore.HasValue)
                {
                    newSafetyDataCheckItem.RealScore = Funs.GetNewDecimal(this.txtShouldScore.Text.Trim());
                }
                newSafetyDataCheckItem.Remark = this.txtRemark.Text.Trim();
                BLL.SafetyDataCheckItemService.UpdateSafetyDataCheckItem(newSafetyDataCheckItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改企业安全管理资料考核信息");
            }
                        
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}