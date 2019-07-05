<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuperviseCheckReport.aspx.cs"
    Inherits="FineUIPro.Web.Supervise.SuperviseCheckReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全监督检查报告</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全监督检查报告" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SuperviseCheckReportId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="SuperviseCheckReportId" AllowSorting="true" SortField="SuperviseCheckReportCode"
                SortDirection="DESC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" EnableColumnLines="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEdit_Click"
                                Hidden="true">
                            </f:Button>
                             <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                Hidden="true" runat="server">
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
                    <f:RenderField Width="100px" ColumnID="SuperviseCheckReportCode" DataField="SuperviseCheckReportCode"
                        SortField="SuperviseCheckReportCode" FieldType="String" HeaderText="检查编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:GroupField HeaderText="检查对象" TextAlign="Left" HeaderTextAlign="Center">
                        <Columns>
                            <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="单位" HeaderToolTip="检查单位" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="210px" ColumnID="ProjectName" DataField="ProjectName" FieldType="String"
                                HeaderText="项目" HeaderToolTip="检查项目" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="250px" ColumnID="CheckTeam" DataField="CheckTeam" SortField="CheckTeam"
                        FieldType="String" HeaderText="检查组/人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfRectify" Width="100px" HeaderText="检查整改" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRectify" runat="server" Text="整改单" CommandArgument='<%#Bind("SuperviseCheckReportId") %>'
                                OnClick="lbtnRectify_Click"></asp:LinkButton>
                            <asp:Label runat="server" ID="lblRectify"></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfSubRec" Width="90px" HeaderText="检查报告" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSubRec" runat="server" Text="报告单" CommandArgument='<%#Bind("SuperviseCheckReportId") %>'
                                OnClick="lbtnSubRec_Click"></asp:LinkButton>
                            <asp:Label runat="server" ID="lblSubRec"></asp:Label>
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
    <f:Window ID="Window1" Title="编辑安全监督检查整改" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1300px" Height="500px">
    </f:Window>
    <f:Window ID="Window2" Title="编辑安全监督检查报告" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="600px" OnClose="Window2_Close1">
    </f:Window>
    </form>
    <script type="text/javascript">

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }        
    </script>
</body>
</html>
