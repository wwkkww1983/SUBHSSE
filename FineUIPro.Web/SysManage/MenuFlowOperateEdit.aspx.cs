using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class MenuFlowOperateEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get
            {
                return (string)ViewState["MenuId"];
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string FlowOperateId
        {
            get
            {
                return (string)ViewState["FlowOperateId"];
            }
            set
            {
                ViewState["FlowOperateId"] = value;
            }
        }
        #endregion

        #region 加载页面
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
                this.MenuId = Request.Params["MenuId"];
                this.FlowOperateId = Request.Params["FlowOperateId"];
                this.BindDropDownBox();
                if (!string.IsNullOrEmpty(this.FlowOperateId))
                {
                    var menuFlowOperate = BLL.MenuFlowOperateService.GetMenuFlowOperateByFlowOperateId(this.FlowOperateId);
                    if (menuFlowOperate != null)
                    {
                        this.txtFlowStep.Text = menuFlowOperate.FlowStep.ToString();
                        this.txtAuditFlowName.Text = menuFlowOperate.AuditFlowName;
                        if (menuFlowOperate.IsFlowEnd != null)
                        {
                            this.IsFlowEnd.Checked = Convert.ToBoolean(menuFlowOperate.IsFlowEnd);
                        }
                        drpRoles.Value = menuFlowOperate.RoleId;
                    }
                }
                else
                {
                    int maxId = 0;
                    string str = "SELECT (ISNULL(MAX(FlowStep),0)+1) FROM Sys_MenuFlowOperate WHERE MenuId='" + this.MenuId + "'";
                    maxId = BLL.SQLHelper.GetIntValue(str);
                    this.txtFlowStep.Text = maxId.ToString();
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Sys_MenuFlowOperate newMenuFlowOperate = new Model.Sys_MenuFlowOperate
            {
                MenuId = this.MenuId,
                FlowStep = Funs.GetNewIntOrZero(this.txtFlowStep.Text),
                AuditFlowName = this.txtAuditFlowName.Text.Trim()
            };
            string[] roleIds = drpRoles.Values;
            newMenuFlowOperate.RoleId = this.ConvertRole(roleIds);
            newMenuFlowOperate.IsFlowEnd = this.IsFlowEnd.Checked;
            if (string.IsNullOrEmpty(this.FlowOperateId))
            {
                BLL.MenuFlowOperateService.AddAuditFlow(newMenuFlowOperate);
                BLL.LogService.AddSys_Log(this.CurrUser,  newMenuFlowOperate.AuditFlowName, newMenuFlowOperate.FlowOperateId,BLL.Const.SysConstSetMenuId,BLL.Const.BtnAdd);
            }
            else
            {
                newMenuFlowOperate.FlowOperateId = this.FlowOperateId;
                BLL.MenuFlowOperateService.UpdateAuditFlow(newMenuFlowOperate);
                BLL.LogService.AddSys_Log(this.CurrUser, newMenuFlowOperate.AuditFlowName, newMenuFlowOperate.FlowOperateId, BLL.Const.SysConstSetMenuId, BLL.Const.BtnModify);
            }

            ShowNotify("设置成功!", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        
        /// <summary>
        /// 加载角色下拉框
        /// </summary>
        private void BindDropDownBox()
        {
            string strSql = @"SELECT RoleId,RoleName FROM Sys_Role WHERE IsAuditFlow=1 ORDER BY RoleCode";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, null);
            rbRoles.DataSource = tb;
            this.rbRoles.DataTextField = "RoleName";
            this.rbRoles.DataValueField = "RoleId";
            rbRoles.DataBind();
        }
       
        /// <summary>
        /// 得到角色ID字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(string[] roleIds)
        {
            string roles = null;
            if (roleIds != null && roleIds.Count() > 0)
            {
                foreach (string roleId in roleIds)
                {
                    roles += roleId + ",";
                }
                if (roles != string.Empty)
                {
                    roles = roles.Substring(0, roles.Length - 1); ;
                }
            }

            return roles;
        }

        #region 是否审核结束
        /// <summary>
        /// 是否审核结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsFlowEnd_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (IsFlowEnd.Checked)
            {
                this.drpRoles.Value = null;
                this.drpRoles.Text = string.Empty;
                this.drpRoles.Hidden = true;
                if (string.IsNullOrEmpty(this.txtAuditFlowName.Text))
                {
                    this.txtAuditFlowName.Text = "完成";
                }
            }
            else
            {
                this.drpRoles.Hidden = false;
            }
        }
        #endregion
    }
}