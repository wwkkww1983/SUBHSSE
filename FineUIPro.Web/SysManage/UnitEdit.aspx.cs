using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class UnitEdit : PageBase
    {
        /// <summary>
        /// 单位主键
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                LoadData();
                ////权限按钮方法
                this.GetButtonPower();

                UnitTypeService.InitUnitTypeDropDownList(this.ddlUnitTypeId, true);
                UnitService.InitUnitDropDownList(this.drpSupUnit, null, true);
                this.UnitId = Request.Params["UnitId"];
                if (!string.IsNullOrEmpty(this.UnitId))
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit!=null)
                    {
                        this.txtUnitCode.Text = unit.UnitCode;
                        this.txtUnitName.Text = unit.UnitName;
                        if (!string.IsNullOrEmpty(unit.UnitTypeId))
                        {
                            this.ddlUnitTypeId.SelectedValue = unit.UnitTypeId;
                        }
                        this.txtCorporate.Text = unit.Corporate;
                        this.txtAddress.Text = unit.Address;
                        this.txtTelephone.Text = unit.Telephone;
                        this.txtFax.Text = unit.Fax;
                        this.txtEMail.Text = unit.EMail;
                        this.txtProjectRange.Text = unit.ProjectRange;
                        if (unit.IsBranch == true)
                        {
                            this.rblIsBranch.SelectedValue = "true";
                            this.drpSupUnit.Hidden = false;
                            if (!string.IsNullOrEmpty(unit.SupUnitId))
                            {
                                this.drpSupUnit.SelectedValue = unit.SupUnitId;
                            }
                        }
                        if (unit.IsThisUnit== true)
                        {
                            this.rblIsThisUnit.SelectedValue = "true";
                            //this.rblIsThisUnit.Enabled = true;
                        }
                        else
                        {
                            this.rblIsThisUnit.SelectedValue = "false";
                        }
                        if (unit.IsBuild == true && this.CurrUser.UserId != BLL.Const.sysglyId)
                        {
                            this.txtUnitCode.Readonly = true;
                            this.txtUnitName.Readonly = true;
                        }
                    }
                }

                //var thisUnit = BLL.CommonService.GetIsThisUnit();
                //if (this.CurrUser.UserId == BLL.Const.sysglyId)
                //{
                //    this.rblIsThisUnit.Enabled = true;
                //}
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.UnitService.IsExitUnitByUnitName(this.UnitId, this.txtUnitName.Text.Trim()))
            {
                Alert.ShowInTop("单位名称已存在！",MessageBoxIcon.Warning);
                return;
            }
            if (BLL.UnitService.IsExitUnitByUnitName(this.UnitId, this.txtUnitCode.Text.Trim()))
            {
                Alert.ShowInTop("单位代码已存在！", MessageBoxIcon.Warning);
                return;
            }

            Model.Base_Unit unit = new Model.Base_Unit
            {
                UnitCode = this.txtUnitCode.Text.Trim(),
                UnitName = this.txtUnitName.Text.Trim()
            };

            ////单位类型下拉框
            if (this.ddlUnitTypeId.SelectedValue != BLL.Const._Null)
            {
                if (string.IsNullOrEmpty(this.ddlUnitTypeId.SelectedValue))
                {
                    var unitType = BLL.UnitTypeService.GetUnitTypeByName(this.ddlUnitTypeId.Text);
                    if (unitType != null)
                    {
                        unit.UnitTypeId = unitType.UnitTypeId;
                    }
                    else
                    {
                        Model.Base_UnitType newUitType = new Model.Base_UnitType
                        {
                            UnitTypeId = SQLHelper.GetNewID(typeof(Model.Base_UnitType)),
                            UnitTypeName = this.ddlUnitTypeId.Text
                        };
                        BLL.UnitTypeService.AddUnitType(newUitType);
                        unit.UnitTypeId = newUitType.UnitTypeId;
                    }
                }
                else
                {
                    unit.UnitTypeId = this.ddlUnitTypeId.SelectedValue;
                }
            }

            unit.Corporate = this.txtCorporate.Text.Trim();
            unit.Address = this.txtAddress.Text.Trim();
            unit.Telephone = this.txtTelephone.Text.Trim();
            unit.Fax = this.txtFax.Text.Trim();
            unit.EMail = this.txtEMail.Text.Trim();
            unit.ProjectRange = this.txtProjectRange.Text.Trim();
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null && this.UnitId != thisUnit.UnitId)
            {
                unit.IsThisUnit = false;
            }
            else
            {
                unit.IsThisUnit = Convert.ToBoolean(this.rblIsThisUnit.SelectedValue);
            }
            var updateName = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitCode == this.txtUnitCode.Text.Trim() || x.UnitName == this.txtUnitName.Text.Trim());
            if (updateName != null)
            {
                unit.IsHide = false;
                this.UnitId = updateName.UnitId;
            }
            unit.IsBranch = Convert.ToBoolean(this.rblIsBranch.SelectedValue);
            if (unit.IsBranch == true)
            {
                unit.SupUnitId = this.drpSupUnit.SelectedValue;
            }
            else
            {
                unit.SupUnitId = null;
            }
            if (string.IsNullOrEmpty(this.UnitId))
            {
                unit.UnitId = SQLHelper.GetNewID(typeof(Model.Base_Unit));
                unit.DataSources = this.CurrUser.LoginProjectId;
                BLL.UnitService.AddUnit(unit);
                BLL.LogService.AddSys_Log(this.CurrUser, unit.UnitCode, unit.UnitId, BLL.Const.UnitMenuId, Const.BtnAdd);
                SaveHSSEManage("add");
            }
            else
            {
                var getUnit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                if (getUnit != null)
                {
                    unit.FromUnitId = getUnit.FromUnitId;
                }
                unit.UnitId = this.UnitId;
                BLL.UnitService.UpdateUnit(unit);
                SaveHSSEManage("update");
                BLL.LogService.AddSys_Log(this.CurrUser, unit.UnitCode, unit.UnitId, BLL.Const.UnitMenuId, Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 增加HSSE管理机构
        /// </summary>
        private void SaveHSSEManage(string type)
        {
            bool isInsert = false;
            if (type == "add")
            {
                isInsert = true;
            }
            else 
            {
                var hsseM = Funs.DB.HSSESystem_HSSEManage.FirstOrDefault(x => x.HSSEManageName == this.txtUnitName.Text.Trim());
                if (hsseM == null && this.rblIsThisUnit.SelectedValue == "true")
                {
                    isInsert = true;
                } 
            }

            if (isInsert)
            {
                Model.HSSESystem_HSSEManage m = new Model.HSSESystem_HSSEManage
                {
                    HSSEManageId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEManage))
                };
                if (this.rblIsThisUnit.SelectedValue == "true")
                {
                    m.HSSEManageCode = "0" + this.txtUnitCode.Text.Trim();
                }
                else
                {
                    m.HSSEManageCode = "1" + this.txtUnitCode.Text.Trim();
                }
                m.HSSEManageName = this.txtUnitName.Text.Trim();
                m.SupHSSEManageId = "0";
                BLL.HSSEManageService.AddHSSEManage(m);
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UnitMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.UnitId))
            //{
            //    var updateName = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitCode == this.txtUnitCode.Text.Trim() || x.UnitName == this.txtUnitName.Text.Trim());
            //    if (updateName != null)
            //    {
            //        this.txtUnitCode.Text = updateName.UnitCode;
            //        this.txtUnitName.Text = updateName.UnitName;
            //        if (!string.IsNullOrEmpty(updateName.UnitTypeId))
            //        {
            //            this.ddlUnitTypeId.SelectedValue = updateName.UnitTypeId;
            //        }
            //        this.txtCorporate.Text = updateName.Corporate;
            //        this.txtAddress.Text = updateName.Address;
            //        this.txtTelephone.Text = updateName.Telephone;
            //        this.txtFax.Text = updateName.Fax;
            //        this.txtEMail.Text = updateName.EMail;
            //        this.txtProjectRange.Text = updateName.ProjectRange;
            //        if (updateName.IsThisUnit == true)
            //        {
            //            this.rblIsThisUnit.SelectedValue = "true";
            //            this.rblIsThisUnit.Enabled = true;
            //        }
            //        else
            //        {
            //            this.rblIsThisUnit.SelectedValue = "false";
            //        }
            //        this.UnitId = updateName.UnitId;
            //    }
            //    else
            //    {
            //        this.UnitId = string.Empty;
            //    }
            //}
        }

        protected void rblIsBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpSupUnit.Hidden = true;
            if (this.rblIsBranch.SelectedValue == "true")
            {
                this.drpSupUnit.Hidden = false;
            }
        }
    }
}