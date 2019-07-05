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
    public partial class DrillConductedQuarterlyReportBar : PageBase
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

                AddDatasetToSQL(ds.Tables[0], 19);
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
                for (int i = 1; i < ir; i++)
                {
                    string row1 = pds.Rows[i][0].ToString();
                    string unitId = string.Empty;
                    if (!string.IsNullOrEmpty(row1))
                    {
                        var unit = Funs.DB.Base_Unit.FirstOrDefault(x=>x.UnitName == row1.Trim()) ;
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
                            result += "第" + (i + 2).ToString() + "行," + "季度" + "," + "[" + row3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "季度" + "," + "此项为必填项！" + "|";
                    }

                    ////判断是否 已存在该季度数据
                    var drillConductedQuarterlyReport = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportByUnitIdAndYearAndQuarters(unitId, Funs.GetNewIntOrZero(row2.Trim()), Funs.GetNewIntOrZero(row3.Trim()));
                    if (drillConductedQuarterlyReport != null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "已存在该季度季报！" + "|";
                    }

                    string row5 = pds.Rows[i][4].ToString().Trim();
                    if (!string.IsNullOrEmpty(row5))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row5);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总体情况举办次数" + "," + "[" + row5 + "]错误！" + "|";
                        }
                    }
                    string row6 = pds.Rows[i][5].ToString().Trim();
                    if (!string.IsNullOrEmpty(row6))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row6);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总体情况参演人数" + "," + "[" + row6 + "]错误！" + "|";
                        }
                    }
                    string row7 = pds.Rows[i][6].ToString().Trim();
                    if (!string.IsNullOrEmpty(row7))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row7);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "总体情况直接投入" + "," + "[" + row7 + "]错误！" + "|";
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
                            result += "第" + (i + 2).ToString() + "行," + "企业总部举办次数" + "," + "[" + row8 + "]错误！" + "|";
                        }
                    }
                    string row9 = pds.Rows[i][8].ToString().Trim();
                    if (!string.IsNullOrEmpty(row9))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row9);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "企业总部参演人数" + "," + "[" + row9 + "]错误！" + "|";
                        }
                    }
                    string row10 = pds.Rows[i][9].ToString().Trim();
                    if (!string.IsNullOrEmpty(row10))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row10);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "企业总部直接投入" + "," + "[" + row10 + "]错误！" + "|";
                        }
                    }
                    string row11 = pds.Rows[i][10].ToString().Trim();
                    if (!string.IsNullOrEmpty(row11))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row11);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "基层单位举办次数" + "," + "[" + row11 + "]错误！" + "|";
                        }
                    }
                    string row12 = pds.Rows[i][11].ToString().Trim();
                    if (!string.IsNullOrEmpty(row12))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row12);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "基层单位参演人数" + "," + "[" + row12 + "]错误！" + "|";
                        }
                    }
                    string row13 = pds.Rows[i][12].ToString().Trim();
                    if (!string.IsNullOrEmpty(row13))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row13);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "基层单位直接投入" + "," + "[" + row13 + "]错误！" + "|";
                        }
                    }
                    string row14 = pds.Rows[i][13].ToString().Trim();
                    if (!string.IsNullOrEmpty(row14))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row14);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "综合演练" + "," + "[" + row14 + "]错误！" + "|";
                        }
                    }
                    string row15 = pds.Rows[i][14].ToString().Trim();
                    if (!string.IsNullOrEmpty(row15))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row15);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "综合演练其中现场" + "," + "[" + row15 + "]错误！" + "|";
                        }
                    }
                    string row16 = pds.Rows[i][15].ToString().Trim();
                    if (!string.IsNullOrEmpty(row16))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row16);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "综合演练其中桌面" + "," + "[" + row16 + "]错误！" + "|";
                        }
                    }

                    string row17 = pds.Rows[i][16].ToString().Trim();
                    if (!string.IsNullOrEmpty(row17))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row17);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "专项演练" + "," + "[" + row17 + "]错误！" + "|";
                        }
                    }
                    string row18 = pds.Rows[i][17].ToString().Trim();
                    if (!string.IsNullOrEmpty(row18))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row18);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "专项演练其中现场" + "," + "[" + row18 + "]错误！" + "|";
                        }
                    }
                    string row19 = pds.Rows[i][18].ToString().Trim();
                    if (!string.IsNullOrEmpty(row19))
                    {
                        try
                        {
                            Decimal deathAccidentFrequency = Convert.ToDecimal(row19);
                        }
                        catch (Exception)
                        {
                            result += "第" + (i + 2).ToString() + "行," + "专项演练其中桌面" + "," + "[" + row19 + "]错误！" + "|";
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