using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Administrative
{
    public partial class DriverManagerView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string DriverManagerId
        {
            get
            {
                return (string)ViewState["DriverManagerId"];
            }
            set
            {
                ViewState["DriverManagerId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.DriverManagerId = Request.Params["DriverManagerId"];
                if (!string.IsNullOrEmpty(this.DriverManagerId))
                {
                    var DriverManager = BLL.DriverManagerService.GetDriverManagerById(this.DriverManagerId);
                    if (DriverManager != null)
                    {
                        this.txtDriverManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.DriverManagerId);
                        this.txtDriverName.Text = DriverManager.DriverName;
                        this.txtDriverCode.Text = DriverManager.DriverCode;
                        this.txtDrivingDate.Text = string.Format("{0:yyyy-MM-dd}", DriverManager.DrivingDate);
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DriverManager.CheckDate);
                        this.txtDriverCarModel.Text = DriverManager.DriverCarModel;
                        this.txtRemark.Text = DriverManager.Remark;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.DriverManagerMenuId;
                this.ctlAuditFlow.DataId = this.DriverManagerId;
            }
        }
        #endregion

        #region 年检有效期
        /// <summary>
        /// 年检有效期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var driverManager = BLL.DriverManagerService.GetDriverManagerById(this.DriverManagerId);
            if (driverManager != null)
            {
                var buttonList = BLL.CommonService.GetAllButtonList(driverManager.ProjectId, this.CurrUser.UserId, BLL.Const.DriverManagerMenuId);
                if (buttonList.Count() > 0 && buttonList.Contains(BLL.Const.BtnModify))
                {
                    driverManager.CheckDate = BLL.Funs.GetNewDateTime(this.txtCheckDate.Text);
                    BLL.DriverManagerService.UpdateDriverManager(driverManager);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
                }
            }
        }
        #endregion
    }
}