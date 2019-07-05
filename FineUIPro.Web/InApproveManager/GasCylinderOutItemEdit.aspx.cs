using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GasCylinderOutItemEdit : PageBase
    {
        /// <summary>
        /// 主表
        /// </summary>
        private string GasCylinderOutItemId
        {
            get
            {
                return (string)ViewState["GasCylinderOutItemId"];
            }
            set
            {
                ViewState["GasCylinderOutItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        private string GasCylinderOutId
        {
            get
            {
                return (string)ViewState["GasCylinderOutId"];
            }
            set
            {
                ViewState["GasCylinderOutId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.drpGasCylinderId.DataValueField = "GasCylinderId";
                this.drpGasCylinderId.DataTextField = "GasCylinderName";
                this.drpGasCylinderId.DataSource = BLL.GasCylinderService.GetGasCylinderList();
                this.drpGasCylinderId.DataBind();
                Funs.FineUIPleaseSelect(this.drpGasCylinderId);

                this.GasCylinderOutId = Request.Params["GasCylinderOutId"];
                this.GasCylinderOutItemId = Request.Params["GasCylinderOutItemId"];
                if (!string.IsNullOrEmpty(this.GasCylinderOutItemId))
                {
                    Model.InApproveManager_GasCylinderOutItem gasCylinderOutItem = BLL.GasCylinderOutItemService.GetGasCylinderOutItemById(this.GasCylinderOutItemId);
                    if (gasCylinderOutItem!=null)
                    {
                        this.GasCylinderOutId = gasCylinderOutItem.GasCylinderOutId;
                        if (!string.IsNullOrEmpty(gasCylinderOutItem.GasCylinderId))
                        {
                            this.drpGasCylinderId.SelectedValue = gasCylinderOutItem.GasCylinderId;
                        }
                        if (gasCylinderOutItem.GasCylinderNum!=null)
                        {
                            this.txtGasCylinderNum.Text = Convert.ToString(gasCylinderOutItem.GasCylinderNum);
                        }
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
            Model.InApproveManager_GasCylinderOutItem gasCylinderOutItem = new Model.InApproveManager_GasCylinderOutItem
            {
                GasCylinderOutId = this.GasCylinderOutId
            };
            if (this.drpGasCylinderId.SelectedValue!=BLL.Const._Null)
            {
                gasCylinderOutItem.GasCylinderId = this.drpGasCylinderId.SelectedValue;
            }
            gasCylinderOutItem.GasCylinderNum = Funs.GetNewInt(this.txtGasCylinderNum.Text.Trim());
            if (!string.IsNullOrEmpty(this.GasCylinderOutItemId))
            {
                gasCylinderOutItem.GasCylinderOutItemId = this.GasCylinderOutItemId;
                BLL.GasCylinderOutItemService.UpdateGasCylinderOutItem(gasCylinderOutItem);
            }
            else
            {
                this.GasCylinderOutItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GasCylinderOutItem));
                gasCylinderOutItem.GasCylinderOutItemId = this.GasCylinderOutItemId;
                BLL.GasCylinderOutItemService.AddGasCylinderOutItem(gasCylinderOutItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}