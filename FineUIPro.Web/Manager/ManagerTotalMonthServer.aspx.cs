using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalMonthServer : PageBase
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
                ////权限按钮方法                 
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();                 
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据GV

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT DISTINCT CONVERT(NVARCHAR(7),CompileDate,20) AS TotalMonth FROM Manager_ManagerTotalMonth ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            //strSql += " AND ManagerTotalMonth.ProjectId = @ProjectId";
            //if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            //{
            //    listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
            //}
            //else
            //{
            //    listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            //}

            //if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            //{
            //    strSql += " AND ManagerTotalMonth.Title LIKE @Title";
            //    listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            //}
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {            
            BindGrid();
        }
        #endregion        

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "monthContent")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView.aspx?TotalMonth={0}&Type=1", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent2")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView.aspx?TotalMonth={0}&Type=2", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent3")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView.aspx?TotalMonth={0}&Type=3", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent4")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ManagerTotalMonthServerView2.aspx?TotalMonth={0}&Type=4", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent5")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView.aspx?TotalMonth={0}&Type=5", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent6")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView.aspx?TotalMonth={0}&Type=6", e.CommandArgument.ToString(), "查看 - ")));
            }
            if (e.CommandName == "monthContent7")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthServerView3.aspx?TotalMonth={0}&Type=7", e.CommandArgument.ToString(), "查看 - ")));
            }
        }
        #endregion
    }
}