using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonChange : PageBase
    {
        #region 定义项
        /// <summary>
        /// 项目主键ID
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.CurrUser.LoginProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                ////权限按钮方法
                this.GetButtonPower();
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                } 
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.InitTreeMenu();//加载树
                this.txtTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvUnit.Nodes.Clear();
            TreeNode rootNode = new TreeNode
            {
                Text = "单位",
                NodeID = "0",
                Expanded = true
            };
            this.tvUnit.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes);
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        /// <param name="type"></param>
        private void BoundTree(TreeNodeCollection nodes)
        {
            var unitLists = BLL.ProjectUnitService.GetProjectUnitListByProjectIdUnitType(this.ProjectId, BLL.Const.ProjectUnitType_2);
            if (unitLists.Count() > 0)
            {
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.ProjectId, this.CurrUser.UnitId))
                {
                    unitLists = unitLists.Where(x => x.UnitId == this.CurrUser.UnitId).ToList();
                }

                TreeNode tn = null;
                foreach (var dr in unitLists)
                {
                    tn = new TreeNode();
                    var unitName = BLL.UnitService.GetUnitNameByUnitId(dr.UnitId);
                    if (unitName != null)
                    {
                        tn.Text = unitName;
                    }
                    tn.NodeID = dr.UnitId;
                    tn.EnableClickEvent = true;
                    nodes.Add(tn);
                }
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (this.tvUnit != null && this.tvUnit.SelectedNode != null && !string.IsNullOrEmpty(this.tvUnit.SelectedNodeID))
            {
                //string strSql = @"SELECT A.ProjectId,A.UnitId,A.ChangeTime AS changeTime,ISNULL(B.InCout,0) AS InCout,ISNULL(C.OutCout,0) AS OutCout"
                //       + @" ,(SELECT COUNT(*) AS TotalCout FROM SitePerson_Person WHERE InTime <=A.ChangeTime AND (OutTime IS NULL OR OutTime >= A.ChangeTime) AND ProjectId = A.ProjectId AND UnitId =A.UnitId) AS TotalCout "
                //       + @" FROM (SELECT DISTINCT ChangeTime,UnitId,ProjectId FROM SitePerson_PersonInOut) AS A "
                //       + @" LEFT JOIN (SELECT COUNT(*) AS InCout,ProjectId,UnitId,ChangeTime FROM SitePerson_PersonInOut WHERE IsIn=1  GROUP BY ProjectId,UnitId,ChangeTime,IsIn) B  "
                //       + @" ON A.ChangeTime=B.ChangeTime AND A.ProjectId=B.ProjectId AND A.UnitId=B.UnitId "
                //       + @" LEFT JOIN (SELECT COUNT(*) AS OutCout,ProjectId,UnitId,ChangeTime FROM SitePerson_PersonInOut WHERE IsIn=0 GROUP BY ProjectId,UnitId,ChangeTime,IsIn) C  "
                //       + @" ON A.ChangeTime=C.ChangeTime AND A.ProjectId=C.ProjectId AND A.UnitId=C.UnitId "
                //       + @" WHERE 1=1 ";
                //List<SqlParameter> listStr = new List<SqlParameter>();
                //strSql += " AND A.ProjectId = @ProjectId";
                //listStr.Add(new SqlParameter("@ProjectId", this.ProjectId));

                //strSql += " AND A.UnitId = @UnitId";
                //listStr.Add(new SqlParameter("@UnitId", this.tvUnit.SelectedNodeID));
                //if (!string.IsNullOrEmpty(this.txtTime.Text.Trim()))
                //{
                //    strSql += " AND A.ChangeTime = @ChangeTime";
                //    listStr.Add(new SqlParameter("@ChangeTime", this.txtTime.Text.Trim()));
                //}

                //SqlParameter[] parameter = listStr.ToArray();
                //DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                //Grid1.RecordCount = tb.Rows.Count;
                //tb = GetFilteredTable(Grid1.FilteredData, tb);
                //var table = this.GetPagedDataTable(Grid1, tb);
                //Grid1.DataSource = table;
                //Grid1.DataBind();

                List<Model.SitePerson_PersonInOut> personInOuts = new List<Model.SitePerson_PersonInOut>();
                Model.SitePerson_PersonInOut personInOut = new Model.SitePerson_PersonInOut
                {
                    PersonInOutId = SQLHelper.GetNewID(typeof(Model.SitePerson_PersonInOut)),
                    UnitId = this.tvUnit.SelectedNodeID,
                    ChangeTime = Funs.GetNewDateTime(this.txtTime.Text.Trim())
                };
                var persons = from x in Funs.DB.SitePerson_Person
                              where x.ProjectId == this.ProjectId && x.UnitId == this.tvUnit.SelectedNodeID
                              select x;
                personInOut.InCount = (from x in persons
                                       where x.InTime != null && x.InTime == Funs.GetNewDateTime(this.txtTime.Text.Trim())
                                       select x).Count();
                personInOut.OutCount = (from x in persons
                                        where x.OutTime != null && x.OutTime == Funs.GetNewDateTime(this.txtTime.Text.Trim())
                                        select x).Count();
                personInOut.TotalCount = (from x in persons
                                          where x.InTime != null && x.InTime <= Funs.GetNewDateTime(this.txtTime.Text.Trim())
                                          && (x.OutTime == null || x.OutTime > Funs.GetNewDateTime(this.txtTime.Text.Trim()))
                                          select x).Count();
                personInOuts.Add(personInOut);
                Grid1.DataSource = personInOuts;
                Grid1.DataBind();

            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvUnit_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region GV 排序 页码
        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        /// <summary>
        /// 出入场按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInOut_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonChangeEdit.aspx", "出入场 - ")));
        }

        //#region 双击查看事件
        ///// <summary>
        ///// Grid双击事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        //{
        //    if (Grid1.SelectedRowIndexArray.Length == 0)
        //    {
        //        Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
        //        return;
        //    }

        //    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PersonChangeEdit.aspx?PersonInOutId={0}", Grid1.SelectedRowID, "查看 - ")));
        //}
        //#endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ProjectPersonChangeMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    //this.btnInOut.Hidden = false;
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
            if (this.tvUnit.SelectedNode != null)
            {
                Response.ClearContent();
                string filename = this.tvUnit.SelectedNode.Text;
                Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员出入场记录" + filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                this.Grid1.PageSize = 500;
                BindGrid();
                Response.Write(GetGridTableHtml(Grid1));
                Response.End();
            }
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
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfI")
                    {
                        html = (row.FindControl("lbI") as AspNet.Label).Text;
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
            this.BindGrid();
        }
        #endregion
    }
}