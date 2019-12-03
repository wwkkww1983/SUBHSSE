using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.HSSESystem
{
    public partial class HSSEOrganize : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                var units = BLL.CommonService.GetIsThisUnit();
                if (units != null)
                {
                    this.hdUnitId.Text = units.UnitId;
                    var organize = BLL.HSSEOrganizeService.GetHSSEOrganizeByUnitId(units.UnitId);
                    if (organize != null)
                    {
                        this.hdHSSEOrganizeId.Text = organize.HSSEOrganizeId;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(organize.SeeFile);
                    }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEOrganizeMenuId);
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
        ///  保存按钮事件
        /// </summary>
        /// <param name="isClose"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.HSSESystem_HSSEOrganize organize = new Model.HSSESystem_HSSEOrganize
            {
                SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text),
                UnitId = this.hdUnitId.Text
            };
            if (!string.IsNullOrEmpty(organize.UnitId))
            {
                if (string.IsNullOrEmpty(this.hdHSSEOrganizeId.Text))
                {
                    this.hdHSSEOrganizeId.Text = organize.HSSEOrganizeId = SQLHelper.GetNewID(typeof(Model.HSSESystem_HSSEOrganize));
                    BLL.HSSEOrganizeService.AddHSSEOrganize(organize);
                    BLL.LogService.AddSys_Log(this.CurrUser, null, organize.HSSEOrganizeId,BLL.Const.HSSEOrganizeMenuId,BLL.Const.BtnModify);
                }
                else
                {
                    organize.HSSEOrganizeId = this.hdHSSEOrganizeId.Text;
                    BLL.HSSEOrganizeService.UpdateHSSEOrganize(organize);
                    BLL.LogService.AddSys_Log(this.CurrUser, null, organize.HSSEOrganizeId, BLL.Const.HSSEOrganizeMenuId, BLL.Const.BtnAdd);
                }

                this.UpHSSEOrganize(organize);
                ShowNotify("数据保存成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请先选择单位!", MessageBoxIcon.Warning);
            }
        }

        #region 组织体系上报到集团单位
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpHSSEOrganize(Model.HSSESystem_HSSEOrganize organize)
        {  /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertHSSESystem_HSSEOrganizeTableCompleted += new EventHandler<HSSEService.DataInsertHSSESystem_HSSEOrganizeTableCompletedEventArgs>(poxy_DataInsertHSSESystem_HSSEOrganizeTableCompleted);
            var newOrganize = new HSSEService.HSSESystem_HSSEOrganize
            {
                UnitId = organize.UnitId,
                SeeFile = organize.SeeFile
            };
            poxy.DataInsertHSSESystem_HSSEOrganizeTableAsync(newOrganize);
        }

        /// <summary>
        /// 组织体系上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertHSSESystem_HSSEOrganizeTableCompleted(object sender, HSSEService.DataInsertHSSESystem_HSSEOrganizeTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ShowNotify("【安全体系】上报成功!", MessageBoxIcon.Success);
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全体系】上传到服务器1条数据；", null, BLL.Const.HSSEOrganizeMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                ShowNotify("【安全体系】上报失败!", MessageBoxIcon.Error);
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全体系】上传到服务器失败；", null, BLL.Const.HSSEOrganizeMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
    }
}