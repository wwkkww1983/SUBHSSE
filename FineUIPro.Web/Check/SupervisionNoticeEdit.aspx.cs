using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Check
{
    public partial class SupervisionNoticeEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SupervisionNoticeId
        {
            get
            {
                return (string)ViewState["SupervisionNoticeId"];
            }
            set
            {
                ViewState["SupervisionNoticeId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                string states = Request.Params["states"];
                if (!string.IsNullOrEmpty(states))
                {
                    this.drpIsRectify.Hidden = false;
                    this.drpCheckPerson.Hidden = false;
                }
                this.SupervisionNoticeId = Request.Params["SupervisionNoticeId"];
                var SupervisionNotice = BLL.SupervisionNoticeService.GetSupervisionNoticeById(this.SupervisionNoticeId);
                if (SupervisionNotice != null)
                {
                    this.ProjectId = SupervisionNotice.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    //隐患
                    this.txtSupervisionNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.SupervisionNoticeId);
                    if (!string.IsNullOrEmpty(SupervisionNotice.UnitId))
                    {
                        this.drpUnitId.SelectedValue = SupervisionNotice.UnitId;
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValue = SupervisionNotice.WorkAreaId;
                    }
                    if (SupervisionNotice.CheckedDate != null)
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.CheckedDate);
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.WrongContent))
                    {
                        this.txtWrongContent.Text = SupervisionNotice.WrongContent;
                    }
                    else
                    {
                        this.txtWrongContent.Text = "隐患问题及整改要求：";
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.SignPerson))
                    {
                        var user = BLL.UserService.GetUserByUserId(SupervisionNotice.SignPerson);
                        if (user != null)
                        {
                            this.txtSignPerson.Text = user.UserName;
                        }
                    }
                    if (SupervisionNotice.SignDate != null)
                    {
                        this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.SignDate);
                    }
                    if (!string.IsNullOrEmpty(SupervisionNotice.CompleteStatus))
                    {
                        this.txtCompleteStatus.Text = SupervisionNotice.CompleteStatus;
                    }
                    else
                    {
                        this.txtCompleteStatus.Text = "整改结果：";
                    }
                    this.txtDutyPerson.Text = SupervisionNotice.DutyPerson;
                    if (SupervisionNotice.CompleteDate != null)
                    {
                        this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", SupervisionNotice.CompleteDate);
                    }
                    this.drpIsRectify.SelectedValue = Convert.ToString(SupervisionNotice.IsRectify);
                    if (!string.IsNullOrEmpty(SupervisionNotice.CheckPerson))
                    {
                        this.drpCheckPerson.SelectedValue = SupervisionNotice.CheckPerson;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtSupervisionNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectSupervisionNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtSignPerson.Text = this.CurrUser.UserName;
                    this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtWrongContent.Text = "隐患描述：";
                    this.txtCompleteStatus.Text = "整改结果：";
                }
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {           
            //责任单位
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            //检查区域
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.ProjectId, true);
            //本部检查人
            BLL.UserService.InitUserDropDownList(this.drpCheckPerson, this.ProjectId, true);
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
            if (string.IsNullOrEmpty(this.SupervisionNoticeId))
            {
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择责任单位！", MessageBoxIcon.Warning);
                    return;
                }
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SupervisionNotice&menuId=0038D764-D628-46F0-94FF-D0A22C3C45A3", this.SupervisionNoticeId)));
        }
        #endregion

        #region 单位选择事件
        /// <summary>
        /// 单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnitId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                this.txtSupervisionNoticeCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectSupervisionNoticeMenuId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue);
            }
            else
            {
                this.txtSupervisionNoticeCode.Text = string.Empty;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(bool isColse)
        {
            Model.Check_SupervisionNotice SupervisionNotice = new Model.Check_SupervisionNotice
            {
                ProjectId = this.CurrUser.LoginProjectId,
                SupervisionNoticeCode = this.txtSupervisionNoticeCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                SupervisionNotice.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                SupervisionNotice.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            SupervisionNotice.CheckedDate = Funs.GetNewDateTime(this.txtCheckedDate.Text.Trim());
            SupervisionNotice.WrongContent = this.txtWrongContent.Text.Trim();
            SupervisionNotice.SignPerson = this.CurrUser.UserId;
            SupervisionNotice.SignDate = Funs.GetNewDateTime(this.txtSignDate.Text.Trim());
            SupervisionNotice.CompleteStatus = this.txtCompleteStatus.Text.Trim();
            SupervisionNotice.DutyPerson = this.txtDutyPerson.Text.Trim();
            SupervisionNotice.CompleteDate = Funs.GetNewDateTime(this.txtCompleteDate.Text.Trim());
            SupervisionNotice.IsRectify = Convert.ToBoolean(this.drpIsRectify.SelectedValue);
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                SupervisionNotice.CheckPerson = this.drpCheckPerson.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.SupervisionNoticeId))
            {
                SupervisionNotice.SupervisionNoticeId = this.SupervisionNoticeId;
                BLL.SupervisionNoticeService.UpdateSupervisionNotice(SupervisionNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, SupervisionNotice.SupervisionNoticeCode, SupervisionNotice.SupervisionNoticeId, BLL.Const.ProjectSupervisionNoticeMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.SupervisionNoticeId = SQLHelper.GetNewID(typeof(Model.Check_SupervisionNotice));
                SupervisionNotice.SupervisionNoticeId = this.SupervisionNoticeId;
                BLL.SupervisionNoticeService.AddSupervisionNotice(SupervisionNotice);
                BLL.LogService.AddSys_Log(this.CurrUser, SupervisionNotice.SupervisionNoticeCode, SupervisionNotice.SupervisionNoticeId, BLL.Const.ProjectSupervisionNoticeMenuId, BLL.Const.BtnAdd);
            }
            if (isColse)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择责任单位！", MessageBoxIcon.Warning);
                return;
            }
            SaveData(true);
        }
        #endregion
    }
}