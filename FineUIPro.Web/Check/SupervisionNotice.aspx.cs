using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BLL;
using System.IO;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class SupervisionNotice : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("SupervisionNoticeEdit.aspx") + "return false;";

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT SupervisionNotice.SupervisionNoticeId,"
                          + @"SupervisionNotice.ProjectId,"
                          + @"CodeRecords.Code AS SupervisionNoticeCode,"
                          + @"SupervisionNotice.UnitId,"
                          + @"SupervisionNotice.WorkAreaId,"
                          + @"SupervisionNotice.CheckedDate,"
                          + @"SupervisionNotice.WrongContent,"
                          + @"SupervisionNotice.SignPerson,"
                          + @"SupervisionNotice.SignDate,"
                          + @"SupervisionNotice.CompleteStatus,"
                          + @"SupervisionNotice.DutyPerson,"
                          + @"SupervisionNotice.CompleteDate,"
                          + @"SupervisionNotice.IsRectify,"
                          + @"SupervisionNotice.CheckPerson,"
                          + @"SupervisionNotice.AttachUrl,"
                          + @"Unit.UnitName,"
                          + @"WorkArea.WorkAreaName,"
                          + @"Person.UserName AS CheckPersonName"
                          + @" FROM Check_SupervisionNotice AS SupervisionNotice "
                          + @" LEFT JOIN Base_Project AS Project ON Project.ProjectId = SupervisionNotice.ProjectId "
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = SupervisionNotice.UnitId "
                          + @" LEFT JOIN ProjectData_WorkArea AS WorkArea ON WorkArea.WorkAreaId = SupervisionNotice.WorkAreaId "
                          + @" LEFT JOIN Sys_User AS Users ON SupervisionNotice.SignPerson = Users.UserId "
                          + @" LEFT JOIN Sys_User AS Person ON Person.UserId = SupervisionNotice.CheckPerson "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON SupervisionNotice.SupervisionNoticeId = CodeRecords.DataId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND SupervisionNotice.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            /// 施工分包 只看到自己单位
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND SupervisionNotice.UnitId = @UnitId";  ///责任单位
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }

            if (!string.IsNullOrEmpty(this.txtSupervisionNoticeCode.Text.Trim()))
            {
                strSql += " AND SupervisionNoticeCode LIKE @SupervisionNoticeCode";
                listStr.Add(new SqlParameter("@SupervisionNoticeCode", "%" + this.txtSupervisionNoticeCode.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
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

        #region 过滤表头、排序、分页、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
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
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string SupervisionNoticeId = Grid1.SelectedRowID;
            var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(SupervisionNoticeId);
            if (SupervisionNotice != null)
            {
                if (!string.IsNullOrEmpty(SupervisionNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeView.aspx?SupervisionNoticeId={0}", SupervisionNoticeId, "查看 - ")));
                }
                else
                {
                    if (this.btnAuditing.Hidden == true)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeEdit.aspx?SupervisionNoticeId={0}", SupervisionNoticeId, "编辑 - ")));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeEdit.aspx?states=check&SupervisionNoticeId={0}", SupervisionNoticeId, "审核 - ")));
                    }
                }
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string SupervisionNoticeId = Grid1.SelectedRowID;
            var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(SupervisionNoticeId);
            if (SupervisionNotice != null)
            {
                if (!string.IsNullOrEmpty(SupervisionNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeView.aspx?SupervisionNoticeId={0}", SupervisionNoticeId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeEdit.aspx?SupervisionNoticeId={0}", SupervisionNoticeId, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAuditing_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string SupervisionNoticeId = Grid1.SelectedRowID;
            var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(SupervisionNoticeId);
            if (SupervisionNotice != null)
            {
                if (!string.IsNullOrEmpty(SupervisionNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeView.aspx?SupervisionNoticeId={0}", SupervisionNoticeId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SupervisionNoticeEdit.aspx?states=check&SupervisionNoticeId={0}", SupervisionNoticeId, "审核 - ")));
                }
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(rowID);
                    if (SupervisionNotice != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, SupervisionNotice.SupervisionNoticeCode, SupervisionNotice.SupervisionNoticeId, BLL.Const.ProjectSupervisionNoticeMenuId, BLL.Const.BtnDelete);
                        BLL.SupervisionNoticeService.DeleteSupervisionNoticeById(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）");
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectSupervisionNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnAuditing.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("监理整改通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “SupervisionNotice.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “SupervisionNotice.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    //if (column.ColumnID == "CheckArea")
                    //{
                    //    html = (row.FindControl("lblCheckArea") as AspNet.Label).Text;
                    //}
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