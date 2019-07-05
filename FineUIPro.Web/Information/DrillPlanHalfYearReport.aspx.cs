using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Information
{
    public partial class DrillPlanHalfYearReport : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillPlanHalfYearReportId
        {
            get
            {
                return (string)ViewState["DrillPlanHalfYearReportId"];
            }
            set
            {
                ViewState["DrillPlanHalfYearReportId"] = value;
            }
        }
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpHalfYear.DataTextField = "ConstText";
                drpHalfYear.DataValueField = "ConstValue";
                drpHalfYear.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0010);
                drpHalfYear.DataBind();

                this.drpYear.DataTextField = "ConstText";
                drpYear.DataValueField = "ConstValue";
                drpYear.DataSource = BLL.ConstValue.drpConstItemList(ConstValue.Group_0008);
                drpYear.DataBind();

                this.drpUnit.DataTextField = "UnitName";
                drpUnit.DataValueField = "UnitId";
                drpUnit.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                drpUnit.DataBind();
                this.drpUnit.Readonly = true;

                int lastYear = 0, lastHalfYear = 0;
                int year = DateTime.Now.Year;
                int halfYear = Funs.GetNowHalfYearByTime(DateTime.Now);
                if (halfYear == 1)
                {
                    lastYear = year - 1;
                    lastHalfYear = 2;
                }
                else
                {
                    lastYear = year;
                    lastHalfYear = halfYear - 1;
                }
                this.drpYear.SelectedValue = lastYear.ToString();
                this.drpHalfYear.SelectedValue = lastHalfYear.ToString();

                GetValue();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
            }
        }
        #endregion

        #region 清空文本
        private void SetEmpty()
        {
            SimpleForm1.Title = string.Empty;
            txtUnitName.Text = string.Empty;
            txtCompileMan.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtCompileDate.Text = string.Empty;
            lbHandleMan.Text = string.Empty;
            Grid1.DataSource = null;
            Grid1.DataBind();
        }
        #endregion

        #region 获取记录值
        private void GetValue()
        {

            int year = Funs.GetNewIntOrZero(drpYear.SelectedValue);

            int halfYear = Funs.GetNewIntOrZero(drpHalfYear.SelectedValue);
            Model.View_Information_DrillPlanHalfYearReport report = Funs.DB.View_Information_DrillPlanHalfYearReport.FirstOrDefault(e => e.UnitId == drpUnit.SelectedValue && e.HalfYearId == halfYear && e.YearId == year);
            if (report != null)
            {
                string state = string.Empty;
                if (report.UpState == BLL.Const.UpState_3)
                {
                    state = "(已上报)";
                }
                else
                {
                    if (report.HandleState == BLL.Const.HandleState_1)
                    {
                        state = "(待提交)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_2)
                    {
                        state = "(待审核)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_3)
                    {
                        state = "(待审批)";
                    }
                    else if (report.HandleState == BLL.Const.HandleState_4)
                    {
                        state = "(待上报)";
                    }
                }
                this.SimpleForm1.Title = "应急演练工作计划半年报表" + state;
                if (report.HandleState == BLL.Const.HandleState_1 || report.UpState == BLL.Const.UpState_3)
                {
                    this.lbHandleMan.Hidden = true;
                }
                else
                {
                    this.lbHandleMan.Hidden = false;
                    var user = BLL.UserService.GetUserByUserId(report.HandleMan);
                    if (user != null)
                    {
                        this.lbHandleMan.Text = "下一步办理人：" + user.UserName;
                    }
                }
                this.txtUnitName.Text = report.UnitName;
                this.txtCompileMan.Text = report.CompileMan;
                this.txtTel.Text = report.Telephone;
                if (report.CompileDate != null)
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", report.CompileDate);
                }
                DrillPlanHalfYearReportId = report.DrillPlanHalfYearReportId;
                BindGrid1();
            }
            else
            {
                SetEmpty();
            }
            this.GetButtonPower();
        }
        #endregion

        #region 加载Grid1
        /// <summary>
        /// 加载Grid1
        /// </summary>
        private void BindGrid1()
        {
            if (!string.IsNullOrEmpty(DrillPlanHalfYearReportId))
            {
                string strSql = "select * from dbo.Information_DrillPlanHalfYearReportItem where DrillPlanHalfYearReportId = @DrillPlanHalfYearReportId order by SortIndex";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@DrillPlanHalfYearReportId",DrillPlanHalfYearReportId),
                    };
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 过滤、分页、排序
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        /// <summary>
        /// 分页列表显示条数下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid1();
        }
        #endregion

        #region 增加、编辑、删除、审核、审批、上报按钮事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillPlanHalfYearReportAdd.aspx?UnitId={0}&&Year={1}&&HalfYear={2}", this.CurrUser.UnitId, this.drpYear.SelectedValue, this.drpHalfYear.SelectedValue, "编辑 - ")));
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit1_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit2_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdata_Click(object sender, EventArgs e)
        {
            ShowEdit();
        }

        /// <summary>
        /// 弹出编辑窗口
        /// </summary>
        private void ShowEdit()
        {
            Model.Information_DrillPlanHalfYearReport report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpHalfYear.SelectedValue));
            if (report == null)
            {
                Alert.ShowInTop("所选时间无报表记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("DrillPlanHalfYearReportAdd.aspx?DrillPlanHalfYearReportId={0}", report.DrillPlanHalfYearReportId, "编辑 - ")));
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Model.Information_DrillPlanHalfYearReport report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpHalfYear.SelectedValue));
            if (report != null)
            {
                string ID = report.DrillPlanHalfYearReportId;
                BLL.ProjectDataFlowSetService.DeleteFlowSetByDataId(ID);
                BLL.DrillPlanHalfYearReportItemService.DeleteDrillPlanHalfYearReportItemList(ID);
                BLL.DrillPlanHalfYearReportService.DeleteDrillPlanHalfYearReportById(ID);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除应急演练工作计划半年报表");
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

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.DrillPlanHalfYearReportMenuId);
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
                int year = Funs.GetNewIntOrZero(this.drpYear.SelectedValue);
                int halfYear = Funs.GetNewIntOrZero(this.drpHalfYear.SelectedValue);
                var report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(this.drpUnit.SelectedValue, year, halfYear);
                this.btnAudit1.Hidden = true;
                this.btnAudit2.Hidden = true;
                this.btnUpdata.Hidden = true;
                if (report!=null)
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
                            this.btnDelete.Hidden = true;                            
                            this.btnUpdata.Hidden = false;
                        }
                    }
                    if (report.UpState == BLL.Const.UpState_3)
                    {
                        this.btnUpdata.Hidden = true;
                        this.btnEdit.Hidden = true;
                        this.btnDelete.Hidden = true;
                    }
                    if (report.HandleMan == this.CurrUser.UserId || report.CompileMan == this.CurrUser.UserName)
                    {
                        this.btnEdit.Hidden = false;
                    }
                    else
                    {
                        this.btnEdit.Hidden = true;
                    }
                }
            }

            if (this.CurrUser.UserId == BLL.Const.sysglyId)
            {
                this.btnDelete.Hidden = false;
            }
        }
        #endregion

        #region 单位下拉框联动事件
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

        #region 导入
        /// <summary>
        /// 导入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("../DataIn/DrillPlanHalfYearReportImport.aspx", "导入 - ")));
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
            Model.Information_DrillPlanHalfYearReport report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpHalfYear.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("../ReportPrint/ExReportPrint.aspx?reportId={0}&&replaceParameter={1}&&varValue={2}", Const.Information_DrillPlanHalfYearReportId, report.DrillPlanHalfYearReportId, "", "打印 - ")));
            }
        }
        #endregion

        #region 半年向前/向后
        /// <summary>
        /// 前一半年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBulletLeft_Click(object sender, EventArgs e)
        {
            SetMonthChange("-");
        }

        /// <summary>
        /// 后一半年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulletRight_Click(object sender, EventArgs e)
        {
            SetMonthChange("+");
        }

        /// <summary>
        /// 半年加减变化
        /// </summary>
        /// <param name="type"></param>
        private void SetMonthChange(string type)
        {
            DateTime? nowDate = Funs.GetNewDateTime(this.drpYear.SelectedValue + "-" + (Funs.GetNewIntOrZero(this.drpHalfYear.SelectedValue) * 6).ToString());
            if (nowDate.HasValue)
            {
                DateTime showDate = new DateTime();
                if (type == "+")
                {
                    showDate = nowDate.Value.AddMonths(6);
                }
                else
                {
                    showDate = nowDate.Value.AddMonths(-6);
                }

                this.drpYear.SelectedValue = showDate.Year.ToString();
                this.drpHalfYear.SelectedValue = Funs.GetNowHalfYearByTime(showDate).ToString();
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
            Model.Information_DrillPlanHalfYearReport report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(drpUnit.SelectedValue, Funs.GetNewIntOrZero(drpYear.SelectedValue), Funs.GetNewIntOrZero(drpHalfYear.SelectedValue));
            if (report != null)
            {
                PageContext.RegisterStartupScript(Window4.GetShowReference(String.Format("ReportAuditSee.aspx?Id={0}", report.DrillPlanHalfYearReportId, "查看 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("应急演练工作计划半年报表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid1();
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
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
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
        #endregion        
    }
}