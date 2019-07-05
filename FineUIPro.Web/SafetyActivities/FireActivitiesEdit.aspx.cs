using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class FireActivitiesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FireActivitiesId
        {
            get
            {
                return (string)ViewState["FireActivitiesId"];
            }
            set
            {
                ViewState["FireActivitiesId"] = value;
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
  
                this.FireActivitiesId = Request.Params["FireActivitiesId"];
                if (!string.IsNullOrEmpty(this.FireActivitiesId))
                {
                    Model.SafetyActivities_FireActivities FireActivities = BLL.FireActivitiesService.GetFireActivitiesById(this.FireActivitiesId);
                    if (FireActivities != null)
                    {
                        this.ProjectId = FireActivities.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = FireActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", FireActivities.CompileDate);
                        if (!string.IsNullOrEmpty(FireActivities.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = FireActivities.CompileMan;
                        }
                        this.txtRemark.Text = FireActivities.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(FireActivities.SeeFile);                                               
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectFireActivitiesMenuId, this.ProjectId);
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
            Model.SafetyActivities_FireActivities newFireActivities = new Model.SafetyActivities_FireActivities
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newFireActivities.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newFireActivities.Remark = this.txtRemark.Text.Trim();
            newFireActivities.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.FireActivitiesId))
            {
                newFireActivities.FireActivitiesId = this.FireActivitiesId;
                BLL.FireActivitiesService.UpdateFireActivities(newFireActivities);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改119消防活动", newFireActivities.FireActivitiesId);
            }
            else
            {
                this.FireActivitiesId = SQLHelper.GetNewID(typeof(Model.SafetyActivities_FireActivities));
                newFireActivities.FireActivitiesId = this.FireActivitiesId;
                BLL.FireActivitiesService.AddFireActivities(newFireActivities);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加119消防活动", newFireActivities.FireActivitiesId);
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
            if (string.IsNullOrEmpty(this.FireActivitiesId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/FireActivitiesAttachUrl&menuId={1}", FireActivitiesId,BLL.Const.ProjectFireActivitiesMenuId)));
        }
        #endregion
    }
}