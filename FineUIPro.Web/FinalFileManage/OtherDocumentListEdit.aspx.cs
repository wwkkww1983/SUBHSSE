using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.FinalFileManage
{
    public partial class OtherDocumentListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string FileId
        {
            get
            {
                return (string)ViewState["FileId"];
            }
            set
            {
                ViewState["FileId"] = value;
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
                this.InitDropDownList();
                this.FileId = Request.Params["FileId"];
                if (!string.IsNullOrEmpty(this.FileId))
                {
                    Model.FinalFileManage_OtherDocumentList OtherDocumentList = BLL.OtherDocumentListService.GetOtherDocumentListById(this.FileId);
                    if (OtherDocumentList != null)
                    {
                        this.ProjectId = OtherDocumentList.ProjectId; 
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.FileId);
                        this.txtFileName.Text = OtherDocumentList.FileName;
                        this.txtKeyWords.Text = OtherDocumentList.KeyWords;
                        if (!string.IsNullOrEmpty(OtherDocumentList.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = OtherDocumentList.CompileMan;
                        }
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", OtherDocumentList.CompileDate);
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(OtherDocumentList.FileContent);
                    }
                }
                else
                {
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var FileCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.OtherDocumentListMenuId, this.ProjectId);
                    if (FileCodeTemplateRule != null)
                    {
                        this.txtFileContent.Text = HttpUtility.HtmlDecode(FileCodeTemplateRule.Template);
                    }

                    ////自动生成编码
                    this.txtFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.OtherDocumentListMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtFileName.Text = this.SimpleForm1.Title;
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.OtherDocumentListMenuId;
                this.ctlAuditFlow.DataId = this.FileId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

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
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.FinalFileManage_OtherDocumentList OtherDocumentList = new Model.FinalFileManage_OtherDocumentList
            {
                ProjectId = this.ProjectId,
                FileCode = this.txtFileCode.Text.Trim(),
                FileName = this.txtFileName.Text.Trim(),
                KeyWords = this.txtKeyWords.Text.Trim()
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                OtherDocumentList.CompileMan = this.drpCompileMan.SelectedValue;
            }
            OtherDocumentList.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            OtherDocumentList.FileContent = HttpUtility.HtmlEncode(this.txtFileContent.Text);
            ////单据状态
            OtherDocumentList.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                OtherDocumentList.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.FileId))
            {
                OtherDocumentList.FileId = this.FileId;
                BLL.OtherDocumentListService.UpdateOtherDocumentList(OtherDocumentList);
                BLL.LogService.AddSys_Log(this.CurrUser, OtherDocumentList.FileCode, OtherDocumentList.FileId,BLL.Const.OtherDocumentListMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.FileId = SQLHelper.GetNewID(typeof(Model.FinalFileManage_OtherDocumentList));
                OtherDocumentList.FileId = this.FileId;
                BLL.OtherDocumentListService.AddOtherDocumentList(OtherDocumentList);
                BLL.LogService.AddSys_Log(this.CurrUser, OtherDocumentList.FileCode, OtherDocumentList.FileId, BLL.Const.OtherDocumentListMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.OtherDocumentListMenuId, this.FileId, (type == BLL.Const.BtnSubmit ? true : false), OtherDocumentList.FileName, "../FinalFileManage/OtherDocumentListView.aspx?FileId={0}");
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
            if (string.IsNullOrEmpty(this.FileId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherDocumentListAttachUrl&menuId={1}", FileId, BLL.Const.OtherDocumentListMenuId)));
        }
        #endregion
    }
}