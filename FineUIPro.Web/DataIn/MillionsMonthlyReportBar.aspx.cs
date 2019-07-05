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
    public partial class MillionsMonthlyReportBar : PageBase
    { 
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!string.IsNullOrEmpty(fileName))
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

                    AddDatasetToSQL(ds.Tables[0], 38);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                ShowNotify("请选择要导入的文件", MessageBoxIcon.Warning);
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
                var units = from x in Funs.DB.Base_Unit select x;
                for (int i = 0; i < ir; i++)
                {
                    string row1 = pds.Rows[i][0].ToString();
                    string unitId = string.Empty;
                    if (!string.IsNullOrEmpty(row1))
                    {
                        var unit = units.FirstOrDefault(x => x.UnitName == row1.Trim());
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
                            Int32 year = Convert.ToInt32(row2.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "年份" + "," + "[" + row2 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "年份" + "," + "此项为必填项！" + "|";
                    }

                    string row3 = pds.Rows[i][2].ToString();
                    if (!string.IsNullOrEmpty(row3))
                    {
                        try
                        {
                            Int32 month = Convert.ToInt32(row3.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "月份" + "," + "[" + row3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "月份" + "," + "此项为必填项！" + "|";
                    }

                    ////判断是否 已存在该月份数据
                    var millionsMonthlyReport = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByUnitIdAndYearAndMonth(unitId, Funs.GetNewIntOrZero(row2.Trim()), Funs.GetNewIntOrZero(row3.Trim()));
                    if (millionsMonthlyReport != null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "已存在该月份月报！" + "|";
                    }

                    string row4 = pds.Rows[i][3].ToString();
                    if (string.IsNullOrEmpty(row4))
                    {
                        result += "第" + (i + 2).ToString() + "行," + "负责人" + "," + "此项为必填项！" + "|";
                    }
                    string row5 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(row5))
                    {
                        try
                        {
                            Decimal recordableIncidentRate = Convert.ToDecimal(row5);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时总可记录事故率" + "," + "[" + row5 + "]错误！" + "|";
                        }
                    }
                    string row6 = pds.Rows[i][5].ToString().Trim();
                    if (!string.IsNullOrEmpty(row6))
                    {
                        try
                        {
                            Decimal lostTimeRate = Convert.ToDecimal(row6);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时损失工时率" + "," + "[" + row6 + "]错误！" + "|";
                        }
                    }
                    string row7 = pds.Rows[i][6].ToString().Trim();
                    if (!string.IsNullOrEmpty(row7))
                    {
                        try
                        {
                            Decimal lostTimeInjuryRate = Convert.ToDecimal(row7);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时损失工时伤害事故率" + "," + "[" + row7 + "]错误！" + "|";
                        }
                    }
                    string row8 = pds.Rows[i][7].ToString().Trim();
                    if (!string.IsNullOrEmpty(row8))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row8);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时死亡事故频率" + "," + "[" + row8 + "]错误！" + "|";
                        }
                    }
                    string row9 = pds.Rows[i][8].ToString().Trim();
                    if (!string.IsNullOrEmpty(row9))
                    {
                        try
                        {
                            Decimal accidentMortality = Convert.ToDecimal(row9);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "百万工时事故死亡率" + "," + "[" + row9 + "]错误！" + "|";
                        }
                    }
                    //string row10 = pds.Rows[i][9].ToString().Trim();
                    //string row11 = pds.Rows[i][10].ToString().Trim();
                    string row12 = pds.Rows[i][11].ToString().Trim();
                    if (!string.IsNullOrEmpty(row12))
                    {
                        try
                        {
                            Int32 postPersonNum = Convert.ToInt32(row12);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "在岗员工数" + "," + "[" + row12 + "]错误！" + "|";
                        }
                    }
                    string row13 = pds.Rows[i][12].ToString().Trim();
                    if (!string.IsNullOrEmpty(row13))
                    {
                        try
                        {
                            Int32 snapPersonNum = Convert.ToInt32(row13);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "临时员工数" + "," + "[" + row13 + "]错误！" + "|";
                        }
                    }
                    string row14 = pds.Rows[i][13].ToString().Trim();
                    if (!string.IsNullOrEmpty(row14))
                    {
                        try
                        {
                            Int32 contractorNum = Convert.ToInt32(row14);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "承包商数量" + "," + "[" + row14 + "]错误！" + "|";
                        }
                    }
                    string row15 = pds.Rows[i][14].ToString().Trim();
                    if (!string.IsNullOrEmpty(row15))
                    {
                        try
                        {
                            Int32 sumPersonNum = Convert.ToInt32(row15);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "员工总数合计" + "," + "[" + row15 + "]错误！" + "|";
                        }
                    }
                    string row16 = pds.Rows[i][15].ToString().Trim();
                    if (!string.IsNullOrEmpty(row16))
                    {
                        try
                        {
                            Decimal totalWorkNum = Convert.ToDecimal(row16);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总工时数（万）" + "," + "[" + row16 + "]错误！" + "|";
                        }
                    }
                    string row17 = pds.Rows[i][16].ToString().Trim();
                    if (!string.IsNullOrEmpty(row17))
                    {
                        try
                        {
                            Int32 seriousInjuriesNum = Convert.ToInt32(row17);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤事故起数" + "," + "[" + row17 + "]错误！" + "|";
                        }
                    }
                    string row18 = pds.Rows[i][17].ToString().Trim();
                    if (!string.IsNullOrEmpty(row18))
                    {
                        try
                        {
                            Int32 seriousInjuriesPersonNum = Convert.ToInt32(row18);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤事故人数" + "," + "[" + row18 + "]错误！" + "|";
                        }
                    }
                    string row19 = pds.Rows[i][18].ToString().Trim();
                    if (!string.IsNullOrEmpty(row19))
                    {
                        try
                        {
                            Int32 seriousInjuriesLossHour = Convert.ToInt32(row19);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤事故损失工时" + "," + "[" + row19 + "]错误！" + "|";
                        }
                    }
                    string row20 = pds.Rows[i][19].ToString().Trim();
                    if (!string.IsNullOrEmpty(row20))
                    {
                        try
                        {
                            Int32 minorAccidentNum = Convert.ToInt32(row20);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "轻伤事故起数" + "," + "[" + row20 + "]错误！" + "|";
                        }
                    }
                    string row21 = pds.Rows[i][20].ToString().Trim();
                    if (!string.IsNullOrEmpty(row21))
                    {
                        try
                        {
                            Int32 minorAccidentPersonNum = Convert.ToInt32(row21);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "轻伤事故人数" + "," + "[" + row21 + "]错误！" + "|";
                        }
                    }
                    string row22 = pds.Rows[i][21].ToString().Trim();
                    if (!string.IsNullOrEmpty(row22))
                    {
                        try
                        {
                            Int32 minorAccidentLossHour = Convert.ToInt32(row22);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "轻伤事故损失工时" + "," + "[" + row22 + "]错误！" + "|";
                        }
                    }
                    string row23 = pds.Rows[i][22].ToString().Trim();
                    if (!string.IsNullOrEmpty(row23))
                    {
                        try
                        {
                            Int32 otherAccidentNum = Convert.ToInt32(row23);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其它事故起数" + "," + "[" + row23 + "]错误！" + "|";
                        }
                    }
                    string row24 = pds.Rows[i][23].ToString().Trim();
                    if (!string.IsNullOrEmpty(row24))
                    {
                        try
                        {
                            Int32 otherAccidentPersonNum = Convert.ToInt32(row24);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其它事故人数" + "," + "[" + row24 + "]错误！" + "|";
                        }
                    }
                    string row25 = pds.Rows[i][24].ToString().Trim();
                    if (!string.IsNullOrEmpty(row25))
                    {
                        try
                        {
                            Int32 otherAccidentLossHour = Convert.ToInt32(row25);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其它事故损失工时" + "," + "[" + row25 + "]错误！" + "|";
                        }
                    }
                    string row26 = pds.Rows[i][25].ToString().Trim();
                    if (!string.IsNullOrEmpty(row26))
                    {
                        try
                        {
                            Int32 restrictedWorkPersonNum = Convert.ToInt32(row26);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "工作受限人数" + "," + "[" + row26 + "]错误！" + "|";
                        }
                    }
                    string row27 = pds.Rows[i][26].ToString().Trim();
                    if (!string.IsNullOrEmpty(row27))
                    {
                        try
                        {
                            Int32 restrictedWorkLossHour = Convert.ToInt32(row27);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "工作受限损失工时" + "," + "[" + row27 + "]错误！" + "|";
                        }
                    }
                    string row28 = pds.Rows[i][27].ToString().Trim();
                    if (!string.IsNullOrEmpty(row28))
                    {
                        try
                        {
                            Int32 medicalTreatmentPersonNum = Convert.ToInt32(row28);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "医疗处置人数" + "," + "[" + row28 + "]错误！" + "|";
                        }
                    }
                    string row29 = pds.Rows[i][28].ToString().Trim();
                    if (!string.IsNullOrEmpty(row29))
                    {
                        try
                        {
                            Int32 medicalTreatmentLossHour = Convert.ToInt32(row29);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "医疗处置损失工时" + "," + "[" + row29 + "]错误！" + "|";
                        }
                    }
                    string row30 = pds.Rows[i][29].ToString().Trim();
                    if (!string.IsNullOrEmpty(row30))
                    {
                        try
                        {
                            Int32 fireNum = Convert.ToInt32(row30);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "火灾起数" + "," + "[" + row30 + "]错误！" + "|";
                        }
                    }
                    string row31 = pds.Rows[i][30].ToString().Trim();
                    if (!string.IsNullOrEmpty(row31))
                    {
                        try
                        {
                            Int32 explosionNum = Convert.ToInt32(row31);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "爆炸起数" + "," + "[" + row31 + "]错误！" + "|";
                        }
                    }
                    string row32 = pds.Rows[i][31].ToString().Trim();
                    if (!string.IsNullOrEmpty(row32))
                    {
                        try
                        {
                            Int32 trafficNum = Convert.ToInt32(row32);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "交通起数" + "," + "[" + row32 + "]错误！" + "|";
                        }
                    }
                    string row33 = pds.Rows[i][32].ToString().Trim();
                    if (!string.IsNullOrEmpty(row33))
                    {
                        try
                        {
                            Int32 equipmentNum = Convert.ToInt32(row33);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "机械设备起数" + "," + "[" + row33 + "]错误！" + "|";
                        }
                    }
                    string row34 = pds.Rows[i][33].ToString().Trim();
                    if (!string.IsNullOrEmpty(row34))
                    {
                        try
                        {
                            Int32 qualityNum = Convert.ToInt32(row34);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "质量起数" + "," + "[" + row34 + "]错误！" + "|";
                        }
                    }
                    string row35 = pds.Rows[i][34].ToString().Trim();
                    if (!string.IsNullOrEmpty(row35))
                    {
                        try
                        {
                            Int32 otherNum = Convert.ToInt32(row35);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其它起数" + "," + "[" + row35 + "]错误！" + "|";
                        }
                    }
                    string row36 = pds.Rows[i][35].ToString().Trim();
                    if (!string.IsNullOrEmpty(row36))
                    {
                        try
                        {
                            Int32 firstAidDressingsNum = Convert.ToInt32(row36);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "急救包扎起数" + "," + "[" + row36 + "]错误！" + "|";
                        }
                    }
                    string row37 = pds.Rows[i][36].ToString().Trim();
                    if (!string.IsNullOrEmpty(row37))
                    {
                        try
                        {
                            Int32 attemptedEventNum = Convert.ToInt32(row37);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "未遂事件起数" + "," + "[" + row37 + "]错误！" + "|";
                        }
                    }
                    string row38 = pds.Rows[i][37].ToString().Trim();
                    if (!string.IsNullOrEmpty(row38))
                    {
                        try
                        {
                            Int32 lossDayNum = Convert.ToInt32(row38);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "损失工日" + "," + "[" + row38 + "]错误！" + "|";
                        }
                    }                    
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