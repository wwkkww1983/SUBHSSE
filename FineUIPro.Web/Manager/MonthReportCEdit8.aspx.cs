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
    public partial class MonthReportCEdit8 : PageBase
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
        /// 8 其他工作情况集合
        /// </summary>
        private static List<Model.Manager_Month_OtherWorkC> otherWorks = new List<Model.Manager_Month_OtherWorkC>();

        #endregion

        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                otherWorks.Clear();
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                DateTime months = Convert.ToDateTime(Request.Params["months"]);
                startTime = Convert.ToDateTime(Request.Params["startTime"]);
                endTime = Convert.ToDateTime(Request.Params["endTime"]);
                yearStartTime = Convert.ToDateTime(Request.Params["yearStartTime"]);
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(months, this.CurrUser.LoginProjectId);
                Model.Manager_MonthReportC mr = BLL.MonthReportCService.GetLastMonthReportByDate(endTime, this.ProjectId);
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(ProjectId);
                if (project.StartDate != null)
                {
                    projectStartTime = Convert.ToDateTime(project.StartDate);
                }
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    months = Convert.ToDateTime(monthReport.Months);
                    Model.SUBHSSEDB db = Funs.DB;
                    //8 其他工作情况
                    otherWorks = (from x in db.Manager_Month_OtherWorkC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (otherWorks.Count > 0)
                    {
                        this.gvOtherWork.DataSource = otherWorks;
                        this.gvOtherWork.DataBind();
                    }
                }
                else
                {

                }
            }
        }
        #endregion

        #region 其他工作情况
        /// <summary>
        /// 增加其他工作情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewOtherWork_Click(object sender, EventArgs e)
        {
            jerqueSaveOtherWorkList();
            Model.Manager_Month_OtherWorkC otherWorkSort = new Model.Manager_Month_OtherWorkC
            {
                OtherWorkId = SQLHelper.GetNewID(typeof(Model.Manager_Month_OtherWorkC))
            };
            otherWorks.Add(otherWorkSort);
            this.gvOtherWork.DataSource = otherWorks;
            this.gvOtherWork.DataBind();
        }

        /// <summary>
        /// 检查并保存其他工作情况集合
        /// </summary>
        private void jerqueSaveOtherWorkList()
        {
            otherWorks.Clear();
            int rowsCount = this.gvOtherWork.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.Manager_Month_OtherWorkC otherWorkSort = new Model.Manager_Month_OtherWorkC
                {
                    OtherWorkId = this.gvOtherWork.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    WorkContentDes = this.gvOtherWork.Rows[i].Values[2].ToString()
                };
                otherWorks.Add(otherWorkSort);
            }
        }

        protected void gvOtherWork_RowCommand(object sender, GridCommandEventArgs e)
        {
            jerqueSaveOtherWorkList();
            string rowID = this.gvOtherWork.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Delete")
            {
                foreach (var item in otherWorks)
                {
                    if (item.OtherWorkId == rowID)
                    {
                        otherWorks.Remove(item);
                        break;
                    }
                }
                gvOtherWork.DataSource = otherWorks;
                gvOtherWork.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
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
            Model.Manager_MonthReportC oldMonthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
            if (oldMonthReport != null)
            {
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperateOtherWorkSort(MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改HSE月报告", MonthReportId);
            }
            else
            {
                Model.Manager_MonthReportC monthReport = new Model.Manager_MonthReportC();
                string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_MonthReportC));
                monthReport.MonthReportId = newKeyID;
                monthReport.ProjectId = this.CurrUser.LoginProjectId;
                this.MonthReportId = newKeyID;
                monthReport.MonthReportCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerMonthCMenuId, this.ProjectId, this.CurrUser.UnitId);
                monthReport.Months = Funs.GetNewDateTime(Request.Params["months"]);
                monthReport.ReportMan = this.CurrUser.UserId;
                monthReport.MonthReportDate = DateTime.Now;
                BLL.MonthReportCService.AddMonthReport(monthReport);
                OperateOtherWorkSort(MonthReportId);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加HSE月报告", monthReport.MonthReportId);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 8 其他工作情况
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateOtherWorkSort(string monthReportId)
        {
            BLL.OtherWorkCService.DeleteOtherWorkByMonthReportId(monthReportId);
            jerqueSaveOtherWorkList();
            foreach (Model.Manager_Month_OtherWorkC otherWork in otherWorks)
            {
                otherWork.MonthReportId = monthReportId;
                BLL.OtherWorkCService.AddOtherWork(otherWork);
            }
        }
        #endregion
    }
}