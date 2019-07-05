using System;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class SeeTest : PageBase
    {
        private string rootPath = "~/FileUpload/Image/OnlineCheck/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.WorkPostService.InitWorkPostDropDownList(this.ddlWorkPost, true);
                Funs.FineUIPleaseSelect(ddlABVolume, "-请选择AB卷-");

                imgTestContent.ImageUrl = Funs.RootPath + "Images\\Null.jpg";

                // 绑定表格
                BindGrid();
            }
        }

        private void BindGrid()
        {
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@WorkPostId",this.ddlWorkPost.SelectedValue),
                        new SqlParameter("@ABVolume",this.ddlABVolume.SelectedValue),
                    };
            DataTable tb = SQLHelper.GetDataTableRunProc("sp_GetTest", parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }

        protected void btnSee_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            string itemType = Grid1.Rows[e.RowIndex].Values[4].ToString();
            string testCode = Grid1.Rows[e.RowIndex].Values[0].ToString();
            string path = Grid1.DataKeys[e.RowIndex][1].ToString();
            GroupPanel1.Title = itemType + "(" + testCode + ")";
            imgTestContent.ImageUrl = rootPath + path;
        }
    }
}