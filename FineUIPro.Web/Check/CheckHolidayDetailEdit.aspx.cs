using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckHolidayDetailEdit : PageBase
    { 
        #region 定义项
        /// <summary>
        /// 季节性/节假日检查明细id
        /// </summary>
        public string CheckHolidayDetailId
        {
            get
            {
                return (string)ViewState["CheckHolidayDetailId"];
            }
            set
            {
                ViewState["CheckHolidayDetailId"] = value;
            }
        }        
        #endregion

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                this.CheckHolidayDetailId = Request.Params["CheckHolidayDetailId"];
                if (!string.IsNullOrEmpty(this.CheckHolidayDetailId))
                {
                    var checkHolidayDetail = BLL.Check_CheckHolidayDetailService.GetCheckHolidayDetailByCheckHolidayDetailId(this.CheckHolidayDetailId);
                    if (checkHolidayDetail != null)
                    {
                        this.txtCheckItemType.Text = BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(checkHolidayDetail.CheckItem);
                        this.txtCheckItem.Text = checkHolidayDetail.CheckContent;
                        this.txtCheckResult.Text = checkHolidayDetail.CheckResult;
                        this.txtCheckOpinion.Text = checkHolidayDetail.CheckOpinion;
                        this.txtHandleResult.Text = checkHolidayDetail.HandleResult;
                        this.txtCheckStation.Text = checkHolidayDetail.CheckStation;
                    }
                }
            }
        }


        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Check_CheckHolidayDetail detail = BLL.Check_CheckHolidayDetailService.GetCheckHolidayDetailByCheckHolidayDetailId(this.CheckHolidayDetailId);
            if (detail != null)
            {
                detail.CheckResult = this.txtCheckResult.Text.Trim();
                detail.CheckOpinion = this.txtCheckOpinion.Text.Trim();
                detail.HandleResult = this.txtHandleResult.Text.Trim();
                detail.CheckStation = this.txtCheckStation.Text.Trim();
                detail.CheckContent = this.txtCheckItem.Text.Trim();
                BLL.Check_CheckHolidayDetailService.UpdateCheckHolidayDetail(detail);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckHoliday&menuId={1}", this.CheckHolidayDetailId, BLL.Const.ProjectCheckHolidayMenuId)));
        }
        #endregion
    }
}