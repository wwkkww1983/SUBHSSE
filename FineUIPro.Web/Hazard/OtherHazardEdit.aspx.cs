using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Hazard
{
    public partial class OtherHazardEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string OtherHazardId
        {
            get
            {
                return (string)ViewState["OtherHazardId"];
            }
            set
            {
                ViewState["OtherHazardId"] = value;
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.OtherHazardId = Request.Params["OtherHazardId"];
                var otherHazard = BLL.Hazard_OtherHazardService.GetOtherHazardByOtherHazardId(this.OtherHazardId);
                if (otherHazard != null)
                {
                    this.ProjectId = otherHazard.ProjectId;
                    this.txtOtherHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.OtherHazardId);
                    this.txtOtherHazardName.Text = otherHazard.OtherHazardName;
                    this.txtCompileMan.Text = BLL.UserService.GetUserNameByUserId(otherHazard.CompileMan);
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", otherHazard.CompileDate);
                }
                else
                {
                    ////自动生成编码
                    this.txtOtherHazardCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.OtherHazardMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCompileMan.Text = this.CurrUser.UserName;
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.OtherHazardMenuId;
                this.ctlAuditFlow.DataId = this.OtherHazardId; 
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 提交按钮
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
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Hazard_OtherHazard otherHazard = new Model.Hazard_OtherHazard
            {
                OtherHazardCode = this.txtOtherHazardCode.Text.Trim(),
                OtherHazardName = this.txtOtherHazardName.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim()),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0
            };
            if (type == BLL.Const.BtnSubmit)
            {
                otherHazard.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.OtherHazardId))
            {
                otherHazard.OtherHazardId = this.OtherHazardId;
                BLL.Hazard_OtherHazardService.UpdateOtherHazard(otherHazard);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改其他危险源辨识文件", otherHazard.OtherHazardId);
            }
            else
            {
                otherHazard.OtherHazardId = SQLHelper.GetNewID(typeof(Model.Hazard_OtherHazard));
                otherHazard.CompileMan = this.CurrUser.UserId;
                this.OtherHazardId = otherHazard.OtherHazardId;
                BLL.Hazard_OtherHazardService.AddOtherHazard(otherHazard);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加其他危险源辨识文件", otherHazard.OtherHazardId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.OtherHazardMenuId, this.OtherHazardId, (type == BLL.Const.BtnSubmit ? true : false), otherHazard.OtherHazardName, "../Hazard/OtherHazardView.aspx?OtherHazardId={0}");
        }

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.OtherHazardId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/OtherHazard&menuId={1}", this.OtherHazardId, BLL.Const.OtherHazardMenuId)));
        }
        #endregion

        private void SaveNew()
        {
            Model.Hazard_OtherHazard otherHazard = new Model.Hazard_OtherHazard
            {
                OtherHazardCode = this.txtOtherHazardCode.Text.Trim(),
                OtherHazardName = this.txtOtherHazardName.Text.Trim(),
                CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim()),
                ProjectId = this.ProjectId,
                ////单据状态
                States = BLL.Const.State_0,
                OtherHazardId = SQLHelper.GetNewID(typeof(Model.Hazard_OtherHazard)),
                CompileMan = this.CurrUser.UserId
            };
            this.OtherHazardId = otherHazard.OtherHazardId;
            BLL.Hazard_OtherHazardService.AddOtherHazard(otherHazard);
            BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加其他危险源辨识文件", otherHazard.OtherHazardId);
        }
    }
}