using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Supervise
{
    public partial class SuperviseCheckReport : PageBase
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
                btnNew.OnClientClick = Window1.GetShowReference("SuperviseCheckReportEdit.aspx") + "return false;";
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());

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
            string strSql = @"SELECT S.SuperviseCheckReportId,S.SuperviseCheckReportCode,S.CheckDate,S.ProjectId,P.ProjectName,S.UnitId,u.UnitName,S.CheckTeam,S.EvaluationResult,S.AttachUrl,S.IsIssued"
                            + @" FROM dbo.Supervise_SuperviseCheckReport AS S"
                            + @" LEFT JOIN dbo.Base_Project AS P ON P.ProjectId=S.ProjectId"
                            + @" LEFT JOIN dbo.Base_Unit AS U ON U.UnitId=S.UnitId"
                            + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();          
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim())) 
            {
                strSql += " AND (S.SuperviseCheckReportCode like @name OR P.ProjectName like @name OR u.UnitName like @name OR S.CheckTeam like @name OR S.EvaluationResult like @name)";
                listStr.Add(new SqlParameter("@name", "%" + this.txtName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                System.Web.UI.WebControls.LinkButton lbtnRectify = (System.Web.UI.WebControls.LinkButton)(Grid1.Rows[i].FindControl("lbtnRectify"));
                System.Web.UI.WebControls.Label lblRectify = (System.Web.UI.WebControls.Label)(Grid1.Rows[i].FindControl("lblRectify"));
                Model.Supervise_SuperviseCheckReport report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(lbtnRectify.CommandArgument);
                if (report != null)
                {
                    if (report.IsIssued == "1")   //已下发
                    {
                        lbtnRectify.Visible = false;
                        Model.Supervise_SuperviseCheckRectify rectify = BLL.SuperviseCheckRectifyService.GetSuperviseCheckRectifyBySuperviseCheckReportId(lbtnRectify.CommandArgument);
                        string code = string.Empty;
                        if (rectify != null)
                        {
                            code = rectify.SuperviseCheckRectifyCode;
                        }
                        lblRectify.Text = "已下发：" + code;

                    }
                    else
                    {
                        lbtnRectify.Visible = true;
                        lblRectify.Text = string.Empty;
                        Grid1.Rows[i].RowCssClass = "red";
                    }

                    Model.Supervise_SubUnitCheckRectify sub = BLL.SubUnitCheckRectifyService.GetSubUnitCheckRectifyBySuperviseCheckReportId(lbtnRectify.CommandArgument);
                    if (sub != null && sub.UpState == BLL.Const.UpState_3)
                    {
                        lbtnSubRec.Text = "报告单（已上报）";
                    }
                    else
                    {
                        lbtnSubRec.Text = "报告单";
                    }
                }
            }
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

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void Window2_Close1(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
            System.Web.UI.WebControls.LinkButton lbtnSubRec = sender as System.Web.UI.WebControls.LinkButton;
            if (lbtnSubRec != null)
            {
                Model.Supervise_SubUnitCheckRectify sub = BLL.SubUnitCheckRectifyService.GetSubUnitCheckRectifyBySuperviseCheckReportId(lbtnSubRec.CommandArgument);
                if (sub != null && sub.UpState == BLL.Const.UpState_3)
                {
                    lbtnSubRec.Text = "报告（已上报）";
                }
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                Model.Supervise_SuperviseCheckRectify retify = BLL.SuperviseCheckRectifyService.GetSuperviseCheckRectifyBySuperviseCheckReportId(rowID);
                if (retify != null)
                {
                    ShowNotify("该检查报告已下发检查整改记录，请先删除对应检查整改记录！", MessageBoxIcon.Warning);
                    return;
                }
                BLL.SubUnitCheckRectifyItemService.DeleteSubUnitCheckRectifyBySuperviseCheckReportId(rowID);
                BLL.SuperviseCheckReportItemService.DeleteSuperviseCheckReportItemBySuperviseCheckReportId(rowID);
                BLL.SuperviseCheckReportService.DeleteSuperviseCheckReportById(rowID);

                BindGrid();
                ShowNotify("删除数据成功!");
            }
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
            btnEdit_Click(null, null);
        }
        #endregion

        #region 下发整改单
        /// <summary>
        /// 下发整改单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRectify_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.LinkButton lbtnRectify = sender as System.Web.UI.WebControls.LinkButton;
            Model.Supervise_SuperviseCheckReport report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(lbtnRectify.CommandArgument);
            if (report != null)
            {
                var superviseCheckRectify = BLL.SuperviseCheckRectifyService.GetSuperviseCheckRectifyBySuperviseCheckReportId(report.SuperviseCheckReportId);

                if (superviseCheckRectify == null)
                {
                    if (BLL.SuperviseCheckReportItemService.GetSelectedSuperviseCheckReportItemBySuperviseCheckReportId(lbtnRectify.CommandArgument).Count == 0)
                    {
                        ShowNotify("尚未选择检查隐患，无法下发！", MessageBoxIcon.Warning);
                        return;
                    }

                    Model.Supervise_SuperviseCheckRectify rectify = new Model.Supervise_SuperviseCheckRectify
                    {
                        SuperviseCheckRectifyId = SQLHelper.GetNewID(typeof(Model.Supervise_SuperviseCheckRectify)),
                        SuperviseCheckRectifyCode = report.SuperviseCheckReportCode + "-R",
                        ProjectId = report.ProjectId,
                        UnitId = report.UnitId,
                        SuperviseCheckReportId = report.SuperviseCheckReportId,
                        CheckDate = report.CheckDate,
                        HandleState = "1"    //未签发
                    };
                    BLL.SuperviseCheckRectifyService.AddSuperviseCheckRectify(rectify);
                    var reportItems = BLL.SuperviseCheckReportItemService.GetSuperviseCheckReportItemBySuperviseCheckReportId(lbtnRectify.CommandArgument);
                    foreach (var item in reportItems)
                    {
                        if (item.IsSelected == true)
                        {
                            Model.Supervise_SuperviseCheckRectifyItem newItem = new Model.Supervise_SuperviseCheckRectifyItem
                            {
                                SuperviseCheckRectifyItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SuperviseCheckRectifyItem)),
                                SuperviseCheckRectifyId = rectify.SuperviseCheckRectifyId,
                                RectifyItemId = item.RectifyItemId,
                                ConfirmMan = this.CurrUser.UserName,
                                ConfirmDate = DateTime.Now,
                                AttachUrl = item.AttachUrl
                            };
                            BLL.SuperviseCheckRectifyItemService.AddSuperviseCheckRectifyItem(newItem);
                        }
                    }
                    report.IsIssued = "1";  //已下发
                    BLL.SuperviseCheckReportService.UpdateSuperviseCheckReport(report);
                    ShowNotify("已下发完成！", MessageBoxIcon.Success);
                    BindGrid();
                }
                else
                {
                    ShowNotify("已下发整改单！", MessageBoxIcon.Warning);
                    return;
                }
            }
          
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {

            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string SuperviseCheckReportId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SuperviseCheckReportEdit.aspx?SuperviseCheckReportId={0}", SuperviseCheckReportId, "编辑 - ")));

        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    Model.Supervise_SuperviseCheckRectify retify = BLL.SuperviseCheckRectifyService.GetSuperviseCheckRectifyBySuperviseCheckReportId(rowID);
                    if (retify != null)
                    {
                        ShowNotify("删除的检查报告中已下发检查整改记录，请先删除对应检查整改记录！", MessageBoxIcon.Warning);
                        return;
                    }
                    BLL.SubUnitCheckRectifyItemService.DeleteSubUnitCheckRectifyBySuperviseCheckReportId(rowID);
                    BLL.SuperviseCheckReportItemService.DeleteSuperviseCheckReportItemBySuperviseCheckReportId(rowID);
                    BLL.SuperviseCheckReportService.DeleteSuperviseCheckReportById(rowID);
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 报告报告
        /// <summary>
        /// 报告报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSubRec_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.LinkButton lbtnSubRec = sender as System.Web.UI.WebControls.LinkButton;
            Model.Supervise_SuperviseCheckReport report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(lbtnSubRec.CommandArgument);
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SubUnitCheckRectifyEdit.aspx?SuperviseCheckReportId={0}", lbtnSubRec.CommandArgument, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SuperviseCheckReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全监督检查报告" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtmlThis(Grid1));
            Response.End();

        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtmlThis(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            MultiHeaderTable mht = new MultiHeaderTable();
            mht.ResolveMultiHeaderTable(Grid1.Columns);
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            foreach (List<object[]> rows in mht.MultiTable)
            {
                sb.Append("<tr>");
                foreach (object[] cell in rows)
                {
                    int rowspan = Convert.ToInt32(cell[0]);
                    int colspan = Convert.ToInt32(cell[1]);
                    GridColumn column = cell[2] as GridColumn;

                    sb.AppendFormat("<th{0}{1}{2}>{3}</th>",
                       rowspan != 1 ? " rowspan=\"" + rowspan + "\"" : "",
                       colspan != 1 ? " colspan=\"" + colspan + "\"" : "",
                      colspan != 1 ? " style=\"text-align:center;\"" : "",
                        column.HeaderText);
                }
                sb.Append("</tr>");
            }
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in mht.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfRectify")
                    {
                        html = (row.FindControl("lbtnRectify") as AspNet.LinkButton).Text;
                    }
                    if (column.ColumnID == "tfSubRec")
                    {
                        html = (row.FindControl("lbtnSubRec") as AspNet.LinkButton).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        #region 多表头处理
        /// <summary>
        /// 多表头处理
        /// </summary>
        public class MultiHeaderTable
        {
            // 包含 rowspan，colspan 的多表头，方便生成 HTML 的 table 标签
            public List<List<object[]>> MultiTable = new List<List<object[]>>();
            // 最终渲染的列数组
            public List<GridColumn> Columns = new List<GridColumn>();
            public void ResolveMultiHeaderTable(GridColumnCollection columns)
            {
                List<object[]> row = new List<object[]>();
                foreach (GridColumn column in columns)
                {
                    object[] cell = new object[4];
                    cell[0] = 1;    // rowspan
                    cell[1] = 1;    // colspan
                    cell[2] = column;
                    cell[3] = null;
                    row.Add(cell);
                }
                ResolveMultiTable(row, 0);
                ResolveColumns(row);
            }

            private void ResolveColumns(List<object[]> row)
            {
                foreach (object[] cell in row)
                {
                    GroupField groupField = cell[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        List<object[]> subrow = new List<object[]>();
                        foreach (GridColumn column in groupField.Columns)
                        {
                            subrow.Add(new object[]
                           {
                               1,
                                1,
                               column,
                                groupField
                            });
                        }
                        ResolveColumns(subrow);
                    }
                    else
                    {
                        Columns.Add(cell[2] as GridColumn);
                    }
                }
            }

            private void ResolveMultiTable(List<object[]> row, int level)
            {
                List<object[]> nextrow = new List<object[]>();

                foreach (object[] cell in row)
                {
                    GroupField groupField = cell[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        // 如果当前列包含子列，则更改当前列的 colspan，以及增加父列（向上递归）的colspan
                        cell[1] = Convert.ToInt32(groupField.Columns.Count);
                        PlusColspan(level - 1, cell[3] as GridColumn, groupField.Columns.Count - 1);

                        foreach (GridColumn column in groupField.Columns)
                        {
                            nextrow.Add(new object[]
                           {
                               1,
                                1,
                                column,
                                groupField
                           });
                        }
                    }
                }
                MultiTable.Add(row);
                // 如果当前下一行，则增加上一行（向上递归）中没有子列的列的 rowspan
                if (nextrow.Count > 0)
                {
                    PlusRowspan(level);
                    ResolveMultiTable(nextrow, level + 1);
                }
            }

            private void PlusRowspan(int level)
            {
                if (level < 0)
                {
                    return;
                }
                foreach (object[] cells in MultiTable[level])
                {
                    GroupField groupField = cells[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        // ...
                    }
                    else
                    {
                        cells[0] = Convert.ToInt32(cells[0]) + 1;
                    }
                }
                PlusRowspan(level - 1);
            }

            private void PlusColspan(int level, GridColumn parent, int plusCount)
            {
                if (level < 0)
                {
                    return;
                }

                foreach (object[] cells in MultiTable[level])
                {
                    GridColumn column = cells[2] as GridColumn;
                    if (column == parent)
                    {
                        cells[1] = Convert.ToInt32(cells[1]) + plusCount;

                        PlusColspan(level - 1, cells[3] as GridColumn, plusCount);
                    }
                }
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}