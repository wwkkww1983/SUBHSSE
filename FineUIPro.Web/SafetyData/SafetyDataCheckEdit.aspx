<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataCheckEdit.aspx.cs"
        Inherits="FineUIPro.Web.SafetyData.SafetyDataCheckEdit" ValidateRequest="false" %>
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
                <f:Panel runat="server" ID="panelTopRegion" RegionPosition="Top" RegionSplit="true" EnableCollapse="true" Height="115px"
                    Title="企业安全管理资料考核" ShowBorder="true" ShowHeader="false" BodyPadding="5px" IconFont="ArrowCircleUp">
                    <Items>
                       <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="企业安全管理资料考核" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                            <Rows>
                                <f:FormRow>
                                    <Items>
                                        <f:TextBox ID="txtCode" runat="server" Label="编号" LabelAlign="Right" Required="true" ShowRedStar="true"
                                            MaxLength="50" FocusOnPageLoad="true">
                                        </f:TextBox>
                                        <f:TextBox ID="txtTitle" runat="server" Label="名称" Required="true" ShowRedStar="true"
                                            LabelAlign="Right" MaxLength="100">
                                        </f:TextBox>
                                        <f:DropDownList ID="drpCompileMan" runat="server" Label="制单人" LabelAlign="Right" EnableEdit="true">
                                        </f:DropDownList>
                                    </Items>
                                </f:FormRow>                               
                                <f:FormRow>
                                    <Items>
                                        <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期" LabelAlign="Right" Required="true" ShowRedStar="true"
                                            EnableEdit="true">
                                        </f:DatePicker>  
                                         <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期" LabelAlign="Right" Required="true" ShowRedStar="true"
                                            EnableEdit="true">
                                        </f:DatePicker>
                                        <f:DatePicker ID="txtCompileDate" runat="server" Label="制单日期" LabelAlign="Right"
                                            EnableEdit="true">
                                        </f:DatePicker>             
                                    </Items>
                                </f:FormRow>
                            </Rows>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>        
                                        <f:Label runat="server" Text="考核状态说明：1、未上报，白色；2、准时上报，绿色；3、离警告时间一周未报，黄色；4、超期未报，红色；5、超期上报，紫色。"  CssClass="LabelColor"></f:Label>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>
                                         <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                                            OnClick="btnSave_Click" Hidden="true">
                                        </f:Button>                  
                                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                    Width="250px" Title="考核项目" TitleToolTip="选择和显示要考核的项目" ShowBorder="true" ShowHeader="false" 
                    BodyPadding="5px" Layout="HBox"> 
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                            <Items>                                            
                                <f:Button ID="btnExtract" Icon="ApplicationGet" runat="server" ToolTip="抽取企业安全管理资料考核计划"  Hidden="true"
                                    OnClick="btnExtract_Click" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true" AjaxLoadingMaskText="正在抽取企业安全管理资料考核计划，请稍候">
                                </f:Button>           
                                <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择项目" 
                                    ValidateForms="SimpleForm1" OnClick="btnSelect_Click" Hidden="true">
                                </f:Button>                              
                            </Items>
                        </f:Toolbar>
                    </Toolbars>                                              
                    <Items>
                        <f:Tree ID="tvProject" KeepCurrentSelection="true"
                            ShowHeader="false" OnNodeCommand="tvProject_NodeCommand" runat="server"
                            ShowBorder="false" EnableSingleClickExpand="true">
                            <Listeners>
                                <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                            </Listeners>
                        </f:Tree>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" ID="panel2" RegionPosition="Center" ShowBorder="true"
                    Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="要考核的项"
                    AutoScroll="true">
                    <Items>
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                            BoxFlex="1" DataKeyNames="SafetyDataCheckItemId" ClicksToEdit="2"
                            DataIDField="SafetyDataCheckItemId" AllowSorting="true" SortField="SafetyDataCode,StartDate" SortDirection="ASC"
                            OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" EnableColumnLines="true"
                            PageSize="100" OnPageIndexChange="Grid1_PageIndexChange" EnableSummary="true" SummaryPosition="Bottom"
                            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                    <Items>   
                                        <f:TextBox runat="server" Label="资料名称" ID="txtSTitle" EmptyText="输入查询条件" 
                                            AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>                                 
                                        <f:Button ID="btnGVNew" ToolTip="新增" Icon="Add" runat="server" OnClick="btnGVNew_Click" Hidden="true">
                                        </f:Button>
                                         <f:Button ID="btnOne" Icon="ApplicationGet" runat="server" ToolTip="当项目抽取企业安全管理资料考核计划"  Hidden="true"
                                            OnClick="btnOne_Click" AjaxLoadingType="Mask" ShowAjaxLoadingMaskText="true" AjaxLoadingMaskText="正在抽取企业安全管理资料考核计划，请稍候">
                                        </f:Button>   
                                        <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                            EnableAjax="false" DisableControlBeforePostBack="false">
                                        </f:Button>                                  
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>        
                                <f:RenderField ColumnID="SafetyDataTitle" DataField="SafetyDataTitle" SortField="SafetyDataTitle" Width="200px" 
                                    FieldType="String" HeaderText="考核资料" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>   
                                <f:RenderField ColumnID="SafetyDataCode" DataField="SafetyDataCode" SortField="SafetyDataCode" Width="90px"
                                    FieldType="String" HeaderText="编码" HeaderTextAlign="Center" TextAlign="left">
                                </f:RenderField>                                                                                   
                                <f:RenderField Width="120px" ColumnID="StartDate" DataField="StartDate"  Renderer="Date"
                                    SortField="StartDate" HeaderText="上报开始日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="120px" ColumnID="EndDate" DataField="EndDate"  Renderer="Date"
                                    SortField="EndDate" HeaderText="上报结束日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                 <f:RenderField Width="120px" ColumnID="CheckDate" DataField="CheckDate"  Renderer="Date"
                                    SortField="CheckDate" HeaderText="计划考核日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="95px" ColumnID="ReminderDate" DataField="ReminderDate"  Renderer="Date"
                                    SortField="ReminderDate" HeaderText="提醒日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                <f:RenderField Width="95px" ColumnID="SubmitDate" DataField="SubmitDate"  Renderer="Date"
                                    SortField="SubmitDate" HeaderText="提交日期" HeaderTextAlign="Center"  FieldType="Date"
                                    TextAlign="Left" >
                                </f:RenderField> 
                                 <f:RenderField Width="70px" ColumnID="ShouldScore" DataField="ShouldScore" SortField="ShouldScore" FieldType="Double"
                                    HeaderText="应得分" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>
                                 <f:RenderField Width="70px" ColumnID="RealScore" DataField="RealScore" SortField="RealScore" FieldType="Double"
                                    HeaderText="实得分" HeaderTextAlign="Center" TextAlign="Left">
                                </f:RenderField>     
                                 <f:RenderField Width="250px" ColumnID="Remark" DataField="Remark" SortField="Remark" FieldType="String"
                                    HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left" >
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
    <f:Menu ID="Menu1" runat="server">
        <Items>            
            <f:MenuButton ID="btnTreeDel" EnablePostBack="true" runat="server"  Text="删除" Icon="Delete" ConfirmText="确认删除选中内容？"
                OnClick="btnTreeDel_Click">
            </f:MenuButton>
        </Items>
    </f:Menu>
    <f:Menu ID="Menu2" runat="server">
        <Items>
            <f:MenuButton ID="btnGVModify" EnablePostBack="true" runat="server" Icon="BulletEdit"  Text="修改"
                OnClick="btnGVModify_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnGVDel" EnablePostBack="true" runat="server"  Icon="Delete" Text="删除" ConfirmText="确认删除选中内容？"
                OnClick="btnGVDel_Click" Hidden="true">
            </f:MenuButton>          
        </Items>
    </f:Menu>  
    <f:Window ID="Window1" Title="文件内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="800px" Height="550px">
    </f:Window>
    <f:Window ID="windowProject" Title="项目信息" Hidden="true" EnableIFrame="true" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1000px" Height="520px" EnableClose="true" OnClose="windowProject_Close"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
    <script type="text/javascript">
        var treeID = '<%= tvProject.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';
        var menu2ID = '<%= Menu2.ClientID %>';
        // 保存当前菜单对应的树节点ID
        var currentNodeId;
        // 返回false，来阻止浏览器右键菜单
        function onTreeNodeContextMenu(event, nodeId) {
            currentNodeId = nodeId;
            F(menuID).show();
            return false;
        }
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
