<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileCabinetB.aspx.cs" Inherits="FineUIPro.Web.InformationProject.FileCabinetB" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>文件柜</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
     <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>         
        <f:Panel ID="RegionPanel1" Layout="Region" ShowBorder="false" ShowHeader="false" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                    <Items>
                        <f:DropDownList runat="server" ID="drpProject" RegionPosition="Center" Label="项目" EnableEdit="true" Width="500px" 
                           AutoPostBack="true" OnSelectedIndexChanged ="drpProject_OnSelectedIndexChanged" LabelWidth="60px" MarginLeft="50px">
                         </f:DropDownList>
                    </Items>                    
                </f:Toolbar>
            </Toolbars>          
            <Items>                    
                <f:Tree runat="server" ID="leftTree" CssClass="leftregion" RegionPosition="Left" RegionSplit="false" EnableCollapse="false"
                    Width="200px" Title="业务菜单" ShowBorder="true" ShowHeader="false" EnableNodeHyperLink="true" EnableSingleExpand="true">
                </f:Tree>
                <f:Panel ID="mainPanel" CssClass="centerregion" ShowHeader="false" RegionPosition="Center" ShowBorder="true"
                    EnableIFrame="true" IFrameName="mainframe" runat="server">
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script type="text/javascript">
        var leftTreeID = '<%= leftTree.ClientID %>';
        var mainPanelID = '<%= mainPanel.ClientID %>';
        function selectMenu(menuClassName) {
            // 选中当前菜单
            $('#header .topmenu').removeClass('ui-state-active');
            $('#header .topmenu.' + menuClassName).addClass('ui-state-active');

            // 展开树的第一个节点，并选中第一个节点下的第一个子节点（在右侧IFrame中打开）
            var tree = F(leftTreeID);
            var treeFirstChild = tree.getRootNode().children[0];

            // 展开第一个节点（如果想要展开全部节点，调用 tree.expandAll();）
            tree.expandNode(treeFirstChild);

            // 选中第一个链接节点，并在右侧IFrame中打开此链接
            var treeFirstLink = treeFirstChild.children[0];
            tree.selectNode(treeFirstLink);

            // 在主区域内打开链接
            F(mainPanelID).setIFrameUrl(treeFirstLink.href);

            //window.frames['mainframe'].location.href = treeFirstLink.href;

        }

        F.ready(function () {

            selectMenu('menu-mail');
        });
    </script>
</body>
</html>
