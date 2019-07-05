using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Perfomance
{
    public partial class PerfomanceRecordView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string PerfomanceRecordId
        {
            get
            {
                return (string)ViewState["PerfomanceRecordId"];
            }
            set
            {
                ViewState["PerfomanceRecordId"] = value;
            }
        }

        /// <summary>
        /// 附件路径1
        /// </summary>
        public string AttachUrl1
        {
            get
            {
                return (string)ViewState["AttachUrl1"];
            }
            set
            {
                ViewState["AttachUrl1"] = value;
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

                this.PerfomanceRecordId = Request.Params["PerfomanceRecordId"];
                if (!string.IsNullOrEmpty(this.PerfomanceRecordId))
                {
                    Model.Perfomance_PerfomanceRecord perfomanceRecord = BLL.PerfomanceRecordService.GetPerfomanceRecordById(this.PerfomanceRecordId);
                    if (perfomanceRecord != null)
                    {
                        this.txtPerfomanceRecordCode.Text = CodeRecordsService.ReturnCodeByDataId(this.PerfomanceRecordId);
                        if (!string.IsNullOrEmpty(perfomanceRecord.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(perfomanceRecord.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        this.txtSubContractNum.Text = perfomanceRecord.SubContractNum;
                        if (perfomanceRecord.EvaluationDate != null)
                        {
                            this.txtEvaluationDate.Text = string.Format("{0:yyyy-MM-dd}", perfomanceRecord.EvaluationDate);
                        }
                        this.txtEvaluationDef.Text = perfomanceRecord.EvaluationDef;
                        if (!string.IsNullOrEmpty(perfomanceRecord.RewardOrPunish))
                        {
                            if (perfomanceRecord.RewardOrPunish == "1")
                            {
                                this.txtRewardOrPunish.Text = "奖励";
                            }
                            else if (perfomanceRecord.RewardOrPunish == "2")
                            {
                                this.txtRewardOrPunish.Text = "处罚";
                            }
                        }
                        if (perfomanceRecord.RPMoney != null)
                        {
                            this.txtRPMoney.Text = Convert.ToString(perfomanceRecord.RPMoney);
                        }
                        this.txtAssessmentGroup.Text = perfomanceRecord.AssessmentGroup;
                        this.txtBehavior_1.Text = perfomanceRecord.Behavior_1;
                        this.txtBehavior_2.Text = perfomanceRecord.Behavior_2;
                        this.txtBehavior_3.Text = perfomanceRecord.Behavior_3;
                        this.txtBehavior_4.Text = perfomanceRecord.Behavior_4;
                        this.txtBehavior_5.Text = perfomanceRecord.Behavior_5;
                        this.txtBehavior_6.Text = perfomanceRecord.Behavior_6;
                        this.txtBehavior_7.Text = perfomanceRecord.Behavior_7;
                        this.txtBehavior_8.Text = perfomanceRecord.Behavior_8;
                        this.txtBehavior_9.Text = perfomanceRecord.Behavior_9;
                        this.txtBehavior_10.Text = perfomanceRecord.Behavior_10;
                        this.txtBehavior_11.Text = perfomanceRecord.Behavior_11;
                        this.txtBehavior_12.Text = perfomanceRecord.Behavior_12;
                        this.txtBehavior_13.Text = perfomanceRecord.Behavior_13;
                        this.txtBehavior_14.Text = perfomanceRecord.Behavior_14;
                        this.txtBehavior_15.Text = perfomanceRecord.Behavior_15;
                        this.txtBehavior_16.Text = perfomanceRecord.Behavior_16;
                        this.txtBehavior_17.Text = perfomanceRecord.Behavior_17;
                        this.txtBehavior_18.Text = perfomanceRecord.Behavior_18;
                        this.txtBehavior_19.Text = perfomanceRecord.Behavior_19;
                        this.txtBehavior_20.Text = perfomanceRecord.Behavior_20;
                        this.txtScore_1.Text = Convert.ToString(perfomanceRecord.Score_1);
                        this.txtScore_2.Text = Convert.ToString(perfomanceRecord.Score_2);
                        this.txtScore_3.Text = Convert.ToString(perfomanceRecord.Score_3);
                        this.txtScore_4.Text = Convert.ToString(perfomanceRecord.Score_4);
                        this.txtScore_5.Text = Convert.ToString(perfomanceRecord.Score_5);
                        this.txtScore_6.Text = Convert.ToString(perfomanceRecord.Score_6);
                        this.txtScore_7.Text = Convert.ToString(perfomanceRecord.Score_7);
                        this.txtScore_8.Text = Convert.ToString(perfomanceRecord.Score_8);
                        this.txtScore_9.Text = Convert.ToString(perfomanceRecord.Score_9);
                        this.txtScore_10.Text = Convert.ToString(perfomanceRecord.Score_10);
                        this.txtScore_11.Text = Convert.ToString(perfomanceRecord.Score_11);
                        this.txtScore_12.Text = Convert.ToString(perfomanceRecord.Score_12);
                        this.txtScore_13.Text = Convert.ToString(perfomanceRecord.Score_13);
                        this.txtScore_14.Text = Convert.ToString(perfomanceRecord.Score_14);
                        this.txtScore_15.Text = Convert.ToString(perfomanceRecord.Score_15);
                        this.txtScore_16.Text = Convert.ToString(perfomanceRecord.Score_16);
                        this.txtScore_17.Text = Convert.ToString(perfomanceRecord.Score_17);
                        this.txtScore_18.Text = Convert.ToString(perfomanceRecord.Score_18);
                        this.txtScore_19.Text = Convert.ToString(perfomanceRecord.Score_19);
                        this.txtScore_20.Text = Convert.ToString(perfomanceRecord.Score_20);
                        this.txtTotalJudging.Text = perfomanceRecord.TotalJudging;
                        this.txtTotalScore.Text = Convert.ToString(perfomanceRecord.TotalScore);
                        this.AttachUrl1 = perfomanceRecord.AttachUrl;
                        this.divFile1.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", this.AttachUrl1);
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.PerfomanceRecordMenuId;
                this.ctlAuditFlow.DataId = this.PerfomanceRecordId;
            }
        }
        #endregion
    }
}