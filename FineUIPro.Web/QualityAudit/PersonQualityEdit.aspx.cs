using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.QualityAudit
{
    public partial class PersonQualityEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 人员主键
        /// </summary>
        private string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }

        /// <summary>
        /// 资质主键
        /// </summary>
        private string PersonQualityId
        {
            get
            {
                return (string)ViewState["PersonQualityId"];
            }
            set
            {
                ViewState["PersonQualityId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        private string ProjectId
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();               
                this.PersonId = Request.Params["PersonId"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                BLL.CertificateService.InitCertificateDropDownList(this.drpCertificate, true);
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;
                        var unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitCode.Text = unit.UnitCode;
                            this.txtUnitName.Text = unit.UnitName;
                        }
                        this.txtPersonName.Text = person.PersonName;
                        var workPost = BLL.WorkPostService.GetWorkPostById(person.WorkPostId);
                        if (workPost != null)
                        {
                            this.txtWorkPostName.Text = workPost.WorkPostName;
                        }
                    }

                    var personQuality = BLL.PersonQualityService.GetPersonQualityByPersonId(this.PersonId);
                    if (personQuality != null)
                    {
                        this.PersonQualityId = personQuality.PersonQualityId;
                        this.txtCertificateNo.Text = personQuality.CertificateNo;
                        this.drpCertificate.SelectedValue = personQuality.CertificateId;
                        this.txtGrade.Text = personQuality.Grade;
                        this.txtSendUnit.Text = personQuality.SendUnit;
                        this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.SendDate);
                        this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LateCheckDate);
                        this.txtApprovalPerson.Text = personQuality.ApprovalPerson;
                        this.txtRemark.Text = personQuality.Remark;
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", personQuality.AuditDate);
                    }
                }
                else
                {
                    this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                   
                }
            }
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
            if (string.IsNullOrEmpty(this.PersonQualityId))
            {
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/PersonQualityAttachUrl&menuId={1}", PersonQualityId, BLL.Const.PersonQualityMenuId)));
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
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            if (this.drpCertificate.SelectedValue == BLL.Const._Null || string.IsNullOrEmpty(this.drpCertificate.SelectedValue))
            {
                Alert.ShowInTop("请选择特岗证书！", MessageBoxIcon.Warning);
                return;
            }

            if (!String.IsNullOrEmpty(this.PersonId))
            {
                Model.QualityAudit_PersonQuality personQuality = new Model.QualityAudit_PersonQuality
                {
                    PersonId = this.PersonId,
                    CertificateNo = this.txtCertificateNo.Text.Trim(),
                    CertificateId = this.drpCertificate.SelectedValue,
                    CertificateName = this.drpCertificate.SelectedItem.Text,
                    Grade = this.txtGrade.Text.Trim(),
                    SendUnit = this.txtSendUnit.Text.Trim(),
                    SendDate = Funs.GetNewDateTime(this.txtSendDate.Text.Trim()),
                    LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim()),
                    LateCheckDate = Funs.GetNewDateTime(this.txtLateCheckDate.Text.Trim()),
                    ApprovalPerson = this.txtApprovalPerson.Text.Trim(),
                    Remark = this.txtRemark.Text.Trim(),
                    CompileMan = this.CurrUser.UserId,
                    CompileDate = DateTime.Now,
                    AuditDate = Funs.GetNewDateTime(this.txtAuditDate.Text.Trim())
                };
                if (!string.IsNullOrEmpty(this.PersonQualityId))
                {
                    personQuality.PersonQualityId = this.PersonQualityId;
                    BLL.PersonQualityService.UpdatePersonQuality(personQuality);
                    BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改特殊岗位人员资质", personQuality.PersonQualityId);
                }
                else
                {
                    this.PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality));
                    personQuality.PersonQualityId = this.PersonQualityId;
                    BLL.PersonQualityService.AddPersonQuality(personQuality);
                    BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加特殊岗位人员资质", personQuality.PersonQualityId);
                }
                if (isClose)
                {
                    var thisUnit = BLL.CommonService.GetIsThisUnit();
                    if (thisUnit != null && thisUnit.UnitId == BLL.Const.UnitId_ECEC)
                    {
                        ////判断单据是否 加入到企业管理资料
                        string menuId = BLL.Const.PersonQualityMenuId;
                        var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                        if (safeData != null)
                        {
                            BLL.SafetyDataService.AddSafetyData(menuId, this.PersonId, this.txtPersonName.Text + "：特岗人员资质", "../QualityAudit/PersonQualityEdit.aspx?PersonId={0}", this.ProjectId);
                        }
                    }

                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
        }
        #endregion
    }
}