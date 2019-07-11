using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class SubUploadWeekEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string SubUploadWeekId
        {
            get
            {
                return (string)ViewState["SubUploadWeekId"];
            }
            set
            {
                ViewState["SubUploadWeekId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SubUploadWeekId = Request.Params["SubUploadWeekId"];
                Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.lbProjectName.Text = project.ProjectName;
                }
                if (!string.IsNullOrEmpty(this.SubUploadWeekId))
                {
                    Model.Manager_SubUploadWeek subUploadWeek = BLL.SubUploadWeekService.GetSubUploadWeekById(this.SubUploadWeekId);
                    if (subUploadWeek != null)
                    {
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", subUploadWeek.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", subUploadWeek.EndDate);
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(subUploadWeek.UnitId);
                        if (unit != null)
                        {
                            this.lbUnitName.Text = unit.UnitName;
                        }
                        if (subUploadWeek.StartWorkDate!=null)
                        {
                            this.txtStartWorkDate.Text = string.Format("{0:yyyy-MM-dd}", subUploadWeek.StartWorkDate);
                        }
                        if (subUploadWeek.EndWorkDate != null)
                        {
                            this.txtEndWorkDate.Text = string.Format("{0:yyyy-MM-dd}", subUploadWeek.EndWorkDate);
                        }
                        this.nbPersonWeekNum.Text = subUploadWeek.PersonWeekNum.ToString();
                        this.nbManHours.Text = subUploadWeek.ManHours.ToString();
                        this.nbTotalManHours.Text = subUploadWeek.TotalManHours.ToString();
                        this.txtRemark.Text = subUploadWeek.Remark;
                        this.nbHappenNum1.Text = subUploadWeek.HappenNum1.ToString();
                        this.nbHappenNum2.Text = subUploadWeek.HappenNum2.ToString();
                        this.nbHappenNum3.Text = subUploadWeek.HappenNum3.ToString();
                        this.nbHappenNum4.Text = subUploadWeek.HappenNum4.ToString();
                        this.nbHappenNum5.Text = subUploadWeek.HappenNum5.ToString();
                        this.nbHappenNum6.Text = subUploadWeek.HappenNum6.ToString();
                        this.nbHurtPersonNum1.Text = subUploadWeek.HurtPersonNum1.ToString();
                        this.nbHurtPersonNum2.Text = subUploadWeek.HurtPersonNum2.ToString();
                        this.nbHurtPersonNum3.Text = subUploadWeek.HurtPersonNum3.ToString();
                        this.nbHurtPersonNum4.Text = subUploadWeek.HurtPersonNum4.ToString();
                        this.nbHurtPersonNum5.Text = subUploadWeek.HurtPersonNum5.ToString();
                        this.nbHurtPersonNum6.Text = subUploadWeek.HurtPersonNum6.ToString();
                        this.nbLossHours1.Text = subUploadWeek.LossHours1.ToString();
                        this.nbLossHours2.Text = subUploadWeek.LossHours2.ToString();
                        this.nbLossHours3.Text = subUploadWeek.LossHours3.ToString();
                        this.nbLossHours4.Text = subUploadWeek.LossHours4.ToString();
                        this.nbLossHours5.Text = subUploadWeek.LossHours5.ToString();
                        this.nbLossHours6.Text = subUploadWeek.LossHours6.ToString();
                        this.nbLossMoney1.Text = subUploadWeek.LossMoney1.ToString();
                        this.nbLossMoney2.Text = subUploadWeek.LossMoney2.ToString();
                        this.nbLossMoney3.Text = subUploadWeek.LossMoney3.ToString();
                        this.nbLossMoney4.Text = subUploadWeek.LossMoney4.ToString();
                        this.nbLossMoney5.Text = subUploadWeek.LossMoney5.ToString();
                        this.nbLossMoney6.Text = subUploadWeek.LossMoney6.ToString();
                        this.nbHSEPersonNum.Text = subUploadWeek.HSEPersonNum.ToString();
                        this.nbCheckNum.Text = subUploadWeek.CheckNum.ToString();
                        this.nbEmergencyDrillNum.Text = subUploadWeek.EmergencyDrillNum.ToString();
                        this.nbLicenseNum.Text = subUploadWeek.LicenseNum.ToString();
                        this.nbTrainNum.Text = subUploadWeek.TrainNum.ToString();
                        this.nbHazardNum.Text = subUploadWeek.HazardNum.ToString();
                        this.nbMeetingNum.Text = subUploadWeek.MeetingNum.ToString();
                        this.nbHiddenDanger1.Text = subUploadWeek.HiddenDanger1.ToString();
                        this.nbHiddenDanger2.Text = subUploadWeek.HiddenDanger2.ToString();
                        this.nbHiddenDanger3.Text = subUploadWeek.HiddenDanger3.ToString();
                        this.nbRectifyNum1.Text = subUploadWeek.RectifyNum1.ToString();
                        this.nbRectifyNum2.Text = subUploadWeek.RectifyNum2.ToString();
                        this.nbRectifyNum3.Text = subUploadWeek.RectifyNum3.ToString();
                        this.txtThisWorkSummary.Text = subUploadWeek.ThisWorkSummary;
                        this.txtNextWorkPlan.Text = subUploadWeek.NextWorkPlan;
                        this.txtOtherProblems.Text = subUploadWeek.OtherProblems;
                        GetTotal();
                    }
                }
                else
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                    if (unit != null)
                    {
                        this.lbUnitName.Text = unit.UnitName;
                    }
                    this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-6));
                    this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    if (project.StartDate != null)
                    {
                        this.txtStartWorkDate.Text = string.Format("{0:yyyy-MM-dd}", project.StartDate);
                    }
                    if (project.EndDate != null)
                    {
                        this.txtEndWorkDate.Text = string.Format("{0:yyyy-MM-dd}", project.EndDate);
                    }
                }
                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
                }
            }
        }

        protected void Text_TextChanged(object sender, EventArgs e)
        {
            GetTotal();
        }

        private void GetTotal()
        {
            this.nbHappenNumTotal.Text = (Funs.GetNewIntOrZero(this.nbHappenNum1.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHappenNum2.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHappenNum3.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHappenNum4.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHappenNum5.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHappenNum6.Text.Trim())).ToString();
            this.nbHurtPersonNumTotal.Text = (Funs.GetNewIntOrZero(this.nbHurtPersonNum1.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHurtPersonNum2.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHurtPersonNum3.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHurtPersonNum4.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHurtPersonNum5.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHurtPersonNum6.Text.Trim())).ToString();
            this.nbLossHoursTotal.Text = (Funs.GetNewDecimalOrZero(this.nbLossHours1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossHours2.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossHours3.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossHours4.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossHours5.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossHours6.Text.Trim())).ToString();
            this.nbLossMoneyTotal.Text = (Funs.GetNewDecimalOrZero(this.nbLossMoney1.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossMoney2.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossMoney3.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossMoney4.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossMoney5.Text.Trim()) + Funs.GetNewDecimalOrZero(this.nbLossMoney6.Text.Trim())).ToString();

            this.nbHiddenDangerTotal.Text = (Funs.GetNewIntOrZero(this.nbHiddenDanger1.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHiddenDanger2.Text.Trim()) + Funs.GetNewIntOrZero(this.nbHiddenDanger3.Text.Trim())).ToString();
            this.nbRectifyNumTotal.Text = (Funs.GetNewIntOrZero(this.nbRectifyNum1.Text.Trim()) + Funs.GetNewIntOrZero(this.nbRectifyNum2.Text.Trim()) + Funs.GetNewIntOrZero(this.nbRectifyNum3.Text.Trim())).ToString();
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStartDate.Text.Trim()) || string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                Alert.ShowInTop("报告日期不能为空！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///    保存方法
        /// </summary>
        private void SaveData()
        {
            Model.Manager_SubUploadWeek subUploadWeek = new Model.Manager_SubUploadWeek
            {
                ProjectId = this.CurrUser.LoginProjectId,
                StartDate = Funs.GetNewDateTime(this.txtStartDate.Text.Trim()),
                EndDate = Funs.GetNewDateTime(this.txtEndDate.Text.Trim()),
                PersonWeekNum = Funs.GetNewIntOrZero(this.nbPersonWeekNum.Text.Trim()),
                ManHours = Funs.GetNewDecimalOrZero(this.nbManHours.Text.Trim()),
                TotalManHours = Funs.GetNewDecimalOrZero(this.nbTotalManHours.Text.Trim()),
                StartWorkDate = Funs.GetNewDateTime(this.txtStartWorkDate.Text.Trim()),
                EndWorkDate = Funs.GetNewDateTime(this.txtEndWorkDate.Text.Trim()),
                Remark = this.txtRemark.Text.Trim(),
                HappenNum1 = Funs.GetNewIntOrZero(this.nbHappenNum1.Text.Trim()),
                HappenNum2 = Funs.GetNewIntOrZero(this.nbHappenNum2.Text.Trim()),
                HappenNum3 = Funs.GetNewIntOrZero(this.nbHappenNum3.Text.Trim()),
                HappenNum4 = Funs.GetNewIntOrZero(this.nbHappenNum4.Text.Trim()),
                HappenNum5 = Funs.GetNewIntOrZero(this.nbHappenNum5.Text.Trim()),
                HappenNum6 = Funs.GetNewIntOrZero(this.nbHappenNum6.Text.Trim()),
                HurtPersonNum1 = Funs.GetNewIntOrZero(this.nbHurtPersonNum1.Text.Trim()),
                HurtPersonNum2 = Funs.GetNewIntOrZero(this.nbHurtPersonNum2.Text.Trim()),
                HurtPersonNum3 = Funs.GetNewIntOrZero(this.nbHurtPersonNum3.Text.Trim()),
                HurtPersonNum4 = Funs.GetNewIntOrZero(this.nbHurtPersonNum4.Text.Trim()),
                HurtPersonNum5 = Funs.GetNewIntOrZero(this.nbHurtPersonNum5.Text.Trim()),
                HurtPersonNum6 = Funs.GetNewIntOrZero(this.nbHurtPersonNum6.Text.Trim()),
                LossHours1 = Funs.GetNewDecimalOrZero(this.nbLossHours1.Text.Trim()),
                LossHours2 = Funs.GetNewDecimalOrZero(this.nbLossHours2.Text.Trim()),
                LossHours3 = Funs.GetNewDecimalOrZero(this.nbLossHours3.Text.Trim()),
                LossHours4 = Funs.GetNewDecimalOrZero(this.nbLossHours4.Text.Trim()),
                LossHours5 = Funs.GetNewDecimalOrZero(this.nbLossHours5.Text.Trim()),
                LossHours6 = Funs.GetNewDecimalOrZero(this.nbLossHours6.Text.Trim()),
                LossMoney1 = Funs.GetNewDecimalOrZero(this.nbLossMoney1.Text.Trim()),
                LossMoney2 = Funs.GetNewDecimalOrZero(this.nbLossMoney2.Text.Trim()),
                LossMoney3 = Funs.GetNewDecimalOrZero(this.nbLossMoney3.Text.Trim()),
                LossMoney4 = Funs.GetNewDecimalOrZero(this.nbLossMoney4.Text.Trim()),
                LossMoney5 = Funs.GetNewDecimalOrZero(this.nbLossMoney5.Text.Trim()),
                LossMoney6 = Funs.GetNewDecimalOrZero(this.nbLossMoney6.Text.Trim()),
                HSEPersonNum = Funs.GetNewIntOrZero(this.nbHSEPersonNum.Text.Trim()),
                CheckNum = Funs.GetNewIntOrZero(this.nbCheckNum.Text.Trim()),
                EmergencyDrillNum = Funs.GetNewIntOrZero(this.nbEmergencyDrillNum.Text.Trim()),
                LicenseNum = Funs.GetNewIntOrZero(this.nbLicenseNum.Text.Trim()),
                TrainNum = Funs.GetNewIntOrZero(this.nbTrainNum.Text.Trim()),
                HazardNum = Funs.GetNewIntOrZero(this.nbHazardNum.Text.Trim()),
                MeetingNum = Funs.GetNewIntOrZero(this.nbMeetingNum.Text.Trim()),
                HiddenDanger1 = Funs.GetNewIntOrZero(this.nbHiddenDanger1.Text.Trim()),
                HiddenDanger2 = Funs.GetNewIntOrZero(this.nbHiddenDanger2.Text.Trim()),
                HiddenDanger3 = Funs.GetNewIntOrZero(this.nbHiddenDanger3.Text.Trim()),
                RectifyNum1 = Funs.GetNewIntOrZero(this.nbRectifyNum1.Text.Trim()),
                RectifyNum2 = Funs.GetNewIntOrZero(this.nbRectifyNum2.Text.Trim()),
                RectifyNum3 = Funs.GetNewIntOrZero(this.nbRectifyNum3.Text.Trim()),
                ThisWorkSummary = this.txtThisWorkSummary.Text.Trim(),
                NextWorkPlan = this.txtNextWorkPlan.Text.Trim(),
                OtherProblems = this.txtOtherProblems.Text.Trim(),

            };
            if (!string.IsNullOrEmpty(this.SubUploadWeekId))
            {
                subUploadWeek.SubUploadWeekId = this.SubUploadWeekId;
                BLL.SubUploadWeekService.UpdateSubUploadWeek(subUploadWeek);
                BLL.LogService.AddSys_Log(this.CurrUser, null, subUploadWeek.SubUploadWeekId, BLL.Const.ProjectSubUploadWeekMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.SubUploadWeekId = SQLHelper.GetNewID(typeof(Model.Manager_SubUploadWeek));
                subUploadWeek.SubUploadWeekId = this.SubUploadWeekId;
                subUploadWeek.UnitId = this.CurrUser.UnitId;
                subUploadWeek.CompileMan = this.CurrUser.UserId;
                subUploadWeek.CompileDate = DateTime.Now;
                BLL.SubUploadWeekService.AddSubUploadWeek(subUploadWeek);
                BLL.LogService.AddSys_Log(this.CurrUser, null, subUploadWeek.SubUploadWeekId, BLL.Const.ProjectSubUploadWeekMenuId, BLL.Const.BtnAdd);
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubUploadWeekAttachUrl&type=-1", this.SubUploadWeekId, BLL.Const.ProjectSubUploadWeekMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.SubUploadWeekId))
                {
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SubUploadWeekAttachUrl&menuId={1}", this.SubUploadWeekId, BLL.Const.ProjectSubUploadWeekMenuId)));
            }
        }
        #endregion
    }
}