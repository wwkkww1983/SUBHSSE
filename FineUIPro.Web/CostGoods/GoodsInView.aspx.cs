using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsInView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsInId
        {
            get
            {
                return (string)ViewState["GoodsInId"];
            }
            set
            {
                ViewState["GoodsInId"] = value;
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
                this.GoodsInId = Request.Params["GoodsInId"];
                if (!string.IsNullOrEmpty(this.GoodsInId))
                {
                    Model.CostGoods_GoodsIn goodsIn = BLL.GoodsIn2Service.GetGoodsInById(this.GoodsInId);
                    if (goodsIn != null)
                    {
                        this.txtGoodsInCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.GoodsInId);
                        if (!string.IsNullOrEmpty(goodsIn.GoodsDefId))
                        {
                            var goodsDef = BLL.GoodsDefService.GetGoodsDefById(goodsIn.GoodsDefId);
                            if (goodsDef != null)
                            {
                                this.txtGoodsDefId.Text = goodsDef.GoodsDefName;
                            }
                        }
                        if (goodsIn.GoodsNum != null)
                        {
                            this.txtCounts.Text = Convert.ToString(goodsIn.GoodsNum);
                        }
                        this.txtInPerson.Text = goodsIn.InPerson;
                        if (goodsIn.InDate != null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", goodsIn.InDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsIn2MenuId;
                this.ctlAuditFlow.DataId = this.GoodsInId;
            }
        }
        #endregion
    }
}