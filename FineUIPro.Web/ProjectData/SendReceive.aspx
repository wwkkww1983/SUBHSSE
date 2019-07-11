<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendReceive.aspx.cs" Inherits="FineUIPro.Web.ProjectData.SendReceive" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目用户转换</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
          .f-grid-row.Red
        {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="项目用户转换" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SendReceiveId" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="SendReceiveId" AllowSorting="true" SortField="SendTime" SortDirection="DESC"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10" EnableColumnLines="true"
                OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="姓名" ID="txtUserName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="单位" ID="txtUnitName" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server"></f:ToolbarSeparator> 
                            <f:RadioButtonList runat="server" ID="cbIsSend" ShowLabel="false" Width="140px"  LabelAlign="Right"
                                OnSelectedIndexChanged="TextBox_TextChanged"  AutoPostBack="true">
                                <f:RadioItem Text="送出" Value="0" Selected="true" />
                                <f:RadioItem Text="接收" Value="1" />
                             </f:RadioButtonList>  
                            <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server"></f:ToolbarSeparator>                             
                            <f:CheckBox ID="ckbShow" runat="server" LabelWidth="60px" Label="待接收" 
                                AutoPostBack="true" OnCheckedChanged="ckbShow_CheckedChanged">
                            </f:CheckBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSend" OnClick="btnSend_Click" runat="server" 
                                ToolTip="送出" Icon="DoorOut" Hidden="true">
                            </f:Button>                                                       
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
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
                    <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center"
                        TextAlign="Left">                        
                    </f:RenderField>
                    <f:RenderField Width="85px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Center">                        
                    </f:RenderField>
                    <f:RenderField Width="155px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                        FieldType="String" HeaderText="身份证号码" HeaderTextAlign="Center"
                        TextAlign="Left">                        
                    </f:RenderField>
                     <f:RenderField Width="250px" HeaderText="送出项目" ColumnID="SendProjectName" DataField="SendProjectName" SortField="SendProjectName"
                            FieldType="String" HeaderTextAlign="Center" TextAlign="Left" >                               
                    </f:RenderField>
                    <f:RenderField Width="145px" ColumnID="SendTime" DataField="SendTime" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="SendTime" HeaderText="送出时间" >
                    </f:RenderField>
                    <f:RenderField Width="80px" HeaderText="是否接收" ColumnID="IsAgreeName" DataField="IsAgreeName" SortField="IsAgreeName"
                            FieldType="String" HeaderTextAlign="Center" TextAlign="Left" >                               
                    </f:RenderField>                    
                    <f:RenderField Width="145px" ColumnID="ReceiveTime" DataField="ReceiveTime" HeaderTextAlign="Center" TextAlign="Center"
                        SortField="ReceiveTime" HeaderText="处理时间" >
                    </f:RenderField>
                    <f:RenderField Width="250px" HeaderText="接收项目" ColumnID="ReceiveProjectName" DataField="ReceiveProjectName" 
                         SortField="ReceiveProjectName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left" >                               
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="项目用户转换" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1024px" Height="620px" >
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnReceive" OnClick="btnReceive_Click" EnablePostBack="true"
             runat="server" Icon="DoorIn" Text="接收" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnNoReceive" OnClick="btnNoReceive_Click" EnablePostBack="true"
             runat="server" Icon="DoorIn" Text="拒收" Hidden="true">
        </f:MenuButton>
        <f:MenuButton ID="btnView" OnClick="btnView_Click" EnablePostBack="true"
             runat="server" Icon="Pencil" Text="查看">
        </f:MenuButton>
         <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server"
            Text="删除">
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
