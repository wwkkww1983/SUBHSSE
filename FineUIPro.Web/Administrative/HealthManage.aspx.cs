using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.Administrative
{
    public partial class HealthManage :PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("HealthManageEdit.aspx") + "return false;";
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
            string strSql = "SELECT HealthManage.HealthManageId,"
                + @"HealthManage.ProjectId,"
                + @"HealthManage.PersonId,"
                + @"HealthManage.Age,"
                + @"HealthManage.Bloodtype,"
                + @"HealthManage.HealthState,"
                + @"HealthManage.Taboo,"
                + @"HealthManage.CheckTime,"
                + @"HealthManage.Remark,"
                + @"HealthManage.States, "
                + @"HealthManage.CompileMan, "
                + @"HealthManage.CompileDate, "
                + @"Users.UserName AS PersonName, "
                 + @"(CASE WHEN HealthManage.States = " + BLL.Const.State_0 + " OR HealthManage.States IS NULL THEN 'ISNULL(OperateUser.UserName,Users.UserName)' WHEN HealthManage.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                + @" FROM Administrative_HealthManage AS HealthManage "
                + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON HealthManage.HealthManageId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                + @" LEFT JOIN Sys_User AS Users ON HealthManage.PersonId = Users.UserId "
                + @" LEFT JOIN Sys_User AS Users2 ON HealthManage.CompileMan = Users2.UserId WHERE 1=1 ";       
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND HealthManage.ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND HealthManage.CheckTime >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND HealthManage.CheckTime <= @EndDate";
                listStr.Add(new SqlParameter("@EndDate", this.txtEndDate.Text.Trim()));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtStartDate.Text.Trim());
                DateTime endTime = Convert.ToDateTime(this.txtEndDate.Text.Trim());
                if (startTime > endTime)
                {
                    Alert.ShowInTop("开始时间不能大于结束时间！", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
        }

        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
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
            var healthManage = BLL.HealthManageService.GetHealthManageById(id);
            if (healthManage != null)
            {
                if (this.btnMenuEdit.Hidden || healthManage.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HealthManageView.aspx?HealthManageId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HealthManageEdit.aspx?HealthManageId={0}", id, "编辑 - ")));
                }
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
                    var healthManage = BLL.HealthManageService.GetHealthManageById(rowID);
                    if (healthManage != null)
                    {
                        var user = BLL.UserService.GetUserByUserId(healthManage.PersonId);
                        BLL.LogService.AddSys_Log(this.CurrUser, user.UserName, rowID, BLL.Const.HealthManageMenuId, BLL.Const.BtnDelete);
                    }
                    BLL.HealthManageService.DeleteHealthManageById(rowID);
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HealthManageMenuId);
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