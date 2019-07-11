using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class HAZOPUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 单位id
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string HAZOPId
        {
            get
            {
                return (string)ViewState["HAZOPId"];
            }
            set
            {
                ViewState["HAZOPId"] = value;
            }
        }
        #endregion

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
                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null)
                {
                    this.hdUnitId.Text = unit.UnitId;
                    this.txtUnitName.Text = unit.UnitName;
                }
                this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
                var hazop = BLL.HAZOPService.GetHAZOPByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, hazop);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_HAZOP> hazop)
        {
            List<Model.Technique_HAZOP> chidLaw = new List<Model.Technique_HAZOP>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = hazop.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = hazop.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = hazop.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.HAZOPTitle,
                        NodeID = item.HAZOPId,
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
            this.HAZOPId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.HAZOPService.GetHAZOPById(this.HAZOPId);
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

                    if (!string.IsNullOrEmpty(q.UnitId))
                    {
                        var u = BLL.UnitService.GetUnitByUnitId(q.UnitId);
                        if (u != null)
                        {
                            this.hdUnitId.Text = u.UnitId;
                            this.txtUnitName.Text = u.UnitName;
                        }
                    }
                    this.txtTitle.Text = q.HAZOPTitle;
                    this.txtAbstract.Text = q.Abstract;
                    if (q.HAZOPDate != null)
                    {
                        this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", q.HAZOPDate);
                    }
                    //if (!string.IsNullOrEmpty(q.AttachUrl))
                    //{
                    //    this.FullAttachUrl = q.AttachUrl;
                    //this.lbAttachUrl.Text = q.AttachUrl.Substring(q.AttachUrl.IndexOf("~") + 1);
                    //}
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
            var hazop = BLL.HAZOPService.GetHAZOPById(this.HAZOPId);
            if (hazop != null && !hazop.AuditDate.HasValue)
            {
                BLL.HAZOPService.DeleteHAZOPById(this.HAZOPId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！", MessageBoxIcon.Success);
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
            SaveData();
            this.InitTreeMenu();
            ShowNotify("保存成功！", MessageBoxIcon.Success);
        }

        private void SaveData()
        {
            Model.Technique_HAZOP hazop = new Model.Technique_HAZOP
            {
                UnitId = this.hdUnitId.Text,
                HAZOPTitle = this.txtTitle.Text.Trim(),
                Abstract = this.txtAbstract.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkHAZOPDate.Text.Trim()))
            {
                hazop.HAZOPDate = Convert.ToDateTime(this.dpkHAZOPDate.Text.Trim());
            }
            //hazop.AttachUrl = this.FullAttachUrl;
            hazop.CompileMan = this.CurrUser.UserName;
            hazop.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            hazop.IsPass = null;
            if (string.IsNullOrEmpty(HAZOPId))
            {
                this.HAZOPId = hazop.HAZOPId = SQLHelper.GetNewID(typeof(Model.Technique_HAZOP));
                BLL.HAZOPService.AddHAZOP(hazop);
                BLL.LogService.AddSys_Log(this.CurrUser, hazop.HAZOPTitle, hazop.HAZOPId, BLL.Const.HAZOPMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                hazop.HAZOPId = this.HAZOPId;
                BLL.HAZOPService.UpdateHAZOP(hazop);
                BLL.LogService.AddSys_Log(this.CurrUser, hazop.HAZOPTitle, hazop.HAZOPId, BLL.Const.HAZOPMenuId, BLL.Const.BtnModify);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HAZOPId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HAZOP&menuId=41C22E63-36B7-4C44-89EC-F765BFBB7C55&type=0", HAZOPId)));
        }
        #endregion

        #region 验证标题是否存在
        /// <summary>
        /// 验证标题是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_HAZOP.FirstOrDefault(x => x.HAZOPTitle == this.txtTitle.Text.Trim() && (x.HAZOPId != this.HAZOPId || (this.HAZOPId == null && x.HAZOPId != null)));
            if (q != null)
            {
                ShowNotify("输入的标题已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTemp()
        {
            this.txtTitle.Focus();
            this.HAZOPId = string.Empty;
            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            this.txtTitle.Text = string.Empty;
            this.txtAbstract.Text = string.Empty;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HAZOPMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }

                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    //this.btnDeleteFile.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                }
            }
        }
        #endregion
    }
}