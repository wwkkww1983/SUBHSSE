<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APPFile.aspx.cs" Inherits="FineUIPro.Web.Personal.APPFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>手机APP上传内容</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="手机APP上传内容" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="attach_image_id" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="attach_image_id" AllowSorting="true" SortField="created_date"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand"
                AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="文件系列" ID="txtimage_series" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="200px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="文件名称" ID="txtfile_name" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="250px">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartcreated_date" runat="server" Label="创建时间" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px">
                            </f:DatePicker>
                            <f:Label ID="Label1" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndcreated_date" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="100px">
                            </f:DatePicker>
                            <f:TextBox ID="txtcreated_by" runat="server" Label="创建人" LabelAlign="Right" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" EmptyText="输入查询条件" LabelWidth="80px">
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
                    <f:RenderField Width="150px" ColumnID="image_series" DataField="image_series" SortField="image_series"
                        FieldType="String" HeaderText="文件系列" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="file_name" DataField="file_name"
                        SortField="file_name" FieldType="String" HeaderText="文件名称" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="file_type" DataField="file_type"
                        SortField="file_type" FieldType="String" HeaderText="文件类型" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="created_date" DataField="created_date"
                        SortField="created_date" FieldType="Date" Renderer="Date" HeaderText="创建时间"
                        TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="created_byManName" DataField="created_byManName" SortField="created_byManName"
                        FieldType="String" HeaderText="创建人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="series_desc" DataField="series_desc" SortField="series_desc" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="文件系列描述" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfImageUrl" Width="150px" HeaderText="文件" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnImageUrl" runat="server" Text='<%# ConvertImageUrl(Eval("attach_image_id")) %>'></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField Width="80px" HeaderText="删除" ConfirmText="确定要删除此条信息吗？" ConfirmTarget="Parent"
                        CommandName="del" TextAlign="Center" ToolTip="删除" Icon="Delete" />
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
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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
