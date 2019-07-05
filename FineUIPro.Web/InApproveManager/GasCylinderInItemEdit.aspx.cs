using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GasCylinderInItemEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GasCylinderInItemId
        {
            get
            {
                return (string)ViewState["GasCylinderInItemId"];
            }
            set
            {
                ViewState["GasCylinderInItemId"] = value;
            }
        }

        /// <summary>
        /// 主表主键
        /// </summary>
        private string GasCylinderInId
        {
            get
            {
                return (string)ViewState["GasCylinderInId"];
            }
            set
            {
                ViewState["GasCylinderInId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.drpGasCylinderId.DataValueField = "GasCylinderId";
                this.drpGasCylinderId.DataTextField = "GasCylinderName";
                this.drpGasCylinderId.DataSource = BLL.GasCylinderService.GetGasCylinderList();
                this.drpGasCylinderId.DataBind();
                Funs.FineUIPleaseSelect(this.drpGasCylinderId);

                this.GasCylinderInId = Request.Params["GasCylinderInId"];
                this.GasCylinderInItemId = Request.Params["GasCylinderInItemId"];
                if (!string.IsNullOrEmpty(this.GasCylinderInItemId))
                {
                    Model.InApproveManager_GasCylinderInItem gasCylinderInItem = BLL.GasCylinderInItemService.GetGasCylinderInItemById(this.GasCylinderInItemId);
                    if (gasCylinderInItem!=null)
                    {
                        this.GasCylinderInId = gasCylinderInItem.GasCylinderInId;
                        if (!string.IsNullOrEmpty(gasCylinderInItem.GasCylinderId))
                        {
                            this.drpGasCylinderId.SelectedValue = gasCylinderInItem.GasCylinderId;
                        }
                        if (gasCylinderInItem.GasCylinderNum!=null)
                        {
                            this.txtGasCylinderNum.Text = Convert.ToString(gasCylinderInItem.GasCylinderNum);
                        }
                        if (gasCylinderInItem.PM_IsFull != null)
                        {
                            this.drpPM_IsFull.SelectedValue = Convert.ToString(gasCylinderInItem.PM_IsFull);
                        }
                        if (gasCylinderInItem.FZQ_IsFull != null)
                        {
                            this.drpFZQ_IsFull.SelectedValue = Convert.ToString(gasCylinderInItem.FZQ_IsFull);
                        }
                        if (gasCylinderInItem.IsSameCar != null)
                        {
                            this.drpIsSameCar.SelectedValue = Convert.ToString(gasCylinderInItem.IsSameCar);
                        }
                    }
                }
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
            Model.InApproveManager_GasCylinderInItem gasCylinderInItem = new Model.InApproveManager_GasCylinderInItem
            {
                GasCylinderInId = this.GasCylinderInId
            };
            if (this.drpGasCylinderId.SelectedValue!=BLL.Const._Null)
            {
                gasCylinderInItem.GasCylinderId = this.drpGasCylinderId.SelectedValue;
            }
            else
            {
                Alert.ShowInTop("请选择气瓶类型！", MessageBoxIcon.Warning);
                return;
            }
            gasCylinderInItem.GasCylinderNum = Funs.GetNewInt(this.txtGasCylinderNum.Text.Trim());
            if (this.drpPM_IsFull.SelectedValue!="0")
            {
                gasCylinderInItem.PM_IsFull = Convert.ToBoolean(this.drpPM_IsFull.SelectedValue);
            }
            if (this.drpFZQ_IsFull.SelectedValue!="0")
            {
                gasCylinderInItem.FZQ_IsFull = Convert.ToBoolean(this.drpFZQ_IsFull.SelectedValue);
            }
            if (this.drpIsSameCar.SelectedValue!="0")
            {
                gasCylinderInItem.IsSameCar = Convert.ToBoolean(this.drpIsSameCar.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.GasCylinderInItemId))
            {
                gasCylinderInItem.GasCylinderInItemId = this.GasCylinderInItemId;
                BLL.GasCylinderInItemService.UpdateGasCylinderInItem(gasCylinderInItem);
            }
            else
            {
                this.GasCylinderInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GasCylinderInItem));
                gasCylinderInItem.GasCylinderInItemId = this.GasCylinderInItemId;
                BLL.GasCylinderInItemService.AddGasCylinderInItem(gasCylinderInItem);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}