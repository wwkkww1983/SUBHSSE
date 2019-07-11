<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentStatisc.aspx.cs" Inherits="FineUIPro.Web.SafetyData.AccidentStatisc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理台账</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管理台账" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="AccidentReportId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="AccidentReportId" AllowSorting="true" SortField="AccidentDate"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                EnableColumnLines="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True"
                AllowColumnLocking="true" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="查询日期" ID="txtDate" LabelWidth="100px" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged"
                                Width="200px" LabelAlign="right">
                            </f:DatePicker>
                            <f:DropDownList ID="drpProject" runat="server" Label="项目" Width="300px" LabelWidth="60px" EmptyText="请选择项目"
                                 EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpAccidentTypeId" runat="server" Label="事故类型" LabelAlign="Right"
                                    EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                                    </f:DropDownList>
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
                    <f:RenderField Width="100px" ColumnID="AccidentDate" DataField="AccidentDate" SortField="AccidentDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="事故日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                        FieldType="String" HeaderText="项目名称" ExpandUnusedSpace="true"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="AccidentTypeName" DataField="AccidentTypeName"
                        SortField="AccidentTypeName" FieldType="String" HeaderText="事故类型" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="200px" HeaderText="提要" HeaderTextAlign="Center" TextAlign="Center" ColumnID="Abstract">
                                <ItemTemplate>
                                    <asp:Label ID="lblAbstract" runat="server" Text='<%# ConvertAbstract(Eval("Abstract")) %>' ToolTip='<%# Bind("Abstract") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="WorkingHoursLoss" DataField="WorkingHoursLoss"
                        SortField="WorkingHoursLoss" FieldType="String" HeaderText="工时损失" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="MoneyLoss" DataField="MoneyLoss"
                        SortField="MoneyLoss" FieldType="String" HeaderText="经济损失" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                </Columns>
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
    <f:Window ID="Window1" Title="事故查看页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="650px">
    </f:Window>
    </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
