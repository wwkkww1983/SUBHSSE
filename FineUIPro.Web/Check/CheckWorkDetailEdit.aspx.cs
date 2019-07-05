using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckWorkDetailEdit : PageBase
    {
        /// <summary>
        /// 开工前检查明细id
        /// </summary>
        public string CheckWorkDetailId
        {
            get
            {
                return (string)ViewState["CheckWorkDetailId"];
            }
            set
            {
                ViewState["CheckWorkDetailId"] = value;
            }
        }

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
                this.CheckWorkDetailId = Request.Params["CheckWorkDetailId"];
                if (!string.IsNullOrEmpty(this.CheckWorkDetailId))
                {
                    var checkWorkDetail = BLL.Check_CheckWorkDetailService.GetCheckWorkDetailByCheckWorkDetailId(this.CheckWorkDetailId);
                    if (checkWorkDetail != null)
                    {
                        this.txtCheckItemType.Text = BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(checkWorkDetail.CheckItem);
                        Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(checkWorkDetail.CheckItem);
                        if (checkItemDetail != null)
                        {
                            this.txtCheckItem.Text = checkItemDetail.CheckContent;                            
                        }
                        this.txtCheckItem.Text = checkWorkDetail.CheckContent;
                        this.txtCheckResult.Text = checkWorkDetail.CheckResult;
                        this.txtCheckOpinion.Text = checkWorkDetail.CheckOpinion;
                        this.txtHandleResult.Text = checkWorkDetail.HandleResult;
                        this.txtCheckStation.Text = checkWorkDetail.CheckStation;
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
            Model.Check_CheckWorkDetail detail = BLL.Check_CheckWorkDetailService.GetCheckWorkDetailByCheckWorkDetailId(this.CheckWorkDetailId);
            if (detail != null)
            {
                detail.CheckResult = this.txtCheckResult.Text.Trim();
                detail.CheckOpinion = this.txtCheckOpinion.Text.Trim();
                detail.HandleResult = this.txtHandleResult.Text.Trim();
                detail.CheckStation = this.txtCheckStation.Text.Trim();
                detail.CheckContent = this.txtCheckItem.Text.Trim();
                BLL.Check_CheckWorkDetailService.UpdateCheckWorkDetail(detail);
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckWork&menuId={1}", this.CheckWorkDetailId, BLL.Const.ProjectCheckWorkMenuId)));
        }
        #endregion
    }
}