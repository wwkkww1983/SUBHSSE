<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalFolder.aspx.cs" Inherits="FineUIPro.Web.Personal.PersonalFolder" %>

<!DOCTYPE html>
<html>
<head runat="server">   
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
        
        .f-grid-row .f-grid-cell-inner {
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
                EnableCollapse="true" Width="260" Title="个人文件夹" TitleToolTip="右键点击添加、修改、删除" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="Fit">               
                <Items>
                    <f:Tree ID="tvPersonalFolder" KeepCurrentSelection="true"
                        ShowHeader="false" OnNodeCommand="tvPersonalFolder_NodeCommand" runat="server"
                        ShowBorder="false" EnableSingleClickExpand="true">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="个人文件夹明细"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="PersonalFolderItemId" ClicksToEdit="2"
                        DataIDField="PersonalFolderItemId" AllowSorting="true" SortField="Code" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>                                    
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server"
                                        OnClick="btnNewDetail_Click">
                                    </f:Button>                                   
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                           
                            <f:RenderField ColumnID="Code" DataField="Code" SortField="Code" Width="120px"
                                FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                            <f:RenderField ColumnID="Title" DataField="Title" SortField="Title" Width="200px" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>                                                         
                            <f:RenderField Width="120px" ColumnID="CompileDate" DataField="CompileDate"  Renderer="Date"
                                SortField="CompileDate" HeaderText="编制时间" HeaderTextAlign="Center"  FieldType="Date"
                                TextAlign="Left" >
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
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server"  Text="增加" Icon="Add"
                OnClick="btnMenuNew_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server"  Text="修改" Icon="BulletEdit"
                OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server"  Text="删除" Icon="Delete" ConfirmText="确认删除选中文件夹？"
                OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModifyDetail" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="修改"
                OnClick="btnMenuModifyDetail_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelDetail" EnablePostBack="true" runat="server"  Icon="Delete" Text="删除" ConfirmText="确认删除选中内容？"
                OnClick="btnMenuDelDetail_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Window ID="Window2" Title="设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="600px" Height="240px">
    </f:Window>
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="520px">
    </f:Window>
    </form>
    <script type="text/javascript">
        var treeID = '<%= tvPersonalFolder.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menu2ID = '<%= Menu2.ClientID %>';
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
    </script>
</body>
</html>
