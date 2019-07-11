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
    public partial class ViolationClassificationStatistics : PageBase
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
            bf.HeaderText = "统计项";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            Grid1.Columns.Add(bf);

            bf = new FineUIPro.BoundField();
            bf.DataField = "Total";
            bf.DataFormatString = "{0}";
            bf.HeaderText = "合计";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(80);
            Grid1.Columns.Add(bf);

            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                bf = new FineUIPro.BoundField();
                bf.DataField = hazardRegisterType.TypeCode;
                bf.DataFormatString = "{0}";
                bf.HeaderText = hazardRegisterType.RegisterTypesName;
                bf.Width = System.Web.UI.WebControls.Unit.Pixel(85);
                Grid1.Columns.Add(bf);
            }
            //专项检查
            bf = new FineUIPro.BoundField();
            bf.DataField = "Special";
            bf.DataFormatString = "{0}";
            bf.HeaderText = "专项检查";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(85);
            Grid1.Columns.Add(bf);
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
                                   && x.States != "4" && (x.ProblemTypes == "1" || x.ProblemTypes == "4")
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

            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                table.Columns.Add(new DataColumn(hazardRegisterType.TypeCode, typeof(String)));
            }
            //专项检查
            table.Columns.Add(new DataColumn("Special", typeof(String)));
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
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                double num = hazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                if (totalNum > 0)
                {
                    rowTime[hazardRegisterType.TypeCode] = (num / totalNum).ToString("P1");
                }
                else
                {
                    rowTime[hazardRegisterType.TypeCode] = "0%";
                }
                rowTime2[hazardRegisterType.TypeCode] = hazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
            }
            //专项检查
            double num1 = hazardRegisters.Where(x => x.ProblemTypes == "4").Count();
            if (totalNum > 0)
            {
                rowTime["Special"] = (num1 / totalNum).ToString("P1");
            }
            else
            {
                rowTime["Special"] = "0%";
            }
            rowTime2["Special"] = hazardRegisters.Where(x => x.ProblemTypes == "4").Count();

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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("违章分类占比统计" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
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
                                   && x.States != "4" && (x.ProblemTypes == "1" || x.ProblemTypes == "4")
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
            table.Columns.Add("类型", typeof(string));
            table.Columns.Add("数量", typeof(string));
            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                DataRow rowTime = table.NewRow();
                rowTime["类型"] = hazardRegisterType.RegisterTypesName;
                var typeHazad = hazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId);
                rowTime["数量"] = typeHazad.Count();
                table.Rows.Add(rowTime);
            }
            //专项检查
            DataRow rowSpecial = table.NewRow();
            rowSpecial["类型"] = "专项检查";
            var typeSpecial = hazardRegisters.Where(x => x.ProblemTypes == "4");
            rowSpecial["数量"] = typeSpecial.Count();
            table.Rows.Add(rowSpecial);
            this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(table, "违章分类占比统计", this.drpChartType.SelectedValue, 1130, 450, this.ckbShow.Checked));
        }
    }
}