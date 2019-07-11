<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckDay.aspx.cs" Inherits="FineUIPro.Web.Check.CheckDay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>日常巡检</title>
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
          .LabelColor
        {
            color: Red;
            font-size:small;
        } 
         .f-grid-row.Yellow
        {
            background-color: Yellow;
        } 
        .f-grid-row.Green
        {
            background-color: LightGreen;
        }
          .f-grid-row.Red
        {
            background-color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="日常巡检" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="NewChcekId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="NewChcekId" AllowSorting="true" SortField="CheckTime"
                SortDirection="DESC" OnSort="Grid1_Sort" EnableColumnLines="true" 
                AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>    
                            <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="210px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="区域" ID="txtWorkAreaName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px"
                                LabelAlign="right">
                            </f:TextBox>                      
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                            AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="180px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                             AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="right" Width="180px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                        OnClick="btnPrint_Click">
                                    </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                   <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center"
                        EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="CheckDayCode" DataField="CheckDayCode"
                        SortField="CheckDayCode" FieldType="String" HeaderText="检查编号" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CheckCount" DataField="CheckCount"
                        SortField="CheckCount" FieldType="Int" HeaderText="不合格数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="WorkArea" DataField="WorkArea"
                        SortField="WorkArea" FieldType="String" HeaderText="检查区域" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" 
                        SortField="UnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="Unqualified" DataField="Unqualified"
                        SortField="Unqualified" FieldType="String" HeaderText="不合格项描述" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="70px" ColumnID="CompleteStatusName" DataField="CompleteStatusName"
                        SortField="CompleteStatusName" FieldType="String" HeaderText="整改" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="LimitedDate" DataField="LimitedDate" SortField="LimitedDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改限时"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CompletedDate" DataField="CompletedDate" SortField="CompletedDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="闭环时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CheckPersonName" DataField="CheckPersonName" SortField="CheckPersonName"
                        FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        FieldType="String" HeaderText="检查日期" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="FlowOperateName" DataField="FlowOperateName"
                        SortField="FlowOperateName" FieldType="String" HeaderText="状态" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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
                    <f:ToolbarFill runat="server">
                    </f:ToolbarFill>
                     <f:Label ID="Label2" runat="server" Text="说明：绿色-未审核完成；黄色-未闭环；白色-已闭环。"  CssClass="LabelColor"></f:Label>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="编辑日常巡检" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1300px" Height="660px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server" Hidden="true" Text="修改" Icon="Pencil"
                OnClick="btnMenuModify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuRectify" EnablePostBack="true" runat="server" Hidden="true" Text="生成整改单" Icon="ScriptLightning"
                OnClick="btnMenuRectify_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnCompletedDate" EnablePostBack="true" runat="server" Hidden="true" Text="闭环" Icon="TimeGreen"
                OnClick="btnCompletedDate_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server" Hidden="true" Icon="Delete" Text="删除" ConfirmText="确定删除当前数据？"
                OnClick="btnMenuDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['CheckDayCode', 'CheckCount', 'CheckPersonName', 'CheckTime', 'FlowOperateName'], { depends: true });
//            this.mergeColumns(['CheckCount']);
//            this.mergeColumns(['CheckPerson']);
//            this.mergeColumns(['CheckTime']);
//            this.mergeColumns(['FlowOperateName']);
        }
    </script>
</body>
</html>
