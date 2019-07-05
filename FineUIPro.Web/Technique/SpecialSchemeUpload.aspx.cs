using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.Technique
{
    public partial class SpecialSchemeUpload :PageBase
    {
        #region 定义变量
        /// <summary>
        /// 专项方案
        /// </summary>
        public string SpecialSchemeId
        {
            get
            {
                return (string)ViewState["SpecialSchemeId"];
            }
            set
            {
                ViewState["SpecialSchemeId"] = value;
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
                this.InitTreeMenu();
                ////权限按钮方法
                this.GetButtonPower();
                //单位
                this.ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataSource = BLL.UnitService.GetUnitDropDownList();
                ddlUnit.DataBind();
                Funs.FineUIPleaseSelect(this.ddlUnit);
                //类型
                this.ddlSpecialSchemeType.DataTextField = "SpecialSchemeTypeName";
                ddlSpecialSchemeType.DataValueField = "SpecialSchemeTypeId";
                ddlSpecialSchemeType.DataSource = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeList();
                ddlSpecialSchemeType.DataBind();
                Funs.FineUIPleaseSelect(this.ddlSpecialSchemeType);

                //加载默认整理人、整理日期
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
                var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, specialScheme);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_SpecialScheme> emergency)
        {
            List<Model.Technique_SpecialScheme> chidLaw = new List<Model.Technique_SpecialScheme>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = emergency.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = emergency.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = emergency.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.SpecialSchemeName,
                        NodeID = item.SpecialSchemeId,
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
            this.SpecialSchemeId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.SpecialSchemeService.GetSpecialSchemeListById(this.SpecialSchemeId);
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
                    this.txtSpecialSchemeCode.Text = q.SpecialSchemeCode;
                    this.txtSpecialSchemeName.Text = q.SpecialSchemeName;
                    this.txtSummary.Text = q.Summary;
                    if (!string.IsNullOrEmpty(q.SpecialSchemeTypeId))
                    {
                        this.ddlSpecialSchemeType.SelectedValue = q.SpecialSchemeTypeId;
                    }
                    if (!string.IsNullOrEmpty(q.UnitId))
                    {
                        this.ddlUnit.SelectedValue = q.UnitId;
                    }                    
                    if (!string.IsNullOrEmpty(q.CompileMan))
                    {
                        this.txtCompileMan.Text = BLL.UserService.GetUserByUserId(q.CompileMan).UserName;
                    }
                    if (q.CompileDate!=null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
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
            var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(this.SpecialSchemeId);
            if (specialScheme != null && !specialScheme.AuditDate.HasValue)
            {
                BLL.SpecialSchemeService.DeleteSpecialSchemeListById(this.SpecialSchemeId);
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
            this.SaveData();
            InitTreeMenu();
            ShowNotify("保存成功!", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_SpecialScheme specialScheme = new Model.Technique_SpecialScheme();
            if (!string.IsNullOrEmpty(this.txtSpecialSchemeCode.Text.Trim()))
            {

                specialScheme.SpecialSchemeCode = this.txtSpecialSchemeCode.Text.Trim();
            }
            else
            {
                ShowNotify("请输入方案编号", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtSpecialSchemeName.Text.Trim()))
            {
                specialScheme.SpecialSchemeName = this.txtSpecialSchemeName.Text.Trim();
            }
            else
            {
                ShowNotify("请输入方案名称", MessageBoxIcon.Warning);
                return;
            }
            specialScheme.Summary = this.txtSummary.Text.Trim();
            if (this.ddlUnit.SelectedValue != Const._Null)
            {
                specialScheme.UnitId = this.ddlUnit.SelectedValue;
            }
            else
            {
                ShowNotify("请选择单位", MessageBoxIcon.Warning);
                return;
            }
            ////专项方案类型下拉框
            if (this.ddlSpecialSchemeType.SelectedValue != BLL.Const._Null || !string.IsNullOrEmpty(this.ddlSpecialSchemeType.Text))
            {
                var specialSchemeType = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeByName(this.ddlSpecialSchemeType.SelectedText);
                if (specialSchemeType != null)
                {
                    specialScheme.SpecialSchemeTypeId = specialSchemeType.SpecialSchemeTypeId;
                }
                else
                {
                    Model.Base_SpecialSchemeType newSpecialSchemeType = new Model.Base_SpecialSchemeType
                    {
                        SpecialSchemeTypeId = SQLHelper.GetNewID(typeof(Model.Base_SpecialSchemeType)),
                        SpecialSchemeTypeName = this.ddlSpecialSchemeType.Text
                    };
                    BLL.SpecialSchemeTypeService.AddSpecialSchemeType(newSpecialSchemeType);
                    specialScheme.SpecialSchemeTypeId = newSpecialSchemeType.SpecialSchemeTypeId;
                }
            }
            if (string.IsNullOrEmpty(this.SpecialSchemeId))
            {
                specialScheme.CompileMan = this.CurrUser.UserName;
                specialScheme.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
                specialScheme.IsPass = null;
                this.SpecialSchemeId = specialScheme.SpecialSchemeId = SQLHelper.GetNewID(typeof(Model.Technique_SpecialScheme));
                BLL.SpecialSchemeService.AddSpecialSchemeList(specialScheme);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加专项方案资源");
            }
            else
            {
                specialScheme.SpecialSchemeId = this.SpecialSchemeId;
                BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改专项方案资源");
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
            if (string.IsNullOrEmpty(this.txtSpecialSchemeCode.Text.Trim()))
            {
                ShowNotify("请输入方案编号", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtSpecialSchemeName.Text.Trim()))
            {
                ShowNotify("请输入方案名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlUnit.SelectedValue == Const._Null)
            {
                ShowNotify("请选择单位", MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(this.SpecialSchemeId))
            {
                this.SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialScheme&menuId=3E2F2FFD-ED2E-4914-8370-D97A68398814&type=0", this.SpecialSchemeId)));
        }
        #endregion

        #region 验证方案编号、名称是否存在
        /// <summary>
        /// 验证方案编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.SpecialSchemeCode == this.txtSpecialSchemeCode.Text.Trim() && (x.SpecialSchemeId != this.SpecialSchemeId || (this.SpecialSchemeId == null && x.SpecialSchemeId != null)));
            if (q != null)
            {
                ShowNotify("输入的方案编号已存在！", MessageBoxIcon.Warning);
            }
            var q2 = Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.SpecialSchemeName == this.txtSpecialSchemeName.Text.Trim() && (x.SpecialSchemeId != this.SpecialSchemeId || (this.SpecialSchemeId == null && x.SpecialSchemeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的方案名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void SetTemp()
        {
            this.txtSpecialSchemeCode.Focus();
            this.SpecialSchemeId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtSpecialSchemeCode.Text = string.Empty;
            this.txtSpecialSchemeName.Text = string.Empty;
            this.ddlUnit.SelectedValue = "null";
            this.ddlSpecialSchemeType.SelectedValue = "null";
            this.txtSummary.Text = string.Empty;
            this.lbAttachUrl.Text = string.Empty;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialSchemeMenuId);
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
                    this.btnDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}