using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Technique
{
    public partial class CheckItemDetailEdit : PageBase
    {
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

        /// <summary>
        ///  加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.CheckItemDetailId = Request.Params["checkItemDetailId"];
                if (!string.IsNullOrEmpty(this.CheckItemDetailId))
                {
                    var checkItemDetail = BLL.Technique_CheckItemDetailService.GetCheckItemDetailById(this.CheckItemDetailId);
                    if (checkItemDetail != null)
                    {
                        this.txtCheckContent.Text = checkItemDetail.CheckContent;
                        this.txtSortIndex.Text = checkItemDetail.SortIndex.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCheckContent.Text))
            {
                Model.Technique_CheckItemDetail checkItemDetail = new Model.Technique_CheckItemDetail
                {
                    CheckItemSetId = this.CheckItemSetId,
                    CheckContent = this.txtCheckContent.Text.Trim(),
                    SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim())
                };


                if (string.IsNullOrEmpty(this.CheckItemDetailId))
                {
                    checkItemDetail.CheckItemDetailId = SQLHelper.GetNewID(typeof(Model.Technique_CheckItemDetail));
                    BLL.Technique_CheckItemDetailService.AddCheckItemDetail(checkItemDetail);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "增加检查项明细表信息");
                }
                else
                {
                    checkItemDetail.CheckItemDetailId = this.CheckItemDetailId;
                    BLL.Technique_CheckItemDetailService.UpdateCheckItemDetail(checkItemDetail);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改检查项明细表信息");
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("检查项内容不能为空！");
            }
        }
    }
}