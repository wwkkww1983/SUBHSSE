<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentData.aspx.cs" Inherits="FineUIPro.Web.ProjectAccident.AccidentData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>事故台账</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="事故台账" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="AccidentDetailSortId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="AccidentDetailSortId" AllowSorting="true" SortField="AccidentDate"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                EnableColumnLines="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True"
                AllowColumnLocking="true" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="查询日期" ID="txtDate" LabelWidth="100px" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged"
                                Width="200px" LabelAlign="right">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="95px" ColumnID="AccidentDate" DataField="AccidentDate" SortField="AccidentDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发生时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField Width="200px" HeaderText="项目名称" HeaderTextAlign="Center" TextAlign="Center" ColumnID="ProjectName"
                                ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# ConvertProjectName(Eval("MonthReportId")) %>'
                                        ToolTip='<%# ConvertProjectName(Eval("MonthReportId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="90px" HeaderText="项目经理" HeaderTextAlign="Center" TextAlign="Center" ColumnID="ProjectManagerName"
                                ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectManagerName" runat="server" Text='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'
                                        ToolTip='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="Abstract" DataField="Abstract" SortField="Abstract"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="提要" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="AccidentType" DataField="AccidentType" SortField="AccidentType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="事故类型">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="PeopleNum" DataField="PeopleNum" SortField="PeopleNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人数">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="WorkingHoursLoss" DataField="WorkingHoursLoss" SortField="WorkingHoursLoss"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="工时损失">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="EconomicLoss" DataField="EconomicLoss" SortField="EconomicLoss"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="经济损失">
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
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true"
            Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？" OnClick="btnMenuDel_Click">
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
