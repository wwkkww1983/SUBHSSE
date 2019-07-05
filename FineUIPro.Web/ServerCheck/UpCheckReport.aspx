<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpCheckReport.aspx.cs"
    Inherits="FineUIPro.Web.ServerCheck.UpCheckReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业上报监督检查报告</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }   
        .f-grid-row.yellow
        {
            background-color: YellowGreen;
            background-image: none;
        }
        
        .f-grid-row.red
        {
            background-color: Yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业上报监督检查报告" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UpCheckReportId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="UpCheckReportId" AllowSorting="true" SortField="CheckStartTime"
                SortDirection="DESC" OnSort="Grid1_Sort"  EnableColumnLines="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" ToolTip="新增" Hidden="true" Icon="Add" runat="server" 
                                OnClick="btnNew_Click">
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
                    <f:RenderField Width="100px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" TextAlign="Left" HeaderTextAlign="Center" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CheckStartTime" DataField="CheckStartTime" SortField="CheckStartTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查开始日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CheckEndTime" DataField="CheckEndTime" SortField="CheckEndTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查结束日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>                    
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="填报日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="AuditDate" DataField="AuditDate" SortField="AuditDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="审核日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="UpStateName" DataField="UpStateName" SortField="UpStateName" FieldType="String"
                        HeaderText="上报状态" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="UpDateTime" DataField="UpDateTime" SortField="UpDateTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="上报日期"
                        HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" runat="server" Hidden="true" ShowHeader="false"
        IsModal="true" Target="Top" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="编辑企业上报监督检查报告" EnableIFrame="true" Height="560px"
        Width="1180px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">      
        <f:MenuButton ID="btnMenuEdit" OnClick="btnEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit" >
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnDelete_Click" EnablePostBack="true"  Icon="Delete"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除">
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
