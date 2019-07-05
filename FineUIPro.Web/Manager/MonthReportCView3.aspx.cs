using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Manager
{
    public partial class MonthReportCView3 : PageBase
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

        private static DateTime startTime;

        private static DateTime endTime;

        private static DateTime yearStartTime;

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    if (monthReport.MonthHSEDay != null)  //保存过数据
                    {
                        //本月项目现场HSE人工日统计
                        Model.MonthReportCHSEDay hseDay = new Model.MonthReportCHSEDay
                        {
                            MonthHSEDay = monthReport.MonthHSEDay ?? 0,
                            SumHSEDay = monthReport.SumHSEDay ?? 0,
                            MonthHSEWorkDay = monthReport.MonthHSEWorkDay ?? 0,
                            YearHSEWorkDay = monthReport.YearHSEWorkDay ?? 0,
                            SumHSEWorkDay = monthReport.SumHSEWorkDay ?? 0,
                            HseManhours = monthReport.HseManhours ?? 0,
                            SubcontractManHours = monthReport.SubcontractManHours ?? 0,
                            TotalHseManhours = monthReport.TotalHseManhours ?? 0
                        };
                        List<Model.MonthReportCHSEDay> list = new List<Model.MonthReportCHSEDay>();
                        list.Add(hseDay);
                        this.gvHSEDay.DataSource = list;
                        this.gvHSEDay.DataBind();
                    }
                }
            }
        }
        #endregion
    }
}