﻿using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class PauseNotice : PageBase
    {
        #region 项目主键
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
                Funs.DropDownPageSize(this.ddlPageSize);
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("PauseNoticeEdit.aspx") + "return false;";

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
            string strSql = @"SELECT PauseNotice.PauseNoticeId,PauseNotice.ProjectId,CodeRecords.Code AS PauseNoticeCode,Unit.UnitName,PauseNotice.ProjectPlace,PauseNotice.UnitId,PauseNotice.PauseTime,case PauseNotice.IsConfirm when 1 then '已确认' else '未确认' end as IsConfirmStr ,
             (CASE WHEN PauseNotice.PauseStates = '0' OR PauseNotice.PauseStates IS NULL THEN '待['+CompileMan.UserName+']提交' WHEN PauseNotice.PauseStates = '1' THEN '待['+SignMan.UserName+']签发'  WHEN PauseNotice.PauseStates = '2' THEN '待['+ApproveMan.UserName+']批准' WHEN PauseNotice.PauseStates = '3' THEN '待['+DutyPerson.UserName+']接收' WHEN PauseNotice.PauseStates = '4' THEN '审批完成' END) AS  FlowOperateName 
            FROM Check_PauseNotice AS PauseNotice 
             LEFT JOIN Sys_User AS CompileMan ON CompileMan.UserId=PauseNotice.CompileManId 
             LEFT JOIN Sys_User AS SignMan ON SignMan.UserId=PauseNotice.SignManId 
             LEFT JOIN Sys_User AS ApproveMan ON ApproveMan.UserId=PauseNotice.ApproveManId 
             LEFT JOIN Sys_User AS DutyPerson ON DutyPerson.UserId=PauseNotice.DutyPersonId
             LEFT JOIN Sys_CodeRecords AS CodeRecords ON PauseNotice.PauseNoticeId=CodeRecords.DataId LEFT JOIN Base_Unit AS Unit ON Unit.UnitId=PauseNotice.UnitId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND PauseNotice.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND PauseNotice.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (this.rbStates.SelectedValue != "-1")
            {
                strSql += " AND PauseNotice.PauseStates =@PauseStates";
                listStr.Add(new SqlParameter("@PauseStates", this.rbStates.SelectedValue));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND PauseNotice.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));

                //strSql += " AND PauseNotice.States = @States";  ///状态为已完成
                //listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            if (!string.IsNullOrEmpty(this.txtPauseNoticeCode.Text.Trim()))
            {
                strSql += " AND PauseNoticeCode LIKE @PauseNoticeCode";
                listStr.Add(new SqlParameter("@PauseNoticeCode", "%" + this.txtPauseNoticeCode.Text.Trim() + "%"));
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
            string PauseNoticeId = Grid1.SelectedRowID;
            var pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
            if (pauseNotice != null)
            {
                bool flag = false;
                if (this.btnMenuModify.Hidden || pauseNotice.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", PauseNoticeId, "查看 - ")));
                }
                else
                {
                    if (pauseNotice.PauseStates == BLL.Const.State_0 && pauseNotice.CompileManId == this.CurrUser.UserId)
                    {
                        flag = true;
                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_1 && pauseNotice.SignManId == this.CurrUser.UserId)
                    {
                        flag = true;
                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_2 && pauseNotice.ApproveManId == this.CurrUser.UserId)
                    {
                        flag = true;
                    }
                    else if (pauseNotice.PauseStates == BLL.Const.State_3 && pauseNotice.DutyPersonId == this.CurrUser.UserId)
                    {
                        flag = true;
                    }
                    else {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeView.aspx?PauseNoticeId={0}", PauseNoticeId, "查看 - ")));
                    }
                    if (flag) {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeEdit.aspx?PauseNoticeId={0}", PauseNoticeId, "编辑 - ")));
                    }
                    
                }
            }
        }
        #endregion

        #region 签字确认
        /// <summary>
        /// 签字确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnMenuConfirm_Click(object sender, EventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //    string PauseNoticeId = Grid1.SelectedRowID;
        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PauseNoticeEdit.aspx?type=confirm&PauseNoticeId={0}", PauseNoticeId, "编辑 - ")));

        //}
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
                    Model.Check_PunishNoticeFlowOperate Operate = (from x in Funs.DB.Check_PunishNoticeFlowOperate
                                                                   where x.PunishNoticeId == rowID
                                                            select x).FirstOrDefault();
                    var getV = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.PauseNoticeCode, getV.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnDelete);                    
                        BLL.Check_PauseNoticeService.DeletePauseNotice(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectPauseNoticeMenuId);
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
                //if (buttonList.Contains(BLL.Const.BtnConfirm))
                //{
                //    this.btnMenuConfirm.Hidden = false;
                //}
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("工程暂停令" + filename, System.Text.Encoding.UTF8) + ".xls");
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

        protected void rbStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}