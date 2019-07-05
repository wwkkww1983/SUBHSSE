<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonChange.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonChange" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员变化</title>
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
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="300px" Title="人员变化" TitleToolTip="人员变化" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="Fit">
                <Items>
                    <f:Tree ID="tvUnit" EnableCollapse="true" ShowHeader="false" Title="项目单位" OnNodeCommand="tvUnit_NodeCommand" AutoLeafIdentification="true" runat="server" ShowBorder="false" 
                         EnableTextSelection="True">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="人员变化"
                TitleToolTip="人员变化" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="人员变化" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="PersonInOutId"
                        DataIDField="PersonInOutId" AllowSorting="true" SortField="ChangeTime" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                        OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:DatePicker ID="txtTime" runat="server" Label="日期" LabelAlign="Right" EnableEdit="true" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                    </f:DatePicker>                              
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <%--<f:Button ID="btnInOut" ToolTip="出/入场" Icon="LayoutEdit" runat="server" Hidden="true" OnClick="btnInOut_Click">
                                    </f:Button> --%>                                   
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                            EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>  
                             <f:RenderField HeaderText="日期" ColumnID="ChangeTime" DataField="ChangeTime" SortField="ChangeTime"
                                 FieldType="Date" HeaderTextAlign="Center" Renderer="Date" TextAlign="Center" Width="200px">                               
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="InCount" DataField="InCount" 
                                FieldType="Int" HeaderText="入场人数" HeaderTextAlign="Center"
                                TextAlign="Right">                          
                            </f:RenderField>                          
                            <f:RenderField Width="160px" ColumnID="OutCount" DataField="OutCount" 
                                FieldType="Int" HeaderText="出场人数" HeaderTextAlign="Center"
                                TextAlign="Right">                          
                            </f:RenderField> 
                            <f:RenderField Width="160px" ColumnID="TotalCount" DataField="TotalCount" 
                                FieldType="Int" HeaderText="当前人数" HeaderTextAlign="Center"
                                TextAlign="Right" ExpandUnusedSpace="true">                          
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
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑人员变化" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="550px">
    </f:Window>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }     

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
