using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class SummaryList : PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                //BindGrid();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT * FROM dbo.View_SummaryList" +
                            @" WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            strSql += " AND CheckType = @CheckType";
            listStr.Add(new SqlParameter("@CheckType", this.drpCheckType.SelectedValue));
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                strSql += " AND CheckTime >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                strSql += " AND CheckTime <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndTime.Text.Trim()));
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

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Text_OnTextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 查询按钮
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BindGrid();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("汇总清单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
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
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "CheckItemType")
                    {
                        html = (row.FindControl("lbCheckType") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
        }
        #endregion
    }
}