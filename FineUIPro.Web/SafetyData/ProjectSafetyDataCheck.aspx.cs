using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataCheck : PageBase
    {
        #region 自定义项
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

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["ProjectId"]))
                {
                    this.ProjectId = Request.Params["ProjectId"];
                }

                if (this.ProjectId != this.CurrUser.LoginProjectId)
                {
                    this.btnMenuNew.Hidden = true;
                }

                int ThisMonth = DateTime.Now.Month;
                int FirstMonthOfSeason = ThisMonth - (ThisMonth % 3 == 0 ? 3 : (ThisMonth % 3)) + 1;
                DateTime date1 = new DateTime(DateTime.Now.AddMonths(FirstMonthOfSeason - ThisMonth).Year, DateTime.Now.AddMonths(FirstMonthOfSeason - ThisMonth).Month, 1).AddMonths(-1);
                this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", date1);
                this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(3).AddDays(5));

                if (date1.AddMonths(3).AddDays(5) < DateTime.Now)
                {
                    this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(3));
                    this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(6).AddDays(5));
                }

                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();
                this.BindGrid();          
            }
        }        

        #region GV 绑定数据
        /// <summary>
        /// GV 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT p.SafetyDataPlanId,p.ProjectId,p.SafetyDataId,safetyData.Code,safetyData.Title,p.CheckDate "
                         + @",p.RealStartDate,p.RealEndDate,p.Score,p.Remark,p.ReminderDate,p.SubmitDate,p.ShouldScore,p.RealScore"
                         + @" FROM dbo.SafetyData_SafetyDataPlan AS p "
                         + @" LEFT JOIN SafetyData_SafetyData AS safetyData ON P.SafetyDataId = safetyData.SafetyDataId "
                         + @" WHERE ProjectId ='" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND (safetyData.Title LIKE @Title OR safetyData.Code LIKE @Title)";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtStarTime.Text.Trim()))
            {
                strSql += " AND p.RealEndDate >= @StarTime";
                listStr.Add(new SqlParameter("@StarTime", Funs.GetNewDateTime(this.txtStarTime.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                strSql += " AND p.RealEndDate <= @EndTime";
                listStr.Add(new SqlParameter("@EndTime", Funs.GetNewDateTime(this.txtEndTime.Text.Trim())));
            }

            if (this.drpState.SelectedValue == "1")
            {
                strSql += " AND p.SubmitDate IS NULL";
            }
            else if (this.drpState.SelectedValue == "2")
            {
                strSql += " AND p.SubmitDate IS NOT NULL";
            }
            else if (this.drpState.SelectedValue == "3")
            {
                strSql += " AND p.SubmitDate IS NULL AND p.CheckDate < @Now";
                listStr.Add(new SqlParameter("@Now", System.DateTime.Now));
            }
            else if (this.drpState.SelectedValue == "4")
            {
                strSql += " AND p.SubmitDate IS NOT NULL AND p.CheckDate < p.SubmitDate";
            }
            else if (this.drpState.SelectedValue == "5")
            {
                strSql += " AND p.SubmitDate IS NOT NULL AND p.CheckDate >= p.SubmitDate";
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            this.Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            this.OutputSummaryData(tb); ///取合计值
            this.Grid1.DataSource = table;
            this.Grid1.DataBind();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string safetyDataPlanId = Grid1.Rows[i].DataKeys[0].ToString();
                var safetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(safetyDataPlanId); ///考核计划
                if (safetyDataPlan != null)
                {
                    if (safetyDataPlan.SubmitDate.HasValue) ////已提交
                    {
                        if (safetyDataPlan.CheckDate < safetyDataPlan.SubmitDate)  ///过期提交
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
                        if (safetyDataPlan.CheckDate >= System.DateTime.Now)  ///未到结束时间
                        {
                            if (safetyDataPlan.ReminderDate.HasValue && safetyDataPlan.ReminderDate.Value <= System.DateTime.Now.AddDays(7))
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
        #endregion

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData(DataTable tb)
        {
            decimal ScoreT = 0;//计划分 
            decimal ShouldScoreT = 0;//应得分
            decimal RealScoreT = 0;//实际得分          
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["Score"].ToString());
                ShouldScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["ShouldScore"].ToString());
                RealScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["RealScore"].ToString());
            }

            JObject summary = new JObject();
            summary.Add("Title", "合计：");
            summary.Add("Score", ScoreT);
            summary.Add("ShouldScore", ShouldScoreT);
            summary.Add("RealScore", RealScoreT);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region GV 排序翻页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 企业安全管理资料上报
        /// <summary>
        /// 双击企业安全管理资料上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 企业安全管理资料上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

            var SafetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(Grid1.SelectedRowID);
            if (SafetyDataPlan != null)
            {                
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ProjectSafetyDataItemEdit.aspx?SafetyDataId={0}&SafetyDataPlanId={1}", SafetyDataPlan.SafetyDataId, SafetyDataPlan.SafetyDataPlanId, "新增 - ")));
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion 
                                
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全考核资料计划表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 10000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion

        #region 树 右键查看时间总表
        /// <summary>
        /// 树 右键查看时间总表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTreeView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataPlanItem.aspx?ProjectId={0}", this.ProjectId, "查看 - ")));
        }
        #endregion

        /// <summary>
        /// 查看项目上报资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }

            var safetyDataPlan = BLL.SafetyDataPlanService.GetSafetyDataPlanBySafetyDataPlanId(Grid1.SelectedRowID);
            if (safetyDataPlan != null && safetyDataPlan.SubmitDate.HasValue)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ProjectSafetyDataItem.aspx?SafetyDataPlanId={0}", Grid1.SelectedRowID), "修改明细", 1024, 560));
            }
            else
            {
                Alert.ShowInParent("当前资料项，项目尚未上传资料！", MessageBoxIcon.Warning);
            }
        }
    }
}