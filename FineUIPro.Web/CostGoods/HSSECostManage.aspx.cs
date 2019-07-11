using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.CostGoods
{
    public partial class HSSECostManage : PageBase
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
            string strSql = @" SELECT  hsseCost.HSSECostManageId,hsseCost.ProjectId,Project.ProjectName,hsseCost.CompileManId,sysUser.UserName AS CompileManName,hsseCost.CompileDate,hsseCost.Month,ISNULL(hsseCost.Code,CodeRecords.Code) AS Code,hsseCost.ReportDate"
                        + @", hsseCost.MainIncome,hsseCost.Remark1,hsseCost.ConstructionIncome,hsseCost.Remark2,hsseCost.SafetyCosts,hsseCost.Remark3"
                        + @" ,(CASE WHEN hsseCost.States =  '" + BLL.Const.State_0 + "' OR hsseCost.States IS NULL"
                        + @" THEN '待[' + ISNULL(OperateUser.UserName, SysUser.UserName) + ']提交'"
                        + @" WHEN hsseCost.States = '" + BLL.Const.State_2 + "' AND FlowOperate.FlowOperateId is not null THEN '待[' + OperateUser.UserName + ']办理'"
                        + @" WHEN hsseCost.States = '" + BLL.Const.State_2 + "' THEN '核定完成'"
                        + @" ELSE '待[' + OperateUser.UserName + ']办理' END) AS FlowOperateName"
                        + @" FROM CostGoods_HSSECostManage AS hsseCost"
                        + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON hsseCost.HSSECostManageId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                        + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId"
                        + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON hsseCost.HSSECostManageId = CodeRecords.DataId"
                        + @" LEFT JOIN Base_Project AS Project ON hsseCost.ProjectId = Project.ProjectId"
                        + @" LEFT JOIN Sys_User AS sysUser ON hsseCost.CompileManId = sysUser.UserId"
                        + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND hsseCost.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

            if (this.rbStateType.SelectedValue == "0")
            {
                strSql += " AND (hsseCost.States IS NULL OR hsseCost.States  = " + BLL.Const.State_0 + ")";
            }
            else if (this.rbStateType.SelectedValue == "1")
            {
                strSql += " AND hsseCost.States  = " + BLL.Const.State_2;
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
            var unitManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(id);
            if (unitManage != null)
            {
                if (this.btnMenuEdit.Hidden || unitManage.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostManageView.aspx?HSSECostManageId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostManageEdit.aspx?HSSECostManageId={0}", id, "编辑 - ")));
                }
            }
        }
        #endregion

        #region 右键查看
        /// <summary>
        /// 右键查看事件
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
            
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSECostManageView.aspx?HSSECostManageId={0}", Grid1.SelectedRowID, "查看 - ")));
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
                    var HSSECostManage = BLL.HSSECostManageService.GetHSSECostManageByHSSECostManageId(rowID);
                    if (HSSECostManage != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, HSSECostManage.Code, HSSECostManage.HSSECostManageId, BLL.Const.ProjectHSSECostManageMenuId, BLL.Const.BtnDelete);

                        BLL.HSSECostUnitManageService.DeleteHSSECostUnitManageByHSSECostManageId(rowID);
                        BLL.HSSECostManageService.DeleteHSSECostManageById(rowID);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectHSSECostManageMenuId);
            if (buttonList.Count() > 0)
            {                
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

        protected void rbStateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}