<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostUnitManageAudit.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostUnitManageAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全分包费用审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全分包费用审核" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HSSECostUnitManageId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="HSSECostUnitManageId" AllowSorting="true"
                SortField="Month" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="280px">
                            </f:DropDownList>
                             <f:DropDownList ID="drpYear" runat="server" Label="月份" Required="true"
                                ShowRedStar="true" LabelWidth="60px" Width="150px"  AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonths" runat="server" Required="true" 
                                ShowRedStar="true" Width="100px"  AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged">
                            </f:DropDownList> 
                            <f:RadioButtonList runat="server" ID="rbStateType" AutoPostBack="true" Width="350px"
                                    OnSelectedIndexChanged="TextBox_TextChanged">
                                <f:RadioItem  Text="安全审核" Value="2"  Selected="true"/>
                                <f:RadioItem  Text="费控核定" Value="3"/>
                                <f:RadioItem  Text="已完成" Value="4"/>
                                <f:RadioItem  Text="全部" Value="5"/>
                            </f:RadioButtonList>                            
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                     <f:RenderField Width="80px" ColumnID="Month" DataField="Month" SortField="Month"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="月份"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode"
                        FieldType="String" HeaderText="单位编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" ExpandUnusedSpace="true"
                        SortField="UnitName" FieldType="String" HeaderText="填报单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="CostAsum" DataField="CostAsum"
                        SortField="CostAsum" FieldType="Double" HeaderText="审核安全生产费用" HeaderTextAlign="Center"
                        TextAlign="Right">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
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
    <f:Window ID="Window1" Title="安全分包费用审核" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1100px"
        Height="600px">
    </f:Window>    
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="审核">
        </f:MenuButton>
         <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" Icon="Find" EnablePostBack="true"
           runat="server" Text="查看">
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
