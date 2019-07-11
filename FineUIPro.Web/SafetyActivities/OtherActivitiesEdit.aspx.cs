using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class OtherActivitiesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string OtherActivitiesId
        {
            get
            {
                return (string)ViewState["OtherActivitiesId"];
            }
            set
            {
                ViewState["OtherActivitiesId"] = value;
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
  
                this.OtherActivitiesId = Request.Params["OtherActivitiesId"];
                if (!string.IsNullOrEmpty(this.OtherActivitiesId))
                {
                    Model.SafetyActivities_OtherActivities OtherActivities = BLL.OtherActivitiesService.GetOtherActivitiesById(this.OtherActivitiesId);
                    if (OtherActivities != null)
                    {
                        this.ProjectId = OtherActivities.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = OtherActivities.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", OtherActivities.CompileDate);
                        if (!string.IsNullOrEmpty(OtherActivities.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = OtherActivities.CompileMan;
                        }
                        this.txtRemark.Text = OtherActivities.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(OtherActivities.SeeFile);                                               
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectOtherActivitiesMenuId, this.ProjectId);
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
            Model.SafetyActivities_OtherActivities newOtherActivities = new Model.SafetyActivities_OtherActivities
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newOtherActivities.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newOtherActivities.Remark = this.txtRemark.Text.Trim();
            newOtherActivities.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.OtherActivitiesId))
            {
                newOtherActivities.OtherActivitiesId = this.OtherActivitiesId;
                BLL.OtherActivitiesService.UpdateOtherActivities(newOtherActivities);
                BLL.LogService.AddSys_Log(this.CurrUser, newOtherActivities.Title, newOtherActivities.OtherActivitiesId, BLL.Const.ProjectOtherActivitiesMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.OtherActivitiesId = SQLHelper.GetNewID(typeof(Model.SafetyActivities_OtherActivities));
                newOtherActivities.OtherActivitiesId = this.OtherActivitiesId;
                BLL.OtherActivitiesService.AddOtherActivities(newOtherActivities);
                BLL.LogService.AddSys_Log(this.CurrUser, newOtherActivities.Title, newOtherActivities.OtherActivitiesId, BLL.Const.ProjectOtherActivitiesMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.OtherActivitiesId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherActivitiesAttachUrl&menuId={1}", OtherActivitiesId,BLL.Const.ProjectOtherActivitiesMenuId)));
        }
        #endregion
    }
}