using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class MeasuresPlanEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string MeasuresPlanId
        {
            get
            {
                return (string)ViewState["MeasuresPlanId"];
            }
            set
            {
                ViewState["MeasuresPlanId"] = value;
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
                
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, true);

                this.MeasuresPlanId = Request.Params["MeasuresPlanId"];
                if (!string.IsNullOrEmpty(this.MeasuresPlanId))
                {
                    Model.CostGoods_MeasuresPlan measuresPlan = BLL.MeasuresPlanService.GetMeasuresPlanById(this.MeasuresPlanId);
                    if (measuresPlan != null)
                    {
                        ///读取编号
                        this.txtMeasuresPlanCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.MeasuresPlanId);
                        if (!string.IsNullOrEmpty(measuresPlan.UnitId))
                        {
                            this.drpUnitId.SelectedValue = measuresPlan.UnitId;
                        }
                        if (measuresPlan.CompileDate != null)
                        {
                            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", measuresPlan.CompileDate);
                        }
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(measuresPlan.FileContents);
                    }
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectMeasuresPlanMenuId, this.CurrUser.LoginProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtFileContents.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                    ////自动生成编码
                    this.txtMeasuresPlanCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectMeasuresPlanMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                }

                if (Request.Params["value"] == "0")
                {
                    this.btnSave.Hidden = true;
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
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称！", MessageBoxIcon.Warning);
                return;
            }
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData()
        {
            Model.CostGoods_MeasuresPlan measuresPlan = new Model.CostGoods_MeasuresPlan
            {
                ProjectId = this.CurrUser.LoginProjectId,
                MeasuresPlanCode = this.txtMeasuresPlanCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                measuresPlan.UnitId = this.drpUnitId.SelectedValue;
            }
            measuresPlan.FileContents = HttpUtility.HtmlEncode(this.txtFileContents.Text);
            measuresPlan.CompileMan = this.CurrUser.UserId;
            measuresPlan.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.MeasuresPlanId))
            {
                measuresPlan.MeasuresPlanId = this.MeasuresPlanId;
                BLL.MeasuresPlanService.UpdateMeasuresPlan(measuresPlan);
                BLL.LogService.AddSys_Log(this.CurrUser, measuresPlan.MeasuresPlanCode, measuresPlan.MeasuresPlanId, BLL.Const.ProjectMeasuresPlanMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.MeasuresPlanId = SQLHelper.GetNewID(typeof(Model.CostGoods_MeasuresPlan));
                measuresPlan.MeasuresPlanId = this.MeasuresPlanId;
                BLL.MeasuresPlanService.AddMeasuresPlan(measuresPlan);
                BLL.LogService.AddSys_Log(this.CurrUser, measuresPlan.MeasuresPlanCode, measuresPlan.MeasuresPlanId, BLL.Const.ProjectMeasuresPlanMenuId, BLL.Const.BtnAdd);
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MeasuresPlanAttachUrl&type=-1", this.MeasuresPlanId, BLL.Const.ProjectMeasuresPlanMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.MeasuresPlanId))
                {
                    if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                    {
                        Alert.ShowInTop("请选择单位名称！", MessageBoxIcon.Warning);
                        return;
                    }
                    SaveData();
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MeasuresPlanAttachUrl&menuId={1}", this.MeasuresPlanId, BLL.Const.ProjectMeasuresPlanMenuId)));
            }            
        }
        #endregion
    }
}