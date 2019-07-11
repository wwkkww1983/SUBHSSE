using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayXAEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckDayId
        {
            get
            {
                return (string)ViewState["CheckDayId"];
            }
            set
            {
                ViewState["CheckDayId"] = value;
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.CheckDayId = Request.Params["CheckDayId"];
                var checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.ProjectId = checkDay.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckDayId);
                    if (checkDay.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkDay.CheckDate);
                    }
                    if (checkDay.NotOKNum != null)
                    {
                        this.txtNotOKNum.Text = checkDay.NotOKNum.ToString();
                    }
                    if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, checkDay.CompileUnit))  //施工单位
                    {
                        this.trTeamGroup.Hidden = false;
                        if (!string.IsNullOrEmpty(checkDay.DutyTeamGroupIds))
                        {
                            this.drpDutyTeamGroupIds.SelectedValueArray = checkDay.DutyTeamGroupIds.Split(',');
                        }
                    }
                    else
                    {
                        this.trUnit.Hidden = false;
                        if (!string.IsNullOrEmpty(checkDay.DutyUnitIds))
                        {
                            this.drpDutyUnitIds.SelectedValueArray = checkDay.DutyUnitIds.Split(',');
                        }
                    }
                    if (!string.IsNullOrEmpty(checkDay.WorkAreaIds))
                    {
                        this.drpWorkAreaIds.SelectedValueArray = checkDay.WorkAreaIds.Split(',');
                    }
                    if (!string.IsNullOrEmpty(checkDay.CompileMan))
                    {
                        this.drpCheckMan.SelectedValue = checkDay.CompileMan;
                    }
                    this.txtUnqualified.Text = checkDay.Unqualified;
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckDayXAMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))  //施工单位
                    {
                           this.trTeamGroup.Hidden = false;
                    }
                    else
                    {
                      this.trUnit.Hidden = false;
                    }
                    this.drpCheckMan.SelectedValue = this.CurrUser.UserId;
                }
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            //受检单位            
            BLL.UnitService.InitUnitDropDownList(this.drpDutyUnitIds, this.ProjectId, false);
            //受检班组
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpCheckMan, this.ProjectId, this.CurrUser.UnitId, false);
            //受检班组
            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpDutyTeamGroupIds, this.ProjectId, this.CurrUser.UnitId, false);
            //检查区域
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaIds, this.ProjectId, false);
        }

        private void SaveNew()
        {
            Model.Check_CheckDayXA checkDay = new Model.Check_CheckDayXA
            {
                CheckDayId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayXA)),
                CheckDayCode = this.txtCheckDayCode.Text.Trim(),
                ProjectId = this.ProjectId,
                NotOKNum = Funs.GetNewIntOrZero(this.txtNotOKNum.Text.Trim()),
                CheckDate = Funs.GetNewDateTimeOrNow(this.txtCheckDate.Text.Trim())
            };
            if (this.trUnit.Hidden == false)
            {
                //受检单位
                string unitIds = string.Empty;
                foreach (var item in this.drpDutyUnitIds.SelectedValueArray)
                {
                    unitIds += item + ",";
                }
                if (!string.IsNullOrEmpty(unitIds))
                {
                    unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
                }
                checkDay.DutyUnitIds = unitIds;
            }
            if (this.trTeamGroup.Hidden == false)
            {
                //受检班组
                string teamGroupIds = string.Empty;
                foreach (var item in this.drpDutyTeamGroupIds.SelectedValueArray)
                {
                    teamGroupIds += item + ",";
                }
                if (!string.IsNullOrEmpty(teamGroupIds))
                {
                    teamGroupIds = teamGroupIds.Substring(0, teamGroupIds.LastIndexOf(","));
                }
                checkDay.DutyTeamGroupIds = teamGroupIds;
            }
            //检查区域
            string workAreaIds = string.Empty;
            foreach (var item in this.drpWorkAreaIds.SelectedValueArray)
            {
                workAreaIds += item + ",";
            }
            if (!string.IsNullOrEmpty(workAreaIds))
            {
                workAreaIds = workAreaIds.Substring(0, workAreaIds.LastIndexOf(","));
            }
            checkDay.WorkAreaIds = workAreaIds;
            checkDay.Unqualified = this.txtUnqualified.Text.Trim();
            ////单据状态
            checkDay.States = BLL.Const.State_0;
            this.CheckDayId = checkDay.CheckDayId;
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                checkDay.CompileMan = this.drpCheckMan.SelectedValue;
            }
            else
            {
                checkDay.CompileMan = this.CurrUser.UserId;
            }
            checkDay.CompileUnit = this.CurrUser.UnitId;
            BLL.Check_CheckDayXAService.AddCheckDay(checkDay);
            BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId,BLL.Const.ProjectCheckDayXAMenuId,BLL.Const.BtnAdd);
        }

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_CheckDayXA checkDay = new Model.Check_CheckDayXA
            {
                CheckDayCode = this.txtCheckDayCode.Text.Trim(),
                ProjectId = this.ProjectId,
                NotOKNum = Funs.GetNewIntOrZero(this.txtNotOKNum.Text.Trim()),
                CheckDate = Funs.GetNewDateTimeOrNow(this.txtCheckDate.Text.Trim())
            };
            if (this.trUnit.Hidden == false)
            {
                //受检单位
                string unitIds = string.Empty;
                foreach (var item in this.drpDutyUnitIds.SelectedValueArray)
                {
                    unitIds += item + ",";
                }
                if (!string.IsNullOrEmpty(unitIds))
                {
                    unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
                }
                checkDay.DutyUnitIds = unitIds;
            }
            if (this.trTeamGroup.Hidden == false)
            {
                //受检班组
                string teamGroupIds = string.Empty;
                foreach (var item in this.drpDutyTeamGroupIds.SelectedValueArray)
                {
                    teamGroupIds += item + ",";
                }
                if (!string.IsNullOrEmpty(teamGroupIds))
                {
                    teamGroupIds = teamGroupIds.Substring(0, teamGroupIds.LastIndexOf(","));
                }
                checkDay.DutyTeamGroupIds = teamGroupIds;
            }
            //检查区域
            string workAreaIds = string.Empty;
            foreach (var item in this.drpWorkAreaIds.SelectedValueArray)
            {
                workAreaIds += item + ",";
            }
            if (!string.IsNullOrEmpty(workAreaIds))
            {
                workAreaIds = workAreaIds.Substring(0, workAreaIds.LastIndexOf(","));
            }
            checkDay.WorkAreaIds = workAreaIds;
            checkDay.Unqualified = this.txtUnqualified.Text.Trim();
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                checkDay.CompileMan = this.drpCheckMan.SelectedValue;
            }
            else
            {
                checkDay.CompileMan = this.CurrUser.UserId;
            }
            ////单据状态
            //checkDay.States = BLL.Const.State_0;
            //if (type == BLL.Const.BtnSubmit)
            //{
            //    checkDay.States = this.ctlAuditFlow.NextStep;
            //}
            if (!string.IsNullOrEmpty(this.CheckDayId))
            {
                checkDay.CheckDayId = this.CheckDayId;
                BLL.Check_CheckDayXAService.UpdateCheckDay(checkDay);
                BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId, BLL.Const.ProjectCheckDayXAMenuId, BLL.Const.BtnModify);
            }
            else
            {
                checkDay.CheckDayId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayXA));
                this.CheckDayId = checkDay.CheckDayId;
                checkDay.CompileUnit = this.CurrUser.UnitId;
                BLL.Check_CheckDayXAService.AddCheckDay(checkDay);
                BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId, BLL.Const.ProjectCheckDayXAMenuId, BLL.Const.BtnAdd);
            }
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckDayId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDayXA&menuId={1}", this.CheckDayId, BLL.Const.ProjectCheckDayXAMenuId)));
        }
        #endregion
    }
}