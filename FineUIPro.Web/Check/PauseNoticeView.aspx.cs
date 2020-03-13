using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class PauseNoticeView : PageBase
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
                LoadData();

                PauseNoticeId = Request.Params["PauseNoticeId"];
                if (!string.IsNullOrEmpty(PauseNoticeId))
                {
                    Model.Check_PauseNotice pauseNotice = BLL.Check_PauseNoticeService.GetPauseNoticeByPauseNoticeId(PauseNoticeId);

                    this.txtPauseNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.PauseNoticeId);
                    if (!string.IsNullOrEmpty(pauseNotice.UnitId))
                    {
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(pauseNotice.UnitId);
                        if (unit != null)
                        {
                            this.txtUnit.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(pauseNotice.SignMan))
                    {
                        var signName = BLL.UserService.GetUserNameByUserId(pauseNotice.SignMan);
                        if (signName !=null)
                        {
                            this.txtSignMan.Text = signName;
                        }
                    }
                    if (!string.IsNullOrEmpty(pauseNotice.ApproveMan))
                    {
                        var approve = BLL.UserService.GetUserNameByUserId(pauseNotice.ApproveMan);
                        if (approve!=null)
                        {
                            this.txtApproveMan.Text = approve;
                        }
                    }
                    this.txtProjectPlace.Text = pauseNotice.ProjectPlace;
                    this.txtSignPerson.Text = pauseNotice.SignPerson;
                    if (pauseNotice.CompileDate != null)
                    {
                        this.txtComplieDate.Text = string.Format("{0:yyyy-MM-dd}", pauseNotice.CompileDate);
                    }
                    this.txtWrongContent.Text = pauseNotice.WrongContent;
                    if (pauseNotice.PauseTime != null)
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
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPauseNoticeMenuId;
                this.ctlAuditFlow.DataId = this.PauseNoticeId;
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoticeUrl_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.PauseNoticeId))
            {
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
}