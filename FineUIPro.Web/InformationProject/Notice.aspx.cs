using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.InformationProject
{
    public partial class Notice : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("NoticeEdit.aspx") + "return false;";
                this.SddlPageSize.SelectedValue = SGrid.PageSize.ToString();
                this.AddlPageSize.SelectedValue = AGrid.PageSize.ToString();
                // 绑定表格
                this.SBindGrid();
                // 绑定表格
                this.ABindGrid();  
            }
        }
        #endregion

        #region 绑定数据SBindGrid
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void SBindGrid()
        {
            string strSql = @"SELECT Notice.NoticeId AS SNoticeId,(CASE WHEN Notice.NoticeCode IS NULL THEN  CodeRecords.Code ELSE Notice.NoticeCode END) AS NoticeCode,Notice.NoticeTitle,Notice.MainContent,Notice.CompileDate,Notice.CompileMan,Users.UserName AS CompileManName,Notice.CompileDate,Notice.States "
                          + @" ,Notice.IsRelease,(CASE WHEN IsRelease=1 THEN '已发布' ELSE '未发布' END) AS IsReleaseName,Notice.ReleaseDate,AccessProjectText"
                          + @" ,Notice.ProjectId,(CASE WHEN Notice.ProjectId IS NULL THEN '公司本部' ELSE Project.ProjectName END ) AS ProjectName"
                          + @" ,(CASE WHEN LEN(Notice.AccessProjectText) > 40 THEN SUBSTRING(Notice.AccessProjectText,0,40)+'...' ELSE  AccessProjectText END)  AS SortAccessProjectText"
                          + @" ,(CASE WHEN Notice.States = " + BLL.Const.State_0 + " OR Notice.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN Notice.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  StateName"
                          + @" FROM InformationProject_Notice AS Notice "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Notice.NoticeId=CodeRecords.DataId "
                          + @" LEFT JOIN Base_Project AS Project ON Notice.ProjectId=Project.ProjectId"
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON Notice.NoticeId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Sys_User AS Users ON Notice.CompileMan=Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                strSql += " AND Notice.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND Notice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND Notice.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            else
            {                
                strSql += " AND Notice.ProjectId IS NULL";
            }
           
            if (!string.IsNullOrEmpty(this.txtNoticeCode.Text.Trim()))
            {
                strSql += " AND NoticeCode LIKE @NoticeCode";
                listStr.Add(new SqlParameter("@NoticeCode", "%" + this.txtNoticeCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtNoticeTitle.Text.Trim()))
            {
                strSql += " AND Notice.NoticeTitle LIKE @NoticeTitle";
                listStr.Add(new SqlParameter("@NoticeTitle", "%" + this.txtNoticeTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            SGrid.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(SGrid.FilteredData, tb);
            var table = this.GetPagedDataTable(SGrid, tb);
            SGrid.DataSource = table;
            SGrid.DataBind();
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_PageIndexChange(object sender, GridPageEventArgs e)
        {
            SBindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SGrid.PageSize = Convert.ToInt32(this.SddlPageSize.SelectedValue);
            SBindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.SBindGrid();
        }
        #endregion
        #endregion

        #region 绑定数据ABindGrid
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void ABindGrid()
        {
            string strSql = @"SELECT Notice.NoticeId,(CASE WHEN Notice.NoticeCode IS NULL THEN  CodeRecords.Code ELSE Notice.NoticeCode END) AS NoticeCode,Notice.NoticeTitle,Notice.ReleaseDate,AccessProjectText,AccessProjectId"
                          + @" ,Notice.ProjectId,(CASE WHEN Notice.ProjectId IS NULL THEN '公司本部' ELSE Project.ProjectName END ) AS ProjectName"
                          + @" ,(CASE WHEN LEN(Notice.AccessProjectText) > 40 THEN SUBSTRING(Notice.AccessProjectText,0,40)+'...' ELSE  AccessProjectText END)  AS SortAccessProjectText"
                          + @" FROM InformationProject_Notice AS Notice"
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Notice.NoticeId=CodeRecords.DataId "
                          + @" LEFT JOIN Base_Project AS Project ON Notice.ProjectId=Project.ProjectId"
                          + @" WHERE IsRelease = 1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND Notice.AccessProjectId LIKE @ProjectId"; 
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {                
                listStr.Add(new SqlParameter("@ProjectId", "%" + Request.Params["projectId"] + "%"));              
            }
            else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
               
                listStr.Add(new SqlParameter("@ProjectId", "%" + this.CurrUser.LoginProjectId + "%"));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", "%#%"));
            }

            if (!string.IsNullOrEmpty(this.txtNoticeCode.Text.Trim()))
            {
                strSql += " AND NoticeCode LIKE @NoticeCode";
                listStr.Add(new SqlParameter("@NoticeCode", "%" + this.txtNoticeCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtNoticeTitle.Text.Trim()))
            {
                strSql += " AND Notice.NoticeTitle LIKE @NoticeTitle";
                listStr.Add(new SqlParameter("@NoticeTitle", "%" + this.txtNoticeTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            AGrid.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(AGrid.FilteredData, tb);
            var table = this.GetPagedDataTable(AGrid, tb);
            AGrid.DataSource = table;
            AGrid.DataBind();
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AGrid_PageIndexChange(object sender, GridPageEventArgs e)
        {
            ABindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AGrid.PageSize = Convert.ToInt32(this.AddlPageSize.SelectedValue);
            ABindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AGrid_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.ABindGrid();
        }
        #endregion
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void STextBox_TextChanged(object sender, EventArgs e)
        {
            this.SBindGrid();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ATextBox_TextChanged(object sender, EventArgs e)
        {
            this.ABindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SGrid_RowDoubleClick(object sender, GridRowClickEventArgs e)
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
            if (SGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = SGrid.SelectedRowID;
            var Notice = BLL.NoticeService.GetNoticeById(id);
            if (Notice != null)
            {
                if (this.btnMenuEdit.Hidden || Notice.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("NoticeView.aspx?NoticeId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("NoticeEdit.aspx?NoticeId={0}", id, "编辑 - ")));
                }
            }            
        }
       
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (SGrid.SelectedRowIndexArray.Length > 0)
            {
                string showNot = string.Empty;
                foreach (int rowIndex in SGrid.SelectedRowIndexArray)
                {
                    string rowID = SGrid.DataKeys[rowIndex][0].ToString();
                    var notice = BLL.NoticeService.GetNoticeById(rowID);
                    if (notice != null)
                    {
                        if ((notice.IsRelease == false || !notice.IsRelease.HasValue))
                        {
                            BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除通知管理", notice.NoticeCode);
                            BLL.NoticeService.DeleteNoticeById(rowID);
                        }
                        showNot += notice.NoticeCode + ";";
                    }
                }

                this.SBindGrid();
                if (!string.IsNullOrEmpty(showNot))
                {
                    ShowNotify("通知编号：" + showNot + "已经发布，不能删除!", MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除完成!", MessageBoxIcon.Success);
                }
            }
        }
     
        /// <summary>
        /// 右键发布事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuIssuance_Click(object sender, EventArgs e)
        {
            if (SGrid.SelectedRowIndexArray.Length > 0)
            {
                string strShowNotify = string.Empty;
                foreach (int rowIndex in SGrid.SelectedRowIndexArray)
                {
                    string rowID = SGrid.DataKeys[rowIndex][0].ToString();
                    var notice = BLL.NoticeService.GetNoticeById(rowID);
                    if (notice != null)
                    {
                        if (notice.States == BLL.Const.State_2 && (notice.IsRelease == false || !notice.IsRelease.HasValue))
                        {
                            notice.IsRelease = true;
                            notice.ReleaseDate = System.DateTime.Now;
                            BLL.NoticeService.UpdateNotice(notice);
                            BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "发布通知", notice.NoticeCode);
                        }
                        else
                        {
                            strShowNotify += "通知：" + notice.NoticeCode;
                        }
                    }
                }

                this.SBindGrid();
                this.ABindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    ShowNotify(strShowNotify + "审核未完成不能发布！", MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("发布成功!", MessageBoxIcon.Success);
                }
            }
        }
        #endregion

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AGrid_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (AGrid.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

           PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("NoticeView.aspx?NoticeId={0}", AGrid.SelectedRowID, "查看 - ")));
        }

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
            string menuId = BLL.Const.ServerNoticeMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectNoticeMenuId;
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
                if (buttonList.Contains(BLL.Const.BtnIssuance))
                {
                    this.btnMenuIssuance.Hidden = false;
                }
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理通知(发出)" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.SGrid.PageSize = 500;
            this.SBindGrid();
            Response.Write(GetGridTableHtml(SGrid));
            Response.End();
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("管理通知(接收)" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.AGrid.PageSize = 500;
            this.ABindGrid();
            Response.Write(GetGridTableHtml(AGrid));
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
                    if (column.ColumnID == "tfANumber")
                    {
                        html = (row.FindControl("lblANumber") as AspNet.Label).Text;
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