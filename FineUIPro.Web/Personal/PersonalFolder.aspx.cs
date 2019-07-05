using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Personal
{
    public partial class PersonalFolder : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.PersonalFolderDataBind();//加载树
            }
        }

        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvPersonalFolder.SelectedNodeID))
            {
                string strSql = @"SELECT PersonalFolderItemId,PersonalFolderId,Code,Title,FileContent,CompileDate,AttachUrl"
                        + @" FROM dbo.Personal_PersonalFolderItem WHERE PersonalFolderId=@PersonalFolderId";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@PersonalFolderId",this.tvPersonalFolder.SelectedNodeID)
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
        private void PersonalFolderDataBind()
        {
            this.tvPersonalFolder.Nodes.Clear();
            this.tvPersonalFolder.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "[" + this.CurrUser.UserName + "]文件夹",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvPersonalFolder.Nodes.Add(rootNode);
            var personalFolderList = BLL.PersonalFolderService.GetPersonalFolderListByUserId(this.CurrUser.UserId);
            if (personalFolderList.Count() > 0)
            {
                this.GetNodes(personalFolderList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.Personal_PersonalFolder> personalFolderList, TreeNode node)
        {
            var personalFolders = personalFolderList.Where(x => x.SupPersonalFolderId == node.NodeID).OrderByDescending(x=>x.Code);
            foreach (var item in personalFolders)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = "[" + item.Code + "]" + item.Title,
                    NodeID = item.PersonalFolderId
                };
                if (item.IsEndLever == true)
                {
                    newNode.EnableClickEvent = true;
                }
                node.Nodes.Add(newNode);

                if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                {
                    GetNodes(personalFolderList, newNode);
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
        protected void tvPersonalFolder_NodeCommand(object sender, TreeCommandEventArgs e)
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

        #region 右键增加、修改、删除个人文件夹方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvPersonalFolder.SelectedNode != null)
            {
                var personalFolder = BLL.PersonalFolderService.GetPersonalFolderByID(this.tvPersonalFolder.SelectedNodeID);
                if (personalFolder != null && personalFolder.IsEndLever == true)
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                    return;                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("PersonalFolderEdit.aspx?SupPersonalFolderId={0}", this.tvPersonalFolder.SelectedNodeID, "新增 - ")));                    
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
            if (this.tvPersonalFolder.SelectedNode != null && this.tvPersonalFolder.SelectedNodeID != "0")
            {
                var personalFolder = BLL.PersonalFolderService.GetPersonalFolderByID(this.tvPersonalFolder.SelectedNodeID);
                if (personalFolder != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("PersonalFolderEdit.aspx?PersonalFolderId={0}", personalFolder.PersonalFolderId, "编辑 - ")));
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
            if (this.tvPersonalFolder.SelectedNode != null)
            {
                var personalFolder = BLL.PersonalFolderService.GetPersonalFolderByID(this.tvPersonalFolder.SelectedNodeID);
                if (personalFolder != null && BLL.PersonalFolderService.IsDeletePersonalFolder(personalFolder.PersonalFolderId))
                {
                    BLL.PersonalFolderService.DeletePersonalFolderByID(personalFolder.PersonalFolderId);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除个人文件夹设置信息",this.tvPersonalFolder.SelectedNode.Text);
                    PersonalFolderDataBind();
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

        #region 增加、修改、删除个人文件夹明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (tvPersonalFolder.SelectedNode != null)
            {
                var personalFolder = BLL.PersonalFolderService.GetPersonalFolderByID(this.tvPersonalFolder.SelectedNodeID);
                if (personalFolder != null && personalFolder.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonalFolderItemEdit.aspx?PersonalFolderId={0}", tvPersonalFolder.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择最后一级节点添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择个人文件夹！", MessageBoxIcon.Warning);
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonalFolderItemEdit.aspx?PersonalFolderItemId={0}", Grid1.SelectedRowID, "修改 - ")));            
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
                    var item = BLL.PersonalFolderItemService.GetPersonalFolderItemByID(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.PersonalFolderItemService.DeletePersonalFolderItemByID(item.PersonalFolderItemId);
                        BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除个人文件明细信息", item.Code);  
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
            PersonalFolderDataBind();
        }
        #endregion      
    }
}