using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyData : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                this.SafetyDataDataBind();//加载树
                this.GetButtonPower();
            }
        }
              
        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void SafetyDataDataBind()
        {
            this.tvSafetyData.Nodes.Clear();
            this.tvSafetyData.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "企业安全管理资料",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvSafetyData.Nodes.Add(rootNode);
            var safetyDataList = BLL.SafetyDataService.GetSafetyDataList();
            if (!this.cbType.Items[0].Selected && this.cbType.Items[1].Selected)
            {
                safetyDataList = safetyDataList.Where(x => x.IsCheck == true).ToList();
            }

            if (safetyDataList.Count() > 0)
            {
                this.GetNodes(safetyDataList, rootNode);
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
                TreeNode newNode = new TreeNode
                {
                    Text = item.Code + "：" + item.Title
                };
                if (item.IsEndLever == true )
                {
                    if (!string.IsNullOrEmpty(item.MenuId))
                    {
                        var menu = BLL.SysMenuService.GetSysMenuByMenuId(item.MenuId);
                        {
                            if (menu != null)
                            {
                                newNode.Text += "  【匹配菜单：" + menu.MenuName + "】";
                            }
                        }
                    }
                    else
                    {
                        newNode.Text += "  【未匹配菜单传定稿文件】";
                    }

                    if (item.IsCheck == true)
                    {
                        if (item.Score.HasValue)
                        {
                            newNode.Text += "【分值：" + item.Score.ToString() + "】";
                        }

                        if (!string.IsNullOrEmpty(item.CheckType))
                        {
                            if (item.CheckType == BLL.Const.SafetyDataCheckType_1)
                            {
                                newNode.Text += "  【月报】";
                            }
                            else if (item.CheckType == BLL.Const.SafetyDataCheckType_2)
                            {
                                newNode.Text += "  【季报】";
                            }
                            else if (item.CheckType == BLL.Const.SafetyDataCheckType_3)
                            {
                                newNode.Text += "  【定时报】";
                            }
                            else if (item.CheckType == BLL.Const.SafetyDataCheckType_4)
                            {
                                newNode.Text += "  【开工后报】";
                            }
                            else if (item.CheckType == BLL.Const.SafetyDataCheckType_5)
                            {
                                newNode.Text += "  【半年报】";
                            }
                            else if (item.CheckType == BLL.Const.SafetyDataCheckType_6)
                            {
                                newNode.Text += "  【其他】";
                            }

                            newNode.Text += "  【考核项】";                            
                        }
                    }
                    else
                    {
                        newNode.Text += "  【非考核项】";
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
                newNode.EnableClickEvent = false;
                node.Nodes.Add(newNode);
                if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                {
                    GetNodes(SafetyDataList, newNode);
                }
            }
        }
        #endregion
        #endregion

        #region 右键增加、修改、删除企业安全管理资料方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            if (this.tvSafetyData.SelectedNode != null)
            {
                var SafetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.tvSafetyData.SelectedNodeID);
                if (SafetyData != null && SafetyData.IsEndLever == true)
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                    return;                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataEdit.aspx?SupSafetyDataId={0}", this.tvSafetyData.SelectedNodeID, "新增 - ")));                    
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
            if (this.tvSafetyData.SelectedNode != null && this.tvSafetyData.SelectedNodeID != "0")
            {
                var SafetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.tvSafetyData.SelectedNodeID);
                if (SafetyData != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataEdit.aspx?SafetyDataId={0}", SafetyData.SafetyDataId, "编辑 - ")));
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
            if (this.tvSafetyData.SelectedNode != null)
            {
                var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.tvSafetyData.SelectedNodeID);               
                if (safetyData != null && BLL.SafetyDataService.IsDeleteSafetyData(safetyData.SafetyDataId))
                {
                    BLL.SafetyDataService.DeleteSafetyDataByID(safetyData.SafetyDataId);                    
                    this.SafetyDataDataBind();
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

        #region 关闭弹出窗口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            SafetyDataDataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerSafetyDataMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnMenuNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuModify.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDel.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 文件来源事件变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvSafetyData.SelectedNodeID = string.Empty;
            this.SafetyDataDataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.SafetyDataDataBind();//加载树
        }
    }
}