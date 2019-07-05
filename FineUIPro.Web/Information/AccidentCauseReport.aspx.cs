using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;

namespace FineUIPro.Web.Information
{
    public partial class AccidentCauseReport : PageBase
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
            lb1.Text = "一、填报单位：";
            lb2.Text = "二、本月发生死亡事故0次，死亡0人，重伤事故0次，重伤0人，轻伤事故0次，轻伤0人。";
            lb3.Text = "三、本月平均工时总数0，人数0人。";
            lb4.Text = "四、本月事故损失工时总数0小时，上月事故损失工时总数0。";
            lb5.Text = "五、伤者在本月内的歇工总日数0天。";
            lb6.Text = "六、事故直接损失0,间接损失0,总损失0。";
            lb7.Text = "七、无损失工时总数0。";
            lbFillCompanyPersonCharge.Text = "填报单位负责人：";
            lbTabPeople.Text = "制表人：";
            lbAuditPerson.Text = "审核人：";
            lbFillingDate.Text = "填报日期：";
            this.lbHandleMan.Text = string.Empty;
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
        }
        #endregion

        #region 获取记录值
        private void GetValue()
        {
            int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
            int monthId = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
            Model.View_Information_AccidentCauseReport report = Funs.DB.View_Information_AccidentCauseReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.Month == monthId && e.Year == year);
            if (report != null)
            {
                string month = report.MonthStr;
                string lastMonth = string.Empty;
                if (month == "一月")
                {
                    lastMonth = "十二月";
                }
                else
                {
                    lastMonth = (from x in Funs.DB.Sys_Const where x.GroupId == BLL.ConstValue.Group_0009 && Convert.ToInt32(x.ConstValue) == (Convert.ToInt32(report.Month) - 1) select x.ConstText).FirstOrDefault();
                }
                string state = string.Empty;
                if (report.UpState == BLL.Const.UpState_3)
                {
                    state = "(已上报)";                   
                }
                else
                {
                    if (report.HandleState == BLL.Const.HandleState_1)
                    {
                        if (string.IsNullOrEmpty(report.TabPeople))
                        {                          
                            report.TabPeople = this.CurrUser.UserName;
                        }
                        state = "(待提交)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_2)
                    {
                        if (string.IsNullOrEmpty(report.AuditPerson))
                        {
                            report.AuditPerson = this.CurrUser.UserName;
                        }
                        state = "(待审核)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_3)
                    {
                        state = "(待审批)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_4)
                    {
                        if (string.IsNullOrEmpty(report.FillCompanyPersonCharge))
                        {
                            report.FillCompanyPersonCharge = this.CurrUser.UserName;
                        }
                        state = "(待上报)";
                    }
                }
                this.SimpleForm1.Title = "职工伤亡事故原因分析" + month + report.YearStr + "报表(" + report.AccidentCauseReportCode + ")" + state;
                string unitName = string.Empty;
                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(report.UnitId);
                if (unit != null)
                {
                    unitName = unit.UnitName;
                }
                lb1.Text = "一、填报单位：" + unitName;
                lb2.Text = "二、本月（" + month + "）发生死亡事故" + (report.DeathAccident ?? 0).ToString() + "次，死亡" + (report.DeathToll ?? 0).ToString() + "人，重伤事故" + (report.InjuredAccident ?? 0).ToString() + "次，重伤"
                       + (report.InjuredToll ?? 0).ToString() + "人，轻伤事故" + (report.MinorWoundAccident ?? 0).ToString() + "次，轻伤" + (report.MinorWoundToll ?? 0).ToString() + "人。";
                lb3.Text = "三、本月（" + month + ")平均工时总数" + (report.AverageTotalHours ?? 0).ToString() + "，人数" + (report.AverageManHours ?? 0).ToString() + "人。";
                lb4.Text = "四、本月事故损失工时总数" + (report.TotalLossMan ?? 0).ToString() + "小时，上月（" + lastMonth + "）事故损失工时总数" + (report.LastMonthLossHoursTotal ?? 0).ToString() + "。";
                lb5.Text = "五、伤者在本月（" + month + "）内的歇工总日数" + (report.KnockOffTotal ?? 0).ToString() + "天。";
                lb6.Text = "六、事故直接损失" + (report.DirectLoss ?? 0).ToString() + ",间接损失" + (report.IndirectLosses ?? 0).ToString() + ",总损失" + (report.TotalLoss ?? 0).ToString() + "。";
                lb7.Text = "七、无损失工时总数" + (report.TotalLossTime ?? 0).ToString() + "。";
                lbFillCompanyPersonCharge.Text = "填报单位负责人：" + report.FillCompanyPersonCharge;
                lbTabPeople.Text = "制表人：" + report.TabPeople;
                lbAuditPerson.Text = "审核人：" + report.AuditPerson;
                if (report.FillingDate != null)
                {
                    lbFillingDate.Text = "填报日期：" + string.Format("{0:yyyy-MM-dd}", report.FillingDate);
                }
                else
                {
                    lbFillingDate.Text = "填报日期：";
                }
                if (report.HandleState == BLL.Const.HandleState_1 || report.UpState == BLL.Const.UpState_3)
                {
                    this.lbHandleMan.Hidden = true;
                }
                else
                {
                    this.lbHandleMan.Hidden = false;
                    lbHandleMan.Text = "下一步办理人：" + report.UserName;
                }
                if (!string.IsNullOrEmpty(report.TabPeople))
                {
                    List<Model.Information_AccidentCauseReportItem> items = BLL.AccidentCauseReportItemService.GetItems(report.AccidentCauseReportId);
                    Grid1.DataSource = items;
                    Grid1.DataBind();
                }
            }
            else
            {
                SetEmpty();
            }
            this.GetButtonPower();
        }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
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

        #region 增加、修改、审核、审批、上报、删除
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportSave.aspx?year={0}&&month={1}",this.drpYear.SelectedValue,this.drpMonth.SelectedValue, "编辑 - ")));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportSave.aspx?AccidentCauseReportId={0}", report.AccidentCauseReportId, "编辑 - ")));
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportSave.aspx?AccidentCauseReportId={0}", report.AccidentCauseReportId, "编辑 - ")));
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportSave.aspx?AccidentCauseReportId={0}", report.AccidentCauseReportId, "编辑 - ")));
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("AccidentCauseReportSave.aspx?AccidentCauseReportId={0}", report.AccidentCauseReportId, "编辑 - ")));
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                BLL.ProjectDataFlowSetService.DeleteFlowSetByDataId(report.AccidentCauseReportId);
                BLL.AccidentCauseReportItemService.DeleteAccidentCauseReportItemByAccidentCauseReportId(report.AccidentCauseReportId);
                BLL.AccidentCauseReportService.DeleteAccidentCauseReportByAccidentCauseReportId(report.AccidentCauseReportId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除职工伤亡事故原因分析报表");
                SetEmpty();
                this.btnNew.Hidden = false;
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("所选时间无报表记录！", MessageBoxIcon.Warning);
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
        protected string ConvertDate(object AccidentCauseReportId)
        {
            if (AccidentCauseReportId != null)
            {
                Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(AccidentCauseReportId.ToString());
                if (report != null)
                {
                    return report.Year + "年" + report.Month + "月";
                }
            }
            return "";
        }
        #endregion

        #region 是否允许删除话题类型
        /// <summary>
        /// 是否允许删除话题类型
        /// </summary>
        /// <param name="contentTypeId"></param>
        /// <returns></returns>
        private bool IsAllowDeleteContentType(string contentTypeId)
        {
            return BLL.ContentService.IsExitContentType(contentTypeId);
        }
        #endregion

        #region 单位下来选择事件
        /// <summary>
        /// 单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetValue();
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCauseReportMenuId);
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
                int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);
                int monthId = Funs.GetNewIntOrZero(drpMonth.SelectedValue);
                Model.Information_AccidentCauseReport report = Funs.DB.Information_AccidentCauseReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.Month == monthId && e.Year == year);
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
                    if (report.UpState == BLL.Const.UpState_3)
                    {
                        this.btnUpdata.Hidden = true;
                        this.btnEdit.Hidden = true;                      
                    }

                    if (report.HandleMan == this.CurrUser.UserId || report.FillCompanyPersonCharge == this.CurrUser.UserName || report.TabPeople == this.CurrUser.UserName || report.AuditPerson == this.CurrUser.UserName)
                    {
                        this.btnEdit.Hidden = false;
                    }
                    else
                    {
                        this.btnEdit.Hidden = true;
                    }
                }
            }

            //if (this.CurrUser.UserId == BLL.Const.sysglyId)
            //{
            //    this.btnDelete.Hidden = false;
            //}
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../DataIn/AccidentCauseReportImport.aspx", "导入 - ")));
        }

        /// <summary>
        /// 关闭导入弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            GetValue();
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.Information_AccidentCauseReportId, report.AccidentCauseReportId, "", "打印 - ")));
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
            Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpMonth.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("ReportAuditSee.aspx?Id={0}", report.AccidentCauseReportId, "查看 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("职工伤亡事故原因分析报" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();

        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
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
                {
                    int rowspan = Convert.ToInt32(cell[0]);
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
                    //if (column.ColumnID == "tfNumber")
                    //{
                    //    html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    //}
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
    }
}