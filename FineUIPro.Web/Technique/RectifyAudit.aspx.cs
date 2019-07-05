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
    public partial class RectifyAudit : PageBase
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
                ////权限按钮方法
                this.GetButtonPower();

            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Technique_RectifyItem where IsPass is null";
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
                    var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(rowID);
                    if (rectifyItem != null)
                    {
                        rectifyItem.AuditDate = System.DateTime.Now;
                        rectifyItem.AuditMan = this.CurrUser.UserId;
                        rectifyItem.IsPass = isPass;
                        rectifyItem.UpState = upSate;
                        BLL.RectifyItemService.UpdateRectifyItemIsPass(rectifyItem);
                    }
                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpRectifyItem(rowID, unit.UnitId);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "审核安全隐患");
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
        public void UpRectifyItem(string rectifyItemId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_RectifyItemTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_RectifyItemTableCompletedEventArgs>(poxy_DataInsertTechnique_RectifyItemTableCompleted);
            var rectifyItemList = from x in Funs.DB.Technique_RectifyItem
                                 where x.RectifyItemId == rectifyItemId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                 select new HSSEService.Technique_RectifyItem
                                 {
                                     RectifyItemId = x.RectifyItemId,
                                     RectifyId = x.RectifyId,
                                     HazardSourcePoint = x.HazardSourcePoint,
                                     RiskAnalysis = x.RiskAnalysis,
                                     RiskPrevention = x.RiskPrevention,
                                     SimilarRisk = x.SimilarRisk,
                                     CompileMan = x.CompileMan,
                                     CompileDate = x.CompileDate,
                                     UnitId = unitId,
                                     IsPass = null,
                                 };
            poxy.DataInsertTechnique_RectifyItemTableAsync(rectifyItemList.ToList());
        }

        /// <summary>
        /// 安全隐患明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_RectifyItemTableCompleted(object sender, HSSEService.DataInsertTechnique_RectifyItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(item);
                    if (rectifyItem != null)
                    {
                        rectifyItem.UpState = BLL.Const.UpState_3;
                        BLL.RectifyItemService.UpdateRectifyItemIsPass(rectifyItem);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全隐患明细】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全隐患明细】上报到集团公司失败；");
            }
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RectifyMenuId);
            if (buttonList.Count() > 0)
            {
               
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                 
                    this.btnUpPass.Hidden = false;
                }
               
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                    this.btnNoPass.Hidden = false;

                }
              
            }
        }
        #endregion
    }
}