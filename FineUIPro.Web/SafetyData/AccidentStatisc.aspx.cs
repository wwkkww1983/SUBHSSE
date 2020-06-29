using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SafetyData
{
    public partial class AccidentStatisc : PageBase
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
                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, true);
                drpAccidentTypeId.DataValueField = "ConstValue";
                drpAccidentTypeId.DataTextField = "ConstText";
                List<Model.Sys_Const> list = new List<Model.Sys_Const>();
                list.AddRange(BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentReportRegistration));
                list.AddRange(BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_AccidentInvestigationProcessingReport));
                drpAccidentTypeId.DataSource = list;
                drpAccidentTypeId.DataBind();
                Funs.FineUIPleaseSelect(drpAccidentTypeId);
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
            string strSql = "select * from View_AccidentStatisc WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtDate.Text.Trim()))
            {
                strSql += " AND AccidentDate = @AccidentDate";
                listStr.Add(new SqlParameter("@AccidentDate", this.txtDate.Text.Trim()));
            }
            if (this.drpProject.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
            }
            if (this.drpAccidentTypeId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND AccidentTypeName = @AccidentTypeName";
                listStr.Add(new SqlParameter("@AccidentTypeName", this.drpAccidentTypeId.SelectedItem.Text));
            }
            strSql += " order by AccidentDate desc";
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 转换提要
        /// </summary>
        /// <param name="Abstract"></param>
        /// <returns></returns>
        protected string ConvertAbstract(object Abstract)
        {
            if (Abstract != null)
            {
                return Funs.GetSubStr(Abstract, 20);
            }
            return "";
        }

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string id = Grid1.SelectedRowID;
            Model.Accident_AccidentReport accidentReport = BLL.AccidentReport2Service.GetAccidentReportById(id);
            Model.Accident_AccidentReportOther otherAccidentReport = BLL.AccidentReportOtherService.GetAccidentReportOtherById(id);
            if (accidentReport != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Accident/AccidentReportView.aspx?AccidentReportId={0}", id, "查看 - ")));
            }
            else if (otherAccidentReport != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Accident/AccidentReportOtherView.aspx?AccidentReportOtherId={0}", id, "查看 - ")));
            }
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理台账" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “AccidentStatisc.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “AccidentStatisc.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "Abstract")
                    {
                        html = (row.FindControl("lblAbstract") as AspNet.Label).ToolTip;
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