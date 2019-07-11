using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataEPlan : PageBase
    {
        
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
                this.ddlPageSize.SelectedValue = this.Grid1.PageSize.ToString();

                int ThisMonth = DateTime.Now.Month;
                int FirstMonthOfSeason = ThisMonth - (ThisMonth % 3 == 0 ? 3 : (ThisMonth % 3)) + 1;
                DateTime date1 = new DateTime(DateTime.Now.AddMonths(FirstMonthOfSeason - ThisMonth).Year, DateTime.Now.AddMonths(FirstMonthOfSeason - ThisMonth).Month, 1).AddMonths(-1);
                this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", date1);
                this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(3).AddDays(5));

                if (date1.AddMonths(3).AddDays(5) < DateTime.Now)
                {
                    this.txtStarTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(3));
                    this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd}", date1.AddMonths(6).AddDays(5));
                }

                this.ProjectDataBind(); ///加载树
            }
        }

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void ProjectDataBind()
        {
            this.tvProject.Nodes.Clear();
            this.tvProject.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "项目",
                Expanded = true,
                NodeID = "0"
            };//定义根节点

            this.tvProject.Nodes.Add(rootNode);
            var projects = BLL.ProjectService.GetProjectByProjectTypeDropDownList("5");
            foreach (var item in projects)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = item.ProjectCode + "：" + item.ProjectName,
                    NodeID = item.ProjectId,
                    EnableClickEvent = true
                };
                var safetyDataEPlan = Funs.DB.SafetyDataE_SafetyDataEPlan.FirstOrDefault(x => x.ProjectId == item.ProjectId && x.States == BLL.Const.State_1);
                if (safetyDataEPlan != null)
                {
                    newNode.ToolTip = "存在待审核计划考核项！";
                    newNode.Text = "<font color='#FF7575'>" + newNode.Text + "</font>";
                }

                rootNode.Nodes.Add(newNode);
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvProject_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 树 右键查看时间总表
        /// <summary>
        /// 树 右键查看时间总表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTreeView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tvProject.SelectedNodeID))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafetyDataEPlanItem.aspx?ProjectId={0}", this.tvProject.SelectedNodeID, "查看 - ")));
            }
        }
        #endregion
        
        #region GV 绑定数据
        /// <summary>
        /// GV 绑定数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.tvProject.SelectedNodeID))
            {
                string strSql = "SELECT p.SafetyDataEPlanId,p.ProjectId,p.SafetyDataEId,SafetyDataE.Code,SafetyDataE.Title,p.CheckDate "
                         + @",p.Score,p.Remark,p.ReminderDate,p.SubmitDate,p.ShouldScore,p.RealScore"
                         + @" FROM dbo.SafetyDataE_SafetyDataEPlan AS p "
                         + @" LEFT JOIN SafetyDataE_SafetyDataE AS SafetyDataE ON P.SafetyDataEId = SafetyDataE.SafetyDataEId "
                         + @" WHERE  p.States='" + BLL.Const.State_2 + "' AND ProjectId ='" + this.tvProject.SelectedNodeID + "'";
                List<SqlParameter> listStr = new List<SqlParameter>();               
                if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
                {
                    strSql += " AND (SafetyDataE.Title LIKE @Title OR SafetyDataE.Code LIKE @Title)";
                    listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtStarTime.Text.Trim()))
                {
                    strSql += " AND p.CheckDate >= @StarTime";
                    listStr.Add(new SqlParameter("@StarTime", Funs.GetNewDateTime(this.txtStarTime.Text.Trim())));
                }
                if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
                {
                    strSql += " AND p.CheckDate <= @EndTime";
                    listStr.Add(new SqlParameter("@EndTime", Funs.GetNewDateTime(this.txtEndTime.Text.Trim())));
                }

                if (this.drpState.SelectedValue == "1")
                {
                    strSql += " AND p.SubmitDate IS NULL";
                }
                else if (this.drpState.SelectedValue == "2")
                {
                    strSql += " AND p.SubmitDate IS NOT NULL";
                }
                else if (this.drpState.SelectedValue == "3")
                {
                    strSql += " AND p.SubmitDate IS NULL AND p.CheckDate < @Now";
                    listStr.Add(new SqlParameter("@Now", System.DateTime.Now));
                }
                else if (this.drpState.SelectedValue == "4")
                {
                    strSql += " AND p.SubmitDate IS NOT NULL AND p.CheckDate < p.SubmitDate";
                }
                else if (this.drpState.SelectedValue == "5")
                {
                    strSql += " AND p.SubmitDate IS NOT NULL AND p.CheckDate >= p.SubmitDate";
                }

                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);
                this.OutputSummaryData(tb); ///取合计值
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string SafetyDataEPlanId = Grid1.Rows[i].DataKeys[0].ToString();
                    var SafetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(SafetyDataEPlanId); ///考核计划
                    if (SafetyDataEPlan != null)
                    {
                        if (SafetyDataEPlan.SubmitDate.HasValue) ////已提交
                        {
                            if (SafetyDataEPlan.CheckDate < SafetyDataEPlan.SubmitDate)  ///过期提交
                            {
                                Grid1.Rows[i].RowCssClass = "Purple";
                            }
                            else
                            {
                                Grid1.Rows[i].RowCssClass = "Green";
                            }
                        }
                        else  ///未提交
                        {
                            if (SafetyDataEPlan.CheckDate >= System.DateTime.Now)  ///未到结束时间
                            {
                                if (SafetyDataEPlan.ReminderDate.HasValue && SafetyDataEPlan.ReminderDate.Value <= System.DateTime.Now.AddDays(7))
                                {
                                    Grid1.Rows[i].RowCssClass = "Yellow";
                                }
                            }
                            else
                            {
                                Grid1.Rows[i].RowCssClass = "Red"; ///超期未提交
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 计算合计
        /// <summary>
        /// 计算合计
        /// </summary>
        private void OutputSummaryData(DataTable tb)
        {
            decimal ScoreT = 0;//计划分 
            decimal ShouldScoreT = 0;//应得分
            decimal RealScoreT = 0;//实际得分          
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["Score"].ToString());
                ShouldScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["ShouldScore"].ToString());
                RealScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["RealScore"].ToString());
            }

            JObject summary = new JObject();
            summary.Add("Title", "合计：");
            summary.Add("Score", ScoreT);
            summary.Add("ShouldScore", ShouldScoreT);
            summary.Add("RealScore", RealScoreT);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region GV 排序翻页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            this.BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 增加、修改、删除企业安全管理资料明细事件
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            var project = BLL.ProjectService.GetProjectByProjectId(this.tvProject.SelectedNodeID);
            if (project != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafetyDataEPlanAdd.aspx?ProjectId={0}", project.ProjectId, "新增 - ")));
            }            
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVModify_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 双击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
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
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

            PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("SafetyDataEPlanEdit.aspx?SafetyDataEPlanId={0}", Grid1.SelectedRowID), "修改明细", 800, 400));
        }

        /// <summary>
        /// 删除明细方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVDel_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.LogService.AddSys_Log(this.CurrUser, null, rowID, BLL.Const.ServerSafetyDataEPlanMenuId, BLL.Const.BtnDelete);
                    BLL.SafetyDataEPlanService.DeleteSafetyDataEPlanByID(rowID);
                }

                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 查看项目上报资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGVView_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！",MessageBoxIcon.Warning);
                return;
            }

            var SafetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(Grid1.SelectedRowID);
            if (SafetyDataEPlan != null && SafetyDataEPlan.SubmitDate.HasValue)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ProjectSafetyDataEItem.aspx?SafetyDataEPlanId={0}", Grid1.SelectedRowID), "修改明细", 1024, 560));
            }
            else
            {
                Alert.ShowInParent("当前资料项，项目尚未上传资料！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
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
            string menuId = BLL.Const.ServerSafetyDataEPlanMenuId;
            //if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            //{
            //    menuId = BLL.Const.ProjectSetMenuId;
            //}
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnGVDel.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify) || buttonList.Contains(BLL.Const.BtnSave) || buttonList.Contains(BLL.Const.BtnAuditing))
                {                    
                    this.btnGVModify.Hidden = false;
                    this.btnAudit.Hidden = false;
                }
            }
        }
        #endregion
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("安全考核资料计划表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 10000;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion        
    }
}