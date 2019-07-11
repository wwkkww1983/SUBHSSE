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
    public partial class MonthReportCEdit3 : PageBase
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
                    months = Convert.ToDateTime(monthReport.Months);
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
                    else
                    {
                        GetData(months);
                    }
                }
                else
                {
                    GetData(months);
                }
            }
        }
        #endregion

        private void GetData(DateTime months)
        {
            //本月项目现场HSE人工日统计
            Model.MonthReportCHSEDay hseDay = new Model.MonthReportCHSEDay
            {
                MonthHSEDay = (endTime - startTime).Days + 1
            };
            Model.Manager_MonthReportC mr = BLL.MonthReportCService.GetLastMonthReportByDate(endTime, this.ProjectId);
            if (mr != null)
            {
                if (mr.SumHSEDay != 0)
                {
                    hseDay.SumHSEDay = (mr.SumHSEDay ?? 0) + hseDay.MonthHSEDay;
                }
                else
                {
                    hseDay.SumHSEDay = hseDay.MonthHSEDay;
                }
            }
            else
            {
                hseDay.SumHSEDay = hseDay.MonthHSEDay;
            }

            int? monthHSEWorkDay = 0;
            int? yearHSEWorkDay = 0;
            int? sumHSEWorkDay = 0;
            decimal? hSEManhours = 0;
            decimal? sumHseManhours = 0;
            decimal? SubcontractManHours = 0;

            Model.SitePerson_MonthReport monthReport = BLL.Funs.DB.SitePerson_MonthReport.FirstOrDefault(x => x.CompileDate == months && x.ProjectId == this.CurrUser.LoginProjectId);   //当月人工时月报
            if (monthReport != null)
            {
                monthHSEWorkDay = Convert.ToInt32((from x in Funs.DB.SitePerson_MonthReportDetail
                                                   where x.MonthReportId == monthReport.MonthReportId
                                                   select x.RealPersonNum ?? 0).Sum());
                hSEManhours = (from x in Funs.DB.SitePerson_MonthReportDetail
                               join z in Funs.DB.Project_ProjectUnit
                               on x.UnitId equals z.UnitId
                               where z.UnitType == "1" && z.ProjectId == this.CurrUser.LoginProjectId && x.MonthReportId == monthReport.MonthReportId   //总包
                               select x.PersonWorkTime ?? 0).Sum();
                var q = (from x in Funs.DB.SitePerson_MonthReportDetail
                         join z in Funs.DB.Project_ProjectUnit
                         on x.UnitId equals z.UnitId
                         where z.UnitType == "2" && z.ProjectId == this.CurrUser.LoginProjectId && x.MonthReportId == monthReport.MonthReportId   //分包
                         select x);
                foreach (var item in q)
                {
                    SubcontractManHours += item.PersonWorkTime ?? 0;
                }
                //SubcontractManHours = (from x in Funs.DB.SitePerson_MonthReportDetail
                //                       join y in Funs.DB.SitePerson_MonthReportUnitDetail
                //                  on x.MonthReportDetailId equals y.MonthReportDetailId
                //                       join z in Funs.DB.Project_ProjectUnit
                //                       on x.UnitId equals z.UnitId
                //                       where z.UnitType == "2" && z.ProjectId == this.CurrUser.LoginProjectId && x.MonthReportId == monthReport.MonthReportId   //分包
                //                       select y.PersonWorkTime ?? 0).Sum();
            }
            else
            {
                monthHSEWorkDay = 0;
                hSEManhours = 0;
                SubcontractManHours = 0;
            }
            //年度人工日
            if (months.Month == 1)
            {
                yearHSEWorkDay = monthHSEWorkDay;
            }
            else
            {
                if (mr != null)
                {
                    if (mr.YearHSEWorkDay != null)
                    {
                        yearHSEWorkDay = (mr.YearHSEWorkDay ?? 0) + monthHSEWorkDay;
                    }
                    else
                    {
                        yearHSEWorkDay = monthHSEWorkDay;
                    }
                }
                else
                {
                    yearHSEWorkDay = monthHSEWorkDay;
                }
            }
            if (mr != null)
            {
                if (mr.SumHSEWorkDay != 0)
                {
                    sumHSEWorkDay = (mr.SumHSEWorkDay ?? 0) + monthHSEWorkDay;
                }
                else
                {
                    sumHSEWorkDay = monthHSEWorkDay;
                }
                if (mr.TotalHseManhours != 0)
                {
                    sumHseManhours = (mr.TotalHseManhours ?? 0) + hSEManhours + SubcontractManHours;
                }
                else
                {
                    sumHseManhours = hSEManhours + SubcontractManHours;
                }
            }
            else
            {
                sumHSEWorkDay = monthHSEWorkDay;
                sumHseManhours = hSEManhours + SubcontractManHours;
            }
            hseDay.MonthHSEWorkDay = monthHSEWorkDay ?? 0;
            hseDay.YearHSEWorkDay = yearHSEWorkDay ?? 0;
            hseDay.SumHSEWorkDay = sumHSEWorkDay ?? 0;
            if (hSEManhours.ToString().Contains("."))
            {
                hseDay.HseManhours = int.Parse(hSEManhours.ToString().Substring(0, hSEManhours.ToString().LastIndexOf(".")));
            }
            else
            {
                hseDay.HseManhours = int.Parse(hSEManhours.ToString());
            }
            if (SubcontractManHours.ToString().Contains("."))
            {
                hseDay.SubcontractManHours = int.Parse(SubcontractManHours.ToString().Substring(0, SubcontractManHours.ToString().LastIndexOf(".")));
            }
            else
            {
                hseDay.SubcontractManHours = int.Parse(SubcontractManHours.ToString());
            }
            if (sumHseManhours.ToString().Contains("."))
            {
                hseDay.TotalHseManhours = int.Parse(sumHseManhours.ToString().Substring(0, sumHseManhours.ToString().LastIndexOf(".")));
            }
            else
            {
                hseDay.TotalHseManhours = int.Parse(sumHseManhours.ToString());
            }
            List<Model.MonthReportCHSEDay> list = new List<Model.MonthReportCHSEDay>();
            list.Add(hseDay);
            this.gvHSEDay.DataSource = list;
            this.gvHSEDay.DataBind();
        }

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Manager_MonthReportC oldMonthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
            string MonthHSEDay = string.Empty, SumHSEDay = string.Empty, MonthHSEWorkDay = string.Empty, YearHSEWorkDay = string.Empty, SumHSEWorkDay = string.Empty,
                HseManhours = string.Empty, SubcontractManHours = string.Empty, TotalHseManhours = string.Empty;
            JArray mergedData = gvHSEDay.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                MonthHSEDay = values.Value<string>("MonthHSEDay").ToString();
                SumHSEDay = values.Value<string>("SumHSEDay").ToString();
                MonthHSEWorkDay = values.Value<string>("MonthHSEWorkDay").ToString();
                YearHSEWorkDay = values.Value<string>("YearHSEWorkDay").ToString();
                SumHSEWorkDay = values.Value<string>("SumHSEWorkDay").ToString();
                HseManhours = values.Value<string>("HseManhours").ToString();
                SubcontractManHours = values.Value<string>("SubcontractManHours").ToString();
                TotalHseManhours = values.Value<string>("TotalHseManhours").ToString();
            }
            if (oldMonthReport != null)
            {
                oldMonthReport.MonthHSEDay = Funs.GetNewIntOrZero(MonthHSEDay);
                oldMonthReport.SumHSEDay = Funs.GetNewIntOrZero(SumHSEDay);
                oldMonthReport.MonthHSEWorkDay = Funs.GetNewIntOrZero(MonthHSEWorkDay);
                oldMonthReport.YearHSEWorkDay = Funs.GetNewIntOrZero(YearHSEWorkDay);
                oldMonthReport.SumHSEWorkDay = Funs.GetNewIntOrZero(SumHSEWorkDay);
                oldMonthReport.HseManhours = Funs.GetNewIntOrZero(HseManhours);
                oldMonthReport.SubcontractManHours = Funs.GetNewIntOrZero(SubcontractManHours);
                oldMonthReport.TotalHseManhours = Funs.GetNewIntOrZero(TotalHseManhours);
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, oldMonthReport.MonthReportCode, oldMonthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnModify);
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
                monthReport.MonthHSEDay = Funs.GetNewIntOrZero(MonthHSEDay);
                monthReport.SumHSEDay = Funs.GetNewIntOrZero(SumHSEDay);
                monthReport.MonthHSEWorkDay = Funs.GetNewIntOrZero(MonthHSEWorkDay);
                monthReport.YearHSEWorkDay = Funs.GetNewIntOrZero(YearHSEWorkDay);
                monthReport.SumHSEWorkDay = Funs.GetNewIntOrZero(SumHSEWorkDay);
                monthReport.HseManhours = Funs.GetNewIntOrZero(HseManhours);
                monthReport.SubcontractManHours = Funs.GetNewIntOrZero(SubcontractManHours);
                monthReport.TotalHseManhours = Funs.GetNewIntOrZero(TotalHseManhours);
                BLL.MonthReportCService.AddMonthReport(monthReport);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}