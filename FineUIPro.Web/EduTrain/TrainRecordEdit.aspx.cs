using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainRecordEdit : PageBase
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
        /// 主键
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                trainRecordDetails.Clear();

                this.TrainingId = Request.Params["TrainingId"];
                var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (trainRecord != null)
                {
                    this.ProjectId = trainRecord.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtTrainingCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.TrainingId);
                    if (!string.IsNullOrEmpty(trainRecord.TrainTypeId))
                    {
                        this.drpTrainType.SelectedValue = trainRecord.TrainTypeId;
                    }
                    if (!string.IsNullOrEmpty(trainRecord.TrainLevelId))
                    {
                        this.drpTrainLevel.SelectedValue = trainRecord.TrainLevelId;
                    }
                    this.txtTrainTitle.Text = trainRecord.TrainTitle;
                    if (!string.IsNullOrEmpty(trainRecord.UnitIds))
                    {
                        this.drpUnits.SelectedValueArray = trainRecord.UnitIds.Split(',');
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
                else
                {
                    ////自动生成编码
                    this.txtTrainingCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectTrainRecordMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtTrainStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtTeachMan.Text = this.CurrUser.UserName;
                    this.txtTeachAddress.Text = "办公室";
                    this.txtTeachHour.Text = "1";
                    //this.txtTrainEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                Grid1.DataSource = trainRecordDetails;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectTrainRecordMenuId;
                this.ctlAuditFlow.DataId = this.TrainingId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void InitDropDownList()
        {
            //培训类别
            this.drpTrainType.DataTextField = "TrainTypeName";
            this.drpTrainType.DataValueField = "TrainTypeId";
            this.drpTrainType.DataSource = BLL.TrainTypeService.GetTrainTypeList();
            drpTrainType.DataBind();
            Funs.FineUIPleaseSelect(this.drpTrainType, "-请选择-");
            //培训级别
            this.drpTrainLevel.DataTextField = "TrainLevelName";
            this.drpTrainLevel.DataValueField = "TrainLevelId";
            this.drpTrainLevel.DataSource = BLL.TrainLevelService.GetTrainLevelList();
            drpTrainLevel.DataBind();
            Funs.FineUIPleaseSelect(this.drpTrainLevel, "-请选择-");
            //培训单位
            BLL.UnitService.InitUnitDropDownList(this.drpUnits, this.ProjectId, false);
            this.drpUnits.SelectedValue = this.CurrUser.UnitId;
        }

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowPerson.aspx?TrainingId={0}&TrainTypeId={1}", this.TrainingId, this.drpTrainType.SelectedValue, "编辑 - ")));
        }
        #endregion

        #region 提交按钮
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
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }

            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.EduTrain_TrainRecord trainRecord = new Model.EduTrain_TrainRecord
            {
                TrainingCode = this.txtTrainingCode.Text.Trim(),
                ProjectId = this.ProjectId,
                TrainTitle = this.txtTrainTitle.Text.Trim(),
                TrainContent = this.txtTrainContent.Text.Trim(),
                TrainStartDate = Funs.GetNewDateTime(this.txtTrainStartDate.Text.Trim()),
                TrainPersonNum = this.Grid1.Rows.Count,
                TeachHour = Funs.GetNewDecimalOrZero(this.txtTeachHour.Text.Trim()),
                TeachMan = this.txtTeachMan.Text.Trim(),
                TeachAddress = this.txtTeachAddress.Text.Trim()
            };
            if (this.drpTrainType.SelectedValue != BLL.Const._Null)
            {
                trainRecord.TrainTypeId = this.drpTrainType.SelectedValue;
            }
            if (this.drpTrainLevel.SelectedValue != BLL.Const._Null)
            {
                trainRecord.TrainLevelId = this.drpTrainLevel.SelectedValue;
            }
            //培训单位
            string unitIds = string.Empty;
            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                unitIds += item + ",";
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
            }
            trainRecord.UnitIds = unitIds;
            ////单据状态
            trainRecord.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                trainRecord.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.TrainingId))
            {
                trainRecord.TrainingId = this.TrainingId;
                BLL.EduTrain_TrainRecordService.UpdateTraining(trainRecord);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改培训记录", trainRecord.TrainingId);
            }
            else
            {
                trainRecord.TrainingId = SQLHelper.GetNewID(typeof(Model.EduTrain_TrainRecord));
                this.TrainingId = trainRecord.TrainingId;
                trainRecord.CompileMan = this.CurrUser.UserId;
                BLL.EduTrain_TrainRecordService.AddTraining(trainRecord);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加培训记录", trainRecord.TrainingId);
            }

            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                Model.EduTrain_TrainRecordDetail detail = BLL.EduTrain_TrainRecordDetailService.GetTrainDetailByTrainDetailId(values.Value<string>("TrainDetailId").ToString());
                if (detail != null)
                {
                    if (values.Value<string>("CheckResult").ToString() == "1")
                    {
                        detail.CheckResult = true;
                    }
                    else
                    {
                        detail.CheckResult = false;
                    }
                    detail.CheckScore =Funs.GetNewDecimalOrZero(values.Value<string>("CheckScore").ToString()); ;
                    BLL.EduTrain_TrainRecordDetailService.UpdateTrainDetail(detail);
                }
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectTrainRecordMenuId, this.TrainingId, (type == BLL.Const.BtnSubmit ? true : false), this.txtTrainTitle.Text.Trim(), "../EduTrain/TrainRecordView.aspx?TrainingId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            trainRecordDetails = (from x in Funs.DB.View_EduTrain_TrainRecordDetail where x.TrainingId == this.TrainingId orderby x.UnitName select x).ToList();
            Grid1.DataSource = trainRecordDetails;
            Grid1.DataBind();
            this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.EduTrain_TrainRecordDetailService.DeleteTrainDetailByTrainDetail(rowID);
                }
                trainRecordDetails = (from x in Funs.DB.View_EduTrain_TrainRecordDetail where x.TrainingId == this.TrainingId orderby x.UnitName select x).ToList();
                this.Grid1.DataSource = trainRecordDetails;
                this.Grid1.DataBind();
                this.txtTrainPersonNum.Text = trainRecordDetails.Count.ToString();
                this.ShowNotify("删除数据成功!（表格数据已重新绑定）");
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
            if (this.drpTrainType.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择培训类型！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.TrainingId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainRecord&menuId={1}", this.TrainingId, BLL.Const.ProjectTrainRecordMenuId)));
        }
        #endregion
    }
}