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
    public partial class DrillPlanHalfYearReportBar : PageBase
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

                AddDatasetToSQL(ds.Tables[0], 9);
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
                            result += "第" + (i + 2).ToString() + "行," + "半年度" + "," + "[" + row3 + "]错误！" + "|";
                        }
                    }
                    else
                    {
                        result += "第" + (i + 2).ToString() + "行," + "半年度" + "," + "此项为必填项！" + "|";
                    }

                    ////判断是否 已存在该季度数据
                    var drillPlanHalfYearReport = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportByUnitIdAndYearAndHalfYear(unitId, Funs.GetNewIntOrZero(row2.Trim()), Funs.GetNewIntOrZero(row3.Trim()));
                    if (drillPlanHalfYearReport != null)
                    {
                        result += "第" + (i + 2).ToString() + "行," + "已存在该半年度半年报！" + "|";
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