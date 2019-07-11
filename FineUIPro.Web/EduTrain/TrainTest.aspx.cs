using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainTest : PageBase
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            string strSql = @"SELECT TrainTestId,TrainingId,QsnCode,COrder, QsnContent, QsnAnswer, QsnCategory,QsnKind,QsnImportant,"
                        + @"(CASE QsnCategory WHEN '2' THEN '多媒体题' WHEN '3' THEN '图片题' ELSE '文字题' END) AS QsnCategoryName,"
                        + @" (CASE QsnKind WHEN '1' THEN '单选' WHEN '2' THEN '多选' WHEN '3' THEN '判断' ELSE '' END) AS QsnKindName, "
                        + @"(CASE QsnImportant WHEN '0' THEN '容易' WHEN '1' THEN '一般' WHEN '2' THEN '困难' ELSE '' END) AS QsnImportantName,Analysis"
                        + @" FROM EduTrain_TrainTest WHERE TrainingId=@TrainingId ";
            List<SqlParameter> listStr = new List<SqlParameter>
            {
                new SqlParameter("@TrainingId", Request.Params["TrainingId"])
            };
            if (!string.IsNullOrEmpty(this.txtQsnContent.Text.Trim()))
            {
                strSql += " AND QsnContent LIKE @QsnContent";
                listStr.Add(new SqlParameter("@QsnContent", "%" + this.txtQsnContent.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion        

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}