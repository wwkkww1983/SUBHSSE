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
    public partial class AccidentCauseReportBar : PageBase
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
            if (!IsPostBack)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = rootPath + initPath + Request.Params["FileName"];
                ImportXlsToData(fileName);
            }
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

                AddDatasetToSQL(ds.Tables[0], 54);
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
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "行",MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {                
                var accidentTypes = from x in Funs.DB.Sys_Const where x.GroupId == "0012" select x;
                for (int i = 0; i < ir; i++)
                {
                    string row1 = pds.Rows[i][0].ToString();
                    string unitId = string.Empty;
                    if (!string.IsNullOrEmpty(row1))
                    {
                        var unit = Funs.DB.Base_Unit.FirstOrDefault(x=>x.UnitName == row1.Trim());
                        if (unit == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "填报单位" + "," + "[" + row1 + "]不存在！" + "|";
                        }
                        else
                        {
                            unitId = unit.UnitId;
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "填报单位" + "," + "此项为必填项！" + "|";
                    }
                    string row2 = pds.Rows[i][1].ToString();
                    if (string.IsNullOrEmpty(row2))
                    {
                         result += "第" + (i + 2).ToString() + "行," + "编号" + "," + "此项为必填项！" + "|";
                    }
                    string row3 = pds.Rows[i][2].ToString();
                    if (!string.IsNullOrEmpty(row3))
                    {
                        try
                        {
                            Int32 year = Convert.ToInt32(row3.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "年份" + "," + "[" + row3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "年份" + "," + "此项为必填项！" + "|";
                    }
                    string row4 = pds.Rows[i][3].ToString();
                    if (!string.IsNullOrEmpty(row4))
                    {
                        try
                        {
                            Int32 month = Convert.ToInt32(row4.Trim());
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "月份" + "," + "[" + row4 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "月份" + "," + "此项为必填项！" + "|";
                    }

                    ////判断是否 已存在该月份数据
                    var accidentCauseReport = BLL.AccidentCauseReportService.GetAccidentCauseReportByUnitIdAndYearAndMonth(unitId, Funs.GetNewIntOrZero(row3.Trim()), Funs.GetNewIntOrZero(row4.Trim()));
                    if (accidentCauseReport != null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "已存在该月份月报！" + "|";
                    }

                    string row5 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(row5))
                    {
                        try
                        {
                            Int32 deathAccident = Convert.ToInt32(row5);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "死亡事故数" + "," + "[" + row5 + "]错误！" + "|";
                        }
                    }
                    string row6 = pds.Rows[i][5].ToString().Trim();
                    if (!string.IsNullOrEmpty(row6))
                    {
                        try
                        {
                            Int32 deathToll = Convert.ToInt32(row6);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "死亡人数" + "," + "[" + row6 + "]错误！" + "|";
                        }
                    }
                    string row7 = pds.Rows[i][6].ToString().Trim();
                    if (!string.IsNullOrEmpty(row7))
                    {
                        try
                        {
                            Int32 injuredAccident = Convert.ToInt32(row7);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤事故" + "," + "[" + row7 + "]错误！" + "|";
                        }
                    }
                    string row8 = pds.Rows[i][7].ToString().Trim();
                    if (!string.IsNullOrEmpty(row8))
                    {
                        try
                        {
                            Int32 injuredToll = Convert.ToInt32(row8);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "重伤人数" + "," + "[" + row8 + "]错误！" + "|";
                        }
                    }
                    string row9 = pds.Rows[i][8].ToString().Trim();
                    if (!string.IsNullOrEmpty(row9))
                    {
                        try
                        {
                            Int32 minorWoundAccident = Convert.ToInt32(row9);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "轻伤事故" + "," + "[" + row9 + "]错误！" + "|";
                        }
                    }
                    string row10 = pds.Rows[i][9].ToString().Trim();
                    if (!string.IsNullOrEmpty(row10))
                    {
                        try
                        {
                            Int32 minorWoundToll = Convert.ToInt32(row10);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "轻伤人数" + "," + "[" + row10 + "]错误！" + "|";
                        }
                    }
                    string row11 = pds.Rows[i][10].ToString().Trim();
                    if (!string.IsNullOrEmpty(row11))
                    {
                        try
                        {
                            Decimal averageTotalHours = Funs.GetNewDecimalOrZero(row11);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "平均工时总数" + "," + "[" + row11 + "]错误！" + "|";
                        }
                    }
                    string row12 = pds.Rows[i][11].ToString().Trim();
                    if (!string.IsNullOrEmpty(row12))
                    {
                        try
                        {
                            Int32 averageManHours = Convert.ToInt32(row12);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "平均工时人数" + "," + "[" + row12 + "]错误！" + "|";
                        }
                    }
                    string row13 = pds.Rows[i][12].ToString().Trim();
                    if (!string.IsNullOrEmpty(row13))
                    {
                        try
                        {
                            Int32 totalLossMan = Convert.ToInt32(row13);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "损失工时总数" + "," + "[" + row13 + "]错误！" + "|";
                        }
                    }
                    string row14 = pds.Rows[i][13].ToString().Trim();
                    if (!string.IsNullOrEmpty(row14))
                    {
                        try
                        {
                            Int32 lastMonthLossHoursTotal = Convert.ToInt32(row14);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "上月损失工时总数" + "," + "[" + row14 + "]错误！" + "|";
                        }
                    }
                    string row15 = pds.Rows[i][14].ToString().Trim();
                    if (!string.IsNullOrEmpty(row15))
                    {
                        try
                        {
                            Int32 knockOffTotal = Convert.ToInt32(row15);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "歇工总日数" + "," + "[" + row15 + "]错误！" + "|";
                        }
                    }
                    string row16 = pds.Rows[i][15].ToString().Trim();
                    if (!string.IsNullOrEmpty(row16))
                    {
                        try
                        {
                            Int32 directLoss = Convert.ToInt32(row16);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "直接损失" + "," + "[" + row16 + "]错误！" + "|";
                        }
                    }
                    string row17 = pds.Rows[i][16].ToString().Trim();
                    if (!string.IsNullOrEmpty(row17))
                    {
                        try
                        {
                            Int32 indirectLosses = Convert.ToInt32(row17);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "间接损失" + "," + "[" + row17 + "]错误！" + "|";
                        }
                    }
                    string row18 = pds.Rows[i][17].ToString().Trim();
                    if (!string.IsNullOrEmpty(row18))
                    {
                        try
                        {
                            Int32 totalLoss = Convert.ToInt32(row18);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总损失" + "," + "[" + row18 + "]错误！" + "|";
                        }
                    }
                    string row19 = pds.Rows[i][18].ToString().Trim();
                    if (!string.IsNullOrEmpty(row19))
                    {
                        try
                        {
                            Int32 totalLossTime = Convert.ToInt32(row19);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "无损失工时总数" + "," + "[" + row19 + "]错误！" + "|";
                        }
                    }
                    //string row20 = pds.Rows[i][19].ToString().Trim();                    
                    string row21 = pds.Rows[i][20].ToString().Trim();
                    if (!string.IsNullOrEmpty(row21))
                    {
                        if (accidentTypes.Where(x => x.ConstValue == row21.Trim()).FirstOrDefault() == null)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "事故类别" + "," + "[" + row21 + "]不存在！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "事故类别" + "," + "此项为必填项！" + "|";
                    }
                    string row22 = pds.Rows[i][21].ToString().Trim();
                    if (!string.IsNullOrEmpty(row22))
                    {
                        try
                        {
                            Int32 death1 = Convert.ToInt32(row22);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "防护保险信号缺失死亡数" + "," + "[" + row22 + "]错误！" + "|";
                        }
                    }
                    string row23 = pds.Rows[i][22].ToString().Trim();
                    if (!string.IsNullOrEmpty(row23))
                    {
                        try
                        {
                            Int32 injuries1 = Convert.ToInt32(row23);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "防护保险信号缺失重伤数" + "," + "[" + row23 + "]错误！" + "|";
                        }
                    }
                    string row24 = pds.Rows[i][23].ToString().Trim();
                    if (!string.IsNullOrEmpty(row24))
                    {
                        try
                        {
                            Int32 minorInjuries1 = Convert.ToInt32(row24);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "防护保险信号缺失轻伤数" + "," + "[" + row24 + "]错误！" + "|";
                        }
                    }
                    string row25 = pds.Rows[i][24].ToString().Trim();
                    if (!string.IsNullOrEmpty(row25))
                    {
                        try
                        {
                            Int32 death2 = Convert.ToInt32(row25);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设备工具缺陷死亡数" + "," + "[" + row25 + "]错误！" + "|";
                        }
                    }
                    string row26 = pds.Rows[i][25].ToString().Trim();
                    if (!string.IsNullOrEmpty(row26))
                    {
                        try
                        {
                            Int32 injuries2 = Convert.ToInt32(row26);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设备工具缺陷重伤数" + "," + "[" + row26 + "]错误！" + "|";
                        }
                    }
                    string row27 = pds.Rows[i][26].ToString().Trim();
                    if (!string.IsNullOrEmpty(row27))
                    {
                        try
                        {
                            Int32 minorInjuries2 = Convert.ToInt32(row27);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设备工具缺陷轻伤数" + "," + "[" + row27 + "]错误！" + "|";
                        }
                    }
                    string row28 = pds.Rows[i][27].ToString().Trim();
                    if (!string.IsNullOrEmpty(row28))
                    {
                        try
                        {
                            Int32 death3 = Convert.ToInt32(row28);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "个人防护缺陷死亡数" + "," + "[" + row28 + "]错误！" + "|";
                        }
                    }
                    string row29 = pds.Rows[i][28].ToString().Trim();
                    if (!string.IsNullOrEmpty(row29))
                    {
                        try
                        {
                            Int32 injuries3 = Convert.ToInt32(row29);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "个人防护缺陷重伤数" + "," + "[" + row29 + "]错误！" + "|";
                        }
                    }
                    string row30 = pds.Rows[i][29].ToString().Trim();
                    if (!string.IsNullOrEmpty(row30))
                    {
                        try
                        {
                            Int32 minorInjuries3 = Convert.ToInt32(row30);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "个人防护缺陷轻伤数" + "," + "[" + row30 + "]错误！" + "|";
                        }
                    }
                    string row31 = pds.Rows[i][30].ToString().Trim();
                    if (!string.IsNullOrEmpty(row31))
                    {
                        try
                        {
                            Int32 death4 = Convert.ToInt32(row31);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "光线不足死亡数" + "," + "[" + row31 + "]错误！" + "|";
                        }
                    }
                    string row32 = pds.Rows[i][31].ToString().Trim();
                    if (!string.IsNullOrEmpty(row32))
                    {
                        try
                        {
                            Int32 injuries4 = Convert.ToInt32(row32);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "光线不足重伤数" + "," + "[" + row32 + "]错误！" + "|";
                        }
                    }
                    string row33 = pds.Rows[i][32].ToString().Trim();
                    if (!string.IsNullOrEmpty(row33))
                    {
                        try
                        {
                            Int32 minorInjuries4 = Convert.ToInt32(row33);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "光线不足轻伤数" + "," + "[" + row33 + "]错误！" + "|";
                        }
                    }
                    string row34 = pds.Rows[i][33].ToString().Trim();
                    if (!string.IsNullOrEmpty(row34))
                    {
                        try
                        {
                            Int32 death5 = Convert.ToInt32(row34);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "劳动组织不合理死亡数" + "," + "[" + row34 + "]错误！" + "|";
                        }
                    }
                    string row35 = pds.Rows[i][34].ToString().Trim();
                    if (!string.IsNullOrEmpty(row35))
                    {
                        try
                        {
                            Int32 injuries5 = Convert.ToInt32(row35);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "劳动组织不合理重伤数" + "," + "[" + row35 + "]错误！" + "|";
                        }
                    }
                    string row36 = pds.Rows[i][35].ToString().Trim();
                    if (!string.IsNullOrEmpty(row36))
                    {
                        try
                        {
                            Int32 minorInjuries5 = Convert.ToInt32(row36);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "劳动组织不合理轻伤数" + "," + "[" + row36 + "]错误！" + "|";
                        }
                    }
                    string row37 = pds.Rows[i][36].ToString().Trim();
                    if (!string.IsNullOrEmpty(row37))
                    {
                        try
                        {
                            Int32 death6 = Convert.ToInt32(row37);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "现场指导错误死亡数" + "," + "[" + row37 + "]错误！" + "|";
                        }
                    }
                    string row38 = pds.Rows[i][37].ToString().Trim();
                    if (!string.IsNullOrEmpty(row38))
                    {
                        try
                        {
                            Int32 injuries6 = Convert.ToInt32(row38);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "现场指导错误重伤数" + "," + "[" + row38 + "]错误！" + "|";
                        }
                    }
                    string row39 = pds.Rows[i][38].ToString().Trim();
                    if (!string.IsNullOrEmpty(row39))
                    {
                        try
                        {
                            Int32 minorInjuries6 = Convert.ToInt32(row39);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "现场指导错误轻伤数" + "," + "[" + row39 + "]错误！" + "|";
                        }
                    }
                    string row40 = pds.Rows[i][39].ToString().Trim();
                    if (!string.IsNullOrEmpty(row40))
                    {
                        try
                        {
                            Int32 death7 = Convert.ToInt32(row40);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设计有缺陷死亡数" + "," + "[" + row40 + "]错误！" + "|";
                        }
                    }
                    string row41 = pds.Rows[i][40].ToString().Trim();
                    if (!string.IsNullOrEmpty(row41))
                    {
                        try
                        {
                            Int32 injuries7 = Convert.ToInt32(row41);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设计有缺陷重伤数" + "," + "[" + row41 + "]错误！" + "|";
                        }
                    }
                    string row42 = pds.Rows[i][41].ToString().Trim();
                    if (!string.IsNullOrEmpty(row42))
                    {
                        try
                        {
                            Int32 minorInjuries7 = Convert.ToInt32(row42);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "设计有缺陷轻伤数" + "," + "[" + row42 + "]错误！" + "|";
                        }
                    }
                    string row43 = pds.Rows[i][42].ToString().Trim();
                    if (!string.IsNullOrEmpty(row43))
                    {
                        try
                        {
                            Int32 death8 = Convert.ToInt32(row43);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "不懂操作死亡数" + "," + "[" + row43 + "]错误！" + "|";
                        }
                    }
                    string row44 = pds.Rows[i][43].ToString().Trim();
                    if (!string.IsNullOrEmpty(row44))
                    {
                        try
                        {
                            Int32 injuries8 = Convert.ToInt32(row44);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "不懂操作重伤数" + "," + "[" + row44 + "]错误！" + "|";
                        }
                    }
                    string row45 = pds.Rows[i][44].ToString().Trim();
                    if (!string.IsNullOrEmpty(row45))
                    {
                        try
                        {
                            Int32 minorInjuries8 = Convert.ToInt32(row45);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "不懂操作轻伤数" + "," + "[" + row45 + "]错误！" + "|";
                        }
                    }
                    string row46 = pds.Rows[i][45].ToString().Trim();
                    if (!string.IsNullOrEmpty(row46))
                    {
                        try
                        {
                            Int32 death9 = Convert.ToInt32(row46);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "违反操作死亡数" + "," + "[" + row46 + "]错误！" + "|";
                        }
                    }
                    string row47 = pds.Rows[i][46].ToString().Trim();
                    if (!string.IsNullOrEmpty(row47))
                    {
                        try
                        {
                            Int32 injuries9 = Convert.ToInt32(row47);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "违反操作重伤数" + "," + "[" + row47 + "]错误！" + "|";
                        }
                    }
                    string row48 = pds.Rows[i][47].ToString().Trim();
                    if (!string.IsNullOrEmpty(row48))
                    {
                        try
                        {
                            Int32 minorInjuries9 = Convert.ToInt32(row48);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "违反操作轻伤数" + "," + "[" + row48 + "]错误！" + "|";
                        }
                    }
                    string row49 = pds.Rows[i][48].ToString().Trim();
                    if (!string.IsNullOrEmpty(row49))
                    {
                        try
                        {
                            Int32 death10 = Convert.ToInt32(row49);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "没有安全操作死亡数" + "," + "[" + row49 + "]错误！" + "|";
                        }
                    }
                    string row50 = pds.Rows[i][49].ToString().Trim();
                    if (!string.IsNullOrEmpty(row50))
                    {
                        try
                        {
                            Int32 injuries10 = Convert.ToInt32(row50);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "没有安全操作重伤数" + "," + "[" + row50 + "]错误！" + "|";
                        }
                    }
                    string row51 = pds.Rows[i][50].ToString().Trim();
                    if (!string.IsNullOrEmpty(row51))
                    {
                        try
                        {
                            Int32 minorInjuries10 = Convert.ToInt32(row51);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "没有安全操作轻伤数" + "," + "[" + row51 + "]错误！" + "|";
                        }
                    }
                    string row52 = pds.Rows[i][51].ToString().Trim();
                    if (!string.IsNullOrEmpty(row52))
                    {
                        try
                        {
                            Int32 death11 = Convert.ToInt32(row52);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其他死亡数" + "," + "[" + row52 + "]错误！" + "|";
                        }
                    }
                    string row53 = pds.Rows[i][52].ToString().Trim();
                    if (!string.IsNullOrEmpty(row53))
                    {
                        try
                        {
                            Int32 injuries11 = Convert.ToInt32(row53);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其他重伤数" + "," + "[" + row53 + "]错误！" + "|";
                        }
                    }
                    string row54 = pds.Rows[i][53].ToString().Trim();
                    if (!string.IsNullOrEmpty(row54))
                    {
                        try
                        {
                            Int32 minorInjuries11 = Convert.ToInt32(row54);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "其他轻伤数" + "," + "[" + row54 + "]错误！" + "|";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                    ShowNotify(result, MessageBoxIcon.Warning);
                    Session["errorInfos"] = result;
                }
                else
                {
                    Session["errorInfos"] = null;
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