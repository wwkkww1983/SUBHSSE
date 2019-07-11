using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using System.Data;

namespace FineUIPro.Web.Technique
{
    public partial class ProjectSolutionTemplete : PageBase
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
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("ProjectSolutionTempleteEdit.aspx") + "return false;";
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
            string strSql = @"SELECT ProjectSolutionTemplete.TempleteId, "
                            + @"ProjectSolutionTemplete.TempleteCode,"
                            + @"ProjectSolutionTemplete.TempleteName,"
                            + @"SolutionTempleteType.SolutionTempleteTypeName AS TempleteType,"
                            + @"ProjectSolutionTemplete.FileContents,"
                            + @"ProjectSolutionTemplete.CompileMan,"
                            + @"ProjectSolutionTemplete.CompileDate,"
                            + @"Users.UserName AS CompileManName "
                          + @" FROM dbo.Technique_ProjectSolutionTemplete AS ProjectSolutionTemplete "
                          + @" LEFT JOIN Sys_User AS Users ON ProjectSolutionTemplete.CompileMan = Users.UserId "
                          + @" LEFT JOIN Base_SolutionTempleteType AS SolutionTempleteType ON SolutionTempleteType.SolutionTempleteTypeCode = ProjectSolutionTemplete.TempleteType ";
            List<SqlParameter> listStr = new List<SqlParameter>();            
            if (!string.IsNullOrEmpty(this.txtTempleteCode.Text.Trim()))
            {
                strSql += " AND TempleteCode LIKE @TempleteCode";
                listStr.Add(new SqlParameter("@TempleteCode", "%" + this.txtTempleteCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtTempleteName.Text.Trim()))
            {
                strSql += " AND TempleteName LIKE @TempleteName";
                listStr.Add(new SqlParameter("@TempleteName", "%" + this.txtTempleteName.Text.Trim() + "%"));
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

        #region 编辑
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var projectSolutionTemplete = BLL.ProjectSolutionTempleteService.GetProjectSolutionTempleteById(id);
            if (projectSolutionTemplete != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSolutionTempleteEdit.aspx?TempleteId={0}", id, "编辑 - ")));
            }
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
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.ProjectSolutionTempleteService.GetProjectSolutionTempleteById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.TempleteCode, getV.TempleteId, BLL.Const.ProjectSolutionTempleteMenuId, BLL.Const.BtnDelete);
                        BLL.ProjectSolutionTempleteService.DeleteProjectSolutionTempleteById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectSolutionTempleteMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}