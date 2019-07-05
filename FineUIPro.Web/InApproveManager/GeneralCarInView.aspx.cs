using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GeneralCarInView :PageBase
    {
        /// <summary>
        /// 主键 
        /// </summary>
        private string GeneralCarInId
        {
            get
            {
                return (string)ViewState["GeneralCarInId"];
            }
            set
            {
                ViewState["GeneralCarInId"] = value;
            }
        }

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
              
                this.GeneralCarInId = Request.Params["GeneralCarInId"];
                if (!string.IsNullOrEmpty(this.GeneralCarInId))
                {
                    Model.InApproveManager_GeneralCarIn carIn = BLL.GeneralCarInService.GetGeneralCarInById(this.GeneralCarInId);
                    if (carIn != null)
                    {
                        if (!string.IsNullOrEmpty(carIn.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(carIn.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        this.txtDriverName.Text = carIn.DriverName;
                        this.txtCarNum.Text = carIn.CarNum;
                        this.txtCarType.Text = carIn.CarType;
                        this.txtDescription.Text = carIn.Descriptions;
                    }
                    List<Model.InApproveManager_GeneralCarInItem> carChecks = BLL.GeneralCarInItemService.GetGeneralCarInItemByGeneralCarInId(this.GeneralCarInId);
                    if (carChecks != null && carChecks.Count > 0)
                    {
                        this.ckbCheckItem1.Checked = Convert.ToBoolean(carChecks[0].CheckItem1);
                        this.ckbCheckItem2.Checked = Convert.ToBoolean(carChecks[0].CheckItem2);
                        this.ckbCheckItem3.Checked = Convert.ToBoolean(carChecks[0].CheckItem3);
                        this.ckbCheckItem4.Checked = Convert.ToBoolean(carChecks[0].CheckItem4);
                        this.ckbCheckItem5.Checked = Convert.ToBoolean(carChecks[0].CheckItem5);
                        this.ckbCheckItem6.Checked = Convert.ToBoolean(carChecks[0].CheckItem6);
                        this.ckbCheckItem7.Checked = Convert.ToBoolean(carChecks[0].CheckItem7);
                        this.ckbCheckItem8.Checked = Convert.ToBoolean(carChecks[0].CheckItem8);
                        this.ckbCheckItem9.Checked = Convert.ToBoolean(carChecks[0].CheckItem9);
                        this.ckbCheckItem10.Checked = Convert.ToBoolean(carChecks[0].CheckItem10);
                        this.ckbCheckItem11.Checked = Convert.ToBoolean(carChecks[0].CheckItem11);
                        this.ckbCheckItem12.Checked = Convert.ToBoolean(carChecks[0].CheckItem12);
                        this.ckbCheckItem13.Checked = Convert.ToBoolean(carChecks[0].CheckItem13);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GeneralCarInMenuId;
                this.ctlAuditFlow.DataId = this.GeneralCarInId;
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GeneralCarInId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralCarInAttachUrl&menuId={1}&type=-1", this.GeneralCarInId, BLL.Const.GeneralCarInMenuId)));
            }
        }
        #endregion
    }
}