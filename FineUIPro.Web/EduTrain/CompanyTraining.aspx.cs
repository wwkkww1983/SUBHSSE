using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace FineUIPro.Web.EduTrain
{
    public partial class CompanyTraining : PageBase
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
                this.GetButtonPower();
                btnDeleteDetail.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDeleteDetail.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                btnSelectColumns.OnClientClick = Window5.GetShowReference("CompanyTrainingSelectCloumn.aspx");
                InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trCompanyTraining.Nodes.Clear();
            trCompanyTraining.ShowBorder = false;
            trCompanyTraining.ShowHeader = false;
            trCompanyTraining.EnableIcons = true;
            trCompanyTraining.AutoScroll = true;
            trCompanyTraining.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "公司教材库",
                NodeID = "0",
                Expanded = true
            };
            this.trCompanyTraining.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewTraining(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.CompanyTrainingName,
                        NodeID = dr.CompanyTrainingId,
                        EnableClickEvent = true,
                        ToolTip = dr.CompanyTrainingName
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.CompanyTrainingId);
                }
            }
        }

        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Training_CompanyTraining> GetNewTraining(string parentId)
        {
            return (from x in Funs.DB.Training_CompanyTraining where x.SupCompanyTrainingId == parentId orderby x.CompanyTrainingCode select x).ToList(); ;
        }
        #endregion

        #region 增加、修改、删除公司教材库类别
        /// <summary>
        /// 增加公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                Model.Training_CompanyTraining training = BLL.CompanyTrainingService.GetCompanyTrainingById(this.trCompanyTraining.SelectedNode.NodeID);
                if ((training != null && training.IsEndLever == false) || this.trCompanyTraining.SelectedNode.NodeID == "0")   //根节点或者非末级节点，可以增加
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CompanyTrainingSave.aspx?SupCompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
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
        /// 修改公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                if (this.trCompanyTraining.SelectedNode.NodeID != "0")   //非根节点可以编辑
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CompanyTrainingSave.aspx?CompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("根节点无法编辑！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除公司教材库类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                var q = BLL.CompanyTrainingService.GetCompanyTrainingById(this.trCompanyTraining.SelectedNode.NodeID);

                if (q != null && BLL.CompanyTrainingService.IsDeleteCompanyTraining(this.trCompanyTraining.SelectedNode.NodeID))
                {
                    BLL.CompanyTrainingService.DeleteCompanyTraining(this.trCompanyTraining.SelectedNode.NodeID);
                    InitTreeMenu();
                }
                else
                {
                    ShowNotify("存在下级菜单或已增加资源数据或者为内置项，不允许删除！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择删除项！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 树点击事件
        /// <summary>
        /// 树点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trCompanyTraining_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.trCompanyTraining.SelectedNode.NodeID))
            {
                string strSql = @"SELECT CompanyTrainingItem.CompanyTrainingItemId,
                                         CompanyTrainingItem.CompanyTrainingId,
                                         CompanyTrainingItem.CompanyTrainingItemCode,
                                         CompanyTrainingItem.CompanyTrainingItemName,
                                         CompanyTrainingItem.AttachUrl,
                                         CompanyTrainingItem.CompileMan,
                                         CompanyTrainingItem.CompileDate "
                                + @" FROM dbo.Training_CompanyTrainingItem AS CompanyTrainingItem "
                                + @" WHERE CompanyTrainingItem.CompanyTrainingId=@CompanyTrainingId";

                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@CompanyTrainingId", this.trCompanyTraining.SelectedNode.NodeID));
                if (!string.IsNullOrEmpty(this.txtCompanyTrainingItemCode.Text.Trim()))
                {
                    strSql += " AND CompanyTrainingItemCode LIKE @CompanyTrainingItemCode";
                    listStr.Add(new SqlParameter("@CompanyTrainingItemCode", "%" + this.txtCompanyTrainingItemCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtCompanyTrainingItemName.Text.Trim()))
                {
                    strSql += " AND CompanyTrainingItemName LIKE @CompanyTrainingItemName";
                    listStr.Add(new SqlParameter("@CompanyTrainingItemName", "%" + this.txtCompanyTrainingItemName.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 弹出窗关闭事件
        protected void Window1_Close(object sender, EventArgs e)
        {
            InitTreeMenu();
        }

        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion        

        #region 排序、分页
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
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

        #region 增加、修改、删除明细信息
        #region 新增明细
        /// <summary>
        /// 新增明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (this.trCompanyTraining.SelectedNode != null)
            {
                if (this.trCompanyTraining.SelectedNode.Nodes.Count == 0)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyTrainingItemSave.aspx?CompanyTrainingId={0}", this.trCompanyTraining.SelectedNode.NodeID, "编辑 - ")));
                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑明细
        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

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

            string companyTrainingItemId = Grid1.SelectedRowID;

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("CompanyTrainingItemSave.aspx?CompanyTrainingItemId={0}", companyTrainingItemId, "编辑 - ")));
        }
        #endregion

        #region 删除明细
        // 删除数据
        protected void btnDeleteDetail_Click(object sender, EventArgs e)
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
                    var getD = BLL.CompanyTrainingItemService.GetCompanyTrainingItemById(rowID);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.CompanyTrainingItemCode, getD.CompanyTrainingItemId, BLL.Const.CompanyTrainingMenuId, BLL.Const.BtnDelete);
                        BLL.CompanyTrainingItemService.DeleteCompanyTrainingItemById(rowID);
                        
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion
        #endregion

        #region 导出
        /// <summary>
        /// 关闭导出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window5_Close(object sender, WindowCloseEventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1, e.CloseArgument.Split('#')));
            Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid, string[] columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            List<string> columnHeaderTexts = new List<string>(columns);
            List<int> columnIndexs = new List<int>();
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (columnHeaderTexts.Contains(column.HeaderText))
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                    columnIndexs.Add(column.ColumnIndex);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                int columnIndex = 0;
                foreach (object value in row.Values)
                {
                    if (columnIndexs.Contains(columnIndex))
                    {
                        string html = value.ToString();
                        if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                        {
                            // 模板列                            
                            string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                            Control templateCtrl = row.FindControl(templateID);
                            html = GetRenderedHtmlSource(templateCtrl);
                        }
                        //else
                        //{
                        //    // 处理CheckBox             
                        //    if (html.Contains("f-grid-static-checkbox"))
                        //    {
                        //        if (!html.Contains("f-checked"))
                        //        {
                        //            html = "×";
                        //        }
                        //        else
                        //        {
                        //            html = "√";
                        //        }
                        //    }
                        //    // 处理图片                           
                        //    if (html.Contains("<img"))
                        //    {
                        //        string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, ""); 
                        //        html = html.Replace("src=\"", "src=\"" + prefix);
                        //    }
                        //}
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                    columnIndex++;
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>        
        /// 获取控件渲染后的HTML源代码        
        /// </summary>        
        /// <param name="ctrl"></param>        
        /// <returns></returns>        
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);
                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
        #endregion         

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CompanyTrainingMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnEditDetail.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnDeleteDetail.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }                
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnSelectColumns.Hidden = false;
                }
            }
        }
        #endregion
    }
}