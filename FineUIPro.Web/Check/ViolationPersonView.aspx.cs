using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class ViolationPersonView : PageBase
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

                this.ViolationPersonId = Request.Params["ViolationPersonId"];
                if (!string.IsNullOrEmpty(this.ViolationPersonId))
                {
                    Model.Check_ViolationPerson violationPerson = BLL.ViolationPersonService.GetViolationPersonById(this.ViolationPersonId);
                    if (violationPerson != null)
                    {
                        this.txtViolationPersonCode.Text = CodeRecordsService.ReturnCodeByDataId(this.ViolationPersonId);
                    }
                    if (!string.IsNullOrEmpty(violationPerson.PersonId))
                    {
                        var person = BLL.PersonService.GetPersonById(violationPerson.PersonId);
                        if (person!=null)
                        {
                            this.txtPersonName.Text = person.PersonName;
                            var workPost = BLL.WorkPostService.GetWorkPostById(person.WorkPostId);
                            if (workPost != null)
                            {
                                this.txtWorkPostName.Text = workPost.WorkPostName;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(violationPerson.UnitId))
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(violationPerson.UnitId);
                        if (unit != null)
                        {
                            this.txtUnitName.Text = unit.UnitName;
                        }
                    }
                    if (violationPerson.ViolationDate != null)
                    {
                        this.txtViolationDate.Text = string.Format("{0:yyyy-MM-dd}", violationPerson.ViolationDate);
                    }
                    if (!string.IsNullOrEmpty(violationPerson.ViolationName))
                    {
                        var c = BLL.ConstValue.GetConstByConstValueAndGroupId(violationPerson.ViolationName, BLL.ConstValue.Group_ViolationName);
                        if (c!=null)
                        {
                            this.txtViolationName.Text = c.ConstText;
                        }
                    }
                    if (!string.IsNullOrEmpty(violationPerson.ViolationType))
                    {
                        var v = BLL.ConstValue.GetConstByConstValueAndGroupId(violationPerson.ViolationType, BLL.ConstValue.Group_ViolationType);
                        var v2 = BLL.ConstValue.GetConstByConstValueAndGroupId(violationPerson.ViolationType, BLL.ConstValue.Group_ViolationTypeOther);
                        if (v!=null)
                        {
                            this.txtViolationType.Text = v.ConstText;
                        }
                        if (v2 != null)
                        {
                            this.txtViolationType.Text = v2.ConstText;
                        }
                    }
                    if (!string.IsNullOrEmpty(violationPerson.HandleStep))
                    {
                        var v = BLL.ConstValue.GetConstByConstValueAndGroupId(violationPerson.HandleStep, BLL.ConstValue.Group_ViolationPersonHandleStep);
                        if (v != null)
                        {
                            this.txtHandleStep.Text = v.ConstText;
                        }
                    }
                    this.txtViolationDef.Text = violationPerson.ViolationDef;
                }
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectViolationPersonMenuId;
                this.ctlAuditFlow.DataId = this.ViolationPersonId;
            }
        }
        #endregion
    }
}