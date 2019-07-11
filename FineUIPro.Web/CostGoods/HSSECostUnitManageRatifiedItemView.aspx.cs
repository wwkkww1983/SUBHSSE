using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageRatifiedItemView : PageBase
    {
        #region 定义项    
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }
        /// <summary>
        /// 施工单位安全费用主键
        /// </summary>
        private string HSSECostUnitManageId
        {
            get
            {
                return (string)ViewState["HSSECostUnitManageId"];
            }
            set
            {
                ViewState["HSSECostUnitManageId"] = value;
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
                this.HSSECostUnitManageId = Request.Params["HSSECostUnitManageId"];
                this.Type = Request.Params["Type"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                BindGrid();
            }
        }
        #endregion

        #region 列表绑定
        /// <summary>
        /// 列表绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT *,(CASE WHEN IsAgree =1 THEN '同意' WHEN IsAgree =0 THEN '不同意' ELSE '' END) AS IsAgreeName"
                        + @" ,(CASE WHEN IsRatified =1 THEN '同意' WHEN IsRatified =0 THEN '不同意' ELSE '' END) AS IsRatifiedName"
                        + @" FROM dbo.CostGoods_HSSECostUnitManageItem"
                        + @" WHERE HSSECostUnitManageId=@HSSECostUnitManageId AND Type =@Type";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@HSSECostUnitManageId", this.HSSECostUnitManageId),
                new SqlParameter("@Type", this.Type),
            };

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 同意
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIsAgree_Click(object sender, EventArgs e)
        {
            this.InitText();
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var item = BLL.HSSECostUnitManageItemService.GetHSSECostUnitManageItemByHSSECostUnitManageItemId(Grid1.SelectedRowID);
            if (item != null && item.IsRatified != false)
            {
                item.RatifiedQuantity = item.AuditQuantity;
                item.RatifiedPrice = item.AuditPrice;
                item.RatifiedTotalPrice = item.AuditTotalPrice;
                item.IsRatified = true;
                BLL.HSSECostUnitManageItemService.UpdateHSSECostUnitManageItem(item);
                this.InitText();
                this.UpdateHSSECostUnitManage();
                this.BindGrid();
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.HSSECostUnitManageId) + "parent.__doPostBack('','RatifiedItemWindowClose');");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoAgree_Click(object sender, EventArgs e)
        {            
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var item = BLL.HSSECostUnitManageItemService.GetHSSECostUnitManageItemByHSSECostUnitManageItemId(Grid1.SelectedRowID);
            if (item != null)
            {
                this.tr1.Hidden = false;
                this.tr2.Hidden = false;
                this.tr3.Hidden = false;
                this.btnSure.Hidden = false;

                this.hdHSSECostUnitManageItemId.Text = item.HSSECostUnitManageItemId;
                this.txtCostContent.Text = item.CostContent;
                if (item.AuditQuantity.HasValue)
                {
                    this.txtRatifiedQuantity.Text = item.RatifiedQuantity.ToString();
                }
                else
                {
                    this.txtRatifiedQuantity.Text = item.AuditQuantity.ToString();
                }
                if (item.RatifiedPrice.HasValue)
                {
                    this.txtRatifiedPrice.Text = item.RatifiedPrice.ToString();
                }
                else
                {
                    this.txtRatifiedPrice.Text = item.AuditPrice.ToString();
                }
                if (item.RatifiedTotalPrice.HasValue)
                {
                    this.txtRatifiedTotalPrice.Text = item.RatifiedTotalPrice.ToString();
                }
                else
                {
                    this.txtRatifiedTotalPrice.Text = item.AuditTotalPrice.ToString();
                }
                this.txtRatifiedExplain.Text = item.RatifiedExplain;
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            var newItem = BLL.HSSECostUnitManageItemService.GetHSSECostUnitManageItemByHSSECostUnitManageItemId(this.hdHSSECostUnitManageItemId.Text);
            if(newItem != null)
            {
               
                newItem.RatifiedExplain = this.txtRatifiedExplain.Text.Trim();
                newItem.IsRatified = false;

                newItem.RatifiedQuantity = Funs.GetNewDecimal(this.txtRatifiedQuantity.Text);
                newItem.RatifiedPrice = Funs.GetNewDecimal(this.txtRatifiedPrice.Text);
                newItem.RatifiedTotalPrice = Funs.GetNewDecimal(this.txtRatifiedTotalPrice.Text);
                BLL.HSSECostUnitManageItemService.UpdateHSSECostUnitManageItem(newItem);
            };

            this.InitText();
            this.UpdateHSSECostUnitManage();
            this.BindGrid();
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.HSSECostUnitManageId) + "parent.__doPostBack('','RatifiedItemWindowClose');");
        }

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdHSSECostUnitManageItemId.Text = string.Empty;
            this.txtCostContent.Text = string.Empty;
            this.txtRatifiedQuantity.Text = string.Empty;
            this.txtRatifiedPrice.Text = string.Empty;
            this.txtRatifiedTotalPrice.Text = string.Empty;
            this.tr1.Hidden = true;
            this.tr1.Hidden = true;
            this.tr1.Hidden = true;
            this.btnSure.Hidden = true;
        }
        #endregion

        /// <summary>
        /// 单价、工程量联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            this.txtRatifiedTotalPrice.Text = (Funs.GetNewDecimalOrZero(this.txtRatifiedQuantity.Text) * Funs.GetNewDecimalOrZero(this.txtRatifiedPrice.Text)).ToString("#0.0000");
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateHSSECostUnitManage()
        {
            var unitItem = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(this.HSSECostUnitManageId);
            if (unitItem != null)
            {
                var items = from x in Funs.DB.CostGoods_HSSECostUnitManageItem
                            where x.HSSECostUnitManageId == this.HSSECostUnitManageId && x.Type == this.Type
                            select x;
                decimal totalValue = items.Sum(x => x.RatifiedTotalPrice) ?? 0;
               
                switch (this.Type)
                {
                    case "A1":
                        unitItem.RatifiedCostA1 = totalValue;
                        break;
                    case "A2":
                        unitItem.RatifiedCostA2 = totalValue;
                        break;
                    case "A3":
                        unitItem.RatifiedCostA3 = totalValue;
                        break;
                    case "A4":
                        unitItem.RatifiedCostA4 = totalValue;
                        break;
                    case "A5":
                        unitItem.RatifiedCostA5 = totalValue;
                        break;
                    case "A6":
                        unitItem.RatifiedCostA6 = totalValue;
                        break;
                    case "A7":
                        unitItem.RatifiedCostA7 = totalValue;
                        break;
                    case "A8":
                        unitItem.RatifiedCostA8 = totalValue;
                        break;                   
                }

                BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(unitItem);
            }
        }
    }
}