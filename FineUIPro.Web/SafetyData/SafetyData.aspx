<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyData.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyData" %>
<!DOCTYPE html>
<html>
<head runat="server">   
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />
    <style type="text/css">
        .customlabel span
        {
            color: red;
            font-weight: bold;
        }                     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" Layout="Region"
             EnableCollapse="false"  Title="企业安全管理资料" TitleToolTip="右键点击添加、修改、删除"
            ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Left">
                <Items>
                    <f:CheckBoxList runat="server" ID="cbType" ShowLabel="true" Width="260px"  LabelAlign="Right"
                        OnSelectedIndexChanged="cbType_SelectedIndexChanged"  AutoPostBack="true" Label="资料类型">
                        <f:CheckItem Text="全部" Value="0" />
                        <f:CheckItem Text="考核项" Value="1" />
                     </f:CheckBoxList>  
                </Items>                    
            </f:Toolbar>
        </Toolbars> 
        <Items>
            <f:Tree ID="tvSafetyData" KeepCurrentSelection="true" ShowHeader="false"  runat="server"
                ShowBorder="true" EnableSingleClickExpand="true" >
                <Listeners>
                    <f:Listener Event="beforenodecontextmenu" Handler="onTreeNodeContextMenu" />
                </Listeners>
            </f:Tree>         
        </Items>
    </f:Panel>
    <f:Menu ID="Menu1" runat="server">
        <Items>
            <f:MenuButton ID="btnMenuNew" EnablePostBack="true" runat="server"  Text="增加" Icon="Add"
                OnClick="btnMenuNew_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuModify" EnablePostBack="true" runat="server"  Text="修改" Icon="BulletEdit"
                OnClick="btnMenuModify_Click" Hidden="true">
            </f:MenuButton>
            <f:MenuButton ID="btnMenuDel" EnablePostBack="true" runat="server"  Text="删除" Icon="Delete" ConfirmText="确认删除选中企业安全管理资料？"
                OnClick="btnMenuDel_Click" Hidden="true">
            </f:MenuButton>
        </Items>
    </f:Menu>    
    <f:Window ID="Window1" Title="" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="900px" Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">
        var treeID = '<%= tvSafetyData.ClientID %>';
        var menuID = '<%= Menu1.ClientID %>';  
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
    </script>
</body>
</html>
