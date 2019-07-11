using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.InformationAnalysis
{
    public partial class HazardAnalyse : PageBase
    {
        #region 定义项
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]))
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpChartType, ConstValue.Group_ChartType, false);
                this.AnalyseData();                
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计方法
        /// </summary>
        private void AnalyseData()
        {
            List<string> unitValues = new List<string>();
            foreach (ListItem item in this.drpUnit.SelectedItemArray)
            {
                unitValues.Add(item.Value);              
            }
            ////危险观察等级
            var registrationList = from x in Funs.DB.Inspection_Registration
                                   where x.ProjectId == this.ProjectId && (x.State == BLL.Const.State_2 || x.State == BLL.Const.State_R)
                                   select x;
            var WorkAreaIdList = registrationList.Select(x => x.WorkAreaId).Distinct();

            #region 按时间统计
            ///按单位统计           
            DataTable dtHazard = new DataTable();
            dtHazard.Columns.Add("区域", typeof(string));
            foreach (var itemUnit in unitValues)
            {
                var unitName = BLL.UnitService.GetUnitNameByUnitId(itemUnit);
                dtHazard.Columns.Add(unitName, typeof(string));
            }
            foreach (var itemWorkArea in WorkAreaIdList)
            {
                DataRow rowUnit = dtHazard.NewRow();
                var WorkArea = Funs.DB.ProjectData_WorkArea.FirstOrDefault(x => x.WorkAreaId == itemWorkArea);
                if (WorkArea != null)
                {
                    rowUnit["区域"] = WorkArea.WorkAreaName;
                }
                else
                {
                    rowUnit["区域"] = "其他";
                }
                foreach (var itemUnit in unitValues)
                {
                    var unitName = BLL.UnitService.GetUnitNameByUnitId(itemUnit);
                    rowUnit[unitName] = registrationList.Where(x => x.ResponsibilityUnitId == itemUnit && x.WorkAreaId == itemWorkArea).Count();
                }
                dtHazard.Rows.Add(rowUnit);
            }

            this.gvHazard.DataSource = dtHazard;
            this.gvHazard.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtHazard, "按区域危险观察分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));
            #endregion

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
            this.drpUnit.SelectedValue = this.CurrUser.UnitId;
            this.AnalyseData();
        }      
        #endregion

        #region 统计查询
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

        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnalyse_Click(object sender, EventArgs e)
        {
            this.AnalyseData();
        }
        #endregion
    }
}