<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expert.aspx.cs" Inherits="FineUIPro.Web.Technique.Expert" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全专家" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ExpertId" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="ExpertId" AllowSorting="true" SortField="ExpertCode" SortDirection="ASC"
                OnSort="Grid1_Sort" AllowColumnLocking="true" EnableColumnLines="true" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                        <Items>
                            <f:TextBox ID="ExpertName" runat="server" Label="姓名" EmptyText="输入查询姓名" 
                                Width="210px" LabelWidth="70px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="UnitName" runat="server" Label="单位" EmptyText="输入查询单位" 
                                Width="210px" LabelWidth="70px" LabelAlign="Right">
                            </f:TextBox>
                            <f:TextBox ID="ExpertTypeName" runat="server" Label="类别" EmptyText="输入查询专家类别" 
                                Width="210px" LabelWidth="70px" LabelAlign="Right">
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
                    <f:TemplateField Width="90px" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="ExpertCode">
                        <ItemTemplate>
                            <asp:Label ID="lblExpertCode" runat="server" Text='<%# Bind("ExpertCode") %>' ToolTip='<%#Bind("ExpertCode") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="90px" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="ExpertName">
                        <ItemTemplate>
                            <asp:Label ID="lblExpertName" runat="server" Text='<%# Bind("ExpertName") %>' ToolTip='<%#Bind("ExpertName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="250px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="UnitName" ExpandUnusedSpace="true">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UnitName") %>' ToolTip='<%#Bind("UnitName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="120px" HeaderText="专家类别" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="ExpertTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblExpertTypeName" runat="server" Text='<%# Bind("ExpertTypeName") %>'
                                ToolTip='<%#Bind("ExpertTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="90px" HeaderText="专业" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="PersonSpecialtyName">
                        <ItemTemplate>
                            <asp:Label ID="lblPersonSpecialtyName" runat="server" Text='<%# Bind("PersonSpecialtyName") %>'
                                ToolTip='<%#Bind("PersonSpecialtyName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField Width="90px" HeaderText="职称" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="PostTitleName">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PostTitleName") %>' ToolTip='<%#Bind("PostTitleName") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="资质有效期"
                        EnableFilter="true">
                        <Filter EnableMultiFilter="true" ShowMatcher="true">
                            <Operator>
                                <f:DropDownList ID="DropDownList3" runat="server">
                                    <f:ListItem Text="大于" Value="greater" Selected="true" />
                                    <f:ListItem Text="小于" Value="less" />
                                    <f:ListItem Text="等于" Value="equal" />
                                </f:DropDownList>
                            </Operator>
                            <Field>
                                <f:DatePicker runat="server" Required="true" ID="DatePicker1">
                                </f:DatePicker>
                            </Field>
                        </Filter>
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="80px" WindowID="WindowAtt" Text="附件"
                        ToolTip="附件上传查看" DataIFrameUrlFields="ExpertId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Expert&menuId=05495F29-B583-43D9-89D3-3384D6783A3F"
                         />
                    <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <p>
                                    <strong>姓名：</strong><%# Eval("ExpertName")%>
                                </p>
                                <p>
                                    <strong>简介：</strong><%# Eval("Performance")%>
                                </p>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
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
    <f:Window ID="Window1" Title="编辑安全专家" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1100px" Height="560px">
    </f:Window>
    <f:Window ID="Window2" runat="server" Hidden="true" AutoScroll="false"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window2_Close"
        Title="安全专家" CloseAction="HidePostBack" EnableIFrame="true" Height="610px" Width="900px">
    </f:Window>
    <f:Window ID="Window3" Title="上传资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window4" Title="审核资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1024px" Height="500px">
    </f:Window>
    <f:Window ID="Window5" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window5_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
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
