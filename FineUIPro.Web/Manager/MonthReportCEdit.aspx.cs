using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 月报告查主键
        /// </summary>
        public string MonthReportId
        {
            get
            {
                return (string)ViewState["MonthReportId"];
            }
            set
            {
                ViewState["MonthReportId"] = value;
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

        private static DateTime months;

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

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
                this.txtReportMan.Text = this.CurrUser.UserName;
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = Request.Params["monthReportId"];
                if (!string.IsNullOrEmpty(MonthReportId))
                {
                    Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonthReportId(MonthReportId);
                    this.ProjectId = monthReport.ProjectId;
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MonthReportId);
                    if (monthReport.Months != null)
                    {
                        months = Convert.ToDateTime(monthReport.Months);
                        this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", monthReport.Months);
                    }
                    this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    Model.Sys_User reportMan = BLL.UserService.GetUserByUserId(monthReport.ReportMan);
                    if (reportMan != null)
                    {
                        this.txtReportMan.Text = reportMan.UserName;
                    }
                    startTime = Convert.ToDateTime(this.txtReportMonths.Text + "-1").AddMonths(-1).AddDays(25);
                    endTime = Convert.ToDateTime(this.txtReportMonths.Text + "-1").AddDays(24);
                }
                else
                {
                    this.txtMonthReportCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthCMenuId, this.ProjectId, this.CurrUser.UnitId);
                    months = Convert.ToDateTime(Request.Params["months"]);
                    this.txtReportMonths.Text = string.Format("{0:yyyy-MM}", months);
                    this.txtMonthReportDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    startTime = months.AddMonths(-1).AddDays(25);
                    endTime = months.AddDays(24);
                }
                if (months.Month == 1)   //1月份月报的年度值就是当月值
                {
                    yearStartTime = startTime;
                }
                else
                {
                    yearStartTime = Convert.ToDateTime((months.Year - 1) + "-12-26");
                }
                this.BindLeftTree();
            }
        }
        #endregion

        /// <summary>
        /// 加载树
        /// </summary>
        private void BindLeftTree()
        {
            this.leftTree.Nodes.Clear();
            var items = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportCItem);
            foreach (var item in items)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = item.ConstText,
                    ToolTip = item.ConstText,
                    NodeID = item.ConstValue,
                    NavigateUrl = item.ConstValue + "?monthReportId=" + MonthReportId + "&months=" + months + "&startTime=" + startTime + "&endTime=" + endTime + "&yearStartTime=" + yearStartTime,
                    Target = "mainframe"
                };
                this.leftTree.Nodes.Add(newNode);
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MonthReportId))
            {
                Model.Manager_MonthReportC monthReport = new Model.Manager_MonthReportC
                {
                    MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportC)),
                    MonthReportCode = this.txtMonthReportCode.Text,
                    ProjectId = this.ProjectId,
                    MonthReportDate = Funs.GetNewDateTime(this.txtMonthReportDate.Text),
                    Months = Convert.ToDateTime(this.txtReportMonths.Text + "-1"),
                    ReportMan = this.CurrUser.UserId
                };
                monthReport.MonthReportId = SQLHelper.GetNewID(typeof(Model.Manager_MonthReport));
                this.MonthReportId = monthReport.MonthReportId;
                BLL.MonthReportCService.AddMonthReport(monthReport);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "增加管理月报", monthReport.MonthReportId);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerMonthReport&menuId={1}", this.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId)));
        }
        #endregion
    }
}