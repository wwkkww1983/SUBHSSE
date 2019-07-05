<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRuleAudit.aspx.cs"
    Inherits="FineUIPro.Web.Law.ManageRuleAudit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理规定 资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管理规定" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ManageRuleId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="ManageRuleId" AllowSorting="true"
                SortField="CompileDate" SortDirection="ASC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand"
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="100px" ColumnID="ManageRuleCode" DataField="ManageRuleCode"
                        SortField="ManageRuleCode" FieldType="String" HeaderText="文件编号" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ManageRuleName" DataField="ManageRuleName"
                        EnableFilter="true" SortField="ManageRuleName" FieldType="String" HeaderText="文件名称"
                        HeaderTextAlign="Center" TextAlign="Center">
                        <Filter EnableMultiFilter="true" ShowMatcher="true">
                            <Operator>
                                <f:DropDownList ID="DropDownList1" runat="server">
                                    <f:ListItem Text="等于" Value="equal" />
                                    <f:ListItem Text="包含" Value="contain" Selected="true" />
                                </f:DropDownList>
                            </Operator>
                        </Filter>
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ManageRuleTypeName" DataField="ManageRuleTypeName"
                        SortField="ManageRuleTypeName" FieldType="String" HeaderText="分类" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <%--<f:RenderField Width="300px" ColumnID="Remark" DataField="Remark" SortField="Remark"
                        FieldType="String" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>--%>
                    <f:TemplateField Width="300px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("RemarkDef") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <%--<f:WindowField TextAlign="Center" Width="120px" WindowID="WindowAtt" HeaderText="附件"
                        Text="附件上传查看" ToolTip="附件上传查看" DataIFrameUrlFields="ManageRuleId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId=56960940-81A8-43D1-9565-C306EC7AFD12"
                         />--%>
                    <f:RenderField Width="90px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="管理规定" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="380px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnPass" OnClick="btnPass_Click" EnablePostBack="true" runat="server"
            Text="采用" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnUpPass" OnClick="btnUpPass_Click" EnablePostBack="true" runat="server"
            Text="采用并上报" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnNoPass" OnClick="btnNoPass_Click" EnablePostBack="true" runat="server"
            Text="不采用" Hidden="true">
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
