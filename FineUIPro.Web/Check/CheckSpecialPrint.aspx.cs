using System;
using System.Web;
using BLL;
using System.Collections.Generic;

namespace FineUIPro.Web.Check
{
    public partial class CheckSpecialPrint : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
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
                this.CheckSpecialId = Request.Params["CheckSpecialId"];
                var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(this.CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.txtCheckTime.Text = "检查时间：";
                    if (checkSpecial.CheckTime != null)
                    {
                        this.txtCheckTime.Text += string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                    string personStr = "检查人：";
                    Model.Sys_User user = BLL.UserService.GetUserByUserId(checkSpecial.CheckPerson);
                    if (user != null)
                    {
                        personStr += user.UserName + "、";
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                    {
                        string[] strs = checkSpecial.PartInPersonIds.Split(',');
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
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersonNames))
                    {
                        if (personStr != "检查人：")
                        {
                            personStr += "、" + checkSpecial.PartInPersonNames;
                        }
                        else
                        {
                            personStr += checkSpecial.PartInPersonNames;
                        }
                    }
                    this.txtCheckPerson.Text = personStr;
                    this.txtCheckType.Text = "检查项目：";
                    if (checkSpecial.CheckType == "0")
                    {
                        this.txtCheckType.Text += "周检";
                    }
                    else if (checkSpecial.CheckType == "1")
                    {
                        this.txtCheckType.Text += "月检";
                    }
                    else if (checkSpecial.CheckType == "2")
                    {
                        this.txtCheckType.Text += "其它";
                    }
                    var checkSpecialDetails = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialId(this.CheckSpecialId);
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
                    foreach (var checkSpecialDetail in checkSpecialDetails)
                    {
                        string photo1 = string.Empty;
                        string photo2 = string.Empty;
                        Model.AttachFile attachFile = BLL.AttachFileService.GetAttachFile(checkSpecialDetail.CheckSpecialDetailId, BLL.Const.ProjectCheckSpecialMenuId);
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
                        Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(checkSpecialDetail.UnitId);
                        if (unit != null)
                        {
                            unitName = unit.UnitName;
                        }
                        if (checkSpecialDetail.CompletedDate != null)
                        {
                            completedDate = string.Format("{0:yyyy-MM-dd}", checkSpecialDetail.CompletedDate);
                        }
                        str += "<tr><td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;'>" + i + "</td>"
                                           + "<td align='center' style='border: 1px solid #000000; font-size:15px; border-top: none; border-right: none;' >" + checkSpecialDetail.Unqualified + "<br/>" + photo1 + "<br/>" + photo2 + "</td>"
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