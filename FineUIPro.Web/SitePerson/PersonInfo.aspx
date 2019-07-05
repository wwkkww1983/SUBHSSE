<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInfo.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonInfo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场人员考勤管理</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="现场人员考勤管理" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckingId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="CheckingId" AllowSorting="true" SortField="IntoOutTime"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableColumnLines="true" EnableTextSelection="True"
                OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="增加" Icon="Add" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField> 
                    <f:RenderField Width="230px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center" ExpandUnusedSpace="true"
                        TextAlign="Left">
                    </f:RenderField>
                      <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:TemplateField ColumnID="tfIntoOut" Width="70px" HeaderText="进出" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIntoOut" runat="server" Text='<%# ConvertIntoOut(Eval("IntoOut")) %>'
                                ToolTip='<%#ConvertIntoOut(Eval("IntoOut")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="140px" ColumnID="IntoOutTime" DataField="IntoOutTime" SortField="IntoOutTime"
                        HeaderText="出入现场时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfAddress" Width="130px" HeaderText="进出地点" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>' ToolTip='<%#Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfWorkAreaName" Width="120px" HeaderText="作业区域" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkAreaName" runat="server" Text='<%# Bind("WorkAreaName") %>' ToolTip='<%#Bind("WorkAreaName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                        FieldType="String" HeaderText="卡号" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfIdentityCard" Width="160px" HeaderText="身份证号" HeaderTextAlign="Center" TextAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblIdentityCard" runat="server" Text='<%# Bind("IdentityCard") %>' ToolTip='<%#Bind("IdentityCard") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                     
                </Columns>
                <Listeners>
                      <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    <f:Window ID="Window1" Title="编辑考勤" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="950px" Height="450px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" runat="server" Text="删除">
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

        function onGridDataLoad(event) {
            this.mergeColumns(['UnitName']);            
        }
    </script>
</body>
</html>
