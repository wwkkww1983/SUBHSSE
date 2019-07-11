<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataPlanAdd.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyDataPlanAdd" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />  
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="2px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业管理资料" EnableCollapse="true" runat="server"
                BoxFlex="1"  EnableColumnLines="true" DataKeyNames="SafetyDataId" DataIDField="SafetyDataId"
                AllowSorting="true" SortField="Code" SortDirection="ASC"  OnSort="Grid1_Sort"                
                AllowPaging="false" IsDatabasePaging="true"  PageSize="500000"  AllowCellEditing="true" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar id="tooo01" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DropDownBox runat="server" ID="drpProject" Label="项目" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                                EnableMultiSelect="true"  LabelWidth="80px" Width="800px" AutoPostBack="true" OnTextChanged="drpProject_TextChanged">
                                <PopPanel>
                                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="ProjectId" DataTextField="ProjectName"
                                        DataKeyNames="ProjectId"  AllowSorting="true" SortField="ProjectCode" SortDirection="ASC" EnableColumnLines="true"
                                        Hidden="true" Width="600px" Height="400px" EnableMultiSelect="true" PageSize="300">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                                                <Items>
                                                    <f:TextBox runat="server" Label="名称" ID="txtProjectName" EmptyText="输入查询条件" 
                                                        AutoPostBack="true" OnTextChanged="gvTextBox_TextChanged" Width="250px" LabelWidth="80px">
                                                     </f:TextBox>
                                                     <f:TextBox runat="server" Label="编号" ID="txtProjectCode" EmptyText="输入查询条件" 
                                                        AutoPostBack="true" OnTextChanged="gvTextBox_TextChanged" Width="250px" LabelWidth="80px">
                                                     </f:TextBox>                                                                          
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                                HeaderTextAlign="Center" TextAlign="Center"/>
                                              <f:RenderField Width="120px" ColumnID="ProjectCode" DataField="ProjectCode" 
                                                SortField="ProjectCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                                                TextAlign="Left">                       
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName" EnableFilter="true"
                                                SortField="ProjectName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center" ExpandUnusedSpace="true"
                                                TextAlign="Left">                      
                                            </f:RenderField>                                          
                                        </Columns>
                                    </f:Grid>
                                </PopPanel>
                            </f:DropDownBox>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="资料名称" ID="txtTitle" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            
                             <f:Button ID="btnSure" ToolTip="确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                            </f:Button>                                                   
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>     
                     <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false" HeaderText="选择"
                                AutoPostBack="true" CommandName="IsSelected"  HeaderTextAlign="Center"/>    
                    <f:RenderField Width="100px" ColumnID="Code" DataField="Code" SortField="Code" FieldType="String"
                        HeaderText="资料编号">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="Title" DataField="Title" SortField="Title" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="资料名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="70px" ColumnID="Score" DataField="Score" SortField="Score" FieldType="Double"
                        HeaderText="分值" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField HeaderText="单据开始时间" ColumnID="RealStartDate" DataField="RealStartDate" SortField="RealStartDate"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="110px" RendererArgument="yyyy-MM-dd" FieldType="Date" Renderer="Date">
                        <Editor>
                            <f:DatePicker ID="txtRealStartDate"  runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true">
                            </f:DatePicker> 
                        </Editor>
                   </f:RenderField>
                   <f:RenderField HeaderText="单据结束时间" ColumnID="RealEndDate" DataField="RealEndDate" SortField="RealEndDate"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="110px" RendererArgument="yyyy-MM-dd" FieldType="Date" Renderer="Date">
                        <Editor>
                            <f:DatePicker ID="txtRealEndDate"  runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true">
                            </f:DatePicker> 
                        </Editor>
                   </f:RenderField>
                     <f:RenderField HeaderText="考核截止时间" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="110px" RendererArgument="yyyy-MM-dd" FieldType="Date" Renderer="Date">
                        <Editor>
                            <f:DatePicker ID="txtCheckDate"  runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true">
                            </f:DatePicker> 
                        </Editor>
                   </f:RenderField>
                    <f:RenderField HeaderText="提醒时间" ColumnID="ReminderDate" DataField="ReminderDate" SortField="ReminderDate"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="90px" RendererArgument="yyyy-MM-dd" FieldType="Date" Renderer="Date">
                        <Editor>
                            <f:DatePicker ID="txtReminderDate"  runat="server" DateFormatString="yyyy-MM-dd" Required="true">
                            </f:DatePicker> 
                        </Editor>
                   </f:RenderField>
                  <f:RenderField HeaderText="应得分" ColumnID="ShouldScore" DataField="ShouldScore"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="70px">
                        <Editor>
                            <f:NumberBox ID="txtShouldScore"  runat="server" NoNegative="true" DecimalPrecision="1" ShowRedStar="true">
                            </f:NumberBox>
                        </Editor>
                  </f:RenderField>
                  <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" SortField="Remark" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="备注"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>                 
            </f:Grid>
        </Items>
    </f:Panel>          
    </form>
     <%--<f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnEnter" OnClick="btnEnter_Click" Icon="ShapeSquareGo" EnablePostBack="true" 
            runat="server" Text="选择资料">
        </f:MenuButton>        
    </f:Menu>--%>
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
