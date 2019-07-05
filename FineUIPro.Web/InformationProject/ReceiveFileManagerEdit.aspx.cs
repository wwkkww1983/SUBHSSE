﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ReceiveFileManagerEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string ReceiveFileManagerId
        {
            get
            {
                return (string)ViewState["ReceiveFileManagerId"];
            }
            set
            {
                ViewState["ReceiveFileManagerId"] = value;
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
                this.ReceiveFileManagerId = Request.Params["ReceiveFileManagerId"];
                if (!string.IsNullOrEmpty(this.ReceiveFileManagerId))
                {
                    Model.InformationProject_ReceiveFileManager ReceiveFileManager = BLL.ReceiveFileManagerService.GetReceiveFileManagerById(this.ReceiveFileManagerId);
                    if (ReceiveFileManager != null)
                    {
                        this.ProjectId = ReceiveFileManager.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtReceiveFileCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ReceiveFileManagerId);
                        this.txtReceiveFileName.Text = ReceiveFileManager.ReceiveFileName;                       
                        if (!string.IsNullOrEmpty(ReceiveFileManager.FileUnitId))
                        {
                            this.drpUnit.SelectedValue = ReceiveFileManager.FileUnitId;
                        }
                       
                        this.txtGetFileDate.Text = string.Format("{0:yyyy-MM-dd}", ReceiveFileManager.GetFileDate);                       
                        this.txtFileCode.Text = ReceiveFileManager.FileCode;
                        if (ReceiveFileManager.FilePageNum.HasValue)
                        {
                            this.txtFilePageNum.Text = ReceiveFileManager.FilePageNum.ToString();
                        }
                        this.txtVersion.Text = ReceiveFileManager.Version;
                        if (!string.IsNullOrEmpty(ReceiveFileManager.SendPersonId))
                        {
                            this.drpSendPerson.SelectedValue = ReceiveFileManager.SendPersonId;
                        }
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(ReceiveFileManager.MainContent);
                        if (!string.IsNullOrEmpty(ReceiveFileManager.UnitIds))
                        {
                            this.drpUnitIds.SelectedValueArray = ReceiveFileManager.UnitIds.Split(',');
                        }
                    }
                }
                else
                {
                    this.drpSendPerson.SelectedValue = this.CurrUser.UserId;
                    this.txtGetFileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var ReceiveFileCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ReceiveFileManagerMenuId, this.ProjectId);
                    if (ReceiveFileCodeTemplateRule != null)
                    {
                        this.txtMainContent.Text = HttpUtility.HtmlDecode(ReceiveFileCodeTemplateRule.Template);
                    }                   
                    this.drpSendPerson.SelectedValue = this.CurrUser.UserId;
                    ////自动生成编码
                    this.txtReceiveFileCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId (BLL.Const.ReceiveFileManagerMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtReceiveFileName.Text = this.SimpleForm1.Title;
                    this.txtVersion.Text = "V1.0";
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ReceiveFileManagerMenuId;
                this.ctlAuditFlow.DataId = this.ReceiveFileManagerId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpSendPerson.DataValueField = "UserId";
            this.drpSendPerson.DataTextField = "UserName";
            this.drpSendPerson.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.ProjectId);
            this.drpSendPerson.DataBind();
            Funs.FineUIPleaseSelect(this.drpSendPerson);

            this.drpUnit.DataValueField = "UnitId";
            this.drpUnit.DataTextField = "UnitName";
            this.drpUnit.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnit.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnit);

            this.drpUnitIds.DataValueField = "UnitId";
            this.drpUnitIds.DataTextField = "UnitName";
            this.drpUnitIds.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitIds.DataBind();
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
            Model.InformationProject_ReceiveFileManager ReceiveFileManager = new Model.InformationProject_ReceiveFileManager
            {
                ProjectId = this.ProjectId,
                ReceiveFileCode = this.txtReceiveFileCode.Text.Trim(),
                ReceiveFileName = this.txtReceiveFileName.Text.Trim()
            };
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                ReceiveFileManager.FileUnitId = this.drpUnit.SelectedValue;
            }

            ReceiveFileManager.GetFileDate = Funs.GetNewDateTime(this.txtGetFileDate.Text.Trim());
            
            ReceiveFileManager.FileCode = this.txtFileCode.Text.Trim();
            ReceiveFileManager.FilePageNum =Funs.GetNewInt(this.txtFilePageNum.Text.Trim());
            ReceiveFileManager.Version = this.txtVersion.Text.Trim();
            if (this.drpSendPerson.SelectedValue != BLL.Const._Null)
            {
                ReceiveFileManager.SendPersonId = this.drpSendPerson.SelectedValue;
            }
            ReceiveFileManager.MainContent = HttpUtility.HtmlEncode(this.txtMainContent.Text);
            //接收单位
            if (this.drpUnitIds.SelectedValueArray.Count() > 0)
            {
                string unitIds = string.Empty;
                foreach (var item in this.drpUnitIds.SelectedValueArray)
                {
                    unitIds += item + ",";
                }
                if (!string.IsNullOrEmpty(unitIds))
                {
                    unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
                }
                ReceiveFileManager.UnitIds = unitIds;
            }
            ////单据状态
            ReceiveFileManager.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                ReceiveFileManager.States = this.ctlAuditFlow.NextStep;
            }

            if (!string.IsNullOrEmpty(this.ReceiveFileManagerId))
            {
                ReceiveFileManager.ReceiveFileManagerId = this.ReceiveFileManagerId;
                BLL.ReceiveFileManagerService.UpdateReceiveFileManager(ReceiveFileManager);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改一般来文管理", ReceiveFileManager.ReceiveFileManagerId);
            }
            else
            {
                this.ReceiveFileManagerId = SQLHelper.GetNewID(typeof(Model.InformationProject_ReceiveFileManager));
                ReceiveFileManager.ReceiveFileManagerId = this.ReceiveFileManagerId;
                BLL.ReceiveFileManagerService.AddReceiveFileManager(ReceiveFileManager);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加一般来文管理", ReceiveFileManager.ReceiveFileManagerId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ReceiveFileManagerMenuId, this.ReceiveFileManagerId, (type == BLL.Const.BtnSubmit ? true : false), ReceiveFileManager.ReceiveFileName, "../InformationProject/ReceiveFileManagerView.aspx?ReceiveFileManagerId={0}");
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
            if (string.IsNullOrEmpty(this.ReceiveFileManagerId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ReceiveFileManagerAttachUrl&menuId={1}", ReceiveFileManagerId,BLL.Const.ReceiveFileManagerMenuId)));
        }
        #endregion

        #region 接收单位 全选/全不选事件
        /// <summary>
        /// 接收单位全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectALL_Click(object sender, EventArgs e)
        {
            string value = string.Empty;
            foreach (var item in this.drpUnitIds.Items)
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = item.Value;
                }
                else
                {
                    value += "," + item.Value; ;
                }
            }
            if (!string.IsNullOrEmpty(value))
            {
                this.drpUnitIds.SelectedValueArray = value.Split(',');
            }
        }

        /// <summary>
        /// 接收单位全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelectNoALL_Click(object sender, EventArgs e)
        {
            this.drpUnitIds.SelectedValueArray = null;
        }
        #endregion
    }
}