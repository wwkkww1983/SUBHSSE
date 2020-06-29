﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class CheckSpecial : PageBase
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
                Technique_CheckItemSetService.InitCheckItemSetDropDownList(this.drpSupCheckItemSet, "2", "0", true);
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("CheckSpecialEdit.aspx") + "return false;";
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
            string strSql = @"SELECT CheckSpecial.CheckSpecialId,CodeRecords.Code AS CheckSpecialCode,"
                          + @" CheckItemSet.CheckItemName,CheckSpecial.CheckTime,(CASE WHEN CheckSpecial.States='0' OR CheckSpecial.States IS NULL THEN '待提交' ELSE '已提交' END) AS StatesName"
                          + @" FROM Check_CheckSpecial AS CheckSpecial "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON CheckSpecial.CheckSpecialId=CodeRecords.DataId "
                          + @" LEFT JOIN Technique_CheckItemSet AS CheckItemSet ON CheckItemSet.CheckItemSetId = CheckSpecial.CheckItemSetId where 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckSpecial.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (this.rbStates.SelectedValue!="-1")
            {
                strSql += " AND CheckSpecial.States = @States"; 
                listStr.Add(new SqlParameter("@States", this.rbStates.SelectedValue));
            }
            if (this.drpSupCheckItemSet.SelectedValue!=BLL.Const._Null)
            {
                strSql += " AND CheckSpecial.CheckItemSetId = @CheckItemSetId";
                listStr.Add(new SqlParameter("@CheckItemSetId", this.drpSupCheckItemSet.SelectedValue ));
            }
            if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
            {
                strSql += " AND CheckSpecial.CheckTime >= @StartTime";
                listStr.Add(new SqlParameter("@StartTime", this.txtStartTime.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                strSql += " AND CheckSpecial.CheckTime <= @EndTime";
                listStr.Add(new SqlParameter("@EndTime", this.txtEndTime.Text.Trim()));
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
            string CheckSpecialId = Grid1.SelectedRowID.Split(',')[0];
            var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
            if (checkSpecial != null)
            {
                if (this.btnMenuModify.Hidden || checkSpecial.States == BLL.Const.State_1)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialView.aspx?CheckSpecialId={0}", CheckSpecialId, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx?CheckSpecialId={0}", CheckSpecialId, "编辑 - ")));
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
                    var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(rowID);
                    if (checkSpecial != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckSpecialDetailService.DeleteCheckSpecialDetails(rowID);

                        BLL.Check_CheckSpecialService.DeleteCheckSpecial(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectCheckSpecialMenuId);
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
            string CheckSpecialId = Grid1.SelectedRowID.Split(',')[0];
            var checkSpecial = Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
            if (checkSpecial.States == Const.State_2)
            {
                string CheckSpecialDetailId = Grid1.SelectedRowID.Split(',')[1];
                Model.Check_CheckSpecialDetail detail = Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(CheckSpecialDetailId);
                if (string.IsNullOrEmpty(detail.RectifyNoticeId))
                {
                    Model.Check_RectifyNotices rectifyNotice = new Model.Check_RectifyNotices
                    {
                        RectifyNoticesId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotice)),
                        ProjectId = checkSpecial.ProjectId,
                        UnitId = detail.UnitId,
                        CheckedDate = checkSpecial.CheckTime,
                        RectifyNoticesCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, checkSpecial.ProjectId, detail.UnitId),
                        WrongContent = "开展了专项检查,发现问题及隐患:" + detail.Unqualified + "\n" + detail.Suggestions,
                        SignPerson = this.CurrUser.UserId,
                        SignDate = DateTime.Now,
                        States = Const.State_0,
                    };

                    var workArea = Funs.DB.ProjectData_WorkArea.FirstOrDefault(x => x.ProjectId == checkSpecial.ProjectId && x.WorkAreaName == detail.WorkArea);
                    if (workArea != null)
                    {
                        rectifyNotice.WorkAreaId = workArea.WorkAreaId;
                    }

                    RectifyNoticesService.AddRectifyNotices(rectifyNotice);
                    rectifyNoticeCode = rectifyNotice.RectifyNoticesCode;
                    detail.RectifyNoticeId = rectifyNotice.RectifyNoticesId;
                    Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(detail);
                }
                if (!string.IsNullOrEmpty(rectifyNoticeCode))
                {
                    Alert.ShowInTop("已生成隐患整改通知单：" + rectifyNoticeCode + "！", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("隐患整改通知单已存在，请到对应模块进行处理！", MessageBoxIcon.Warning);
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
            string CheckSpecialId = Grid1.SelectedRowID.Split(',')[0];
            var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
            if (checkSpecial.States == BLL.Const.State_2)
            {
                string CheckSpecialDetailId = Grid1.SelectedRowID.Split(',')[1];
                Model.Check_CheckSpecialDetail detail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(CheckSpecialDetailId);
                if (detail != null && !detail.CompletedDate.HasValue)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCompletedDate.aspx?CheckSpecialDetailId={0}", CheckSpecialDetailId), "编辑闭环时间", 400, 250));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("专项检查" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “CheckSpecial.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “CheckSpecial.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialPrint.aspx?CheckSpecialId={0}", Grid1.SelectedRowID.Split(',')[0], "打印 - ")));
        }

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void drpSupCheckItemSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "click")
            {
                string[] checkSpecialDetail = (Grid1.DataKeys[e.RowIndex][0].ToString()).Split(',');
                if (checkSpecialDetail.Count() > 1)
                {
                    var detail = Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(checkSpecialDetail[1]);
                    if (detail != null)
                    {
                        if (detail.DataType == "1")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RectifyNoticesView.aspx?RectifyNoticesId={0}", detail.DataId, "查看 - ")));
                        }
                        else if (detail.DataType == "2")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishNoticeView.aspx?PunishNoticeId={0}", detail.DataId, "查看 - ")));
                        }
                        else if (detail.DataType == "3")
                        {
                            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", detail.DataId, "查看 - ")));
                        }
                    }
                }
            }
        }
    }
}