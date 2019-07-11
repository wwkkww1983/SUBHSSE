<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelTestRecord.aspx.cs" Inherits="FineUIPro.Web.EduTrain.ModelTestRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模拟考试</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
       .f-grid-row.Red {
             background-color: red;
         } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="模拟考试" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ModelTestRecordId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="ModelTestRecordId" AllowSorting="true"
                SortField="TestStartTime" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="15" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                        <Items>                         
                            <f:TextBox ID="txtName" runat="server" Label="查询" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px" LabelAlign="Right">
                            </f:TextBox>
                            <f:NumberBox runat="server" ID="txtMinScores" Label="分值范围" AutoPostBack="true" NoNegative="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="80px" LabelAlign="Right"></f:NumberBox>
                                <f:NumberBox runat="server" ID="txtMaxScores" Label="至" AutoPostBack="true" NoNegative="true"
                                OnTextChanged="TextBox_TextChanged" Width="200px" LabelWidth="50px" LabelAlign="Right"></f:NumberBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                             <f:Button ID="btnOut" OnClick="btnMenuOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>                    
                    <f:RenderField Width="100px" ColumnID="TestManName" DataField="TestManName" FieldType="String"
                        HeaderText="考生姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160" ColumnID="TestStartTime" DataField="TestStartTime" FieldType="String"
                        HeaderText="考试开始时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160" ColumnID="TestEndTime" DataField="TestEndTime" FieldType="String"
                        HeaderText="考试结束时间" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                   
                     <f:RenderField Width="100px" ColumnID="TestScores" DataField="TestScores" FieldType="String"
                        HeaderText="考试成绩" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="TotalScore" DataField="TotalScore" FieldType="String"
                        HeaderText="总分" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="Duration" DataField="Duration" FieldType="String"
                        HeaderText="时长" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="QuestionCount" DataField="QuestionCount" FieldType="String"
                        HeaderText="题目数量" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="TrainingName" DataField="TrainingName" FieldType="String"
                        HeaderText="考题类型" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
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
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="模拟考试查看" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="560px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuView" OnClick="btnMenuView_Click" EnablePostBack="true"
            runat="server" Text="查看" Icon="TableGo">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
            Hidden="true">
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
