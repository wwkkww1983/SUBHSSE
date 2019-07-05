<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListAudit.aspx.cs"
    Async="true" Inherits="FineUIPro.Web.Technique.HazardListAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险源清单资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="危险源清单" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="HazardId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="HazardId" AllowSorting="true" SortField="CompileDate"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="100px" ColumnID="HazardCode" DataField="HazardCode" SortField="HazardCode"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险源代码">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="HazardListTypeCode" DataField="HazardListTypeCode"
                        SortField="HazardListTypeCode" FieldType="String" HeaderText="危险源类别编号" EnableFilter="true"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="HazardItems" DataField="HazardItems" SortField="HazardItems"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险因素明细">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="DefectsType" DataField="DefectsType" SortField="DefectsType"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="缺陷类型">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="MayLeadAccidents" DataField="MayLeadAccidents"
                        SortField="MayLeadAccidents" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                        HeaderText="可能导致的事故">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HelperMethod" DataField="HelperMethod" SortField="HelperMethod"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="辅助方法">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_L" DataField="HazardJudge_L" SortField="HazardJudge_L"
                        FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(L)">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_E" DataField="HazardJudge_E" SortField="HazardJudge_E"
                        FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(E)">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_C" DataField="HazardJudge_C" SortField="HazardJudge_C"
                        FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(C)">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardJudge_D" DataField="HazardJudge_D" SortField="HazardJudge_D"
                        FieldType="Float" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险评价(D)">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="HazardLevel" DataField="HazardLevel" SortField="HazardLevel"
                        FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="危险级别">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                        SortField="ControlMeasures" FieldType="String" HeaderTextAlign="Center" TextAlign="Center"
                        HeaderText="控制措施">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="整理人" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="380px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnPass" OnClick="btnPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用">
        </f:MenuButton>
        <f:MenuButton ID="btnUpPass" OnClick="btnUpPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用并上报">
        </f:MenuButton>
        <f:MenuButton ID="btnNoPass" OnClick="btnNoPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="不采用">
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
    </script>
</body>
</html>
