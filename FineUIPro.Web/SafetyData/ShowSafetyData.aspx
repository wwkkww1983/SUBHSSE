<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSafetyData.aspx.cs" Inherits="FineUIPro.Web.SafetyData.ShowSafetyData" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="2px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业管理资料" EnableCollapse="true" runat="server"
                BoxFlex="1"  EnableColumnLines="true" DataKeyNames="SafetyDataId" DataIDField="SafetyDataId"
                AllowSorting="true" SortField="Code" SortDirection="ASC"  OnSort="Grid1_Sort"                
                AllowPaging="true" IsDatabasePaging="true"  PageSize="50" OnPageIndexChange="Grid1_PageIndexChange" 
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="资料名称" ID="txtTitle" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                             <f:Button ID="btnSure" ToolTip="确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                            </f:Button>                                                   
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                   
                    <f:RenderField Width="150px" ColumnID="Code" DataField="Code" SortField="Code" FieldType="String"
                        HeaderText="资料编号">
                    </f:RenderField>
                    <f:RenderField Width="350px" ColumnID="Title" DataField="Title" SortField="Title" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="资料名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="Score" DataField="Score" SortField="Score" FieldType="Double"
                        HeaderText="分值" HeaderTextAlign="Center" TextAlign="Left">
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
    </form>
     <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnEnter" OnClick="btnEnter_Click" Icon="ShapeSquareGo" EnablePostBack="true" 
            runat="server" Text="选择资料">
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
