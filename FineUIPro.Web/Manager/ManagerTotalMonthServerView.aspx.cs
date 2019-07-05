using System;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalMonthServerView : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string TotalMonth = Request.Params["TotalMonth"];
                string Type = Request.Params["Type"];

                if (Type == "1")
                {
                    this.SimpleForm1.Title = "一、本月HSE工作基本情况内容";
                }
                else if (Type == "2")
                {
                    this.SimpleForm1.Title = "二、本月主要完成HSE工作量数据统计";
                }
                else if (Type == "3")
                {
                    this.SimpleForm1.Title = "三、本月具体HSE工作开展情况（包括不可接受风险与控制情况）";
                }
                else if (Type == "4")
                {
                    this.SimpleForm1.Title = "四、本月HSE工作存在问题与处理（或拟采取对策）";
                }
                else if (Type == "5")
                {
                    this.SimpleForm1.Title = "五、下月工作计划";
                }
                else if (Type == "6")
                {
                    this.SimpleForm1.Title = "六、其他";
                }

                string showVulues = string.Empty;
                string showVuluesNULL = string.Empty;

                DateTime? monthTime = Funs.GetNewDateTime(TotalMonth);
                if (monthTime.HasValue)
                {
                    var projectList = BLL.ProjectService.GetTotalMonthProjectWorkList(monthTime);
                    if (projectList.Count() > 0)
                    {
                        foreach (var item in projectList)
                        {
                            string rowString = "&lt;p&gt;";
                            var totalMonth = Funs.DB.Manager_ManagerTotalMonth.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.CompileDate.Value.Year == monthTime.Value.Year && x.CompileDate.Value.Month == monthTime.Value.Month);
                            if (totalMonth != null)
                            {
                                if (Type == "1")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }
                                else if (Type == "2")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent2))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent2;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }
                                else if (Type == "3")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent3))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent3;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }
                                else if (Type == "4")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent4))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent4;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }
                                else if (Type == "5")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent5))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent5;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }
                                else if (Type == "6")
                                {
                                    if (!string.IsNullOrEmpty(totalMonth.MonthContent6))
                                    {
                                        showVulues += "【" + item.ProjectName + "】：";
                                        showVulues += totalMonth.MonthContent6;
                                    }
                                    else
                                    {
                                        showVuluesNULL += "【" + item.ProjectName + "】：<br />" + "无数据。";
                                    }
                                }

                                if (!string.IsNullOrEmpty(showVuluesNULL))
                                {
                                    showVuluesNULL += rowString;
                                }
                                if (!string.IsNullOrEmpty(showVulues))
                                {
                                    showVulues += rowString;
                                }
                            }
                            else
                            {
                                //string str=""
                                showVuluesNULL += "<p>【" + item.ProjectName + "】：<br />" + "无数据。 <br/></p>";
                                showVuluesNULL += rowString;
                            }
                        }

                        if (!string.IsNullOrEmpty(showVulues))
                        {
                            if (!string.IsNullOrEmpty(showVuluesNULL))
                            {
                                showVulues = showVulues + showVuluesNULL;
                            }
                        }
                        else
                        {
                            showVulues = showVuluesNULL;
                        }
                    }
                } 
                this.txtFileContent.Text = HttpUtility.HtmlDecode(showVulues);
            }
        }
        #endregion
    }
}