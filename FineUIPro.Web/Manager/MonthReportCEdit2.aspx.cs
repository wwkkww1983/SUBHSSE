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
    public partial class MonthReportCEdit2 : PageBase
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
        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.MonthReportId = Request.Params["monthReportId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
                if (monthReport != null)
                {
                    this.MonthReportId = monthReport.MonthReportId;
                    this.ProjectId = monthReport.ProjectId;
                    Model.SUBHSSEDB db = Funs.DB;
                    var q = from x in db.Manager_PersonSortC
                            where x.MonthReportId == MonthReportId
                            select x;
                    if (q.Count() > 0)  //保存过数据
                    {
                        //人力投入情况
                        List<Model.Manager_PersonSortC> sorts = (from x in db.Manager_PersonSortC
                                                                 join y in db.Project_ProjectUnit
                                                                 on x.UnitId equals y.UnitId
                                                                 where x.MonthReportId == MonthReportId && y.ProjectId == this.ProjectId
                                                                 orderby y.UnitType
                                                                 select x).Distinct().ToList();
                        List<Model.Manager_PersonSortC> personSorts = new List<Model.Manager_PersonSortC>();
                        var units = from x in db.Project_ProjectUnit
                                    where x.ProjectId == this.ProjectId && (x.UnitType == "1" || x.UnitType == "2")
                                    orderby x.UnitType
                                    select x;     //1为总包，2为施工分包
                        foreach (var unit in units)
                        {
                            Model.Manager_PersonSortC personSort = sorts.FirstOrDefault(x => x.UnitId == unit.UnitId);
                            if (personSort != null)
                            {
                                personSorts.Add(personSort);
                            }
                        }
                        this.gvPersonSort.DataSource = personSorts;
                        this.gvPersonSort.DataBind();
                        if (this.gvPersonSort.Rows.Count > 0)
                        {
                            JObject summary = new JObject();
                            summary.Add("UnitId", "合计：");
                            summary.Add("SumPersonNum", (from x in personSorts select x.SumPersonNum ?? 0).Sum());
                            summary.Add("HSEPersonNum", (from x in personSorts select x.HSEPersonNum ?? 0).Sum());
                            this.gvPersonSort.SummaryData = summary;
                        }
                        else
                        {
                            this.gvPersonSort.SummaryData = null;
                        }
                    }
                    else 
                    {
                        GetPersonSort();
                    }
                }
                else
                {
                    GetPersonSort();
                }
            }
        }
        #endregion

        #region 人力投入情况
        /// <summary>
        /// 显示月报告人员投入情况
        /// </summary>
        private void GetPersonSort()
        {
            List<Model.Manager_PersonSortC> personSorts = new List<Model.Manager_PersonSortC>();
            var units = from x in Funs.DB.Project_ProjectUnit
                        where x.ProjectId == this.ProjectId && (x.UnitType == "1" || x.UnitType == "2")
                        orderby x.UnitType
                        select x;     //1为总包，2为施工分包
            int totalSumPersonNum = 0;
            int totalHSEPersonNum = 0;
            if (units.Count() > 0)
            {
                foreach (Model.Project_ProjectUnit u in units)
                {
                    Model.Manager_PersonSortC personSort = new Model.Manager_PersonSortC
                    {
                        PersonSortId = SQLHelper.GetNewID(typeof(Model.Manager_PersonSortC)),
                        UnitId = u.UnitId,
                        SumPersonNum = BLL.PersonService.GetPersonCountByUnitId(u.UnitId, this.CurrUser.LoginProjectId),
                        HSEPersonNum = BLL.PersonService.GetHSEPersonCountByUnitId(u.UnitId, this.CurrUser.LoginProjectId),
                        ContractRange = u.ContractRange
                    };
                    personSorts.Add(personSort);
                    totalSumPersonNum += Convert.ToInt32(personSort.SumPersonNum);
                    totalHSEPersonNum += Convert.ToInt32(personSort.HSEPersonNum);
                }
            }
            this.gvPersonSort.DataSource = personSorts;
            this.gvPersonSort.DataBind();
            if (this.gvPersonSort.Rows.Count > 0)
            {
                JObject summary = new JObject();
                summary.Add("UnitId", "合计：");
                summary.Add("SumPersonNum", totalSumPersonNum);
                summary.Add("HSEPersonNum", totalHSEPersonNum);
                this.gvPersonSort.SummaryData = summary;
            }
            else
            {
                this.gvPersonSort.SummaryData = null;
            }
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 把单位Id转换为单位名称
        /// </summary>
        /// <param name="UnitId"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object UnitId)
        {
            if (UnitId != null)
            {
                Model.Base_Unit u = BLL.UnitService.GetUnitByUnitId(UnitId.ToString());
                if (u != null)
                {
                    return u.UnitName;
                }
            }
            return "";
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
                BLL.LogService.AddSys_Log(this.CurrUser, oldMonthReport.MonthReportCode, oldMonthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnModify);
                OperatePersonSort(MonthReportId);
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
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
                OperatePersonSort(newKeyID);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 操作月报告人力投入情况
        /// </summary>
        private void OperatePersonSort(string monthReportId)
        {
            BLL.PersonSortCService.DeletePersonSortsByMonthReportId(monthReportId);
            JArray mergedData = gvPersonSort.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_PersonSortC personSort = new Model.Manager_PersonSortC
                {
                    MonthReportId = monthReportId,
                    UnitId = this.gvPersonSort.Rows[i].DataKeys[1].ToString(),
                    SumPersonNum = Funs.GetNewIntOrZero(values.Value<string>("SumPersonNum").ToString()),
                    HSEPersonNum = Funs.GetNewIntOrZero(values.Value<string>("HSEPersonNum").ToString()),
                    ContractRange = values.Value<string>("ContractRange").ToString(),
                    Remark = values.Value<string>("Remark").ToString()
                };
                BLL.PersonSortCService.AddPersonSort(personSort);
            }
        }
        #endregion
    }
}