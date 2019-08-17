using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ServerCheck
{
    public partial class UpCheckReportEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string UpCheckReportId
        {
            get
            {
                return (string)ViewState["UpCheckReportId"];
            }
            set
            {
                ViewState["UpCheckReportId"] = value;
            }
        }
        private bool AppendToEnd = false;
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UpCheckReportId = Request.Params["UpCheckReportId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                var upCheckReport = BLL.UpCheckReportService.GetUpCheckReportById(this.UpCheckReportId);
                if (upCheckReport != null)
                {
                    this.txtValues1.Text = upCheckReport.Values1;
                    this.txtValues2.Text = upCheckReport.Values2;
                    this.txtValues3.Text = upCheckReport.Values3;
                    this.txtValues4.Text = upCheckReport.Values4;
                    this.txtValues5.Text = upCheckReport.Values5;
                    this.txtValues6.Text = upCheckReport.Values6;
                    this.txtValues7.Text = upCheckReport.Values7;

                    this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", upCheckReport.CheckStartTime);
                    this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", upCheckReport.CheckEndTime);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", upCheckReport.CompileDate);
                    this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", upCheckReport.AuditDate);

                    if (upCheckReport.UpState == BLL.Const.UpState_3 && this.CurrUser.UserId != BLL.Const.sysglyId)
                    {
                        this.btnSave.Hidden = true;
                        this.btnSaveUp.Hidden = true;
                        this.btnNewItem.Hidden = true;
                        this.btnNewItem2.Hidden = true;
                        this.btnDeleteItem.Hidden = true;
                        this.btnDeleteItem2.Hidden = true;
                    }
                }
                else
                {
                    this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                }
                /// 加载报告页面
                this.LoadData1();
                this.LoadData2();
            }
        }
        #endregion

        #region 标签一
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadData1()
        {            
            // 删除选中单元格的客户端脚本
            string deleteScript = GetDeleteScript();
            // 新增数据初始值
            JObject defaultObj = new JObject();
            defaultObj.Add("SortIndex", "");
            defaultObj.Add("Name", "");
            defaultObj.Add("Sex", "");
            defaultObj.Add("UnitName", "");
            defaultObj.Add("PostName", "");
            defaultObj.Add("WorkTitle", "");
            defaultObj.Add("CheckPostName", "");
            defaultObj.Add("CheckDate", string.Format("{0:yyyy-MM-dd}", System.DateTime.Now));
            if (!this.btnSave.Hidden)
            {
                defaultObj.Add("Delete1", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));
            }
            // 在第一行新增一条数据
            this.btnNewItem.OnClientClick = gvItem.GetAddNewRecordReference(defaultObj, AppendToEnd);
            // 删除选中行按钮
            this.btnDeleteItem.OnClientClick = gvItem.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
            // 绑定表格
            this.gvItemBindGrid();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript()
        {            
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, gvItem.GetDeleteSelectedRowsReference(), String.Empty);         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItem_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = gvItem.FindColumn("Delete1") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void gvItemBindGrid()
        {
            string strSql = @"SELECT UpCheckReportItemId,UpCheckReportId,SortIndex,Name,Sex,UnitName,PostName,WorkTitle,CheckPostName,CheckDate"
                + @" FROM dbo.Supervise_UpCheckReportItem "
                + @" WHERE UpCheckReportId=@UpCheckReportId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {                       
                        new SqlParameter("@UpCheckReportId",this.UpCheckReportId),
                    };

            strSql += "   ORDER BY SortIndex";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            gvItem.DataSource = tb;
            gvItem.DataBind();
        }
        #endregion
        #endregion

        #region 标签二
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadData2()
        {           
            // 删除选中单元格的客户端脚本
            string deleteScript2 = GetDeleteScript2();
            // 新增数据初始值
            JObject defaultObj2 = new JObject();
            defaultObj2.Add("SortIndex", "");
            defaultObj2.Add("SubjectObject", "");
            defaultObj2.Add("SubjectObjectInfo", "");
            defaultObj2.Add("UnitMan", "");
            defaultObj2.Add("UnitManTel", "");
            defaultObj2.Add("UnitHSSEMan", "");
            defaultObj2.Add("UnitHSSEManTel", "");
            defaultObj2.Add("CheckDate", string.Format("{0:yyyy-MM-dd}", System.DateTime.Now));
            defaultObj2.Add("RectifyCount", 0);
            defaultObj2.Add("CompRectifyCount", 0);
            defaultObj2.Add("TotalGetScore", 0);
            defaultObj2.Add("ResultLevel", "合格");
            if (!this.btnSave.Hidden)
            {
                defaultObj2.Add("Delete2", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript2, IconHelper.GetResolvedIconUrl(Icon.Delete)));
            }
            // 在第一行新增一条数据
            this.btnNewItem2.OnClientClick = gvItem2.GetAddNewRecordReference(defaultObj2, AppendToEnd);
            // 删除选中行按钮
            this.btnDeleteItem2.OnClientClick = gvItem2.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript2;
            // 绑定表格
            this.gvItem2BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript2()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, gvItem2.GetDeleteSelectedRowsReference(), String.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvItem2_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = gvItem2.FindColumn("Delete2") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript2();
        }

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void gvItem2BindGrid()
        {
            string strSql = @"SELECT UpCheckReportItem2Id,UpCheckReportId,SortIndex,SubjectObject,SubjectObjectInfo,UnitMan,UnitManTel,UnitHSSEMan,UnitHSSEManTel,CheckDate,RectifyCount,CompRectifyCount,TotalGetScore,ResultLevel"
                + @" FROM dbo.Supervise_UpCheckReportItem2 "
                + @" WHERE UpCheckReportId=@UpCheckReportId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {                       
                        new SqlParameter("@UpCheckReportId",this.UpCheckReportId),
                    };

            strSql += "   ORDER BY SortIndex";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            gvItem2.DataSource = tb;
            gvItem2.DataBind();
        }
        #endregion
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="state"></param>
        private void SaveData(string state)
        {
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null)
            {
                Model.Supervise_UpCheckReport newUpCheckReport = new Model.Supervise_UpCheckReport
                {
                    UnitId = thisUnit.UnitId,
                    Values1 = this.txtValues1.Text.Trim(),
                    Values2 = this.txtValues2.Text.Trim(),
                    Values3 = this.txtValues3.Text.Trim(),
                    Values4 = this.txtValues4.Text.Trim(),
                    Values5 = this.txtValues5.Text.Trim(),
                    Values6 = this.txtValues6.Text.Trim(),
                    Values7 = this.txtValues7.Text.Trim(),
                    UpState = state,
                    CheckStartTime = Funs.GetNewDateTime(this.txtCheckStartTime.Text),
                    CheckEndTime = Funs.GetNewDateTime(this.txtCheckEndTime.Text),
                    CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text),
                    AuditDate = Funs.GetNewDateTime(this.txtAuditDate.Text)
                };
                if (!string.IsNullOrEmpty(this.UpCheckReportId))
                {
                    newUpCheckReport.UpCheckReportId = this.UpCheckReportId;
                    BLL.UpCheckReportService.UpdateUpCheckReport(newUpCheckReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, newUpCheckReport.UpCheckReportId, BLL.Const.UpCheckReportMenuId, BLL.Const.BtnModify);
                }
                else
                {
                   this.UpCheckReportId = newUpCheckReport.UpCheckReportId = SQLHelper.GetNewID(typeof(Model.Supervise_UpCheckReport));
                    BLL.UpCheckReportService.AddUpCheckReport(newUpCheckReport);
                    BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, newUpCheckReport.UpCheckReportId, BLL.Const.UpCheckReportMenuId, BLL.Const.BtnAdd);
                }

                if (gvItem.GetModifiedData().Count > 0 && !string.IsNullOrEmpty(newUpCheckReport.UpCheckReportId))
                {
                    BLL.UpCheckReportService.DeleteUpCheckReportItemByUpCheckReportId(newUpCheckReport.UpCheckReportId);
                    JArray teamGroupData = gvItem.GetMergedData();
                    foreach (JObject teamGroupRow in teamGroupData)
                    {
                        //string status = teamGroupRow.Value<string>("status");
                        JObject values = teamGroupRow.Value<JObject>("values");
                        Model.Supervise_UpCheckReportItem newItem = new Model.Supervise_UpCheckReportItem
                        {
                            UpCheckReportItemId = SQLHelper.GetNewID(typeof(Model.Supervise_UpCheckReportItem)),
                            UpCheckReportId = newUpCheckReport.UpCheckReportId,
                            SortIndex = values.Value<string>("SortIndex"),
                            Name = values.Value<string>("Name"),
                            Sex = values.Value<string>("Sex"),
                            UnitName = values.Value<string>("UnitName"),
                            PostName = values.Value<string>("PostName"),
                            WorkTitle = values.Value<string>("WorkTitle"),
                            CheckPostName = values.Value<string>("CheckPostName"),
                            CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate"))
                        };
                        Funs.DB.Supervise_UpCheckReportItem.InsertOnSubmit(newItem);
                        Funs.DB.SubmitChanges();
                    }
                }

                if (gvItem2.GetModifiedData().Count > 0 && !string.IsNullOrEmpty(newUpCheckReport.UpCheckReportId))
                {
                    BLL.UpCheckReportService.DeleteUpCheckReportItem2ByUpCheckReportId(newUpCheckReport.UpCheckReportId);
                    JArray teamGroupData2 = gvItem2.GetMergedData();
                    foreach (JObject teamGroupRow2 in teamGroupData2)
                    {
                        //string status = teamGroupRow.Value<string>("status");
                        JObject values = teamGroupRow2.Value<JObject>("values");
                        Model.Supervise_UpCheckReportItem2 newItem2 = new Model.Supervise_UpCheckReportItem2
                        {
                            UpCheckReportItem2Id = SQLHelper.GetNewID(typeof(Model.Supervise_UpCheckReportItem2)),
                            UpCheckReportId = newUpCheckReport.UpCheckReportId,
                            SortIndex = values.Value<string>("SortIndex"),
                            SubjectObject = values.Value<string>("SubjectObject"),
                            SubjectObjectInfo = values.Value<string>("SubjectObjectInfo"),
                            UnitMan = values.Value<string>("UnitMan"),
                            UnitManTel = values.Value<string>("UnitManTel"),
                            UnitHSSEMan = values.Value<string>("UnitHSSEMan"),
                            UnitHSSEManTel = values.Value<string>("UnitHSSEManTel"),
                            CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate")),

                            RectifyCount = Funs.GetNewIntOrZero(values.Value<string>("RectifyCount")),
                            CompRectifyCount = Funs.GetNewIntOrZero(values.Value<string>("CompRectifyCount")),
                            TotalGetScore = Funs.GetNewDecimalOrZero(values.Value<string>("TotalGetScore")),
                            ResultLevel = values.Value<string>("ResultLevel")
                        };
                        Funs.DB.Supervise_UpCheckReportItem2.InsertOnSubmit(newItem2);
                        Funs.DB.SubmitChanges();
                    }
                }
            }
            else
            {
                ShowNotify("单位信息中未设置本单位！", MessageBoxIcon.Success);
                return;
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2);
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());            
        }


        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                Update(this.UpCheckReportId);//上报
            }
            ShowNotify("保存并上报完成！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 同步数据
        private void Update(string upCheckReportId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertSupervise_UpCheckReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertSupervise_UpCheckReportTableCompletedEventArgs>(poxy_DataInsertSupervise_UpCheckReportTableCompleted);
            var upCheckReport = from x in Funs.DB.Supervise_UpCheckReport
                                     where x.UpCheckReportId == upCheckReportId
                                     select new BLL.HSSEService.Supervise_UpCheckReport
                                     {
                                         UpCheckReportId = x.UpCheckReportId,
                                         UnitId = x.UnitId,
                                         CheckStartTime = x.CheckStartTime,
                                         CheckEndTime = x.CheckEndTime,
                                         Values1 = x.Values1,
                                         Values2 = x.Values2,
                                         Values3 = x.Values3,
                                         Values4 = x.Values4,
                                         Values5 = x.Values5,
                                         Values6 = x.Values6,
                                         Values7 = x.Values7,
                                         CompileDate = x.CompileDate,
                                         AuditDate = x.AuditDate,
                                     };

            var upCheckReportItem = from x in Funs.DB.Supervise_UpCheckReportItem
                                          where x.UpCheckReportId == upCheckReportId
                                          select new BLL.HSSEService.Supervise_UpCheckReportItem
                                          {
                                              UpCheckReportItemId = x.UpCheckReportItemId,
                                              UpCheckReportId = x.UpCheckReportId,
                                              SortIndex = x.SortIndex,
                                              Name = x.Name,
                                              Sex = x.Sex,
                                              UnitName = x.UnitName,
                                              PostName = x.PostName,
                                              WorkTitle = x.WorkTitle,
                                              CheckPostName = x.CheckPostName,
                                              CheckDate = x.CheckDate,
                                          };
            var upCheckReportItem2 = from x in Funs.DB.Supervise_UpCheckReportItem2
                                    where x.UpCheckReportId == upCheckReportId
                                    select new BLL.HSSEService.Supervise_UpCheckReportItem2
                                    {
                                        UpCheckReportItem2Id = x.UpCheckReportItem2Id,
                                        UpCheckReportId = x.UpCheckReportId,
                                        SortIndex = x.SortIndex,
                                        SubjectObject = x.SubjectObject,
                                        SubjectObjectInfo = x.SubjectObjectInfo,
                                        UnitMan = x.UnitMan,
                                        UnitManTel = x.UnitManTel,
                                        UnitHSSEMan = x.UnitHSSEMan,
                                        UnitHSSEManTel = x.UnitHSSEManTel,
                                        CheckDate = x.CheckDate,
                                        RectifyCount = x.RectifyCount,
                                        CompRectifyCount = x.CompRectifyCount,
                                        TotalGetScore = x.TotalGetScore,
                                        ResultLevel = x.ResultLevel,
                                    };
            poxy.DataInsertSupervise_UpCheckReportTableAsync(upCheckReport.ToList(), upCheckReportItem.ToList(), upCheckReportItem2.ToList());
        }

        #region 安全监督检查评价报告
        /// <summary>
        /// 安全监督检查评价报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertSupervise_UpCheckReportTableCompleted(object sender, BLL.HSSEService.DataInsertSupervise_UpCheckReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.UpCheckReportService.GetUpCheckReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        report.UpDateTime = System.DateTime.Now;
                        BLL.UpCheckReportService.UpdateUpCheckReport(report);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全监督检查评价报告】上传到服务器" + idList.Count.ToString() + "条数据；",string.Empty,BLL.Const.UpCheckReportMenuId,BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全监督检查评价报告】上传到服务器失败；", string.Empty, BLL.Const.UpCheckReportMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
        #endregion
    }
}