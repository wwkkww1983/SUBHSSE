using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class RectifyNotice : PageBase
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
                btnNew.OnClientClick = Window1.GetShowReference("RectifyNoticeEdit.aspx") + "return false;";
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
            string strSql = @"SELECT RectifyNotices.RectifyNoticesId,RectifyNotices.ProjectId,CodeRecords.Code AS RectifyNoticesCode,RectifyNotices.UnitId,RectifyNotices.WorkAreaId,RectifyNotices.CheckedDate,RectifyNotices.WrongContent,RectifyNotices.SignPerson,"
                          + @" RectifyNotices.SignDate,RectifyNotices.CompleteStatus,RectifyNotices.DutyPerson,RectifyNotices.CompleteDate,RectifyNotices.IsRectify,RectifyNotices.CheckPerson,RectifyNotices.AttachUrl,Unit.UnitName,WorkArea.WorkAreaName,Person.UserName AS CheckPersonName"
                          + @" FROM Check_RectifyNotices AS RectifyNotices "
                          + @" LEFT JOIN Base_Project AS Project ON Project.ProjectId = RectifyNotices.ProjectId "
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = RectifyNotices.UnitId "
                          + @" LEFT JOIN ProjectData_WorkArea AS WorkArea ON WorkArea.WorkAreaId = RectifyNotices.WorkAreaId "
                          + @" LEFT JOIN Sys_User AS Users ON RectifyNotices.SignPerson = Users.UserId "
                          + @" LEFT JOIN Sys_User AS Person ON Person.UserId = RectifyNotices.CheckPerson "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON RectifyNotices.RectifyNoticesId = CodeRecords.DataId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND RectifyNotices.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND RectifyNotices.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            if (!string.IsNullOrEmpty(this.txtRectifyNoticesCode.Text.Trim()))
            {
                strSql += " AND RectifyNoticesCode LIKE @RectifyNoticesCode";
                listStr.Add(new SqlParameter("@RectifyNoticesCode", "%" + this.txtRectifyNoticesCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            {
                strSql += " AND WorkArea.WorkAreaName LIKE @WorkAreaName";
                listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
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
            string RectifyNoticeId = Grid1.SelectedRowID;
            var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(RectifyNoticeId);
            if (rectifyNotice != null)
            {
                if (!string.IsNullOrEmpty(rectifyNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeView.aspx?RectifyNoticeId={0}", RectifyNoticeId, "查看 - ")));
                }
                else
                {
                    if (this.btnAuditing.Hidden == true)
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeEdit.aspx?RectifyNoticeId={0}", RectifyNoticeId, "编辑 - ")));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeEdit.aspx?states=check&RectifyNoticeId={0}", RectifyNoticeId, "审核 - ")));
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
            string RectifyNoticeId = Grid1.SelectedRowID;
            var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(RectifyNoticeId);
            if (rectifyNotice != null)
            {
                if (!string.IsNullOrEmpty(rectifyNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeView.aspx?RectifyNoticeId={0}", RectifyNoticeId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeEdit.aspx?RectifyNoticeId={0}", RectifyNoticeId, "编辑 - ")));
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
            string RectifyNoticeId = Grid1.SelectedRowID;
            var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(RectifyNoticeId);
            if (rectifyNotice != null)
            {
                if (!string.IsNullOrEmpty(rectifyNotice.CheckPerson))
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeView.aspx?RectifyNoticeId={0}", RectifyNoticeId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticeEdit.aspx?states=check&RectifyNoticeId={0}", RectifyNoticeId, "审核 - ")));
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
                    var RectifyNotices = BLL.RectifyNoticesService.GetRectifyNoticesById(rowID);
                    if (RectifyNotices != null)
                    {

                        BLL.LogService.AddSys_Log(this.CurrUser, RectifyNotices.RectifyNoticesCode, rowID, BLL.Const.ProjectRectifyNoticeMenuId, BLL.Const.BtnDelete);
                        BLL.RectifyNoticesService.DeleteRectifyNoticesById(rowID);
                        Model.Check_CheckDayDetail dayDetail = (from x in Funs.DB.Check_CheckDayDetail
                                                                where x.RectifyNoticeId == rowID
                                                                select x).FirstOrDefault();
                        Model.Check_CheckSpecialDetail specialDetail = (from x in Funs.DB.Check_CheckSpecialDetail
                                                                        where x.RectifyNoticeId == rowID
                                                                        select x).FirstOrDefault();
                        Model.Check_CheckColligationDetail colligationDetail = (from x in Funs.DB.Check_CheckColligationDetail
                                                                                where x.RectifyNoticeId == rowID
                                                                                select x).FirstOrDefault();
                        if (dayDetail != null)
                        {
                            dayDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
                        else if (specialDetail != null)
                        {
                            specialDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
                        else if (colligationDetail != null)
                        {
                            colligationDetail.RectifyNoticeId = null;
                            Funs.DB.SubmitChanges();
                        }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectRectifyNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnPrint.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("隐患整改通知单" + filename, System.Text.Encoding.UTF8) + ".xls");
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticePrint.aspx?RectifyNoticeId={0}", Grid1.SelectedRowID, "打印 - ")));
        }
    }
}