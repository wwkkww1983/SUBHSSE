using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class ThemeActivitiesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ThemeActivitiesId
        {
            get
            {
                return (string)ViewState["ThemeActivitiesId"];
            }
            set
            {
                ViewState["ThemeActivitiesId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
  
                this.ThemeActivitiesId = Request.Params["ThemeActivitiesId"];
                if (!string.IsNullOrEmpty(this.ThemeActivitiesId))
                {
                    Model.SafetyActivities_ThemeActivities ThemeActivities = BLL.ThemeActivitiesService.GetThemeActivitiesById(this.ThemeActivitiesId);
                    if (ThemeActivities != null)
                    {
                        this.ProjectId = ThemeActivities.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = ThemeActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ThemeActivities.CompileDate);
                        if (!string.IsNullOrEmpty(ThemeActivities.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = ThemeActivities.CompileMan;
                        }
                        this.txtRemark.Text = ThemeActivities.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(ThemeActivities.SeeFile);                                               
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectThemeActivitiesMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                                        
                    this.txtTitle.Text = this.SimpleForm1.Title;
                }
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

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
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.SafetyActivities_ThemeActivities newThemeActivities = new Model.SafetyActivities_ThemeActivities
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newThemeActivities.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newThemeActivities.Remark = this.txtRemark.Text.Trim();
            newThemeActivities.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.ThemeActivitiesId))
            {
                newThemeActivities.ThemeActivitiesId = this.ThemeActivitiesId;
                BLL.ThemeActivitiesService.UpdateThemeActivities(newThemeActivities);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改主题安全活动", newThemeActivities.ThemeActivitiesId);
            }
            else
            {
                this.ThemeActivitiesId = SQLHelper.GetNewID(typeof(Model.SafetyActivities_ThemeActivities));
                newThemeActivities.ThemeActivitiesId = this.ThemeActivitiesId;
                BLL.ThemeActivitiesService.AddThemeActivities(newThemeActivities);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加主题安全活动", newThemeActivities.ThemeActivitiesId);
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
            if (string.IsNullOrEmpty(this.ThemeActivitiesId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ThemeActivitiesAttachUrl&menuId={1}", ThemeActivitiesId,BLL.Const.ProjectThemeActivitiesMenuId)));
        }
        #endregion
    }
}