using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GoodsOutView : PageBase
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
                this.GoodsOutId = Request.Params["GoodsOutId"];
                if (!string.IsNullOrEmpty(this.GoodsOutId))
                {
                    Model.InApproveManager_GoodsOut goodsOut = BLL.GoodsOutService.GetGoodsOutById(this.GoodsOutId);
                    if (goodsOut != null)
                    {
                        this.txtGoodsOutCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsOutId);
                        if (!string.IsNullOrEmpty(goodsOut.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(goodsOut.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (goodsOut.OutDate != null && goodsOut.OutTime != null)
                        {
                            string outDate = string.Format("{0:yyyy-MM-dd}", goodsOut.OutDate);
                            string outTime = string.Format("{0:t}", goodsOut.OutTime);
                            this.txtOutTime.Text = outDate + " " + outTime;
                        }
                        this.txtCarNum.Text = goodsOut.CarNum;
                        this.txtCarModel.Text = goodsOut.CarModel;
                        this.txtStartPlace.Text = goodsOut.StartPlace;
                        this.txtEndPlace.Text = goodsOut.EndPlace;
                        this.txtGoodsOutNote.Text = goodsOut.GoodsOutNote;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsOutMenuId;
                this.ctlAuditFlow.DataId = this.GoodsOutId;
            }
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
            if (!string.IsNullOrEmpty(this.GoodsOutId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GoodsOutAttachUrl&menuId={1}&type=-1", this.GoodsOutId, BLL.Const.GoodsOutMenuId)));
            }
        }
        #endregion
    }
}