using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class ProjectSafetyDataEItem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 资料主键
        /// </summary>
        public string SafetyDataEId
        {
            get
            {
                return (string)ViewState["SafetyDataEId"];
            }
            set
            {
                ViewState["SafetyDataEId"] = value;
            }
        }
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
        /// <summary>
        /// 上报开始时间
        /// </summary>
        public DateTime? RealStartDate
        {
            get
            {
                return (DateTime?)ViewState["RealStartDate"];
            }
            set
            {
                ViewState["RealStartDate"] = value;
            }
        }
        /// <summary>
        /// 上报结束时间
        /// </summary>
        public DateTime? RealEndDate
        {
            get
            {
                return (DateTime?)ViewState["RealEndDate"];
            }
            set
            {
                ViewState["RealEndDate"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var SafetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(Request.Params["SafetyDataEPlanId"]);
                if (SafetyDataEPlan != null)
                {
                    this.SafetyDataEId = SafetyDataEPlan.SafetyDataEId;
                    this.ProjectId = SafetyDataEPlan.ProjectId;
                }
                this.BindGrid();
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();                              
            }
        }

        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Item.SafetyDataEItemId,Item.SafetyDataEId,Item.Code,Item.Title,Item.CompileMan,UserName AS CompileManName,Item.CompileDate,Item.SubmitDate,Item.Remark,Item.AttachUrl "
                              + @" FROM dbo.SafetyDataE_SafetyDataEItem AS Item"
                              + @" LEFT JOIN Sys_User ON CompileMan=UserId "
                              + @" WHERE SafetyDataEId=@SafetyDataEId AND ProjectId=@ProjectId  ";
            SqlParameter[] parameter = new SqlParameter[]
                {
                        new SqlParameter("@SafetyDataEId",this.SafetyDataEId),
                        new SqlParameter("@ProjectId",this.ProjectId),
                };

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            this.Grid1.RecordCount = tb.Rows.Count;
            var table = this.GetPagedDataTable(this.Grid1, tb);
            this.Grid1.DataSource = table;
            this.Grid1.DataBind();
        }
        
        #region gv排序翻页
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {          
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion
        
        #region 企业安全管理资料明细事件       
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            this.ViewData();
        }

        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.ViewData();
        }

        /// <summary>
        /// 查看
        /// </summary>
        private void ViewData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

            var ProjectSafetyDataE = BLL.SafetyDataEItemService.GetSafetyDataEItemByID(Grid1.SelectedRowID);
            if (ProjectSafetyDataE != null)
            {
                if (ProjectSafetyDataE.IsMenu == true)  ///是否菜单记录 
                {
                    if (!string.IsNullOrEmpty(ProjectSafetyDataE.Url))
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(ProjectSafetyDataE.Url, Grid1.SelectedRowID), "查看", 1100, 620));
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataEItemView.aspx?SafetyDataEItemId={0}&SafetyDataEId={1}", Grid1.SelectedRowID, ProjectSafetyDataE.SafetyDataEId, "查看 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目安全管理资料" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion        
    }
}