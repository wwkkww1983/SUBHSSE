using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using System.Data;

namespace FineUIPro.Web.Check
{
    public partial class ViolationPersonEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ViolationPersonId
        {
            get
            {
                return (string)ViewState["ViolationPersonId"];
            }
            set
            {
                ViewState["ViolationPersonId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.BindGrid(string.Empty);

                this.InitDropDownList();
                this.ViolationPersonId = Request.Params["ViolationPersonId"];
                if (!string.IsNullOrEmpty(this.ViolationPersonId))
                {
                    Model.Check_ViolationPerson violationPerson = BLL.ViolationPersonService.GetViolationPersonById(this.ViolationPersonId);
                    if (violationPerson != null)
                    {
                        this.ProjectId = violationPerson.ProjectId;
                        if (violationPerson != null)
                        {
                            this.txtViolationPersonCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ViolationPersonId);
                        }
                        if (!string.IsNullOrEmpty(violationPerson.PersonId))
                        {
                            BindGrid(violationPerson.PersonId);
                            this.drpPersonId.Value = violationPerson.PersonId;
                        }
                        if (!string.IsNullOrEmpty(violationPerson.UnitId))
                        {
                            this.hdUnitId.Text = violationPerson.UnitId;
                            if (!string.IsNullOrEmpty(this.hdUnitId.Text))
                            {
                                var unit = BLL.UnitService.GetUnitByUnitId(this.hdUnitId.Text.Trim());
                                if (unit != null)
                                {
                                    this.txtUnitName.Text = unit.UnitName;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(violationPerson.WorkPostId))
                        {
                            this.hdWorkPostId.Text = violationPerson.WorkPostId;
                            if (!string.IsNullOrEmpty(this.hdWorkPostId.Text))
                            {
                                var workPost = BLL.WorkPostService.GetWorkPostById(this.hdWorkPostId.Text);
                                if (workPost != null)
                                {
                                    this.txtWorkPostName.Text = workPost.WorkPostName;
                                }
                            }
                        }
                        if (violationPerson.ViolationDate != null)
                        {
                            this.txtViolationDate.Text = string.Format("{0:yyyy-MM-dd}", violationPerson.ViolationDate);
                        }
                        if (!string.IsNullOrEmpty(violationPerson.ViolationName))
                        {
                            this.drpViolationName.SelectedValue = violationPerson.ViolationName;
                            if (violationPerson.ViolationName == "1")
                            {
                                BLL.ConstValue.InitConstValueDropDownList(this.drpViolationType, ConstValue.Group_ViolationType, true);
                            }
                            else if (violationPerson.ViolationName == "2")
                            {
                                BLL.ConstValue.InitConstValueDropDownList(this.drpViolationType, ConstValue.Group_ViolationTypeOther, true);
                            }
                        }
                        if (!string.IsNullOrEmpty(violationPerson.ViolationType))
                        {
                            this.drpViolationType.SelectedValue = violationPerson.ViolationType.Trim();
                        }
                        if (!string.IsNullOrEmpty(violationPerson.HandleStep))
                        {
                            this.drpHandleStep.SelectedValue = violationPerson.HandleStep;
                        }
                        this.txtViolationDef.Text = violationPerson.ViolationDef;
                    }
                }
                else
                {
                    this.txtViolationDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    ////自动生成编码
                    this.txtViolationPersonCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectViolationPersonMenuId, this.ProjectId, this.CurrUser.UnitId);
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectViolationPersonMenuId;
                this.ctlAuditFlow.DataId = this.ViolationPersonId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.ConstValue.InitConstValueDropDownList(this.drpViolationName, ConstValue.Group_ViolationName, true);
            //BLL.ConstValue.InitConstValueDropDownList(this.drpViolationType, ConstValue.Group_ViolationType, true);
            BLL.ConstValue.InitConstValueDropDownList(this.drpHandleStep, BLL.ConstValue.Group_ViolationPersonHandleStep, true);
        }

        #region DropDownList下拉选择事件
        /// <summary>
        ///  人员下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpPersonId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpPersonId.Value))
            {
                var person = BLL.PersonService.GetPersonById(this.drpPersonId.Value);
                if (person != null)
                {
                    if (!string.IsNullOrEmpty(person.UnitId))
                    {
                        this.hdUnitId.Text = person.UnitId;
                        var unit = BLL.UnitService.GetUnitByUnitId(this.hdUnitId.Text);
                        if (unit != null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
                    }
                    if (!string.IsNullOrEmpty(person.WorkPostId))
                    {
                        var workPost = BLL.WorkPostService.GetWorkPostById(person.WorkPostId);
                        if (workPost != null)
                        {
                            this.txtWorkPostName.Text = workPost.WorkPostName;
                            this.hdWorkPostId.Text = person.WorkPostId;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid(string.Empty);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid(string personId)
        {
            string strSql = @"SELECT Person.PersonId,Person.CardNo,Person.PersonName,Person.IdentityCard,Person.UnitId,Person.WorkPostId,Unit.UnitName,WorkPost.WorkPostName "
                + @" FROM SitePerson_Person AS Person "
                + @" LEFT JOIN Base_Unit AS Unit ON Unit.UnitId = Person.UnitId "
                + @" LEFT JOIN Base_WorkPost AS WorkPost ON WorkPost.WorkPostId = Person.WorkPostId  WHERE ProjectId='" + this.CurrUser.LoginProjectId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                strSql += " AND Person.CardNo LIKE @CardNo";
                listStr.Add(new SqlParameter("@CardNo", "%" + this.txtCardNo.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
            {
                strSql += " AND Person.PersonName LIKE @PersonName";
                listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(this.txtSelectUnitName.Text.Trim()))
            {
                strSql += " AND Unit.UnitName LIKE @UnitName";
                listStr.Add(new SqlParameter("@UnitName", "%" + this.txtSelectUnitName.Text.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(personId))
            {
                strSql += " AND Person.PersonId = @PersonId";
                listStr.Add(new SqlParameter("@PersonId", personId));
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

        #region 保存、提交
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="p"></param>
        private void SaveData(string type)
        {
            Model.Check_ViolationPerson violationPerson = new Model.Check_ViolationPerson
            {
                ProjectId = this.ProjectId,
                ViolationPersonCode = this.txtViolationPersonCode.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.drpPersonId.Value))
            {
                violationPerson.PersonId = this.drpPersonId.Value;
            }
            if (!string.IsNullOrEmpty(this.hdUnitId.Text.Trim()))
            {
                violationPerson.UnitId = this.hdUnitId.Text.Trim();
            }
            if (!string.IsNullOrEmpty(this.hdWorkPostId.Text.Trim()))
            {
                violationPerson.WorkPostId = this.hdWorkPostId.Text.Trim();
            }
            violationPerson.ViolationDate = Funs.GetNewDateTime(this.txtViolationDate.Text.Trim());
            if (this.drpViolationName.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.drpViolationName.SelectedValue))
                {
                    violationPerson.ViolationName = this.drpViolationName.SelectedValue.Trim();
                }
            }
            if (this.drpViolationType.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.drpViolationType.SelectedValue))
                {
                    violationPerson.ViolationType = this.drpViolationType.SelectedValue.Trim();
                }
            }
            if (this.drpHandleStep.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.drpHandleStep.SelectedValue))
                {
                    violationPerson.HandleStep = this.drpHandleStep.SelectedValue.Trim();
                }
            }
            violationPerson.ViolationDef = this.txtViolationDef.Text.Trim();
            violationPerson.CompileMan = this.CurrUser.UserId;
            violationPerson.CompileDate = DateTime.Now;
            violationPerson.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                violationPerson.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ViolationPersonId))
            {
                violationPerson.ViolationPersonId = this.ViolationPersonId;
                BLL.ViolationPersonService.UpdateViolationPerson(violationPerson);
                BLL.LogService.AddSys_Log(this.CurrUser, violationPerson.ViolationPersonCode, violationPerson.ViolationPersonId,BLL.Const.ProjectViolationPersonMenuId,BLL.Const.BtnModify);
            }
            else
            {
                this.ViolationPersonId = SQLHelper.GetNewID(typeof(Model.Check_ViolationPerson));
                violationPerson.ViolationPersonId = this.ViolationPersonId;
                BLL.ViolationPersonService.AddViolationPerson(violationPerson);
                BLL.LogService.AddSys_Log(this.CurrUser, violationPerson.ViolationPersonCode, violationPerson.ViolationPersonId,BLL.Const.ProjectViolationPersonMenuId,BLL.Const.BtnAdd);                
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectViolationPersonMenuId, this.ViolationPersonId, (type == BLL.Const.BtnSubmit ? true : false), violationPerson.ViolationPersonCode, "../Check/ViolationPersonView.aspx?ViolationPersonId={0}");
        }
        #endregion

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            this.BindGrid(string.Empty);
        }

        /// <summary>
        /// 违章名称下拉框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpViolationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpViolationType.Items.Clear();
            if (this.drpViolationName.SelectedValue != BLL.Const._Null)
            {
                if (this.drpViolationName.SelectedValue == "1")
                {
                    BLL.ConstValue.InitConstValueDropDownList(this.drpViolationType, ConstValue.Group_ViolationType, true);
                }
                else
                {
                    BLL.ConstValue.InitConstValueDropDownList(this.drpViolationType, ConstValue.Group_ViolationTypeOther, true);
                }
                this.drpViolationType.SelectedValue = BLL.Const._Null;
            }
        }
    }
}