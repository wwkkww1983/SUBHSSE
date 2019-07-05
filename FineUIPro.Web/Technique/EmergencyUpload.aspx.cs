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
    public partial class EmergencyUpload : PageBase
    {
        #region 定义变量
        public string EmergencyId
        {
            get
            {
                return (string)ViewState["EmergencyId"];
            }
            set
            {
                ViewState["EmergencyId"] = value;
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
                BLL.EmergencyTypeService.InitEmergencyTypeDropDownList(this.ddlEmergencyType, true);

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
                var emergency = BLL.EmergencyService.GetEmergencyByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, emergency);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_Emergency> emergency)
        {
            List<Model.Technique_Emergency> chidLaw = new List<Model.Technique_Emergency>();
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
                        Text = item.EmergencyName,
                        NodeID = item.EmergencyId,
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
            this.EmergencyId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.EmergencyService.GetEmergencyListById(this.EmergencyId);
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
                    this.txtEmergencyCode.Text = q.EmergencyCode;
                    this.txtEmergencyName.Text = q.EmergencyName;
                    this.txtSummary.Text = q.Summary;
                    if (!string.IsNullOrEmpty(q.EmergencyTypeId))
                    {
                        this.ddlEmergencyType.SelectedValue = q.EmergencyTypeId;
                    }
                    this.txtRemark.Text = q.Remark;                    
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
            var emergency = BLL.EmergencyService.GetEmergencyListById(this.EmergencyId);
            if (emergency != null && !emergency.AuditDate.HasValue)
            {
                BLL.EmergencyService.DeleteEmergencyListById(this.EmergencyId);
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
            ShowNotify("保存成功！", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_Emergency emergency = new Model.Technique_Emergency
            {
                EmergencyCode = this.txtEmergencyCode.Text.Trim(),
                EmergencyName = this.txtEmergencyName.Text.Trim(),
                Summary = this.txtSummary.Text.Trim(),
                Remark = this.txtRemark.Text.Trim()
            };
            ////应急预案类型下拉框
            if (this.ddlEmergencyType.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.ddlEmergencyType.SelectedValue))
                {
                    emergency.EmergencyTypeId = this.ddlEmergencyType.SelectedValue;
                }
                else
                {
                    var emergencyType = BLL.EmergencyTypeService.GetEmergencyTypeByName(this.ddlEmergencyType.Text);
                    if (emergencyType != null)
                    {
                        emergency.EmergencyTypeId = emergencyType.EmergencyTypeId;
                    }
                    else
                    {
                        Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
                        {
                            EmergencyTypeId = SQLHelper.GetNewID(typeof(Model.Base_EmergencyType)),
                            EmergencyTypeName = this.ddlEmergencyType.Text
                        };
                        BLL.EmergencyTypeService.AddEmergencyType(newEmergencyType);
                        emergency.EmergencyTypeId = newEmergencyType.EmergencyTypeId;
                    }
                }
            }

            if (string.IsNullOrEmpty(this.EmergencyId))
            {
                emergency.CompileMan = this.CurrUser.UserName;
                emergency.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                emergency.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
                emergency.IsPass = null;
                this.EmergencyId = emergency.EmergencyId = SQLHelper.GetNewID(typeof(Model.Technique_Emergency));
                BLL.EmergencyService.AddEmergencyList(emergency);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加应急预案");
            }
            else
            {
                emergency.EmergencyId = this.EmergencyId;
                BLL.EmergencyService.UpdateEmergencyList(emergency);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改应急预案");
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
            if (string.IsNullOrEmpty(this.EmergencyId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Emergency&menuId=575C5154-A135-4737-8682-A129EA717660&type=0", this.EmergencyId)));
        }
        #endregion

        #region 清空文本框
        /// <summary>
        /// 清空文本框
        /// </summary>
        private void SetTemp()
        {
            this.txtEmergencyCode.Focus();
            this.EmergencyId = string.Empty;
            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            this.txtEmergencyCode.Text = string.Empty;
            this.txtEmergencyName.Text = string.Empty;
            this.ddlEmergencyType.SelectedValue = "null";
            this.txtSummary.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.lbAttachUrl.Text = string.Empty;
            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion

        #region 验证编号、名称否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.EmergencyCode == this.txtEmergencyCode.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q != null)
            {
                ShowNotify("输入的应急预案编号已存在！", MessageBoxIcon.Warning);
            }
            var q2 = Funs.DB.Technique_Emergency.FirstOrDefault(x => x.EmergencyName == this.txtEmergencyName.Text.Trim() && (x.EmergencyId != this.EmergencyId || (this.EmergencyId == null && x.EmergencyId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的应急预案名称已存在！", MessageBoxIcon.Warning);
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EmergencyMenuId);
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