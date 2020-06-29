using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Information
{
    public partial class MillionsMonthlyReport : PageBase
    {
        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                BLL.ConstValue.InitConstValueDropDownList(this.drpMonth, ConstValue.Group_0009, false);
                BLL.ConstValue.InitConstValueDropDownList(this.drpYear, ConstValue.Group_0008, false);
              
                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;
                ////取上个报表时间
                DateTime showDate = System.DateTime.Now.AddMonths(-1);
                drpMonth.SelectedValue = showDate.Month.ToString();
                drpYear.SelectedValue = showDate.Year.ToString();
                GetValue();
            }
        }

        private void SetEmpty()
        {
            this.SimpleForm1.Title = string.Empty;
            lbUnitName.Text = "填报企业：";
            lbFillingDate.Text = "填报日期：";
            lbDutyPerson.Text = "负责人：";
            lbRecordableIncidentRate.Text = "百万工时总可记录事故率:";
            lbLostTimeRate.Text = "百万工时损失工时率:";
            lbLostTimeInjuryRate.Text = "百万工时损失工时伤害事故率:";
            lbDeathAccidentFrequency.Text = "百万工时死亡事故频率:";
            lbAccidentMortality.Text = "百万工时事故死亡率:";
            lbHandleMan.Text = string.Empty;
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
        }
        #endregion

        #region 获取记录值
        private void GetValue()
        {
            int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
            int month = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
            Model.View_Information_MillionsMonthlyReport report = Funs.DB.View_Information_MillionsMonthlyReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.Month == month && e.Year == year);
            if (report != null)
            {
                string upState = string.Empty;
                if (report.UpState == BLL.Const.UpState_3)
                {
                    upState = "(已上报)";
                }
                else
                {
                    upState = "(未上报)";
                }
                this.SimpleForm1.Title = "企业百万工时安全统计月报表" + report.MonthStr + report.YearStr + upState;
                lbUnitName.Text = "填报企业：" + report.UnitName;
                if (report.FillingDate != null)
                {
                    lbFillingDate.Text = "填报日期：" + string.Format("{0:yyyy-MM-dd}", report.FillingDate);
                }
                else
                {
                    lbFillingDate.Text = "填报日期：";
                }
                lbDutyPerson.Text = "负责人：" + report.DutyPerson;
                if (report.HandleState == BLL.Const.HandleState_1 || report.UpState == BLL.Const.UpState_3)
                {
                    this.lbHandleMan.Hidden = true;
                }
                else
                {
                    this.lbHandleMan.Hidden = false;
                    lbHandleMan.Text = "下一步办理人：" + report.UserName;
                }
                lbRecordableIncidentRate.Text = "百万工时总可记录事故率:" + (report.RecordableIncidentRate ?? 0).ToString();
                lbLostTimeRate.Text = "百万工时损失工时率:" + (report.LostTimeRate ?? 0).ToString();
                lbLostTimeInjuryRate.Text = "百万工时损失工时伤害事故率:" + (report.LostTimeInjuryRate ?? 0).ToString();
                lbDeathAccidentFrequency.Text = "百万工时死亡事故频率:" + (report.DeathAccidentFrequency ?? 0).ToString();
                lbAccidentMortality.Text = "百万工时事故死亡率:" + (report.AccidentMortality ?? 0).ToString();
                List<Model.Information_MillionsMonthlyReportItem> items = BLL.MillionsMonthlyReportItemService.GetItems(report.MillionsMonthlyReportId);
                //本年度累计行
                Model.Information_MillionsMonthlyReportItem yearTotalItem = new Model.Information_MillionsMonthlyReportItem();
                //之前月度合计值集合
                List<Model.Information_MillionsMonthlyReportItem> yearSumItems = BLL.MillionsMonthlyReportItemService.GetYearSumItems(report.UnitId, report.Year, report.Month);
                yearTotalItem.MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.Information_MillionsMonthlyReportItem));
                yearTotalItem.MillionsMonthlyReportId = report.MillionsMonthlyReportId;
                yearTotalItem.SortIndex = (items.Count + 1) * 10;
                yearTotalItem.Affiliation = "本年度累计";
                yearTotalItem.Name = "本年度累计";
                yearTotalItem.PostPersonNum = yearSumItems.Sum(x => x.PostPersonNum ?? 0);
                yearTotalItem.SnapPersonNum = yearSumItems.Sum(x => x.SnapPersonNum ?? 0);
                yearTotalItem.ContractorNum = yearSumItems.Sum(x => x.ContractorNum ?? 0);
                yearTotalItem.SumPersonNum = yearSumItems.Sum(x => x.SumPersonNum ?? 0);
                yearTotalItem.TotalWorkNum = yearSumItems.Sum(x => x.TotalWorkNum ?? 0);
                yearTotalItem.SeriousInjuriesNum = yearSumItems.Sum(x => x.SeriousInjuriesNum ?? 0);
                yearTotalItem.SeriousInjuriesPersonNum = yearSumItems.Sum(x => x.SeriousInjuriesPersonNum ?? 0);
                yearTotalItem.SeriousInjuriesLossHour = yearSumItems.Sum(x => x.SeriousInjuriesLossHour ?? 0);
                yearTotalItem.MinorAccidentNum = yearSumItems.Sum(x => x.MinorAccidentNum ?? 0);
                yearTotalItem.MinorAccidentPersonNum = yearSumItems.Sum(x => x.MinorAccidentPersonNum ?? 0);
                yearTotalItem.MinorAccidentLossHour = yearSumItems.Sum(x => x.MinorAccidentLossHour ?? 0);
                yearTotalItem.OtherAccidentNum = yearSumItems.Sum(x => x.OtherAccidentNum ?? 0);
                yearTotalItem.OtherAccidentPersonNum = yearSumItems.Sum(x => x.OtherAccidentPersonNum ?? 0);
                yearTotalItem.OtherAccidentLossHour = yearSumItems.Sum(x => x.OtherAccidentLossHour ?? 0);
                yearTotalItem.RestrictedWorkPersonNum = yearSumItems.Sum(x => x.RestrictedWorkPersonNum ?? 0);
                yearTotalItem.RestrictedWorkLossHour = yearSumItems.Sum(x => x.RestrictedWorkLossHour ?? 0);
                yearTotalItem.MedicalTreatmentPersonNum = yearSumItems.Sum(x => x.MedicalTreatmentPersonNum ?? 0);
                yearTotalItem.MedicalTreatmentLossHour = yearSumItems.Sum(x => x.MedicalTreatmentLossHour ?? 0);
                yearTotalItem.FireNum = yearSumItems.Sum(x => x.FireNum ?? 0);
                yearTotalItem.ExplosionNum = yearSumItems.Sum(x => x.ExplosionNum ?? 0);
                yearTotalItem.TrafficNum = yearSumItems.Sum(x => x.TrafficNum ?? 0);
                yearTotalItem.EquipmentNum = yearSumItems.Sum(x => x.EquipmentNum ?? 0);
                yearTotalItem.QualityNum = yearSumItems.Sum(x => x.QualityNum ?? 0);
                yearTotalItem.OtherNum = yearSumItems.Sum(x => x.OtherNum ?? 0);
                yearTotalItem.FirstAidDressingsNum = yearSumItems.Sum(x => x.FirstAidDressingsNum ?? 0);
                yearTotalItem.AttemptedEventNum = yearSumItems.Sum(x => x.AttemptedEventNum ?? 0);
                yearTotalItem.LossDayNum = yearSumItems.Sum(x => x.LossDayNum ?? 0);
                items.Add(yearTotalItem);
                Grid1.DataSource = items;
                Grid1.DataBind();
                if (Grid1.Rows.Count > 2)
                {
                    Grid1.Rows[Grid1.Rows.Count - 2].Values[0] = "";
                    Grid1.Rows[Grid1.Rows.Count - 2].Values[1] = "";
                }
                if (Grid1.Rows.Count > 1)
                {
                    Grid1.Rows[Grid1.Rows.Count - 1].Values[0] = "";
                    Grid1.Rows[Grid1.Rows.Count - 1].Values[1] = "";
                }
            }
            else
            {
                SetEmpty();
            }
            this.GetButtonPower();
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }

        /// <summary>
        /// 关闭查看审批信息弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {

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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.MillionsMonthlyReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnPrint))
                {
                    this.btnPrint.Hidden = false;
                }
                Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
                this.btnAudit1.Hidden = true;
                this.btnAudit2.Hidden = true;
                this.btnUpdata.Hidden = true;
                if (report != null)
                {
                    this.btnNew.Hidden = true;
                    if (report.HandleMan == this.CurrUser.UserId)   //当前人是下一步办理入
                    {
                        if (report.HandleState == BLL.Const.HandleState_2)
                        {
                            this.btnAudit1.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_3)
                        {
                            this.btnAudit2.Hidden = false;
                        }
                        else if (report.HandleState == BLL.Const.HandleState_4)
                        {
                            this.btnUpdata.Hidden = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region 增加、修改、审核、审批、上报、删除
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportSave.aspx?UnitId={0}&&Year={1}&&Months={2}", this.CurrUser.UnitId, this.drpYear.SelectedValue, this.drpMonth.SelectedValue, "编辑 - ")));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportSave.aspx?MillionsMonthlyReportId={0}", report.MillionsMonthlyReportId, "编辑 - ")));
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit1_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportSave.aspx?MillionsMonthlyReportId={0}", report.MillionsMonthlyReportId, "编辑 - ")));
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit2_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportSave.aspx?MillionsMonthlyReportId={0}", report.MillionsMonthlyReportId, "编辑 - ")));
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MillionsMonthlyReportSave.aspx?MillionsMonthlyReportId={0}", report.MillionsMonthlyReportId, "编辑 - ")));
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, report.Year.ToString() + "-" + report.Month.ToString(), report.MillionsMonthlyReportId, BLL.Const.MillionsMonthlyReportMenuId, BLL.Const.BtnDelete);
                BLL.ProjectDataFlowSetService.DeleteFlowSetByDataId(report.MillionsMonthlyReportId);
                BLL.MillionsMonthlyReportItemService.DeleteMillionsMonthlyReportItemByMillionsMonthlyReportId(report.MillionsMonthlyReportId);
                BLL.MillionsMonthlyReportService.DeleteMillionsMonthlyReportByMillionsMonthlyReportId(report.MillionsMonthlyReportId);
                
                SetEmpty();
                this.btnNew.Hidden = false;
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("所选时间无报表记录！");
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 把时间转换为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCompileDate(object CompileDate)
        {
            if (CompileDate != null)
            {
                return string.Format("{0:yyyy-MM-dd}", CompileDate);
            }
            return "";
        }

        /// <summary>
        /// 把时间转换为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertDate(object MillionsMonthlyReportId)
        {
            if (MillionsMonthlyReportId != null)
            {
                Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(MillionsMonthlyReportId.ToString());
                if (report != null)
                {
                    return report.Year + "年" + report.Month + "月";
                }
            }
            return "";
        }
        #endregion

        #region 单位下拉选择事件
        /// <summary>
        /// 单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var units = BLL.UnitService.GetUnitDropDownList();
            if (units != null && !string.IsNullOrEmpty(this.drpUnit.SelectedText))
            {
                var unit = units.FirstOrDefault(x => x.UnitName.Contains(this.drpUnit.SelectedText));
                if (unit != null)
                {
                    drpUnit.SelectedValue = unit.UnitId;
                }
            }

            GetValue();
        }
        #endregion

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../DataIn/MillionsMonthlyReportImport.aspx", "导入 - ")));
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.Information_MillionsMonthlyReportId, report.MillionsMonthlyReportId, "", "打印 - ")));
            }
        }
        #endregion

        #region 月份向前/向后
        /// <summary>
        /// 前一个月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBulletLeft_Click(object sender, EventArgs e)
        {
            SetMonthChange("-");
        }

        /// <summary>
        /// 后一个月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulletRight_Click(object sender, EventArgs e)
        {
            SetMonthChange("+");
        }

        /// <summary>
        /// 月份加减变化
        /// </summary>
        /// <param name="type"></param>
        private void SetMonthChange(string type)
        {
            DateTime? nowDate = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue);
            if (nowDate.HasValue)
            {
                DateTime showDate = new DateTime();
                if (type == "+")
                {
                    showDate = nowDate.Value.AddMonths(1);
                }
                else
                {
                    showDate = nowDate.Value.AddMonths(-1);
                }

                this.drpYear.SelectedValue = showDate.Year.ToString();
                drpMonth.SelectedValue = showDate.Month.ToString();
                ///值变化
                GetValue();
            }
        }
        #endregion

        #region 查看审批信息
        /// <summary>
        /// 查看审批信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("ReportAuditSee.aspx?Id={0}", report.MillionsMonthlyReportId, "查看 - ")));
            }
            else
            {
                ShowNotify("所选月份无记录！", MessageBoxIcon.Warning);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("百万工时安全统计月报" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();

        }

#pragma warning disable CS0108 // “MillionsMonthlyReport.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “MillionsMonthlyReport.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            MultiHeaderTable mht = new MultiHeaderTable();
            mht.ResolveMultiHeaderTable(Grid1.Columns); 
           sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");  
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">"); 
            foreach (List<object[]> rows in mht.MultiTable)
            {
                sb.Append("<tr>");
               foreach (object[] cell in rows)
               {                    int rowspan = Convert.ToInt32(cell[0]);
                   int colspan = Convert.ToInt32(cell[1]);
                   GridColumn column = cell[2] as GridColumn;

                    sb.AppendFormat("<th{0}{1}{2}>{3}</th>",
                       rowspan != 1 ? " rowspan=\"" + rowspan + "\"" : "",
                       colspan != 1 ? " colspan=\"" + colspan + "\"" : "",
                      colspan != 1 ? " style=\"text-align:center;\"" : "",
                        column.HeaderText);
                }
               sb.Append("</tr>");
            }
           foreach (GridRow row in grid.Rows)
           {
               sb.Append("<tr>");
                foreach (GridColumn column in mht.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                   sb.AppendFormat("<td>{0}</td>", html);
               }
               sb.Append("</tr>");
            } 
            sb.Append("</table>"); 
            return sb.ToString();
        }

        #region 多表头处理
        /// <summary>
        /// 多表头处理
        /// </summary>
        public class MultiHeaderTable
        {
            // 包含 rowspan，colspan 的多表头，方便生成 HTML 的 table 标签
            public List<List<object[]>> MultiTable = new List<List<object[]>>();
            // 最终渲染的列数组
            public List<GridColumn> Columns = new List<GridColumn>();
            public void ResolveMultiHeaderTable(GridColumnCollection columns)
            {
                List<object[]> row = new List<object[]>();
                foreach (GridColumn column in columns)
                {
                    object[] cell = new object[4];
                    cell[0] = 1;    // rowspan
                    cell[1] = 1;    // colspan
                    cell[2] = column;
                    cell[3] = null;
                    row.Add(cell);
                }
                ResolveMultiTable(row, 0);
                ResolveColumns(row);
            }

            private void ResolveColumns(List<object[]> row)
            {
                foreach (object[] cell in row)
                {
                    GroupField groupField = cell[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        List<object[]> subrow = new List<object[]>();
                        foreach (GridColumn column in groupField.Columns)
                        {
                            subrow.Add(new object[]
                           {
                               1,
                                1,
                               column,
                                groupField
                            });
                        }
                        ResolveColumns(subrow);
                    }
                    else
                    {
                        Columns.Add(cell[2] as GridColumn);
                    }
                }
            }

            private void ResolveMultiTable(List<object[]> row, int level)
            {
                List<object[]> nextrow = new List<object[]>();

                foreach (object[] cell in row)
                {
                    GroupField groupField = cell[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        // 如果当前列包含子列，则更改当前列的 colspan，以及增加父列（向上递归）的colspan
                        cell[1] = Convert.ToInt32(groupField.Columns.Count);
                        PlusColspan(level - 1, cell[3] as GridColumn, groupField.Columns.Count - 1);

                        foreach (GridColumn column in groupField.Columns)
                        {
                            nextrow.Add(new object[]
                           {
                               1,
                                1,
                                column,
                                groupField
                           });
                        }
                    }
                }
                MultiTable.Add(row);
                // 如果当前下一行，则增加上一行（向上递归）中没有子列的列的 rowspan
                if (nextrow.Count > 0)
                {
                    PlusRowspan(level);
                    ResolveMultiTable(nextrow, level + 1);
                }
            }

            private void PlusRowspan(int level)
            {
                if (level < 0)
                {
                    return;
                }
                foreach (object[] cells in MultiTable[level])
                {
                    GroupField groupField = cells[2] as GroupField;
                    if (groupField != null && groupField.Columns.Count > 0)
                    {
                        // ...
                    }
                    else
                    {
                        cells[0] = Convert.ToInt32(cells[0]) + 1;
                    }
                }
                PlusRowspan(level - 1);
            }

            private void PlusColspan(int level, GridColumn parent, int plusCount)
            {
                if (level < 0)
                {
                    return;
                }

                foreach (object[] cells in MultiTable[level])
                {
                    GridColumn column = cells[2] as GridColumn;
                    if (column == parent)
                    {
                        cells[1] = Convert.ToInt32(cells[1]) + plusCount;

                        PlusColspan(level - 1, cells[3] as GridColumn, plusCount);
                    }
                }
            }
        }
        #endregion
        #endregion

        #region 查看未上报的项目
        /// <summary>
        /// 查看未上报的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            string info = string.Empty;
            DateTime date = Convert.ToDateTime(this.drpYear.SelectedValue + "-" + this.drpMonth.SelectedValue + "-01").AddDays(-1).AddMonths(1);
            var projects = (from x in Funs.DB.Base_Project
                            where (x.ProjectState == BLL.Const.ProjectState_1 || x.ProjectState == null)
                            && x.StartDate <= date
                            select x).ToList();
            foreach (var item in projects)
            {
                var millionsMonthlyReport = Funs.DB.InformationProject_MillionsMonthlyReport.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.Year == date.Year && x.Month == date.Month);
                if (millionsMonthlyReport == null)
                {
                    info += item.ProjectCode + ":" + item.ProjectName + "，未填写报表；</br>";
                }
                else
                {                   
                    if (millionsMonthlyReport.States != BLL.Const.State_2)
                    {
                        info += item.ProjectCode + ":" + item.ProjectName + "报表未报；";
                        var flows = (from x in Funs.DB.Sys_FlowOperate
                                     join y in Funs.DB.Sys_User on x.OperaterId equals y.UserId
                                     where x.DataId == millionsMonthlyReport.MillionsMonthlyReportId && x.IsClosed != false
                                     select y).FirstOrDefault();
                        if (flows != null)
                        {
                            info += "待" + flows.UserName + "处理；";
                        }
                        info += "</br>";
                    }
                }
            }

            if (!string.IsNullOrEmpty(info))
            {
                Alert.ShowInTop(info + "项目报表未上报。", MessageBoxIcon.Warning);
            }
            else
            {
                ShowNotify("项目报表已上报", MessageBoxIcon.Success);
            }

        }
        #endregion
    }
}