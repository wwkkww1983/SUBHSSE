using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class SpecialSchemeEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 专项方案
        /// </summary>
        public string SpecialSchemeId
        {
            get
            {
                return (string)ViewState["SpecialSchemeId"];
            }
            set
            {
                ViewState["SpecialSchemeId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //单位
                this.ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataSource = BLL.UnitService.GetUnitDropDownList();
                ddlUnit.DataBind();
                Funs.FineUIPleaseSelect(this.ddlUnit);
                //类型
                this.ddlSpecialSchemeType.DataTextField = "SpecialSchemeTypeName";
                ddlSpecialSchemeType.DataValueField = "SpecialSchemeTypeId";
                ddlSpecialSchemeType.DataSource = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeList();
                ddlSpecialSchemeType.DataBind();
                Funs.FineUIPleaseSelect(this.ddlSpecialSchemeType);
                //人员
                this.ddlCompileMan.DataTextField = "UserName";
                ddlCompileMan.DataValueField = "UserId";
                ddlCompileMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                ddlCompileMan.DataBind();
                Funs.FineUIPleaseSelect(this.ddlCompileMan);

                //加载默认整理人、整理日期
                this.ddlCompileMan.SelectedValue = this.CurrUser.UserId;
                this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.SpecialSchemeId = Request.Params["SpecialSchemeId"];
                if (!string.IsNullOrEmpty(this.SpecialSchemeId))
                {
                    var SpecialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(this.SpecialSchemeId);
                    if (SpecialScheme != null)
                    {
                        this.txtSpecialSchemeCode.Text = SpecialScheme.SpecialSchemeCode;
                        this.txtSpecialSchemeName.Text = SpecialScheme.SpecialSchemeName;
                        this.txtSummary.Text = SpecialScheme.Summary;
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", SpecialScheme.CompileDate);
                        if (!string.IsNullOrEmpty(SpecialScheme.SpecialSchemeTypeId))
                        {
                            this.ddlSpecialSchemeType.SelectedValue = SpecialScheme.SpecialSchemeTypeId;
                        }
                        if (!string.IsNullOrEmpty(SpecialScheme.UnitId))
                        {
                            this.ddlUnit.SelectedValue = SpecialScheme.UnitId;
                        }
                        if (!string.IsNullOrEmpty(SpecialScheme.CompileMan))
                        {
                            this.ddlCompileMan.SelectedItem.Text = SpecialScheme.CompileMan;
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
                UpSpecialScheme(this.SpecialSchemeId, unit.UnitId);//上报
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upSate)
        {
            Model.Technique_SpecialScheme specialScheme = new Model.Technique_SpecialScheme
            {
                UpState = upSate
            };
            if (!string.IsNullOrEmpty(this.txtSpecialSchemeCode.Text.Trim()))
            {

                specialScheme.SpecialSchemeCode = this.txtSpecialSchemeCode.Text.Trim();
            }
            else
            {
                ShowNotify("请输入方案编号", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(this.txtSpecialSchemeName.Text.Trim()))
            {
                specialScheme.SpecialSchemeName = this.txtSpecialSchemeName.Text.Trim();
            }
            else
            {
                ShowNotify("请输入方案名称", MessageBoxIcon.Warning);
                return;
            }
            specialScheme.Summary = this.txtSummary.Text.Trim();
            if (this.ddlUnit.SelectedValue != Const._Null)
            {
                specialScheme.UnitId = this.ddlUnit.SelectedValue;
            }
            else
            {
                ShowNotify("请选择单位", MessageBoxIcon.Warning);
                return;
            }
            ////专项方案类型下拉框
            if (this.ddlSpecialSchemeType.SelectedValue != BLL.Const._Null || !string.IsNullOrEmpty(this.ddlSpecialSchemeType.Text))
            {
                var specialSchemeType = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeByName(this.ddlSpecialSchemeType.SelectedText);
                if (specialSchemeType != null)
                {
                    specialScheme.SpecialSchemeTypeId = specialSchemeType.SpecialSchemeTypeId;
                }
                else
                {
                    Model.Base_SpecialSchemeType newSpecialSchemeType = new Model.Base_SpecialSchemeType
                    {
                        SpecialSchemeTypeId = SQLHelper.GetNewID(typeof(Model.Base_SpecialSchemeType)),
                        SpecialSchemeTypeName = this.ddlSpecialSchemeType.Text
                    };
                    BLL.SpecialSchemeTypeService.AddSpecialSchemeType(newSpecialSchemeType);
                    specialScheme.SpecialSchemeTypeId = newSpecialSchemeType.SpecialSchemeTypeId;
                }
            }
            if (this.ddlCompileMan.SelectedValue != null)
            {
                specialScheme.CompileMan = this.ddlCompileMan.SelectedItem.Text;
            }
            else
            {
                specialScheme.CompileMan = this.CurrUser.UserName;
            }
            if (!string.IsNullOrEmpty(this.dpkCompileDate.Text))
            {
                specialScheme.CompileDate = Convert.ToDateTime(this.dpkCompileDate.Text);
            }
            else
            {
                specialScheme.CompileDate = DateTime.Now;
            }
            if (string.IsNullOrEmpty(this.SpecialSchemeId))
            {
                specialScheme.IsPass = true;
                this.SpecialSchemeId = specialScheme.SpecialSchemeId = SQLHelper.GetNewID(typeof(Model.Technique_SpecialScheme));
                BLL.SpecialSchemeService.AddSpecialSchemeList(specialScheme);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加专项方案");
            }
            else
            {
                specialScheme.SpecialSchemeId = this.SpecialSchemeId;
                BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改专项方案");
            }
        }
        #endregion

        #region 专项方案上报到集团公司
        /// <summary>
        /// 上报资源
        /// </summary>
        /// <param name="specialSchemeId"></param>
        /// <param name="unitId"></param>
        private void UpSpecialScheme(string specialSchemeId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_SpecialSchemeTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs>(poxy_DataInsertTechnique_SpecialSchemeTableCompleted);
            var specialScheme = from x in Funs.DB.View_Technique_SpecialScheme
                                join y in Funs.DB.AttachFile on x.SpecialSchemeId equals y.ToKeyId
                                where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                select new HSSEService.Technique_SpecialScheme
                                {
                                    SpecialSchemeId = x.SpecialSchemeId,
                                    SpecialSchemeTypeId = x.SpecialSchemeTypeId,
                                    SpecialSchemeCode = x.SpecialSchemeCode,
                                    SpecialSchemeName = x.SpecialSchemeName,
                                    UnitId = unitId,
                                    CompileMan = x.CompileMan,
                                    CompileDate = x.CompileDate,
                                    Summary = x.Summary,
                                    IsPass = null,
                                    AttachFileId = y.AttachFileId,
                                    ToKeyId = y.ToKeyId,
                                    AttachSource = y.AttachSource,
                                    AttachUrl = y.AttachUrl,
                                    ////附件转为字节传送
                                    FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                };
            poxy.DataInsertTechnique_SpecialSchemeTableAsync(specialScheme.ToList());
        }

        /// <summary>
        /// 专项方案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_SpecialSchemeTableCompleted(object sender, HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(item);
                    if (specialScheme != null)
                    {
                        specialScheme.UpState = BLL.Const.UpState_3;
                        BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【专项方案】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【专项方案】上报到集团公司失败；");
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
            if (string.IsNullOrEmpty(this.txtSpecialSchemeCode.Text.Trim()))
            {
                ShowNotify("请输入方案编号", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtSpecialSchemeName.Text.Trim()))
            {
                ShowNotify("请输入方案名称", MessageBoxIcon.Warning);
                return;
            }
            if (this.ddlUnit.SelectedValue == Const._Null)
            {
                ShowNotify("请选择单位", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.SpecialSchemeId))
            {
                SaveData(BLL.Const.UpState_1);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialScheme&menuId=3E2F2FFD-ED2E-4914-8370-D97A68398814", this.SpecialSchemeId)));
        }
        #endregion

        #region 验证方案编号、名称是否存在
        /// <summary>
        /// 验证方案编号、名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.IsPass == true && x.SpecialSchemeCode == this.txtSpecialSchemeCode.Text.Trim() && (x.SpecialSchemeId != this.SpecialSchemeId || (this.SpecialSchemeId == null && x.SpecialSchemeId != null)));
            if (q != null)
            {
                ShowNotify("输入的方案编号已存在！", MessageBoxIcon.Warning);
            }
            var q2 = Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.IsPass == true && x.SpecialSchemeName == this.txtSpecialSchemeName.Text.Trim() && (x.SpecialSchemeId != this.SpecialSchemeId || (this.SpecialSchemeId == null && x.SpecialSchemeId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的方案名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialSchemeMenuId);
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
    }
}