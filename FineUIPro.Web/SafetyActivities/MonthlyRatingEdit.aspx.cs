using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class MonthlyRatingEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MonthlyRatingId
        {
            get
            {
                return (string)ViewState["MonthlyRatingId"];
            }
            set
            {
                ViewState["MonthlyRatingId"] = value;
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
  
                this.MonthlyRatingId = Request.Params["MonthlyRatingId"];
                if (!string.IsNullOrEmpty(this.MonthlyRatingId))
                {
                    Model.SafetyActivities_MonthlyRating MonthlyRating = BLL.MonthlyRatingService.GetMonthlyRatingById(this.MonthlyRatingId);
                    if (MonthlyRating != null)
                    {
                        this.ProjectId = MonthlyRating.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = MonthlyRating.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", MonthlyRating.CompileDate);
                        if (!string.IsNullOrEmpty(MonthlyRating.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = MonthlyRating.CompileMan;
                        }
                        this.txtRemark.Text = MonthlyRating.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(MonthlyRating.SeeFile);                                               
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectMonthlyRatingMenuId, this.ProjectId);
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
            Model.SafetyActivities_MonthlyRating newMonthlyRating = new Model.SafetyActivities_MonthlyRating
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newMonthlyRating.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newMonthlyRating.Remark = this.txtRemark.Text.Trim();
            newMonthlyRating.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.MonthlyRatingId))
            {
                newMonthlyRating.MonthlyRatingId = this.MonthlyRatingId;
                BLL.MonthlyRatingService.UpdateMonthlyRating(newMonthlyRating);
                BLL.LogService.AddSys_Log(this.CurrUser, newMonthlyRating.Title, newMonthlyRating.MonthlyRatingId,BLL.Const.ProjectMonthlyRatingMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.MonthlyRatingId = SQLHelper.GetNewID(typeof(Model.SafetyActivities_MonthlyRating));
                newMonthlyRating.MonthlyRatingId = this.MonthlyRatingId;
                BLL.MonthlyRatingService.AddMonthlyRating(newMonthlyRating);
                BLL.LogService.AddSys_Log(this.CurrUser, newMonthlyRating.Title, newMonthlyRating.MonthlyRatingId, BLL.Const.ProjectMonthlyRatingMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.MonthlyRatingId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthlyRatingAttachUrl&menuId={1}", MonthlyRatingId,BLL.Const.ProjectMonthlyRatingMenuId)));
        }
        #endregion
    }
}