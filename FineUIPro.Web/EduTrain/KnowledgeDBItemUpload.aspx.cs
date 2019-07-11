using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class KnowledgeDBItemUpload : PageBase
    {
        public string KnowledgeItemId
        {
            get
            {
                return (string)ViewState["KnowledgeItemId"];
            }
            set
            {
                ViewState["KnowledgeItemId"] = value;
            }
        }

        public string KnowledgeId
        {
            get
            {
                return (string)ViewState["KnowledgeId"];
            }
            set
            {
                ViewState["KnowledgeId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.InitTreeMenu();

                this.KnowledgeId = Request.Params["KnowledgeId"];
               
                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            tvUploadResources.Nodes.Clear();

            ///加载当前人
            TreeNode rootNode = new TreeNode
            {
                Text = this.CurrUser.UserName,
                NodeID = this.CurrUser.UserId,
                Expanded = true
            };
            this.tvUploadResources.Nodes.Add(rootNode);
            ////加载上传资源的状态
            var uploadType = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_UploadResources);
            if (uploadType.Count() > 0)
            {
                var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, knowledgeItem);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Training_KnowledgeItem> knowledgeItem)
        {
            List<Model.Training_KnowledgeItem> chidLaw = new List<Model.Training_KnowledgeItem>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = knowledgeItem.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = knowledgeItem.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = knowledgeItem.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.KnowledgeItemName,
                        NodeID = item.KnowledgeItemId,
                        EnableClickEvent = true
                    };
                    nodes.Add(gChidNode);
                }
            }
        }
        #endregion

        #region 树节点选择
        /// <summary>
        ///  树节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvUploadResources_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            this.SetTemp();
            this.KnowledgeItemId = this.tvUploadResources.SelectedNode.NodeID;
            var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(this.KnowledgeItemId);
            if (knowledgeItem != null)
            {
                if (knowledgeItem.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
                if (knowledgeItem != null)
                {
                    this.txtKnowledgeItemCode.Text = knowledgeItem.KnowledgeItemCode;
                    this.txtKnowledgeItemName.Text = knowledgeItem.KnowledgeItemName;
                    this.txtRemark.Text = knowledgeItem.Remark;
                }
            }
        }
        #endregion

        #region 增加
        /// <summary>
        /// 增加上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(this.KnowledgeItemId);
            if (knowledgeItem != null && !knowledgeItem.AuditDate.HasValue)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnDelete);
                BLL.KnowledgeItemService.DeleteKnowledgeItem(this.KnowledgeItemId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
            }
        }

        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTemp()
        {
            this.txtKnowledgeItemCode.Focus();
            this.KnowledgeItemId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtKnowledgeItemCode.Text = string.Empty;
            this.txtKnowledgeItemName.Text = string.Empty;
            this.txtRemark.Text = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }

        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_KnowledgeItem knowledgeItem = new Model.Training_KnowledgeItem
            {
                KnowledgeItemCode = this.txtKnowledgeItemCode.Text.Trim(),
                KnowledgeItemName = this.txtKnowledgeItemName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(this.KnowledgeItemId))
            {
                knowledgeItem.CompileMan = this.CurrUser.UserName;
                knowledgeItem.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                knowledgeItem.CompileDate = DateTime.Now;
                knowledgeItem.IsPass = null;
                knowledgeItem.KnowledgeId = this.KnowledgeId;
                knowledgeItem.KnowledgeItemId = SQLHelper.GetNewID(typeof(Model.Training_KnowledgeItem));
                BLL.KnowledgeItemService.AddKnowledgeItem(knowledgeItem);
                BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var k = BLL.KnowledgeItemService.GetKnowledgeItemById(this.KnowledgeItemId);
                if (k!=null)
                {
                    knowledgeItem.KnowledgeId = k.KnowledgeId;
                }
                knowledgeItem.KnowledgeItemId = this.KnowledgeItemId;
                BLL.KnowledgeItemService.UpdateKnowledgeItem(knowledgeItem);
                BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnModify);
            }
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.KnowledgeDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证应知应会库名称是否存在
        /// <summary>
        /// 验证应知应会库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_KnowledgeItem.FirstOrDefault(x => x.KnowledgeId == this.KnowledgeId && x.KnowledgeItemName == this.txtKnowledgeItemName.Text.Trim() && (x.KnowledgeItemId != this.KnowledgeItemId || (this.KnowledgeItemId == null && x.KnowledgeItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}