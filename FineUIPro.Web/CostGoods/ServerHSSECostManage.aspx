<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerHSSECostManage.aspx.cs" Inherits="FineUIPro.Web.CostGoods.ServerHSSECostManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全费用管理</title>
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
                            <f:DropDownList ID="drpProject" runat="server" Label="项目" AutoPostBack="true"  EnableEdit="true"
                                    OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="50px" Width="400px">
                            </f:DropDownList>  
                            <f:DropDownList ID="drpYear" runat="server" Label="年度" EnableEdit="true"
                                 AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="50px" Width="150px">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonths" runat="server" Label="月份" EnableEdit="true"
                                AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="50px" Width="150px">
                            </f:DropDownList> 
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
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
                    <f:RenderField Width="100px" ColumnID="Code" DataField="Code" SortField="Code"
                        FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="Months" DataField="Months" SortField="Months"
                        FieldType="String"  HeaderText="月份" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="ProjectName" DataField="ProjectName" ExpandUnusedSpace="true"
                        SortField="ProjectName" FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ReportDate" DataField="ReportDate" SortField="ReportDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="填报日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                     <%--<f:RenderField Width="100px" ColumnID="MainIncome" DataField="MainIncome"
                        SortField="MainIncome" FieldType="Double" HeaderText="主营业务收入（万元）" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="ConstructionIncome" DataField="ConstructionIncome"
                        SortField="ConstructionIncome" FieldType="Double" HeaderText="施工收入（万元）" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                     <f:RenderField Width="110px" ColumnID="SafetyCosts" DataField="SafetyCosts" 
                        SortField="SafetyCosts" FieldType="Double" HeaderText="已投入安全生产费用（万元）" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>--%>
                    <f:RenderField Width="140px" ColumnID="EngineeringCostSum" DataField="EngineeringCostSum" 
                        SortField="EngineeringCostSum" FieldType="Double" HeaderText="当月已支付工程款</br>（万元）" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="AuditedSubUnitCostSum" DataField="AuditedSubUnitCostSum" 
                        SortField="AuditedSubUnitCostSum" FieldType="Double" HeaderText="安全生产费用</br>（万元）" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CostRatioSum" DataField="CostRatioSum" 
                        SortField="CostRatioSum" FieldType="Double" HeaderText="占比(%)" HeaderTextAlign="Center"
                        TextAlign="Center">
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
