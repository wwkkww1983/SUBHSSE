using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.License
{
    public partial class EquipmentSafetyListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string EquipmentSafetyListId
        {
            get
            {
                return (string)ViewState["EquipmentSafetyListId"];
            }
            set
            {
                ViewState["EquipmentSafetyListId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.EquipmentSafetyListId = Request.Params["EquipmentSafetyListId"];
                if (!string.IsNullOrEmpty(this.EquipmentSafetyListId))
                {
                    Model.License_EquipmentSafetyList equipmentSafetyList = BLL.EquipmentSafetyListService.GetEquipmentSafetyListById(this.EquipmentSafetyListId);
                    if (equipmentSafetyList != null)
                    {
                        this.ProjectId = equipmentSafetyList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtEquipmentSafetyListCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.EquipmentSafetyListId);
                        this.txtEquipmentSafetyListName.Text = equipmentSafetyList.EquipmentSafetyListName;
                        if (!string.IsNullOrEmpty(equipmentSafetyList.UnitId))
                        {
                            this.drpUnitId.SelectedValue = equipmentSafetyList.UnitId;
                        }
                        if (!string.IsNullOrEmpty(equipmentSafetyList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = equipmentSafetyList.CompileMan;
                        }
                        if (!string.IsNullOrEmpty(equipmentSafetyList.WorkAreaId))
                        {
                            this.drpWorkAreaId.SelectedValue = equipmentSafetyList.WorkAreaId;
                        }
                        if (equipmentSafetyList.EquipmentSafetyListCount.HasValue)
                        {
                            this.txtEquipmentSafetyListCount.Text = Convert.ToString(equipmentSafetyList.EquipmentSafetyListCount);
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", equipmentSafetyList.CompileDate);
                        this.txtSendMan.Text = equipmentSafetyList.SendMan;
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                    ////自动生成编码
                    this.txtEquipmentSafetyListCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectEquipmentSafetyListMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectEquipmentSafetyListMenuId;
                this.ctlAuditFlow.DataId = this.EquipmentSafetyListId;
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

            this.drpWorkAreaId.DataValueField = "WorkAreaId";
            this.drpWorkAreaId.DataTextField = "WorkAreaName";
            this.drpWorkAreaId.DataSource = BLL.WorkAreaService.GetWorkAreaByProjectList(this.ProjectId);
            this.drpWorkAreaId.DataBind();
            Funs.FineUIPleaseSelect(this.drpWorkAreaId);

            this.drpCompileMan.DataValueField = "UserId";
            this.drpCompileMan.DataTextField = "UserName";
            this.drpCompileMan.DataSource = BLL.UserService.GetUserList();
            this.drpCompileMan.DataBind();
            Funs.FineUIPleaseSelect(this.drpCompileMan);
        }

        #region 保存
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
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择申请单位！", MessageBoxIcon.Warning);
                return;
            }
            Model.License_EquipmentSafetyList equipmentSafetyList = new Model.License_EquipmentSafetyList
            {
                ProjectId = this.ProjectId,
                EquipmentSafetyListCode = this.txtEquipmentSafetyListCode.Text.Trim(),
                EquipmentSafetyListName = this.txtEquipmentSafetyListName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                equipmentSafetyList.UnitId = this.drpUnitId.SelectedValue;
            }
            equipmentSafetyList.EquipmentSafetyListCount = Funs.GetNewInt(this.txtEquipmentSafetyListCount.Text.Trim());
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                equipmentSafetyList.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                equipmentSafetyList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            equipmentSafetyList.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            equipmentSafetyList.SendMan = this.txtSendMan.Text.Trim();
            equipmentSafetyList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                equipmentSafetyList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.EquipmentSafetyListId))
            {
                equipmentSafetyList.EquipmentSafetyListId = this.EquipmentSafetyListId;
                BLL.EquipmentSafetyListService.UpdateEquipmentSafetyList(equipmentSafetyList);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改施工机具、安全设施检查验收", equipmentSafetyList.EquipmentSafetyListId);
            }
            else
            {
                this.EquipmentSafetyListId = SQLHelper.GetNewID(typeof(Model.License_EquipmentSafetyList));
                equipmentSafetyList.EquipmentSafetyListId = this.EquipmentSafetyListId;
                BLL.EquipmentSafetyListService.AddEquipmentSafetyList(equipmentSafetyList);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加施工机具、安全设施检查验收", equipmentSafetyList.EquipmentSafetyListId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectEquipmentSafetyListMenuId, this.EquipmentSafetyListId, (type == BLL.Const.BtnSubmit ? true : false), equipmentSafetyList.EquipmentSafetyListName, "../License/EquipmentSafetyListView.aspx?EquipmentSafetyListId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.EquipmentSafetyListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentSafetyListAttachUrl&menuId={1}", EquipmentSafetyListId, BLL.Const.ProjectEquipmentSafetyListMenuId)));
        }
        #endregion
    }
}