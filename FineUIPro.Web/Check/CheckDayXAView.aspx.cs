using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayXAView : PageBase
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
                this.CheckDayId = Request.Params["CheckDayId"];
                var checkDay = BLL.Check_CheckDayXAService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.ProjectId = checkDay.ProjectId;
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckDayId);
                    if (checkDay.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkDay.CheckDate);
                    }
                    Model.Sys_User checkMan = BLL.UserService.GetUserByUserId(checkDay.CompileMan);
                    if (checkMan != null)
                    {
                        this.txtCheckMan.Text = checkMan.UserName;
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
                            string names = string.Empty;
                            string[] ids = checkDay.DutyTeamGroupIds.Split(',');
                            foreach (var item in ids)
                            {
                                Model.ProjectData_TeamGroup teamGroup = BLL.TeamGroupService.GetTeamGroupById(item);
                                if (teamGroup != null)
                                {
                                    names += teamGroup.TeamGroupName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(names))
                            {
                                names = names.Substring(0, names.LastIndexOf(","));
                            }
                            this.drpDutyTeamGroupIds.Text = names;
                        }
                    }
                    else
                    {
                        this.trUnit.Hidden = false;
                        if (!string.IsNullOrEmpty(checkDay.DutyUnitIds))
                        {
                            string names = string.Empty;
                            string[] ids = checkDay.DutyUnitIds.Split(',');
                            foreach (var item in ids)
                            {
                                Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                                if (unit != null)
                                {
                                    names += unit.UnitName + ",";
                                }
                            }
                            if (!string.IsNullOrEmpty(names))
                            {
                                names = names.Substring(0, names.LastIndexOf(","));
                            }
                            this.drpDutyUnitIds.Text = names;
                        }
                    }
                    if (!string.IsNullOrEmpty(checkDay.WorkAreaIds))
                    {
                        string names = string.Empty;
                        string[] ids = checkDay.WorkAreaIds.Split(',');
                        foreach (var item in ids)
                        {
                            Model.ProjectData_WorkArea workArea = BLL.WorkAreaService.GetWorkAreaByWorkAreaId(item);
                            if (workArea != null)
                            {
                                names += workArea.WorkAreaName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(names))
                        {
                            names = names.Substring(0, names.LastIndexOf(","));
                        }
                        this.drpWorkAreaIds.Text = names;
                    }
                    this.txtUnqualified.Text = checkDay.Unqualified;
                    this.txtHandleStation.Text = checkDay.HandleStation;
                    if (checkDay.IsOK == true)
                    {
                        this.txtIsOK.Text = "是";
                    }
                    else
                    {
                        this.txtIsOK.Text = "否";
                    }
                }
            }
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDayXA&menuId={1}&type=-1", this.CheckDayId, BLL.Const.ProjectCheckDayXAMenuId)));
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHandleAttachUrl_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDayXA&menuId={1}&type=-1", this.CheckDayId + "1", BLL.Const.ProjectCheckDayXAMenuId)));
        }
        #endregion
    }
}