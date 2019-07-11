<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthPlan.aspx.cs" Inherits="FineUIPro.Web.SiteConstruction.MonthPlan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月度计划</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" Layout="HBox"
                EnableCollapse="true" Width="200" Title="月度计划" TitleToolTip="月度计划" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>                   
                    <f:Tree ID="trMonthPlan" EnableCollapse="true" ShowHeader="false" ShowBorder="false" EnableIcons="true"
                        Title="月度计划" OnNodeCommand="trMonthPlan_NodeCommand" AutoLeafIdentification="true"  AutoScroll="true"
                        runat="server" EnableTextSelection="True" EnableSingleClickExpand="true" EnableSingleExpand="true">
                    </f:Tree>
                </Items>                
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="月度计划"
                TitleToolTip="月度计划" AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="月度计划" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="MonthPlanId"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="MonthPlanId" AllowSorting="true"
                        SortField="CompileDate" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>                                                        
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" Hidden="true"
                                        runat="server">
                                    </f:Button>
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
                            <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate"
                                SortField="CompileDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                HeaderText="日期" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName"
                                SortField="UnitName" FieldType="String" HeaderText="施工单位" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="JobContent" DataField="JobContent"
                                FieldType="String" HeaderText="主要工作内容" TextAlign="Left" ExpandUnusedSpace="true"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <%--<f:TemplateField Width="250px" HeaderText="主要工作内容" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="JobContent" runat="server" Text='<%# Bind("ShortJobContent") %>'
                                        ToolTip='<%#Bind("JobContent") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>--%>
                            <f:RenderField Width="70px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="编制人" HeaderTextAlign="Center"
                        TextAlign="Left">
                        </f:RenderField>
                        <f:RenderField Width="70px" ColumnID="AuditManName" DataField="AuditManName"
                        SortField="AuditManName" FieldType="String" HeaderText="审批人" HeaderTextAlign="Center"
                        TextAlign="Left">
                        </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                            <f:WindowField TextAlign="Left" Width="50px" WindowID="WindowAtt" HeaderText="附件" Text="附件" ToolTip="附件上传查看"
                                DataIFrameUrlFields="MonthPlanId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/MonthPlanAttachUrl&menuId=9D04CD8B-575C-4854-B8B0-F90CEEB75815"
                                HeaderTextAlign="Center" />
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
    <f:Window ID="Window1" Title="编辑月度计划" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="830px"
        Height="550px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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
