using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Information
{
    public partial class AnalyseWorkTimeCost : PageBase
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

                BLL.ConstValue.InitConstValueDropDownList(this.drpQuarter, ConstValue.Group_0011, false);
                this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(DateTime.Now).ToString();

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

            List<string> quarterValues = new List<string>();          
            foreach (ListItem item in this.drpQuarter.SelectedItemArray)
            {
                quarterValues.Add(item.Value);               
            }

            var safetyQuarterlyReport = (from x in Funs.DB.Information_SafetyQuarterlyReport 
                                         where yearValues.Contains(x.YearId.ToString()) && quarterValues.Contains(x.Quarters.ToString())
                                         select x);
          
            #region 按时间统计
            ///按单位统计
            DataTable dtTime = new DataTable();            
            dtTime.Columns.Add("时间", typeof(string));
            dtTime.Columns.Add("工时", typeof(string));
            dtTime.Columns.Add("费用", typeof(string));
            foreach (var itemYear in yearValues)
            {
                string yearName = string.Empty;
                var year = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008).FirstOrDefault(x => x.ConstValue == itemYear);
                if (year != null)
                {
                    yearName = year.ConstText;
                }
                foreach (var itemQuarter in quarterValues)
                {
                    string quarterName = string.Empty;
                    var quarter = BLL.ConstValue.drpConstItemList(ConstValue.Group_0011).FirstOrDefault(x => x.ConstValue == itemQuarter);
                    if (quarter != null)
                    {
                        quarterName = quarter.ConstText;
                    }

                    DataRow rowTime = dtTime.NewRow();
                    Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                    rowTime["时间"] = yearName + quarterName;
                    var quartersSafe = (from x in safetyQuarterlyReport
                                        where x.YearId.ToString() == itemYear && x.Quarters.ToString() == itemQuarter
                                        select x).ToList();
                    rowTime["工时"] = quartersSafe.Sum(x => x.TotalInWorkHours) ?? 0;
                    rowTime["费用"] = quartersSafe.Sum(x => x.ProductionSafetyInTotal) ?? 0;
                    dtTime.Rows.Add(rowTime);
                }
            }

            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "人工时费用", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
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
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AnalyseWorkTimeCostMenuId, button);
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
        protected void drpQuarter_ClearIconClick(object sender, EventArgs e)
        {
            this.drpQuarter.SelectedValue = Funs.GetNowQuarterlyByTime(Convert.ToDateTime(DateTime.Now)).ToString();
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