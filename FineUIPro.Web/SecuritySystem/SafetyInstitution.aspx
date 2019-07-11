<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyInstitution.aspx.cs"
    Inherits="FineUIPro.Web.SecuritySystem.SafetyInstitution" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>安全制度</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                Layout="HBox" EnableCollapse="true" Width="300" Title="安全制度" TitleToolTip="安全制度"
                ShowBorder="true" ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSafetyInstitution" EnableCollapse="true" ShowHeader="false" ShowBorder="false"
                        EnableIcons="true" Title="安全制度" OnNodeCommand="trSafetyInstitution_NodeCommand"
                        AutoLeafIdentification="true" AutoScroll="true" runat="server" EnableTextSelection="True"
                        EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全制度"
                TitleToolTip="安全制度" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="SafetyInstitutionId" AllowSorting="true"
                        OnSort="Grid1_Sort" SortField="EffectiveDate" SortDirection="DESC" AllowCellEditing="true"
                        ClicksToEdit="2" EnableColumnLines="true" DataIDField="SafetyInstitutionId" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
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
                                    <asp:Label ID="Label1" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="Title" DataField="Title" ExpandUnusedSpace="true"
                                SortField="Title" FieldType="String" HeaderText="制度名称" HeaderTextAlign="Center"
                                TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发布日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="Scope" DataField="Scope" SortField="Scope"
                                FieldType="String" HeaderText="适用范围" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                                FieldType="String" HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:WindowField TextAlign="Left" Width="60px" WindowID="WindowAtt" HeaderText="附件"
                                Text="附件" ToolTip="附件上传查看" DataIFrameUrlFields="SafetyInstitutionId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SafetyInstitutionAttachUrl&menuId=9D04CD8B-575C-4854-B8B0-F90CEEB75815"
                                HeaderTextAlign="Center" />
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑安全制度" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="500px">
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
