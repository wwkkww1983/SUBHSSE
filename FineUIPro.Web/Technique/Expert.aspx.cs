using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using BLL;
using Model;
using System.IO;
using System.Text;

namespace FineUIPro.Web.Technique
{
    public partial class Expert : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;

            if (!IsPostBack)
            { 
                ////权限按钮方法
                this.GetButtonPower();
                btnNew.OnClientClick = Window2.GetShowReference("ExpertSave.aspx") + "return false;";
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                btnUploadResources.OnClientClick = Window3.GetShowReference("ExpertUpload.aspx") + "return false";
                btnAuditResources.OnClientClick = Window4.GetShowReference("ExpertAudit.aspx") + "return false;"; btnSelectColumns.OnClientClick = Window5.GetShowReference("ExpertSelectCloumn.aspx");

                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }
       
        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Expert where IsPass=@IsPass";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@IsPass", true));
            if (!string.IsNullOrEmpty(this.ExpertName.Text.Trim()))
            {
                strSql += " AND ExpertName LIKE @ExpertName";
                listStr.Add(new SqlParameter("@ExpertName", "%" + this.ExpertName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.UnitName.Text.Trim()))
            {
                strSql += " AND UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.UnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.ExpertTypeName.Text.Trim()))
            {
                strSql += " AND ExpertTypeName LIKE @ExpertTypeName";
                listStr.Add(new SqlParameter("@ExpertTypeName", "%" + this.ExpertTypeName.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
#endregion

        #region 表头过滤
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, object fillteredObj, string column)
        {
            bool valid = false;

            if (column == "ExpertName")
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
            else if (column == "EffectiveDate")
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
            return valid;
        }

        #endregion

        #region 分页、排序、关闭窗口
        /// <summary>
        /// Grid分页索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
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

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string wedId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ExpertSave.aspx?ExpertId={0}", wedId, "编辑 - ")));
        }
        #endregion

        #region 删除
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

            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    Model.Technique_Expert expert = BLL.ExpertService.GetExpertById(rowID);
                    if (expert.IsBuild == true)
                    {
                        ShowNotify("内置项无法删除", MessageBoxIcon.Warning);
                        return;
                    }
                    BLL.ExpertService.DeleteExpertId(rowID);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除安全专家");
                }

                BindGrid();
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region Grid行双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
        #endregion

        #region 文本框查询事件
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

        #region 导出
        /// <summary>
        /// 关闭导出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window5_Close(object sender, WindowCloseEventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1, e.CloseArgument.Split('#')));
            Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid, string[] columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            List<string> columnHeaderTexts = new List<string>(columns);
            List<int> columnIndexs = new List<int>();
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                if (columnHeaderTexts.Contains(column.HeaderText))
                {
                    sb.AppendFormat("<td>{0}</td>", column.HeaderText);
                    columnIndexs.Add(column.ColumnIndex);
                }
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                int columnIndex = 0;
                foreach (object value in row.Values)
                {
                    if (columnIndexs.Contains(columnIndex))
                    {
                        string html = value.ToString();
                        if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                        {
                            // 模板列                            
                            string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                            Control templateCtrl = row.FindControl(templateID);
                            html = GetRenderedHtmlSource(templateCtrl);
                        }
                        //else
                        //{
                        //    // 处理CheckBox             
                        //    if (html.Contains("f-grid-static-checkbox"))
                        //    {
                        //        if (!html.Contains("f-checked"))
                        //        {
                        //            html = "×";
                        //        }
                        //        else
                        //        {
                        //            html = "√";
                        //        }
                        //    }
                        //    // 处理图片                           
                        //    if (html.Contains("<img"))
                        //    {
                        //        string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, ""); 
                        //        html = html.Replace("src=\"", "src=\"" + prefix);
                        //    }
                        //}
                        sb.AppendFormat("<td>{0}</td>", html);
                    }
                    columnIndex++;
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>        
        /// 获取控件渲染后的HTML源代码        
        /// </summary>        
        /// <param name="ctrl"></param>        
        /// <returns></returns>        
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);
                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ExpertMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnEdit.Hidden = false;
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnUploadResources.Hidden = false;                    
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnAuditResources.Hidden = false;                    
                }
                if (buttonList.Contains(BLL.Const.BtnOut))
                {
                    this.btnSelectColumns.Hidden = false;                 
                }
            }
        }
        #endregion
    }
}