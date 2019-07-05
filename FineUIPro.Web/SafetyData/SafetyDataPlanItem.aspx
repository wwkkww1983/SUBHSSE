<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataPlanItem.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyDataPlanItem" %>

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
                    <f:Button ID="btnDelete" ToolTip="删除项目不考核项" Icon="TableDelete" EnableAjax="false" Hidden="true"
                        runat="server" OnClick="btnDelete_Click">
                    </f:Button>
                     <f:Button ID="btnAdd" ToolTip="恢复项目不考核项" Icon="TableAdd" EnableAjax="false" Hidden="true"
                        runat="server" OnClick="btnAdd_Click">
                    </f:Button>
                     <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>                                  
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Grid ID="Grid1" runat="server" Width="100%" EnableCheckBoxSelect="false" ShowHeader="false" EnableColumnLines="true"
                Title="计划总表" BoxFlex="1" Height="500px" >
            </f:Grid>
        </Items>
    </f:Panel> 
    <f:Window ID="Window1" Title="考核项考核维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="900px"   OnClose="Window1_Close"
        Height="560px">
    </f:Window>             
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
