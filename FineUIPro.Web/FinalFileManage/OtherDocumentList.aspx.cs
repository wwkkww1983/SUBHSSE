using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.FinalFileManage
{
    public partial class OtherDocumentList : PageBase
    {
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
                ////权限按钮方法
                this.GetButtonPower();
                this.btnNew.OnClientClick = Window1.GetShowReference("OtherDocumentListEdit.aspx") + "return false;";
                if (this.CurrUser != null && this.CurrUser.PageSize.HasValue)
                {
                    Grid1.PageSize = this.CurrUser.PageSize.Value;
                }
                this.ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                this.BindGrid();                
            }
        }
        #endregion

        #region GV绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT OtherDocumentList.FileId,OtherDocumentList.ProjectId,CodeRecords.Code AS FileCode,OtherDocumentList.FileName,OtherDocumentList.KeyWords,CompileManUser.UserName AS CompileManName,OtherDocumentList.CompileDate,OtherDocumentList.States "
                          + @" ,(CASE WHEN OtherDocumentList.States = " + BLL.Const.State_0 + " OR OtherDocumentList.States IS NULL THEN '待['+OperateUser.UserName+']提交' WHEN OtherDocumentList.States =  " + BLL.Const.State_2 + " THEN '审核/审批完成' ELSE '待['+OperateUser.UserName+']办理' END) AS  FlowOperateName"
                          + @" FROM FinalFileManage_OtherDocumentList AS OtherDocumentList"
                          + @" LEFT JOIN Sys_CodeRecords AS CodeRecords ON OtherDocumentList.FileId=CodeRecords.DataId "
                          + @" LEFT JOIN Sys_FlowOperate AS FlowOperate ON OtherDocumentList.FileId=FlowOperate.DataId AND FlowOperate.IsClosed <> 1"
                          + @" LEFT JOIN Sys_User AS OperateUser ON FlowOperate.OperaterId=OperateUser.UserId"
                          + @" LEFT JOIN Sys_User AS CompileManUser ON OtherDocumentList.CompileMan =CompileManUser.UserId WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND OtherDocumentList.ProjectId = @ProjectId";
            if (!string.IsNullOrEmpty(Request.Params["projectId"]))  ///是否文档柜查看页面传项目值
            {
                listStr.Add(new SqlParameter("@ProjectId", Request.Params["projectId"]));
                strSql += " AND OtherDocumentList.States = @States";  ///状态为已完成
                listStr.Add(new SqlParameter("@States", BLL.Const.State_2)); 
            }
            else
            {
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            }

            if (!string.IsNullOrEmpty(this.txtFileCode.Text.Trim()))
            {
                strSql += " AND FileCode LIKE @FileCode";
                listStr.Add(new SqlParameter("@FileCode", "%" + this.txtFileCode.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtFileName.Text.Trim()))
            {
                strSql += " AND OtherDocumentList.FileName LIKE @FileName";
                listStr.Add(new SqlParameter("@FileName", "%" + this.txtFileName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtKeyWords.Text.Trim()))
            {
                strSql += " AND OtherDocumentList.KeyWords LIKE @KeyWords";
                listStr.Add(new SqlParameter("@KeyWords", "%" + this.txtKeyWords.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 分页 排序
        /// <summary>
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Grid1.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
            BindGrid();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            this.BindGrid();
        }
        #endregion
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
            var FinalFileManage = BLL.OtherDocumentListService.GetOtherDocumentListById(id);
            if (FinalFileManage != null)
            {
                if (this.btnMenuEdit.Hidden || FinalFileManage.States == BLL.Const.State_2)   ////双击事件 编辑权限有：编辑页面，无：查看页面 或者状态是完成时查看页面
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("OtherDocumentListView.aspx?FileId={0}", id, "查看 - ")));                    
                }
                else
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("OtherDocumentListEdit.aspx?FileId={0}", id, "编辑 - ")));
                }
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
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var OtherDocumentList = BLL.OtherDocumentListService.GetOtherDocumentListById(rowID);
                    if (OtherDocumentList != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, OtherDocumentList.FileCode, OtherDocumentList.FileId, BLL.Const.OtherDocumentListMenuId, BLL.Const.BtnDelete);
                        BLL.OtherDocumentListService.DeleteOtherDocumentListById(rowID);
                    }
                }

                this.BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }       
        #endregion
       
        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.OtherDocumentListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}