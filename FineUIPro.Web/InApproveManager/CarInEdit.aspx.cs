using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class CarInEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键 
        /// </summary>
        private string CarInId
        {
            get
            {
                return (string)ViewState["CarInId"];
            }
            set
            {
                ViewState["CarInId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.CarInId = Request.Params["CarInId"];
                if (!string.IsNullOrEmpty(this.CarInId))
                {
                    Model.InApproveManager_CarIn carIn = BLL.CarInService.GetCarInById(this.CarInId);
                    if (carIn!=null)
                    {
                        this.ProjectId = carIn.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        if (!string.IsNullOrEmpty(carIn.UnitId))
                        {
                            this.drpUnitId.SelectedValue = carIn.UnitId;
                        }
                        this.txtDriverName.Text = carIn.DriverName;
                        this.txtCarNum.Text = carIn.CarNum;
                        this.txtCarType.Text = carIn.CarType;
                    }
                    List<Model.InApproveManager_CarInItem> carChecks = BLL.CarInItemService.GetCarInItemByCarInId(this.CarInId);
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
                this.ctlAuditFlow.MenuId = BLL.Const.CarInMenuId;
                this.ctlAuditFlow.DataId = this.CarInId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CarInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CarInAttachUrl&menuId={1}", this.CarInId, BLL.Const.CarInMenuId)));
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InApproveManager_CarIn carIn = new Model.InApproveManager_CarIn
            {
                ProjectId = this.ProjectId
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                carIn.UnitId = this.drpUnitId.SelectedValue;
            }
            carIn.DriverName = this.txtDriverName.Text.Trim();
            carIn.CarNum = this.txtCarNum.Text.Trim();
            carIn.CarType = this.txtCarType.Text.Trim();
            carIn.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                carIn.States = this.ctlAuditFlow.NextStep;
            }
            carIn.CompileMan = this.CurrUser.UserId;
            carIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(CarInId))
            {
                carIn.CarInId = this.CarInId;
                BLL.CarInService.UpdateCarIn(carIn);
                BLL.LogService.AddSys_Log(this.CurrUser, carIn.CarNum, carIn.CarInId,BLL.Const.CarInMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.CarInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_CarIn));
                carIn.CarInId = this.CarInId;
                BLL.CarInService.AddCarIn(carIn);
                BLL.LogService.AddSys_Log(this.CurrUser, carIn.CarNum, carIn.CarInId, BLL.Const.CarInMenuId, BLL.Const.BtnAdd);
            }
            BLL.CarInItemService.DeleteCarInItemByCarInId(this.CarInId);
            Model.InApproveManager_CarInItem carInItem = new Model.InApproveManager_CarInItem
            {
                CarInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_CarInItem)),
                CarInId = this.CarInId,
                CheckItem1 = this.ckbCheckItem1.Checked,
                CheckItem2 = this.ckbCheckItem2.Checked,
                CheckItem3 = this.ckbCheckItem3.Checked,
                CheckItem4 = this.ckbCheckItem4.Checked,
                CheckItem5 = this.ckbCheckItem5.Checked,
                CheckItem6 = this.ckbCheckItem6.Checked,
                CheckItem7 = this.ckbCheckItem7.Checked,
                CheckItem8 = this.ckbCheckItem8.Checked,
                CheckItem9 = this.ckbCheckItem9.Checked,
                CheckItem10 = this.ckbCheckItem10.Checked,
                CheckItem11 = this.ckbCheckItem11.Checked,
                CheckItem12 = this.ckbCheckItem12.Checked,
                CheckItem13 = this.ckbCheckItem13.Checked
            };
            BLL.CarInItemService.AddCarInItem(carInItem);

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.CarInMenuId, this.CarInId, (type == BLL.Const.BtnSubmit ? true : false), (carIn.DriverName + carIn.CarNum), "../InApproveManager/CarInView.aspx?CarInId={0}");
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion
    }
}