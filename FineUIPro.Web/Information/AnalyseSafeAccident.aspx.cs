using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Information
{
    public partial class AnalyseSafeAccident : PageBase
    {
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
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, false);
                this.drpYear.SelectedValue = DateTime.Now.Year.ToString();

                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, ConstValue.Group_0009, false);
                this.drpMonth.SelectedValue = DateTime.Now.Month.ToString();

                BLL.ConstValue.InitConstValueDropDownList(this.drpChartType, ConstValue.Group_ChartType, false);
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            if (!GetButtonPower(BLL.Const.BtnAnalyse))
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }

            this.AnalyseData();
        }

        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            List<string> yearValues = new List<string>();
            foreach (ListItem item in this.drpYear.SelectedItemArray)
            {
                yearValues.Add(item.Value);
            }

            List<string> monthValues = new List<string>();
            foreach (ListItem item in this.drpMonth.SelectedItemArray)
            {
                monthValues.Add(item.Value);
            }

            var accidentCauseReport = (from x in Funs.DB.Information_AccidentCauseReport
                                       where yearValues.Contains(x.Year.ToString()) && monthValues.Contains(x.Month.ToString())
                                       select x);
          
            #region 按时间统计
            ///按单位统计
            DataTable dtTime = new DataTable();
            dtTime.Columns.Add("事故类别", typeof(string));
            dtTime.Columns.Add("数量", typeof(string));
           
            var reportItem = from x in Funs.DB.Information_AccidentCauseReportItem
                             join y in accidentCauseReport on x.AccidentCauseReportId equals y.AccidentCauseReportId                            
                             select x;

            var accidentType = BLL.ConstValue.drpConstItemList(ConstValue.Group_0012).Where(x => x.ConstValue != "总计");
            foreach (var item in accidentType)
            {
                DataRow rowTime = dtTime.NewRow();
                Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                rowTime["事故类别"] = item.ConstText;
                var typeAccident = (from x in reportItem
                                    where x.AccidentType == item.ConstValue
                                    select x).ToList();

                rowTime["数量"] = typeAccident.Sum(x => x.TotalDeath) ?? 0 + typeAccident.Sum(x => x.TotalInjuries) ?? 0 + typeAccident.Sum(x => x.TotalMinorInjuries) ?? 0;
                dtTime.Rows.Add(rowTime);
            }

            this.ChartAccidentTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "事故类别分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
            #endregion

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
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AnalyseSafeAccidentMenuId, button);
        }
        #endregion

        #region 清空
        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpYear_ClearIconClick(object sender, EventArgs e)
        {
            this.drpYear.SelectedValue = DateTime.Now.Year.ToString();
            this.AnalyseData();
        }

        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMonth_ClearIconClick(object sender, EventArgs e)
        {
            this.drpMonth.SelectedValue = DateTime.Now.Month.ToString();
            this.AnalyseData();
        }
        #endregion

        #region 图形
        /// <summary>
        /// 图形变换 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.AnalyseData();
        }

        protected void ckbShow_CheckedChanged(object sender, CheckedEventArgs e)
        {
            this.AnalyseData();
        }
        #endregion
    }
}