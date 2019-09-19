using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class TestRecordItem : PageBase
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
                // 绑定表格
                BindGrid();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT TestRecordItemId,TestRecordId,TrainingItemName,Abstracts,AttachUrl,AItem,BItem,CItem,DItem,EItem,Replace(Replace(Replace(Replace(Replace(AnswerItems,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E') AS AnswerItems
                            ,Score,SubjectScore,Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E') AS SelectedItem"
                         + @",TestType,TrainingItemCode,(CASE WHEN TestType = '1' THEN '单选题' WHEN TestType = '2' THEN '多选题' ELSE '判断题' END) AS TestTypeName"
                         + @" FROM Training_TestRecordItem "
                         + @" WHERE TestRecordId= '" + Request.Params["TestRecordId"] + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                strSql += " AND (Abstracts LIKE @Name OR SubjectScore LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtName.Text.Trim() + "%"));
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
                var item = BLL.TestRecordItemService.GetTestRecordItemTestRecordItemId(Grid1.Rows[i].DataKeys[0].ToString());
                if (item != null && item.Score != item.SubjectScore)
                {
                    Grid1.Rows[i].RowCssClass = "Red";
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestRecordView.aspx?TestRecordItemId={0}", Grid1.SelectedRowID, "编辑 - ")));
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
                    var getV = BLL.TestRecordItemService.GetTestRecordItemTestRecordItemId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.TrainingItemCode, rowID, BLL.Const.ProjectTestRecordMenuId, BLL.Const.BtnDelete);
                        BLL.TestRecordItemService.DeleteTestRecordItemmByTestRecordItemId(rowID);
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("考试记录" + filename, System.Text.Encoding.UTF8) + ".xls");
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