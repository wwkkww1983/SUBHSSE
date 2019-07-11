using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SiteConstruction
{
    public partial class ConstructionDynamicEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ConstructionDynamicId
        {
            get
            {
                return (string)ViewState["ConstructionDynamicId"];
            }
            set
            {
                ViewState["ConstructionDynamicId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ///加载单位
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnit.Enabled = false;
                }

                this.ConstructionDynamicId = Request.Params["ConstructionDynamicId"];
                if (!string.IsNullOrEmpty(this.ConstructionDynamicId))
                {
                    Model.SiteConstruction_ConstructionDynamic ConstructionDynamic = BLL.ConstructionDynamicService.GetConstructionDynamicById(this.ConstructionDynamicId);
                    if (ConstructionDynamic != null)
                    {
                        this.ProjectId = ConstructionDynamic.ProjectId;
                        this.txtJobContent.Text = ConstructionDynamic.JobContent;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ConstructionDynamic.CompileDate);
                        if (!string.IsNullOrEmpty(ConstructionDynamic.UnitId))
                        {
                            this.drpUnit.SelectedValue = ConstructionDynamic.UnitId;
                        }
                        //this.txtSeeFile.Text = HttpUtility.HtmlDecode(ConstructionDynamic.SeeFile);                                               
                    }
                }
                else
                {
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectConstructionDynamicMenuId, this.ProjectId);
                    //if (codeTemplateRule != null)
                    //{
                    //    this.txtSeeFile.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    //}
                    this.txtCompileDate.MinDate = DateTime.Now;
                    this.txtJobContent.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now) +"工作安排："; 
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
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.SiteConstruction_ConstructionDynamic newConstructionDynamic = new Model.SiteConstruction_ConstructionDynamic
            {
                ProjectId = this.ProjectId
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                newConstructionDynamic.UnitId = this.drpUnit.SelectedValue;
            }
            newConstructionDynamic.CompileMan = this.CurrUser.UserId;
            newConstructionDynamic.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            newConstructionDynamic.JobContent = this.txtJobContent.Text.Trim();
            //newConstructionDynamic.SeeFile = HttpUtility.HtmlEncode(this.txtSeeFile.Text);
            if (!string.IsNullOrEmpty(this.ConstructionDynamicId))
            {
                newConstructionDynamic.ConstructionDynamicId = this.ConstructionDynamicId;
                BLL.ConstructionDynamicService.UpdateConstructionDynamic(newConstructionDynamic);
                BLL.LogService.AddSys_Log(this.CurrUser, this.drpUnit.SelectedText, newConstructionDynamic.ConstructionDynamicId, BLL.Const.ProjectConstructionDynamicMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ConstructionDynamicId = SQLHelper.GetNewID(typeof(Model.SiteConstruction_ConstructionDynamic));
                newConstructionDynamic.ConstructionDynamicId = this.ConstructionDynamicId;
                BLL.ConstructionDynamicService.AddConstructionDynamic(newConstructionDynamic);
                BLL.LogService.AddSys_Log(this.CurrUser, this.drpUnit.SelectedText, newConstructionDynamic.ConstructionDynamicId, BLL.Const.ProjectConstructionDynamicMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.ConstructionDynamicId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ConstructionDynamicAttachUrl&menuId={1}", ConstructionDynamicId,BLL.Const.ProjectConstructionDynamicMenuId)));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.drpUnit.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.txtCompileDate.Text))
            {
                var co = Funs.DB.SiteConstruction_ConstructionDynamic.FirstOrDefault(x => x.UnitId == this.drpUnit.SelectedValue && (x.CompileDate.Value.AddDays(1) > Funs.GetNewDateTime(this.txtCompileDate.Text) && x.CompileDate.Value.AddDays(-1) < Funs.GetNewDateTime(this.txtCompileDate.Text)));
                if (co != null)
                {
                    ShowNotify("该单位本日工作动态已存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    this.txtJobContent.Text = this.txtCompileDate.Text + "工作安排："; 
                }
            }
        }
    }
}