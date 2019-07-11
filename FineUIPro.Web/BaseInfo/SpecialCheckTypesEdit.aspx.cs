using System;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class SpecialCheckTypesEdit : PageBase
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
                this.CheckItemSetId = Request.Params["checkItemSetId"];
                this.SupCheckItem = Request.Params["supCheckItem"];
                
                if (!string.IsNullOrEmpty(this.CheckItemSetId))
                {
                    var checkItemSet = BLL.CheckItemSetService.GetCheckItemSetById(this.CheckItemSetId);
                    if (checkItemSet != null)
                    {
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
                    this.chkIsEndLevel.Enabled = BLL.CheckItemSetService.IsDeleteCheckItemSet(this.CheckItemSetId);
                }
                if (this.SupCheckItem != "0")
                {
                    this.chkIsEndLevel.Checked = true;
                    this.chkIsEndLevel.Enabled = false;
                }
            }
        }
        #endregion

        #region 保存
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
                if (!BLL.CheckItemSetService.IsExistCheckItemName(this.CheckItemSetId, this.SupCheckItem, staName))
                {
                    Model.HSSE_Check_CheckItemSet checkItemSet = new Model.HSSE_Check_CheckItemSet
                    {
                        CheckItemName = staName,
                        SupCheckItem = this.SupCheckItem,
                        MapCode = this.txtMapCode.Text.Trim(),
                        SortIndex = Funs.GetNewIntOrZero(this.txtSortIndex.Text.Trim()),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked)
                    };
                    if (string.IsNullOrEmpty(this.CheckItemSetId))
                    {
                        checkItemSet.CheckItemSetId = SQLHelper.GetNewID(typeof(Model.HSSE_Check_CheckItemSet));
                        BLL.CheckItemSetService.AddCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.SpecialCheckTypesMenuId, BLL.Const.BtnAdd);
                    }
                    else
                    {
                        checkItemSet.CheckItemSetId = this.CheckItemSetId;
                        BLL.CheckItemSetService.UpdateCheckItemSet(checkItemSet);
                        BLL.LogService.AddSys_Log(this.CurrUser, checkItemSet.MapCode, checkItemSet.CheckItemSetId, BLL.Const.SpecialCheckTypesMenuId, BLL.Const.BtnModify);
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
        #endregion
    }
}