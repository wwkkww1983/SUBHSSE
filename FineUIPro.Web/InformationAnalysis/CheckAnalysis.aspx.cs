using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BLL;

namespace FineUIPro.Web.InformationAnalysis
{
    public partial class CheckAnalysis : PageBase
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
            
            ///按单位统计           
            DataTable dtCheck = new DataTable();
            dtCheck.Columns.Add("检查类型", typeof(string));
            dtCheck.Columns.Add("数量", typeof(string));

            List<Model.SpResourceCollection> newCheckAnalyseView = new List<Model.SpResourceCollection>(); 
            var checkAnalyseView = from x in Funs.DB.View_CheckAnalysis                                    
                                   where x.ProjectId == this.ProjectId
                                   select x;
            if (!string.IsNullOrEmpty(this.txtStarTime.Text))
            {
                checkAnalyseView = checkAnalyseView.Where(x => x.CheckTime >= Funs.GetNewDateTime(this.txtStarTime.Text));
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text))
            {
                checkAnalyseView = checkAnalyseView.Where(x => x.CheckTime <= Funs.GetNewDateTime(this.txtEndTime.Text));
            }
            if (checkAnalyseView.Count() > 0)
            {
                foreach (var item in checkAnalyseView)
                {
                    Model.SpResourceCollection view = new Model.SpResourceCollection
                    {
                        UnitName = BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(item.CheckItem),
                        TotalCount = 1
                    };
                    newCheckAnalyseView.Add(view);
                }
            }

            var checkType = newCheckAnalyseView.Select(x => x.UnitName).Distinct();
            foreach (var itemType in checkType)
            {
                DataRow rowUnit = dtCheck.NewRow();
                rowUnit["检查类型"] = itemType;
                rowUnit["数量"] = newCheckAnalyseView.Where(x => x.UnitName == itemType).Count();                
                dtCheck.Rows.Add(rowUnit);
            }

            this.gvCheck.DataSource = dtCheck;
            this.gvCheck.DataBind();
            this.ChartCostTime.CreateChart(BLL.ChartControlService.GetDataSourceChart(dtCheck, "危险因素分析", this.drpChartType.SelectedValue, 1150, 450, this.ckbShow.Checked));         
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