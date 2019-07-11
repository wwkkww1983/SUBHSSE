using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayWHView :PageBase
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
                    this.txtCheckPerson.Text = BLL.UserService.GetUserNameByUserId(checkDay.CheckPerson);
                    if (checkDay.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkDay.CheckTime);
                    }
                    //this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkDay.DaySummary);
                    checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
                }
                Grid1.DataSource = checkDayDetails;
                Grid1.DataBind();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckDayWHMenuId;
                this.ctlAuditFlow.DataId = this.CheckDayId;
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
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}&type=-1", this.CheckDayId, BLL.Const.ProjectCheckDayWHMenuId)));
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 处理措施
        /// </summary>
        /// <param name="handleStep"></param>
        /// <returns></returns>
        protected string HandleStepStr(object handleStep)
        {
            if (handleStep != null)
            {
                string name = string.Empty;
                List<string> lists = handleStep.ToString().Split('|').ToList();
                if (lists.Count > 0)
                {
                    foreach (var item in lists)
                    {
                        Model.Sys_Const con = BLL.ConstValue.GetConstByConstValueAndGroupId(item, BLL.ConstValue.Group_HandleStep);
                        if (con != null)
                        {
                            name += con.ConstText + "|";
                        }
                    }
                    if (!string.IsNullOrEmpty(name))
                    {
                        name = name.Substring(0, name.LastIndexOf('|'));
                    }
                }
                return name;
            }
            return null;
        }
        #endregion
    }
}