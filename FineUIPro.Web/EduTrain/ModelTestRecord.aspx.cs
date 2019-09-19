using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class ModelTestRecord : PageBase
    {
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();

                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnMenuDelete.Hidden = false;
                }

                BLL.ModelTestRecordService.UpdateTestEndTimeNull();
                // 绑定表格
                BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ModelTestRecord.ModelTestRecordId,ModelTestRecord.TrainingId, ModelTestRecord.TestManId, ModelTestRecord.TestStartTime, ModelTestRecord.TestEndTime, ModelTestRecord.TestScores,Training.TrainingName,person.PersonName AS TestManName"
                         + @" ,'100' as TotalScore,'90' as Duration,'95' as QuestionCount"
                         + @" FROM dbo.Training_ModelTestRecord AS ModelTestRecord"
                         + @" LEFT JOIN dbo.Training_TestTraining AS Training ON Training.TrainingId=ModelTestRecord.TrainingId"
                         + @" LEFT JOIN dbo.SitePerson_Person AS person ON person.PersonId = ModelTestRecord.TestManId WHERE 1=1";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (this.CurrUser.UserId != BLL.Const.sysglyId)
            {
                strSql += " AND ModelTestRecord.TestManId ='" + this.CurrUser.UserId + "'";
            }
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (person.PersonName LIKE @TestManName OR Training.TrainingName LIKE @TestManName)";
                listStr.Add(new SqlParameter("@TestManName", "%" + this.txtName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtMinScores.Text.Trim()))
            {
                strSql += " AND ModelTestRecord.TestScores >= @MinScores";
                listStr.Add(new SqlParameter("@MinScores", Funs.GetNewDecimalOrZero(this.txtMinScores.Text.Trim())));
            }
            if (!string.IsNullOrEmpty(this.txtMaxScores.Text.Trim()))
            {
                strSql += " AND ModelTestRecord.TestScores <= @MaxScores";
                listStr.Add(new SqlParameter("@MaxScores", Funs.GetNewDecimalOrZero(this.txtMaxScores.Text.Trim())));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string ModelTestRecordId = Grid1.Rows[i].DataKeys[0].ToString();
                var ModelTestRecord = BLL.ModelTestRecordService.GetModelTestRecordById(ModelTestRecordId);
                if (ModelTestRecord != null)
                {
                    if (ModelTestRecord.TestScores < 80)
                    {
                        Grid1.Rows[i].RowCssClass = "Red";
                    }
                }
            }
        }
        #endregion

        #region 分页、关闭窗口
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        #endregion

        #region 编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuView_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ModelTestRecordItem.aspx?ModelTestRecordId={0}", Grid1.SelectedRowID, "编辑 - ")));
        }
        #endregion

        #region 输入框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.ModelTestRecordService.GetModelTestRecordById(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, null, rowID, BLL.Const.ProjectModelTestRecordMenuId, BLL.Const.BtnDelete);
                        BLL.ModelTestRecordService.DeleteModelTestRecordByModelTestRecordId(rowID);
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 导出按钮
        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("模拟考试" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = Grid1.RecordCount;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}