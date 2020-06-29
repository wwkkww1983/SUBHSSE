using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectSet : PageBase
    {
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
                ////权限按钮方法
                this.GetButtonPower();
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, string.Empty, true);
                this.btnNew.OnClientClick = Window1.GetShowReference("ProjectSetSave.aspx") + "return false;";
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

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "SELECT Project.ProjectId,Project.ProjectCode,Project.ProjectName,unit.UnitName,Project.StartDate,Project.EndDate,Project.ProjectAddress,SysConst.ConstText AS ProjectTypeName, "
                          + @" (CASE WHEN ProjectState='" + BLL.Const.ProjectState_2 + "' THEN '暂停中' WHEN ProjectState='" + BLL.Const.ProjectState_3 + "' THEN '已完工' WHEN SysConst.ConstText='E' THEN '设计中' ELSE '施工中' END) AS ProjectStateName,Project.ProjectState"
                          + @" FROM Base_Project AS Project LEFT JOIN Base_Unit as unit on unit.UnitId=Project.UnitId LEFT JOIN Sys_Const AS SysConst ON Project.ProjectType =SysConst.ConstValue AND SysConst.GroupId='" + BLL.ConstValue.Group_ProjectType + "'"
                          + @" WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();            
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文件柜查看页面传项目值
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));               
            }
            else if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
            {
                strSql += " AND ProjectName LIKE @ProjectName";
                listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtProjectCode.Text.Trim()))
            {
                strSql += " and ProjectCode LIKE @ProjectCode";
                listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtProjectCode.Text.Trim() + "%"));
            }
            if (this.drpUnit.SelectedValue != BLL.Const._Null && !String.IsNullOrEmpty(this.drpUnit.SelectedValue) )
            {
                strSql += " and Project.UnitId = @UnitId";
                listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
            }
            if (this.ckState.SelectedValue != "0")
            {
                if (this.ckState.SelectedValue == "1")
                {
                    strSql += " AND (ProjectState = '1' OR ProjectState IS NULL)";
                }
                else
                {
                    strSql += " AND (ProjectState = '2' OR ProjectState = '3' )";
                }
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            BLL.UnitService.InitUnitDropDownList(this.drpUnit, string.Empty, true);
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null && thisUnit.UnitId == BLL.Const.UnitId_6)
            {
                this.drpUnit.Label = "所属分公司";
                this.Grid1.Columns[10].HeaderText = "所属分公司";
            }
        }
        #endregion

        #region 操作 Events
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
                        var project = BLL.ProjectService.GetProjectByProjectId(rowID);
                        if (project != null)
                        {                           
                            BLL.LogService.DeleteLog(rowID);
                            BLL.ReportRemindService.DeleteReportRemindByProjectId(rowID);
                            BLL.ProjectUnitService.DeleteProjectUnitByProjectId(rowID);
                            BLL.ProjectService.DeleteProject(rowID);
                        }
                    }
                }

                BindGrid();
                ShowNotify("操作完成!", MessageBoxIcon.Success);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }
        
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

            var project = BLL.ProjectService.GetProjectByProjectId(Grid1.SelectedRowID);
            if (project != null)
            {
                if (project.ProjectState == BLL.Const.ProjectState_2 ||project.ProjectState == BLL.Const.ProjectState_3)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSetView.aspx?ProjectId={0}", Grid1.SelectedRowID, "查看 - ")));
                }                
                else
                {
                    if (this.btnMenuEdit.Hidden && this.CurrUser.UserId != BLL.Const.sysglyId)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSetView.aspx?ProjectId={0}", Grid1.SelectedRowID, "查看 - ")));
                    }
                    else
                    {
                        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSetSave.aspx?ProjectId={0}", Grid1.SelectedRowID, "编辑 - ")));
                    }
                }
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnView_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectShutdown.aspx?projectId={0}&value=0", Grid1.SelectedRowID, "查看 - ")));
        }

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
            string menuId = BLL.Const.SeverProjectSetMenuId;
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                menuId = BLL.Const.ProjectSetMenuId;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify) || buttonList.Contains(BLL.Const.BtnSave))
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

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.ProjectData_TeamGroup.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目班组】中使用，不能删除！";
            }
            if (Funs.DB.ProjectData_WorkArea.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【作业区域】中使用，不能删除！";
            }
            if (Funs.DB.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目单位】中使用，不能删除！";
            }
            if (Funs.DB.Project_ProjectUser.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目用户】中使用，不能删除！";
            }
            if (Funs.DB.SecuritySystem_SafetyManageOrganization.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目安全管理组织机构表】中使用，不能删除！";
            }
            if (Funs.DB.SecuritySystem_SafetyOrganization.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目安全管理机构表】中使用，不能删除！";
            }
            if (Funs.DB.SecuritySystem_SafetySystem.FirstOrDefault(x => x.ProjectId == id) != null)
            {
                content += "该项目已在【项目安全组织体系表】中使用，不能删除！";
            }
            var sysCode = Funs.DB.Sys_CodeRecords.Where(x => x.ProjectId == id);
            if (sysCode.Count()> 0)
            {
                foreach (var item in sysCode)
                {
                    var menu = Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == item.MenuId);
                    if (menu != null)
                    {
                        content += "该项目已在【" + menu.MenuName + "】中使用，不能删除！";
                    }
                    else
                    {

                        content += "该项目已在【单据编码】中使用，不能删除！";
                    }
                }
            }

            var flowOperate = Funs.DB.Sys_FlowOperate.Where(x => x.ProjectId == id);
            if (flowOperate.Count() > 0)
            {
                foreach (var item in flowOperate)
                {
                    var menu = Funs.DB.Sys_Menu.FirstOrDefault(x => x.MenuId == item.MenuId);
                    if (menu != null)
                    {
                        content += "该项目已在【" + menu.MenuName + "】中使用，不能删除！";
                    }
                    else
                    {

                        content += "该项目已在【审核流程】中使用，不能删除！";
                    }
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

        #region 项目经理
        /// <summary>
        /// 项目经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertProjectManager(object projectId)
        {            
            return BLL.ProjectService.GetProjectManagerName(projectId.ToString());
        }

        /// <summary>
        /// 施工经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertConstructionManager(object projectId)
        {
            return BLL.ProjectService.GetConstructionManagerName(projectId.ToString());
        }

        /// <summary>
        /// 安全经理
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        protected string ConvertHSSEManager(object projectId)
        {
            return BLL.ProjectService.GetHSSEManagerName(projectId.ToString());
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("项目信息" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “ProjectSet.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “ProjectSet.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfPM")
                    {
                        html = (row.FindControl("lblPM") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfCM")
                    {
                        html = (row.FindControl("lblCM") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfHSSEM")
                    {
                        html = (row.FindControl("lblHSSEM") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion        
    }
}