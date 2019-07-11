using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckDayId
        {
            get
            {
                return (string)ViewState["CheckDayId"];
            }
            set
            {
                ViewState["CheckDayId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckDayDetail> checkDayDetails = new List<Model.View_Check_CheckDayDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                checkDayDetails.Clear();

                this.CheckDayId = Request.Params["CheckDayId"];
                var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckDayId);
                    if (!string.IsNullOrEmpty(checkDay.WeatherId))
                    {
                        this.txtWeather.Text = BLL.ConstValue.drpConstItemList(ConstValue.Group_Weather).FirstOrDefault(x => x.ConstValue == checkDay.WeatherId).ConstText;
                    }
                    this.txtCheckPerson.Text =BLL.UserService.GetUserNameByUserId(checkDay.CheckPerson);
                    if (checkDay.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkDay.CheckTime);
                    }
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkDay.DaySummary);
                    checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
                }
                Grid1.DataSource = checkDayDetails;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckDayMenuId;
                this.ctlAuditFlow.DataId = this.CheckDayId;
            }
        }
        #endregion

        #region 生成隐患整改通知单按钮
        /// <summary>
        /// 生成隐患整改通知单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateRectifyNotice_Click(object sender, EventArgs e)
        {
            if (this.Grid1.Rows.Count == 0)
            {
                Alert.ShowInTop("请先增加检查记录！", MessageBoxIcon.Warning);
                return;
            }
            int n = 0;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                if (this.Grid1.Rows[i].Values[7].ToString() == "隐患整改通知单")
                {
                    n++;
                    break;
                }
            }
            if (n == 0)
            {
                Alert.ShowInTop("没有需要生成隐患整改通知单的检查项！", MessageBoxIcon.Warning);
                return;
            }
            string unitIds = string.Empty;
            string rectifyNoticeAndUnitIds = string.Empty;
            string rectifyNoticeCode = string.Empty;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                if (this.Grid1.Rows[i].Values[7].ToString() == "隐患整改通知单")
                {
                    Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(this.Grid1.Rows[i].DataKeys[0].ToString());
                    if (string.IsNullOrEmpty(detail.RectifyNoticeId))
                    {
                        string unitId = this.Grid1.Rows[i].Values[10].ToString();
                        if (!unitIds.Contains(unitId))
                        {
                            Model.Check_RectifyNotice rectifyNotice = new Model.Check_RectifyNotice
                            {
                                RectifyNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotice)),
                                ProjectId = this.CurrUser.LoginProjectId,
                                UnitId = unitId,
                                RectifyNoticeCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, this.CurrUser.LoginProjectId, unitId)
                            };
                            rectifyNotice.CheckArea += this.Grid1.Rows[i].Values[11].ToString();
                            BLL.Check_RectifyNoticeService.AddRectifyNotice(rectifyNotice);

                            Model.Check_RectifyNoticeDetail d = new Model.Check_RectifyNoticeDetail
                            {
                                RectifyNoticeDetailId = detail.CheckDayDetailId,
                                RectifyNoticeId = rectifyNotice.RectifyNoticeId,
                                CheckItem = detail.CheckItem,
                                CheckItemType = detail.CheckItemType,
                                Unqualified = detail.Unqualified,
                                CheckArea = detail.CheckArea,
                                UnitId = detail.UnitId,
                                Suggestions = detail.Suggestions,
                                CheckContent = detail.CheckContent
                            };
                            BLL.Check_RectifyNoticeDetailService.AddRectifyNoticeDetail(d);
                            unitIds += unitId + ",";
                            rectifyNoticeAndUnitIds += rectifyNotice.RectifyNoticeId + "," + unitId + "|";
                            detail.RectifyNoticeId = rectifyNotice.RectifyNoticeId;
                            BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
                            if (string.IsNullOrEmpty(rectifyNoticeCode))
                            {
                                rectifyNoticeCode += rectifyNotice.RectifyNoticeCode;
                            }
                            else
                            {
                                rectifyNoticeCode += "," + rectifyNotice.RectifyNoticeCode;
                            }
                        }
                        else
                        {
                            string[] list = rectifyNoticeAndUnitIds.Split('|');
                            foreach (var item in list)
                            {
                                if (item.Contains(unitId))
                                {
                                    string rectifyNoticeId = item.Split(',')[0];
                                    Model.Check_RectifyNotice rectifyNotice = BLL.Check_RectifyNoticeService.GetRectifyNoticeByRectifyNoticeId(rectifyNoticeId);
                                    if (!rectifyNotice.CheckArea.Contains(this.Grid1.Rows[i].Values[11].ToString()))
                                    {
                                        rectifyNotice.CheckArea += "," + this.Grid1.Rows[i].Values[11].ToString();
                                    }
                                    BLL.Check_RectifyNoticeService.UpdateRectifyNotice(rectifyNotice);
                                    Model.Check_RectifyNoticeDetail d = new Model.Check_RectifyNoticeDetail
                                    {
                                        RectifyNoticeDetailId = detail.CheckDayDetailId,
                                        RectifyNoticeId = rectifyNoticeId,
                                        CheckItem = detail.CheckItem,
                                        CheckItemType = detail.CheckItemType,
                                        Unqualified = detail.Unqualified,
                                        CheckArea = detail.CheckArea,
                                        UnitId = detail.UnitId,
                                        Suggestions = detail.Suggestions,
                                        CheckContent = detail.CheckContent
                                    };
                                    BLL.Check_RectifyNoticeDetailService.AddRectifyNoticeDetail(d);
                                    detail.RectifyNoticeId = rectifyNoticeId;
                                    BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(rectifyNoticeCode))
            {
                Alert.ShowInTop("已生成隐患整改通知单：" + rectifyNoticeCode + "！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("隐患整改通知单已存在，请到对应模块进行处理！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}&type=-1", this.CheckDayId, BLL.Const.ProjectCheckDayMenuId)));
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换整改完成情况
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertCompleteStatus(object CompleteStatus)
        {
            if (CompleteStatus != null)
            {
                if (!string.IsNullOrEmpty(CompleteStatus.ToString()))
                {
                    bool completeStatus = Convert.ToBoolean(CompleteStatus.ToString());
                    if (completeStatus)
                    {
                        return "是";
                    }
                    else
                    {
                        return "否";
                    }
                }
            }
            return "";
        }
        #endregion
    }
}