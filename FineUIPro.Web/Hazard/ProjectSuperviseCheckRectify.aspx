<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSuperviseCheckRectify.aspx.cs"
    Inherits="FineUIPro.Web.Hazard.ProjectSuperviseCheckRectify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业监督检查整改</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row.yellow
        {
            background-color: YellowGreen;
            background-image: none;
        }
        
        .f-grid-row.red
        {
            background-color: Yellow;
        }    
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业监督检查整改" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SuperviseCheckRectifyId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="SuperviseCheckRectifyId" AllowSorting="true" SortField="CheckDate"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true"
                AllowPaging="true" IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>     
                             <f:TextBox runat="server" ID="txtName" Label="查询" EmptyText="请输入查询条件" LabelWidth="50px" Width="250px"
                                AutoPostBack="true" OnTextChanged="txtName_TextChanged">
                            </f:TextBox>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>                           
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
                    <f:RenderField Width="80px" ColumnID="SuperviseCheckRectifyCode" DataField="SuperviseCheckRectifyCode"
                        SortField="SuperviseCheckRectifyCode" FieldType="String" HeaderText="编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:GroupField HeaderText="检查对象" TextAlign="Left" HeaderTextAlign="Center">
                        <Columns>
                            <f:RenderField Width="200px" ColumnID="ProjectName" DataField="ProjectName" FieldType="String" ExpandUnusedSpace="true"
                                HeaderText="项目" HeaderToolTip="检查项目" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                HeaderText="单位" HeaderToolTip="检查单位" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="95px" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="受检时间"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="IssueMan" DataField="IssueMan" SortField="IssueMan"
                        FieldType="String" HeaderText="签发人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="IssueDate" DataField="IssueDate" SortField="IssueDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="签发时间"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="60px" ColumnID="HandleStateName" DataField="HandleStateName" FieldType="String"
                        HeaderText="状态" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="50px" ColumnID="TotalCount" DataField="TotalCount" FieldType="String"
                        HeaderText="总项" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="60px" ColumnID="CompleteCount" DataField="CompleteCount" FieldType="String"
                        HeaderText="完成项" TextAlign="Center" HeaderTextAlign="Center">
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
                        <f:ListItem Text="15" Value="15" Selected="true"/>
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="编辑企业监督检查整改" CloseAction="HidePostBack" EnableIFrame="true" Height="500px"
        Width="1200px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
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
