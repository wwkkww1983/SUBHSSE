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
    public partial class ViolationClassificationComparison : PageBase
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
            bf.HeaderText = "项目";
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
                this.drpUnit.DataTextField = "UnitName";
                this.drpUnit.DataValueField = "UnitId";
                this.drpUnit.DataSource = (from x in Funs.DB.Base_Unit
                                           join y in Funs.DB.Project_ProjectUnit
                                           on x.UnitId equals y.UnitId
                                           where y.ProjectId == this.CurrUser.LoginProjectId && y.UnitType == BLL.Const.ProjectUnitType_2
                                           orderby x.UnitCode
                                           select x).ToList();
                this.drpUnit.DataBind();
                Funs.FineUIPleaseSelect(this.drpUnit);
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
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                hazardRegisters = hazardRegisters.Where(x => x.ResponsibleUnit == this.drpUnit.SelectedValue);
            }
            var oldHazardRegisters = hazardRegisters;
            var newHazardRegisters = hazardRegisters;
            oldHazardRegisters = oldHazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtOldStartDate.Text.Trim()));
            oldHazardRegisters = oldHazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtOldEndDate.Text.Trim()).AddDays(1));
            newHazardRegisters = newHazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtNewStartDate.Text.Trim()));
            newHazardRegisters = newHazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtNewEndDate.Text.Trim()).AddDays(1));

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Total", typeof(string));

            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                table.Columns.Add(new DataColumn(hazardRegisterType.TypeCode, typeof(String)));
            }
            DataRow rowTime = table.NewRow();
            DataRow rowTime2 = table.NewRow();
            rowTime["Name"] = "上期违章数";
            rowTime["Total"] = oldHazardRegisters.Count();
            rowTime2["Name"] = "本期违章数";
            rowTime2["Total"] = newHazardRegisters.Count();
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                rowTime[hazardRegisterType.TypeCode] = oldHazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                rowTime2[hazardRegisterType.TypeCode] = newHazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
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
            if (string.IsNullOrEmpty(this.txtOldStartDate.Text.Trim()) || string.IsNullOrEmpty(this.txtOldEndDate.Text.Trim()) || string.IsNullOrEmpty(this.txtNewStartDate.Text.Trim()) || string.IsNullOrEmpty(this.txtNewEndDate.Text.Trim()))
            {
                Alert.ShowInTop("上期及本期开始结束时间不能为空！", MessageBoxIcon.Warning);
                return;
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("违章分类比较图" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ViolationClassificationComparison.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ViolationClassificationComparison.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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

        #region 图形
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
        #endregion

        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            var hazardRegisters = (from x in Funs.DB.View_Hazard_HazardRegister
                                   where x.ProjectId == this.CurrUser.LoginProjectId
                                   && x.States != "4" && x.ProblemTypes == "1"
                                   select x);
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                hazardRegisters = hazardRegisters.Where(x => x.ResponsibleUnit == this.drpUnit.SelectedValue);
            }
            var oldHazardRegisters = hazardRegisters;
            var newHazardRegisters = hazardRegisters;
            oldHazardRegisters = oldHazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtOldStartDate.Text.Trim()));
            oldHazardRegisters = oldHazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtOldEndDate.Text.Trim()).AddDays(1));
            newHazardRegisters = newHazardRegisters.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtNewStartDate.Text.Trim()));
            newHazardRegisters = newHazardRegisters.Where(x => x.CheckTime <= Convert.ToDateTime(this.txtNewEndDate.Text.Trim()).AddDays(1));

            DataTable table = new DataTable();
            table.Columns.Add("类型", typeof(string));
            table.Columns.Add("上期违章", typeof(string));
            table.Columns.Add("本期违章", typeof(string));
            var hazardRegisterTypes = from x in Funs.DB.HSSE_Hazard_HazardRegisterTypes where x.HazardRegisterType == "1" orderby x.TypeCode select x;
            foreach (var hazardRegisterType in hazardRegisterTypes)
            {
                DataRow rowTime = table.NewRow();
                rowTime["类型"] = hazardRegisterType.RegisterTypesName;
                rowTime["上期违章"] = oldHazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                rowTime["本期违章"] = newHazardRegisters.Where(x => x.RegisterTypesId == hazardRegisterType.RegisterTypesId).Count();
                table.Rows.Add(rowTime);
            }
            this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(table, "违章分类比较图", this.drpChartType.SelectedValue, 1130, 450, this.ckbShow.Checked));

        }
    }
}