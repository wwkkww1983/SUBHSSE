using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class Environmental : PageBase
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
                //权限设置
                this.GetButtonPower();
                btnNew.OnClientClick = Window1.GetShowReference("EnvironmentalEdit.aspx?IsCompany=False") + "return false;";
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                btnSelectColumns.OnClientClick = Window5.GetShowReference("EnvironmentalSelectCloumn.aspx");
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                //环境因素类型             
                BLL.ConstValue.InitConstValueDropDownList(this.drpEType, ConstValue.Group_EnvironmentalType, true);
                BLL.ConstValue.InitConstValueDropDownList(this.drpSmallType, ConstValue.Group_EnvironmentalSmallType, true);
                // 绑定表格
                this.BindGrid();
            }
            else
            {
                if (GetRequestEventArgument() == "reloadGrid")
                {
                    this.BindGrid();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Environmental.EnvironmentalId,Environmental.EType,Environmental.ActivePoint,Environmental.EnvironmentalFactors,Environmental.AValue,Environmental.BValue,Environmental.CValue,Environmental.DValue,Environmental.EValue,Environmental.ZValue,Environmental.SmallType,Environmental.IsImportant,Environmental.Code,Environmental.ControlMeasures,Environmental.Remark"
                + @",(ISNULL(Environmental.AValue,0) + ISNULL(Environmental.BValue,0)+ ISNULL(Environmental.CValue,0)+ ISNULL(Environmental.DValue,0)+ ISNULL(Environmental.EValue,0)) AS ZValue1"
                + @",Environmental.FValue,Environmental.GValue,(ISNULL(Environmental.FValue,0) + ISNULL(Environmental.GValue,0)) AS ZValue2"
                + @" ,Sys_ConstEType.ConstText AS ETypeName,Sys_ConstESmallType.ConstText AS SmallTypeName "
                + @" FROM dbo.Technique_Environmental AS Environmental"
                + @" LEFT JOIN Sys_Const AS  Sys_ConstEType  ON Environmental.EType=Sys_ConstEType.ConstValue and Sys_ConstEType.GroupId='" + BLL.ConstValue.Group_EnvironmentalType + "'"
                + @" LEFT JOIN Sys_Const AS Sys_ConstESmallType ON Environmental.SmallType=Sys_ConstESmallType.ConstValue and Sys_ConstESmallType.GroupId='" + BLL.ConstValue.Group_EnvironmentalSmallType + "'"
                + @" WHERE (Environmental.IsCompany IS NULL OR Environmental.IsCompany='False') ";
            List<SqlParameter> listStr = new List<SqlParameter>();           
            if (this.drpEType.SelectedValue!=BLL.Const._Null)
            {
                strSql += " AND Environmental.EType= @EType";
                listStr.Add(new SqlParameter("@EType", this.drpEType.SelectedValue));
            }
            if (this.drpSmallType.SelectedValue != BLL.Const._Null)
            {
                strSql += " AND Environmental.SmallType= @SmallType";
                listStr.Add(new SqlParameter("@SmallType", this.drpSmallType.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtActivePoint.Text.Trim()))
            {
                strSql += " AND Environmental.ActivePoint LIKE @ActivePoint";
                listStr.Add(new SqlParameter("@ActivePoint", "%" + this.txtActivePoint.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtEnvironmentalFactors.Text.Trim()))
            {
                strSql += " AND Environmental.EnvironmentalFactors LIKE @EnvironmentalFactors";
                listStr.Add(new SqlParameter("@EnvironmentalFactors", "%" + this.txtEnvironmentalFactors.Text.Trim() + "%"));
            }
            strSql += " order by Environmental.SmallType, Environmental.EType,Environmental.Code";

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                bool isShow = false;
                if (Grid1.SelectedRowIndexArray.Length == 1)
                {
                    isShow = true;
                }
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (this.judgementDelete(rowID, isShow))
                    {
                        BLL.Technique_EnvironmentalService.DeleteEnvironmental(rowID);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除环境因素危险源");
                BindGrid();
                //ShowNotify("删除数据成功!");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            var environmentalRiskItem = Hazard_EnvironmentalRiskItemService.GetEnvironmentalRiskItemByEnvironmentalId(rowID);
            if (environmentalRiskItem.Count > 0)
            {
                content = "环境因素识别与评价中已使用了该环境因素，不能删除！";
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content, MessageBoxIcon.Warning);
                }
                return false;
            }
        }
        #endregion

        #region 数据编辑事件
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
        /// 编辑
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
                Alert.ShowInTop("请选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string EnvironmentalId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("EnvironmentalEdit.aspx?EnvironmentalId={0}", EnvironmentalId, "编辑 - ")));
        }
        #endregion

        #region 根据表头信息过滤列表数据
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

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

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
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
                        string str=value.ToString();
                        if (value.ToString() == "False")
                        {
                            str = "否";
                        }
                        else if(value.ToString()=="True")
                        {
                            str = "是";
                        }
                        string html = str;
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

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.EnvironmentalMenuId);
            if (buttonList.Count > 0)
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
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnSelectColumns.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnIn))
                {
                    this.btnImport.Hidden = false;
                }
            }
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

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("EnvironmentalIn.aspx?IsCompany=False", "导入 - ")));
        }
        #endregion
    }
}