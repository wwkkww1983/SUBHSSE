namespace FineUIPro.Web.Manager
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class HSEDiaryView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string HSEDiaryId
        {
            get
            {
                return (string)ViewState["HSEDiaryId"];
            }
            set
            {
                ViewState["HSEDiaryId"] = value;
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
                this.HSEDiaryId = Request.Params["HSEDiaryId"];
                if (!string.IsNullOrEmpty(this.HSEDiaryId))
                {
                    var getHSEDiary = Funs.DB.Project_HSEDiary.FirstOrDefault(x => x.HSEDiaryId == this.HSEDiaryId);
                    if (getHSEDiary != null)
                    {                      
                        this.txtDiaryDate.Text = string.Format("{0:yyyy-MM-dd}", getHSEDiary.DiaryDate);
                        this.txtDailySummary.Text = getHSEDiary.DailySummary;
                        this.txtTomorrowPlan.Text = getHSEDiary.TomorrowPlan;
                        this.txtUserName.Text = UserService.GetUserNameByUserId(getHSEDiary.UserId);
                        if (getHSEDiary.DiaryDate.HasValue && !string.IsNullOrEmpty(getHSEDiary.UserId))
                        {
                            SetItem(getHSEDiary.UserId, getHSEDiary.DiaryDate.Value);
                        }
                    }
                }
            }
        }
        #endregion

        #region 获取审核记录信息
        /// <summary>
        /// 
        /// </summary>
        private void SetItem(string userId,DateTime diaryDate)
        {           
        }
        #endregion
    }
}