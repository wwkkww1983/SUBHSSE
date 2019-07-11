using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataE : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                this.SafetyDataEDataBind();//加载树
                this.GetButtonPower();
            }
        }
              
        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void SafetyDataEDataBind()
        {
            this.tvSafetyDataE.Nodes.Clear();
            this.tvSafetyDataE.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "设计项目资料目录",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvSafetyDataE.Nodes.Add(rootNode);
            var SafetyDataEList = BLL.SafetyDataEService.GetSafetyDataEList();
            if (this.cbType.Items[1].Selected)
            {
                SafetyDataEList = SafetyDataEList.Where(x => x.IsCheck == true).ToList();
            }
            if (this.cbType.Items[2].Selected)
            {
                SafetyDataEList = SafetyDataEList.Where(x =>  !x.IsCheck.HasValue || x.IsCheck == false).ToList();
            }

            if (SafetyDataEList.Count() > 0)
            {
                this.GetNodes(SafetyDataEList, rootNode);
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(List<Model.SafetyDataE_SafetyDataE> SafetyDataEList, TreeNode node)
        {
            var SafetyDataEs = SafetyDataEList.Where(x => x.SupSafetyDataEId == node.NodeID);
            foreach (var item in SafetyDataEs)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = item.Code + "：" + item.Title
                };
                if (item.IsEndLever == true )
                {
                    if (item.Score.HasValue)
                    {
                        newNode.Text += "【分值：" + item.Score.ToString() + "】";
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

                newNode.NodeID = item.SafetyDataEId;
                newNode.EnableClickEvent = false;
                node.Nodes.Add(newNode);
                if (!item.IsEndLever.HasValue || item.IsEndLever == false)
                {
                    GetNodes(SafetyDataEList, newNode);
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
            if (this.tvSafetyDataE.SelectedNode != null)
            {
                var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.tvSafetyDataE.SelectedNodeID);
                if (SafetyDataE != null && SafetyDataE.IsEndLever == true)
                {
                    Alert.ShowInTop("选择的项已是末级！", MessageBoxIcon.Warning);
                    return;                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataEEdit.aspx?SupSafetyDataEId={0}", this.tvSafetyDataE.SelectedNodeID, "新增 - ")));                    
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
            if (this.tvSafetyDataE.SelectedNode != null && this.tvSafetyDataE.SelectedNodeID != "0")
            {
                var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.tvSafetyDataE.SelectedNodeID);
                if (SafetyDataE != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataEEdit.aspx?SafetyDataEId={0}", SafetyDataE.SafetyDataEId, "编辑 - ")));
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
            if (this.tvSafetyDataE.SelectedNode != null)
            {
                var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.tvSafetyDataE.SelectedNodeID);               
                if (SafetyDataE != null && BLL.SafetyDataEService.IsDeleteSafetyDataE(SafetyDataE.SafetyDataEId))
                {                  
                    BLL.SafetyDataEService.DeleteSafetyDataEByID(SafetyDataE.SafetyDataEId);                    
                    this.SafetyDataEDataBind();
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
            SafetyDataEDataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerSafetyDataEMenuId);
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
            this.tvSafetyDataE.SelectedNodeID = string.Empty;
            this.SafetyDataEDataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.SafetyDataEDataBind();//加载树
        }
    }
}