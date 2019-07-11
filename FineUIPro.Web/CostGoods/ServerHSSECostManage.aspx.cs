using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CostGoods
{
    public partial class ServerHSSECostManage : PageBase
    {
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
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.ProjectService.InitNoEProjectDropDownList(this.drpProject, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonths, BLL.ConstValue.Group_0009, true);
                // 绑定表格
                this.BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT  hsseCost.HSSECostManageId,hsseCost.ProjectId,Project.ProjectName,hsseCost.CompileManId,sysUser.UserName AS CompileManName,hsseCost.CompileDate,hsseCost.Month,(CAST(YEAR(hsseCost.Month) AS varchar(4)) +'年'+ CAST( Month(hsseCost.Month) AS varchar(2))+'月') AS Months,ISNULL(hsseCost.Code,CodeRecords.Code) AS Code,hsseCost.ReportDate"
                        + @" ,hsseCost.MainIncome,hsseCost.Remark1,hsseCost.ConstructionIncome,hsseCost.Remark2,hsseCost.SafetyCosts,hsseCost.Remark3 "                        
                        + @",UnitCost.AuditedSubUnitCostSum,UnitCost.EngineeringCostSum,UnitCost.CostRatioSum"
                        + @" FROM CostGoods_HSSECostManage AS hsseCost"
                        + @" LEFT JOIN (SELECT HSSECostManageId,SUM(AuditedSubUnitCost) AS AuditedSubUnitCostSum,SUM(EngineeringCost) AS EngineeringCostSum,"
			            + @" (CASE WHEN SUM(EngineeringCost) IS NULL OR SUM(EngineeringCost)=0 THEN 0 ELSE"
                        + @" Convert(decimal(18,2),(ISNULL(SUM(AuditedSubUnitCost),0)/SUM(EngineeringCost) *100)) END) AS CostRatioSum"
			            + @" FROM CostGoods_HSSECostUnitManage GROUP BY HSSECostManageId) AS UnitCost ON hsseCost.HSSECostManageId=UnitCost.HSSECostManageId"
                        + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON hsseCost.HSSECostManageId = CodeRecords.DataId"
                        + @" LEFT JOIN Base_Project AS Project ON hsseCost.ProjectId =Project.ProjectId"
                        + @" LEFT JOIN Sys_User AS sysUser ON hsseCost.CompileManId =sysUser.UserId"                        
                        + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND hsseCost.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            }
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND YEAR(hsseCost.Month) = @Year";
                listStr.Add(new SqlParameter("@Year", this.drpYear.SelectedValue));
            }
            if (this.drpMonths.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Month(hsseCost.Month) = @Months";
                listStr.Add(new SqlParameter("@Months", this.drpMonths.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 查看
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键查看事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostManageView.aspx?HSSECostManageId={0}", Grid1.SelectedRowID, "查看 - ")));
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全费用管理" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 50000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                   
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion  
    }
}