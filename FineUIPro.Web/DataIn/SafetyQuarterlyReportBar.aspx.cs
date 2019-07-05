using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace FineUIPro.Web.DataIn
{
    public partial class SafetyQuarterlyReportBar : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            string rootPath = Server.MapPath("~/");
            string fileName = rootPath + initPath + Request.Params["FileName"];
            ImportXlsToData(fileName);
        }
        #endregion

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            try
            {
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {

                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL(ds.Tables[0], 74);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集行数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds, int Cols)
        {
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "行", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {                
                for (int i = 0; i < ir; i++)
                {
                    string row1 = pds.Rows[i][0].ToString();
                    string unitId = string.Empty;
                    if (!string.IsNullOrEmpty(row1))
                    {
                        var unit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitName == row1.Trim());
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "单位名称" + "," + "[" + row1 + "]不存在！" + "|";
                        }
                        else
                        {
                            unitId = unit.UnitId;
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "单位名称" + "," + "此项为必填项！" + "|";
                    }

                    string row2 = pds.Rows[i][1].ToString();
                    if (!string.IsNullOrEmpty(row2))
                    {
                        try
                        {
                            Int32 yearId = Convert.ToInt32(row2.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "年度" + "," + "[" + row2 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "年度" + "," + "此项为必填项！" + "|";
                    }

                    string row3 = pds.Rows[i][2].ToString();
                    if (!string.IsNullOrEmpty(row3))
                    {
                        try
                        {
                            Int32 quarters = Convert.ToInt32(row3.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "季度" + "," + "[" + row3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "季度" + "," + "此项为必填项！" + "|";
                    }

                    ////判断是否 已存在该季度数据
                    var safetyQuarterlyReport = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportByUnitIdAndYearAndQuarters(unitId, Funs.GetNewIntOrZero(row2.Trim()), Funs.GetNewIntOrZero(row3.Trim()));
                    if (safetyQuarterlyReport != null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "已存在该季度季报！" + "|";
                    }

                    string row4 = pds.Rows[i][3].ToString();
                    if (!string.IsNullOrEmpty(row4))
                    {
                        try
                        {
                            Int32 totalInWorkHours = Convert.ToInt32(row4.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总投入工时数" + "," + "[" + row4 + "]错误！" + "|";
                        }
                    }
                    //string row5 = pds.Rows[i][4].ToString();
                    string row6 = pds.Rows[i][5].ToString();
                    if (!string.IsNullOrEmpty(row6))
                    {
                        try
                        {
                            Int32 totalOutWorkHours = Convert.ToInt32(row6.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总损失工时数" + "," + "[" + row6 + "]错误！" + "|";
                        }
                    }
                    //string row7 = pds.Rows[i][6].ToString();
                    string row8 = pds.Rows[i][7].ToString();
                    if (!string.IsNullOrEmpty(row8))
                    {
                        try
                        {
                            Decimal workHoursLossRate = Convert.ToDecimal(row8.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时损失率" + "," + "[" + row8 + "]错误！" + "|";
                        }
                    }
                    //string row9 = pds.Rows[i][8].ToString().Trim();
                    string row10 = pds.Rows[i][9].ToString().Trim();
                    if (!string.IsNullOrEmpty(row10))
                    {
                        try
                        {
                            Decimal workHoursAccuracy = Convert.ToDecimal(row10.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "工时统计准确率" + "," + "[" + row10 + "]错误！" + "|";
                        }
                    }
                    //string row11 = pds.Rows[i][10].ToString().Trim();
                    string row12 = pds.Rows[i][11].ToString().Trim();
                    if (!string.IsNullOrEmpty(row12))
                    {
                        try
                        {
                            Decimal mainBusinessIncome = Convert.ToDecimal(row12);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "主营业务收入/亿元" + "," + "[" + row12 + "]错误！" + "|";
                        }
                    }
                    //string row13 = pds.Rows[i][12].ToString().Trim();
                    string row14 = pds.Rows[i][13].ToString().Trim();
                    if (!string.IsNullOrEmpty(row14))
                    {
                        try
                        {
                            Decimal constructionRevenue = Convert.ToDecimal(row14);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "施工收入/亿元" + "," + "[" + row14 + "]错误！" + "|";
                        }
                    }
                    //string row15 = pds.Rows[i][14].ToString().Trim();
                    string row16 = pds.Rows[i][15].ToString().Trim();
                    if (!string.IsNullOrEmpty(row16))
                    {
                        try
                        {
                            Decimal unitTimeIncome = Convert.ToDecimal(row16);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "单位工时收入/元" + "," + "[" + row16 + "]错误！" + "|";
                        }
                    }
                    //string row17 = pds.Rows[i][16].ToString().Trim();
                    string row18 = pds.Rows[i][17].ToString().Trim();
                    if (!string.IsNullOrEmpty(row18))
                    {
                        try
                        {
                            Decimal billionsOutputMortality = Convert.ToDecimal(row18);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百亿产值死亡率" + "," + "[" + row18 + "]错误！" + "|";
                        }                        
                    }
                    //string row19 = pds.Rows[i][18].ToString().Trim();
                    string row20 = pds.Rows[i][19].ToString().Trim();
                    if (!string.IsNullOrEmpty(row20))
                    {
                        try
                        {
                            Int32 majorFireAccident = Convert.ToInt32(row20);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重大火灾事故报告数" + "," + "[" + row20 + "]错误！" + "|";
                        }
                    }
                    //string row21 = pds.Rows[i][20].ToString().Trim();
                    string row22 = pds.Rows[i][21].ToString().Trim();
                    if (!string.IsNullOrEmpty(row22))
                    {
                        try
                        {
                            Int32 majorEquipAccident = Convert.ToInt32(row22);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重大机械设备事故报告数" + "," + "[" + row22 + "]错误！" + "|";
                        }
                    }
                    //string row23 = pds.Rows[i][22].ToString().Trim();
                    string row24 = pds.Rows[i][23].ToString().Trim();
                    if (!string.IsNullOrEmpty(row24))
                    {
                        try
                        {
                            Decimal accidentFrequency = Convert.ToDecimal(row24);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "事故发生频率（占总收入之比）" + "," + "[" + row24 + "]错误！" + "|";
                        }
                    }
                    //string row25 = pds.Rows[i][24].ToString().Trim();
                    string row26 = pds.Rows[i][25].ToString().Trim();
                    if (!string.IsNullOrEmpty(row26))
                    {
                        try
                        {
                            Int32 seriousInjuryAccident = Convert.ToInt32(row26);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤以上事故报告数" + "," + "[" + row26 + "]错误！" + "|";
                        }
                    }
                    //string row27 = pds.Rows[i][26].ToString().Trim();
                    string row28 = pds.Rows[i][27].ToString().Trim();
                    if (!string.IsNullOrEmpty(row28))
                    {
                        try
                        {
                            Int32 fireAccident = Convert.ToInt32(row28);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "火灾事故统计报告数" + "," + "[" + row28 + "]错误！" + "|";
                        }
                    }
                    //string row29 = pds.Rows[i][28].ToString().Trim();
                    string row30 = pds.Rows[i][29].ToString().Trim();
                    if (!string.IsNullOrEmpty(row30))
                    {
                        try
                        {
                            Int32 equipmentAccident = Convert.ToInt32(row30);
                        }
                        catch (Exception)
                        {                            
                            result += "第" + (i + 2).ToString() + "行," + "装备事故统计报告数" + "," + "[" + row30 + "]错误！" + "|";
                        }
                    }
                    //string row31 = pds.Rows[i][30].ToString().Trim();
                    string row32 = pds.Rows[i][31].ToString().Trim();
                    if (!string.IsNullOrEmpty(row32))
                    {
                        try
                        {
                            Int32 poisoningAndInjuries = Convert.ToInt32(row32);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "中毒及职业伤害报告数" + "," + "[" + row32 + "]错误！" + "|";
                        }
                    }
                    //string row33 = pds.Rows[i][32].ToString().Trim();
                    string row34 = pds.Rows[i][33].ToString().Trim();
                    if (!string.IsNullOrEmpty(row34))
                    {
                        try
                        {
                            Int32 productionSafetyInTotal = Convert.ToInt32(row34);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全生产投入总额/元" + "," + "[" + row34 + "]错误！" + "|";
                        }
                    }
                    //string row35 = pds.Rows[i][34].ToString().Trim();
                    string row36 = pds.Rows[i][35].ToString().Trim();
                    if (!string.IsNullOrEmpty(row36))
                    {
                        try
                        {
                            Decimal protectionInput = Convert.ToDecimal(row36);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全防护投入/元" + "," + "[" + row36 + "]错误！" + "|";
                        }
                    }
                    //string row37 = pds.Rows[i][36].ToString().Trim();
                    string row38 = pds.Rows[i][37].ToString().Trim();
                    if (!string.IsNullOrEmpty(row38))
                    {
                        try
                        {
                            Decimal laboAndHealthIn = Convert.ToDecimal(row38);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "劳动保护及职业健康投入/元" + "," + "[" + row38 + "]错误！" + "|";
                        }
                    }
                    //string row39 = pds.Rows[i][38].ToString().Trim();
                    string row40 = pds.Rows[i][39].ToString().Trim();
                    if (!string.IsNullOrEmpty(row40))
                    {
                        try
                        {
                            Decimal technologyProgressIn = Convert.ToDecimal(row40);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全技术进步投入/元" + "," + "[" + row40 + "]错误！" + "|";
                        }
                    }
                    //string row41 = pds.Rows[i][40].ToString().Trim();
                    string row42 = pds.Rows[i][41].ToString().Trim();
                    if (!string.IsNullOrEmpty(row42))
                    {
                        try
                        {
                            Decimal educationTrainIn = Convert.ToDecimal(row42);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全教育培训投入/元" + "," + "[" + row42 + "]错误！" + "|";
                        }
                    }
                    //string row43 = pds.Rows[i][42].ToString().Trim();
                    string row44 = pds.Rows[i][43].ToString().Trim();
                    if (!string.IsNullOrEmpty(row44))
                    {
                        try
                        {
                            Decimal projectCostRate = Convert.ToDecimal(row44);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "工程造价占比(%)" + "," + "[" + row44 + "]错误！" + "|";
                        }
                    }
                    //string row45 = pds.Rows[i][44].ToString().Trim();
                    string row46 = pds.Rows[i][45].ToString().Trim();
                    if (!string.IsNullOrEmpty(row46))
                    {
                        try
                        {
                            Decimal productionInput = Convert.ToDecimal(row46);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时安全生产投入额/万元" + "," + "[" + row46 + "]错误！" + "|";
                        }
                    }
                    //string row47 = pds.Rows[i][46].ToString().Trim();
                    string row48 = pds.Rows[i][47].ToString().Trim();
                    if (!string.IsNullOrEmpty(row48))
                    {
                        try
                        {
                            Decimal revenue = Convert.ToDecimal(row48);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全生产投入占施工收入之比" + "," + "[" + row48 + "]错误！" + "|";
                        }
                    }
                    //string row49 = pds.Rows[i][48].ToString().Trim();
                    string row50 = pds.Rows[i][49].ToString().Trim();
                    if (!string.IsNullOrEmpty(row50))
                    {
                        try
                        {
                            Int32 fullTimeMan = Convert.ToInt32(row50);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全专职人员总数" + "," + "[" + row50 + "]错误！" + "|";
                        }
                    }
                    //string row51 = pds.Rows[i][50].ToString().Trim();
                    string row52 = pds.Rows[i][51].ToString().Trim();
                    if (!string.IsNullOrEmpty(row52))
                    {
                        try
                        {
                            Int32 pMMan = Convert.ToInt32(row52);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "项目经理人员总数" + "," + "[" + row52 + "]错误！" + "|";
                        }
                    }
                    //string row53 = pds.Rows[i][52].ToString().Trim();
                    string row54 = pds.Rows[i][53].ToString().Trim();
                    if (!string.IsNullOrEmpty(row54))
                    {
                        try
                        {
                            Int32 corporateDirectorEdu = Convert.ToInt32(row54);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "企业负责人安全生产继续教育数" + "," + "[" + row54 + "]错误！" + "|";
                        }
                    }
                    //string row55 = pds.Rows[i][54].ToString().Trim();
                    string row56 = pds.Rows[i][55].ToString().Trim();
                    if (!string.IsNullOrEmpty(row56))
                    {
                        try
                        {
                            Int32 projectLeaderEdu = Convert.ToInt32(row56);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "项目负责人安全生产继续教育数" + "," + "[" + row56 + "]错误！" + "|";
                        }
                    }
                    //string row57 = pds.Rows[i][56].ToString().Trim();
                    string row58 = pds.Rows[i][57].ToString().Trim();
                    if (!string.IsNullOrEmpty(row58))
                    {
                        try
                        {
                            Int32 fullTimeEdu = Convert.ToInt32(row58);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全专职人员安全生产继续教育数" + "," + "[" + row58 + "]错误！" + "|";
                        }
                    }
                    //string row59 = pds.Rows[i][58].ToString().Trim();
                    string row60 = pds.Rows[i][59].ToString().Trim();
                    if (!string.IsNullOrEmpty(row60))
                    {
                        try
                        {
                            Decimal threeKidsEduRate = Convert.ToDecimal(row60);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "安全生产三类人员继续教育覆盖率" + "," + "[" + row60 + "]错误！" + "|";
                        }
                    }
                    //string row61 = pds.Rows[i][60].ToString().Trim();
                    string row62 = pds.Rows[i][61].ToString().Trim();
                    if (!string.IsNullOrEmpty(row62))
                    {
                        try
                        {
                            Decimal uplinReportRate = Convert.ToDecimal(row62);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "上行报告履行率" + "," + "[" + row62 + "]错误！" + "|";
                        }
                    }
                    //string row63 = pds.Rows[i][62].ToString().Trim();
                    string row64 = pds.Rows[i][63].ToString().Trim();
                    if (!string.IsNullOrEmpty(row64))
                    {
                        try
                        {
                            Int32 keyEquipmentTotal = Convert.ToInt32(row64);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重点装备总数" + "," + "[" + row64 + "]错误！" + "|";
                        }
                    }
                    //string row65 = pds.Rows[i][64].ToString().Trim();
                    string row66 = pds.Rows[i][65].ToString().Trim();
                    if (!string.IsNullOrEmpty(row66))
                    {
                        try
                        {
                            Int32 keyEquipmentReportCount = Convert.ToInt32(row66);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重点装备安全控制检查报告数" + "," + "[" + row66 + "]错误！" + "|";
                        }
                    }
                    //string row67 = pds.Rows[i][66].ToString().Trim();
                    string row68 = pds.Rows[i][67].ToString().Trim();
                    if (!string.IsNullOrEmpty(row68))
                    {
                        try
                        {
                            Int32 chemicalAreaProjectCount = Convert.ToInt32(row68);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "化工界区施工作业项目数" + "," + "[" + row68 + "]错误！" + "|";
                        }
                    }
                    //string row69 = pds.Rows[i][68].ToString().Trim();
                    string row70 = pds.Rows[i][69].ToString().Trim();
                    if (!string.IsNullOrEmpty(row70))
                    {
                        try
                        {
                            Int32 harmfulMediumCoverCount = Convert.ToInt32(row70);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "化工界区施工作业有害介质检测复测覆盖数" + "," + "[" + row70 + "]错误！" + "|";
                        }
                    }
                    //string row71 = pds.Rows[i][70].ToString().Trim();
                    string row72 = pds.Rows[i][71].ToString().Trim();
                    if (!string.IsNullOrEmpty(row72))
                    {
                        try
                        {
                            Decimal harmfulMediumCoverRate = Convert.ToDecimal(row72);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "施工作业安全技术交底覆盖率（%）" + "," + "[" + row72 + "]错误！" + "|";
                        }                        
                    }
                    //string row73 = pds.Rows[i][72].ToString().Trim();
                    //string row74 = pds.Rows[i][73].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                    ShowNotify(result, MessageBoxIcon.Warning);
                    //Session["errorInfos"] = result;
                }
                else
                {
                    ShowNotify("审核完成,请点击导入！", MessageBoxIcon.Success);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                ShowNotify("导入数据为空！", MessageBoxIcon.Warning);
            }
            return true;
        }
        #endregion
    }
}