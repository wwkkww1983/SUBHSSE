<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckInfoReport.aspx.cs"
    Inherits="FineUIPro.Web.ServerCheck.CheckInfoReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>监督检查报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全监督检查整改" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckInfoId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="CheckInfoId" AllowSorting="true" SortField="CheckStartTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
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
                    <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="受检单位" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="260px" ColumnID="SubjectObject" DataField="SubjectObject" SortField="SubjectObject"
                        FieldType="String" HeaderText="受检对象" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckStartTime" DataField="CheckStartTime"
                        SortField="CheckStartTime" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="开始时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckEndTime" DataField="CheckEndTime" SortField="CheckEndTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="结束时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="SubjectUnitAdd" DataField="SubjectUnitAdd"
                        SortField="SubjectUnitAdd" FieldType="String" HeaderText="单位地址" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SubjectUnitMan" DataField="SubjectUnitMan"
                        SortField="SubjectUnitMan" FieldType="String" HeaderText="单位负责人" TextAlign="Left"
                        HeaderTextAlign="Center">
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
    <f:Window ID="Window1" runat="server" Hidden="true" ShowHeader="true" IsModal="false"
        Target="Parent" EnableMaximize="true" EnableResize="true" Title="安全监督检查报告查看"
        EnableIFrame="true" Height="560px" Width="1100px">
    </f:Window>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //  F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
