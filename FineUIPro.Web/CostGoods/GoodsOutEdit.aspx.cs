using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.CostGoods
{
    public partial class GoodsOutEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string GoodsOutId
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
                this.GoodsOutId = Request.Params["GoodsOutId"];
                if (!string.IsNullOrEmpty(this.GoodsOutId))
                {
                    Model.CostGoods_GoodsOut goodsOut = BLL.GoodsOut2Service.GetGoodsOutById(this.GoodsOutId);
                    if (goodsOut != null)
                    {
                        this.ProjectId = goodsOut.ProjectId;
                        this.txtGoodsOutCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.GoodsOutId);
                        if (!string.IsNullOrEmpty(goodsOut.GoodsDefId))
                        {
                            this.hdGoodsDefId.Text = goodsOut.GoodsDefId;
                            var goodsDef = BLL.GoodsDefService.GetGoodsDefById(this.hdGoodsDefId.Text.Trim());
                            if (goodsDef != null)
                            {
                                this.txtGoodsDefId.Text = goodsDef.GoodsDefName;
                            }
                        }
                        if (goodsOut.GoodsNum.HasValue)
                        {
                            this.txtCounts.Text = Convert.ToString(goodsOut.GoodsNum);
                        }
                        this.txtOutPerson.Text = goodsOut.OutPerson;
                        this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", goodsOut.OutDate);
                    }
                }
                else
                {
                    this.txtOutDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtGoodsOutCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.GoodsOut2MenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsOut2MenuId;
                this.ctlAuditFlow.DataId = this.GoodsOutId;
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
                var newResult = goodsDefs.Split('|');
                this.txtGoodsDefId.Text = newResult[0];
                this.hdGoodsDefId.Text = newResult[1];
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
            Model.CostGoods_GoodsOut goodsOut = new Model.CostGoods_GoodsOut
            {
                ProjectId = this.ProjectId,
                GoodsOutCode = this.txtGoodsOutCode.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.hdGoodsDefId.Text.Trim()))
            {
                goodsOut.GoodsDefId = this.hdGoodsDefId.Text.Trim();
            }
            goodsOut.GoodsNum = Funs.GetNewInt(this.txtCounts.Text.Trim());
            goodsOut.OutPerson = this.txtOutPerson.Text.Trim();
            goodsOut.OutDate = Funs.GetNewDateTime(this.txtOutDate.Text.Trim());
            goodsOut.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                goodsOut.States = this.ctlAuditFlow.NextStep;
            }
            goodsOut.CompileMan = this.CurrUser.UserId;
            goodsOut.CompileDate = DateTime.Now;
            if (!string.IsNullOrEmpty(this.GoodsOutId))
            {
                goodsOut.GoodsOutId = this.GoodsOutId;
                BLL.GoodsOut2Service.UpdateGoodsOut(goodsOut);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsOut.GoodsOutCode, goodsOut.GoodsOutId,BLL.Const.GoodsOut2MenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.GoodsOutId = SQLHelper.GetNewID(typeof(Model.CostGoods_GoodsOut));
                goodsOut.GoodsOutId = this.GoodsOutId;
                BLL.GoodsOut2Service.AddGoodsOut(goodsOut);
                BLL.LogService.AddSys_Log(this.CurrUser, goodsOut.GoodsOutCode, goodsOut.GoodsOutId, BLL.Const.GoodsOut2MenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.GoodsOut2MenuId, this.GoodsOutId, (type == BLL.Const.BtnSubmit ? true : false), goodsOut.GoodsOutCode, "../CostGoods/GoodsOutView.aspx?GoodsOutId={0}");
        }
        #endregion

        #region 验证数量
        /// <summary>
        /// 验证数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCounts.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(this.hdGoodsDefId.Text.Trim()))
                {
                    int count = BLL.GoodsDefService.GetGoodsNumByGoodsDefId(this.hdGoodsDefId.Text.Trim(), this.CurrUser.LoginProjectId);
                    if (count < Convert.ToInt32(this.txtCounts.Text.Trim()))
                    {
                        Alert.ShowInTop("出库数量不能大于库存量！", MessageBoxIcon.Warning);
                        this.txtCounts.Text = string.Empty;
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("请先选择物资名称！", MessageBoxIcon.Warning);
                    this.txtCounts.Text = string.Empty;
                    return;
                }
            }
        }
        #endregion
    }
}