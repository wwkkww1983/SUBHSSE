using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonChangeEdit : PageBase
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                //项目施工单位
                BLL.ProjectUnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, BLL.Const.ProjectUnitType_2, false);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                this.txtChangeTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.BindGrid();
            }
        }

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = string.Empty;
            if (this.rbIsIn.SelectedValue == "True") /// 离场人员再次入场
            {
                strSql = @"SELECT PersonId,CardNo,PersonName,IdentityCard"                     
                        + @" FROM SitePerson_Person WHERE (OutTime IS NOT NULL AND OutTime<= GETDATE())";
            }
            else       ///在场人员出场
            {
                strSql = @"SELECT PersonId,CardNo,PersonName,IdentityCard"
                          + @" FROM SitePerson_Person WHERE (OutTime IS  NULL OR OutTime>= GETDATE())";
            }

            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND ProjectId = @ProjectId";
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));

            strSql += " AND UnitId = @UnitId";
            listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue));

            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
            {
                strSql += " AND IdentityCard LIKE @IdentityCard";
                listStr.Add(new SqlParameter("@IdentityCard", "%" + this.txtIdentityCard.Text.Trim() + "%"));
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (var item in this.drpPerson.Values)
            {
                var person = BLL.PersonService.GetPersonById(item);
                if (person != null)
                {
                    if (this.rbIsIn.SelectedValue == "True")
                    {
                        person.InTime = Funs.GetNewDateTime(this.txtChangeTime.Text);
                        person.OutTime = null;
                    }
                    else
                    {
                        person.OutTime = Funs.GetNewDateTime(this.txtChangeTime.Text);
                    }

                    BLL.PersonService.UpdatePerson(person);
                }
            }

            BLL.LogService.AddSys_Log(this.CurrUser, "添加人员出入场", null, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion 
    }
}