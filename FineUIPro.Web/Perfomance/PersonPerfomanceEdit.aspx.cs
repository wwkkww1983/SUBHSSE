using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Perfomance
{
    public partial class PersonPerfomanceEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PersonPerfomanceId
        {
            get
            {
                return (string)ViewState["PersonPerfomanceId"];
            }
            set
            {
                ViewState["PersonPerfomanceId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachUrl
        {
            get
            {
                return (string)ViewState["AttachUrl"];
            }
            set
            {
                ViewState["AttachUrl"] = value;
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
                Funs.FineUIPleaseSelect(this.drpTeamGroupId);
                this.InitDropDownList();
                this.PersonPerfomanceId = Request.Params["PersonPerfomanceId"];
                if (!string.IsNullOrEmpty(this.PersonPerfomanceId))
                {
                    Model.Perfomance_PersonPerfomance personPerfomance = BLL.PersonPerfomanceService.GetPersonPerfomanceById(this.PersonPerfomanceId);
                    if (personPerfomance!=null)
                    {
                        this.ProjectId = personPerfomance.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtPersonPerfomanceCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PersonPerfomanceId);
                        if (!string.IsNullOrEmpty(personPerfomance.UnitId))
                        {
                            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroupId, this.ProjectId, this.drpUnitId.SelectedValue, true);
                            this.drpTeamGroupId.SelectedIndex = 0;
                            if (!string.IsNullOrEmpty(personPerfomance.TeamGroupId))
                            {
                                this.drpTeamGroupId.SelectedValue = personPerfomance.TeamGroupId;
                            }
                            BLL.PersonService.InitPersonByProjectUnitDropDownList(this.drpPersonId, this.ProjectId, this.drpUnitId.SelectedValue, true);
                            this.drpPersonId.SelectedIndex = 0;
                            if (!string.IsNullOrEmpty(personPerfomance.PersonId))
                            {
                                this.drpPersonId.SelectedValue = personPerfomance.PersonId;
                            }
                        }

                        this.txtSubContractNum.Text = personPerfomance.SubContractNum;
                        if (personPerfomance.EvaluationDate!=null)
                        {
                            this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", personPerfomance.EvaluationDate);
                        }
                        this.txtEvaluationDef.Text = personPerfomance.EvaluationDef;
                        if (!string.IsNullOrEmpty(personPerfomance.RewardOrPunish))
                        {
                            this.drpRewardOrPunish.SelectedValue = personPerfomance.RewardOrPunish;
                        }
                        if (personPerfomance.RPMoney!=null)
                        {
                            this.txtRPMoney.Text = Convert.ToString(personPerfomance.RPMoney);
                        }
                        this.txtAssessmentGroup.Text = personPerfomance.AssessmentGroup;
                        this.txtBehavior_1.Text = personPerfomance.Behavior_1;
                        this.txtBehavior_2.Text = personPerfomance.Behavior_2;
                        this.txtBehavior_3.Text = personPerfomance.Behavior_3;
                        this.txtBehavior_4.Text = personPerfomance.Behavior_4;
                        this.txtBehavior_5.Text = personPerfomance.Behavior_5;
                        this.txtBehavior_6.Text = personPerfomance.Behavior_6;
                        this.txtBehavior_7.Text = personPerfomance.Behavior_7;
                        this.txtBehavior_8.Text = personPerfomance.Behavior_8;
                        this.txtBehavior_9.Text = personPerfomance.Behavior_9;
                        this.txtBehavior_10.Text = personPerfomance.Behavior_10;
                        this.txtScore_1.Text = Convert.ToString(personPerfomance.Score_1);
                        this.txtScore_2.Text = Convert.ToString(personPerfomance.Score_2);
                        this.txtScore_3.Text = Convert.ToString(personPerfomance.Score_3);
                        this.txtScore_4.Text = Convert.ToString(personPerfomance.Score_4);
                        this.txtScore_5.Text = Convert.ToString(personPerfomance.Score_5);
                        this.txtScore_6.Text = Convert.ToString(personPerfomance.Score_6);
                        this.txtScore_7.Text = Convert.ToString(personPerfomance.Score_7);
                        this.txtScore_8.Text = Convert.ToString(personPerfomance.Score_8);
                        this.txtScore_9.Text = Convert.ToString(personPerfomance.Score_9);
                        this.txtScore_10.Text = Convert.ToString(personPerfomance.Score_10);
                        this.txtTotalJudging.Text = personPerfomance.TotalJudging;
                        this.txtTotalScore.Text = Convert.ToString(personPerfomance.TotalScore);
                        this.AttachUrl = personPerfomance.AttachUrl;
                        this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
                    }
                }
                else
                {
                    this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtPersonPerfomanceCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.PersonPerfomanceMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.PersonPerfomanceMenuId;
                this.ctlAuditFlow.DataId = this.PersonPerfomanceId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            BLL.ConstValue.InitConstValueDropDownList(this.drpRewardOrPunish, BLL.ConstValue.Group_RewardOrPunish, true);
        }

        #region 保存、提交
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
                Alert.ShowInTop("请选择分包单位", MessageBoxIcon.Warning);
                return;
            }
            if (this.drpPersonId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择人员姓名！", MessageBoxIcon.Warning);
                return;
            }

            Model.Perfomance_PersonPerfomance personPerfomance = new Model.Perfomance_PersonPerfomance
            {
                ProjectId = this.ProjectId,
                PersonPerfomanceCode = this.txtPersonPerfomanceCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                personPerfomance.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpTeamGroupId.SelectedValue != BLL.Const._Null)
            {
                personPerfomance.TeamGroupId = this.drpTeamGroupId.SelectedValue;
            }
            if (this.drpPersonId.SelectedValue != BLL.Const._Null)
            {
                personPerfomance.PersonId = this.drpPersonId.SelectedValue;
            }
            personPerfomance.SubContractNum = this.txtSubContractNum.Text.Trim();
            personPerfomance.EvaluationDate = Funs.GetNewDateTime(this.txtEvaluationDate.Text.Trim());
            personPerfomance.EvaluationDef = this.txtEvaluationDef.Text.Trim();
            if (this.drpRewardOrPunish.SelectedValue != BLL.Const._Null)
            {
                personPerfomance.RewardOrPunish = this.drpRewardOrPunish.SelectedValue;
            }
            personPerfomance.RPMoney = Funs.GetNewDecimalOrZero(this.txtRPMoney.Text.Trim());
            personPerfomance.AssessmentGroup = this.txtAssessmentGroup.Text.Trim();
            personPerfomance.Behavior_1 = this.txtBehavior_1.Text.Trim();
            personPerfomance.Behavior_2 = this.txtBehavior_2.Text.Trim();
            personPerfomance.Behavior_3 = this.txtBehavior_3.Text.Trim();
            personPerfomance.Behavior_4 = this.txtBehavior_4.Text.Trim();
            personPerfomance.Behavior_5 = this.txtBehavior_5.Text.Trim();
            personPerfomance.Behavior_6 = this.txtBehavior_6.Text.Trim();
            personPerfomance.Behavior_7 = this.txtBehavior_7.Text.Trim();
            personPerfomance.Behavior_8 = this.txtBehavior_8.Text.Trim();
            personPerfomance.Behavior_9 = this.txtBehavior_9.Text.Trim();
            personPerfomance.Behavior_10 = this.txtBehavior_10.Text.Trim();
            personPerfomance.Score_1 = Funs.GetNewDecimalOrZero(this.txtScore_1.Text.Trim());
            personPerfomance.Score_2 = Funs.GetNewDecimalOrZero(this.txtScore_2.Text.Trim());
            personPerfomance.Score_3 = Funs.GetNewDecimalOrZero(this.txtScore_3.Text.Trim());
            personPerfomance.Score_4 = Funs.GetNewDecimalOrZero(this.txtScore_4.Text.Trim());
            personPerfomance.Score_5 = Funs.GetNewDecimalOrZero(this.txtScore_5.Text.Trim());
            personPerfomance.Score_6 = Funs.GetNewDecimalOrZero(this.txtScore_6.Text.Trim());
            personPerfomance.Score_7 = Funs.GetNewDecimalOrZero(this.txtScore_7.Text.Trim());
            personPerfomance.Score_8 = Funs.GetNewDecimalOrZero(this.txtScore_8.Text.Trim());
            personPerfomance.Score_9 = Funs.GetNewDecimalOrZero(this.txtScore_9.Text.Trim());
            personPerfomance.Score_10 = Funs.GetNewDecimalOrZero(this.txtScore_10.Text.Trim());
            personPerfomance.TotalJudging = this.txtTotalJudging.Text.Trim();
            personPerfomance.TotalScore = Funs.GetNewDecimalOrZero(this.txtTotalScore.Text.Trim());
            personPerfomance.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                personPerfomance.States = this.ctlAuditFlow.NextStep;
            }
            personPerfomance.CompileMan = this.CurrUser.UserId;
            personPerfomance.CompileDate = DateTime.Now;
            personPerfomance.AttachUrl = this.AttachUrl;
            if (!string.IsNullOrEmpty(this.PersonPerfomanceId))
            {
                personPerfomance.PersonPerfomanceId = this.PersonPerfomanceId;
                BLL.PersonPerfomanceService.UpdatePersonPerfomance(personPerfomance);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改个人绩效评价", personPerfomance.PersonPerfomanceId);
            }
            else
            {
                this.PersonPerfomanceId = SQLHelper.GetNewID(typeof(Model.Perfomance_PersonPerfomance));
                personPerfomance.PersonPerfomanceId = this.PersonPerfomanceId;
                BLL.PersonPerfomanceService.AddPersonPerfomance(personPerfomance);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加个人绩效评价", personPerfomance.PersonPerfomanceId);
                ////保存流程审核数据         
                this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.PersonPerfomanceMenuId, this.PersonPerfomanceId, (type == BLL.Const.BtnSubmit ? true : false), personPerfomance.PersonPerfomanceCode, "../Perfomance/PersonPerfomanceView.aspx?PersonPerfomanceId={0}");
            }
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        /// 分包单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroupId, this.ProjectId, this.drpUnitId.SelectedValue, true);
            this.drpTeamGroupId.SelectedIndex = 0;
            BLL.PersonService.InitPersonByProjectUnitDropDownList(this.drpPersonId, this.ProjectId, this.drpUnitId.SelectedValue, true);
            this.drpPersonId.SelectedIndex = 0;
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFile1_Click(object sender, EventArgs e)
        {
            if (btnFile1.HasFile)
            {
                this.AttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.btnFile1, this.AttachUrl, UploadFileService.PersonPerfomanceFilePath);
                this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl);
            }
        }
        #endregion

        #region 计算总分数
        /// <summary>
        /// 计算总分数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_OnTextChanged(object sender, EventArgs e)
        {
            decimal score_1 = 0, score_2 = 0, score_3 = 0, score_4 = 0, score_5 = 0, score_6 = 0, score_7 = 0, score_8 = 0, score_9 = 0, score_10 = 0;
            if (!string.IsNullOrEmpty(this.txtScore_1.Text.Trim()))
            {
                score_1 = Convert.ToDecimal(this.txtScore_1.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_2.Text.Trim()))
            {
                score_2 = Convert.ToDecimal(this.txtScore_2.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_3.Text.Trim()))
            {
                score_3 = Convert.ToDecimal(this.txtScore_3.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_4.Text.Trim()))
            {
                score_4 = Convert.ToDecimal(this.txtScore_4.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_5.Text.Trim()))
            {
                score_5 = Convert.ToDecimal(this.txtScore_5.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_6.Text.Trim()))
            {
                score_6 = Convert.ToDecimal(this.txtScore_6.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_7.Text.Trim()))
            {
                score_7 = Convert.ToDecimal(this.txtScore_7.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_8.Text.Trim()))
            {
                score_8 = Convert.ToDecimal(this.txtScore_8.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_9.Text.Trim()))
            {
                score_9 = Convert.ToDecimal(this.txtScore_9.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtScore_10.Text.Trim()))
            {
                score_10 = Convert.ToDecimal(this.txtScore_10.Text.Trim());
            }
            this.txtTotalScore.Text = Convert.ToString(score_1 + score_2 + score_3 + score_4 + score_5 + score_6 + score_7 + score_8 + score_9 + score_10);
        }
        #endregion
    }
}