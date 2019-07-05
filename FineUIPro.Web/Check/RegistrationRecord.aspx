<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationRecord.aspx.cs" Inherits="FineUIPro.Web.Check.RegistrationRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>隐患巡检记录</title>
    <style type="text/css">
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="隐患巡检记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RegistrationRecordId" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="RegistrationRecordId" AllowSorting="true" SortField="CheckDate" SortDirection="DESC"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True" AllowColumnLocking="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <%--<f:Button ID="btnNew" Text="编制" Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>--%>
                            <f:DropDownList ID="drpCheckMan" runat="server" Label="巡检人" Width="200px" LabelWidth="60px" EmptyText="请选择巡检人"
                                EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="巡检日期" ID="txtDate"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="200px" LabelWidth="70px">
                            </f:DatePicker>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField Width="100px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                        EnableLock="true" Locked="true">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="400px" ColumnID="Date" DataField="Date" SortField="Date" FieldType="String" ExpandUnusedSpace="true"
                        HeaderText="巡检日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="400px" ColumnID="CheckPersonMan" DataField="CheckPersonMan" SortField="CheckPersonMan" FieldType="String"
                        HeaderText="巡检人" HeaderTextAlign="Center" TextAlign="Center">
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="隐患巡检记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1300px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
    <f:MenuButton ID="btnMenuSee" EnablePostBack="true" runat="server" Text="查看" Icon="Pencil"
                OnClick="btnMenuSee_Click">
            </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Text="删除" Icon="Delete"
                OnClick="btnMenuDelete_Click" Hidden="true">
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
