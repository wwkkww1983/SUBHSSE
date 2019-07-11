<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportRemind.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.ReportRemind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表上报情况</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="报表上报情况" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ReportRemindId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="ReportRemindId" AllowSorting="true" SortField="CompileDate"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                EnableColumnLines="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True"
                AllowColumnLocking="true" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpProject" runat="server" Label="项目" Width="280px" LabelWidth="50px" EmptyText="请选择项目"
                                EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                             <f:DropDownList ID="drpYear" AutoPostBack="true" runat="server" EnableEdit="true" LabelAlign="Right"
                                Width="150px" LabelWidth="50px" Label="年度" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonth" AutoPostBack="true"  runat="server"  EnableEdit="true" LabelAlign="Right"
                                Width="150px" LabelWidth="50px" Label="月份" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                             <f:DropDownList ID="drpQuarter" AutoPostBack="true" runat="server" EnableEdit="true" LabelAlign="Right"
                                Width="150px" LabelWidth="50px" Label="季度" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:TextBox ID="txtReportName" runat="server" Label="报表" EmptyText="输入报表名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="60px"
                                LabelAlign="Right">
                            </f:TextBox>
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
                    <f:RenderField Width="150px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode"
                        FieldType="String" HeaderText="项目代号"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                        FieldType="String" HeaderText="项目名称" 
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:TemplateField Width="400px" HeaderText="未上报报表情况" HeaderTextAlign="Center" TextAlign="Left" ColumnID="lblReportName"
                        ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblReportName" runat="server" Text='<%# ConvertReportName(Eval("ReportRemindId")) %>'
                                ToolTip='<%# ConvertReportName(Eval("ReportRemindId")) %>'></asp:Label>
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
