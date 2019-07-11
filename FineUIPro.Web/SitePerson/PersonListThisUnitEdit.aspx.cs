using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonListThisUnitEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }

        /// <summary>
        /// 项目ID
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

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.ProjectId = Request.Params["ProjectId"];
                this.UnitId = Request.Params["UnitId"];
                this.PersonId = Request.Params["PersonId"];
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    Model.SitePerson_Person person = BLL.PersonService.GetPersonById(this.PersonId);
                    this.ProjectId = person.ProjectId;
                    this.UnitId = person.UnitId;
                }

                this.InitDropDownList();
                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    Model.SitePerson_Person person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            InitDropDownList();
                        }

                        if (!string.IsNullOrEmpty(person.Sex))
                        {
                            this.rblSex.SelectedValue = person.Sex;
                        }
                        if (!string.IsNullOrEmpty(person.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                                this.UnitId = person.UnitId;
                            }
                        }
                        if (!string.IsNullOrEmpty(person.WorkPostId))
                        {
                            this.drpPost.SelectedValue = person.WorkPostId;
                        }
                        if (!string.IsNullOrEmpty(person.DepartId))
                        {
                            this.drpDepart.SelectedValue = person.DepartId;
                        }
                        this.txtPersonName.Text = person.PersonName;                    
                        this.txtTelephone.Text = person.Telephone;
                    }
                }
                else
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.WorkPostService.InitWorkPostDropDownList(this.drpPost, true);
            BLL.DepartService.InitDepartDropDownList(this.drpDepart, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPersonName.Text))
            {
                ShowNotify("人员姓名不能为空！", MessageBoxIcon.Warning);
                return;
            }
            Model.SitePerson_Person person = new Model.SitePerson_Person
            {
                Sex = this.rblSex.SelectedValue,
                ProjectId = this.ProjectId
            };
            if (!string.IsNullOrEmpty(this.UnitId))
            {
                person.UnitId = this.UnitId;
            }
            if (this.drpPost.SelectedValue != BLL.Const._Null)
            {
                person.WorkPostId = this.drpPost.SelectedValue;
            }
            if (this.drpDepart.SelectedValue != BLL.Const._Null)
            {
                person.DepartId = this.drpDepart.SelectedValue;
            }
            person.PersonName = this.txtPersonName.Text.Trim();
            person.Telephone = this.txtTelephone.Text.Trim();
            if (string.IsNullOrEmpty(PersonId))
            {
                if (!BLL.PersonService.IsExistPersonByUnit(this.UnitId, this.txtPersonName.Text.Trim(), this.ProjectId))
                {
                    string newKeyID = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                    person.PersonId = newKeyID;
                    BLL.PersonService.AddPerson(person);
                }
                BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var getPerson = BLL.PersonService.GetPersonById(PersonId);
                if (getPerson != null)
                {
                    getPerson.Sex = person.Sex;
                    getPerson.ProjectId = person.ProjectId;
                    getPerson.UnitId = person.UnitId;
                    getPerson.WorkPostId = person.WorkPostId;
                    getPerson.DepartId = person.DepartId;
                    getPerson.PersonName = person.PersonName;
                    getPerson.Telephone = person.Telephone;
                    BLL.PersonService.UpdatePerson(getPerson);
                    BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
                }
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}