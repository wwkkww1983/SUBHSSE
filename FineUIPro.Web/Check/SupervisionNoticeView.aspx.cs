using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Check
{
    public partial class SupervisionNoticeView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SupervisionNoticeId
        {
            get
            {
                return (string)ViewState["SupervisionNoticeId"];
            }
            set
            {
                ViewState["SupervisionNoticeId"] = value;
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
                this.SupervisionNoticeId = Request.Params["SupervisionNoticeId"];
                var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(this.SupervisionNoticeId);
                if (SupervisionNotice != null)
                {
                    //隐患
                    this.txtSupervisionNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SupervisionNoticeId);
                    if (!string.IsNullOrEmpty(SupervisionNotice.UnitId))
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(SupervisionNotice.UnitId);
                        if (unit !=null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.WorkAreaId))
                    {
                        var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(SupervisionNotice.WorkAreaId);
                        if (workArea!=null)
                        {
                            this.txtWorkAreaName.Text = workArea.WorkAreaName;
                        }
                    }
                    if (SupervisionNotice.CheckedDate != null)
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.CheckedDate);
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.WrongContent))
                    {
                        this.txtWrongContent.Text = SupervisionNotice.WrongContent;
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.SignPerson))
                    {
                        var user = BLL.UserService.GetUserByUserId(SupervisionNotice.SignPerson);
                        if (user != null)
                        {
                            this.txtSignPerson.Text = user.UserName;
                        }
                    }
                    if (SupervisionNotice.SignDate != null)
                    {
                        this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.SignDate);
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.CompleteStatus))
                    {
                        this.txtCompleteStatus.Text = SupervisionNotice.CompleteStatus;
                    }
                    this.txtDutyPerson.Text = SupervisionNotice.DutyPerson;
                    if (SupervisionNotice.CompleteDate != null)
                    {
                        this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.CompleteDate);
                    }
                    if (SupervisionNotice.IsRectify==true)
                    {
                        this.txtIsRectify.Text = "是";
                    }
                    else
                    {
                        this.txtIsRectify.Text = "否";
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.CheckPerson))
                    {
                        string userName = BLL.UserService.GetUserNameByUserId(SupervisionNotice.CheckPerson);
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
            if (!string.IsNullOrEmpty(this.SupervisionNoticeId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SupervisionNotice&menuId=0038D764-D628-46F0-94FF-D0A22C3C45A3", this.SupervisionNoticeId)));
            }
        }
        #endregion
    }
}