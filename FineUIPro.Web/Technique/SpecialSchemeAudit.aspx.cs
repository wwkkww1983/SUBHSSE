﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.IO;

namespace FineUIPro.Web.Technique
{
    public partial class SpecialSchemeAudit : PageBase
    {
        #region 加载页面
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
            string strSql = "select * from View_Technique_SpecialScheme where IsPass is null";
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

        #region 过滤表头、分页、排序、关闭窗口
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
                var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(rowID);
                if (specialScheme != null)
                {
                    PageBase.ShowFileEvent(specialScheme.AttachUrl);
                }
            }
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
        private void SetIsPass(bool isPass,string upSate)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var unit = BLL.CommonService.GetIsThisUnit();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(rowID);
                    if (specialScheme != null)
                    {
                        specialScheme.AuditDate = System.DateTime.Now;
                        specialScheme.AuditMan = this.CurrUser.UserId;
                        specialScheme.IsPass = isPass;
                        specialScheme.UpState = upSate;
                        BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                    }

                    if (upSate == BLL.Const.UpState_2 && unit != null && !string.IsNullOrEmpty(unit.UnitId))
                    {
                        UpSpecialScheme(rowID, unit.UnitId);
                    }
                }

                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "审核专项方案");
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

        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="specialSchemeId"></param>
        /// <param name="unitId"></param>
        private void UpSpecialScheme(string specialSchemeId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_SpecialSchemeTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs>(poxy_DataInsertTechnique_SpecialSchemeTableCompleted);
            var specialScheme = from x in Funs.DB.View_Technique_SpecialScheme
                                join y in Funs.DB.AttachFile on x.SpecialSchemeId equals y.ToKeyId
                                where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                select new HSSEService.Technique_SpecialScheme
                                {
                                    SpecialSchemeId = x.SpecialSchemeId,
                                    SpecialSchemeTypeId = x.SpecialSchemeTypeId,
                                    SpecialSchemeCode = x.SpecialSchemeCode,
                                    SpecialSchemeName = x.SpecialSchemeName,
                                    UnitId = unitId,
                                    CompileMan = x.CompileMan,
                                    CompileDate = x.CompileDate,
                                    Summary = x.Summary,
                                    IsPass = null,
                                    AttachFileId = y.AttachFileId,
                                    ToKeyId = y.ToKeyId,
                                    AttachSource = y.AttachSource,
                                    AttachUrl = y.AttachUrl,
                                    ////附件转为字节传送
                                    FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                };
            poxy.DataInsertTechnique_SpecialSchemeTableAsync(specialScheme.ToList());
        }
        #endregion

        #region 专项方案上报到集团公司
        /// <summary>
        /// 专项方案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_SpecialSchemeTableCompleted(object sender, HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(item);
                    if (specialScheme != null)
                    {
                        specialScheme.UpState = BLL.Const.UpState_3;
                        BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【专项方案】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【专项方案】上报到集团公司失败；");
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SpecialSchemeMenuId);
            if (buttonList.Count() > 0)
            {

                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {

                    this.btnUpPass.Hidden = false;
                }

                if (buttonList.Contains(BLL.Const.BtnAuditing))
                {
                    this.btnPass.Hidden = false;
                    this.btnNoPass.Hidden = false;

                }

            }
        }
        #endregion
    }
}