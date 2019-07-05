using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.ProjectData
{
    public partial class CodeTemplateRule : PageBase
    {       

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////权限按钮方法
                this.GetButtonPower();
                this.treeMenu.Nodes.Clear();
                var sysMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(BLL.Const.Menu_Project);
                if (sysMenu.Count() > 0)
                {
                    this.InitTreeMenu(sysMenu, null);
                }
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

        #region 下拉框回发事件
        /// <summary>
        /// 下拉框回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMenu_TextChanged(object sender, EventArgs e)
        {
            string menuId = this.drpMenu.Value;
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(sysMenu.MenuId,this.CurrUser.LoginProjectId);
                if (codeTemplateRule != null)
                {
                    if (codeTemplateRule.IsProjectCode == true)
                    {
                        this.ckProjectCode.Checked = true;
                    }
                    else
                    {
                        this.ckProjectCode.Checked = false;
                    }
                    this.txtPrefix.Text = codeTemplateRule.Prefix;
                    if (codeTemplateRule.IsUnitCode == true)
                    {
                        this.ckUnitCode.Checked = true;
                    }
                    else
                    {
                        this.ckUnitCode.Checked = false;
                    }
                    this.txtDigit.Text = codeTemplateRule.Digit.ToString();
                    if (codeTemplateRule.OwerIsProjectCode == true)
                    {
                        this.ckProjectCodeOwer.Checked = true;
                    }
                    else
                    {
                        this.ckProjectCodeOwer.Checked = false;
                    }
                    this.txtPrefixOwer.Text = codeTemplateRule.OwerPrefix;
                    if (codeTemplateRule.OwerIsUnitCode == true)
                    {
                        this.ckUnitCodeOwer.Checked = true;
                    }
                    else
                    {
                        this.ckUnitCodeOwer.Checked = false;
                    }
                    this.txtDigitOwer.Text = codeTemplateRule.OwerDigit.ToString();
                    this.txtTemplate.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    this.txtSymbol.Text = codeTemplateRule.Symbol;
                    this.txtSymbolOwer.Text = codeTemplateRule.OwerSymbol;
                }
                else
                {
                    this.ckProjectCode.Checked = true;
                    this.txtDigit.Text = "4";
                    this.txtSymbol.Text = "-";                   
                    this.txtPrefix.Text = string.Empty;
                    this.ckUnitCode.Checked = false;

                    this.ckProjectCodeOwer.Checked = true;
                    this.txtDigitOwer.Text = "4";                  
                    this.txtSymbolOwer.Text = "-";                    
                    this.txtPrefixOwer.Text = string.Empty;
                    this.ckUnitCodeOwer.Checked = false;
                    this.txtTemplate.Text = HttpUtility.HtmlDecode(string.Empty);
                }
            }
            else
            {
                this.drpMenu.Text = string.Empty;
                this.drpMenu.Value = string.Empty;
                ShowNotify("请选择末级菜单操作！", MessageBoxIcon.Warning);
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
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                this.SaveData2(sysMenu.MenuId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改菜单编码模板设置！");
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        private void SaveData2(string menuId)
        {
            Model.ProjectData_CodeTemplateRule newCodeTemplateRule = new ProjectData_CodeTemplateRule
            {
                MenuId = menuId,
                ProjectId = this.CurrUser.LoginProjectId,
                Template = HttpUtility.HtmlEncode(this.txtTemplate.Text),
                Symbol = this.txtSymbol.Text.Trim(),
                IsProjectCode = this.ckProjectCode.Checked,
                Prefix = this.txtPrefix.Text.Trim(),
                IsUnitCode = this.ckUnitCode.Checked,
                Digit = Funs.GetNewInt(this.txtDigit.Text),
                OwerSymbol = this.txtSymbolOwer.Text.Trim(),
                OwerIsProjectCode = this.ckProjectCodeOwer.Checked,
                OwerPrefix = this.txtPrefixOwer.Text.Trim(),
                OwerIsUnitCode = this.ckUnitCodeOwer.Checked,
                OwerDigit = Funs.GetNewInt(this.txtDigitOwer.Text)
            };
            var getCodeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(menuId,this.CurrUser.LoginProjectId);
            if (getCodeTemplateRule != null)
            {
                newCodeTemplateRule.CodeTemplateRuleId = getCodeTemplateRule.CodeTemplateRuleId;
                BLL.ProjectData_CodeTemplateRuleService.UpdateProjectData_CodeTemplateRule(newCodeTemplateRule);
            }
            else
            {
                BLL.ProjectData_CodeTemplateRuleService.AddProjectData_CodeTemplateRule(newCodeTemplateRule);
            }
        }
        #endregion

        /// <summary>
        /// 更新施工编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPageWhiteRefresh_Click(object sender, EventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                this.SaveData2(sysMenu.MenuId);
                BLL.CodeRecordsService.UpdateCodeRecordsByMenuIdProjectId(this.drpMenu.Value, this.CurrUser.LoginProjectId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "更新编码！");
                ShowNotify("更新成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请选择菜单！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 重新排序并生成编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDatabaseRefresh_Click(object sender, EventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                this.SaveData2(sysMenu.MenuId);
                BLL.CodeRecordsService.DatabaseRefreshCodeRecordsByMenuIdProjectId(this.drpMenu.Value,this.CurrUser.LoginProjectId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "重新排序并生成编码！");
                ShowNotify("重新生成成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请选择菜单！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 重置规则/模板，恢复初始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            BLL.ProjectData_CodeTemplateRuleService.DeleteProjectData_CodeTemplateRule(this.CurrUser.LoginProjectId);
            BLL.ProjectData_CodeTemplateRuleService.InertProjectData_CodeTemplateRuleByProjectId(this.CurrUser.LoginProjectId);
        }


        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            this.btnReset.Hidden = true;
            this.btnSave.Hidden = true;
            this.btnPageWhiteRefresh.Hidden = true;
            this.btnDatabaseRefresh.Hidden = true;
            if (Request.Params["value"] == "0")
            {
                return;
            }
            else
            {
                string menuId = BLL.Const.ProjectCodeTemplateRuleMenuId;
                var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, menuId);
                if (buttonList.Count() > 0 && buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnReset.Hidden = false;
                    this.btnSave.Hidden = false;
                    this.btnPageWhiteRefresh.Hidden = false;
                    this.btnDatabaseRefresh.Hidden = false;
                }
            }
        }
        #endregion
    }
}