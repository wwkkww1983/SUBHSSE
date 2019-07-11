using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckColligationWHView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckColligationId
        {
            get
            {
                return (string)ViewState["CheckColligationId"];
            }
            set
            {
                ViewState["CheckColligationId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckColligationDetail> checkColligationDetails = new List<Model.View_Check_CheckColligationDetail>();
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

                this.CheckColligationId = Request.Params["CheckColligationId"];
                var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(this.CheckColligationId);
                if (checkColligation != null)
                {
                    this.txtCheckColligationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckColligationId);
                    if (checkColligation.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkColligation.CheckTime);
                    }
                    if (!string.IsNullOrEmpty(checkColligation.CheckType))
                    {
                        if (checkColligation.CheckType == "0")
                        {
                            this.txtCheckType.Text = "周检";
                        }
                        else if (checkColligation.CheckType == "1")
                        {
                            this.txtCheckType.Text = "月检";
                        }
                        else if (checkColligation.CheckType == "2")
                        {
                            this.txtCheckType.Text = "其它";
                        }
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInUnits))
                    {
                        string unitNames = string.Empty;
                        string[] unitIds = checkColligation.PartInUnits.Split(',');
                        foreach (var item in unitIds)
                        {
                            string name = BLL.UnitService.GetUnitNameByUnitId(item);
                            if (!string.IsNullOrEmpty(name))
                            {
                                unitNames += name + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            unitNames = unitNames.Substring(0, unitNames.LastIndexOf(","));
                        }
                        this.txtUnit.Text = unitNames;
                    }

                    this.txtCheckPerson.Text = BLL.UserService.GetUserNameByUserId(checkColligation.CheckPerson);

                    if (!string.IsNullOrEmpty(checkColligation.PartInPersonIds))
                    {
                        string personStr = string.Empty;
                        string[] strs = checkColligation.PartInPersonIds.Split(',');
                        foreach (var s in strs)
                        {
                            Model.Sys_User checkPerson = BLL.UserService.GetUserByUserId(s);
                            if (checkPerson != null)
                            {
                                personStr += checkPerson.UserName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(personStr))
                        {
                            personStr = personStr.Substring(0, personStr.LastIndexOf(","));
                        }
                        this.txtPartInPersons.Text = personStr;
                    }
                    this.txtPartInPersonNames.Text = checkColligation.PartInPersonNames;
                    //this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkColligation.DaySummary);
                    checkColligationDetails = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
                }
                Grid1.DataSource = checkColligationDetails;
                Grid1.DataBind();

                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckColligationWHMenuId;
                this.ctlAuditFlow.DataId = this.CheckColligationId;
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
            if (!string.IsNullOrEmpty(this.CheckColligationId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckColligation&menuId={1}&type=-1", this.CheckColligationId, BLL.Const.ProjectCheckColligationWHMenuId)));
            }
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
                return name;
            }
            return null;
        }
        #endregion
    }
}