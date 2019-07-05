<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataPlan.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyDataPlan" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业安全管理资料考核计划</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目信息" 
                EnableCollapse="true" runat="server" BoxFlex="1"  EnableColumnLines="true"
                DataKeyNames="ProjectId" AllowCellEditing="true" ClicksToEdit="2" DataIDField="ProjectId"
                AllowSorting="true" SortField="ProjectCode" SortDirection="DESC"  OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="项目名称" ID="txtProjectName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="right"></f:TextBox>                       
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
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
                    <f:RenderField Width="100px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode" FieldType="String"
                        HeaderText="项目编号">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                        FieldType="String" HeaderText="项目名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="开工日期"
                         HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="EndDate" DataField="EndDate" SortField="EndDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="竣工日期"
                       HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="ProjectTypeName" DataField="ProjectTypeName" SortField="ProjectTypeName"
                        FieldType="String" HeaderText="项目类型"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="ProjectStateName" DataField="ProjectStateName" SortField="ProjectStateName"
                        FieldType="String" HeaderText="项目状态"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField ColumnID="ProjectAddress" DataField="ProjectAddress" SortField="ProjectAddress" FieldType="String"
                        HeaderText="项目地址" ExpandUnusedSpace="true"  HeaderTextAlign="Center" TextAlign="Left">
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
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="1300px" Height="650px">
    </f:Window>                  
    <f:Menu ID="Menu1" runat="server">       
         <f:MenuButton ID="btnView" EnablePostBack="true" runat="server"  Icon="Find" Text="查看"
                OnClick="btnView_Click">
        </f:MenuButton>     
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server"  Text="删除" Icon="Delete" ConfirmText="确认删除选中企业安全管理资料？"
            OnClick="btnMenuDel_Click" Hidden="true">
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

