using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InformationProject
{
    public partial class ExposureEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ExposureId
        {
            get
            {
                return (string)ViewState["ExposureId"];
            }
            set
            {
                ViewState["ExposureId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                BLL.UnitService.InitUnitNoUnitIdDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, BLL.Const.UnitId_XA, false);
                this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.Enabled = false;
                }

                this.ExposureId = Request.Params["ExposureId"];
                if (!string.IsNullOrEmpty(this.ExposureId))
                {
                    Model.InformationProject_Exposure exposure = BLL.ExposureService.GetExposureById(this.ExposureId);
                    if (exposure != null)
                    {
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            BLL.UnitService.InitUnitNoUnitIdDropDownList(this.drpUnitId, this.ProjectId, BLL.Const.UnitId_XA, false);
                               
                        }
                        if (!string.IsNullOrEmpty(exposure.UnitId))
                        {
                            this.drpUnitId.SelectedValueArray = exposure.UnitId.Split(',');
                        }  
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ExposureId);
                        if (exposure.ExposureDate != null)
                        {
                            this.txtExposureDate.Text = string.Format("{0:yyyy-MM-dd}", exposure.ExposureDate);
                        }                        
                        this.txtFileName.Text = exposure.FileName;
                        this.txtRemark.Text = exposure.Remark;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectExposureMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtExposureDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData()
        {
            Model.InformationProject_Exposure exposure = new Model.InformationProject_Exposure
            {
                ProjectId = this.ProjectId,
                ExposureCode = this.txtCode.Text.Trim(),
                ExposureDate = Funs.GetNewDateTime(this.txtExposureDate.Text.Trim())
            };
            ///授权角色
            string unitIds = string.Empty;
            string unitNames = string.Empty;
            foreach (var item in this.drpUnitId.SelectedValueArray)
            {
                var unit = BLL.UnitService.GetUnitByUnitId(item);
                if (unit != null)
                {
                    unitIds += unit.UnitId + ",";
                    unitNames += unit.UnitName + ",";
                }
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                exposure.UnitId = unitIds.Substring(0, unitIds.LastIndexOf(","));
                exposure.UnitName = unitNames.Substring(0, unitNames.LastIndexOf(","));
            }

            exposure.FileName = this.txtFileName.Text.Trim();
            exposure.Remark = this.txtRemark.Text.Trim();
            exposure.CompileMan = this.CurrUser.UserId;
            exposure.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.ExposureId))
            {
                exposure.ExposureId = this.ExposureId;
                BLL.ExposureService.UpdateExposure(exposure);
                BLL.LogService.AddSys_Log(this.CurrUser, exposure.ExposureCode, exposure.ExposureId,BLL.Const.ProjectExposureMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.ExposureId = SQLHelper.GetNewID(typeof(Model.InformationProject_Exposure));
                exposure.ExposureId = this.ExposureId;
                BLL.ExposureService.AddExposure(exposure);
                BLL.LogService.AddSys_Log(this.CurrUser, exposure.ExposureCode, exposure.ExposureId, BLL.Const.ProjectExposureMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.ExposureId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ExposureAttachUrl&menuId={1}", this.ExposureId, BLL.Const.ProjectExposureMenuId)));
        }
        #endregion
    }
}