using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Perfomance
{
    public partial class PersonPerfomanceView : PageBase
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
                this.PersonPerfomanceId = Request.Params["PersonPerfomanceId"];
                if (!string.IsNullOrEmpty(this.PersonPerfomanceId))
                {
                    Model.Perfomance_PersonPerfomance personPerfomance = BLL.PersonPerfomanceService.GetPersonPerfomanceById(this.PersonPerfomanceId);
                    if (personPerfomance != null)
                    {
                        this.txtPersonPerfomanceCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PersonPerfomanceId);
                        if (!string.IsNullOrEmpty(personPerfomance.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(personPerfomance.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (!string.IsNullOrEmpty(personPerfomance.TeamGroupId))
                        {
                            var teamGroup = BLL.TeamGroupService.GetTeamGroupById(personPerfomance.TeamGroupId);
                            if (teamGroup != null)
                            {
                                this.txtTeamGroupName.Text = teamGroup.TeamGroupName;
                            }
                        }
                        if (!string.IsNullOrEmpty(personPerfomance.PersonId))
                        {
                            var person = BLL.PersonService.GetPersonById(personPerfomance.PersonId);
                            if (person != null)
                            {
                                this.txtPersonName.Text = person.PersonName;
                            }
                        }
                        this.txtSubContractNum.Text = personPerfomance.SubContractNum;
                        if (personPerfomance.EvaluationDate != null)
                        {
                            this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", personPerfomance.EvaluationDate);
                        }
                        this.txtEvaluationDef.Text = personPerfomance.EvaluationDef;
                        if (!string.IsNullOrEmpty(personPerfomance.RewardOrPunish))
                        {
                            if (personPerfomance.RewardOrPunish == "1")
                            {
                                this.txtRewardOrPunish.Text = "奖励";
                            }
                            else if (personPerfomance.RewardOrPunish == "2")
                            {
                                this.txtRewardOrPunish.Text = "处罚";
                            }
                        }
                        if (personPerfomance.RPMoney != null)
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
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.PersonPerfomanceMenuId;
                this.ctlAuditFlow.DataId = this.PersonPerfomanceId;
            }
        }
        #endregion
    }
}