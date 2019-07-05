using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.SafetyData
{
    public partial class ProjectSafetyDataCheckEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string SafetyDataCheckId
        {
            get
            {
                return (string)ViewState["SafetyDataCheckId"];
            }
            set
            {
                ViewState["SafetyDataCheckId"] = value;
            }
        }

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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                
                this.SafetyDataCheckId = Request.Params["SafetyDataCheckId"];
                this.ProjectId = Request.Params["ProjectId"];
                if (this.ProjectId != this.CurrUser.LoginProjectId)
                {
                    this.btnMenuNew.Hidden = true;
                }
                if (!string.IsNullOrEmpty(this.SafetyDataCheckId))
                {
                    Model.SafetyData_SafetyDataCheck SafetyDataCheck = BLL.SafetyDataCheckService.GetSafetyDataCheckById(this.SafetyDataCheckId);
                    if (SafetyDataCheck != null)
                    {
                        this.txtCode.Text = SafetyDataCheck.Code;
                        this.txtTitle.Text = SafetyDataCheck.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.CompileDate);
                        this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.StartDate);
                        this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", SafetyDataCheck.EndDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(SafetyDataCheck.CompileMan);
                    }
                }

                this.BindGrid();
            }
        }
        #endregion

        #region 绑定明细列表数据
        /// <summary>
        /// 绑定明细列表数据
        /// </summary>
        private void BindGrid()
        {
            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            if (!string.IsNullOrEmpty(this.ProjectId))
            {
                string strSql = @"SELECT item.SafetyDataCheckItemId,item.SafetyDataCheckProjectId,item.SafetyDataId,item.StartDate,item.EndDate,item.ReminderDate,item.SubmitDate,item.ShouldScore,item.RealScore,SafetyData.Title AS SafetyDataTitle,SafetyData.Code AS SafetyDataCode,item.Remark"
                              + @" FROM dbo.SafetyData_SafetyDataCheckItem AS item"
                              + @" LEFT JOIN DBO.SafetyData_SafetyData AS SafetyData ON item.SafetyDataId =SafetyData.SafetyDataId"
                              + @" LEFT JOIN DBO.SafetyData_SafetyDataCheckProject AS checkProject ON item.SafetyDataCheckProjectId =checkProject.SafetyDataCheckProjectId"
                              + @" WHERE checkProject.ProjectId =@ProjectId AND checkProject.SafetyDataCheckId = @SafetyDataCheckId";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@ProjectId",this.ProjectId),        
                        new SqlParameter("@SafetyDataCheckId",this.SafetyDataCheckId),  
                    };

                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                this.Grid1.RecordCount = tb.Rows.Count;
                var table = this.GetPagedDataTable(this.Grid1, tb);
                this.OutputSummaryData(tb); ///取合计值
                this.Grid1.DataSource = table;
                this.Grid1.DataBind();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string safetyDataCheckItemId = Grid1.Rows[i].DataKeys[0].ToString();
                    var safetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(safetyDataCheckItemId); ///考核明细
                    if (safetyDataCheckItem != null)
                    {
                        if (safetyDataCheckItem.SubmitDate.HasValue) ////已提交
                        {
                            if (safetyDataCheckItem.EndDate < safetyDataCheckItem.SubmitDate)  ///过期提交
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
                            if (safetyDataCheckItem.EndDate >= System.DateTime.Now)  ///未到结束时间
                            {
                                if (safetyDataCheckItem.ReminderDate.HasValue && safetyDataCheckItem.ReminderDate.Value.AddDays(7) >= System.DateTime.Now)
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
            decimal ShouldScoreT = 0;//应得分
            decimal RealScore = 0;//实际得分          
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                ShouldScoreT += Funs.GetNewDecimalOrZero(tb.Rows[i]["ShouldScore"].ToString());
                RealScore += Funs.GetNewDecimalOrZero(tb.Rows[i]["RealScore"].ToString());
            }
            JObject summary = new JObject();
            summary.Add("SafetyDataTitle", "合计：");
            summary.Add("ShouldScore", ShouldScoreT);
            summary.Add("RealScore", RealScore);
            Grid1.SummaryData = summary;
        }
        #endregion

        #region gv排序翻页
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
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion
        #endregion

        #region 增加、修改、删除企业安全管理资料明细事件
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuNew_Click(object sender, EventArgs e)
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

            var safetyDataCheckItem = BLL.SafetyDataCheckItemService.GetSafetyDataCheckItemById(Grid1.SelectedRowID);
            if (safetyDataCheckItem != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ProjectSafetyDataItemEdit.aspx?SafetyDataId={0}&SafetyDataCheckItemId={1}", safetyDataCheckItem.SafetyDataId,safetyDataCheckItem.SafetyDataCheckItemId, "新增 - ")));
            }
        }   
        #endregion

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
    }
}