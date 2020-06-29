using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.Personal
{
    public partial class APPFile : PageBase
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
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT * FROM View_Common_Attach_Image WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtimage_series.Text.Trim()))
            {
                strSql += " AND image_series LIKE @image_series";
                listStr.Add(new SqlParameter("@image_series", "%" + this.txtimage_series.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtfile_name.Text.Trim()))
            {
                strSql += " AND file_name LIKE @file_name";
                listStr.Add(new SqlParameter("@file_name", "%" + this.txtfile_name.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(txtStartcreated_date.Text.Trim()))
            {
                strSql += " AND created_date >= @Startcreated_date";
                listStr.Add(new SqlParameter("@Startcreated_date", this.txtStartcreated_date.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtEndcreated_date.Text.Trim()))
            {
                strSql += " AND created_date <= @Endcreated_date";
                listStr.Add(new SqlParameter("@Endcreated_date", this.txtEndcreated_date.Text.Trim()));
            }
            if (!string.IsNullOrEmpty(this.txtcreated_by.Text.Trim()))
            {
                strSql += " AND created_byManName LIKE @created_by";
                listStr.Add(new SqlParameter("@created_by", "%" + this.txtcreated_by.Text.Trim() + "%"));
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

        #region 过滤表头、排序、分页、关闭窗口
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
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
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
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string attach_image_id = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "del")
            {
                Model.Attach_Image image = BLL.Attach_ImageService.GetAttach_ImageByattach_image_id(attach_image_id);
                if (image != null && (image.Created_by == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId))
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, image.File_name, attach_image_id, BLL.Const.PersonalFolderMenuId, BLL.Const.BtnDelete);
                    BLL.Attach_ImageService.DeleteAttach_ImageByattach_image_id(attach_image_id);                    
                    BindGrid();
                    ShowNotify("删除成功!", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("您不是创建人或系统管理员，无法删除！", MessageBoxIcon.Warning);
                    return;
                }
            }
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

            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    Model.Attach_Image image = BLL.Attach_ImageService.GetAttach_ImageByattach_image_id(rowID);
                    if (image != null && (image.Created_by == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId))
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, image.File_name, rowID, BLL.Const.PersonalFolderMenuId, BLL.Const.BtnDelete);
                        BLL.Attach_ImageService.DeleteAttach_ImageByattach_image_id(rowID);
                        
                    }
                    else
                    {
                        Alert.ShowInTop("您不是创建人或系统管理员，无法删除！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                BindGrid();
                ShowNotify("删除成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertImageUrl(object attach_image_id)
        {
            string url = string.Empty;
            if (attach_image_id != null)
            {
                var registration = BLL.Attach_ImageService.GetAttach_ImageByattach_image_id(attach_image_id.ToString());
                if (registration != null)
                {
                    url = BLL.UploadAttachmentService.ShowAttachment("../", registration.File_path);
                }
            }
            return url;
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("手机APP上传内容" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “APPFile.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “APPFile.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfImageUrl")
                    {
                        html = (row.FindControl("lbtnImageUrl") as AspNet.LinkButton).Text;
                    }
                    if (column.ColumnID == "tfRectificationImageUrl")
                    {
                        html = (row.FindControl("lbtnRectificationImageUrl") as AspNet.LinkButton).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtStartcreated_date.Text.Trim()) && !string.IsNullOrEmpty(this.txtEndcreated_date.Text.Trim()))
            {
                if (Funs.GetNewDateTime(this.txtStartcreated_date.Text.Trim()) > Funs.GetNewDateTime(this.txtEndcreated_date.Text.Trim()))
                {
                    Alert.ShowInTop("开始时间不能大于结束时间！", MessageBoxIcon.Warning);
                    return;
                }
            }
            this.BindGrid();
        }
        #endregion
    }
}