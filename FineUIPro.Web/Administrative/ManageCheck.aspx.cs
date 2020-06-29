using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using System.Data;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class ManageCheck : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("ManageCheckEdit.aspx") + "return false;";
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
            string strSql = "SELECT ManageCheck.ManageCheckId,"
                + @"CodeRecords.Code AS ManageCheckCode,"
                + @"ManageCheck.ProjectId,"
                + @"ManageCheck.CheckTypeCode,"
                + @"ManageCheck.SupplyCheck,"
                + @"ManageCheck.IsSupplyCheck,"
                + @"ManageCheck.ViolationRule,"
                + @"ManageCheck.ToViolationHandleCode,"
                + @"ManageCheck.CheckPerson,"
                + @"ManageCheck.CheckTime,"
                + @"ManageCheck.VerifyPerson,"
                + @"ManageCheck.VerifyTime,"
                + @"ManageCheck.States, "
                + @"ManageCheck.CompileMan, "
                + @"ManageCheck.CompileDate, "
                + @"CheckTypeSet.CheckTypeContent, "
                + @"(CASE WHEN ManageCheck.States = " + BLL.Const.State_0 + " OR ManageCheck.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ManageCheck.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                + @" FROM Administrative_ManageCheck AS  ManageCheck "
                + @" LEFT JOIN Sys_User AS Users ON Users.UserId = ManageCheck.CompileMan "
                + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ManageCheck.ManageCheckId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ManageCheck.ManageCheckId = CodeRecords.DataId "
                + @" LEFT JOIN Administrative_CheckTypeSet AS CheckTypeSet ON CheckTypeSet.CheckTypeCode = ManageCheck.CheckTypeCode WHERE 1=1 ";
            
            List<SqlParameter> listStr = new List<SqlParameter>();            
            strSql += " AND ManageCheck.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ManageCheck.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND ManageCheck.CheckTime >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND ManageCheck.CheckTime <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtStartDate.Text.Trim());
                DateTime endTime = Convert.ToDateTime(this.txtEndDate.Text.Trim());
                if (startTime > endTime)
                {
                    Alert.ShowInTop("开始时间不能大于结束时间！", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ManageCheckMenuId, Const.BtnQuery);
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

            var manageCheck = BLL.ManageCheckService.GetManageCheckById(id);
            if (manageCheck != null)
            {
                if (this.btnMenuEdit.Hidden || manageCheck.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManageCheckView.aspx?ManageCheckId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManageCheckEdit.aspx?ManageCheckId={0}", id, "编辑 - ")));
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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getManageCheck = BLL.ManageCheckService.GetManageCheckById(rowID);
                    if (getManageCheck != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getManageCheck.ManageCheckCode, getManageCheck.ManageCheckId, BLL.Const.ManageCheckMenuId, Const.BtnDelete);                        
                        BLL.ManageCheckService.DeleteManageCheckById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 根据行政管理检查项目Id查询问题数量
        /// </summary>
        /// <param name="AllowType">行政管理检查项目Id</param>
        /// <returns></returns>
        protected string GetManageCheckItemCountByManageCheckId(object manageCheckId)
        {
            if (manageCheckId == null)
            {
                return "0";
            }
            else
            {
                return BLL.ManageCheckItemService.GetManageCheckItemCountByManageCheckId(manageCheckId.ToString()).ToString();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageCheckMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("行政管理检查记录" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.ManageCheckMenuId, Const.BtnOut);
        }

#pragma warning disable CS0108 // “ManageCheck.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ManageCheck.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfC")
                    {
                        html = (row.FindControl("lblC") as AspNet.Label).Text;
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