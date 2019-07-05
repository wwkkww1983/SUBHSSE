using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ShowSafetyDataCheckProjects : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string SafetyDataCheckId
        {
            get
            {
                return (string)ViewState["SafetyDataCheckId"];
            }
            set
            {
                ViewState["SafetyDataCheckId"] = value;
            }
        }

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
                this.SafetyDataCheckId = Request.Params["SafetyDataCheckId"];
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
            List<SqlParameter> listStr = new List<SqlParameter>();
            string strSql = "SELECT Project.ProjectId,Project.ProjectCode,Project.ProjectName,Project.StartDate,Project.EndDate,Project.ProjectAddress,SysConst.ConstText AS ProjectTypeName, "
                        + @" (CASE WHEN Project.ProjectState='" + BLL.Const.ProjectState_2 + "' THEN '暂停中' WHEN ProjectState='" + BLL.Const.ProjectState_3 + "' THEN '已完工' ELSE '施工中' END) AS ProjectStateName,Project.ProjectState"
                        + @" FROM Base_Project AS Project LEFT JOIN Sys_Const AS SysConst ON Project.ProjectType =SysConst.ConstValue AND SysConst.GroupId='" + BLL.ConstValue.Group_ProjectType + "'"
                        + @" LEFT JOIN (SELECT SafetyDataCheckProjectId,ProjectId FROM SafetyData_SafetyDataCheckProject WHERE SafetyDataCheckId =@SafetyDataCheckId) AS item ON Project.ProjectId =item.ProjectId  "
                        + @" WHERE item.SafetyDataCheckProjectId IS NULL AND (Project.ProjectState IS NULL OR Project.ProjectState != '" + BLL.Const.ProjectState_3 + "')";          
            listStr.Add(new SqlParameter("@SafetyDataCheckId", this.SafetyDataCheckId));
            if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
            {
                strSql += " ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
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
            this.GetProjectIds();
        }

        /// <summary>
        /// 右键进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnter_Click(object sender, EventArgs e)
        {
            this.GetProjectIds();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            this.GetProjectIds();
        }

        /// <summary>
        /// 得到项目id 并保存
        /// </summary>
        private void GetProjectIds()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
         
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                Model.SafetyData_SafetyDataCheckProject newSafetyDataCheckItem = new Model.SafetyData_SafetyDataCheckProject
                {
                    SafetyDataCheckProjectId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckProject)),
                    SafetyDataCheckId = this.SafetyDataCheckId,
                    ProjectId = Grid1.DataKeys[rowIndex][0].ToString()
                };
                BLL.SafetyDataCheckItemService.AddSafetyDataCheckProject(newSafetyDataCheckItem);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

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
    }
}