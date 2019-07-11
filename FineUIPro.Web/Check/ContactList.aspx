<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="FineUIPro.Web.Check.ContactList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>联系单</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="联系单" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ContactListId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="ContactListId" AllowSorting="true"
                SortField="Code" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>       
                             <f:DropDownList ID="drpSponsorUnit" runat="server" Label="发起单位" EnableEdit="true"  EmptyText="请选择查询条件"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" Width="300px" LabelWidth="80px" LabelAlign="right">
                            </f:DropDownList>
                            <f:DropDownList ID="drpReceivingUnits" runat="server" Label="接收单位" EnableEdit="true"  EmptyText="请选择查询条件"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" Width="300px" LabelWidth="80px" LabelAlign="right">
                            </f:DropDownList>                     
                            <%--<f:TextBox runat="server" Label="标题" ID="txtTitle" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>--%>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                runat="server">
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
                    <f:RenderField Width="220px" ColumnID="SponsorUnitName" DataField="SponsorUnitName"
                         SortField="SponsorUnitName" FieldType="String" HeaderText="发起单位"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="ReceivingUnitNames" DataField="ReceivingUnitNames"
                        SortField="ReceivingUnitNames" FieldType="String" HeaderText="接收单位" HeaderTextAlign="Center"
                        TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate"
                        SortField="CompileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="整理日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="Scope" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Remark" DataField="Remark"
                        SortField="Remark" FieldType="String" HeaderText="摘要" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="60px" WindowID="WindowAtt" HeaderText="附件" Text="附件" ToolTip="附件上传查看"
                        DataIFrameUrlFields="ContactListId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ContactListAttachUrl&menuId=9D04CD8B-575C-4854-B8B0-F90CEEB75815"
                        HeaderTextAlign="Center" />
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
    <f:Window ID="Window1" Title="联系单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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
