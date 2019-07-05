<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="FineUIPro.Web.Check.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>隐患巡检（手机端）</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="隐患巡检（手机端）" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RegistrationId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="RegistrationId" AllowSorting="true" SortField="CheckTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="250px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="责任单位" ID="txtResponsibilityUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="250px">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartRectificationTime" runat="server" Label="整改时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndRectificationTime" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"
                                Width="100px">
                            </f:DatePicker>
                            <f:TextBox ID="txtStates" runat="server" Label="状态" LabelAlign="Right" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" EmptyText="输入查询条件">
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
                    <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="85px" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                        FieldType="String" HeaderText="区域" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="ResponsibilityUnitName" DataField="ResponsibilityUnitName"
                        SortField="ResponsibilityUnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="75px" ColumnID="ProblemTypes" DataField="ProblemTypes" SortField="ProblemTypes"
                        FieldType="String" HeaderText="问题类型" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ProblemDescription" DataField="ProblemDescription"
                        SortField="ProblemDescription" FieldType="String" HeaderText="问题描述" TextAlign="Left"
                        HeaderTextAlign="Center" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="TakeSteps" DataField="TakeSteps" SortField="TakeSteps"
                        FieldType="String" HeaderText="采取措施" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RectificationPeriod" DataField="RectificationPeriod"
                        SortField="RectificationPeriod" FieldType="Date" Renderer="Date" HeaderText="整改期限"
                        TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="95px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        HeaderText="检查时间" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RectificationTime" DataField="RectificationTime"
                        SortField="RectificationTime" HeaderText="整改时间" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="65px" ColumnID="States" DataField="States" SortField="States"
                        FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfImageUrl1" Width="100px" HeaderText="整改前" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("RegistrationId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="100px" HeaderText="整改后" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("RegistrationId")) %>'></asp:Label>
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
    <f:Window ID="Window1" Title="隐患巡检（手机端）" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="900px" Height="520px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuDelete" EnablePostBack="true" runat="server" Text="删除" Icon="Delete"
                OnClick="btnMenuDelete_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuSee" EnablePostBack="true" runat="server" Text="查看" Icon="Pencil"
                OnClick="btnMenuSee_Click">
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
