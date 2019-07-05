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
    public partial class AccidentCauseReportBarIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 职工伤亡事故原因分析表集合
        /// </summary>
        private List<Model.View_DataIn_AccidentCauseReport> reports = new List<Model.View_DataIn_AccidentCauseReport>();
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
                Session["reports"] = null;
                string rootPath = Server.MapPath("~/");
                string fileName = rootPath + initPath + Request.Params["FileName"];
                ImportXlsToData(fileName);
            }
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
                    int totalDeath = 0;
                    int totalInjuries = 0;
                    int totalMinorInjuries = 0;
                    Model.View_DataIn_AccidentCauseReport report = new Model.View_DataIn_AccidentCauseReport();
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

                    if (!string.IsNullOrEmpty(row1))//填报单位
                    {
                        report.UnitId = units.Where(x => x.UnitName == row1.Trim()).FirstOrDefault().UnitId;
                    }
                    report.AccidentCauseReportCode = row2;//编号                 
                    if (!string.IsNullOrEmpty(row3))//年份
                    {
                        report.Year = Convert.ToInt32(row3);
                    }
                    if (!string.IsNullOrEmpty(row4))//月份
                    {
                        report.Month = Convert.ToInt32(row4);
                    }
                    if (!string.IsNullOrEmpty(row5))//死亡事故
                    {
                        report.DeathAccident = Convert.ToInt32(row5);
                    }
                    if (!string.IsNullOrEmpty(row6))//死亡人数
                    {
                        report.DeathToll = Convert.ToInt32(row6);
                    }
                    if (!string.IsNullOrEmpty(row7))//重伤事故
                    {
                        report.InjuredAccident = Convert.ToInt32(row7);
                    }
                    if (!string.IsNullOrEmpty(row8))//重伤人数
                    {
                        report.InjuredToll = Convert.ToInt32(row8);
                    }
                    if (!string.IsNullOrEmpty(row9))//轻伤事故
                    {
                        report.MinorWoundAccident = Convert.ToInt32(row9);
                    }
                    if (!string.IsNullOrEmpty(row10))//轻伤人数
                    {
                        report.MinorWoundToll = Convert.ToInt32(row10);
                    }
                    if (!string.IsNullOrEmpty(row11))//平均工时总数
                    {
                        report.AverageTotalHours = Convert.ToInt32(row11);    
                    }
                    if (!string.IsNullOrEmpty(row12))//平均工时人数
                    {
                        report.AverageManHours = Convert.ToInt32(row12);
                    }
                    if (!string.IsNullOrEmpty(row13))//损失工时总数
                    {
                        report.TotalLossMan = Convert.ToInt32(row13);
                    }
                    if (!string.IsNullOrEmpty(row14))//上月损失工时总数
                    {
                        report.LastMonthLossHoursTotal = Convert.ToInt32(row14);
                    }
                    if (!string.IsNullOrEmpty(row15))//歇工总日数
                    {
                        report.KnockOffTotal = Convert.ToInt32(row15);
                    }
                    if (!string.IsNullOrEmpty(row16))//直接损失
                    {
                        report.DirectLoss = Convert.ToInt32(row16);
                    }
                    if (!string.IsNullOrEmpty(row17))//间接损失
                    {
                        report.IndirectLosses = Convert.ToInt32(row17);
                    }
                    if (!string.IsNullOrEmpty(row18))//总损失
                    {
                        report.TotalLoss = Convert.ToInt32(row18);
                    }
                    if (!string.IsNullOrEmpty(row19))//无损失工时总数
                    {
                        report.TotalLossTime = Convert.ToInt32(row19);
                    }
                    report.FillCompanyPersonCharge = row20;//填报单位负责人
                    report.AccidentType = row21;//事故类别
                    if (!string.IsNullOrEmpty(row22))//防护保险信号缺失死亡数
                    {
                        report.Death1 = Convert.ToInt32(row22);
                        totalDeath += Convert.ToInt32(row22);
                    }
                    if (!string.IsNullOrEmpty(row23))//防护保险信号缺失重伤数
                    {
                        report.Injuries1 = Convert.ToInt32(row23);
                        totalInjuries += Convert.ToInt32(row23);
                    }
                    if (!string.IsNullOrEmpty(row24))//防护保险信号缺失轻伤数
                    {
                        report.MinorInjuries1 = Convert.ToInt32(row24);
                        totalInjuries += Convert.ToInt32(row24);
                    }
                    if (!string.IsNullOrEmpty(row25))//设备工具缺陷死亡数
                    {
                        report.Death2 = Convert.ToInt32(row25);
                        totalDeath += Convert.ToInt32(row25);
                    }
                    if (!string.IsNullOrEmpty(row26))//设备工具缺陷重伤数
                    {
                        report.Injuries2 = Convert.ToInt32(row26);
                        totalInjuries += Convert.ToInt32(row26);
                    }
                    if (!string.IsNullOrEmpty(row27))//设备工具缺陷轻伤数
                    {
                        report.MinorInjuries2 = Convert.ToInt32(row27);
                        totalMinorInjuries += Convert.ToInt32(row27);
                    }
                    if (!string.IsNullOrEmpty(row28))//个人防护缺陷死亡数
                    {
                        report.Death3 = Convert.ToInt32(row28);
                        totalDeath += Convert.ToInt32(row28);
                    }
                    if (!string.IsNullOrEmpty(row29))//个人防护缺陷重伤数
                    {
                        report.Injuries3 = Convert.ToInt32(row29);
                        totalInjuries += Convert.ToInt32(row29);
                    }
                    if (!string.IsNullOrEmpty(row30))//个人防护缺陷轻伤数
                    {
                        report.MinorInjuries3 = Convert.ToInt32(row30);
                        totalMinorInjuries += Convert.ToInt32(row30);
                    }
                    if (!string.IsNullOrEmpty(row31))//光线不足死亡数
                    {
                        report.Death4 = Convert.ToInt32(row31);
                        totalDeath += Convert.ToInt32(row31);
                    }
                    if (!string.IsNullOrEmpty(row32))//光线不足重伤数
                    {
                        report.Injuries4 = Convert.ToInt32(row32);
                        totalInjuries += Convert.ToInt32(row32);
                    }
                    if (!string.IsNullOrEmpty(row33))//光线不足轻伤数
                    {
                        report.MinorInjuries4 = Convert.ToInt32(row33);
                        totalMinorInjuries += Convert.ToInt32(row33);
                    }
                    if (!string.IsNullOrEmpty(row34))//劳动组织不合理死亡数
                    {
                        report.Death5 = Convert.ToInt32(row34);
                        totalDeath += Convert.ToInt32(row34);
                    }
                    if (!string.IsNullOrEmpty(row35))//劳动组织不合理重伤数
                    {
                        report.Injuries5 = Convert.ToInt32(row35);
                        totalInjuries += Convert.ToInt32(row35);
                    }
                    if (!string.IsNullOrEmpty(row36))//劳动组织不合理轻伤数
                    {
                        report.MinorInjuries5 = Convert.ToInt32(row36);
                        totalMinorInjuries += Convert.ToInt32(row36);
                    }
                    if (!string.IsNullOrEmpty(row37))//现场指导错误死亡数
                    {
                        report.Death6 = Convert.ToInt32(row37);
                        totalDeath += Convert.ToInt32(row37);
                    }
                    if (!string.IsNullOrEmpty(row38))//现场指导错误重伤数
                    {
                        report.Injuries6 = Convert.ToInt32(row38);
                        totalInjuries += Convert.ToInt32(row38);
                    }
                    if (!string.IsNullOrEmpty(row39))//现场指导错误轻伤数
                    {
                        report.MinorInjuries6 = Convert.ToInt32(row39);
                        totalMinorInjuries += Convert.ToInt32(row39);
                    }
                    if (!string.IsNullOrEmpty(row40))//设计有缺陷死亡数
                    {
                        report.Death7 = Convert.ToInt32(row40);
                        totalDeath += Convert.ToInt32(row40);
                    }
                    if (!string.IsNullOrEmpty(row41))//设计有缺陷重伤数
                    {
                        report.Injuries7 = Convert.ToInt32(row41);
                        totalInjuries += Convert.ToInt32(row41);
                    }
                    if (!string.IsNullOrEmpty(row42))//设计有缺陷轻伤数
                    {
                        report.MinorInjuries7 = Convert.ToInt32(row42);
                        totalMinorInjuries += Convert.ToInt32(row42);
                    }
                    if (!string.IsNullOrEmpty(row43))//不懂操作死亡数
                    {
                        report.Death8 = Convert.ToInt32(row43);
                        totalDeath += Convert.ToInt32(row43);
                    }
                    if (!string.IsNullOrEmpty(row44))//不懂操作重伤数
                    {
                        report.Injuries8 = Convert.ToInt32(row44);
                        totalInjuries += Convert.ToInt32(row44);
                    }
                    if (!string.IsNullOrEmpty(row45))//不懂操作轻伤数
                    {
                        report.MinorInjuries8 = Convert.ToInt32(row45);
                        totalMinorInjuries += Convert.ToInt32(row45);
                    }
                    if (!string.IsNullOrEmpty(row46))//违反操作死亡数
                    {
                        report.Death9 = Convert.ToInt32(row46);
                        totalDeath += Convert.ToInt32(row46);
                    }
                    if (!string.IsNullOrEmpty(row47))//违反操作重伤数
                    {
                        report.Injuries9 = Convert.ToInt32(row47);
                        totalInjuries += Convert.ToInt32(row47);
                    }
                    if (!string.IsNullOrEmpty(row48))//违反操作轻伤数
                    {
                        report.MinorInjuries9 = Convert.ToInt32(row48);
                        totalMinorInjuries += Convert.ToInt32(row48);
                    }
                    if (!string.IsNullOrEmpty(row49))//没有安全操作死亡数
                    {
                        report.Death10 = Convert.ToInt32(row49);
                        totalDeath += Convert.ToInt32(row49);
                    }
                    if (!string.IsNullOrEmpty(row50))//没有安全操作重伤数
                    {
                        report.Injuries10 = Convert.ToInt32(row50);
                        totalInjuries += Convert.ToInt32(row50);
                    }
                    if (!string.IsNullOrEmpty(row51))//没有安全操作轻伤数
                    {
                        report.MinorInjuries10 = Convert.ToInt32(row51);
                        totalMinorInjuries += Convert.ToInt32(row51);
                    }
                    if (!string.IsNullOrEmpty(row52))//其他死亡数
                    {
                        report.Death11 = Convert.ToInt32(row52);
                        totalDeath += Convert.ToInt32(row52);
                    }
                    if (!string.IsNullOrEmpty(row53))//其他重伤数
                    {
                        report.Injuries11 = Convert.ToInt32(row53);
                        totalInjuries += Convert.ToInt32(row53);
                    }
                    if (!string.IsNullOrEmpty(row54))//其他轻伤数
                    {
                        report.MinorInjuries11 = Convert.ToInt32(row54);
                        totalMinorInjuries += Convert.ToInt32(row54);
                    }
                    report.TotalDeath = totalDeath;
                    report.TotalInjuries = totalInjuries;
                    report.TotalMinorInjuries = totalMinorInjuries;

                    if (reports.Where(e => e.AccidentCauseReportItemId == report.AccidentCauseReportItemId).FirstOrDefault() == null)
                    {
                        report.AccidentCauseReportItemId = SQLHelper.GetNewID(typeof(Model.View_DataIn_AccidentCauseReport));
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