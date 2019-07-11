using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class PunishItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 问题巡检类型主键
        /// </summary>
        public string PunishItemId
        {
            get
            {
                return (string)ViewState["PunishItemId"];
            }
            set
            {
                ViewState["PunishItemId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.PunishItemId = Request.QueryString["PunishItemId"];
                if (!string.IsNullOrEmpty(this.PunishItemId))
                {
                    Model.HSSE_Hazard_PunishItem title = BLL.HSSE_Hazard_PunishItemService.GetTitleByPunishItemId(this.PunishItemId);
                    if (title != null)
                    {
                        this.txtPunishItemCode.Text = title.PunishItemCode;
                        if (title.PunishItemType == "1")
                        {
                            this.rblPunishItemType.SelectedValue = "1";
                        }
                        else
                        {
                            this.rblPunishItemType.SelectedValue = "2";
                        }
                        this.txtPunishItemContent.Text = title.PunishItemContent;
                        if (title.Deduction != null)
                        {
                            this.drpDeduction.SelectedValue = title.Deduction.ToString();
                        }
                        if (title.PunishMoney != null)
                        {
                            this.txtPunishMoney.Text = title.PunishMoney.ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.PunishItemMenuId, Const.BtnSave))
            {
                Model.HSSE_Hazard_PunishItem title = new Model.HSSE_Hazard_PunishItem();
                title.PunishItemCode = this.txtPunishItemCode.Text.Trim();
                title.PunishItemType = this.rblPunishItemType.SelectedValue;
                title.PunishItemContent = this.txtPunishItemContent.Text.Trim();
                title.Deduction = Funs.GetNewInt(this.drpDeduction.SelectedValue);
                title.PunishMoney = Funs.GetNewInt(this.txtPunishMoney.Text.Trim());
                if (string.IsNullOrEmpty(this.PunishItemId))
                {
                    this.PunishItemId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_PunishItem));
                    title.PunishItemId = this.PunishItemId;
                    BLL.HSSE_Hazard_PunishItemService.AddPunishItem(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.PunishItemCode, title.PunishItemId, BLL.Const.PunishItemMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    title.PunishItemId = this.PunishItemId;
                    BLL.HSSE_Hazard_PunishItemService.UpdatePunishItem(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.PunishItemCode, title.PunishItemId, BLL.Const.PunishItemMenuId, BLL.Const.BtnModify);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
        #endregion
    }
}