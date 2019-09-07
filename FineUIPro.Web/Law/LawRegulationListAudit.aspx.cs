namespace FineUIPro.Web.Law
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BLL;
    using System.IO;
    using System.Data;
    using System.Data.SqlClient;

    public partial class LawRegulationListAudit : PageBase
    {
        #region 加载页面
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
            string strSql = "select * from View_Law_LawRegulationList where IsPass is null";
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
            if (column == "LawRegulationName")
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

        #region 右键事件
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
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpLawRegulation(string LawRegulationId, string unitId)
        {  /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_LawRegulationListTableCompleted += new EventHandler<HSSEService.DataInsertLaw_LawRegulationListTableCompletedEventArgs>(poxy_DataInsertLaw_LawRegulationListTableCompleted);
            var LawRegulationList = from x in Funs.DB.View_Law_LawRegulationList
                                    join y in Funs.DB.AttachFile on x.LawRegulationId equals y.ToKeyId
                                    where x.LawRegulationId == LawRegulationId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == null || x.IsBuild == false)
                                    select new BLL.HSSEService.Law_LawRegulationList
                                    {
                                        LawRegulationId = x.LawRegulationId,
                                        ApprovalDate = x.ApprovalDate,
                                        CompileDate = x.CompileDate,
                                        CompileMan = x.CompileMan,
                                        Description = x.Description,
                                        EffectiveDate = x.EffectiveDate,
                                        LawRegulationCode = x.LawRegulationCode,
                                        LawRegulationName = x.LawRegulationName,
                                        UnitId = unitId,
                                        IsPass = null,
                                        AttachFileId = y.AttachFileId,
                                        ToKeyId = y.ToKeyId,
                                        AttachSource = y.AttachSource,
                                        AttachUrl = y.AttachUrl,
                                        ////附件转为字节传送
                                        FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                        LawsRegulationsTypeId = x.LawsRegulationsTypeId,
                                        LawsRegulationsTypeCode = x.LawsRegulationsTypeCode,
                                        LawsRegulationsTypeName = x.LawsRegulationsTypeName,
                                    };
            poxy.DataInsertLaw_LawRegulationListTableAsync(LawRegulationList.ToList());
        }

        /// <summary>
        /// 法律法规从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_LawRegulationListTableCompleted(object sender, HSSEService.DataInsertLaw_LawRegulationListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var law = BLL.LawRegulationListService.GetLawRegulationListById(item);
                    if (law != null)
                    {
                        law.UpState = BLL.Const.UpState_3;
                        BLL.LawRegulationListService.UpdateLawRegulationList(law);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【法律法规】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【法律法规】上传到服务器失败；", null, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnUploadResources);
            }
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
                    var law = BLL.LawRegulationListService.GetLawRegulationListById(rowID);
                    if (law != null)
                    {
                        law.AuditDate = System.DateTime.Now;
                        law.AuditMan = this.CurrUser.UserId;
                        law.IsPass = isPass;
                        law.UpState = upSate;
                        BLL.LawRegulationListService.UpdateLawRegulationListIsPass(law);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpLawRegulation(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, null, null, BLL.Const.LawRegulationListMenuId, BLL.Const.BtnAuditing);
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
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string lawRegulationId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("LawRegulationListEdit.aspx?LawRegulationId={0}", lawRegulationId, "编辑 - ")));
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "Attach")
            {
                var lawRegulationList = BLL.LawRegulationListService.GetLawRegulationListById(rowID);
                if (lawRegulationList != null)
                {
                    PageBase.ShowFileEvent(lawRegulationList.AttachUrl);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.LawRegulationListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                    this.btnUpPass.Hidden = false;
                    this.btnNoPass.Hidden = false;
                }
            }
        }
        #endregion
    }
}