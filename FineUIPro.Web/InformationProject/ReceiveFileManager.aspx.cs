using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.InformationProject
{
    public partial class ReceiveFileManager : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("ReceiveFileManagerEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();                
            }
        }
        #endregion

        #region GV绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ReceiveFileManager.ReceiveFileManagerId,ReceiveFileManager.ProjectId,CodeRecords.Code AS ReceiveFileCode,ReceiveFileManager.ReceiveFileName,ReceiveFileManager.Version,Unit.UnitName,ReceiveFileManager.FileCode,ReceiveFileManager.FilePageNum,ReceiveFileManager.GetFileDate,ReceiveFileManager.States "                          
                          + @" ,(CASE WHEN ReceiveFileManager.States = " + BLL.Const.State_0 + " OR ReceiveFileManager.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN ReceiveFileManager.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" ,(CASE WHEN ReceiveFileManager.States != 2 THEN '未审核完成' WHEN (SELECT COUNT(*) FROM AttachFile WHERE ToKeyId =ReceiveFileManager.ReceiveFileManagerId) > 0 THEN '已闭环' ELSE '未回执' END) AS  RetrunSateName"
                          + @" FROM InformationProject_ReceiveFileManager AS ReceiveFileManager  "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON ReceiveFileManager.ReceiveFileManagerId=CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON ReceiveFileManager.ReceiveFileManagerId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Base_Unit AS Unit ON ReceiveFileManager.FileUnitId=Unit.UnitId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ReceiveFileManager.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND ReceiveFileManager.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtReceiveFileCode.Text.Trim()))
            {
                strSql += " AND ReceiveFileCode LIKE @ReceiveFileCode";
                listStr.Add(new SqlParameter("@ReceiveFileCode", "%" + this.txtReceiveFileCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtReceiveFileName.Text.Trim()))
            {
                strSql += " AND ReceiveFileManager.ReceiveFileName LIKE @ReceiveFileName";
                listStr.Add(new SqlParameter("@ReceiveFileName", "%" + this.txtReceiveFileName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
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
            var InformationProject = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(id);
            if (InformationProject != null)
            {
                if (this.btnMenuEdit.Hidden || InformationProject.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}", id, "查看 - ")));                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReceiveFileManagerEdit.aspx?ReceiveFileManagerId={0}", id, "编辑 - ")));
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
                    var ReceiveFileManager = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(rowID);
                    if (ReceiveFileManager != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, ReceiveFileManager.ReceiveFileCode, ReceiveFileManager.ReceiveFileManagerId, BLL.Const.ReceiveFileManagerMenuId, BLL.Const.BtnDelete);
                        BLL.ReceiveFileManagerService.DeleteReceiveFileManagerById(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ReceiveFileManagerMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("一般来文管理" + filename, System.Text.Encoding.UTF8) + ".xls");
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

        #region 关闭弹出框
        protected void WindowAtt_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}