<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataEPlanItem.aspx.cs" Inherits="FineUIPro.Web.SafetyDataE.SafetyDataEPlanItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>项目考核计划明细</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                <Items> 
                     <f:TextBox runat="server" Label="名称" ID="txtTitle" EmptyText="输入查询条件"
                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="180px" LabelWidth="50px"
                        LabelAlign="right">
                    </f:TextBox>
                                                 
                    <f:ToolbarFill runat="server" ID="lbTemp1"></f:ToolbarFill>
                     <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>                                  
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
           <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="企业管理资料" EnableCollapse="true" runat="server"
                BoxFlex="1"  EnableColumnLines="true" DataKeyNames="SafetyDataEPlanId" DataIDField="SafetyDataEPlanId"
                AllowSorting="true" SortField="CheckDate" SortDirection="ASC"  OnSort="Grid1_Sort" Height="500"                
                AllowPaging="false" IsDatabasePaging="true"  PageSize="500000"  AllowCellEditing="true" EnableTextSelection="True">
                <Columns>                             
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
                   </f:RenderField>
                  <f:RenderField HeaderText="应得分" ColumnID="ShouldScore" DataField="ShouldScore"
                        HeaderTextAlign="Center"  TextAlign="Left" Width="80px">                       
                  </f:RenderField>
                  <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" SortField="Remark" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="备注"  HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>                          
            </f:Grid>
        </Items>
    </f:Panel>               
    </form>
    <script type="text/javascript">               
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            //F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }            
    </script>
</body>
</html>
