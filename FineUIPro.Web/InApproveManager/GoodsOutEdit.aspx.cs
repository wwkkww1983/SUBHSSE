using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GoodsOutEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GoodsOutId
        {
            get
            {
                return (string)ViewState["GoodsOutId"];
            }
            set
            {
                ViewState["GoodsOutId"] = value;
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
                this.GoodsOutId = Request.Params["GoodsOutId"];
                if (!string.IsNullOrEmpty(this.GoodsOutId))
                {
                    Model.InApproveManager_GoodsOut goodsOut = BLL.GoodsOutService.GetGoodsOutById(this.GoodsOutId);
                    if (goodsOut!=null)
                    {
                        this.ProjectId = goodsOut.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGoodsOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsOutId);
                        if (!string.IsNullOrEmpty(goodsOut.UnitId))
                        {
                            this.drpUnitId.SelectedValue = goodsOut.UnitId;
                        }
                        if (goodsOut.OutDate != null)
                        {
                            this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", goodsOut.OutDate);
                        }
                        if (goodsOut.OutTime != null)
                        {
                            this.txtOutTime.Text = string.Format("{0:t}", goodsOut.OutTime);
                        }
                        this.txtCarNum.Text = goodsOut.CarNum;
                        this.txtCarModel.Text = goodsOut.CarModel;
                        this.txtStartPlace.Text = goodsOut.StartPlace;
                        this.txtEndPlace.Text = goodsOut.EndPlace;
                        this.txtGoodsOutNote.Text = goodsOut.GoodsOutNote;
                    }
                }
                else
                {
                    this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtOutTime.Text = string.Format("{0:t}", DateTime.Now);
                    ////自动生成编码
                    this.txtGoodsOutCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GoodsOutMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsOutMenuId;
                this.ctlAuditFlow.DataId = this.GoodsOutId;
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
            this.drpUnitId.DataValueField = "UnitId";
            this.drpUnitId.DataTextField = "UnitName";
            this.drpUnitId.DataSource = BLL.UnitService.GetUnitByProjectIdList(this.ProjectId);
            this.drpUnitId.DataBind();
            Funs.FineUIPleaseSelect(this.drpUnitId);
        }

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
                Alert.ShowInTop("请选择单位!", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请选择单位!", MessageBoxIcon.Warning);
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
            Model.InApproveManager_GoodsOut goodsOut = new Model.InApproveManager_GoodsOut
            {
                ProjectId = this.ProjectId,
                GoodsOutCode = this.txtGoodsOutCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                goodsOut.UnitId = this.drpUnitId.SelectedValue;
            }
            goodsOut.OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim());
            goodsOut.OutTime = Funs.GetNewDateTime(this.txtOutTime.Text.Trim());
            goodsOut.CarNum = this.txtCarNum.Text.Trim();
            goodsOut.CarModel = this.txtCarModel.Text.Trim();
            goodsOut.StartPlace = this.txtStartPlace.Text.Trim();
            goodsOut.EndPlace = this.txtEndPlace.Text.Trim();
            goodsOut.GoodsOutNote = this.txtGoodsOutNote.Text.Trim();
            goodsOut.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                goodsOut.States = this.ctlAuditFlow.NextStep;
            }
            goodsOut.CompileMan = this.CurrUser.UserId;
            goodsOut.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GoodsOutId))
            {
                goodsOut.GoodsOutId = this.GoodsOutId;
                BLL.GoodsOutService.UpdateGoodsOut(goodsOut);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "修改普通货物出场报批");
            }
            else
            {
                this.GoodsOutId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GoodsOut));
                goodsOut.GoodsOutId = this.GoodsOutId;
                BLL.GoodsOutService.AddGoodsOut(goodsOut);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "添加普通货物出场报批");
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GoodsOutMenuId, this.GoodsOutId, (type == BLL.Const.BtnSubmit ? true : false), goodsOut.GoodsOutNote, "../InApproveManager/GoodsOutView.aspx?GoodsOutId={0}");
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
            if (string.IsNullOrEmpty(this.GoodsOutId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GoodsOutAttachUrl&menuId={1}", this.GoodsOutId, BLL.Const.GoodsOutMenuId)));
        }
        #endregion
    }
}