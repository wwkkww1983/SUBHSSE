using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyData : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();               
                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, false);
                if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                {
                    this.drpProject.SelectedValue = this.CurrUser.LoginProjectId;
                    this.drpProject.Enabled = false;
                    this.Toolbar2.Hidden = true;
                }

                this.ProjectSafetyDataDataBind();//加载树
                this.GetButtonPower();
            }
        }

        /// <summary>
        /// 项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProjectSafetyDataDataBind();//加载树
        }

        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvProjectSafetyData.SelectedNodeID) && !string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                string strSql = @"SELECT Item.SafetyDataItemId,Item.SafetyDataId,Item.Code,Item.Title,Item.CompileMan,UserName AS CompileManName,Item.CompileDate,Item.SubmitDate,Item.Remark,Item.AttachUrl,(CASE WHEN Item.IsMenu = 1 THEN '系统' ELSE '定稿' END) AS FromData "
                              + @" FROM dbo.SafetyData_SafetyDataItem AS Item"
                              + @" LEFT JOIN Sys_User ON CompileMan=UserId WHERE SafetyDataId=@SafetyDataId AND ProjectId=@ProjectId ";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@SafetyDataId",this.tvProjectSafetyData.SelectedNodeID),
                        new SqlParameter("@ProjectId",this.drpProject.SelectedValue)
                    };

                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(this.Grid1, tb);
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();
            }
        }

        /// <summary>
        /// 文件来源事件变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbIsMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvProjectSafetyData.SelectedNodeID = string.Empty;
            this.ProjectSafetyDataDataBind();
            this.BindGrid();
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void ProjectSafetyDataDataBind()
        {
            this.tvProjectSafetyData.Nodes.Clear();
            this.tvProjectSafetyData.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "企业安全管理资料",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvProjectSafetyData.Nodes.Add(rootNode);
            var SafetyDataList = BLL.SafetyDataService.GetSafetyDataList();
            if (SafetyDataList.Count() > 0)
            {
                this.GetNodes(SafetyDataList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.SafetyData_SafetyData> SafetyDataList, TreeNode node)
        {
            var SafetyDatas = SafetyDataList.Where(x => x.SupSafetyDataId == node.NodeID);
            foreach (var item in SafetyDatas)
            {
                bool isShow = false; ///此节点是否显示
                if ((this.cbIsMenu.Items[0].Selected && this.cbIsMenu.Items[1].Selected) || (!this.cbIsMenu.Items[0].Selected && !this.cbIsMenu.Items[1].Selected))
                {
                    isShow = true;
                }
                else
                {
                    bool isMenu = true; ///来自系统
                    if (this.cbIsMenu.Items[1].Selected) ///定稿
                    {
                        isMenu = false;
                    }

                    isShow = BLL.SafetyDataService.IsShowSafetyDataTreeNode(item.SafetyDataId, isMenu);
                }

                if (isShow)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.Code + "：" + item.Title,
                        ToolTip = item.Code + "：" + item.Title
                    };
                    if (item.IsEndLever == true)
                    {
                        newNode.EnableClickEvent = true;
                        if (item.Score.HasValue)
                        {
                            newNode.ToolTip += "【分值：" + item.Score.ToString() + "】";
                        }
                        if (!string.IsNullOrEmpty(item.MenuId))
                        {
                            var menu = BLL.SysMenuService.GetSysMenuByMenuId(item.MenuId);
                            {
                                if (menu != null)
                                {
                                    newNode.ToolTip += "  【匹配菜单：" + menu.MenuName + "】";
                                }                                
                            }
                        }else
                        {
                            newNode.ToolTip += "  【手动做定稿文件】";
                        }
                    }

                    if (item.IsCheck == true)
                    {
                        newNode.Text = "<span class=\"isnew\">" + newNode.Text + "</span>";
                        if (newNode.ParentNode != null)
                        {
                            newNode.ParentNode.Text = "<span class=\"isnew\">" + newNode.ParentNode.Text + "</span>";
                        }
                    }

                    newNode.NodeID = item.SafetyDataId;
                    node.Nodes.Add(newNode);

                    if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                    {
                        GetNodes(SafetyDataList, newNode);
                    }
                }
            }
        }
        #endregion
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvProjectSafetyData_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region gv排序翻页
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion
        
        #region 增加、修改、删除企业安全管理资料明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (tvProjectSafetyData.SelectedNode != null)
            {
                var ProjectSafetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.tvProjectSafetyData.SelectedNodeID);
                if (ProjectSafetyData != null && ProjectSafetyData.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataItemEdit.aspx?SafetyDataId={0}", tvProjectSafetyData.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择末级节点添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择末级节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModifyDetail_Click(object sender, EventArgs e)
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
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

            var ProjectSafetyData = BLL.SafetyDataItemService.GetSafetyDataItemByID(Grid1.SelectedRowID);
            if (ProjectSafetyData != null)
            {
                if (ProjectSafetyData.IsMenu == true)  ///是否菜单记录 
                {
                    if (!string.IsNullOrEmpty(ProjectSafetyData.Url))
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(ProjectSafetyData.Url, Grid1.SelectedRowID), "查看", 1100, 620));
                    }
                }
                else
                {
                    if (this.btnMenuModifyDetail.Hidden)  ///是否具有操作权限
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataItemView.aspx?SafetyDataItemId={0}", Grid1.SelectedRowID, "查看 - ")));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataItemEdit.aspx?SafetyDataItemId={0}", Grid1.SelectedRowID, "修改 - ")));
                    }
                }
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

            var ProjectSafetyData = BLL.SafetyDataItemService.GetSafetyDataItemByID(Grid1.SelectedRowID);
            if (ProjectSafetyData != null)
            {
                if (ProjectSafetyData.IsMenu == true)  ///是否菜单记录 
                {
                    if (!string.IsNullOrEmpty(ProjectSafetyData.Url))
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(ProjectSafetyData.Url, Grid1.SelectedRowID), "查看", 1100, 620));
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataItemView.aspx?SafetyDataItemId={0}", Grid1.SelectedRowID, "查看 - ")));
                }
            }
        }

        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelDetail_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    var item = BLL.SafetyDataItemService.GetSafetyDataItemByID(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.SafetyDataItemService.DeleteSafetyDataItemByID(item.SafetyDataItemId);
                        BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除项目文件明细信息", item.Code);  
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
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
            string menuId = BLL.Const.ServerProjectSafetyDataMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectSafetyDataMenuId;
            }

            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNewDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModifyDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelDetail.Hidden = false;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目安全管理资料" + filename, System.Text.Encoding.UTF8) + ".xls");
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
                    //if (column.ColumnID == "tfNumber")
                    //{
                    //    html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    //}
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