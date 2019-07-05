using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class HazardListAudit : PageBase
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
                ////权限按钮方法
                this.GetButtonPower();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Technique_HazardList where IsPass is null";
            SqlParameter[] parameter = null;
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 根据表头信息过滤列表数据
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 分页排序
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
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
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 采用/不采用
        /// <summary>
        /// 右键采用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(true, BLL.Const.UpState_1);
        }

        /// <summary>
        /// 右键采用并上报事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(true, BLL.Const.UpState_2);
        }

        /// <summary>
        /// 右键不采用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(false, null);
        }

        /// <summary>
        /// 是否采用方法更新
        /// </summary>
        /// <param name="isPass"></param>
        private void SetIsPass(bool isPass, string upSate)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var unit = BLL.CommonService.GetIsThisUnit();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var hazardList = BLL.HazardListService.GetHazardListById(rowID);
                    if (hazardList != null)
                    {
                        hazardList.AuditDate = System.DateTime.Now;
                        hazardList.AuditMan = this.CurrUser.UserId;
                        hazardList.IsPass = isPass;
                        hazardList.UpState = upSate;
                        BLL.HazardListService.UpdateHazardListIsPass(hazardList);
                    }
                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpHazardList(rowID, unit.UnitId);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "审核危险源清单");
                BindGrid();
                ShowNotify("操作成功!");
                if (isPass)
                {
                    // 1. 这里放置保存窗体中数据的逻辑
                    // 2. 不关闭窗体，直接回发父窗体
                    PageContext.RegisterStartupScript("F.getActiveWindow().window.reloadGrid();");

                }
            }
        }

        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpHazardList(string hazardId, string unitId)
        {  
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_HazardListTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs>(poxy_DataInsertTechnique_HazardListTableCompleted);
            var hazardListList = from x in Funs.DB.Technique_HazardList
                                       where x.HazardId == hazardId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                       select new HSSEService.Technique_HazardList
                                       {
                                           HazardId = x.HazardId,
                                           HazardListTypeId = x.HazardListTypeId,
                                           HazardCode = x.HazardCode,
                                           HazardItems = x.HazardItems,
                                           DefectsType = x.DefectsType,
                                           MayLeadAccidents = x.MayLeadAccidents,
                                           HelperMethod = x.HelperMethod,
                                           HazardJudge_L = x.HazardJudge_L,
                                           HazardJudge_E = x.HazardJudge_E,
                                           HazardJudge_C = x.HazardJudge_C,
                                           HazardJudge_D = x.HazardJudge_D,
                                           HazardLevel = x.HazardLevel,
                                           ControlMeasures = x.ControlMeasures,
                                           CompileMan = x.CompileMan,
                                           CompileDate = x.CompileDate,
                                           UnitId = unitId,
                                           IsPass = null,
                                       };
            poxy.DataInsertTechnique_HazardListTableAsync(hazardListList.ToList());
        }

        /// <summary>
        /// 危险源清单明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_HazardListTableCompleted(object sender, HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var hazardList = BLL.HazardListService.GetHazardListById(item);
                    if (hazardList != null)
                    {
                        hazardList.UpState = BLL.Const.UpState_3;
                        BLL.HazardListService.UpdateHazardListIsPass(hazardList);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【危险源清单明细】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【危险源清单明细】上报到集团公司失败；");
            }
        }

        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseItemSave.aspx?AccidentCaseItemId={0}", id, "编辑 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HazardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnUpPass.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnNoPass.Hidden = false;
                }
            }
        }
        #endregion
    }
}