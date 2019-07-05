using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Hazard
{
    public partial class ShowWorkStage : PageBase
    {
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
                this.chblWorkStage.DataTextField = "WorkStageName";
                this.chblWorkStage.DataValueField = "WorkStageId";
                this.chblWorkStage.DataSource = BLL.WorkStageService.GetWorkStageList();
                this.chblWorkStage.DataBind();
            }
        }
        #endregion

        #region 确定
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            string workStage = string.Empty; ;
            int count = this.chblWorkStage.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.chblWorkStage.Items[i].Selected)
                {
                    workStage += this.chblWorkStage.Items[i].Text + ",";
                }
            }
            if (workStage != "")
            {
                workStage = workStage.Substring(0, workStage.LastIndexOf(","));
                workStage += "|";
            }
            for (int i = 0; i < count; i++)
            {
                if (this.chblWorkStage.Items[i].Selected)
                {
                    workStage += this.chblWorkStage.Items[i].Value + ",";
                }
            }
            if (workStage != "")
            {
                workStage = workStage.Substring(0, workStage.LastIndexOf(","));
                Session["workStages"] = workStage;
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}