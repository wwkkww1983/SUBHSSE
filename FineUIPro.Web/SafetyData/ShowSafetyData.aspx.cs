using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SafetyData
{
    public partial class ShowSafetyData : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string SafetyDataCheckProjectId
        {
            get
            {
                return (string)ViewState["SafetyDataCheckProjectId"];
            }
            set
            {
                ViewState["SafetyDataCheckProjectId"] = value;
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
                this.SafetyDataCheckProjectId = Request.Params["SafetyDataCheckProjectId"];
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
            string strSql = @"SELECT SafetyData.SafetyDataId,SafetyData.Code,SafetyData.Title,SafetyData.Score"
                       + @" FROM SafetyData_SafetyData AS SafetyData "                       
                       + @" WHERE SafetyData.IsEndLever =1 ";
                             
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
            var safetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckProjectBySafetyDataCheckProjectId(this.SafetyDataCheckProjectId);
            if (safetyDataCheckItem != null)
            {
                var safetyDataCheck = BLL.SafetyDataCheckService.GetSafetyDataCheckById(safetyDataCheckItem.SafetyDataCheckId);
                if (safetyDataCheck != null)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        Model.SafetyData_SafetyDataCheckItem newSafetyDataCheckItem = new Model.SafetyData_SafetyDataCheckItem
                        {
                            SafetyDataCheckItemId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckItem)),
                            SafetyDataCheckProjectId = this.SafetyDataCheckProjectId,
                            SafetyDataId = Grid1.DataKeys[rowIndex][0].ToString(),
                            StartDate = safetyDataCheck.StartDate,
                            EndDate = safetyDataCheck.EndDate
                        };
                        //newSafetyDataCheckItem.ReminderDate = SafetyDataCheckItem.ReminderDate;
                        //newSafetyDataCheckItem.SubmitDate = SafetyDataCheckItem.SubmitDate;
                        var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(newSafetyDataCheckItem.SafetyDataId);
                        if (safetyData != null)
                        {
                            newSafetyDataCheckItem.ShouldScore = safetyData.Score;
                            newSafetyDataCheckItem.Remark = safetyData.Remark;
                        }
                        newSafetyDataCheckItem.RealScore = 0;                        
                        BLL.SafetyDataCheckItemService.AddSafetyDataCheckItem(newSafetyDataCheckItem);
                    }

                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
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