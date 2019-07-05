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
    public partial class GasCylinderOutView :PageBase
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                
                this.GasCylinderOutId = Request.Params["GasCylinderOutId"];
                if (!string.IsNullOrEmpty(this.GasCylinderOutId))
                {
                    Model.InApproveManager_GasCylinderOut gasCylinderOut = BLL.GasCylinderOutService.GetGasCylinderOutById(this.GasCylinderOutId);
                    if (gasCylinderOut != null)
                    {
                        this.txtGasCylinderOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GasCylinderOutId);
                        if (!string.IsNullOrEmpty(gasCylinderOut.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(gasCylinderOut.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (gasCylinderOut.OutDate != null && gasCylinderOut.OutTime != null)
                        {
                            string outDate = string.Format("{0:yyyy-MM-dd}", gasCylinderOut.OutDate);
                            string outTime = string.Format("{0:t}", gasCylinderOut.OutTime);
                            this.txtOutDate.Text = outDate + " " + outTime;
                        }
                        this.txtDriverName.Text = gasCylinderOut.DriverName;
                        this.txtDriverNum.Text = gasCylinderOut.DriverNum;
                        this.txtCarNum.Text = gasCylinderOut.CarNum;
                        this.txtLeaderName.Text = gasCylinderOut.LeaderName;
                    }
                    BindGrid();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GasCylinderOutMenuId;
                this.ctlAuditFlow.DataId = this.GasCylinderOutId;
            }
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

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GasCylinderOutId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GasCylinderOutAttachUrl&menuId={1}&type=-1", this.GasCylinderOutId, BLL.Const.GasCylinderOutMenuId)));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("气瓶出场报批明细" + filename, System.Text.Encoding.UTF8) + ".xls");
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