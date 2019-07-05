using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class CarManagerView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CarManagerId
        {
            get
            {
                return (string)ViewState["CarManagerId"];
            }
            set
            {
                ViewState["CarManagerId"] = value;
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
                this.CarManagerId = Request.Params["CarManagerId"];
                if (!string.IsNullOrEmpty(this.CarManagerId))
                {
                    var carManager = BLL.CarManagerService.GetCarManagerById(this.CarManagerId);
                    if (carManager != null)
                    {
                        this.txtCarManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CarManagerId);
                        this.txtCarName.Text = carManager.CarName;
                        this.txtCarModel.Text = carManager.CarModel;
                        this.txtBuyDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.BuyDate);
                        this.txtLastYearCheckDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.LastYearCheckDate);
                        this.txtInsuranceDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.InsuranceDate);
                        this.txtRemark.Text = carManager.Remark;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.CarManagerMenuId;
                this.ctlAuditFlow.DataId = this.CarManagerId;
            }
        }
        #endregion

        #region 年检、保险有效期
        /// <summary>
        /// 年检、保险有效期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
           var carManager = BLL.CarManagerService.GetCarManagerById(this.CarManagerId);
           if (carManager != null)
           {
               var buttonList = BLL.CommonService.GetAllButtonList(carManager.ProjectId, this.CurrUser.UserId, BLL.Const.CarManagerMenuId);
               if (buttonList.Count() > 0 && buttonList.Contains(BLL.Const.BtnModify))
               {
                   carManager.LastYearCheckDate = BLL.Funs.GetNewDateTime(this.txtLastYearCheckDate.Text);
                   carManager.InsuranceDate = BLL.Funs.GetNewDateTime(this.txtInsuranceDate.Text);
                   BLL.CarManagerService.UpdateCarManager(carManager);
                   PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
               }
           }
        }
        #endregion
    }
}