using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class HSSELogView : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                              
                this.HSSELogId = Request.Params["HSSELogId"];
                if (!string.IsNullOrEmpty(this.HSSELogId))
                {
                    Model.Manager_HSSELog getHSSELog = BLL.HSSELogService.GetHSSELogByHSSELogId(this.HSSELogId);
                    if (getHSSELog != null)
                    {
                        this.InitText(getHSSELog);
                    }
                }             
            }
        }
        #endregion

        #region 初始化页面信息
        /// <summary>
        /// 初始话页面输入框
        /// </summary>
        /// <param name="getHSSELog"></param>
        private void InitText(Model.Manager_HSSELog getHSSELog)
        {            
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", getHSSELog.CompileDate);
            this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(getHSSELog.CompileMan);
            var constText =BLL.ConstValue.GetConstByConstValueAndGroupId(getHSSELog.Weather, BLL.ConstValue.Group_Weather);
            if(constText != null)
            {
                this.drpWeather.Text = constText.ConstText;
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
        #endregion        
    }
}