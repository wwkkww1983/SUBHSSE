using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Law
{
    public partial class ManageRuleEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ManageRuleId
        {
            get
            {
                return (string)ViewState["ManageRuleId"];
            }
            set
            {
                ViewState["ManageRuleId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
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
                this.GetButtonPower();//设置权限
                LoadData();

                //加载管理规定类别下拉选项
                this.ddlManageRuleTypeId.DataTextField = "ManageRuleTypeName";
                this.ddlManageRuleTypeId.DataValueField = "ManageRuleTypeId";
                this.ddlManageRuleTypeId.DataSource = BLL.ManageRuleTypeService.GetManageRuleTypeList();
                ddlManageRuleTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlManageRuleTypeId);

                //整理人下拉选项
                this.ddlCompileMan.DataTextField = "UserName";
                ddlCompileMan.DataValueField = "UserId";
                ddlCompileMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                ddlCompileMan.DataBind();
                Funs.FineUIPleaseSelect(this.ddlCompileMan);

                //加载默认整理人、整理日期
                this.ddlCompileMan.SelectedValue = this.CurrUser.UserId;
                this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.ManageRuleId = Request.Params["ManageRuleId"];
                if (!string.IsNullOrEmpty(this.ManageRuleId))
                {
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(this.ManageRuleId);
                    if (manageRule != null)
                    {
                        this.txtManageRuleCode.Text = manageRule.ManageRuleCode;
                        this.txtManageRuleName.Text = manageRule.ManageRuleName;
                        if (!string.IsNullOrEmpty(manageRule.ManageRuleTypeId))
                        {
                            this.ddlManageRuleTypeId.SelectedValue = manageRule.ManageRuleTypeId;
                        }
                        this.txtVersionNo.Text = manageRule.VersionNo;
                        this.ddlCompileMan.SelectedItem.Text = manageRule.CompileMan;
                        if (manageRule.CompileDate != null)
                        {
                            this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", manageRule.CompileDate);
                        }
                        this.txtRemark.Text = manageRule.Remark;
                        this.txtSeeFile.Text = HttpUtility.HtmlDecode(manageRule.SeeFile);
                    }
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
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
                UpManageRule(this.ManageRuleId, unit.UnitId);//上报
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState, bool isColose)
        {
            Model.Law_ManageRule manageRule = new Model.Law_ManageRule
            {
                ManageRuleCode = this.txtManageRuleCode.Text.Trim(),
                ManageRuleName = this.txtManageRuleName.Text.Trim()
            };
            if (this.ddlManageRuleTypeId.SelectedValue != BLL.Const._Null)
            {
                manageRule.ManageRuleTypeId = this.ddlManageRuleTypeId.SelectedValue;
            }
            manageRule.VersionNo = this.txtVersionNo.Text.Trim();
            manageRule.AttachUrl = this.FullAttachUrl;
            manageRule.Remark = this.txtRemark.Text.Trim();
            manageRule.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
            manageRule.UpState = upState;
            manageRule.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                manageRule.IsPass = true;
                manageRule.CompileMan = this.CurrUser.UserName;
                manageRule.CompileDate = System.DateTime.Now;
                manageRule.UnitId = this.CurrUser.UnitId;
                this.ManageRuleId = SQLHelper.GetNewID(typeof(Model.Law_ManageRule));
                manageRule.ManageRuleId = this.ManageRuleId;
                BLL.ManageRuleService.AddManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                manageRule.ManageRuleId = this.ManageRuleId;
                BLL.ManageRuleService.UpdateManageRule(manageRule);
                BLL.LogService.AddSys_Log(this.CurrUser, manageRule.ManageRuleCode, manageRule.ManageRuleId, BLL.Const.ManageRuleMenuId, BLL.Const.BtnModify);
            }
            if (isColose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 管理规定上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <param name="unitId"></param>
        public void UpManageRule(string manageRuleId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_ManageRuleTableCompleted += new EventHandler<HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs>(poxy_DataInsertLaw_ManageRuleTableCompleted);
            var manageRule = from x in Funs.DB.View_Law_ManageRule
                             join y in Funs.DB.AttachFile on x.ManageRuleId equals y.ToKeyId
                             where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                             select new HSSEService.Law_ManageRule
                             {
                                 ManageRuleId = x.ManageRuleId,
                                 ManageRuleCode = x.ManageRuleCode,
                                 ManageRuleName = x.ManageRuleName,
                                 ManageRuleTypeId = x.ManageRuleTypeId,
                                 VersionNo = x.VersionNo,
                                 CompileMan = x.CompileMan,
                                 CompileDate = x.CompileDate,
                                 Remark = x.Remark,
                                 IsPass = null,
                                 UnitId = unitId,
                                 AttachFileId = y.AttachFileId,
                                 ToKeyId = y.ToKeyId,
                                 AttachSource = y.AttachSource,
                                 AttachUrl = y.AttachUrl,
                                 ////附件转为字节传送
                                 FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                             };
            poxy.DataInsertLaw_ManageRuleTableAsync(manageRule.ToList());
        }

        /// <summary>
        /// 管理规定上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_ManageRuleTableCompleted(object sender, HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(item);
                    if (manageRule != null)
                    {
                        manageRule.UpState = BLL.Const.UpState_3;
                        BLL.ManageRuleService.UpdateManageRule(manageRule);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全管理规定】上报到集团公司" + idList.Count.ToString() + "条数据；", null, BLL.Const.ManageRuleMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全管理规定】上报到集团公司失败；", null, BLL.Const.ManageRuleMenuId, BLL.Const.BtnUploadResources);
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
            if (string.IsNullOrEmpty(this.ManageRuleId))
            {
                SaveData(BLL.Const.UpState_1, false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId={1}", ManageRuleId, BLL.Const.ManageRuleMenuId)));
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageRuleMenuId);
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

        #region 验证管理规定名称是否存在
        /// <summary>
        /// 验证管理规定名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_ManageRule.FirstOrDefault(x => x.IsPass == true && x.ManageRuleName == this.txtManageRuleName.Text.Trim() && (x.ManageRuleId != this.ManageRuleId || (this.ManageRuleId == null && x.ManageRuleId != null)));
            if (standard != null)
            {
                ShowNotify("输入的文件名称已存在！", MessageBoxIcon.Warning);
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&type=-1", ManageRuleId, BLL.Const.ManageRuleMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.ManageRuleId))
                {
                    SaveData(BLL.Const.UpState_1, false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId={1}", ManageRuleId, BLL.Const.ManageRuleMenuId)));
            }
        }
        #endregion
    }
}