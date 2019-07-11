using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class ProjectSafetyDataEPlanAdd : PageBase
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
                    // 绑定表格
                    this.BindGrid();
                    // 绑定表格
                    this.BindGrid2();
                }
            }
        }
        #endregion

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            List<SqlParameter> listStr = new List<SqlParameter>();
            string strSql = @"SELECT SafetyDataE.SafetyDataEId,SafetyDataE.Code,SafetyDataE.Title,SafetyDataE.Score,SafetyDataE.Score AS ShouldScore"                      
                       + @" FROM SafetyDataE_SafetyDataE AS SafetyDataE "                       
                       + @" WHERE SafetyDataE.IsEndLever =1";           
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                var safetyDataEPlan = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEIdProjectId(Grid1.Rows[i].Values[7].ToString(), this.ProjectId);
                if (safetyDataEPlan != null)
                {
                    CheckBoxField ckbIsSelected = Grid1.FindColumn("ckbIsSelected") as CheckBoxField;
                    this.Grid1.Rows[i].CellCssClasses[ckbIsSelected.ColumnIndex] = "hidethis";
                    Grid1.Rows[i].RowCssClass = "Silver";
                    this.Grid1.Rows[i].CellCssClasses[4] = "f-grid-cell-uneditable";
                    this.Grid1.Rows[i].CellCssClasses[5] = "f-grid-cell-uneditable";
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid2()
        {
            List<SqlParameter> listStr = new List<SqlParameter>();
            string strSql = @"SELECT SafetyDataE.SafetyDataEId,SafetyDataE.Code,SafetyDataE.Title,SafetyDataE.Score,SafetyDataE.Score AS ShouldScore"
                       + @", Plans.CheckDate ,Plans.ReminderDate,Plans.ProjectId,Plans.SafetyDataEPlanId "
                       + @" FROM SafetyDataE_SafetyDataEPlan AS Plans "
                       + @" LEFT JOIN SafetyDataE_SafetyDataE AS SafetyDataE ON SafetyDataE.SafetyDataEId =Plans.SafetyDataEId "
                       + @" WHERE SafetyDataE.IsEndLever =1 AND Plans.ProjectId='" + this.ProjectId + "' AND Plans.States='" + BLL.Const.State_1 + "'";
            if (!string.IsNullOrEmpty(this.txtTitle.Text.Trim()))
            {
                strSql += " AND Title LIKE @Title";
                listStr.Add(new SqlParameter("@Title", "%" + this.txtTitle.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid2.RecordCount = tb.Rows.Count;
            //tb = GetFilteredTable(Grid2.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid2();
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
            string info = string.Empty;
            JArray mergedData = Grid1.GetMergedData();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(i))
                {
                    JObject values = mergedData[i].Value<JObject>("values");
                    var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(Grid1.DataKeys[i][0].ToString());
                    if (SafetyDataE != null)
                    {
                        Model.SafetyDataE_SafetyDataEPlan newSafetyDataEPlan = new Model.SafetyDataE_SafetyDataEPlan
                        {
                            SafetyDataEPlanId = SQLHelper.GetNewID(typeof(Model.SafetyDataE_SafetyDataEPlan)),
                            ProjectId = this.ProjectId,
                            SafetyDataEId = SafetyDataE.SafetyDataEId,
                            CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate")),
                            Score = SafetyDataE.Score,
                            Remark = SafetyDataE.Remark,

                            ShouldScore = Funs.GetNewDecimal(values.Value<string>("ShouldScore")),
                            RealScore = 0,
                            States = BLL.Const.State_1,
                        };

                        if (newSafetyDataEPlan.CheckDate.HasValue)
                        {
                            newSafetyDataEPlan.ReminderDate = newSafetyDataEPlan.CheckDate.Value.AddDays(-7);
                            BLL.SafetyDataEPlanService.AddSafetyDataEPlan(newSafetyDataEPlan);
                        }
                        else
                        {
                            info += "第" + (i + 1).ToString() + "行["+ SafetyDataE .Code+ "]请填写考核时间；";
                        }
                       
                        ////根据项目和安全资料项生成企业安全管理资料计划总表
                        //  BLL.SafetyDataEPlanService.GetSafetyDataEPlanByProjectInfo(newSafetyDataEPlan.ProjectId, SafetyDataE.SafetyDataEId, null, null);

                        rowCount++;
                    }
                }
            }

            if (rowCount > 0)
            {
                if (!string.IsNullOrEmpty(info))
                {
                    ShowNotify(info, MessageBoxIcon.Warning);                    
                }
                else
                {
                    ShowNotify("保存成功！", MessageBoxIcon.Success);
                }

                this.BindGrid();
                this.BindGrid2();
            }
            else
            {
                Alert.ShowInTop("请勾选增加项！", MessageBoxIcon.Warning);
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid2();
        }
        #endregion

        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid2.SelectedRowIndexArray)
                {
                    BLL.SafetyDataEPlanService.DeleteSafetyDataEPlanByID(Grid2.DataKeys[rowIndex][0].ToString());
                }

                BindGrid();
                BindGrid2();
            }
            Alert.ShowInTop("删除完成！", MessageBoxIcon.Success);
        }
    }
}