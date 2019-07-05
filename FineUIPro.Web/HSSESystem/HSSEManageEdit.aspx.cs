using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.HSSESystem
{
    public partial class HSSEManageEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string HSSEManageId
        {
            get
            {
                return (string)ViewState["HSSEManageId"];
            }
            set
            {
                ViewState["HSSEManageId"] = value;
            }
        }

        /// <summary>
        /// 上级节点id
        /// </summary>
        public string SupHSSEManageId
        {
            get
            {
                return (string)ViewState["SupHSSEManageId"];
            }
            set
            {
                ViewState["SupHSSEManageId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                ////权限按钮方法
                this.GetButtonPower();
                this.SupHSSEManageId = Request.Params["SupHSSEManageId"];
                this.HSSEManageId = Request.Params["HSSEManageId"];
                if (!string.IsNullOrEmpty(this.HSSEManageId))
                {
                    Model.HSSESystem_HSSEManage hsseManage = BLL.HSSEManageService.GetHSSEManageById(this.HSSEManageId);
                    if (hsseManage != null)
                    {
                        this.txtHSSEManageCode.Text = hsseManage.HSSEManageCode;
                        this.txtHSSEManageName.Text = hsseManage.HSSEManageName;                       
                        this.SupHSSEManageId = hsseManage.SupHSSEManageId;
                    }
                }
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
            Model.HSSESystem_HSSEManage hsseManage = new Model.HSSESystem_HSSEManage();
            if (!string.IsNullOrEmpty(this.txtHSSEManageCode.Text.Trim()))
            {
                hsseManage.HSSEManageCode = this.txtHSSEManageCode.Text.Trim();
            }
            else
            {
                ShowNotify("请输入编号！",MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtHSSEManageName.Text.Trim()))
            {
                hsseManage.HSSEManageName = this.txtHSSEManageName.Text.Trim();
            }
            else
            {
                ShowNotify("请输入机构名称",MessageBoxIcon.Warning);
                return;
            }           
            hsseManage.SupHSSEManageId = this.SupHSSEManageId;
            if (string.IsNullOrEmpty(this.HSSEManageId))
            {
                hsseManage.HSSEManageId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEManage));
                BLL.HSSEManageService.AddHSSEManage(hsseManage);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全管理机构");
            }
            else
            {
                hsseManage.HSSEManageId = this.HSSEManageId;
                BLL.HSSEManageService.UpdateHSSEManage(hsseManage);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全管理机构");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEManageMenuId);
            if (buttonList.Count() > 0)
            {
                
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        

        #region 验证安全管理机构名称是否存在
        /// <summary>
        /// 验证安全管理机构名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.HSSESystem_HSSEManage.FirstOrDefault(x => x.SupHSSEManageId==this.SupHSSEManageId && x.HSSEManageName == this.txtHSSEManageName.Text.Trim() && (x.HSSEManageId != this.HSSEManageId || (this.HSSEManageId == null && x.HSSEManageId != null)));
            if (standard != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}