using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Information
{
    public partial class DrillConductedQuarterlyReport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillConductedQuarterlyReportId
        {
            get
            {
                return (string)ViewState["DrillConductedQuarterlyReportId"];
            }
            set
            {
                ViewState["DrillConductedQuarterlyReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Information_DrillConductedQuarterlyReportItem> drillConductedQuarterlyReportItems = new List<Model.Information_DrillConductedQuarterlyReportItem>();
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                BLL.ConstValue.InitConstValueDropDownList(this.drpQuarter, ConstValue.Group_0011, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, false);
                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;
                DateTime showDate = System.DateTime.Now.AddMonths(-3);
                this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                this.drpYear.SelectedValue = showDate.Year.ToString();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                GetValue();
            }
        }
        #endregion

        #region 清空文本框
        private void SetEmpty()
        {
            this.SimpleForm1.Title = string.Empty;
            txtUnitName.Text = string.Empty;
            txtQuarter.Text = string.Empty;
            txtCompileDate.Text = string.Empty;
            this.lbHandleMan.Text = string.Empty;
            Grid1.DataSource = null;
            Grid1.DataBind();

            this.OutputSummaryData();
        }
        #endregion

        #region 获取记录值
        private void GetValue()
        {
            int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
            int quarter = Funs.GetNewIntOrZero(drpQuarter.SelectedValue);
            this.DrillConductedQuarterlyReportId = string.Empty;
            Model.View_Information_DrillConductedQuarterlyReport report = Funs.DB.View_Information_DrillConductedQuarterlyReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.Quarter == quarter && e.YearId == year);
            if (report != null)
            {
                string state = string.Empty;
                if (report.UpState == BLL.Const.UpState_3)
                {
                    state = "(已上报)";
                }
                else
                {
                    if (report.HandleState == BLL.Const.HandleState_1)
                    {
                        state = "(待提交)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_2)
                    {
                        state = "(待审核)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_3)
                    {
                        state = "(待审批)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_4)
                    {
                        state = "(待上报)";
                    }
                }
                this.SimpleForm1.Title = "应急演练开展情况季报表" + state;
                this.DrillConductedQuarterlyReportId = report.DrillConductedQuarterlyReportId;
                txtUnitName.Text = "单位：" + report.UnitName;
                txtQuarter.Text = "季度：" + report.Quarters;
                if (report.HandleState == BLL.Const.HandleState_1 || report.UpState == BLL.Const.UpState_3)
                {
                    this.lbHandleMan.Hidden = true;
                }
                else
                {
                    this.lbHandleMan.Hidden = false;
                    this.lbHandleMan.Text = "下一步办理人：" + report.UserName;
                }
                if (report.ReportDate != null)
                {
                    txtCompileDate.Text = "制表时间：" + string.Format("{0:yyyy-MM-dd}", report.ReportDate);
                }
                BindGrid1();
            }
            else
            {
                SetEmpty();
            }
            this.GetButtonPower();
        }
        #endregion

        #region 加载Grid
        private void BindGrid1()
        {
            string strSql = "select * from dbo.Information_DrillConductedQuarterlyReportItem where DrillConductedQuarterlyReportId = @DrillConductedQuarterlyReportId order by SortIndex";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@DrillConductedQuarterlyReportId",this.DrillConductedQuarterlyReportId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            OutputSummaryData();

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData()
        {
            int totalConductCount = 0;//总体情况（举办次数）
            int totalPeopleCount = 0;//总体情况（参演人数）
            decimal totalTotalInvestment = 0;//总体情况（直接投入）
            int hqConductCount = 0;//企业总部（举办次数）
            int hqPeopleCount = 0;//企业总部（参演人数）
            decimal hqInvestment = 0;//企业总部（直接投入）
            int basicConductCount = 0;//基层单位（举办次数）
            int basicPeopleCount = 0;//基层单位（参演人数）
            decimal basicInvestment = 0;//基层单位（直接投入）

            drillConductedQuarterlyReportItems = (from x in Funs.DB.Information_DrillConductedQuarterlyReportItem where x.DrillConductedQuarterlyReportId == this.DrillConductedQuarterlyReportId select x).ToList();
            foreach (var item in drillConductedQuarterlyReportItems)
            {
                totalConductCount += Convert.ToInt32(item.TotalConductCount);
                totalPeopleCount += Convert.ToInt32(item.TotalPeopleCount);
                totalTotalInvestment += Convert.ToDecimal(item.TotalInvestment);
                hqConductCount += Convert.ToInt32(item.HQConductCount);
                hqPeopleCount += Convert.ToInt32(item.HQPeopleCount);
                hqInvestment += Convert.ToDecimal(item.HQInvestment);
                basicConductCount += Convert.ToInt32(item.BasicConductCount);
                basicPeopleCount += Convert.ToInt32(item.BasicPeopleCount);
                basicInvestment += Convert.ToDecimal(item.BasicInvestment);
            }

            JObject summary = new JObject();
            summary.Add("IndustryType", "合计：");
            summary.Add("TotalConductCount", totalConductCount);
            summary.Add("TotalPeopleCount", totalPeopleCount);
            summary.Add("TotalInvestment", totalTotalInvestment);
            summary.Add("HQConductCount", hqConductCount);
            summary.Add("HQPeopleCount", hqPeopleCount);
            summary.Add("HQInvestment", hqInvestment);
            summary.Add("BasicConductCount", basicConductCount);
            summary.Add("BasicPeopleCount", basicPeopleCount);
            summary.Add("BasicInvestment", basicInvestment);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region 增加、修改、删除、审核、审批、上报按钮事件
        /// <summary>
        /// Tree增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillConductedQuarterlyReportAdd.aspx?UnitId={0}&&Year={1}&&Quarter={2}", this.CurrUser.UnitId, this.drpYear.SelectedValue, this.drpQuarter.SelectedValue, "编辑 - ")));
        }

        /// <summary>
        /// 显示编辑页面
        /// </summary>
        private void ShowEdit()
        {
            Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report == null)
            {
                Alert.ShowInTop("所选时间无报表记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillConductedQuarterlyReportAdd.aspx?DrillConductedQuarterlyReportId={0}", report.DrillConductedQuarterlyReportId, "编辑 - ")));
        }

        /// <summary>
        /// Tree编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit1_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit2_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// Tree删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, report.YearId.ToString() + "-" + report.Quarter.ToString(),
                        report.DrillConductedQuarterlyReportId, BLL.Const.DrillConductedQuarterlyReportMenuId, BLL.Const.BtnDelete);
                BLL.ProjectDataFlowSetService.DeleteFlowSetByDataId(report.DrillConductedQuarterlyReportId);
                BLL.DrillConductedQuarterlyReportItemService.DeleteDrillConductedQuarterlyReportItemList(report.DrillConductedQuarterlyReportId);
                BLL.DrillConductedQuarterlyReportService.DeleteDrillConductedQuarterlyReportById(report.DrillConductedQuarterlyReportId);
                
                SetEmpty();
                this.btnNew.Hidden = false;
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 过滤、分页、排序、关闭窗口
        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        /// <summary>
        /// 分页列表显示条数下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid1();
        }

        /// <summary>
        /// 关闭Grid1弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DrillConductedQuarterlyReportMenuId);
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
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnPrint))
                {
                    this.btnPrint.Hidden = false;
                }
                int year = Funs.GetNewIntOrZero(this.drpYear.SelectedValue);
                int quarter = Funs.GetNewIntOrZero(this.drpQuarter.SelectedValue);
                var report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(this.drpUnit.SelectedValue, year, quarter);
                this.btnAudit1.Hidden = true;
                this.btnAudit2.Hidden = true;
                this.btnUpdata.Hidden = true;
                if (report != null)
                {
                    this.btnNew.Hidden = true;
                    if (report.HandleMan == this.CurrUser.UserId)   //当前人是下一步办理入
                    {
                        if (report.HandleState == BLL.Const.HandleState_2)
                        {
                            this.btnAudit1.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_3)
                        {
                            this.btnAudit2.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnDelete.Hidden = true;
                            this.btnUpdata.Hidden = false;
                        }
                    }
                    if (report.UpState == BLL.Const.UpState_3)
                    {
                        this.btnUpdata.Hidden = true;
                        this.btnEdit.Hidden = true;
                        this.btnDelete.Hidden = true;
                    }
                    if (report.HandleMan == this.CurrUser.UserId || report.CompileMan == this.CurrUser.UserName)
                    {
                        this.btnEdit.Hidden = false;
                    }
                    else
                    {
                        this.btnEdit.Hidden = true;
                    }
                }
            }

            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                this.btnDelete.Hidden = false;
            }
        }
        #endregion

        #region 单位下拉框联动事件
        /// <summary>
        /// 单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetValue();
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../DataIn/DrillConductedQuarterlyReportImport.aspx", "导入 - ")));
        }
        #endregion

        #region 关闭导入弹出窗口
        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }

        /// <summary>
        /// 关闭查看审批信息弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {

        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.Information_DrillConductedQuarterlyReportId, report.DrillConductedQuarterlyReportId, "", "打印 - ")));
            }
        }
        #endregion

        #region 季度向前/向后
        /// <summary>
        /// 前一季度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBulletLeft_Click(object sender, EventArgs e)
        {
            SetMonthChange("-");
        }

        /// <summary>
        /// 后一季度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulletRight_Click(object sender, EventArgs e)
        {
            SetMonthChange("+");
        }

        /// <summary>
        /// 季度加减变化
        /// </summary>
        /// <param name="type"></param>
        private void SetMonthChange(string type)
        {
            DateTime? nowDate = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + (Funs.GetNewIntOrZero(this.drpQuarter.SelectedValue) * 3).ToString());
            if (nowDate.HasValue)
            {
                DateTime showDate = new DateTime();
                if (type == "+")
                {
                    showDate = nowDate.Value.AddMonths(3);
                }
                else
                {
                    showDate = nowDate.Value.AddMonths(-3);
                }

                this.drpYear.SelectedValue = showDate.Year.ToString();
                this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(showDate).ToString();
                ///值变化
                GetValue();
            }
        }
        #endregion

        #region 查看审批信息
        /// <summary>
        /// 查看审批信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpQuarter.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("ReportAuditSee.aspx?Id={0}", report.DrillConductedQuarterlyReportId, "查看 - ")));
            }
            else
            {
                ShowNotify("所选月份无记录！", MessageBoxIcon.Warning);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("应急演练开展情况季报表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();

        }

#pragma warning disable CS0108 // “DrillConductedQuarterlyReport.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “DrillConductedQuarterlyReport.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
    }
}