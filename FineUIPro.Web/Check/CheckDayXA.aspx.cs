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
    public partial class CheckDayXA : PageBase
    {
        /// <summary>
        /// 项目id
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnit.Enabled = false;
                }
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("CheckDayXAEdit.aspx") + "return false;";
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
            string strSql = @"SELECT CheckDay.CheckDayId,CheckDay.ProjectId,CodeRecords.Code AS CheckDayCode,CheckDay.WorkAreaIds,CheckDay.CheckDate,CheckDay.CompileUnit,Users.UserName as CheckPersonName,CheckDay.NotOKNum,CheckDay.Unqualified,CheckDay.HandleStation"
                          + @" ,(CASE WHEN CheckDay.States = " + BLL.Const.State_0 + " OR CheckDay.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN CheckDay.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM Check_CheckDayXA AS CheckDay "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON CheckDay.CheckDayId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId "
                          + @" LEFT JOIN Sys_User AS Users ON CheckDay.CompileMan=Users.UserId "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON CheckDay.CheckDayId=CodeRecords.DataId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckDay.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (!string.IsNullOrEmpty(this.txtCheckDayCode.Text.Trim()))
            {
                strSql += " AND CheckDayCode LIKE @CheckDayCode";
                listStr.Add(new SqlParameter("@CheckDayCode", "%" + this.txtCheckDayCode.Text.Trim() + "%"));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND CheckDay.CompileUnit = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            //if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            //{
            //    strSql += " AND Area LIKE @UnitName";
            //    listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            //}
            //if (!string.IsNullOrEmpty(this.txtWorkAreaName.Text.Trim()))
            //{
            //    strSql += " AND Area LIKE @WorkAreaName";
            //    listStr.Add(new SqlParameter("@WorkAreaName", "%" + this.txtWorkAreaName.Text.Trim() + "%"));
            //}
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND CheckDay.CompileUnit = @UnitId2";
                listStr.Add(new SqlParameter("@UnitId2", this.drpUnit.SelectedValue));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Model.Check_CheckDayXA checkHoliday = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(this.Grid1.DataKeys[i][0].ToString());
                if (checkHoliday != null)
                {
                    if (checkHoliday.IsOK != true)
                    {
                        Grid1.Rows[i].RowCssClass = "yellow";
                    }
                }
            }
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

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
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
            btnMenuModify_Click(null, null);
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
            string CheckDayId = Grid1.SelectedRowID;
            var checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(CheckDayId);
            if (checkDay != null)
            {
                if (this.btnMenuModify.Hidden || checkDay.IsOK == true)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayXAView.aspx?CheckDayId={0}", CheckDayId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayXAEdit.aspx?CheckDayId={0}", CheckDayId, "编辑 - ")));
                }
            }
        }
        #endregion

        #region Grid点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string id = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Handle")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CheckDayXAHandleEdit.aspx?CheckDayId={0}", id, "整改情况 - ")));
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
                    var getV = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(rowID);
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.CheckDayCode, getV.CheckDayId, BLL.Const.ProjectCheckDayXAMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckDayXAService.DeleteCheckDay(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckDayXAMenuId);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("日常巡检" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “CheckDayXA.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “CheckDayXA.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 获取受检单位/班组
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertUnitOrTeamGroup(object CheckDayId)
        {
            string name = string.Empty;
            if (CheckDayId != null)
            {
                Model.Check_CheckDayXA checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(CheckDayId.ToString());
                if (checkDay != null)
                {
                    if (!string.IsNullOrEmpty(checkDay.DutyUnitIds))
                    {
                        string[] unitIds = checkDay.DutyUnitIds.Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                            name += unit.UnitName + ",";
                        }
                        if (!string.IsNullOrEmpty(name))
                        {
                            name = name.Substring(0, name.LastIndexOf(","));
                        }
                    }
                    if (!string.IsNullOrEmpty(checkDay.DutyTeamGroupIds))
                    {
                        string[] teamGroupIds = checkDay.DutyTeamGroupIds.Split(',');
                        foreach (var item in teamGroupIds)
                        {
                            Model.ProjectData_TeamGroup unit = BLL.TeamGroupService.GetTeamGroupById(item);
                            name += unit.TeamGroupName + ",";
                        }
                        if (!string.IsNullOrEmpty(name))
                        {
                            name = name.Substring(0, name.LastIndexOf(","));
                        }
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 获取检查区域
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertWorkArea(object WorkAreaIds)
        {
            string name = string.Empty;
            if (WorkAreaIds != null)
            {
                if (!string.IsNullOrEmpty(WorkAreaIds.ToString()))
                {
                    string[] workAreaIds = WorkAreaIds.ToString().Split(',');
                    foreach (var item in workAreaIds)
                    {
                        Model.ProjectData_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(item);
                        name += workArea.WorkAreaName + ",";
                    }
                    if (!string.IsNullOrEmpty(name))
                    {
                        name = name.Substring(0, name.LastIndexOf(","));
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 获取不合格项描述提要
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertUnqualified(object Unqualified)
        {
            if (Unqualified != null)
            {
                return BLL.Funs.GetSubStr(Unqualified, 20);
            }
            return "";
        }

        /// <summary>
        /// 获取整改情况描述提要
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertHandleStation(object HandleStation)
        {
            if (HandleStation != null)
            {
                return BLL.Funs.GetSubStr(HandleStation, 20);
            }
            return "";
        }
        #endregion
    }
}