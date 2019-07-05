using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ShowDialog
{
    public partial class ShowSeeFile : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string toKeyId = Request.Params["toKeyId"];
                string type = Request.Params["type"];
                if (type == "ManageRule")
                {
                    var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageRuleMenuId);
                    if (buttonList.Count() > 0)
                    {
                        if (buttonList.Contains(BLL.Const.BtnSave))
                        {
                            this.btnSave.Hidden = false;
                        }
                    }
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(toKeyId);
                    if (manageRule != null)
                    {
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(manageRule.SeeFile);
                    }
                }
                else if (type == "ActionPlanManageRule")
                {
                    var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ActionPlan_ManagerRuleMenuId);
                    if (buttonList.Count() > 0)
                    {
                        if (buttonList.Contains(BLL.Const.BtnSave))
                        {
                            this.btnSave.Hidden = false;
                        }
                    }
                    var managerRule = BLL.ActionPlan_ManagerRuleService.GetManagerRuleById(toKeyId);
                    if (managerRule != null)
                    {
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(managerRule.SeeFile);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isClose"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string toKeyId = Request.Params["toKeyId"];
            string type = Request.Params["type"];
            if (type == "ManageRule")
            {
                var manageRule = BLL.ManageRuleService.GetManageRuleById(toKeyId);
                manageRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
                BLL.ManageRuleService.UpdateManageRule(manageRule);
            }
            else if (type == "ActionPlanManageRule")
            {
                var managerRule = BLL.ActionPlan_ManagerRuleService.GetManagerRuleById(toKeyId);
                managerRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
                BLL.ActionPlan_ManagerRuleService.UpdateManageRule(managerRule);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}