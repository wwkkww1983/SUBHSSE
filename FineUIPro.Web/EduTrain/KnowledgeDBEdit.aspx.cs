using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class KnowledgeDBEdit : PageBase
    {
        /// <summary>
        /// 应知应会库主键
        /// </summary>
        public string KnowledgeId
        {
            get
            {
                return (string)ViewState["KnowledgeId"];
            }
            set
            {
                ViewState["KnowledgeId"] = value;
            }
        }

        /// <summary>
        /// 上一节点id
        /// </summary>
        public string SupKnowledgeId
        {
            get
            {
                return (string)ViewState["SupKnowledgeId"];
            }
            set
            {
                ViewState["SupKnowledgeId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetButtonPower();
                LoadData();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.drpIsEndLever.DataTextField = "TypeName";
                drpIsEndLever.DataValueField = "TypeId";
                drpIsEndLever.DataSource = BLL.TrainingService.GetIsEndLeverList();
                drpIsEndLever.DataBind();

                this.KnowledgeId = Request.Params["KnowledgeId"];
                this.SupKnowledgeId = Request.Params["SupKnowledgeId"];
                if (!string.IsNullOrEmpty(this.KnowledgeId))
                {
                    Model.Training_Knowledge knowledge = BLL.KnowledgeService.GetKnowLedgeById(this.KnowledgeId);
                    if (knowledge != null)
                    {
                        this.txtKnowledgeCode.Text = knowledge.KnowledgeCode;
                        this.txtKnowledgeName.Text = knowledge.KnowledgeName;
                        if (knowledge.IsEndLever == true)
                        {
                            this.drpIsEndLever.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpIsEndLever.SelectedValue = "false";
                        }
                    }
                }
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Training_Knowledge knowledge = new Model.Training_Knowledge
            {
                KnowledgeCode = this.txtKnowledgeCode.Text.Trim(),
                KnowledgeName = this.txtKnowledgeName.Text.Trim()
            };
            if (this.drpIsEndLever.SelectedValue == "true")
            {
                knowledge.IsEndLever = true;
            }
            else
            {
                knowledge.IsEndLever = false;
            }
            if (string.IsNullOrEmpty(this.KnowledgeId))
            {
                knowledge.SupKnowledgeId = this.SupKnowledgeId;
                knowledge.KnowledgeId = SQLHelper.GetNewID(typeof(Model.Training_Knowledge));
                BLL.KnowledgeService.AddKnowledge(knowledge);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加应知应会库类型");
            }
            else
            {
                Model.Training_Knowledge k = BLL.KnowledgeService.GetKnowLedgeById(this.KnowledgeId);
                if (k != null)
                {
                    knowledge.SupKnowledgeId = k.SupKnowledgeId;
                }
                knowledge.KnowledgeId = this.KnowledgeId;
                BLL.KnowledgeService.UpdateKnowledge(knowledge);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改应知应会库类型");
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.KnowledgeDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证应知应会名称是否存在
        /// <summary>
        /// 验证应知应会名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_Knowledge.FirstOrDefault(x => x.SupKnowledgeId == this.SupKnowledgeId && x.KnowledgeName == this.txtKnowledgeName.Text.Trim() && (x.KnowledgeId != this.KnowledgeId || (this.KnowledgeId == null && x.KnowledgeId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}