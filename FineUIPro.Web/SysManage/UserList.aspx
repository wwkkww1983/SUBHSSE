<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="FineUIPro.Web.SysManage.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="用户信息" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UserId" AllowSorting="true" SortField="UserCode"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                OnRowDoubleClick="Grid1_RowDoubleClick" Width="980px" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:TextBox runat="server" Label="用户名称" ID="txtUserName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="单位名称" ID="txtUnitName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="角色名称" ID="txtRoleName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="类型名称" ID="txtRoleTypeName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                            </f:TextBox>
                            <f:ToolbarFill runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server"
                                Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="45px" HeaderTextAlign="Center"
                        TextAlign="Center" />                  
                    <f:RenderField Width="80px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="姓名" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="75px" ColumnID="Account" DataField="Account" SortField="Account"
                        FieldType="String" HeaderText="登录账号" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard"
                        FieldType="String" HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Right" ExpandUnusedSpace="true">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                        FieldType="String" HeaderText="角色" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="85px" ColumnID="RoleTypeName" DataField="RoleTypeName" SortField="RoleTypeName"
                        FieldType="String" HeaderText="角色类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="Telephone" DataField="Telephone" SortField="Telephone"
                        FieldType="String" HeaderText="手机号码" HeaderTextAlign="Center" TextAlign="Right">
                    </f:RenderField>
                    <f:CheckBoxField Width="50px" SortField="IsPost" RenderAsStaticField="true" DataField="IsPost"
                        HeaderText="在岗" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField>
                    <f:CheckBoxField Width="75px" SortField="IsOffice" RenderAsStaticField="true" DataField="IsOffice"
                        HeaderText="本部人员" HeaderTextAlign="Center" TextAlign="Center">
                    </f:CheckBoxField>
                    <f:TemplateField Width="75px" ColumnID="UserId" HeaderText="参与项目" TextAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnPro" runat="server" Text="查看" OnClick="lbtnPro_Click" ></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
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
    <f:Window ID="Window1" Title="用户信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="360px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Hidden="true" ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除"
            Icon="Delete">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script type="text/jscript">
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
