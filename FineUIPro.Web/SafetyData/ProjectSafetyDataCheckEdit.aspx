<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafetyDataCheckEdit.aspx.cs"
        Inherits="FineUIPro.Web.SafetyData.ProjectSafetyDataCheckEdit" ValidateRequest="false" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑企业安全管理资料考核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
            background-color:Purple;
        }        
    </style>
</head>
<body>
     <form id="form1" runat="server">
        <f:PageManager ID="PageSafetyData1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
            <Items>
                <f:Panel runat="server" ID="panelTopRegion" RegionPosition="Top" RegionSplit="true" EnableCollapse="false" Height="115px"
                    Title="企业安全管理资料考核" ShowBorder="true" ShowHeader="false" BodyPadding="5px" IconFont="ArrowCircleUp">
                    <Items>
                       <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="企业安全管理资料考核" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtTitle" runat="server" Label="名称" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>
                                         <f:TextBox ID="drpCompileMan" runat="server" Label="制单人" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>   
                                    </Items>
                                </f:FormRow>                               
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtStartDate" runat="server" Label="开始日期" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>  
                                         <f:TextBox ID="txtEndDate" runat="server" Label="结束日期" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtCompileDate" runat="server" Label="制单日期" LabelAlign="Right" Readonly="true">
                                        </f:TextBox>             
                                    </Items>
                                </f:FormRow>
                            </Rows>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>        
                                        <f:Label ID="Label1" runat="server" Text="考核状态说明：1、未上报，白色；2、准时上报，绿色；3、离警告时间一周未报，黄色；4、超期未报，红色；5、超期上报，紫色。"  CssClass="LabelColor"></f:Label>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>                                                    
                                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                    </Items>
                </f:Panel>               
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="要考核的项"
                    AutoScroll="true">
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="false" runat="server"
                            BoxFlex="1" DataKeyNames="SafetyDataCheckItemId" ClicksToEdit="2"
                            DataIDField="SafetyDataCheckItemId" AllowSorting="true" SortField="SafetyDataCode,StartDate" SortDirection="ASC"
                            OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true" EnableSummary="true" SummaryPosition="Bottom"
                            PageSize="15" OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">                           
                            <Columns>        
                                <f:RenderField ColumnID="SafetyDataTitle" DataField="SafetyDataTitle" SortField="SafetyDataTitle" Width="250px" 
                                    FieldType="String" HeaderText="考核资料" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>   
                                <f:RenderField ColumnID="SafetyDataCode" DataField="SafetyDataCode" SortField="SafetyDataCode" Width="90px"
                                    FieldType="String" HeaderText="编码" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>                                                                                   
                                <f:RenderField Width="100px" ColumnID="StartDate" DataField="StartDate"  Renderer="Date"
                                    SortField="StartDate" HeaderText="开始日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="120px" ColumnID="EndDate" DataField="EndDate"  Renderer="Date"
                                    SortField="EndDate" HeaderText="结束(截至)日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="100px" ColumnID="ReminderDate" DataField="ReminderDate"  Renderer="Date"
                                    SortField="ReminderDate" HeaderText="提醒日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="100px" ColumnID="SubmitDate" DataField="SubmitDate"  Renderer="Date"
                                    SortField="SubmitDate" HeaderText="提交日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                 <f:RenderField Width="80px" ColumnID="ShouldScore" DataField="ShouldScore" SortField="ShouldScore" FieldType="Double"
                                    HeaderText="应得分" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                 <f:RenderField Width="80px" ColumnID="RealScore" DataField="RealScore" SortField="RealScore" FieldType="Double"
                                    HeaderText="实得分" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>     
                                 <f:RenderField Width="90px" ColumnID="Remark" DataField="Remark" SortField="Remark" FieldType="String"
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
                                    <f:ListItem Text="所有行" Value="100000" />
                                </f:DropDownList>
                            </PageItems>
                        </f:Grid>                      
                    </Items>
                </f:Panel>               
            </Items>
        </f:Panel>
    </form>    
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="增加"
                OnClick="btnMenuNew_Click">
            </f:MenuButton>                   
        </Items>
    </f:Menu>  
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="600px">
    </f:Window>   
    <script type="text/javascript">
        var menu2ID = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID    
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menu2ID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['SafetyDataTitle']);
            this.mergeColumns(['SafetyDataCode']);
        }
    </script>
</body>
</html>
