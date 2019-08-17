using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.EduTrain
{
    public partial class AccidentCaseItemAudit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
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
            string strSql = "select * from View_EduTrain_AccidentCaseItem where IsPass is null";
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

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }


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
                    var item = BLL.AccidentCaseItemService.GetAccidentCaseItemById(rowID);
                    if (item != null)
                    {
                        item.AuditDate = System.DateTime.Now;
                        item.AuditMan = this.CurrUser.UserId;
                        item.IsPass = isPass;
                        item.UpState = upSate;
                        BLL.AccidentCaseItemService.UpdateAccidentCaseItemIsPass(item);
                    }
                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpAccidentCaseItem(rowID, unit.UnitId);
                    }
                }
                
                BLL.LogService.AddSys_Log(this.CurrUser, "审核事故案例库", null, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnAuditing);
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
        public void UpAccidentCaseItem(string accidentCaseItemId, string unitId)
        {  /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertEduTrain_AccidentCaseItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertEduTrain_AccidentCaseItemTableCompletedEventArgs>(poxy_DataInsertEduTrain_AccidentCaseItemTableCompleted);
            var AccidentCaseItemList = from x in Funs.DB.EduTrain_AccidentCaseItem
                                       where x.AccidentCaseItemId == accidentCaseItemId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                       select new BLL.HSSEService.EduTrain_AccidentCaseItem
                                       {
                                           AccidentCaseItemId = x.AccidentCaseItemId,
                                           AccidentCaseId = x.AccidentCaseId,
                                           Activities = x.Activities,
                                           AccidentName = x.AccidentName,
                                           AccidentProfiles = x.AccidentProfiles,
                                           AccidentReview = x.AccidentReview,
                                           CompileMan = x.CompileMan,
                                           CompileDate = x.CompileDate,
                                           UnitId = unitId,
                                           IsPass = null,
                                       };
            poxy.DataInsertEduTrain_AccidentCaseItemTableAsync(AccidentCaseItemList.ToList());
        }

        /// <summary>
        /// 事故案例明细从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertEduTrain_AccidentCaseItemTableCompleted(object sender, BLL.HSSEService.DataInsertEduTrain_AccidentCaseItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var accidentCaseItem = BLL.AccidentCaseItemService.GetAccidentCaseItemById(item);
                    if (accidentCaseItem != null)
                    {
                        accidentCaseItem.UpState = BLL.Const.UpState_3;
                        BLL.AccidentCaseItemService.UpdateAccidentCaseItemIsPass(accidentCaseItem);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【事故案例明细】上报到集团公司" + idList.Count.ToString() + "条数据；", null, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【事故案例明细】上报到集团公司失败；", null, BLL.Const.AccidentCaseMenuId, BLL.Const.BtnUploadResources);
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
                Alert.ShowInTop("请至少选择一条记录！",MessageBoxIcon.Warning);
                return;
            }
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("AccidentCaseItemSave.aspx?AccidentCaseItemId={0}", id, "编辑 - ")));
        }

        #region 按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.AccidentCaseMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnNoPass.Hidden = false;
                    this.btnPass.Hidden = false;
                    this.btnUpPass.Hidden = false;
                }
            }
        }
        #endregion
    }
}