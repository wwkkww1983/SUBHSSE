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
    public partial class GasCylinderInView : PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
               
                this.GasCylinderInId = Request.Params["GasCylinderInId"];
                if (!string.IsNullOrEmpty(this.GasCylinderInId))
                {
                    Model.InApproveManager_GasCylinderIn gasCylinderIn = BLL.GasCylinderInService.GetGasCylinderInById(this.GasCylinderInId);
                    if (gasCylinderIn != null)
                    {
                        this.txtGasCylinderInCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GasCylinderInId);
                        if (!string.IsNullOrEmpty(gasCylinderIn.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(gasCylinderIn.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (gasCylinderIn.InDate != null && gasCylinderIn.InTime != null)
                        {
                            string inDate = string.Format("{0:yyyy-MM-dd}", gasCylinderIn.InDate);
                            string inTime = string.Format("{0:t}", gasCylinderIn.InTime);
                            this.txtInDate.Text = inDate + " " + inTime;
                        }
                        this.txtDriverMan.Text = gasCylinderIn.DriverMan;
                        this.txtDriverNum.Text = gasCylinderIn.DriverNum;
                        this.txtCarNum.Text = gasCylinderIn.CarNum;
                        this.txtLeadCarMan.Text = gasCylinderIn.LeadCarMan;
                    }
                    BindGrid();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GasCylinderInMenuId;
                this.ctlAuditFlow.DataId = this.GasCylinderInId;
            }
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
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GasCylinderInId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GasCylinderInAttachUrl&menuId={1}&type=-1", this.GasCylinderInId, BLL.Const.GasCylinderInMenuId)));
            }
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

#pragma warning disable CS0108 // “GasCylinderInView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(Grid grid)
#pragma warning restore CS0108 // “GasCylinderInView.GetGridTableHtml(Grid)”隐藏继承的成员“PageBase.GetGridTableHtml(Grid)”。如果是有意隐藏，请使用关键字 new。
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