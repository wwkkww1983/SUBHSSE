using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class RectifyUpload : PageBase
    {
        #region 定义变量
        public string RectifyItemId
        {
            get
            {
                return (string)ViewState["RectifyItemId"];
            }
            set
            {
                ViewState["RectifyItemId"] = value;
            }
        }

        public string RectifyId
        {
            get
            {
                return (string)ViewState["RectifyId"];
            }
            set
            {
                ViewState["RectifyId"] = value;
            }
        }
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();

                this.RectifyId = Request.Params["RectifyId"];
                if (!string.IsNullOrEmpty(this.RectifyId))
                {
                    var rectify = BLL.RectifyService.GetRectifyById(this.RectifyId);
                    if (rectify != null)
                    {
                        this.lblRectifyName.Text = rectify.RectifyName;
                    }
                }
                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }
        #endregion

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
                var rectifyItem = BLL.RectifyItemService.GetRectifyItemByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, rectifyItem);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_RectifyItem> hazardList)
        {
            List<Model.Technique_RectifyItem> chidLaw = new List<Model.Technique_RectifyItem>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = hazardList.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = hazardList.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = hazardList.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.HazardSourcePoint,
                        NodeID = item.RectifyItemId,
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
            this.RectifyItemId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.RectifyItemService.GetRectifyItemById(this.RectifyItemId);
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
                    var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(this.RectifyItemId);
                    if (rectifyItem != null)
                    {
                        if (!string.IsNullOrEmpty(rectifyItem.RectifyId))
                        {
                            var rectify = BLL.RectifyService.GetRectifyById(rectifyItem.RectifyId);
                            if (rectify != null)
                            {
                                this.lblRectifyName.Text = rectify.RectifyName;
                            }
                        }
                        this.txtHazardSourcePoint.Text = rectifyItem.HazardSourcePoint;
                        this.txtRiskAnalysis.Text = rectifyItem.RiskAnalysis;
                        this.txtRiskPrevention.Text = rectifyItem.RiskPrevention;
                        this.txtSimilarRisk.Text = rectifyItem.SimilarRisk;
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
            var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(this.RectifyItemId);
            if (rectifyItem != null && !rectifyItem.AuditDate.HasValue)
            {
                BLL.RectifyItemService.DeleteRectifyItemByRectifyId(this.RectifyItemId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！",MessageBoxIcon.Success);
            }
        }
        #endregion        

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Technique_RectifyItem rectifyItem = new Model.Technique_RectifyItem
            {
                HazardSourcePoint = this.txtHazardSourcePoint.Text.Trim(),
                RiskAnalysis = this.txtRiskAnalysis.Text.Trim(),
                RiskPrevention = this.txtRiskPrevention.Text.Trim(),
                SimilarRisk = this.txtSimilarRisk.Text.Trim(),
                CompileMan = this.CurrUser.UserName,
                UnitId = CommonService.GetUnitId(this.CurrUser.UnitId),
                CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim()),
                IsPass = null
            };
            if (string.IsNullOrEmpty(this.RectifyItemId))
            {
                rectifyItem.RectifyItemId = SQLHelper.GetNewID(typeof(Model.Technique_RectifyItem));
                rectifyItem.RectifyId = this.RectifyId;
                BLL.RectifyItemService.AddRectifyItem(rectifyItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全隐患");
            }
            else
            {
                rectifyItem.RectifyItemId = this.RectifyItemId;
                Model.Technique_RectifyItem r = BLL.RectifyItemService.GetRectifyItemById(this.RectifyId);
                if (r != null)
                {
                    rectifyItem.RectifyId = r.RectifyId;
                }
                BLL.RectifyItemService.UpdateRectifyItem(rectifyItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全隐患");
            }
            this.InitTreeMenu();
            ShowNotify("保存成功！", MessageBoxIcon.Success);
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtHazardSourcePoint.Focus();
            this.RectifyItemId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtHazardSourcePoint.Text = string.Empty;
            this.txtRiskAnalysis.Text = string.Empty;
            this.txtRiskPrevention.Text = string.Empty;
            this.txtSimilarRisk.Text = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RectifyMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}