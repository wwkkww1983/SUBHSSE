using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Administrative
{
    public partial class HealthManageEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HealthManageId
        {
            get
            {
                return (string)ViewState["HealthManageId"];
            }
            set
            {
                ViewState["HealthManageId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;

                this.drpPersonId.DataValueField = "UserId";
                this.drpPersonId.DataTextField = "UserName";
                this.drpPersonId.DataSource = BLL.UserService.GetUserList();
                this.drpPersonId.DataBind();
                Funs.FineUIPleaseSelect(this.drpPersonId);
                this.HealthManageId = Request.Params["HealthManageId"];
                if (!string.IsNullOrEmpty(this.HealthManageId))
                {
                    Model.Administrative_HealthManage healthManage = BLL.HealthManageService.GetHealthManageById(this.HealthManageId);
                    if (healthManage != null)
                    {
                        this.ProjectId = healthManage.ProjectId;
                        if (!string.IsNullOrEmpty(healthManage.PersonId))
                        {
                            this.drpPersonId.SelectedValue = healthManage.PersonId;
                        }
                        if (healthManage.Age != null)
                        {
                            this.txtAge.Text = Convert.ToString(healthManage.Age);
                        }
                        this.txtBloodtype.Text = healthManage.Bloodtype;
                        this.txtHealthState.Text = healthManage.HealthState;
                        this.txtTaboo.Text = healthManage.Taboo;
                        if (healthManage.CheckTime != null)
                        {
                            this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", healthManage.CheckTime);
                        }
                        this.txtRemark.Text = healthManage.Remark;
                    }
                }
                else
                {
                    this.txtCheckTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.HealthManageMenuId;
                this.ctlAuditFlow.DataId = this.HealthManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (this.drpPersonId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                return;
            }
            SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpPersonId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                return;
            }
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
            Model.Administrative_HealthManage healthManage = new Model.Administrative_HealthManage
            {
                ProjectId = this.ProjectId
            };
            if (this.drpPersonId.SelectedValue != BLL.Const._Null)
            {
                healthManage.PersonId = this.drpPersonId.SelectedValue;
            }
            healthManage.Age = Funs.GetNewInt(this.txtAge.Text.Trim());
            healthManage.Bloodtype = this.txtBloodtype.Text.Trim();
            healthManage.HealthState = this.txtHealthState.Text.Trim();
            healthManage.Taboo = this.txtTaboo.Text.Trim();
            healthManage.CheckTime = Funs.GetNewDateTime(this.txtCheckTime.Text.Trim());
            healthManage.Remark = this.txtRemark.Text.Trim();
            healthManage.CompileMan = this.CurrUser.UserId;
            healthManage.CompileDate = DateTime.Now;
            healthManage.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                healthManage.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.HealthManageId))
            {
                healthManage.HealthManageId = this.HealthManageId;
                BLL.HealthManageService.UpdateHealthManage(healthManage);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改【" + this.drpPersonId.SelectedText.Trim() + "】的职业健康管理");
            }
            else
            {
                this.HealthManageId = SQLHelper.GetNewID(typeof(Model.Administrative_HealthManage));
                healthManage.HealthManageId = this.HealthManageId;
                BLL.HealthManageService.AddHealthManage(healthManage);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加【" + this.drpPersonId.SelectedText.Trim() + "】的职业健康管理");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.HealthManageMenuId, this.HealthManageId, (type == BLL.Const.BtnSubmit ? true : false), this.drpPersonId.SelectedText.Trim(), "../Administrative/HealthManageView.aspx?HealthManageId={0}");
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
            if (string.IsNullOrEmpty(this.HealthManageId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HealthManageAttachUrl&menuId={1}", this.HealthManageId, BLL.Const.HealthManageMenuId)));
        }
        #endregion
    }
}