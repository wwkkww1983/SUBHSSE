<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyTraining.aspx.cs"
    Inherits="FineUIPro.Web.EduTrain.CompanyTraining" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>公司教材库</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="公司教材库" TitleToolTip="公司教材库" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" runat="server" OnClick="btnNew_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" Icon="Pencil" runat="server" OnClick="btnEdit_Click" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                    <f:Tree ID="trCompanyTraining" EnableCollapse="true" ShowHeader="true" Title="公司教材库"
                        OnNodeCommand="trCompanyTraining_NodeCommand" AutoLeafIdentification="true" runat="server"
                        EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="公司教材明细"
                TitleToolTip="公司教材明细" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="CompanyTrainingItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="CompanyTrainingItemId" AllowSorting="true" SortField="CompanyTrainingItemCode"
                        SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" EnableColumnLines="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="txtCompanyTrainingItemCode" runat="server" Label="教材编号" EmptyText="输入查询教材编号"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="txtCompanyTrainingItemName" runat="server" Label="教材名称" EmptyText="输入查询教材名称"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                        LabelAlign="Right">
                                    </f:TextBox>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnEditDetail" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEditDetail_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnDeleteDetail" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                        OnClick="btnDeleteDetail_Click" runat="server" Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnSelectColumns" runat="server" ToolTip="导出" Icon="FolderUp" EnablePostBack="false"
                                        Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:TemplateField Width="120px" HeaderText="教材编号" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="CompanyTrainingItemCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyTrainingItemCode" runat="server" Text='<%# Bind("CompanyTrainingItemCode") %>'
                                        ToolTip='<%#Bind("CompanyTrainingItemCode") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="300px" HeaderText="教材名称" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="CompanyTrainingItemName" ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyTrainingItemName" runat="server" Text='<%# Bind("CompanyTrainingItemName") %>'
                                        ToolTip='<%#Bind("CompanyTrainingItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="CompileMan">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompileMan" runat="server" Text='<%# Bind("CompileMan") %>' ToolTip='<%#Bind("CompileMan") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="120px" HeaderText="整理时间" HeaderTextAlign="Center" TextAlign="Left"
                                SortField="CompileDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompileDate" runat="server" Text='<%# Bind("CompileDate","{0:yyyy-MM-dd}") %>'
                                        ToolTip='<%#Bind("CompileDate") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:WindowField TextAlign="Left" Width="80px" WindowID="WindowAtt" HeaderText="附件"
                                Text="查看" ToolTip="附件上传查看" DataIFrameUrlFields="CompanyTrainingItemId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Training&menuId=9D4F76A1-CD2E-4E66-B833-49425CD879EB" />
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="公司教材库类别" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="400px" Height="200px">
    </f:Window>
    <f:Window ID="Window2" Title="公司教材库详情" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="500px" Height="280px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Text="编辑" Hidden="true" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
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
