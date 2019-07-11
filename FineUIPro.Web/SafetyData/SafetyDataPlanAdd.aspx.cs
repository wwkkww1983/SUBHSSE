using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataPlanAdd : PageBase
    {
        /// <summary>
        /// 定义项
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
                this.ProjectId = Request.Params["ProjectId"];
                if (!string.IsNullOrEmpty(this.ProjectId))
                {
                    this.drpProject.Value = this.ProjectId;
                }
                // 绑定表格
                this.BindGrid();
                this.BindGrid2();
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            DateTime startDate = System.DateTime.Now;
            DateTime endDate = System.DateTime.Now.AddMonths(1);
            var project = BLL.ProjectService.GetProjectByProjectId(this.drpProject.Value);
            if (project != null && project.EndDate.HasValue)
            {
                endDate = project.EndDate.Value;
            }
            List<SqlParameter> listStr = new List<SqlParameter>();
            string strSql = @"SELECT SafetyData.SafetyDataId,SafetyData.Code,SafetyData.Title,SafetyData.Score,SafetyData.Score AS ShouldScore"
                        + @",'" + startDate + "' AS RealStartDate,'" + endDate + "' AS RealEndDate ,'" + endDate + "' AS CheckDate ,'" + endDate.AddDays(-7) + "' AS ReminderDate "
                        + @" FROM SafetyData_SafetyData AS SafetyData "                       
                        + @" WHERE SafetyData.IsEndLever =1 ";                             
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 排序分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
        
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSure_Click(object sender, EventArgs e)
        {
            this.GetProjectIds();
        }

        /// <summary>
        /// 得到项目id 并保存
        /// </summary>
        private void GetProjectIds()
        {
            int rowCount = 0;
            if (this.drpProject.Values != null)
            {
                for (int j = 0;  j < this.drpProject.Values.Length; j++)
                {                    
                    JArray mergedData = Grid1.GetMergedData();
                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                        if (checkField.GetCheckedState(i))
                        {
                            JObject values = mergedData[i].Value<JObject>("values");
                            var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(Grid1.DataKeys[i][0].ToString());
                            if (safetyData != null)
                            {
                                Model.SafetyData_SafetyDataPlan newSafetyDataPlan = new Model.SafetyData_SafetyDataPlan
                                {
                                    SafetyDataPlanId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyDataPlan)),
                                    ProjectId = this.drpProject.Values[j].ToString(),
                                    SafetyDataId = safetyData.SafetyDataId,
                                    RealStartDate = Funs.GetNewDateTime(values.Value<string>("RealStartDate")),
                                    RealEndDate = Funs.GetNewDateTime(values.Value<string>("RealEndDate")),
                                    CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate")),
                                    Score = safetyData.Score,
                                    Remark = safetyData.Remark,
                                    ReminderDate = Funs.GetNewDateTime(values.Value<string>("ReminderDate")),
                                    ShouldScore = Funs.GetNewDecimal(values.Value<string>("ShouldScore")),
                                    RealScore = 0,
                                    IsManual = true,
                                };

                                //BLL.SafetyDataPlanService.AddSafetyDataPlan(newSafetyDataPlan);
                                ////根据项目信息生成企业安全管理资料计划总表
                                BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(newSafetyDataPlan.ProjectId, string.Empty, newSafetyDataPlan.RealStartDate, newSafetyDataPlan.RealEndDate);
                                rowCount++;
                            }
                        }
                    }
                }                
            }
            if (rowCount > 0)
            {
                ShowNotify("保存成功！", MessageBoxIcon.Success);
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("请选择项目且勾选增加项！", MessageBoxIcon.Warning);
            }
        }

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

        #region 项目下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT * FROM Base_Project WHERE ProjectType != '5' AND (ProjectState IS NULL OR ProjectState = '" + BLL.Const.ProjectState_1 + "')";

            if (!string.IsNullOrEmpty(strSql))
            {
                List<SqlParameter> listStr = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
                {
                    strSql += " AND ProjectName LIKE @ProjectName";
                    listStr.Add(new SqlParameter("@ProjectName", "%" + this.txtProjectName.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtProjectCode.Text.Trim()))
                {
                    strSql += " AND ProjectCode LIKE @ProjectCode";
                    listStr.Add(new SqlParameter("@ProjectCode", "%" + this.txtProjectCode.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid2.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid2.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid2, tb);
                Grid2.DataSource = table;
                Grid2.DataBind();
            }
            else
            {
                Grid2.DataSource = null;
                Grid2.DataBind();
            }
        }
        #endregion

        #region 项目查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        protected void drpProject_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
    }
}