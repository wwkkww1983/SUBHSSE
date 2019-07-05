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
    public partial class RulesRegulationsUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string RulesRegulationsId
        {
            get
            {
                return (string)ViewState["RulesRegulationsId"];
            }
            set
            {
                ViewState["RulesRegulationsId"] = value;
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
                this.GetButtonPower();//获取权限
                this.InitTreeMenu();

                //加载规章制度类别下拉选项
                this.ddlRulesRegulationsTypeId.DataTextField = "RulesRegulationsTypeName";
                this.ddlRulesRegulationsTypeId.DataValueField = "RulesRegulationsTypeId";
                this.ddlRulesRegulationsTypeId.DataSource = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeList();
                ddlRulesRegulationsTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlRulesRegulationsTypeId);

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
                var newLaw = BLL.RulesRegulationsService.GetRulesRegulationByCompileMan(this.CurrUser.UserName);
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
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Law_RulesRegulations> rulesRegulations)
        {
            List<Model.Law_RulesRegulations> chidRulesRegulations = new List<Model.Law_RulesRegulations>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidRulesRegulations = rulesRegulations.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidRulesRegulations = rulesRegulations.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidRulesRegulations = rulesRegulations.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidRulesRegulations.Count() > 0)
            {
                foreach (var item in chidRulesRegulations)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.RulesRegulationsName,
                        NodeID = item.RulesRegulationsId,
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
            this.RulesRegulationsId = this.tvUploadResources.SelectedNode.NodeID;
            var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(this.RulesRegulationsId);
            if (rulesRegulations != null)
            {
                if (rulesRegulations.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                    //this.btnUpFile.Hidden = true;
                    //this.btnDeleteFile.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                    //this.btnDeleteFile.Hidden = false;
                }

                if (rulesRegulations != null)
                {
                    this.txtRulesRegulationsCode.Text = rulesRegulations.RulesRegulationsCode;
                    this.txtRulesRegulationsName.Text = rulesRegulations.RulesRegulationsName;
                    if (!string.IsNullOrEmpty(rulesRegulations.RulesRegulationsTypeId))
                    {
                        this.ddlRulesRegulationsTypeId.SelectedValue = rulesRegulations.RulesRegulationsTypeId;
                    }
                    if (rulesRegulations.CustomDate != null)
                    {
                        this.dpkCustomDate.Text = string.Format("{0:yyyy-MM-dd}", rulesRegulations.CustomDate);
                    }
                    this.txtApplicableScope.Text = rulesRegulations.ApplicableScope;
                    //if (!string.IsNullOrEmpty(rulesRegulations.AttachUrl))
                    //{
                    //    this.FullAttachUrl = rulesRegulations.AttachUrl;
                        //this.lbAttachUrl.Text = rulesRegulations.AttachUrl.Substring(rulesRegulations.AttachUrl.IndexOf("~") + 1);
                    //}
                    this.txtRemark.Text = rulesRegulations.Remark;
                    this.txtCompileMan.Text = rulesRegulations.CompileMan;
                    if (rulesRegulations.CompileDate.HasValue)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", rulesRegulations.CompileDate);
                    }
                }
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
            var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(this.RulesRegulationsId);
            if (rulesRegulations != null && !rulesRegulations.AuditDate.HasValue)
            {
                BLL.RulesRegulationsService.DeleteRuleRegulationsById(this.RulesRegulationsId);
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
            this.RulesRegulationsId = string.Empty;
            this.txtRulesRegulationsCode.Text = string.Empty;
            this.txtRulesRegulationsName.Text = string.Empty;
            this.ddlRulesRegulationsTypeId.SelectedValue = "null";
            this.dpkCustomDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.txtApplicableScope.Text = string.Empty;
            this.txtRemark.Text = string.Empty;

            //this.fuAttachUrl.Reset();
            //this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
            //this.btnUpFile.Hidden = false;
            //this.btnDeleteFile.Hidden = false;
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
            SaveDate();
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        private void SaveDate()
        {
            Model.Law_RulesRegulations rulesRegulations = new Model.Law_RulesRegulations
            {
                RulesRegulationsCode = this.txtRulesRegulationsCode.Text.Trim(),
                RulesRegulationsName = this.txtRulesRegulationsName.Text.Trim()
            };
            if (this.ddlRulesRegulationsTypeId.SelectedValue != BLL.Const._Null)
            {
                rulesRegulations.RulesRegulationsTypeId = this.ddlRulesRegulationsTypeId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.dpkCustomDate.Text.Trim()))
            {
                rulesRegulations.CustomDate = Convert.ToDateTime(this.dpkCustomDate.Text.Trim());
            }
            rulesRegulations.ApplicableScope = this.txtApplicableScope.Text.Trim();
            rulesRegulations.Remark = this.txtRemark.Text.Trim();
            rulesRegulations.AttachUrl = this.FullAttachUrl;
            rulesRegulations.IsPass = null;
            rulesRegulations.CompileMan = this.CurrUser.UserName;
            rulesRegulations.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text);
            rulesRegulations.UnitId = this.CurrUser.UnitId;
            if (string.IsNullOrEmpty(this.RulesRegulationsId))
            {
                this.RulesRegulationsId = rulesRegulations.RulesRegulationsId = SQLHelper.GetNewID(typeof(Model.Law_RulesRegulations));
                BLL.RulesRegulationsService.AddRulesRegulations(rulesRegulations);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "增加政府部门安全规章");
            }
            else
            {
                rulesRegulations.RulesRegulationsId = this.RulesRegulationsId;
                BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改政府部门安全规章");
            }
        }
        #endregion

        #region 按钮权限设置
        /// <summary>
        /// 按钮权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RulesRegulationsMenuId);
            if (buttonList.Count()>0)
            {
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                    //this.btnDeleteFile.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证规章制度名称是否存在
        /// <summary>
        /// 验证规章制度名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_RulesRegulations.FirstOrDefault(x => x.RulesRegulationsName == this.txtRulesRegulationsName.Text.Trim() && (x.RulesRegulationsId != this.RulesRegulationsId || (this.RulesRegulationsId == null && x.RulesRegulationsId != null)));
            if (standard != null)
            {
                ShowNotify("输入的规章名称已存在！", MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(this.RulesRegulationsId))
            {
                SaveDate();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&menuId=DF1413F3-4CE5-40B3-A574-E01CE64FEA25&type=0", RulesRegulationsId)));
        }
        #endregion
    }
}