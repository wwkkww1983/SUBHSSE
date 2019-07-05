using System;
using System.Data;
using System.Linq;
using System.Text;
using BLL;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataPlanItem : PageBase
    {
        #region 项目id
        /// <summary>
        /// 项目id
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

        #region Page_Init
        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            this.ProjectId = Request.Params["ProjectId"];
            this.InitGrid();
        }

        /// <summary>
        /// 构造GV
        /// </summary>
        private void InitGrid()
        {            
            FineUIPro.BoundField bf;
            bf = new FineUIPro.BoundField
            {
                DataField = "Code",
                DataFormatString = "{0}",
                HeaderText = "编号"
            };
            Grid1.Columns.Add(bf);

            bf = new FineUIPro.BoundField
            {
                DataField = "Title",
                DataFormatString = "{0}",
                HeaderText = "考核内容",
                Width = 180
            };
            Grid1.Columns.Add(bf);

            bf = new FineUIPro.BoundField
            {
                DataField = "Score",
                DataFormatString = "{0}",
                HeaderText = "分值"
            };
            Grid1.Columns.Add(bf);

            var safetyDataPlan = from x in Funs.DB.View_SafetyData_SafetyDataPlan
                                 where x.ProjectId == this.ProjectId
                                 orderby x.CheckDate
                                 select x;
            if (safetyDataPlan.Count() > 0)
            {
                DateTime minCheckDate = safetyDataPlan.Where(x => x.CheckDate.HasValue).Select(x => x.CheckDate.Value).Min();
                DateTime maxCheckDate = safetyDataPlan.Where(x => x.CheckDate.HasValue).Select(x => x.CheckDate.Value).Max();
                //var safetyDataIdList = safetyDataPlan.Select(x => x.SafetyDataId).Distinct();
                ////算出 开始、结束时间跨度 然后循环增加一个月 并把在此时间段的 考核项写入计划表
                for (int i = 0; minCheckDate.AddMonths(i) <= maxCheckDate; i++)
                {
                    bf = new FineUIPro.BoundField
                    {
                        DataField = string.Format("{0:yyyy-MM}", minCheckDate.AddMonths(i)),
                        DataFormatString = "{0}",
                        HeaderText = string.Format("{0:yyyy-MM}", minCheckDate.AddMonths(i))
                    };
                    if (minCheckDate.AddMonths(i) >= System.DateTime.Now)
                    {
                        bf.HeaderText = "<font color='#FF7575'>" + bf.HeaderText + "</font>";
                    }
                    Grid1.Columns.Add(bf);
                }              
            }

            bf = new FineUIPro.BoundField
            {
                DataField = "Remark",
                DataFormatString = "{0}",
                HeaderText = "备注",
                Width = 250
            };
            Grid1.Columns.Add(bf);
           
            Grid1.DataKeyNames = new string[] { "SafetyDataPlanId", "Title" };
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
                ////权限按钮方法
                this.GetButtonPower();
                ////权限按钮方法
                //this.ProjectId = Request.Params["projectId"];               
                //this.InitGrid();
                // 绑定表格              
                this.BindGrid();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            DataTable table = this.GetDataTable();
            Grid1.DataSource = table;
            Grid1.DataBind();       
        }

        #region GetDataTable

        /// <summary>
        /// 获取模拟表格
        /// </summary>
        /// <returns></returns>
        public  DataTable GetDataTable()
        {
            DataTable table = new DataTable();           
            table.Columns.Add(new DataColumn("Code", typeof(String)));
            table.Columns.Add(new DataColumn("Title", typeof(String)));
            table.Columns.Add(new DataColumn("Score", typeof(decimal)));
            var safetyDataPlan = from x in Funs.DB.View_SafetyData_SafetyDataPlan                               
                                 where x.ProjectId == this.ProjectId
                                 orderby x.CheckDate
                                 select x;
            DateTime minCheckDate = System.DateTime.Now;
            DateTime maxCheckDate = System.DateTime.Now;
            if (safetyDataPlan.Count() > 0)
            {
                 minCheckDate = safetyDataPlan.Where(x => x.CheckDate.HasValue).Select(x => x.CheckDate.Value).Min();
                 maxCheckDate = safetyDataPlan.Where(x => x.CheckDate.HasValue).Select(x => x.CheckDate.Value).Max();                
                ////算出 开始、结束时间跨度 然后循环增加一个月 并把在此时间段的 考核项写入计划表
                for (int i = 0; minCheckDate.AddMonths(i) <= maxCheckDate; i++)
                {
                    table.Columns.Add(new DataColumn(string.Format("{0:yyyy-MM}", minCheckDate.AddMonths(i)), typeof(String)));
                }
            }
            table.Columns.Add(new DataColumn("Remark", typeof(String)));

            if (safetyDataPlan.Count() > 0)
            {
                var safetyDataList = safetyDataPlan.OrderBy(x=>x.Code).ToList();
                var safetyDataIdList = safetyDataList.Select(x => x.SafetyDataId).Distinct();
                if (safetyDataIdList.Count() > 0)
                {
                    foreach (var itemSafetyDataId in safetyDataIdList)
                    {                      
                        ////算出 开始、结束时间跨度 然后循环增加一个月 并把在此时间段的 考核项写入计划表
                        var safeData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(itemSafetyDataId);
                        if (safeData != null)
                        {
                            var rowSafePlan = from x in  safetyDataPlan                                               
                                              where x.SafetyDataId == itemSafetyDataId 
                                              select x;
                            if (rowSafePlan.Count() > 0)
                            {
                                DataRow row = table.NewRow();
                                row[0] = rowSafePlan.FirstOrDefault().Code;
                                row[1] = rowSafePlan.FirstOrDefault().Title;
                                decimal? score =rowSafePlan.FirstOrDefault().Score;
                                row[2] = score.HasValue ? score.Value : 0;
                                int rowCount = 3;
                                for (int i = 0; minCheckDate.AddMonths(i) <= maxCheckDate; i++)
                                {
                                    string getCheckMonth = string.Format("{0:yyyy-MM}", minCheckDate.AddMonths(i));
                                    var rowItemPlan = rowSafePlan.FirstOrDefault(x => x.CheckMonth == getCheckMonth);
                                    if (safeData != null && rowItemPlan != null)
                                    {
                                        row[i + 3] = string.Format("{0:yyyy-MM-dd}", rowItemPlan.CheckDate);                                        
                                    }

                                    rowCount = i + 4;
                                }
                                row[rowCount] = rowSafePlan.FirstOrDefault().Remark;
                                table.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            
            return table;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {          
            this.BindGrid();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全资料计划总表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 50000;
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
                    if (column.ColumnID == "tfPageIndex")
                    {
                        html = (row.FindControl("lblPageIndex") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTime")
                    {
                        html = (row.FindControl("lblWorkTime") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfWorkTimeYear")
                    {
                        html = (row.FindControl("lblWorkTimeYear") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfTotal")
                    {
                        html = (row.FindControl("lblTotal") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
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
            string menuId = BLL.Const.ServerSafetyDataPlanMenuId;
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnAdd.Hidden = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// 删除当前项目不考核项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataPlanItemDelete.aspx?ProjectId={0}", this.ProjectId, "删除考核项 - ")));
        }   
        /// <summary>
        /// 恢复当前项目不考核项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataPlanItemAdd.aspx?ProjectId={0}", this.ProjectId, "恢复考核项 - ")));
        }
    }
}