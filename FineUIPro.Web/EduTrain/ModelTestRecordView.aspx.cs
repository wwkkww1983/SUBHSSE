using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.EduTrain
{
    public partial class ModelTestRecordView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 模拟考试ID
        /// </summary>
        private string ModelTestRecordItemId
        {
            get
            {
                return (string)ViewState["ModelTestRecordItemId"];
            }
            set
            {
                ViewState["ModelTestRecordItemId"] = value;
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
                this.ModelTestRecordItemId = Request.Params["ModelTestRecordItemId"];
                if (!string.IsNullOrEmpty(this.ModelTestRecordItemId))
                {
                    var ModelTestRecordItem = BLL.ModelTestRecordItemService.GetModelTestRecordItemModelTestRecordItemId(this.ModelTestRecordItemId);
                    if (ModelTestRecordItem != null)
                    {
                        this.txtAbstracts.Text = ModelTestRecordItem.Abstracts;
                        if (!string.IsNullOrEmpty(ModelTestRecordItem.TestType))
                        {
                            if (ModelTestRecordItem.TestType == "1")
                            {
                                this.txtTestType.Text = "单选题";
                                this.txtEItem.Hidden = true;
                            }
                            else if (ModelTestRecordItem.TestType == "2")
                            {
                                this.txtTestType.Text = "多选题";
                            }
                            else if (ModelTestRecordItem.TestType == "3")
                            {
                                this.txtTestType.Text = "判断题";
                                this.txtCItem.Hidden = true;
                                this.txtDItem.Hidden = true;
                                this.txtEItem.Hidden = true;
                            }
                        }
                        this.txtAItem.Text = ModelTestRecordItem.AItem;
                        this.txtBItem.Text = ModelTestRecordItem.BItem;
                        this.txtCItem.Text = ModelTestRecordItem.CItem;
                        this.txtDItem.Text = ModelTestRecordItem.DItem;
                        this.txtEItem.Text = ModelTestRecordItem.EItem;

                        if (!string.IsNullOrEmpty(ModelTestRecordItem.AnswerItems))
                        {
                            this.txtAnswerItems.Text = ModelTestRecordItem.AnswerItems.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E");
                        }
                        if (ModelTestRecordItem.Score != null)
                        {
                            this.txtScore.Text = ModelTestRecordItem.Score.ToString();
                        }
                        if (ModelTestRecordItem.SubjectScore != null)
                        {
                            this.txtSubjectScore.Text = ModelTestRecordItem.SubjectScore.ToString();
                        }
                        if (!string.IsNullOrEmpty(ModelTestRecordItem.SelectedItem))
                        {
                            this.txtSelectedItem.Text = ModelTestRecordItem.SelectedItem.Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E");
                        }
                        if (!string.IsNullOrEmpty(ModelTestRecordItem.AttachUrl))
                        {
                            this.divFile.InnerHtml = BLL.UploadAttachmentService.ShowAttachment("../", ModelTestRecordItem.AttachUrl);
                        }

                        var ModelTestRecord = BLL.ModelTestRecordService.GetModelTestRecordById(ModelTestRecordItem.ModelTestRecordId);
                        if (ModelTestRecord != null)
                        {
                            if (!string.IsNullOrEmpty(ModelTestRecord.TestManId))
                            {
                                Model.SitePerson_Person person = BLL.PersonService.GetPersonById(ModelTestRecord.TestManId);
                                if (person != null)
                                {
                                    this.txtTestManName.Text = person.PersonName;
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