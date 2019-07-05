using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class ContactListView : PageBase
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
                this.ContactListId = Request.Params["ContactListId"];
                if (!string.IsNullOrEmpty(this.ContactListId))
                {
                    Model.Check_ContactList ContactList = BLL.ContactListService.GetContactListById(this.ContactListId);
                    if (ContactList != null)
                    {
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ContactListId);
                        this.drpSponsorUnit.Text = BLL.UnitService.GetUnitNameByUnitId(ContactList.SponsorUnitId);
                        this.drpReceivingUnits.Text = ContactList.ReceivingUnitNames;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ContactList.CompileDate);
                        this.txtRemark.Text = ContactList.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(ContactList.SeeFile);
                        this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(ContactList.CompileMan);
                    }
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
            if (!string.IsNullOrEmpty(this.ContactListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContactListAttachUrl&menuId={1}&type=-1", ContactListId, BLL.Const.ProjectContactListMenuId)));
            }
        }
        #endregion
    }
}