using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.InApproveManager
{
    public partial class GoodsInView : PageBase
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

                this.GoodsInId = Request.Params["GoodsInId"];
                if (!string.IsNullOrEmpty(this.GoodsInId))
                {
                    Model.InApproveManager_GoodsIn goodsIn = BLL.GoodsInService.GetGoodsInById(this.GoodsInId);
                    if (goodsIn != null)
                    {
                        this.txtGoodsInCode.Text = CodeRecordsService.ReturnCodeByDataId(this.GoodsInId);
                        if (!string.IsNullOrEmpty(goodsIn.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(goodsIn.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                            }
                        }
                        if (goodsIn.InDate != null && goodsIn.InTime != null)
                        {
                            string inDate = string.Format("{0:yyyy-MM-dd}", goodsIn.InDate);
                            string inTime = string.Format("{0:t}", goodsIn.InTime);
                            this.txtInTime.Text = inDate + " " + inTime;
                        }
                        this.txtCarNum.Text = goodsIn.CarNum;
                        this.txtDriverNameAndNum.Text = goodsIn.DriverNameAndNum;
                        this.txtGoodsInResult.Text = goodsIn.GoodsInResult;
                        this.txtGoodsInNote.Text = goodsIn.GoodsInNote;
                    }
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.GoodsInMenuId;
                this.ctlAuditFlow.DataId = this.GoodsInId;
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
            if (!string.IsNullOrEmpty(this.GoodsInId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/GoodsInAttachUrl&menuId={1}&type=-1", this.GoodsInId, BLL.Const.GoodsInMenuId)));
            }
        }
        #endregion
    }
}