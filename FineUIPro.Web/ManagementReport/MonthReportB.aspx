<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportB.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.MonthReportB" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理月报B</title>
    <style type="text/css">
        .labcenter
        {
            text-align: center;
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管理月报B" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HeadMonthReportId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="HeadMonthReportId" AllowSorting="true" SortField="Months"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtMonthReportCode" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="50px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:DatePicker runat="server" Label="选择月份" ID="txtReportDate" LabelWidth="100px" Width="200px"
                                DateFormatString="yyyy-MM" LabelAlign="right">
                            </f:DatePicker>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
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
                    <f:RenderField Width="100px" ColumnID="Months" DataField="Months" SortField="Months"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="月份" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ReportUnitName" DataField="ReportUnitName"
                        SortField="ReportUnitName" FieldType="String" HeaderText="填报单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ReportDate" DataField="ReportDate" SortField="ReportDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="填报日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ReportMan" DataField="ReportMan" SortField="ReportMan"
                        FieldType="String" HeaderText="填报人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfSee" Width="100px" HeaderText="查看" HeaderTextAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSee" runat="server" OnClick="lbtnSee_Click" Text="查看"></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
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
    <f:Window ID="Window1" Title="查看管理月报B" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1300px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true"
                Text="修改" Icon="Pencil" OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
                Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
