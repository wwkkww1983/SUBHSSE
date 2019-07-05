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
    public partial class DrillPlanHalfYearReportBarIn : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 应急演练工作计划半年报表集合
        /// </summary>
        private List<Model.View_Information_DrillPlanHalfYearReportItem> reports = new List<Model.View_Information_DrillPlanHalfYearReportItem>();
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
                var units = from x in Funs.DB.Base_Unit where x.IsHide == false || !x.IsHide.HasValue select x; 
                for (int i = 0; i < ir; i++)
                {
                    Model.View_Information_DrillPlanHalfYearReportItem report = new Model.View_Information_DrillPlanHalfYearReportItem();
                    string row1 = pds.Rows[i][0].ToString().Trim();
                    string row2 = pds.Rows[i][1].ToString().Trim();
                    string row3 = pds.Rows[i][2].ToString().Trim();
                    string row4 = pds.Rows[i][3].ToString().Trim();
                    string row5 = pds.Rows[i][4].ToString().Trim();
                    string row6 = pds.Rows[i][5].ToString().Trim();
                    string row7 = pds.Rows[i][6].ToString().Trim();
                    string row8 = pds.Rows[i][7].ToString().Trim();
                    string row9 = pds.Rows[i][8].ToString().Trim();

                    if (!string.IsNullOrEmpty(row1))
                    {
                        var unit = units.FirstOrDefault(x => x.UnitName == row1.Trim());
                        if (unit != null)
                        {
                            report.UnitId = unit.UnitId;
                        }
                    }

                    if (!string.IsNullOrEmpty(row2))
                    {
                        report.YearId = Convert.ToInt32(row2);
                    }
                    if (!string.IsNullOrEmpty(row3))
                    {
                        report.HalfYearId = Convert.ToInt32(row3);
                    }
                    report.Telephone = row4;
                    report.DrillPlanName = row5;
                    report.OrganizationUnit = row6;
                    report.DrillPlanDate = row7;
                    report.AccidentScene = row8;
                    report.ExerciseWay = row9;

                    if (reports.Where(e => e.DrillPlanHalfYearReportItemId == report.DrillPlanHalfYearReportItemId).FirstOrDefault() == null)
                    {
                        report.DrillPlanHalfYearReportItemId = SQLHelper.GetNewID(typeof(Model.View_Information_DrillPlanHalfYearReportItem));
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