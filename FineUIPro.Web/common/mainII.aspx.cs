using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using BLL;
using Newtonsoft.Json.Linq;
using System.Linq;


namespace FineUIPro.Web.common
{
    public partial class mainII : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null)
                {
                    this.txtUnitName.Text = unit.UnitName;                  
                }
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, false);
                this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
                BLL.ConstValue.InitConstValueDropDownList(this.drpStartMonth, ConstValue.Group_0009, false);
                this.drpStartMonth.SelectedIndex = 0; ;

                BLL.ConstValue.InitConstValueDropDownList(this.drpEndMonth, ConstValue.Group_0009, false);
                this.drpEndMonth.SelectedValue = DateTime.Now.Month.ToString();
                                                                   
                /// 安全人工时
                this.SetformLeft1Values(1, DateTime.Now.Month);
                 /// 安全投入                
                this.SetformLeft2Values(1, DateTime.Now.Month);  
                /// 事故管理
                this.SetformLeft3Values(1, DateTime.Now.Month);
                ////安全工时费用率
                this.SetSafetyQuarterly(1, DateTime.Now.Month);
                ///事故率分析
                this.SetChartAccidentRate(1, DateTime.Now.Month);
                ///事故分析
                this.SetAccidentCause(1, DateTime.Now.Month);
                /// 隐患治理
                this.SetformRight1Values(1, DateTime.Now.Month);
                /// 应急演练
                this.SetformRight2Values(1, DateTime.Now.Month);

                BindGridToDoMatter("close");
            }
        }

        /// <summary>
        /// 单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int startMonth = Funs.GetNewIntOrZero(this.drpStartMonth.SelectedValue);
            int endMonth = Funs.GetNewIntOrZero(this.drpEndMonth.SelectedValue);
            this.SetformLeft1Values(startMonth, endMonth);
            this.SetformLeft2Values(startMonth, endMonth);
            this.SetformLeft3Values(startMonth, endMonth);
            this.SetformRight1Values(startMonth, endMonth);
            this.SetformRight2Values(startMonth, endMonth);

            ////安全工时费用率
            this.SetSafetyQuarterly(startMonth, endMonth);
            ///事故率分析
            this.SetChartAccidentRate(startMonth, endMonth);
            ///事故分析
            this.SetAccidentCause(startMonth, endMonth);

        }

        #region 安全人工时
        /// <summary>
        /// 安全人工时
        /// </summary>
        private void SetformLeft1Values(int startMonth,int endMonth)
        {
            this.lbSumPersonNum.Text = "0";
            this.lbTotalWorkNum.Text = "0";
            this.lbLossDayNum.Text = "0";
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var millionsMonthly = (from x in Funs.DB.Information_MillionsMonthlyReport
                                   where x.Year == year
                                   && x.Month >= startMonth && x.Month <= endMonth
                                   select x);          
            if (millionsMonthly.Count() > 0)
            {
                var millionsMonthlyReportItem = from x in Funs.DB.Information_MillionsMonthlyReportItem
                                                join y in millionsMonthly on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                                                where x.Affiliation != "本月合计"
                                                select x;
                this.lbSumPersonNum.Text = (millionsMonthlyReportItem.Sum(x => x.SumPersonNum) ?? 0).ToString();
                this.lbTotalWorkNum.Text = (millionsMonthlyReportItem.Sum(x => x.TotalWorkNum) ?? 0).ToString();
                this.lbLossDayNum.Text = (millionsMonthlyReportItem.Sum(x => x.LossDayNum) ?? 0).ToString();
                this.formLeft1.TitleToolTip = "数据来源：百万工时安全统计月报；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
            }
        }        
        #endregion

        #region 安全投入
        /// <summary>
        /// 安全投入
        /// </summary>
        private void SetformLeft2Values(int startMonth, int endMonth)
        {
            int startQuarter = Funs.GetNowQuarterlyByMonth(startMonth);
            int endQuarter = Funs.GetNowQuarterlyByMonth(endMonth);
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var safetyQuarterlyReport = (from x in Funs.DB.Information_SafetyQuarterlyReport
                                         where x.YearId == year
                                         && x.Quarters >= startQuarter && x.Quarters <=endQuarter
                                         select x);        
            this.lblProductionSafetyInTotal.Text = (safetyQuarterlyReport.Sum(x => x.ProductionSafetyInTotal) ?? 0).ToString();
            this.lblProductionInput.Text = (safetyQuarterlyReport.Sum(x => x.ProductionInput) ?? 0).ToString();
            this.formLeft2.TitleToolTip = "数据来源：安全生产数据季报；统计时间：" + this.drpYear.SelectedText + Funs.GetQuarterlyNameByMonth(startMonth) + "至" + Funs.GetQuarterlyNameByMonth(endMonth);
        }
       
        #endregion

        #region 事故管理
        /// <summary>
        /// 事故管理
        /// </summary>
        private void SetformLeft3Values(int startMonth, int endMonth)
        {
            this.lbCount5_17.Text = "0";
            this.lbcout18_23.Text = "0";
            this.lbcout24.Text = "0";
            this.lbcout25.Text = "0";
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var millionsMonthly = (from x in Funs.DB.Information_MillionsMonthlyReport
                                   where x.Year == year
                                   && x.Month >= startMonth && x.Month <=endMonth
                                   select x);          
            if (millionsMonthly.Count() > 0)
            {
                var millionsMonthlyReportItem = from x in Funs.DB.Information_MillionsMonthlyReportItem
                                                join y in millionsMonthly on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                                                where x.Affiliation != "本月合计"
                                                select x;

                int cout5 = millionsMonthlyReportItem.Sum(x => x.SeriousInjuriesNum) ?? 0;
                int cout8 = millionsMonthlyReportItem.Sum(x => x.MinorAccidentNum) ?? 0;             
                int cout11 = millionsMonthlyReportItem.Sum(x => x.OtherAccidentNum) ?? 0;
                               
                int cout14 = millionsMonthlyReportItem.Sum(x => x.RestrictedWorkPersonNum) ?? 0;
                //int cout15 = millionsMonthlyReportItem.Sum(x => x.RestrictedWorkLossHour) ?? 0;
                int cout16 = millionsMonthlyReportItem.Sum(x => x.MedicalTreatmentPersonNum) ?? 0;
                //int cout17 = millionsMonthlyReportItem.Sum(x => x.MedicalTreatmentLossHour) ?? 0;
                this.lbCount5_17.Text = (cout5 +  cout8 + cout11).ToString();

                int cout18 = millionsMonthlyReportItem.Sum(x => x.FireNum) ?? 0;
                int cout19 = millionsMonthlyReportItem.Sum(x => x.ExplosionNum) ?? 0;
                int cout20 = millionsMonthlyReportItem.Sum(x => x.TrafficNum) ?? 0;
                int cout21 = millionsMonthlyReportItem.Sum(x => x.EquipmentNum) ?? 0;
                int cout22 = millionsMonthlyReportItem.Sum(x => x.QualityNum) ?? 0;
                int cout23 = millionsMonthlyReportItem.Sum(x => x.OtherNum) ?? 0;
                this.lbcout18_23.Text = (cout18 + cout19 + cout20 + cout21 + cout22 + cout23).ToString();

                int cout24 = millionsMonthlyReportItem.Sum(x => x.FirstAidDressingsNum) ?? 0;
                this.lbcout24.Text = cout24.ToString();
                int cout25 = millionsMonthlyReportItem.Sum(x => x.AttemptedEventNum) ?? 0;
                this.lbcout25.Text = cout25.ToString();
                this.formLeft3.TitleToolTip = "数据来源：百万工时安全统计月报；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
            }
        }
        #endregion

        #region 安全工时费用率
        /// <summary>
        /// 安全工时费用率
        /// </summary>
        private void SetSafetyQuarterly(int startMonth, int endMonth)
        {
            int startQuarter = Funs.GetNowQuarterlyByMonth(startMonth);
            int endQuarter = Funs.GetNowQuarterlyByMonth(endMonth);
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var report = (from x in Funs.DB.Information_SafetyQuarterlyReport
                                         where x.YearId == year
                                         && x.Quarters >= startQuarter && x.Quarters <= endQuarter
                                         select x);
            var upReport = (from x in Funs.DB.Information_SafetyQuarterlyReport
                            where x.YearId == (year - 1)
                                         && x.Quarters >= startQuarter && x.Quarters <= endQuarter
                                         select x);                     
            ///安全工时费用率
            DataTable dt = new DataTable();
            var quarters = BLL.ConstValue.drpConstItemList(ConstValue.Group_0011).Where(x => Convert.ToInt32(x.ConstValue) >= startQuarter && Convert.ToInt32(x.ConstValue) <= endQuarter);
            dt.Columns.Add("季度", typeof(string));
            dt.Columns.Add("工时费用率", typeof(string));
            dt.Columns.Add("上年同期", typeof(string));
            foreach (var item in quarters)
            {
                DataRow row = dt.NewRow();
                Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                row["季度"] = item.ConstText;
                var itemReprot = (from x in report
                                    where x.Quarters.ToString() == item.ConstValue
                                    select x).ToList();
              
                decimal pTotal = itemReprot.Sum(x => x.ProductionSafetyInTotal) ?? 0;
                decimal tHours = itemReprot.Sum(x => x.TotalInWorkHours) ?? 0;               
                if (tHours == 0)
                {
                    row["工时费用率"] = 0.00;
                }
                else
                {
                    row["工时费用率"] = Math.Round(Convert.ToDecimal((pTotal / tHours)), 2);
                }

                var itemUpReprot = (from x in upReport
                                  where x.Quarters.ToString() == item.ConstValue
                                  select x).ToList();

                decimal pUpTotal = itemUpReprot.Sum(x => x.ProductionSafetyInTotal) ?? 0;
                decimal tUpHours = itemUpReprot.Sum(x => x.TotalInWorkHours) ?? 0;
                if (tUpHours == 0)
                {
                    row["上年同期"] = 0.00;
                }
                else
                {
                    row["上年同期"] = Math.Round(Convert.ToDecimal((pUpTotal / tUpHours)), 2);
                }

                dt.Rows.Add(row);
            }
            this.cpSafetyQuarterly.TitleToolTip = "数据来源：安全生产数据季报；统计时间：" + this.drpYear.SelectedText + Funs.GetQuarterlyNameByMonth(startMonth) + "至" + Funs.GetQuarterlyNameByMonth(endMonth);
            this.ChartSafetyQuarterly.CreateChart(BLL.ChartControlService.GetDataSourceChart(dt, "人工时费用比率", "Line", 580, 180, false));
        }
        #endregion

        #region 事故率分析
        /// <summary>
        /// 事故分析
        /// </summary>
        private void SetChartAccidentRate(int startMonth, int endMonth)
        {
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var report = (from x in Funs.DB.Information_MillionsMonthlyReport
                          where x.Year <= year && x.Year >= (year - 5)
                          select x);
            ///事故分析
            DataTable dt = new DataTable();            
            dt.Columns.Add("年度", typeof(string));
            dt.Columns.Add("事故率", typeof(string));
            dt.Columns.Add("严重率", typeof(string));
            dt.Columns.Add("可记录事故率", typeof(string));
            for (int i = -5; i <= 0; i++)
            {
                DataRow row = dt.NewRow();
                Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                row["年度"] = year + i;
                var reportItem = from x in Funs.DB.Information_MillionsMonthlyReportItem
                                 join y in report on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                                 where y.Year == year + i
                                 select x;

                int cout5 = reportItem.Sum(x => x.SeriousInjuriesNum) ?? 0;
                int cout8 = reportItem.Sum(x => x.MinorAccidentNum) ?? 0;
                int cout11 = reportItem.Sum(x => x.OtherAccidentNum) ?? 0;
                int cout14 = reportItem.Sum(x => x.RestrictedWorkPersonNum) ?? 0;
                //int cout15 = millionsMonthlyReportItem.Sum(x => x.RestrictedWorkLossHour) ?? 0;
                int cout16 = reportItem.Sum(x => x.MedicalTreatmentPersonNum) ?? 0;
                //int cout17 = millionsMonthlyReportItem.Sum(x => x.MedicalTreatmentLossHour) ?? 0;              
                int cout18 = reportItem.Sum(x => x.FireNum) ?? 0;
                int cout19 = reportItem.Sum(x => x.ExplosionNum) ?? 0;
                int cout20 = reportItem.Sum(x => x.TrafficNum) ?? 0;
                int cout21 = reportItem.Sum(x => x.EquipmentNum) ?? 0;
                int cout22 = reportItem.Sum(x => x.QualityNum) ?? 0;
                int cout23 = reportItem.Sum(x => x.OtherNum) ?? 0;               
                int cout24 = reportItem.Sum(x => x.FirstAidDressingsNum) ?? 0;             
                int cout25 = reportItem.Sum(x => x.AttemptedEventNum) ?? 0;
                ///总数
                decimal totalCount = (cout5 + cout8 + cout11 + +cout18 + cout19 + cout20 + cout21 + cout22 + cout23 + cout24 + cout25);
                ///损失工时事故
                decimal totalCount1 = (cout5 + cout8 + cout11 + cout14 + cout16);
                ///严重
                decimal totalCount2 = cout5;
                ///可记录
                decimal totalCount3 = (cout5 + cout8 + cout11);
                if (totalCount == 0)
                {
                    row["事故率"] = 0;
                    row["严重率"] = 0;
                    row["可记录事故率"] = 0;
                }
                else
                {
                    row["事故率"] = Math.Round(Convert.ToDecimal((totalCount1 / totalCount)), 2);
                    row["严重率"] =  Math.Round(Convert.ToDecimal((totalCount2 / totalCount)), 2);
                    row["可记录事故率"] = Math.Round(Convert.ToDecimal((totalCount3 / totalCount)), 2);
                }
                dt.Rows.Add(row);
            }
            this.ContentPanel1.TitleToolTip = "数据来源：百万工时安全统计月报；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
            this.ChartAccidentRate.CreateChart(BLL.ChartControlService.GetDataSourceChart(dt, "事故率", "Spline", 580, 160, false));
        }
        #endregion

        #region 事故分析
        /// <summary>
        /// 事故分析
        /// </summary>
        private void SetAccidentCause(int startMonth, int endMonth)
        {
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var report = (from x in Funs.DB.Information_AccidentCauseReport 
                                 where x.Year ==year 
                                 && x.Month >= startMonth && x.Month<=endMonth
                                 select x);
            var upReport = (from x in Funs.DB.Information_AccidentCauseReport
                            where x.Year == (year - 1)
                          && x.Month >= startMonth && x.Month <= endMonth
                          select x);
            //if (!BLL.CommonService.IsMainUnitOrAdmin(this.drpUnit.SelectedValue))
            //{
            //    report = report.Where(x => x.UnitId == this.drpUnit.SelectedValue);
            //    upReport = upReport.Where(x => x.UnitId == this.drpUnit.SelectedValue);
            //}

            var reportItem = from x in Funs.DB.Information_AccidentCauseReportItem
                             join y in report on x.AccidentCauseReportId equals y.AccidentCauseReportId
                             select x;
            var reportUpItem = from x in Funs.DB.Information_AccidentCauseReportItem
                               join y in upReport on x.AccidentCauseReportId equals y.AccidentCauseReportId
                             select x;  

            ///事故分析
            DataTable dt = new DataTable();
            var accidentType = BLL.ConstValue.drpConstItemList(ConstValue.Group_0012).Where(x => x.ConstValue != "总计");
            dt.Columns.Add("事故类别", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("上年同期数量", typeof(string));
            foreach (var item in accidentType)
            {
                DataRow row = dt.NewRow();
                Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                row["事故类别"] = item.ConstText;
                var typeAccident = (from x in reportItem
                                    where x.AccidentType == item.ConstValue
                                    select x).ToList();

                row["数量"] = typeAccident.Sum(x => x.TotalDeath) ?? 0 + typeAccident.Sum(x => x.TotalInjuries) ?? 0 + typeAccident.Sum(x => x.TotalMinorInjuries) ?? 0;

                var typeUpAccident = (from x in reportUpItem
                                    where x.AccidentType == item.ConstValue
                                    select x).ToList();

                row["上年同期数量"] = typeUpAccident.Sum(x => x.TotalDeath) ?? 0 + typeUpAccident.Sum(x => x.TotalInjuries) ?? 0 + typeUpAccident.Sum(x => x.TotalMinorInjuries) ?? 0;
                dt.Rows.Add(row);
            }
            this.ContentPanel2.TitleToolTip = "数据来源：职工伤亡事故原因分析月报；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
            this.ChartAccidentCause.CreateChart(BLL.ChartControlService.GetDataSourceChart(dt, "事故分析", "Column", 580, 180, false));
        }
        #endregion

        #region 隐患治理
        /// <summary>
        /// 隐患治理
        /// </summary>
        private void SetformRight1Values(int startMonth, int endMonth)
        {
            this.lbCheckRectifyCount.Text = "0";
            this.lbResponseCount.Text = "0";
            int year = Convert.ToInt32(this.drpYear.SelectedValue);
            var rectify = (from x in Funs.DB.Supervise_SuperviseCheckRectify 
                           where x.IssueDate.Value.Year == year
                           && x.IssueDate.Value.Month >= startMonth && x.IssueDate.Value.Month <= endMonth
                           select x);
            //if (!BLL.CommonService.IsMainUnitOrAdmin(this.drpUnit.SelectedValue))
            //{
            //    rectify = rectify.Where(x => x.UnitId == this.drpUnit.SelectedValue);
            //}
            if (rectify.Count() > 0)
            {
                var item = from x in Funs.DB.Supervise_SuperviseCheckRectifyItem
                                                join y in rectify on x.SuperviseCheckRectifyId equals y.SuperviseCheckRectifyId
                                                select x;
                this.lbCheckRectifyCount.Text = item.Count().ToString();
                this.lbResponseCount.Text = item.Where(x => x.RealEndDate.HasValue).Count().ToString();
            }

            this.formRight1.TitleToolTip = "数据来源：安全监督检查整改；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
        }
        #endregion

        #region 应急演练
        /// <summary>
        /// 应急演练
        /// </summary>
        private void SetformRight2Values(int startMonth, int endMonth)
        {
            this.lbTotalConductCount.Text = "0";
            this.lbTotalPeopleCount.Text = "0";
            this.lbTotalInvestment.Text = "0";          
            int startQuarter = Funs.GetNowQuarterlyByMonth(startMonth);
            int endQuarter = Funs.GetNowQuarterlyByMonth(endMonth);

            int year = Convert.ToInt32(this.drpYear.SelectedValue);

            var report = (from x in Funs.DB.Information_DrillConductedQuarterlyReport 
                          where x.YearId == year  && x.Quarter >= startQuarter && x.Quarter <= endQuarter
                          select x);
            //if (!BLL.CommonService.IsMainUnitOrAdmin(this.drpUnit.SelectedValue))
            //{
            //    report = report.Where(x => x.UnitId == this.drpUnit.SelectedValue);
            //}
            if (report.Count() > 0)
            {
                var item = from x in Funs.DB.Information_DrillConductedQuarterlyReportItem
                           join y in report on x.DrillConductedQuarterlyReportId equals y.DrillConductedQuarterlyReportId
                           select x;
                this.lbTotalConductCount.Text = (item.Sum(x => x.TotalConductCount) ?? 0).ToString();
                this.lbTotalPeopleCount.Text = (item.Sum(x => x.TotalPeopleCount) ?? 0).ToString();
                this.lbTotalInvestment.Text = (item.Sum(x => x.TotalInvestment) ?? 0).ToString();               
            }

            this.formRight2.TitleToolTip = "数据来源：应急演练开展情况季报；统计时间：" + this.drpYear.SelectedText + this.drpStartMonth.SelectedText + "至" + this.drpEndMonth.SelectedText;
        }
        #endregion

        #region 待办事项
        /// <summary>
        /// 绑定数据(待办事项)
        /// </summary>
        private void BindGridToDoMatter(string type)
        {
            var q = from x in Funs.DB.View_ToDoMatter select x;
            List<string> lawRegulationButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.LawRegulationListMenuId);
            if (!lawRegulationButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "法律法规");
            }
            List<string> standardButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HSSEStandardListMenuId);
            if (!standardButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "标准规范");
            }
            List<string> rulesRegulationsButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.RulesRegulationsMenuId);
            if (!rulesRegulationsButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "规章制度");
            }
            List<string> manageRuleButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ManageRuleMenuId);
            if (!manageRuleButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "管理规定");
            }
            List<string> trainingItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TrainDBMenuId);
            if (!trainingItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "培训教材");
            }
            List<string> trainTestItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.TrainTestDBMenuId);
            if (!trainTestItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全试题");
            }
            List<string> accidentCaseItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.AccidentCaseMenuId);
            if (!accidentCaseItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "事故案例");
            }
            List<string> knowledgeItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.KnowledgeDBMenuId);
            if (!knowledgeItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "应知应会");
            }
            List<string> hazardButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HazardListMenuId);
            if (!hazardButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "危险源");
            }
            List<string> rectifyItemButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.RectifyMenuId);
            if (!rectifyItemButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全隐患");
            }
            List<string> HAZOPButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HAZOPMenuId);
            if (!HAZOPButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "HAZOP");
            }
            List<string> expertButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ExpertMenuId);
            if (!expertButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "安全专家");
            }
            List<string> emergencyButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.EmergencyMenuId);
            if (!emergencyButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "应急预案");
            }
            List<string> specialSchemeButtonList = CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.SpecialSchemeMenuId);
            if (!specialSchemeButtonList.Contains(Const.BtnAuditing))
            {
                q = q.Where(e => e.Type != "专项方案");
            }
            q = q.Where(e => e.UserId == "" || e.UserId == this.CurrUser.UserId);
            if (type == "oper")
            {
            }
            else
            {
                q = q.Take(5);
            }
            var toDoMatterList = q.ToList();
            var dataIdList = (from x in Funs.DB.Sys_FlowOperate
                              where x.OperaterId == this.CurrUser.UserId && (x.IsClosed == false || !x.IsClosed.HasValue)
                              select x).ToList();
            if (dataIdList.Count() > 0)
            {
                foreach (var item in dataIdList)
                {
                    Model.View_ToDoMatter newTodo = new Model.View_ToDoMatter
                    {
                        Id = item.DataId
                    };
                    var menu = BLL.SysMenuService.GetSysMenuByMenuId(item.MenuId);
                    if (menu != null)
                    {
                        newTodo.Type = menu.MenuName;
                        if (!string.IsNullOrEmpty(item.Url))
                        {
                            string newUrl = item.Url.Replace("View.aspx", "Edit.aspx");
                            newTodo.Url = String.Format(newUrl, item.DataId, "审核 - ");
                        }
                    }
                    else
                    {
                        newTodo.Type = "项目单据";
                    }
                    string userName = BLL.UserService.GetUserNameByUserId(item.OperaterId);
                    var project = BLL.ProjectService.GetProjectByProjectId(item.ProjectId);
                    if (project != null)
                    {
                        newTodo.Name = project.ProjectName + ":待" + userName + "处理";
                    }
                    else
                    {
                        newTodo.Name = "本部系统：待" + userName + "处理";
                    }
                    newTodo.Date = item.OperaterTime;
                    newTodo.UserId = item.OperaterId;
                    toDoMatterList.Add(newTodo);
                }
            }

            DataTable tb = this.LINQToDataTable(toDoMatterList);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(GridNewDynamic, tb1);
            GridToDoMatter.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(GridToDoMatter.FilteredData, tb);
            var table = this.GetPagedDataTable(GridToDoMatter, tb);

            GridToDoMatter.DataSource = table;
            GridToDoMatter.DataBind();            
        }
        #endregion
    }
}