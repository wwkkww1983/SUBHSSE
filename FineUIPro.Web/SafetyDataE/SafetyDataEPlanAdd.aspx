<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataEPlanAdd.aspx.cs" Inherits="FineUIPro.Web.SafetyDataE.SafetyDataEPlanAdd" %>

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
    <f:PageManager ID="PageSafetyDataE1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="2px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业管理资料" EnableCollapse="true" runat="server"
                BoxFlex="1"  EnableColumnLines="true" DataKeyNames="SafetyDataEPlanId" DataIDField="SafetyDataEPlanId"
                AllowSorting="true" SortField="Code" SortDirection="ASC"  OnSort="Grid1_Sort"                
                AllowPaging="false" IsDatabasePaging="true"  PageSize="500000"  AllowCellEditing="true" EnableTextSelection="True">                 
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="资料名称" ID="txtTitle" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                                                   
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                             <f:Button ID="btnSure" ToolTip="审核确定按钮" Icon="Accept" runat="server" OnClick="btnSure_Click">
                            </f:Button> 
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>        
                     <f:CheckBoxField ColumnID="ckbIsSelected" Width="50px" RenderAsStaticField="false" HeaderText="选择"
                                AutoPostBack="true" CommandName="IsSelected"  HeaderTextAlign="Center"/>  
                    <f:RenderField Width="180px" ColumnID="Code" DataField="Code" SortField="Code" FieldType="String"
                        HeaderText="资料编号">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="Title" DataField="Title" SortField="Title"  ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="资料名称"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="Score" DataField="Score" SortField="Score" FieldType="Double"
                        HeaderText="分值" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                    
                     <f:RenderField HeaderText="考核时间" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="120px" RendererArgument="yyyy-MM-dd" FieldType="Date" Renderer="Date">
                        <Editor>
                            <f:DatePicker ID="txtCheckDate"  runat="server" DateFormatString="yyyy-MM-dd" Required="true" ShowRedStar="true">
                            </f:DatePicker> 
                        </Editor>
                   </f:RenderField>
                  <f:RenderField HeaderText="应得分" ColumnID="ShouldScore" DataField="ShouldScore"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="80px">
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
     <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" Icon="Delete" EnablePostBack="true" 
            runat="server" Text="删除">
        </f:MenuButton>        
    </f:Menu>
    <script type="text/javascript">       
        // 返回false，来阻止浏览器右键菜单
         var menuID = '<%= Menu1.ClientID %>';
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
