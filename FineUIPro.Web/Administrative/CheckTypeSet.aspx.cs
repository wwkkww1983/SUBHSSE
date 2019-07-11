using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class CheckTypeSet : PageBase
    {
        #region 定义项
        /// <summary>
        /// 检查类别ID
        /// </summary>
        public string CheckTypeCode
        {
            get
            {
                return (string)ViewState["CheckTypeCode"];
            }
            set
            {
                ViewState["CheckTypeCode"] = value;
            }
        }

        /// <summary>
        /// 上级节点ID
        /// </summary>
        public string SupItem
        {
            get
            {
                return (string)ViewState["SupItem"];
            }
            set
            {
                ViewState["SupItem"] = value;
            }
        }

        /// <summary>
        /// 操作状态，是增加，修改，还是删除
        /// </summary>
        public string OperateState
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        #endregion

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
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
                this.CheckTypeSetDataBind();
            }
        }

        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckTypeSetDataBind()
        {
            this.tvCheckTypeSet.Nodes.Clear();

            TreeNode rootNode = new TreeNode
            {
                Text = "检查类别",
                NodeID = "0",
                EnableClickEvent = true
            };//定义根节点

            this.tvCheckTypeSet.Nodes.Add(rootNode);
            this.GetNodes(rootNode.Nodes, null);
        }
        #endregion

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.Administrative_CheckTypeSet> checkTypeSet = null;
            if (parentId == null)
            {
                checkTypeSet = (from x in BLL.Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == "0" orderby x.SortIndex select x).ToList();
            }
            else
            {
                checkTypeSet = (from x in BLL.Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == parentId orderby x.SortIndex select x).ToList();
            }

            foreach (var q in checkTypeSet)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = q.CheckTypeContent,
                    NodeID = q.CheckTypeCode,
                    EnableClickEvent = true
                };
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion

        #region TreeView点击事件
        /// <summary>
        /// TreeView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvCheckTypeSet_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            ResetInterface();
            this.TextIsReadOnly(false);
            this.ButtonIsEnabled(false);
            OperateState = "修改";

            if (this.tvCheckTypeSet.SelectedNode.NodeID != "0")
            {
                this.SupItem = this.tvCheckTypeSet.SelectedNode.ParentNode.NodeID;
            }
            else
            {
                this.SupItem = null;
            }

            CheckTypeCode = this.tvCheckTypeSet.SelectedNode.NodeID;

            Model.Administrative_CheckTypeSet checkTypeSet = BLL.CheckTypeSetService.GetCheckTypeSetByCheckTypeCode(CheckTypeCode);
            if (checkTypeSet != null)
            {
                this.txtCheckTypeCode.Text = checkTypeSet.CheckTypeCode;
                this.txtCheckTypeContent.Text = checkTypeSet.CheckTypeContent;
                this.txtSortIndex.Text = checkTypeSet.SortIndex.ToString();
                this.drpIsEndLevel.SelectedValue = checkTypeSet.IsEndLevel.ToString();
            }
            else
            {
                this.txtCheckTypeContent.Text = "检查类别顶级";
                this.drpIsEndLevel.SelectedValue = "False";
            }
        }
        #endregion

        #region 重置界面
        /// <summary>
        /// 重置界面
        /// </summary>
        private void ResetInterface()
        {
            this.txtCheckTypeCode.Text = string.Empty;
            this.txtCheckTypeContent.Text = string.Empty;
            this.txtSortIndex.Text = string.Empty;
            this.drpIsEndLevel.SelectedIndex = 1;
        }
        #endregion

        #region 增加
        /// <summary>
       /// 增加按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (CheckTypeCode != null)
            {
                if (BLL.CheckTypeSetService.IsEndLevel(CheckTypeCode) == false)
                {
                    this.OperateState = "增加";
                    this.ButtonIsEnabled(false);
                    this.TextIsReadOnly(false);
                    ResetInterface();
                    this.txtCheckTypeCode.Focus();
                }
                else
                {
                    Alert.ShowInTop("检查类别为末级，不能再向下添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择节点项！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Administrative_CheckTypeSet checkTypeSet = new Model.Administrative_CheckTypeSet
            {
                CheckTypeCode = this.txtCheckTypeCode.Text.Trim(),
                CheckTypeContent = this.txtCheckTypeContent.Text.Trim(),
                SortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim()),
                IsEndLevel = Convert.ToBoolean(this.drpIsEndLevel.SelectedValue.Trim())
            };
            if (this.OperateState == "增加")
            {
                Model.Administrative_CheckTypeSet cts = BLL.CheckTypeSetService.GetCheckTypeSetByCheckTypeCode(this.txtCheckTypeCode.Text.Trim());
                if (cts != null)
                {
                    Alert.ShowInTop("检查类别编码已经存在！", MessageBoxIcon.Warning);
                    return;
                }
                checkTypeSet.SupCheckTypeCode = this.CheckTypeCode;
                BLL.CheckTypeSetService.AddCheckTypeSet(checkTypeSet);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtCheckTypeCode.Text, this.CheckTypeCode, BLL.Const.CheckTypeSetMenuId, BLL.Const.BtnAdd);
            }
            if (this.OperateState == "修改")
            {
                checkTypeSet.SupCheckTypeCode = SupItem;
                if (this.drpIsEndLevel.SelectedValue == "True")
                {
                    int count = BLL.CheckTypeSetService.GetCheckTypeSetCountBySupCheckTypeCode(this.CheckTypeCode);
                    if (count > 0)
                    {
                        Alert.ShowInTop("你选择的节点有对应的子节点，不能设为末节点！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                checkTypeSet.CheckTypeCode = this.CheckTypeCode;
                BLL.CheckTypeSetService.UpdateCheckTypeSet(checkTypeSet);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtCheckTypeCode.Text, this.CheckTypeCode, BLL.Const.CheckTypeSetMenuId, BLL.Const.BtnModify);

            }
            // 重新绑定表格，并点击当前编辑或者新增的行
            CheckTypeSetDataBind();
            this.tvCheckTypeSet.ExpandAllNodes();
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
            ResetInterface();
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
            if (this.CheckTypeCode != null && this.tvCheckTypeSet.SelectedNode.NodeID != "0")
            {
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtCheckTypeCode.Text, this.CheckTypeCode, BLL.Const.CheckTypeSetMenuId, BLL.Const.BtnDelete);
                DeleteCheckTypeSets(CheckTypeCode);
                CheckTypeSetDataBind();
                this.tvCheckTypeSet.ExpandAllNodes();
                ResetInterface();
                //Alert.Show("删除成功！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("请选择要删除的节点项！", MessageBoxIcon.Warning);
                return;
            }
            CheckTypeCode = null;
            SupItem = null;
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
        }

        /// <summary>
        /// 递归删除
        /// </summary>
        /// <param name="checkItemSetId"></param>
        private void DeleteCheckTypeSets(string checkTypeCode)
        {
            int count = BLL.CheckTypeSetService.GetCheckTypeSetCountBySupCheckTypeCode(checkTypeCode);
            if (count > 0)
            {
                List<string> checkTypeCodes = BLL.CheckTypeSetService.GetCheckTypeCodesBySupCheckTypeCode(checkTypeCode);
                foreach (string piId in checkTypeCodes)
                {
                    DeleteCheckTypeSets(piId);
                }
            }
            BLL.CheckTypeSetService.DeleteCheckTypeSetById(checkTypeCode);
        }
        #endregion

        #region 文本框是否可编辑
        /// <summary>
        /// 文本框是否可编辑、按钮是否可用
        /// </summary>
        /// <param name="readOnly">布尔值</param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtCheckTypeCode.Readonly = readOnly;
            this.txtCheckTypeContent.Readonly = readOnly;
            this.txtSortIndex.Readonly = readOnly;
            this.drpIsEndLevel.Enabled = !readOnly;
        }

        /// <summary>
        /// 增加，修改，删除,保存,取消按钮是否可用
        /// </summary>
        /// <param name="enabled">布尔值</param>
        private void ButtonIsEnabled(bool enabled)
        {
            this.btnNew.Enabled = !enabled;           
            this.btnSave.Enabled = !enabled;
        }
        #endregion
    }
}