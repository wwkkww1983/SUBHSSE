using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SiteConstruction
{
    public partial class ConstructionDynamicView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionDynamicId
        {
            get
            {
                return (string)ViewState["ConstructionDynamicId"];
            }
            set
            {
                ViewState["ConstructionDynamicId"] = value;
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
                this.ConstructionDynamicId = Request.Params["ConstructionDynamicId"];
                if (!string.IsNullOrEmpty(this.ConstructionDynamicId))
                {
                    Model.SiteConstruction_ConstructionDynamic ConstructionDynamic = BLL.ConstructionDynamicService.GetConstructionDynamicById(this.ConstructionDynamicId);
                    if (ConstructionDynamic != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ConstructionDynamic.CompileDate);
                        this.txtUnit.Text = BLL.UnitService.GetUnitNameByUnitId(ConstructionDynamic.UnitId);
                        this.txtJobContent.Text = ConstructionDynamic.JobContent;
                        //this.txtSeeFile.Text = HttpUtility.HtmlDecode(ConstructionDynamic.SeeFile);                        
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
            if (!string.IsNullOrEmpty(this.ConstructionDynamicId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructionDynamicAttachUrl&menuId={1}&type=-1", ConstructionDynamicId, BLL.Const.ProjectConstructionDynamicMenuId)));
            }
        }
        #endregion
    }
}