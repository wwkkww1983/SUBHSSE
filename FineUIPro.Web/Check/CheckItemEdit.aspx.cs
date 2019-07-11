using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckItemEdit : PageBase
    {
        #region 定义项

        /// <summary>
        /// 上级检查项
        /// </summary>
        public string SupCheckItem
        {
            get
            {
                return (string)ViewState["SupCheckItem"];
            }
            set
            {
                ViewState["SupCheckItem"] = value;
            }
        }

        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckItemSetId
        {
            get
            {
                return (string)ViewState["CheckItemSetId"];
            }
            set
            {
                ViewState["CheckItemSetId"] = value;
            }
        }
        /// <summary>
        /// 检查项类型
        /// </summary>
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

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

        /// <summary>
        /// 角色编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.SupCheckItem = Request.Params["supCheckItem"];
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(this.CheckItemSetId))
                {
                    var checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(this.CheckItemSetId);
                    if (checkItemSet != null)
                    {
                        this.ProjectId = checkItemSet.ProjectId;
                        this.Type = checkItemSet.CheckType;
                        this.txtCheckItemName.Text = checkItemSet.CheckItemName;
                        if (checkItemSet.IsEndLever == true)
                        {
                            this.chkIsEndLevel.Checked = true;
                        }
                        else
                        {
                            chkIsEndLevel.Checked = false;
                        }
                        this.txtMapCode.Text = checkItemSet.MapCode;
                        this.txtSortIndex.Text = checkItemSet.SortIndex.ToString();
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    this.chkIsEndLevel.Enabled = BLL.Check_ProjectCheckItemSetService.IsDeleteCheckItemSet(this.CheckItemSetId);
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string staName = this.txtCheckItemName.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.Check_ProjectCheckItemSetService.IsExistCheckItemName(this.ProjectId, this.Type,this.CheckItemSetId, this.SupCheckItem, staName))
                {
                    Model.Check_ProjectCheckItemSet checkItemSet = new Model.Check_ProjectCheckItemSet
                    {
                        CheckItemName = staName,
                        SupCheckItem = this.SupCheckItem,
                        MapCode = this.txtMapCode.Text.Trim(),
                        SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim()),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked),
                        CheckType = Request.Params["checkType"]
                    };
                    if (string.IsNullOrEmpty(this.CheckItemSetId))
                    {
                        checkItemSet.CheckItemSetId = SQLHelper.GetNewID(typeof(Model.Check_ProjectCheckItemSet));
                        checkItemSet.ProjectId = this.CurrUser.LoginProjectId;
                        BLL.Check_ProjectCheckItemSetService.AddCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnAdd);
                    }
                    else
                    {
                        checkItemSet.CheckItemSetId = this.CheckItemSetId;
                        BLL.Check_ProjectCheckItemSetService.UpdateCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.ProjectCheckItemSetMenuId, BLL.Const.BtnModify);
                    }
                }
                else
                {
                    Alert.ShowInTop("检查项目名称已存在！", MessageBoxIcon.Warning);
                    return;
                }

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInTop("检查项目名称不能为空！", MessageBoxIcon.Warning);
                return;
            }
        }
    }
}