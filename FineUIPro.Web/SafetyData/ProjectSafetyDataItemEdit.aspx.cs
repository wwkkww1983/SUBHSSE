using System;
using System.Web;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataItemEdit : PageBase
    {
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
                    var safetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(Request.Params["SafetyDataCheckItemId"]);
                    if (safetyDataCheckItem != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", safetyDataCheckItem.StartDate);
                    }
                    else
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    }
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
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加项目文件", newProjectSafetyDataItem.Code);
                }
                else
                {
                    newProjectSafetyDataItem.SafetyDataItemId = this.SafetyDataItemId;
                    BLL.SafetyDataItemService.UpdateSafetyDataItem(newProjectSafetyDataItem);
                    BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改项目文件", newProjectSafetyDataItem.Code);
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
    }
}