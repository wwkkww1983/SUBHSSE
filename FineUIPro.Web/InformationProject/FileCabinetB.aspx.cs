namespace FineUIPro.Web.InformationProject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BLL;

    public partial class FileCabinetB : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                BLL.ProjectService.InitAllProjectDropDownList(this.drpProject, false);
                if (this.drpProject.Items.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
                    {
                        this.drpProject.SelectedValue = this.CurrUser.LoginProjectId;
                        this.drpProject.Enabled = false;
                    }

                    this.BindLeftTree();
                }
            }          
        }
        #endregion        
        
        /// <summary>
        /// 加载树
        /// </summary>
        private void BindLeftTree()
        {
            this.leftTree.Nodes.Clear();            
            if (!string.IsNullOrEmpty(this.drpProject.SelectedValue))
            {
                var sysMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(BLL.Const.Menu_Project);
                if (sysMenu.Count() > 0)
                {
                    this.InitTreeMenu(sysMenu, null);
                }
            }
        }
        
        #region 加载菜单下拉框树
        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeMenu(List<Model.Sys_Menu> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            {
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SuperMenu == supMenu).OrderBy(x => x.SortIndex);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    var codeTemplateRule = Funs.DB.Sys_CodeTemplateRule.FirstOrDefault(x => x.MenuId == item.MenuId && x.IsFileCabinetB == false);
                    if (codeTemplateRule == null)
                    {
                        var noMenu = BLL.Const.noSysSetMenusList.FirstOrDefault(x => x == item.MenuId);
                        if (noMenu == null)
                        {
                            TreeNode newNode = new TreeNode
                            {
                                Text = item.MenuName,
                                NodeID = item.MenuId
                            };
                            if (item.IsEnd == true)
                            {
                                newNode.NavigateUrl = "../" + item.Url + "?value=0&projectId=" + this.drpProject.SelectedValue;
                            }
                            newNode.Target = "mainframe";
                            if (node == null)
                            {
                                this.leftTree.Nodes.Add(newNode);
                            }
                            else
                            {
                                node.Nodes.Add(newNode);
                            }
                            if (!item.IsEnd.HasValue || item.IsEnd == false)
                            {
                                InitTreeMenu(menusList, newNode);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 项目下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {            
            this.BindLeftTree();            
        }
    }
}