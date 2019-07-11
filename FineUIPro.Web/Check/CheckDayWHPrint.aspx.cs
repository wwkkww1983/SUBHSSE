using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayWHPrint : PageBase
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
                this.CheckDayId = Request.Params["CheckDayId"];
                var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.txtCheckTime.Text = "检查时间：";
                    if (checkDay.CheckTime != null)
                    {
                        this.txtCheckTime.Text += string.Format("{0:yyyy-MM-dd}", checkDay.CheckTime);
                    }
                    this.txtCheckPerson.Text = "检查人：";
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(checkDay.CheckPerson);
                    if (user != null)
                    {
                        this.txtCheckPerson.Text += user.UserName;
                    }
                    var checkDayDetails = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayId(this.CheckDayId);
                    int i = 1;
                    string str = "<table id='Table3' runat='server' width='100%' cellpadding='0' cellspacing='0' border='0' frame='vsides' bordercolor='#000000'>"
                                + "<tr><td align='center' style='width:4%; border: 1px solid #000000; font-size:15px; border-right: none;'>序号</td>"
                                + "<td align='center' style='width:13%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患内容</td>"
                                + "<td align='center' style='width:10%; border: 1px solid #000000; font-size:15px; border-right: none;'>检查区域</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患类型</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患级别</td>"
                                + "<td align='center' style='width:14%; border: 1px solid #000000; font-size:15px; border-right: none;'>责任单位</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>责任人</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改限时</td>"
                                + "<td align='center' style='width:14%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改要求</td>"
                                + "<td align='center' style='width:13%; border: 1px solid #000000; font-size:15px; border-right: none;'>处理措施</td>";
                    foreach (var checkDayDetail in checkDayDetails)
                    {
                        string photo1 = string.Empty;
                        string photo2 = string.Empty;
                        Model.AttachFile attachFile = BLL.AttachFileService.GetAttachFile(checkDayDetail.CheckDayDetailId, BLL.Const.ProjectCheckDayWHMenuId);
                        if (attachFile != null)
                        {
                            List<string> urls = new List<string>();
                            string[] lists = attachFile.AttachUrl.Split(',');
                            foreach (var list in lists)
                            {
                                if (!string.IsNullOrEmpty(list))
                                {
                                    urls.Add(list);
                                }
                            }
                            if (urls.Count > 1)   //两个附件
                            {
                                photo1 = "<img alt='' runat='server' id='img111' width='180' height='180' src='" + "../" + urls[0] + "' />";
                                photo2 = "<img alt='' runat='server' id='img111' width='180' height='180' src='" + "../" + urls[1] + "' />";
                            }
                            else
                            {
                                photo1 = "<img alt='' runat='server' id='img111' width='180' height='180' src='" + "../" + urls[0] + "' />";
                            }
                        }
                        string unitName = string.Empty;
                        string personName = string.Empty;
                        string handleStepName = string.Empty;
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(checkDayDetail.UnitId);
                        if (unit != null)
                        {
                            unitName = unit.UnitName;
                        }
                        if (!string.IsNullOrEmpty(checkDayDetail.PersonId))
                        {
                            Model.SitePerson_Person person = BLL.PersonService.GetPersonById(checkDayDetail.PersonId);
                            if (person != null)
                            {
                                personName = person.PersonName;
                            }
                        }
                        if (!string.IsNullOrEmpty(checkDayDetail.HandleStep))
                        {
                            List<string> lists = checkDayDetail.HandleStep.Split('|').ToList();
                            if (lists.Count > 0)
                            {
                                foreach (var item in lists)
                                {
                                    Model.Sys_Const con = BLL.ConstValue.GetConstByConstValueAndGroupId(item, BLL.ConstValue.Group_HandleStep);
                                    if (con != null)
                                    {
                                        handleStepName += con.ConstText + "|";
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(handleStepName))
                            {
                                handleStepName = handleStepName.Substring(0, handleStepName.LastIndexOf("|"));
                            }
                        }
                        str += "<tr><td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;'>" + i + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkDayDetail.Unqualified + "<br/>" + photo1 + "<br/>" + photo2 + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkDayDetail.WorkArea + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkDayDetail.HiddenDangerType + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkDayDetail.HiddenDangerLevel + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + unitName + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + personName + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + string.Format("{0:yyyy-MM-dd}", checkDayDetail.LimitedDate) + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkDayDetail.Suggestions + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + handleStepName + "</td></tr>";
                        i++;
                    }
                    str += "</table>";
                    this.div3.InnerHtml = str;
                }
            }
        }
        #endregion
    }
}