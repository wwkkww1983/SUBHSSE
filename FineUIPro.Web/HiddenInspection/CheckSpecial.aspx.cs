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
    public partial class CheckSpecial : PageBase
    {
        #region 定义项
        /// <summary>
        /// APP专项检查主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
            }
        }

        /// <summary>
        /// 新建APP专项检查主键
        /// </summary>
        public string NewCheckSpecialId
        {
            get
            {
                return (string)ViewState["NewCheckSpecialId"];
            }
            set
            {
                ViewState["NewCheckSpecialId"] = value;
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
            string strSql = "select * from View_Hazard_CheckSpecial where ProjectId=@ProjectId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (!string.IsNullOrEmpty(this.txtCheckSpecialCode.Text.Trim()))
            {
                strSql += " And CheckSpecialCode like @CheckSpecialCode";
                listStr.Add(new SqlParameter("@CheckSpecialCode", "%" + this.txtCheckSpecialCode.Text.Trim() + "%"));
            }
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
            //var q = BLL.HSSE_Solution_ConstructSolutionService.GetConstructSolutionByCheckSpecialId(this.NewCheckSpecialId);
            //if (q == null)
            //{
            //    BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, this.NewCheckSpecialId, Const.HSSE_APPCheckSpecialMenuId);
            //}
            BindGrid();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.hdRemark.Text))
            //{
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationPrint.aspx?CheckSpecialId={0}&CheckType=CheckSpecial", this.CheckSpecialId, "查看 - ")));
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("HiddenRectificationRecordPrint.aspx?CheckSpecialId={0}&CheckType=CheckSpecial&Remark={1}", this.CheckSpecialId, this.hdRemark.Text, "查看 - ")));
            this.hdRemark.Text = string.Empty;
            //}
        }
        #endregion

        #region 编制
        /// <summary>
        /// 编制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnAdd))
            {
                Model.Project_ProjectUnit unit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId,this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == BLL.Const.ProjectUnitType_2)
                {
                    Alert.ShowInTop("施工分包无法编制专项检查！", MessageBoxIcon.Warning);
                    return;
                }
                //var audiFlowsList = BLL.AudiFlowService.GetAudiFlowsByFlowModule(Const.HSSE_APPCheckSpecialMenuId);
                //if (audiFlowsList.Count > 0)
                //{
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx", "编辑 - ")));
                //}
                //else
                //{
                //    Alert.ShowInTop("请先设置审批流程！", MessageBoxIcon.Warning);
                //    return;
                //}
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
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
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete))
            {
                Model.Project_ProjectUnit unit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == BLL.Const.ProjectUnitType_2)
                {
                    Alert.ShowInTop("施工分包无法删除专项检查！", MessageBoxIcon.Warning);
                    return;
                }
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
                                          where x.CheckSpecialId == rowID
                                          select x;
                            foreach (var detail in details)
                            {
                                detail.CheckSpecialId = null;
                                Funs.DB.SubmitChanges();
                            }

                            BLL.HSSE_Hazard_CheckSpecialAuditService.DeleteCheckSpecialAuditByCheckSpecialId(rowID);
                            BLL.LogService.AddSys_Log(this.CurrUser, "删除APP专项检查", CheckSpecialId, Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete);
                            // 先删除附件，再删除数据信息
                            BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, rowID, Const.HSSE_APPCheckSpecialMenuId);
                            BLL.HSSE_Hazard_CheckSpecialService.DeleteCheckSpecial(rowID);
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

        #region 格式化字符串
        /// <summary>
        /// 获取编制人
        /// </summary>
        /// <param name="CheckSpecialId"></param>
        /// <returns></returns>
        public string ConvertCompileManName(object CheckSpecialId)
        {
            string name = string.Empty;
            Model.HSSE_Hazard_CheckSpecialAudit audit = BLL.HSSE_Hazard_CheckSpecialAuditService.GetCheckSpecialAuditByCheckSpecialIdAndHandleStep(CheckSpecialId.ToString(), BLL.Const.APPCheckSpecial_Compile);
            if (audit != null)
            {
                name = BLL.UserService.GetUserNameByUserId(audit.AuditMan);
            }
            return name;
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
            this.CheckSpecialId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Project_ProjectUnit unit = BLL.ProjectUnitService.GetProjectUnitByUnitIdProjectId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
            if (unit != null && unit.UnitType == BLL.Const.ProjectUnitType_2)
            {
                Alert.ShowInTop("施工分包无法操作专项检查！", MessageBoxIcon.Warning);
                return;
            }
            Model.HSSE_Hazard_CheckSpecial checkSpecial = BLL.HSSE_Hazard_CheckSpecialService.GetCheckSpecialByCheckSpecialId(CheckSpecialId);
            #region 修改、审核
            if (e.CommandName == "click")
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnAuditing))
                {
                    if (checkSpecial.States == BLL.Const.APPCheckSpecial_ApproveCompleted)
                    {
                        Alert.ShowInTop("此APP专项检查已经办理完结！", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Model.HSSE_Hazard_CheckSpecialAudit audit = BLL.HSSE_Hazard_CheckSpecialAuditService.GetCheckSpecialAuditByCheckSpecialId(CheckSpecialId);
                        if (audit == null)
                        {
                            if (checkSpecial.States == BLL.Const.APPCheckSpecial_Compile)
                            {
                                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx?CheckSpecialId={0}", CheckSpecialId, "编辑 - ")));
                            }
                            else
                            {
                                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx", "编辑 - ")));
                            }
                        }
                        else
                        {
                            if (this.CurrUser.UserId == audit.AuditMan || this.CurrUser.UserId == BLL.Const.sysglyId)
                            {
                                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialEdit.aspx?CheckSpecialId={0}", CheckSpecialId, "编辑 - ")));
                            }
                            else
                            {
                                ShowNotify("你不是指定的办理人，不能操作此条信息！", MessageBoxIcon.Warning);
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
            #endregion
            #region 查看详细信息
            if (e.CommandName == "particular")//查看详细信息
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialParticular.aspx?CheckSpecialId={0}", this.CheckSpecialId, "查看 - ")));
            }
            #endregion
            #region 打印
            if (e.CommandName == "print")//打印
            {
                //PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationPrint.aspx?CheckSpecialId={0}&CheckType=CheckSpecial", this.CheckSpecialId, "查看 - ")));
                string window = String.Format("CheckRemark.aspx", "编辑 - ");
                PageContext.RegisterStartupScript(Window2.GetSaveStateReference(hdRemark.ClientID) + Window2.GetShowReference(window));
            }
            #endregion
            #region 删除
            if (e.CommandName == "del")//删除
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete))
                {
                    var details = from x in Funs.DB.HSSE_Hazard_HazardRegister
                                  where x.CheckSpecialId == CheckSpecialId
                                  select x;
                    foreach (var detail in details)
                    {
                        detail.CheckSpecialId = null;
                        Funs.DB.SubmitChanges();
                    }
                    BLL.HSSE_Hazard_CheckSpecialAuditService.DeleteCheckSpecialAuditByCheckSpecialId(CheckSpecialId);

                    BLL.LogService.AddSys_Log(this.CurrUser, "删除APP专项检查", CheckSpecialId, Const.HSSE_APPCheckSpecialMenuId, BLL.Const.BtnDelete);
                    // 先删除附件，再删除数据信息
                    BLL.AttachFileService.DeleteAttachFile(BLL.Funs.RootPath, this.CheckSpecialId, Const.HSSE_APPCheckSpecialMenuId);
                    BLL.HSSE_Hazard_CheckSpecialService.DeleteCheckSpecial(CheckSpecialId);
                   
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
    }
}