namespace FineUIPro.Web.Information
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using BLL;
    using Newtonsoft.Json.Linq;

    public partial class AnalyseResource : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                ///判断是否集团公司
                if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId))
                {
                    this.drpUnit.Enabled = false;
                }
             
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, false);
                this.drpUnit.SelectedValue = this.CurrUser.UnitId; ///当前人单位 
                this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddMonths(-1));
                this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                // 绑定表格
                //BindGrid();
            }            
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<string> unitValues = new List<string>();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                foreach (ListItem item in this.drpUnit.SelectedItemArray)
                {
                    unitValues.Add(item.Value);                  
                }
            }

            List<Model.SpResourceCollection> resourceCollections = BLL.AnalyseResourceService.GetListResourceCollection(unitValues, this.txtUserName.Text.Trim(), Funs.GetNewDateTime(this.txtStarTime.Text.Trim()), Funs.GetNewDateTime(this.txtEndTime.Text.Trim()));          
            DataTable tb = this.GetPagedDataTable(Grid1, resourceCollections);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            this.OutputSummaryData(resourceCollections);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData(List<Model.SpResourceCollection> resourceCollections)
        {
            int TotalCount = 0;
            int TotalUsedCount = 0;
            string TotalUsedRate = "0%";
            int LawRegulationCount = 0;
            int HSSEStandardListCount = 0;
            int RulesRegulationsCount = 0;
            int ManageRuleCount = 0;
            int TrainDBCount = 0;
            int TrainTestDBCount = 0;
            int AccidentCaseCount = 0;
            int KnowledgeDBCount = 0;
            int HazardListCount = 0;
            int RectifyCount = 0;          
            int HAZOPCount = 0;
            int AppraiseCount = 0;
            int ExpertCount = 0;
            int EmergencyCount = 0;
            int SpecialSchemeCount = 0;

            foreach (var item in resourceCollections)
            {
                TotalCount += item.TotalCount;
                TotalUsedCount += item.TotalUsedCount;
                LawRegulationCount += item.LawRegulationCount;
                HSSEStandardListCount += item.HSSEStandardListCount;
                RulesRegulationsCount += item.RulesRegulationsCount;
                ManageRuleCount += item.ManageRuleCount;
                TrainDBCount += item.TrainDBCount;
                TrainTestDBCount += item.TrainTestDBCount;
                AccidentCaseCount += item.AccidentCaseCount;
                KnowledgeDBCount += item.KnowledgeDBCount;
                HazardListCount += item.HazardListCount;
                RectifyCount += item.RectifyCount;               
                HAZOPCount += item.HAZOPCount;
                AppraiseCount += item.AppraiseCount;
                ExpertCount += item.ExpertCount;
                EmergencyCount += item.EmergencyCount;
                SpecialSchemeCount += item.SpecialSchemeCount;              
            }

            string rate = string.Empty;
            if (TotalCount > 0)
            {
                decimal totalUsedRate = Convert.ToDecimal(TotalUsedCount) / Convert.ToDecimal(TotalCount);
                totalUsedRate = Math.Round(totalUsedRate * 100, 2, MidpointRounding.AwayFromZero);
                if (totalUsedRate == 1)
                {
                    rate = "100.00";
                }
                else
                {
                    rate = totalUsedRate.ToString();
                }
            }
            else
            {
                rate = "0";
            }
            TotalUsedRate = rate + "%";

            JObject summary = new JObject();
            summary.Add("UnitName", "合计：");
            summary.Add("TotalCount", TotalCount);
            summary.Add("TotalUsedCount", TotalUsedCount);
            summary.Add("TotalUsedRate", TotalUsedRate);

            summary.Add("LawRegulationCount", LawRegulationCount);
            summary.Add("HSSEStandardListCount", HSSEStandardListCount);
            summary.Add("RulesRegulationsCount", RulesRegulationsCount);
            summary.Add("ManageRuleCount", ManageRuleCount);
            summary.Add("TrainDBCount", TrainDBCount);
            summary.Add("TrainTestDBCount", TrainTestDBCount);
            summary.Add("AccidentCaseCount", AccidentCaseCount);
            summary.Add("KnowledgeDBCount", KnowledgeDBCount);
            summary.Add("HazardListCount", HazardListCount);
            summary.Add("RectifyCount", RectifyCount);            
            summary.Add("HAZOPCount", HAZOPCount);
            summary.Add("AppraiseCount", AppraiseCount);
            summary.Add("ExpertCount", ExpertCount);
            summary.Add("EmergencyCount", EmergencyCount);
            summary.Add("SpecialSchemeCount", SpecialSchemeCount);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region Grid行点击前事件
        /// <summary>
        /// 行点击事件 考虑单据查看数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
           
        }
        #endregion

        #region 分页 排序 过滤
        #region 根据表头信息过滤列表数据
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 根据表头信息过滤列表数据
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <param name="fillteredOperator"></param>
        /// <param name="fillteredObj"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, object fillteredObj, string column)
        {
            bool valid = false;
            if (column == "UnitName")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.ToString();
                if ((fillteredOperator == "equal" && sourceValue == fillteredValue)||fillteredOperator == "contain" && sourceValue.Contains(fillteredValue))
                {
                    valid = true;
                }               
            }

            if (column == "UserName")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.ToString();
                if ((fillteredOperator == "equal" && sourceValue == fillteredValue) || fillteredOperator == "contain" && sourceValue.Contains(fillteredValue))
                {
                    valid = true;
                }
            }
            return valid;
        }
        #endregion

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AnalyseResourceMenuId, button);
        }
        #endregion

        #region 清空
        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_ClearIconClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
            {
                this.drpUnit.SelectedValue = this.CurrUser.UnitId;
            }
            else
            {
                this.drpUnit.SelectedIndex = 0;
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}