using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.IO;

namespace FineUIPro.Web.SafetyData
{
    public partial class AccidentDataEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                this.drpProject.DataTextField = "ProjectName";
                drpProject.DataValueField = "ProjectId";
                drpProject.DataSource = BLL.ProjectService.GetAllProjectDropDownList();
                drpProject.DataBind();
                Funs.FineUIPleaseSelect(drpProject);
                drpAccidentType.DataValueField = "ConstValue";
                drpAccidentType.DataTextField = "ConstText";
                drpAccidentType.DataSource = (from x in Funs.DB.Sys_Const
                                              where x.GroupId == BLL.ConstValue.Group_AccidentReportRegistration && (x.ConstValue == "1" || x.ConstValue == "2" || x.ConstValue == "3")
                                              orderby x.SortIndex
                                              select x).ToList();
                drpAccidentType.DataBind();
                Funs.FineUIPleaseSelect(drpAccidentType);
                this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                if (!String.IsNullOrEmpty(Request.Params["resetManHoursId"]))
                {
                    var q = BLL.ResetManHoursService.GetResetManHoursByResetManHoursId(Request.Params["resetManHoursId"]);
                    if (q != null)
                    {
                        this.drpProject.SelectedValue = q.ProjectId;
                        if (q.AccidentDate != null)
                        {
                            this.txtAccidentDate.Text = string.Format("{0:yyyy-MM-dd}", q.AccidentDate);
                        }
                    }
                }
            }
        }

        private void LoadData()
        {

            btnClose.OnClientClick = ActiveWindow.GetHideReference();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpProject.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择项目！", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpAccidentType.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择事故类型！", MessageBoxIcon.Warning);
                return;
            }
            Model.Manager_ResetManHours newResetManHorus = new Model.Manager_ResetManHours
            {
                ProjectId = this.drpProject.SelectedValue,
                AccidentTypeId = this.drpAccidentType.SelectedValue,
                Abstract = "",
                AccidentDate = Funs.GetNewDateTimeOrNow(this.txtAccidentDate.Text.Trim())
            };
            //之前累计安全人工时指的是发生轻重死事故时，公司当时的累计安全人工时。
            int sumHseManhours3 = 0;
            DateTime date = Convert.ToDateTime(newResetManHorus.AccidentDate);
            DateTime months = Funs.GetNewDateTimeOrNow(date.Year.ToString() + "-" + date.Month.ToString() + "-01");
            DateTime endTime = months.AddMonths(1);
            Model.Manager_HeadMonthReportB headMonthReport = BLL.HeadMonthReportBService.GetLastHeadMonthReportByMonths(months);
            if (headMonthReport != null)
            {
                sumHseManhours3 = headMonthReport.AllSumTotalHseManhours ?? 0;   //上月公司月报累计安全人工时
            }
            List<Model.SitePerson_DayReport> newDayReports3 = (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= months && x.CompileDate < date && x.States == BLL.Const.State_2 select x).ToList();
            if (newDayReports3.Count > 0)
            {
                foreach (var dayReport in newDayReports3)
                {
                    sumHseManhours3 += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                }
            }
            newResetManHorus.BeforeManHours = sumHseManhours3;
            newResetManHorus.ProjectManager = BLL.ProjectService.GetProjectManagerName(this.drpProject.SelectedValue);
            newResetManHorus.HSSEManager = BLL.ProjectService.GetHSSEManagerName(this.drpProject.SelectedValue);
            newResetManHorus.AccidentReportId = null;
            BLL.ResetManHoursService.AddResetManHours(newResetManHorus);
            //更新总部月报的累计安全人工时
            Model.Manager_HeadMonthReportB headMonthReportB = BLL.HeadMonthReportBService.GetHeadMonthReportByMonths(months);
            if (headMonthReportB != null)   //清零当月总部月报存在
            {
                int hseManhours = 0;
                List<Model.SitePerson_DayReport> newDayReports = (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= date.AddDays(1) && x.CompileDate < endTime && x.States == BLL.Const.State_2 select x).ToList();
                if (newDayReports.Count > 0)
                {
                    foreach (var dayReport in newDayReports)
                    {
                        hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                int minusHseManhours = (headMonthReportB.AllSumTotalHseManhours ?? 0) - hseManhours;  //当前月份之后月报需要减去的累计安全人工时数
                headMonthReportB.AllSumHseManhours = hseManhours;
                headMonthReportB.AllSumTotalHseManhours = hseManhours;
                headMonthReportB.CheckMan = "清零月份";
                BLL.HeadMonthReportBService.UpdateHeadMonthReport(headMonthReportB);
                List<Model.Manager_HeadMonthReportB> headMonthReportBs = (from x in Funs.DB.Manager_HeadMonthReportB
                                                                          where x.Months > months
                                                                          select x).ToList();
                foreach (var item in headMonthReportBs)
                {
                    item.AllSumTotalHseManhours -= minusHseManhours;
                    BLL.HeadMonthReportBService.UpdateHeadMonthReport(item);
                }
            }
            //更新现场月报的累计安全人工时
            Model.Manager_MonthReportB monthReportB = BLL.MonthReportBService.GetMonthReportByMonth(months, this.drpProject.SelectedValue);
            {
                int hseManhours = 0;
                List<Model.SitePerson_DayReport> newDayReports = (from x in Funs.DB.SitePerson_DayReport where x.CompileDate >= date.AddDays(1) && x.CompileDate < endTime && x.ProjectId == this.drpProject.SelectedValue && x.States == BLL.Const.State_2 select x).ToList();
                if (newDayReports.Count > 0)
                {
                    foreach (var dayReport in newDayReports)
                    {
                        hseManhours += Convert.ToInt32((from y in Funs.DB.SitePerson_DayReportDetail where y.DayReportId == dayReport.DayReportId.ToString() select y.PersonWorkTime ?? 0).Sum());
                    }
                }
                int minusHseManhours = (monthReportB.TotalHseManhours ?? 0) - hseManhours;  //当前月份之后月报需要减去的累计安全人工时数
                monthReportB.HseManhours = hseManhours;
                monthReportB.TotalHseManhours = hseManhours;
                monthReportB.SafetyManhours = hseManhours;
                monthReportB.NoStartDate = date.AddDays(1);
                BLL.MonthReportBService.UpdateMonthReport(monthReportB);
                List<Model.Manager_MonthReportB> monthReportBs = (from x in Funs.DB.Manager_MonthReportB
                                                                      where x.Months > months && x.ProjectId == this.drpProject.SelectedValue
                                                                      select x).ToList();
                foreach (var item in monthReportBs)
                {
                    item.TotalHseManhours -= minusHseManhours;
                    item.SafetyManhours -= minusHseManhours;
                    item.NoStartDate = date.AddDays(1);
                    BLL.MonthReportBService.UpdateMonthReport(item);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}