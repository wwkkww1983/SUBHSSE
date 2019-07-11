using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GeneralCarInEdit : PageBase
    {
        /// <summary>
        /// 主键 
        /// </summary>
        private string GeneralCarInId
        {
            get
            {
                return (string)ViewState["GeneralCarInId"];
            }
            set
            {
                ViewState["GeneralCarInId"] = value;
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
                this.GeneralCarInId = Request.Params["GeneralCarInId"];
                if (!string.IsNullOrEmpty(this.GeneralCarInId))
                {
                    Model.InApproveManager_GeneralCarIn carIn = BLL.GeneralCarInService.GetGeneralCarInById(this.GeneralCarInId);
                    if (carIn != null)
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
                        this.txtDescription.Text = carIn.Descriptions;
                    }
                    List<Model.InApproveManager_GeneralCarInItem> carChecks = BLL.GeneralCarInItemService.GetGeneralCarInItemByGeneralCarInId(this.GeneralCarInId);
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
                this.ctlAuditFlow.MenuId = BLL.Const.GeneralCarInMenuId;
                this.ctlAuditFlow.DataId = this.GeneralCarInId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

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
            if (string.IsNullOrEmpty(this.GeneralCarInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralCarInAttachUrl&menuId={1}", this.GeneralCarInId, BLL.Const.GeneralCarInMenuId)));
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InApproveManager_GeneralCarIn carIn = new Model.InApproveManager_GeneralCarIn
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
            carIn.Descriptions = this.txtDescription.Text.Trim();
            carIn.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                carIn.States = this.ctlAuditFlow.NextStep;
            }
            carIn.CompileMan = this.CurrUser.UserId;
            carIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(GeneralCarInId))
            {
                carIn.GeneralCarInId = this.GeneralCarInId;
                BLL.GeneralCarInService.UpdateGeneralCarIn(carIn);
                BLL.LogService.AddSys_Log(this.CurrUser, carIn.CarNum, carIn.GeneralCarInId, BLL.Const.GeneralCarInMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GeneralCarInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralCarIn));
                carIn.GeneralCarInId = this.GeneralCarInId;
                BLL.GeneralCarInService.AddGeneralCarIn(carIn);
                BLL.LogService.AddSys_Log(this.CurrUser, carIn.CarNum, carIn.GeneralCarInId,BLL.Const.GeneralCarInMenuId,BLL.Const.BtnAdd);
            }
            BLL.GeneralCarInItemService.DeleteGeneralCarInItemByGeneralCarInId(this.GeneralCarInId);
            Model.InApproveManager_GeneralCarInItem carInItem = new Model.InApproveManager_GeneralCarInItem
            {
                GeneralCarInItemId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralCarInItem)),
                GeneralCarInId = this.GeneralCarInId,
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
            BLL.GeneralCarInItemService.AddGeneralCarInItem(carInItem);

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GeneralCarInMenuId, this.GeneralCarInId, (type == BLL.Const.BtnSubmit ? true : false), (carIn.DriverName + carIn.CarNum), "../InApproveManager/GeneralCarInView.aspx?GeneralCarInId={0}");
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
    }
}