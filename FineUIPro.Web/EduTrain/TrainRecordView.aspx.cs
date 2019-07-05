using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainRecordView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_EduTrain_TrainRecordDetail> trainRecordDetails = new List<Model.View_EduTrain_TrainRecordDetail>();
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                trainRecordDetails.Clear();

                this.TrainingId = Request.Params["TrainingId"];
                var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (trainRecord != null)
                {
                    this.txtTrainingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.TrainingId);
                    Model.Base_TrainType trainType = BLL.TrainTypeService.GetTrainTypeById(trainRecord.TrainTypeId);
                    if (trainType != null)
                    {
                        this.txtTrainType.Text = trainType.TrainTypeName;
                    }
                    Model.Base_TrainLevel trainLevel = BLL.TrainLevelService.GetTrainLevelById(trainRecord.TrainLevelId);
                    if (trainLevel != null)
                    {
                        this.txtTrainLevel.Text = trainLevel.TrainLevelName;
                    }
                    this.txtTrainTitle.Text = trainRecord.TrainTitle;
                    if (!string.IsNullOrEmpty(trainRecord.UnitIds))
                    {
                        string unitNames = string.Empty;
                        string[] unitIds = trainRecord.UnitIds.Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                            if (unit != null)
                            {
                                unitNames += unit.UnitName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                        this.txtUnits.Text = unitNames;
                    }
                    this.txtTeachMan.Text = trainRecord.TeachMan;
                    this.txtTeachAddress.Text = trainRecord.TeachAddress;
                    if (trainRecord.TeachHour != null)
                    {
                        this.txtTeachHour.Text = trainRecord.TeachHour.ToString();
                    }
                    if (trainRecord.TrainStartDate != null)
                    {
                        this.txtTrainStartDate.Text = string.Format("{0:yyyy-MM-dd}", trainRecord.TrainStartDate);
                    }
                    if (trainRecord.TrainPersonNum != null)
                    {
                        this.txtTrainPersonNum.Text = Convert.ToString(trainRecord.TrainPersonNum);
                    }
                    this.txtTrainContent.Text = trainRecord.TrainContent;
                    trainRecordDetails = (from x in Funs.DB.View_EduTrain_TrainRecordDetail where x.TrainingId == this.TrainingId orderby x.UnitName select x).ToList();
                }
                Grid1.DataSource = trainRecordDetails;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectTrainRecordMenuId;
                this.ctlAuditFlow.DataId = this.TrainingId;
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
            if (!string.IsNullOrEmpty(this.TrainingId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId={1}&type=-1", this.TrainingId, BLL.Const.ProjectTrainRecordMenuId)));
            }
        }
        #endregion
    }
}