using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataCheck : PageBase
    {
        #region 定义项
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
                ////权限按钮方法                              
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                // 绑定表格
                this.BindGrid();                
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT SafetyDataCheck.SafetyDataCheckId,SafetyDataCheck.Code,SafetyDataCheck.Title,SafetyDataCheck.StartDate,SafetyDataCheck.EndDate,SafetyDataCheck.CompileDate,Users.UserName AS CompileManName"
                          + @" FROM SafetyData_SafetyDataCheckProject AS CheckProject"
                          + @" LEFT JOIN SafetyData_SafetyDataCheck AS SafetyDataCheck ON CheckProject.SafetyDataCheckId =SafetyDataCheck.SafetyDataCheckId"
                          + @" LEFT JOIN Sys_User AS Users ON Users.UserId = SafetyDataCheck.CompileMan WHERE CheckProject.ProjectId=@ProjectId ";                         
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                strSql += " AND Code LIKE @Code";
                listStr.Add(new SqlParameter("@Code", "%" + this.txtCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();        
           
            var safetyDataCheckProject = from x in Funs.DB.SafetyData_SafetyDataCheckProject select x;
            var safetyDataCheckItem = from x in Funs.DB.SafetyData_SafetyDataCheckItem where !x.SubmitDate.HasValue select x;
            if (safetyDataCheckItem.Count() > 0)
            {
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string safetyDataCheckId = Grid1.Rows[i].DataKeys[0].ToString();
                    var isNull = from x in safetyDataCheckItem
                                 join y in safetyDataCheckProject on x.SafetyDataCheckProjectId equals y.SafetyDataCheckProjectId
                                 where y.SafetyDataCheckId == safetyDataCheckId
                                 select x;
                    if (isNull.Count() > 0) ////是否存在未上报的单据
                    {
                        Grid1.Rows[i].RowCssClass = "Green";
                    }
                }
            }
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

        #region 编辑
        /// <summary>
        /// 双击事件
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

            string id = Grid1.SelectedRowID;
            var SafetyData = BLL.SafetyDataCheckService.GetSafetyDataCheckById(id);
            if (SafetyData != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataCheckEdit.aspx?SafetyDataCheckId={0}&ProjectId={1}", id, this.ProjectId, "查看 - ")));
            }     
        }      
        #endregion
    }
}