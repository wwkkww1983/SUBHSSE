using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.License
{
    public partial class EquipmentSafetyListView : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentSafetyListId
        {
            get
            {
                return (string)ViewState["EquipmentSafetyListId"];
            }
            set
            {
                ViewState["EquipmentSafetyListId"] = value;
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

                this.EquipmentSafetyListId = Request.Params["EquipmentSafetyListId"];
                if (!string.IsNullOrEmpty(this.EquipmentSafetyListId))
                {
                    Model.License_EquipmentSafetyList equipmentSafetyList = BLL.EquipmentSafetyListService.GetEquipmentSafetyListById(this.EquipmentSafetyListId);
                    if (equipmentSafetyList != null)
                    {
                        this.txtEquipmentSafetyListCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EquipmentSafetyListId);
                        this.txtEquipmentSafetyListName.Text = equipmentSafetyList.EquipmentSafetyListName;
                        if (!string.IsNullOrEmpty(equipmentSafetyList.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(equipmentSafetyList.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(equipmentSafetyList.CompileMan))
                        {
                            var user = BLL.UserService.GetUserByUserId(equipmentSafetyList.CompileMan);
                            if (user != null)
                            {
                                this.txtCompileMan.Text = user.UserName;
                            }
                        }
                        if (!string.IsNullOrEmpty(equipmentSafetyList.WorkAreaId))
                        {
                            var workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(equipmentSafetyList.WorkAreaId);
                            if (workArea != null)
                            {
                                this.txtWorkAreaName.Text = workArea.WorkAreaName;
                            }
                        }
                        if (equipmentSafetyList.EquipmentSafetyListCount != null)
                        {
                            this.txtEquipmentSafetyListCount.Text = Convert.ToString(equipmentSafetyList.EquipmentSafetyListCount);
                        }
                        if (equipmentSafetyList.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentSafetyList.CompileDate);
                        }
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEquipmentSafetyListMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentSafetyListId;
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
            if (!string.IsNullOrEmpty(this.EquipmentSafetyListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentSafetyListAttachUrl&menuId={1}", EquipmentSafetyListId, BLL.Const.ProjectEquipmentSafetyListMenuId)));
            }
        }
        #endregion
    }
}