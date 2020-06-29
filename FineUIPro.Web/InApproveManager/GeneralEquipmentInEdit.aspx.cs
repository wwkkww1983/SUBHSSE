﻿using System;
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
    public partial class GeneralEquipmentInEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GeneralEquipmentInId
        {
            get
            {
                return (string)ViewState["GeneralEquipmentInId"];
            }
            set
            {
                ViewState["GeneralEquipmentInId"] = value;
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
        public static List<Model.InApproveManager_GeneralEquipmentInItem> generalEquipmentInItems = new List<Model.InApproveManager_GeneralEquipmentInItem>();
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
                this.GeneralEquipmentInId = Request.Params["GeneralEquipmentInId"];
                if (!string.IsNullOrEmpty(this.GeneralEquipmentInId))
                {
                    Model.InApproveManager_GeneralEquipmentIn generalEquipmentIn = BLL.GeneralEquipmentInService.GetGeneralEquipmentInById(this.GeneralEquipmentInId);
                    if (generalEquipmentIn!=null)
                    {
                        this.ProjectId = generalEquipmentIn.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGeneralEquipmentInCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.GeneralEquipmentInId);
                        if (!string.IsNullOrEmpty(generalEquipmentIn.UnitId))
                        {
                            this.drpUnitId.SelectedValue = generalEquipmentIn.UnitId;
                        }
                        this.txtCarNumber.Text = generalEquipmentIn.CarNumber;
                        this.txtSubProjectName.Text = generalEquipmentIn.SubProjectName;
                        this.txtContentDef.Text = generalEquipmentIn.ContentDef;
                        this.txtOtherDef.Text = generalEquipmentIn.OtherDef;
                    }
                    BindGrid();
                }
                else
                {
                    ////自动生成编码
                    this.txtGeneralEquipmentInCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GeneralEquipmentInMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GeneralEquipmentInMenuId;
                this.ctlAuditFlow.DataId = this.GeneralEquipmentInId;
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
            generalEquipmentInItems = BLL.GeneralEquipmentInItemService.GetGeneralEquipmentInItemByGeneralEquipmentInId(this.GeneralEquipmentInId);
            this.Grid1.DataSource = generalEquipmentInItems;
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GeneralEquipmentInItemEdit.aspx?GeneralEquipmentInItemId={0}", id, "编辑 - ")));
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
                        BLL.GeneralEquipmentInItemService.DeleteGeneralEquipmentInItemById(rowID);
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
            Model.InApproveManager_GeneralEquipmentIn generalEquipmentIn = new Model.InApproveManager_GeneralEquipmentIn
            {
                ProjectId = this.ProjectId,
                GeneralEquipmentInCode = this.txtGeneralEquipmentInCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                generalEquipmentIn.UnitId = this.drpUnitId.SelectedValue;
            }
            generalEquipmentIn.CarNumber = this.txtCarNumber.Text.Trim();
            generalEquipmentIn.SubProjectName = this.txtSubProjectName.Text.Trim();
            generalEquipmentIn.ContentDef = this.txtContentDef.Text.Trim();
            generalEquipmentIn.OtherDef = this.txtOtherDef.Text.Trim();
            generalEquipmentIn.State = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                generalEquipmentIn.State = this.ctlAuditFlow.NextStep;
            }
            generalEquipmentIn.CompileMan = this.CurrUser.UserId;
            generalEquipmentIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GeneralEquipmentInId))
            {
                generalEquipmentIn.GeneralEquipmentInId = this.GeneralEquipmentInId;
                BLL.GeneralEquipmentInService.UpdateGeneralEquipmentIn(generalEquipmentIn);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentIn.GeneralEquipmentInCode, generalEquipmentIn.GeneralEquipmentInId, BLL.Const.GeneralEquipmentInMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GeneralEquipmentInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GeneralEquipmentIn));
                generalEquipmentIn.GeneralEquipmentInId = this.GeneralEquipmentInId;
                BLL.GeneralEquipmentInService.AddGeneralEquipmentIn(generalEquipmentIn);
                BLL.LogService.AddSys_Log(this.CurrUser, generalEquipmentIn.GeneralEquipmentInCode, generalEquipmentIn.GeneralEquipmentInId,BLL.Const.GeneralEquipmentInMenuId,BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GeneralEquipmentInMenuId, this.GeneralEquipmentInId, (type == BLL.Const.BtnSubmit ? true : false), (generalEquipmentIn.CarNumber + generalEquipmentIn.SubProjectName), "../InApproveManager/GeneralEquipmentInView.aspx?GeneralEquipmentInId={0}");
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
            if (string.IsNullOrEmpty(this.GeneralEquipmentInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GeneralEquipmentInAttachUrl&menuId={1}", this.GeneralEquipmentInId, BLL.Const.GeneralEquipmentInMenuId)));
        }
        #endregion

        #region 新增主要设备基础情况
        /// <summary>
        /// 添加主要设备基础情况
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
            if (string.IsNullOrEmpty(this.GeneralEquipmentInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("GeneralEquipmentInItemEdit.aspx?GeneralEquipmentInId={0}", this.GeneralEquipmentInId, "编辑 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取设备名称
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        protected string ConvertEqiupment(object equipmentId)
        {
            string equipmentName = string.Empty;
            if (equipmentId != null)
            {
                var specialEquipment = BLL.SpecialEquipmentService.GetSpecialEquipmentById(equipmentId.ToString());
                if (specialEquipment != null)
                {
                    equipmentName = specialEquipment.SpecialEquipmentName;
                }
            }
            return equipmentName;
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("一般设备机具入场明细报批" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = 500;
            this.BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

#pragma warning disable CS0108 // “GeneralEquipmentInEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “GeneralEquipmentInEdit.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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
                    if (column.ColumnID == "tfSpecialEquipmentId")
                    {
                        html = (row.FindControl("lblSpecialEquipmentId") as AspNet.Label).Text;
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