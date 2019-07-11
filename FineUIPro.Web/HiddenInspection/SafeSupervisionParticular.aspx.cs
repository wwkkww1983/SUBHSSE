using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class SafeSupervisionParticular : PageBase
    {
        #region 定义项
        /// <summary>
        /// APP安全督查主键
        /// </summary>
        public string SafeSupervisionId
        {
            get
            {
                return (string)ViewState["SafeSupervisionId"];
            }
            set
            {
                ViewState["SafeSupervisionId"] = value;
            }
        }
        #endregion

        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                SafeSupervisionId = Request.Params["SafeSupervisionId"];
                Model.View_Hazard_SafeSupervision safeSupervision = BLL.HSSE_Hazard_SafeSupervisionService.GetSafeSupervisionViewBySafeSupervisionId(SafeSupervisionId);
                if (safeSupervision != null)
                {
                    this.txtSafeSupervisionCode.Text = safeSupervision.SafeSupervisionCode;
                    this.txtCheckType.Text = safeSupervision.CheckTypeStr;
                    this.txtCheckMan.Text = safeSupervision.CheckMan;
                    if (safeSupervision.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", safeSupervision.CheckDate);
                    }
                    this.Grid1.DataSource = from x in Funs.DB.View_Hazard_HazardRegister
                                            where x.SafeSupervisionId == SafeSupervisionId
                                            select x;
                    this.Grid1.DataBind();
                }
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
        }
        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationView.aspx?HazardRegisterId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }
        #endregion
    }
}