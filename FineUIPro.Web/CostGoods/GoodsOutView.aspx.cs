using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsOutView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsOutId
        {
            get
            {
                return (string)ViewState["GoodsOutId"];
            }
            set
            {
                ViewState["GoodsOutId"] = value;
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
                this.GoodsOutId = Request.Params["GoodsOutId"];
                if (!string.IsNullOrEmpty(this.GoodsOutId))
                {
                    Model.CostGoods_GoodsOut goodsOut = BLL.GoodsOut2Service.GetGoodsOutById(this.GoodsOutId);
                    if (goodsOut != null)
                    {
                        this.txtGoodsOutCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.GoodsOutId);
                        if (!string.IsNullOrEmpty(goodsOut.GoodsDefId))
                        {
                            var goodsDef = BLL.GoodsDefService.GetGoodsDefById(goodsOut.GoodsDefId);
                            if (goodsDef != null)
                            {
                                this.txtGoodsDefId.Text = goodsDef.GoodsDefName;
                            }
                        }
                        if (goodsOut.GoodsNum != null)
                        {
                            this.txtCounts.Text = Convert.ToString(goodsOut.GoodsNum);
                        }
                        this.txtOutPerson.Text = goodsOut.OutPerson;
                        if (goodsOut.OutDate != null)
                        {
                            this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", goodsOut.OutDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsOut2MenuId;
                this.ctlAuditFlow.DataId = this.GoodsOutId;
            }
        }
        #endregion
    }
}