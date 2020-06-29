﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNotices.aspx.cs"
    Inherits="FineUIPro.Web.Check.RectifyNotices" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
    <title>隐患整改通知单</title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
            ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
            <Items>
                <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="隐患整改通知单" EnableCollapse="true"
                    runat="server" BoxFlex="1" DataKeyNames="RectifyNoticesId" AllowCellEditing="true"
                    ClicksToEdit="2" DataIDField="RectifyNoticesId" AllowSorting="true" SortField="RectifyNoticesCode"
                    SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                    IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                    EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                    OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="50px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"
                                    LabelAlign="right">
                                </f:TextBox>
                                <f:TextBox runat="server" Label="编号" ID="txtRectifyNoticesCode" EmptyText="输入查询条件" Width="200px"
                                    AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="50px" LabelAlign="right">
                                </f:TextBox>

                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                            <Items>
                                <f:RadioButtonList runat="server" ID="rbStates" Width="450px" LabelWidth="60px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                    <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                    <f:RadioItem Text="待提交" Value="0" />
                                    <f:RadioItem Text="待签发" Value="1" />
                                    <f:RadioItem Text="待整改" Value="2" />
                                    <f:RadioItem Text="待审核" Value="3" />
                                    <f:RadioItem Text="待复审" Value="4" />
                                    <f:RadioItem Text="已完成" Value="5" />
                                </f:RadioButtonList>
                                <f:ToolbarSeparator ID="ToolbarFill2" runat="server">
                                </f:ToolbarSeparator>
                                <f:RadioButtonList runat="server" ID="rbrbHiddenHazardType" Width="280px" Label="隐患类别" LabelWidth="80px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rbStates_SelectedIndexChanged">
                                    <f:RadioItem Text="全部" Value="-1" Selected="true" />
                                    <f:RadioItem Text="一般" Value="1" />
                                    <f:RadioItem Text="较大" Value="2" />
                                    <f:RadioItem Text="重大" Value="3" />
                                </f:RadioButtonList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                    Hidden="true">
                                </f:Button>
                                <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                    OnClick="btnPrint_Click">
                                </f:Button>
                                <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                    EnableAjax="false" DisableControlBeforePostBack="false">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RenderField Width="100px" ColumnID="RectifyNoticesCode" DataField="RectifyNoticesCode"
                            SortField="RectifyNoticesCode" FieldType="String" HeaderText="整改单号" TextAlign="Left"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="110px" ColumnID="WorkAreaName" DataField="WorkAreaName"
                            SortField="WorkAreaName" FieldType="String" HeaderText="单位工程名称" TextAlign="Left"
                            HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="240px" ColumnID="UnitName" DataField="UnitName" ExpandUnusedSpace="true"
                            SortField="UnitName" FieldType="String" HeaderText="受检单位名称" TextAlign="Left" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:TemplateField ColumnID="CheckManNames" Width="200px" HeaderText="检查人" HeaderTextAlign="Center" TextAlign="Left"
                            EnableLock="true" Locked="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertCheckPerson(Eval("CheckManIds")) %>'></asp:Label>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:RenderField Width="100px" ColumnID="CheckedDate" DataField="CheckedDate" SortField="CheckedDate"
                            FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查日期"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="90px" ColumnID="DutyPersonName" DataField="DutyPersonName"
                            SortField="DutyPersonName" FieldType="String" HeaderText="接收人"
                            TextAlign="Left" HeaderTextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="145px" ColumnID="DutyPersonTime" DataField="DutyPersonTime" SortField="DutyPersonTime"
                            FieldType="Date" Renderer="Date" HeaderText="接收日期"
                            HeaderTextAlign="Center" TextAlign="Center">
                        </f:RenderField>
                        <f:RenderField Width="130px" ColumnID="StatesName" DataField="StatesName"
                            SortField="StatesName" FieldType="String" HeaderText="状态"
                            TextAlign="Left" HeaderTextAlign="Center">
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
        <f:Window ID="Window1" Title="编辑隐患整改通知单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
            Width="1200px" Height="600px">
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
