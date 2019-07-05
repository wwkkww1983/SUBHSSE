using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.ProjectData
{
    public partial class TeamGroupView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TeamGroupId
        {
            get
            {
                return (string)ViewState["TeamGroupId"];
            }
            set
            {
                ViewState["TeamGroupId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.TeamGroupId = Request.Params["TeamGroupId"];
                if (!string.IsNullOrEmpty(this.TeamGroupId))
                {
                    Model.ProjectData_TeamGroup teamGroup = BLL.TeamGroupService.GetTeamGroupById(this.TeamGroupId);
                    if (teamGroup != null)
                    {
                        this.txtTeamGroupCode.Text = teamGroup.TeamGroupCode;
                        this.txtTeamGroupName.Text = teamGroup.TeamGroupName;
                        var unit = BLL.UnitService.GetUnitByUnitId(teamGroup.UnitId);
                        if (unit != null)
                        {
                            this.drpUnitId.Text = unit.UnitName;
                        }
                        this.txtRemark.Text = teamGroup.Remark;
                    }
                }
            }
        }
        #endregion
    }
}