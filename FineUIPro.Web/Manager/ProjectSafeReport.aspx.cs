using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ProjectSafeReport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全文件上报主键
        /// </summary>
        public string SafeReportId
        {
            get
            {
                return (string)ViewState["SafeReportId"];
            }
            set
            {
                ViewState["SafeReportId"] = value;
            }
        }
        /// <summary>
        /// 安全文件上报明细主键
        /// </summary>
        public string SafeReportItemId
        {
            get
            {
                return (string)ViewState["SafeReportItemId"];
            }
            set
            {
                ViewState["SafeReportItemId"] = value;
            }
        }
        /// <summary>
        /// 项目id
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
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            if (!IsPostBack && this.CurrUser != null)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                this.GetButtonPower();  ////得到权限   
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpReportManId, this.ProjectId, null, true);
                this.InitTreeMenu(); ////初始化树节点
            }
        }
        #endregion

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trSafeReport.Nodes.Clear();            
            trSafeReport.EnableIcons = true;
            trSafeReport.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "项目安全文件",
                NodeID = "0",
                Expanded = true,
                EnableClickEvent = true,
            };

            this.trSafeReport.Nodes.Add(rootNode);
            List<Model.Manager_SafeReport> safeReports = BLL.SafeReportService.getProjectSafeReportList(this.ProjectId);
            BoundTree(rootNode.Nodes, "0", safeReports);
        }

        /// <summary>
        /// 绑定树 数据
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="supSafeReportId"></param>
        private void BoundTree(TreeNodeCollection nodes, string supSafeReportId, List<Model.Manager_SafeReport> safeReports)
        {
            var getSafeReporytsList = safeReports.Where(x => x.SupSafeReportId == supSafeReportId);
            if (getSafeReporytsList.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in getSafeReporytsList)
                {
                    tn = new TreeNode
                    {
                        Text = "[" + dr.SafeReportCode + "]" + dr.SafeReportName,
                        NodeID = dr.SafeReportId,
                        ToolTip = "[" + dr.SafeReportCode + "]" + dr.SafeReportName,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    if (!BLL.SafeReportService.IsUpLoadSafeReport(dr.SafeReportId, this.ProjectId))
                    {
                        tn.Text = "<font color='#FF7575'>" + tn.Text + "</font>";
                        this.SetNodeColor(tn);
                    }
                    BoundTree(tn.Nodes, dr.SafeReportId, safeReports);
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
                this.SetNodeColor(tn.ParentNode);
            }
        }
        #endregion

        #region Tree选择事件
        /// <summary>
        /// Tree选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trSafeReport_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            if (this.trSafeReport.SelectedNode != null)
            {
                this.SetPageText();
            }
        }

        private void SetPageText()
        {
            this.txtSafeReportCode.Text = string.Empty;
            this.txtSafeReportName.Text = string.Empty;
            this.txtRequestTime.Text = string.Empty;
            this.txtRequirement.Text = string.Empty;
            this.SimpleForm1.Hidden = true;

            this.SafeReportItemId = string.Empty;
            this.txtReportContent.Text = string.Empty;
            this.txtReportTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            this.txtUpReportTime.Text = string.Empty;
            this.drpReportManId.SelectedValue = this.CurrUser.UserId;

            this.SafeReportId = this.trSafeReport.SelectedNode.NodeID;
            var safeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(this.SafeReportId);
            if (safeReport != null)
            {
                this.SimpleForm1.Hidden = false;
                this.txtSafeReportCode.Text = safeReport.SafeReportCode;
                this.txtSafeReportName.Text = safeReport.SafeReportName;
                this.txtRequestTime.Text = string.Format("{0:yyyy-MM-dd}", safeReport.RequestTime);
                this.txtRequirement.Text = safeReport.Requirement;
                this.lbState.Text = "未上报";
                var safeReportItem = BLL.SafeReportItemService.GetSafeReportItemBySafeReportProjectId(this.SafeReportId, this.ProjectId);
                if (safeReportItem != null)
                {
                    this.SafeReportItemId = safeReportItem.SafeReportItemId;
                    this.txtReportContent.Text = safeReportItem.ReportContent;
                    if (safeReportItem.ReportTime.HasValue)
                    {
                        this.txtReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeReportItem.ReportTime);
                    }
                    this.txtUpReportTime.Text = string.Format("{0:yyyy-MM-dd}", safeReportItem.UpReportTime);
                    if (!string.IsNullOrEmpty(safeReportItem.ReportManId))
                    {
                        this.drpReportManId.SelectedValue = safeReportItem.ReportManId;
                    }
                    if (safeReportItem.States == BLL.Const.State_2)
                    {
                        this.lbState.Text = "已上报";
                    }
                }                
            }
            //else
            //{
            //    Alert.ShowInTop("请选择末级安全文件目录！", MessageBoxIcon.Warning);
            //}
        }
        #endregion     

        #region 标准模板附件查看
        /// <summary>
        /// 标准模板附件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTemplateView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SafeReportId))
            {
               PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ServerSafeReportAttachUrl&menuId={1}&type=-1", this.SafeReportId, BLL.Const.ServerSafeReportMenuId)));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectSafeReportMenuId);
            if (buttonList.Count() > 0)
            {              
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSubmit.Hidden = false;
                    this.btnSave.Hidden = false;
                }
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
            if (!string.IsNullOrEmpty(this.SafeReportItemId))
            {
                if (this.btnSubmit.Hidden)
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafeReportAttachUrl&menuId={1}type=-1", this.SafeReportItemId, BLL.Const.ProjectSafeReportMenuId)));
                }
                else
                {
                    PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafeReportAttachUrl&menuId={1}", this.SafeReportItemId, BLL.Const.ProjectSafeReportMenuId)));
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            var updateSafeReportItem = BLL.SafeReportItemService.GetSafeReportItemBySafeReportItemId(this.SafeReportItemId);
            if (updateSafeReportItem != null)
            {
                updateSafeReportItem.ReportContent = this.txtReportContent.Text.Trim();
                if (this.drpReportManId.SelectedValue != BLL.Const._Null)
                {
                    updateSafeReportItem.ReportManId = this.drpReportManId.SelectedValue;
                }
                updateSafeReportItem.ReportTime = Funs.GetNewDateTime(this.txtReportTime.Text.Trim());
              
                updateSafeReportItem.States = BLL.Const.State_0;
                updateSafeReportItem.UpReportTime = null;
                if (type == BLL.Const.BtnSubmit)
                {
                    updateSafeReportItem.States = BLL.Const.State_2;
                    updateSafeReportItem.UpReportTime = System.DateTime.Now;
                }
                BLL.SafeReportItemService.UpdateSafeReportItem(updateSafeReportItem);
                BLL.LogService.AddSys_Log(this.CurrUser, this.txtReportTime.Text, updateSafeReportItem.SafeReportItemId, BLL.Const.ProjectSafeReportMenuId, BLL.Const.BtnModify );
            }
            this.InitTreeMenu(); 
            this.SetPageText();
        }
        #endregion
    }
}