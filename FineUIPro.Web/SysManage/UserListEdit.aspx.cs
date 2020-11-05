﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{ 
    public partial class UserListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId
        {
            get
            {
                return (string)ViewState["UserId"];
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }
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
        /// <summary>
        /// 签名附件路径
        /// </summary>
        public string SignatureUrl
        {
            get
            {
                return (string)ViewState["SignatureUrl"];
            }
            set
            {
                ViewState["SignatureUrl"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CommonService.GetIsThisUnit(Const.UnitId_SEDIN) || CommonService.GetIsThisUnit(Const.UnitId_7))
                {
                    this.Image2.Hidden = false;
                    this.fileSignature.Hidden = false;
                }
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///权限
                this.GetButtonPower();
                this.UserId = Request.Params["userId"];
                this.UnitId = Request.Params["UnitId"];
                ConstValue.InitConstValueDropDownList(this.drpIsPost, ConstValue.Group_0001, false);
                ConstValue.InitConstValueDropDownList(this.drpIsOffice, ConstValue.Group_0001, false);
                UnitService.InitUnitDropDownList(this.drpUnit, this.CurrUser.LoginProjectId, true);
                DepartService.InitDepartDropDownList(this.drpDepart, true);
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                }
                if (!string.IsNullOrEmpty(this.UnitId))
                {
                    this.drpUnit.SelectedValue = this.UnitId;
                    this.drpIsOffice.SelectedValue= "False";
                }
                if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
                {
                    this.drpUnit.Enabled = false;
                }

                ///角色下拉框
                BLL.RoleService.InitRoleDropDownList(this.drpRole, string.Empty, true);
                if (!string.IsNullOrEmpty(this.UserId))
                {
                    var user = BLL.UserService.GetUserByUserId(this.UserId);
                    if (user != null)
                    {
                        if (!string.IsNullOrEmpty(user.UnitId))
                        {
                            this.drpUnit.SelectedValue = user.UnitId;
                        }
                        this.txtUserCode.Text = user.UserCode;
                        this.txtUserName.Text = user.UserName;
                        this.txtAccount.Text = user.Account;
                        if (!string.IsNullOrEmpty(user.RoleId))
                        {
                            this.drpRole.SelectedValue = user.RoleId;
                        }                       
                        if (user.IsPost.HasValue)
                        {
                            this.drpIsPost.SelectedValue = Convert.ToString(user.IsPost);
                        }
                        this.txtTelephone.Text = user.Telephone;
                        if (user.IsOffice == true)
                        {
                            this.drpIsOffice.SelectedValue = "True";
                        }
                        else
                        {
                            this.drpIsOffice.SelectedValue = "False";
                        }
                        this.txtIdentityCard.Text = user.IdentityCard;
                        if (!string.IsNullOrEmpty(user.SignatureUrl))
                        {
                            this.SignatureUrl = user.SignatureUrl;
                            this.Image2.ImageUrl = "~/" + this.SignatureUrl;
                        }
                        this.drpDepart.SelectedValue = user.DepartId;
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue == Const._Null)
            {
                Alert.ShowInParent("请选择单位！", MessageBoxIcon.Warning);
                return;
            }
            if (BLL.UserService.IsExistUserAccount(this.UserId, this.txtAccount.Text.Trim()))
            {
                Alert.ShowInParent("用户账号已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("身份证号码已存在，请修改后再保存！", MessageBoxIcon.Warning);
                return;
            }
            //if (this.txtIdentityCard.Text.Trim().Length!=18)
            //{
            //    ShowNotify("身份证号码必须是18位！", MessageBoxIcon.Warning);
            //    return;
            //}
            Model.Sys_User newUser = new Model.Sys_User
            {
                UserCode = this.txtUserCode.Text.Trim(),
                UserName = this.txtUserName.Text.Trim(),
                Account = this.txtAccount.Text.Trim(),
                IdentityCard = this.txtIdentityCard.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
            };
            if (this.drpUnit.SelectedValue != Const._Null)
            {
                newUser.UnitId = this.drpUnit.SelectedValue;
            }
            if (!BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId)) ///不是企业单位或者管理员
            {
                newUser.UnitId = this.CurrUser.UnitId;
            }
            if (this.drpRole.SelectedValue != Const._Null)
            {
                newUser.RoleId = this.drpRole.SelectedValue;
            }
            if (this.drpDepart.SelectedValue != Const._Null)
            {
                newUser.DepartId = this.drpDepart.SelectedValue;
            }
            newUser.SignatureUrl = this.SignatureUrl;
            newUser.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
            newUser.IsOffice = Convert.ToBoolean(this.drpIsOffice.SelectedValue);
            if (string.IsNullOrEmpty(this.UserId))
            {
                newUser.Password = Funs.EncryptionPassword(Const.Password);
                newUser.UserId = SQLHelper.GetNewID(typeof(Model.Sys_User));
                newUser.DataSources = this.CurrUser.LoginProjectId;
                UserService.AddUser(newUser);
                SaveHSSEManageItem();//增加组织机构明细
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.UserMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                newUser.UserId = this.UserId;
                UserService.UpdateUser(newUser);
                LogService.AddSys_Log(this.CurrUser, newUser.UserCode, newUser.UserId, BLL.Const.UserMenuId, BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        #region 增加组织机构明细
        /// <summary>
        /// 增加组织机构明细
        /// </summary>
        private void SaveHSSEManageItem()
        {
            var hsseManage = Funs.DB.HSSESystem_HSSEManage.FirstOrDefault(x => x.HSSEManageName == this.drpUnit.SelectedText.Trim());
            if (hsseManage != null)
            {
                Model.HSSESystem_HSSEManageItem hsseManageItem = new Model.HSSESystem_HSSEManageItem();
                hsseManageItem.HSSEManageItemId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEManageItem));
                hsseManageItem.HSSEManageId = hsseManage.HSSEManageId;
                hsseManageItem.Names = this.txtUserName.Text.Trim();
                BLL.HSSEManageItemService.AddHSSEManageItem(hsseManageItem);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                this.btnSave.Hidden = false;
            }
            else
            {
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UserMenuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnSave))
                    {
                        this.btnSave.Hidden = false;
                    }
                }
            }
        }
        #endregion

        #region 验证用户编号、账号是否存在
        /// <summary>
        /// 验证用户编号、账号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Sys_User.FirstOrDefault(x => x.Account == this.txtAccount.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q != null)
            {
                ShowNotify("输入的账号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Sys_User.FirstOrDefault(x => x.UserCode == this.txtUserCode.Text.Trim() && (x.UserId != this.UserId || (this.UserId == null && x.UserId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text) && BLL.UserService.IsExistUserIdentityCard(this.UserId, this.txtIdentityCard.Text.Trim()) == true)
            {
                ShowNotify("输入的身份证号码已存在！", MessageBoxIcon.Warning);                
            }
        }
        #endregion

        #region 上传签名
        /// <summary>
        /// 上传签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSignature_Click(object sender, EventArgs e)
        {
            if (fileSignature.HasFile)
            {
                string fileName = fileSignature.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.SignatureUrl = UploadFileService.UploadAttachment(Funs.RootPath, this.fileSignature, this.SignatureUrl, UploadFileService.UserFilePath);
                this.Image2.ImageUrl = "~/" + this.SignatureUrl;
            }
        }
        #endregion   
    }
}