using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageAuditItemView : PageBase
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
            string strSql = @"SELECT *,(CASE WHEN IsAgree =1 THEN '同意' WHEN IsAgree =0 THEN '不同意' ELSE '' END) AS IsAgreeName FROM dbo.CostGoods_HSSECostUnitManageItem"
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
    }
}