using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataPlanItemDelete : PageBase
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
                this.ProjectId = Request.Params["ProjectId"];
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
            string strSql = @"SELECT DISTINCT SafetyDataPlan.ProjectId,SafetyDataPlan.SafetyDataId,SafetyData.Title,SafetyData.Code"
                          + @" ,(CASE WHEN SafetyDataPlanDelete.SafetyDataPlanDeleteId IS NULL THEN 1 ELSE 0 END) AS IsDelete"
                          + @" FROM SafetyData_SafetyData AS SafetyData"
                          + @" LEFT JOIN SafetyData_SafetyDataPlan AS SafetyDataPlan ON SafetyDataPlan.SafetyDataId =SafetyData.SafetyDataId"
                          + @" LEFT JOIN SafetyData_SafetyDataPlanDelete AS SafetyDataPlanDelete ON SafetyDataPlan.ProjectId = SafetyDataPlanDelete.ProjectId and SafetyData.SafetyDataId=SafetyDataPlanDelete.SafetyDataId"
                          + @" WHERE SafetyDataPlanDelete.SafetyDataPlanDeleteId IS NULL AND SafetyDataPlan.ProjectId ='" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();          
            if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                strSql += " AND SafetyData.Code LIKE @Code";
                listStr.Add(new SqlParameter("@Code", "%" + this.txtCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND SafetyData.Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
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
                    string safetyDataId = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安全管理资料计划项");
                    BLL.SafetyDataPlanService.AddSafetyDataPlanDelete(this.ProjectId, safetyDataId);
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                int i = 0;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    i++;
                    BLL.SafetyDataPlanService.AddSafetyDataPlanDelete(this.ProjectId, rowID);
                }
                BindGrid();
                if (i > 0)
                {
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安全管理资料计划项");
                    ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);

                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion
    }
}