using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Emergency
{
    public partial class DrillRecordListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillRecordListId
        {
            get
            {
                return (string)ViewState["DrillRecordListId"];
            }
            set
            {
                ViewState["DrillRecordListId"] = value;
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
                this.DrillRecordListId = Request.Params["DrillRecordListId"];
                if (!string.IsNullOrEmpty(this.DrillRecordListId))
                {
                    Model.Emergency_DrillRecordList DrillRecordList = BLL.DrillRecordListService.GetDrillRecordListById(this.DrillRecordListId);
                    if (DrillRecordList != null)
                    {
                        this.ProjectId = DrillRecordList.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtDrillRecordCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.DrillRecordListId);
                        this.txtDrillRecordName.Text = DrillRecordList.DrillRecordName;
                        if (!string.IsNullOrEmpty(DrillRecordList.UnitIds))
                        {
                            this.drpUnits.SelectedValueArray = DrillRecordList.UnitIds.Split(',');
                        }
                        if (!string.IsNullOrEmpty(DrillRecordList.DrillRecordType))
                        {
                            this.drpDrillRecordType.SelectedValue = DrillRecordList.DrillRecordType;
                        }
                        this.txtDrillRecordDate.Text = string.Format("{0:yyyy-MM-dd}", DrillRecordList.DrillRecordDate);
                        if(DrillRecordList.JointPersonNum!=null)
                        {
                            this.txtJointPersonNum.Text = DrillRecordList.JointPersonNum.ToString();
                        }
                        if (DrillRecordList.DrillCost != null)
                        {
                            this.txtDrillCost.Text = DrillRecordList.DrillCost.ToString();
                        }
                        this.txtDrillRecordContents.Text = HttpUtility.HtmlDecode(DrillRecordList.DrillRecordContents);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                    {
                        this.drpUnits.SelectedValue = this.CurrUser.UnitId;
                    }
                    this.txtDrillRecordDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectDrillRecordListMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtDrillRecordContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtDrillRecordCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectDrillRecordListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtDrillRecordName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillRecordListMenuId;
                this.ctlAuditFlow.DataId = this.DrillRecordListId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpUnits, this.ProjectId, false);
            BLL.ConstValue.InitConstValueDropDownList(this.drpDrillRecordType, BLL.ConstValue.Group_DrillRecordType, false);    
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
            Model.Emergency_DrillRecordList DrillRecordList = new Model.Emergency_DrillRecordList
            {
                ProjectId = this.ProjectId,
                DrillRecordCode = this.txtDrillRecordCode.Text.Trim(),
                DrillRecordName = this.txtDrillRecordName.Text.Trim(),
                DrillRecordDate = Funs.GetNewDateTime(this.txtDrillRecordDate.Text.Trim()),
                DrillRecordType = this.drpDrillRecordType.SelectedValue,
                DrillRecordContents = HttpUtility.HtmlEncode(this.txtDrillRecordContents.Text),
                JointPersonNum = Funs.GetNewIntOrZero(this.txtJointPersonNum.Text.Trim()),
                DrillCost = Funs.GetNewDecimalOrZero(this.txtDrillCost.Text.Trim())
            };
            //参与单位
            string unitIds = string.Empty;
            string unitNames = string.Empty;
            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(item);
                if (unit != null)
                {
                    unitIds += unit.UnitId + ",";
                    unitNames += unit.UnitName + ",";
                }
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                DrillRecordList.UnitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
                DrillRecordList.UnitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
            }
            ////单据状态
            DrillRecordList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                DrillRecordList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.DrillRecordListId))
            {
                DrillRecordList.DrillRecordListId = this.DrillRecordListId;
                BLL.DrillRecordListService.UpdateDrillRecordList(DrillRecordList);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改应急演练", DrillRecordList.DrillRecordListId);
            }
            else
            {
                this.DrillRecordListId = SQLHelper.GetNewID(typeof(Model.Emergency_DrillRecordList));
                DrillRecordList.CompileMan = this.CurrUser.UserId;
                DrillRecordList.DrillRecordListId = this.DrillRecordListId;
                BLL.DrillRecordListService.AddDrillRecordList(DrillRecordList);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加应急演练", DrillRecordList.DrillRecordListId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectDrillRecordListMenuId, this.DrillRecordListId, (type == BLL.Const.BtnSubmit ? true : false), DrillRecordList.DrillRecordName, "../Emergency/DrillRecordListView.aspx?DrillRecordListId={0}");
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
            if (string.IsNullOrEmpty(this.DrillRecordListId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/DrillRecordListAttachUrl&menuId={1}", DrillRecordListId,BLL.Const.ProjectDrillRecordListMenuId)));
        }
        #endregion
    }
}