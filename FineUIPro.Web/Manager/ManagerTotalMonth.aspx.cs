using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalMonth : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("ManagerTotalMonthEdit.aspx?ManagerTotalMonthId=" + string.Empty) + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, false);
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.drpProject.SelectedValue = this.CurrUser.LoginProjectId;
                    this.drpProject.Enabled = false;
                }
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.drpProject.SelectedValue = Request.Params["projectId"];
                    this.drpProject.Enabled = false;
                }

                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据GV

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (this.drpProject.Items.Count() > 0)
            {
                string strSql = "SELECT ManagerTotalMonth.ManagerTotalMonthId, ManagerTotalMonth.ProjectId,ManagerTotalMonth.Title,ManagerTotalMonth.CompileDate,ManagerTotalMonth.States,ManagerTotalMonth.AttachUrl,CodeRecords.Code,Users.UserName AS CompileManName,"
                            + @" (CASE WHEN ManagerTotalMonth.States = " + BLL.Const.State_0 + " OR ManagerTotalMonth.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ManagerTotalMonth.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName "
                            + @" FROM Manager_ManagerTotalMonth AS ManagerTotalMonth "
                            + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ManagerTotalMonth.ManagerTotalMonthId=CodeRecords.DataId  "
                            + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ManagerTotalMonth.ManagerTotalMonthId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1 "
                            + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                            + @" LEFT JOIN Sys_User AS Users ON ManagerTotalMonth.CompileMan=Users.UserId WHERE 1=1 ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ManagerTotalMonth.ProjectId = @ProjectId";
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
                {
                    listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                }
                else
                {
                    listStr.Add(new SqlParameter("@ProjectId", this.drpProject.SelectedValue));
                }

                if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                {
                    strSql += " AND ManagerTotalMonth.Title LIKE @Title";
                    listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {            
            BindGrid();
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

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var ManagerTotalMonth = BLL.ManagerTotalMonthService.GetManagerTotalMonthById(id);
            if (ManagerTotalMonth != null)
            { 
                if (this.btnMenuEdit.Hidden || ManagerTotalMonth.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthView.aspx?ManagerTotalMonthId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerTotalMonthEdit.aspx?ManagerTotalMonthId={0}", id, "编辑 - ")));                    
                }
            } 
        }
        #endregion 

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        var getV = BLL.ManagerTotalMonthService.GetManagerTotalMonthById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, null, rowID, BLL.Const.ProjectManagerTotalMonthMenuId, BLL.Const.BtnDelete);
                            BLL.ManagerTotalMonthService.deleteManagerTotalMonthById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;

            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            string menuId = BLL.Const.ServerManagerTotalMonthMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectManagerTotalMonthMenuId;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全月总结" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
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