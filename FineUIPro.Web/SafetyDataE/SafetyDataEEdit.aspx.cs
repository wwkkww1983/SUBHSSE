using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyDataE
{
    public partial class SafetyDataEEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级企业安全管理资料项
        /// </summary>
        public string SupSafetyDataEId
        {
            get
            {
                return (string)ViewState["SupSafetyDataEId"];
            }
            set
            {
                ViewState["SupSafetyDataEId"] = value;
            }
        }

        /// <summary>
        /// 企业安全管理资料项
        /// </summary>
        public string SafetyDataEId
        {
            get
            {
                return (string)ViewState["SafetyDataEId"];
            }
            set
            {
                ViewState["SafetyDataEId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 企业安全管理资料编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafetyDataEId = Request.Params["SafetyDataEId"];
                this.SupSafetyDataEId = Request.Params["SupSafetyDataEId"];
                ///上级节点集合
                List<Model.SafetyDataE_SafetyDataE> SafetyDataEMenu = new List<Model.SafetyDataE_SafetyDataE>();
                if (!string.IsNullOrEmpty(this.SafetyDataEId))
                {
                    SafetyDataEMenu = (from x in Funs.DB.SafetyDataE_SafetyDataE 
                                      where (x.IsEndLever == null || x.IsEndLever == false) && x.SafetyDataEId != this.SafetyDataEId  && x.SupSafetyDataEId != this.SafetyDataEId
                                      select x ).ToList();  
                }
                else
                {
                    SafetyDataEMenu = (from x in Funs.DB.SafetyDataE_SafetyDataE where x.IsEndLever == null || x.IsEndLever == false select x).ToList();
                }
                this.InitTreeSupMenu(SafetyDataEMenu);

                if (!string.IsNullOrEmpty(this.SafetyDataEId))
                {
                    var SafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.SafetyDataEId);
                    if (SafetyDataE != null)
                    {
                        this.SupSafetyDataEId = SafetyDataE.SupSafetyDataEId;
                        this.txtTitle.Text = SafetyDataE.Title;
                        if (SafetyDataE.IsEndLever.HasValue)
                        {
                            this.chkIsEndLevel.Checked = SafetyDataE.IsEndLever.Value;
                        }
                        if (SafetyDataE.IsCheck.HasValue)
                        {
                            this.chkIsCheck.Checked = SafetyDataE.IsCheck.Value;
                        }
                        if (SafetyDataE.IsEndLever == true && SafetyDataE.IsCheck == true)
                        {
                            this.rowScore.Hidden = false;
                        }
                        else
                        {                           
                            this.rowScore.Hidden = true;
                        }

                        this.txtCode.Text = SafetyDataE.Code;
                        this.txtScore.Text = SafetyDataE.Score.ToString();
                        this.txtDigit.Text = SafetyDataE.Digit.ToString();                        
                        this.txtRemark.Text = SafetyDataE.Remark;                        
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    bool isEnabled = BLL.SafetyDataEService.IsDeleteSafetyDataE(this.SafetyDataEId);
                    this.chkIsEndLevel.Enabled = isEnabled;                    
                }
                else
                {
                    this.txtDigit.Text = "3";
                    var supSafetyDataE = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(this.SupSafetyDataEId);
                    if (supSafetyDataE != null)
                    {
                        this.txtCode.Text = supSafetyDataE.Code + "-";
                    }
                }
                this.drpSupMenu.Value = this.SupSafetyDataEId;
                //this.SetCheckTypeChange(); ///考核类型变化
            }
        }              

        #region 加载上级节点菜单下拉框树
        /// <summary>
        /// 加载上级节点树
        /// </summary>
        /// <param name="menusList"></param>
        private void InitTreeSupMenu(List<Model.SafetyDataE_SafetyDataE> menusList)
        {
            TreeNode newNode = new TreeNode
            {
                Text = "企业安全管理资料",
                NodeID = "0"
            };
            this.treeSupMenu.Nodes.Add(newNode);
            this.InitTreeSupMenu(menusList, newNode);
        }

        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeSupMenu(List<Model.SafetyDataE_SafetyDataE> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            {
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SupSafetyDataEId == supMenu).OrderBy(x => x.Code);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item.Title,
                        NodeID = item.SafetyDataEId
                    };
                    if (node == null)
                    {
                        this.treeSupMenu.Nodes.Add(newNode);
                    }
                    else
                    {
                        node.Nodes.Add(newNode);
                    }

                    var supSafe = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SupSafetyDataEId == item.SafetyDataEId && (x.IsEndLever == null || x.IsEndLever == false));
                    if (supSafe != null)
                    {
                        InitTreeSupMenu(menusList, newNode);
                    }
                }
            }
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
            if(string.IsNullOrEmpty(this.drpSupMenu.Value))
            {
                Alert.ShowInParent("上级节点不能为空！");
                return;
            }
            string staName = this.txtTitle.Text.Trim();
            if (!string.IsNullOrEmpty(staName))
            {
                if (!BLL.SafetyDataEService.IsExistTitle(this.SafetyDataEId, this.SupSafetyDataEId, staName))
                {
                    Model.SafetyDataE_SafetyDataE newSafetyDataE = new Model.SafetyDataE_SafetyDataE
                    {
                        Title = staName,
                        SupSafetyDataEId = this.drpSupMenu.Value,
                        Code = this.txtCode.Text.Trim(),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked),
                        IsCheck = Convert.ToBoolean(this.chkIsCheck.Checked)
                    };
                    if (newSafetyDataE.IsEndLever == true && newSafetyDataE.IsCheck == true)
                    {
                        newSafetyDataE.Score = Funs.GetNewDecimal(this.txtScore.Text);
                        newSafetyDataE.Digit = Funs.GetNewInt(this.txtDigit.Text);
                        
                    }

                    newSafetyDataE.Remark = this.txtRemark.Text.Trim();
                
                    if (string.IsNullOrEmpty(this.SafetyDataEId))
                    {
                        newSafetyDataE.SafetyDataEId = SQLHelper.GetNewID(typeof(Model.SafetyDataE_SafetyDataE));
                        BLL.SafetyDataEService.AddSafetyDataE(newSafetyDataE);
                    }
                    else
                    {
                        newSafetyDataE.SafetyDataEId = this.SafetyDataEId;
                        BLL.SafetyDataEService.UpdateSafetyDataE(newSafetyDataE);
                    }
                    
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInParent("企业安全管理资料名称已存在！", MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                Alert.ShowInParent("企业安全管理资料名称不能为空！");
                return;
            }
        }
        #endregion

        #region 末级、考核类型下拉框变化事件
        /// <summary>
        /// 末级变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkIsEndLevel_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (this.chkIsEndLevel.Checked && this.chkIsCheck.Checked)
            {
                this.rowScore.Hidden = false;   
            }
            else
            {
                this.rowScore.Hidden = true;
            }
        }
        #endregion

        #region 清空
        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSupMenu_ClearIconClick(object sender, EventArgs e)
        {
            this.drpSupMenu.Value = string.Empty;
        }
        #endregion        
    }
}