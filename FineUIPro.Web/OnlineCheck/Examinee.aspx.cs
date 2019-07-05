using System;
using System.Data;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class Examinee : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                BLL.UserService.InitUserDropDownList(this.drpUser, this.CurrUser.LoginProjectId, true);
                BLL.WorkPostService.InitWorkPostDropDownList(this.ddlWorkPost, true);
                Funs.FineUIPleaseSelect(ddlABVolume, "-请选择AB卷-");

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        #region BindGrid

        private void BindGrid()
        {
            string sql = "select e.*,CONVERT(varchar(100), (e.EndTime-e.StartTime), 8) as UserTime,u.Account,u.UserName,w.WorkPostName from dbo.Edu_Online_Examinee e left join dbo.Sys_User u on e.UserId=u.UserId left join dbo.Base_WorkPost w on w.WorkPostId=e.WorkPostId";
            DataTable tb = SQLHelper.GetDataTableRunText(sql, null);
            Grid1.RecordCount = tb.Rows.Count;
            // 2.获取当前分页数据
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        #endregion

        // 删除数据
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.ExamineeService.DeleteExaminee(hfFormID.Text);
            // 重新绑定表格，并模拟点击[新增按钮]
            BindGrid();
            PageContext.RegisterStartupScript("onNewButtonClick();");
        }

        // 保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Edu_Online_Examinee examinee = new Model.Edu_Online_Examinee
            {
                UserId = this.drpUser.SelectedValue,
                WorkPostId = this.ddlWorkPost.SelectedValue,
                ABVolume = this.ddlABVolume.SelectedValue
            };

            if (String.IsNullOrEmpty(strRowID))
            {
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Edu_Online_Examinee));
                examinee.ExamineeId = newKeyID;
                BLL.ExamineeService.AddExaminee(examinee);
            }
            else
            {
                var q = BLL.ExamineeService.GetExamineeById(strRowID);
                if (q.IsChecked == true)
                {
                    ShowNotify("已考完，不能修改！");
                    return;
                }
                else
                {
                    examinee.ExamineeId = strRowID;
                    BLL.ExamineeService.UpdateExaminee(examinee);
                }
            }

            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, examinee.ExamineeId));
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }

            string examineeId = Grid1.SelectedRowID;
            var q=BLL.ExamineeService.GetExamineeById(examineeId);

            if (q != null && q.IsChecked == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ExamineeDetail.aspx?examineeId={0}", examineeId, "查看考试明细")));
            }
            else
            {
                Alert.ShowInTop("还未进行考试！");
                return;
            }
        }
    }
}