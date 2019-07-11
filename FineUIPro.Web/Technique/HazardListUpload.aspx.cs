using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class HazardListUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HazardId
        {
            get
            {
                return (string)ViewState["HazardId"];
            }
            set
            {
                ViewState["HazardId"] = value;
            }
        }

        public string HazardListTypeId
        {
            get
            {
                return (string)ViewState["HazardListTypeId"];
            }
            set
            {
                ViewState["HazardListTypeId"] = value;
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
                 this.ddlHelperMethod.DataTextField = "ConstText";
                 ddlHelperMethod.DataValueField = "ConstValue";
                 ddlHelperMethod.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0006);
                 ddlHelperMethod.DataBind();
                 Funs.FineUIPleaseSelect(this.ddlHelperMethod);

                 this.ddlHazardLevel.DataTextField = "ConstText";
                 ddlHazardLevel.DataValueField = "ConstValue";
                 ddlHazardLevel.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0007);
                 ddlHazardLevel.DataBind();
                 Funs.FineUIPleaseSelect(this.ddlHazardLevel);

                this.HazardListTypeId = Request.Params["HazardListTypeId"];
               
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
                var hazardList = BLL.HazardListService.GetHazardListByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, hazardList);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_HazardList> hazardList)
        {
            List<Model.Technique_HazardList> chidLaw = new List<Model.Technique_HazardList>();
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
                        Text = item.HazardCode,
                        NodeID = item.HazardId,
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
            this.HazardId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.HazardListService.GetHazardListById(this.HazardId);
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
                    this.txtHazardCode.Text = q.HazardCode;
                    this.txtHazardItems.Text = q.HazardItems;
                    this.txtDefectsType.Text = q.DefectsType;
                    this.txtMayLeadAccidents.Text = q.MayLeadAccidents;
                    if (q.HelperMethod != "null")
                    {
                        this.ddlHelperMethod.SelectedValue = q.HelperMethod;
                    }
                    if (q.HazardJudge_L != null)
                    {
                        this.txtHazardJudge_L.Text = Convert.ToString(q.HazardJudge_L);
                    }
                    if (q.HazardJudge_E != null)
                    {
                        this.txtHazardJudge_E.Text = Convert.ToString(q.HazardJudge_E);
                    }
                    if (q.HazardJudge_C != null)
                    {
                        this.txtHazardJudge_C.Text = Convert.ToString(q.HazardJudge_C);
                    }
                    if (q.HazardJudge_D != null)
                    {
                        this.txtHazardJudge_D.Text = Convert.ToString(q.HazardJudge_D);
                    }
                    if (q.HazardLevel != "0")
                    {
                        this.ddlHazardLevel.SelectedValue = q.HazardLevel;
                    }
                    this.txtControlMeasures.Text = q.ControlMeasures;
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
            var hazardList = BLL.HazardListService.GetHazardListById(this.HazardId);
            if (hazardList != null && !hazardList.AuditDate.HasValue)
            {
                BLL.HazardListService.DeleteHazardListById(this.HazardId);
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
            Model.Technique_HazardList hazardList = new Model.Technique_HazardList
            {
                HazardCode = this.txtHazardCode.Text.Trim(),
                HazardItems = this.txtHazardItems.Text.Trim(),
                DefectsType = this.txtDefectsType.Text.Trim(),
                MayLeadAccidents = this.txtMayLeadAccidents.Text.Trim()
            };
            if (this.ddlHelperMethod.SelectedValue != "null")
            {
                hazardList.HelperMethod = this.ddlHelperMethod.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_L.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_L = Convert.ToDecimal(this.txtHazardJudge_L.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(L)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_E.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_E = Convert.ToDecimal(this.txtHazardJudge_E.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(E)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_C.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_C = Convert.ToDecimal(this.txtHazardJudge_C.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(C)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }

            }
            if (!string.IsNullOrEmpty(this.txtHazardJudge_D.Text.Trim()))
            {
                try
                {
                    hazardList.HazardJudge_D = Convert.ToDecimal(this.txtHazardJudge_D.Text.Trim());
                }
                catch (Exception)
                {
                    ShowNotify("输入格式不正确，作业条件危险性评价(D)必须是数字！", MessageBoxIcon.Warning);
                    return;
                }

            }
            if (this.ddlHazardLevel.SelectedValue != "null")
            {
                hazardList.HazardLevel = this.ddlHazardLevel.SelectedValue;
            }
            hazardList.ControlMeasures = this.txtControlMeasures.Text.Trim();

            if (string.IsNullOrEmpty(this.HazardId))
            {
                hazardList.CompileMan = this.CurrUser.UserName;
                hazardList.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                hazardList.CompileDate = DateTime.Now;
                hazardList.IsPass = null;
                hazardList.HazardId = SQLHelper.GetNewID(typeof(Model.Technique_HazardList));
                hazardList.HazardListTypeId = this.HazardListTypeId;
                BLL.HazardListService.AddHazardList(hazardList);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardCode, hazardList.HazardId, BLL.Const.HazardListMenuId, Const.BtnAdd);
            }
            else
            {
                hazardList.HazardId = this.HazardId;
                Model.Technique_HazardList hazard = BLL.HazardListService.GetHazardListById(this.HazardId);
                if (hazard != null)
                {
                    hazardList.HazardListTypeId = hazard.HazardListTypeId;
                }
                BLL.HazardListService.UpdateHazardList(hazardList);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardCode, hazardList.HazardId, BLL.Const.HazardListMenuId, Const.BtnModify);
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
            this.txtHazardCode.Focus();
            this.HazardId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtHazardCode.Text = string.Empty;
            this.txtHazardItems.Text = string.Empty;
            this.txtDefectsType.Text = string.Empty;
            this.txtMayLeadAccidents.Text = string.Empty;
            this.ddlHelperMethod.SelectedValue = "null";
            this.txtHazardJudge_E.Text = string.Empty;
            this.txtHazardJudge_L.Text = string.Empty;
            this.txtHazardJudge_D.Text = string.Empty;
            this.txtHazardJudge_C.Text = string.Empty;
            this.ddlHazardLevel.SelectedValue = "null";
            this.txtControlMeasures.Text = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion        

        #region 验证危险源代码是否存在
        /// <summary>
        /// 验证危险源名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_HazardList.FirstOrDefault(x => x.HazardListTypeId == this.HazardListTypeId && x.HazardCode == this.txtHazardCode.Text.Trim() && (x.HazardId != this.HazardId || (this.HazardId == null && x.HazardId != null)));
            if (q != null)
            {
                ShowNotify("输入的危险源代码已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
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
                }
            }
        }
        #endregion
    }
}