using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostUnitManage : PageBase
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
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, BLL.ConstValue.Group_0008, false);
                this.drpYear.SelectedValue = System.DateTime.Now.Year.ToString();
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonths, BLL.ConstValue.Group_0009, false);
                this.drpMonths.SelectedValue = System.DateTime.Now.Month.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT UnitManage.HSSECostUnitManageId,UnitManage.HSSECostManageId,CostManage.Month,CostManage.Code,Unit.UnitCode,Unit.UnitName,SysUser.UserName AS CompileMan,UnitManage.CompileDate"
                        + @" ,(UnitManage.CostA1+UnitManage.CostA2+UnitManage.CostA3+UnitManage.CostA4+UnitManage.CostA5+UnitManage.CostA6+UnitManage.CostA7+UnitManage.CostA8) AS CostAsum"
                        + @" ,(UnitManage.CostB1+UnitManage.CostB2) AS CostBsum,UnitManage.CostC1 AS CostCsum,(UnitManage.CostD1+UnitManage.CostD2+UnitManage.CostD3) AS CostDsum"
                        + @" ,(CASE WHEN UnitManage.States = " + BLL.Const.State_0 + " OR UnitManage.States IS NULL THEN '待['+ISNULL(OperateUser.UserName,SysUser.UserName)+']提交' WHEN UnitManage.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                        + @" FROM CostGoods_HSSECostUnitManage AS UnitManage"
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON UnitManage.HSSECostUnitManageId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
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
                listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue.Trim()));
            }
            if (this.rbStateType.SelectedValue == "1")
            {
                strSql += " AND (UnitManage.StateType  = " + this.rbStateType.SelectedValue + " OR UnitManage.StateType IS NULL)";
            }
            else if (this.rbStateType.SelectedValue == "2")
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
                if (!this.btnMenuEdit.Hidden && (unitManage.StateType == "1" || String.IsNullOrEmpty(unitManage.StateType)))   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageEdit.aspx?HSSECostUnitManageId={0}", id, "编辑 - ")));
                }
                else
                {
                    SetPage(id, unitManage.StateType);
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
                    var unitManage = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostUnitManageId(rowID);
                    if (unitManage != null && (this.CurrUser.UnitId == unitManage.UnitId || BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)))
                    {                        
                        BLL.HSSECostUnitManageService.DeleteHSSECostUnitManageById(rowID);
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectHSSECostUnitManageMenuId);
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

        #region 增加按钮事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNew_Click(object sender, EventArgs e)
        {
            if (this.drpYear.SelectedValue != BLL.Const._Null && this.drpMonths.SelectedValue != BLL.Const._Null
                && this.drpUnitId.SelectedValue != BLL.Const._Null && this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                DateTime? Months = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonths.SelectedValue);
                string hsseCostManageId = string.Empty;
                string unitId = this.drpUnitId.SelectedValue;
                var hsseCost = BLL.HSSECostManageService.GetHSSECostManageByProjectIdMonth(this.ProjectId, Funs.GetNewIntOrZero(this.drpYear.SelectedValue), Funs.GetNewIntOrZero(this.drpMonths.SelectedValue));
                if (hsseCost != null)
                {
                    hsseCostManageId = hsseCost.HSSECostManageId;
                }
                else
                {
                    hsseCostManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_HSSECostManage));
                    Model.CostGoods_HSSECostManage newHSSECostManage = new Model.CostGoods_HSSECostManage
                    {
                        HSSECostManageId = hsseCostManageId,
                        ProjectId = this.ProjectId,
                        Month = Months,
                        Code = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHSSECostManageMenuId, this.ProjectId, string.Empty),
                        ReportDate = System.DateTime.Now,
                        CompileDate = System.DateTime.Now,
                    };

                    BLL.HSSECostManageService.AddHSSECostManage(newHSSECostManage);
                }
                var unitCost = BLL.HSSECostUnitManageService.GetHSSECostUnitManageByHSSECostManageIdUnitId(hsseCostManageId, unitId);
                if (unitCost == null)
                {
                    string HSSECostUnitManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_HSSECostUnitManage));
                    Model.CostGoods_HSSECostUnitManage newHSSECostUnit = new Model.CostGoods_HSSECostUnitManage
                    {
                        HSSECostUnitManageId = HSSECostUnitManageId,
                        HSSECostManageId = hsseCostManageId,
                        UnitId = unitId,
                        StateType = "1",
                        States = "0",
                        CompileManId = this.CurrUser.UserId,
                        CompileDate = DateTime.Now,
                        CostA1 = 0,
                        CostA2 = 0,
                        CostA3 = 0,
                        CostA4 = 0,
                        CostA5 = 0,
                        CostA6 = 0,
                        CostA7 = 0,
                        CostA8 = 0,
                        CostB1 = 0,
                        CostB2 = 0,
                        CostC1 = 0,
                        CostD1 = 0,
                        CostD2 = 0,
                        CostD3 = 0,
                    };

                    BLL.HSSECostUnitManageService.AddHSSECostUnitManage(newHSSECostUnit);
                    
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostUnitManageEdit.aspx?HSSECostUnitManageId={0}", HSSECostUnitManageId, "编辑 - ")));
                }
                else
                {
                    Alert.ShowInTop("当月已存在费用表！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                Alert.ShowInTop("请选择月份、单位！", MessageBoxIcon.Warning);
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