<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthServer.aspx.cs" Inherits="FineUIPro.Web.Manager.ManagerTotalMonthServer" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>HSSE月总结</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全月总结" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="TotalMonth"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="TotalMonth" AllowSorting="true"
                SortField="TotalMonth" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"   OnRowCommand="Grid1_RowCommand"
                 EnableTextSelection="True"> 
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                     
                    <f:RenderField Width="90px" ColumnID="TotalMonth" DataField="TotalMonth" SortField="TotalMonth"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM" HeaderText="月份"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:LinkButtonField Width="120px" HeaderText="本月HSE工作<br/>基本情况内容" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent">
                    </f:LinkButtonField>  
                    <f:LinkButtonField Width="150px" HeaderText="本月主要完成<br/>HSE工作量数据统计" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent2">
                    </f:LinkButtonField>  
                    <f:LinkButtonField Width="200px" HeaderText="本月具体HSE工作开展情况<br/>(包括不可接受风险与控制情况)" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent3">
                    </f:LinkButtonField>  
                    <f:LinkButtonField Width="190px" HeaderText="本月HSE工作<br/>存在问题与处理(或拟采取对策)" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent4" ExpandUnusedSpace="true">
                    </f:LinkButtonField>  
                    <f:LinkButtonField Width="110px" HeaderText="下月工作计划" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent5">
                    </f:LinkButtonField>  
                    <f:LinkButtonField Width="60px" HeaderText="其他" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent6">
                    </f:LinkButtonField>       
                     <f:LinkButtonField Width="90px" HeaderText="工时人数" Text="查阅" HeaderTextAlign="Center"
                        TextAlign="Center"  DataCommandArgumentField="TotalMonth" CommandName="monthContent7">
                    </f:LinkButtonField>            
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
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" 
        Width="1024px" Height="620px">
    </f:Window> 
    <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" 
        Width="1200px" Height="620px">
    </f:Window> 
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
    </script>
</body>
</html>
