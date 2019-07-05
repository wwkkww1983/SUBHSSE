using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class CostManageEdit : PageBase
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                this.CostManageId = Request.Params["CostManageId"];
                if (!string.IsNullOrEmpty(this.CostManageId))
                {
                    Model.CostGoods_CostManage costManage = BLL.CostManageService.GetCostManageById(this.CostManageId);
                    if (costManage != null)
                    {
                        this.ProjectId = costManage.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtCostManageCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CostManageId);
                        this.txtCostManageName.Text = costManage.CostManageName;
                        if (!string.IsNullOrEmpty(costManage.UnitId))
                        {
                            this.drpUnitId.SelectedValue = costManage.UnitId;
                        }
                        this.txtContractNum.Text = costManage.ContractNum;
                        this.txtCostManageDate.Text = string.Format("{0:yyyy-MM-dd}", costManage.CostManageDate);
                        this.txtOpinion.Text = costManage.Opinion;
                        this.txtSubHSE.Text = costManage.SubHSE;
                        this.txtSubCN.Text = costManage.SubCN;
                        this.txtSubProject.Text = costManage.SubProject;
                    }
                    BindGrid();
                }
                else
                {
                    this.txtCostManageCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectCostManageMenuId, this.ProjectId, this.CurrUser.UserId);
                    this.txtCostManageName.Text = "分包商HSE措施费申请表";
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCostManageMenuId;
                this.ctlAuditFlow.DataId = this.CostManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {  
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
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
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CostManageItemEdit.aspx?CostManageItemId={0}", id, "编辑 - ")));
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
                    BLL.CostManageItemService.DeleteCostManageItemById(rowID);
                }
                BindGrid();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
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
                Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
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
            Model.CostGoods_CostManage costManage = new Model.CostGoods_CostManage
            {
                ProjectId = this.ProjectId,
                CostManageCode = this.txtCostManageCode.Text.Trim(),
                CostManageName = this.txtCostManageName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                costManage.UnitId = this.drpUnitId.SelectedValue;
            }
            costManage.ContractNum = this.txtContractNum.Text.Trim();
            costManage.CostManageDate = Funs.GetNewDateTime(this.txtCostManageDate.Text.Trim());
            costManage.Opinion = this.txtOpinion.Text.Trim();
            costManage.SubHSE = this.txtSubHSE.Text.Trim();
            costManage.SubCN = this.txtSubCN.Text.Trim();
            costManage.SubProject = this.txtSubProject.Text.Trim();
            costManage.CompileMan = this.CurrUser.UserId;
            costManage.CompileDate = DateTime.Now;
            costManage.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                costManage.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CostManageId))
            {
                costManage.CostManageId = this.CostManageId;
                BLL.CostManageService.UpdateCostManage(costManage);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改安全费用管理", costManage.CostManageId);
            }
            else
            {
                this.CostManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_CostManage));
                costManage.CostManageId = this.CostManageId;
                BLL.CostManageService.AddCostManage(costManage);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加安全费用管理", costManage.CostManageId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCostManageMenuId, this.CostManageId, (type == BLL.Const.BtnSubmit ? true : false), costManage.CostManageName, "../CostGoods/CostManageView.aspx?CostManageId={0}");
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
            if (string.IsNullOrEmpty(this.CostManageId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CostManageAttachUrl&menuId={1}", this.CostManageId, BLL.Const.ProjectCostManageMenuId)));
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
                Alert.ShowInTop("请选择分包商！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.CostManageId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CostManageItemEdit.aspx?CostManageId={0}", this.CostManageId, "编辑 - ")));
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