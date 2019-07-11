<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAZOP.aspx.cs" Inherits="FineUIPro.Web.Technique.HAZOP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HAZOP管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HAZOPId" AllowCellEditing="true" ClicksToEdit="2"
                        DataIDField="HAZOPId" AllowSorting="true" SortField="HAZOPDate" SortDirection="DESC"
                        OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                        EnableColumnMove="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                        OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                        EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                                <Items>
                                    <f:TextBox ID="HAZOPTitle" runat="server" Label="标题" EmptyText="输入查询标题" 
                                        Width="250px" LabelWidth="90px" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    
                                    <f:Button ID="btnQuery" ToolTip="查询" Icon="SystemSearch" EnablePostBack="true" 	
                                        OnClick="TextBox_TextChanged" runat="server" >
                                    </f:Button>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNew_Click"
                                        Hidden="true">
                                    </f:Button>
                                    <f:Button ID="btnEdit" ToolTip="编辑" Icon="Pencil" runat="server" OnClick="btnEdit_Click"
                                        Hidden="true">
                                    </f:Button>
                                     <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" Hidden="true"
                                        OnClick="btnDelete_Click" runat="server">
                                    </f:Button>
                                   <f:Button ID="btnUploadResources" ToolTip="上传资源" Icon="SystemNew" runat="server" Hidden="true"
                                        OnClick="btnUploadResources_Click">
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
                            <f:RenderField Width="180px" ColumnID="HAZOPTitle" DataField="HAZOPTitle" SortField="HAZOPTitle"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="标题">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="Abstract" DataField="Abstract" SortField="Abstract" ExpandUnusedSpace="true"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="摘要">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="HAZOPDate" DataField="HAZOPDate" SortField="HAZOPDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" 
                                Text="附件" ToolTip="附件上传查看" DataIFrameUrlFields="HAZOPId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HAZOP&menuId=41C22E63-36B7-4C44-89EC-F765BFBB7C55"/>
                            <f:RenderField Width="90px" ColumnID="CompileMan" DataField="CompileMan" SortField="CompileMan"
                                FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
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
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="编辑HAZOP管理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="550px" Height="360px">
    </f:Window>
    <f:Window ID="Window2" Title="上传资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window3" Title="审核资源" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1024px" Height="500px">
    </f:Window>
    <f:Window ID="Window4" Title="选择需要导出的列" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window4_Close" IsModal="true"
        Width="450px" Height="250px" EnableAjax="false">
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
