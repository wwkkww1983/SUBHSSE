using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class PunishNoticeView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PunishNoticeId
        {
            get
            {
                return (string)ViewState["PunishNoticeId"];
            }
            set
            {
                ViewState["PunishNoticeId"] = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        private string AttchUrl
        {
            get
            {
                return (string)ViewState["AttchUrl"];
            }
            set
            {
                ViewState["AttchUrl"] = value;
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
                
                this.PunishNoticeId = Request.Params["PunishNoticeId"];
                this.txtCurrency.Text = "人民币";
                if (!string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    Model.Check_PunishNotice punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(this.PunishNoticeId);
                    if (punishNotice != null)
                    {
                        this.txtPunishNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PunishNoticeId);
                        if (punishNotice.PunishNoticeDate != null)
                        {
                            this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", punishNotice.PunishNoticeDate);
                        }
                        if (!string.IsNullOrEmpty(punishNotice.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(punishNotice.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        Model.Sys_User user1 = BLL.UserService.GetUserByUserId(punishNotice.SignMan);
                        if (user1 != null)
                        {
                            this.txtSignMan.Text = user1.UserName;
                        }
                        Model.Sys_User user2 = BLL.UserService.GetUserByUserId(punishNotice.ApproveMan);
                        if (user2 != null)
                        {
                            this.txtApproveMan.Text = user2.UserName;
                        }
                        this.txtContractNum.Text = punishNotice.ContractNum;
                        this.txtIncentiveReason.Text = punishNotice.IncentiveReason;
                        this.txtBasicItem.Text = punishNotice.BasicItem;
                        if (punishNotice.PunishMoney != null)
                        {
                            this.txtPunishMoney.Text = Convert.ToString(punishNotice.PunishMoney);
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));//转换大写
                        }
                        this.AttchUrl = punishNotice.AttachUrl;
                        //this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttchUrl);
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(punishNotice.FileContents);
                        if (!string.IsNullOrEmpty(punishNotice.Currency))
                        {
                            this.txtCurrency.Text = punishNotice.Currency;
                        }
                        this.txtPunishName.Text = punishNotice.PunishName;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPunishNoticeMenuId;
                this.ctlAuditFlow.DataId = this.PunishNoticeId;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&type=-1&menuId=" + BLL.Const.ProjectPunishNoticeMenuId, this.PunishNoticeId)));
        }

        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&type=-1&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
        }
        #endregion
    }
}