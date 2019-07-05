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
    public partial class MonthReportCView7 : PageBase
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

        private static DateTime projectStartTime;

        #region 定义集合
        /// <summary>
        /// 7.1 管理绩效数据统计集合(表一)
        /// </summary>
        private static List<Model.Manager_Month_AccidentDesciptionC> accidentDesciptions = new List<Model.Manager_Month_AccidentDesciptionC>();

        /// <summary>
        /// 7.2 管理绩效数据统计集合（表二）
        /// </summary>
        private static List<Model.Manager_Month_AccidentDesciptionItemC> AccidentDesciptionItems = new List<Model.Manager_Month_AccidentDesciptionItemC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                accidentDesciptions.Clear();
                AccidentDesciptionItems.Clear();
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(ProjectId);
                if (project.StartDate != null)
                {
                    projectStartTime = Convert.ToDateTime(project.StartDate);
                }
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    Model.SUBHSSEDB db = Funs.DB;
                    //7.1 管理绩效数据统计(表一)
                    accidentDesciptions = (from x in db.Manager_Month_AccidentDesciptionC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (accidentDesciptions.Count > 0)
                    {
                        this.gvAccidentDesciption.DataSource = accidentDesciptions;
                        this.gvAccidentDesciption.DataBind();
                    }
                    //7.2 管理绩效数据统计（表二）
                    AccidentDesciptionItems = (from x in db.Manager_Month_AccidentDesciptionItemC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (AccidentDesciptionItems.Count > 0)
                    {
                        this.gvAccidentDesciptionItem.DataSource = AccidentDesciptionItems;
                        this.gvAccidentDesciptionItem.DataBind();
                    }
                    this.txtAccidentDes.Text = monthReport.AccidentDes;
                }
            }
        }
        #endregion
    }
}