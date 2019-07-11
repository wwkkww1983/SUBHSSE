<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafetyDataEItem.aspx.cs" Inherits="FineUIPro.Web.SafetyDataE.ProjectSafetyDataEItem" %>
<!DOCTYPE html>
<html>
<head runat="server">   
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>    
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyDataE1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>            
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="企业安全管理资料明细"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="SafetyDataEItemId" ClicksToEdit="2"
                        DataIDField="SafetyDataEItemId" AllowSorting="true" SortField="Code" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>                                                                       
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>                                 
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>                           
                            <f:RenderField ColumnID="Code" DataField="Code" SortField="Code" Width="150px"
                                FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>
                            <f:RenderField ColumnID="Title" DataField="Title" SortField="Title" Width="200px" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="标题" HeaderTextAlign="Center" TextAlign="left">
                            </f:RenderField>                                                                                                                
                            <f:RenderField Width="110px" ColumnID="SubmitDate" DataField="SubmitDate"  Renderer="Date"
                                SortField="SubmitDate" HeaderText="提交日期" HeaderTextAlign="Center"  FieldType="Date"
                                TextAlign="Left" >
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
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="30" Value="30" />
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>   
    <f:Menu ID="Menu2" runat="server">
        <Items>            
            <f:MenuButton ID="btnView" EnablePostBack="true" runat="server"  Icon="Find" Text="查看"
                OnClick="btnView_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>   
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="800px" Height="560px">
    </f:Window>
    </form>
    <script type="text/javascript">        
        var menu2ID = '<%= Menu2.ClientID %>';        
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
