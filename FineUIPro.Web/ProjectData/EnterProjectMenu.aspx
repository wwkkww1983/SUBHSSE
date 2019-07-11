<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterProjectMenu.aspx.cs" Inherits="FineUIPro.Web.ProjectData.EnterProjectMenu" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
    <f:Panel ID="Panel1" runat="server" Margin="2px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目信息" EnableCollapse="true" runat="server"
                BoxFlex="1"  EnableColumnLines="true" DataKeyNames="ProjectId" DataIDField="ProjectId"
                AllowSorting="true" SortField="ProjectCode" SortDirection="DESC"  OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="项目名称" ID="txtProjectName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>  
                            <f:TextBox runat="server" Label="项目编号" ID="txtProjectCode" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                             
                            <f:DropDownList ID="drpUnit" Label="所属单位" runat="server" EnableEdit="true" Width="360px" LabelWidth="120px"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>                                                  
                            <f:ToolbarFill runat="server"></f:ToolbarFill> 
                             <f:CheckBoxList runat="server" ID="cbIsState" ShowLabel="false" Width="200px"  LabelAlign="Right"
                                OnSelectedIndexChanged="TextBox_TextChanged"  AutoPostBack="true">
                                <f:CheckItem Text="施工中" Value="1" Selected="true" />
                                <f:CheckItem Text="已完工/停工" Value="2" />
                             </f:CheckBoxList>                                                   
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                    <f:RenderField Width="120px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode" FieldType="String"
                        HeaderText="项目号">
                    </f:RenderField>
                    <f:RenderField Width="300px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                        FieldType="String" HeaderText="项目名称"  HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="开工日期"
                         HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="EndDate" DataField="EndDate" SortField="EndDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="竣工日期"
                       HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="ProjectTypeName" DataField="ProjectTypeName" SortField="ProjectTypeName"
                        FieldType="String" HeaderText="项目类型"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="ProjectStateName" DataField="ProjectStateName" SortField="ProjectStateName"
                        FieldType="String" HeaderText="项目状态"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="ProjectRoleName" DataField="ProjectRoleName" SortField="ProjectRoleName"
                        FieldType="String" HeaderText="所任角色"  HeaderTextAlign="Center" TextAlign="Left">
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
    </form>
     <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnEnter" OnClick="btnEnter_Click" Icon="ShapeSquareGo" EnablePostBack="true" 
            runat="server" Text="进入项目">
        </f:MenuButton>        
    </f:Menu>
    <script type="text/javascript">       
        // 返回false，来阻止浏览器右键菜单
        var menuID = '<%= Menu1.ClientID %>';
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
