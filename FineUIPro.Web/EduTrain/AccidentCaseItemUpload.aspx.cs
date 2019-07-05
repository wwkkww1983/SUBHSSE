using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class AccidentCaseItemUpload : PageBase
    {
        #region 定义变量
        public string AccidentCaseItemId
        {
            get
            {
                return (string)ViewState["AccidentCaseItemId"];
            }
            set
            {
                ViewState["AccidentCaseItemId"] = value;
            }
        }

        public string AccidentCaseId
        {
            get
            {
                return (string)ViewState["AccidentCaseId"];
            }
            set
            {
                ViewState["AccidentCaseId"] = value;
            }
        }
        #endregion

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
                this.InitTreeMenu();

                this.AccidentCaseId = Request.Params["AccidentCaseId"];
                if (!string.IsNullOrEmpty(this.AccidentCaseId))
                {
                    var a = BLL.AccidentCaseService.GetAccidentCaseById(this.AccidentCaseId);
                    if (a != null)
                    {
                        this.txtAccidentCaseName.Text = a.AccidentCaseName;
                    }
                }
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
                var accidentCaseItem = BLL.AccidentCaseItemService.GetAccidentCaseItemByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, accidentCaseItem);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.EduTrain_AccidentCaseItem> accidentCaseItem)
        {
            List<Model.EduTrain_AccidentCaseItem> chidLaw = new List<Model.EduTrain_AccidentCaseItem>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = accidentCaseItem.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = accidentCaseItem.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = accidentCaseItem.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.AccidentName,
                        NodeID = item.AccidentCaseItemId,
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
            this.AccidentCaseItemId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
            if (q != null)
            {
                if (q.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
                if (q != null)
                {
                    this.txtActivities.Text = q.Activities;
                    this.txtAccidentName.Text = q.AccidentName;
                    this.txtAccidentProfiles.Text = q.AccidentProfiles;
                    this.txtAccidentReview.Text = q.AccidentReview;
                    if (!string.IsNullOrEmpty(q.AccidentCaseId))
                    {
                        var accidentCase = BLL.AccidentCaseService.GetAccidentCaseById(q.AccidentCaseId);
                        if (accidentCase != null)
                        {
                            this.txtAccidentCaseName.Text = accidentCase.AccidentCaseName;
                        }
                    }
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
            var accidentCaseItem = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
            if (accidentCaseItem != null && !accidentCaseItem.AuditDate.HasValue)
            {
                BLL.AccidentCaseItemService.DeleteAccidentCaseItemId(this.AccidentCaseItemId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
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
            Model.EduTrain_AccidentCaseItem accidentCaseItem = new Model.EduTrain_AccidentCaseItem
            {
                Activities = this.txtActivities.Text.Trim(),
                AccidentName = this.txtAccidentName.Text.Trim(),
                AccidentProfiles = this.txtAccidentProfiles.Text.Trim(),
                AccidentReview = this.txtAccidentReview.Text.Trim(),
                IsPass = null,
                CompileMan = this.CurrUser.UserName,
                UnitId = CommonService.GetUnitId(this.CurrUser.UnitId),
                CompileDate = Convert.ToDateTime(this.txtCompileDate.Text)
            };
            if (string.IsNullOrEmpty(this.AccidentCaseItemId))
            {
                accidentCaseItem.AccidentCaseId = this.AccidentCaseId;
                this.AccidentCaseItemId = accidentCaseItem.AccidentCaseItemId = SQLHelper.GetNewID(typeof(Model.EduTrain_AccidentCaseItem));
                BLL.AccidentCaseItemService.AddAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加事故案例库");
            }
            else
            {
                var t = BLL.AccidentCaseItemService.GetAccidentCaseItemById(this.AccidentCaseItemId);
                if (t != null)
                {
                    accidentCaseItem.AccidentCaseId = t.AccidentCaseId;
                }
                accidentCaseItem.AccidentCaseItemId = this.AccidentCaseItemId;
                BLL.AccidentCaseItemService.UpdateAccidentCaseItem(accidentCaseItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改事故案例库");
            }
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtActivities.Focus();
            this.AccidentCaseItemId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtActivities.Text = string.Empty;
            this.txtAccidentName.Text = string.Empty;
            this.txtAccidentProfiles.Text = string.Empty;
            this.txtAccidentReview.Text = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
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

        #region 验证事故名称是否存在
        /// <summary>
        /// 验证事故名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.EduTrain_AccidentCaseItem.FirstOrDefault(x => x.AccidentCaseId == this.AccidentCaseId && x.AccidentName == this.txtAccidentName.Text.Trim() && (x.AccidentCaseItemId != this.AccidentCaseItemId || (this.AccidentCaseItemId == null && x.AccidentCaseItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的事故名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}