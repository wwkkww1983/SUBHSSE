using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class PunishNoticeEdit : PageBase
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
        ///// <summary>
        ///// 附件
        ///// </summary>
        //private string AttchUrl
        //{
        //    get
        //    {
        //        return (string)ViewState["AttchUrl"];
        //    }
        //    set
        //    {
        //        ViewState["AttchUrl"] = value;
        //    }
        //}
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
                this.PunishNoticeId = Request.Params["PunishNoticeId"];
                this.txtCurrency.Text = "人民币";
                if (!string.IsNullOrEmpty(this.PunishNoticeId))
                {
                    Model.Check_PunishNotice punishNotice = BLL.PunishNoticeService.GetPunishNoticeById(this.PunishNoticeId);
                    if (punishNotice != null)
                    {
                        this.ProjectId = punishNotice.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPunishNoticeCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PunishNoticeId);
                        this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", punishNotice.PunishNoticeDate);
                        if (!string.IsNullOrEmpty(punishNotice.UnitId))
                        {
                            this.drpUnitId.SelectedValue = punishNotice.UnitId;
                        }
                        this.txtIncentiveReason.Text = punishNotice.IncentiveReason;
                        this.txtBasicItem.Text = punishNotice.BasicItem;
                        if (punishNotice.PunishMoney.HasValue)
                        {
                            this.txtPunishMoney.Text = Convert.ToString(punishNotice.PunishMoney);
                            this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
                        }
                        //this.AttchUrl = punishNotice.AttachUrl;
                        //this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttchUrl);
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(punishNotice.FileContents);
                        if (!string.IsNullOrEmpty(punishNotice.SignMan))
                        {
                            this.drpSignMan.SelectedValue = punishNotice.SignMan;
                        }
                        if (!string.IsNullOrEmpty(punishNotice.ApproveMan))
                        {
                            this.drpApproveMan.SelectedValue = punishNotice.ApproveMan;
                        }
                        this.txtContractNum.Text = punishNotice.ContractNum;
                        if (!string.IsNullOrEmpty(punishNotice.Currency))
                        {
                            this.txtCurrency.Text = punishNotice.Currency;
                        }

                        this.drpPunishName.SelectedValue = punishNotice.PunishName;
                    }
                }
                else
                {
                    this.drpSignMan.SelectedValue = this.CurrUser.UserId;
                    this.txtPunishNoticeDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectPunishNoticeMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                    ////自动生成编码
                    this.txtPunishNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectPunishNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectPunishNoticeMenuId;
                this.ctlAuditFlow.DataId = this.PunishNoticeId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {          
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, false);
            BLL.UserService.InitUserDropDownList(this.drpSignMan, this.ProjectId, true);
            BLL.UserService.InitUserDropDownList(this.drpApproveMan, this.ProjectId, true);
        }

        #region  获取大写金额事件
        /// <summary>
        /// 获取大写金额事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPunishMoney_Blur(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPunishMoney.Text))
            {
                this.txtBig.Text = Funs.NumericCapitalization(Funs.GetNewDecimalOrZero(txtPunishMoney.Text));
            }
            else
            {
                this.txtBig.Text = string.Empty;
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 回执单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrEmpty(this.PunishNoticeId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }

            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNotice&menuId=" + Const.ProjectPunishNoticeMenuId, this.PunishNoticeId)));
        }
        
        /// <summary>
        /// 通知单上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPunishNoticeUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PunishNoticeId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.ProjectPunishNoticeMenuId);
            if (buttonList.Count() > 0)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&type=0&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PunishNoticeStatistics&menuId=" + BLL.Const.ProjectPunishNoticeStatisticsMenuId, this.PunishNoticeId)));
            }
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {         
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            if (string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
            {
                Alert.ShowInTop("请选择受罚单位", MessageBoxIcon.Warning);
                return;
            }

            Model.Check_PunishNotice punishNotice = new Model.Check_PunishNotice
            {
                ProjectId = this.ProjectId,
                PunishNoticeCode = this.txtPunishNoticeCode.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
            {
                punishNotice.UnitId = this.drpUnitId.SelectedValue;
            }
            punishNotice.PunishName = this.drpPunishName.SelectedValue;
            punishNotice.PunishNoticeDate = Funs.GetNewDateTime(this.txtPunishNoticeDate.Text.Trim());
            punishNotice.IncentiveReason = this.txtIncentiveReason.Text.Trim();
            punishNotice.BasicItem = this.txtBasicItem.Text.Trim();
            punishNotice.PunishMoney = Funs.GetNewDecimalOrZero(this.txtPunishMoney.Text.Trim());
            punishNotice.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            //punishNotice.AttachUrl = this.AttchUrl;
            punishNotice.CompileMan = this.CurrUser.UserId;
            punishNotice.CompileDate = DateTime.Now;
            punishNotice.States = BLL.Const.State_0;
            if (this.drpSignMan.SelectedValue != BLL.Const._Null)
            {
                punishNotice.SignMan = this.drpSignMan.SelectedValue;
            }
            if (this.drpApproveMan.SelectedValue != BLL.Const._Null)
            {
                punishNotice.ApproveMan = this.drpApproveMan.SelectedValue;
            }
            punishNotice.ContractNum = this.txtContractNum.Text.Trim();
            punishNotice.Currency = this.txtCurrency.Text.Trim();
            if (type == BLL.Const.BtnSubmit)
            {
                punishNotice.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.PunishNoticeId))
            {
                punishNotice.PunishNoticeId = this.PunishNoticeId;
                BLL.PunishNoticeService.UpdatePunishNotice(punishNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, punishNotice.PunishNoticeCode, punishNotice.PunishNoticeId, BLL.Const.ProjectPunishNoticeMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.PunishNoticeId = SQLHelper.GetNewID(typeof(Model.Check_PunishNotice));
                punishNotice.PunishNoticeId = this.PunishNoticeId;
                BLL.PunishNoticeService.AddPunishNotice(punishNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, punishNotice.PunishNoticeCode, punishNotice.PunishNoticeId,BLL.Const.ProjectPunishNoticeMenuId,BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectPunishNoticeMenuId, this.PunishNoticeId, (type == BLL.Const.BtnSubmit ? true : false), punishNotice.PunishNoticeCode, "../Check/PunishNoticeView.aspx?PunishNoticeId={0}");
        }
        #endregion
    }
}