using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Administrative
{
    public partial class CarManagerEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CarManagerId
        {
            get
            {
                return (string)ViewState["CarManagerId"];
            }
            set
            {
                ViewState["CarManagerId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.CarManagerId = Request.Params["CarManagerId"];
                if (!string.IsNullOrEmpty(this.CarManagerId))
                {
                    Model.Administrative_CarManager carManager = BLL.CarManagerService.GetCarManagerById(this.CarManagerId);
                    if (carManager != null)
                    {
                        this.ProjectId = carManager.ProjectId;
                        this.txtCarManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CarManagerId);
                        this.txtCarName.Text = carManager.CarName;
                        this.txtCarModel.Text = carManager.CarModel;
                        this.txtBuyDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.BuyDate);
                        this.txtLastYearCheckDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.LastYearCheckDate);
                        this.txtInsuranceDate.Text = string.Format("{0:yyyy-MM-dd}", carManager.InsuranceDate);
                        this.txtRemark.Text = carManager.Remark;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtCarManagerCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.CarManagerMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtBuyDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.txtLastYearCheckDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddYears(1));
                    this.txtInsuranceDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddYears(1));
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.CarManagerMenuId;
                this.ctlAuditFlow.DataId = this.CarManagerId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Administrative_CarManager carManager = new Model.Administrative_CarManager
            {
                ProjectId = this.ProjectId,
                CarManagerCode = this.txtCarManagerCode.Text.Trim(),
                CarName = this.txtCarName.Text.Trim(),
                CarModel = this.txtCarModel.Text.Trim(),
                BuyDate = Funs.GetNewDateTime(this.txtBuyDate.Text.Trim()),
                LastYearCheckDate = Funs.GetNewDateTime(this.txtLastYearCheckDate.Text.Trim()),
                InsuranceDate = Funs.GetNewDateTime(this.txtInsuranceDate.Text.Trim()),
                Remark = this.txtRemark.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
                States = BLL.Const.State_0
            };
            if (type==BLL.Const.BtnSubmit)
            {
                carManager.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CarManagerId))
            {
                carManager.CarManagerId = this.CarManagerId;
                BLL.CarManagerService.UpdateCarManager(carManager);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改现场车辆管理", carManager.CarManagerId);
            }
            else
            {
                this.CarManagerId = SQLHelper.GetNewID(typeof(Model.Administrative_CarManager));
                carManager.CarManagerId = this.CarManagerId;
                BLL.CarManagerService.AddCarManager(carManager);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改现场车辆管理", carManager.CarManagerId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.CarManagerMenuId, this.CarManagerId, (type == BLL.Const.BtnSubmit ? true : false), carManager.CarName, "../Administrative/CarManagerView.aspx?CarManagerId={0}");
        }
        #endregion
    }
}