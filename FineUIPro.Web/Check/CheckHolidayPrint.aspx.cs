using System;
using System.Web;
using BLL;
using System.Collections.Generic;

namespace FineUIPro.Web.Check
{
    public partial class CheckHolidayPrint : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckHolidayId
        {
            get
            {
                return (string)ViewState["CheckHolidayId"];
            }
            set
            {
                ViewState["CheckHolidayId"] = value;
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
                this.CheckHolidayId = Request.Params["CheckHolidayId"];
                var checkHoliday = BLL.Check_CheckHolidayService.GetCheckHolidayByCheckHolidayId(this.CheckHolidayId);
                if (checkHoliday != null)
                {
                    this.txtCheckTime.Text = "检查时间：";
                    if (checkHoliday.CheckTime != null)
                    {
                        this.txtCheckTime.Text += string.Format("{0:yyyy-MM-dd}", checkHoliday.CheckTime);
                    }
                    string personStr = "检查人：";
                    if (!string.IsNullOrEmpty(checkHoliday.MainUnitPerson))
                    {
                        string[] strs = checkHoliday.MainUnitPerson.Split(',');
                        foreach (var s in strs)
                        {
                            Model.Sys_User checkPerson = BLL.UserService.GetUserByUserId(s);
                            if (checkPerson != null)
                            {
                                personStr += checkPerson.UserName + "、";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(checkHoliday.SubUnitPerson))
                    {
                        string[] strs = checkHoliday.SubUnitPerson.Split(',');
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
                    if (!string.IsNullOrEmpty(checkHoliday.PartInPersonNames))
                    {
                        if (personStr != "检查人：")
                        {
                            personStr += "、" + checkHoliday.PartInPersonNames;
                        }
                        else
                        {
                            personStr += checkHoliday.PartInPersonNames;
                        }
                    }
                    this.txtCheckPerson.Text = personStr;
                    var checkHolidayDetails = BLL.Check_CheckHolidayDetailService.GetCheckHolidayDetailByCheckHolidayId(this.CheckHolidayId);
                    int i = 1;
                    string str = "<table id='Table3' runat='server' width='100%' cellpadding='0' cellspacing='0' border='0' frame='vsides' bordercolor='#000000'>"
                                + "<tr><td align='center' style='width:5%; border: 1px solid #000000; font-size:15px; border-right: none;'>序号</td>"
                                + "<td align='center' style='width:30%; border: 1px solid #000000; font-size:15px; border-right: none;'>隐患照片或描述</td>"
                                + "<td align='center' style='width:20%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改措施</td>"
                                + "<td align='center' style='width:8%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改责任人</td>"
                                + "<td align='center' style='width:7%; border: 1px solid #000000; font-size:15px; border-right: none;'>整改时间</td>"
                                + "<td align='center' style='width:10%; border: 1px solid #000000; font-size:15px; border-right: none;'>责任单位</td>"
                                + "<td align='center' style='width:7%; border: 1px solid #000000; font-size:15px; border-right: none;'>复检人</td>"
                                + "<td align='center' style='width:7%; border: 1px solid #000000; font-size:15px; border-right: none;'>复检时间</td>"
                                + "<td align='center' style='width:7%; border: 1px solid #000000; font-size:15px; '>复检结果</td></tr>";
                    foreach (var checkHolidayDetail in checkHolidayDetails)
                    {
                        string photo1 = string.Empty;
                        string photo2 = string.Empty;
                        Model.AttachFile attachFile = BLL.AttachFileService.GetAttachFile(checkHolidayDetail.CheckHolidayDetailId, BLL.Const.ProjectCheckHolidayMenuId);
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
                        string completedDate = string.Empty;
                        //Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(checkHolidayDetail.UnitId);
                        //if (unit != null)
                        //{
                        //    unitName = unit.UnitName;
                        //}

                        str += "<tr><td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;'>" + i + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkHolidayDetail.CheckResult + "<br/>" + photo1 + "<br/>" + photo2 + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' ></td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' ></td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + completedDate + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + unitName + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' ></td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' ></td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none;' ></td></tr>";
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