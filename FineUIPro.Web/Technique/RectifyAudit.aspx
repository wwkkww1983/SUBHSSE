<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyAudit.aspx.cs" Async="true"
    Inherits="FineUIPro.Web.Technique.RectifyAudit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全隐患资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全隐患" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="RectifyItemId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="RectifyItemId" AllowSorting="true"
                SortField="CompileDate" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True">
                <Columns>
                  <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="120px" ColumnID="RectifyName" DataField="RectifyName" SortField="RectifyName"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="作业类别">
                    </f:RenderField>
                    <f:TemplateField Width="180px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("HazardSourcePoint") %>' ToolTip='<%#Bind("HazardSourcePoint") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="风险分析" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskAnalysis") %>' ToolTip='<%#Bind("RiskAnalysis") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="风险防范" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("RiskPrevention") %>' ToolTip='<%#Bind("RiskPrevention") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="同类风险" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("SimilarRisk") %>' ToolTip='<%#Bind("SimilarRisk") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="安全隐患" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="380px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnPass" OnClick="btnPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用">
        </f:MenuButton>
        <f:MenuButton ID="btnUpPass" OnClick="btnUpPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用并上报">
        </f:MenuButton>
        <f:MenuButton ID="btnNoPass" OnClick="btnNoPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="不采用">
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
    </script>
</body>
</html>
