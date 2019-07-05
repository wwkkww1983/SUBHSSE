using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataPlanItemAdd : PageBase
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
            string strSql = @"SELECT DISTINCT SafetyDataPlanDelete.ProjectId,SafetyDataPlanDelete.SafetyDataId,SafetyData.Title,SafetyData.Code,"
                          + @" (CASE WHEN SafetyDataPlanDelete.SafetyDataPlanDeleteId IS NULL THEN 1 ELSE 0 END) AS IsDelete"
                          + @" FROM SafetyData_SafetyDataPlanDelete AS SafetyDataPlanDelete"
                          + @" LEFT JOIN SafetyData_SafetyData AS SafetyData ON SafetyDataPlanDelete.SafetyDataId =SafetyData.SafetyDataId "
                          + @" LEFT JOIN SafetyData_SafetyDataPlan AS SafetyDataPlan ON SafetyDataPlan.SafetyDataId =SafetyData.SafetyDataId  AND SafetyDataPlan.ProjectId=SafetyDataPlanDelete.ProjectId "
                          + @" WHERE SafetyDataPlanDelete.ProjectId ='" + this.ProjectId + "'";
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

        #region 增加
        /// <summary>
        /// 右键增加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuAdd_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加安全管理资料计划项", rowID);
                    BLL.SafetyDataPlanService.DeleteSafetyDataPlanDelete(this.ProjectId, rowID);
                    ////根据项目和安全资料项生成企业安全管理资料计划总表
                    BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(this.ProjectId, rowID);
                }

                this.BindGrid();
                ShowNotify("增加数据成功!", MessageBoxIcon.Success);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }


        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                int i = 0;
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    i++;
                    BLL.SafetyDataPlanService.DeleteSafetyDataPlanDelete(this.ProjectId, rowID);
                    ////根据项目和安全资料项生成企业安全管理资料计划总表
                    BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(this.ProjectId, rowID);
                }
                BindGrid();
                if (i > 0)
                {
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加安全管理资料计划项");
                    ShowNotify("增加数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                }
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion        
    }
}