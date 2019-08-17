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
    public partial class HSSEStandardListAudit : PageBase
    {
        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {           
            // 表头过滤
            FilterDataRowItem = FilterDataRowItemImplement;
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
            string strSql = "select * from View_HSSEStandardsList where IsPass is null";
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
            if (column == "StandardName")
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
        /// 右键不采用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNoPass_Click(object sender, EventArgs e)
        {
            this.SetIsPass(false, null);
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
        /// 上报方法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void UpHSSEStandardsList(string standardId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_HSSEStandardsListTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs>(poxy_DataInsertLaw_HSSEStandardsListTableCompleted);
            var HSSEStandardsList = from x in Funs.DB.View_HSSEStandardsList
                                    join y in Funs.DB.AttachFile on x.StandardId equals y.ToKeyId
                                    where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                    select new BLL.HSSEService.Law_HSSEStandardsList
                                    {
                                        StandardId = x.StandardId,
                                        StandardGrade = x.StandardGrade,
                                        StandardNo = x.StandardNo,
                                        StandardName = x.StandardName,
                                        TypeId = x.TypeId,
                                        TypeCode = x.TypeCode,
                                        TypeName = x.TypeName,
                                        IsSelected1 = x.IsSelected1,
                                        IsSelected2 = x.IsSelected2,
                                        IsSelected3 = x.IsSelected3,
                                        IsSelected4 = x.IsSelected4,
                                        IsSelected5 = x.IsSelected5,
                                        IsSelected6 = x.IsSelected6,
                                        IsSelected7 = x.IsSelected7,
                                        IsSelected8 = x.IsSelected8,
                                        IsSelected9 = x.IsSelected9,
                                        IsSelected10 = x.IsSelected10,
                                        IsSelected11 = x.IsSelected11,
                                        IsSelected12 = x.IsSelected12,
                                        IsSelected13 = x.IsSelected13,
                                        IsSelected14 = x.IsSelected14,
                                        IsSelected15 = x.IsSelected15,
                                        IsSelected16 = x.IsSelected16,
                                        IsSelected17 = x.IsSelected17,
                                        IsSelected18 = x.IsSelected18,
                                        IsSelected19 = x.IsSelected19,
                                        IsSelected20 = x.IsSelected20,
                                        IsSelected21 = x.IsSelected21,
                                        IsSelected22 = x.IsSelected22,
                                        IsSelected23 = x.IsSelected23,
                                        IsSelected90 = x.IsSelected90,
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
            poxy.DataInsertLaw_HSSEStandardsListTableAsync(HSSEStandardsList.ToList());
        }

        /// <summary>
        /// 标准规范上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_HSSEStandardsListTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var standardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(item);
                    if (standardsList != null)
                    {
                        standardsList.UpState = BLL.Const.UpState_3;
                        BLL.HSSEStandardsListService.UpdateHSSEStandardsList(standardsList);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【标准规范】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【标准规范】上传到服务器失败；", null, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnUploadResources);
            }
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
                    var law = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(rowID);
                    if (law != null)
                    {
                        law.AuditDate = System.DateTime.Now;
                        law.AuditMan = this.CurrUser.UserId;
                        law.IsPass = isPass;
                        law.UpState = upSate;
                        BLL.HSSEStandardsListService.UpdateHSSEStandardsListIsPass(law);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpHSSEStandardsList(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, null, null, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnAuditing);
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
            string standardId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSEStandardListSave.aspx?StandardId={0}", standardId, "编辑 - ")));
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
                var hSSEStandardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(rowID);
                if (hSSEStandardsList != null)
                {
                    PageBase.ShowFileEvent(hSSEStandardsList.AttachUrl);
                }
            }
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
            if (buttonList.Count() > 0)
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