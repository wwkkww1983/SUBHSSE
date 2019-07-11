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
    public partial class ManagerQuarterly : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("ManagerQuarterlyEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT ManagerQuarterly.ManagerQuarterlyId,ManagerQuarterly.ProjectId,CodeRecords.Code AS ManagerQuarterlyCode,ManagerQuarterly.ManagerQuarterlyName,ManagerQuarterly.FileContent,ManagerQuarterly.CompileMan,ManagerQuarterly.CompileDate,ManagerQuarterly.States, "
                        + @" (CASE WHEN ManagerQuarterly.States = " + BLL.Const.State_0 + " OR ManagerQuarterly.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ManagerQuarterly.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                        + @" FROM Manager_ManagerQuarterly AS  ManagerQuarterly "
                        + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ManagerQuarterly.ManagerQuarterlyId=CodeRecords.DataId "
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ManagerQuarterly.ManagerQuarterlyId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                        + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                        + @" LEFT JOIN Sys_User AS Users ON ManagerQuarterly.CompileMan=Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ManagerQuarterly.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ManagerQuarterly.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtManagerQuarterlyCode.Text.Trim()))
            {
                strSql += " AND ManagerQuarterlyCode LIKE @ManagerQuarterlyCode";
                listStr.Add(new SqlParameter("@ManagerQuarterlyCode", "%" + this.txtManagerQuarterlyCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtManagerQuarterlyName.Text.Trim()))
            {
                strSql += " AND ManagerQuarterlyName LIKE @ManagerQuarterlyName";
                listStr.Add(new SqlParameter("@ManagerQuarterlyName", "%" + this.txtManagerQuarterlyName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
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
            var managerQuarterly = BLL.ManagerQuarterlyService.GetManagerQuarterlyById(id);
            if (managerQuarterly != null)
            {
                if (this.btnMenuEdit.Hidden || managerQuarterly.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerQuarterlyView.aspx?ManagerQuarterlyId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManagerQuarterlyEdit.aspx?ManagerQuarterlyId={0}", id, "编辑 - ")));
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
                        var getD = BLL.ManagerQuarterlyService.GetManagerQuarterlyById(rowID);
                        if (getD != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getD.ManagerQuarterlyCode, getD.ManagerQuarterlyId, BLL.Const.ProjectManagerQuarterlyMenuId, BLL.Const.BtnDelete);
                            BLL.ManagerQuarterlyService.DeleteManagerQuarterlyById(rowID);
                        }
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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

        #region 格式化字符串
        /// <summary>
        /// 获取整理人姓名
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        protected string ConvertCompileMan(object userId)
        {
            string userName = string.Empty;
            if (userId != null)
            {
                var user = BLL.UserService.GetUserByUserId(userId.ToString());
                if (user != null)
                {
                    userName = user.UserName;
                }
            }
            return userName;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectManagerQuarterlyMenuId);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理季报" + filename, System.Text.Encoding.UTF8) + ".xls");
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
                    if (column.ColumnID == "tfCompileMan")
                    {
                        html = (row.FindControl("lblCompileMan") as AspNet.Label).Text;
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