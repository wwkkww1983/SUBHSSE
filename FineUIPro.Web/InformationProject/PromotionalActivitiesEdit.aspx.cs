using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class PromotionalActivitiesEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PromotionalActivitiesId
        {
            get
            {
                return (string)ViewState["PromotionalActivitiesId"];
            }
            set
            {
                ViewState["PromotionalActivitiesId"] = value;
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
                this.PromotionalActivitiesId = Request.Params["PromotionalActivitiesId"];
                if (!string.IsNullOrEmpty(this.PromotionalActivitiesId))
                {
                    Model.InformationProject_PromotionalActivities PromotionalActivities = BLL.PromotionalActivitiesService.GetPromotionalActivitiesById(this.PromotionalActivitiesId);
                    if (PromotionalActivities != null)
                    {
                        this.ProjectId = PromotionalActivities.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PromotionalActivitiesId);
                        this.txtTitle.Text = PromotionalActivities.Title;                       
                        if (!string.IsNullOrEmpty(PromotionalActivities.UnitIds))
                        {
                            this.drpUnits.SelectedValueArray = PromotionalActivities.UnitIds.Split(',');
                        }
                        if (!string.IsNullOrEmpty(PromotionalActivities.UserIds))
                        {
                            this.drpUsers.SelectedValueArray = PromotionalActivities.UserIds.Split(',');
                        }
                        this.txtActivitiesDate.Text = string.Format("{0:yyyy-MM-dd}", PromotionalActivities.ActivitiesDate);
                        if (!string.IsNullOrEmpty(PromotionalActivities.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = PromotionalActivities.CompileMan;
                        }
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(PromotionalActivities.MainContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtActivitiesDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectPromotionalActivitiesMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                    {
                        this.drpUnits.SelectedValue = this.CurrUser.UnitId;
                    }
                    this.drpUsers.SelectedValue = this.CurrUser.UserId;
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPromotionalActivitiesMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtTitle.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPromotionalActivitiesMenuId;
                this.ctlAuditFlow.DataId = this.PromotionalActivitiesId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnits, this.ProjectId, false);
            BLL.UserService.InitUserDropDownList(this.drpUsers, this.ProjectId, false);
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
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InformationProject_PromotionalActivities PromotionalActivities = new Model.InformationProject_PromotionalActivities
            {
                ProjectId = this.ProjectId,
                Code = this.txtCode.Text.Trim(),
                Title = this.txtTitle.Text.Trim()
            };
            //参与单位
            string unitIds = string.Empty;
            string unitNames = string.Empty;
            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(item);
                if (unit != null)
                {
                    unitIds += unit.UnitId + ",";
                    unitNames += unit.UnitName + ",";
                }
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                PromotionalActivities.UnitIds =  unitIds.Substring(0, unitIds.LastIndexOf(","));
                PromotionalActivities.UnitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
            }

            //参与用户
            string userIds = string.Empty;
            string userNames = string.Empty;
            foreach (var item in this.drpUsers.SelectedValueArray)
            {
                var user = BLL.UserService.GetUserByUserId(item);
                if (user != null)
                {
                    userIds += user.UserId + ",";
                    userNames += user.UserName + ",";
                }
            }
            if (!string.IsNullOrEmpty(userIds))
            {
                PromotionalActivities.UserIds = userIds.Substring(0, userIds.LastIndexOf(","));
                PromotionalActivities.UserNames = userNames.Substring(0, userNames.LastIndexOf(","));
            }

            PromotionalActivities.ActivitiesDate = Funs.GetNewDateTime(this.txtActivitiesDate.Text.Trim());
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                PromotionalActivities.CompileMan = this.drpCompileMan.SelectedValue;
            }
            PromotionalActivities.MainContent = HttpUtility.HtmlEncode(this.txtMainContent.Text);
            PromotionalActivities.CompileDate = DateTime.Now;
            ////单据状态
            PromotionalActivities.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                PromotionalActivities.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.PromotionalActivitiesId))
            {
                PromotionalActivities.PromotionalActivitiesId = this.PromotionalActivitiesId;
                BLL.PromotionalActivitiesService.UpdatePromotionalActivities(PromotionalActivities);
                BLL.LogService.AddSys_Log(this.CurrUser, PromotionalActivities.Code, PromotionalActivities.PromotionalActivitiesId, BLL.Const.ProjectPromotionalActivitiesMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.PromotionalActivitiesId = SQLHelper.GetNewID(typeof(Model.InformationProject_PromotionalActivities));
                PromotionalActivities.PromotionalActivitiesId = this.PromotionalActivitiesId;
                BLL.PromotionalActivitiesService.AddPromotionalActivities(PromotionalActivities);
                BLL.LogService.AddSys_Log(this.CurrUser, PromotionalActivities.Code, PromotionalActivities.PromotionalActivitiesId, BLL.Const.ProjectPromotionalActivitiesMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectPromotionalActivitiesMenuId, this.PromotionalActivitiesId, (type == BLL.Const.BtnSubmit ? true : false), PromotionalActivities.Title, "../InformationProject/PromotionalActivitiesView.aspx?PromotionalActivitiesId={0}");
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
            if (string.IsNullOrEmpty(this.PromotionalActivitiesId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PromotionalActivitiesAttachUrl&menuId={1}", PromotionalActivitiesId,BLL.Const.ProjectPromotionalActivitiesMenuId)));
        }
        #endregion
    }
}