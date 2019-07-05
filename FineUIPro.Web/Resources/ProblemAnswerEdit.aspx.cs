using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Resources
{
    public partial class ProblemAnswerEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 资料主键
        /// </summary>
        public string ProblemAnswerId
        {
            get
            {
                return (string)ViewState["ProblemAnswerId"];
            }
            set
            {
                ViewState["ProblemAnswerId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 资料编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProblemAnswerId = Request.Params["ProblemAnswerId"];
                if (!string.IsNullOrEmpty(this.ProblemAnswerId))
                {
                    var ProblemAnswer = BLL.ProblemAnswerService.GetProblemAnswerByProblemAnswerId(this.ProblemAnswerId);
                    if (ProblemAnswer != null)
                    {
                        this.txtProblem.Text = ProblemAnswer.Problem;
                        this.txtAnswer.Text = ProblemAnswer.Answer;

                        this.txtProblemContent.Text = HttpUtility.HtmlDecode(ProblemAnswer.ProblemContent);
                        this.txtAnswerContent.Text = HttpUtility.HtmlDecode(ProblemAnswer.AnswerContent);
                    }
                }
            }
        }


        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(true);
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="isClose"></param>
        private void SaveData(bool isClose)
        {
            Model.Resources_ProblemAnswer newProblemAnswer = new Model.Resources_ProblemAnswer
            {
                Problem = this.txtProblem.Text.Trim(),
                Answer = this.txtAnswer.Text.Trim(),
                ProblemContent = HttpUtility.HtmlEncode(this.txtProblemContent.Text.Trim()),
                AnswerContent = HttpUtility.HtmlEncode(this.txtAnswerContent.Text.Trim())
            };
            if (string.IsNullOrEmpty(this.ProblemAnswerId))
            {
                this.ProblemAnswerId = SQLHelper.GetNewID(typeof(Model.Resources_ProblemAnswer));
                newProblemAnswer.ProblemAnswerId = this.ProblemAnswerId; 
                BLL.ProblemAnswerService.AddProblemAnswer(newProblemAnswer);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加资料信息", newProblemAnswer.ProblemAnswerId);
            }
            else
            {
                newProblemAnswer.ProblemAnswerId = this.ProblemAnswerId;
                BLL.ProblemAnswerService.UpdateProblemAnswer(newProblemAnswer);
                BLL.LogService.AddLogDataId(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改资料信息", newProblemAnswer.ProblemAnswerId);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            if (string.IsNullOrEmpty(this.ProblemAnswerId))
            {
                this.SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProblemAnswerAttachUrl&menuId={1}", this.ProblemAnswerId, BLL.Const.ProblemAnswerMenuId)));
        }
        #endregion
    }
}