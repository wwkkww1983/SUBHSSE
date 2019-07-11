using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class CostLedger : PageBase
    {
        #region 项目主键
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

        private static string headerStr;

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
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
                if (project.StartDate == null || project.EndDate == null)
                {
                    Alert.ShowInTop("请先设置项目的开始时间和结束时间！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    headerStr = string.Empty;
                    DateTime startTime;
                    DateTime endTime;
                    DateTime monthStartTime;
                    DateTime monthEndTime;
                    int sortIndex = 1;
                    List<Model.Project_ProjectUnit> subUnits = BLL.ProjectUnitService.GetProjectUnitListByProjectIdUnitType(this.ProjectId, BLL.Const.ProjectUnitType_2);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("R0");   //序号
                    dt.Columns.Add("R1");   //施工分包商
                    dt.Columns.Add("R2");   //HSE费用（元）
                    dt.Columns.Add("R3");   //审批支付、剩余
                    startTime = Convert.ToDateTime(Convert.ToDateTime(project.StartDate).ToString("yyyy-MM"));
                    endTime = Convert.ToDateTime(Convert.ToDateTime(project.EndDate).ToString("yyyy-MM"));

                    string monthStr = string.Empty;
                    while (endTime >= startTime)
                    {
                        monthStr += startTime.ToString("yyyy-MM") + "|";
                        startTime = startTime.AddMonths(1);
                    }
                    if (!string.IsNullOrEmpty(monthStr))
                    {
                        monthStr = monthStr.Substring(0, monthStr.LastIndexOf("|"));
                    }
                    string[] months = monthStr.Split('|');
                    //如果超过15个月，需要加宽GridView以保证样式
                    if (months.Length > 15)
                    {
                        int addNum = months.Length - 15;
                        this.gvCostLedger.Width = 1100 + addNum * 60;
                    }
                    string monthTitle = string.Empty;
                    for (int i = 0; i < months.Length; i++)
                    {
                        monthTitle += months[i] + ",";
                        dt.Columns.Add("M" + i);
                    }
                    if (!string.IsNullOrEmpty(monthTitle))
                    {
                        monthTitle = monthTitle.Substring(0, monthTitle.LastIndexOf(","));
                    }
                    dt.Columns.Add("合计");
                    headerStr += "序号#施工分包商#HSE费用(元)#步骤#审批金额";
                    headerStr += " " + monthTitle + "#合计";
                    foreach (Model.Project_ProjectUnit proUnit in subUnits)
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(proUnit.UnitId);
                        decimal? planCost = proUnit.PlanCostA + proUnit.PlanCostB ?? 0;
                        decimal planAmount = planCost ?? 0;
                        decimal lastCost = planAmount;
                        decimal totalCost = 0;
                        DataRow row1 = dt.NewRow();
                        DataRow row2 = dt.NewRow();
                        row1[0] = sortIndex;
                        row1[1] = unit.UnitName;
                        row1[2] = planAmount.ToString("0.00");
                        row1[3] = "审批支付";
                        row2[0] = sortIndex;
                        row2[1] = unit.UnitName;
                        row2[2] = planAmount.ToString("0.00");
                        row2[3] = "剩余";
                        int r = 4;
                        for (int i = 0; i < months.Length; i++)
                        {
                            DateTime month = Convert.ToDateTime(months[i] + "-01");
                            monthStartTime = month.AddMonths(-1).AddDays(25);
                            monthEndTime = month.AddDays(24);
                            decimal? costitem =BLL.CostManageItemService.GetCostsByUnitId(unit.UnitId, monthStartTime, monthEndTime);//分包商HSE费用申请总价
                            decimal? subPay = BLL.SubPayRegistrationService.GetSubPayRegistrationByUnitId(unit.UnitId, monthStartTime, monthEndTime);//分包商HSE费用投入登记
                            decimal? cost = null;
                            if (costitem != null && subPay != null)
                            {
                                cost = Convert.ToDecimal(costitem) + Convert.ToDecimal(subPay);
                            }
                            else
                            {
                                if (costitem != null)
                                {
                                    cost = Convert.ToDecimal(costitem);
                                }
                                else if (subPay != null)
                                {
                                    cost = Convert.ToDecimal(subPay);
                                }
                            }                           

                            if (cost!=null)
                            {
                                decimal c = Convert.ToDecimal(cost);
                                row1[r + i] = c.ToString("0.00"); ;
                                totalCost += c;
                                if (lastCost == planAmount)
                                {
                                    lastCost = planAmount - c;
                                }
                                else
                                {
                                    lastCost = lastCost - c;
                                }
                                row2[r + i] = lastCost.ToString("0.00");
                            }
                        }
                        row1[r + months.Length] = totalCost.ToString("0.00"); ;
                        dt.Rows.Add(row1);
                        dt.Rows.Add(row2);
                        sortIndex++;
                    }
                    this.gvCostLedger.DataSource = dt;
                    this.gvCostLedger.DataBind();
                }
            }
        }

        protected void gvCostLedger_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DynamicTHeaderHepler dHelper = new DynamicTHeaderHepler();
                dHelper.SplitTableHeader(e.Row, headerStr);
            }
        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCostLedger_DataBound(object sender, EventArgs e)
        {
            int row1 = 1, t1 = 0, row2 = 1, t2 = 0, row0 = 1, t0 = 0;
            for (int i = 0; i < this.gvCostLedger.Rows.Count - 1; i++)
            {
                GridViewRow gvr = this.gvCostLedger.Rows[i];
                GridViewRow gvrNext = this.gvCostLedger.Rows[i + 1];
                if (gvr.Cells[0].Text == gvrNext.Cells[0].Text)
                {
                    gvrNext.Cells[0].Visible = false;
                    row0++;
                    this.gvCostLedger.Rows[t0].Cells[0].RowSpan = row0;
                }
                else
                {
                    t0 = row0 + t0;
                    row0 = 1;
                }
                if (gvr.Cells[1].Text == gvrNext.Cells[1].Text)
                {
                    gvrNext.Cells[1].Visible = false;
                    row1++;
                    this.gvCostLedger.Rows[t1].Cells[1].RowSpan = row1;
                }
                else
                {
                    t1 = row1 + t1;
                    row1 = 1;
                }
                if (gvr.Cells[2].Text == gvrNext.Cells[2].Text)
                {
                    gvrNext.Cells[2].Visible = false;
                    row2++;
                    this.gvCostLedger.Rows[t2].Cells[2].RowSpan = row2;
                }
                else
                {
                    t2 = row2 + t2;
                    row2 = 1;
                }
            }
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("HSSE合同HSE费用及支付台账", System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            this.gvCostLedger.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        #endregion
    }
}