using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.SafetyData
{
    public partial class SafetyDataEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 上级企业安全管理资料项
        /// </summary>
        public string SupSafetyDataId
        {
            get
            {
                return (string)ViewState["SupSafetyDataId"];
            }
            set
            {
                ViewState["SupSafetyDataId"] = value;
            }
        }

        /// <summary>
        /// 企业安全管理资料项
        /// </summary>
        public string SafetyDataId
        {
            get
            {
                return (string)ViewState["SafetyDataId"];
            }
            set
            {
                ViewState["SafetyDataId"] = value;
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
                this.SafetyDataId = Request.Params["SafetyDataId"];
                this.SupSafetyDataId = Request.Params["SupSafetyDataId"];
                //报表考核类型            
                BLL.ConstValue.InitConstValueDropDownList(this.drpCheckType, ConstValue.Group_SafetyDataCheckType, false);
                ////菜单集合
                var sysMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(BLL.Const.Menu_Project);
                if (sysMenu.Count() > 0)
                {
                    this.InitTreeMenu(sysMenu, null);
                }
                ///上级节点集合
                List<Model.SafetyData_SafetyData> safetyDataMenu = new List<Model.SafetyData_SafetyData>();
                if (!string.IsNullOrEmpty(this.SafetyDataId))
                {
                    safetyDataMenu = (from x in Funs.DB.SafetyData_SafetyData 
                                      where (x.IsEndLever == null || x.IsEndLever == false) && x.SafetyDataId != this.SafetyDataId  && x.SupSafetyDataId != this.SafetyDataId
                                      select x ).ToList();  
                }
                else
                {
                    safetyDataMenu = (from x in Funs.DB.SafetyData_SafetyData where x.IsEndLever == null || x.IsEndLever == false select x).ToList();
                }
                this.InitTreeSupMenu(safetyDataMenu);

                if (!string.IsNullOrEmpty(this.SafetyDataId))
                {
                    var safetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.SafetyDataId);
                    if (safetyData != null)
                    {
                        this.SupSafetyDataId = safetyData.SupSafetyDataId;
                        this.txtTitle.Text = safetyData.Title;
                        if (safetyData.IsEndLever.HasValue)
                        {
                            this.chkIsEndLevel.Checked = safetyData.IsEndLever.Value;
                        }
                        if (safetyData.IsCheck.HasValue)
                        {
                            this.chkIsCheck.Checked = safetyData.IsCheck.Value;
                        }
                        if (safetyData.IsEndLever == true && safetyData.IsCheck == true)
                        {
                            this.rowScore.Hidden = false;
                           // this.rowMenu.Hidden = false;
                            this.rowCheckType.Hidden = false;
                        }
                        else
                        {                           
                            this.rowScore.Hidden = true;
                            //this.rowMenu.Hidden = true;
                            this.rowCheckType.Hidden = true;
                        }
                        this.rowMenu.Hidden = !this.chkIsEndLevel.Checked;
                        this.txtCode.Text = safetyData.Code;
                        this.txtScore.Text = safetyData.Score.ToString();
                        this.txtDigit.Text = safetyData.Digit.ToString();
                        this.drpMenu.Value = safetyData.MenuId;
                        
                        this.txtRemark.Text = safetyData.Remark;
                        if (!string.IsNullOrEmpty(safetyData.CheckType))
                        {
                            this.drpCheckType.SelectedValue = safetyData.CheckType;
                            if (safetyData.CheckTypeValue1.HasValue)
                            {
                                this.txtCheckTypeValue1.Text = safetyData.CheckTypeValue1.ToString();
                            }
                            if (safetyData.CheckTypeValue2.HasValue)
                            {
                                this.txtCheckTypeValue2.Text = safetyData.CheckTypeValue2.ToString();
                            }
                        }
                    }
                    // 是末级存在明细 或者 不是末级存在下级 不修改是否末级菜单
                    bool isEnabled = BLL.SafetyDataService.IsDeleteSafetyData(this.SafetyDataId);
                    this.chkIsEndLevel.Enabled = isEnabled;                    
                }
                else
                {
                    this.txtDigit.Text = "3";
                    var supSafetyData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(this.SupSafetyDataId);
                    if (supSafetyData != null)
                    {
                        this.txtCode.Text = supSafetyData.Code + "-";
                    }
                }
                this.drpSupMenu.Value = this.SupSafetyDataId;
                this.SetCheckTypeChange(); ///考核类型变化
            }
        }

        #region 加载菜单下拉框树
        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeMenu(List<Model.Sys_Menu> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            {
                supMenu = node.NodeID;
            }

            var menuItemList = menusList.Where(x => x.SuperMenu == supMenu).OrderBy(x => x.SortIndex);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    var noMenu = BLL.Const.noSysSetMenusList.FirstOrDefault(x => x == item.MenuId);
                    if (noMenu == null)
                    {
                        //var safeDataMenu = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == item.MenuId);
                        //if (!string.IsNullOrEmpty(this.SafetyDataId))
                        //{
                        //    safeDataMenu = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == item.MenuId && x.SafetyDataId != this.SafetyDataId);
                        //}

                        //if (safeDataMenu == null)
                        //{

                        //}
                        TreeNode newNode = new TreeNode
                        {
                            Text = item.MenuName,
                            NodeID = item.MenuId
                        };
                        if (item.IsEnd == true)
                        {
                            newNode.Selectable = true;
                        }
                        else
                        {
                            newNode.Selectable = false;
                        }
                        if (node == null)
                        {
                            this.treeMenu.Nodes.Add(newNode);
                        }
                        else
                        {
                            node.Nodes.Add(newNode);
                        }
                        if (!item.IsEnd.HasValue || item.IsEnd == false)
                        {
                            InitTreeMenu(menusList, newNode);
                        }
                    }
                }
            }
        }
        #endregion

        #region 加载上级节点菜单下拉框树
        /// <summary>
        /// 加载上级节点树
        /// </summary>
        /// <param name="menusList"></param>
        private void InitTreeSupMenu(List<Model.SafetyData_SafetyData> menusList)
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
        private void InitTreeSupMenu(List<Model.SafetyData_SafetyData> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            {
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SupSafetyDataId == supMenu).OrderBy(x => x.Code);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    var safeDataMenu = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == item.MenuId);                  
                    if (safeDataMenu == null)
                    {
                        TreeNode newNode = new TreeNode
                        {
                            Text = item.Title,
                            NodeID = item.SafetyDataId
                        };
                        if (node == null)
                        {
                            this.treeSupMenu.Nodes.Add(newNode);
                        }
                        else
                        {
                            node.Nodes.Add(newNode);
                        }

                        var supSafe = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SupSafetyDataId == item.SafetyDataId && x.IsEndLever == true);
                        if (supSafe != null)
                        {
                            InitTreeSupMenu(menusList, newNode);
                        }
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
                if (!BLL.SafetyDataService.IsExistTitle(this.SafetyDataId, this.SupSafetyDataId, staName))
                {
                    Model.SafetyData_SafetyData newSafetyData = new Model.SafetyData_SafetyData
                    {
                        Title = staName,
                        SupSafetyDataId = this.drpSupMenu.Value,
                        Code = this.txtCode.Text.Trim(),
                        IsEndLever = Convert.ToBoolean(this.chkIsEndLevel.Checked),
                        IsCheck = Convert.ToBoolean(this.chkIsCheck.Checked)
                    };
                    if (newSafetyData.IsEndLever == true && newSafetyData.IsCheck == true)
                    {
                        newSafetyData.Score = Funs.GetNewDecimal(this.txtScore.Text);
                        newSafetyData.Digit = Funs.GetNewInt(this.txtDigit.Text);
                        
                    }

                    if (newSafetyData.IsEndLever == true)
                    {
                        newSafetyData.MenuId = this.drpMenu.Value;
                    }
                    else
                    {
                        newSafetyData.MenuId = null;
                    }

                    newSafetyData.Remark = this.txtRemark.Text.Trim();
                    if (!string.IsNullOrEmpty(this.drpCheckType.SelectedValue) && this.drpCheckType.SelectedValue != BLL.Const._Null)
                    {
                        newSafetyData.CheckType = this.drpCheckType.SelectedValue;
                        if (!string.IsNullOrEmpty(this.txtCheckTypeValue1.Text))
                        {
                            newSafetyData.CheckTypeValue1 = Funs.GetNewInt(this.txtCheckTypeValue1.Text);
                        }
                        if (!string.IsNullOrEmpty(this.txtCheckTypeValue2.Text))
                        {
                            newSafetyData.CheckTypeValue2 = Funs.GetNewInt(this.txtCheckTypeValue2.Text);                            
                        }
                    }

                    if (string.IsNullOrEmpty(this.SafetyDataId))
                    {
                        newSafetyData.SafetyDataId = SQLHelper.GetNewID(typeof(Model.SafetyData_SafetyData));
                        BLL.SafetyDataService.AddSafetyData(newSafetyData);
                    }
                    else
                    {
                        newSafetyData.SafetyDataId = this.SafetyDataId;
                        BLL.SafetyDataService.UpdateSafetyData(newSafetyData);
                    }

                    ////根据项目和安全资料项生成企业安全管理资料计划总表
                    BLL.SafetyDataPlanService.GetSafetyDataPlanByProjectInfo(string.Empty, newSafetyData.SafetyDataId, null, null);
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
                this.rowCheckType.Hidden = false;
            }
            else
            {
                this.rowScore.Hidden = true;
                this.rowCheckType.Hidden = true;
            }

            this.rowMenu.Hidden = !this.chkIsEndLevel.Checked;
        }

        /// <summary>
        /// 考核类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCheckTypeValue1.Text = string.Empty;
            this.txtCheckTypeValue2.Text = string.Empty;
            this.SetCheckTypeChange();
        }

        /// <summary>
        ///  考核类型变化
        /// </summary>
        private void SetCheckTypeChange()
        {            
            string checkType = this.drpCheckType.SelectedValue;
            if (checkType == BLL.Const.SafetyDataCheckType_1)
            {
                this.txtCheckTypeValue1.Hidden = false;
                this.txtCheckTypeValue1.Label = "月份加月数";  
                this.txtCheckTypeValue2.Hidden = false;                
                this.txtCheckTypeValue2.Label = "每月考核日";               
            }
            else if (checkType == BLL.Const.SafetyDataCheckType_2)
            {
                this.txtCheckTypeValue1.Hidden = false;
                this.txtCheckTypeValue1.Label = "季度加月数";                
                this.txtCheckTypeValue2.Hidden = false;
                this.txtCheckTypeValue2.Label = "季度考核日";
            }
            else if (checkType == BLL.Const.SafetyDataCheckType_3)
            {
                this.txtCheckTypeValue1.Hidden = false;
                this.txtCheckTypeValue1.Label = "定时报月";
                this.txtCheckTypeValue2.Hidden = false;
                this.txtCheckTypeValue2.Label = "定时报日";
            }
            else if (checkType == BLL.Const.SafetyDataCheckType_4)
            {
                this.txtCheckTypeValue1.Hidden = false;
                this.txtCheckTypeValue1.Label = "开工几个月内报";
                this.txtCheckTypeValue2.Hidden = true;
            }
            else  ///其他
            {
                this.txtCheckTypeValue1.Hidden = false;
                this.txtCheckTypeValue2.Hidden = false;
                this.txtCheckTypeValue1.Label = "当年上报月";
                this.txtCheckTypeValue2.Label = "当年上报日";
            }
        }
        #endregion

        #region 清空
        /// <summary>
        /// 清空下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMenu_ClearIconClick(object sender, EventArgs e)
        {
            this.drpMenu.Value = string.Empty;
        }    
        
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