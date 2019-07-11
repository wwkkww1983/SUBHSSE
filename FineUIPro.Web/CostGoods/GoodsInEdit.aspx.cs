using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsInEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsInId
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
                this.GoodsInId = Request.Params["GoodsInId"];
                if (!string.IsNullOrEmpty(this.GoodsInId))
                {
                    Model.CostGoods_GoodsIn goodsIn = BLL.GoodsIn2Service.GetGoodsInById(this.GoodsInId);
                    if (goodsIn != null)
                    {
                        this.ProjectId = goodsIn.ProjectId;
                        this.txtGoodsInCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.GoodsInId);
                        if (!string.IsNullOrEmpty(goodsIn.GoodsDefId))
                        {
                            this.hdGoodsDefId.Text = goodsIn.GoodsDefId;
                            var goodsDef = BLL.GoodsDefService.GetGoodsDefById(this.hdGoodsDefId.Text.Trim());
                            if (goodsDef != null)
                            {
                                this.txtGoodsDefId.Text = goodsDef.GoodsDefName;
                            }
                        }
                        if (goodsIn.GoodsNum != null)
                        {
                            this.txtCounts.Text = Convert.ToString(goodsIn.GoodsNum);
                        }
                        this.txtInPerson.Text = goodsIn.InPerson;
                        this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", goodsIn.InDate);
                    }
                }
                else
                {
                    this.txtInDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtGoodsInCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GoodsIn2MenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsIn2MenuId;
                this.ctlAuditFlow.DataId = this.GoodsInId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        #region 查找物资类别
        /// <summary>
        /// 查找物资类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchGoodsDef_Click(object sender, EventArgs e)
        {
            Session["goodsDefs"] = null;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../ShowDialog/ShowGoodsDef.aspx", "物资类别 - ")));
        }

        /// <summary>
        /// 关闭弹出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            if (Session["goodsDefs"] != null)
            {
                string goodsDefs = Session["goodsDefs"].ToString();
                if (!string.IsNullOrEmpty(goodsDefs))
                {
                    var newResult = goodsDefs.Split('|');
                    this.txtGoodsDefId.Text = newResult[0];
                    this.hdGoodsDefId.Text = newResult[1];
                }
            }
        }
        #endregion

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
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
            Model.CostGoods_GoodsIn goodsIn = new Model.CostGoods_GoodsIn
            {
                ProjectId = this.ProjectId,
                GoodsInCode = this.txtGoodsInCode.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.hdGoodsDefId.Text.Trim()))
            {
                goodsIn.GoodsDefId = this.hdGoodsDefId.Text.Trim();
            }
            goodsIn.GoodsNum = Funs.GetNewInt(this.txtCounts.Text.Trim());
            goodsIn.InPerson = this.txtInPerson.Text.Trim();
            goodsIn.InDate = Funs.GetNewDateTime(this.txtInDate.Text.Trim());
            goodsIn.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                goodsIn.States = this.ctlAuditFlow.NextStep;
            }
            goodsIn.CompileMan = this.CurrUser.UserId;
            goodsIn.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GoodsInId))
            {
                goodsIn.GoodsInId = this.GoodsInId;
                BLL.GoodsIn2Service.UpdateGoodsIn(goodsIn);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsIn.GoodsInCode, goodsIn.GoodsInId, BLL.Const.GoodsIn2MenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.GoodsInId = SQLHelper.GetNewID(typeof(Model.CostGoods_GoodsIn));
                goodsIn.GoodsInId = this.GoodsInId;
                BLL.GoodsIn2Service.AddGoodsIn(goodsIn);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsIn.GoodsInCode, goodsIn.GoodsInId, BLL.Const.GoodsIn2MenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GoodsIn2MenuId, this.GoodsInId, (type == BLL.Const.BtnSubmit ? true : false), goodsIn.GoodsInCode, "../CostGoods/GoodsInView.aspx?GoodsInId={0}");
        }
        #endregion

    }
}