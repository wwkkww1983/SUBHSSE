using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.Law
{
    public partial class ManageRuleAudit : PageBase
    {
        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetButtonPower();
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
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
            string strSql = "select * from View_Law_ManageRule where IsPass is null";
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

        /// <summary>
        /// 根据表头信息过滤列表数据
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <param name="fillteredOperator"></param>
        /// <param name="fillteredObj"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, object fillteredObj, string column)
        {
            bool valid = false;
            if (column == "ManageRuleName")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.ToString();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }
            return valid;
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

        #region 右键采用、不采用
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
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(rowID);
                    if (manageRule != null)
                    {
                        manageRule.AuditDate = System.DateTime.Now;
                        manageRule.AuditMan = this.CurrUser.UserId;
                        manageRule.IsPass = isPass;
                        manageRule.UpState = upSate;
                        BLL.ManageRuleService.UpdateManageRuleIsPass(manageRule);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpManageRule(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, null, null, BLL.Const.ManageRuleMenuId, BLL.Const.BtnAuditing);
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
        #endregion

        #region 管理规定上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <param name="unitId"></param>
        public void UpManageRule(string manageRuleId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_ManageRuleTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs>(poxy_DataInsertLaw_ManageRuleTableCompleted);
            var manageRule = from x in Funs.DB.View_Law_ManageRule
                             join y in Funs.DB.AttachFile on x.ManageRuleId equals y.ToKeyId
                             where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                             select new BLL.HSSEService.Law_ManageRule
                             {
                                 ManageRuleId = x.ManageRuleId,
                                 ManageRuleCode = x.ManageRuleCode,
                                 ManageRuleName = x.ManageRuleName,
                                 ManageRuleTypeId = x.ManageRuleTypeId,
                                 VersionNo = x.VersionNo,
                                 CompileMan = x.CompileMan,
                                 CompileDate = x.CompileDate,
                                 Remark = x.Remark,
                                 IsPass = null,
                                 UnitId = unitId,
                                 AttachFileId = y.AttachFileId,
                                 ToKeyId = y.ToKeyId,
                                 AttachSource = y.AttachSource,
                                 AttachUrl = y.AttachUrl,
                                 ////附件转为字节传送
                                 FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                             };
            poxy.DataInsertLaw_ManageRuleTableAsync(manageRule.ToList());
        }

        /// <summary>
        /// 管理规定上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_ManageRuleTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(item);
                    if (manageRule != null)
                    {
                        manageRule.UpState = BLL.Const.UpState_3;
                        BLL.ManageRuleService.UpdateManageRule(manageRule);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全管理规定】上报到集团公司" + idList.Count.ToString() + "条数据；", null, BLL.Const.ManageRuleMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【安全管理规定】上报到集团公司失败；", null, BLL.Const.ManageRuleMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion

        #region Grid 行事件
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Attach")
            {
                var manageRule = BLL.ManageRuleService.GetManageRuleById(rowID);
                if (manageRule != null)
                {
                    PageBase.ShowFileEvent(manageRule.AttachUrl);
                }
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
            string manageRuleId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ManageRuleEdit.aspx?ManageRuleId={0}", manageRuleId, "编辑 - ")));
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ManageRuleMenuId);
            if (buttonList.Count()>0)
            {
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                    this.btnNoPass.Hidden = false;
                    this.btnUpPass.Hidden = false;
                }
            }
        }
        #endregion
    }
}