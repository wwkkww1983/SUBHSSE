using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ServerCheck
{
    public partial class SubUnitReport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SubUnitReportId
        {
            get
            {
                return (string)ViewState["SubUnitReportId"];
            }
            set
            {
                ViewState["SubUnitReportId"] = value;
            }
        }

        /// <summary>
        /// 明细主键
        /// </summary>
        public string SubUnitReportItemId
        {
            get
            {
                return (string)ViewState["SubUnitReportItemId"];
            }
            set
            {
                ViewState["SubUnitReportItemId"] = value;
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
                this.GetButtonPower();               
                this.InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trSubUnitReport.Nodes.Clear();
            trSubUnitReport.ShowBorder = false;
            trSubUnitReport.ShowHeader = false;
            trSubUnitReport.EnableIcons = true;
            trSubUnitReport.AutoScroll = true;
            trSubUnitReport.EnableSingleClickExpand = true;
            //TreeNode rootNode = new TreeNode
            //{
            //    Text = "企业安全文件",
            //    NodeID = "0",
            //    Expanded = true
            //};
            //this.trSubUnitReport.Nodes.Add(rootNode);
            BoundTree(this.trSubUnitReport.Nodes, "0");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewSubUnitReport(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.SubUnitReportName,
                        NodeID = dr.SubUnitReportId,
                        ToolTip = "[" + dr.SubUnitReportCode + "]" + dr.SubUnitReportName,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);

                    if (tn != null)
                    {
                        if (BLL.SubUnitReportService.IsUpLoadSubUnitReport(dr.SubUnitReportId))
                        {
                            tn.Text = "<font color='#FF7575'>" + tn.Text + "</font>";
                            this.SetNodeColor(tn);
                        }
                    }

                    BoundTree(tn.Nodes, dr.SubUnitReportId);
                }
            }
        }

        /// <summary>
        ///  设置父级节点颜色
        /// </summary>
        private void SetNodeColor(TreeNode tn)
        {
            if (tn.NodeID != "0")
            {                
                tn.Text = "<font color='#FF7575'>" + tn.Text + "</font>";
                if (tn.ParentNode != null)
                {
                    this.SetNodeColor(tn.ParentNode);
                }
            }
        }
        #endregion

        #region 得到菜单方法
        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Supervise_SubUnitReport> GetNewSubUnitReport(string parentId)
        {
            return (from x in Funs.DB.Supervise_SubUnitReport where x.SupSubUnitReportId == parentId orderby x.SubUnitReportCode descending select x).ToList(); ;
        }
        #endregion

        #region 树点击事件
        /// <summary>
        /// 选择Tree事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trSubUnitReport_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.SubUnitReportId = string.Empty;
            this.SubUnitReportItemId = string.Empty;
            this.dpkReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            this.txtPlanReortDate.Text = string.Empty;
            this.formTitle.Title = string.Empty;
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null)
            {
                this.txtUnitName.Text = thisUnit.UnitName;
            }
            this.panelCenterRegion.Hidden = true;
            this.SubUnitReportId = this.trSubUnitReport.SelectedNodeID;          
            var subUnitReortItem = BLL.SubUnitReportItemService.GetSubUnitReportItemBySubUnitReportId(this.SubUnitReportId);
            if (subUnitReortItem != null)
            {
                this.panelCenterRegion.Hidden = false;
                this.txtReportTitle.Text = subUnitReortItem.ReportTitle;
                this.dpkReportDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.SubUnitReportItemId = subUnitReortItem.SubUnitReportItemId;
                if (!string.IsNullOrEmpty(subUnitReortItem.UnitId))
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(subUnitReortItem.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                    }
                }
                if (subUnitReortItem.PlanReortDate != null)
                {
                    this.txtPlanReortDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitReortItem.PlanReortDate);
                }
                this.txtReportTitle.Text = subUnitReortItem.ReportTitle;
                this.txtReportContent.Text = subUnitReortItem.ReportContent;
                if (subUnitReortItem.ReportDate.HasValue)
                {
                    this.dpkReportDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitReortItem.ReportDate);
                }
                if (subUnitReortItem.UpState == Const.UpState_3)
                {
                    this.formTitle.Title = "上报状态：已上报";
                    if (this.CurrUser.UserId != BLL.Const.sysglyId)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSaveUp.Hidden = true;
                    }
                }
                else
                {
                    this.formTitle.Title = "上报状态：未上报";
                    this.btnSave.Hidden = false;
                    this.btnSaveUp.Hidden = false;
                }
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
            if (!string.IsNullOrEmpty(this.trSubUnitReport.SelectedNodeID))
            {
                SaveData(BLL.Const.UpState_1);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                this.InitTreeMenu();
                this.EmptyText();
            }
            else
            {
                ShowNotify("请选择上报名称！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.trSubUnitReport.SelectedNodeID))
            {
                SaveData(BLL.Const.UpState_2);
                this.UpSubUnitReport(SubUnitReportId);//上报
                this.InitTreeMenu();
                this.EmptyText();
            }
            else
            {
                ShowNotify("请选择上报名称！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string upState)
        {
            Model.Supervise_SubUnitReportItem item = new Model.Supervise_SubUnitReportItem
            {
                ReportTitle = this.txtReportTitle.Text.Trim(),
                ReportContent = this.txtReportContent.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkReportDate.Text.Trim()))
            {
                item.ReportDate = Convert.ToDateTime(this.dpkReportDate.Text.Trim());
            }           
            item.UpState = upState;
            if (!string.IsNullOrEmpty(this.SubUnitReportItemId))
            {
                item.SubUnitReportItemId = this.SubUnitReportItemId;
                BLL.SubUnitReportItemService.UpdateSubUnitReportItem(item);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtReportTitle.Text.Trim(), item.SubUnitReportItemId, BLL.Const.SubUnitReportMenuId, BLL.Const.BtnModify);
            }
        }
        #endregion

        #region 子单位上报到集团单位
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="SubUnitReportId"></param>
        /// <param name="p"></param>
        private void UpSubUnitReport(string SubUnitReportId)
        {
            ///创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertSupervise_SubUnitReportItemItemTableCompleted += new EventHandler<HSSEService.DataInsertSupervise_SubUnitReportItemItemTableCompletedEventArgs>(poxy_DataInsertSupervise_SubUnitReportTableCompleted);
            var subUnitReport = from x in Funs.DB.View_Supervise_SubUnitReportItem
                               // join y in Funs.DB.AttachFile on x.SubUnitReportItemId equals y.ToKeyId
                                where x.SubUnitReportId == SubUnitReportId && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4 || x.UpState == null)
                                select new BLL.HSSEService.Supervise_SubUnitReportItem
                                {
                                    SubUnitReportItemId = x.SubUnitReportItemId,
                                    ReportTitle = x.ReportTitle,
                                    ReportContent = x.ReportContent,
                                   // AttachUrl = x.AttachUrl,
                                    ReportDate = x.ReportDate,
                                    State = x.State,
                                    ////附件转为字节传送
                                    //FileContext = FileStructService.GetFileStructByAttachUrl(x.AttachUrl),

                                    AttachFileId = x.AttachFileId,
                                    ToKeyId = x.ToKeyId,
                                    AttachSource = x.AttachSource,
                                    AttachUrl = x.AttachUrl,
                                    ////附件转为字节传送
                                    FileContext = FileStructService.GetMoreFileStructByAttachUrl(x.AttachUrl),

                                };
            poxy.DataInsertSupervise_SubUnitReportItemItemTableAsync(subUnitReport.ToList());
        }

        /// <summary>
        /// 企业安全文件上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertSupervise_SubUnitReportTableCompleted(object sender, HSSEService.DataInsertSupervise_SubUnitReportItemItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var subUnitReportItem = BLL.SubUnitReportItemService.GetSubUnitReportItemById(item);
                    if (subUnitReportItem != null)
                    {
                        subUnitReportItem.UpState = BLL.Const.UpState_3;
                        subUnitReportItem.State = BLL.Const.UpState_3;
                        BLL.SubUnitReportItemService.UpdateSubUnitReportItem(subUnitReportItem);
                    }
                }
                this.InitTreeMenu();
                this.EmptyText();

                ShowNotify("【企业安全文件上报】上报到集团公司成功！", MessageBoxIcon.Success);
                BLL.LogService.AddSys_Log(this.CurrUser, "【企业安全文件上报】上报到集团公司" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.SubUnitReportMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                ShowNotify("【企业安全文件上报】上报到集团公司失败！", MessageBoxIcon.Warning);
                BLL.LogService.AddSys_Log(this.CurrUser, "【企业安全文件上报】上报到集团公司失败；",string.Empty,BLL.Const.SubUnitReportMenuId,BLL.Const.BtnUploadResources);
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
            if (!string.IsNullOrEmpty(this.SubUnitReportItemId))
            {
                if (this.btnSave.Hidden)
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/SubUnitReport&menuId=" + BLL.Const.SubUnitReportMenuId, this.SubUnitReportItemId)));
                }
                else
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubUnitReport&menuId=" + BLL.Const.SubUnitReportMenuId, this.SubUnitReportItemId)));
                }
            }
            else
            {
                ShowNotify("请选择上报名称！", MessageBoxIcon.Warning);
                return;
            }
                           
        }
        #endregion

        #region 清空文本
        /// <summary>
        /// 清空文本
        /// </summary>
        private void EmptyText()
        {
            this.SubUnitReportId = string.Empty;
            this.SubUnitReportItemId = string.Empty;
            this.txtReportTitle.Text = string.Empty;
            this.txtReportContent.Text = string.Empty;
            this.dpkReportDate.Text = string.Empty;       
            this.formTitle.Title = string.Empty;
            this.txtUnitName.Text = string.Empty;
        }
        #endregion

        #region 按钮权限
        /// <summary>
        /// 按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SubUnitReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSaveUp.Hidden = false;
                }
            }
        }
        #endregion
    }
}