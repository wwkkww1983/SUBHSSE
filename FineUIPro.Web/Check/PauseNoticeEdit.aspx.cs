using BLL;
using System;
using System.Linq;
using System.Web.UI;

namespace FineUIPro.Web.Check
{
    public partial class PauseNoticeEdit : PageBase
    {
        #region  定义项
        /// <summary>
        /// 工程暂停令主键
        /// </summary>
        public string PauseNoticeId
        {
            get
            {
                return (string)ViewState["PauseNoticeId"];
            }
            set
            {
                ViewState["PauseNoticeId"] = value;
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
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
            }
        }
        #endregion

        #region 加载页面
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
                this.PauseNoticeId = Request.Params["PauseNoticeId"];
                this.InitDropDownList();
                if (!string.IsNullOrEmpty(PauseNoticeId))
                {
                    Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                    if (pauseNotice != null)
                    {
                        this.ProjectId = pauseNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PauseNoticeId);
                        if (!string.IsNullOrEmpty(pauseNotice.UnitId))
                        {
                            this.drpUnit.SelectedValue = pauseNotice.UnitId;
                        }
                        this.txtProjectPlace.Text = pauseNotice.ProjectPlace;
                        this.txtSignPerson.Text = pauseNotice.SignPerson;
                        this.txtComplieDate.Text = string.Format("{0:yyyy-MM-dd}", pauseNotice.CompileDate);
                        this.txtWrongContent.Text = pauseNotice.WrongContent;
                        if (pauseNotice.PauseTime.HasValue)
                        {
                            string strPauseTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", pauseNotice.PauseTime);
                            int index1 = strPauseTime.IndexOf("-");
                            int index2 = strPauseTime.Substring(index1 + 1).IndexOf("-");
                            int index3 = strPauseTime.Substring(index1 + 1 + index2 + 1).IndexOf(" ");
                            int index4 = strPauseTime.Substring(index1 + 1 + index2 + 1 + index3 + 1).IndexOf(":");
                            this.txtYear.Text = strPauseTime.Substring(0, index1);
                            this.txtMonth.Text = strPauseTime.Substring(index1 + 1, index2);
                            this.txtDay.Text = strPauseTime.Substring(index1 + 1 + index2 + 1, index3);
                            this.txtHour.Text = strPauseTime.Substring(index1 + 1 + index2 + 1 + index3 + 1, index4);
                        }
                        this.txtPauseContent.Text = pauseNotice.PauseContent;
                        this.txtOneContent.Text = pauseNotice.OneContent;
                        this.txtSecondContent.Text = pauseNotice.SecondContent;
                        this.txtThirdContent.Text = pauseNotice.ThirdContent;
                        this.txtProjectHeadConfirm.Text = pauseNotice.ProjectHeadConfirm;
                        if (pauseNotice.ConfirmDate != null)
                        {
                            this.txtConfirmDate.Text = string.Format("{0:yyyy-MM-dd}", pauseNotice.ConfirmDate);
                        }
                        this.AttachUrl = pauseNotice.AttachUrl;
                        this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
                        if (Request.Params["type"] == "confirm")   //签字确认
                        {
                            this.txtProjectHeadConfirm.Enabled = true;
                            this.txtConfirmDate.Enabled = true;
                            this.txtProjectHeadConfirm.Text = this.CurrUser.UserName;
                            this.txtConfirmDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        }
                        if (!string.IsNullOrEmpty(pauseNotice.SignMan))
                        {
                            this.drpSignMan.SelectedValue = pauseNotice.SignMan;
                        }
                        if (!string.IsNullOrEmpty(pauseNotice.ApproveMan))
                        {
                            this.drpApproveMan.SelectedValue = pauseNotice.ApproveMan;
                        }
                    }
                }
                else
                {
                    this.txtSignPerson.Text = this.CurrUser.UserName;
                    this.txtComplieDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPauseNoticeMenuId;
                this.ctlAuditFlow.DataId = this.PauseNoticeId;
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
            this.drpUnit.DataTextField = "UnitName";
            this.drpUnit.DataValueField = "UnitId";
            this.drpUnit.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnit.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnit);

            this.drpSignMan.DataValueField = "UserId";
            this.drpSignMan.DataTextField = "UserName";
            this.drpSignMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
            this.drpSignMan.DataBind();
            Funs.FineUIPleaseSelect(this.drpSignMan);

            this.drpApproveMan.DataValueField = "UserId";
            this.drpApproveMan.DataTextField = "UserName";
            this.drpApproveMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
            this.drpApproveMan.DataBind();
            Funs.FineUIPleaseSelect(this.drpApproveMan);
        }
        
        #region  单位变化事件
        /// <summary>
        /// 单位变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPauseNoticeMenuId, this.ProjectId, this.drpUnit.SelectedValue);
            }
            else
            {
                this.txtPauseNoticeCode.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile1_Click(object sender, EventArgs e)
        {
            if (btnFile1.HasFile)
            {
                this.AttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile1, this.AttachUrl, UploadFileService.PauseNoticeFilePath);
                this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
            }
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检单位！", MessageBoxIcon.Warning);
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
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择受检单位！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            if (Request.Params["type"] == "confirm")   //签字确认
            {
                Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);
                pauseNotice.ProjectHeadConfirm = this.txtProjectHeadConfirm.Text.Trim();
                pauseNotice.ConfirmDate = Funs.GetNewDateTime(this.txtConfirmDate.Text.Trim());
                pauseNotice.IsConfirm = true;
                ////单据状态
                pauseNotice.States = BLL.Const.State_0;
                if (type == BLL.Const.BtnSubmit)
                {
                    pauseNotice.States = this.ctlAuditFlow.NextStep;
                }
                BLL.Check_PauseNoticeService.UpdatePauseNotice(pauseNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, pauseNotice.PauseNoticeCode, pauseNotice.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnModify);
            }
            else
            {
                Model.Check_PauseNotice pauseNotice = new Model.Check_PauseNotice
                {
                    PauseNoticeCode = this.txtPauseNoticeCode.Text.Trim(),
                    ProjectId = this.ProjectId,
                    UnitId = this.drpUnit.SelectedValue,
                    SignPerson = this.txtSignPerson.Text.Trim(),
                    CompileDate = Funs.GetNewDateTime(this.txtComplieDate.Text.Trim()),
                    ProjectPlace = this.txtProjectPlace.Text.Trim(),
                    WrongContent = this.txtWrongContent.Text.Trim()
                };
                if (!string.IsNullOrEmpty(this.txtYear.Text.Trim()))
                {
                    string pauseTime = this.txtYear.Text.Trim() + "-" + this.txtMonth.Text.Trim() + "-" + this.txtDay.Text.Trim() + " " + this.txtHour.Text.Trim() + ":00:00";
                    try
                    {
                        DateTime aa = Convert.ToDateTime(pauseTime);
                        pauseNotice.PauseTime = Convert.ToDateTime(pauseTime);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请填写正确的日期格式！')", true);
                        return;
                    }
                }
                else
                {
                    pauseNotice.PauseTime = null;
                }
                pauseNotice.PauseContent = this.txtPauseContent.Text.Trim();
                pauseNotice.OneContent = this.txtOneContent.Text.Trim();
                pauseNotice.SecondContent = this.txtSecondContent.Text.Trim();
                pauseNotice.ThirdContent = this.txtThirdContent.Text.Trim();
                pauseNotice.ProjectHeadConfirm = this.txtProjectHeadConfirm.Text.Trim();
                pauseNotice.ConfirmDate = Funs.GetNewDateTime(this.txtConfirmDate.Text.Trim());
                pauseNotice.IsConfirm = false;
                pauseNotice.AttachUrl = this.AttachUrl;
                ////单据状态
                pauseNotice.States = BLL.Const.State_0;
                if (this.drpSignMan.SelectedValue!=BLL.Const._Null)
                {
                    pauseNotice.SignMan = this.drpSignMan.SelectedValue;
                }
                if (this.drpApproveMan.SelectedValue!=BLL.Const._Null)
                {
                    pauseNotice.ApproveMan = this.drpApproveMan.SelectedValue;
                }
                if (type == BLL.Const.BtnSubmit)
                {
                    pauseNotice.States = this.ctlAuditFlow.NextStep;
                }
                if (!string.IsNullOrEmpty(this.PauseNoticeId))
                {
                    pauseNotice.PauseNoticeId = this.PauseNoticeId;
                    BLL.Check_PauseNoticeService.UpdatePauseNotice(pauseNotice);
                    BLL.LogService.AddSys_Log(this.CurrUser, pauseNotice.PauseNoticeCode, pauseNotice.PauseNoticeId,BLL.Const.ProjectPauseNoticeMenuId,BLL.Const.BtnModify);
                }
                else
                {
                    pauseNotice.PauseNoticeId = SQLHelper.GetNewID(typeof(Model.Check_PauseNotice));
                    pauseNotice.CompileMan = this.CurrUser.UserId;
                    this.PauseNoticeId = pauseNotice.PauseNoticeId;
                    BLL.Check_PauseNoticeService.AddPauseNotice(pauseNotice);
                    BLL.LogService.AddSys_Log(this.CurrUser, pauseNotice.PauseNoticeCode, pauseNotice.PauseNoticeId, BLL.Const.ProjectPauseNoticeMenuId, BLL.Const.BtnAdd);
                }
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckWorkMenuId, this.PauseNoticeId, (type == BLL.Const.BtnSubmit ? true : false), this.txtPauseContent.Text.Trim(), "../Check/PauseNoticeView.aspx?PauseNoticeId={0}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoticeUrl_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.PauseNoticeId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPauseNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.PauseNoticeId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PauseNotice&menuId=" + BLL.Const.ProjectPauseNoticeMenuId, this.PauseNoticeId)));
            }
        }
    }
}