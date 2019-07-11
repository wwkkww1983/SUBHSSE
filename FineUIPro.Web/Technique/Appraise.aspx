<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appraise.aspx.cs" Inherits="FineUIPro.Web.Technique.Appraise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全评价</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全评价" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="AppraiseId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="AppraiseId" AllowSorting="true" SortField="AppraiseCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="AppraiseTitle" runat="server" Label="标题" EmptyText="输入查询标题" Width="250px" LabelWidth="60px" LabelAlign="Right">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                OnClick="TextBox_TextChanged" runat="server" >
                            </f:Button>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnEdit" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEdit_Click"
                                Hidden="true">
                            </f:Button>
                             <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                Hidden="true" runat="server">
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
                    <f:TemplateField Width="80px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="AppraiseCode">
                        <ItemTemplate>
                            <asp:Label ID="lblAppraiseCode" runat="server" Text='<%# Bind("AppraiseCode") %>'
                                ToolTip='<%#Bind("AppraiseCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="300px" HeaderText="标题" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="AppraiseTitle">
                        <ItemTemplate>
                            <asp:Label ID="lblAppraiseTitle" runat="server" Text='<%# Bind("AppraiseTitle") %>'
                                ToolTip='<%#Bind("AppraiseTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="250px" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="Abstract">
                        <ItemTemplate>
                            <asp:Label ID="lblAbstract" runat="server" Text='<%# Bind("Abstract") %>' ToolTip='<%#Bind("Abstract") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="AppraiseDate" DataField="AppraiseDate" SortField="AppraiseDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="评价时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ArrangementPerson" DataField="ArrangementPerson"
                        SortField="ArrangementPerson" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="ArrangementDate" DataField="ArrangementDate"
                        SortField="ArrangementDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="整理时间" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="120px" WindowID="WindowAtt" Text="附件上传查看"
                        ToolTip="附件上传查看" DataIFrameUrlFields="AppraiseId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Appraise&menuId=0ADD01FB-8088-4595-BB40-6A73F332A229"
                         />
                    <f:RenderField Width="90px" ColumnID="IsBuildName" DataField="IsBuildName" SortField="IsBuildName"
                        FieldType="String" HeaderText="数据来源" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="UpStates" DataField="UpStates" SortField="UpStates"
                        FieldType="String" HeaderText="上报状态" HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="编辑安全评价" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="700px" Height="320px">
    </f:Window>
    <f:Window ID="Window6" Title="请点击下方保存下载附件到本地" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" Hidden="true" IsModal="true"
        Width="400px" Height="10px" EnableAjax="false">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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
