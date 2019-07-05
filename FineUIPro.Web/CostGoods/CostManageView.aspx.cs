using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class CostManageView :PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string CostManageId
        {
            get
            {
                return (string)ViewState["CostManageId"];
            }
            set
            {
                ViewState["CostManageId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.CostGoods_CostManageItem> costManageItems = new List<Model.CostGoods_CostManageItem>();
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
               
                this.CostManageId = Request.Params["CostManageId"];
                if (!string.IsNullOrEmpty(this.CostManageId))
                {
                    Model.CostGoods_CostManage costManage = BLL.CostManageService.GetCostManageById(this.CostManageId);
                    if (costManage != null)
                    {
                        this.txtCostManageCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CostManageId);
                        this.txtCostManageName.Text = costManage.CostManageName;
                        if (!string.IsNullOrEmpty(costManage.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(costManage.UnitId);
                            if (unit!=null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        this.txtContractNum.Text = costManage.ContractNum;
                        if (costManage.CostManageDate != null)
                        {
                            this.txtCostManageDate.Text = string.Format("{0:yyyy-MM-dd}", costManage.CostManageDate);
                        }
                        this.txtOpinion.Text = costManage.Opinion;
                        this.txtSubHSE.Text = costManage.SubHSE;
                        this.txtSubCN.Text = costManage.SubCN;
                        this.txtSubProject.Text = costManage.SubProject;
                    }
                    BindGrid();
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCostManageMenuId;
                this.ctlAuditFlow.DataId = this.CostManageId;
            }
        }

        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            costManageItems = BLL.CostManageItemService.GetCostManageItemByCostManageId(this.CostManageId);
            this.Grid1.DataSource = costManageItems;
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

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CostManageId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CostManageAttachUrl&menuId={1}", this.CostManageId, BLL.Const.ProjectCostManageMenuId)));
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取总价
        /// </summary>
        /// <param name="costManageId"></param>
        /// <returns></returns>
        protected string GetTotalMoney(object costManageItemId)
        {
            string total = string.Empty;
            if (costManageItemId != null)
            {
                var costManageItem = BLL.CostManageItemService.GetCostManageItemById(costManageItemId.ToString());
                if (costManageItem != null)
                {
                    decimal? price = costManageItem.PriceMoney;
                    int? count = costManageItem.Counts;
                    total = Convert.ToString(price * count);
                }
            }
            return total;
        }
        #endregion
    }
}