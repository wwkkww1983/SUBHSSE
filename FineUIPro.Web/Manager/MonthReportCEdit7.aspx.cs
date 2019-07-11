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
    public partial class MonthReportCEdit7 : PageBase
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
                    //7.1 管理绩效数据统计(表一)
                    accidentDesciptions = (from x in db.Manager_Month_AccidentDesciptionC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (accidentDesciptions.Count > 0)
                    {
                        this.gvAccidentDesciption.DataSource = accidentDesciptions;
                        this.gvAccidentDesciption.DataBind();
                    }
                    else
                    {
                        GetAccidentDesciption();//管理绩效数据统计（表一）
                    }
                    //7.2 管理绩效数据统计（表二）
                    AccidentDesciptionItems = (from x in db.Manager_Month_AccidentDesciptionItemC where x.MonthReportId == MonthReportId orderby x.SortIndex select x).ToList();
                    if (AccidentDesciptionItems.Count > 0)
                    {
                        this.gvAccidentDesciptionItem.DataSource = AccidentDesciptionItems;
                        this.gvAccidentDesciptionItem.DataBind();
                    }
                    else
                    {
                        GetAccidentDesciptionItem();//管理绩效数据统计（表二）
                    }
                    this.txtAccidentDes.Text = monthReport.AccidentDes;
                }
                else
                {
                    GetAccidentDesciption();//管理绩效数据统计（表一）
                    GetAccidentDesciptionItem();//管理绩效数据统计（表二）
                }
            }
        }
        #endregion

        #region 事故
        /// <summary>
        ///加载显示初始值
        /// </summary>
        private void GetAccidentDesciption()
        {
            System.Web.UI.WebControls.ListItem[] list = BLL.AccidentDesciptionCService.GetAccidentDesciptionList();
            var accidentPersonRecords = from x in Funs.DB.Accident_AccidentPersonRecord
                                        where x.ProjectId == this.CurrUser.LoginProjectId && x.AccidentDate >= startTime && x.AccidentDate < endTime
                                        select x;
            var yearAccidentPersonRecords = from x in Funs.DB.Accident_AccidentPersonRecord
                                            where x.ProjectId == this.CurrUser.LoginProjectId && x.AccidentDate >= yearStartTime && x.AccidentDate < endTime
                                            select x;
            var accidentHandles = from x in Funs.DB.Accident_AccidentHandle
                                  where x.ProjectId == this.CurrUser.LoginProjectId && x.AccidentDate >= startTime && x.AccidentDate < endTime
                                  select x;
            var yearAccidentHandles = from x in Funs.DB.Accident_AccidentHandle
                                      where x.ProjectId == this.CurrUser.LoginProjectId && x.AccidentDate >= yearStartTime && x.AccidentDate < endTime
                                      select x;
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Model.Manager_Month_AccidentDesciptionC des = new Model.Manager_Month_AccidentDesciptionC
                    {
                        AccidentDesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_AccidentDesciptionC)),
                        Matter = list[i].Value
                    };
                    if (des.Matter == "百万工时伤害率" || des.Matter == "伤害严重率")
                    {
                        //百万工时伤害率:轻重死总累计人数*1000000/累计总人工时（保留2位小数）
                        //伤害严重率:事故损失工时数/9*1000000/累计总人工时（保留2位小数）
                        if (accidentHandles.Count() > 0)
                        {
                            decimal totalHseManhours = 0;
                            Model.Manager_MonthReportC monthReport = BLL.MonthReportCService.GetMonthReportByMonths(Convert.ToDateTime(Request.Params["months"]), this.CurrUser.LoginProjectId);
                            if (monthReport != null)
                            {
                                if (monthReport.TotalHseManhours != null)
                                {
                                    totalHseManhours = monthReport.TotalHseManhours ?? 0;
                                }
                            }
                            else
                            {
                                Model.SitePerson_MonthReport mReport = BLL.Funs.DB.SitePerson_MonthReport.FirstOrDefault(x => x.CompileDate == Convert.ToDateTime(Request.Params["months"]));   //当月人工时月报
                                if (mReport != null)
                                {
                                    totalHseManhours = (from x in Funs.DB.SitePerson_MonthReportDetail
                                                        join y in Funs.DB.SitePerson_MonthReportUnitDetail
                                                            on x.MonthReportDetailId equals y.MonthReportDetailId
                                                        where x.MonthReportId == mReport.MonthReportId
                                                        select y.PersonWorkTime ?? 0).Sum();
                                }
                            }
                            if (des.Matter == "百万工时伤害率")
                            {
                                int a = (from x in accidentHandles
                                         select x.MinorInjuriesPersonNum ?? 0).Sum();
                                int b = (from x in accidentHandles
                                         select x.InjuriesPersonNum ?? 0).Sum();
                                int c = (from x in accidentHandles
                                         select x.DeathPersonNum ?? 0).Sum();
                                if (totalHseManhours != 0)
                                {
                                    des.MonthDataNum = decimal.Round((Convert.ToDecimal(a + b + c) * 1000000 / totalHseManhours), 2);
                                }
                                else
                                {
                                    des.MonthDataNum = 0;
                                }
                                int ya = (from x in yearAccidentHandles
                                          select x.MinorInjuriesPersonNum ?? 0).Sum();
                                int yb = (from x in yearAccidentHandles
                                          select x.InjuriesPersonNum ?? 0).Sum();
                                int yc = (from x in yearAccidentHandles
                                          select x.DeathPersonNum ?? 0).Sum();
                                if (totalHseManhours != 0)
                                {
                                    des.YearDataNum = decimal.Round((Convert.ToDecimal(ya + yb + yc) * 1000000 / totalHseManhours), 2);
                                }
                                else
                                {
                                    des.YearDataNum = 0;
                                }
                            }
                            else if (des.Matter == "伤害严重率")
                            {
                                decimal workHoursLoss = (from x in accidentHandles
                                                         select x.WorkHoursLoss ?? 0).Sum();
                                if (totalHseManhours != 0)
                                {
                                    des.MonthDataNum = decimal.Round((Convert.ToDecimal(workHoursLoss) / 9 * 1000000 / totalHseManhours), 2);
                                }
                                else
                                {
                                    des.MonthDataNum = 0;
                                }
                                decimal yearWorkHoursLoss = (from x in yearAccidentHandles
                                                         select x.WorkHoursLoss ?? 0).Sum();
                                if (totalHseManhours != 0)
                                {
                                    des.YearDataNum = decimal.Round((Convert.ToDecimal(yearWorkHoursLoss) / 9 * 1000000 / totalHseManhours), 2);
                                }
                                else
                                {
                                    des.YearDataNum = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        des.MonthDataNum = (from x in accidentPersonRecords
                                            join y in Funs.DB.Base_AccidentType
                                                on x.AccidentTypeId equals y.AccidentTypeId
                                            where y.AccidentTypeName.Contains(des.Matter)
                                            select x).Count();
                        des.YearDataNum = (from x in yearAccidentPersonRecords
                                           join y in Funs.DB.Base_AccidentType
                                               on x.AccidentTypeId equals y.AccidentTypeId
                                           where y.AccidentTypeName.Contains(des.Matter)
                                           select x).Count();
                    }
                    accidentDesciptions.Add(des);
                }
            }
            this.gvAccidentDesciption.DataSource = accidentDesciptions;
            this.gvAccidentDesciption.DataBind();
        }

        /// <summary>
        /// 检查并保存事故（表一）集合
        /// </summary>
        private void jerqueSaveAccidentDesciptionList()
        {
            accidentDesciptions.Clear();
            JArray mergedData = gvAccidentDesciption.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_AccidentDesciptionC accidentDesciptionSort = new Model.Manager_Month_AccidentDesciptionC
                {
                    AccidentDesId = this.gvAccidentDesciption.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    Matter = values.Value<string>("Matter").ToString(),
                    MonthDataNum = Funs.GetNewDecimalOrZero(values.Value<string>("MonthDataNum").ToString()),
                    YearDataNum = Funs.GetNewDecimalOrZero(values.Value<string>("YearDataNum").ToString())
                };
                accidentDesciptions.Add(accidentDesciptionSort);
            }
        }

        /// <summary>
        /// 加载显示初始值
        /// </summary>
        private void GetAccidentDesciptionItem()
        {
            System.Web.UI.WebControls.ListItem[] list = BLL.AccidentDesciptionItemCService.GetMatterList();
            var accidentHandles = from x in Funs.DB.Accident_AccidentHandle
                                  where x.ProjectId == this.CurrUser.LoginProjectId && x.AccidentDate >= startTime && x.AccidentDate < endTime
                                  select x;
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    Model.Manager_Month_AccidentDesciptionItemC item = new Model.Manager_Month_AccidentDesciptionItemC
                    {
                        AccidentDesItemId = SQLHelper.GetNewID(typeof(Model.Manager_Month_AccidentDesciptionItemC)),
                        Matter = list[i].Value
                    };
                    if (accidentHandles.Count() > 0)
                    {
                        if (item.Matter == "轻伤人数")
                        {
                            item.Datas = (from x in accidentHandles
                                          select x.MinorInjuriesPersonNum ?? 0).Sum().ToString();
                        }
                        else if (item.Matter == "重伤人数")
                        {
                            item.Datas = (from x in accidentHandles
                                          select x.InjuriesPersonNum ?? 0).Sum().ToString();
                        }
                        else if (item.Matter == "死亡人数")
                        {
                            item.Datas = (from x in accidentHandles
                                          select x.DeathPersonNum ?? 0).Sum().ToString();
                        }
                        else if (item.Matter == "直接经济损失")
                        {
                            item.Datas = (from x in accidentHandles
                                          select x.MoneyLoss ?? 0).Sum().ToString();
                        }
                        else if (item.Matter == "事故失时数")
                        {
                            item.Datas = (from x in accidentHandles
                                          select x.WorkHoursLoss ?? 0).Sum().ToString();
                        }
                    }
                    else
                    {
                        item.Datas = "无";
                    }
                    AccidentDesciptionItems.Add(item);
                }
            }
            this.gvAccidentDesciptionItem.DataSource = AccidentDesciptionItems;
            this.gvAccidentDesciptionItem.DataBind();
        }

        /// <summary>
        /// 检查并保存事故（表二）集合
        /// </summary>
        private void jerqueSaveAccidentDesciptionItemList()
        {
            AccidentDesciptionItems.Clear();
            JArray mergedData = gvAccidentDesciptionItem.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                int i = mergedRow.Value<int>("index");
                Model.Manager_Month_AccidentDesciptionItemC accidentDesciptionItemSort = new Model.Manager_Month_AccidentDesciptionItemC
                {
                    AccidentDesItemId = this.gvAccidentDesciptionItem.Rows[i].DataKeys[0].ToString(),
                    SortIndex = i,
                    Matter = values.Value<string>("Matter").ToString(),
                    Datas = values.Value<string>("Datas").ToString()
                };
                AccidentDesciptionItems.Add(accidentDesciptionItemSort);
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
                oldMonthReport.AccidentDes = this.txtAccidentDes.Text.Trim();
                BLL.MonthReportCService.UpdateMonthReport(oldMonthReport);
                OperateAccidentDesciptionSort(MonthReportId);
                OperateAccidentDesciptionItemSort(MonthReportId);
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
                monthReport.AccidentDes = this.txtAccidentDes.Text.Trim();
                BLL.MonthReportCService.AddMonthReport(monthReport);
                OperateAccidentDesciptionSort(MonthReportId);
                OperateAccidentDesciptionItemSort(MonthReportId);
                BLL.LogService.AddSys_Log(this.CurrUser, monthReport.MonthReportCode, monthReport.MonthReportId, BLL.Const.ProjectManagerMonthCMenuId, BLL.Const.BtnAdd);
            }
            ShowNotify("保存成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 7.1 管理绩效数据统计 表一
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateAccidentDesciptionSort(string monthReportId)
        {
            BLL.AccidentDesciptionCService.DeleteAccidentDesciptionByMonthReportId(monthReportId);
            jerqueSaveAccidentDesciptionList();
            foreach (Model.Manager_Month_AccidentDesciptionC des in accidentDesciptions)
            {
                des.MonthReportId = monthReportId;
                BLL.AccidentDesciptionCService.AddAccidentDesciption(des);
            }
        }

        /// <summary>
        /// 7.2 管理绩效数据统计 表二
        /// </summary>
        /// <param name="monthReportId"></param>
        private void OperateAccidentDesciptionItemSort(string monthReportId)
        {
            BLL.AccidentDesciptionItemCService.DeleteAccidentDesciptionItemByMonthReportId(monthReportId);
            jerqueSaveAccidentDesciptionItemList();
            foreach (Model.Manager_Month_AccidentDesciptionItemC item in AccidentDesciptionItems)
            {
                item.MonthReportId = monthReportId;
                BLL.AccidentDesciptionItemCService.AddAccidentDesciptionItem(item);
            }
        }
        #endregion
    }
}