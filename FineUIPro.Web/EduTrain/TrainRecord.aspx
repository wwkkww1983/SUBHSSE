<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecord.aspx.cs" Inherits="FineUIPro.Web.EduTrain.TrainRecord" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训记录</title>
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="培训记录" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="TrainingId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="TrainingId" AllowSorting="true"
                SortField="TrainingCode" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" AllowFilters="true"
                OnFilterChange="Grid1_FilterChange" EnableTextSelection="True" EnableSummary="true" SummaryPosition="Flow">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="所属单位" AutoPostBack="true" OnSelectedIndexChanged="Text_TextChanged"
                                LabelAlign="Right">
                            </f:DropDownList>
                            <f:TextBox ID="txtTrainTypeName" runat="server" Label="培训类型" Width="250px" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:TextBox>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="培训日期" Width="220px" LabelAlign="Right"
                                AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:DatePicker>
                            <f:Label ID="lblTo" runat="server" Text="至">
                            </f:Label>
                            <f:DatePicker ID="txtEndDate" runat="server" Width="120px" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="增加" Icon="Add" runat="server" Hidden="true" OnClick="btnNew_Click">
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
                    <f:RenderField Width="110px" ColumnID="TrainingCode" DataField="TrainingCode" SortField="TrainingCode"
                        FieldType="String" HeaderText="培训编号" HeaderTextAlign="Center" TextAlign="Left">                        
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfUnitIds" Width="400px" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="left" ExpandUnusedSpace="true"> 
                        <ItemTemplate>
                            <asp:Label ID="lblUnitId" runat="server" Text='<%# ConvertUnitName(Eval("UnitIds")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="TrainTypeName" DataField="TrainTypeName" SortField="TrainTypeName"
                        FieldType="String" HeaderText="培训类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TrainStartDate" DataField="TrainStartDate"
                        SortField="TrainStartDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                        HeaderText="培训日期" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="55px" ColumnID="TeachHour" DataField="TeachHour" SortField="TeachHour"
                        FieldType="Float" HeaderText="学时" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TeachMan" DataField="TeachMan" SortField="TeachMan"
                        FieldType="String" HeaderText="授课人" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TrainPersonNum" DataField="TrainPersonNum"
                        SortField="TrainPersonNum" FieldType="String" HeaderText="培训人数" HeaderTextAlign="Center"
                        TextAlign="Center">
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
    <f:Window ID="Window1" Title="编制培训记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="768px">
    </f:Window>
    <f:Window ID="Window2" Title="打印培训记录" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        Height="768px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            runat="server" Icon="TableEdit" Hidden="true" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnPrint" OnClick="btnPrint_Click" EnablePostBack="true"
            runat="server" Icon="Printer" Text="打印">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            ConfirmText="删除选中行？" Icon="Delete" Hidden="true" ConfirmTarget="Top" runat="server"
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
