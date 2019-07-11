using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Information
{
    public partial class DrillConductedQuarterlyReportAdd : PageBase
    {
        #region 定义变量
        public string DrillConductedQuarterlyReportId
        {
            get
            {
                return (string)ViewState["DrillConductedQuarterlyReportId"];
            }
            set
            {
                ViewState["DrillConductedQuarterlyReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Information_DrillConductedQuarterlyReportItem> items = new List<Model.Information_DrillConductedQuarterlyReportItem>();
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                items.Clear();
                this.ddlUnitId.DataTextField = "UnitName";
                this.ddlUnitId.DataValueField = "UnitId";
                this.ddlUnitId.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                this.ddlUnitId.DataBind();

                this.ddlYearId.DataTextField = "ConstText";
                ddlYearId.DataValueField = "ConstValue";
                ddlYearId.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                ddlYearId.DataBind();

                this.ddlQuarter.DataTextField = "ConstText";
                ddlQuarter.DataValueField = "ConstValue";
                ddlQuarter.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0011);
                ddlQuarter.DataBind();
                this.ddlUnitId.Readonly = true;
                string unitId = Request.Params["UnitId"];
                string year = Request.QueryString["Year"];
                string quarter = Request.QueryString["Quarter"];
                this.DrillConductedQuarterlyReportId = Request.Params["DrillConductedQuarterlyReportId"];
                if (!string.IsNullOrEmpty(this.DrillConductedQuarterlyReportId))
                {
                    items = BLL.DrillConductedQuarterlyReportItemService.GetDrillConductedQuarterlyReportItemList(this.DrillConductedQuarterlyReportId);
                    int i = items.Count * 10;
                    int count = items.Count;
                    if (count < 10)
                    {
                        for (int j = 0; j < (10 - count); j++)
                        {
                            i += 10;
                            Model.Information_DrillConductedQuarterlyReportItem newItem = new Model.Information_DrillConductedQuarterlyReportItem
                            {
                                DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReportItem)),
                                SortIndex = i
                            };
                            items.Add(newItem);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drillConductedQuarterlyReport = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(this.DrillConductedQuarterlyReportId);
                    if (drillConductedQuarterlyReport != null)
                    {
                        this.btnCopy.Hidden = true;
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        if (drillConductedQuarterlyReport.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                        else
                        {
                            if (drillConductedQuarterlyReport.HandleMan == this.CurrUser.UserId)
                            {
                                this.btnSave.Hidden = false;
                                this.btnSubmit.Hidden = false;
                            }
                        }
                        if (drillConductedQuarterlyReport.UpState == BLL.Const.UpState_3)  //已上报
                        {
                            this.btnSave.Hidden = true;
                            this.btnUpdata.Hidden = true;
                        }
                        if (!string.IsNullOrEmpty(drillConductedQuarterlyReport.UnitId))
                        {
                            this.ddlUnitId.SelectedValue = drillConductedQuarterlyReport.UnitId;
                        }
                        if (drillConductedQuarterlyReport.YearId.HasValue)
                        {
                            this.ddlYearId.SelectedValue = drillConductedQuarterlyReport.YearId.ToString();
                        }
                        if (drillConductedQuarterlyReport.Quarter.HasValue)
                        {
                            this.ddlQuarter.SelectedValue = drillConductedQuarterlyReport.Quarter.ToString();
                        }
                        if (drillConductedQuarterlyReport.ReportDate != null)
                        {
                            this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drillConductedQuarterlyReport.ReportDate);
                        }
                    }
                }
                else
                {
                    this.btnCopy.Hidden = false;
                    this.ddlUnitId.SelectedValue = unitId;
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.ddlYearId.SelectedValue = year;
                    this.ddlQuarter.SelectedValue = quarter;
                     //获取项目报告集合
                    List<Model.InformationProject_DrillConductedQuarterlyReportItem> projectItems = (from x in Funs.DB.InformationProject_DrillConductedQuarterlyReport 
                                                                                                     join y in Funs.DB.InformationProject_DrillConductedQuarterlyReportItem
                                                                                                     on x.DrillConductedQuarterlyReportId equals y.DrillConductedQuarterlyReportId
                                                                                                     where x.YearId.ToString() == year && x.Quarter.ToString() == quarter && x.States == BLL.Const.State_2 
                                                                                                     select y).Distinct().ToList();
                    if (projectItems.Count > 0)
                    {
                        int i = 0;
                        foreach (var projectItem in projectItems)
                        {
                            i += 10;
                            Model.Information_DrillConductedQuarterlyReportItem item = new Model.Information_DrillConductedQuarterlyReportItem
                            {
                                DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReportItem)),
                                SortIndex = i
                            };
                            Model.Base_Project project = (from x in Funs.DB.Base_Project
                                                          join y in Funs.DB.InformationProject_DrillConductedQuarterlyReport
                                                          on x.ProjectId equals y.ProjectId
                                                          join z in Funs.DB.InformationProject_DrillConductedQuarterlyReportItem
                                                          on y.DrillConductedQuarterlyReportId equals z.DrillConductedQuarterlyReportId
                                                          where z.DrillConductedQuarterlyReportItemId == projectItem.DrillConductedQuarterlyReportItemId
                                                          select x).FirstOrDefault();
                            if (project != null)
                            {
                                item.IndustryType = project.ProjectName;
                            }
                            item.TotalConductCount = projectItem.TotalConductCount;
                            item.TotalPeopleCount = projectItem.TotalPeopleCount;
                            item.TotalInvestment = projectItem.TotalInvestment;
                            item.HQConductCount = projectItem.HQConductCount;
                            item.HQPeopleCount = projectItem.HQPeopleCount;
                            item.HQInvestment = projectItem.HQInvestment;
                            item.BasicConductCount = projectItem.BasicConductCount;
                            item.BasicPeopleCount = projectItem.BasicPeopleCount;
                            item.BasicInvestment = projectItem.BasicInvestment;
                            item.ComprehensivePractice = projectItem.ComprehensivePractice;
                            item.CPScene = projectItem.CPScene;
                            item.CPDesktop = projectItem.CPDesktop;
                            item.SpecialDrill = projectItem.SpecialDrill;
                            item.SDScene = projectItem.SDScene;
                            item.SDDesktop = projectItem.SDDesktop;
                            items.Add(item);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            i += 10;
                            Model.Information_DrillConductedQuarterlyReportItem newItem = new Model.Information_DrillConductedQuarterlyReportItem
                            {
                                DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReportItem)),
                                SortIndex = i
                            };
                            items.Add(newItem);
                        }
                    }
                    Grid1.DataSource = items;
                    Grid1.DataBind();


                }
            }
        }

        #endregion

        #region 保存、提交、上报
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save("add");
        }

        /// <summary>
        /// 上报按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            Save("updata");
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save("submit");
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void Save(string type)
        {
            if (this.ddlUnitId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            var drill = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdDate(this.ddlUnitId.SelectedValue, Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue), Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue), this.DrillConductedQuarterlyReportId);
            if (drill != null)
            {
                ShowNotify("本单位本季度报表已存在，不能重复编制", MessageBoxIcon.Warning);
                return;
            }

            Model.Information_DrillConductedQuarterlyReport drillConductedQuarterlyReport = new Model.Information_DrillConductedQuarterlyReport();
            if (this.ddlUnitId.SelectedValue != BLL.Const._Null)
            {
                drillConductedQuarterlyReport.UnitId = this.ddlUnitId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.dpkCompileDate.Text.Trim()))
            {
                drillConductedQuarterlyReport.ReportDate = Convert.ToDateTime(this.dpkCompileDate.Text.Trim());
            }
            drillConductedQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            drillConductedQuarterlyReport.Quarter = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
            if (string.IsNullOrEmpty(this.DrillConductedQuarterlyReportId))
            {
                this.DrillConductedQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReport)); drillConductedQuarterlyReport.DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId;
                drillConductedQuarterlyReport.CompileMan = this.CurrUser.UserName;
                drillConductedQuarterlyReport.UpState = BLL.Const.UpState_2;
                drillConductedQuarterlyReport.HandleMan = this.CurrUser.UserId;
                drillConductedQuarterlyReport.HandleState = BLL.Const.HandleState_1;
                BLL.DrillConductedQuarterlyReportService.AddDrillConductedQuarterlyReport(drillConductedQuarterlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, drillConductedQuarterlyReport.YearId.ToString() + "-" + drillConductedQuarterlyReport.Quarter.ToString(),
                        drillConductedQuarterlyReport.DrillConductedQuarterlyReportId, BLL.Const.DrillConductedQuarterlyReportMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Information_DrillConductedQuarterlyReport oldReport = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(this.DrillConductedQuarterlyReportId);
                if (oldReport != null)
                {
                    drillConductedQuarterlyReport.HandleMan = oldReport.HandleMan;
                    drillConductedQuarterlyReport.HandleState = oldReport.HandleState;
                }
                drillConductedQuarterlyReport.DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId;
                drillConductedQuarterlyReport.UpState = BLL.Const.UpState_2;
                BLL.DrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(drillConductedQuarterlyReport);
                BLL.LogService.AddSys_Log(this.CurrUser, drillConductedQuarterlyReport.YearId.ToString() + "-" + drillConductedQuarterlyReport.Quarter.ToString(),
                        drillConductedQuarterlyReport.DrillConductedQuarterlyReportId, BLL.Const.DrillConductedQuarterlyReportMenuId, BLL.Const.BtnModify);
                BLL.DrillConductedQuarterlyReportItemService.DeleteDrillConductedQuarterlyReportItemList(drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            }
            GetItems(drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            foreach (var item in items)
            {
                BLL.DrillConductedQuarterlyReportItemService.AddDrillConductedQuarterlyReportItem(item);
            }
            if (type == "updata")     //保存并上报
            {
                Update(drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            }
            if (type == "submit")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReportSubmit.aspx?Type=DrillConductedQuarterlyReport&Id={0}", drillConductedQuarterlyReport.DrillConductedQuarterlyReportId, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 获取明细值
        /// </summary>
        /// <param name="drillConductedQuarterlyReportId"></param>
        private void GetItems(string drillConductedQuarterlyReportId)
        {
            items.Clear();
            int i = 10;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                //if (values["IndustryType"].ToString() != "")
                //{
                Model.Information_DrillConductedQuarterlyReportItem item = new Model.Information_DrillConductedQuarterlyReportItem();
                if (values["DrillConductedQuarterlyReportItemId"].ToString() != "")
                {
                    item.DrillConductedQuarterlyReportItemId = values.Value<string>("DrillConductedQuarterlyReportItemId");
                }
                item.DrillConductedQuarterlyReportId = drillConductedQuarterlyReportId;
                item.SortIndex = i;
                if (values["IndustryType"].ToString() != "")
                {
                    item.IndustryType = values.Value<string>("IndustryType");
                }
                if (values["TotalConductCount"].ToString() != "")
                {
                    item.TotalConductCount = values.Value<int>("TotalConductCount");
                }
                if (values["TotalPeopleCount"].ToString() != "")
                {
                    item.TotalPeopleCount = values.Value<int>("TotalPeopleCount");
                }
                if (values["TotalInvestment"].ToString() != "")
                {
                    item.TotalInvestment = values.Value<int>("TotalInvestment");
                }
                if (values["HQConductCount"].ToString() != "")
                {
                    item.HQConductCount = values.Value<int>("HQConductCount");
                }
                if (values["HQPeopleCount"].ToString() != "")
                {
                    item.HQPeopleCount = values.Value<int>("HQPeopleCount");
                }
                if (values["HQInvestment"].ToString() != "")
                {
                    item.HQInvestment = values.Value<int>("HQInvestment");
                }
                if (values["BasicConductCount"].ToString() != "")
                {
                    item.BasicConductCount = values.Value<int>("BasicConductCount");
                }
                if (values["BasicPeopleCount"].ToString() != "")
                {
                    item.BasicPeopleCount = values.Value<int>("BasicPeopleCount");
                }
                if (values["BasicInvestment"].ToString() != "")
                {
                    item.BasicInvestment = values.Value<int>("BasicInvestment");
                }
                if (values["ComprehensivePractice"].ToString() != "")
                {
                    item.ComprehensivePractice = values.Value<int>("ComprehensivePractice");
                }
                if (values["CPScene"].ToString() != "")
                {
                    item.CPScene = values.Value<int>("CPScene");
                }
                if (values["CPDesktop"].ToString() != "")
                {
                    item.CPDesktop = values.Value<int>("CPDesktop");
                }
                if (values["SpecialDrill"].ToString() != "")
                {
                    item.SpecialDrill = values.Value<int>("SpecialDrill");
                }
                if (values["SDScene"].ToString() != "")
                {
                    item.SDScene = values.Value<int>("SDScene");
                }
                if (values["SDDesktop"].ToString() != "")
                {
                    item.SDDesktop = values.Value<int>("SDDesktop");
                }
                items.Add(item);
                i += 10;
                //}
            }
        }
        #endregion

        #region 同步数据
        private void Update(string drillConductedQuarterlyReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertInformation_DrillConductedQuarterlyReportTableCompleted += new EventHandler<HSSEService.DataInsertInformation_DrillConductedQuarterlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_DrillConductedQuarterlyReportTableCompleted);
            var report = from x in Funs.DB.Information_DrillConductedQuarterlyReport
                         where x.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId && x.UpState == BLL.Const.UpState_2
                         select new HSSEService.Information_DrillConductedQuarterlyReport
                         {
                             DrillConductedQuarterlyReportId = x.DrillConductedQuarterlyReportId,
                             UnitId = x.UnitId,
                             ReportDate = x.ReportDate,
                             Quarter = x.Quarter,
                             YearId = x.YearId,
                             CompileMan = x.CompileMan,
                         };

            var reportItem = from x in Funs.DB.Information_DrillConductedQuarterlyReportItem
                             where x.DrillConductedQuarterlyReportId == drillConductedQuarterlyReportId
                             select new HSSEService.Information_DrillConductedQuarterlyReportItem
                             {
                                 DrillConductedQuarterlyReportItemId = x.DrillConductedQuarterlyReportItemId,
                                 DrillConductedQuarterlyReportId = x.DrillConductedQuarterlyReportId,
                                 IndustryType = x.IndustryType,
                                 TotalConductCount = x.TotalConductCount,
                                 TotalPeopleCount = x.TotalPeopleCount,
                                 TotalInvestment = x.TotalInvestment,
                                 HQConductCount = x.HQConductCount,
                                 HQPeopleCount = x.HQPeopleCount,
                                 HQInvestment = x.HQInvestment,
                                 BasicConductCount = x.BasicConductCount,
                                 BasicPeopleCount = x.BasicPeopleCount,
                                 BasicInvestment = x.BasicInvestment,
                                 ComprehensivePractice = x.ComprehensivePractice,
                                 CPScene = x.CPScene,
                                 CPDesktop = x.CPDesktop,
                                 SpecialDrill = x.SpecialDrill,
                                 SDScene = x.SDScene,
                                 SDDesktop = x.SDDesktop,
                                 SortIndex = x.SortIndex,
                             };
            poxy.DataInsertInformation_DrillConductedQuarterlyReportTableAsync(report.ToList(), reportItem.ToList());
        }

        #region 应急演练开展情况季报表
        /// <summary>
        /// 应急演练开展情况季报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_DrillConductedQuarterlyReportTableCompleted(object sender, HSSEService.DataInsertInformation_DrillConductedQuarterlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.DrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(report);
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillConductedQuarterlyReportMenuId, item);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_4 && x.YearId == report.YearId.ToString() && x.QuarterId == report.Quarter.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急演练开展情况季报表】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.DrillConductedQuarterlyReportMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {                
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急演练开展情况季报表】上传到服务器失败；", null, BLL.Const.DrillConductedQuarterlyReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
        #endregion

        #region 关闭办理流程窗口
        /// <summary>
        /// 关闭办理流程窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(this.DrillConductedQuarterlyReportId);
            if (report.HandleMan == this.CurrUser.UserId)
            {
                this.btnSave.Hidden = false;
                this.btnSubmit.Hidden = false;
            }
            else
            {
                this.btnSave.Hidden = true;
                this.btnSubmit.Hidden = true;
            }
        }
        #endregion

        #region Grid行事件
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            GetItems(string.Empty);
            if (e.CommandName == "Add")
            {
                Model.Information_DrillConductedQuarterlyReportItem oldItem = items.FirstOrDefault(x => x.DrillConductedQuarterlyReportItemId == rowID);
                Model.Information_DrillConductedQuarterlyReportItem newItem = new Model.Information_DrillConductedQuarterlyReportItem
                {
                    DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReportItem))
                };
                if (oldItem != null)
                {
                    newItem.SortIndex = oldItem.SortIndex + 1;
                }
                else
                {
                    newItem.SortIndex = 0;
                }
                items.Add(newItem);
                items = items.OrderBy(x => x.SortIndex).ToList();
                Grid1.DataSource = items;
                Grid1.DataBind();
            }
            if (e.CommandName == "Delete")
            {
                if (Grid1.Rows.Count == 1)
                {
                    ShowNotify("只有一条数据，无法删除", MessageBoxIcon.Warning);
                    return;
                }
                foreach (var item in items)
                {
                    if (item.DrillConductedQuarterlyReportItemId == rowID)
                    {
                        items.Remove(item);
                        break;
                    }
                }
                Grid1.DataSource = items;
                Grid1.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DrillConductedQuarterlyReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnCopy.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnSaveUp))
                //{
                //    this.btnUpdata.Hidden = false;

                //}
                if (buttonList.Contains(BLL.Const.BtnSubmit))
                {
                    this.btnSubmit.Hidden = false;
                }
            }
        }
        #endregion

        #region 复制上个季度数据
        /// <summary>
        /// 复制上个季度的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            int lastYear = 0, lastQuarter = 0;
            int year = Convert.ToInt32(this.ddlYearId.SelectedValue);
            int quarter = Convert.ToInt32(this.ddlQuarter.SelectedValue);
            if (quarter == 1)
            {
                lastYear = year - 1;
                lastQuarter = 4;
            }
            else
            {
                lastYear = year;
                lastQuarter = quarter - 1;
            }
            Model.Information_DrillConductedQuarterlyReport drillConductedQuarterlyReport = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(this.ddlUnitId.SelectedValue, lastYear, lastQuarter);
            if (drillConductedQuarterlyReport != null)
            {
                Model.Information_DrillConductedQuarterlyReport newDrillConductedQuarterlyReport = new Model.Information_DrillConductedQuarterlyReport();
                this.DrillConductedQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReport));
                newDrillConductedQuarterlyReport.DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId;
                newDrillConductedQuarterlyReport.UnitId = this.ddlUnitId.SelectedValue;
                newDrillConductedQuarterlyReport.ReportDate = DateTime.Now;
                newDrillConductedQuarterlyReport.Quarter = Funs.GetNewIntOrZero(this.ddlQuarter.SelectedValue);
                newDrillConductedQuarterlyReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
                newDrillConductedQuarterlyReport.CompileMan = this.CurrUser.UserName;
                newDrillConductedQuarterlyReport.UpState = BLL.Const.UpState_2;
                newDrillConductedQuarterlyReport.HandleState = BLL.Const.HandleState_1;
                newDrillConductedQuarterlyReport.HandleMan = this.CurrUser.UserId;
                BLL.DrillConductedQuarterlyReportService.AddDrillConductedQuarterlyReport(newDrillConductedQuarterlyReport);

                items = BLL.DrillConductedQuarterlyReportItemService.GetDrillConductedQuarterlyReportItemList(drillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        Model.Information_DrillConductedQuarterlyReportItem newItem = new Model.Information_DrillConductedQuarterlyReportItem
                        {
                            DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillConductedQuarterlyReportItem)),
                            DrillConductedQuarterlyReportId = this.DrillConductedQuarterlyReportId,
                            IndustryType = item.IndustryType,
                            TotalConductCount = item.TotalConductCount,
                            TotalPeopleCount = item.TotalPeopleCount,
                            TotalInvestment = item.TotalInvestment,
                            HQConductCount = item.HQConductCount,
                            HQPeopleCount = item.HQPeopleCount,
                            HQInvestment = item.HQInvestment,
                            BasicConductCount = item.BasicConductCount,
                            BasicPeopleCount = item.BasicPeopleCount,
                            BasicInvestment = item.BasicInvestment,
                            ComprehensivePractice = item.ComprehensivePractice,
                            CPScene = item.CPScene,
                            CPDesktop = item.CPDesktop,
                            SpecialDrill = item.SpecialDrill,
                            SDScene = item.SDScene,
                            SDDesktop = item.SDDesktop,
                            SortIndex = item.SortIndex
                        };
                        BLL.DrillConductedQuarterlyReportItemService.AddDrillConductedQuarterlyReportItem(newItem);
                    }
                }
                GetValues(newDrillConductedQuarterlyReport.DrillConductedQuarterlyReportId);
            }

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private void GetValues(string drillConductedQuarterlyReportId)
        {
            var report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(drillConductedQuarterlyReportId);
            if (report != null)
            {
                if (!string.IsNullOrEmpty(report.UnitId))
                {
                    this.ddlUnitId.SelectedValue = report.UnitId;
                }
                if (report.YearId.HasValue)
                {
                    this.ddlYearId.SelectedValue = report.YearId.ToString();
                }
                if (report.Quarter.HasValue)
                {
                    this.ddlQuarter.SelectedValue = report.Quarter.ToString();
                }
                if (report.ReportDate != null)
                {
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", report.ReportDate);
                }
                items = BLL.DrillConductedQuarterlyReportItemService.GetDrillConductedQuarterlyReportItemList(drillConductedQuarterlyReportId);
                this.Grid1.DataSource = items;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}