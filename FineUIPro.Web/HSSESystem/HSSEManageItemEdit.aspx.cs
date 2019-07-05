using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HSSESystem
{
    public partial class HSSEManageItemEdit : PageBase
    {
        public string HSSEManageItemId
        {
            get
            {
                return (string)ViewState["HSSEManageItemId"];
            }
            set
            {
                ViewState["HSSEManageItemId"] = value;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.HSSEManageId = Request.Params["HSSEManageId"];
                this.HSSEManageItemId = Request.Params["HSSEManageItemId"];
                if (!string.IsNullOrEmpty(this.HSSEManageItemId))
                {
                    var item = BLL.HSSEManageItemService.GetHSSEManageItemById(this.HSSEManageItemId);
                    if (item!=null)
                    {
                        this.txtPost.Text = item.Post;
                        this.txtNames.Text = item.Names;
                        this.txtTelephone.Text = item.Telephone;
                        this.txtMobilePhone.Text = item.MobilePhone;
                        this.txtEMail.Text = item.EMail;
                        this.txtDuty.Text = item.Duty;
                        this.txtSortIndex.Text = item.SortIndex;
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
            Model.HSSESystem_HSSEManageItem newItem = new Model.HSSESystem_HSSEManageItem
            {
                Post = this.txtPost.Text.Trim(),
                Names = this.txtNames.Text.Trim(),
                Telephone = this.txtTelephone.Text.Trim(),
                MobilePhone = this.txtMobilePhone.Text.Trim(),
                EMail = this.txtEMail.Text.Trim(),
                Duty = this.txtDuty.Text.Trim(),
                SortIndex = this.txtSortIndex.Text.Trim()
            };
            if (string.IsNullOrEmpty(this.HSSEManageItemId))
            {
                newItem.HSSEManageId = this.HSSEManageId;
                this.HSSEManageItemId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEManageItem));
                newItem.HSSEManageItemId = this.HSSEManageItemId;
                BLL.HSSEManageItemService.AddHSSEManageItem(newItem);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加安全管理机构", this.HSSEManageItemId);
            }
            else
            {
                var i = BLL.HSSEManageItemService.GetHSSEManageItemById(this.HSSEManageItemId);
                if (i!=null)
                {
                    newItem.HSSEManageId = i.HSSEManageId;
                }
                newItem.HSSEManageItemId = this.HSSEManageItemId;
                BLL.HSSEManageItemService.UpdateHSSEManageItem(newItem);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改安全管理机构", this.HSSEManageItemId);
            }

            ///更新集团组织机构
            this.UpHSSEManageList(this.HSSEManageId);

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

        #region 组织机构上报
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void UpHSSEManageList(string hsseManageId)
        {
            var unit = BLL.CommonService.GetIsThisUnit();
            var hsseMange = BLL.HSSEManageService.GetHSSEManageById(hsseManageId);
            if (unit != null && hsseMange != null && unit.UnitName == hsseMange.HSSEManageName)
            {

                ////创建客户端服务
                var poxy = Web.ServiceProxy.CreateServiceClient();
                poxy.DataInsertHSSESystem_HSSEManageItemTableCompleted += new EventHandler<HSSEService.DataInsertHSSESystem_HSSEManageItemTableCompletedEventArgs>(poxy_DataInsertHSSESystem_HSSEManageItemTableCompleted);
                var HSSEStandardsList = from x in Funs.DB.HSSESystem_HSSEManageItem
                                        where x.HSSEManageId == hsseManageId
                                        select new HSSEService.HSSESystem_HSSEManageItem
                                        {
                                            HSSEManageItemId = x.HSSEManageItemId,
                                            HSSEManageName = hsseMange.HSSEManageName,
                                            Post = x.Post,
                                            Names = x.Names,
                                            Telephone = x.Telephone,
                                            MobilePhone = x.MobilePhone,
                                            EMail = x.EMail,
                                            Duty = x.Duty,
                                            SortIndex = x.SortIndex,
                                        };
                poxy.DataInsertHSSESystem_HSSEManageItemTableAsync(HSSEStandardsList.ToList());
            }
        }

        /// <summary>
        /// 标准规范上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertHSSESystem_HSSEManageItemTableCompleted(object sender, HSSEService.DataInsertHSSESystem_HSSEManageItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {               
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【组织机构】上报到集团公司成功；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【组织机构】上报到集团公司失败；");
            }
        }
        #endregion
    }
}