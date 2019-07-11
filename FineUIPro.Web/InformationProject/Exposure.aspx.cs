using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.InformationProject
{
    public partial class Exposure : PageBase
    {
        #region 项目主键
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }

                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("ExposureEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion

        #region 绑定数据GV
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT Exposure.ExposureId,Exposure.ProjectId,CodeRecords.Code,Exposure.ExposureDate,Exposure.UnitId,Exposure.FileName,Exposure.Remark,Exposure.CompileMan"
                            + @",Exposure.CompileDate,Exposure.UnitName,Users.UserName AS CompileManName,ExUnit.UnitName AS ExUnitName"
                            + @",(CASE WHEN LEN(Exposure.FileName) > 40 THEN LEFT(Exposure.FileName,40)+'...' ELSE Exposure.FileName END) AS ShortFileName"
                            + @",(CASE WHEN LEN(Exposure.Remark) > 40 THEN LEFT(Exposure.Remark,40)+'...' ELSE Exposure.Remark END) AS ShortRemark"
                            + @" FROM InformationProject_Exposure AS Exposure "
                            + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON Exposure.ExposureId = CodeRecords.DataId "
                            + @" LEFT JOIN Sys_User AS Users ON Exposure.CompileMan = Users.UserId "
                            + @" LEFT JOIN Base_Unit AS ExUnit ON ExUnit.UnitId = Users.UnitId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND Exposure.ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));
            if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
            {
                strSql += " AND Exposure.UnitId LIKE @UnitId OR ExUnit.UnitId=@ExUnitId";
                listStr.Add(new SqlParameter("@UnitId", "%" + this.CurrUser.UnitId + "%"));
                listStr.Add(new SqlParameter("@ExUnitId", this.CurrUser.UnitId));
            }
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND Exposure.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtUnitName.Text.Trim() + "%"));
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

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
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
            BindGrid();
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
            this.BindGrid();
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 双击事件
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
            string id = Grid1.SelectedRowID;
            var txposure = BLL.ExposureService.GetExposureById(id);
            if (txposure != null)
            {
                bool isEdit = false;
                var user = BLL.UserService.GetUserByUserId(txposure.CompileMan);
                if (user.UnitId == this.CurrUser.UnitId && !this.btnMenuEdit.Hidden)
                {
                    isEdit = true;
                }

                if (isEdit)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ExposureEdit.aspx?ExposureId={0}", id), "违章曝光台维护"));
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ExposureView.aspx?ExposureId={0}", id), "违章曝光台查看"));
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
                        var getV = BLL.ExposureService.GetExposureById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.ExposureCode, rowID, BLL.Const.ProjectExposureMenuId, BLL.Const.BtnDelete);
                            BLL.ExposureService.DeleteExposureById(rowID);
                        }
                    }
                }

                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            var exposure = Funs.DB.InformationProject_Exposure.FirstOrDefault(x => x.ExposureId == id);
            if (exposure != null)
            {
                var user = BLL.UserService.GetUserByUserId(exposure.CompileMan);
                if (user.UnitId != this.CurrUser.UnitId)
                {
                    content = "该条信息不是本单位编制，不能删除！";
                }
            }
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.ProjectId, this.CurrUser.UserId, BLL.Const.ProjectExposureMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("违规曝光台" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
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
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfFileName")
                    {
                        html = (row.FindControl("lblFileName") as AspNet.Label).ToolTip;
                    }
                    if (column.ColumnID == "tfRemark")
                    {
                        html = (row.FindControl("lblRemark") as AspNet.Label).ToolTip;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 获取受检单位/班组
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object UnitId)
        {
            string name = string.Empty;
            if (UnitId != null)
            {
                if (!string.IsNullOrEmpty(UnitId.ToString()))
                {
                    if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                    {
                        if (UnitId.ToString().Contains(this.CurrUser.UnitId))
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                            name = unit.UnitName;
                        }
                    }
                    else
                    {
                        string[] unitIds = UnitId.ToString().Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(item);
                            name += unit.UnitName + ",";
                        }
                        if (!string.IsNullOrEmpty(name))
                        {
                            name = name.Substring(0, name.LastIndexOf(","));
                        }
                    }
                }
            }
            return name;
        }
        #endregion
    }
}