﻿using System;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using System.Data;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayWH :PageBase
    {
        #region 定义项
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
        #endregion

        #region 加载页面
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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("CheckDayWHEdit.aspx") + "return false;";

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
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                    new SqlParameter("@ProjectId", this.ProjectId),
                    new SqlParameter("@StartTime", !string.IsNullOrEmpty(this.txtStartTime.Text)?this.txtStartTime.Text:null),
                    new SqlParameter("@EndTime", !string.IsNullOrEmpty(this.txtEndTime.Text)?this.txtEndTime.Text:null),
                    new SqlParameter("@States", !string.IsNullOrEmpty(Request.Params["projectId"])?BLL.Const.State_2:null),
                    new SqlParameter("@UnitName", !string.IsNullOrEmpty(this.txtUnitName.Text)?this.txtUnitName.Text:null),
                    new SqlParameter("@WorkAreaName",  !string.IsNullOrEmpty(this.txtWorkAreaName.Text)?this.txtWorkAreaName.Text:null)
                    };
            DataTable tb = SQLHelper.GetDataTableRunProc("SpCheckDayStatistic", parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string[] rowID = Grid1.Rows[i].DataKeys[0].ToString().Split(',');
                if (rowID.Count() > 0)
                {
                    var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(rowID[0]);
                    if (checkDay != null)
                    {
                        if (checkDay.States == BLL.Const.State_2)
                        {
                            if (rowID.Count() > 1)
                            {
                                Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(rowID[1]);
                                if (detail != null)
                                {
                                    if (!detail.CompletedDate.HasValue)
                                    {
                                        Grid1.Rows[i].RowCssClass = "Yellow";
                                    }
                                }
                                //else
                                //{
                                //    Grid1.Rows[i].RowCssClass = "Red";
                                //}
                            }
                        }
                        else
                        {
                            Grid1.Rows[i].RowCssClass = "Green";
                        }
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
            string CheckDayId = Grid1.SelectedRowID.Split(',')[0];
            var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(CheckDayId);
            if (checkDay != null)
            {
                if (this.btnMenuModify.Hidden || checkDay.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayWHView.aspx?CheckDayId={0}", CheckDayId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayWHEdit.aspx?CheckDayId={0}", CheckDayId, "编辑 - ")));
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
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString().Split(',')[0];
                    var getV = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.CheckDayCode, getV.CheckDayId, BLL.Const.ProjectCheckDayMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckDayDetailService.DeleteCheckDayDetails(rowID);
                        BLL.Check_CheckDayService.DeleteCheckDay(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckDayWHMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnCompletedDate.Hidden = false;
                    this.btnMenuRectify.Hidden = false;
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
            }
        }
        #endregion

        #region 生成隐患整改单
        /// <summary>
        /// 生成隐患整改单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuRectify_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string rectifyNoticeCode = string.Empty;
            string CheckDayId = Grid1.SelectedRowID.Split(',')[0];
            var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(CheckDayId);
            if (checkDay.States == BLL.Const.State_2)
            {
                string CheckDayDetailId = Grid1.SelectedRowID.Split(',')[1];
                Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(CheckDayDetailId);
                if (string.IsNullOrEmpty(detail.RectifyNoticeId))
                {
                    if (!string.IsNullOrEmpty(detail.UnitId))
                    {
                        Model.Check_RectifyNotices rectifyNotice = new Model.Check_RectifyNotices
                        {
                            RectifyNoticesId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotice)),
                            ProjectId = checkDay.ProjectId,
                            UnitId = detail.UnitId,
                            CheckedDate = checkDay.CheckTime,
                            RectifyNoticesCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, checkDay.ProjectId, detail.UnitId),
                            WrongContent = "开展了日常巡检,发现问题及隐患:" + detail.Unqualified + "\n" + detail.Suggestions,
                            SignPerson = this.CurrUser.UserId,
                            SignDate = DateTime.Now,
                            DutyPersonId = detail.PersonId,
                        };
                        var workArea = Funs.DB.ProjectData_WorkArea.FirstOrDefault(x => x.ProjectId == checkDay.ProjectId && x.WorkAreaName == detail.WorkArea);
                        if (workArea != null)
                        {
                            rectifyNotice.WorkAreaId = workArea.WorkAreaId;
                        }
                        
                        BLL.RectifyNoticesService.AddRectifyNotices(rectifyNotice);

                        rectifyNoticeCode = rectifyNotice.RectifyNoticesCode;
                        detail.RectifyNoticeId = rectifyNotice.RectifyNoticesId;
                        BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
                        ///写入工程师日志
                        BLL.HSSELogService.CollectHSSELog(rectifyNotice.ProjectId, rectifyNotice.SignPerson, rectifyNotice.SignDate, "22", rectifyNotice.WrongContent, Const.BtnAdd, 1);
                        if (!string.IsNullOrEmpty(rectifyNoticeCode))
                        {
                            Alert.ShowInTop("已生成隐患整改通知单：" + rectifyNoticeCode + "！", MessageBoxIcon.Success);
                            return;
                        }
                    }
                    else
                    {
                        Alert.ShowInTop("单位不能为空！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("隐患整改通知单已存在，请到对应模块进行处理！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("该记录尚未审批完成，无法进行操作！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 闭环
        /// <summary>
        /// 闭环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompletedDate_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string CheckDayId = Grid1.SelectedRowID.Split(',')[0];
            var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(CheckDayId);
            if (checkDay.States == BLL.Const.State_2)
            {
                string CheckDayDetailId = Grid1.SelectedRowID.Split(',')[1];
                Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(CheckDayDetailId);
                if (detail != null && !detail.CompletedDate.HasValue)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCompletedDate.aspx?CheckDayDetailId={0}", CheckDayDetailId), "编辑闭环时间", 400, 250));
                }
                else
                {
                    Alert.ShowInTop("该记录已闭环或不存在明细项！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("该记录尚未审批完成，无法进行闭环操作！", MessageBoxIcon.Warning);
                return;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("日常巡检（五环）" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “CheckDayWH.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “CheckDayWH.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayWHPrint.aspx?CheckDayId={0}", Grid1.SelectedRowID.Split(',')[0], "打印 - ")));
        }
        #endregion

        
    }
}