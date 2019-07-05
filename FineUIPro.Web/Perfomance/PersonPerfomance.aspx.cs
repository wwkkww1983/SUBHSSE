using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.Perfomance
{
    public partial class PersonPerfomance : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                this.btnNew.OnClientClick = Window1.GetShowReference("PersonPerfomanceEdit.aspx") + "return false;";
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
            string strSql = @"SELECT PersonPerfomance.PersonPerfomanceId,"
                          + @"PersonPerfomance.ProjectId,"
                          + @"CodeRecords.Code AS PersonPerfomanceCode,"
                          + @"PersonPerfomance.UnitId,"
                          + @"PersonPerfomance.TeamGroupId,"
                          + @"PersonPerfomance.PersonId,"
                          + @"PersonPerfomance.SubContractNum,"
                          + @"PersonPerfomance.EvaluationDate,"
                          + @"PersonPerfomance.CompileMan,"
                          + @"PersonPerfomance.CompileDate,"
                          + @"PersonPerfomance.States,"
                          + @"Unit.UnitName,"
                          + @"TeamGroup.TeamGroupName,"
                          + @"Person.PersonName,"
                          + @"(CASE WHEN PersonPerfomance.States = " + BLL.Const.State_0 + " OR PersonPerfomance.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN PersonPerfomance.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM Perfomance_PersonPerfomance AS PersonPerfomance "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON PersonPerfomance.PersonPerfomanceId = CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON PersonPerfomance.PersonPerfomanceId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = PersonPerfomance.UnitId "
                          + @" LEFT JOIN ProjectData_TeamGroup AS TeamGroup ON TeamGroup.TeamGroupId = PersonPerfomance.TeamGroupId "
                          + @" LEFT JOIN SitePerson_Person AS Person ON Person.PersonId = PersonPerfomance.PersonId "
                          + @" LEFT JOIN Sys_User AS Users ON PersonPerfomance.CompileMan = Users.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND PersonPerfomance.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND PersonPerfomance.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND PersonPerfomance.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
            }
            if (!string.IsNullOrEmpty(this.txtPersonPerfomanceCode.Text.Trim()))
            {
                strSql += " AND PersonPerfomance.PersonPerfomanceCode LIKE @PersonPerfomanceCode";
                listStr.Add(new SqlParameter("@PersonPerfomanceCode", "%" + this.txtPersonPerfomanceCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
            {
                strSql += " AND PersonPerfomance.EvaluationDate >= @StartDate";
                listStr.Add(new SqlParameter("@StartDate", this.txtStartDate.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " AND PersonPerfomance.EvaluationDate <= @EndDate";
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

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                if (Convert.ToDateTime(this.txtStartDate.Text.Trim()) > Convert.ToDateTime(this.txtEndDate.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间", MessageBoxIcon.Warning);
                    return;
                }
            }
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
            var personPerfomance = BLL.PersonPerfomanceService.GetPersonPerfomanceById(id);
            if (personPerfomance != null)
            {
                if (this.btnMenuEdit.Hidden || personPerfomance.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonPerfomanceView.aspx?PersonPerfomanceId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonPerfomanceEdit.aspx?PersonPerfomanceId={0}", id, "编辑 - ")));
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
                    BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除个人绩效评价", rowID);
                    BLL.PersonPerfomanceService.DeletePersonPerfomanceById(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonPerfomanceMenuId);
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