using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class TestDB : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = Window1.GetShowReference("TestDBEdit.aspx") + "return false;";
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strSql = "select * from Edu_Online_TestDB order by TestType ,ItemType,TestNo";

            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, object fillteredObj, string column)
        {
            bool valid = false;

            if (column == "WED_Name")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.ToString();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }
            else if (column == "WED_Birthday")
            {
                if (!String.IsNullOrEmpty(sourceObj.ToString()))
                {
                    DateTime sourceValue = Convert.ToDateTime(sourceObj);
                    DateTime fillteredValue = Convert.ToDateTime(fillteredObj);

                    if (fillteredOperator == "greater")
                    {
                        if (sourceValue > fillteredValue)
                        {
                            valid = true;
                        }
                    }
                    else if (fillteredOperator == "less")
                    {
                        if (sourceValue < fillteredValue)
                        {
                            valid = true;
                        }
                    }
                    else if (fillteredOperator == "equal")
                    {
                        if (sourceValue == fillteredValue)
                        {
                            valid = true;
                        }
                    }
                }

            }

            //else if (column == "Major")
            //{
            //    string sourceValue = sourceObj.ToString();
            //    JArray fillteredValue = JArray.Parse(fillteredObj.ToString());

            //    foreach (string filltereditem in fillteredValue)
            //    {
            //        if (filltereditem == sourceValue)
            //        {
            //            valid = true;
            //            break;
            //        }
            //    }
            //}

            return valid;
        }

        #endregion

        #region Events

        // 删除数据
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

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
            if (GetButtonPower(BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        BLL.TestDBService.DeleteTestDB(rowID);
                        BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除试题库维护");
                    }
                    BindGrid();
                    ShowNotify("删除数据成功!");
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {            
        }

        #endregion

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);


            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;

            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (GetButtonPower(BLL.Const.BtnModify))
            {
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.ShowInTop("请至少选择一条记录！");
                    return;
                }

                string testId = Grid1.SelectedRowID;
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("TestDBEdit.aspx?TestId={0}", testId, "编辑 - ")));
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！");
            }
        }

        /// <summary>
        /// Grid1行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool GetButtonPower(string button)
        {
            return BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.TestDBMenuId, button);
        }
    }
}