using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.QualityAudit
{
    public partial class ProjectRecordEdit :PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ProjectRecordId
        {
            get
            {
                return (string)ViewState["ProjectRecordId"];
            }
            set
            {
                ViewState["ProjectRecordId"] = value;
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
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, false);
                this.ProjectRecordId = Request.Params["ProjectRecordId"];
                if (!string.IsNullOrEmpty(this.ProjectRecordId))
                {
                    Model.QualityAudit_ProjectRecord projectRecord = BLL.ProjectRecordService.GetProjectRecordById(this.ProjectRecordId);
                    if (projectRecord!=null)
                    {
                        this.txtProjectRecordCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ProjectRecordId);
                        if (!string.IsNullOrEmpty(projectRecord.UnitId))
                        {
                            //this.drpUnitId.SelectedValue = projectRecord.UnitId;
                            this.drpUnitId.SelectedValueArray = projectRecord.UnitId.Split(',');
                        }
                        this.txtProjectRecordName.Text = projectRecord.ProjectRecordName;
                        this.txtRemark.Text = projectRecord.Remark;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtProjectRecordCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRecordMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
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
            if (string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            SaveData(true);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(bool isClose)
        {
            Model.QualityAudit_ProjectRecord projectRecord = new Model.QualityAudit_ProjectRecord
            {
                ProjectId = this.CurrUser.LoginProjectId,
                ProjectRecordCode = this.txtProjectRecordCode.Text.Trim(),
                ProjectRecordName = this.txtProjectRecordName.Text.Trim(),
                Remark = this.txtRemark.Text.Trim(),
                CompileMan = this.CurrUser.UserId,
                CompileDate = DateTime.Now,
            };

            if (!string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
            {
                string units = string.Empty;
                var unitIds = this.drpUnitId.SelectedValueArray;
                foreach (var item in unitIds)
                {
                    units += item + ",";
                }

                projectRecord.UnitId = units;
            }
            
            if (!string.IsNullOrEmpty(this.ProjectRecordId))
            {
                projectRecord.ProjectRecordId = this.ProjectRecordId;
                BLL.ProjectRecordService.UpdateProjectRecord(projectRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, projectRecord.ProjectRecordCode, projectRecord.ProjectRecordId,BLL.Const.ProjectRecordMenuId,BLL.Const.BtnModify );
            }
            else
            {
                this.ProjectRecordId = BLL.SQLHelper.GetNewID(typeof(Model.QualityAudit_ProjectRecord));
                projectRecord.ProjectRecordId = this.ProjectRecordId;
                BLL.ProjectRecordService.AddProjectRecord(projectRecord);
                BLL.LogService.AddSys_Log(this.CurrUser, projectRecord.ProjectRecordCode, projectRecord.ProjectRecordId, BLL.Const.ProjectRecordMenuId, BLL.Const.BtnAdd);
                ////判断单据是否 加入到企业管理资料
                var safeData = BLL.Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == BLL.Const.ProjectRecordMenuId);
                if (safeData != null)
                {
                    BLL.SafetyDataService.AddSafetyData(BLL.Const.ProjectRecordMenuId, this.ProjectRecordId, this.txtProjectRecordName.Text, "../QualityAudit/ProjectRecordEdit.aspx?ProjectRecordId={0}", projectRecord.ProjectId);
                }
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectRecordAttachUrl&type=-1", this.ProjectRecordId, BLL.Const.ProjectRecordMenuId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.drpUnitId.SelectedValue))
                {
                    Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.ProjectRecordId))
                {
                    SaveData(false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ProjectRecordAttachUrl&menuId={1}", this.ProjectRecordId, BLL.Const.ProjectRecordMenuId)));
            }
        }
        #endregion
    }
}