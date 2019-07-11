using System;
using System.Web;
using BLL;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class ProjectSafetyDataEItemEdit : PageBase
    {
        #region 自定义项
        /// <summary>
        /// 考核计划id
        /// </summary>
        public string SafetyDataEPlanId
        {
            get
            {
                return (string)ViewState["SafetyDataEPlanId"];
            }
            set
            {
                ViewState["SafetyDataEPlanId"] = value;
            }
        }

        /// <summary>
        /// 文件明细id
        /// </summary>
        public string SafetyDataEItemId
        {
            get
            {
                return (string)ViewState["SafetyDataEItemId"];
            }
            set
            {
                ViewState["SafetyDataEItemId"] = value;
            }
        }

        /// <summary>
        /// 文件项id
        /// </summary>
        public string SafetyDataEId
        {
            get
            {
                return (string)ViewState["SafetyDataEId"];
            }
            set
            {
                ViewState["SafetyDataEId"] = value;
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
                this.SafetyDataEId = Request.Params["SafetyDataEId"];
                this.SafetyDataEItemId = Request.Params["SafetyDataEItemId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                var getSafetyDataPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEIdProjectId(this.SafetyDataEId, this.ProjectId);
                if (getSafetyDataPlan != null)
                {
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", getSafetyDataPlan.CheckDate);
                }
                if (!string.IsNullOrEmpty(this.SafetyDataEItemId))
                {
                    var projectSafetyDataEItem = BLL.SafetyDataEItemService.GetSafetyDataEItemByID(this.SafetyDataEItemId);
                    if (projectSafetyDataEItem != null)
                    {
                        this.ProjectId = projectSafetyDataEItem.ProjectId;
                        this.SafetyDataEId = projectSafetyDataEItem.SafetyDataEId;
                        ///读取编号
                        this.txtCode.Text = projectSafetyDataEItem.Code;   ///编号是上级编号 + 流水号
                        this.hdSortIndex.Text = projectSafetyDataEItem.SortIndex.ToString();
                        this.txtTitle.Text = projectSafetyDataEItem.Title;                      
                        this.txtSubmitDate.Text = string.Format("{0:yyyy-MM-dd}", projectSafetyDataEItem.SubmitDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(projectSafetyDataEItem.FileContent);
                    }
                }
                else
                {
                    string newCode = BLL.SafetyDataEItemService.GetNewSafetyDataEItemCode(this.CurrUser.LoginProjectId, this.SafetyDataEId);
                    this.hdSortIndex.Text = newCode;
                    var safeData = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.SafetyDataEId);
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
                Model.SafetyDataE_SafetyDataEItem newProjectSafetyDataEItem = new Model.SafetyDataE_SafetyDataEItem
                {
                    SafetyDataEId = this.SafetyDataEId,
                    Title = this.txtTitle.Text.Trim(),
                    Code = this.txtCode.Text.Trim(),
                    SortIndex = Funs.GetNewInt(this.hdSortIndex.Text),
                    FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text),
                    CompileDate = System.DateTime.Now,
                    CompileMan = this.CurrUser.UserId,
                    ProjectId = this.ProjectId
                };
                
                if (string.IsNullOrEmpty(this.SafetyDataEItemId))
                {
                    newProjectSafetyDataEItem.IsMenu = false;
                    newProjectSafetyDataEItem.SubmitDate = System.DateTime.Now;
                    this.SafetyDataEItemId = SQLHelper.GetNewID(typeof(Model.SafetyDataE_SafetyDataEItem));
                    newProjectSafetyDataEItem.SafetyDataEItemId = this.SafetyDataEItemId;
                    BLL.SafetyDataEItemService.AddSafetyDataEItem(newProjectSafetyDataEItem);
                }
                else
                {
                    newProjectSafetyDataEItem.SafetyDataEItemId = this.SafetyDataEItemId;
                    BLL.SafetyDataEItemService.UpdateSafetyDataEItem(newProjectSafetyDataEItem);
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
            if (string.IsNullOrEmpty(this.SafetyDataEItemId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectSafetyDataEAttachUrl&menuId={1}&Type=0", this.SafetyDataEItemId, BLL.Const.ProjectSafetyDataEMenuId)));
        }
        #endregion
    }
}