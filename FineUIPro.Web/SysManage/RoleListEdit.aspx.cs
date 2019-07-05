using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class RoleListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId
        {
            get
            {
                return (string)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.RoleId = Request.Params["roleId"];
                ///权限
                this.GetButtonPower();
                BLL.ConstValue.InitConstValueDropDownList(this.drpRoleType, BLL.ConstValue.Group_0013, false);
                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, this.RoleId, true);
                if (!string.IsNullOrEmpty(this.RoleId))
                {
                    var role = BLL.RoleService.GetRoleByRoleId(this.RoleId);
                    if (role != null)
                    {
                        this.txtRoleCode.Text = role.RoleCode;
                        this.txtRoleName.Text = role.RoleName;
                        if (!string.IsNullOrEmpty(role.RoleType))
                        {
                            this.drpRoleType.SelectedValue = role.RoleType;
                        }
                        if (role.IsAuditFlow == true)
                        {
                            chkIsAuditFlow.Checked = true;
                        }
                        else
                        {
                            chkIsAuditFlow.Checked = false;
                        }
                        this.txtDef.Text = role.Def;
                        if (!string.IsNullOrEmpty(role.AuthorizedRoleIds))
                        {
                            this.drpRole.SelectedValueArray = role.AuthorizedRoleIds.Split(',');
                        }                       
                    }
                }

                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.drpRole.Enabled = false;
                }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RoleMenuId);
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
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (BLL.RoleService.IsExistRoleName(this.RoleId, this.txtRoleName.Text.Trim()))
            {
                Alert.ShowInParent("角色名称已存在！", MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Model.Sys_Role newRole = new Model.Sys_Role
                {
                    RoleCode = this.txtRoleCode.Text.Trim(),
                    RoleName = this.txtRoleName.Text.Trim(),
                    RoleType = this.drpRoleType.SelectedValue,
                    Def = this.txtDef.Text.Trim()
                };
                if (this.chkIsAuditFlow.Checked)
                {
                    newRole.IsAuditFlow = true;
                }
                else
                {
                    newRole.IsAuditFlow = false;
                }

                ///授权角色
                string authorizedRoleIds = string.Empty;
                string authorizedRoleNames = string.Empty;
                foreach (var item in this.drpRole.SelectedValueArray)
                {
                    var role = BLL.RoleService.GetRoleByRoleId(item);
                    if (role != null)
                    {
                        authorizedRoleIds += role.RoleId + ",";
                        authorizedRoleNames += role.RoleName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(authorizedRoleIds))
                {
                    newRole.AuthorizedRoleIds = authorizedRoleIds.Substring(0, authorizedRoleIds.LastIndexOf(","));
                    newRole.AuthorizedRoleNames = authorizedRoleNames.Substring(0, authorizedRoleNames.LastIndexOf(","));
                }

                if (string.IsNullOrEmpty(this.RoleId))
                {
                    newRole.RoleId = SQLHelper.GetNewID(typeof(Model.Sys_Role));
                    newRole.IsSystemBuilt = false;
                    BLL.RoleService.AddRole(newRole);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加角色管理");
                }
                else
                {
                    newRole.RoleId = this.RoleId;
                    BLL.RoleService.UpdateRole(newRole);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改角色管理");
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
        }

        #region 验证角色编号、名称是否存在
        /// <summary>
        /// 验证角色编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            //var q = Funs.DB.Sys_Role.FirstOrDefault(x => x.RoleCode == this.txtRoleCode.Text.Trim() && (x.RoleId != this.RoleId || (this.RoleId == null && x.RoleId != null)));
            //if (q != null)
            //{
            //    ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            //}

            var q2 = Funs.DB.Sys_Role.FirstOrDefault(x => x.RoleName == this.txtRoleName.Text.Trim() && (x.RoleId != this.RoleId || (this.RoleId == null && x.RoleId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的角色名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectALL_Click(object sender, EventArgs e)
        {
            string value = string.Empty;
            foreach (var item in this.drpRole.Items)
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = item.Value;
                }
                else
                {
                    value += "," + item.Value; ;
                }
            }
            if (!string.IsNullOrEmpty(value))
            {
                this.drpRole.SelectedValueArray = value.Split(',');
            }
        }

        protected void SelectNoALL_Click(object sender, EventArgs e)
        {
            this.drpRole.SelectedValueArray = null;
        }
    }
}