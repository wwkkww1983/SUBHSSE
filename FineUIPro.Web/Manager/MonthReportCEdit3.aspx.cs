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
            if (oldMonthReport != null)
            {
                oldMonthReport.MonthHSEDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[1].ToString());
                oldMonthReport.SumHSEDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[2].ToString());
                oldMonthReport.MonthHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[4].ToString());
                oldMonthReport.YearHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[5].ToString());
                oldMonthReport.SumHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[6].ToString());
                oldMonthReport.HseManhours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[9].ToString());
                oldMonthReport.SubcontractManHours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[10].ToString());
                oldMonthReport.TotalHseManhours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[11].ToString());
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
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
                monthReport.MonthHSEDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[1].ToString());
                monthReport.SumHSEDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[2].ToString());
                monthReport.MonthHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[4].ToString());
                monthReport.YearHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[5].ToString());
                monthReport.SumHSEWorkDay = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[6].ToString());
                monthReport.HseManhours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[9].ToString());
                monthReport.SubcontractManHours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[10].ToString());
                monthReport.TotalHseManhours = Funs.GetNewIntOrZero(this.gvHSEDay.Rows[0].Values[11].ToString());
                BLL.MonthReportCService.AddMonthReport(monthReport);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加HSE月报告", monthReport.MonthReportId);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}