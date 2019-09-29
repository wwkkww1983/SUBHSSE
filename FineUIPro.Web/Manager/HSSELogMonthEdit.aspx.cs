using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class HSSELogMonthEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSSELogMonthId
        {
            get
            {
                return (string)ViewState["HSSELogMonthId"];
            }
            set
            {
                ViewState["HSSELogMonthId"] = value;
            }
        }

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

        #region 加载
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
                if (unit != null && !string.IsNullOrEmpty(unit.UnitCode))
                {
                    string url = "../Images/SUBimages/" + unit.UnitCode + ".gif";
                    if (url.Contains('*'))
                    {
                        url = url.Replace('*', '-');
                    }
                    this.Image1.ImageUrl = url;
                }

                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.HSSELogMonthId = Request.Params["HSSELogMonthId"]; 
                Model.Manager_HSSELogMonth hsseLogMonth = BLL.HSSELogMonthService.GetHSSELogMonthByHSSELogMonthId(this.HSSELogMonthId);
                if (hsseLogMonth != null)
                {
                    this.InitText(hsseLogMonth);                     
                }
                else
                {
                    this.txtMonths.Text = string.Format("{0:yyyy-MM}", DateTime.Now); 
                    this.CleanText();
                }
            }
        }

        /// <summary>
        ///  设置页面项目信息
        /// </summary>
        private void SetProject()
        {
            var project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
            if (project != null)
            {
                this.lblProjectName.Text = project.ProjectName;
                this.lblProjectCode.Text = project.ProjectCode;
                this.txtProjectName.Text = project.ProjectName;
            }
        }
        #endregion

        /// <summary>
        /// 清空当前页面值
        /// </summary>
        private void CleanText()
        {
            this.HSSELogMonthId = string.Empty;
            this.ProjectId = this.CurrUser.LoginProjectId;
            this.SetProject();
            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.lbHSSELogMonthCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectAccidentReportMenuId, this.ProjectId, this.CurrUser.UnitId);
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            string monthValue =  Funs.GetNewDateTimeOrNow(this.txtMonths.Text).GetDateTimeFormats('y')[0]; ///页面当前年月字符串
            this.lbTitleName.Text = monthValue + "HSE经理暨HSE工程师细则";

            this.txtProjectRange.Text = string.Empty;
            this.txtRate.Text = "1";

            this.hdTotalManHour.Text = string.Empty;
            this.txtManHour.Text = "0";
            this.ChangedValueManHour();
            this.txtNum1.Text = string.Empty;
            this.txtNum2.Text = string.Empty;
            this.txtNum3.Text = string.Empty;
            this.txtNum4.Text = string.Empty;
            this.txtNum5.Text = string.Empty;
            this.txtNum6.Text = string.Empty;
            this.txtNum7.Text = string.Empty;
            this.txtNum8.Text = string.Empty;
            this.txtNum9.Text = string.Empty;
            this.txtNum10.Text = string.Empty;
            this.txtNum11.Text = string.Empty;
            this.txtNum12.Text = string.Empty;
            this.txtNum13.Text = string.Empty;
            this.txtNum14.Text = string.Empty;
            this.txtNum15.Text = string.Empty;

            ///页面当前年月
            DateTime monthTime =Funs.GetNewDateTimeOrNow(monthValue) ;
            DateTime startTime = (monthTime.AddMonths(-1)).AddDays(25); ///取值的开始时间
            DateTime endTime = monthTime.AddDays(24); ///取值的结束时间 
            if (monthTime.Month != 1)
            {
                /// 上一个月年度累计工时
                var hsseLogMonth = BLL.HSSELogMonthService.GetHSSELogMonthByCompileManDateProjectId(this.ProjectId, monthTime.AddMonths(-1), this.CurrUser.UserId);
                if (hsseLogMonth != null)
                {
                    this.hdTotalManHour.Text = hsseLogMonth.ManHour.ToString();
                }
            }

            var hsseLogs = BLL.HSSELogService.GetHSSELogListByCompileManDatesProjectId(this.ProjectId, startTime, endTime, this.CurrUser.UserId);
            if (hsseLogs.Count() > 0)
            {
                this.txtManHour.Text = hsseLogs.Sum(x=>x.Num11).ToString();
                this.ChangedValueManHour();
                this.txtNum1.Text = hsseLogs.Sum(x => x.Num21).ToString();
                this.txtNum2.Text = hsseLogs.Sum(x => x.Num22).ToString();
                this.txtNum3.Text = hsseLogs.Sum(x => x.Num23).ToString();
                this.txtNum4.Text = hsseLogs.Sum(x => x.Num24).ToString();
                this.txtNum5.Text = hsseLogs.Sum(x => x.Num25).ToString();
                this.txtNum6.Text = hsseLogs.Sum(x => x.Num26).ToString();
                this.txtNum7.Text = hsseLogs.Sum(x => x.Num27).ToString();
                this.txtNum8.Text = hsseLogs.Sum(x => x.Num28).ToString();
                this.txtNum9.Text = hsseLogs.Sum(x => x.Num29).ToString();
                this.txtNum10.Text = hsseLogs.Sum(x => x.Num210).ToString();
                this.txtNum11.Text = hsseLogs.Sum(x => x.Num211).ToString();
                this.txtNum12.Text = hsseLogs.Sum(x => x.Num31).ToString();
                this.txtNum13.Text = hsseLogs.Sum(x => x.Num32).ToString();
                this.txtNum14.Text = hsseLogs.Sum(x => x.Num33).ToString();
                this.txtNum15.Text = hsseLogs.Sum(x => x.Num34).ToString();
            }
        }

        /// <summary>
        /// 初始页面信息
        /// </summary>
        private void InitText(Model.Manager_HSSELogMonth hsseLogMonth)
        {
            this.ProjectId = hsseLogMonth.ProjectId;
            this.SetProject();  //设置页面项目信息
            this.HSSELogMonthId = hsseLogMonth.HSSELogMonthId;
            this.lbHSSELogMonthCode.Text = hsseLogMonth.HSSELogMonthCode;
            this.txtMonths.Text = string.Format("{0:yyyy-MM}", hsseLogMonth.Months);
            this.lbHSSELogMonthCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.HSSELogMonthId);
            this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(hsseLogMonth.CompileMan);
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", hsseLogMonth.CompileDate);
            this.lbTitleName.Text = hsseLogMonth.Months.Value.GetDateTimeFormats('y')[0].ToString() + "HSE经理暨HSE工程师细则";

            this.txtProjectRange.Text = hsseLogMonth.ProjectRange;
            this.txtManHour.Text = hsseLogMonth.ManHour.ToString();
            this.txtRate.Text = hsseLogMonth.Rate.ToString();
            this.txtRealManHour.Text = hsseLogMonth.RealManHour.ToString();
            this.txtTotalManHour.Text = hsseLogMonth.TotalManHour.ToString();
            this.txtNum1.Text = hsseLogMonth.Num1.ToString();
            this.txtNum2.Text = hsseLogMonth.Num2.ToString();
            this.txtNum3.Text = hsseLogMonth.Num3.ToString();
            this.txtNum4.Text = hsseLogMonth.Num4.ToString();
            this.txtNum5.Text = hsseLogMonth.Num5.ToString();
            this.txtNum6.Text = hsseLogMonth.Num6.ToString();
            this.txtNum7.Text = hsseLogMonth.Num7.ToString();
            this.txtNum8.Text = hsseLogMonth.Num8.ToString();
            this.txtNum9.Text = hsseLogMonth.Num9.ToString();
            this.txtNum10.Text = hsseLogMonth.Num10.ToString();
            this.txtNum11.Text = hsseLogMonth.Num11.ToString();
            this.txtNum12.Text = hsseLogMonth.Num12.ToString();
            this.txtNum13.Text = hsseLogMonth.Num13.ToString();
            this.txtNum14.Text = hsseLogMonth.Num14.ToString();
            this.txtNum15.Text = hsseLogMonth.Num15.ToString();
        }

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var hsseLogMonth = BLL.HSSELogMonthService.GetHSSELogMonthByCompileManDateProjectId(this.ProjectId,Funs.GetNewDateTime(this.txtMonths.Text), this.CurrUser.UserId);
            if (hsseLogMonth != null)
            {
                this.InitText(hsseLogMonth);
            }
            else
            {
                this.CleanText();
            }
        }
        #endregion

        #region 人工日统计 输入框事件
        /// <summary>
        /// 人工日统计 输入框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Value_TextChanged(object sender, EventArgs e)
        {
            this.ChangedValueManHour();
        }

        /// <summary>
        /// 变化事件
        /// </summary>
        private void ChangedValueManHour()
        {
            int manHour = Funs.GetNewIntOrZero(this.txtManHour.Text);
            decimal rale = Funs.GetNewDecimalOrZero(this.txtRate.Text);
            this.txtRealManHour.Text = (manHour * rale).ToString("f2");
            this.txtTotalManHour.Text = (Funs.GetNewDecimalOrZero(this.txtRealManHour.Text) + Funs.GetNewDecimalOrZero(this.hdTotalManHour.Text)).ToString();
        }

        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="type"></param>
        private void SaveData()
        {
            Model.Manager_HSSELogMonth hsseLogMonth = new Model.Manager_HSSELogMonth
            {
                ProjectId = this.ProjectId,
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text),
                HSSELogMonthCode = this.lbHSSELogMonthCode.Text.Trim(),
                Num1 = Funs.GetNewInt(this.txtNum1.Text.Trim()),
                Num2 = Funs.GetNewInt(this.txtNum2.Text.Trim()),
                Num3 = Funs.GetNewInt(this.txtNum3.Text.Trim()),
                Num4 = Funs.GetNewInt(this.txtNum4.Text.Trim()),
                Num5 = Funs.GetNewInt(this.txtNum5.Text.Trim()),
                Num6 = Funs.GetNewInt(this.txtNum6.Text.Trim()),
                Num7 = Funs.GetNewInt(this.txtNum7.Text.Trim()),
                Num8 = Funs.GetNewInt(this.txtNum8.Text.Trim()),
                Num9 = Funs.GetNewInt(this.txtNum9.Text.Trim()),
                Num10 = Funs.GetNewInt(this.txtNum10.Text.Trim()),
                Num11 = Funs.GetNewInt(this.txtNum11.Text.Trim()),
                Num12 = Funs.GetNewInt(this.txtNum12.Text.Trim()),
                Num13 = Funs.GetNewInt(this.txtNum13.Text.Trim()),
                Num14 = Funs.GetNewInt(this.txtNum14.Text.Trim()),
                Num15 = Funs.GetNewInt(this.txtNum15.Text.Trim())
            };
            if (!string.IsNullOrEmpty(this.HSSELogMonthId))
            {  
                hsseLogMonth.HSSELogMonthId = this.HSSELogMonthId;
                BLL.HSSELogMonthService.UpdateHSSELogMonth(hsseLogMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseLogMonth.HSSELogMonthCode, hsseLogMonth.HSSELogMonthId, BLL.Const.ProjectHSSELogMonthMenuId, BLL.Const.BtnModify);
            }
            else
            {
                hsseLogMonth.Months = Funs.GetNewDateTime(this.txtMonths.Text);
                hsseLogMonth.CompileMan = this.CurrUser.UserId;
                this.HSSELogMonthId = SQLHelper.GetNewID(typeof(Model.Manager_HSSELogMonth));
                hsseLogMonth.HSSELogMonthId = this.HSSELogMonthId;
                BLL.HSSELogMonthService.AddHSSELogMonth(hsseLogMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, hsseLogMonth.HSSELogMonthCode, hsseLogMonth.HSSELogMonthId,BLL.Const.ProjectHSSELogMonthMenuId,BLL.Const.BtnAdd);
            }            
        }
        #endregion
    }
}
