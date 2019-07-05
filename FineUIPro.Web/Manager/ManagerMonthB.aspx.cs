using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerMonthB : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
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
            string strSql = @"SELECT MonthReport.MonthReportId,MonthReport.ProjectId,MonthReport.Months,CodeRecords.Code AS MonthReportCode,MonthReport.Manhours,MonthReport.HseManhours,MonthReport.NoStartDate,MonthReport.NoEndDate,MonthReport.AccidentRateA,MonthReport.AccidentRateB,MonthReport.AccidentRateC,Users.UserName as ReportManName"
                          + @" FROM Manager_MonthReportB AS MonthReport "
                          + @" LEFT JOIN Sys_User AS Users ON MonthReport.ReportMan=Users.UserId "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON MonthReport.MonthReportId=CodeRecords.DataId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND MonthReport.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtMonthReportCode.Text.Trim()))
            {
                strSql += " AND CodeRecords.Code LIKE @MonthReportCode";
                listStr.Add(new SqlParameter("@MonthReportCode", "%" + this.txtMonthReportCode.Text.Trim() + "%"));
            }
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

        #region 过滤表头、排序、分页、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

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

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuModify_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string MonthReportId = Grid1.SelectedRowID;
            var monthReport = BLL.MonthReportBService.GetMonthReportByMonthReportId(MonthReportId);
            int n = 6;  //月报冻结时间
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet != null)
            {
                n = Convert.ToInt32(sysSet.ConstValue);
            }
            if (monthReport != null)
            {
                int d = Convert.ToInt32(DateTime.Now.Day);
                if ((monthReport.Months.Value.Year == DateTime.Now.Year && monthReport.Months.Value.Month == DateTime.Now.Month) ||
                    ((monthReport.Months.Value.AddMonths(1).Month == DateTime.Now.Month) && d < n + 1))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportBEdit.aspx?MonthReportId={0}", MonthReportId, "编辑 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportBView.aspx?MonthReportId={0}", MonthReportId, "查看 - ")));
                }
            }
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string MonthReportId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportBView.aspx?MonthReportId={0}", MonthReportId, "查看 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.ManhoursSortBService.DeleteManhoursSortsByMonthReportId(rowID);
                    BLL.AccidentSortBService.DeleteAccidentSortsByMonthReportId(rowID);
                    BLL.HseCostBService.DeleteHseCostsByMonthReportId(rowID);
                    BLL.TrainSortBService.DeleteTrainSortsByMonthReportId(rowID);
                    BLL.MeetingSortBService.DeleteMeetingSortsByMonthReportId(rowID);
                    BLL.CheckSortBService.DeleteCheckSortsByMonthReportId(rowID);
                    BLL.IncentiveSortBService.DeleteIncentiveSortsByMonthReportId(rowID);
                    BLL.AccidentDetailSortBService.DeleteAccidentDetailSortsByMonthReportId(rowID);
                    if (BLL.CostAnalyseService.getCostAnalyseByMonths(BLL.MonthReportBService.GetMonthReportByMonthReportId(rowID).Months, this.CurrUser.LoginProjectId) != null)
                    {
                        BLL.CostAnalyseService.DeleteCostAnalyseByMonths(BLL.MonthReportBService.GetMonthReportByMonthReportId(rowID).Months);
                    }
                    BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除管理月报", rowID);
                    BLL.MonthReportBService.DeleteMonthReportByMonthReportId(rowID);
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet != null)
            {
                if (BLL.MonthReportBService.GetMonthReportByDate(DateTime.Now, this.CurrUser.LoginProjectId))
                {
                    Alert.ShowInTop("当前月份的月报已存在！", MessageBoxIcon.Warning);
                    return;
                }
                else if (BLL.MonthReportBService.GetFreezeMonthReportByDate(DateTime.Now, this.CurrUser.LoginProjectId, !string.IsNullOrEmpty(sysSet.ConstValue) ? Convert.ToInt32(sysSet.ConstValue) : 4))
                {
                    Alert.ShowInTop("上月份月报未冻结，不能增加！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportBEdit.aspx", "编辑 - ")));
                }
            }
            else
            {
                if (BLL.MonthReportBService.GetMonthReportByDate(DateTime.Now, this.CurrUser.LoginProjectId))
                {
                    Alert.ShowInTop("当前月份的月报已存在！", MessageBoxIcon.Warning);
                    return;
                }
                else if (BLL.MonthReportBService.GetFreezeMonthReportByDate(DateTime.Now, this.CurrUser.LoginProjectId, 4))
                {
                    Alert.ShowInTop("上月份月报未冻结，不能增加！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MonthReportBEdit.aspx", "编辑 - ")));
                }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectManagerMonthBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                    this.btnMenuView.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理月报B" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
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