using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.QualityAudit
{
    public partial class ManagePersonQualityEdit : PageBase
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
        private string ManagePersonQualityId
        {
            get
            {
                return (string)ViewState["ManagePersonQualityId"];
            }
            set
            {
                ViewState["ManagePersonQualityId"] = value;
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

        private bool AppendToEnd = true;
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

                    var ManagePersonQuality = BLL.ManagePersonQualityService.GetManagePersonQualityByPersonId(this.PersonId);
                    if (ManagePersonQuality != null)
                    {
                        this.ManagePersonQualityId = ManagePersonQuality.ManagePersonQualityId;
                        //this.txtCertificateNo.Text = ManagePersonQuality.CertificateNo;
                        //this.txtCertificateName.Text = ManagePersonQuality.CertificateName;
                        this.txtGrade.Text = ManagePersonQuality.Grade;
                        //this.txtSendUnit.Text = ManagePersonQuality.SendUnit;
                        //this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", ManagePersonQuality.SendDate);
                        //this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", ManagePersonQuality.LimitDate);
                        this.txtLateCheckDate.Text = string.Format("{0:yyyy-MM-dd}", ManagePersonQuality.LateCheckDate);
                        this.txtApprovalPerson.Text = ManagePersonQuality.ApprovalPerson;
                        this.txtRemark.Text = ManagePersonQuality.Remark;
                        this.txtAuditDate.Text = string.Format("{0:yyyy-MM-dd}", ManagePersonQuality.AuditDate);
                    }
                }
                //else
                //{
                //    this.txtSendDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                //    this.txtLimitDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                //}
                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }

                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("CertificateNo", "");
                defaultObj.Add("CertificateName", "");
                defaultObj.Add("SendUnit", "");
                defaultObj.Add("SendDate", DateTime.Now);
                defaultObj.Add("LimitDate", DateTime.Now);
                defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                // 绑定表格
                BindGrid();
            }
        }

        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<Model.QualityAudit_ManagePersonQualityItem> lists = BLL.ManagePersonQualityItemService.GetManagePersonQualityItemById(this.ManagePersonQualityId);
            if (lists != null)
            {
                Grid1.DataSource = lists;
                Grid1.DataBind();
            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagePersonQualityAttachUrl&type=-1", ManagePersonQualityId, BLL.Const.ManagePersonQualityMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.ManagePersonQualityId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagePersonQualityAttachUrl&menuId={1}", ManagePersonQualityId, BLL.Const.ManagePersonQualityMenuId)));
            }
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
            if (!String.IsNullOrEmpty(this.PersonId))
            {
                Model.QualityAudit_ManagePersonQuality ManagePersonQuality = new Model.QualityAudit_ManagePersonQuality
                {
                    PersonId = this.PersonId,
                    //CertificateNo = this.txtCertificateNo.Text.Trim(),
                    //CertificateName = this.txtCertificateName.Text.Trim(),
                    Grade = this.txtGrade.Text.Trim(),
                    //SendUnit = this.txtSendUnit.Text.Trim(),
                    //SendDate = Funs.GetNewDateTime(this.txtSendDate.Text.Trim()),
                    //LimitDate = Funs.GetNewDateTime(this.txtLimitDate.Text.Trim()),
                    LateCheckDate = Funs.GetNewDateTime(this.txtLateCheckDate.Text.Trim()),
                    ApprovalPerson = this.txtApprovalPerson.Text.Trim(),
                    Remark = this.txtRemark.Text.Trim(),
                    CompileMan = this.CurrUser.UserId,
                    CompileDate = DateTime.Now,
                    AuditDate = Funs.GetNewDateTime(this.txtAuditDate.Text.Trim())
                };
                if (!string.IsNullOrEmpty(this.ManagePersonQualityId))
                {
                    ManagePersonQuality.ManagePersonQualityId = this.ManagePersonQualityId;
                    BLL.ManagePersonQualityService.UpdateManagePersonQuality(ManagePersonQuality);
                    BLL.LogService.AddSys_Log(this.CurrUser, ManagePersonQuality.CertificateNo, ManagePersonQuality.ManagePersonQualityId, BLL.Const.ManagePersonQualityMenuId, BLL.Const.BtnModify);
                }
                else
                {
                    var updateManger = BLL.ManagePersonQualityService.GetManagePersonQualityByPersonId(this.PersonId);
                    if (updateManger != null)
                    {
                        this.ManagePersonQualityId = updateManger.ManagePersonQualityId;
                        BLL.ManagePersonQualityService.UpdateManagePersonQuality(ManagePersonQuality);
                        BLL.LogService.AddSys_Log(this.CurrUser, ManagePersonQuality.CertificateNo, ManagePersonQuality.ManagePersonQualityId, BLL.Const.ManagePersonQualityMenuId, BLL.Const.BtnModify);

                    }
                    else
                    {
                        this.ManagePersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_ManagePersonQuality));
                        ManagePersonQuality.ManagePersonQualityId = this.ManagePersonQualityId;
                        BLL.ManagePersonQualityService.AddManagePersonQuality(ManagePersonQuality);
                        BLL.LogService.AddSys_Log(this.CurrUser, ManagePersonQuality.CertificateNo, ManagePersonQuality.ManagePersonQualityId, BLL.Const.ManagePersonQualityMenuId, BLL.Const.BtnAdd);
                    }
                }
                //增加资质证书
                BLL.ManagePersonQualityItemService.DeleteManagePersonQualityItemByManagePersonQualityId(this.ManagePersonQualityId);
                if (Grid1.GetModifiedData().Count > 0)
                {
                    JArray teamGroupData = Grid1.GetMergedData();
                    foreach (JObject teamGroupRow in teamGroupData)
                    {
                        JObject values = teamGroupRow.Value<JObject>("values");
                        Model.QualityAudit_ManagePersonQualityItem newManagerTotalMonthItem = new Model.QualityAudit_ManagePersonQualityItem
                        {
                            ManagePersonQualityItemId = SQLHelper.GetNewID(typeof(Model.QualityAudit_ManagePersonQualityItem)),
                            ManagePersonQualityId = this.ManagePersonQualityId,
                            CertificateNo = values.Value<string>("CertificateNo"),
                            CertificateName = values.Value<string>("CertificateName"),
                            SendUnit = values.Value<string>("SendUnit"),
                            SendDate = Funs.GetNewDateTime(values.Value<string>("SendDate")),
                            LimitDate = Funs.GetNewDateTime(values.Value<string>("LimitDate"))
                        };
                        BLL.ManagePersonQualityItemService.AddManagePersonQualityItem(newManagerTotalMonthItem);
                    }
                }

                if (isClose)
                {
                    var thisUnit = BLL.CommonService.GetIsThisUnit();
                    if (thisUnit != null && thisUnit.UnitId == BLL.Const.UnitId_ECEC)
                    {
                        ////判断单据是否 加入到企业管理资料
                        string menuId = BLL.Const.ManagePersonQualityMenuId;
                        var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                        if (safeData != null)
                        {
                            BLL.SafetyDataService.AddSafetyData(menuId, this.PersonId, this.txtPersonName.Text + "：管理人员资质", "../QualityAudit/SafePersonQualityEdit.aspx?PersonId={0}", this.ProjectId);
                        }
                    }
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
        }
        #endregion
    }
}