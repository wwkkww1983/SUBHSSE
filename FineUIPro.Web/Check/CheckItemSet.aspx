<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckItemSet.aspx.cs" Inherits="FineUIPro.Web.Check.CheckItemSet" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
        
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="350" Title="检查项目设置" TitleToolTip="右键点击添加、修改、删除"
                ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Center" runat="server">
                        <Items>
                            <f:RadioButtonList ID="ckType" runat="server" AutoPostBack="true" ColumnNumber="3"
                                AutoColumnWidth="true" Width="340px" OnSelectedIndexChanged="ckType_SelectedIndexChanged">
                                <f:RadioItem Value="1" Selected="true" Text="日常检查" />
                                <f:RadioItem Value="2" Text="专项检查" />
                                <f:RadioItem Value="3" Text="综合检查" />
                                <f:RadioItem Value="4" Text="开工前检查" />
                                <f:RadioItem Value="5" Text="季节性/节假日检查" />
                            </f:RadioButtonList>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:TextBox ID="txtCheckItemSet" runat="server" Label="检查项" LabelAlign="Left" LabelWidth="70px" AutoPostBack="true" OnTextChanged="Text_TextChanged" EmptyText="输入查询条件">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnCheckItemExtract" Icon="FolderUp" runat="server" ToolTip="抽取公共资源检查项内容"
                                Hidden="true" OnClick="btnCheckItemExtract_Click" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true"
                                AjaxLoadingMaskText="正在抽取公共资源检查项内容，请稍候">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" runat="server" ToolTip="批量删除检查项内容" ConfirmText="确认删除此类型检查项所有内容？"
                                Hidden="true" OnClick="btnDelete_Click" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true"
                                AjaxLoadingMaskText="正在删除检查项内容，请稍候">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvCheckItemSet" KeepCurrentSelection="true" ShowHeader="false" OnNodeCommand="tvCheckItemSet_NodeCommand"
                        runat="server" ShowBorder="false" EnableSingleClickExpand="true">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="检查项明细"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="CheckItemDetailId" AllowCellEditing="true" ClicksToEdit="2"
                        DataIDField="CheckItemDetailId" AllowSorting="true" SortField="SortIndex" SortDirection="ASC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="12" EnableColumnLines="true"
                        AllowColumnLocking="true" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:HiddenField runat="server" ID="hdSelectId">
                                    </f:HiddenField>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" Hidden="true" runat="server"
                                        OnClick="btnNewDetail_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                    <f:Button ID="btnHelp" runat="server" ToolTip="点击下载本页面使用说明" Text="帮助" Icon="Help">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                                        </Listeners>
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField ColumnID="CheckContent" DataField="CheckContent" SortField="CheckContent"
                                FieldType="String" HeaderText="检查项目内容" HeaderTextAlign="Center" TextAlign="left"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="30" Value="30" />
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Add" Text="增加" OnClick="btnMenuNew_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Pencil" Text="修改" OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Delete" Text="删除" ConfirmText="删除选中检查项？" OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModifyDetail" EnablePostBack="true" runat="server" Hidden="true"
                Text="修改" Icon="Pencil" OnClick="btnMenuModifyDetail_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelDetail" EnablePostBack="true" runat="server" Hidden="true"
                Text="删除" Icon="Delete" ConfirmText="删除选中检查项明细？" OnClick="btnMenuDelDetail_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Window ID="Window2" Title="检查项目设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="600px" Height="240px">
    </f:Window>
    <f:Window ID="Window1" Title="编辑检查项目明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="600px" Height="240px">
    </f:Window>
    </form>
    <script type="text/jscript">
        var treeID = '<%= tvCheckItemSet.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menu2ID = '<%= Menu2.ClientID %>';
        var panelLeftRegionID = '<%= panelLeftRegion.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;

        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        // 点击标题栏工具图标 - 查看源代码
        function onToolSourceCodeClick(event) {
            window.open('../Doc/检查项目设置.doc', '_blank');
        }
    </script>
</body>
</html>
