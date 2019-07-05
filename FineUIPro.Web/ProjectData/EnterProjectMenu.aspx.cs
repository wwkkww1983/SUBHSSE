﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class EnterProjectMenu : PageBase
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

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId)) //根据用户ID判断是否 管理角色、领导角色 且是本单位用户
            {
                strSql = @"SELECT ProjectId,ProjectCode,ProjectName,StartDate,EndDate,ProjectAddress,"
                        + @" (CASE WHEN ProjectState='" + BLL.Const.ProjectState_2 + "' THEN '暂停中' WHEN ProjectState='" + BLL.Const.ProjectState_3 + "' THEN '已完工' ELSE '施工中' END) AS ProjectStateName,ProjectState"
                        + @" FROM Base_Project WHERE 1=1 ";
            }
            else
            {
                strSql = @"SELECT Project.ProjectId,Project.ProjectCode,Project.ProjectName,Project.StartDate,Project.EndDate,Project.ProjectAddress,"
                        + @" (CASE WHEN Project.ProjectState='" + BLL.Const.ProjectState_2 + "' THEN '暂停中' WHEN Project.ProjectState='" + BLL.Const.ProjectState_3 + "' THEN '已完工' ELSE '施工中' END) AS ProjectStateName,Project.ProjectState"
                        + @" FROM Base_Project AS Project "
                        + @" LEFT JOIN Project_ProjectUser AS ProjectUser ON Project.ProjectId =ProjectUser.ProjectId "
                        + @" WHERE ProjectUser.UserId=@UserId";
                listStr.Add(new SqlParameter("@UserId", this.CurrUser.UserId));
            }
                  
            if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
            {
                strSql += " and ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtProjectCode.Text.Trim()))
            {
                strSql += " and ProjectCode LIKE @ProjectCode";
                listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtProjectCode.Text.Trim() + "%"));
            }

            if (this.cbIsState.SelectedValueArray.Length == 1)
            {
                ///
                string selectValue = String.Join(", ", this.cbIsState.SelectedValueArray);
                if (selectValue == "1")
                {
                    strSql += " AND (ProjectState = '" + BLL.Const.ProjectState_1 + "' OR ProjectState IS NULL)";
                }
                else
                {
                    strSql += " AND ProjectState IS NOT NULL AND ProjectState != '" + BLL.Const.ProjectState_1 + "'";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 排序分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            //Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            //Grid1.SortDirection = e.SortDirection;
            //Grid1.SortField = e.SortField;
            this.BindGrid();
        }
        #endregion

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 1)
            {
                this.CurrUser.LoginProjectId = Grid1.SelectedRowID;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 1)
            {
                this.CurrUser.LoginProjectId = Grid1.SelectedRowID;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}