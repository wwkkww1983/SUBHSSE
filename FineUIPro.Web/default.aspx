<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FineUIPro.Web._default" Async="true" AsyncTimeout="360000" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>施工安全生产信息化管理系统</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />  
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />    
    <style type="text/css"> 
    .titler
    {   
        font-size:smaller;            
    } 
    </style>
</head>
<body class="defaultpage">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"></f:PageManager>
        <f:Panel ID="Panel1" Layout="Region" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:ContentPanel ID="topPanel" CssClass="topregion" RegionPosition="Top" ShowBorder="false" ShowHeader="false" EnableCollapse="true" runat="server">
                    <div id="header" class="ui-widget-header f-mainheader">
                        <table>
                            <tr>
                                <td>
                                    <f:Button runat="server" CssClass="icononlyaction" ID="btnHomePage" ToolTip="首页" IconAlign="Top" IconFont="Home"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onHomePageClick" />
                                        </Listeners>
                                    </f:Button>
                                    <a class="logo" href="./default.aspx">
                                         <f:Label ID="lbTitle" runat="server" CssClass="titler"></f:Label>
                                         <f:HiddenField ID="hdTitle" runat="server"></f:HiddenField>
                                    </a>
                                </td>
                                <td style="text-align: right;">                                        
                                    <f:Button runat="server" CssClass="icontopaction nexttheme" ID="btnSever" Text="本部管理" ToolTip="本部管理系统"
                                            IconAlign="Top"  EnableDefaultState="false" OnClick="btnSever_Click"
                                          EnableDefaultCorner="false">
                                    </f:Button>
                                     <f:Button runat="server" CssClass="icontopaction nexttheme" ID="btnPoject" 
                                        Text="项目现场"  ToolTip="项目现场管理系统" IconAlign="Top"  
                                        EnableDefaultState="false" EnableDefaultCorner="false" OnClick="btnPoject_Click">                                       
                                    </f:Button>                                   
                                     <f:Button runat="server" CssClass="icontopaction nexttheme" ID="btnResource" Text="公共资源" ToolTip="公共资源管理系统"
                                        IconAlign="Top" EnableDefaultState="false" OnClick="btnResource_Click"
                                          EnableDefaultCorner="false">
                                    </f:Button>                                  
                                    <f:Button runat="server" CssClass="icontopaction nexttheme" ID="btnBaseInfo" Text="基础信息" ToolTip="基础信息管理"
                                        IconAlign="Top"  EnableDefaultState="false" OnClick="btnBaseInfo_Click"
                                          EnableDefaultCorner="false">
                                    </f:Button> 
                                    <f:Button runat="server" CssClass="icontopaction nexttheme" ID="btnSystemSet" Text="系统设置" ToolTip="系统设置管理"
                                        IconAlign="Top"  EnableDefaultState="false" OnClick="btnSystemSet_Click"
                                          EnableDefaultCorner="false">
                                    </f:Button>                                                                                  
                                    <f:Button runat="server" CssClass="userpicaction" IconUrl="~/res/images/my_face_80.jpg" IconAlign="Left"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false" ID="liUser">
                                        <Menu runat="server">
                                             <f:MenuButton ID="MenuButton1" Text="个人设置" IconFont="User" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onUserProfileClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator ID="msSysMenuSet" runat="server" Hidden="true"></f:MenuSeparator>
                                            <f:MenuButton Text="功能菜单" IconFont="ThList" EnablePostBack="false" runat="server" ID="SysMenuSet" Hidden="true">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onSysMenuSetClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator runat="server" ID="msCustomQuery" Hidden="true"></f:MenuSeparator>
                                             <f:MenuButton Text="自定义查询" IconFont="Table" EnablePostBack="false" runat="server" ID="CustomQuery" Hidden="true">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onCustomQueryClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator runat="server"></f:MenuSeparator>
                                            <f:MenuButton Text="安全退出" IconFont="Close" EnablePostBack="false" runat="server">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onSignOutClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                        </Menu>
                                    </f:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </f:ContentPanel>
                <f:Panel ID="leftPanel" CssClass="leftregion" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="3px"
                    ShowBorder="true" Width="230px" ShowHeader="true" Title="系统菜单"
                    EnableCollapse="false" Collapsed="false" Layout="Fit" runat="server">
                    <Tools>
                        <%--自定义展开折叠工具图标--%>
                        <f:Tool ID="leftPanelToolCollapse" runat="server" IconFont="ChevronCircleLeft" EnablePostBack="false" ToolTip="展开/折叠">
                            <Listeners>
                                <f:Listener Event="click" Handler="onLeftPanelToolCollapseClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool ID="leftPanelToolGear" runat="server" IconFont="Gear" EnablePostBack="false" ToolTip="设置">
                            <Menu runat="server" ID="menuSettings">
                                <f:MenuButton ID="btnExpandAll" Text="展开菜单" EnablePostBack="false" runat="server"  >
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onExpandAllClick" />
                                    </Listeners>
                                </f:MenuButton>
                                <f:MenuButton ID="btnCollapseAll" Text="折叠菜单" EnablePostBack="false" runat="server" >
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onCollapseAllClick" />
                                    </Listeners>
                                </f:MenuButton>                               
                                <f:MenuSeparator runat="server">
                                </f:MenuSeparator>
                                <f:MenuButton runat="server" EnablePostBack="false" ID="MenuMode" Text="显示模式">
                                    <Menu runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="普通模式" ID="MenuModeNormal" AttributeDataTag="normal" Checked="true" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="紧凑模式" ID="MenuModeCompact" AttributeDataTag="compact" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="大字体模式" ID="MenuModeLarge" AttributeDataTag="large" GroupName="MenuMode" runat="server">
                                            </f:MenuCheckBox>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuModeCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>
                                <f:MenuButton EnablePostBack="false" Text="菜单样式" ID="MenuStyle" runat="server">
                                    <Menu runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="智能树菜单" ID="MenuStyleTree" AttributeDataTag="tree" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="智能树菜单（默认折叠）" ID="MenuStyleMiniModeTree" AttributeDataTag="tree_minimode" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="树菜单" ID="MenuStylePlainTree" AttributeDataTag="plaintree" GroupName="MenuStyle" runat="server" Checked="true">
                                            </f:MenuCheckBox>
                                           <%-- <f:MenuCheckBox Text="手风琴+树菜单" ID="MenuStyleAccordion" AttributeDataTag="accordion" GroupName="MenuStyle" runat="server">
                                            </f:MenuCheckBox>--%>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuStyleCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>
                                <f:MenuButton EnablePostBack="false" Text="语言" ID="MenuLang" runat="server">
                                    <Menu ID="Menu2" runat="server">
                                        <Items>
                                            <f:MenuCheckBox Text="简体中文" ID="MenuLangZHCN" AttributeDataTag="zh_CN" Checked="true" GroupName="MenuLang" runat="server">
                                            </f:MenuCheckBox>
                                            <%--<f:MenuCheckBox Text="繁體中文" ID="MenuLangZHTW" AttributeDataTag="zh_TW" GroupName="MenuLang" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="English" ID="MenuLangEN" AttributeDataTag="en" GroupName="MenuLang" runat="server">
                                            </f:MenuCheckBox>
                                            <f:MenuCheckBox Text="ئۇيغۇر تىلى" ID="MenuLangZHUEY" AttributeDataTag="zh_UEY" GroupName="MenuLang" runat="server">
                                            </f:MenuCheckBox>--%>
                                        </Items>
                                        <Listeners>
                                            <f:Listener Event="checkchange" Handler="onMenuLangCheckChange" />
                                        </Listeners>
                                    </Menu>
                                </f:MenuButton>                              
                            </Menu>
                        </f:Tool>
                    </Tools>
                    <Toolbars>
                        <f:Toolbar ID="leftPanelBottomToolbar" Position="Bottom" HeaderStyle="true" runat="server" Layout="Fit">
                            <Items>
                                <f:TwinTriggerBox ID="ttbxSearch" ShowLabel="false" Trigger1Icon="Clear" ShowTrigger1="False" EmptyText="搜索菜单" Trigger2Icon="Search"
                                    EnableTrigger1PostBack="false" EnableTrigger2PostBack="false" runat="server" Width="235px">
                                    <Listeners>
                                        <f:Listener Event="trigger1click" Handler="onSearchTrigger1Click" />
                                        <f:Listener Event="trigger2click" Handler="onSearchTrigger2Click" />
                                    </Listeners>
                                </f:TwinTriggerBox>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:Panel>
                <f:TabStrip ID="mainTabStrip" CssClass="centerregion" RegionPosition="Center" ShowBorder="true" EnableTabCloseMenu="true" runat="server">
                    <Tabs>
                        <f:Tab ID="Tab1" Title="首页" IconFont="Home" EnableIFrame="true" IFrameUrl="~/common/mainI.aspx" runat="server">
                        </f:Tab>
                    </Tabs>
                    <Tools> 
                        <f:Tool runat="server" EnablePostBack="false" IconFont="ExternalLink" CssClass="tabtool" ToolTip="打开待办事项详细" ID="toolPageSet" 
                            Hidden="true" Text="待办事项">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolPageSetClick" />
                            </Listeners>
                        </f:Tool>                                               
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Refresh" MarginRight="5" CssClass="tabtool" ToolTip="刷新本页" ID="toolRefresh">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolRefreshClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Share" MarginRight="5" CssClass="tabtool" ToolTip="在新标签页中打开" ID="toolNewWindow">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolNewWindowClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Expand" CssClass="tabtool" ToolTip="最大化" ID="toolMaximize">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolMaximizeClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Bank" CssClass="tabtool" ToolTip="皮肤" ID="btnThemeSelect">
                            <Listeners>
                                <f:Listener Event="click" Handler="onThemeSelectClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="Spinner" CssClass="tabtool" ToolTip="加载动画" ID="btnLoadingSelect">
                            <Listeners>
                                <f:Listener Event="click" Handler="onLoadingSelectClick" />
                            </Listeners>
                        </f:Tool>
                         <f:Tool runat="server" EnablePostBack="false" IconFont="Eye" CssClass="tabtool" ToolTip="帮助" ID="toolSourceCode">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                            </Listeners>
                        </f:Tool>
                        <f:Tool runat="server" EnablePostBack="false" IconFont="SignOut" CssClass="tabtool" ToolTip="注销" ID="toolSignOut">
                            <Listeners>
                                <f:Listener Event="click" Handler="onToolSignOutClick" />
                            </Listeners>
                        </f:Tool>
                    </Tools>
                </f:TabStrip>
                <f:ContentPanel ID="bottomPanel" CssClass="bottomregion" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server">
                    <table class="bottomtable ui-widget-header f-mainheader">
                         <tr runat="server" id="trBottom">
                            <td style="text-align: left;">&nbsp;&nbsp;</td>                                                     
                            <td style="text-align: center;" id="trGJ" runat="server">
                              <a target="_blank" href="http://www.chinasafety.gov.cn">国家安全生产监督管理总局</a>
                            </td>                                                    
                            <td style="text-align: center;" runat="server" id="trEHtml" visible="false">
                              <a target="_blank" href="http://www.njajzelaq.com/2015/application/ui/EnterpriseLogin.html">“e路安全”</a>
                            </td>
                            <td style="text-align: center;" runat="server" id="trEHtml1" visible="false">
                              <a target="_blank" href="http://221.226.86.91:8000/zhaj/login_ent.jsp">南京智慧安监云平台</a>
                            </td>
                             <td style="text-align: center;" runat="server" id="trEHtml2" visible="false">
                              <a target="_blank" href="http://218.94.101.3:8089/hbpj/toLogin.do">南京环保信用评价管理系统</a>
                            </td>
                             <td style="text-align: center;" runat="server" id="trEHtml3" visible="false">
                              <a target="_blank" href="http://www.aqt365.com/jssajxt/application/ui/login.htm">傲途建筑安全智能监督软件</a>
                            </td>
                            <td style="text-align: center;"  id="trcncec" runat="server">
                              <a target="_blank" href="http://cncec.cn/" >Copyright © 2015 China National Chemical Engineering Group Corporation All rights reserved.p 中国化学工程集团公司 版权所有</a>
                            </td>
                             <td style="text-align: center;"  id="trXJYJ" runat="server" visible="false">
                              <a target="_blank" href="http://www.xjyjgs.cn" >新ICP备16001300号</a>
                            </td>
                            <td style="text-align: right;">&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </f:ContentPanel>
            </Items>
        </f:Panel>      
        <f:Window ID="windowThemeRoller" Title="皮肤" Hidden="true" EnableIFrame="true" IFrameUrl="./common/themes.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1020px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowLoadingSelector" Title="加载动画" Hidden="true" EnableIFrame="true" IFrameUrl="./common/loading.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1000px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowSysMenuSet" Title="功能菜单" Hidden="true" EnableIFrame="true" IFrameUrl="./SysManage/SystemMenuSet.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1024px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowUserProfile" Title="个人设置" Hidden="true" EnableIFrame="true" IFrameUrl="./Personal/PersonalSet.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1000px" Height="560px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
         <f:Window ID="windowtoolPageSet" Title="待办信息" Hidden="true" EnableIFrame="true" IFrameUrl="./common/mainI.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1000px" Height="560px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
         <f:Window ID="windowProject" Title="项目信息" Hidden="true" EnableIFrame="true" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1150px" Height="500px" EnableClose="true" OnClose="windowProject_Close"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowCustomQuery" Title="自定义查询" Hidden="true" EnableIFrame="true" IFrameUrl="./SysManage/CustomQuery.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1200px" Height="620px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false"></asp:XmlDataSource>
    </form>

    <script type="text/javascript">
        var toolRefreshClientID = '<%= toolRefresh.ClientID %>';
        var toolNewWindowClientID = '<%= toolNewWindow.ClientID %>';
        var mainTabStripClientID = '<%= mainTabStrip.ClientID %>';
        var windowSysMenuSetClientID = '<%= windowSysMenuSet.ClientID %>';
         var windowCustomQueryClientID = '<%= windowCustomQuery.ClientID %>';
        var windowUserProfileClientID = '<%= windowUserProfile.ClientID %>';
        var windowtoolPageSetClientID = '<%= windowtoolPageSet.ClientID %>';

        var windowThemeRollerClientID = '<%= windowThemeRoller.ClientID %>';
        var windowLoadingSelectorClientID = '<%= windowLoadingSelector.ClientID %>';
        var MenuStyleClientID = '<%= MenuStyle.ClientID %>';
        var MenuLangClientID = '<%= MenuLang.ClientID %>';
        var topPanelClientID = '<%= topPanel.ClientID %>';
        var leftPanelClientID = '<%= leftPanel.ClientID %>';
        var leftPanelToolGearClientID = '<%= leftPanelToolGear.ClientID %>';
        var leftPanelBottomToolbarClientID = '<%= leftPanelBottomToolbar.ClientID %>';
        var leftPanelToolCollapseClientID = '<%= leftPanelToolCollapse.ClientID %>';
        var tab1ClientID = '<%= Tab1.ClientID %>';
        // 点击官网首页
        function onHomePageClick(event) {
           top.location='./default.aspx';
        }

        // 点击主题仓库
        function onThemeSelectClick(event) {
            var windowThemeRoller = F(windowThemeRollerClientID);
            windowThemeRoller.show();
        }

        // 点击加载动画
        function onLoadingSelectClick(event) {
            var windowLoadingSelector = F(windowLoadingSelectorClientID);
            windowLoadingSelector.show();
        }
        // 展开左侧面板
        function expandLeftPanel() {
            var leftPanel = F(leftPanelClientID);

            // 获取左侧树控件实例
            var leftMenuTree = leftPanel.items[0];
            leftMenuTree.miniMode = false;
            leftPanel.el.removeClass('minimodeinside');
            leftPanel.setWidth(220);
            F(leftPanelToolGearClientID).show();
            F(leftPanelBottomToolbarClientID).show();
            F(leftPanelToolCollapseClientID).setIconFont('chevron-circle-left');
            // 重新加载树菜单
            leftMenuTree.loadData();
        }

        // 展开左侧面板
        function collapseLeftPanel() {
            var leftPanel = F(leftPanelClientID);
            // 获取左侧树控件实例
            var leftMenuTree = leftPanel.items[0];
            leftMenuTree.miniMode = true;
            leftMenuTree.miniModePopWidth = 220;
            leftPanel.el.addClass('minimodeinside');
            leftPanel.setWidth(50);
            F(leftPanelToolGearClientID).hide();
            F(leftPanelBottomToolbarClientID).hide();
            F(leftPanelToolCollapseClientID).setIconFont('chevron-circle-right');
            // 重新加载树菜单
            leftMenuTree.loadData();
        }

        // 自定义展开折叠工具图标
        function onLeftPanelToolCollapseClick(event) {
            var leftPanel = F(leftPanelClientID);
            var menuStyle = F.cookie('MenuStyle_Pro') || 'tree';

            if (menuStyle === 'tree' || menuStyle === 'tree_minimode') {
                // 获取左侧树控件实例
                var leftMenuTree = leftPanel.items[0];

                // 设置 miniMode 模式
                if (leftMenuTree.miniMode) {
                    expandLeftPanel();
                } else {
                    collapseLeftPanel();
                }

                // 对左侧面板重新布局
                leftPanel.doLayout();
            } else {
                leftPanel.toggleCollapse();
            }
        }

        // 点击展开菜单
        function onExpandAllClick(event) {
            var leftPanel = F(leftPanelClientID);
            var firstChild = leftPanel.items[0];

            if (firstChild.isType('tree')) {
                // 左侧为树控件
                firstChild.expandAll();
            } else {
                // 左侧为树控件+手风琴控件
                var activePane = firstChild.getActivePane();
                if (activePane) {
                    activePane.items[0].expandAll();
                }
            }
        }

        // 点击折叠菜单
        function onCollapseAllClick(event) {
            var leftPanel = F(leftPanelClientID);
            var firstChild = leftPanel.items[0];

            if (firstChild.isType('tree')) {
                // 左侧为树控件
                firstChild.collapseAll();
            } else {
                // 左侧为树控件+手风琴控件
                var activePane = firstChild.getActivePane();
                if (activePane) {
                    activePane.items[0].collapseAll();
                }
            }
        }
        
        function onSearchTrigger1Click(event) {
            F.removeCookie('SearchText_Pro');
            top.window.location.reload();
        }

        function onSearchTrigger2Click(event) {
            F.cookie('SearchText_Pro', this.getValue(), {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击标题栏工具图标 - 待办
        function onToolPageSetClick(event) {
            var windowtoolPageSet = F(windowtoolPageSetClientID);
            windowtoolPageSet.show();
        }

        // 点击标题栏工具图标 - 查看源代码
        function onToolSourceCodeClick(event) {
            window.open('Doc/help.doc', '_blank');
        }

        // 点击标题栏工具图标 - 刷新
        function onToolRefreshClick(event) {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeWnd = activeTab.getIFrameWindow();
                iframeWnd.location.reload();
            }
        }

        // 点击标题栏工具图标 - 在新标签页中打开
        function onToolNewWindowClick(event) {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab.iframe) {
                var iframeUrl = activeTab.getIFrameUrl();
                iframeUrl = iframeUrl.replace(/\/mobile\/\?file=/ig, '/mobile/');
                window.open(iframeUrl, '_blank');
            }
        }

        // 点击标题栏工具图标 - 注销
        function onToolSignOutClick(event) {
            var bConfirmed = confirm('您确定要注销吗?');
            if (bConfirmed) { window.open('login.aspx', '_top'); }
        }

        // 点击标题栏工具图标 - 最大化
        function onToolMaximizeClick(event) {
            var topPanel = F(topPanelClientID);
            var leftPanel = F(leftPanelClientID);

            var currentTool = this;
            if (currentTool.iconFont.indexOf('expand') >= 0) {
                topPanel.collapse();
                currentTool.setIconFont('compress');

                collapseLeftPanel();
            } else {
                topPanel.expand();
                currentTool.setIconFont('expand');

                expandLeftPanel();
            }
        }

        // 添加示例标签页
        // id： 选项卡ID
        // iframeUrl: 选项卡IFrame地址 
        // title： 选项卡标题
        // icon： 选项卡图标
        // createToolbar： 创建选项卡前的回调函数（接受tabOptions参数）
        // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
        // iconFont： 选项卡图标字体
        function addExampleTab(tabOptions) {
            if (typeof (tabOptions) === 'string') {
                tabOptions = {
                    id: arguments[0],
                    iframeUrl: arguments[1],
                    title: arguments[2],
                    icon: arguments[3],
                    createToolbar: arguments[4],
                    refreshWhenExist: arguments[5],
                    iconFont: arguments[6]
                };
            }

            F.addMainTab(F(mainTabStripClientID), tabOptions);
        }

        // 移除选中标签页
        function removeActiveTab() {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab) {
                if (activeTab.id != tab1ClientID) {
                    activeTab.hide();
                    removeActiveTab();
                }
            }
        }
       
        // 获取当前激活选项卡的ID
        function getActiveTabId() {
            var mainTabStrip = F(mainTabStripClientID);
            var activeTab = mainTabStrip.getActiveTab();
            if (activeTab) {
                return activeTab.id;
            }
            return '';
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后刷新父选项卡）
        function activeTabAndRefresh(tabId) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.refreshIFrame();
            }
        }

        // 激活选项卡，并刷新其中的内容，示例：表格控件->杂项->在新标签页中打开（关闭后更新父选项卡中的表格）
        function activeTabAndUpdate(tabId, param1) {
            var mainTabStrip = F(mainTabStripClientID);
            var targetTab = mainTabStrip.getTab(tabId);
            if (targetTab) {
                mainTabStrip.activeTab(targetTab);
                targetTab.getIFrameWindow().updatePage(param1);
            }
        }

        // 通知框
        function notify(msg) {
            F.notify({
                message: msg,
                messageIcon: 'information',
                target: '_top',
                header: false,
                displayMilliseconds: 3 * 1000,
                positionX: 'center',
                positionY: 'center'
            });
        }

        // 点击菜单样式
        function onMenuStyleCheckChange(event, item, checked) {
            var menuStyle = item.getAttr('data-tag');
            F.cookie('MenuStyle_Pro', menuStyle, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击显示模式
        function onMenuModeCheckChange(event, item, checked) {
            var menuMode = item.getAttr('data-tag');

            F.cookie('MenuMode_Pro', menuMode, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击语言
        function onMenuLangCheckChange(event, item, checked) {
            var lang = item.getAttr('data-tag');
            F.cookie('Language_Pro', lang, {
                expires: 100 // 单位：天
            });
            top.window.location.reload();
        }

        // 点击标题栏工具图标 - 退出
        function onSignOutClick(event) {
            var bConfirmed = confirm('您确定要退出吗?');
            if (bConfirmed) { window.close(); }
        }

        ///个人信息
        function onUserProfileClick() {
            var windowUserProfile = F(windowUserProfileClientID);
            windowUserProfile.show();
        }

        ///功能菜单设置
        function onSysMenuSetClick() {
            var windowSysMenuSet = F(windowSysMenuSetClientID);
            windowSysMenuSet.show();
        }

        ///功能菜单设置
        function onCustomQueryClick() {
            var windowCustomQuery = F(windowCustomQueryClientID);
            windowCustomQuery.show();
        }

        F.ready(function () {

            var mainTabStrip = F(mainTabStripClientID);
            var leftPanel = F(leftPanelClientID);
            var mainMenu = leftPanel.items[0];

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.initTreeTabStrip(mainMenu, mainTabStrip, true, false, false);
            var themeTitle = F.cookie('Theme_Pro_Title');
            var themeName = F.cookie('Theme_Pro');
            if (themeTitle) {
                F.removeCookie('Theme_Pro_Title');
                //notify('主题更改为：' + themeTitle + '（' + themeName + '）');
            }

        });
    </script>
</body>
</html>
