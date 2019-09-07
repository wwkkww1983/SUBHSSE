using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.Technique
{
    public partial class AppraiseEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全评价
        /// </summary>
        public string AppraiseId
        {
            get
            {
                return (string)ViewState["AppraiseId"];
            }
            set
            {
                ViewState["AppraiseId"] = value;
            }
        }
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
                ////权限按钮方法
                this.GetButtonPower();
                //整理人下拉选项
                this.ddlArrangementPerson.DataTextField = "UserName";
                ddlArrangementPerson.DataValueField = "UserId";
                ddlArrangementPerson.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                ddlArrangementPerson.DataBind();
                Funs.FineUIPleaseSelect(this.ddlArrangementPerson);

                //加载默认整理人、整理日期
                this.ddlArrangementPerson.SelectedValue = this.CurrUser.UserId;
                this.dpkArrangementDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.AppraiseId = Request.Params["AppraiseId"];
                if (!string.IsNullOrEmpty(this.AppraiseId))
                {
                    var Appraise = BLL.AppraiseService.GetAppraiseById(this.AppraiseId);
                    if (Appraise != null)
                    {
                        this.txtAppraiseCode.Text = Appraise.AppraiseCode;
                        this.txtAppraiseTitle.Text = Appraise.AppraiseTitle;
                        if (Appraise.AppraiseDate != null)
                        {
                            this.dpkAppraiseDate.Text = string.Format("{0:yyyy-MM-dd}", Appraise.AppraiseDate);
                        }
                        if (Appraise.ArrangementDate != null)
                        {
                            this.dpkArrangementDate.Text = string.Format("{0:yyyy-MM-dd}", Appraise.ArrangementDate);
                        }
                        this.txtAbstract.Text = Appraise.Abstract;
                        if (!string.IsNullOrEmpty(Appraise.ArrangementPerson))
                        {
                            this.ddlArrangementPerson.SelectedItem.Text = Appraise.ArrangementPerson;
                        }                       
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_1);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
                UpAppraise(this.AppraiseId, unit.UnitId);//上报
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
        {
            Model.Technique_Appraise appraise = new Model.Technique_Appraise
            {
                AppraiseCode = this.txtAppraiseCode.Text.Trim(),
                AppraiseTitle = this.txtAppraiseTitle.Text.Trim(),
                Abstract = this.txtAbstract.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkAppraiseDate.Text.Trim()))
            {
                appraise.AppraiseDate = Convert.ToDateTime(this.dpkAppraiseDate.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.dpkArrangementDate.Text.Trim()))
            {
                appraise.ArrangementDate = Convert.ToDateTime(this.dpkArrangementDate.Text.Trim());
            }
            appraise.UpState = upState;
            if (this.ddlArrangementPerson.SelectedValue != "null")
            {
                appraise.ArrangementPerson = this.ddlArrangementPerson.SelectedItem.Text;
            }
            appraise.UnitId = this.CurrUser.UnitId;
            if (string.IsNullOrEmpty(this.AppraiseId))
            {
                appraise.IsPass = true;
                appraise.CompileMan = this.CurrUser.UserName;
                appraise.CompileDate = DateTime.Now;
                this.AppraiseId = appraise.AppraiseId = SQLHelper.GetNewID(typeof(Model.Technique_Appraise));
                BLL.AppraiseService.AddAppraise(appraise);
                BLL.LogService.AddSys_Log(this.CurrUser, appraise.AppraiseCode, appraise.AppraiseId, BLL.Const.AppraiseMenuId, Const.BtnAdd);
            }
            else
            {
                appraise.AppraiseId = this.AppraiseId;
                BLL.AppraiseService.UpdateAppraise(appraise);
                BLL.LogService.AddSys_Log(this.CurrUser, appraise.AppraiseCode, appraise.AppraiseId, BLL.Const.AppraiseMenuId, Const.BtnModify);
            }
        }
        #endregion

        #region 安全评价上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="appraiseId"></param>
        /// <param name="unitId"></param>
        private void UpAppraise(string appraiseId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_AppraiseTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_AppraiseTableCompletedEventArgs>(poxy_DataInsertTechnique_AppraiseTableCompleted);
            var appraise = from x in Funs.DB.View_Technique_Appraise
                           join y in Funs.DB.AttachFile on x.AppraiseId equals y.ToKeyId
                           where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                           select new BLL.HSSEService.Technique_Appraise
                           {
                               AppraiseId = x.AppraiseId,
                               AppraiseCode = x.AppraiseCode,
                               AppraiseTitle = x.AppraiseTitle,
                               Abstract = x.Abstract,
                               AppraiseDate = x.AppraiseDate,
                               ArrangementPerson = x.ArrangementPerson,
                               ArrangementDate = x.ArrangementDate,
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
            poxy.DataInsertTechnique_AppraiseTableAsync(appraise.ToList());
        }
               
        /// <summary>
        /// 安全评价上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_AppraiseTableCompleted(object sender, HSSEService.DataInsertTechnique_AppraiseTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var appraise = BLL.AppraiseService.GetAppraiseById(item);
                    if (appraise != null)
                    {
                        appraise.UpState = BLL.Const.UpState_3;
                        BLL.AppraiseService.UpdateAppraise(appraise);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【安全评价】上报到集团公司" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.AppraiseMenuId, Const.BtnUploadResources);             
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全评价】上报到集团公司失败；", string.Empty, BLL.Const.AppraiseMenuId, Const.BtnUploadResources);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Appraise&type=-1", this.AppraiseId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.AppraiseId))
                {
                    SaveData(BLL.Const.UpState_1);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Appraise&menuId={1}", this.AppraiseId, BLL.Const.AppraiseMenuId)));
            }
        }
        #endregion

        #region 验证编号是否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Appraise.FirstOrDefault(x => x.IsPass == true && x.AppraiseCode == this.txtAppraiseCode.Text.Trim() && (x.AppraiseId != this.AppraiseId || (this.AppraiseId == null && x.AppraiseId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Technique_Appraise.FirstOrDefault(x => x.IsPass == true && x.AppraiseTitle == this.txtAppraiseTitle.Text.Trim() && (x.AppraiseId != this.AppraiseId || (this.AppraiseId == null && x.AppraiseId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的标题已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取按钮权限
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AppraiseMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnSaveUp.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}