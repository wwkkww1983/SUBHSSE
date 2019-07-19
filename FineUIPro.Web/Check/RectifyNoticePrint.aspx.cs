using System;
using System.Web;
using BLL;
using System.Collections.Generic;

namespace FineUIPro.Web.Check
{
    public partial class RectifyNoticePrint : PageBase
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
                var thisUnit = BLL.CommonService.GetIsThisUnit();
                if (thisUnit != null)
                {
                    this.Label8.Text = thisUnit.UnitName + this.Label8.Text;
                    this.Label18.Text = thisUnit.UnitName + this.Label18.Text;
                    this.Label19.Text = thisUnit.UnitName + this.Label19.Text;
                }

                this.RectifyNoticeId = Request.Params["RectifyNoticeId"];
                var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(this.RectifyNoticeId);
                if (rectifyNotice != null)
                {
                    this.txtRectifyNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RectifyNoticeId);
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(rectifyNotice.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                        this.txtUnitNameProject.Text = unit.UnitName + "项目部：";
                    }
                    this.txtDutyPerson1.Text = rectifyNotice.DutyPerson;
                    if (rectifyNotice.CheckedDate != null)
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CheckedDate);
                        this.txtCheckedDate2.Text = rectifyNotice.CheckedDate.Value.Year + "年" + rectifyNotice.CheckedDate.Value.Month + "月" + rectifyNotice.CheckedDate.Value.Day + "日";
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.WrongContent))
                    {
                        this.txtWrongContent.Text = rectifyNotice.WrongContent;
                    }
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(rectifyNotice.SignPerson);
                    if (user != null)
                    {
                        this.txtSignPerson.Text = user.UserName;
                    }
                    if (rectifyNotice.SignDate != null)
                    {
                        this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.SignDate);
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.CompleteStatus))
                    {
                        this.txtCompleteStatus.Text = rectifyNotice.CompleteStatus;
                    }
                    this.txtDutyPerson2.Text = rectifyNotice.DutyPerson;
                    if (rectifyNotice.CompleteDate != null)
                    {
                        this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CompleteDate);
                    }
                    Model.AttachFile attachFile = BLL.AttachFileService.GetAttachFile(this.RectifyNoticeId, BLL.Const.ProjectRectifyNoticeMenuId);
                    if (attachFile != null)
                    {
                        List<string> urls = new List<string>();
                        string[] lists = attachFile.AttachUrl.Split(',');
                        foreach (var list in lists)
                        {
                            if (!string.IsNullOrEmpty(list))
                            {
                                urls.Add(list);
                            }
                        }
                        string str = string.Empty;
                        str = "<table id='Table3' runat='server' width='100%' cellpadding='0' cellspacing='0' border='0' bordercolor='#000000'>";
                        if (urls.Count > 1)   //两个附件
                        {
                            string photo1 = "<img alt='' runat='server' id='img111' width='280' height='280' src='" + "../" + urls[0] + "' />";
                            string photo2 = "<img alt='' runat='server' id='img111' width='280' height='280' src='" + "../" + urls[1] + "' />";
                            str += "<tr><td align='center' colspan='2'>" + photo1 + "</td>";
                            str += "<td align='center' colspan='2'>" + photo2 + "</td></tr>";
                        }
                        else if (urls.Count == 1)
                        {
                            string photo1 = "<img alt='' runat='server' id='img111' width='250' height='250' src='" + "../" + urls[0] + "' />";
                            str += "<td align='center' colspan='4'>" + photo1 + "</td></tr>";
                        }
                        str += "</table>";
                        this.div3.InnerHtml = str;
                    }
                }
            }
        }
        #endregion
    }
}