using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class SafeSupervision : PageBase
    {
        #region 定义项
        /// <summary>
        /// APP安全督查主键
        /// </summary>
        public string SafeSupervisionId
        {
            get
            {
                return (string)ViewState["SafeSupervisionId"];
            }
            set
            {
                ViewState["SafeSupervisionId"] = value;
            }
        }

        /// <summary>
        /// 新建APP安全督查主键
        /// </summary>
        public string NewSafeSupervisionId
        {
            get
            {
                return (string)ViewState["NewSafeSupervisionId"];
            }
            set
            {
                ViewState["NewSafeSupervisionId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // 表头过滤
            //FilterDataRowItem = FilterDataRowItemImplement;
            if (!IsPostBack)
            {
                this.btnMenuDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());

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
            string strSql = "select * from View_Hazard_SafeSupervision where ProjectId=@ProjectId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

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

        #region 表头过滤
        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            // 关闭窗体时未保存则删除已上传的附件
            //var q = BLL.HSSE_Solution_ConstructSolutionService.GetConstructSolutionBySafeSupervisionId(this.NewSafeSupervisionId);
            //if (q == null)
            //{
            //    BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, this.NewSafeSupervisionId, Const.HSSE_APPSafeSupervisionMenuId);
            //}
            BindGrid();
        }
        #endregion

        #region 删除
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
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPSafeSupervisionMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    bool isShow = false;
                    if (Grid1.SelectedRowIndexArray.Length == 1)
                    {
                        isShow = true;
                    }
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        if (this.judgementDelete(rowID, isShow))
                        {
                            var details = from x in Funs.DB.HSSE_Hazard_HazardRegister
                                          where x.SafeSupervisionId == rowID
                                          select x;
                            if (details.Count() > 0)
                            {
                                Funs.DB.HSSE_Hazard_HazardRegister.DeleteAllOnSubmit(details);
                            }
                            BLL.LogService.AddSys_Log(this.CurrUser, "删除APP领导督查", rowID, BLL.Const.HSSE_APPSafeSupervisionMenuId, BLL.Const.BtnDelete);
                            // 先删除附件，再删除数据信息
                            BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, rowID, Const.HSSE_APPSafeSupervisionMenuId);
                            BLL.HSSE_Hazard_SafeSupervisionService.DeleteSafeSupervision(rowID);
                           
                            BindGrid();
                            ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                        }
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
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
            this.SafeSupervisionId = Grid1.DataKeys[e.RowIndex][0].ToString();

            Model.HSSE_Hazard_SafeSupervision checkSpecial = BLL.HSSE_Hazard_SafeSupervisionService.GetSafeSupervisionBySafeSupervisionId(SafeSupervisionId);
            #region 查看详细信息
            if (e.CommandName == "particular")//查看详细信息
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafeSupervisionParticular.aspx?SafeSupervisionId={0}", this.SafeSupervisionId, "查看 - ")));
            }
            #endregion
            #region 打印
            if (e.CommandName == "print")//打印
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("SafeSupervisionPrint.aspx?SafeSupervisionId={0}", this.SafeSupervisionId, "打印 - ")));
                //string window = String.Format("CheckRemark.aspx", "编辑 - ");
                //PageContext.RegisterStartupScript(Window2.GetSaveStateReference(hdRemark.ClientID) + Window2.GetShowReference(window));
            }
            #endregion
            #region 删除
            if (e.CommandName == "del")//删除
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPSafeSupervisionMenuId, BLL.Const.BtnDelete))
                {
                    var details = from x in Funs.DB.HSSE_Hazard_HazardRegister
                                  where x.SafeSupervisionId == SafeSupervisionId
                                  select x;
                    if (details.Count() > 0)
                    {
                        Funs.DB.HSSE_Hazard_HazardRegister.DeleteAllOnSubmit(details);
                    }
                    BLL.LogService.AddSys_Log(this.CurrUser, null, SafeSupervisionId, BLL.Const.HSSE_APPSafeSupervisionMenuId, BLL.Const.BtnDelete);
                    // 先删除附件，再删除数据信息
                    BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, this.SafeSupervisionId, Const.HSSE_APPSafeSupervisionMenuId);
                    BLL.HSSE_Hazard_SafeSupervisionService.DeleteSafeSupervision(SafeSupervisionId);
                  
                    BindGrid();
                    ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                }
                else
                {
                    Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 获取检查项数
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertCheckNum(object SafeSupervisionId)
        {
            string checkNum = string.Empty;
            if (SafeSupervisionId != null)
            {
                var q = from x in Funs.DB.View_Hazard_HazardRegister
                        where x.SafeSupervisionId == SafeSupervisionId.ToString()
                        select x;
                checkNum = q.Count().ToString();
            }
            return checkNum;
        }

        /// <summary>
        /// 获取合格项数
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertOKNum(object SafeSupervisionId)
        {
            string okNum = string.Empty;
            if (SafeSupervisionId != null)
            {
                var q = from x in Funs.DB.View_Hazard_HazardRegister
                        where x.SafeSupervisionId == SafeSupervisionId.ToString() && x.SafeSupervisionIsOK == true
                        select x;
                okNum = q.Count().ToString();
            }
            return okNum;
        }

        /// <summary>
        /// 获取不合格项数
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertNotOKNum(object SafeSupervisionId)
        {
            string notOKNum = string.Empty;
            if (SafeSupervisionId != null)
            {
                var q = from x in Funs.DB.View_Hazard_HazardRegister
                        where x.SafeSupervisionId == SafeSupervisionId.ToString() && (x.SafeSupervisionIsOK == false || x.SafeSupervisionIsOK == null)
                        select x;
                notOKNum = q.Count().ToString();
            }
            return notOKNum;
        }

        /// <summary>
        /// 获取整改完成项数
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        protected string ConvertCompletedNum(object SafeSupervisionId)
        {
            string completedNum = string.Empty;
            if (SafeSupervisionId != null)
            {
                var q = from x in Funs.DB.View_Hazard_HazardRegister
                        where x.SafeSupervisionId == SafeSupervisionId.ToString() && x.States == "3"
                        select x;
                completedNum = q.Count().ToString();
            }
            return completedNum;
        }
    }
}