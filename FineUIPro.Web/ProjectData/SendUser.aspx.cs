using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.ProjectData
{
    public partial class SendUser : PageBase
    {
        #region 自定义变量
        /// <summary>
        /// 项目id
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                }
                BLL.ProjectService.InitProjectDropDownList(this.drpProject, true);
                this.drpProject.Items.Remove(this.drpProject.Items.FindByValue(this.ProjectId));
                this.oldProject.Text = BLL.ProjectService.GetProjectNameByProjectId(this.ProjectId);
                this.BindGrid();
            }
        }
        #endregion

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT Unit.UnitCode,Unit.UnitName,puser.UserId,users.UserName,users.IdentityCard,users.UserCode,puser.ProjectId"
                    + @" FROM Project_ProjectUser AS puser"
                    + @" LEFT JOIN Sys_User AS users ON puser.UserId=users.UserId"
                    + @" LEFT JOIN Base_Unit AS Unit ON users.UnitId = Unit.UnitId"
                    + @" WHERE puser.ProjectId ='" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND (users.UserName LIKE @Name OR users.IdentityCard LIKE @Name OR Unit.UnitName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtUserName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;          
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(this.drpProject.SelectedValue) && this.drpProject.SelectedValue != BLL.Const._Null)
            {
                this.SaveData(BLL.Const.BtnSave);
               PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            else
            {
                ShowNotify("请选择人员新项目！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            foreach (var item in this.drpUser.Values)
            {
                var user = BLL.UserService.GetUserByUserId(item);
                if (user != null)
                {                   
                    var gSendUser = BLL.ProjectSendReceiveService.GetNoSendReceiveByPersonId(item);
                    if (gSendUser == null)
                    {
                        Model.Project_SendReceive newSendReceive = new Model.Project_SendReceive
                        {
                            SendReceiveId = SQLHelper.GetNewID(typeof(Model.Project_SendReceive)),
                            UserId = user.UserId,
                            SendProjectId = this.ProjectId,
                            SendTime = System.DateTime.Now,
                            ReceiveProjectId = this.drpProject.SelectedValue,
                        };

                        BLL.ProjectSendReceiveService.AddSendReceive(newSendReceive);                       
                    }
                }
            }

            BLL.LogService.AddSys_Log(this.CurrUser, "项目用户批量转换！", null, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.drpUser.Values = null;
            this.BindGrid();
        }
        #endregion
    }
}