using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataEPlanItem : PageBase
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
              
                // 绑定表格              
                this.BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {

            List<SqlParameter> listStr = new List<SqlParameter>();
            string strSql = @"SELECT Plans.SafetyDataEPlanId,SafetyDataE.SafetyDataEId,SafetyDataE.Code,SafetyDataE.Title,SafetyDataE.Score,SafetyDataE.Score AS ShouldScore"
                       + @", Plans.CheckDate ,Plans.ReminderDate,Plans.ProjectId "
                       + @" FROM SafetyDataE_SafetyDataEPlan AS Plans  "
                       + @" LEFT JOIN SafetyDataE_SafetyDataE AS SafetyDataE ON SafetyDataE.SafetyDataEId =Plans.SafetyDataEId "
                       + @" WHERE Plans.ProjectId='" + Request.Params["ProjectId"] + "' AND  Plans.States='" + BLL.Const.State_2 + "'";
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 排序分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全资料计划总表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 50000;
            BindGrid();
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
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTime")
                    {
                        html = (row.FindControl("lblWorkTime") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTimeYear")
                    {
                        html = (row.FindControl("lblWorkTimeYear") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTotal")
                    {
                        html = (row.FindControl("lblTotal") as AspNet.Label).Text;
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