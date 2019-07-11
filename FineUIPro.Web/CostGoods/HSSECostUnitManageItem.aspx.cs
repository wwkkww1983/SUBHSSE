using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageItem : PageBase
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
                ////权限按钮方法
                //this.GetButtonPower(); 
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
            string strSql = @"SELECT *  FROM dbo.CostGoods_HSSECostUnitManageItem"                          
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

        #region 修改
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var item = BLL.HSSECostUnitManageItemService.GetHSSECostUnitManageItemByHSSECostUnitManageItemId(Grid1.SelectedRowID);
            if (item != null)
            {
                this.hdHSSECostUnitManageItemId.Text = item.HSSECostUnitManageItemId;
                this.txtSortIndex.Text = item.SortIndex.ToString();
                this.txtReportTime.Text = string.Format("{0:yyyy-MM-dd}", item.ReportTime);
                this.txtCostContent.Text = item.CostContent;
                this.txtQuantity.Text = item.Quantity.ToString();
                this.txtMetric.Text = item.Metric;
                this.txtPrice.Text = item.Price.ToString();
                this.txtTotalPrice.Text = item.TotalPrice.ToString();
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getD = BLL.HSSECostUnitManageItemService.GetHSSECostUnitManageItemByHSSECostUnitManageItemId(rowID);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, null, getD.HSSECostUnitManageItemId, BLL.Const.ProjectHSSECostUnitManageMenuId, BLL.Const.BtnDelete);
                        BLL.HSSECostUnitManageItemService.DeleteHSSECostUnitManageItemByHSSECostManageId(rowID);
                    }
                }

                this.UpdateHSSECostUnitManage();
                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            Model.CostGoods_HSSECostUnitManageItem newItem = new Model.CostGoods_HSSECostUnitManageItem
            {
                HSSECostUnitManageId=this.HSSECostUnitManageId,
                Type = this.Type,
                SortIndex = Funs.GetNewInt(this.txtSortIndex.Text.Trim()),
                ReportTime = Funs.GetNewDateTime(this.txtReportTime.Text),
                CostContent = this.txtCostContent.Text.Trim(),
                Quantity = Funs.GetNewDecimal(this.txtQuantity.Text),
                Metric = this.txtMetric.Text.Trim(),
                Price = Funs.GetNewDecimal(this.txtPrice.Text),
                TotalPrice = Funs.GetNewDecimal(this.txtTotalPrice.Text),

                AuditQuantity = Funs.GetNewDecimal(this.txtQuantity.Text),
                AuditPrice = Funs.GetNewDecimal(this.txtPrice.Text),
                AuditTotalPrice = Funs.GetNewDecimal(this.txtTotalPrice.Text),
            };

            if (!string.IsNullOrEmpty(this.hdHSSECostUnitManageItemId.Text))
            {
                newItem.HSSECostUnitManageItemId = this.hdHSSECostUnitManageItemId.Text;
                BLL.HSSECostUnitManageItemService.UpdateHSSECostUnitManageItem(newItem);               
            }
            else
            {
                newItem.HSSECostUnitManageItemId = SQLHelper.GetNewID(typeof(Model.CostGoods_HSSECostUnitManageItem));
                BLL.HSSECostUnitManageItemService.AddHSSECostUnitManageItem(newItem);
            }

            this.InitText();
            this.UpdateHSSECostUnitManage();
            this.BindGrid();
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.HSSECostUnitManageId) + "parent.__doPostBack('','HSSECostUnitManageItemWindowClose');");
        }

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdHSSECostUnitManageItemId.Text = string.Empty;
            this.txtSortIndex.Text = string.Empty;
            this.txtReportTime.Text = string.Empty;
            this.txtCostContent.Text = string.Empty;
            this.txtQuantity.Text = string.Empty;
            this.txtMetric.Text = string.Empty;
            this.txtPrice.Text = string.Empty;
            this.txtTotalPrice.Text = string.Empty;
        }
        #endregion

        /// <summary>
        /// 单价、工程量联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            this.txtTotalPrice.Text = (Funs.GetNewDecimalOrZero(this.txtQuantity.Text) * Funs.GetNewDecimalOrZero(this.txtPrice.Text)).ToString("#0.0000");
        }

        #region 保存
        ///// <summary>
        ///// 保存按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    ShowNotify("保存成功！", MessageBoxIcon.Success);
        //    PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(this.HSSECostUnitManageId)
        //         + ActiveWindow.GetHidePostBackReference());
        //}
        #endregion

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
                decimal totalValue = items.Sum(x => x.TotalPrice) ?? 0;
               
                switch (this.Type)
                {
                    case "A1":
                        unitItem.CostA1 = totalValue;
                        unitItem.AuditCostA1 = totalValue;
                        break;
                    case "A2":
                        unitItem.CostA2 = totalValue;
                        unitItem.AuditCostA2 = totalValue;
                        break;
                    case "A3":
                        unitItem.CostA3 = totalValue;
                        unitItem.AuditCostA3 = totalValue;
                        break;
                    case "A4":
                        unitItem.CostA4 = totalValue;
                        unitItem.AuditCostA4 = totalValue;
                        break;
                    case "A5":
                        unitItem.CostA5 = totalValue;
                        unitItem.AuditCostA5 = totalValue;
                        break;
                    case "A6":
                        unitItem.CostA6 = totalValue;
                        unitItem.AuditCostA6 = totalValue;
                        break;
                    case "A7":
                        unitItem.CostA7 = totalValue;
                        unitItem.AuditCostA7 = totalValue;
                        break;
                    case "A8":
                        unitItem.CostA8 = totalValue;
                        unitItem.AuditCostA8 = totalValue;
                        break;
                    case "B1":
                        unitItem.CostB1 = totalValue;
                        break;
                    case "B2":
                        unitItem.CostB2 = totalValue;
                        break;
                    case "C1":
                        unitItem.CostC1 = totalValue;
                        break;
                    case "D1":
                        unitItem.CostD1 = totalValue;
                        break;
                    case "D2":
                        unitItem.CostD2 = totalValue;
                        break;
                    case "D3":
                        unitItem.CostD3 = totalValue;
                        break;
                }

                BLL.HSSECostUnitManageService.UpdateHSSECostUnitManage(unitItem);
            }
        }
    }
}