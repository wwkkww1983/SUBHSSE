using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using BLL;
using System.Linq;

namespace FineUIPro.Web
{
    public partial class _default : PageBase
    {
        #region Page_Init

        private string _menuType = "menu";
        private bool _compactMode = false;
        private int _examplesCount = 0;
        private string _searchText = "";
        #region Page_Init

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////
            string themeStr = Request.QueryString["theme"];
            string menuStr = Request.QueryString["menu"];
            if (!String.IsNullOrEmpty(themeStr) || !String.IsNullOrEmpty(menuStr))
            {
                if (!String.IsNullOrEmpty(themeStr))
                {
                    if (themeStr == "bootstrap1")
                    {
                        themeStr = "bootstrap_pure";
                    }
                    HttpCookie cookie = new HttpCookie("Theme_Pro", themeStr)
                    {
                        Expires = DateTime.Now.AddYears(1)
                    };
                    Response.Cookies.Add(cookie);
                }

                if (!String.IsNullOrEmpty(menuStr))
                {
                    HttpCookie cookie = new HttpCookie("MenuStyle_Pro", menuStr)
                    {
                        Expires = DateTime.Now.AddYears(1)
                    };
                    Response.Cookies.Add(cookie);
                }

                PageContext.Redirect("~/default.aspx");
                return;
            }
            ////////////////////////////////////////////////////////////////

            // 从Cookie中读取 - 左侧菜单类型
            HttpCookie menuCookie = Request.Cookies["MenuStyle_Pro"];
            if (menuCookie != null)
            {
                _menuType = menuCookie.Value;
            }
            
            // 从Cookie中读取 - 是否启用紧凑模式
            HttpCookie menuCompactMode = Request.Cookies["EnableCompactMode_Pro"];
            if (menuCompactMode != null)
            {
                _compactMode = Convert.ToBoolean(menuCompactMode.Value);
            }
            // 从Cookie中读取 - 搜索文本
            HttpCookie searchText = Request.Cookies["SearchText_Pro"];
            if (searchText != null)
            {
                _searchText = HttpUtility.UrlDecode(searchText.Value);
            }

            if (!IsPostBack)
            {
                this.MenuSwitchMethod(string.Empty); ///调用菜单功能选择方法
                this.liUser.Text = this.CurrUser.UserName;
                this.lbTitle.Text = Funs.SystemName;
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null && !string.IsNullOrEmpty(unit.UnitName))
                {
                   // this.lbTitle.Text = unit.UnitName + Funs.SystemName;
                    if (unit.IsBuild == null || unit.IsBuild == false)
                    {
                        this.trBottom.Visible = false;
                    }
                }
                this.hdTitle.Text = this.lbTitle.Text;                               
                //this.InitMainPage();  /// 根据登录人的角色类型 判断首页
                if (this.CurrUser != null && !string.IsNullOrEmpty(this.CurrUser.PhotoUrl))
                {
                    this.liUser.IconUrl = "~/" + this.CurrUser.PhotoUrl;
                }
            }
        }

        #endregion

        #region 根据登录人的角色类型 判断首页
        /// <summary>
        /// 根据登录人的角色类型 判断首页
        /// </summary>
        //private void InitMainPage()
        //{
        //    //Tab1.IFrameUrl = "~/common/main_bootstrap_pure.aspx";
        //    var role = BLL.RoleService.GetRoleByRoleId(this.CurrUser.RoleId);
        //    if (role != null)
        //    {
        //        if (role.RoleType == "3") ///领导岗位
        //        {
        //            Tab1.IFrameUrl = "~/common/mainII.aspx";
        //            this.toolPageSet.Hidden = false;
        //        }
        //        else if (role.RoleType == "2" || role.RoleType == "1")  ///管理岗位
        //        {
        //            Tab1.IFrameUrl = "~/common/mainI.aspx";
        //        }
        //        else   ///一般岗位
        //        {
        //            Tab1.IFrameUrl = "~/common/main.aspx";
        //        }
        //    }
        //}
        #endregion

        #region InitAccordionMenu

        private Accordion InitAccordionMenu()
        {
            Accordion accordionMenu = new Accordion
            {
                ID = "accordionMenu",
                EnableFill = false,
                ShowBorder = false,
                ShowHeader = false
            };

            leftPanel.Items.Add(accordionMenu);
            XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
            XmlNodeList xmlNodes = xmlDoc.SelectNodes("/Tree/TreeNode");
            foreach (XmlNode xmlNode in xmlNodes)
            {
                //if (xmlNode.HasChildNodes)
                //{
                    string accordionPaneTitle = xmlNode.Attributes["Text"].Value;
                    if (GetIsNewHtml(xmlNode))
                    {
                        accordionPaneTitle = "<span class=\"isnew\">" + accordionPaneTitle + "</span>";
                        if (xmlNode.ParentNode != null)
                        {
                            xmlNode.ParentNode.Attributes["Text"].Value = "<span class=\"isnew\">" + xmlNode.ParentNode.Attributes["Text"].Value + "</span>";
                        }
                    }

                AccordionPane accordionPane = new AccordionPane
                {
                    Title = accordionPaneTitle,
                    //Layout = Layout.Fit,
                    ShowBorder = false
                };
                var accordionPaneIconAttr = xmlNode.Attributes["Icon"];
                    if (accordionPaneIconAttr != null)
                    {
                        accordionPane.Icon = (Icon)Enum.Parse(typeof(Icon), accordionPaneIconAttr.Value, true);
                    }

                    accordionMenu.Items.Add(accordionPane);
                Tree innerTree = new Tree
                {
                    ShowBorder = false,
                    ShowHeader = false,
                    EnableIcons = false,
                    AutoScroll = true,
                    EnableSingleClickExpand = true
                };
                accordionPane.Items.Add(innerTree);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Tree>{0}</Tree>", xmlNode.InnerXml));
                    ResolveXmlDocument(doc);
                    // 绑定AccordionPane内部的树控件
                    innerTree.NodeDataBound += treeMenu_NodeDataBound;
                    innerTree.PreNodeDataBound += treeMenu_PreNodeDataBound;
                    innerTree.DataSource = doc;
                    innerTree.DataBind();
                //}
            }

            return accordionMenu;
        }

        #endregion

        #region InitTreeMenu
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Tree InitTreeMenu()
        {
            Tree treeMenu = new Tree
            {
                ID = "treeMenu",
                ShowBorder = false,
                ShowHeader = false,
                EnableIcons = true,
                AutoScroll = true,
                EnableSingleClickExpand = true
            };
            if (_menuType == "tree" || _menuType == "tree_minimode")
            {
                treeMenu.HideHScrollbar = true;
                treeMenu.ExpanderToRight = true;
                treeMenu.HeaderStyle = true;              
                if (_menuType == "tree_minimode")
                {
                    treeMenu.MiniMode = true;
                    treeMenu.MiniModePopWidth = Unit.Pixel(300);

                    leftPanelToolGear.Hidden = true;
                    leftPanelBottomToolbar.Hidden = true;
                    leftPanelToolCollapse.IconFont = IconFont.ChevronCircleRight;
                    leftPanel.Width = Unit.Pixel(50);
                    leftPanel.CssClass = "minimodeinside";
                }
            }

            leftPanel.Items.Add(treeMenu);            
            XmlDocument doc = XmlDataSource1.GetXmlDocument();
            ResolveXmlDocument(doc);
            // 绑定 XML 数据源到树控件
            treeMenu.NodeDataBound += treeMenu_NodeDataBound;
            treeMenu.PreNodeDataBound += treeMenu_PreNodeDataBound;
            treeMenu.DataSource = doc;
            treeMenu.DataBind();
            return treeMenu;
        }

        #endregion

        #region ResolveXmlDocument

        private void ResolveXmlDocument(XmlDocument doc)
        {
            ResolveXmlDocument(doc, doc.DocumentElement.ChildNodes);
        }

        private int ResolveXmlDocument(XmlDocument doc, XmlNodeList nodes)
        {
            // nodes 中渲染到页面上的节点个数
            int nodeVisibleCount = 0;
            foreach (XmlNode node in nodes)
            {
                // Only process Xml elements (ignore comments, etc)
                if (node.NodeType == XmlNodeType.Element)
                {
                    XmlAttribute removedAttr;
                    // 是否叶子节点
                    bool isLeaf = node.ChildNodes.Count == 0;
                    // 所有过滤条件均是对叶子节点而言，而是否显示目录，要看是否存在叶子节点
                    if (isLeaf)
                    {
                        // 存在搜索关键字
                        if (!String.IsNullOrEmpty(_searchText))
                        {
                            XmlAttribute textAttr = node.Attributes["Text"];
                            if (textAttr != null)
                            {
                                if (!textAttr.Value.Contains(_searchText) && isLeaf)
                                {
                                    removedAttr = doc.CreateAttribute("Removed");
                                    removedAttr.Value = "true";
                                    node.Attributes.Append(removedAttr);
                                }
                            }
                        }                     
                    }

                    // 存在子节点
                    if (!isLeaf)
                    {
                        // 递归
                        int childVisibleCount = ResolveXmlDocument(doc, node.ChildNodes);

                        if (childVisibleCount == 0)
                        {
                            removedAttr = doc.CreateAttribute("Removed");
                            removedAttr.Value = "true";

                            node.Attributes.Append(removedAttr);
                        }
                    }

                    removedAttr = node.Attributes["Removed"];
                    if (removedAttr == null)
                    {
                        nodeVisibleCount++;
                    }
                }
            }

            return nodeVisibleCount;
        }
        #endregion

        #region treeMenu_NodeDataBound treeMenu_PreNodeDataBound
        /// <summary>
        /// 树节点的绑定后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_NodeDataBound(object sender, TreeNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;            
            if (!String.IsNullOrEmpty(e.Node.Text))
            {
                if (GetIsNewHtml(e.XmlNode))
                {
                    e.Node.Text = "<span class=\"isnew\">" + e.Node.Text + "</span>";
                    if (e.Node.ParentNode != null)
                    {
                        e.Node.ParentNode.Text = "<span class=\"isnew\">" + e.Node.ParentNode.Text + "</span>";
                    }
                }
            }

            if (isLeaf)
            {
                // 设置节点的提示信息
                e.Node.ToolTip = e.Node.Text;
            }
            // 如果仅显示最新示例，或者存在搜索文本
            if (!String.IsNullOrEmpty(_searchText))
            {
                e.Node.Expanded = true;
            }
        }

        /// <summary>
        /// 树节点的预绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_PreNodeDataBound(object sender, TreePreNodeEventArgs e)
        {
            // 是否叶子节点
            bool isLeaf = e.XmlNode.ChildNodes.Count == 0;
            XmlAttribute removedAttr = e.XmlNode.Attributes["Removed"];
            if (removedAttr != null)
            {
                e.Cancelled = true;
            }

            bool isShow = GetIsPowerMenu(e.XmlNode);
            if (!isShow)
            {
                e.Cancelled = true;
            }

            if (isLeaf && !e.Cancelled)
            {
                _examplesCount++;
            }
        }
        #endregion

        #region 该节点是否显示
        /// <summary>
        /// 该节点是否显示
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool GetIsPowerMenu(XmlNode node)
        {
            bool result = false;           
            XmlAttribute isNewAttr = node.Attributes["id"];
            if (isNewAttr != null)
            {
                result = BLL.CommonService.ReturnMenuByUserIdMenuId(this.CurrUser.UserId, isNewAttr.Value.ToString(),this.CurrUser.LoginProjectId);
            }

            return result;
        }
        #endregion

        #region GetIsNewHtml 是否必填项
        /// <summary>
        /// 是否必填项
        /// </summary>
        /// <param name="titleText"></param>
        /// <returns></returns>
        private bool GetIsNewHtml(XmlNode xmlNode)
        {
            bool isShow = false;
            if (xmlNode.Attributes["Text"].Value.Contains("*"))
            {
                isShow = true;
            }
            else
            {
                var safe = BLL.SafetyDataService.GetSafetyDataByMenuId(xmlNode.Attributes["id"].Value);
                if (safe != null)
                {
                    isShow = true;
                }
            }
           
            return isShow;
        }
        #endregion

        #endregion

        #region Page_Load
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.CurrUser.UserId == BLL.Const.hfnbdId)
                {
                    this.SysMenuSet.Hidden = false;
                    this.msSysMenuSet.Hidden = false;
                    this.CustomQuery.Hidden = false;
                    this.msCustomQuery.Hidden = false;
                }

                this.btnHomePage.OnClientClick = "parent.removeActiveTab();";
                this.btnSever.OnClientClick = "parent.removeActiveTab();";
                this.btnPoject.OnClientClick = "parent.removeActiveTab();";
                this.btnResource.OnClientClick = "parent.removeActiveTab();";
                this.btnBaseInfo.OnClientClick = "parent.removeActiveTab();";
                this.btnSystemSet.OnClientClick = "parent.removeActiveTab();";               
                this.InitSearchBox();
                this.InitMenuStyleButton();
                this.InitMenuModeButton();
                this.InitLangMenuButton();
                this.ShowVeionInfo();
                
                if (CommonService.GetIsThisUnit(Const.UnitId_14))
                {
                    this.trEHtml.Visible = true;
                    this.trEHtml1.Visible = true;
                    this.trEHtml2.Visible = true;
                    this.trEHtml3.Visible = true;
                }                
            }
        }

        /// <summary>
        /// 版本信息提示
        /// </summary>
        private void ShowVeionInfo()
        {
            this.CheckNewVeion();          
        }

        #region 版本信息从集团公司提取
        /// <summary>
        /// 版本信息从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetSys_VersionToSUBCompleted(object sender, HSSEService.GetSys_VersionToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var versionItems = e.Result;
                if (versionItems.Count() > 0)
                {
                    count = versionItems.Count();
                    foreach (var item in versionItems)
                    {
                        var version = Funs.DB.Sys_Version.FirstOrDefault(x => x.VersionId == item.VersionId);
                        if (version == null)
                        {
                            Model.Sys_Version newVersion = new Model.Sys_Version
                            {
                                VersionId = item.VersionId,
                                VersionName = item.VersionName,
                                VersionDate = item.VersionDate,
                                CompileMan = item.CompileMan,
                                AttachUrl = item.AttachUrl,
                                IsSub = item.IsSub
                            };
                            Funs.DB.Sys_Version.InsertOnSubmit(newVersion);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            if (e.Error == null)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【版本信息】从集团提取" + count.ToString() + "条数据；", null, BLL.Const.SynchronizationMenuId, BLL.Const.BtnDownload);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【版本信息】从集团提取失败；", null, BLL.Const.SynchronizationMenuId, BLL.Const.BtnDownload);
            }

            this.CheckNewVeion();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckNewVeion()
        {
            var sysVer = (from x in Funs.DB.Sys_Version where x.IsSub == true orderby x.VersionDate descending select x).FirstOrDefault();
            if (sysVer != null && sysVer.VersionName != Funs.SystemVersion && sysVer.VersionName.Length > 27 && Funs.SystemVersion.Length > 27)
            {
                DateTime? vdate = Funs.GetNewDateTime(sysVer.VersionName.Substring(17, 10));
                DateTime? fdate = Funs.GetNewDateTime(Funs.SystemVersion.Substring(17, 10));
                string vNum = sysVer.VersionName.Substring(27);
                string fNum = Funs.SystemVersion.Substring(27);
                if (vdate > fdate || (vdate == fdate && vNum != fNum))
                {
                    Alert.ShowInTop("系统新版本:" + sysVer.VersionName + "；请联系系统管理员及时升级！", MessageBoxIcon.Information);
                    return;
                }
            }
        }
        #endregion 

        /// <summary>
        /// 搜索菜单
        /// </summary>
        private void InitSearchBox()
        {           
            if (!String.IsNullOrEmpty(_searchText))
            {
                ttbxSearch.Text = _searchText;
                ttbxSearch.ShowTrigger1 = true;
            }
        }

        /// <summary>
        /// 菜单树样式
        /// </summary>
        private void InitMenuStyleButton()
        {
            string menuStyle = "tree";
            HttpCookie menuStyleCookie = Request.Cookies["MenuStyle_Pro"];
            if (menuStyleCookie != null)
            {
                menuStyle = menuStyleCookie.Value;
            }

            SetSelectedMenuItem(MenuStyle, menuStyle);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitMenuModeButton()
        {
            string menuMode = "normal";
            HttpCookie menuModeCookie = Request.Cookies["MenuMode_Pro"];
            if (menuModeCookie != null)
            {
                menuMode = menuModeCookie.Value;
            }

            SetSelectedMenuItem(MenuMode, menuMode);
        }

        /// <summary>
        /// 加载菜单语言
        /// </summary>
        private void InitLangMenuButton()
        {
            string language = "zh_CN";
            HttpCookie languageCookie = Request.Cookies["Language_Pro"];
            if (languageCookie != null)
            {
                language = languageCookie.Value;
            }

            SetSelectedMenuItem(MenuLang, language);
        }

        /// <summary>
        /// 过滤菜单
        /// </summary>
        /// <param name="menuButton"></param>
        /// <param name="selectedDataTag"></param>
        private void SetSelectedMenuItem(MenuButton menuButton, string selectedDataTag)
        {
            foreach (MenuItem item in menuButton.Menu.Items)
            {
                MenuCheckBox checkBox = (item as MenuCheckBox);
                if (checkBox != null)
                {
                    checkBox.Checked = checkBox.AttributeDataTag == selectedDataTag;
                }
            }
        }
        #endregion		

        #region 功能模块菜单切换
        /// <summary>
        /// 项目现场
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPoject_Click(object sender, EventArgs e)
        {
            this.CurrUser.LoginProjectId = string.Empty;
            var projectCount = ProjectService.GetAllProjectDropDownList();
            if (projectCount.Count == 0)
            {
                ShowNotify("未添加任何项目！", MessageBoxIcon.Warning);
                return;
            }
            if (CommonService.IsLeaderOrManage(this.CurrUser.UserId))
            {
                if (!CommonService.GetIsThisUnit(this.CurrUser.UnitId))
                {
                    var unitIdLists = CommonService.GetChildrenUnitId(this.CurrUser.UnitId);
                    if (unitIdLists.Count > 0)
                    {
                        projectCount = projectCount.Where(x => x.UnitId == this.CurrUser.UnitId || unitIdLists.Contains(x.UnitId)).OrderByDescending(x => x.ProjectCode).ToList();
                    }
                    else
                    {
                        projectCount = projectCount.Where(x => x.UnitId == this.CurrUser.UnitId).OrderByDescending(x => x.ProjectCode).ToList();
                    }                  
                }
                if (projectCount.Count == 1)
                {
                    var projectFirst = projectCount.FirstOrDefault();
                    if (projectFirst != null)
                    {
                        this.CurrUser.LoginProjectId = projectFirst.ProjectId;
                        this.MenuSwitchMethod(BLL.Const.Menu_Project);
                    }
                    else
                    {
                        ShowNotify("请刷新页面后再试！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(windowProject.GetShowReference(String.Format("~/ProjectData/EnterProjectMenu.aspx")));
                }
            }
            else
            {
                if (projectCount.Count == 1 && BLL.CommonService.IsThisUnitLeaderOrManage(this.CurrUser.UserId))
                {
                    var projectFirst = projectCount.FirstOrDefault();
                    if (projectFirst != null)
                    {
                        this.CurrUser.LoginProjectId = projectFirst.ProjectId;
                        this.MenuSwitchMethod(BLL.Const.Menu_Project);
                    }
                    else
                    {
                        ShowNotify("请刷新页面后再试！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    var projectUser = ProjectUserService.GetProjectUserByUserId(this.CurrUser.UserId);
                    int count = projectUser.Count();
                    if (count == 1)
                    {
                        var projectUserFirst = projectUser.FirstOrDefault();
                        if (projectUserFirst != null)
                        {
                            this.CurrUser.LoginProjectId = projectUserFirst.ProjectId;
                            this.MenuSwitchMethod(Const.Menu_Project);
                        }
                        else
                        {
                            ShowNotify("请刷新页面后再试！", MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (count > 1 || this.CurrUser.UserId == Const.sysglyId)
                    {
                        PageContext.RegisterStartupScript(windowProject.GetShowReference(String.Format("~/ProjectData/EnterProjectMenu.aspx")));
                    }
                    else
                    {
                        ShowNotify("当前人没有参与任何项目！", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 项目返回方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void windowProject_Close(object sender, WindowCloseEventArgs e)
        {           
            this.MenuSwitchMethod(Const.Menu_Project);
        }

        /// <summary>
        /// 本部管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSever_Click(object sender, EventArgs e)
        {
            this.CurrUser.LoginProjectId = null;
            this.MenuSwitchMethod(BLL.Const.Menu_Sever);
        }

        /// <summary>
        /// 资源平台
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResource_Click(object sender, EventArgs e)
        {
            this.CurrUser.LoginProjectId = null;
            this.MenuSwitchMethod(BLL.Const.Menu_Resource);
        }
      
        /// <summary>
        /// 基础信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBaseInfo_Click(object sender, EventArgs e)
        {
            this.CurrUser.LoginProjectId = null;
            this.MenuSwitchMethod(BLL.Const.Menu_BaseInfo);
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSystemSet_Click(object sender, EventArgs e)
        {
            this.CurrUser.LoginProjectId = null;
            this.MenuSwitchMethod(BLL.Const.Menu_SystemSet);
        }

        /// <summary>
        ///  功能模块切换方法
        /// </summary>
        /// <param name="type"></param>
        protected void MenuSwitchMethod(string type)
        {                               
            this.lbTitle.Text = hdTitle.Text;
            string projectType = string.Empty;
            if (type == Const.Menu_Project)
            {                             
                var project = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    projectType = project.ProjectType;
                    if (!string.IsNullOrEmpty(project.ProjectCode))
                    {
                        this.lbTitle.Text = "[" + project.ProjectCode + "]" + project.ProjectName;
                    }
                    else
                    {
                        this.lbTitle.Text = project.ProjectName;
                    }

                    if (this.lbTitle.Text.Length > 40)
                    {
                        this.lbTitle.Text = this.lbTitle.Text.Substring(0, 40) + "..."; 
                    }
                }               
            }
            if (!string.IsNullOrEmpty(type))
            {
                this.XmlDataSource1.DataFile = "common/" + type + ".xml";
                if (projectType == "5")
                {
                    var unit = CommonService.GetIsThisUnit();
                    if (unit != null && unit.UnitId == Const.UnitId_ECEC)
                    {
                        this.XmlDataSource1.DataFile = "common/Menu_EProject.xml";
                    }
                }
            }
            else
            {
                this.XmlDataSource1.DataFile = "common/" + BLL.Const.Menu_Personal + ".xml";
            }

            if (_menuType == "accordion")
            {
                InitAccordionMenu();
            }
            else
            {
                InitTreeMenu();
            }            
        }
        #endregion       
    }
}
