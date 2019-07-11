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
    public partial class GasCylinderInEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GasCylinderInId
        {
            get
            {
                return (string)ViewState["GasCylinderInId"];
            }
            set
            {
                ViewState["GasCylinderInId"] = value;
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
        /// 定义集合
        /// </summary>
        private static List<Model.InApproveManager_GasCylinderInItem> gasCylinderInItems = new List<Model.InApproveManager_GasCylinderInItem>();
        #endregion

        #region 加载
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
                this.GasCylinderInId = Request.Params["GasCylinderInId"];
                if (!string.IsNullOrEmpty(this.GasCylinderInId))
                {
                    Model.InApproveManager_GasCylinderIn gasCylinderIn = BLL.GasCylinderInService.GetGasCylinderInById(this.GasCylinderInId);
                    if (gasCylinderIn != null)
                    {
                        this.ProjectId = gasCylinderIn.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGasCylinderInCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GasCylinderInId);
                        if (!string.IsNullOrEmpty(gasCylinderIn.UnitId))
                        {
                            this.drpUnitId.SelectedValue = gasCylinderIn.UnitId;
                        }
                        if (gasCylinderIn.InDate != null)
                        {
                            this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", gasCylinderIn.InDate);
                        }
                        if (gasCylinderIn.InTime != null)
                        {
                            this.txtInTime.Text = string.Format("{0:t}", gasCylinderIn.InTime);
                        }
                        this.txtDriverMan.Text = gasCylinderIn.DriverMan;
                        this.txtDriverNum.Text = gasCylinderIn.DriverNum;
                        this.txtCarNum.Text = gasCylinderIn.CarNum;
                        this.txtLeadCarMan.Text = gasCylinderIn.LeadCarMan;
                    }
                    BindGrid();
                }
                else
                {
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtInTime.Text = string.Format("{0:t}", DateTime.Now);
                    ////自动生成编码
                    this.txtGasCylinderInCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GasCylinderInMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GasCylinderInMenuId;
                this.ctlAuditFlow.DataId = this.GasCylinderInId;
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
            gasCylinderInItems = BLL.GasCylinderInItemService.GetGasCylinderInItemByGasCylinderInId(this.GasCylinderInId);
            this.Grid1.DataSource = gasCylinderInItems;
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
        #endregion

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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GasCylinderInItemEdit.aspx?GasCylinderInItemId={0}", id, "编辑 - ")));
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
                        BLL.GasCylinderInItemService.DeleteGasCylinderInItemById(rowID);
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
            Model.InApproveManager_GasCylinderIn gasCylinderIn = new Model.InApproveManager_GasCylinderIn
            {
                ProjectId = this.ProjectId,
                GasCylinderInCode = this.txtGasCylinderInCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                gasCylinderIn.UnitId = this.drpUnitId.SelectedValue;
            }
            gasCylinderIn.InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim());
            gasCylinderIn.InTime = Funs.GetNewDateTime(this.txtInTime.Text.Trim());
            gasCylinderIn.DriverMan = this.txtDriverMan.Text.Trim();
            gasCylinderIn.DriverNum = this.txtDriverNum.Text.Trim();
            gasCylinderIn.CarNum = this.txtCarNum.Text.Trim();
            gasCylinderIn.LeadCarMan = this.txtLeadCarMan.Text.Trim();
            gasCylinderIn.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                gasCylinderIn.States = this.ctlAuditFlow.NextStep;
            }
            gasCylinderIn.CompileMan = this.CurrUser.UserId;
            gasCylinderIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GasCylinderInId))
            {
                gasCylinderIn.GasCylinderInId = this.GasCylinderInId;
                BLL.GasCylinderInService.UpdateGasCylinderIn(gasCylinderIn);
                BLL.LogService.AddSys_Log(this.CurrUser, gasCylinderIn.GasCylinderInCode, gasCylinderIn.GasCylinderInId,BLL.Const.GasCylinderInMenuId,BLL.Const.BtnModify );
            }
            else
            {
                this.GasCylinderInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GasCylinderIn));
                gasCylinderIn.GasCylinderInId = this.GasCylinderInId;
                BLL.GasCylinderInService.AddGasCylinderIn(gasCylinderIn);
                BLL.LogService.AddSys_Log(this.CurrUser, gasCylinderIn.GasCylinderInCode, gasCylinderIn.GasCylinderInId, BLL.Const.GasCylinderInMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GasCylinderInMenuId, this.GasCylinderInId, (type == BLL.Const.BtnSubmit ? true : false), (gasCylinderIn.DriverMan + gasCylinderIn.CarNum), "../InApproveManager/GasCylinderInView.aspx?GasCylinderInId={0}");
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
            if (string.IsNullOrEmpty(this.GasCylinderInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GasCylinderInAttachUrl&menuId={1}", this.GasCylinderInId, BLL.Const.GasCylinderInMenuId)));
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
            if (string.IsNullOrEmpty(this.GasCylinderInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GasCylinderInItemEdit.aspx?GasCylinderInId={0}", this.GasCylinderInId, "编辑 - ")));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("气瓶入场报批明细" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
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

                    if (column.ColumnID == "tfPM_IsFull")
                    {
                        html = (row.FindControl("lblPM_IsFull") as AspNet.Label).Text;
                    }

                    if (column.ColumnID == "tfFZQ_IsFull")
                    {
                        html = (row.FindControl("lblFZQ_IsFull") as AspNet.Label).Text;
                    }

                    if (column.ColumnID == "tfIsSameCar")
                    {
                        html = (row.FindControl("lblIsSameCar") as AspNet.Label).Text;
                    }
                    sb.AppendFormat("<td>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        protected string ConvertB(object i)
        {
            if (i != null)
            {
                if (i.ToString() == "True")
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            return null;
        }
    }
}