using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyActivities
{
    public partial class SafetyMonthEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyMonthId
        {
            get
            {
                return (string)ViewState["SafetyMonthId"];
            }
            set
            {
                ViewState["SafetyMonthId"] = value;
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
  
                this.SafetyMonthId = Request.Params["SafetyMonthId"];
                if (!string.IsNullOrEmpty(this.SafetyMonthId))
                {
                    Model.SafetyActivities_SafetyMonth SafetyMonth = BLL.SafetyMonthService.GetSafetyMonthById(this.SafetyMonthId);
                    if (SafetyMonth != null)
                    {
                        this.ProjectId = SafetyMonth.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }

                        this.txtTitle.Text = SafetyMonth.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyMonth.CompileDate);
                        if (!string.IsNullOrEmpty(SafetyMonth.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = SafetyMonth.CompileMan;
                        }
                        this.txtRemark.Text = SafetyMonth.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(SafetyMonth.SeeFile);                                               
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectSafetyMonthMenuId, this.ProjectId);
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
            Model.SafetyActivities_SafetyMonth newSafetyMonth = new Model.SafetyActivities_SafetyMonth
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newSafetyMonth.CompileMan = this.drpCompileMan.SelectedValue;
            }
            newSafetyMonth.Remark = this.txtRemark.Text.Trim();
            newSafetyMonth.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.SafetyMonthId))
            {
                newSafetyMonth.SafetyMonthId = this.SafetyMonthId;
                BLL.SafetyMonthService.UpdateSafetyMonth(newSafetyMonth);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改全国安全月活动", newSafetyMonth.SafetyMonthId);
            }
            else
            {
                this.SafetyMonthId = SQLHelper.GetNewID(typeof(Model.SafetyActivities_SafetyMonth));
                newSafetyMonth.SafetyMonthId = this.SafetyMonthId;
                BLL.SafetyMonthService.AddSafetyMonth(newSafetyMonth);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加全国安全月活动", newSafetyMonth.SafetyMonthId);
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
            if (string.IsNullOrEmpty(this.SafetyMonthId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyMonthAttachUrl&menuId={1}", SafetyMonthId,BLL.Const.ProjectSafetyMonthMenuId)));
        }
        #endregion
    }
}