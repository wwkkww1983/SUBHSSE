using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;
using System.Web;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class SubUnitRateStatistics : PageBase
    {
        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行
        protected void Page_Init(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void InitGrid()
        {
            FineUIPro.BoundField bf;
            bf = new FineUIPro.BoundField();
            bf.DataField = "Name";
            bf.DataFormatString = "{0}";
            bf.HeaderText = "考核内容";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            Grid1.Columns.Add(bf);

            bf = new FineUIPro.BoundField();
            bf.DataField = "Total";
            bf.DataFormatString = "{0}";
            bf.HeaderText = "项目部";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            Grid1.Columns.Add(bf);

            var responsibilityUnits = (from x in Funs.DB.Base_Unit
                                       join y in Funs.DB.Project_ProjectUnit
                                       on x.UnitId equals y.UnitId
                                       where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                       select x).Distinct().ToList();
            foreach (var responsibilityUnit in responsibilityUnits)
            {
                bf = new FineUIPro.BoundField();
                bf.DataField = responsibilityUnit.UnitCode;
                bf.DataFormatString = "{0}";
                bf.HeaderText = !string.IsNullOrEmpty(responsibilityUnit.ShortUnitName) ? responsibilityUnit.ShortUnitName : responsibilityUnit.UnitName;
                bf.Width = System.Web.UI.WebControls.Unit.Pixel(200);
                Grid1.Columns.Add(bf);
            }

        }

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

            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var hazardRegisters = (from x in Funs.DB.View_Hazard_HazardRegister
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   && x.States != "4" && x.ProblemTypes == "1"
                                   select x);
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtEndDate.Text.Trim()).AddDays(1));
            }

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Total", typeof(string));

            var responsibilityUnits = (from x in Funs.DB.Base_Unit
                                       join y in Funs.DB.Project_ProjectUnit
                                       on x.UnitId equals y.UnitId
                                       where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                       select x).Distinct().ToList();
            foreach (var responsibilityUnit in responsibilityUnits)
            {
                table.Columns.Add(new DataColumn(responsibilityUnit.UnitCode, typeof(String)));
            }
            double totalNum = hazardRegisters.Count();
            DataRow rowTime = table.NewRow();
            DataRow rowTime2 = table.NewRow();
            rowTime["Name"] = "违章率";
            if (totalNum > 0)
            {
                rowTime["Total"] = "100%";
            }
            else
            {
                rowTime["Total"] = "0%";
            }
            rowTime2["Name"] = "违章数";
            rowTime2["Total"] = hazardRegisters.Count();
            foreach (var responsibilityUnit in responsibilityUnits)
            {
                double num = hazardRegisters.Where(x => x.ResponsibleUnit == responsibilityUnit.UnitId).Count();
                if (totalNum > 0)
                {
                    rowTime[responsibilityUnit.UnitCode] = (num / totalNum).ToString("P2");
                }
                else
                {
                    rowTime[responsibilityUnit.UnitCode] = "0%";
                }
                rowTime2[responsibilityUnit.UnitCode] = hazardRegisters.Where(x => x.ResponsibleUnit == responsibilityUnit.UnitId).Count();
            }
            table.Rows.Add(rowTime);
            table.Rows.Add(rowTime2);
            //Grid1.DataKeyNames = new string[] { "Name"};
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
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

        #region 统计按钮事件
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BindGrid();
            AnalyseData();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("分包商占比统计" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “SubUnitRateStatistics.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “SubUnitRateStatistics.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/><html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
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
                    sb.AppendFormat("<td x:str>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 图形变换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AnalyseData();
        }

        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.AnalyseData();
        }

        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            var hazardRegisters = (from x in Funs.DB.View_Hazard_HazardRegister
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   && x.States != "4" && x.ProblemTypes == "1"
                                   select x);
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                hazardRegisters = hazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtEndDate.Text.Trim()).AddDays(1));
            }

            DataTable table = new DataTable();
            table.Columns.Add("单位名称", typeof(string));
            table.Columns.Add("数量", typeof(string));
            var responsibilityUnits = (from x in Funs.DB.Base_Unit
                                       join y in Funs.DB.Project_ProjectUnit
                                       on x.UnitId equals y.UnitId
                                       where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                       select x).Distinct().ToList();
            foreach (var responsibilityUnit in responsibilityUnits)
            {
                DataRow rowTime = table.NewRow();
                rowTime["单位名称"] = !string.IsNullOrEmpty(responsibilityUnit.ShortUnitName) ? responsibilityUnit.ShortUnitName : responsibilityUnit.UnitName;
                int num = hazardRegisters.Where(x => x.ResponsibleUnit == responsibilityUnit.UnitId).Count();
                rowTime["数量"] = num;
                table.Rows.Add(rowTime);
            }
            this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(table, "分包商占比统计", this.drpChartType.SelectedValue, 1130, 450, this.ckbShow.Checked));

        }
    }
}