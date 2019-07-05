using System;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Manager
{
    public partial class HSSELogEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string HSSELogId
        {
            get
            {
                return (string)ViewState["HSSELogId"];
            }
            set
            {
                ViewState["HSSELogId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();                
                this.HSSELogId = Request.Params["HSSELogId"];
                if (!string.IsNullOrEmpty(this.HSSELogId))
                {
                    Model.Manager_HSSELog getHSSELog = BLL.HSSELogService.GetHSSELogByHSSELogId(this.HSSELogId);
                    if (getHSSELog != null)
                    {
                        this.InitText(getHSSELog);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                   
                }

                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.drpCompileMan.Enabled = true;
                }
            }
        }
        #endregion

        #region 日期变化
        /// <summary>
        /// 日期变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.CleanText();
            DateTime? date = Funs.GetNewDateTime(this.txtCompileDate.Text);
            if (date.HasValue)
            {
                var log = BLL.HSSELogService.GetHSSELogByCompileManDateProjectId(this.ProjectId, date.Value, this.drpCompileMan.SelectedValue);
                if (log != null)
                {
                    this.HSSELogId = log.HSSELogId;
                    this.InitText(log);
                }
            }
        }
        #endregion

        #region 初始化页面信息
        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, false);
            BLL.ConstValue.InitConstValueDropDownList(this.drpWeather, BLL.ConstValue.Group_Weather, true);
        }

        /// <summary>
        /// 初始话页面输入框
        /// </summary>
        /// <param name="getHSSELog"></param>
        private void InitText(Model.Manager_HSSELog getHSSELog)
        {
            this.ProjectId = getHSSELog.ProjectId;
            if (this.ProjectId != this.CurrUser.LoginProjectId)
            {
                this.InitDropDownList();
            }
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", getHSSELog.CompileDate);
            if (!string.IsNullOrEmpty(getHSSELog.CompileMan))
            {
                this.drpCompileMan.SelectedValue = getHSSELog.CompileMan;
            }
            if (!string.IsNullOrEmpty(getHSSELog.Weather))
            {
                this.drpWeather.SelectedValue = getHSSELog.Weather;
            }
            this.txtNum11.Text = getHSSELog.Num11.ToString();
            this.txtContents12.Text = getHSSELog.Contents12;
            this.txtContents13.Text = getHSSELog.Contents13;
            this.txtContents21.Text = getHSSELog.Contents21;
            this.txtNum21.Text = getHSSELog.Num21.ToString();
            this.txtContents22.Text = getHSSELog.Contents22;
            this.txtNum22.Text = getHSSELog.Num22.ToString();
            this.txtContents23.Text = getHSSELog.Contents23;
            this.txtNum23.Text = getHSSELog.Num23.ToString();
            this.txtContents24.Text = getHSSELog.Contents24;
            this.txtNum24.Text = getHSSELog.Num24.ToString();
            this.txtContents25.Text = getHSSELog.Contents25;
            this.txtNum25.Text = getHSSELog.Num25.ToString();
            this.txtContents26.Text = getHSSELog.Contents26;
            this.txtNum26.Text = getHSSELog.Num26.ToString();
            this.txtContents27.Text = getHSSELog.Contents27;
            this.txtNum27.Text = getHSSELog.Num27.ToString();
            this.txtContents28.Text = getHSSELog.Contents28;
            this.txtNum28.Text = getHSSELog.Num28.ToString();
            this.txtContents29.Text = getHSSELog.Contents29;
            this.txtNum29.Text = getHSSELog.Num29.ToString();
            this.txtContents210.Text = getHSSELog.Contents210;
            this.txtNum210.Text = getHSSELog.Num210.ToString();
            this.txtNum211.Text = getHSSELog.Num211.ToString();
            this.txtContents31.Text = getHSSELog.Contents31;
            this.txtNum31.Text = getHSSELog.Num31.ToString();
            this.txtContents32.Text = getHSSELog.Contents32;
            this.txtNum32.Text = getHSSELog.Num32.ToString();
            this.txtContents33.Text = getHSSELog.Contents33;
            this.txtNum33.Text = getHSSELog.Num33.ToString();
            this.txtNum34.Text = getHSSELog.Num34.ToString();
            this.txtContents41.Text = getHSSELog.Contents41;
            this.txtContents42.Text = getHSSELog.Contents42;
            this.txtContents43.Text = getHSSELog.Contents43;
            this.txtContents51.Text = getHSSELog.Contents51;
            this.txtContents52.Text = getHSSELog.Contents52;
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void CleanText()
        {
            this.HSSELogId = string.Empty;
            this.drpWeather.SelectedIndex = 0;               
            if (this.ProjectId != this.CurrUser.LoginProjectId)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
            }

            this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
            this.txtNum11.Text = string.Empty;
            this.txtContents12.Text = string.Empty;
            this.txtContents13.Text = string.Empty;
            this.txtContents21.Text = string.Empty;
            this.txtNum21.Text = string.Empty;
            this.txtContents22.Text = string.Empty;
            this.txtNum22.Text = string.Empty;
            this.txtContents23.Text = string.Empty;
            this.txtNum23.Text = string.Empty;
            this.txtContents24.Text = string.Empty;
            this.txtNum24.Text = string.Empty;
            this.txtContents25.Text = string.Empty;
            this.txtNum25.Text = string.Empty;
            this.txtContents26.Text = string.Empty;
            this.txtNum26.Text = string.Empty;
            this.txtContents27.Text = string.Empty;
            this.txtNum27.Text = string.Empty;
            this.txtContents28.Text = string.Empty;
            this.txtNum28.Text = string.Empty;
            this.txtContents29.Text = string.Empty;
            this.txtNum29.Text = string.Empty;
            this.txtContents210.Text = string.Empty;
            this.txtNum210.Text = string.Empty;
            this.txtNum211.Text = string.Empty;
            this.txtContents31.Text = string.Empty;
            this.txtNum31.Text = string.Empty;
            this.txtContents32.Text = string.Empty;
            this.txtNum32.Text = string.Empty;
            this.txtContents33.Text = string.Empty;
            this.txtNum33.Text = string.Empty;
            this.txtNum34.Text = string.Empty;
            this.txtContents41.Text = string.Empty;
            this.txtContents42.Text = string.Empty;
            this.txtContents43.Text = string.Empty;
            this.txtContents51.Text = string.Empty;
            this.txtContents52.Text = string.Empty; 
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {           
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData()
        {
            Model.Manager_HSSELog newHSSELog = new Model.Manager_HSSELog
            {
                ProjectId = this.ProjectId,
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text),
                CompileMan = this.drpCompileMan.SelectedValue
            };
            if (this.drpWeather.SelectedValue != BLL.Const._Null)
            {
                newHSSELog.Weather = this.drpWeather.SelectedValue;
            }
            newHSSELog.Num11 = Funs.GetNewInt(this.txtNum11.Text.Trim());
            newHSSELog.Contents12 = this.txtContents12.Text.Trim();
            newHSSELog.Contents13 = this.txtContents13.Text.Trim();
            newHSSELog.Contents21 = this.txtContents21.Text.Trim();
            newHSSELog.Num21 = Funs.GetNewInt(this.txtNum21.Text.Trim());
            newHSSELog.Contents22 = this.txtContents22.Text.Trim();
            newHSSELog.Num22 = Funs.GetNewInt(this.txtNum22.Text.Trim());
            newHSSELog.Contents23 = this.txtContents23.Text.Trim();
            newHSSELog.Num23 = Funs.GetNewInt(this.txtNum23.Text.Trim());
            newHSSELog.Contents24 = this.txtContents24.Text.Trim();
            newHSSELog.Num24 = Funs.GetNewInt(this.txtNum24.Text.Trim());
            newHSSELog.Contents25 = this.txtContents25.Text.Trim();
            newHSSELog.Num25 = Funs.GetNewInt(this.txtNum25.Text.Trim());
            newHSSELog.Contents26 = this.txtContents26.Text.Trim();
            newHSSELog.Num26 = Funs.GetNewInt(this.txtNum26.Text.Trim());
            newHSSELog.Contents27 = this.txtContents27.Text.Trim();
            newHSSELog.Num27 = Funs.GetNewInt(this.txtNum27.Text.Trim());
            newHSSELog.Contents28 = this.txtContents28.Text.Trim();
            newHSSELog.Num28 = Funs.GetNewInt(this.txtNum28.Text.Trim());
            newHSSELog.Contents29 = this.txtContents29.Text.Trim();
            newHSSELog.Num29 = Funs.GetNewInt(this.txtNum29.Text.Trim());
            newHSSELog.Contents210 = this.txtContents210.Text.Trim();
            newHSSELog.Num210 = Funs.GetNewInt(this.txtNum210.Text.Trim());
            newHSSELog.Num211 = Funs.GetNewInt(this.txtNum211.Text.Trim());
            newHSSELog.Contents31 = this.txtContents31.Text.Trim();
            newHSSELog.Num31 = Funs.GetNewInt(this.txtNum31.Text.Trim());
            newHSSELog.Contents32 = this.txtContents32.Text.Trim();
            newHSSELog.Num32 = Funs.GetNewInt(this.txtNum32.Text.Trim());
            newHSSELog.Contents33 = this.txtContents33.Text.Trim();
            newHSSELog.Num33 = Funs.GetNewInt(this.txtNum33.Text.Trim());
            newHSSELog.Num34 = Funs.GetNewInt(this.txtNum34.Text.Trim());
            newHSSELog.Contents41 = this.txtContents41.Text.Trim();
            newHSSELog.Contents42 = this.txtContents42.Text.Trim();
            newHSSELog.Contents43 = this.txtContents43.Text.Trim();
            newHSSELog.Contents51 = this.txtContents51.Text.Trim();
            newHSSELog.Contents52 = this.txtContents52.Text.Trim();
            newHSSELog.IsVisible = true;   
            if (!string.IsNullOrEmpty(this.HSSELogId))
            {
                newHSSELog.HSSELogId = this.HSSELogId;
                BLL.HSSELogService.UpdateHSSELog(newHSSELog);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改HSSE日志暨管理数据收集", newHSSELog.HSSELogId);
            }
            else
            {
                this.HSSELogId = SQLHelper.GetNewID(typeof(Model.Manager_HSSELog));
                newHSSELog.HSSELogId = this.HSSELogId;
                BLL.HSSELogService.AddHSSELog(newHSSELog);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加HSSE日志暨管理数据收集", newHSSELog.HSSELogId);
            }
        }
        #endregion               
    }
}