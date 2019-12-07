<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckSpecial.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.CheckSpecial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专项检查</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专项检查" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="CheckSpecialId" PageSize="10" EnableColumnLines="true"
                AllowPaging="true" IsDatabasePaging="true" Width="1110px" DataIDField="CheckSpecialId"
                OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True" AllowColumnLocking="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                           
                            <f:TextBox ID="txtCheckSpecialCode" runat="server" Label="编号" EmptyText="按专项检查编号查询"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelWidth="60px">
                            </f:TextBox>
                            <f:HiddenField runat="server" ID="hdRemark">
                            </f:HiddenField>
                             <f:ToolbarFill runat="server"></f:ToolbarFill>
                             <f:Button ID="btnNew" ToolTip="编制" Icon="Add" runat="server" OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                        EnableLock="true" Locked="true">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField Width="200px" HeaderText="专项检查编号" ConfirmTarget="Parent" CommandName="click"
                        TextAlign="Center" DataTextField="CheckSpecialCode" EnableLock="true" Locked="true" />
                    <%--<f:RenderField Width="300px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="受检单位" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>--%>
                    <f:RenderField Width="100px" ColumnID="Date" DataField="Date" SortField="Date" FieldType="Date"
                        Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="日期" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="CheckMan" DataField="CheckMan" SortField="CheckMan"
                        FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="JointCheckMan" DataField="JointCheckMan" SortField="JointCheckMan"
                        FieldType="String" HeaderText="陪检人员" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField Width="100px" HeaderText="编制人" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# ConvertCompileManName(Eval("CheckSpecialId")) %>'
                                ToolTip='<%# ConvertCompileManName(Eval("CheckSpecialId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="120px" HeaderText="办理人" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("AuditManName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="150px" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("StateStr") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField Width="90px" HeaderText="详细" ConfirmTarget="Parent" CommandName="particular"
                        TextAlign="Center" ToolTip="查看详细信息" Text="查看" />
                    <%--<f:LinkButtonField Width="90px" HeaderText="打印" CommandName="print" TextAlign="Center"
                        ToolTip="打印APP专项检查" Icon="Printer" />--%>
                    <f:LinkButtonField Width="90px" HeaderText="删除" ConfirmText="确定要删除此条信息吗？" ConfirmTarget="Parent"
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
    <f:Window ID="Window1" Title="专项检查" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="检查小结" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="600px" Height="300px">
    </f:Window>
    <f:Window ID="Window3" Title="专项检查" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="600px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/javascript">
        function renderIsConfirm(value) {
            var returnValue = null;
            if (value == 'True') {
                returnValue = '是';
            } else {
                returnValue = '否';
            }
            return returnValue;
        }

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
