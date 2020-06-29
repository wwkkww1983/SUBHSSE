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
    public partial class UnitViolationStatistics : PageBase
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
            bf.HeaderText = "单位名称";
            bf.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            Grid1.Columns.Add(bf);

            bf = new FineUIPro.BoundField();
            bf.DataField = "Total";
            bf.DataFormatString = "{0}";
            bf.HeaderText = "小计";
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

            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            List<int> numList = new List<int>();
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                table.Columns.Add(new DataColumn(hazardRegisterType.TypeCode, typeof(String)));
                numList.Add(0);
            }
            var responsibilityUnitNames = (from x in Funs.DB.Base_Unit
                                           join y in Funs.DB.Project_ProjectUnit
                                           on x.UnitId equals y.UnitId
                                           where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                           select x.UnitName).Distinct().ToList();
            foreach (var item in responsibilityUnitNames)
            {
                DataRow rowTime = table.NewRow();
                rowTime["Name"] = item;
                rowTime["Total"] = hazardRegisters.Where(x => x.ResponsibilityUnitName == item).Count();
                int i = 0;
                foreach (var hazardRegisterType in hazardRegisterTypes)
                {
                    rowTime[hazardRegisterType.TypeCode] = hazardRegisters.Where(x => x.ResponsibilityUnitName == item && x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                    numList[i] += hazardRegisters.Where(x => x.ResponsibilityUnitName == item && x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                    i++;
                }
                table.Rows.Add(rowTime);
            }
            DataRow totalRow = table.NewRow();
            totalRow["Name"] = "合计";
            totalRow["Total"] = hazardRegisters.Count();
            int a = 0;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                totalRow[hazardRegisterType.TypeCode] = numList[a];
                a++;
            }
            table.Rows.Add(totalRow);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("单位违章统计明细表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “UnitViolationStatistics.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “UnitViolationStatistics.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
    }
}