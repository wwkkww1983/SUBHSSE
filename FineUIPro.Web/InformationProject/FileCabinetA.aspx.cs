using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class FileCabinetA : PageBase
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
                }

                this.FileCabinetADataBind();//加载树
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
            this.FileCabinetADataBind();//加载树
        }

        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvFileCabinetA.SelectedNodeID))
            {
                string strSql = @"SELECT Item.FileCabinetAItemId,Item.FileCabinetAId,(CASE WHEN Item.IsMenu =1 THEN CodeRecords.Code ELSE Item.Code END)  AS Code,Item.Title,Item.CompileMan,UserName AS CompileManName,Item.CompileDate,Item.Remark,Item.AttachUrl"
                              + @" FROM dbo.InformationProject_FileCabinetAItem AS Item"
                              + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Item.FileCabinetAItemId= CodeRecords.DataId "
                              + @" LEFT JOIN Sys_User ON CompileMan=UserId WHERE FileCabinetAId=@FileCabinetAId";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@FileCabinetAId",this.tvFileCabinetA.SelectedNodeID)
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
        private void FileCabinetADataBind()
        {
            this.tvFileCabinetA.Nodes.Clear();
            this.tvFileCabinetA.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode();//定义根节点
            var project = BLL.ProjectService.GetProjectByProjectId(this.drpProject.SelectedValue);
            if (project != null)
            {
                rootNode.Text = "[" + project.ProjectName + "]";
            }
            else
            {
                rootNode.Text =  "文件柜A(集团文件柜类)";
            }
            rootNode.Expanded = true;
            rootNode.NodeID = "0";
            this.tvFileCabinetA.Nodes.Add(rootNode);
            var fileCabinetAList = BLL.FileCabinetAService.GetFileCabinetAListByProjectId(this.drpProject.SelectedValue);
            if (fileCabinetAList.Count() > 0)
            {
                this.GetNodes(fileCabinetAList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.InformationProject_FileCabinetA> fileCabinetAList, TreeNode node)
        {
            var FileCabinetAs = fileCabinetAList.Where(x => x.SupFileCabinetAId == node.NodeID);
            foreach (var item in FileCabinetAs)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = "[" + item.Code + "]" + item.Title,
                    NodeID = item.FileCabinetAId
                };
                if (item.IsEndLever == true)
                {
                    newNode.EnableClickEvent = true;
                }
                node.Nodes.Add(newNode);

                if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                {
                    GetNodes(fileCabinetAList, newNode);
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
        protected void tvFileCabinetA_NodeCommand(object sender, TreeCommandEventArgs e)
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

        #region 右键增加、修改、删除文件柜A(集团文件柜类)方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvFileCabinetA.SelectedNode != null)
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);
                if (fileCabinetA != null && fileCabinetA.IsEndLever == true)
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                    return;                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("FileCabinetAEdit.aspx?SupFileCabinetAId={0}&ProjectId={1}", this.tvFileCabinetA.SelectedNodeID,this.drpProject.SelectedValue, "新增 - ")));                    
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
            if (this.tvFileCabinetA.SelectedNode != null && this.tvFileCabinetA.SelectedNodeID != "0")
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);
                if (fileCabinetA != null)
                {
                    PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("FileCabinetAEdit.aspx?FileCabinetAId={0}&ProjectId={1}", fileCabinetA.FileCabinetAId,this.drpProject.SelectedValue, "编辑 - ")));
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
            if (this.tvFileCabinetA.SelectedNode != null)
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);               
                if (fileCabinetA != null && BLL.FileCabinetAService.IsDeleteFileCabinetA(fileCabinetA.FileCabinetAId))
                {
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除文件柜A(集团文件柜类)信息", this.tvFileCabinetA.SelectedNode.Text);
                    BLL.FileCabinetAService.DeleteFileCabinetAByID(fileCabinetA.FileCabinetAId);                    
                    this.FileCabinetADataBind();
                    Alert.ShowInTop("删除成功！",MessageBoxIcon.Success);
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

        #region 增加、修改、删除文件柜A(集团文件柜类)明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewDetail_Click(object sender, EventArgs e)
        {
            if (tvFileCabinetA.SelectedNode != null)
            {
                var fileCabinetA = BLL.FileCabinetAService.GetFileCabinetAByID(this.tvFileCabinetA.SelectedNodeID);
                if (fileCabinetA != null && fileCabinetA.IsEndLever == true)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("FileCabinetAItemEdit.aspx?FileCabinetAId={0}", tvFileCabinetA.SelectedNodeID, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择最后一级节点添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择文件柜A(集团文件柜类)！", MessageBoxIcon.Warning);
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

            var fileCabinetA = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(Grid1.SelectedRowID);
            if (fileCabinetA != null)
            {
                if (fileCabinetA.IsMenu == true)  ///是否菜单记录 
                {
                    if (!string.IsNullOrEmpty(fileCabinetA.Url))
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format(fileCabinetA.Url, Grid1.SelectedRowID), "查看", 1100, 620));
                    }
                }
                else
                {
                    if (this.btnMenuModifyDetail.Hidden)  ///是否具有操作权限
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("FileCabinetAItemView.aspx?FileCabinetAItemId={0}", Grid1.SelectedRowID, "查看 - ")));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("FileCabinetAItemEdit.aspx?FileCabinetAItemId={0}", Grid1.SelectedRowID, "修改 - ")));
                    }
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
                    var item = BLL.FileCabinetAItemService.GetFileCabinetAItemByID(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.FileCabinetAItemService.DeleteFileCabinetAItemByID(item.FileCabinetAItemId);
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
            FileCabinetADataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectFileCabinetAMenuId);
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