using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ProjectFolder : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.ProjectFolderDataBind();//加载树
                this.GetButtonPower();
            }
        }

        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvProjectFolder.SelectedNodeID))
            {
                string strSql = @"SELECT Item.ProjectFolderItemId,Item.ProjectFolderId,CodeRecords.Code,Item.Title,Item.FileContent,Item.CompileMan,UserName AS CompileManName,Item.CompileDate,Item.AttachUrl"
                              + @" FROM dbo.InformationProject_ProjectFolderItem AS Item"
                              + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Item.ProjectFolderItemId=CodeRecords.DataId"
                              + @" LEFT JOIN Sys_User ON CompileMan=UserId WHERE ProjectFolderId=@ProjectFolderId";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@ProjectFolderId",this.tvProjectFolder.SelectedNodeID)
                    };

                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(this.Grid1, tb);
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();
            }
        }
        
        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void ProjectFolderDataBind()
        {
            this.tvProjectFolder.Nodes.Clear();
            this.tvProjectFolder.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode();//定义根节点
            var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
            if (project != null)
            {
                rootNode.Text = "[" + project.ProjectName + "]文件夹";
            }
            else
            {
                rootNode.Text =  "项目文件夹";
            }
            rootNode.Expanded = true;
            rootNode.NodeID = "0";
            this.tvProjectFolder.Nodes.Add(rootNode);
            var ProjectFolderList = BLL.ProjectFolderService.GetProjectFolderListByProjectId(this.CurrUser.LoginProjectId);
            if (ProjectFolderList.Count() > 0)
            {
                this.GetNodes(ProjectFolderList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.InformationProject_ProjectFolder> projectFolderList, TreeNode node)
        {
            var ProjectFolders = projectFolderList.Where(x => x.SupProjectFolderId == node.NodeID);
            foreach (var item in ProjectFolders)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = "[" + item.Code + "]" + item.Title,
                    NodeID = item.ProjectFolderId
                };
                if (item.IsEndLever == true)
                {
                    newNode.EnableClickEvent = true;
                }
                node.Nodes.Add(newNode);

                if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                {
                    GetNodes(projectFolderList, newNode);
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
        protected void tvProjectFolder_NodeCommand(object sender, TreeCommandEventArgs e)
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

        #region 右键增加、修改、删除项目文件夹方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvProjectFolder.SelectedNode != null)
            {
                var ProjectFolder = BLL.ProjectFolderService.GetProjectFolderByID(this.tvProjectFolder.SelectedNodeID);
                if (ProjectFolder != null && ProjectFolder.IsEndLever == true)
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                    return;                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ProjectFolderEdit.aspx?SupProjectFolderId={0}", this.tvProjectFolder.SelectedNodeID, "新增 - ")));                    
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModify_Click(object sender, EventArgs e)
        {
            if (this.tvProjectFolder.SelectedNode != null && this.tvProjectFolder.SelectedNodeID != "0")
            {
                var ProjectFolder = BLL.ProjectFolderService.GetProjectFolderByID(this.tvProjectFolder.SelectedNodeID);
                if (ProjectFolder != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ProjectFolderEdit.aspx?ProjectFolderId={0}", ProjectFolder.ProjectFolderId, "编辑 - ")));
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDel_Click(object sender, EventArgs e)
        {
            if (this.tvProjectFolder.SelectedNode != null)
            {
                var ProjectFolder = BLL.ProjectFolderService.GetProjectFolderByID(this.tvProjectFolder.SelectedNodeID);
                if (ProjectFolder != null && BLL.ProjectFolderService.IsDeleteProjectFolder(ProjectFolder.ProjectFolderId))
                {
                    BLL.ProjectFolderService.DeleteProjectFolderByID(ProjectFolder.ProjectFolderId);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除项目文件夹设置信息",this.tvProjectFolder.SelectedNode.Text);
                    this.ProjectFolderDataBind();
                    Alert.ShowInTop("删除成功！");
                }
                else
                {
                    Alert.ShowInTop("存在下级菜单或已增加明细，不允许删除！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 增加、修改、删除项目文件夹明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (tvProjectFolder.SelectedNode != null)
            {
                var ProjectFolder = BLL.ProjectFolderService.GetProjectFolderByID(this.tvProjectFolder.SelectedNodeID);
                if (ProjectFolder != null && ProjectFolder.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectFolderItemEdit.aspx?ProjectFolderId={0}", tvProjectFolder.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择最后一级节点添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择项目文件夹！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuModifyDetail_Click(object sender, EventArgs e)
        {
            EditData();
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
            if (this.btnMenuModifyDetail.Hidden)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectFolderItemView.aspx?ProjectFolderItemId={0}", Grid1.SelectedRowID, "修改 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectFolderItemEdit.aspx?ProjectFolderItemId={0}", Grid1.SelectedRowID, "修改 - ")));
            }
        }

        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
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
                    var item = BLL.ProjectFolderItemService.GetProjectFolderItemByID(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.ProjectFolderItemService.DeleteProjectFolderItemByID(item.ProjectFolderItemId);
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

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            ProjectFolderDataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectFolderMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNewDetail.Hidden = false;
                    this.btnMenuNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                    this.btnMenuModifyDetail.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                    this.btnMenuDelDetail.Hidden = false;
                }
            }
        }
        #endregion
    }
}