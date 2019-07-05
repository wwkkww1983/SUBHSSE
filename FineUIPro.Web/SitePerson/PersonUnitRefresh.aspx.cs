using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonUnitRefresh : PageBase
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
                //项目单位
                BLL.UnitService.InitUnitDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, false);
                if (BLL.ProjectUnitService.GetProjectUnitTypeByProjectIdUnitId(this.CurrUser.LoginProjectId, this.CurrUser.UnitId))
                {
                    this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                    this.drpUnitId.Enabled = false;
                }
                //项目单位
                BLL.UnitService.InitUnitDropDownList(this.drpToUnit, this.CurrUser.LoginProjectId, false);
                BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.CurrUser.LoginProjectId, this.drpToUnit.SelectedValue, true);
                this.BindGrid();
            }
        }

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.CurrUser.LoginProjectId))
            {
                string strSql = string.Empty;
                ///在场人员出场
                strSql = @"SELECT PersonId,CardNo,PersonName,IdentityCard"
                          + @" FROM SitePerson_Person WHERE UnitId ='" + this.drpUnitId.SelectedValue + "'";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += " AND ProjectId = @ProjectId";
                listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    strSql += " AND UnitId = @UnitId";
                    listStr.Add(new SqlParameter("@UnitId", this.drpUnitId.SelectedValue));
                }
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
        }
        #endregion

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpToUnit.SelectedValue))
            {
                foreach (var item in this.drpPerson.Values)
                {
                    var person = BLL.PersonService.GetPersonById(item);
                    if (person != null)
                    {
                        person.UnitId = this.drpToUnit.SelectedValue;
                        if (this.drpTeamGroup.SelectedValue != BLL.Const._Null)
                        {
                            person.TeamGroupId = this.drpTeamGroup.SelectedValue;
                        }
                        else
                        {
                            person.TeamGroupId = null;
                        }
                        BLL.PersonService.UpdatePerson(person);
                    }
                }

                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "人员批量调整单位！");
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            }
            else
            {
                ShowNotify("请选择人员新单位！", MessageBoxIcon.Warning);
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.CurrUser.LoginProjectId, this.drpToUnit.SelectedValue, true);
        }
    }
}