using System;
using System.Web;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataItemEdit : PageBase
    {
        #region 自定义项
        /// <summary>
        /// 考核计划id
        /// </summary>
        public string SafetyDataPlanId
        {
            get
            {
                return (string)ViewState["SafetyDataPlanId"];
            }
            set
            {
                ViewState["SafetyDataPlanId"] = value;
            }
        }

        /// <summary>
        /// 文件明细id
        /// </summary>
        public string SafetyDataItemId
        {
            get
            {
                return (string)ViewState["SafetyDataItemId"];
            }
            set
            {
                ViewState["SafetyDataItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string SafetyDataId
        {
            get
            {
                return (string)ViewState["SafetyDataId"];
            }
            set
            {
                ViewState["SafetyDataId"] = value;
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

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafetyDataId = Request.Params["SafetyDataId"];
                this.SafetyDataItemId = Request.Params["SafetyDataItemId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                string lbtext = "【提交时间】为系统默认时间，不可修改；";
                var getSafetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(Request.Params["SafetyDataPlanId"]);
                if (getSafetyDataPlan != null)
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.RealStartDate);
                    lbtext = "【所属时段】应在要求上报开始时间" + string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.RealStartDate) +"；"
                        +"结束时间" + string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.RealEndDate) + "；"+ lbtext + getSafetyDataPlan.Remark;

                    this.hdRealStartDate.Text = string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.RealStartDate);
                    this.hdRealEndDate.Text = string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.RealEndDate);
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                }
                this.Label2.Text = lbtext;

                if (!string.IsNullOrEmpty(this.SafetyDataItemId))
                {
                    var projectSafetyDataItem = BLL.SafetyDataItemService.GetSafetyDataItemByID(this.SafetyDataItemId);
                    if (projectSafetyDataItem != null)
                    {
                        this.ProjectId = projectSafetyDataItem.ProjectId;
                        this.SafetyDataId = projectSafetyDataItem.SafetyDataId;
                        ///读取编号
                        this.txtCode.Text = projectSafetyDataItem.Code;   ///编号是上级编号 + 流水号
                        this.hdSortIndex.Text = projectSafetyDataItem.SortIndex.ToString();
                        this.txtTitle.Text = projectSafetyDataItem.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataItem.CompileDate);
                        this.txtSubmitDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataItem.SubmitDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(projectSafetyDataItem.FileContent);
                    }
                }
                else
                {
                    string newCode = BLL.SafetyDataItemService.GetNewSafetyDataItemCode(this.CurrUser.LoginProjectId, this.SafetyDataId);
                    this.hdSortIndex.Text = newCode;
                    var safeData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.SafetyDataId);
                    if (safeData != null && !string.IsNullOrEmpty(safeData.Code))
                    {
                        newCode = safeData.Code + "-" + newCode;
                    }
                    var project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                    if (project != null)
                    {
                        newCode = project.ProjectCode + "-" + newCode;
                    }
                    this.txtCode.Text = newCode;
                    this.txtTitle.Text = safeData.Title;
                   
                    this.txtSubmitDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtFileContent.Text = HttpUtility.HtmlDecode(safeData.Title);
                }
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            if (!string.IsNullOrEmpty(this.txtTitle.Text))
            {
                if (!CopDate())
                {
                    Alert.ShowInTop("所属时段必须在当前列表记录设置的开始时间与结束时间之间！");
                    return;
                }
                Model.SafetyData_SafetyDataItem newProjectSafetyDataItem = new Model.SafetyData_SafetyDataItem
                {
                    SafetyDataId = this.SafetyDataId,
                    Title = this.txtTitle.Text.Trim(),
                    Code = this.txtCode.Text.Trim(),
                    SortIndex = Funs.GetNewInt(this.hdSortIndex.Text),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text),
                    CompileDate = System.DateTime.Now,
                    CompileMan = this.CurrUser.UserId,
                    ProjectId = this.ProjectId
                };
                newProjectSafetyDataItem.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
                if (string.IsNullOrEmpty(this.SafetyDataItemId))
                {
                    newProjectSafetyDataItem.IsMenu = false;
                    newProjectSafetyDataItem.SubmitDate = System.DateTime.Now;
                    this.SafetyDataItemId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataItem));
                    newProjectSafetyDataItem.SafetyDataItemId = this.SafetyDataItemId;
                    BLL.SafetyDataItemService.AddSafetyDataItem(newProjectSafetyDataItem);
                }
                else
                {
                    newProjectSafetyDataItem.SafetyDataItemId = this.SafetyDataItemId;
                    BLL.SafetyDataItemService.UpdateSafetyDataItem(newProjectSafetyDataItem);
                }

                if (isClose)
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }
            else
            {
                Alert.ShowInParent("文件标题不能为空！");
                return;
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
            if (string.IsNullOrEmpty(this.SafetyDataItemId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafetyDataAttachUrl&menuId={1}", this.SafetyDataItemId, BLL.Const.ProjectSafetyDataMenuId)));
        }
        #endregion


        protected void txtCompileDate_TextChanged(object sender, EventArgs e)
        {
            if (!CopDate())
            {
                Alert.ShowInTop("所属时段必须在当前列表记录设置的开始时间与结束时间之间！");
            }
        }

        private bool CopDate()
        {
            bool isOk = false;
            DateTime? compileDate = Funs.GetNewDateTime(this.txtCompileDate.Text);
            DateTime? startDate = Funs.GetNewDateTime(this.hdRealStartDate.Text);
            DateTime? endDate = Funs.GetNewDateTime(this.hdRealEndDate.Text);
            if (!startDate.HasValue || !endDate.HasValue || (compileDate >= startDate && compileDate <= endDate))
            {
                isOk = true;
            }
            return isOk;
        }
    }
}