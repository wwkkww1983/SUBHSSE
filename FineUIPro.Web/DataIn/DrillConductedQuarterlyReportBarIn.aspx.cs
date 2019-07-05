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
    public partial class DrillConductedQuarterlyReportBarIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 应急演练开展情况季报集合
        /// </summary>
        private List<Model.View_Information_DrillConductedQuarterlyReportItem> reports = new List<Model.View_Information_DrillConductedQuarterlyReportItem>();
        #endregion

        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["reports"] = null;
            string rootPath = Server.MapPath("~/");
            string fileName = rootPath + initPath + Request.Params["FileName"];
            ImportXlsToData(fileName);
        }
        #endregion

        #region Excel提取数据
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
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds, int Cols)
        {
            int ic, ir;
            reports.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                ShowNotify("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列", MessageBoxIcon.Warning);
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit select x;

                for (int i = 1; i < ir; i++)
                {
                    Model.View_Information_DrillConductedQuarterlyReportItem report = new Model.View_Information_DrillConductedQuarterlyReportItem();
                    string row1 = pds.Rows[i][0].ToString().Trim();
                    string row2 = pds.Rows[i][1].ToString().Trim();
                    string row3 = pds.Rows[i][2].ToString().Trim();
                    string row4 = pds.Rows[i][3].ToString().Trim();
                    string row5 = pds.Rows[i][4].ToString().Trim();
                    string row6 = pds.Rows[i][5].ToString().Trim();
                    string row7 = pds.Rows[i][6].ToString().Trim();
                    string row8 = pds.Rows[i][7].ToString().Trim();
                    string row9 = pds.Rows[i][8].ToString().Trim();
                    string row10 = pds.Rows[i][9].ToString().Trim();
                    string row11 = pds.Rows[i][10].ToString().Trim();
                    string row12 = pds.Rows[i][11].ToString().Trim();
                    string row13 = pds.Rows[i][12].ToString().Trim();
                    string row14 = pds.Rows[i][13].ToString().Trim();
                    string row15 = pds.Rows[i][14].ToString().Trim();
                    string row16 = pds.Rows[i][15].ToString().Trim();
                    string row17 = pds.Rows[i][16].ToString().Trim();
                    string row18 = pds.Rows[i][17].ToString().Trim();
                    string row19 = pds.Rows[i][18].ToString().Trim();
                    if (!string.IsNullOrEmpty(row1))
                    {
                        report.UnitId = units.Where(x => x.UnitName == row1.Trim()).FirstOrDefault().UnitId;
                    }
                    if (!string.IsNullOrEmpty(row2))
                    {
                        report.YearId = Convert.ToInt32(row2);
                    }
                    if (!string.IsNullOrEmpty(row3))
                    {
                        report.Quarter = Convert.ToInt32(row3);
                    }
                    report.IndustryType = row4;
                    if (!string.IsNullOrEmpty(row5))
                    {
                        report.TotalConductCount = Convert.ToInt32(row5);
                    }
                    if (!string.IsNullOrEmpty(row6))
                    {
                        report.TotalPeopleCount = Convert.ToInt32(row6);
                    }
                    if (!string.IsNullOrEmpty(row7))
                    {
                        report.TotalInvestment = Convert.ToDecimal(row7);
                    }
                    if (!string.IsNullOrEmpty(row8))
                    {
                        report.HQConductCount = Convert.ToInt32(row8);
                    }
                    if (!string.IsNullOrEmpty(row9))
                    {
                        report.HQPeopleCount = Convert.ToInt32(row9);
                    }
                    if (!string.IsNullOrEmpty(row10))
                    {
                        report.HQInvestment = Convert.ToDecimal(row10);
                    }
                    if (!string.IsNullOrEmpty(row11))
                    {
                        report.BasicConductCount = Convert.ToInt32(row11);
                    }
                    if (!string.IsNullOrEmpty(row12))
                    {
                        report.BasicPeopleCount = Convert.ToInt32(row12);
                    }
                    if (!string.IsNullOrEmpty(row13))
                    {
                        report.BasicInvestment = Convert.ToDecimal(row13);
                    }
                    if (!string.IsNullOrEmpty(row14))
                    {
                        report.ComprehensivePractice = Convert.ToInt32(row14);
                    }
                    if (!string.IsNullOrEmpty(row15))
                    {
                        report.CPScene = Convert.ToInt32(row15);
                    }
                    if (!string.IsNullOrEmpty(row16))
                    {
                        report.CPDesktop = Convert.ToInt32(row16);
                    }
                    if (!string.IsNullOrEmpty(row17))
                    {
                        report.SpecialDrill = Convert.ToInt32(row17);
                    }
                    if (!string.IsNullOrEmpty(row18))
                    {
                        report.SDScene = Convert.ToInt32(row18);
                    }
                    if (!string.IsNullOrEmpty(row19))
                    {
                        report.SDDesktop = Convert.ToInt32(row19);
                    }
                   
                    if (reports.Where(e => e.DrillConductedQuarterlyReportItemId == report.DrillConductedQuarterlyReportItemId).FirstOrDefault() == null)
                    {
                        report.DrillConductedQuarterlyReportItemId = SQLHelper.GetNewID(typeof(Model.View_Information_DrillConductedQuarterlyReportItem));
                        reports.Add(report);
                    }
                }
                Session["reports"] = reports;
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