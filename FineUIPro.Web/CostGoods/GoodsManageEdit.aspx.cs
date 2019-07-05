using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsManageEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsManageId
        {
            get
            {
                return (string)ViewState["GoodsManageId"];
            }
            set
            {
                ViewState["GoodsManageId"] = value;
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
                this.GoodsManageId = Request.Params["GoodsManageId"];
                if (!string.IsNullOrEmpty(this.GoodsManageId))
                {
                    Model.CostGoods_GoodsManage goodsManage = BLL.GoodsManageService.GetGoodsManageById(this.GoodsManageId);
                    if (goodsManage != null)
                    {
                        this.ProjectId = goodsManage.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGoodsCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsManageId);
                        this.txtGoodsName.Text = goodsManage.GoodsName;
                        if (!string.IsNullOrEmpty(goodsManage.UnitId))
                        {
                            this.drpUnitId.SelectedValue = goodsManage.UnitId;
                        }
                        if (!string.IsNullOrEmpty(goodsManage.GoodsCategoryId))
                        {
                            this.drpGoodsCategory.SelectedValue = goodsManage.GoodsCategoryId;
                        }

                        this.txtSizeModel.Text = goodsManage.SizeModel;
                        this.txtFactoryCode.Text = goodsManage.FactoryCode;
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", goodsManage.CheckDate);
                        this.txtEnableYear.Text = Convert.ToString(goodsManage.EnableYear);
                        if (!string.IsNullOrEmpty(goodsManage.CheckPerson))
                        {
                            this.drpCheckPerson.SelectedValue = goodsManage.CheckPerson;
                        }
                        this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", goodsManage.InTime);
                        this.txtRemark.Text = goodsManage.Remark;
                    }
                }
                else
                {
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.drpCheckPerson.SelectedValue = this.CurrUser.UserId;
                    this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtEnableYear.Text = "1";
                    ////自动生成编码
                    this.txtGoodsCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GoodsManageMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsManageMenuId;
                this.ctlAuditFlow.DataId = this.GoodsManageId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.ProjectId, true);
            BLL.UserService.InitUserDropDownList(this.drpCheckPerson, this.ProjectId, true);
            BLL.GoodsCategoryService.InitUnitDropDownList(this.drpGoodsCategory, true);
        }

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.drpUnitId.SelectedValue == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择所属单位！", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请选择所属单位！", MessageBoxIcon.Warning);
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
            Model.CostGoods_GoodsManage goodsManage = new Model.CostGoods_GoodsManage
            {
                ProjectId = this.ProjectId,
                GoodsCode = this.txtGoodsCode.Text.Trim(),
                GoodsName = this.txtGoodsName.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                goodsManage.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpGoodsCategory.SelectedValue != BLL.Const._Null)
            {
                goodsManage.GoodsCategoryId = this.drpGoodsCategory.SelectedValue;
            }
            goodsManage.SizeModel = this.txtSizeModel.Text.Trim();
            goodsManage.FactoryCode = this.txtFactoryCode.Text.Trim();
            goodsManage.CheckDate = Funs.GetNewDateTime(this.txtCheckDate.Text);
            if (!string.IsNullOrEmpty(this.txtEnableYear.Text.Trim()))
            {
                goodsManage.EnableYear = Funs.GetNewIntOrZero(this.txtEnableYear.Text);
            }
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                goodsManage.CheckPerson = this.drpCheckPerson.SelectedValue;
            }
            goodsManage.InTime = Funs.GetNewDateTime(this.txtInTime.Text.Trim());
            goodsManage.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                goodsManage.States = this.ctlAuditFlow.NextStep;
            }
            goodsManage.CompileMan = this.CurrUser.UserId;
            goodsManage.CompileDate = DateTime.Now;
            goodsManage.Remark = this.txtRemark.Text.Trim();
            if (!string.IsNullOrEmpty(this.GoodsManageId))
            {
                goodsManage.GoodsManageId = this.GoodsManageId;
                BLL.GoodsManageService.UpdateGoodsManage(goodsManage);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改物资管理", goodsManage.GoodsManageId);
            }
            else
            {
                this.GoodsManageId = SQLHelper.GetNewID(typeof(Model.CostGoods_GoodsManage));
                goodsManage.GoodsManageId = this.GoodsManageId;
                BLL.GoodsManageService.AddGoodsManage(goodsManage);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加物资管理", goodsManage.GoodsManageId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GoodsManageMenuId, this.GoodsManageId, (type == BLL.Const.BtnSubmit ? true : false), goodsManage.GoodsName, "../CostGoods/GoodsManageView.aspx?GoodsManageId={0}");
        }
        #endregion
    }
}