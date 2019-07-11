<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishItem.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.PunishItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处罚项信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="处罚项信息" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="PunishItemId" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="PunishItemId" AllowSorting="true" SortField="PunishItemCode" SortDirection="ASC"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Text="新增" Icon="Add" EnablePostBack="false"  runat="server">
                            </f:Button>
                            <f:Button ID="btnEdit" Text="编辑" Icon="Pencil" runat="server"  OnClick="btnEdit_Click">
                            </f:Button>
                            <f:Button ID="btnDelete" Text="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                 runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                   <f:RenderField Width="150px" ColumnID="PunishItemCode" DataField="PunishItemCode" SortField="PunishItemCode"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="PunishItemContent" DataField="PunishItemContent" SortField="PunishItemContent"
                        FieldType="String" HeaderText="处罚内容" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Center" ExpandUnusedSpace="true">
                        <Filter EnableMultiFilter="true" ShowMatcher="true">
                            <Operator>
                                <f:DropDownList ID="DropDownList1" runat="server">
                                    <f:ListItem Text="等于" Value="equal" />
                                    <f:ListItem Text="包含" Value="contain" Selected="true" />
                                </f:DropDownList>
                            </Operator>
                        </Filter>
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="PunishItemType" DataField="PunishItemType" SortField="PunishItemType"
                        FieldType="String" HeaderText="类型" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="Deduction" DataField="Deduction" SortField="Deduction"
                        FieldType="String" HeaderText="扣分" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PunishMoney" DataField="PunishMoney" SortField="PunishMoney"
                        FieldType="String" HeaderText="罚款" HeaderTextAlign="Center" TextAlign="Center">
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
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑处罚项信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="330px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
