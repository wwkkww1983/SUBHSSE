<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView.aspx.cs"
    Inherits="FineUIPro.Web.Manager.MonthReportCView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看月报C</title>
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
                        <f:TextBox ID="txtMonthReportCode" runat="server" Label="编号" Readonly="true" MaxLength="50" LabelAlign="right" Width="250px" LabelWidth="100px">
                    </f:TextBox>
                    <f:Label ID="txtReportMonths" runat="server" Label="月报月份" Width="250px" LabelAlign="right" LabelWidth="140px">
                    </f:Label>
                    <f:Label ID="txtMonthReportDate" runat="server" Label="报告日期" Width="250px" LabelAlign="right" LabelWidth="140px">
                    </f:Label>
                    <f:TextBox ID="txtReportMan" runat="server" Label="报告人" Readonly="true" LabelAlign="right" Width="250px" LabelWidth="100px">
                    </f:TextBox>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
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
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
     <f:Window ID="Window1" Title="导出HSE月报告" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"  CloseAction="HidePostBack"
        Width="1300px" Height="600px">
    </f:Window>
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
            //            var treeFirstChild = tree.nodes[0];

            // 展开第一个节点（如果想要展开全部节点，调用 tree.expandAll();）
            tree.expandNode(treeFirstChild);

            // 选中第一个链接节点，并在右侧IFrame中打开此链接
            //            var treeFirstLink = treeFirstChild.children[0];
            var treeFirstLink = treeFirstChild;
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
<script type="text/javascript">
    function onGridAfterEdit(event, value, params) {
        updateSummary();
    }

    function updateSummary() {
        // 回发到后台更新
        __doPostBack('', 'UPDATE_SUMMARY');
    }
</script>