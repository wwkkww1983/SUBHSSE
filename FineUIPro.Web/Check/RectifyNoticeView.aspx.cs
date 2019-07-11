using System;

namespace FineUIPro.Web.Check
{
    public partial class RectifyNoticeView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string RectifyNoticeId
        {
            get
            {
                return (string)ViewState["RectifyNoticeId"];
            }
            set
            {
                ViewState["RectifyNoticeId"] = value;
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
                this.RectifyNoticeId = Request.Params["RectifyNoticeId"];
                var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(this.RectifyNoticeId);
                if (rectifyNotice != null)
                {
                    //隐患
                    this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RectifyNoticeId);
                    if (!string.IsNullOrEmpty(rectifyNotice.UnitId))
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(rectifyNotice.UnitId);
                        if (unit !=null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.WorkAreaId))
                    {
                        var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(rectifyNotice.WorkAreaId);
                        if (workArea!=null)
                        {
                            this.txtWorkAreaName.Text = workArea.WorkAreaName;
                        }
                    }
                    if (rectifyNotice.CheckedDate != null)
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CheckedDate);
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.WrongContent))
                    {
                        this.txtWrongContent.Text = rectifyNotice.WrongContent;
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.SignPerson))
                    {
                        var user = BLL.UserService.GetUserByUserId(rectifyNotice.SignPerson);
                        if (user != null)
                        {
                            this.txtSignPerson.Text = user.UserName;
                        }
                    }
                    if (rectifyNotice.SignDate != null)
                    {
                        this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.SignDate);
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.CompleteStatus))
                    {
                        this.txtCompleteStatus.Text = rectifyNotice.CompleteStatus;
                    }
                    this.txtDutyPerson.Text = rectifyNotice.DutyPerson;
                    if (rectifyNotice.CompleteDate != null)
                    {
                        this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CompleteDate);
                    }
                    if (rectifyNotice.IsRectify==true)
                    {
                        this.txtIsRectify.Text = "是";
                    }
                    else
                    {
                        this.txtIsRectify.Text = "否";
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.CheckPerson))
                    {
                        string userName = BLL.UserService.GetUserNameByUserId(rectifyNotice.CheckPerson);
                        if (!string.IsNullOrEmpty(userName))
                        {
                            this.txtCheckPerson.Text = userName;
                        }
                    }
                }
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.RectifyNoticeId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotice&menuId=0038D764-D628-46F0-94FF-D0A22C3C45A3", this.RectifyNoticeId)));
            }
        }
        #endregion
    }
}