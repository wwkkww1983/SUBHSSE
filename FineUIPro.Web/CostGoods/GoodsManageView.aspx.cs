using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsManageView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsManageId
        {
            get
            {
                return (string)ViewState["GoodsManageId"];
            }
            set
            {
                ViewState["GoodsManageId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.GoodsManageId = Request.Params["GoodsManageId"];
                if (!string.IsNullOrEmpty(this.GoodsManageId))
                {
                    Model.CostGoods_GoodsManage goodsManage = BLL.GoodsManageService.GetGoodsManageById(this.GoodsManageId);
                    if (goodsManage != null)
                    {
                        this.txtGoodsCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsManageId);
                        this.txtGoodsName.Text = goodsManage.GoodsName;
                        if (!string.IsNullOrEmpty(goodsManage.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(goodsManage.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        var goodsCategory = BLL.GoodsCategoryService.GetGoodsCategoryById(goodsManage.GoodsCategoryId);
                        if (goodsCategory != null)
                        {
                            this.txtGoodsCategory.Text = goodsCategory.GoodsCategoryName;
                        }

                        this.txtSizeModel.Text = goodsManage.SizeModel;
                        this.txtFactoryCode.Text = goodsManage.FactoryCode;
                        if (goodsManage.CheckDate.HasValue)
                        {
                            this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", goodsManage.CheckDate);
                        }
                        this.txtEnableYear.Text = Convert.ToString(goodsManage.EnableYear);
                        if (!string.IsNullOrEmpty(goodsManage.CheckPerson))
                        {
                            var user = BLL.UserService.GetUserByUserId(goodsManage.CheckPerson);
                            if (user!=null)
                            {
                                this.txtCheckPersonName.Text = user.UserName;
                            }
                        }
                        if (goodsManage.InTime != null)
                        {
                            this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", goodsManage.InTime);
                        }
                        this.txtRemark.Text = goodsManage.Remark;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsManageMenuId;
                this.ctlAuditFlow.DataId = this.GoodsManageId;
            }
        }
        #endregion
    }
}