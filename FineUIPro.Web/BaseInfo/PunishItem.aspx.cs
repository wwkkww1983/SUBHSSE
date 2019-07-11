using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class PunishItem : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                btnNew.OnClientClick = Window1.GetShowReference("PunishItemEdit.aspx") + "return false;";
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select PunishItemId,PunishItemCode,PunishItemContent,Deduction,PunishMoney,case PunishItemType when '1' then '安全' else '质量' end as PunishItemType from HSSE_Hazard_PunishItem order by PunishItemCode";
            SqlParameter[] parameter = null;
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, object fillteredObj, string column)
        {
            bool valid = false;
            if (column == "PunishItemContent")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.ToString();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 弹出编辑窗口关闭事件
        /// <summary>
        /// 弹出编辑窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
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
            string PunishItemId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishItemEdit.aspx?PunishItemId={0}", PunishItemId, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.PunishItemMenuId, Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    bool isShow = false;
                    if (Grid1.SelectedRowIndexArray.Length == 1)
                    {
                        isShow = true;
                    }
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        if (this.judgementDelete(rowID, isShow))
                        {
                            var getD = BLL.HSSE_Hazard_PunishItemService.GetTitleByPunishItemId(rowID);
                            if (getD != null)
                            {
                                BLL.LogService.AddSys_Log(this.CurrUser, getD.PunishItemCode, getD.PunishItemId, BLL.Const.PunishItemMenuId, BLL.Const.BtnDelete);
                                BLL.HSSE_Hazard_PunishItemService.DeleteTitle(rowID);                                
                            }
                        }
                    }
                    BindGrid();
                    ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion
    }
}