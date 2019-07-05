using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentData : PageBase
    {
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
                ////权限按钮方法
                this.GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select AccidentDetailSort.AccidentDetailSortId,AccidentDetailSort.MonthReportId,AccidentDetailSort.Abstract,AccidentDetailSort.AccidentType,AccidentDetailSort.PeopleNum,AccidentDetailSort.WorkingHoursLoss,AccidentDetailSort.EconomicLoss,AccidentDetailSort.AccidentDate"
                         + @" from Manager_AccidentDetailSortB AS AccidentDetailSort WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtDate.Text.Trim()))
            {
                strSql += " AND AccidentDetailSort.AccidentDate = @AccidentDate";
                listStr.Add(new SqlParameter("@AccidentDate", this.txtDate.Text.Trim()));
            }
            strSql += " order by AccidentDetailSort.AccidentDate desc";
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

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
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

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        BLL.AccidentDetailSortBService.DeleteAccidentDetailSortByAccidentDetailSortId(rowID);
                        BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除事故台账信息", rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerAccidentDataListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换项目代码
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectCode(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(monthReport.ProjectId);
                    if (project != null)
                    {
                        return project.ProjectCode;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 转换项目名称
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectName(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    return BLL.ProjectService.GetProjectNameByProjectId(monthReport.ProjectId);
                }
            }
            return "";
        }

        /// <summary>
        /// 转换项目经理
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertProjectManagerName(object monthReportId)
        {
            if (monthReportId != null)
            {
                Model.Manager_MonthReportB monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(monthReportId.ToString());
                if (monthReport != null)
                {
                    ///项目经理
                    var m = Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == monthReport.ProjectId && x.RoleId == BLL.Const.ProjectManager);
                    if (m != null)
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(m.UserId);
                        if (user != null)
                        {
                            return user.UserName;
                        }
                    }
                }
            }
            return "";
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("事故台账" + filename, System.Text.Encoding.UTF8) + ".xls");
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
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "ProjectName")
                    {
                        html = (row.FindControl("lblProjectName") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "ProjectManagerName")
                    {
                        html = (row.FindControl("lblProjectManagerName") as AspNet.Label).Text;
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