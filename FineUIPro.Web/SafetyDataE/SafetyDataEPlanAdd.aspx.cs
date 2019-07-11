using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataEPlanAdd : PageBase
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
            string strSql = @"SELECT Plans.SafetyDataEPlanId,SafetyDataE.SafetyDataEId,SafetyDataE.Code,SafetyDataE.Title,SafetyDataE.Score,SafetyDataE.Score AS ShouldScore"
                       + @", Plans.CheckDate ,Plans.ReminderDate,Plans.ProjectId "
                       + @" FROM SafetyDataE_SafetyDataEPlan AS Plans  "
                       + @" LEFT JOIN SafetyDataE_SafetyDataE AS SafetyDataE ON SafetyDataE.SafetyDataEId =Plans.SafetyDataEId "
                       + @" WHERE Plans.ProjectId='" + this.ProjectId + "' AND  Plans.States='" + BLL.Const.State_1 + "'";        
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
            int rowCount = 0;
            string info = string.Empty;
            JArray mergedData = Grid1.GetMergedData();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(i))
                {
                    JObject values = mergedData[i].Value<JObject>("values");
                    var getSafetyDataE = BLL.SafetyDataEPlanService.GetSafetyDataEPlanBySafetyDataEPlanId(Grid1.DataKeys[i][0].ToString());
                    if (getSafetyDataE != null)
                    {
                        getSafetyDataE.CheckDate = Funs.GetNewDateTime(values.Value<string>("CheckDate"));
                        getSafetyDataE.ShouldScore = Funs.GetNewDecimal(values.Value<string>("ShouldScore"));
                        getSafetyDataE.States = BLL.Const.State_2;
                        if (getSafetyDataE.CheckDate.HasValue)
                        {
                            getSafetyDataE.ReminderDate = getSafetyDataE.CheckDate.Value.AddDays(-7);
                        }

                        if (getSafetyDataE.CheckDate.HasValue && getSafetyDataE.ShouldScore.HasValue)
                        {
                            BLL.SafetyDataEPlanService.UpdateSafetyDataEPlan(getSafetyDataE);
                        }
                        else
                        {
                            info += "第" + (i + 1).ToString() + "行请填写考核时间、应得分；";
                        }

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
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }

                this.BindGrid();
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
        #endregion

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
                    BLL.SafetyDataEPlanService.DeleteSafetyDataEPlanByID(Grid1.DataKeys[rowIndex][0].ToString());
                }

                BindGrid();
            }
            Alert.ShowInTop("删除完成！", MessageBoxIcon.Success);
        }
    }
}