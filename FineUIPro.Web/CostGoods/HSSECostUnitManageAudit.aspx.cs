using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManageAudit : PageBase
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
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.ProjectId, BLL.Const.ProjectUnitType_2, true);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }               
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, true);                
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonths, BLL.ConstValue.Group_0009, true);                
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT UnitManage.HSSECostUnitManageId,UnitManage.HSSECostManageId,CostManage.Month,CostManage.Code,Unit.UnitCode,Unit.UnitName,SysUser.UserName AS CompileMan,UnitManage.CompileDate"
                        + @" ,(UnitManage.AuditCostA1+UnitManage.AuditCostA2+UnitManage.AuditCostA3+UnitManage.AuditCostA4+UnitManage.AuditCostA5+UnitManage.AuditCostA6+UnitManage.AuditCostA7+UnitManage.AuditCostA8) AS CostAsum"
                        + @" ,(UnitManage.CostB1+UnitManage.CostB2) AS CostBsum,UnitManage.CostC1 AS CostCsum,(UnitManage.CostD1+UnitManage.CostD2+UnitManage.CostD3) AS CostDsum"
                        + @" ,(CASE WHEN UnitManage.States =  '" + BLL.Const.State_0 + "' OR UnitManage.States IS NULL "
                        + @" THEN '待['+ISNULL(OperateUser.UserName,SysUser.UserName)+']提交' "
                        + @" WHEN UnitManage.States = '" + BLL.Const.State_2 + "' AND FlowOperate.FlowOperateId is not null THEN '待[' + OperateUser.UserName + ']办理' "
                        + @" WHEN UnitManage.States = '" + BLL.Const.State_2 + "' AND FlowOperate.FlowOperateId is null THEN '审核完成'"
                        + @" ELSE '待[' + OperateUser.UserName + ']办理' END) AS FlowOperateName"
                        + @" FROM CostGoods_HSSECostUnitManage AS UnitManage"
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON UnitManage.HSSECostUnitManageId +'#2'=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                        + @" LEFT JOIN CostGoods_HSSECostManage AS CostManage ON UnitManage.HSSECostManageId=CostManage.HSSECostManageId"
                        + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                        + @" LEFT JOIN Base_Unit AS Unit ON UnitManage.UnitId=Unit.UnitId"
                        + @" LEFT JOIN Sys_User AS SysUser ON UnitManage.CompileManId=SysUser.UserId"
                        + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CostManage.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND UnitManage.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue));
            }
            if (this.drpYear.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND  Year(CostManage.Month) = @Yaer";
                listStr.Add(new SqlParameter("@Yaer", this.drpYear.SelectedValue));
            }
            if (this.drpMonths.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND  Month(CostManage.Month) = @Month";
                listStr.Add(new SqlParameter("@Month", this.drpMonths.SelectedValue));
            }

            if (this.rbStateType.SelectedValue == "2")
            {
                strSql += " AND UnitManage.StateType  = " + this.rbStateType.SelectedValue;
            }
            else if (this.rbStateType.SelectedValue == "3")
            {
                strSql += " AND UnitManage.StateType = " + this.rbStateType.SelectedValue;
            }
            else if (this.rbStateType.SelectedValue == "4")
            {
                strSql += " AND UnitManage.StateType = " + this.rbStateType.SelectedValue;
            }
            else
            {
                strSql += " AND UnitManage.StateType != 1 AND UnitManage.StateType IS NOT NULL";
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
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
            this.BindGrid();
        }        
        #endregion

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
            var unitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(id);
            if (unitManage != null)
            {
                if (!this.btnMenuEdit.Hidden && unitManage.StateType == "2")   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageAuditEdit.aspx?HSSECostUnitManageId={0}", id, "编辑 - ")));
                }
                else
                {
                    SetPage(id, unitManage.StateType);
                }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectHSSECostUnitManageAuditMenuId);
            if (buttonList.Count() > 0)
            {               
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            var unitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(id);
            if (unitManage != null)
            {
                SetPage(id, unitManage.StateType);
            }
        }

        # region 页面调转方法
        /// <summary>
        /// 页面调转方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        private void SetPage(string id, string type)
        {
            if (type == "1")   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageView.aspx?HSSECostUnitManageId={0}", id, "查看 - ")));
            }
            else if (type == "2")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageAuditView.aspx?HSSECostUnitManageId={0}", id, "查看 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageRatifiedView.aspx?HSSECostUnitManageId={0}", id, "查看 - ")));
            }
        }
        #endregion
    }
}