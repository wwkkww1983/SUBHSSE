using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.CostGoods
{
    public partial class TotalPayRegistrationView : PageBase
    {
        private static string headerStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Model.CostGoods_PayRegistration payRegistration = BLL.PayRegistrationService.GetPayRegistrationById(Request.Params["PayRegistrationId"]);
                if (payRegistration != null)
                {
                    DateTime startTime = Convert.ToDateTime(payRegistration.PayDate.Value.Year + "-" + payRegistration.PayDate.Value.Month + "-1");
                    DateTime endTime = startTime.AddMonths(1);
                    headerStr = string.Empty;
                    headerStr += "编号#类别#类别#项目名称#费用明细";
                    DataTable dt = new DataTable();
                    dt.Columns.Add("R0");   //编号
                    dt.Columns.Add("R1");   //类别
                    dt.Columns.Add("R2");   //类别
                    dt.Columns.Add("R3");   //项目名称
                    dt.Columns.Add("R4");   //费用明细
                    List<Model.Base_Unit> units = BLL.UnitService.GetMainAndSubUnitByProjectIdList(this.CurrUser.LoginProjectId);
                    int a = 0;
                    for (int i = 0; i < units.Count; i++)
                    {
                        headerStr += "#" + units[i].UnitName + " 当月累计,当年累计";
                        dt.Columns.Add("M" + a);   //当月累计
                        a++;
                        dt.Columns.Add("M" + a);   //当月累计
                        a++;
                    }
                    List<Model.CostGoods_PayRegistration> payRegistrations = BLL.PayRegistrationService.GetPayRegistrationByPayDate(startTime, endTime, this.CurrUser.LoginProjectId);
                    List<Model.CostGoods_PayRegistration> yearPayRegistrations = BLL.PayRegistrationService.GetPayRegistrationByYear(this.CurrUser.LoginProjectId, endTime);
                    var costManageItems = from x in Funs.DB.CostGoods_CostManageItem
                                          join y in Funs.DB.CostGoods_CostManage
                                          on x.CostManageId equals y.CostManageId
                                          where y.CostManageDate >= startTime && y.CostManageDate <= endTime && y.ProjectId == this.CurrUser.LoginProjectId
                                          select new
                                          {
                                              UnitId = y.UnitId,
                                              InvestCostProject = x.InvestCostProject,
                                              AuditCounts = x.AuditCounts ?? 0,
                                              AuditPriceMoney = x.AuditPriceMoney ?? 0
                                          };
                    var yearCostManageItems = from x in Funs.DB.CostGoods_CostManageItem
                                              join y in Funs.DB.CostGoods_CostManage
                                              on x.CostManageId equals y.CostManageId
                                              where y.CostManageDate.Value.Year == endTime.Year && y.ProjectId == this.CurrUser.LoginProjectId
                                              select new
                                              {
                                                  UnitId = y.UnitId,
                                                  InvestCostProject = x.InvestCostProject,
                                                  AuditCounts = x.AuditCounts ?? 0,
                                                  AuditPriceMoney = x.AuditPriceMoney ?? 0
                                              };
                    #region 1.基础管理
                    DataRow row1 = dt.NewRow();
                    row1[0] = "1";
                    row1[1] = "安全防护";
                    row1[2] = "1.基础管理";
                    row1[3] = "内业管理";
                    row1[4] = "安全生产规章制度、安全手册、应急预案等的编制、印刷";
                    int b = 5;
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row1[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row1[b] = Funs.GetNewDecimalOrZero(row1[b].ToString()).ToString("N2");
                            b++;
                            row1[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row1[b] = Funs.GetNewDecimalOrZero(row1[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row1[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row1[b] = Funs.GetNewDecimalOrZero(row1[b].ToString()).ToString("N2");
                            b++;
                            row1[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row1[b] = Funs.GetNewDecimalOrZero(row1[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row1);
                    b = 5;
                    DataRow row2 = dt.NewRow();
                    row2[0] = "2";
                    row2[1] = "安全防护";
                    row2[2] = "1.基础管理";
                    row2[3] = "内业管理";
                    row2[4] = "施工现场和特殊界区管理出入证、通行证制证、制卡";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row2[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row2[b] = Funs.GetNewDecimalOrZero(row2[b].ToString()).ToString("N2");
                            b++;
                            row2[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row2[b] = Funs.GetNewDecimalOrZero(row2[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row2[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row2[b] = Funs.GetNewDecimalOrZero(row2[b].ToString()).ToString("N2");
                            b++;
                            row2[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row2[b] = Funs.GetNewDecimalOrZero(row2[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row2);
                    b = 5;
                    DataRow row3 = dt.NewRow();
                    row3[0] = "3";
                    row3[1] = "安全防护";
                    row3[2] = "1.基础管理";
                    row3[3] = "内业管理";
                    row3[4] = "安全、环保、应急管理文档汇集、编辑、分析";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row3[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row3[b] = Funs.GetNewDecimalOrZero(row3[b].ToString()).ToString("N2");
                            b++;
                            row3[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row3[b] = Funs.GetNewDecimalOrZero(row3[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row3[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row3[b] = Funs.GetNewDecimalOrZero(row3[b].ToString()).ToString("N2");
                            b++;
                            row3[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row3[b] = Funs.GetNewDecimalOrZero(row3[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row3);
                    b = 5;
                    DataRow row4 = dt.NewRow();
                    row4[0] = "4";
                    row4[1] = "安全防护";
                    row4[2] = "1.基础管理";
                    row4[3] = "内业管理";
                    row4[4] = "安全检测、监测、评定、评价";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row4[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row4[b] = Funs.GetNewDecimalOrZero(row4[b].ToString()).ToString("N2");
                            b++;
                            row4[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row4[b] = Funs.GetNewDecimalOrZero(row4[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row4[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row4[b] = Funs.GetNewDecimalOrZero(row4[b].ToString()).ToString("N2");
                            b++;
                            row4[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row4[b] = Funs.GetNewDecimalOrZero(row4[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row4);
                    b = 5;
                    DataRow row5 = dt.NewRow();
                    row5[0] = "5";
                    row5[1] = "安全防护";
                    row5[2] = "1.基础管理";
                    row5[3] = "内业管理";
                    row5[4] = "报刊、标语、参考书、宣传画、音像制品等宣传品和现场宣传栏";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row5[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row5[b] = Funs.GetNewDecimalOrZero(row5[b].ToString()).ToString("N2");
                            b++;
                            row5[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0);
                            row5[b] = Funs.GetNewDecimalOrZero(row5[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row5[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row5[b] = Funs.GetNewDecimalOrZero(row5[b].ToString()).ToString("N2");
                            b++;
                            row5[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "内业管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row5[b] = Funs.GetNewDecimalOrZero(row5[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row5);
                    b = 5;
                    DataRow row6 = dt.NewRow();
                    row6[0] = "6";
                    row6[1] = "安全防护";
                    row6[2] = "1.基础管理";
                    row6[3] = "检测器材";
                    row6[4] = "员工进出场信息采集识别管理系统（含摄录存取及分析器材）购置、折旧或租赁费";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row6[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row6[b] = Funs.GetNewDecimalOrZero(row6[b].ToString()).ToString("N2");
                            b++;
                            row6[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row6[b] = Funs.GetNewDecimalOrZero(row6[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row6[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row6[b] = Funs.GetNewDecimalOrZero(row6[b].ToString()).ToString("N2");
                            b++;
                            row6[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row6[b] = Funs.GetNewDecimalOrZero(row6[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row6);
                    b = 5;
                    DataRow row7 = dt.NewRow();
                    row7[0] = "7";
                    row7[1] = "安全防护";
                    row7[2] = "1.基础管理";
                    row7[3] = "检测器材";
                    row7[4] = "射线、风速、噪声、温湿度、粉尘、空气质量检测仪器购置、折旧或租赁费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row7[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row7[b] = Funs.GetNewDecimalOrZero(row7[b].ToString()).ToString("N2");
                            b++;
                            row7[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row7[b] = Funs.GetNewDecimalOrZero(row7[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row7[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row7[b] = Funs.GetNewDecimalOrZero(row7[b].ToString()).ToString("N2");
                            b++;
                            row7[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row7[b] = Funs.GetNewDecimalOrZero(row7[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row7);
                    b = 5;
                    DataRow row8 = dt.NewRow();
                    row8[0] = "8";
                    row8[1] = "安全防护";
                    row8[2] = "1.基础管理";
                    row8[3] = "检测器材";
                    row8[4] = "气液成分、电气安全、力学特性、热工特性和几何量检测仪器购置、折旧或租赁费";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row8[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row8[b] = Funs.GetNewDecimalOrZero(row8[b].ToString()).ToString("N2");
                            b++;
                            row8[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row8[b] = Funs.GetNewDecimalOrZero(row8[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row8[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row8[b] = Funs.GetNewDecimalOrZero(row8[b].ToString()).ToString("N2");
                            b++;
                            row8[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row8[b] = Funs.GetNewDecimalOrZero(row8[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row8);
                    b = 5;
                    DataRow row9 = dt.NewRow();
                    row9[0] = "9";
                    row9[1] = "安全防护";
                    row9[2] = "1.基础管理";
                    row9[3] = "检测器材";
                    row9[4] = "监测、检测辅助器具";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row9[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row9[b] = Funs.GetNewDecimalOrZero(row9[b].ToString()).ToString("N2");
                            b++;
                            row9[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row9[b] = Funs.GetNewDecimalOrZero(row9[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row9[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row9[b] = Funs.GetNewDecimalOrZero(row9[b].ToString()).ToString("N2");
                            b++;
                            row9[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row9[b] = Funs.GetNewDecimalOrZero(row9[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row9);
                    b = 5;
                    DataRow row10 = dt.NewRow();
                    row10[0] = "10";
                    row10[1] = "安全防护";
                    row10[2] = "1.基础管理";
                    row10[3] = "检测器材";
                    row10[4] = "警戒警示通讯器材（对讲机、望远镜、测距仪）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row10[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row10[b] = Funs.GetNewDecimalOrZero(row10[b].ToString()).ToString("N2");
                            b++;
                            row10[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row10[b] = Funs.GetNewDecimalOrZero(row10[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row10[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row10[b] = Funs.GetNewDecimalOrZero(row10[b].ToString()).ToString("N2");
                            b++;
                            row10[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row10[b] = Funs.GetNewDecimalOrZero(row10[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row10);
                    b = 5;
                    DataRow row11 = dt.NewRow();
                    row11[0] = "11";
                    row11[1] = "安全防护";
                    row11[2] = "1.基础管理";
                    row11[3] = "检测器材";
                    row11[4] = "监测检测计量器具执行检定和维修费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row11[b] = payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row11[b] = Funs.GetNewDecimalOrZero(row11[b].ToString()).ToString("N2");
                            b++;
                            row11[b] = yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0);
                            row11[b] = Funs.GetNewDecimalOrZero(row11[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row11[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row11[b] = Funs.GetNewDecimalOrZero(row11[b].ToString()).ToString("N2");
                            b++;
                            row11[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "检测器材").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row11[b] = Funs.GetNewDecimalOrZero(row11[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row11);
                    b = 5;
                    DataRow row12 = dt.NewRow();
                    row12[0] = "12";
                    row12[1] = "安全防护";
                    row12[2] = "1.基础管理";
                    row12[3] = "警示警戒";
                    row12[4] = "风险突出处安全警示标志牌、警示灯、警戒线、提示牌等";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row12[b] = payRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row12[b] = Funs.GetNewDecimalOrZero(row12[b].ToString()).ToString("N2");
                            b++;
                            row12[b] = yearPayRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row12[b] = Funs.GetNewDecimalOrZero(row12[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row12[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row12[b] = Funs.GetNewDecimalOrZero(row12[b].ToString()).ToString("N2");
                            b++;
                            row12[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row12[b] = Funs.GetNewDecimalOrZero(row12[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row12);
                    b = 5;
                    DataRow row13 = dt.NewRow();
                    row13[0] = "13";
                    row13[1] = "安全防护";
                    row13[2] = "1.基础管理";
                    row13[3] = "警示警戒";
                    row13[4] = "各工种、各类施工机械的安全操作规程牌";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row13[b] = payRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row13[b] = Funs.GetNewDecimalOrZero(row13[b].ToString()).ToString("N2");
                            b++;
                            row13[b] = yearPayRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row13[b] = Funs.GetNewDecimalOrZero(row13[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row13[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row13[b] = Funs.GetNewDecimalOrZero(row13[b].ToString()).ToString("N2");
                            b++;
                            row13[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row13[b] = Funs.GetNewDecimalOrZero(row13[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row13);
                    b = 5;
                    DataRow row14 = dt.NewRow();
                    row14[0] = "14";
                    row14[1] = "安全防护";
                    row14[2] = "1.基础管理";
                    row14[3] = "警示警戒";
                    row14[4] = "特殊标识、标识设置";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row14[b] = payRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row14[b] = Funs.GetNewDecimalOrZero(row14[b].ToString()).ToString("N2");
                            b++;
                            row14[b] = yearPayRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_14 ?? 0);
                            row14[b] = Funs.GetNewDecimalOrZero(row14[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row14[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row14[b] = Funs.GetNewDecimalOrZero(row14[b].ToString()).ToString("N2");
                            b++;
                            row14[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "警示警戒").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row14[b] = Funs.GetNewDecimalOrZero(row14[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row14);
                    b = 5;
                    DataRow row15 = dt.NewRow();
                    row15[0] = "15";
                    row15[1] = "安全防护";
                    row15[2] = "1.基础管理";
                    row15[3] = "安全奖励";
                    row15[4] = "表彰安全先进集体、个人的奖励";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row15[b] = payRegistrations.Sum(x => x.SMonthType1_15 ?? 0);
                            row15[b] = Funs.GetNewDecimalOrZero(row15[b].ToString()).ToString("N2");
                            b++;
                            row15[b] = yearPayRegistrations.Sum(x => x.SMonthType1_15 ?? 0);
                            row15[b] = Funs.GetNewDecimalOrZero(row15[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row15[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全奖励").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row15[b] = Funs.GetNewDecimalOrZero(row15[b].ToString()).ToString("N2");
                            b++;
                            row15[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全奖励").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row15[b] = Funs.GetNewDecimalOrZero(row15[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row15);
                    b = 5;
                    DataRow row16 = dt.NewRow();
                    row16[0] = "16";
                    row16[1] = "安全防护";
                    row16[2] = "1.基础管理";
                    row16[3] = "其他";
                    row16[4] = "其它安全生产管理直接相关的支出";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row16[b] = payRegistrations.Sum(x => x.SMonthType1_16 ?? 0);
                            row16[b] = Funs.GetNewDecimalOrZero(row16[b].ToString()).ToString("N2");
                            b++;
                            row16[b] = yearPayRegistrations.Sum(x => x.SMonthType1_16 ?? 0);
                            row16[b] = Funs.GetNewDecimalOrZero(row16[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row16[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "其他").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row16[b] = Funs.GetNewDecimalOrZero(row16[b].ToString()).ToString("N2");
                            b++;
                            row16[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "其他").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row16[b] = Funs.GetNewDecimalOrZero(row16[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row16);
                    b = 5;
                    DataRow row17 = dt.NewRow();
                    row17[0] = "";
                    row17[1] = "安全防护";
                    row17[2] = "1.基础管理";
                    row17[3] = "";
                    row17[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row17[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_15 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_16 ?? 0);
                            row17[b] = Funs.GetNewDecimalOrZero(row17[b].ToString()).ToString("N2");
                            b++;
                            row17[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_15 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_16 ?? 0);
                            row17[b] = Funs.GetNewDecimalOrZero(row17[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row17[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "内业管理" || x.InvestCostProject == "检测器材" || x.InvestCostProject == "警示警戒" || x.InvestCostProject == "安全奖励" || x.InvestCostProject == "其他")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row17[b] = Funs.GetNewDecimalOrZero(row17[b].ToString()).ToString("N2");
                            b++;
                            row17[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "内业管理" || x.InvestCostProject == "检测器材" || x.InvestCostProject == "警示警戒" || x.InvestCostProject == "安全奖励" || x.InvestCostProject == "其他")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row17[b] = Funs.GetNewDecimalOrZero(row17[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row17);
                    #endregion
                    #region 2.安全技术
                    b = 5;
                    DataRow row18 = dt.NewRow();
                    row18[0] = "1";
                    row18[1] = "安全防护";
                    row18[2] = "2.安全技术";
                    row18[3] = "安全技术";
                    row18[4] = "专项方案中非常规安全措施费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row18[b] = payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row18[b] = Funs.GetNewDecimalOrZero(row18[b].ToString()).ToString("N2");
                            b++;
                            row18[b] = yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row18[b] = Funs.GetNewDecimalOrZero(row18[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row18[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row18[b] = Funs.GetNewDecimalOrZero(row18[b].ToString()).ToString("N2");
                            b++;
                            row18[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row18[b] = Funs.GetNewDecimalOrZero(row18[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row18);
                    b = 5;
                    DataRow row19 = dt.NewRow();
                    row19[0] = "2";
                    row19[1] = "安全防护";
                    row19[2] = "2.安全技术";
                    row19[3] = "安全技术";
                    row19[4] = "与安全相关的专项方案专家论证审查费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row19[b] = payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row19[b] = Funs.GetNewDecimalOrZero(row19[b].ToString()).ToString("N2");
                            b++;
                            row19[b] = yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row19[b] = Funs.GetNewDecimalOrZero(row19[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row19[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row19[b] = Funs.GetNewDecimalOrZero(row19[b].ToString()).ToString("N2");
                            b++;
                            row19[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row19[b] = Funs.GetNewDecimalOrZero(row19[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row19);
                    b = 5;
                    DataRow row20 = dt.NewRow();
                    row20[0] = "3";
                    row20[1] = "安全防护";
                    row20[2] = "2.安全技术";
                    row20[3] = "安全技术";
                    row20[4] = "各类安全技术方案的编制和咨询费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row20[b] = payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row20[b] = Funs.GetNewDecimalOrZero(row20[b].ToString()).ToString("N2");
                            b++;
                            row20[b] = yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row20[b] = Funs.GetNewDecimalOrZero(row20[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row20[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row20[b] = Funs.GetNewDecimalOrZero(row20[b].ToString()).ToString("N2");
                            b++;
                            row20[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row20[b] = Funs.GetNewDecimalOrZero(row20[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row20);
                    b = 5;
                    DataRow row21 = dt.NewRow();
                    row21[0] = "4";
                    row21[1] = "安全防护";
                    row21[2] = "2.安全技术";
                    row21[3] = "安全技术";
                    row21[4] = "安全技术进步专项费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row21[b] = payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row21[b] = Funs.GetNewDecimalOrZero(row21[b].ToString()).ToString("N2");
                            b++;
                            row21[b] = yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row21[b] = Funs.GetNewDecimalOrZero(row21[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row21[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row21[b] = Funs.GetNewDecimalOrZero(row21[b].ToString()).ToString("N2");
                            b++;
                            row21[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row21[b] = Funs.GetNewDecimalOrZero(row21[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row21);
                    b = 5;
                    DataRow row22 = dt.NewRow();
                    row22[0] = "";
                    row22[1] = "安全防护";
                    row22[2] = "2.安全技术";
                    row22[3] = "";
                    row22[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row22[b] = payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row22[b] = Funs.GetNewDecimalOrZero(row22[b].ToString()).ToString("N2");
                            b++;
                            row22[b] = yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0);
                            row22[b] = Funs.GetNewDecimalOrZero(row22[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row22[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row22[b] = Funs.GetNewDecimalOrZero(row22[b].ToString()).ToString("N2");
                            b++;
                            row22[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全技术").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row22[b] = Funs.GetNewDecimalOrZero(row22[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row22);
                    #endregion
                    #region 3.职业健康
                    b = 5;
                    DataRow row23 = dt.NewRow();
                    row23[0] = "1";
                    row23[1] = "安全防护";
                    row23[2] = "3.职业健康";
                    row23[3] = "工业卫生";
                    row23[4] = "通风、降温、保暖、除尘、防眩光设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row23[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row23[b] = Funs.GetNewDecimalOrZero(row23[b].ToString()).ToString("N2");
                            b++;
                            row23[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row23[b] = Funs.GetNewDecimalOrZero(row23[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row23[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row23[b] = Funs.GetNewDecimalOrZero(row23[b].ToString()).ToString("N2");
                            b++;
                            row23[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row23[b] = Funs.GetNewDecimalOrZero(row23[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row23);
                    b = 5;
                    DataRow row24 = dt.NewRow();
                    row24[0] = "2";
                    row24[1] = "安全防护";
                    row24[2] = "3.职业健康";
                    row24[3] = "工业卫生";
                    row24[4] = "职业病预防措施和有害作业工种保健费";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row24[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row24[b] = Funs.GetNewDecimalOrZero(row24[b].ToString()).ToString("N2");
                            b++;
                            row24[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row24[b] = Funs.GetNewDecimalOrZero(row24[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row24[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row24[b] = Funs.GetNewDecimalOrZero(row24[b].ToString()).ToString("N2");
                            b++;
                            row24[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row24[b] = Funs.GetNewDecimalOrZero(row24[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row24);
                    b = 5;
                    DataRow row25 = dt.NewRow();
                    row25[0] = "3";
                    row25[1] = "安全防护";
                    row25[2] = "3.职业健康";
                    row25[3] = "工业卫生";
                    row25[4] = "特殊环境作业和特殊要求行业人员体检费";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row25[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row25[b] = Funs.GetNewDecimalOrZero(row25[b].ToString()).ToString("N2");
                            b++;
                            row25[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row25[b] = Funs.GetNewDecimalOrZero(row25[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row25[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row25[b] = Funs.GetNewDecimalOrZero(row25[b].ToString()).ToString("N2");
                            b++;
                            row25[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row25[b] = Funs.GetNewDecimalOrZero(row25[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row25);
                    b = 5;
                    DataRow row26 = dt.NewRow();
                    row26[0] = "4";
                    row26[1] = "安全防护";
                    row26[2] = "3.职业健康";
                    row26[3] = "工业卫生";
                    row26[4] = "女工休息室、特殊作业人员休息室";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row26[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row26[b] = Funs.GetNewDecimalOrZero(row26[b].ToString()).ToString("N2");
                            b++;
                            row26[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row26[b] = Funs.GetNewDecimalOrZero(row26[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row26[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row26[b] = Funs.GetNewDecimalOrZero(row26[b].ToString()).ToString("N2");
                            b++;
                            row26[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row26[b] = Funs.GetNewDecimalOrZero(row26[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row26);
                    b = 5;
                    DataRow row27 = dt.NewRow();
                    row27[0] = "5";
                    row27[1] = "安全防护";
                    row27[2] = "3.职业健康";
                    row27[3] = "工业卫生";
                    row27[4] = "水泥等其他易飞扬颗粒建筑材料封闭放置和遮盖措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row27[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row27[b] = Funs.GetNewDecimalOrZero(row27[b].ToString()).ToString("N2");
                            b++;
                            row27[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row27[b] = Funs.GetNewDecimalOrZero(row27[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row27[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row27[b] = Funs.GetNewDecimalOrZero(row27[b].ToString()).ToString("N2");
                            b++;
                            row27[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row27[b] = Funs.GetNewDecimalOrZero(row27[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row27);
                    b = 5;
                    DataRow row28 = dt.NewRow();
                    row28[0] = "6";
                    row28[1] = "安全防护";
                    row28[2] = "3.职业健康";
                    row28[3] = "工业卫生";
                    row28[4] = "边角余料，废旧材料清理回收措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row28[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row28[b] = Funs.GetNewDecimalOrZero(row28[b].ToString()).ToString("N2");
                            b++;
                            row28[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row28[b] = Funs.GetNewDecimalOrZero(row28[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row28[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row28[b] = Funs.GetNewDecimalOrZero(row28[b].ToString()).ToString("N2");
                            b++;
                            row28[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row28[b] = Funs.GetNewDecimalOrZero(row28[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row28);
                    b = 5;
                    DataRow row29 = dt.NewRow();
                    row29[0] = "";
                    row29[1] = "安全防护";
                    row29[2] = "3.职业健康";
                    row29[3] = "";
                    row29[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row29[b] = payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row29[b] = Funs.GetNewDecimalOrZero(row29[b].ToString()).ToString("N2");
                            b++;
                            row29[b] = yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0);
                            row29[b] = Funs.GetNewDecimalOrZero(row29[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row29[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row29[b] = Funs.GetNewDecimalOrZero(row29[b].ToString()).ToString("N2");
                            b++;
                            row29[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "工业卫生").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row29[b] = Funs.GetNewDecimalOrZero(row29[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row29);
                    #endregion
                    #region 4.防护措施
                    b = 5;
                    DataRow row30 = dt.NewRow();
                    row30[0] = "1";
                    row30[1] = "安全防护";
                    row30[2] = "4.防护措施";
                    row30[3] = "安全用电";
                    row30[4] = "漏电保护器";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row30[b] = payRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row30[b] = Funs.GetNewDecimalOrZero(row30[b].ToString()).ToString("N2");
                            b++;
                            row30[b] = yearPayRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row30[b] = Funs.GetNewDecimalOrZero(row30[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row30[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row30[b] = Funs.GetNewDecimalOrZero(row30[b].ToString()).ToString("N2");
                            b++;
                            row30[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row30[b] = Funs.GetNewDecimalOrZero(row30[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row30);
                    b = 5;
                    DataRow row31 = dt.NewRow();
                    row31[0] = "2";
                    row31[1] = "安全防护";
                    row31[2] = "4.防护措施";
                    row31[3] = "安全用电";
                    row31[4] = "保护接地装置，大型机具设备的防雷接地";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row31[b] = payRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row31[b] = Funs.GetNewDecimalOrZero(row31[b].ToString()).ToString("N2");
                            b++;
                            row31[b] = yearPayRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row31[b] = Funs.GetNewDecimalOrZero(row31[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row31[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row31[b] = Funs.GetNewDecimalOrZero(row31[b].ToString()).ToString("N2");
                            b++;
                            row31[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row31[b] = Funs.GetNewDecimalOrZero(row31[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row31);
                    b = 5;
                    DataRow row32 = dt.NewRow();
                    row32[0] = "3";
                    row32[1] = "安全防护";
                    row32[2] = "4.防护措施";
                    row32[3] = "安全用电";
                    row32[4] = "受限空间使用的低压照明设备（隔离变压器、低压照明灯、专用配电箱）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row32[b] = payRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row32[b] = Funs.GetNewDecimalOrZero(row32[b].ToString()).ToString("N2");
                            b++;
                            row32[b] = yearPayRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_3 ?? 0);
                            row32[b] = Funs.GetNewDecimalOrZero(row32[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row32[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row32[b] = Funs.GetNewDecimalOrZero(row32[b].ToString()).ToString("N2");
                            b++;
                            row32[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "安全用电").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row32[b] = Funs.GetNewDecimalOrZero(row32[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row32);
                    b = 5;
                    DataRow row33 = dt.NewRow();
                    row33[0] = "4";
                    row33[1] = "安全防护";
                    row33[2] = "4.防护措施";
                    row33[3] = "高处作业及基坑";
                    row33[4] = "基坑及安全措施费隐蔽工程动土安全措施费（防坍塌措施）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row33[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row33[b] = Funs.GetNewDecimalOrZero(row33[b].ToString()).ToString("N2");
                            b++;
                            row33[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row33[b] = Funs.GetNewDecimalOrZero(row33[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row33[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row33[b] = Funs.GetNewDecimalOrZero(row33[b].ToString()).ToString("N2");
                            b++;
                            row33[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row33[b] = Funs.GetNewDecimalOrZero(row33[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row33);
                    b = 5;
                    DataRow row34 = dt.NewRow();
                    row34[0] = "5";
                    row34[1] = "安全防护";
                    row34[2] = "4.防护措施";
                    row34[3] = "高处作业及基坑";
                    row34[4] = "孔、洞、井的防护盖板和防护栏杆";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row34[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row34[b] = Funs.GetNewDecimalOrZero(row34[b].ToString()).ToString("N2");
                            b++;
                            row34[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row34[b] = Funs.GetNewDecimalOrZero(row34[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row34[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row34[b] = Funs.GetNewDecimalOrZero(row34[b].ToString()).ToString("N2");
                            b++;
                            row34[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row34[b] = Funs.GetNewDecimalOrZero(row34[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row34);
                    b = 5;
                    DataRow row35 = dt.NewRow();
                    row35[0] = "6";
                    row35[1] = "安全防护";
                    row35[2] = "4.防护措施";
                    row35[3] = "高处作业及基坑";
                    row35[4] = "其它临边防护材料（如安全网、踢脚板等）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row35[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row35[b] = Funs.GetNewDecimalOrZero(row35[b].ToString()).ToString("N2");
                            b++;
                            row35[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row35[b] = Funs.GetNewDecimalOrZero(row35[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row35[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row35[b] = Funs.GetNewDecimalOrZero(row35[b].ToString()).ToString("N2");
                            b++;
                            row35[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row35[b] = Funs.GetNewDecimalOrZero(row35[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row35);
                    b = 5;
                    DataRow row36 = dt.NewRow();
                    row36[0] = "7";
                    row36[1] = "安全防护";
                    row36[2] = "4.防护措施";
                    row36[3] = "高处作业及基坑";
                    row36[4] = "有防坠物要求的棚房设施建筑物临边和施工通道的隔离防护棚";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row36[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row36[b] = Funs.GetNewDecimalOrZero(row36[b].ToString()).ToString("N2");
                            b++;
                            row36[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row36[b] = Funs.GetNewDecimalOrZero(row36[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row36[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row36[b] = Funs.GetNewDecimalOrZero(row36[b].ToString()).ToString("N2");
                            b++;
                            row36[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row36[b] = Funs.GetNewDecimalOrZero(row36[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row36);
                    b = 5;
                    DataRow row37 = dt.NewRow();
                    row37[0] = "8";
                    row37[1] = "安全防护";
                    row37[2] = "4.防护措施";
                    row37[3] = "高处作业及基坑";
                    row37[4] = "防坠物措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row37[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row37[b] = Funs.GetNewDecimalOrZero(row37[b].ToString()).ToString("N2");
                            b++;
                            row37[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row37[b] = Funs.GetNewDecimalOrZero(row37[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row37[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row37[b] = Funs.GetNewDecimalOrZero(row37[b].ToString()).ToString("N2");
                            b++;
                            row37[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row37[b] = Funs.GetNewDecimalOrZero(row37[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row37);
                    b = 5;
                    DataRow row38 = dt.NewRow();
                    row38[0] = "9";
                    row38[1] = "安全防护";
                    row38[2] = "4.防护措施";
                    row38[3] = "高处作业及基坑";
                    row38[4] = "钢结构安装时脚手架等安全措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row38[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row38[b] = Funs.GetNewDecimalOrZero(row38[b].ToString()).ToString("N2");
                            b++;
                            row38[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row38[b] = Funs.GetNewDecimalOrZero(row38[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row38[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row38[b] = Funs.GetNewDecimalOrZero(row38[b].ToString()).ToString("N2");
                            b++;
                            row38[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row38[b] = Funs.GetNewDecimalOrZero(row38[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row38);
                    b = 5;
                    DataRow row39 = dt.NewRow();
                    row39[0] = "10";
                    row39[1] = "安全防护";
                    row39[2] = "4.防护措施";
                    row39[3] = "高处作业及基坑";
                    row39[4] = "高处作业下方区域警戒围护";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row39[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row39[b] = Funs.GetNewDecimalOrZero(row39[b].ToString()).ToString("N2");
                            b++;
                            row39[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row39[b] = Funs.GetNewDecimalOrZero(row39[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row39[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row39[b] = Funs.GetNewDecimalOrZero(row39[b].ToString()).ToString("N2");
                            b++;
                            row39[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row39[b] = Funs.GetNewDecimalOrZero(row39[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row39);
                    b = 5;
                    DataRow row40 = dt.NewRow();
                    row40[0] = "11";
                    row40[1] = "安全防护";
                    row40[2] = "4.防护措施";
                    row40[3] = "高处作业及基坑";
                    row40[4] = "其他高处作业安全措施（注：脚手架体、爬梯和通道等施工必要设施不属于安全防护措施，但护栏、安全网、挡脚板、生命线等属于安全防护措施）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row40[b] = payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row40[b] = Funs.GetNewDecimalOrZero(row40[b].ToString()).ToString("N2");
                            b++;
                            row40[b] = yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0);
                            row40[b] = Funs.GetNewDecimalOrZero(row40[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row40[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row40[b] = Funs.GetNewDecimalOrZero(row40[b].ToString()).ToString("N2");
                            b++;
                            row40[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "高处作业及基坑").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row40[b] = Funs.GetNewDecimalOrZero(row40[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row40);
                    b = 5;
                    DataRow row41 = dt.NewRow();
                    row41[0] = "12";
                    row41[1] = "安全防护";
                    row41[2] = "4.防护措施";
                    row41[3] = "临边洞口防护";
                    row41[4] = "为确保建构筑物、钢构、设备施工安全而搭设的操作平台的防护栏杆和踢脚板；洞口临边护栏和盖板、平网、立网（密网）；安全通道的侧护栏和防砸顶板等。";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row41[b] = payRegistrations.Sum(x => x.SMonthType4_12 ?? 0);
                            row41[b] = Funs.GetNewDecimalOrZero(row41[b].ToString()).ToString("N2");
                            b++;
                            row41[b] = yearPayRegistrations.Sum(x => x.SMonthType4_12 ?? 0);
                            row41[b] = Funs.GetNewDecimalOrZero(row41[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row41[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "临边洞口防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row41[b] = Funs.GetNewDecimalOrZero(row41[b].ToString()).ToString("N2");
                            b++;
                            row41[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "临边洞口防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row41[b] = Funs.GetNewDecimalOrZero(row41[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row41);
                    b = 5;
                    DataRow row42 = dt.NewRow();
                    row42[0] = "13";
                    row42[1] = "安全防护";
                    row42[2] = "4.防护措施";
                    row42[3] = "受限空间内作业";
                    row42[4] = "通风、降温、防触电和消防设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row42[b] = payRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row42[b] = Funs.GetNewDecimalOrZero(row42[b].ToString()).ToString("N2");
                            b++;
                            row42[b] = yearPayRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row42[b] = Funs.GetNewDecimalOrZero(row42[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row42[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row42[b] = Funs.GetNewDecimalOrZero(row42[b].ToString()).ToString("N2");
                            b++;
                            row42[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row42[b] = Funs.GetNewDecimalOrZero(row42[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row42);
                    b = 5;
                    DataRow row43 = dt.NewRow();
                    row43[0] = "14";
                    row43[1] = "安全防护";
                    row43[2] = "4.防护措施";
                    row43[3] = "受限空间内作业";
                    row43[4] = "安全电压照明系统";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row43[b] = payRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row43[b] = Funs.GetNewDecimalOrZero(row43[b].ToString()).ToString("N2");
                            b++;
                            row43[b] = yearPayRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row43[b] = Funs.GetNewDecimalOrZero(row43[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row43[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row43[b] = Funs.GetNewDecimalOrZero(row43[b].ToString()).ToString("N2");
                            b++;
                            row43[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row43[b] = Funs.GetNewDecimalOrZero(row43[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row43);
                    b = 5;
                    DataRow row44 = dt.NewRow();
                    row44[0] = "15";
                    row44[1] = "安全防护";
                    row44[2] = "4.防护措施";
                    row44[3] = "受限空间内作业";
                    row44[4] = "支护作业平台及防坠落、防滑设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row44[b] = payRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row44[b] = Funs.GetNewDecimalOrZero(row44[b].ToString()).ToString("N2");
                            b++;
                            row44[b] = yearPayRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_15 ?? 0);
                            row44[b] = Funs.GetNewDecimalOrZero(row44[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row44[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row44[b] = Funs.GetNewDecimalOrZero(row44[b].ToString()).ToString("N2");
                            b++;
                            row44[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "受限空间内作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row44[b] = Funs.GetNewDecimalOrZero(row44[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row44);
                    b = 5;
                    DataRow row45 = dt.NewRow();
                    row45[0] = "16";
                    row45[1] = "安全防护";
                    row45[2] = "4.防护措施";
                    row45[3] = "动火作业";
                    row45[4] = "气瓶固定、防晒、防砸措施（气瓶笼或气瓶架）；气瓶检漏措施；防回火设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row45[b] = payRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row45[b] = Funs.GetNewDecimalOrZero(row45[b].ToString()).ToString("N2");
                            b++;
                            row45[b] = yearPayRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row45[b] = Funs.GetNewDecimalOrZero(row45[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row45[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row45[b] = Funs.GetNewDecimalOrZero(row45[b].ToString()).ToString("N2");
                            b++;
                            row45[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row45[b] = Funs.GetNewDecimalOrZero(row45[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row45);
                    b = 5;
                    DataRow row46 = dt.NewRow();
                    row46[0] = "17";
                    row46[1] = "安全防护";
                    row46[2] = "4.防护措施";
                    row46[3] = "动火作业";
                    row46[4] = "高处动火的接火措施、挡火措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row46[b] = payRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row46[b] = Funs.GetNewDecimalOrZero(row46[b].ToString()).ToString("N2");
                            b++;
                            row46[b] = yearPayRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row46[b] = Funs.GetNewDecimalOrZero(row46[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row46[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row46[b] = Funs.GetNewDecimalOrZero(row46[b].ToString()).ToString("N2");
                            b++;
                            row46[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row46[b] = Funs.GetNewDecimalOrZero(row46[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row46);
                    b = 5;
                    DataRow row47 = dt.NewRow();
                    row47[0] = "18";
                    row47[1] = "安全防护";
                    row47[2] = "4.防护措施";
                    row47[3] = "动火作业";
                    row47[4] = "火源及溅落区附件设备、电缆、管道、电气、仪表等覆盖保护措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row47[b] = payRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row47[b] = Funs.GetNewDecimalOrZero(row47[b].ToString()).ToString("N2");
                            b++;
                            row47[b] = yearPayRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_18 ?? 0);
                            row47[b] = Funs.GetNewDecimalOrZero(row47[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row47[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row47[b] = Funs.GetNewDecimalOrZero(row47[b].ToString()).ToString("N2");
                            b++;
                            row47[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "动火作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row47[b] = Funs.GetNewDecimalOrZero(row47[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row47);
                    b = 5;
                    DataRow row48 = dt.NewRow();
                    row48[0] = "19";
                    row48[1] = "安全防护";
                    row48[2] = "4.防护措施";
                    row48[3] = "机械装备防护";
                    row48[4] = "中小型机具安全附件维护，使用保护（安全锁钩、护套）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row48[b] = payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row48[b] = Funs.GetNewDecimalOrZero(row48[b].ToString()).ToString("N2");
                            b++;
                            row48[b] = yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row48[b] = Funs.GetNewDecimalOrZero(row48[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row48[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row48[b] = Funs.GetNewDecimalOrZero(row48[b].ToString()).ToString("N2");
                            b++;
                            row48[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row48[b] = Funs.GetNewDecimalOrZero(row48[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row48);
                    b = 5;
                    DataRow row49 = dt.NewRow();
                    row49[0] = "20";
                    row49[1] = "安全防护";
                    row49[2] = "4.防护措施";
                    row49[3] = "机械装备防护";
                    row49[4] = "塔吊、吊车、物料提升机、施工电梯等的各种防护装置和保险装置（如安全门、安全钩、限位器、限制器、安全制动器、安全监控器等）检查维护费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row49[b] = payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row49[b] = Funs.GetNewDecimalOrZero(row49[b].ToString()).ToString("N2");
                            b++;
                            row49[b] = yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row49[b] = Funs.GetNewDecimalOrZero(row49[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row49[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row49[b] = Funs.GetNewDecimalOrZero(row49[b].ToString()).ToString("N2");
                            b++;
                            row49[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row49[b] = Funs.GetNewDecimalOrZero(row49[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row49);
                    b = 5;
                    DataRow row50 = dt.NewRow();
                    row50[0] = "21";
                    row50[1] = "安全防护";
                    row50[2] = "4.防护措施";
                    row50[3] = "机械装备防护";
                    row50[4] = "机械设备、电器设备等传动部分为安全增设的安全防护装置及自动开关配置费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row50[b] = payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row50[b] = Funs.GetNewDecimalOrZero(row50[b].ToString()).ToString("N2");
                            b++;
                            row50[b] = yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row50[b] = Funs.GetNewDecimalOrZero(row50[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row50[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row50[b] = Funs.GetNewDecimalOrZero(row50[b].ToString()).ToString("N2");
                            b++;
                            row50[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row50[b] = Funs.GetNewDecimalOrZero(row50[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row50);
                    b = 5;
                    DataRow row51 = dt.NewRow();
                    row51[0] = "22";
                    row51[1] = "安全防护";
                    row51[2] = "4.防护措施";
                    row51[3] = "机械装备防护";
                    row51[4] = "锅炉、压力容器、压缩机及各种有爆炸危险的保险装置检查维护费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row51[b] = payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row51[b] = Funs.GetNewDecimalOrZero(row51[b].ToString()).ToString("N2");
                            b++;
                            row51[b] = yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row51[b] = Funs.GetNewDecimalOrZero(row51[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row51[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row51[b] = Funs.GetNewDecimalOrZero(row51[b].ToString()).ToString("N2");
                            b++;
                            row51[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row51[b] = Funs.GetNewDecimalOrZero(row51[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row51);
                    b = 5;
                    DataRow row52 = dt.NewRow();
                    row52[0] = "23";
                    row52[1] = "安全防护";
                    row52[2] = "4.防护措施";
                    row52[3] = "机械装备防护";
                    row52[4] = "为安全生产采取的信号装置、报警装置维护检查费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row52[b] = payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row52[b] = Funs.GetNewDecimalOrZero(row52[b].ToString()).ToString("N2");
                            b++;
                            row52[b] = yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0);
                            row52[b] = Funs.GetNewDecimalOrZero(row52[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row52[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row52[b] = Funs.GetNewDecimalOrZero(row52[b].ToString()).ToString("N2");
                            b++;
                            row52[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "机械装备防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row52[b] = Funs.GetNewDecimalOrZero(row52[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row52);
                    b = 5;
                    DataRow row53 = dt.NewRow();
                    row53[0] = "24";
                    row53[1] = "安全防护";
                    row53[2] = "4.防护措施";
                    row53[3] = "吊装运输和起重";
                    row53[4] = "现场拆封、检查、安装准备工作所需要脚手架平台";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row53[b] = payRegistrations.Sum(x => x.SMonthType4_24 ?? 0);
                            row53[b] = Funs.GetNewDecimalOrZero(row53[b].ToString()).ToString("N2");
                            b++;
                            row53[b] = yearPayRegistrations.Sum(x => x.SMonthType4_24 ?? 0);
                            row53[b] = Funs.GetNewDecimalOrZero(row53[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row53[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "吊装运输和起重").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row53[b] = Funs.GetNewDecimalOrZero(row53[b].ToString()).ToString("N2");
                            b++;
                            row53[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "吊装运输和起重").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row53[b] = Funs.GetNewDecimalOrZero(row53[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row53);
                    b = 5;
                    DataRow row54 = dt.NewRow();
                    row54[0] = "25";
                    row54[1] = "安全防护";
                    row54[2] = "4.防护措施";
                    row54[3] = "硼砂作业";
                    row54[4] = "吸尘、降尘系统";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row54[b] = payRegistrations.Sum(x => x.SMonthType4_25 ?? 0);
                            row54[b] = Funs.GetNewDecimalOrZero(row54[b].ToString()).ToString("N2");
                            b++;
                            row54[b] = yearPayRegistrations.Sum(x => x.SMonthType4_25 ?? 0);
                            row54[b] = Funs.GetNewDecimalOrZero(row54[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row54[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "硼砂作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row54[b] = Funs.GetNewDecimalOrZero(row54[b].ToString()).ToString("N2");
                            b++;
                            row54[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "硼砂作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row54[b] = Funs.GetNewDecimalOrZero(row54[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row54);
                    b = 5;
                    DataRow row55 = dt.NewRow();
                    row55[0] = "26";
                    row55[1] = "安全防护";
                    row55[2] = "4.防护措施";
                    row55[3] = "拆除工程";
                    row55[4] = "封固、隔离、保护设施及临时平台、通道搭设";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row55[b] = payRegistrations.Sum(x => x.SMonthType4_26 ?? 0);
                            row55[b] = Funs.GetNewDecimalOrZero(row55[b].ToString()).ToString("N2");
                            b++;
                            row55[b] = yearPayRegistrations.Sum(x => x.SMonthType4_26 ?? 0);
                            row55[b] = Funs.GetNewDecimalOrZero(row55[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row55[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "拆除工程").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row55[b] = Funs.GetNewDecimalOrZero(row55[b].ToString()).ToString("N2");
                            b++;
                            row55[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "拆除工程").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row55[b] = Funs.GetNewDecimalOrZero(row55[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row55);
                    b = 5;
                    DataRow row56 = dt.NewRow();
                    row56[0] = "27";
                    row56[1] = "安全防护";
                    row56[2] = "4.防护措施";
                    row56[3] = "试压试车与有害介质作业";
                    row56[4] = "动土作业时的人工探挖、探查等措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row56[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row56[b] = Funs.GetNewDecimalOrZero(row56[b].ToString()).ToString("N2");
                            b++;
                            row56[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row56[b] = Funs.GetNewDecimalOrZero(row56[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row56[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row56[b] = Funs.GetNewDecimalOrZero(row56[b].ToString()).ToString("N2");
                            b++;
                            row56[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row56[b] = Funs.GetNewDecimalOrZero(row56[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row56);
                    b = 5;
                    DataRow row57 = dt.NewRow();
                    row57[0] = "28";
                    row57[1] = "安全防护";
                    row57[2] = "4.防护措施";
                    row57[3] = "试压试车与有害介质作业";
                    row57[4] = "车辆阻火器和施工机具、临时用电设备、照明设备、锤击工具防爆设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row57[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row57[b] = Funs.GetNewDecimalOrZero(row57[b].ToString()).ToString("N2");
                            b++;
                            row57[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row57[b] = Funs.GetNewDecimalOrZero(row57[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row57[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row57[b] = Funs.GetNewDecimalOrZero(row57[b].ToString()).ToString("N2");
                            b++;
                            row57[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row57[b] = Funs.GetNewDecimalOrZero(row57[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row57);
                    b = 5;
                    DataRow row58 = dt.NewRow();
                    row58[0] = "29";
                    row58[1] = "安全防护";
                    row58[2] = "4.防护措施";
                    row58[3] = "试压试车与有害介质作业";
                    row58[4] = "地沟、阀门井、排污井等封闭、冲洗";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row58[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row58[b] = Funs.GetNewDecimalOrZero(row58[b].ToString()).ToString("N2");
                            b++;
                            row58[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row58[b] = Funs.GetNewDecimalOrZero(row58[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row58[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row58[b] = Funs.GetNewDecimalOrZero(row58[b].ToString()).ToString("N2");
                            b++;
                            row58[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row58[b] = Funs.GetNewDecimalOrZero(row58[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row58);
                    b = 5;
                    DataRow row59 = dt.NewRow();
                    row59[0] = "30";
                    row59[1] = "安全防护";
                    row59[2] = "4.防护措施";
                    row59[3] = "试压试车与有害介质作业";
                    row59[4] = "施工区域与生产的空间隔离和系统隔离及警戒措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row59[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row59[b] = Funs.GetNewDecimalOrZero(row59[b].ToString()).ToString("N2");
                            b++;
                            row59[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row59[b] = Funs.GetNewDecimalOrZero(row59[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row59[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row59[b] = Funs.GetNewDecimalOrZero(row59[b].ToString()).ToString("N2");
                            b++;
                            row59[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row59[b] = Funs.GetNewDecimalOrZero(row59[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row59);
                    b = 5;
                    DataRow row60 = dt.NewRow();
                    row60[0] = "31";
                    row60[1] = "安全防护";
                    row60[2] = "4.防护措施";
                    row60[3] = "试压试车与有害介质作业";
                    row60[4] = "清污、限污所用器材专用安全防护器材和隔离设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row60[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row60[b] = Funs.GetNewDecimalOrZero(row60[b].ToString()).ToString("N2");
                            b++;
                            row60[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row60[b] = Funs.GetNewDecimalOrZero(row60[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row60[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row60[b] = Funs.GetNewDecimalOrZero(row60[b].ToString()).ToString("N2");
                            b++;
                            row60[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row60[b] = Funs.GetNewDecimalOrZero(row60[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row60);
                    b = 5;
                    DataRow row61 = dt.NewRow();
                    row61[0] = "32";
                    row61[1] = "安全防护";
                    row61[2] = "4.防护措施";
                    row61[3] = "试压试车与有害介质作业";
                    row61[4] = "消音及噪声隔离设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row61[b] = payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row61[b] = Funs.GetNewDecimalOrZero(row61[b].ToString()).ToString("N2");
                            b++;
                            row61[b] = yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0);
                            row61[b] = Funs.GetNewDecimalOrZero(row61[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row61[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row61[b] = Funs.GetNewDecimalOrZero(row61[b].ToString()).ToString("N2");
                            b++;
                            row61[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "试压试车与有害介质作业").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row61[b] = Funs.GetNewDecimalOrZero(row61[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row61);
                    b = 5;
                    DataRow row62 = dt.NewRow();
                    row62[0] = "33";
                    row62[1] = "安全防护";
                    row62[2] = "4.防护措施";
                    row62[3] = "特种作业防护";
                    row62[4] = "特种作业防护服，绝缘鞋，酸碱，绝缘手套，焊工面罩，鞋盖，护膝，护袖，披肩，各种专用防护眼镜，面罩，绝缘靴，自主呼吸器，防毒面具等，安全帽、安全带等";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row62[b] = payRegistrations.Sum(x => x.SMonthType4_33 ?? 0);
                            row62[b] = Funs.GetNewDecimalOrZero(row62[b].ToString()).ToString("N2");
                            b++;
                            row62[b] = yearPayRegistrations.Sum(x => x.SMonthType4_33 ?? 0);
                            row62[b] = Funs.GetNewDecimalOrZero(row62[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row62[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "特种作业防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row62[b] = Funs.GetNewDecimalOrZero(row62[b].ToString()).ToString("N2");
                            b++;
                            row62[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "特种作业防护").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row62[b] = Funs.GetNewDecimalOrZero(row62[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row62);
                    b = 5;
                    DataRow row63 = dt.NewRow();
                    row63[0] = "34";
                    row63[1] = "安全防护";
                    row63[2] = "4.防护措施";
                    row63[3] = "应急管理";
                    row63[4] = "灭火器、灭火器箱、水带、消防池、消防铲、消防桶、太平斧、消防器材架等";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row63[b] = payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row63[b] = Funs.GetNewDecimalOrZero(row63[b].ToString()).ToString("N2");
                            b++;
                            row63[b] = yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row63[b] = Funs.GetNewDecimalOrZero(row63[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row63[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row63[b] = Funs.GetNewDecimalOrZero(row63[b].ToString()).ToString("N2");
                            b++;
                            row63[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row63[b] = Funs.GetNewDecimalOrZero(row63[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row63);
                    b = 5;
                    DataRow row64 = dt.NewRow();
                    row64[0] = "35";
                    row64[1] = "安全防护";
                    row64[2] = "4.防护措施";
                    row64[3] = "应急管理";
                    row64[4] = "防火毯、防火布、接火盆、挡火板、挡风用三防布、临时消防水管安装、拆除";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row64[b] = payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row64[b] = Funs.GetNewDecimalOrZero(row64[b].ToString()).ToString("N2");
                            b++;
                            row64[b] = yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row64[b] = Funs.GetNewDecimalOrZero(row64[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row64[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row64[b] = Funs.GetNewDecimalOrZero(row64[b].ToString()).ToString("N2");
                            b++;
                            row64[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row64[b] = Funs.GetNewDecimalOrZero(row64[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row64);
                    b = 5;
                    DataRow row65 = dt.NewRow();
                    row65[0] = "36";
                    row65[1] = "安全防护";
                    row65[2] = "4.防护措施";
                    row65[3] = "应急管理";
                    row65[4] = "应急器材及演练器材动用费用、消耗费用和工时损失费用等";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row65[b] = payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row65[b] = Funs.GetNewDecimalOrZero(row65[b].ToString()).ToString("N2");
                            b++;
                            row65[b] = yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row65[b] = Funs.GetNewDecimalOrZero(row65[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row65[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row65[b] = Funs.GetNewDecimalOrZero(row65[b].ToString()).ToString("N2");
                            b++;
                            row65[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row65[b] = Funs.GetNewDecimalOrZero(row65[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row65);
                    b = 5;
                    DataRow row66 = dt.NewRow();
                    row66[0] = "37";
                    row66[1] = "安全防护";
                    row66[2] = "4.防护措施";
                    row66[3] = "应急管理";
                    row66[4] = "应急淋浴和洗眼器、酸碱灼伤专用药品等现场医务室应急器材和消防、救护车辆";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row66[b] = payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row66[b] = Funs.GetNewDecimalOrZero(row66[b].ToString()).ToString("N2");
                            b++;
                            row66[b] = yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0);
                            row66[b] = Funs.GetNewDecimalOrZero(row66[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row66[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row66[b] = Funs.GetNewDecimalOrZero(row66[b].ToString()).ToString("N2");
                            b++;
                            row66[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "应急管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row66[b] = Funs.GetNewDecimalOrZero(row66[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row66);
                    b = 5;
                    DataRow row67 = dt.NewRow();
                    row67[0] = "38";
                    row67[1] = "安全防护";
                    row67[2] = "4.防护措施";
                    row67[3] = "非常措施";
                    row67[4] = "风雨季、沙尘暴、雷击、地质灾害、大水体防护和防洪特殊环境及临时处置费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row67[b] = payRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_39 ?? 0);
                            row67[b] = Funs.GetNewDecimalOrZero(row67[b].ToString()).ToString("N2");
                            b++;
                            row67[b] = yearPayRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_39 ?? 0);
                            row67[b] = Funs.GetNewDecimalOrZero(row67[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row67[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "非常措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row67[b] = Funs.GetNewDecimalOrZero(row67[b].ToString()).ToString("N2");
                            b++;
                            row67[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "非常措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row67[b] = Funs.GetNewDecimalOrZero(row67[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row67);
                    b = 5;
                    DataRow row68 = dt.NewRow();
                    row68[0] = "39";
                    row68[1] = "安全防护";
                    row68[2] = "4.防护措施";
                    row68[3] = "非常措施";
                    row68[4] = "雨季、台风、沙尘暴等恶劣天气下，加固临时设施、大型施工机具等费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row68[b] = payRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_39 ?? 0);
                            row68[b] = Funs.GetNewDecimalOrZero(row68[b].ToString()).ToString("N2");
                            b++;
                            row68[b] = yearPayRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_39 ?? 0);
                            row68[b] = Funs.GetNewDecimalOrZero(row68[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row68[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "非常措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row68[b] = Funs.GetNewDecimalOrZero(row68[b].ToString()).ToString("N2");
                            b++;
                            row68[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "非常措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row68[b] = Funs.GetNewDecimalOrZero(row68[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row68);
                    b = 5;
                    DataRow row69 = dt.NewRow();
                    row69[0] = "40";
                    row69[1] = "安全防护";
                    row69[2] = "4.防护措施";
                    row69[3] = "其他安全措施";
                    row69[4] = "其他";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row69[b] = payRegistrations.Sum(x => x.SMonthType4_40 ?? 0);
                            row69[b] = Funs.GetNewDecimalOrZero(row69[b].ToString()).ToString("N2");
                            b++;
                            row69[b] = yearPayRegistrations.Sum(x => x.SMonthType4_40 ?? 0);
                            row69[b] = Funs.GetNewDecimalOrZero(row69[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row69[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "其他安全措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row69[b] = Funs.GetNewDecimalOrZero(row69[b].ToString()).ToString("N2");
                            b++;
                            row69[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "其他安全措施").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row69[b] = Funs.GetNewDecimalOrZero(row69[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row69);
                    b = 5;
                    DataRow row70 = dt.NewRow();
                    row70[0] = "";
                    row70[1] = "安全防护";
                    row70[2] = "4.防护措施";
                    row70[3] = "";
                    row70[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row70[b] = payRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_15 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_18 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_24 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_25 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_26 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_33 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_39 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_40 ?? 0);
                            row70[b] = Funs.GetNewDecimalOrZero(row70[b].ToString()).ToString("N2");
                            b++;
                            row70[b] = yearPayRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_15 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_18 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_24 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_25 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_26 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_33 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_39 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_40 ?? 0);
                            row70[b] = Funs.GetNewDecimalOrZero(row70[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row70[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "安全用电" || x.InvestCostProject == "高处作业及基坑" || x.InvestCostProject == "临边洞口防护" || x.InvestCostProject == "受限空间内作业" || x.InvestCostProject == "动火作业" || x.InvestCostProject == "机械装备防护" || x.InvestCostProject == "吊装运输和起重" || x.InvestCostProject == "硼砂作业" || x.InvestCostProject == "拆除工程" || x.InvestCostProject == "试压试车与有害介质作业" || x.InvestCostProject == "特种作业防护" || x.InvestCostProject == "应急管理" || x.InvestCostProject == "非常措施" || x.InvestCostProject == "其他安全措施")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row70[b] = Funs.GetNewDecimalOrZero(row70[b].ToString()).ToString("N2");
                            b++;
                            row70[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "安全用电" || x.InvestCostProject == "高处作业及基坑" || x.InvestCostProject == "临边洞口防护" || x.InvestCostProject == "受限空间内作业" || x.InvestCostProject == "动火作业" || x.InvestCostProject == "机械装备防护" || x.InvestCostProject == "吊装运输和起重" || x.InvestCostProject == "硼砂作业" || x.InvestCostProject == "拆除工程" || x.InvestCostProject == "试压试车与有害介质作业" || x.InvestCostProject == "特种作业防护" || x.InvestCostProject == "应急管理" || x.InvestCostProject == "非常措施" || x.InvestCostProject == "其他安全措施")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row70[b] = Funs.GetNewDecimalOrZero(row70[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row70);
                    #endregion
                    #region 5.化工试车
                    b = 5;
                    DataRow row71 = dt.NewRow();
                    row71[0] = "1";
                    row71[1] = "安全防护";
                    row71[2] = "5.化工试车";
                    row71[3] = "装置区封闭管理";
                    row71[4] = "装置区域封闭用围挡";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row71[b] = payRegistrations.Sum(x => x.SMonthType5_1 ?? 0);
                            row71[b] = Funs.GetNewDecimalOrZero(row71[b].ToString()).ToString("N2");
                            b++;
                            row71[b] = yearPayRegistrations.Sum(x => x.SMonthType5_1 ?? 0);
                            row71[b] = Funs.GetNewDecimalOrZero(row71[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row71[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "装置区封闭管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row71[b] = Funs.GetNewDecimalOrZero(row71[b].ToString()).ToString("N2");
                            b++;
                            row71[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "装置区封闭管理").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row71[b] = Funs.GetNewDecimalOrZero(row71[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row71);
                    b = 5;
                    DataRow row72 = dt.NewRow();
                    row72[0] = "2";
                    row72[1] = "安全防护";
                    row72[2] = "5.化工试车";
                    row72[3] = "防爆施工器具";
                    row72[4] = "防爆电箱、防爆插头插座、防爆灯具、防爆施工机具器具";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row72[b] = payRegistrations.Sum(x => x.SMonthType5_2 ?? 0);
                            row72[b] = Funs.GetNewDecimalOrZero(row72[b].ToString()).ToString("N2");
                            b++;
                            row72[b] = yearPayRegistrations.Sum(x => x.SMonthType5_2 ?? 0);
                            row72[b] = Funs.GetNewDecimalOrZero(row72[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row72[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防爆施工器具").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row72[b] = Funs.GetNewDecimalOrZero(row72[b].ToString()).ToString("N2");
                            b++;
                            row72[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防爆施工器具").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row72[b] = Funs.GetNewDecimalOrZero(row72[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row72);
                    b = 5;
                    DataRow row73 = dt.NewRow();
                    row73[0] = "3";
                    row73[1] = "安全防护";
                    row73[2] = "5.化工试车";
                    row73[3] = "标识标签与锁定";
                    row73[4] = "盲板管理用货架、专用锁具、专用标签与警示标志";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row73[b] = payRegistrations.Sum(x => x.SMonthType5_3 ?? 0);
                            row73[b] = Funs.GetNewDecimalOrZero(row73[b].ToString()).ToString("N2");
                            b++;
                            row73[b] = yearPayRegistrations.Sum(x => x.SMonthType5_3 ?? 0);
                            row73[b] = Funs.GetNewDecimalOrZero(row73[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row73[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "标识标签与锁定").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row73[b] = Funs.GetNewDecimalOrZero(row73[b].ToString()).ToString("N2");
                            b++;
                            row73[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "标识标签与锁定").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row73[b] = Funs.GetNewDecimalOrZero(row73[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row73);
                    b = 5;
                    DataRow row74 = dt.NewRow();
                    row74[0] = "4";
                    row74[1] = "安全防护";
                    row74[2] = "5.化工试车";
                    row74[3] = "关键场所封闭";
                    row74[4] = "专区封闭管理措施费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row74[b] = payRegistrations.Sum(x => x.SMonthType5_4 ?? 0);
                            row74[b] = Funs.GetNewDecimalOrZero(row74[b].ToString()).ToString("N2");
                            b++;
                            row74[b] = yearPayRegistrations.Sum(x => x.SMonthType5_4 ?? 0);
                            row74[b] = Funs.GetNewDecimalOrZero(row74[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row74[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "关键场所封闭").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row74[b] = Funs.GetNewDecimalOrZero(row74[b].ToString()).ToString("N2");
                            b++;
                            row74[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "关键场所封闭").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row74[b] = Funs.GetNewDecimalOrZero(row74[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row74);
                    b = 5;
                    DataRow row75 = dt.NewRow();
                    row75[0] = "5";
                    row75[1] = "安全防护";
                    row75[2] = "5.化工试车";
                    row75[3] = "催化剂加装还原";
                    row75[4] = "防毒、放辐射措施和加氢点警戒、监护";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row75[b] = payRegistrations.Sum(x => x.SMonthType5_5 ?? 0);
                            row75[b] = Funs.GetNewDecimalOrZero(row75[b].ToString()).ToString("N2");
                            b++;
                            row75[b] = yearPayRegistrations.Sum(x => x.SMonthType5_5 ?? 0);
                            row75[b] = Funs.GetNewDecimalOrZero(row75[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row75[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "催化剂加装还原").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row75[b] = Funs.GetNewDecimalOrZero(row75[b].ToString()).ToString("N2");
                            b++;
                            row75[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "催化剂加装还原").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row75[b] = Funs.GetNewDecimalOrZero(row75[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row75);
                    b = 5;
                    DataRow row76 = dt.NewRow();
                    row76[0] = "6";
                    row76[1] = "安全防护";
                    row76[2] = "5.化工试车";
                    row76[3] = "联动和化工试车";
                    row76[4] = "其他专项措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row76[b] = payRegistrations.Sum(x => x.SMonthType5_6 ?? 0);
                            row76[b] = Funs.GetNewDecimalOrZero(row76[b].ToString()).ToString("N2");
                            b++;
                            row76[b] = yearPayRegistrations.Sum(x => x.SMonthType5_6 ?? 0);
                            row76[b] = Funs.GetNewDecimalOrZero(row76[b].ToString()).ToString("N2");
                            b++;

                        }
                        else    //分包商
                        {
                            row76[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "联动和化工试车").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row76[b] = Funs.GetNewDecimalOrZero(row76[b].ToString()).ToString("N2");
                            b++;
                            row76[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "联动和化工试车").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row76[b] = Funs.GetNewDecimalOrZero(row76[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row76);
                    b = 5;
                    DataRow row77 = dt.NewRow();
                    row77[0] = "";
                    row77[1] = "安全防护";
                    row77[2] = "5.化工试车";
                    row77[3] = "";
                    row77[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row77[b] = payRegistrations.Sum(x => x.SMonthType5_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_6 ?? 0);
                            row77[b] = Funs.GetNewDecimalOrZero(row77[b].ToString()).ToString("N2");
                            b++;
                            row77[b] = yearPayRegistrations.Sum(x => x.SMonthType5_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_6 ?? 0);
                            row77[b] = Funs.GetNewDecimalOrZero(row77[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row77[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "装置区封闭管理" || x.InvestCostProject == "防爆施工器具" || x.InvestCostProject == "标识标签与锁定" || x.InvestCostProject == "关键场所封闭" || x.InvestCostProject == "催化剂加装还原" || x.InvestCostProject == "联动和化工试车")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row77[b] = Funs.GetNewDecimalOrZero(row77[b].ToString()).ToString("N2");
                            b++;
                            row77[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && (x.InvestCostProject == "装置区封闭管理" || x.InvestCostProject == "防爆施工器具" || x.InvestCostProject == "标识标签与锁定" || x.InvestCostProject == "关键场所封闭" || x.InvestCostProject == "催化剂加装还原" || x.InvestCostProject == "联动和化工试车")).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row77[b] = Funs.GetNewDecimalOrZero(row77[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row77);
                    #endregion
                    #region 6.教育培训
                    b = 5;
                    DataRow row78 = dt.NewRow();
                    row78[0] = "1";
                    row78[1] = "安全防护";
                    row78[2] = "6.教育培训";
                    row78[3] = "教育培训";
                    row78[4] = "安全教育培训工时占用费（入场教育、专项培训、违章停工教育）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row78[b] = payRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row78[b] = Funs.GetNewDecimalOrZero(row78[b].ToString()).ToString("N2");
                            b++;
                            row78[b] = yearPayRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row78[b] = Funs.GetNewDecimalOrZero(row78[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row78[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row78[b] = Funs.GetNewDecimalOrZero(row78[b].ToString()).ToString("N2");
                            b++;
                            row78[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row78[b] = Funs.GetNewDecimalOrZero(row78[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row78);
                    b = 5;
                    DataRow row79 = dt.NewRow();
                    row79[0] = "2";
                    row79[1] = "安全防护";
                    row79[2] = "6.教育培训";
                    row79[3] = "教育培训";
                    row79[4] = "师资费用";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row79[b] = payRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row79[b] = Funs.GetNewDecimalOrZero(row79[b].ToString()).ToString("N2");
                            b++;
                            row79[b] = yearPayRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row79[b] = Funs.GetNewDecimalOrZero(row79[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row79[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row79[b] = Funs.GetNewDecimalOrZero(row79[b].ToString()).ToString("N2");
                            b++;
                            row79[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row79[b] = Funs.GetNewDecimalOrZero(row79[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row79);
                    b = 5;
                    DataRow row80 = dt.NewRow();
                    row80[0] = "3";
                    row80[1] = "安全防护";
                    row80[2] = "6.教育培训";
                    row80[3] = "教育培训";
                    row80[4] = "安全培训教育器材、教材";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row80[b] = payRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row80[b] = Funs.GetNewDecimalOrZero(row80[b].ToString()).ToString("N2");
                            b++;
                            row80[b] = yearPayRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row80[b] = Funs.GetNewDecimalOrZero(row80[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row80[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row80[b] = Funs.GetNewDecimalOrZero(row80[b].ToString()).ToString("N2");
                            b++;
                            row80[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row80[b] = Funs.GetNewDecimalOrZero(row80[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row80);
                    b = 5;
                    DataRow row81 = dt.NewRow();
                    row81[0] = "";
                    row81[1] = "安全防护";
                    row81[2] = "6.教育培训";
                    row81[3] = "";
                    row81[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row81[b] = payRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row81[b] = Funs.GetNewDecimalOrZero(row81[b].ToString()).ToString("N2");
                            b++;
                            row81[b] = yearPayRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_3 ?? 0);
                            row81[b] = Funs.GetNewDecimalOrZero(row81[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row81[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row81[b] = Funs.GetNewDecimalOrZero(row81[b].ToString()).ToString("N2");
                            b++;
                            row81[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "教育培训").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row81[b] = Funs.GetNewDecimalOrZero(row81[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row81);
                    #endregion
                    #region   文明施工和环境保护
                    b = 5;
                    DataRow row82 = dt.NewRow();
                    row82[0] = "1";
                    row82[1] = "文明施工和环境保护";
                    row82[2] = "文明施工和环境保护";
                    row82[3] = "防护控制和排放";
                    row82[4] = "减震与降低噪音设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row82[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row82[b] = Funs.GetNewDecimalOrZero(row82[b].ToString()).ToString("N2");
                            b++;
                            row82[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row82[b] = Funs.GetNewDecimalOrZero(row82[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row82[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row82[b] = Funs.GetNewDecimalOrZero(row82[b].ToString()).ToString("N2");
                            b++;
                            row82[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row82[b] = Funs.GetNewDecimalOrZero(row82[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row82);
                    b = 5;
                    DataRow row83 = dt.NewRow();
                    row83[0] = "2";
                    row83[1] = "文明施工和环境保护";
                    row83[2] = "文明施工和环境保护";
                    row83[3] = "防护控制和排放";
                    row83[4] = "射线防护的设施、措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row83[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row83[b] = Funs.GetNewDecimalOrZero(row83[b].ToString()).ToString("N2");
                            b++;
                            row83[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row83[b] = Funs.GetNewDecimalOrZero(row83[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row83[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row83[b] = Funs.GetNewDecimalOrZero(row83[b].ToString()).ToString("N2");
                            b++;
                            row83[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row83[b] = Funs.GetNewDecimalOrZero(row83[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row83);
                    b = 5;
                    DataRow row84 = dt.NewRow();
                    row84[0] = "3";
                    row84[1] = "文明施工和环境保护";
                    row84[2] = "文明施工和环境保护";
                    row84[3] = "防护控制和排放";
                    row84[4] = "声、光、尘、有害物质防逸散控制措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row84[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row84[b] = Funs.GetNewDecimalOrZero(row84[b].ToString()).ToString("N2");
                            b++;
                            row84[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row84[b] = Funs.GetNewDecimalOrZero(row84[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row84[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row84[b] = Funs.GetNewDecimalOrZero(row84[b].ToString()).ToString("N2");
                            b++;
                            row84[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row84[b] = Funs.GetNewDecimalOrZero(row84[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row84);
                    b = 5;
                    DataRow row85 = dt.NewRow();
                    row85[0] = "4";
                    row85[1] = "文明施工和环境保护";
                    row85[2] = "文明施工和环境保护";
                    row85[3] = "防护控制和排放";
                    row85[4] = "现场出入口和特定场所车辆、器材、人员清洗盥洗设施或设备";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row85[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row85[b] = Funs.GetNewDecimalOrZero(row85[b].ToString()).ToString("N2");
                            b++;
                            row85[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row85[b] = Funs.GetNewDecimalOrZero(row85[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row85[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row85[b] = Funs.GetNewDecimalOrZero(row85[b].ToString()).ToString("N2");
                            b++;
                            row85[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row85[b] = Funs.GetNewDecimalOrZero(row85[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row85);
                    b = 5;
                    DataRow row86 = dt.NewRow();
                    row86[0] = "5";
                    row86[1] = "文明施工和环境保护";
                    row86[2] = "文明施工和环境保护";
                    row86[3] = "防护控制和排放";
                    row86[4] = "土方覆盖遮挡与洒水及其他施工扬尘控制设施设备";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row86[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row86[b] = Funs.GetNewDecimalOrZero(row86[b].ToString()).ToString("N2");
                            b++;
                            row86[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row86[b] = Funs.GetNewDecimalOrZero(row86[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row86[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row86[b] = Funs.GetNewDecimalOrZero(row86[b].ToString()).ToString("N2");
                            b++;
                            row86[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row86[b] = Funs.GetNewDecimalOrZero(row86[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row86);
                    b = 5;
                    DataRow row87 = dt.NewRow();
                    row87[0] = "6";
                    row87[1] = "文明施工和环境保护";
                    row87[2] = "文明施工和环境保护";
                    row87[3] = "防护控制和排放";
                    row87[4] = "运输车辆及输送装置封闭或覆盖设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row87[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row87[b] = Funs.GetNewDecimalOrZero(row87[b].ToString()).ToString("N2");
                            b++;
                            row87[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row87[b] = Funs.GetNewDecimalOrZero(row87[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row87[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row87[b] = Funs.GetNewDecimalOrZero(row87[b].ToString()).ToString("N2");
                            b++;
                            row87[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row87[b] = Funs.GetNewDecimalOrZero(row87[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row87);
                    b = 5;
                    DataRow row88 = dt.NewRow();
                    row88[0] = "7";
                    row88[1] = "文明施工和环境保护";
                    row88[2] = "文明施工和环境保护";
                    row88[3] = "防护控制和排放";
                    row88[4] = "消纳施工污水的设施、措施（沟渠、槽池、管线、机泵及附属临建）";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row88[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row88[b] = Funs.GetNewDecimalOrZero(row88[b].ToString()).ToString("N2");
                            b++;
                            row88[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row88[b] = Funs.GetNewDecimalOrZero(row88[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row88[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row88[b] = Funs.GetNewDecimalOrZero(row88[b].ToString()).ToString("N2");
                            b++;
                            row88[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row88[b] = Funs.GetNewDecimalOrZero(row88[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row88);
                    b = 5;
                    DataRow row89 = dt.NewRow();
                    row89[0] = "8";
                    row89[1] = "文明施工和环境保护";
                    row89[2] = "文明施工和环境保护";
                    row89[3] = "防护控制和排放";
                    row89[4] = "易燃易爆、有毒有害、高腐蚀物质的使用保管、运输、回收过程中的安全防护设施和措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row89[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row89[b] = Funs.GetNewDecimalOrZero(row89[b].ToString()).ToString("N2");
                            b++;
                            row89[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row89[b] = Funs.GetNewDecimalOrZero(row89[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row89[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row89[b] = Funs.GetNewDecimalOrZero(row89[b].ToString()).ToString("N2");
                            b++;
                            row89[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row89[b] = Funs.GetNewDecimalOrZero(row89[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row89);
                    b = 5;
                    DataRow row90 = dt.NewRow();
                    row90[0] = "9";
                    row90[1] = "文明施工和环境保护";
                    row90[2] = "文明施工和环境保护";
                    row90[3] = "防护控制和排放";
                    row90[4] = "现场毒害物质使用、消纳及意外应急相关设施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row90[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row90[b] = Funs.GetNewDecimalOrZero(row90[b].ToString()).ToString("N2");
                            b++;
                            row90[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row90[b] = Funs.GetNewDecimalOrZero(row90[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row90[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row90[b] = Funs.GetNewDecimalOrZero(row90[b].ToString()).ToString("N2");
                            b++;
                            row90[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row90[b] = Funs.GetNewDecimalOrZero(row90[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row90);
                    b = 5;
                    DataRow row91 = dt.NewRow();
                    row91[0] = "10";
                    row91[1] = "文明施工和环境保护";
                    row91[2] = "文明施工和环境保护";
                    row91[3] = "防护控制和排放";
                    row91[4] = "危险物质特性说明书展示牌";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row91[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row91[b] = Funs.GetNewDecimalOrZero(row91[b].ToString()).ToString("N2");
                            b++;
                            row91[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row91[b] = Funs.GetNewDecimalOrZero(row91[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row91[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row91[b] = Funs.GetNewDecimalOrZero(row91[b].ToString()).ToString("N2");
                            b++;
                            row91[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row91[b] = Funs.GetNewDecimalOrZero(row91[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row91);
                    b = 5;
                    DataRow row92 = dt.NewRow();
                    row92[0] = "11";
                    row92[1] = "文明施工和环境保护";
                    row92[2] = "文明施工和环境保护";
                    row92[3] = "防护控制和排放";
                    row92[4] = "施工废弃物、生活垃圾分类存放和消纳的设施、措施";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row92[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row92[b] = Funs.GetNewDecimalOrZero(row92[b].ToString()).ToString("N2");
                            b++;
                            row92[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row92[b] = Funs.GetNewDecimalOrZero(row92[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row92[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row92[b] = Funs.GetNewDecimalOrZero(row92[b].ToString()).ToString("N2");
                            b++;
                            row92[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row92[b] = Funs.GetNewDecimalOrZero(row92[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row92);
                    b = 5;
                    DataRow row93 = dt.NewRow();
                    row93[0] = "";
                    row93[1] = "文明施工和环境保护";
                    row93[2] = "文明施工和环境保护";
                    row93[3] = "";
                    row93[4] = "费用小计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row93[b] = payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row93[b] = Funs.GetNewDecimalOrZero(row93[b].ToString()).ToString("N2");
                            b++;
                            row93[b] = yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row93[b] = Funs.GetNewDecimalOrZero(row93[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row93[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row93[b] = Funs.GetNewDecimalOrZero(row93[b].ToString()).ToString("N2");
                            b++;
                            row93[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId && x.InvestCostProject == "防护控制和排放").Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row93[b] = Funs.GetNewDecimalOrZero(row93[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row93);
                    #endregion
                    //费用累计
                    b = 5;
                    DataRow row94 = dt.NewRow();
                    row94[0] = "";
                    row94[1] = "";
                    row94[2] = "";
                    row94[3] = "";
                    row94[4] = "费用累计：";
                    for (int i = 0; i < units.Count; i++)
                    {
                        if (i == 0)   //五环
                        {
                            row94[b] = payRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_11 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_15 ?? 0) + payRegistrations.Sum(x => x.SMonthType1_16 ?? 0)
                                + payRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType2_4 ?? 0)
                                + payRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType3_6 ?? 0)
                                + payRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_11 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_12 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_15 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_18 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_23 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_24 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_25 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_26 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_32 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_33 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_37 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_39 ?? 0) + payRegistrations.Sum(x => x.SMonthType4_40 ?? 0)
                                + payRegistrations.Sum(x => x.SMonthType5_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_3 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_4 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_5 ?? 0) + payRegistrations.Sum(x => x.SMonthType5_6 ?? 0)
                                + payRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + payRegistrations.Sum(x => x.SMonthType6_3 ?? 0)
                                + payRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + payRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row94[b] = Funs.GetNewDecimalOrZero(row94[b].ToString()).ToString("N2");
                            b++;
                            row94[b] = yearPayRegistrations.Sum(x => x.SMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_11 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_15 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType1_16 ?? 0)
                                + yearPayRegistrations.Sum(x => x.SMonthType2_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType2_4 ?? 0)
                                + yearPayRegistrations.Sum(x => x.SMonthType3_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType3_6 ?? 0)
                                + yearPayRegistrations.Sum(x => x.SMonthType4_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_6 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_7 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_8 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_9 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_10 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_11 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_12 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_13 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_14 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_15 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_16 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_17 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_18 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_19 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_20 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_21 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_22 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_23 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_24 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_25 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_26 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_27 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_28 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_29 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_30 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_31 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_32 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_33 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_34 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_35 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_36 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_37 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_38 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_39 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType4_40 ?? 0)
                                + yearPayRegistrations.Sum(x => x.SMonthType5_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_3 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_4 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_5 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType5_6 ?? 0)
                                + yearPayRegistrations.Sum(x => x.SMonthType6_1 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_2 ?? 0) + yearPayRegistrations.Sum(x => x.SMonthType6_3 ?? 0)
                                + yearPayRegistrations.Sum(x => x.TMonthType1_1 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_2 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_3 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_4 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_5 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_6 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_7 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_8 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_9 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_10 ?? 0) + yearPayRegistrations.Sum(x => x.TMonthType1_11 ?? 0);
                            row94[b] = Funs.GetNewDecimalOrZero(row94[b].ToString()).ToString("N2");
                            b++;
                        }
                        else    //分包商
                        {
                            row94[b] = costManageItems.Where(x => x.UnitId == units[i].UnitId).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row94[b] = Funs.GetNewDecimalOrZero(row94[b].ToString()).ToString("N2");
                            b++;
                            row94[b] = yearCostManageItems.Where(x => x.UnitId == units[i].UnitId).Sum(x => (int?)x.AuditCounts * (decimal?)x.AuditPriceMoney);
                            row94[b] = Funs.GetNewDecimalOrZero(row94[b].ToString()).ToString("N2");
                            b++;
                        }
                    }
                    dt.Rows.Add(row94);
                    this.gvTotalPayRegistration.DataSource = dt;
                    this.gvTotalPayRegistration.DataBind();
                }
            }
        }

        protected void gvTotalPayRegistration_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DynamicTHeaderHepler dHelper = new DynamicTHeaderHepler();
                dHelper.SplitTableHeader(e.Row, headerStr);
            }
        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTotalPayRegistration_DataBound(object sender, EventArgs e)
        {
            int row1 = 1, t1 = 0, row2 = 1, t2 = 0, row0 = 1, t0 = 0;
            int row5 = 1, t5 = 0, row6 = 1, t6 = 0, row7 = 1, t7 = 0, row8 = 1, t8 = 0, row9 = 1, t9 = 0, row10 = 1, t10 = 0, row11 = 1, t11 = 0, row12 = 1, t12 = 0,
                row13 = 1, t13 = 0, row14 = 1, t14 = 0, row15 = 1, t15 = 0, row16 = 1, t16 = 0, row17 = 1, t17 = 0, row18 = 1, t18 = 0, row19 = 1, t19 = 0, row20 = 1, t20 = 0;
            for (int i = 0; i < this.gvTotalPayRegistration.Rows.Count - 1; i++)
            {
                GridViewRow gvr = this.gvTotalPayRegistration.Rows[i];
                GridViewRow gvrNext = this.gvTotalPayRegistration.Rows[i + 1];
                if (gvr.Cells[1].Text == gvrNext.Cells[1].Text)
                {
                    gvrNext.Cells[1].Visible = false;
                    row1++;
                    this.gvTotalPayRegistration.Rows[t1].Cells[1].RowSpan = row1;
                }
                else
                {
                    t1 = row1 + t1;
                    row1 = 1;
                }
                if (gvr.Cells[2].Text == gvrNext.Cells[2].Text)
                {
                    gvrNext.Cells[2].Visible = false;
                    row2++;
                    this.gvTotalPayRegistration.Rows[t2].Cells[2].RowSpan = row2;
                }
                else
                {
                    t2 = row2 + t2;
                    row2 = 1;
                }
                if (gvr.Cells[3].Text == gvrNext.Cells[3].Text)
                {
                    gvrNext.Cells[3].Visible = false;
                    row0++;
                    this.gvTotalPayRegistration.Rows[t0].Cells[3].RowSpan = row0;
                }
                else
                {
                    t0 = row0 + t0;
                    row0 = 1;
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 5)
                {
                    if (gvr.Cells[5].Text == gvrNext.Cells[5].Text)
                    {
                        gvrNext.Cells[5].Visible = false;
                        row5++;
                        this.gvTotalPayRegistration.Rows[t5].Cells[5].RowSpan = row5;
                    }
                    else
                    {
                        t5 = row5 + t5;
                        row5 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 6)
                {
                    if (gvr.Cells[6].Text == gvrNext.Cells[6].Text)
                    {
                        gvrNext.Cells[6].Visible = false;
                        row6++;
                        this.gvTotalPayRegistration.Rows[t6].Cells[6].RowSpan = row6;
                    }
                    else
                    {
                        t6 = row6 + t6;
                        row6 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 7)
                {
                    if (gvr.Cells[7].Text == gvrNext.Cells[7].Text)
                    {
                        gvrNext.Cells[7].Visible = false;
                        row7++;
                        this.gvTotalPayRegistration.Rows[t7].Cells[7].RowSpan = row7;
                    }
                    else
                    {
                        t7 = row7 + t7;
                        row7 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 8)
                {
                    if (gvr.Cells[8].Text == gvrNext.Cells[8].Text)
                    {
                        gvrNext.Cells[8].Visible = false;
                        row8++;
                        this.gvTotalPayRegistration.Rows[t8].Cells[8].RowSpan = row8;
                    }
                    else
                    {
                        t8 = row8 + t8;
                        row8 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 9)
                {
                    if (gvr.Cells[9].Text == gvrNext.Cells[9].Text)
                    {
                        gvrNext.Cells[9].Visible = false;
                        row9++;
                        this.gvTotalPayRegistration.Rows[t9].Cells[9].RowSpan = row9;
                    }
                    else
                    {
                        t9 = row9 + t9;
                        row9 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 10)
                {
                    if (gvr.Cells[10].Text == gvrNext.Cells[10].Text)
                    {
                        gvrNext.Cells[10].Visible = false;
                        row10++;
                        this.gvTotalPayRegistration.Rows[t10].Cells[10].RowSpan = row10;
                    }
                    else
                    {
                        t10 = row10 + t10;
                        row10 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 11)
                {
                    if (gvr.Cells[11].Text == gvrNext.Cells[11].Text)
                    {
                        gvrNext.Cells[11].Visible = false;
                        row11++;
                        this.gvTotalPayRegistration.Rows[t11].Cells[11].RowSpan = row11;
                    }
                    else
                    {
                        t11 = row11 + t11;
                        row11 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 12)
                {
                    if (gvr.Cells[12].Text == gvrNext.Cells[12].Text)
                    {
                        gvrNext.Cells[12].Visible = false;
                        row12++;
                        this.gvTotalPayRegistration.Rows[t12].Cells[12].RowSpan = row12;
                    }
                    else
                    {
                        t12 = row12 + t12;
                        row12 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 13)
                {
                    if (gvr.Cells[13].Text == gvrNext.Cells[13].Text)
                    {
                        gvrNext.Cells[13].Visible = false;
                        row13++;
                        this.gvTotalPayRegistration.Rows[t13].Cells[13].RowSpan = row13;
                    }
                    else
                    {
                        t13 = row13 + t13;
                        row13 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 14)
                {
                    if (gvr.Cells[14].Text == gvrNext.Cells[14].Text)
                    {
                        gvrNext.Cells[14].Visible = false;
                        row14++;
                        this.gvTotalPayRegistration.Rows[t14].Cells[14].RowSpan = row14;
                    }
                    else
                    {
                        t14 = row14 + t14;
                        row14 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 15)
                {
                    if (gvr.Cells[15].Text == gvrNext.Cells[15].Text)
                    {
                        gvrNext.Cells[15].Visible = false;
                        row15++;
                        this.gvTotalPayRegistration.Rows[t15].Cells[15].RowSpan = row15;
                    }
                    else
                    {
                        t15 = row15 + t15;
                        row15 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 16)
                {
                    if (gvr.Cells[16].Text == gvrNext.Cells[16].Text)
                    {
                        gvrNext.Cells[16].Visible = false;
                        row16++;
                        this.gvTotalPayRegistration.Rows[t16].Cells[16].RowSpan = row16;
                    }
                    else
                    {
                        t16 = row16 + t16;
                        row16 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 17)
                {
                    if (gvr.Cells[17].Text == gvrNext.Cells[17].Text)
                    {
                        gvrNext.Cells[17].Visible = false;
                        row17++;
                        this.gvTotalPayRegistration.Rows[t17].Cells[17].RowSpan = row17;
                    }
                    else
                    {
                        t17 = row17 + t17;
                        row17 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 18)
                {
                    if (gvr.Cells[18].Text == gvrNext.Cells[18].Text)
                    {
                        gvrNext.Cells[18].Visible = false;
                        row18++;
                        this.gvTotalPayRegistration.Rows[t18].Cells[18].RowSpan = row18;
                    }
                    else
                    {
                        t18 = row18 + t18;
                        row18 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 19)
                {
                    if (gvr.Cells[19].Text == gvrNext.Cells[19].Text)
                    {
                        gvrNext.Cells[19].Visible = false;
                        row19++;
                        this.gvTotalPayRegistration.Rows[t19].Cells[19].RowSpan = row19;
                    }
                    else
                    {
                        t19 = row19 + t19;
                        row19 = 1;
                    }
                }
                if (this.gvTotalPayRegistration.Rows[i].Cells.Count > 20)
                {
                    if (gvr.Cells[20].Text == gvrNext.Cells[20].Text)
                    {
                        gvrNext.Cells[20].Visible = false;
                        row20++;
                        this.gvTotalPayRegistration.Rows[t20].Cells[20].RowSpan = row20;
                    }
                    else
                    {
                        t20 = row20 + t20;
                        row20 = 1;
                    }
                }
            }
            //if (this.gvTotalPayRegistration.Rows.Count > 0)
            //{
            //    for (int i = 5; i < this.gvTotalPayRegistration.Rows[0].Cells.Count; i++)
            //    {
            //        GridViewRow gvr = this.gvTotalPayRegistration.Rows[i];
            //        GridViewRow gvrNext = this.gvTotalPayRegistration.Rows[i + 1];
            //        if (gvr.Cells[i].Text == gvrNext.Cells[i].Text)
            //        {
            //            gvrNext.Cells[i].Visible = false;
            //            rowN++;
            //            this.gvTotalPayRegistration.Rows[tN].Cells[i].RowSpan = rowN;
            //        }
            //        else
            //        {
            //            tN = rowN + tN;
            //            rowN = 1;
            //        }
            //    }
            //}
        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("项目HSE费用投入登记表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvTotalPayRegistration.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}