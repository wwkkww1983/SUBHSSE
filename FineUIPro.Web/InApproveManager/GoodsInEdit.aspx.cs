using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GoodsInEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string GoodsInId
        {
            get
            {
                return (string)ViewState["GoodsInId"];
            }
            set
            {
                ViewState["GoodsInId"] = value;
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
                this.GoodsInId = Request.Params["GoodsInId"];
                if (!string.IsNullOrEmpty(this.GoodsInId))
                {
                    Model.InApproveManager_GoodsIn goodsIn = BLL.GoodsInService.GetGoodsInById(this.GoodsInId);
                    if (goodsIn != null)
                    {
                        this.ProjectId = goodsIn.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        this.txtGoodsInCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsInId);
                        if (!string.IsNullOrEmpty(goodsIn.UnitId))
                        {
                            this.drpUnitId.SelectedValue = goodsIn.UnitId;
                        }
                        this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", goodsIn.InDate);
                        if (goodsIn.InTime.HasValue)
                        {
                            this.txtInTime.Text = string.Format("{0:t}", goodsIn.InTime);
                        }
                        this.txtCarNum.Text = goodsIn.CarNum;
                        this.txtDriverNameAndNum.Text = goodsIn.DriverNameAndNum;
                        this.txtGoodsInResult.Text = goodsIn.GoodsInResult;
                        this.txtGoodsInNote.Text = goodsIn.GoodsInNote;
                    }
                }
                else
                {
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtInTime.Text = string.Format("{0:t}", DateTime.Now);
                    ////自动生成编码
                    this.txtGoodsInCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GoodsInMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsInMenuId;
                this.ctlAuditFlow.DataId = this.GoodsInId;
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
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请选择单位！", MessageBoxIcon.Warning);
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
            Model.InApproveManager_GoodsIn goodsIn = new Model.InApproveManager_GoodsIn
            {
                ProjectId = this.ProjectId,
                GoodsInCode = this.txtGoodsInCode.Text.Trim()
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                goodsIn.UnitId = this.drpUnitId.SelectedValue;
            }
            goodsIn.InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim());
            goodsIn.InTime = Funs.GetNewDateTime(this.txtInTime.Text.Trim());
            goodsIn.CarNum = this.txtCarNum.Text.Trim();
            goodsIn.DriverNameAndNum = this.txtDriverNameAndNum.Text.Trim();
            goodsIn.GoodsInResult = this.txtGoodsInResult.Text.Trim();
            goodsIn.GoodsInNote = this.txtGoodsInNote.Text.Trim();
            goodsIn.States = BLL.Const.State_0;
            if (type==BLL.Const.BtnSubmit)
            {
                goodsIn.States = this.ctlAuditFlow.NextStep;
            }
            goodsIn.CompileMan = this.CurrUser.UserId;
            goodsIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(GoodsInId))
            {
                goodsIn.GoodsInId = this.GoodsInId;
                BLL.GoodsInService.UpdateGoodsIn(goodsIn);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsIn.GoodsInCode, goodsIn.GoodsInId, BLL.Const.GoodsInMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GoodsInId = SQLHelper.GetNewID(typeof(Model.InApproveManager_GoodsIn));
                goodsIn.GoodsInId = this.GoodsInId;
                BLL.GoodsInService.AddGoodsIn(goodsIn);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsIn.GoodsInCode, goodsIn.GoodsInId,BLL.Const.GoodsInMenuId,BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GoodsInMenuId, this.GoodsInId, (type == BLL.Const.BtnSubmit ? true : false), goodsIn.GoodsInResult, "../InApproveManager/GoodsInView.aspx?GoodsInId={0}");
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
            if (string.IsNullOrEmpty(this.GoodsInId))
            {
                SaveData(BLL.Const.BtnSave);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GoodsInAttachUrl&menuId={1}", this.GoodsInId, BLL.Const.GoodsInMenuId)));
        }
        #endregion
    }
}