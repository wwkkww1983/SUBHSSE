using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FineUIPro.Web.Exchange
{
    public partial class Register : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 表头过滤
                //FilterDataRowItem = FilterDataRowItemImplement;
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();               
                BindGrid1();
            }
        }

        /// <summary>
        /// 加载Grid1
        /// </summary>
        private void BindGrid1()
        {
            string strSql = "select UserId, UserCode,UserName,IsPostName,RoleName,UnitName,RoleTypeName,IsPosts,IsReplies,IsDeletePosts from dbo.View_Sys_User " +
                               @" where  UserId != @UserId order by RoleTypeName, UserCode";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@UserId",BLL.Const.sysglyId),
                    };

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
       
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        /// <summary>
        /// Grid行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (GetButtonPower(BLL.Const.BtnModify))
            {
                Model.Sys_User u = BLL.UserService.GetUserByUserId(rowID);
                if (u != null)
                {
                    if (e.CommandName == "IsPosts")
                    {

                        CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsPosts");
                        u.IsPosts = checkField.GetCheckedState(e.RowIndex);
                        BLL.UserService.UpdateUser(u);
                        BindGrid1();
                        BLL.LogService.AddSys_Log(this.CurrUser, "用户发帖授权", null, BLL.Const.RegisterMenuId, BLL.Const.BtnModify);
                    }
                    if (e.CommandName == "IsReplies")
                    {

                        CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsReplies");
                        u.IsReplies = checkField.GetCheckedState(e.RowIndex);
                        BLL.UserService.UpdateUser(u);
                        BindGrid1();
                        BLL.LogService.AddSys_Log(this.CurrUser, "用户回帖授权", null, BLL.Const.RegisterMenuId, BLL.Const.BtnModify);
                    }
                    if (e.CommandName == "IsDeletePosts")
                    {

                        CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsDeletePosts");
                        u.IsDeletePosts = checkField.GetCheckedState(e.RowIndex);
                        BLL.UserService.UpdateUser(u);
                        BindGrid1();
                        BLL.LogService.AddSys_Log(this.CurrUser, "用户删帖授权", null, BLL.Const.RegisterMenuId, BLL.Const.BtnModify);
                    }
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid1();
        }

        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RegisterMenuId, button);
        }
    }
}