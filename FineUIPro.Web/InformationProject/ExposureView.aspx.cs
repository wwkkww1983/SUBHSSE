using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ExposureView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ExposureId
        {
            get
            {
                return (string)ViewState["ExposureId"];
            }
            set
            {
                ViewState["ExposureId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                
                this.ExposureId = Request.Params["ExposureId"];
                if (!string.IsNullOrEmpty(this.ExposureId))
                {
                    Model.InformationProject_Exposure exposure = BLL.ExposureService.GetExposureById(this.ExposureId);
                    if (exposure != null)
                    {
                        this.drpUnitId.Text = exposure.UnitName; //BLL.UnitService.GetUnitNameByUnitId(exposure.UnitId);
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ExposureId);
                        if (exposure.ExposureDate != null)
                        {
                            this.txtExposureDate.Text = string.Format("{0:yyyy-MM-dd}", exposure.ExposureDate);
                        }
                       
                        this.txtFileName.Text = exposure.FileName;
                        this.txtRemark.Text = exposure.Remark;
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ExposureAttachUrl&type=-1", this.ExposureId)));
        }
        #endregion
    }
}