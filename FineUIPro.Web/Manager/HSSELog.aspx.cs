using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class HSSELog : PageBase
    {
        #region 自定义项
        /// <summary>
        /// 项目主键
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("HSSELogEdit.aspx") + "return false;";
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
            string strSql = @"SELECT HSSELogId,ProjectId,CompileDate,CompileMan,Weather,IsVisible,Users.UserName AS CompileManName,SysConst.ConstText AS WeatherName "
                          + @" FROM Manager_HSSELog AS HSSELog "
                          + @" LEFT JOIN Sys_User AS Users ON HSSELog.CompileMan=Users.UserId "
                          + @" LEFT JOIN Sys_Const AS SysConst ON SysConst.GroupId='" + BLL.ConstValue.Group_Weather + "' AND SysConst.ConstValue=HSSELog.Weather"
                          + @" WHERE IsVisible != 0 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND HSSELog.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtCompileMan.Text.Trim()))
            {
                strSql += " AND Users.UserName LIKE @CompileManName";
                listStr.Add(new SqlParameter("@CompileManName", "%" + this.txtCompileMan.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                strSql += " AND CompileDate = @CompileDate";
                listStr.Add(new SqlParameter("@CompileDate", this.txtCompileDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
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
            var getHSSELog = BLL.HSSELogService.GetHSSELogByHSSELogId(id);
            if (getHSSELog != null)
            {
                if (this.btnMenuEdit.Hidden || (getHSSELog.CompileMan != this.CurrUser.UserId && this.CurrUser.UserId != BLL.Const.sysglyId))   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者不是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSELogView.aspx?HSSELogId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSELogEdit.aspx?HSSELogId={0}", id, "编辑 - ")));
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
                    var hsseLog = BLL.HSSELogService.GetHSSELogByHSSELogId(rowID);
                    if (hsseLog != null)
                    {
                        hsseLog.IsVisible = false;
                        BLL.LogService.AddSys_Log(this.CurrUser, null, hsseLog.HSSELogId, BLL.Const.ProjectHSSELogMenuId, BLL.Const.BtnDelete);
                        BLL.HSSELogService.UpdateHSSELog(hsseLog);
                    }                                    
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectHSSELogMenuId);
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
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("HSELogStatistics.aspx", "导出 - ")));
            //Response.ClearContent();
            //string filename = Funs.GetNewFileName();
            //Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("HSSE日志暨管理数据收集" + filename, System.Text.Encoding.UTF8) + ".xls");
            //Response.ContentType = "application/excel";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //this.Grid1.PageSize = 500;
            //this.BindGrid();
            //Response.Write(GetGridTableHtml(Grid1));
            //Response.End();
        }

        ///// <summary>
        ///// 导出方法
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <returns></returns>
        //private string GetGridTableHtml(Grid grid)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
        //    sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
        //    sb.Append("<tr>");
        //    foreach (GridColumn column in grid.Columns)
        //    {
        //        sb.AppendFormat("<td>{0}</td>", column.HeaderText);
        //    }
        //    sb.Append("</tr>");
        //    foreach (GridRow row in grid.Rows)
        //    {
        //        sb.Append("<tr>");
        //        foreach (GridColumn column in grid.Columns)
        //        {
        //            string html = row.Values[column.ColumnIndex].ToString();
        //            if (column.ColumnID == "tfNumber")
        //            {
        //                html = (row.FindControl("lblNumber") as AspNet.Label).Text;
        //            }
        //            sb.AppendFormat("<td>{0}</td>", html);
        //        }

        //        sb.Append("</tr>");
        //    }

        //    sb.Append("</table>");

        //    return sb.ToString();
        //}
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Grid1.SelectedRowID))
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.HSSELogReportId, Grid1.SelectedRowID, "", "打印 - ")));
            }
        }
        #endregion
    }
}