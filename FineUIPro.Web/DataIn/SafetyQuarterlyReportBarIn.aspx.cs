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
    public partial class SafetyQuarterlyReportBarIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 安全生产数据季报表集合
        /// </summary>
        private List<Model.Information_SafetyQuarterlyReport> safetyQuarterlyReports = new List<Model.Information_SafetyQuarterlyReport>();
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["safetyQuarterlyReports"] = null;
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
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds, int Cols)
        {
            int ic, ir;
            safetyQuarterlyReports.Clear();
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
                    Model.Information_SafetyQuarterlyReport safetyQuarterlyReport = new Model.Information_SafetyQuarterlyReport();
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
                    string row39 = pds.Rows[i][38].ToString().Trim();
                    string row40 = pds.Rows[i][39].ToString().Trim();
                    string row41 = pds.Rows[i][40].ToString().Trim();
                    string row42 = pds.Rows[i][41].ToString().Trim();
                    string row43 = pds.Rows[i][42].ToString().Trim();
                    string row44 = pds.Rows[i][43].ToString().Trim();
                    string row45 = pds.Rows[i][44].ToString().Trim();
                    string row46 = pds.Rows[i][45].ToString().Trim();
                    string row47 = pds.Rows[i][46].ToString().Trim();
                    string row48 = pds.Rows[i][47].ToString().Trim();
                    string row49 = pds.Rows[i][48].ToString().Trim();
                    string row50 = pds.Rows[i][49].ToString().Trim();
                    string row51 = pds.Rows[i][50].ToString().Trim();
                    string row52 = pds.Rows[i][51].ToString().Trim();
                    string row53 = pds.Rows[i][52].ToString().Trim();
                    string row54 = pds.Rows[i][53].ToString().Trim();
                    string row55 = pds.Rows[i][54].ToString().Trim();
                    string row56 = pds.Rows[i][55].ToString().Trim();
                    string row57 = pds.Rows[i][56].ToString().Trim();
                    string row58 = pds.Rows[i][57].ToString().Trim();
                    string row59 = pds.Rows[i][58].ToString().Trim();
                    string row60 = pds.Rows[i][59].ToString().Trim();
                    string row61 = pds.Rows[i][60].ToString().Trim();
                    string row62 = pds.Rows[i][61].ToString().Trim();
                    string row63 = pds.Rows[i][62].ToString().Trim();
                    string row64 = pds.Rows[i][63].ToString().Trim();
                    string row65 = pds.Rows[i][64].ToString().Trim();
                    string row66 = pds.Rows[i][65].ToString().Trim();
                    string row67 = pds.Rows[i][66].ToString().Trim();
                    string row68 = pds.Rows[i][67].ToString().Trim();
                    string row69 = pds.Rows[i][68].ToString().Trim();
                    string row70 = pds.Rows[i][69].ToString().Trim();
                    string row71 = pds.Rows[i][70].ToString().Trim();
                    string row72 = pds.Rows[i][71].ToString().Trim();
                    string row73 = pds.Rows[i][72].ToString().Trim();
                    string row74 = pds.Rows[i][73].ToString().Trim();

                    if (!string.IsNullOrEmpty(row1))
                    {
                        safetyQuarterlyReport.UnitId = units.Where(x => x.UnitName == row1.Trim()).FirstOrDefault().UnitId;
                    }
                    if (!string.IsNullOrEmpty(row2))
                    {
                        safetyQuarterlyReport.YearId = Convert.ToInt32(row2);
                    }
                    if (!string.IsNullOrEmpty(row3))
                    {
                        safetyQuarterlyReport.Quarters = Convert.ToInt32(row3);
                    }
                    if (!string.IsNullOrEmpty(row4))
                    {
                        safetyQuarterlyReport.TotalInWorkHours = Convert.ToInt32(row4);
                    }
                    safetyQuarterlyReport.TotalInWorkHoursRemark = row5;
                    if (!string.IsNullOrEmpty(row6))
                    {
                        safetyQuarterlyReport.TotalOutWorkHours = Convert.ToInt32(row6);
                    }
                    safetyQuarterlyReport.TotalOutWorkHoursRemark = row7;
                    if (!string.IsNullOrEmpty(row8))
                    {
                        safetyQuarterlyReport.WorkHoursLossRate = Convert.ToDecimal(row8);
                    }
                    safetyQuarterlyReport.WorkHoursLossRateRemark = row9;
                    if (!string.IsNullOrEmpty(row10))
                    {
                        safetyQuarterlyReport.WorkHoursAccuracy = Convert.ToDecimal(row10);
                    }
                    safetyQuarterlyReport.WorkHoursAccuracyRemark = row11;
                    if (!string.IsNullOrEmpty(row12))
                    {
                        safetyQuarterlyReport.MainBusinessIncome = Convert.ToDecimal(row12);
                    }
                    safetyQuarterlyReport.MainBusinessIncomeRemark = row13;
                    if (!string.IsNullOrEmpty(row14))
                    {
                        safetyQuarterlyReport.ConstructionRevenue = Convert.ToDecimal(row14);
                    }
                    safetyQuarterlyReport.ConstructionRevenueRemark = row15;
                    if (!string.IsNullOrEmpty(row16))
                    {
                        safetyQuarterlyReport.UnitTimeIncome = Convert.ToDecimal(row16);
                    }
                    safetyQuarterlyReport.UnitTimeIncomeRemark = row17;
                    if (!string.IsNullOrEmpty(row18))
                    {
                        safetyQuarterlyReport.BillionsOutputMortality = Convert.ToDecimal(row18);
                    }
                    safetyQuarterlyReport.BillionsOutputMortalityRemark = row19;
                    if (!string.IsNullOrEmpty(row20))
                    {
                        safetyQuarterlyReport.MajorFireAccident = Convert.ToInt32(row20);
                    }
                    safetyQuarterlyReport.MajorFireAccidentRemark = row21;
                    if (!string.IsNullOrEmpty(row22))
                    {
                        safetyQuarterlyReport.MajorEquipAccident = Convert.ToInt32(row22);
                    }
                    safetyQuarterlyReport.MajorEquipAccidentRemark = row23;
                    if (!string.IsNullOrEmpty(row24))
                    {
                        safetyQuarterlyReport.AccidentFrequency = Convert.ToDecimal(row24);
                    }
                    safetyQuarterlyReport.AccidentFrequencyRemark = row25;
                    if (!string.IsNullOrEmpty(row26))
                    {
                        safetyQuarterlyReport.SeriousInjuryAccident = Convert.ToInt32(row26);
                    }
                    safetyQuarterlyReport.SeriousInjuryAccidentRemark = row27;
                    if (!string.IsNullOrEmpty(row28))
                    {
                        safetyQuarterlyReport.FireAccident = Convert.ToInt32(row28);
                    }
                    safetyQuarterlyReport.FireAccidentRemark = row29;
                    if (!string.IsNullOrEmpty(row30))
                    {
                        safetyQuarterlyReport.EquipmentAccident = Convert.ToInt32(row30);
                    }
                    safetyQuarterlyReport.EquipmentAccidentRemark = row31;
                    if (!string.IsNullOrEmpty(row32))
                    {
                        safetyQuarterlyReport.PoisoningAndInjuries = Convert.ToInt32(row32);
                    }
                    safetyQuarterlyReport.PoisoningAndInjuriesRemark = row33;
                    if (!string.IsNullOrEmpty(row34))
                    {
                        safetyQuarterlyReport.ProductionSafetyInTotal = Convert.ToInt32(row34);
                    }
                    safetyQuarterlyReport.ProductionSafetyInTotalRemark = row35;
                    if (!string.IsNullOrEmpty(row36))
                    {
                        safetyQuarterlyReport.ProtectionInput = Convert.ToDecimal(row36);
                    }
                    safetyQuarterlyReport.ProtectionInputRemark = row37;
                    if (!string.IsNullOrEmpty(row38))
                    {
                        safetyQuarterlyReport.LaboAndHealthIn = Convert.ToDecimal(row38);
                    }
                    safetyQuarterlyReport.LaborAndHealthInRemark = row39;
                    if (!string.IsNullOrEmpty(row40))
                    {
                        safetyQuarterlyReport.TechnologyProgressIn = Convert.ToDecimal(row40);
                    }
                    safetyQuarterlyReport.TechnologyProgressInRemark = row41;
                    if (!string.IsNullOrEmpty(row42))
                    {
                        safetyQuarterlyReport.EducationTrainIn = Convert.ToDecimal(row42);
                    }
                    safetyQuarterlyReport.EducationTrainInRemark = row43;
                    if (!string.IsNullOrEmpty(row44))
                    {
                        safetyQuarterlyReport.ProjectCostRate = Convert.ToDecimal(row44);
                    }
                    safetyQuarterlyReport.ProjectCostRateRemark = row45;
                    if (!string.IsNullOrEmpty(row46))
                    {
                        safetyQuarterlyReport.ProductionInput = Convert.ToDecimal(row46);
                    }
                    safetyQuarterlyReport.ProductionInputRemark = row47;
                    if (!string.IsNullOrEmpty(row48))
                    {
                        safetyQuarterlyReport.Revenue = Convert.ToDecimal(row48);
                    }
                    safetyQuarterlyReport.RevenueRemark = row49;
                    if (!string.IsNullOrEmpty(row50))
                    {
                        safetyQuarterlyReport.FullTimeMan = Convert.ToInt32(row50);
                    }
                    safetyQuarterlyReport.FullTimeManRemark = row51;
                    if (!string.IsNullOrEmpty(row52))
                    {
                        safetyQuarterlyReport.PMMan = Convert.ToInt32(row52);
                    }
                    safetyQuarterlyReport.PMManRemark = row53;
                    if (!string.IsNullOrEmpty(row54))
                    {
                        safetyQuarterlyReport.CorporateDirectorEdu = Convert.ToInt32(row54);
                    }
                    safetyQuarterlyReport.CorporateDirectorEduRemark = row55;
                    if (!string.IsNullOrEmpty(row56))
                    {
                        safetyQuarterlyReport.ProjectLeaderEdu = Convert.ToInt32(row56);
                    }
                    safetyQuarterlyReport.ProjectLeaderEduRemark = row57;
                    if (!string.IsNullOrEmpty(row58))
                    {
                        safetyQuarterlyReport.FullTimeEdu = Convert.ToInt32(row58);
                    }
                    safetyQuarterlyReport.FullTimeEduRemark = row59;
                    if (!string.IsNullOrEmpty(row60))
                    {
                        safetyQuarterlyReport.ThreeKidsEduRate = Convert.ToDecimal(row60); 
                    }
                    safetyQuarterlyReport.ThreeKidsEduRateRemark = row61;
                    if (!string.IsNullOrEmpty(row62))
                    {
                        safetyQuarterlyReport.UplinReportRate = Convert.ToDecimal(row62);
                    }
                    safetyQuarterlyReport.UplinReportRateRemark = row63;
                    if (!string.IsNullOrEmpty(row64))
                    {
                        safetyQuarterlyReport.KeyEquipmentTotal = Convert.ToInt32(row64);
                    }
                    safetyQuarterlyReport.KeyEquipmentTotalRemark = row65;
                    if (!string.IsNullOrEmpty(row66))
                    {
                        safetyQuarterlyReport.KeyEquipmentReportCount = Convert.ToInt32(row66);
                    }
                    safetyQuarterlyReport.KeyEquipmentReportCountRemark = row67;
                    if (!string.IsNullOrEmpty(row68))
                    {
                        safetyQuarterlyReport.ChemicalAreaProjectCount = Convert.ToInt32(row68);
                    }
                    safetyQuarterlyReport.ChemicalAreaProjectCountRemark = row69;
                    if (!string.IsNullOrEmpty(row70))
                    {
                        safetyQuarterlyReport.HarmfulMediumCoverCount = Convert.ToInt32(row70);
                    }
                    safetyQuarterlyReport.HarmfulMediumCoverCountRemark = row71;
                    if (!string.IsNullOrEmpty(row72))
                    {
                        safetyQuarterlyReport.HarmfulMediumCoverRate = Convert.ToDecimal(row72);
                    }
                    safetyQuarterlyReport.HarmfulMediumCoverRateRemark = row73;
                    safetyQuarterlyReport.Remarks = row74;

                    if (safetyQuarterlyReports.Where(e => e.SafetyQuarterlyReportId == safetyQuarterlyReport.SafetyQuarterlyReportId).FirstOrDefault() == null)
                    {
                        safetyQuarterlyReport.SafetyQuarterlyReportId = SQLHelper.GetNewID(typeof(Model.Information_SafetyQuarterlyReport));
                        safetyQuarterlyReport.CompileMan = this.CurrUser.UserName;
                        safetyQuarterlyReport.UpState = BLL.Const.UpState_2;
                        safetyQuarterlyReport.HandleMan = this.CurrUser.UserId;
                        safetyQuarterlyReport.HandleState = BLL.Const.HandleState_1;
                        safetyQuarterlyReports.Add(safetyQuarterlyReport); //增加安全生产数据季报表
                    }
                }
                Session["safetyQuarterlyReports"] = safetyQuarterlyReports;
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