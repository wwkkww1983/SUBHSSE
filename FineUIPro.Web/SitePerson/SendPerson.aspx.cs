using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.SitePerson
{
    public partial class SendPerson : PageBase
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
            string strSql = @"SELECT Unit.UnitCode,Unit.UnitName,Person.PersonId,Person.CardNo,Person.PersonName,Person.IdentityCard,Person.ProjectId"
                    + @" FROM SitePerson_Person AS Person"
                    + @" LEFT JOIN Base_Unit AS Unit ON Person.UnitId = Unit.UnitId"
                    + @" WHERE Person.IsUsed =1 AND Person.ProjectId ='" + this.ProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND (PersonName LIKE @Name OR IdentityCard LIKE @Name OR UnitName LIKE @Name OR CardNo LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtPersonName.Text.Trim() + "%"));
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
            foreach (var item in this.drpPerson.Values)
            {
                var person = BLL.PersonService.GetPersonById(item);
                if (person != null)
                {
                    person.OutTime = System.DateTime.Now;
                    person.IsUsed = false;
                    BLL.PersonService.UpdatePerson(person); ///在原来项目上出场
                    var gSendPerson = BLL.SendReceiveService.GetNoSendReceiveByPersonId(item);
                    if (gSendPerson == null)
                    {
                        Model.SitePerson_SendReceive newSendReceive = new Model.SitePerson_SendReceive
                        {
                            SendReceiveId = SQLHelper.GetNewID(typeof(Model.SitePerson_SendReceive)),
                            PersonId = person.PersonId,
                            SendProjectId = this.ProjectId
                        };
                        newSendReceive.ReceiveProjectId = this.drpProject.SelectedValue;
                        newSendReceive.SendTime = System.DateTime.Now;
                        BLL.SendReceiveService.AddSendReceive(newSendReceive);                       
                    }
                }
            }

            BLL.LogService.AddSys_Log(this.CurrUser, "人员批量项目转换！", null, BLL.Const.PersonInfoMenuId, BLL.Const.BtnModify);
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.drpPerson.Values = null;
            this.BindGrid();
        }
        #endregion
    }
}