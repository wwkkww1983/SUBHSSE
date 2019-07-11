using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.EduTrain
{
    public partial class TestRecordView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 考试记录ID
        /// </summary>
        private string TestRecordItemId
        {
            get
            {
                return (string)ViewState["TestRecordItemId"];
            }
            set
            {
                ViewState["TestRecordItemId"] = value;
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
                this.TestRecordItemId = Request.Params["TestRecordItemId"];
                if (!string.IsNullOrEmpty(this.TestRecordItemId))
                {
                    var testRecordItem = BLL.TestRecordItemService.GetTestRecordItemTestRecordItemId(this.TestRecordItemId);
                    if (testRecordItem != null)
                    {
                        this.txtAbstracts.Text = testRecordItem.Abstracts;
                        if (!string.IsNullOrEmpty(testRecordItem.TestType))
                        {
                            if (testRecordItem.TestType == "1")
                            {
                                this.txtTestType.Text = "单选题";
                                this.txtEItem.Hidden = true;
                            }
                            else if (testRecordItem.TestType == "2")
                            {
                                this.txtTestType.Text = "多选题";
                            }
                            else if (testRecordItem.TestType == "3")
                            {
                                this.txtTestType.Text = "判断题";
                                this.txtCItem.Hidden = true;
                                this.txtDItem.Hidden = true;
                                this.txtEItem.Hidden = true;
                            }
                        }
                        this.txtAItem.Text = testRecordItem.AItem;
                        this.txtBItem.Text = testRecordItem.BItem;
                        this.txtCItem.Text = testRecordItem.CItem;
                        this.txtDItem.Text = testRecordItem.DItem;
                        this.txtEItem.Text = testRecordItem.EItem;

                        if (!string.IsNullOrEmpty(testRecordItem.AnswerItems))
                        {
                            this.txtAnswerItems.Text = testRecordItem.AnswerItems.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E");
                        }
                        if (testRecordItem.Score != null)
                        {
                            this.txtScore.Text = testRecordItem.Score.ToString();
                        }
                        if (testRecordItem.SubjectScore != null)
                        {
                            this.txtSubjectScore.Text = testRecordItem.SubjectScore.ToString();
                        }
                        if (!string.IsNullOrEmpty(testRecordItem.SelectedItem))
                        {
                            this.txtSelectedItem.Text = testRecordItem.SelectedItem.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E");
                        }
                        if (!string.IsNullOrEmpty(testRecordItem.AttachUrl))
                        {
                            this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", testRecordItem.AttachUrl);
                        }

                        var testRecord = BLL.TestRecordService.GetTestRecordById(testRecordItem.TestRecordId);
                        if (testRecord != null)
                        {
                            if (!string.IsNullOrEmpty(testRecord.TestPlanId))
                            {
                                Model.Training_TestPlan testPlan = BLL.TestPlanService.GetTestPlanById(testRecord.TestPlanId);
                                if (testPlan != null)
                                {
                                    this.txtPlanName.Text = testPlan.PlanName;
                                }
                            }
                            if (!string.IsNullOrEmpty(testRecord.TestManId))
                            {
                                Model.Sys_User user = BLL.UserService.GetUserByUserId(testRecord.TestManId);
                                if (user != null)
                                {
                                    this.txtTestManName.Text = user.UserName;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}