<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafetyDataECheck.aspx.cs" Inherits="FineUIPro.Web.SafetyDataE.ProjectSafetyDataECheck" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业安全管理资料考核计划</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
    <style type="text/css">
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
            background-color:Red;
        }
        .f-grid-row.Purple
        {
            background-color:mediumpurple;
        }    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>                             
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"  AutoScroll="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="项目考核项">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>        
                                    <f:Label runat="server" Text="状态说明：1、未上报，白色；2、准时上报，绿色；3、离警告时间一周未报，黄色；4、超期未报，红色；5、超期上报，紫色。"  CssClass="LabelColor"></f:Label>                               
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Left" runat="server">
                                <Items>       
                                     <f:DropDownList ID="drpState" runat="server" Label="状态" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged" 
                                        LabelWidth="50px" Width="160px">
                                        <f:ListItem Text="所有" Value="0"/>
                                        <f:ListItem Text="未上报" Value="1" Selected="true"/>
                                        <f:ListItem Text="已上报" Value="2"/>
                                        <f:ListItem Text="超期未上报" Value="3"/>
                                        <f:ListItem Text="超期上报" Value="4"/>
                                        <f:ListItem Text="正常上报" Value="5"/>
                                    </f:DropDownList>                                   
                                    <f:TextBox runat="server" Label="查询" ID="txtTitle" EmptyText="输入查询条件"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="50px"
                                        LabelAlign="right">
                                    </f:TextBox>
                                     <f:DatePicker runat="server" Label="考核时间" ID="txtStarTime" EnableEdit="true" 
                                         AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="70px"
                                        LabelAlign="right"></f:DatePicker>
                                    <f:DatePicker runat="server" Label="至" ID="txtEndTime" EnableEdit="true" 
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="150px" LabelWidth="40px"
                                        LabelAlign="right"></f:DatePicker>                                    
                                    <f:ToolbarFill runat="server" ID="lbTemp1"></f:ToolbarFill>  
                                    <f:Button ID="btnAdd" ToolTip="增加" Icon="Add" runat="server" OnClick="btnAdd_Click">
                                    </f:Button>
                                    <f:Button ID="btnTreeView" OnClick="btnTreeView_Click" runat="server" ToolTip="查看项目考核计划总表" Icon="Find"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button> 
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>      
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                     <Items>                         
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                            BoxFlex="1" DataKeyNames="SafetyDataEPlanId" ClicksToEdit="2"
                            DataIDField="SafetyDataEPlanId" AllowSorting="true" SortField="CheckDate" SortDirection="ASC"
                            OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                            PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" EnableSummary="true" SummaryPosition="Bottom"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">                           
                            <Columns>       
                                <f:RenderField ColumnID="Code" DataField="Code" SortField="Code" Width="90px" 
                                    FieldType="String" HeaderText="编码" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>      
                                <f:RenderField ColumnID="Title" DataField="Title" SortField="Title" Width="230px" 
                                    FieldType="String" HeaderText="考核资料" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>                                 
                                <f:RenderField Width="120px" ColumnID="CheckDate" DataField="CheckDate"  Renderer="Date"
                                    SortField="CheckDate" HeaderText="考核日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField>
                                 <f:RenderField Width="90px" ColumnID="ReminderDate" DataField="ReminderDate"  Renderer="Date"
                                    SortField="ReminderDate" HeaderText="提醒日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                 <f:RenderField Width="90px" ColumnID="SubmitDate" DataField="SubmitDate"  Renderer="Date"
                                    SortField="SubmitDate" HeaderText="提交日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                 <f:RenderField Width="70px" ColumnID="Score" DataField="Score" SortField="Score" FieldType="Double"
                                    HeaderText="分值" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                 <f:RenderField Width="70px" ColumnID="ShouldScore" DataField="ShouldScore" SortField="ShouldScore" FieldType="Double"
                                    HeaderText="应得值" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                 <f:RenderField Width="70px" ColumnID="RealScore" DataField="RealScore" SortField="RealScore" FieldType="Double"
                                    HeaderText="得分" HeaderTextAlign="Center" TextAlign="Right">
                                </f:RenderField>
                                 <f:RenderField Width="160px" ColumnID="Remark" DataField="Remark" SortField="Remark" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
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
                                    <f:ListItem Text="15" Value="15" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="25" Value="25" />
                                    <f:ListItem Text="30" Value="30" />
                                    <f:ListItem Text="100" Value="100" />
                                    <f:ListItem Text="所有行" Value="100000" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>                      
                    </Items>
                </f:Panel>               
            </Items>
        </f:Panel>    
    </form>   
     <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="900px" Height="560px">
    </f:Window> 
    <f:Window ID="Window2" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" OnClose="Window2_Close" EnableClose="true"
        Width="900px" Height="560px">
    </f:Window>   
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="上报"
                OnClick="btnMenuNew_Click">
            </f:MenuButton>
            <f:MenuButton ID="btnGVView" EnablePostBack="true" runat="server"  Icon="Find" Text="上报资料" 
                OnClick="btnGVView_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>  
    <script type="text/javascript">    
        var Menu1ID = '<%= Menu1.ClientID %>';  
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(Menu1ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }

         function onGridDataLoad(event) {
            this.mergeColumns(['Title']);
            this.mergeColumns(['Code']);
        }
    </script>
</body>
</html>

