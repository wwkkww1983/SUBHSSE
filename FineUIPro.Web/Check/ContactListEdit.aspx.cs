using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class ContactListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ContactListId
        {
            get
            {
                return (string)ViewState["ContactListId"];
            }
            set
            {
                ViewState["ContactListId"] = value;
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
  
                this.ContactListId = Request.Params["ContactListId"];
                if (!string.IsNullOrEmpty(this.ContactListId))
                {
                    Model.Check_ContactList ContactList = BLL.ContactListService.GetContactListById(this.ContactListId);
                    if (ContactList != null)
                    {
                        this.ProjectId = ContactList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ContactListId);
                        if (!String.IsNullOrEmpty(ContactList.SponsorUnitId))
                        {
                            this.drpSponsorUnit.SelectedValue = ContactList.SponsorUnitId;
                        }
                        if (!string.IsNullOrEmpty(ContactList.ReceivingUnits))
                        {
                            this.drpReceivingUnits.SelectedValueArray = ContactList.ReceivingUnits.Split(',');
                        }     

                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ContactList.CompileDate);
                        if (!string.IsNullOrEmpty(ContactList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = ContactList.CompileMan;
                        }
                        this.txtRemark.Text = ContactList.Remark;                       
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(ContactList.SeeFile);                                               
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectContactListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.drpSponsorUnit.SelectedValue = this.CurrUser.UnitId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectContactListMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
            //发起单位
            BLL.UnitService.InitUnitDropDownList(this.drpSponsorUnit, this.ProjectId, true);
            //接收单位
            BLL.UnitService.InitUnitDropDownList(this.drpReceivingUnits, this.ProjectId, true);
        }

        #region 保存
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
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_ContactList newContactList = new Model.Check_ContactList
            {
                ProjectId = this.ProjectId,
                Code = this.txtCode.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim())
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                newContactList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            if (this.drpSponsorUnit.SelectedValue != BLL.Const._Null)
            {
                newContactList.SponsorUnitId = this.drpSponsorUnit.SelectedValue;
            }
            ///接收单位
            string receivingUnits = string.Empty;
            string receivingUnitNames = string.Empty;
            foreach (var item in this.drpReceivingUnits.SelectedValueArray)
            {
                var role = BLL.UnitService.GetUnitByUnitId(item);
                if (role != null)
                {
                    receivingUnits += role.UnitId + ",";
                    receivingUnitNames += role.UnitName + ",";
                }
            }
            if (!string.IsNullOrEmpty(receivingUnits))
            {
                newContactList.ReceivingUnits = receivingUnits.Substring(0, receivingUnits.LastIndexOf(","));
                newContactList.ReceivingUnitNames = receivingUnitNames.Substring(0, receivingUnitNames.LastIndexOf(","));
            }

            newContactList.Remark = this.txtRemark.Text.Trim();
            newContactList.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.ContactListId))
            {
                newContactList.ContactListId = this.ContactListId;
                BLL.ContactListService.UpdateContactList(newContactList);
                BLL.LogService.AddSys_Log(this.CurrUser, newContactList.Code, newContactList.ContactListId,BLL.Const.ProjectContactListMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.ContactListId = SQLHelper.GetNewID(typeof(Model.Check_ContactList));
                newContactList.ContactListId = this.ContactListId;
                BLL.ContactListService.AddContactList(newContactList);
                BLL.LogService.AddSys_Log(this.CurrUser, newContactList.Code, newContactList.ContactListId, BLL.Const.ProjectContactListMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.ContactListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContactListAttachUrl&menuId={1}", ContactListId,BLL.Const.ProjectContactListMenuId)));
        }
        #endregion
    }
}