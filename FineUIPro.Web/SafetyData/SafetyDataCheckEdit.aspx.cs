using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using Newtonsoft.Json.Linq;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataCheckEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyDataCheckId
        {
            get
            {
                return (string)ViewState["SafetyDataCheckId"];
            }
            set
            {
                ViewState["SafetyDataCheckId"] = value;
            }
        }      
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.GetButtonPower();
                BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.CurrUser.LoginProjectId, true);
                this.SafetyDataCheckId = Request.Params["SafetyDataCheckId"];
                if (!string.IsNullOrEmpty(this.SafetyDataCheckId))
                {
                    Model.SafetyData_SafetyDataCheck SafetyDataCheck = BLL.SafetyDataCheckService.GetSafetyDataCheckById(this.SafetyDataCheckId);
                    if (SafetyDataCheck != null)
                    {
                        this.txtCode.Text = SafetyDataCheck.Code;
                        this.txtTitle.Text = SafetyDataCheck.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.CompileDate);
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.EndDate);
                        this.drpCompileMan.SelectedValue = SafetyDataCheck.CompileMan;
                    }
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-3));
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }

                this.ProjectDataBind(); ///加载树
            }
        }
        #endregion

        /// <summary>
        /// 选择项目事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrEmpty(this.SafetyDataCheckId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
                       
             PageContext.RegisterStartupScript(windowProject.GetShowReference(String.Format("ShowSafetyDataCheckProjects.aspx?SafetyDataCheckId={0}", this.SafetyDataCheckId), "选择项目", 900, 450));       
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void ProjectDataBind()
        {
            this.tvProject.Nodes.Clear();
            this.tvProject.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "企业考核项目",
                Expanded = true,
                NodeID = "0"
            };//定义根节点
            this.tvProject.Nodes.Add(rootNode);
            if (!string.IsNullOrEmpty(this.SafetyDataCheckId))
            {
                var projects = from x in Funs.DB.Base_Project
                               join y in Funs.DB.SafetyData_SafetyDataCheckProject on x.ProjectId equals y.ProjectId
                               where y.SafetyDataCheckId == this.SafetyDataCheckId
                               orderby x.ProjectCode descending
                               select x;               
                foreach (var item in projects)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.ProjectCode + "：" + item.ProjectName,
                        NodeID = item.ProjectId,
                        EnableClickEvent = true
                    };
                    rootNode.Nodes.Add(newNode);
                }
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvProject_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 右键删除树节点
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTreeDel_Click(object sender, EventArgs e)
        {
            if (this.tvProject.SelectedNode != null)
            {
                BLL.LogService.AddLogCode(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除企业管理资料信息", this.tvProject.SelectedNode.Text);
                BLL.SafetyDataCheckItemService.DeleteSafetyDataCheckProjectByProjectId(this.tvProject.SelectedNodeID);
                this.ProjectDataBind();
                this.BindGrid();
                Alert.ShowInTop("删除成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请选择树节点！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 绑定明细列表数据
        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvProject.SelectedNodeID))
            {
                List<SqlParameter> listStr = new List<SqlParameter>();

                string strSql = @"SELECT item.SafetyDataCheckItemId,item.SafetyDataCheckProjectId,item.SafetyDataId,item.StartDate,item.EndDate,item.ReminderDate,item.SubmitDate,item.ShouldScore,item.RealScore,SafetyData.Title AS SafetyDataTitle,SafetyData.Code AS SafetyDataCode,item.Remark,item.CheckDate"
                              + @" FROM dbo.SafetyData_SafetyDataCheckItem AS item"
                              + @" LEFT JOIN DBO.SafetyData_SafetyData AS SafetyData ON item.SafetyDataId =SafetyData.SafetyDataId"
                              + @" LEFT JOIN DBO.SafetyData_SafetyDataCheckProject AS checkProject ON item.SafetyDataCheckProjectId =checkProject.SafetyDataCheckProjectId"
                              + @" WHERE checkProject.ProjectId =@ProjectId AND checkProject.SafetyDataCheckId = @SafetyDataCheckId";
               
                listStr.Add(new SqlParameter("@ProjectId", this.tvProject.SelectedNodeID));
                listStr.Add(new SqlParameter("@SafetyDataCheckId", this.SafetyDataCheckId));
                if (!string.IsNullOrEmpty(this.txtSTitle.Text.Trim()))
                {
                    strSql += " AND SafetyData.Title LIKE @Title";
                    listStr.Add(new SqlParameter("@Title", "%" + this.txtSTitle.Text.Trim() + "%"));
                }

                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                this.OutputSummaryData(tb); ///取合计值
                Grid1.DataSource = table;
                Grid1.DataBind();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string safetyDataCheckItemId = Grid1.Rows[i].DataKeys[0].ToString();
                    var safetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(safetyDataCheckItemId); ///考核明细
                    if (safetyDataCheckItem != null)
                    {
                        if (safetyDataCheckItem.SubmitDate.HasValue) ////已提交
                        {
                            if (safetyDataCheckItem.EndDate < safetyDataCheckItem.SubmitDate)  ///过期提交
                            {
                                Grid1.Rows[i].RowCssClass = "Purple";
                            }
                            else
                            {
                                Grid1.Rows[i].RowCssClass = "Green";
                            }
                        }
                        else  ///未提交
                         {
                             if (safetyDataCheckItem.EndDate >= System.DateTime.Now)  ///未到结束时间
                             {
                                 if (safetyDataCheckItem.ReminderDate.HasValue && safetyDataCheckItem.ReminderDate.Value.AddDays(7) >= System.DateTime.Now)
                                 {
                                     Grid1.Rows[i].RowCssClass = "Yellow";
                                 }
                             }
                             else
                             {
                                 Grid1.Rows[i].RowCssClass = "Red"; ///超期未提交
                             }
                        }                       
                    }
                }
            }
        }
        #endregion

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData(DataTable tb)
        {
            decimal ShouldScoreT = 0;//应得分
            decimal RealScore = 0;//实际得分          
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ShouldScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["ShouldScore"].ToString());
                RealScore += Funs.GetNewDecimalOrZero(tb.Rows[i]["RealScore"].ToString());
            }

            JObject summary = new JObject();
            summary.Add("SafetyDataTitle", "合计：");
            summary.Add("ShouldScore", ShouldScoreT);
            summary.Add("RealScore", RealScore);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region gv排序翻页
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 增加、修改、删除企业安全管理资料明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVNew_Click(object sender, EventArgs e)
        {
            if (tvProject.SelectedNode != null)
            {
                var safetyDataCheckProject = BLL.SafetyDataCheckItemService.GetSafetyDataCheckProjectByProjectIdSafetyDataCheckId(this.tvProject.SelectedNodeID, this.SafetyDataCheckId);
                if (safetyDataCheckProject != null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowSafetyData.aspx?SafetyDataCheckProjectId={0}", safetyDataCheckProject.SafetyDataCheckProjectId, "新增 - ")));
                }
                else
                {
                    Alert.ShowInTop("请选择末级节点添加！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop("请选择末级节点！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVModify_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
                
        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }
            
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataCheckEditEdit.aspx?SafetyDataCheckItemId={0}", Grid1.SelectedRowID), "修改明细", 800, 300));       
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    var item = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(Grid1.DataKeys[rowIndex][0].ToString());
                    if (item != null)
                    {
                        BLL.SafetyDataCheckItemService.DeleteSafetyDataCheckItemBySafetyDataCheckItemId(item.SafetyDataCheckItemId);
                        BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除安全资料考核明细信息");
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void windowProject_Close(object sender, WindowCloseEventArgs e)
        {            
            this.ProjectDataBind(); ///加载树
            this.BindGrid();                     
        }
        #endregion 

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {           
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.SafetyData_SafetyDataCheck newSafetyDataCheck = new Model.SafetyData_SafetyDataCheck
            {
                Code = this.txtCode.Text.Trim(),
                Title = this.txtTitle.Text.Trim(),
                StartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim()),
                EndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim()),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newSafetyDataCheck.CompileMan = this.drpCompileMan.SelectedValue;
            }

            if (!string.IsNullOrEmpty(this.SafetyDataCheckId))
            {
                newSafetyDataCheck.SafetyDataCheckId = this.SafetyDataCheckId;
                BLL.SafetyDataCheckService.UpdateSafetyDataCheck(newSafetyDataCheck);
                BLL.LogService.AddLogCode(string.Empty, this.CurrUser.UserId, "修改企业安全管理资料考核", newSafetyDataCheck.Code);
            }
            else
            {
                this.SafetyDataCheckId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheck));
                newSafetyDataCheck.SafetyDataCheckId = this.SafetyDataCheckId;
                BLL.SafetyDataCheckService.AddSafetyDataCheck(newSafetyDataCheck);
                BLL.LogService.AddLogCode(string.Empty, this.CurrUser.UserId, "添加企业安全管理资料考核", newSafetyDataCheck.Code);
            }
        }
        #endregion           

        #region 抽取计划单内容
        /// <summary>
        /// 抽取计划单内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExtract_Click(object sender, EventArgs e)
        {
            DateTime? startDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
            DateTime? endDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
            if (startDate.HasValue && endDate.HasValue)
            {
                this.SaveData(BLL.Const.BtnSave);
                var safetyDataPlan = from x in Funs.DB.SafetyData_SafetyDataPlan
                                     where x.RealStartDate >= startDate && x.RealEndDate <=endDate
                                     select x;
                if (safetyDataPlan.Count() > 0)  ///存在考核计划
                {                  
                    foreach (var itemPlan in safetyDataPlan)
                    {
                        string safetyDataCheckProjectId = string.Empty; ///考核项目表
                        var safetyDataCheckProject = Funs.DB.SafetyData_SafetyDataCheckProject.FirstOrDefault(x => x.SafetyDataCheckId == this.SafetyDataCheckId && x.ProjectId == itemPlan.ProjectId);
                        if (safetyDataCheckProject != null)
                        {
                            safetyDataCheckProjectId = safetyDataCheckProject.SafetyDataCheckProjectId;
                        }
                        else
                        {
                            ////增加考核项目表
                            Model.SafetyData_SafetyDataCheckProject newSafetyDataCheckProject = new Model.SafetyData_SafetyDataCheckProject();
                            safetyDataCheckProjectId = newSafetyDataCheckProject.SafetyDataCheckProjectId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckProject));
                            newSafetyDataCheckProject.SafetyDataCheckId = this.SafetyDataCheckId;
                            newSafetyDataCheckProject.ProjectId = itemPlan.ProjectId;
                            BLL.SafetyDataCheckItemService.AddSafetyDataCheckProject(newSafetyDataCheckProject);
                        }
                        ///验证当前考核明细是否已存在
                        var item = Funs.DB.SafetyData_SafetyDataCheckItem.FirstOrDefault(x => x.SafetyDataCheckProjectId == safetyDataCheckProjectId && x.SafetyDataId == itemPlan.SafetyDataId && x.StartDate == itemPlan.RealStartDate && x.EndDate == itemPlan.RealEndDate);
                        if (item == null)
                        {
                            Model.SafetyData_SafetyDataCheckItem newSafetyDataCheckItem = new Model.SafetyData_SafetyDataCheckItem
                            {
                                SafetyDataCheckItemId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckItem)),
                                SafetyDataCheckProjectId = safetyDataCheckProjectId,
                                SafetyDataId = itemPlan.SafetyDataId,
                                CheckDate = itemPlan.CheckDate,
                                StartDate = itemPlan.RealStartDate,
                                EndDate = itemPlan.RealEndDate
                            };
                            var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(newSafetyDataCheckItem.SafetyDataId);
                            if (safetyData != null)
                            {
                                newSafetyDataCheckItem.ShouldScore = safetyData.Score;
                                newSafetyDataCheckItem.Remark = safetyData.Remark;
                            }
                            newSafetyDataCheckItem.RealScore = 0;
                            newSafetyDataCheckItem.SafetyDataPlanId = itemPlan.SafetyDataPlanId;
                            BLL.SafetyDataCheckItemService.AddSafetyDataCheckItem(newSafetyDataCheckItem);
                        }
                    }
                }
                this.ProjectDataBind(); ///加载树
                this.BindGrid();
            }
        }
        #endregion

        #region 单项目抽取计划单内容
        /// <summary>
        /// 单项目抽取计划单内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOne_Click(object sender, EventArgs e)
        {
            var project = BLL.ProjectService.GetProjectByProjectId(this.tvProject.SelectedNodeID);
            if (project == null)
            {
                Alert.ShowInTop("请选择要抽取的项目！", MessageBoxIcon.Warning);
                return;
            }

            DateTime? startDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim());
            DateTime? endDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim());
            if (startDate.HasValue && endDate.HasValue)
            {
                this.SaveData(BLL.Const.BtnSave);
                var safetyDataPlan = from x in Funs.DB.SafetyData_SafetyDataPlan
                                     where x.ProjectId == project.ProjectId && x.RealStartDate >= startDate && x.RealEndDate <=endDate
                                     select x;
                if (safetyDataPlan.Count() > 0)  ///存在考核计划
                {                  
                    foreach (var itemPlan in safetyDataPlan)
                    {
                        string safetyDataCheckProjectId = string.Empty; ///考核项目表
                        var safetyDataCheckProject = Funs.DB.SafetyData_SafetyDataCheckProject.FirstOrDefault(x => x.SafetyDataCheckId == this.SafetyDataCheckId && x.ProjectId == itemPlan.ProjectId);
                        if (safetyDataCheckProject != null)
                        {
                            safetyDataCheckProjectId = safetyDataCheckProject.SafetyDataCheckProjectId;
                        }
                        else
                        {
                            ////增加考核项目表
                            Model.SafetyData_SafetyDataCheckProject newSafetyDataCheckProject = new Model.SafetyData_SafetyDataCheckProject();
                            safetyDataCheckProjectId = newSafetyDataCheckProject.SafetyDataCheckProjectId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckProject));
                            newSafetyDataCheckProject.SafetyDataCheckId = this.SafetyDataCheckId;
                            newSafetyDataCheckProject.ProjectId = itemPlan.ProjectId;
                            BLL.SafetyDataCheckItemService.AddSafetyDataCheckProject(newSafetyDataCheckProject);
                        }
                        ///验证当前考核明细是否已存在
                        var item = Funs.DB.SafetyData_SafetyDataCheckItem.FirstOrDefault(x => x.SafetyDataCheckProjectId == safetyDataCheckProjectId && x.SafetyDataId == itemPlan.SafetyDataId && x.StartDate == itemPlan.RealStartDate && x.EndDate == itemPlan.RealEndDate);
                        if (item == null)
                        {
                            Model.SafetyData_SafetyDataCheckItem newSafetyDataCheckItem = new Model.SafetyData_SafetyDataCheckItem
                            {
                                SafetyDataCheckItemId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataCheckItem)),
                                SafetyDataCheckProjectId = safetyDataCheckProjectId,
                                SafetyDataId = itemPlan.SafetyDataId,
                                CheckDate = itemPlan.CheckDate,
                                StartDate = itemPlan.RealStartDate,
                                EndDate = itemPlan.RealEndDate
                            };
                            var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(newSafetyDataCheckItem.SafetyDataId);
                            if (safetyData != null)
                            {
                                newSafetyDataCheckItem.ShouldScore = safetyData.Score;
                                newSafetyDataCheckItem.Remark = safetyData.Remark;
                            }
                            newSafetyDataCheckItem.RealScore = 0;
                            newSafetyDataCheckItem.SafetyDataPlanId = itemPlan.SafetyDataPlanId;
                            BLL.SafetyDataCheckItemService.AddSafetyDataCheckItem(newSafetyDataCheckItem);
                        }
                    }
                }
                
                this.BindGrid();
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerSafetyDataCheckMenuId);
            if (buttonList.Count() > 0 && buttonList.Contains(BLL.Const.BtnSave))
            {
                this.btnExtract.Hidden = false;
                this.btnSave.Hidden = false;
                this.btnSelect.Hidden = false;
                this.btnOne.Hidden = false;
                this.btnGVNew.Hidden = false;
                this.btnGVModify.Hidden = false;
                this.btnGVDel.Hidden = false;
                
            }
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全资料考核表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 50000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTime")
                    {
                        html = (row.FindControl("lblWorkTime") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTimeYear")
                    {
                        html = (row.FindControl("lblWorkTimeYear") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTotal")
                    {
                        html = (row.FindControl("lblTotal") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion
    }
}