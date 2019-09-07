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
    public partial class DrillPlanHalfYearReportAdd : PageBase
    {
        #region 定义变量
        public string DrillPlanHalfYearReportId
        {
            get
            {
                return (string)ViewState["DrillPlanHalfYearReportId"];
            }
            set
            {
                ViewState["DrillPlanHalfYearReportId"] = value;
            }
        }

        private static List<Model.Information_DrillPlanHalfYearReportItem> items = new List<Model.Information_DrillPlanHalfYearReportItem>();
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                items.Clear();
                this.ddlUnitId.DataValueField = "UnitId";
                this.ddlUnitId.DataTextField = "UnitName";
                this.ddlUnitId.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                this.ddlUnitId.DataBind();

                this.ddlYearId.DataTextField = "ConstText";
                ddlYearId.DataValueField = "ConstValue";
                ddlYearId.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                ddlYearId.DataBind();
                //this.ddlYearId.SelectedValue = DateTime.Now.Year.ToString();

                this.ddlHalfYearId.DataTextField = "ConstText";
                ddlHalfYearId.DataValueField = "ConstValue";
                ddlHalfYearId.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0010);
                ddlHalfYearId.DataBind();
                this.ddlHalfYearId.SelectedValue = Funs.GetNowHalfYearByTime(DateTime.Now).ToString();

                this.ddlUnitId.Readonly = true;
                string year = Request.QueryString["Year"];
                string halfYear = Request.QueryString["HalfYear"];
                this.DrillPlanHalfYearReportId = Request.Params["DrillPlanHalfYearReportId"];
                if (!string.IsNullOrEmpty(this.DrillPlanHalfYearReportId))
                {
                    items = BLL.DrillPlanHalfYearReportItemService.GetDrillPlanHalfYearReportItemList(this.DrillPlanHalfYearReportId);
                    int i = items.Count * 10;
                    int count = items.Count;
                    if (count < 10)
                    {
                        for (int j = 0; j < (10 - count); j++)
                        {
                            i += 10;
                            Model.Information_DrillPlanHalfYearReportItem newItem = new Model.Information_DrillPlanHalfYearReportItem
                            {
                                DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem)),
                                SortIndex = i
                            };
                            items.Add(newItem);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();

                    var drill = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.DrillPlanHalfYearReportId);
                    if (drill != null)
                    {
                        this.btnCopy.Hidden = true;
                        this.btnSave.Hidden = true;
                        this.btnSubmit.Hidden = true;
                        if (drill.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                        else
                        {
                            if (drill.HandleMan == this.CurrUser.UserId)
                            {
                                this.btnSave.Hidden = false;
                                this.btnSubmit.Hidden = false;
                            }
                        }
                        if (drill.UpState == BLL.Const.UpState_3)  //已上报
                        {
                            this.btnSave.Hidden = true;
                            this.btnUpdata.Hidden = true;
                        }
                        if (!string.IsNullOrEmpty(drill.UnitId.Trim()))
                        {
                            this.ddlUnitId.SelectedValue = drill.UnitId.Trim();
                        }
                        if (drill.YearId.HasValue)
                        {
                            this.ddlYearId.SelectedValue = drill.YearId.ToString();
                        }
                        if (drill.HalfYearId.HasValue)
                        {
                            this.ddlHalfYearId.SelectedValue = drill.HalfYearId.ToString();
                        }
                        if (!string.IsNullOrEmpty(drill.CompileMan))
                        {
                            this.txtCompileMan.Text = drill.CompileMan;
                        }
                        this.txtTel.Text = drill.Telephone;
                        if (drill.CompileDate != null)
                        {
                            this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drill.CompileDate);
                        }
                    }
                }
                else
                {
                    this.btnCopy.Hidden = false;
                    this.ddlUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.ddlYearId.SelectedValue = year;
                    this.ddlHalfYearId.SelectedValue = halfYear;
                    //获取项目报告集合
                    List<Model.InformationProject_DrillPlanHalfYearReportItem> projectItems = (from x in Funs.DB.InformationProject_DrillPlanHalfYearReport
                                                                                                     join y in Funs.DB.InformationProject_DrillPlanHalfYearReportItem
                                                                                                     on x.DrillPlanHalfYearReportId equals y.DrillPlanHalfYearReportId
                                                                                                     where x.YearId.ToString() == year && x.HalfYearId.ToString() == halfYear && x.States == BLL.Const.State_2
                                                                                                     select y).Distinct().ToList();
                    if (projectItems.Count > 0)
                    {
                        int i = 0;
                        foreach (var projectItem in projectItems)
                        {
                            i += 10;
                            Model.Information_DrillPlanHalfYearReportItem item = new Model.Information_DrillPlanHalfYearReportItem
                            {
                                DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem)),
                                SortIndex = i,
                                DrillPlanName = projectItem.DrillPlanName,
                                OrganizationUnit = projectItem.OrganizationUnit,
                                DrillPlanDate = projectItem.DrillPlanDate,
                                AccidentScene = projectItem.AccidentScene,
                                ExerciseWay = projectItem.ExerciseWay
                            };
                            items.Add(item);
                        }
                    }
                    else
                    {
                        //增加明细集合
                        for (int i = 0; i < 100; i++)
                        {
                            i += 10;
                            Model.Information_DrillPlanHalfYearReportItem newItem = new Model.Information_DrillPlanHalfYearReportItem
                            {
                                DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem)),
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

        #region 保存
        private void Save(string type)
        {
            if (this.ddlUnitId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport = new Model.Information_DrillPlanHalfYearReport();

            if (this.ddlUnitId.SelectedValue != "null")
            {
                drillPlanHalfYearReport.UnitId = this.ddlUnitId.SelectedValue;
            }
            drillPlanHalfYearReport.CompileMan = this.CurrUser.UserName;
            drillPlanHalfYearReport.Telephone = this.txtTel.Text.Trim();
            drillPlanHalfYearReport.CompileDate = DateTime.Now;
            if (this.ddlYearId.SelectedValue != BLL.Const._Null)
            {
                drillPlanHalfYearReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
            }
            if (this.ddlHalfYearId.SelectedValue != BLL.Const._Null)
            {
                drillPlanHalfYearReport.HalfYearId = Funs.GetNewIntOrZero(this.ddlHalfYearId.SelectedValue);
            }
            if (string.IsNullOrEmpty(this.DrillPlanHalfYearReportId))
            {
                var drill = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdDate(this.ddlUnitId.SelectedValue, Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue), Funs.GetNewIntOrZero(this.ddlHalfYearId.SelectedValue));
                if (drill != null)
                {
                    ShowNotify("本单位本月报表已存在，不能重复编制", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    this.DrillPlanHalfYearReportId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReport));
                    drillPlanHalfYearReport.DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId;
                    drillPlanHalfYearReport.UpState = BLL.Const.UpState_2;
                    drillPlanHalfYearReport.HandleMan = this.CurrUser.UserId;
                    drillPlanHalfYearReport.HandleState = BLL.Const.HandleState_1;
                    BLL.DrillPlanHalfYearReportService.AddDrillPlanHalfYearReport(drillPlanHalfYearReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, drillPlanHalfYearReport.YearId.ToString() + "-" + drillPlanHalfYearReport.HalfYearId.ToString(),
                        drillPlanHalfYearReport.DrillPlanHalfYearReportId, BLL.Const.DrillPlanHalfYearReportMenuId, BLL.Const.BtnAdd);
                }
            }
            else
            {
                var oldReport = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.DrillPlanHalfYearReportId);
                if (oldReport != null)
                {
                    drillPlanHalfYearReport.HandleMan = oldReport.HandleMan;
                    drillPlanHalfYearReport.HandleState = oldReport.HandleState;
                }
                drillPlanHalfYearReport.DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId;
                drillPlanHalfYearReport.UpState = BLL.Const.UpState_2;
                BLL.DrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(drillPlanHalfYearReport);
                BLL.LogService.AddSys_Log(this.CurrUser, drillPlanHalfYearReport.YearId.ToString() + "-" + drillPlanHalfYearReport.HalfYearId.ToString(),
                        drillPlanHalfYearReport.DrillPlanHalfYearReportId, BLL.Const.DrillPlanHalfYearReportMenuId, BLL.Const.BtnModify);
                BLL.DrillPlanHalfYearReportItemService.DeleteDrillPlanHalfYearReportItemList(drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            }
            GetItems(drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.DrillPlanName))
                {
                    BLL.DrillPlanHalfYearReportItemService.AddDrillPlanHalfYearReportItem(item);
                }
            }
            if (type == "updata")     //保存并上报
            {
                Update(drillPlanHalfYearReport.DrillPlanHalfYearReportId);
            }
            if (type == "submit")
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ReportSubmit.aspx?Type=DrillPlanHalfYearReport&Id={0}", drillPlanHalfYearReport.DrillPlanHalfYearReportId, "编辑 - ")));
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        private void GetItems(string drillPlanHalfYearReportId)
        {           
            items.Clear();
            int i = 10;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.Information_DrillPlanHalfYearReportItem item = new Model.Information_DrillPlanHalfYearReportItem();
                if (values["DrillPlanHalfYearReportItemId"].ToString() != "")
                {
                    item.DrillPlanHalfYearReportItemId = values.Value<string>("DrillPlanHalfYearReportItemId");
                }
                item.DrillPlanHalfYearReportId = drillPlanHalfYearReportId;
                item.SortIndex = i;
                if (values["DrillPlanName"].ToString() != "")
                {
                    item.DrillPlanName = values.Value<string>("DrillPlanName");
                }
                if (values["OrganizationUnit"].ToString() != "")
                {
                    item.OrganizationUnit = values.Value<string>("OrganizationUnit");
                }
                if (values["DrillPlanDate"].ToString() != "")
                {
                    item.DrillPlanDate = values.Value<string>("DrillPlanDate");
                }
                if (values["AccidentScene"].ToString() != "")
                {
                    item.AccidentScene = values.Value<string>("AccidentScene");
                }
                if (values["ExerciseWay"].ToString() != "")
                {
                    item.ExerciseWay = values.Value<string>("ExerciseWay");
                }
                items.Add(item);
                i += 10;
            }
        }

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

        #endregion

        #region 同步数据
        private void Update(string drillPlanHalfYearReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertInformation_DrillPlanHalfYearReportTableCompleted += new EventHandler<HSSEService.DataInsertInformation_DrillPlanHalfYearReportTableCompletedEventArgs>(poxy_DataInsertInformation_DrillPlanHalfYearReportTableCompleted);
            var report = from x in Funs.DB.Information_DrillPlanHalfYearReport
                         where x.DrillPlanHalfYearReportId == drillPlanHalfYearReportId && x.UpState == BLL.Const.UpState_2
                         select new BLL.HSSEService.Information_DrillPlanHalfYearReport
                         {
                             DrillPlanHalfYearReportId = x.DrillPlanHalfYearReportId,
                             UnitId = x.UnitId,
                             CompileMan = x.CompileMan,
                             CompileDate = x.CompileDate,
                             YearId = x.YearId,
                             HalfYearId = x.HalfYearId,
                             Telephone = x.Telephone,
                         };

            var reportItem = from x in Funs.DB.Information_DrillPlanHalfYearReportItem
                             where x.DrillPlanHalfYearReportId == drillPlanHalfYearReportId
                             select new BLL.HSSEService.Information_DrillPlanHalfYearReportItem
                             {
                                 DrillPlanHalfYearReportItemId = x.DrillPlanHalfYearReportItemId,
                                 DrillPlanHalfYearReportId = x.DrillPlanHalfYearReportId,
                                 DrillPlanName = x.DrillPlanName,
                                 OrganizationUnit = x.OrganizationUnit,
                                 DrillPlanDate = x.DrillPlanDate,
                                 AccidentScene = x.AccidentScene,
                                 ExerciseWay = x.ExerciseWay,
                                 SortIndex = x.SortIndex,
                             };
            poxy.DataInsertInformation_DrillPlanHalfYearReportTableAsync(report.ToList(), reportItem.ToList());
        }

        #region 应急演练工作计划半年报表
        /// <summary>
        /// 应急演练工作计划半年报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_DrillPlanHalfYearReportTableCompleted(object sender, HSSEService.DataInsertInformation_DrillPlanHalfYearReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.DrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(report);
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillPlanHalfYearReportMenuId, item);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_5 && x.YearId == report.YearId.ToString() && x.HalfYearId == report.HalfYearId.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急演练工作计划半年报表】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.DrillPlanHalfYearReportMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【应急演练工作计划半年报表】上传到服务器失败；", null, BLL.Const.DrillPlanHalfYearReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
        #endregion

        #region Grid1行点击事件
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            GetItems(string.Empty);
            if (e.CommandName == "Add")
            {
                Model.Information_DrillPlanHalfYearReportItem oldItem = items.FirstOrDefault(x => x.DrillPlanHalfYearReportItemId == rowID);
                Model.Information_DrillPlanHalfYearReportItem newItem = new Model.Information_DrillPlanHalfYearReportItem
                {
                    DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem))
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
                foreach (var item in items)
                {
                    if (item.DrillPlanHalfYearReportItemId == rowID)
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

        #region 获取明细
        //private void GetNewItems(string unitId)
        //{
        //    //增加明细集合
        //    var projects = BLL.ProjectService.GetProjectDropDownList();
        //    int i = 10;
        //    foreach (var p in projects)
        //    {
        //        Model.Information_DrillPlanHalfYearReportItem item = new Model.Information_DrillPlanHalfYearReportItem();
        //        item.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //        item.DrillPlanName = p.ProjectName;
        //        item.SortIndex = i;
        //        items.Add(item);
        //        i += 10;
        //    } 
        //i += 10;
        //Model.Information_DrillPlanHalfYearReportItem newItem1 = new Model.Information_DrillPlanHalfYearReportItem();
        //newItem1.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //newItem1.SortIndex = i;
        //items.Add(newItem1);
        //i += 10;
        //Model.Information_DrillPlanHalfYearReportItem newItem2 = new Model.Information_DrillPlanHalfYearReportItem();
        //newItem2.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //newItem2.SortIndex = i;
        //items.Add(newItem2);
        //i += 10;
        //Model.Information_DrillPlanHalfYearReportItem newItem3 = new Model.Information_DrillPlanHalfYearReportItem();
        //newItem3.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //newItem3.SortIndex = i;
        //items.Add(newItem3);
        //i += 10;
        //Model.Information_DrillPlanHalfYearReportItem newItem4 = new Model.Information_DrillPlanHalfYearReportItem();
        //newItem4.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //newItem4.SortIndex = i;
        //items.Add(newItem4);
        //i += 10;
        //Model.Information_DrillPlanHalfYearReportItem newItem5 = new Model.Information_DrillPlanHalfYearReportItem();
        //newItem5.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem));
        //newItem5.SortIndex = i;
        //items.Add(newItem5);
        //}
        #endregion

        #region 单位下拉选择事件
        /// <summary>
        /// 单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //items.Clear();
            //if (ddlUnitId.SelectedValue != BLL.Const._Null)
            //{
            //    GetNewItems(ddlUnitId.SelectedValue);
            //}
            //Grid1.DataSource = items;
            //Grid1.DataBind();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DrillPlanHalfYearReportMenuId);
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

        #region 关闭办理流程窗口
        /// <summary>
        /// 关闭办理流程窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.DrillPlanHalfYearReportId);
            if (drillPlanHalfYearReport != null)
            {
                if (drillPlanHalfYearReport.HandleMan == this.CurrUser.UserId)
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
        }
        #endregion

        #region 复制上半年数据
        /// <summary>
        /// 复制上半年数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            int lastYear = 0, lastHalfYear = 0;
            int year = Convert.ToInt32(this.ddlYearId.SelectedValue);
            int halfYear = Convert.ToInt32(this.ddlHalfYearId.SelectedValue);
            if (halfYear == 1)
            {
                lastYear = year - 1;
                lastHalfYear = 2;
            }
            else
            {
                lastYear = year;
                lastHalfYear = halfYear - 1;
            }
            Model.Information_DrillPlanHalfYearReport drillPlanHalfYearReport = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(this.ddlUnitId.SelectedValue, lastYear, lastHalfYear);
            if (drillPlanHalfYearReport != null)
            {
                Model.Information_DrillPlanHalfYearReport newDrillPlanHalfYearReport = new Model.Information_DrillPlanHalfYearReport();
                this.DrillPlanHalfYearReportId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReport));
                newDrillPlanHalfYearReport.DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId;
                newDrillPlanHalfYearReport.UnitId = this.ddlUnitId.SelectedValue;
                newDrillPlanHalfYearReport.CompileMan = this.CurrUser.UserName;
                newDrillPlanHalfYearReport.CompileDate = DateTime.Now;
                newDrillPlanHalfYearReport.YearId = Funs.GetNewIntOrZero(this.ddlYearId.SelectedValue);
                newDrillPlanHalfYearReport.HalfYearId = Funs.GetNewIntOrZero(this.ddlHalfYearId.SelectedValue);
                newDrillPlanHalfYearReport.Telephone = drillPlanHalfYearReport.Telephone;
                newDrillPlanHalfYearReport.UpState = BLL.Const.UpState_2;
                newDrillPlanHalfYearReport.HandleMan = this.CurrUser.UserId;
                newDrillPlanHalfYearReport.HandleState = BLL.Const.HandleState_1;
                BLL.DrillPlanHalfYearReportService.AddDrillPlanHalfYearReport(newDrillPlanHalfYearReport);

                items = BLL.DrillPlanHalfYearReportItemService.GetDrillPlanHalfYearReportItemList(drillPlanHalfYearReport.DrillPlanHalfYearReportId);
                if (items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        Model.Information_DrillPlanHalfYearReportItem newItem = new Model.Information_DrillPlanHalfYearReportItem
                        {
                            DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.Information_DrillPlanHalfYearReportItem)),
                            DrillPlanHalfYearReportId = this.DrillPlanHalfYearReportId,
                            DrillPlanName = item.DrillPlanName,
                            OrganizationUnit = item.OrganizationUnit,
                            DrillPlanDate = item.DrillPlanDate,
                            AccidentScene = item.AccidentScene,
                            ExerciseWay = item.ExerciseWay,
                            SortIndex = item.SortIndex
                        };
                        BLL.DrillPlanHalfYearReportItemService.AddDrillPlanHalfYearReportItem(newItem);
                    }
                }
                GetValues(newDrillPlanHalfYearReport.DrillPlanHalfYearReportId);
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="drillPlanHalfYearReportId"></param>
        private void GetValues(string drillPlanHalfYearReportId)
        {
            var drill = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(drillPlanHalfYearReportId);
            if (drill != null)
            {
                if (!string.IsNullOrEmpty(drill.UnitId.Trim()))
                {
                    this.ddlUnitId.SelectedValue = drill.UnitId.Trim();
                }
                if (drill.YearId.HasValue)
                {
                    this.ddlYearId.SelectedValue = drill.YearId.ToString();
                }
                if (drill.HalfYearId.HasValue)
                {
                    this.ddlHalfYearId.SelectedValue = drill.HalfYearId.ToString();
                }
                if (!string.IsNullOrEmpty(drill.CompileMan))
                {
                    this.txtCompileMan.Text = drill.CompileMan;
                }
                this.txtTel.Text = drill.Telephone;
                if (drill.CompileDate != null)
                {
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", drill.CompileDate);
                }

                items = BLL.DrillPlanHalfYearReportItemService.GetDrillPlanHalfYearReportItemList(drillPlanHalfYearReportId);
                this.Grid1.DataSource = items;
                this.Grid1.DataBind();
            }
        }
        #endregion
    }
}