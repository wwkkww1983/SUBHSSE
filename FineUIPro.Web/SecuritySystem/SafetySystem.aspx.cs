using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.SecuritySystem
{
    public partial class SafetySystem : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetySystemId
        {
            get
            {
                return (string)ViewState["SafetySystemId"];
            }
            set
            {
                ViewState["SafetySystemId"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                this.InitTreeMenu();

            }
        }

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trSafetySystem.Nodes.Clear();
            TreeNode rootNode = new TreeNode
            {
                Text = "项目组织体系",
                NodeID = "0",
                Expanded = true
            };
            this.trSafetySystem.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes);
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes)
        {
            var unitLists = BLL.ProjectUnitService.GetProjectUnitListByProjectId(this.ProjectId);
            if (unitLists.Count() > 0)
            {
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    unitLists = unitLists.Where(x => x.UnitId == this.CurrUser.UnitId).ToList();
                }

                TreeNode tn = null;
                foreach (var dr in unitLists)
                {
                    tn = new TreeNode();
                    var unitName = BLL.UnitService.GetUnitNameByUnitId(dr.UnitId);
                    if (unitName != null)
                    {
                        tn.Text = unitName;
                    }
                    tn.NodeID = dr.UnitId;
                    tn.EnableClickEvent = true;

                    var gunitType = BLL.ConstValue.GetConstByConstValueAndGroupId(dr.UnitType, BLL.ConstValue.Group_ProjectUnitType);
                    if (gunitType != null)
                    {
                        tn.ToolTip = gunitType.ConstText + "：" + unitName;
                    }
                    //tn.ToolTip = "编号：" + dr.SafetyOrganizationCode + "；<br/>机构名称：" + dr.SafetyOrganizationName + "；<br/>职责：" + dr.Duties + "；<br/>组成文件：" + dr.BundleFile + "；<br/>机构人员：" + dr.AgencyPersonnel;
                    nodes.Add(tn);
                }
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectSafetySystemMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trSafetySystem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.txtSeeFile.Text = string.Empty;
            if (this.trSafetySystem.SelectedNode != null)
            {
                if (this.trSafetySystem.SelectedNode.Nodes.Count == 0 && !string.IsNullOrEmpty(this.trSafetySystem.SelectedNode.NodeID) && this.trSafetySystem.SelectedNode.NodeID != "0")
                {
                    var safetySystem = BLL.SafetySystemService.GetSafetySystemByProjectId(this.ProjectId, this.trSafetySystem.SelectedNodeID);
                    if (safetySystem != null)
                    {
                        this.SafetySystemId = safetySystem.SafetySystemId;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(safetySystem.SeeFile);
                    }
                    else
                    {
                        this.SafetySystemId = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///  保存按钮事件
        /// </summary>
        /// <param name="isClose"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.trSafetySystem.SelectedNode != null)
            {
                if (this.trSafetySystem.SelectedNode.Nodes.Count == 0 && !string.IsNullOrEmpty(this.trSafetySystem.SelectedNode.NodeID) && this.trSafetySystem.SelectedNode.NodeID != "0")
                {
                    this.SaveData();
                    ShowNotify("数据保存成功!", MessageBoxIcon.Success);
                }
                else
                {
                    ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveData()
        {
            Model.SecuritySystem_SafetySystem newSafetySystem = new Model.SecuritySystem_SafetySystem
            {
                SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text),
                ProjectId = this.ProjectId
            };
            if (!string.IsNullOrEmpty(newSafetySystem.ProjectId))
            {
                if (string.IsNullOrEmpty(this.SafetySystemId))
                {
                    this.SafetySystemId = newSafetySystem.SafetySystemId = SQLHelper.GetNewID(typeof(Model.SecuritySystem_SafetySystem));
                    newSafetySystem.SafetySystemId = this.SafetySystemId;
                    newSafetySystem.UnitId = this.trSafetySystem.SelectedNodeID;
                    BLL.SafetySystemService.AddSafetySystem(newSafetySystem);
                    BLL.LogService.AddSys_Log(this.CurrUser, this.trSafetySystem.SelectedNode.Text, this.SafetySystemId, BLL.Const.ProjectSafetySystemMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    newSafetySystem.SafetySystemId = this.SafetySystemId;
                    BLL.SafetySystemService.UpdateSafetySystem(newSafetySystem);
                    BLL.LogService.AddSys_Log(this.CurrUser, this.trSafetySystem.SelectedNode.Text, this.SafetySystemId,BLL.Const.ProjectSafetySystemMenuId,BLL.Const.BtnModify);
                }
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.SafetySystemId))
            {
                if (this.trSafetySystem.SelectedNode != null)
                {
                    if (this.trSafetySystem.SelectedNode.Nodes.Count == 0 && !string.IsNullOrEmpty(this.trSafetySystem.SelectedNode.NodeID) && this.trSafetySystem.SelectedNode.NodeID != "0")
                    {
                        this.SaveData();
                    }
                    else
                    {
                        ShowNotify("请选择末级节点！", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                }
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetySystemAttachUrl&menuId={1}", this.SafetySystemId, BLL.Const.ProjectSafetySystemMenuId)));
        }
        #endregion
    }
}