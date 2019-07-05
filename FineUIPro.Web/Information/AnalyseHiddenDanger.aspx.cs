using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Information
{
    public partial class AnalyseHiddenDanger : PageBase
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

        #region 统计分析
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
        #endregion

        #region 统计方法
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

            var rectify = (from x in Funs.DB.View_Supervise_SuperviseCheckRectify
                           where yearValues.Contains(x.CheckDate.Value.Year.ToString()) && monthValues.Contains(x.CheckDate.Value.Month.ToString())
                                       select x);

            #region 按时间统计
            ///按单位统计
            DataTable dtTime = new DataTable();
            dtTime.Columns.Add("类别", typeof(string));
            dtTime.Columns.Add("总数", typeof(string));
            dtTime.Columns.Add("完成数", typeof(string));
            var reportItem = from x in Funs.DB.View_Supervise_SuperviseCheckRectifyItem
                             join y in rectify on x.SuperviseCheckRectifyId equals y.SuperviseCheckRectifyId                            
                             select x;

            var rectifyName = (from x in Funs.DB.Technique_Rectify
                              join y in reportItem on x.RectifyName equals y.RectifyName
                              where y.SuperviseCheckRectifyId != null
                              select x.RectifyName).Distinct();
            foreach (var item in rectifyName)
            {
                DataRow rowTime = dtTime.NewRow();
                Model.SpTDesktopItem newspItem = new Model.SpTDesktopItem();
                rowTime["类别"] = item; 
                var typeItem = (from x in reportItem where x.RectifyName == item select x);

                rowTime["总数"] = typeItem.Count();
                rowTime["完成数"] = typeItem.Where(x=>x.RealEndDate.HasValue).Count();
                dtTime.Rows.Add(rowTime);
            }

            this.ChartHiddenDangerTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtTime, "隐患类别分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
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
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AnalyseHiddenDangerMenuId, button);
        }
        #endregion

        #region 清空
        ///// <summary>
        ///// 清空下拉框
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void drpUnit_ClearIconClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
        //    {
        //        this.drpUnit.SelectedValue = this.CurrUser.UnitId;
        //    }
        //    else
        //    {
        //        this.drpUnit.SelectedIndex = 0;
        //    }
        //    this.AnalyseData();
        //}
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