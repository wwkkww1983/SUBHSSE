using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.Law
{
    public partial class ManageRuleUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ManageRuleId
        {
            get
            {
                return (string)ViewState["ManageRuleId"];
            }
            set
            {
                ViewState["ManageRuleId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
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
                this.GetButtonPower();//设置参数
                this.InitTreeMenu();

                //加载管理规定类别下拉选项
                this.ddlManageRuleTypeId.DataTextField = "ManageRuleTypeName";
                this.ddlManageRuleTypeId.DataValueField = "ManageRuleTypeId";
                this.ddlManageRuleTypeId.DataSource = BLL.ManageRuleTypeService.GetManageRuleTypeList();
                ddlManageRuleTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlManageRuleTypeId);

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
                var rule = BLL.ManageRuleService.GetManageRuleByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, rule);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Law_ManageRule> rules)
        {
            List<Model.Law_ManageRule> chidRule = new List<Model.Law_ManageRule>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidRule = rules.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidRule = rules.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidRule = rules.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidRule.Count() > 0)
            {
                foreach (var item in chidRule)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.ManageRuleName,
                        NodeID = item.ManageRuleId,
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
            this.ManageRuleId = this.tvUploadResources.SelectedNode.NodeID;
            var manageRule = BLL.ManageRuleService.GetManageRuleById(this.ManageRuleId);
            if (manageRule != null)
            {
                if (manageRule.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }

                if (manageRule != null)
                {
                    this.txtManageRuleCode.Text = manageRule.ManageRuleCode;
                    this.txtManageRuleName.Text = manageRule.ManageRuleName;
                    if (!string.IsNullOrEmpty(manageRule.ManageRuleTypeId))
                    {
                        this.ddlManageRuleTypeId.SelectedValue = manageRule.ManageRuleTypeId;
                    }
                    this.txtVersionNo.Text = manageRule.VersionNo;
                    this.txtCompileMan.Text = manageRule.CompileMan;
                    if (manageRule.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", manageRule.CompileDate);
                    }
                    this.txtRemark.Text = manageRule.Remark;
                    if (!string.IsNullOrEmpty(manageRule.AttachUrl))
                    {
                        this.FullAttachUrl = manageRule.AttachUrl;
                        this.lbAttachUrl.Text = manageRule.AttachUrl.Substring(manageRule.AttachUrl.IndexOf("~") + 1);
                    }
                }
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        private void SaveData()
        {
            Model.Law_ManageRule manageRule = new Model.Law_ManageRule
            {
                ManageRuleCode = this.txtManageRuleCode.Text.Trim(),
                ManageRuleName = this.txtManageRuleName.Text.Trim()
            };
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                manageRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            manageRule.VersionNo = this.txtVersionNo.Text.Trim();
            manageRule.CompileMan = this.CurrUser.UserName;
            manageRule.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            manageRule.IsPass = null;
            manageRule.AttachUrl = this.FullAttachUrl;
            manageRule.Remark = this.txtRemark.Text.Trim();
            manageRule.UnitId = this.CurrUser.UnitId;
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                this.ManageRuleId = manageRule.ManageRuleId = SQLHelper.GetNewID(typeof(Model.Law_ManageRule));
                BLL.ManageRuleService.AddManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId,BLL.Const.ManageRuleMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                manageRule.ManageRuleId = this.ManageRuleId;
                BLL.ManageRuleService.UpdateManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnModify);
            }
        }
        #endregion

        #region 增加按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }
        #endregion

        #region 删除按钮
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var law = BLL.ManageRuleService.GetManageRuleById(this.ManageRuleId);
            if (law != null && !law.AuditDate.HasValue)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, law.ManageRuleCode, law.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnDelete);
                BLL.ManageRuleService.DeleteManageRuleById(this.ManageRuleId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
            }
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.ManageRuleId = string.Empty;
            this.txtManageRuleCode.Text = string.Empty;
            this.txtManageRuleName.Text = string.Empty;
            this.ddlManageRuleTypeId.SelectedValue = "null";
            this.txtVersionNo.Text = string.Empty;
            this.txtRemark.Text = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion        

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageRuleMenuId);
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

        #region 验证管理规定名称是否存在
        /// <summary>
        /// 验证管理规定名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_ManageRule.FirstOrDefault(x =>x.ManageRuleName == this.txtManageRuleName.Text.Trim() && (x.ManageRuleId != this.ManageRuleId || (this.ManageRuleId == null && x.ManageRuleId != null)));
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion


        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId=56960940-81A8-43D1-9565-C306EC7AFD12", ManageRuleId)));
        }
    }
}