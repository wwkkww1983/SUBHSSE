<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MajorHazardList.aspx.cs" Inherits="FineUIPro.Web.Hazard.MajorHazardList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目现场重大HSE因素控制措施一览表</title>
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目现场重大HSE因素控制措施一览表" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="NewId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="NewId" AllowSorting="true" SortField="HazardListCode"
                SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True" AllowColumnLocking="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:NumberBox runat="server" Label="危险评价D" ID="txtStartD" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="90px" LabelAlign="right" NoNegative="true">
                              </f:NumberBox>
                            <f:NumberBox runat="server" Label="危险级别" ID="txtHazardLevel" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" LabelWidth="80px" LabelAlign="right"  NoNegative="true"  NoDecimal="true">
                            </f:NumberBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server"  ToolTip="导出" Icon="FolderUp"
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
                    <f:RenderField Width="130px" ColumnID="HazardListCode" DataField="HazardListCode" SortField="HazardListCode"
                        FieldType="String" HeaderText="辨识评价编号" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfWorkStage" Width="150px" HeaderText="工作阶段" HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkStage" runat="server" Text='<%# ConvertWorkStage(Eval("WorkStage")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="HazardCode" DataField="HazardCode" SortField="HazardCode"
                        FieldType="String" HeaderText="危险源代码" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>                           
                    <f:RenderField Width="280px" ColumnID="HazardItems" DataField="HazardItems" SortField="HazardItems"
                        FieldType="String" HeaderText="危险因素明细" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="DefectsType" DataField="DefectsType" SortField="DefectsType"
                        FieldType="String" HeaderText="缺陷类型" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="MayLeadAccidents" DataField="MayLeadAccidents"
                        SortField="MayLeadAccidents" FieldType="String" HeaderText="可能导致的事故" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HelperMethod" DataField="HelperMethod"
                        SortField="HelperMethod" FieldType="String" HeaderText="辅助方法" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_L" DataField="HazardJudge_L"
                        SortField="HazardJudge_L" FieldType="String" HeaderText="危险评价(L)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_E" DataField="HazardJudge_E"
                        SortField="HazardJudge_E" FieldType="String" HeaderText="危险评价(E)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_C" DataField="HazardJudge_C"
                        SortField="HazardJudge_C" FieldType="String" HeaderText="危险评价(C)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="HazardJudge_D" DataField="HazardJudge_D"
                        SortField="HazardJudge_D" FieldType="String" HeaderText="危险评价(D)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="HazardLevel" DataField="HazardLevel"
                        SortField="HazardLevel" FieldType="String" HeaderText="危险级别" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                        SortField="ControlMeasures" FieldType="String" HeaderText="控制措施" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
    <script type="text/javascript">       
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['HazardListCode']);
        }
    </script>
</body>
</html>
