using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class KnowledgeDBItemEdit : PageBase
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
        /// 应知应会库明细主键
        /// </summary>
        public string KnowledgeItemId
        {
            get
            {
                return (string)ViewState["KnowledgeItemId"];
            }
            set
            {
                ViewState["KnowledgeItemId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.KnowledgeId = Request.Params["KnowledgeId"];
                this.KnowledgeItemId = Request.Params["KnowledgeItemId"];
                if (!string.IsNullOrEmpty(this.KnowledgeItemId))
                {
                    Model.Training_KnowledgeItem knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(this.KnowledgeItemId);
                    if (knowledgeItem != null)
                    {
                        this.KnowledgeId = knowledgeItem.KnowledgeId;
                        this.txtKnowledgeItemCode.Text = knowledgeItem.KnowledgeItemCode;
                        this.txtKnowledgeItemName.Text = knowledgeItem.KnowledgeItemName;
                        this.txtRemark.Text = knowledgeItem.Remark;
                    }
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
        {
            Model.Training_KnowledgeItem knowledgeItem = new Model.Training_KnowledgeItem
            {
                KnowledgeItemCode = this.txtKnowledgeItemCode.Text.Trim(),
                KnowledgeItemName = this.txtKnowledgeItemName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                UpState = upState
            };
            if (string.IsNullOrEmpty(this.KnowledgeItemId))
            {
                knowledgeItem.CompileMan = this.CurrUser.UserName;
                knowledgeItem.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                knowledgeItem.CompileDate = DateTime.Now;
                knowledgeItem.IsPass = true;
                knowledgeItem.KnowledgeItemId = SQLHelper.GetNewID(typeof(Model.Training_KnowledgeItem));
                KnowledgeItemId = knowledgeItem.KnowledgeItemId;
                knowledgeItem.KnowledgeId = this.KnowledgeId;
                BLL.KnowledgeItemService.AddKnowledgeItem(knowledgeItem);
                BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                Model.Training_KnowledgeItem k = BLL.KnowledgeItemService.GetKnowledgeItemById(this.KnowledgeId);
                if (k != null)
                {
                    knowledgeItem.KnowledgeId = k.KnowledgeId;
                }
                knowledgeItem.KnowledgeItemId = this.KnowledgeItemId;
                BLL.KnowledgeItemService.UpdateKnowledgeItem(knowledgeItem);

                BLL.LogService.AddSys_Log(this.CurrUser, knowledgeItem.KnowledgeItemCode, knowledgeItem.KnowledgeItemId, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnModify);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_1);
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpKnowledgeItem(KnowledgeItemId, unit.UnitId);//上报
            }
        }

        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpKnowledgeItem(string knowledgeItemId, string unitId)
        {  /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTraining_KnowledgeItemTableCompleted += new EventHandler<HSSEService.DataInsertTraining_KnowledgeItemTableCompletedEventArgs>(poxy_DataInsertTraining_KnowledgeItemTableCompleted);
            var TrainingItemList = from x in Funs.DB.Training_KnowledgeItem
                                   where x.KnowledgeItemId == knowledgeItemId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                   select new HSSEService.Training_KnowledgeItem
                                   {
                                       KnowledgeItemId = x.KnowledgeItemId,
                                       KnowledgeId = x.KnowledgeId,
                                       KnowledgeItemCode = x.KnowledgeItemCode,
                                       KnowledgeItemName = x.KnowledgeItemName,
                                       Remark = x.Remark,
                                       CompileMan = x.CompileMan,
                                       CompileDate = x.CompileDate,
                                       UnitId = unitId,
                                       IsPass = null,
                                   };
            poxy.DataInsertTraining_KnowledgeItemTableAsync(TrainingItemList.ToList());
        }

        /// <summary>
        /// 应知应会明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTraining_KnowledgeItemTableCompleted(object sender, HSSEService.DataInsertTraining_KnowledgeItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(item);
                    if (knowledgeItem != null)
                    {
                        knowledgeItem.UpState = BLL.Const.UpState_3;
                        BLL.KnowledgeItemService.UpdateKnowledgeItemIsPass(knowledgeItem);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【应知应会明细】上报到集团公司" + idList.Count.ToString() + "条数据；",null, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【应知应会明细】上报到集团公司失败；", null, BLL.Const.KnowledgeDBMenuId, BLL.Const.BtnUploadResources);
            }
        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.KnowledgeDBMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnSaveUp.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证培训教材库名称是否存在
        /// <summary>
        /// 验证培训教材库名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Training_KnowledgeItem.FirstOrDefault(x => x.IsPass == true && x.KnowledgeId == this.KnowledgeId && x.KnowledgeItemName == this.txtKnowledgeItemName.Text.Trim() && (x.KnowledgeItemId != this.KnowledgeItemId || (this.KnowledgeItemId == null && x.KnowledgeItemId != null)));
            if (q != null)
            {
                ShowNotify("输入的名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}