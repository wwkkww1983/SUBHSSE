using BLL;
using System;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class HiddenRectificationRecordEdit : PageBase
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                string hazardRegisterRecordId = Request.Params["HazardRegisterRecordId"];
                this.GetButtonPower();
                Model.HSSE_Hazard_HazardRegisterRecord record = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByHazardRegisterRecordId(hazardRegisterRecordId);
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
            this.Grid1.DataSource = from x in Funs.DB.View_Hazard_HazardRegister
                                    where x.ProblemTypes == "1" && x.ProjectId == this.CurrUser.LoginProjectId
                                    && x.CheckManId == checkPerson && x.CheckTime.Value.Date == checkDate
                                    orderby x.CheckTime, x.ResponsibleUnit, x.Place
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationView.aspx?HazardRegisterId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
            //if (this.drpCheckMan.SelectedValue == BLL.Const._Null)
            //{
            //    ShowNotify("请选择巡检人！", MessageBoxIcon.Warning);
            //    return;
            //}
            //Model.HSSE_Hazard_HazardRegisterRecord oldRecord = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByCheckManAndDate(this.drpCheckMan.SelectedValue,Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()));
            //if (oldRecord != null)
            //{
            //    ShowNotify("该巡检人当天巡检记录已存在！", MessageBoxIcon.Warning);
            //    return;
            //}
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnSave))
            {
                Model.HSSE_Hazard_HazardRegisterRecord record = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByHazardRegisterRecordId(Request.Params["HazardRegisterRecordId"]);
                if (record != null)
                {
                    string hazardRegisterIds = string.Empty;
                    for (int i = 0; i < this.Grid1.Rows.Count; i++)
                    {
                        hazardRegisterIds += this.Grid1.Rows[i].RowID + ",";
                    }
                    if (!string.IsNullOrEmpty(hazardRegisterIds))
                    {
                        hazardRegisterIds = hazardRegisterIds.Substring(0, hazardRegisterIds.LastIndexOf(","));
                    }
                    record.HazardRegisterIds = hazardRegisterIds;
                    record.CheckType = "1";   //安全
                    record.CompileMan = this.CurrUser.UserId;
                    record.CompileDate = DateTime.Now;
                    BLL.HSSE_Hazard_HazardRegisterRecordService.UpdateHazardRegisterRecord(record);
                }
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
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
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
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
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
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
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
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
                var registration = BLL.HSSE_Hazard_HazardRegisterService.GetHazardRegisterByHazardRegisterId(registrationId.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowImage("../", registration.RectificationImageUrl);
                }
            }
            return url;
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId);
            if (buttonList.Count() > 0)
            {                
                if (buttonList.Contains(Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}