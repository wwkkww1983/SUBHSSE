using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class Unit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            { 
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("UnitEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();
            }
        }
        
             /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT UnitId,UnitCode,UnitName,ProjectRange,Corporate,Address,Telephone,Fax,EMail,UnitType.UnitTypeId,UnitType.UnitTypeCode,UnitType.UnitTypeName,Unit.IsThisUnit,Unit.IsHide"
                          +@" From dbo.Base_Unit AS Unit "
                          +@" LEFT JOIN Base_UnitType AS UnitType ON UnitType.UnitTypeId=Unit.UnitTypeId"
                          +@" WHERE (IsHide IS NULL OR IsHide = 0) AND 1= 1";
            List<SqlParameter> listStr = new List<SqlParameter>();           
            if (!string.IsNullOrEmpty(this.txtUnitName.Text.Trim()))
            {
                strSql += " AND UnitName LIKE @UnitName";
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

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.UnitMenuId);
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
               
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            string strShowNotify = string.Empty;
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                      var unit = BLL.UnitService.GetUnitByUnitId(rowID);
                      if (unit != null)
                      {
                          if (BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId) )
                          {
                              string cont = judgementDelete(rowID);
                              if (string.IsNullOrEmpty(cont))
                              {
                                  BLL.UnitService.DeleteUnitById(rowID);
                                  BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除单位设置");
                              }
                              else
                              {
                                  strShowNotify += "单位：" + unit.UnitName + cont;
                              }
                          }
                          else
                          {
                              strShowNotify += "单位：" + unit.UnitName + "非本单位人员不能删除单位信息";
                          }
                      }
                }

                BindGrid();
                if (!string.IsNullOrEmpty(strShowNotify))
                {
                    Alert.ShowInTop(strShowNotify, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowNotify("删除数据成功!", MessageBoxIcon.Success);
                }
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页显示条数下拉框
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
        
        /// <summary>
        /// Grid行双击事件
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

            string Id = Grid1.SelectedRowID;
            if (BLL.CommonService.IsMainUnitOrAdmin(this.CurrUser.UserId) || this.CurrUser.UnitId == Id)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("UnitEdit.aspx?UnitId={0}", Id, "编辑 - ")));
            }
            else
            {
                Alert.ShowInTop("非本单位人员不能修改别单位信息！", MessageBoxIcon.Warning);
                return;
            }
        }

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private string judgementDelete(string id)
        {
            string content = string.Empty;
            var unit =Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitId == id) ;
            if (unit != null && unit.IsThisUnit == true)
            {
                content += "【本单位】，不能删除！";
            }
            if (Funs.DB.Sys_User.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【用户信息】中使用，不能删除！";
            }
            if (Funs.DB.Supervise_SuperviseCheckReport.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【安全监督检查报告】中使用，不能删除！";
            }
            if (Funs.DB.Technique_SpecialScheme.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content = "该单位已在【专项方案】中使用，不能删除！";
            }
            if (Funs.DB.Information_AccidentCauseReport.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【职工伤亡事故原因分析】中使用，不能删除！";
            }
            if (Funs.DB.Information_SafetyQuarterlyReport.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【安全生产数据季报】中使用，不能删除！";
            }           
            if (Funs.DB.Information_DrillPlanHalfYearReport.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【应急演练工作计划半年报】中使用，不能删除！";
            }
            if (Funs.DB.Information_DrillConductedQuarterlyReport.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【应急演练开展情况季报】中使用，不能删除！";
            }
            if (Funs.DB.Technique_Expert.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【技术专家管理】中使用，不能删除！";
            }
            if (Funs.DB.SecuritySystem_SafetyOrganization.FirstOrDefault(x => x.UnitId == id) != null)
            {
                content += "该单位已在【项目组织机构】中使用，不能删除！";
            }

            return content;
        }
        #endregion       
    }
}