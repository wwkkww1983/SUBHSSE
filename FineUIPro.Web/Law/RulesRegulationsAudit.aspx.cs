using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using System.Data;

namespace FineUIPro.Web.Law
{
    public partial class RulesRegulationsAudit : PageBase
    {
        #region 设置权限
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {           
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.GetButtonPower();//设置权限
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
            string strSql = "select * from View_Law_RulesRegulations where IsPass is null";
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
            if (column == "RulesRegulationsName")
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
            this.SetIsPass(false,null);
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
                    var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(rowID);
                    if (rulesRegulations != null)
                    {
                        rulesRegulations.AuditDate = System.DateTime.Now;
                        rulesRegulations.AuditMan = this.CurrUser.UserId;
                        rulesRegulations.IsPass = isPass;
                        rulesRegulations.UpState = upSate;
                        BLL.RulesRegulationsService.UpdateRulesRegulationsIsPass(rulesRegulations);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpRulesRegulations(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, null, null, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnAuditing);
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

        #region 生产制度上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <param name="unitId"></param>
        public void UpRulesRegulations(string rulesRegulationsId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_RulesRegulationsTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs>(poxy_DataInsertLaw_RulesRegulationsTableCompleted);
            var RulesRegulations = from x in Funs.DB.View_Law_RulesRegulations
                                   join y in Funs.DB.AttachFile on x.RulesRegulationsId equals y.ToKeyId
                                   where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                   select new BLL.HSSEService.Law_RulesRegulations
                                   {
                                       RulesRegulationsId = x.RulesRegulationsId,
                                       RulesRegulationsCode = x.RulesRegulationsCode,
                                       RulesRegulationsName = x.RulesRegulationsName,
                                       RulesRegulationsTypeId = x.RulesRegulationsTypeId,                                      
                                       RulesRegulationsTypeCode = x.RulesRegulationsTypeCode,
                                       RulesRegulationsTypeName = x.RulesRegulationsTypeName,
                                       CustomDate = x.CustomDate,
                                       ApplicableScope = x.ApplicableScope,
                                       Remark = x.Remark,
                                       CompileMan = x.CompileMan,
                                       CompileDate = x.CompileDate,
                                       IsPass = null,
                                       UnitId = unitId,
                                       AttachFileId = y.AttachFileId,
                                       ToKeyId = y.ToKeyId,
                                       AttachSource = y.AttachSource,
                                       AttachUrl = y.AttachUrl,
                                       ////附件转为字节传送
                                       FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                   };
            poxy.DataInsertLaw_RulesRegulationsTableAsync(RulesRegulations.ToList());
        }

        /// <summary>
        /// 生产制度上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_RulesRegulationsTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(item);
                    if (rulesRegulations != null)
                    {
                        rulesRegulations.UpState = BLL.Const.UpState_3;
                        BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【政府部门安全规章】上报到集团公司" + idList.Count.ToString() + "条数据；", null, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【政府部门安全规章】上报到集团公司失败；", null, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion

        #region Grid 行事件
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
            string rulesRegulationsId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RulesRegulationsEdit.aspx?RulesRegulationsId={0}", rulesRegulationsId, "编辑 - ")));
        }

        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Attach")
            {
                var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(rowID);
                if (rulesRegulations != null)
                {
                    PageBase.ShowFileEvent(rulesRegulations.AttachUrl);
                }
            }
        }
        #endregion

        #region 设置权限
        /// <summary>
        /// 设置权限
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RulesRegulationsMenuId);
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