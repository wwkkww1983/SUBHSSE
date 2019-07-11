<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostManage.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全费用管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全费用管理" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="HSSECostManageId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="HSSECostManageId" AllowSorting="true"
                SortField="Month" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">               
                   <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:RadioButtonList runat="server" ID="rbStateType" AutoPostBack="true" Width="250px"
                                    OnSelectedIndexChanged="rbStateType_SelectedIndexChanged">                                 
                                <f:RadioItem Text="待完成" Value="0" Selected="true"/>
                                <f:RadioItem  Text="已完成" Value="1"/>
                                <f:RadioItem  Text="全部" Value="2"/>
                            </f:RadioButtonList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center" />
                       <f:RenderField Width="120px" ColumnID="Code" DataField="Code" SortField="Code"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="Month" DataField="Month" SortField="Month"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="月份"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="ReportDate" DataField="ReportDate" SortField="ReportDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="填报日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="110px" ColumnID="MainIncome" DataField="MainIncome"
                        SortField="MainIncome" FieldType="Double" HeaderText="主营业务收入" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="110px" ColumnID="ConstructionIncome" DataField="ConstructionIncome"
                        SortField="ConstructionIncome" FieldType="Double" HeaderText="施工收入" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="150px" ColumnID="SafetyCosts" DataField="SafetyCosts" 
                        SortField="SafetyCosts" FieldType="Double" HeaderText="已投入安全生产费用" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileManName" DataField="CompileManName"
                        SortField="CompileManName" FieldType="String" HeaderText="编制人" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="编制日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left" ExpandUnusedSpace="true">
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
    <f:Window ID="Window1" Title="安全费用管理" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1200px"
        Height="620px">
    </f:Window>    
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" Icon="Find" EnablePostBack="true"
           runat="server" Text="查看">
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
