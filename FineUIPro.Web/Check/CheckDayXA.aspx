<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckDayXA.aspx.cs" Inherits="FineUIPro.Web.Check.CheckDayXA" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>日常巡检（记录）</title>
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
        
        .f-grid-row.yellow
        {
            background-color: yellow;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="日常巡检（记录）" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckDayId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="CheckDayId" AllowSorting="true" SortField="CheckDate"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <%--<f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="60px"
                                LabelAlign="right">
                            </f:TextBox>--%>
                            <f:TextBox runat="server" Label="编号" ID="txtCheckDayCode" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="50px" LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnit" runat="server" Label="单位" Width="300px" LabelWidth="60px" EmptyText="请选择单位"
                                EnableEdit="true" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
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
                    <f:RenderField Width="100px" ColumnID="CheckDayCode" DataField="CheckDayCode" SortField="CheckDayCode"
                        FieldType="String" HeaderText="检查编号" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="250px" HeaderText="受检单位/班组" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertUnitOrTeamGroup(Eval("CheckDayId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="130px" HeaderText="检查区域" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertWorkArea(Eval("WorkAreaIds")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="NotOKNum" DataField="NotOKNum" SortField="NotOKNum"
                        FieldType="String" HeaderText="不合格数" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="200px" HeaderText="不合格项描述" HeaderTextAlign="Center" TextAlign="Center"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkStage" runat="server" Text='<%# ConvertUnqualified(Eval("Unqualified")) %>' ToolTip='<%# Bind("Unqualified") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="CheckPersonName" DataField="CheckPersonName" SortField="CheckPersonName"
                        FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <%--<f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    <f:LinkButtonField Width="100px" HeaderText="整改情况" ConfirmTarget="Parent" CommandName="Handle"
                        TextAlign="Center" ToolTip="整改情况" Text="整改情况"/>
                        <f:TemplateField Width="200px" HeaderText="整改情况" HeaderTextAlign="Center" TextAlign="Center"
                                >
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertHandleStation(Eval("HandleStation")) %>' ToolTip='<%# Bind("HandleStation") %>'></asp:Label>
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
    <f:Window ID="Window1" Title="编辑日常巡检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1200px" Height="460px">
    </f:Window>
    <f:Window ID="Window2" Title="编辑整改情况" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window2_Close"
        Width="600px" Height="260px">
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
