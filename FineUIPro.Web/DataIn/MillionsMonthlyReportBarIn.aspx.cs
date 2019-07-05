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
    public partial class MillionsMonthlyReportBarIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 百万工时安全统计月报表集合
        /// </summary>
        private List<Model.View_DataIn_MillionsMonthlyReport> reports = new List<Model.View_DataIn_MillionsMonthlyReport>();
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

                for (int i = 0; i < ir; i++)
                {
                    Model.View_DataIn_MillionsMonthlyReport report = new Model.View_DataIn_MillionsMonthlyReport();
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
                    string row20 = pds.Rows[i][19].ToString().Trim();
                    string row21 = pds.Rows[i][20].ToString().Trim();
                    string row22 = pds.Rows[i][21].ToString().Trim();
                    string row23 = pds.Rows[i][22].ToString().Trim();
                    string row24 = pds.Rows[i][23].ToString().Trim();
                    string row25 = pds.Rows[i][24].ToString().Trim();
                    string row26 = pds.Rows[i][25].ToString().Trim();
                    string row27 = pds.Rows[i][26].ToString().Trim();
                    string row28 = pds.Rows[i][27].ToString().Trim();
                    string row29 = pds.Rows[i][28].ToString().Trim();
                    string row30 = pds.Rows[i][29].ToString().Trim();
                    string row31 = pds.Rows[i][30].ToString().Trim();
                    string row32 = pds.Rows[i][31].ToString().Trim();
                    string row33 = pds.Rows[i][32].ToString().Trim();
                    string row34 = pds.Rows[i][33].ToString().Trim();
                    string row35 = pds.Rows[i][34].ToString().Trim();
                    string row36 = pds.Rows[i][35].ToString().Trim();
                    string row37 = pds.Rows[i][36].ToString().Trim();
                    string row38 = pds.Rows[i][37].ToString().Trim();                    

                    if (!string.IsNullOrEmpty(row1))
                    {
                        report.UnitId = units.Where(x => x.UnitName == row1.Trim()).FirstOrDefault().UnitId;
                    }
                    if (!string.IsNullOrEmpty(row2))
                    {
                        report.Year= Convert.ToInt32(row2);
                    }
                    if (!string.IsNullOrEmpty(row3))
                    {
                        report.Month = Convert.ToInt32(row3);
                    }
                    report.DutyPerson = row4;
                    if (!string.IsNullOrEmpty(row5))
                    {
                        report.RecordableIncidentRate =Convert.ToDecimal(row5);
                    }
                    if (!string.IsNullOrEmpty(row6))
                    {
                        report.LostTimeRate = Convert.ToDecimal(row6);
                    }
                    if (!string.IsNullOrEmpty(row7))
                    {
                        report.LostTimeInjuryRate = Convert.ToDecimal(row7);
                    }
                    if (!string.IsNullOrEmpty(row8))
                    {
                        report.DeathAccidentFrequency = Convert.ToDecimal(row8);
                    }
                    if (!string.IsNullOrEmpty(row9))
                    {
                        report.AccidentMortality = Convert.ToDecimal(row9);
                    }
                    report.Affiliation = row10;
                    report.Name = row11;
                    if (!string.IsNullOrEmpty(row12))
                    {
                        report.PostPersonNum = Convert.ToInt32(row12);
                    }
                    if (!string.IsNullOrEmpty(row13))
                    {
                        report.SnapPersonNum =Convert.ToInt32(row13);
                    }
                    if (!string.IsNullOrEmpty(row14))
                    {
                        report.ContractorNum = Convert.ToInt32(row14);
                    }
                    if (!string.IsNullOrEmpty(row15))
                    {
                        report.SumPersonNum = Convert.ToInt32(row15);
                    }
                    if (!string.IsNullOrEmpty(row16))
                    {
                        report.TotalWorkNum = Convert.ToDecimal(row16);
                    }
                    if (!string.IsNullOrEmpty(row17))
                    {
                        report.SeriousInjuriesNum =Convert.ToInt32(row17);
                    }
                    if (!string.IsNullOrEmpty(row18))
                    {
                        report.SeriousInjuriesPersonNum = Convert.ToInt32(row18);
                    }
                    if (!string.IsNullOrEmpty(row19))
                    {
                        report.SeriousInjuriesLossHour =Convert.ToInt32(row19);
                    }
                    if (!string.IsNullOrEmpty(row20))
                    {
                        report.MinorAccidentNum = Convert.ToInt32(row20);
                    }
                    if (!string.IsNullOrEmpty(row21))
                    {
                        report.MinorAccidentPersonNum = Convert.ToInt32(row21);
                    }
                    if (!string.IsNullOrEmpty(row22))
                    {
                        report.MinorAccidentLossHour = Convert.ToInt32(row22);
                    }
                    if (!string.IsNullOrEmpty(row23))
                    {
                        report.OtherAccidentNum =Convert.ToInt32(row23);
                    }
                    if (!string.IsNullOrEmpty(row24))
                    {
                        report.OtherAccidentPersonNum = Convert.ToInt32(row24);
                    }
                    if (!string.IsNullOrEmpty(row25))
                    {
                        report.OtherAccidentLossHour = Convert.ToInt32(row25);
                    }
                    if (!string.IsNullOrEmpty(row26))
                    {
                        report.RestrictedWorkPersonNum = Convert.ToInt32(row26);
                    }
                    if (!string.IsNullOrEmpty(row27))
                    {
                        report.RestrictedWorkLossHour =Convert.ToInt32(row27);
                    }
                    if (!string.IsNullOrEmpty(row28))
                    {
                        report.MedicalTreatmentPersonNum = Convert.ToInt32(row28);
                    }
                    if (!string.IsNullOrEmpty(row29))
                    {
                        report.MedicalTreatmentLossHour =Convert.ToInt32(row29);
                    }
                    if (!string.IsNullOrEmpty(row30))
                    {
                        report.FireNum = Convert.ToInt32(row30);
                    }
                    if (!string.IsNullOrEmpty(row31))
                    {
                        report.ExplosionNum = Convert.ToInt32(row31);
                    }
                    if (!string.IsNullOrEmpty(row32))
                    {
                        report.TrafficNum = Convert.ToInt32(row32);
                    }
                    if (!string.IsNullOrEmpty(row33))
                    {
                        report.EquipmentNum =Convert.ToInt32(row33);
                    }
                    if (!string.IsNullOrEmpty(row34))
                    {
                        report.QualityNum = Convert.ToInt32(row34);
                    }
                    if (!string.IsNullOrEmpty(row35))
                    {
                        report.OtherNum = Convert.ToInt32(row35);
                    }
                    if (!string.IsNullOrEmpty(row36))
                    {
                        report.FirstAidDressingsNum = Convert.ToInt32(row36);
                    }
                    if (!string.IsNullOrEmpty(row37))
                    {
                        report.AttemptedEventNum = Convert.ToInt32(row37);
                    }
                    if (!string.IsNullOrEmpty(row38))
                    {
                        report.LossDayNum = Convert.ToInt32(row38);
                    }

                    if (reports.Where(e => e.MillionsMonthlyReportItemId == report.MillionsMonthlyReportItemId).FirstOrDefault() == null)
                    {
                        report.MillionsMonthlyReportItemId = SQLHelper.GetNewID(typeof(Model.View_DataIn_MillionsMonthlyReport));
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