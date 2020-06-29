using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Hazard
{
    public partial class MajorHazardList : PageBase
    {

        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.txtStartD.Text = "160";
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT Item.HazardId,Item.HazardListTypeId,Item.HazardListId,Item.HazardItems,Item.DefectsType,Item.MayLeadAccidents,Item.HelperMethod,Item.HazardJudge_L "
                          + @" ,Item.HazardJudge_E,Item.HazardJudge_C,Item.HazardJudge_D,Item.HazardLevel,Item.ControlMeasures,Item.IsResponse"
                          + @" ,Item.ResponseRecode,Item.PromptTime,Item.Remark,Technique.HazardCode,List.HazardListCode,List.ProjectId,List.WorkStage,(List.HazardListId + Item.HazardId) AS NewId"
                          + @" FROM dbo.Hazard_HazardSelectedItem AS Item"
                          + @" LEFT JOIN DBO.Technique_HazardList AS Technique ON Item.HazardId =Technique.HazardId"
                          + @" LEFT JOIN DBO.Hazard_HazardList AS List ON Item.HazardListId =List.HazardListId "
                          + @" WHERE List.States=" + BLL.Const.State_2;
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND List.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (!string.IsNullOrEmpty(this.txtStartD.Text.Trim()))
            {
                strSql += " AND Item.HazardJudge_D >= @D";
                listStr.Add(new SqlParameter("@D", Funs.GetNewDecimalOrZero(this.txtStartD.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(this.txtHazardLevel.Text.Trim()))
            {
                strSql += " AND Item.HazardLevel = @HazardLevel";
                listStr.Add(new SqlParameter("@HazardLevel", this.txtHazardLevel.Text.Trim()));
            }
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

        #region 转换字符串
        /// <summary>
        /// 转换工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(object workStage)
        {
            if (workStage != null)
            {
                string workStages = string.Empty;
                string[] strList = workStage.ToString().Split(',');
                foreach (string str in strList)
                {
                    Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                    if (c != null)
                    {
                        workStages += c.WorkStageName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(workStages))
                {
                    workStages = workStages.Substring(0, workStages.LastIndexOf(","));
                }
                return workStages;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目现场重大HSE因素控制措施一览表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “MajorHazardList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “MajorHazardList.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfWorkStage")
                    {
                        html = (row.FindControl("lblWorkStage") as AspNet.Label).Text;
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