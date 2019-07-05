using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class TrainTestItemEdit :PageBase
    {
        #region 定义变量
        public string TrainTestId
        {
            get
            {
                return (string)ViewState["TrainTestId"];
            }
            set
            {
                ViewState["TrainTestId"] = value;
            }
        }

        public string TrainTestItemId
        {
            get
            {
                return (string)ViewState["TrainTestItemId"];
            }
            set
            {
                ViewState["TrainTestItemId"] = value;
            }
        }

        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.TrainTestId = Request.Params["TrainTestId"];
                this.TrainTestItemId = Request.Params["TrainTestItemId"];
                if (!string.IsNullOrEmpty(this.TrainTestItemId))
                {
                    Model.Training_TrainTestDBItem trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
                    if (trainTestDBItem!=null)
                    {
                        this.txtTrainTestItemCode.Text = trainTestDBItem.TrainTestItemCode;
                        this.txtTrainTestItemName.Text = trainTestDBItem.TraiinTestItemName;
                        if (!string.IsNullOrEmpty(trainTestDBItem.AttachUrl))
                        {
                            //this.FullAttachUrl = trainTestDBItem.AttachUrl;
                            //this.lbAttachUrl.Text = trainTestDBItem.AttachUrl.Substring(trainTestDBItem.AttachUrl.IndexOf("~") + 1);
                        }
                    }
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState, bool isClose)
        {
            Model.Training_TrainTestDBItem trainTestDBItem = new Model.Training_TrainTestDBItem
            {
                TrainTestItemCode = this.txtTrainTestItemCode.Text.Trim(),
                TraiinTestItemName = this.txtTrainTestItemName.Text.Trim(),
                //trainTestDBItem.AttachUrl = this.FullAttachUrl;
                UpState = upState
            };
            if (string.IsNullOrEmpty(this.TrainTestItemId))
            {
                trainTestDBItem.IsPass = true;
                trainTestDBItem.CompileMan = this.CurrUser.UserName;
                trainTestDBItem.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                trainTestDBItem.CompileDate = DateTime.Now;
                trainTestDBItem.TrainTestId = this.TrainTestId;
                trainTestDBItem.TrainTestItemId = SQLHelper.GetNewID(typeof(Model.Training_TrainTestDBItem));
                TrainTestItemId = trainTestDBItem.TrainTestItemId;
                BLL.TrainTestDBItemService.AddTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全试题库");
            }
            else
            {
                Model.Training_TrainTestDBItem t = BLL.TrainTestDBItemService.GetTrainTestDBItemById(this.TrainTestItemId);
                if (t != null)
                {
                    trainTestDBItem.TrainTestId = t.TrainTestId;
                }
                trainTestDBItem.TrainTestItemId = this.TrainTestItemId;
                BLL.TrainTestDBItemService.UpdateTrainTestDBItem(trainTestDBItem);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全试题库");
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_1,true);
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2,true);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpTrainTestItem(TrainTestItemId, unit.UnitId);//上报
            }
        }
        #endregion

        #region 上报
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpTrainTestItem(string trainTestItemId, string unitId)
        {  /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTraining_TrainTestDBItemTableCompleted += new EventHandler<HSSEService.DataInsertTraining_TrainTestDBItemTableCompletedEventArgs>(poxy_DataInsertTraining_TrainTestDBItemTableCompleted);
            var TrainTestDBItemList = from x in Funs.DB.Training_TrainTestDBItem
                                      join y in Funs.DB.AttachFile on x.TrainTestItemId equals y.ToKeyId
                                   where x.TrainTestItemId == trainTestItemId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                   select new HSSEService.Training_TrainTestDBItem
                                   {
                                       TrainTestItemId = x.TrainTestItemId,
                                       TrainTestId = x.TrainTestId,
                                       TrainTestItemCode = x.TrainTestItemCode,
                                       TraiinTestItemName = x.TraiinTestItemName,
                                       CompileMan = x.CompileMan,
                                       CompileDate = x.CompileDate,
                                       UnitId = unitId,
                                       IsPass = null,
                                       AttachFileId = y.AttachFileId,
                                       ToKeyId = y.ToKeyId,
                                       AttachSource = y.AttachSource,
                                       AttachUrl = y.AttachUrl,
                                       ////附件转为字节传送
                                       FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                   };
            poxy.DataInsertTraining_TrainTestDBItemTableAsync(TrainTestDBItemList.ToList());
        }

        /// <summary>
        /// 安全试题明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTraining_TrainTestDBItemTableCompleted(object sender, HSSEService.DataInsertTraining_TrainTestDBItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(item);
                    if (trainTestDBItem != null)
                    {
                        trainTestDBItem.UpState = BLL.Const.UpState_3;
                        BLL.TrainTestDBItemService.UpdateTrainTestDBItemIsPass(trainTestDBItem);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全试题明细】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全试题明细】上报到集团公司失败；");
            }
        }
        #endregion        

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TrainTestDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                    //this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnSaveUp.Hidden = false;
                    //this.btnDelete.Hidden = false;
                    //this.btnUpFile.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证试题名称是否存在
        /// <summary>
        /// 验证试题名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_TrainTestDBItem.FirstOrDefault(x => x.IsPass == true && x.TrainTestId == this.TrainTestId && x.TraiinTestItemName == this.txtTrainTestItemName.Text.Trim() && (x.TrainTestItemId != this.TrainTestItemId || (this.TrainTestItemId == null && x.TrainTestItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的试题名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TrainTestItemId))
            {
                SaveData(BLL.Const.UpState_1, false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/TrainTestDB&menuId=F58EE8ED-9EB5-47C7-9D7F-D751EFEA44CA", TrainTestItemId)));
        }
    }
}