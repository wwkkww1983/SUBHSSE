using System;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class ExamineeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strSql = "select *  from dbo.Edu_Online_ExamineeDetail where ExamineeId=@ExamineeId order by TestCode";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@ExamineeId",Request.QueryString["examineeId"]),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.DataSource = tb;
            Grid1.DataBind();
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            string answerKey = row["AnswerKey"].ToString();
            string testKey = row["TestKey"].ToString();
            
            if (answerKey != testKey)
            {
                e.RowCssClass = "special";
            }
        }

    }
}