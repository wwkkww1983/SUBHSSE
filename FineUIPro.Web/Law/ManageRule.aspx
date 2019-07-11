<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRule.aspx.cs" Inherits="FineUIPro.Web.Law.ManageRule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理规定</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全管理规定" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ManageRuleId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="ManageRuleId" AllowSorting="true"
                SortField="ManageRuleCode" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="txtManageRuleCode" runat="server" Label="文件编号" EmptyText="输入查询文件编号"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtManageRuleName" runat="server" Label="文件名称" EmptyText="输入查询文件名称"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="txtManageRuleTypeName" runat="server" Label="分类" EmptyText="输入查询分类"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true" runat="server">
                            </f:Button> 
                            <f:Button ID="btnUploadResources" ToolTip="上传资源" Icon="SystemNew" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnAuditResources" ToolTip="审核资源" Icon="ZoomIn" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:TemplateField Width="230px" HeaderText="文件编号" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="ManageRuleCode">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleCode" runat="server" Text='<%# Bind("ManageRuleCode") %>'
                                ToolTip='<%#Bind("ManageRuleCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="200px" HeaderText="文件名称" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ManageRuleName">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleName" runat="server" Text='<%# Bind("ManageRuleName") %>'
                                ToolTip='<%#Bind("ManageRuleName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="150px" HeaderText="分类" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="ManageRuleTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblManageRuleTypeName" runat="server" Text='<%# Bind("ManageRuleTypeName") %>'
                                ToolTip='<%#Bind("ManageRuleTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="280px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Left"
                        SortField="Remark" ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("RemarkDef") %>' ToolTip='<%#Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="90px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <%--<f:WindowField TextAlign="Center" Width="120px" WindowID="WindowAtt" HeaderText="附件"
                        Text="附件上传查看" ToolTip="附件上传查看" DataIFrameUrlFields="ManageRuleId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/ManageRule&menuId=56960940-81A8-43D1-9565-C306EC7AFD12"
                         />
                    <f:WindowField TextAlign="Center" Width="60px" WindowID="Window6" HeaderText="内容"
                        Text="编辑" ToolTip="编辑内容" DataIFrameUrlFields="ManageRuleId" DataIFrameUrlFormatString="../ShowDialog/ShowSeeFile.aspx?toKeyId={0}&type=ManageRule"
                        Title="编辑内容"  />--%>
                    <f:RenderField Width="80px" ColumnID="IsBuildName" DataField="IsBuildName" SortField="IsBuildName"
                        FieldType="String" HeaderText="数据来源" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="UpStates" DataField="UpStates" SortField="UpStates"
                        FieldType="String" HeaderText="上报状态" HeaderTextAlign="Center" TextAlign="Left">
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
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑安全管理规定" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="650px">
    </f:Window>
    <f:Window ID="Window2" Title="上传资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window3" Title="审核资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1024px" Height="500px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="350px" Height="180px" EnableAjax="false">
    </f:Window>
    <f:Window ID="Window6" Title="编辑内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="850px" Height="550px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Icon="Delete"
            ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除" Hidden="true">
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
