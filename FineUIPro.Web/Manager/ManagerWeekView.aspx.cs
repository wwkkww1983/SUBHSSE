using System;
using System.Web;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerWeekView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ManagerWeekId
        {
            get
            {
                return (string)ViewState["ManagerWeekId"];
            }
            set
            {
                ViewState["ManagerWeekId"] = value;
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
                this.ManagerWeekId = Request.Params["ManagerWeekId"];
                if (!string.IsNullOrEmpty(this.ManagerWeekId))
                {
                    Model.Manager_ManagerWeek managerWeek = BLL.ManagerWeekService.GetManagerWeekById(this.ManagerWeekId);
                    if (managerWeek != null)
                    {
                        this.txtManagerWeekCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerWeekId);
                        this.txtManagerWeekName.Text = managerWeek.ManagerWeekName;
                        var users = BLL.UserService.GetUserByUserId(managerWeek.CompileMan);
                        if (users != null)
                        {
                            this.drpCompileMan.Text = users.UserName;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", managerWeek.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(managerWeek.FileContent);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerWeekMenuId;
                this.ctlAuditFlow.DataId = this.ManagerWeekId;
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
            if (!string.IsNullOrEmpty(this.ManagerWeekId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManagerWeekAttachUrl&menuId={1}&type=-1", ManagerWeekId, BLL.Const.ProjectManagerWeekMenuId)));
            }
            
        }
        #endregion
    }
}