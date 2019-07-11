<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNotice.aspx.cs" Inherits="FineUIPro.Web.ServerCheck.CheckNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全监督检查通知单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }  
        .lab
        {           
            font-size:small;
            color:Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" 
                EnableCollapse="true" Width="250px" Title="监督检查" TitleToolTip="监督检查" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="ArrowCircleLeft" Layout="VBox" AutoScroll="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>       
                            <f:DatePicker ID="txtCheckStartTimeS" runat="server" Label="开始时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged" 
                                DateFormatString="yyyy-MM-dd" LabelWidth="75px">
                            </f:DatePicker>                                                                         
                        </Items> 
                     </f:Toolbar>
                   <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                        <f:DatePicker ID="txtCheckEndTimeS" runat="server" Label="结束时间" AutoPostBack="true" OnTextChanged="Tree_TextChanged" 
                                DateFormatString="yyyy-MM-dd" LabelWidth="75px">
                            </f:DatePicker>                                                                            
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:Tree ID="tvControlItem" EnableCollapse="true" ShowHeader="true" Title="监督检查节点树"
                        OnNodeCommand="tvControlItem_NodeCommand" AutoLeafIdentification="true" Height="410px"
                        runat="server" EnableTextSelection="true" AutoScroll="true">
                    </f:Tree>
                </Items>                
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="监督检查"
                TitleToolTip="监督检查" AutoScroll="true" >
                <Toolbars>
                     <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>                               
                            <f:Button ID="btnFind" Text="安全监督检查管理办法" Icon="Find" runat="server" OnClick="btnFind_Click">
                            </f:Button>                                                        
                            <f:HiddenField runat="server" ID="hdCheckInfoId"></f:HiddenField>                                
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill> 
                            <f:Label runat="server" Text="数据来源于集团下发到各企业查看并准备检查资料。" CssClass="lab"></f:Label>                                                  
                        </Items>
                    </f:Toolbar>                                 
                 </Toolbars>
                 <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true"
                            BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left">
                        <Rows>       
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCheckType" Label="检查方式" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label runat="server" ID="temp">
                                    </f:Label>
                                </Items>
                            </f:FormRow>                                                  
                            <f:FormRow ColumnWidths="40% 60%">
                                <Items>
                                    <f:Label ID="drpSubjectUnit" Label="受检单位" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtSubjectObject" ShowLabel="false" runat="server"></f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtSubjectUnitMan" Label="受检单位负责人" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtSubjectUnitTel" Label="受检单位负责人电话" runat="server" LabelWidth="160px">
                                    </f:Label>
                                </Items>                                    
                            </f:FormRow>                          
                            <f:FormRow >
                                <Items>
                                    <f:Label ID="txtCheckTeamLeader" Label="检查组长" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtSubjectUnitAdd" Label="受检单位地址"  runat="server" LabelWidth="130px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>                                    
                                    <f:Label ID="txtCheckStartTime" Label="检查开始日期" runat="server" LabelWidth="130px">
                                    </f:Label>
                                    <f:Label ID="txtCheckEndTime" Label="检查结束日期" runat="server" LabelWidth="130px">
                                    </f:Label>                                    
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" Label="编制人" runat="server" LabelWidth="90px">
                                    </f:Label>
                                    <f:Label ID="txtCompileDate" Label="编制日期" runat="server" LabelWidth="130px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                 </Items> 
                 <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="true"  Title="检查资料" Hidden="true"
                        runat="server" BoxFlex="1" DataKeyNames="CheckFileId" AllowSorting="true" IsDatabasePaging="true" PageSize="10"
                        OnSort="Grid1_Sort" SortField="SortIndex" SortDirection="ASC" AllowCellEditing="true"  
                        ClicksToEdit="2" EnableColumnLines="true" DataIDField="CheckFileId" AllowPaging="true"  
                         EnableTextSelection="True">
                        <Columns>                           
                            <f:RenderField Width="90px" ColumnID="SortIndex" DataField="SortIndex"
                                SortField="SortIndex" FieldType="Int" HeaderText="序号"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="350px" ColumnID="CheckFileName" DataField="CheckFileName"
                                SortField="CheckFileName" FieldType="String" HeaderText="资料名称"
                                HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="300px" ColumnID="Remark" DataField="Remark"
                                SortField="Remark" FieldType="String" HeaderText="备注"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu1" />
                        </Listeners>                       
                    </f:Grid>
                </Items>
                <Items>
                    <f:Grid ID="Grid2" Width="870px" ShowBorder="true" ShowHeader="true"  Title="检查组成员"
                        runat="server" BoxFlex="1" DataKeyNames="CheckTeamId" AllowSorting="true" IsDatabasePaging="true" PageSize="10"
                        OnSort="Grid2_Sort" SortField="SortIndex" SortDirection="ASC" AllowCellEditing="true" 
                        ClicksToEdit="2" EnableColumnLines="true" DataIDField="CheckTeamId" AllowPaging="true" 
                         EnableTextSelection="True">                                             
                        <Columns>                           
                            <f:RenderField Width="55px" ColumnID="SortIndex" DataField="SortIndex"
                                SortField="SortIndex" FieldType="Int" HeaderText="序号"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName"
                                SortField="UserName" FieldType="String" HeaderText="姓名"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                            <f:RenderField Width="60px" ColumnID="Sex" DataField="Sex"
                                SortField="Sex" FieldType="String" HeaderText="性别"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>   
                            <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName"
                                SortField="UnitName" FieldType="String" HeaderText="所在单位" ExpandUnusedSpace="true"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="PostName" DataField="PostName"
                                SortField="PostName" FieldType="String" HeaderText="单位职务"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="WorkTitle" DataField="WorkTitle"
                                SortField="WorkTitle" FieldType="String" HeaderText="职称"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="CheckPostName" DataField="CheckPostName"
                                SortField="CheckPostName" FieldType="String" HeaderText="检查工作组职务"
                                HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="CheckDate" DataField="CheckDate"
                                SortField="CheckDate" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                HeaderText="检查日期" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu2" />
                        </Listeners>                       
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
   <f:Window ID="Window3" Title="查看" ShowHeader="false" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="900px" Height="510px">
   </f:Window>
    </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu1(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function onRowContextMenu2(event, rowId) {
           // F(menuID2).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
