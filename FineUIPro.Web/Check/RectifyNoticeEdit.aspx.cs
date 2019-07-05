using System;
using System.Web;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class RectifyNoticeEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string RectifyNoticeId
        {
            get
            {
                return (string)ViewState["RectifyNoticeId"];
            }
            set
            {
                ViewState["RectifyNoticeId"] = value;
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
                               
                this.RectifyNoticeId = Request.Params["RectifyNoticeId"];
                var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(this.RectifyNoticeId);
                if (rectifyNotice != null)
                {
                    this.ProjectId = rectifyNotice.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    //隐患
                    this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.RectifyNoticeId);
                    if (!string.IsNullOrEmpty(rectifyNotice.UnitId))
                    {
                        this.drpUnitId.SelectedValue = rectifyNotice.UnitId;
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValue = rectifyNotice.WorkAreaId;
                    }
                    if (rectifyNotice.CheckedDate != null)
                    {
                        this.txtCheckedDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CheckedDate);
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.WrongContent))
                    {
                        this.txtWrongContent.Text = rectifyNotice.WrongContent;
                    }
                    else
                    {
                        this.txtWrongContent.Text = "隐患问题及整改要求：";
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.SignPerson))
                    {
                        this.drpSignPerson.SelectedValue = rectifyNotice.SignPerson;
                    }
                    if (rectifyNotice.SignDate != null)
                    {
                        this.txtSignDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.SignDate);
                    }
                    if (!string.IsNullOrEmpty(rectifyNotice.CompleteStatus))
                    {
                        this.txtCompleteStatus.Text = rectifyNotice.CompleteStatus;
                    }
                    else
                    {
                        this.txtCompleteStatus.Text = "整改结果：";
                    }
                    this.txtDutyPerson.Text = rectifyNotice.DutyPerson;
                    if (rectifyNotice.CompleteDate != null)
                    {
                        this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", rectifyNotice.CompleteDate);
                    }
                    this.drpIsRectify.SelectedValue = Convert.ToString(rectifyNotice.IsRectify);
                    if (!string.IsNullOrEmpty(rectifyNotice.CheckPerson))
                    {
                        this.drpCheckPerson.SelectedValue = rectifyNotice.CheckPerson;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, this.ProjectId, this.CurrUser.UnitId);
                    if (this.CurrUser.UserId != BLL.Const.sysglyId)
                    {
                        this.drpSignPerson.SelectedValue = this.CurrUser.UserId;
                    }
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
            //本单位检查人
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if(thisUnit != null)
            {
                BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpCheckPerson, this.ProjectId, thisUnit.UnitId, true);
            }

            ///签发人
            BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.ProjectId, string.Empty, false);
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
            if (string.IsNullOrEmpty(this.RectifyNoticeId))
            {
                if (this.drpUnitId.SelectedValue == BLL.Const._Null)
                {
                    Alert.ShowInTop("请选择责任单位！", MessageBoxIcon.Warning);
                    return;
                }
                SaveData(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotice&menuId=0038D764-D628-46F0-94FF-D0A22C3C45A3", this.RectifyNoticeId)));
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
                this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue);
            }
            else
            {
                this.txtRectifyNoticesCode.Text = string.Empty;
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
            Model.Check_RectifyNotices rectifyNotices = new Model.Check_RectifyNotices
            {
                ProjectId = this.CurrUser.LoginProjectId,
                RectifyNoticesCode = this.txtRectifyNoticesCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                rectifyNotices.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                rectifyNotices.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            rectifyNotices.CheckedDate = Funs.GetNewDateTime(this.txtCheckedDate.Text.Trim());
            rectifyNotices.WrongContent = HttpUtility.HtmlEncode(this.txtWrongContent.Text.Trim());
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.drpSignPerson.SelectedValue))
            {
                rectifyNotices.SignPerson = this.drpSignPerson.SelectedValue;
            }
            rectifyNotices.SignDate = Funs.GetNewDateTime(this.txtSignDate.Text.Trim());
            rectifyNotices.CompleteStatus = HttpUtility.HtmlEncode(this.txtCompleteStatus.Text.Trim());
            rectifyNotices.DutyPerson = this.txtDutyPerson.Text.Trim();
            rectifyNotices.CompleteDate = Funs.GetNewDateTime(this.txtCompleteDate.Text.Trim());
            rectifyNotices.IsRectify = Convert.ToBoolean(this.drpIsRectify.SelectedValue);
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                rectifyNotices.CheckPerson = this.drpCheckPerson.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.RectifyNoticeId))
            {
                rectifyNotices.RectifyNoticesId = this.RectifyNoticeId;
                BLL.RectifyNoticesService.UpdateRectifyNotices(rectifyNotices);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改隐患整改通知单");
            }
            else
            {
                this.RectifyNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
                rectifyNotices.RectifyNoticesId = this.RectifyNoticeId;
                BLL.RectifyNoticesService.AddRectifyNotices(rectifyNotices);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加隐患整改通知单");
                ///写入工程师日志
                BLL.HSSELogService.CollectHSSELog(rectifyNotices.ProjectId, rectifyNotices.SignPerson, rectifyNotices.SignDate, "22", rectifyNotices.WrongContent, Const.BtnAdd, 1);
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