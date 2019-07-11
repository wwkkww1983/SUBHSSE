using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Hazard
{
    public partial class HazardListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 类别Code
        /// </summary>
        public string HazardSortCode
        {
            get
            {
                return (string)ViewState["HazardSortCode"];
            }
            set
            {
                ViewState["HazardSortCode"] = value;
            }
        }

        /// <summary>
        /// 危险源辨识与评价清单Id
        /// </summary>
        public string HazardListId
        {
            get
            {
                return (string)ViewState["HazardListId"];
            }
            set
            {
                ViewState["HazardListId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
        /// 判断是否完全选
        /// </summary>
        //private bool result = true;

        /// <summary>
        /// 危险源集合
        /// </summary>
        public static List<Model.HSSE_HazardTemplate> hazardTemplates = new List<Model.HSSE_HazardTemplate>();

        /// <summary>
        /// 新增危险源集合
        /// </summary>
        public static List<Model.HSSE_HazardTemplate> newHazardTemplates = new List<Model.HSSE_HazardTemplate>();

        /// <summary>
        /// 类别集合
        /// </summary>
        //private static List<Model.Technique_HazardListType> hazardSorts = new List<Model.Technique_HazardListType>();

        /// <summary>
        /// 工作阶段ID集合
        /// </summary>
        private static List<string> workStageIds = new List<string>();

        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                //this.tvHazardTemplate.Nodes.Clear();
                //TreeNode rootNode = new TreeNode();//定义根节点
                //rootNode.Text = "危险源辨识与评价清单";
                //rootNode.NodeID = "0";
                //this.tvHazardTemplate.Nodes.Add(rootNode);

                hazardTemplates.Clear();
                newHazardTemplates.Clear();
                //hazardSorts.Clear();

                this.InitDropDownList();
                this.HazardListId = Request.Params["HazardListId"];
                if (!string.IsNullOrEmpty(this.HazardListId))
                {
                    Model.Hazard_HazardList hazardList = BLL.Hazard_HazardListService.GetHazardList(this.HazardListId);
                    if (hazardList != null)
                    {
                        this.ProjectId = hazardList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }                       
                        this.txtHazardListCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HazardListId);
                        if (!string.IsNullOrEmpty(hazardList.WorkStage))
                        {
                            List<string> workStages = hazardList.WorkStage.Split(',').ToList();
                            string workStageId = string.Empty;
                            string workStage = string.Empty;
                            foreach (string str in workStages)
                            {
                                workStageId += str + ",";
                                Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                                if (c != null)
                                {
                                    workStage += c.WorkStageName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(workStage))
                            {
                                this.hdWorkStage.Text = workStageId.Substring(0, workStageId.LastIndexOf(","));
                                workStage = workStage.Substring(0, workStage.LastIndexOf(","));
                            }
                            this.txtWorkStage.Text = workStage;
                        }
                        this.hdWorkStage.Text = hazardList.WorkStage;
                        if (!string.IsNullOrEmpty(hazardList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = hazardList.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", hazardList.CompileDate);
                        if (!string.IsNullOrEmpty(hazardList.ControllingPerson))
                        {
                            this.drpControllingPerson.SelectedValue = hazardList.ControllingPerson;
                        }
                        this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", hazardList.IdentificationDate);
                        this.txtWorkArea.Text = hazardList.WorkAreaName;
                        this.txtContents.Text = HttpUtility.HtmlDecode(hazardList.Contents);
                        //this.HazardSortSetDataBind();
                        this.SelectedCheckedHazardItem();
                    }
                }
                else
                {
                    this.txtHazardListCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectHazardListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtIdentificationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectHazardListMenuId;
                this.ctlAuditFlow.DataId = this.HazardListId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            ///区域下拉框
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkArea, this.ProjectId, true);
            ///编制人
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, false);
            ///控制责任人
            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpControllingPerson, this.ProjectId, string.Empty, true);
        }

        #region 绑定树节点
        ///// <summary>
        ///// 绑定树节点
        ///// </summary>
        //private void HazardSortSetDataBind()
        //{
        //    this.tvHazardTemplate.Nodes.Clear();

        //    TreeNode rootNode = new TreeNode();//定义根节点
        //    rootNode.Text = "危险源辨识与评价清单";
        //    rootNode.NodeID = "0";

        //    this.tvHazardTemplate.Nodes.Add(rootNode);
        //    rootNode.Expanded = true;
        //    this.GetNodes(rootNode.Nodes, null, null);
        //}
        #endregion

        #region  遍历节点方法
        ///// <summary>
        ///// 遍历节点方法
        ///// </summary>
        ///// <param name="nodes">节点集合</param>
        ///// <param name="parentId">父节点</param>
        //private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node)
        //{
        //    List<Model.Technique_HazardListType> hazardSort = null;
        //    workStages.Clear();
        //    workStageNames.Clear();
        //    string w = this.hdWorkStage.Text.Trim();
        //    workStages = this.hdWorkStage.Text.Split(',').ToList();
        //    workStageNames = this.txtWorkStage.Text.Trim().Split(',').ToList();

        //    if (parentId == null)
        //    {
        //        for (int i = 0; i < workStages.Count; i++)
        //        {
        //            TreeNode newNode = new TreeNode();
        //            newNode.Text = workStageNames[i];
        //            newNode.NodeID = workStages[i];
        //            newNode.EnableCheckBox = true;
        //            newNode.EnableCheckEvent = true;
        //            nodes.Add(newNode);
        //        }
        //    }
        //    else if (BLL.WorkStageService.GetWorkStageById(parentId) != null)
        //    {
        //        hazardSort = (from x in BLL.Funs.DB.Technique_HazardListType where x.SupHazardListTypeId == "0" && x.WorkStage.Contains(parentId) orderby x.HazardListTypeCode select x).ToList();
        //        foreach (var q in hazardSort)
        //        {
        //            TreeNode newNode = new TreeNode();
        //            newNode.Text = q.HazardListTypeName;
        //            newNode.NodeID = q.HazardListTypeId;
        //            newNode.EnableCheckBox = true;
        //            newNode.EnableCheckEvent = true;
        //            nodes.Add(newNode);
        //        }
        //    }
        //    else
        //    {
        //        hazardSort = (from x in BLL.Funs.DB.Technique_HazardListType where x.SupHazardListTypeId == parentId orderby x.HazardListTypeCode select x).ToList();
        //        foreach (var q in hazardSort)
        //        {
        //            TreeNode newNode = new TreeNode();
        //            newNode.Text = q.HazardListTypeName;
        //            newNode.NodeID = q.HazardListTypeId;
        //            newNode.EnableCheckBox = true;
        //            newNode.EnableCheckEvent = true;
        //            nodes.Add(newNode);
        //        }
        //    }

        //    for (int i = 0; i < nodes.Count; i++)
        //    {
        //        GetNodes(nodes[i].Nodes, nodes[i].NodeID, nodes[i]);
        //    }
        //}
        #endregion

        #region 全选、全不选
        ///// <summary>
        ///// 全选、全不选
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void tvHazardTemplate_NodeCheck(object sender, FineUIPro.TreeCheckEventArgs e)
        //{
        //    if (e.Checked)
        //    {
        //        this.tvHazardTemplate.CheckAllNodes(e.Node.Nodes);
        //        SetCheckParentNode(e.Node);
        //    }
        //    else
        //    {
        //        this.tvHazardTemplate.UncheckAllNodes(e.Node.Nodes);
        //    }
        //}

        ///// <summary>
        ///// 选中父节点
        ///// </summary>
        ///// <param name="node"></param>
        //private void SetCheckParentNode(TreeNode node)
        //{
        //    if (node.ParentNode != null && node.ParentNode.NodeID != "0")
        //    {
        //        node.ParentNode.Checked = true;
        //        if (node.ParentNode.ParentNode.NodeID != "0")
        //        {
        //            SetCheckParentNode(node.ParentNode);
        //        }
        //    }
        //}
        #endregion

        #region 确定按钮
        ///// <summary>
        /////确定按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSure_OnClick(object sender, EventArgs e)
        //{
        //    hazardTemplates.Clear();
        //    GetTvHazardTemplateChecked(this.tvHazardTemplate.Nodes);
        //    GetHazardTemplates();
        //}
        #endregion

        #region 筛选风险评价清单列表
        /// <summary>
        /// 筛选风险评价清单列表
        /// </summary>
        private void GetHazardTemplates()
        {
            List<Model.HSSE_HazardTemplate> newHazardTemplates1 = new List<Model.HSSE_HazardTemplate>();
            List<Model.HSSE_HazardTemplate> newHazardTemplates2 = new List<Model.HSSE_HazardTemplate>();
            if (this.drpHelperMethods.SelectedValue != null)
            {
                foreach (Model.HSSE_HazardTemplate hazardTemplate in hazardTemplates)
                {
                    for (int i = 0; i < this.drpHelperMethods.Items.Count; i++)
                    {
                        if (this.drpHelperMethods.Items[i].Selected)
                        {
                            if (hazardTemplate.HelperMethod == this.drpHelperMethods.Items[i].Value)
                            {
                                newHazardTemplates1.Add(hazardTemplate);
                            }
                        }
                    }
                }
            }
            else
            {
                newHazardTemplates1.AddRange(hazardTemplates);
            }

            if (this.drpHazardLevel.SelectedValue != null)
            {
                foreach (Model.HSSE_HazardTemplate hazardTemplate in newHazardTemplates1)
                {
                    for (int i = 0; i < this.drpHazardLevel.Items.Count; i++)
                    {
                        if (this.drpHazardLevel.Items[i].Selected)
                        {
                            if (hazardTemplate.HazardLevel.ToString() == this.drpHazardLevel.Items[i].Value)
                            {
                                newHazardTemplates2.Add(hazardTemplate);
                            }
                        }
                    }
                }
            }
            else
            {
                newHazardTemplates2.AddRange(newHazardTemplates1);
            }
            this.Grid1.DataSource = newHazardTemplates2;
            this.Grid1.DataBind();
        }
        #endregion

        #region 选中选择项
        /// <summary>
        /// 选中选择项
        /// </summary>
        private void SelectedCheckedHazardItem()
        {
            //CheckedTvHazardTemplate(this.tvHazardTemplate.Nodes);
            //hazardTemplates.Clear();
            //GetTvHazardTemplateChecked(this.tvHazardTemplate.Nodes);

            List<Model.HSSE_HazardTemplate> newHazardTemplates = new List<Model.HSSE_HazardTemplate>();
            List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(HazardListId);
            //foreach (Model.HSSE_HazardTemplate hazardTemplate in hazardTemplates)
            //{
                foreach (Model.Hazard_HazardSelectedItem hazardSelectedItem in hazardSelectedItems)
                {
                //if (hazardSelectedItem.HazardId == hazardTemplate.HazardId && hazardSelectedItem.WorkStage.Trim() == hazardTemplate.WorkStage.Trim())
                //{
                Model.HSSE_HazardTemplate newHazardTemplate = new Model.HSSE_HazardTemplate
                {
                    HazardId = hazardSelectedItem.HazardId,
                    HazardListTypeId = hazardSelectedItem.HazardListTypeId,
                    HazardItems = hazardSelectedItem.HazardItems,
                    DefectsType = hazardSelectedItem.DefectsType,
                    MayLeadAccidents = hazardSelectedItem.MayLeadAccidents,
                    HelperMethod = hazardSelectedItem.HelperMethod,
                    HazardJudge_L = hazardSelectedItem.HazardJudge_L,
                    HazardJudge_E = hazardSelectedItem.HazardJudge_E,
                    HazardJudge_C = hazardSelectedItem.HazardJudge_C,
                    HazardJudge_D = hazardSelectedItem.HazardJudge_D,
                    HazardLevel = hazardSelectedItem.HazardLevel,
                    ControlMeasures = hazardSelectedItem.ControlMeasures,
                    //newHazardTemplate.State = hazardTemplate.State;
                    WorkStage = hazardSelectedItem.WorkStage.Trim()
                };
                newHazardTemplates.Add(newHazardTemplate);
                    //}
                //}
            }
            hazardTemplates = newHazardTemplates;
            this.Grid1.DataSource = hazardTemplates;
            this.Grid1.DataBind();
        }
        #endregion

        #region 遍历节点,选中已存在的节点
        ///// <summary>
        ///// 遍历节点,选中已存在的节点
        ///// </summary>
        ///// <param name="nodes"></param>
        //private void CheckedTvHazardTemplate(TreeNodeCollection nodes)
        //{
        //    foreach (TreeNode tnHazardTemplate in nodes)
        //    {
        //        if (tnHazardTemplate.NodeID != "0" && BLL.WorkStageService.GetWorkStageById(tnHazardTemplate.NodeID) == null)
        //        {
        //            Model.Technique_HazardListType hazardSortSet = (from x in BLL.Funs.DB.Technique_HazardListType where x.HazardListTypeId == tnHazardTemplate.NodeID orderby x.HazardListTypeCode select x).FirstOrDefault();
        //            if (hazardSortSet != null)
        //            {
        //                if (Convert.ToBoolean(hazardSortSet.IsEndLevel))
        //                {
        //                    List<Model.Hazard_HazardSelectedItem> hazardSelectedItems = BLL.Hazard_HazardSelectedItemService.GetHazardSelectedItemsByHazardListId(HazardListId);
        //                    foreach (Model.Hazard_HazardSelectedItem hazardSelectedItem in hazardSelectedItems)
        //                    {
        //                        if (hazardSelectedItem.HazardListTypeId == tnHazardTemplate.NodeID)
        //                        {
        //                            tnHazardTemplate.Checked = true;
        //                            ExpandParents(tnHazardTemplate);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    CheckedTvHazardTemplate(tnHazardTemplate.Nodes);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CheckedTvHazardTemplate(tnHazardTemplate.Nodes);
        //        }
        //    }
        //}
        #endregion

        #region 依次展开此节点的所以上级节点
        /// <summary>
        /// 依次展开此节点的所以上级节点
        /// </summary>
        private void ExpandParents(TreeNode node)
        {
            if (node.NodeID != "0")
            {
                node.Expanded = true;
                node.Checked = true;
                ExpandParents(node.ParentNode);
            }
        }
        #endregion

        #region 遍历选中的节点
        ///// <summary>
        ///// 遍历选中的节点
        ///// </summary>
        ///// <param name="nodes"></param>
        //private void GetTvHazardTemplateChecked(TreeNodeCollection nodes)
        //{
        //    foreach (TreeNode tnHazardTemplate in nodes)
        //    {
        //        if (tnHazardTemplate.NodeID != "0" && BLL.WorkStageService.GetWorkStageById(tnHazardTemplate.NodeID) == null)
        //        {
        //            Model.Technique_HazardListType hazardSortSet = (from x in BLL.Funs.DB.Technique_HazardListType where x.HazardListTypeId == tnHazardTemplate.NodeID orderby x.HazardListTypeCode select x).FirstOrDefault();
        //            if (hazardSortSet != null)
        //            {
        //                if (Convert.ToBoolean(hazardSortSet.IsEndLevel))
        //                {
        //                    if (tnHazardTemplate.Checked)
        //                    {
        //                        List<Model.Technique_HazardList> templates = BLL.HazardListService.GetHazardListByHazardListTypeId(tnHazardTemplate.NodeID);
        //                        foreach (var item in templates)
        //                        {
        //                            Model.HSSE_HazardTemplate hazardTemplate = new Model.HSSE_HazardTemplate();
        //                            hazardTemplate.HazardId = item.HazardId;
        //                            hazardTemplate.HazardListTypeId = item.HazardListTypeId;
        //                            hazardTemplate.HazardItems = item.HazardItems;
        //                            hazardTemplate.MayLeadAccidents = item.MayLeadAccidents;
        //                            hazardTemplate.HelperMethod = item.HelperMethod;
        //                            hazardTemplate.DefectsType = item.DefectsType;
        //                            if (item.HazardJudge_L != null)
        //                            {
        //                                hazardTemplate.HazardJudge_L = Convert.ToDecimal(item.HazardJudge_L);
        //                            }
        //                            if (item.HazardJudge_E != null)
        //                            {
        //                                hazardTemplate.HazardJudge_E = Convert.ToDecimal(item.HazardJudge_E);
        //                            }
        //                            if (item.HazardJudge_C != null)
        //                            {
        //                                hazardTemplate.HazardJudge_C = Convert.ToDecimal(item.HazardJudge_C);
        //                            }
        //                            if (item.HazardJudge_D != null)
        //                            {
        //                                hazardTemplate.HazardJudge_D = Convert.ToDecimal(item.HazardJudge_D);
        //                            }
        //                            hazardTemplate.HazardLevel = item.HazardLevel;
        //                            hazardTemplate.ControlMeasures = item.ControlMeasures;
        //                            if (tnHazardTemplate.ParentNode.ParentNode.ParentNode.NodeID != "0")
        //                            {
        //                                hazardTemplate.WorkStage = tnHazardTemplate.ParentNode.ParentNode.ParentNode.NodeID;
        //                            }
        //                            else
        //                            {
        //                                hazardTemplate.WorkStage = tnHazardTemplate.ParentNode.ParentNode.NodeID;
        //                            }
        //                            hazardTemplates.Add(hazardTemplate);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    GetTvHazardTemplateChecked(tnHazardTemplate.Nodes);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            GetTvHazardTemplateChecked(tnHazardTemplate.Nodes);
        //        }
        //    }
        //}
        #endregion

        #region 查找工作阶段及危险源清单
        /// <summary>
        /// 查找工作阶段及危险源清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnWorkStage_OnClick(object sender, EventArgs e)
        {
            Session["workStages"] = null;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("HazardTemplate.aspx?HazardListId={0}&WorkStageIds={1}", this.HazardListId, this.hdWorkStage.Text, "工作阶段和危险源清单 - ")));
        }

        /// <summary>
        /// 关闭查找工作阶段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["workStages"] != null)
            {
                string newWorkStages = string.Empty;
                string workStages = Session["workStages"].ToString();
                List<string> templates = workStages.Split('|').ToList();
                foreach (var item in templates)
                {
                    Model.Technique_HazardList template = BLL.HazardListService.GetHazardListById(item.Split(',').ToList()[0]);
                    Model.HSSE_HazardTemplate hazardTemplate = new Model.HSSE_HazardTemplate
                    {
                        HazardId = template.HazardId,
                        HazardListTypeId = template.HazardListTypeId,
                        WorkStage = item.Split(',').ToList()[1],
                        HazardItems = template.HazardItems,
                        DefectsType = template.DefectsType,
                        MayLeadAccidents = template.MayLeadAccidents,
                        HelperMethod = template.HelperMethod,
                        HazardJudge_L = template.HazardJudge_L,
                        HazardJudge_E = template.HazardJudge_E,
                        HazardJudge_C = template.HazardJudge_C,
                        HazardJudge_D = template.HazardJudge_D,
                        HazardLevel = template.HazardLevel,
                        ControlMeasures = template.ControlMeasures
                    };
                    hazardTemplates.Add(hazardTemplate);
                    workStageIds.Add(item.Split(',').ToList()[1]);//把工作阶段放入集合里    

                }
                //获取工作阶段加载到文本中
                foreach (var i in workStageIds.Distinct().ToList())
                {
                    this.hdWorkStage.Text = i;
                    newWorkStages += WorkStageService.GetWorkStageById(this.hdWorkStage.Text).WorkStageName + ",";
                }
                if (!string.IsNullOrEmpty(newWorkStages))
                {
                    this.txtWorkStage.Text = newWorkStages.Substring(0, newWorkStages.LastIndexOf(","));
                }

                //循环集合是否存在相同项，如果存在，移除重复项
                for (int i = 0; i < hazardTemplates.Count(); i++)
                {
                    for (int j = hazardTemplates.Count() - 1; j > i; j--)
                    {
                        if (hazardTemplates[j].HazardId.Equals(hazardTemplates[i].HazardId))
                        {
                            hazardTemplates.Remove(hazardTemplates[j]);
                        }
                    }
                }
                this.Grid1.DataSource = hazardTemplates;
                this.Grid1.DataBind();
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(object WorkStage)
        {
            string name = string.Empty;
            if (WorkStage != null)
            {
                string workStage = WorkStage.ToString().Trim();
                Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(workStage); 
                if (c != null)
                {
                    name = c.WorkStageName;
                }
            }
            return name;
        }

        /// <summary>
        /// 获取危险源编号
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertHazardCode(object HazardId)
        {
            string hazardCode = string.Empty;
            if (HazardId != null)
            {
                Model.Technique_HazardList hazardList = BLL.HazardListService.GetHazardListById(HazardId.ToString());
                if (hazardList != null)
                {
                    hazardCode = hazardList.HazardCode;
                }
            }
            return hazardCode;
        }

        /// <summary>
        /// 获取危险源类别
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        protected string ConvertSupHazardListTypeId(object hazardListTypeId)
        {
            if (hazardListTypeId != null)
            {
                Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId.ToString());
                if (hazardListType != null)
                {
                    var hazard = BLL.HazardListTypeService.GetHazardListTypeById(hazardListType.SupHazardListTypeId);
                    if (hazard != null)
                    {
                        return hazard.HazardListTypeName;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取危险源项
        /// </summary>
        /// <param name="hazardListTypeId"></param>
        /// <returns></returns>
        protected string ConvertHazardListTypeId(object hazardListTypeId)
        {
            if (hazardListTypeId!=null)
            {
                Model.Technique_HazardListType hazardListType = BLL.HazardListTypeService.GetHazardListTypeById(hazardListTypeId.ToString());
                if (hazardListType!=null)
                {
                    return hazardListType.HazardListTypeName;
                }
            }
            return null;
        }
        #endregion

        #region 选择辅助方法
        /// <summary>
        /// 选择辅助方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHelperMethods_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string assistWays = "";
            for (int i = 0; i < this.drpHelperMethods.Items.Count; i++)
            {
                if (this.drpHelperMethods.Items[i].Selected)
                {
                    if (assistWays == "")
                    {
                        assistWays = this.drpHelperMethods.Items[i].Text;
                    }
                    else
                    {
                        assistWays += "," + this.drpHelperMethods.Items[i].Text;
                    }
                }
            }
            this.drpHelperMethods.Text = assistWays;
            GetHazardTemplates();
        }
        #endregion

        #region 选择危险级别
        /// <summary>
        /// 选择危险级别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpHazardLevel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string hazardGrades = "";
            for (int i = 0; i < this.drpHazardLevel.Items.Count; i++)
            {
                if (this.drpHazardLevel.Items[i].Selected)
                {
                    if (hazardGrades == "")
                    {
                        hazardGrades = this.drpHazardLevel.Items[i].Text;
                    }
                    else
                    {
                        hazardGrades += "," + this.drpHazardLevel.Items[i].Text;
                    }
                }
            }
            this.drpHazardLevel.Text = hazardGrades;
            GetHazardTemplates();
        }
        #endregion

        #region 增加危险源辨识与评价模板
        ///// <summary>
        ///// 增加危险源辨识与评价模板按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(hdWorkStage.Text))
        //    {
        //        this.hdNewTemplates.Text = "";
        //        PageContext.RegisterStartupScript(Window2.GetSaveStateReference(this.hdNewTemplates.ClientID)
        //                 + Window2.GetShowReference(String.Format("HazardTemplate.aspx?workStages={0}", hdWorkStage.Text, "编辑 - ")));
        //    }
        //    else
        //    {
        //        Alert.ShowInTop("请选择工作阶段后再添加项！", MessageBoxIcon.Warning);
        //        return;
        //    }
        //}
        #endregion

        #region 关闭弹出窗口，重新加载树
        /// <summary>
        /// 关闭弹出窗口，重新加载树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Window2_Close(object sender, WindowCloseEventArgs e)
        //{
            //this.HazardSortSetDataBind();

            //string newTemplates = this.hdNewTemplates.Text.Trim();
            //if (!string.IsNullOrEmpty(newTemplates))
            //{
            //    List<string> templates = newTemplates.Split('|').ToList();
            //    jerqueSaveList();
            //    foreach (var item in templates)
            //    {
            //        Model.Technique_HazardList template = BLL.HazardListService.GetHazardListById(item.Split(',').ToList()[0]);
            //        Model.HSSE_HazardTemplate hazardTemplate = new Model.HSSE_HazardTemplate();
            //        hazardTemplate.HazardId = template.HazardId;
            //        hazardTemplate.HazardListTypeId = template.HazardListTypeId;
            //        hazardTemplate.WorkStage = item.Split(',').ToList()[1];
            //        hazardTemplate.HazardItems = template.HazardItems;
            //        hazardTemplate.DefectsType = template.DefectsType;
            //        hazardTemplate.MayLeadAccidents = template.MayLeadAccidents;
            //        hazardTemplate.HelperMethod = template.HelperMethod;
            //        hazardTemplate.HazardJudge_L = template.HazardJudge_L;
            //        hazardTemplate.HazardJudge_E = template.HazardJudge_E;
            //        hazardTemplate.HazardJudge_C = template.HazardJudge_C;
            //        hazardTemplate.HazardJudge_D = template.HazardJudge_D;
            //        hazardTemplate.HazardLevel = template.HazardLevel;
            //        hazardTemplate.ControlMeasures = template.ControlMeasures;

            //        hazardTemplates.Add(hazardTemplate);
            //    }
            //}

            //List<Model.HSSE_HazardTemplate> hazards = new List<Model.HSSE_HazardTemplate>();
            //foreach (Model.HSSE_HazardTemplate q in hazardTemplates)
            //{
            //    if (BLL.HazardListService.GetHazardListById(q.HazardId) == null)
            //    {
            //        hazards.Add(q);
            //    }
            //}

            //foreach (Model.HSSE_HazardTemplate m in hazards)
            //{
            //    hazardTemplates.Remove(m);
            //}

            //this.Grid1.DataSource = hazardTemplates;
            //this.Grid1.DataBind();

            //int rowsCount = this.Grid1.Rows.Count;
            //for (int i = 0; i < rowsCount; i++)
            //{
            //    this.Grid1.Rows[i].Values[0] = "true";
            //}

            //CheckedTvHazardTemplate2(this.tvHazardTemplate.Nodes);
            //this.tvHazardTemplate.Nodes[0].Expanded = true;
        //}
        #endregion

        #region 遍历节点，选中已存在的节点
        ///// <summary>
        ///// 遍历节点,选中已存在的节点
        ///// </summary>
        ///// <param name="nodes"></param>
        //private void CheckedTvHazardTemplate2(TreeNodeCollection nodes)
        //{
        //    foreach (TreeNode tnHazardTemplate in nodes)
        //    {
        //        if (tnHazardTemplate.NodeID != "0" && BLL.WorkStageService.GetWorkStageById(tnHazardTemplate.NodeID) == null)
        //        {
        //            Model.Technique_HazardListType hazardSortSet = (from x in BLL.Funs.DB.Technique_HazardListType where x.HazardListTypeId == tnHazardTemplate.NodeID orderby x.HazardListTypeCode select x).FirstOrDefault();
        //            if (hazardSortSet != null)
        //            {
        //                if (Convert.ToBoolean(hazardSortSet.IsEndLevel))
        //                {
        //                    foreach (Model.HSSE_HazardTemplate hazardTemplate in hazardTemplates)
        //                    {
        //                        if (hazardTemplate.HazardListTypeId == tnHazardTemplate.NodeID)
        //                        {
        //                            tnHazardTemplate.Checked = true;
        //                            ExpandParents(tnHazardTemplate);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    CheckedTvHazardTemplate2(tnHazardTemplate.Nodes);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            CheckedTvHazardTemplate2(tnHazardTemplate.Nodes);
        //        }
        //    }
        //}
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            if (String.IsNullOrEmpty(this.txtWorkStage.Text))
            {
                ShowNotify("请选择工作阶段！", MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(this.drpCompileMan.SelectedValue) && this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                ShowNotify("请选择编制人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            #region 
            Model.Hazard_HazardList hazardList = new Model.Hazard_HazardList
            {
                ProjectId = this.ProjectId,
                HazardListCode = this.txtHazardListCode.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null && String.IsNullOrEmpty(this.drpCompileMan.SelectedValue))
            {
                hazardList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            else
            {
                hazardList.CompileMan = this.CurrUser.UserId;
            }
           
            //hazardList.WorkStage = this.hdWorkStage.Text.Trim();
            string workStageId = string.Empty;
            string workStage = this.txtWorkStage.Text.Trim();
            if (!string.IsNullOrEmpty(workStage))
            {
                List<string> infos = workStage.Split(',').ToList();
                foreach (var item in infos)
                {
                    Model.Base_WorkStage w = BLL.WorkStageService.GetWorkStageByName(item);
                    if (w != null)
                    {
                        workStageId += w.WorkStageId + ",";
                    }
                }
                hazardList.WorkStage = workStageId;
            }


            if (!string.IsNullOrEmpty(this.txtCompileDate.Text.Trim()))
            {
                hazardList.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
            }
            hazardList.Contents = HttpUtility.HtmlEncode(this.txtContents.Text);
            ////单据状态
            hazardList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                hazardList.States = this.ctlAuditFlow.NextStep;
                if (hazardList.States == BLL.Const.State_2)
                {
                    hazardList.VersionNo = BLL.SQLHelper.RunProcNewId2("SpGetVersionNumber", "Hazard_HazardList", "VersionNo", this.ProjectId);
                }
            }

            hazardList.WorkAreaName = this.txtWorkArea.Text.Trim();
            hazardList.IdentificationDate = Funs.GetNewDateTime(this.txtIdentificationDate.Text.Trim());
            if (this.drpControllingPerson.SelectedValue != BLL.Const._Null)
            {
                hazardList.ControllingPerson = this.drpControllingPerson.SelectedValue;
            }
            if (!string.IsNullOrEmpty(HazardListId))
            {
                hazardList.HazardListId = HazardListId;
                BLL.Hazard_HazardListService.UpdateHazardList(hazardList);
                BLL.Hazard_HazardSelectedItemService.DeleteHazardSelectedItemByHazardListId(HazardListId);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardListCode, hazardList.HazardListId, BLL.Const.ProjectHazardListMenuId, BLL.Const.BtnModify);
            }
            else
            {
                hazardList.HazardListId = SQLHelper.GetNewID(typeof(Model.Hazard_HazardList));
                this.HazardListId = hazardList.HazardListId;
                BLL.Hazard_HazardListService.AddHazardList(hazardList);
                BLL.LogService.AddSys_Log(this.CurrUser, hazardList.HazardListCode, hazardList.HazardListId, BLL.Const.ProjectHazardListMenuId, BLL.Const.BtnAdd);
            }
            #endregion
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                Model.Hazard_HazardSelectedItem hazardSelectedItem = new Model.Hazard_HazardSelectedItem
                {
                    HazardId = values.Value<string>("HazardId"),
                    HazardListTypeId = values.Value<string>("HazardListTypeId"),
                    WorkStage = values.Value<string>("WorkStage"),
                    HazardListId = hazardList.HazardListId,
                    HazardItems = values.Value<string>("HazardItems"),
                    DefectsType = values.Value<string>("DefectsType"),
                    MayLeadAccidents = values.Value<string>("MayLeadAccidents"),
                    HelperMethod = values.Value<string>("HelperMethod"),
                    HazardJudge_L = Funs.GetNewDecimalOrZero(values.Value<string>("L")),
                    HazardJudge_E = Funs.GetNewDecimalOrZero(values.Value<string>("E")),
                    HazardJudge_C = Funs.GetNewDecimalOrZero(values.Value<string>("C")),
                    HazardJudge_D = Funs.GetNewDecimalOrZero(values.Value<string>("D")),
                    HazardLevel = values.Value<string>("G"),
                    ControlMeasures = values.Value<string>("ControlMeasures"),
                    IsResponse = false
                };
                BLL.Hazard_HazardSelectedItemService.AddHazardSelectedItem(hazardSelectedItem);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckWorkMenuId, this.HazardListId, (type == BLL.Const.BtnSubmit ? true : false), ConvertWorkStage(hazardList.WorkStage), "../Hazard/HazardListView.aspx?HazardListId={0}");
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 转换工作阶段
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertWorkStage(string workStage)
        {
            if (workStage != null)
            {
                string workStages = string.Empty;
                string[] strList = workStage.ToString().Split(',');
                foreach (string str in strList)
                {
                    Model.Base_WorkStage c = BLL.WorkStageService.GetWorkStageById(str);
                    if (c != null)
                    {
                        workStages += c.WorkStageName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(workStages))
                {
                    workStages = workStages.Substring(0, workStages.LastIndexOf(","));
                }
                return workStages;
            }
            return "";
        }
        #endregion

        #region GV被选择项列表
        ///// <summary>
        ///// GV被选择项列表
        ///// </summary>
        //public List<string> ItemSelectedList
        //{
        //    get
        //    {
        //        return (List<string>)ViewState["ItemSelectedList"];
        //    }
        //    set
        //    {
        //        ViewState["ItemSelectedList"] = value;
        //    }
        //}
        #endregion  
     
        #region 区域选择框事件
        /// <summary>
        /// 区域选择框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpWorkArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                this.txtWorkArea.Text = this.drpWorkArea.SelectedText;
            }
            else
            {
                this.txtWorkArea.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.HazardListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HazardListAttachUrl&menuId={1}", this.HazardListId, BLL.Const.ProjectHazardListMenuId)));
        }
        #endregion
    }
}