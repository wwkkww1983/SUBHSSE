using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayXAHandleEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckDayId
        {
            get
            {
                return (string)ViewState["CheckDayId"];
            }
            set
            {
                ViewState["CheckDayId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.CheckDayId = Request.Params["CheckDayId"];
                BLL.ConstValue.InitConstValueRadioButtonList(this.rblIsOK, BLL.ConstValue.Group_0001, string.Empty);
                var checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.ProjectId = checkDay.ProjectId;
                    this.txtHandleStation.Text = checkDay.HandleStation;
                    if (checkDay.IsOK == true)
                    {
                        this.rblIsOK.SelectedValue = "True";
                    }
                    if (checkDay.IsOK == false)
                    {
                        this.rblIsOK.SelectedValue = "False";
                    }
                }
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rblIsOK.SelectedValue))
            {
                ShowNotify("请选择是否闭环！", MessageBoxIcon.Warning);
                return;
            }
            Model.Check_CheckDayXA checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(this.CheckDayId);
            if (checkDay != null)
            {
                checkDay.HandleStation = this.txtHandleStation.Text.Trim();
                checkDay.IsOK = Convert.ToBoolean(this.rblIsOK.SelectedValue);
                BLL.Check_CheckDayXAService.UpdateCheckDay(checkDay);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDayXA&menuId={1}", this.CheckDayId+"1", BLL.Const.ProjectCheckDayXAMenuId)));
        }
        #endregion
    }
}