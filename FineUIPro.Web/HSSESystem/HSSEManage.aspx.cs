using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.HSSESystem
{
    public partial class HSSEManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                InitTreeMenu();
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trHSSEManage.Nodes.Clear();
            this.trHSSEManage.ShowBorder = false;
            this.trHSSEManage.ShowHeader = false;
            this.trHSSEManage.EnableIcons = true;
            this.trHSSEManage.AutoScroll = true;
            this.trHSSEManage.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "安全管理机构",
                NodeID = "0",
                Expanded = true
            };
            this.trHSSEManage.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = BLL.HSSEManageService.GetHSSEManageBySupHSSEManageId(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.HSSEManageName,
                        NodeID = dr.HSSEManageId,
                        EnableClickEvent = true
                    };
                    //tn.ToolTip = "编号：" + dr.HSSEManageCode + "；<br/>机构名称：" + dr.HSSEManageName + "；<br/>职责：" + dr.Duties + "；<br/>组成文件：" + dr.BundleFile + "；<br/>机构人员：" + dr.AgencyPersonnel;
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.HSSEManageId);
                }
            }
        }

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            InitTreeMenu();
        }
        #endregion       

        /// <summary>
        /// 增加数节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trHSSEManage.SelectedNode != null)
            {
                Model.HSSESystem_HSSEManage m = BLL.HSSEManageService.GetHSSEManageById(this.trHSSEManage.SelectedNode.NodeID);
                if (m != null || this.trHSSEManage.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEManageEdit.aspx?SupHSSEManageId={0}", this.trHSSEManage.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("选择的项已是末级！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            
                if (this.trHSSEManage.SelectedNode != null)
                {
                    if (this.trHSSEManage.SelectedNode.NodeID != "0")   //非根节点可以编辑
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEManageEdit.aspx?HSSEManageId={0}", this.trHSSEManage.SelectedNode.NodeID, "编辑 - ")));
                    }
                    else
                    {
                        ShowNotify("根节点无法编辑！",MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择树节点！",MessageBoxIcon.Warning);
                }
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trHSSEManage.SelectedNode != null)
            {
                var q = BLL.HSSEManageItemService.GetHSSEManageItemList(this.trHSSEManage.SelectedNode.NodeID);
                if (q.Count() == 0)
                {
                    BLL.HSSEManageService.DeleteHSSEManage(this.trHSSEManage.SelectedNode.NodeID);
                    InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或是根节点，不允许删除！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEManageMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnNewItem.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnEditItem.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnDeleteItem.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }               
            }
        }
        #endregion

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditItem_Click(object sender, EventArgs e)
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

            string hSSEManageItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HSSEManageItemEdit.aspx?HSSEManageItemId={0}", hSSEManageItemId, "编辑 - ")));
        }
        
        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            if (this.trHSSEManage.SelectedNode != null && !string.IsNullOrEmpty(this.trHSSEManage.SelectedNode.NodeID) && this.trHSSEManage.SelectedNode.NodeID != "0")
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HSSEManageItemEdit.aspx?HSSEManageId={0}", this.trHSSEManage.SelectedNode.NodeID, "编辑 - ")));

            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除明细按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteItem_Click(object sender, EventArgs e)
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.HSSEManageItemService.GetHSSEManageItemById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.Names, getV.HSSEManageItemId, BLL.Const.HSSEManageMenuId, BLL.Const.BtnDelete);
                        BLL.HSSEManageItemService.DeleteHSSEManageItem(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 加载Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from HSSESystem_HSSEManageItem where HSSEManageId=@HSSEManageId order by SortIndex";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@HSSEManageId",this.trHSSEManage.SelectedNode.NodeID)
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trHSSEManage_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全管理机构" + filename, System.Text.Encoding.UTF8) + ".xls");
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