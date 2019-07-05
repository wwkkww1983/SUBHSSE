<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafetyData.aspx.cs" Inherits="FineUIPro.Web.SafetyData.ProjectSafetyData" %>
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
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" Layout="HBox"
                EnableCollapse="true" Width="300px" Title="企业安全管理资料" TitleToolTip="右键点击添加、修改、删除" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                 <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList runat="server" ID="drpProject" RegionPosition="Center" Label="项目" EnableEdit="true" Width="285px"
                               AutoPostBack="true" OnSelectedIndexChanged ="drpProject_OnSelectedIndexChanged" LabelWidth="60px" LabelAlign="right"></f:DropDownList>
                        </Items>                    
                    </f:Toolbar>
                </Toolbars> 
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                           <f:CheckBoxList runat="server" ID="cbIsMenu" ShowLabel="true" Width="260px"  LabelAlign="Right"
                                OnSelectedIndexChanged="cbIsMenu_SelectedIndexChanged"  AutoPostBack="true" Label="管理资料">
                                <f:CheckItem Text="系统" Value="0" />
                                <f:CheckItem Text="定稿" Value="1" />
                             </f:CheckBoxList>  
                        </Items>                    
                    </f:Toolbar>
                </Toolbars>               
                <Items>
                    <f:Tree ID="tvProjectSafetyData" KeepCurrentSelection="true"
                        ShowHeader="false" OnNodeCommand="tvProjectSafetyData_NodeCommand" runat="server"
                        ShowBorder="false" EnableSingleClickExpand="true">
                        <Listeners>
                            <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                        </Listeners>
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="企业安全管理资料明细"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="SafetyDataItemId" ClicksToEdit="2"
                        DataIDField="SafetyDataItemId" AllowSorting="true" SortField="Code" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>                                    
                                    <f:Button ID="btnNewDetail" ToolTip="新增" Icon="Add" runat="server" Hidden="true"
                                        OnClick="btnNewDetail_Click">
                                    </f:Button>  
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
                            <f:RenderField Width="110px" ColumnID="CompileDate" DataField="CompileDate"  Renderer="Date"
                                SortField="CompileDate" HeaderText="单据日期" HeaderTextAlign="Center"  FieldType="Date"
                                TextAlign="Left" >
                            </f:RenderField> 
                            <f:RenderField Width="110px" ColumnID="SubmitDate" DataField="SubmitDate"  Renderer="Date"
                                SortField="SubmitDate" HeaderText="提交日期" HeaderTextAlign="Center"  FieldType="Date"
                                TextAlign="Left" >
                            </f:RenderField> 
                            <f:RenderField ColumnID="FromData" DataField="FromData" SortField="FromData" Width="100px"
                                FieldType="String" HeaderText="文件来源" HeaderTextAlign="Center" TextAlign="Center">
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
            <f:MenuButton ID="btnMenuModifyDetail" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="修改"
                OnClick="btnMenuModifyDetail_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDelDetail" EnablePostBack="true" runat="server"  Icon="Delete" Text="删除" ConfirmText="确认删除选中内容？"
                OnClick="btnMenuDelDetail_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnView" EnablePostBack="true" runat="server"  Icon="Find" Text="查看"
                OnClick="btnView_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>   
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="550px">
    </f:Window>
    </form>
    <script type="text/javascript">
        var treeID = '<%= tvProjectSafetyData.ClientID %>';   
        var menu2ID = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            //F(menuID).show();
            return false;
        }
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
