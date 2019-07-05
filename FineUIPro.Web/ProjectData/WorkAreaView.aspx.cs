using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class WorkAreaView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WorkAreaId
        {
            get
            {
                return (string)ViewState["WorkAreaId"];
            }
            set
            {
                ViewState["WorkAreaId"] = value;
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
                this.WorkAreaId = Request.Params["WorkAreaId"];
                if (!string.IsNullOrEmpty(this.WorkAreaId))
                {
                    Model.ProjectData_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(this.WorkAreaId);
                    if (workArea!=null)
                    {
                        this.txtWorkAreaCode.Text = workArea.WorkAreaCode;
                        this.txtWorkAreaName.Text = workArea.WorkAreaName;
                        //var units = BLL.UnitService.GetUnitByUnitId(workArea.UnitId);
                        //if (units != null)
                        //{
                        //    this.drpUnitId.Text = units.UnitName;
                        //}
                        this.txtRemark.Text = workArea.Remark;
                    }
                }
            }
        }
        #endregion
    }
}