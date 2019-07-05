using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ReceiveFileManagerView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ReceiveFileManagerId
        {
            get
            {
                return (string)ViewState["ReceiveFileManagerId"];
            }
            set
            {
                ViewState["ReceiveFileManagerId"] = value;
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
                this.ReceiveFileManagerId = Request.Params["ReceiveFileManagerId"];
                if (!string.IsNullOrEmpty(this.ReceiveFileManagerId))
                {
                    Model.InformationProject_ReceiveFileManager ReceiveFileManager = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(this.ReceiveFileManagerId);
                    if (ReceiveFileManager != null)
                    {
                        ///读取编号
                        this.txtReceiveFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ReceiveFileManagerId);
                        this.txtReceiveFileName.Text = ReceiveFileManager.ReceiveFileName;
                        this.drpUnit.Text = BLL.UnitService.GetUnitNameByUnitId(ReceiveFileManager.FileUnitId);
                        this.txtGetFileDate.Text = string.Format("{0:yyyy-MM-dd}", ReceiveFileManager.GetFileDate);
                        this.txtFileCode.Text = ReceiveFileManager.FileCode;
                        if (ReceiveFileManager.FilePageNum.HasValue)
                        {
                            this.txtFilePageNum.Text = ReceiveFileManager.FilePageNum.ToString();
                        }
                        this.txtVersion.Text = ReceiveFileManager.Version;
                        this.drpSendPerson.Text = BLL.UserService.GetUserNameByUserId(ReceiveFileManager.SendPersonId);
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(ReceiveFileManager.MainContent);
                        if (!string.IsNullOrEmpty(ReceiveFileManager.UnitIds))
                        {
                            string unitNames = string.Empty;
                            List<string> unitIds = ReceiveFileManager.UnitIds.Split(',').ToList();
                            foreach (var item in unitIds)
                            {
                                var unit = BLL.UnitService.GetUnitByUnitId(item);
                                if (unit != null)
                                {
                                    unitNames += unit.UnitName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(unitNames))
                            {
                                unitNames = unitNames.Substring(0, unitNames.LastIndexOf(','));
                            }
                            this.txtUnitNames.Text = unitNames;
                        }
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ReceiveFileManagerMenuId;
                this.ctlAuditFlow.DataId = this.ReceiveFileManagerId;
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
            if (!string.IsNullOrEmpty(this.ReceiveFileManagerId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ReceiveFileManagerAttachUrl&menuId={1}&type=-1", ReceiveFileManagerId, BLL.Const.ReceiveFileManagerMenuId)));
            }            
        }
        #endregion
    }
}