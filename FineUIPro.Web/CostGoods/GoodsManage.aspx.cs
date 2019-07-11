using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsManage : PageBase
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
                this.btnNew.OnClientClick = Window1.GetShowReference("GoodsManageEdit.aspx") + "return false;";
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
            string strSql = @" SELECT GoodsManage.GoodsManageId, "
                          + @" GoodsManage.ProjectId,"
                          + @" CodeRecords.Code AS GoodsCode,"
                          + @" GoodsManage.GoodsName,"
                          + @" GoodsManage.UnitId,"
                          + @" GoodsManage.GoodsCategoryId,"
                          + @" GoodsManage.SizeModel,"
                          + @" GoodsManage.FactoryCode,"
                          + @" GoodsManage.CheckDate,"
                          + @" GoodsManage.EnableYear,"
                          + @" GoodsManage.CheckPerson,"
                          + @" GoodsManage.InTime,"
                          + @" GoodsManage.States,"
                          + @" GoodsManage.CompileMan,"
                          + @" GoodsManage.CompileDate,"
                          + @" GoodsManage.Remark,"
                          + @" Unit.UnitName,"
                          + @" Users.UserName AS CheckPersonName,"
                          + @" (CASE WHEN GoodsManage.States = " + BLL.Const.State_0 + " OR GoodsManage.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN GoodsManage.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM CostGoods_GoodsManage AS GoodsManage "
                          + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = GoodsManage.UnitId "
                          + @" LEFT JOIN Sys_User AS Users ON Users.UserId = GoodsManage.CheckPerson "
                          + @" LEFT JOIN Sys_User AS Users2 ON GoodsManage.CompileMan = Users2.UserId "
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON GoodsManage.GoodsManageId = CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON GoodsManage.GoodsManageId = FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId = OperateUser.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND GoodsManage.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {              
                strSql += " AND GoodsManage.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2));
            }

            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND GoodsManage.UnitId = @UnitId";  ///状态为已完成
                listStr.Add(new SqlParameter("@UnitId", this.CurrUser.UnitId));
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
            var goodsManage = BLL.GoodsManageService.GetGoodsManageById(id);
            if (goodsManage != null)
            {
                if (this.btnMenuEdit.Hidden || goodsManage.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GoodsManageView.aspx?GoodsManageId={0}", id, "查看 - ")));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GoodsManageEdit.aspx?GoodsManageId={0}", id, "编辑 - ")));
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
                    var goodsManage = BLL.GoodsManageService.GetGoodsManageById(rowID);
                    if (goodsManage != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, goodsManage.GoodsCode, goodsManage.GoodsManageId, BLL.Const.GoodsManageMenuId, BLL.Const.BtnDelete);
                        BLL.GoodsManageService.DeleteGoodsManageById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取物资类别名称
        /// </summary>
        /// <param name="goodsCategoryId"></param>
        /// <returns></returns>
        protected string ConvertGoodsType(object goodsCategoryId)
        {
            string name = string.Empty;
            if (goodsCategoryId != null)
            {
                var goodsCategory = BLL.GoodsCategoryService.GetGoodsCategoryById(goodsCategoryId.ToString());
                if (goodsCategory != null)
                {
                    name = goodsCategory.GoodsCategoryName;
                }
            }
            return name;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.GoodsManageMenuId);
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

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("物资管理" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfGoodsType")
                    {
                        html = (row.FindControl("lblGoodsType") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion        
    }
}