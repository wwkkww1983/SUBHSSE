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
    public partial class LawRegulationListUpload : PageBase
    {
        #region 定义项
        /// <summary>
        /// 法律法规主键
        /// </summary>
        public string LawRegulationId
        {
            get
            {
                return (string)ViewState["LawRegulationId"];
            }
            set
            {
                ViewState["LawRegulationId"] = value;
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

        #region  加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();//权限按钮
                this.InitTreeMenu();

                //加载法律法规类别下拉选项
                this.ddlLawsRegulationsTypeId.DataTextField = "Name";
                this.ddlLawsRegulationsTypeId.DataValueField = "Id";
                this.ddlLawsRegulationsTypeId.DataSource = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeList();
                ddlLawsRegulationsTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlLawsRegulationsTypeId);

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
                var newLaw = BLL.LawRegulationListService.GetLawRegulationListByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, newLaw);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Law_LawRegulationList> laws)
        {
            List<Model.Law_LawRegulationList> chidLaw = new List<Model.Law_LawRegulationList>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = laws.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = laws.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = laws.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.LawRegulationName,
                        NodeID = item.LawRegulationId,
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
            this.LawRegulationId = this.tvUploadResources.SelectedNode.NodeID;
            var lawRegulation = BLL.LawRegulationListService.GetViewLawRegulationListById(this.LawRegulationId);
            if (lawRegulation != null)
            {
                if (lawRegulation.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }

                if (lawRegulation != null)
                {
                    this.txtLawRegulationCode.Text = lawRegulation.LawRegulationCode;
                    this.txtLawRegulationName.Text = lawRegulation.LawRegulationName;
                    if (lawRegulation.ApprovalDate.HasValue)
                    {
                        this.dpkApprovalDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.ApprovalDate);
                    }
                    if (lawRegulation.EffectiveDate.HasValue)
                    {
                        this.dpkEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.EffectiveDate);
                    }
                    this.txtDescription.Text = lawRegulation.Description;
                    if (!string.IsNullOrEmpty(lawRegulation.LawsRegulationsTypeId))
                    {
                        this.ddlLawsRegulationsTypeId.SelectedValue = lawRegulation.LawsRegulationsTypeId;
                    }
                    if (!string.IsNullOrEmpty(lawRegulation.AttachUrl))
                    {
                        this.FullAttachUrl = lawRegulation.AttachUrl;
                        this.lbAttachUrl.Text = lawRegulation.AttachUrl.Substring(lawRegulation.AttachUrl.IndexOf("~") + 1);
                    }
                    this.txtCompileMan.Text = lawRegulation.CompileMan;
                    if (lawRegulation.CompileDate.HasValue)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", lawRegulation.CompileDate);
                    }
                }
            }
        }
        #endregion

        #region 保存按钮事件
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveDate();
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        private void SaveDate()
        {
            Model.Law_LawRegulationList lawRegulationList = new Model.Law_LawRegulationList
            {
                LawRegulationCode = this.txtLawRegulationCode.Text.Trim(),
                LawRegulationName = this.txtLawRegulationName.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkApprovalDate.Text.Trim()))
            {
                lawRegulationList.ApprovalDate = Convert.ToDateTime(this.dpkApprovalDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.dpkEffectiveDate.Text.Trim()))
            {
                lawRegulationList.EffectiveDate = Convert.ToDateTime(this.dpkEffectiveDate.Text.Trim());
            }
            lawRegulationList.Description = this.txtDescription.Text.Trim();
            lawRegulationList.AttachUrl = this.FullAttachUrl;
            if (this.ddlLawsRegulationsTypeId.SelectedValue != BLL.Const._Null)
            {
                lawRegulationList.LawsRegulationsTypeId = this.ddlLawsRegulationsTypeId.SelectedValue;
            }
            lawRegulationList.IsPass = null;
            lawRegulationList.CompileMan = this.CurrUser.UserName;
            lawRegulationList.UnitId = this.CurrUser.UnitId;
            lawRegulationList.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text);
            if (string.IsNullOrEmpty(this.LawRegulationId))
            {

                this.LawRegulationId = lawRegulationList.LawRegulationId = SQLHelper.GetNewID(typeof(Model.Law_LawRegulationList));
                BLL.LawRegulationListService.AddLawRegulationList(lawRegulationList);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationList.LawRegulationCode, lawRegulationList.LawRegulationId,BLL.Const.LawRegulationListMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                lawRegulationList.LawRegulationId = this.LawRegulationId;
                BLL.LawRegulationListService.UpdateLawRegulationList(lawRegulationList);
                BLL.LogService.AddSys_Log(this.CurrUser, lawRegulationList.LawRegulationCode, lawRegulationList.LawRegulationId, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnModify);
            }
        }
        #endregion

        #region 增加、删除按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var law = BLL.LawRegulationListService.GetLawRegulationListById(this.LawRegulationId);
            if (law != null && !law.AuditDate.HasValue)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, law.LawRegulationCode, law.LawRegulationId, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnModify);
                BLL.LawRegulationListService.DeleteLawRegulationListById(this.LawRegulationId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
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
            if (string.IsNullOrEmpty(this.LawRegulationId))
            {
                SaveDate();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/LawRegulation&menuId=F4B02718-0616-4623-ABCE-885698DDBEB1&type=0", LawRegulationId)));
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtLawRegulationCode.Focus();
            this.LawRegulationId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtLawRegulationCode.Text = string.Empty;
            this.txtLawRegulationName.Text = string.Empty;
            this.ddlLawsRegulationsTypeId.SelectedIndex = 0;
            this.dpkApprovalDate.Text = string.Empty;
            this.dpkEffectiveDate.Text = string.Empty;
            this.txtDescription.Text = string.Empty;

            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion        

        #region 验证法律法规名称是否存在
        /// <summary>
        /// 验证法律法规名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var lawRegulation = Funs.DB.Law_LawRegulationList.FirstOrDefault(x => x.LawRegulationName == this.txtLawRegulationName.Text.Trim() && (x.LawRegulationId != this.LawRegulationId || (this.LawRegulationId == null && x.LawRegulationId != null)));
            if (lawRegulation != null)
            {
                ShowNotify("输入的法律法规名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 按钮权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.LawRegulationListMenuId);
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
    }
}