using System;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class RegistrationRecordView : PageBase
    {
        #region 页面加载时
        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!string.IsNullOrEmpty(Request.Params["type"]))
                //{
                //    this.btnSave.Hidden = true;
                //}
                LoadData();
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RegistrationRecordMenuId);
                if (buttonList.Count() > 0)
                {
                    if (buttonList.Contains(BLL.Const.BtnSave))
                    {
                        this.btnSave.Hidden = false;
                    }
                }
                string registrationRecordId = Request.Params["RegistrationRecordId"];
                Model.Inspection_RegistrationRecord record = BLL.RegistrationRecordService.GetRegisterRecordByRegisterRecordId(registrationRecordId);
                if (record != null)
                {
                    if (record.CheckDate != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", record.CheckDate);
                    }
                    if (!string.IsNullOrEmpty(record.CheckPerson))
                    {
                        Model.Sys_User user = BLL.UserService.GetUserByUserId(record.CheckPerson);
                        if (user != null)
                        {
                            this.txtCheckMan.Text = user.UserName;
                        }
                    }
                    BindGrid(record.CheckPerson, record.CheckDate.Value.Date);
                }
                else
                {
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            //BindGrid();
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(string checkPerson, DateTime? checkDate)
        {
            this.Grid1.DataSource = from x in Funs.DB.View_Inspection_Registration
                                    where x.ProjectId == this.CurrUser.LoginProjectId
                                    && x.CheckManId == checkPerson && x.CheckTime.Value.Date == checkDate
                                    orderby x.CheckTime, x.ResponsibilityUnitName, x.WorkAreaName
                                    select x;
            this.Grid1.DataBind();
        }


        #endregion

        #region Grid双击事件
        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RegistrationView.aspx?RegistrationId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            Model.Inspection_RegistrationRecord record = BLL.RegistrationRecordService.GetRegisterRecordByRegisterRecordId(Request.Params["RegistrationRecordId"]);
            if (record != null)
            {
                string registrationIds = string.Empty;
                for (int i = 0; i < this.Grid1.Rows.Count; i++)
                {
                    registrationIds += this.Grid1.Rows[i].RowID + ",";
                }
                if (!string.IsNullOrEmpty(registrationIds))
                {
                    registrationIds = registrationIds.Substring(0, registrationIds.LastIndexOf(","));
                }
                record.RegistrationIds = registrationIds;
                record.CompileMan = this.CurrUser.UserId;
                record.CompileDate = DateTime.Now;
                BLL.RegistrationRecordService.UpdateRegisterRecord(record);
            }
            ShowNotify("保存成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //BindGrid();
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.RegistrationService.GetRegistrationById(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.RegistrationService.GetRegistrationById(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.ImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改前图片
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrl(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.RegistrationService.GetRegistrationById(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }

        /// <summary>
        /// 获取整改后图片(放于Img中)
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImgUrlByImage(object registrationId)
        {
            string url = string.Empty;
            if (registrationId != null)
            {
                var registration = BLL.RegistrationService.GetRegistrationById(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }
        #endregion
    }
}