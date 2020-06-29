using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Text;
using AspNet = System.Web.UI.WebControls;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GasCylinderOutEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string GasCylinderOutId
        {
            get
            {
                return (string)ViewState["GasCylinderOutId"];
            }
            set
            {
                ViewState["GasCylinderOutId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        /// <summary>
        /// 定义变量
        /// </summary>
        private static List<Model.InApproveManager_GasCylinderOutItem> gasCylinderOutItems = new List<Model.InApproveManager_GasCylinderOutItem>();

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.GasCylinderOutId = Request.Params["GasCylinderOutId"];
                if (!string.IsNullOrEmpty(this.GasCylinderOutId))
                {
                    Model.InApproveManager_GasCylinderOut gasCylinderOut = BLL.GasCylinderOutService.GetGasCylinderOutById(this.GasCylinderOutId);
                    if (gasCylinderOut!=null)
                    {
                        this.ProjectId = gasCylinderOut.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGasCylinderOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GasCylinderOutId);
                        if (!string.IsNullOrEmpty(gasCylinderOut.UnitId))
                        {
                            this.drpUnitId.SelectedValue = gasCylinderOut.UnitId;
                        }
                        this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", gasCylinderOut.OutDate);
                        if (gasCylinderOut.OutTime!=null)
                        {
                            this.txtOutTime.Text = string.Format("{0:t}", gasCylinderOut.OutTime);
                        }
                        this.txtDriverName.Text = gasCylinderOut.DriverName;
                        this.txtDriverNum.Text = gasCylinderOut.DriverNum;
                        this.txtCarNum.Text = gasCylinderOut.CarNum;
                        this.txtLeaderName.Text = gasCylinderOut.LeaderName;
                    }
                    BindGrid();
                }
                else
                {
                    this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtOutTime.Text = string.Format("{0:t}", DateTime.Now);
                    ////自动生成编码
                    this.txtGasCylinderOutCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GasCylinderOutMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GasCylinderOutMenuId;
                this.ctlAuditFlow.DataId = this.GasCylinderOutId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);
        }

         /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            gasCylinderOutItems = BLL.GasCylinderOutItemService.GetGasCylinderOutItemByGasCylinderOutId(this.GasCylinderOutId);
            this.Grid1.DataSource = gasCylinderOutItems;
            this.Grid1.PageIndex = 0;
            this.Grid1.DataBind();
        }


        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region 编辑
        /// <summary>
        /// 双击事件
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
            string id = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GasCylinderOutItemEdit.aspx?GasCylinderOutItemId={0}", id, "编辑 - ")));
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
                        BLL.GasCylinderOutItemService.DeleteGasCylinderOutItemById(rowID);
                    }
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
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

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称！", MessageBoxIcon.Warning);
                return;
            }
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.InApproveManager_GasCylinderOut gasCylinderOut = new Model.InApproveManager_GasCylinderOut
            {
                ProjectId = this.ProjectId,
                GasCylinderOutCode = this.txtGasCylinderOutCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                gasCylinderOut.UnitId = this.drpUnitId.SelectedValue;
            }
            gasCylinderOut.OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim());
            gasCylinderOut.OutTime = Funs.GetNewDateTime(this.txtOutTime.Text.Trim());
            gasCylinderOut.DriverName = this.txtDriverName.Text.Trim();
            gasCylinderOut.DriverNum = this.txtDriverNum.Text.Trim();
            gasCylinderOut.CarNum = this.txtCarNum.Text.Trim();
            gasCylinderOut.LeaderName = this.txtLeaderName.Text.Trim();
            gasCylinderOut.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                gasCylinderOut.States = this.ctlAuditFlow.NextStep;
            }
            gasCylinderOut.CompileMan = this.CurrUser.UserId;
            gasCylinderOut.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GasCylinderOutId))
            {
                gasCylinderOut.GasCylinderOutId = this.GasCylinderOutId;
                BLL.GasCylinderOutService.UpdateGasCylinderOut(gasCylinderOut);
                BLL.LogService.AddSys_Log(this.CurrUser, gasCylinderOut.GasCylinderOutCode, gasCylinderOut.GasCylinderOutId,BLL.Const.GasCylinderOutMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.GasCylinderOutId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GasCylinderOut));
                gasCylinderOut.GasCylinderOutId = this.GasCylinderOutId;
                BLL.GasCylinderOutService.AddGasCylinderOut(gasCylinderOut);
                BLL.LogService.AddSys_Log(this.CurrUser, gasCylinderOut.GasCylinderOutCode, gasCylinderOut.GasCylinderOutId, BLL.Const.GasCylinderOutMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GasCylinderOutMenuId, this.GasCylinderOutId, (type == BLL.Const.BtnSubmit ? true : false), (gasCylinderOut.DriverName + gasCylinderOut.DriverNum), "../InApproveManager/GasCylinderOutView.aspx?GasCylinderOutId={0}");
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GasCylinderOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GasCylinderOutAttachUrl&menuId={1}", this.GasCylinderOutId, BLL.Const.GasCylinderOutMenuId)));
        }
        #endregion

        #region 新增气瓶基本情况
        /// <summary>
        /// 添加气瓶基本情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择单位名称", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.GasCylinderOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GasCylinderOutItemEdit.aspx?GasCylinderOutId={0}", this.GasCylinderOutId, "编辑 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取气瓶类型名称
        /// </summary>
        /// <param name="gasCylinderId"></param>
        /// <returns></returns>
        protected string ConvertGasCylinder(object gasCylinderId)
        {
            string gasCylinderName = string.Empty;
            if (gasCylinderId != null)
            {
                var gasCylinder = BLL.GasCylinderService.GetGasCylinderById(gasCylinderId.ToString());
                if (gasCylinder != null)
                {
                    gasCylinderName = gasCylinder.GasCylinderName;
                }
            }
            return gasCylinderName;
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("气瓶出场报批明细" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “GasCylinderOutEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “GasCylinderOutEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");
            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (GridColumn column in grid.Columns)
                {
                    string html = row.Values[column.ColumnIndex].ToString();
                    if (column.ColumnID == "tfNumber")
                    {
                        html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    }
                    if (column.ColumnID == "tfGasCylinderId")
                    {
                        html = (row.FindControl("lblGasCylinderId") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion
    }
}