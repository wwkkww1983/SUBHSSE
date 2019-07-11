using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonPunishRecordSearch : PageBase
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
                this.drpUnit.DataTextField = "UnitName";
                this.drpUnit.DataValueField = "UnitId";
                this.drpUnit.DataSource = BLL.UnitService.GetUnitListByProjectId(this.CurrUser.LoginProjectId);
                this.drpUnit.DataBind();
                Funs.FineUIPleaseSelect(this.drpUnit);

                this.drpPerson.DataTextField = "PersonName";
                this.drpPerson.DataValueField = "PersonId";
                Funs.FineUIPleaseSelect(this.drpPerson);
            }
        }
        #endregion

        protected void drpUnit_TextChanged(object sender, EventArgs e)
        {
            this.drpPerson.Items.Clear();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                this.drpPerson.DataSource = BLL.PersonService.GetPersonLitsByprojectIdUnitId(this.CurrUser.LoginProjectId, this.drpUnit.SelectedValue);
                this.drpPerson.DataBind();
            }
            Funs.FineUIPleaseSelect(this.drpPerson);
            this.drpPerson.SelectedValue = BLL.Const._Null;
            BindGrid();
        }

        protected void drpPerson_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            var punishRecords = (from x in Funs.DB.View_Common_PunishRecord
                                 where x.ProjectId == this.CurrUser.LoginProjectId
                                 orderby x.PunishDate descending
                                 select x).ToList();
            if (this.drpUnit.SelectedValue != BLL.Const._Null)
            {
                punishRecords = punishRecords.Where(x => x.UnitId == this.drpUnit.SelectedValue).ToList();
            }
            if (this.drpPerson.SelectedValue != BLL.Const._Null)
            {
                punishRecords = punishRecords.Where(x => x.PersonId == this.drpPerson.SelectedValue).ToList();
            }
            Grid1.DataSource = punishRecords;
            Grid1.DataBind();
            decimal totalScore = punishRecords.Sum(x => x.Deduction ?? 0);
            JObject summary = new JObject();
            summary.Add("PunishReason", "合计：");
            summary.Add("Deduction", totalScore);
            Grid1.SummaryData = summary;
        }

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonListMenuId, BLL.Const.BtnModify))
            {
                btnMenuEdit_Click(null, null);
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            //if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HJGL_PersonManageMenuId, BLL.Const.BtnModify))
            //{
            //    if (Grid1.SelectedRowIndexArray.Length == 0)
            //    {
            //        Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    string punishRecordId = Grid1.SelectedRowID;
            //    if (this.btnMenuEdit.Hidden)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
            //    {
            //        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishRecordView.aspx?PunishRecordId={0}", punishRecordId, "查看 - ")));
            //    }
            //    else
            //    {
            //        PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("PunishRecordEdit.aspx?PunishRecordId={0}", punishRecordId, "编辑 - ")));
            //    }
            //}
            //else
            //{
            //    ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            //}
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonListMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        var getV = BLL.HSSE_Hazard_PunishRecordService.GetPunishRecordById(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser,null, getV.PunishRecordId, BLL.Const.PersonListMenuId, BLL.Const.BtnDelete);
                            BLL.HSSE_Hazard_PunishRecordService.DeletePunishRecordByPunishRecordId(rowID);
                        }
                    }
                    string personId = Request.Params["PersonId"];
                    var punishRecords = (from x in Funs.DB.View_Common_PunishRecord
                                         where x.PersonId == personId
                                         orderby x.PunishDate descending
                                         select x).ToList();
                    Grid1.DataSource = punishRecords;
                    Grid1.DataBind();
                    ShowNotify("删除数据成功!（表格数据已重新绑定）");
                }
            }
            else
            {
                ShowNotify("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            string personId = Request.Params["PersonId"];
            var punishRecords = (from x in Funs.DB.View_Common_PunishRecord
                                 where x.PersonId == personId
                                 orderby x.PunishDate descending
                                 select x).ToList();
            Grid1.DataSource = punishRecords;
            Grid1.DataBind();
        }

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("人员处罚记录" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            BindGrid();
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
                        html = (row.FindControl("labNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfI")
                    {
                        html = "'" + (row.FindControl("lbI") as AspNet.Label).Text;
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