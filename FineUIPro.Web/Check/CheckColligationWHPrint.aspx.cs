using System;
using System.Collections.Generic;
using System.Linq;

namespace FineUIPro.Web.Check
{
    public partial class CheckColligationWHPrint : PageBase
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
                this.CheckColligationId = Request.Params["CheckColligationId"];
                var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(this.CheckColligationId);
                if (checkColligation != null)
                {
                    this.txtCheckTime.Text = "检查时间：";
                    if (checkColligation.CheckTime != null)
                    {
                        this.txtCheckTime.Text += string.Format("{0:yyyy-MM-dd}", checkColligation.CheckTime);
                    }
                    string personStr = "检查人：";
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(checkColligation.CheckPerson);
                    if (user != null)
                    {
                        personStr += user.UserName + "、";
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInPersonIds))
                    {
                        string[] strs = checkColligation.PartInPersonIds.Split(',');
                        foreach (var s in strs)
                        {
                            Model.Sys_User checkPerson = BLL.UserService.GetUserByUserId(s);
                            if (checkPerson != null)
                            {
                                personStr += checkPerson.UserName + "、";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(personStr))
                    {
                        personStr = personStr.Substring(0, personStr.LastIndexOf("、"));
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInPersonNames))
                    {
                        if (personStr != "检查人：")
                        {
                            personStr += "、" + checkColligation.PartInPersonNames;
                        }
                        else
                        {
                            personStr += checkColligation.PartInPersonNames;
                        }
                    }
                    this.txtCheckPerson.Text = personStr;
                    this.txtCheckType.Text = "检查项目：";
                    if (checkColligation.CheckType == "0")
                    {
                        this.txtCheckType.Text += "周检";
                    }
                    else if (checkColligation.CheckType == "1")
                    {
                        this.txtCheckType.Text += "月检";
                    }
                    else if (checkColligation.CheckType == "2")
                    {
                        this.txtCheckType.Text += "其它";
                    }
                    var checkColligationDetails = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationId(this.CheckColligationId);
                    int i = 1;
                    string str = "<table id='Table3' runat='server' width='100%' cellpadding='0' cellspacing='0' border='0' frame='vsides' bordercolor='#000000'>"
                                + "<tr><td align='center' style='width:5%; border: 1px solid #000000; font-size:15px; border-right: none;'>序号</td>"
                                + "<td align='center' style='width:15%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患内容</td>"
                                + "<td align='center' style='width:10%; border: 1px solid #000000; font-size:15px; border-right: none;'>检查区域</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患类型</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患级别</td>"
                                + "<td align='center' style='width:12%; border: 1px solid #000000; font-size:15px; border-right: none;'>责任单位</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>责任人</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改限时</td>"
                                + "<td align='center' style='width:12%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改要求</td>"
                                + "<td align='center' style='width:14%; border: 1px solid #000000; font-size:15px; '>处理措施</td></tr>";
                    foreach (var checkColligationDetail in checkColligationDetails)
                    {
                        string photo1 = string.Empty;
                        string photo2 = string.Empty;
                        Model.AttachFile attachFile = BLL.AttachFileService.GetAttachFile(checkColligationDetail.CheckColligationDetailId, BLL.Const.ProjectCheckColligationWHMenuId);
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
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(checkColligationDetail.UnitId);
                        if (unit != null)
                        {
                            unitName = unit.UnitName;
                        }
                        if (!string.IsNullOrEmpty(checkColligationDetail.PersonId))
                        {
                            var person = BLL.PersonService.GetPersonById(checkColligationDetail.PersonId);
                            if (person != null)
                            {
                                personName = person.PersonName;
                            }
                        }
                        if (!string.IsNullOrEmpty(checkColligationDetail.HandleStep))
                        {
                            List<string> lists = checkColligationDetail.HandleStep.Split('|').ToList();
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
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkColligationDetail.Unqualified + "<br/>" + photo1 + "<br/>" + photo2 + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkColligationDetail.WorkArea + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkColligationDetail.HiddenDangerType + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkColligationDetail.HiddenDangerLevel + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + unitName + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + personName + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + string.Format("{0:yyyy-MM-dd}", checkColligationDetail.LimitedDate) + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkColligationDetail.Suggestions + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none;' >" + handleStepName + "</td></tr>";
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