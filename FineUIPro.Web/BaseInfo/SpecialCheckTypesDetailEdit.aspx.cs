using System;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialCheckTypesDetailEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 检查明细id
        /// </summary>
        public string CheckItemDetailId
        {
            get
            {
                return (string)ViewState["CheckItemDetailId"];
            }
            set
            {
                ViewState["CheckItemDetailId"] = value;
            }
        }

        /// <summary>
        /// 检查项id
        /// </summary>
        public string CheckItemSetId
        {
            get
            {
                return (string)ViewState["CheckItemSetId"];
            }
            set
            {
                ViewState["CheckItemSetId"] = value;
            }
        }
        #endregion

        #region 加载
        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.CheckItemDetailId = Request.Params["checkItemDetailId"];
                if (!string.IsNullOrEmpty(this.CheckItemDetailId))
                {
                    var checkItemDetail = BLL.CheckItemDetailService.GetCheckItemDetailById(this.CheckItemDetailId);
                    if (checkItemDetail != null)
                    {
                        this.txtCheckContent.Text = checkItemDetail.CheckContent;
                        this.txtSortIndex.Text = checkItemDetail.SortIndex.ToString();
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCheckContent.Text))
            {
                Model.HSSE_Check_CheckItemDetail checkItemDetail = new Model.HSSE_Check_CheckItemDetail
                {
                    CheckItemSetId = this.CheckItemSetId,
                    CheckContent = this.txtCheckContent.Text.Trim(),
                    SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim())
                };

                if (string.IsNullOrEmpty(this.CheckItemDetailId))
                {
                    checkItemDetail.CheckItemDetailId = SQLHelper.GetNewID(typeof(Model.HSSE_Check_CheckItemDetail));
                    BLL.CheckItemDetailService.AddCheckItemDetail(checkItemDetail);
                    BLL.LogService.AddSys_Log(this.CurrUser, checkItemDetail.SortIndex.ToString(), checkItemDetail.CheckItemDetailId, BLL.Const.SpecialCheckTypesMenuId, BLL.Const.BtnAdd);
                }
                else
                {
                    checkItemDetail.CheckItemDetailId = this.CheckItemDetailId;
                    BLL.CheckItemDetailService.UpdateCheckItemDetail(checkItemDetail);
                    BLL.LogService.AddSys_Log(this.CurrUser, checkItemDetail.SortIndex.ToString(), checkItemDetail.CheckItemDetailId, BLL.Const.SpecialCheckTypesMenuId, BLL.Const.BtnModify);
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("检查项内容不能为空！");
            }
        }
        #endregion
    }
}