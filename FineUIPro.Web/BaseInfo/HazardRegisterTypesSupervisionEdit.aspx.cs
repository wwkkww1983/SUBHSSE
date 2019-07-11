using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class HazardRegisterTypesSupervisionEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 问题巡检类型主键
        /// </summary>
        public string RegisterTypesId
        {
            get
            {
                return (string)ViewState["RegisterTypesId"];
            }
            set
            {
                ViewState["RegisterTypesId"] = value;
            }
        }
        #endregion

        #region 加载页面
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

                this.RegisterTypesId = Request.QueryString["RegisterTypesId"];
                Funs.FineUIPleaseSelect(this.drpGroupType);
                if (!string.IsNullOrEmpty(this.RegisterTypesId))
                {
                    Model.HSSE_Hazard_HazardRegisterTypes title = BLL.HSSE_Hazard_HazardRegisterTypesService.GetTitleByRegisterTypesId(this.RegisterTypesId);
                    if (title != null)
                    {
                        this.txtRegisterTypesName.Text = title.RegisterTypesName;
                        if (!string.IsNullOrEmpty(title.GroupType))
                        {
                            this.drpGroupType.SelectedValue = title.GroupType;
                        }
                        this.txtTypeCode.Text = title.TypeCode;
                        this.txtRemark.Text = title.Remark;
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, Const.HazardRegisterTypesSupervisionMenuId, Const.BtnSave))
            {
                Model.HSSE_Hazard_HazardRegisterTypes title = new Model.HSSE_Hazard_HazardRegisterTypes();
                title.RegisterTypesName = this.txtRegisterTypesName.Text.Trim();
                title.TypeCode = this.txtTypeCode.Text.Trim();
                if (this.drpGroupType.SelectedValue != BLL.Const._Null)
                {
                    title.GroupType = this.drpGroupType.SelectedValue;
                }
                title.Remark = this.txtRemark.Text.Trim();
                title.HazardRegisterType = "3";   //安全督查类型
                if (string.IsNullOrEmpty(this.RegisterTypesId))
                {
                    this.RegisterTypesId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_HazardRegisterTypes));
                    title.RegisterTypesId = this.RegisterTypesId;
                    BLL.HSSE_Hazard_HazardRegisterTypesService.AddHazardRegisterTypes(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.TypeCode, title.RegisterTypesId, BLL.Const.HazardRegisterTypesSupervisionMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    title.RegisterTypesId = this.RegisterTypesId;
                    BLL.HSSE_Hazard_HazardRegisterTypesService.UpdateHazardRegisterTypes(title);
                    BLL.LogService.AddSys_Log(this.CurrUser, title.TypeCode, title.RegisterTypesId, BLL.Const.HazardRegisterTypesSupervisionMenuId, BLL.Const.BtnModify);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }
        #endregion
    }
}