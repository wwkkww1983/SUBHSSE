<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unit.aspx.cs" Inherits="FineUIPro.Web.SysManage.Unit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>单位设置</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true"  EnableCollapse="true" ShowHeader="false"
                runat="server" BoxFlex="1" DataKeyNames="UnitId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="UnitId" AllowSorting="true" SortField="IsThisUnit"
                SortDirection="DESC" OnSort="Grid1_Sort"  AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                             <f:TextBox runat="server" Label="单位名称" ID="txtUnitName" EmptyText="输入查询条件" 
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"></f:TextBox>                       
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>    
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server" Hidden="true">
                            </f:Button>                           
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="150px" ColumnID="UnitCode" DataField="UnitCode" SortField="UnitCode"
                        FieldType="String" HeaderText="单位代码" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" TextAlign="Left"
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center">                       
                    </f:RenderField>
                    <f:RenderField Width="115px" ColumnID="UnitTypeName" DataField="UnitTypeName" SortField="UnitTypeName"
                        FieldType="String" HeaderText="单位类型" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>                    
                    <f:RenderField Width="250px" ColumnID="Address" DataField="Address" ExpandUnusedSpace="true"
                        FieldType="String" HeaderText="联系地址" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="Telephone" DataField="Telephone"  Hidden="true"
                        FieldType="String" HeaderText="联系电话" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="160px" ColumnID="EMail" DataField="EMail"   Hidden="true"
                        FieldType="String" HeaderText="邮箱" HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:CheckBoxField Width="75px" RenderAsStaticField="true" TextAlign="Center"  DataField="IsThisUnit" HeaderText="本单位" />                   
                    <f:WindowField ColumnID="SubUnit" Width="80px" WindowID="WindowSubUnit" HeaderText="资质"
                                Text="详细" ToolTip="资质详细信息" DataTextFormatString="{0}" DataIFrameUrlFields="UnitId"
                                DataIFrameUrlFormatString="../QualityAudit/SubUnitQualityEdit.aspx?UnitId={0}"/>             
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
    <f:Window ID="Window1" Title="单位设置" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server"  IsModal="true"
        Width="1000px" Height="350px">
    </f:Window>
    <f:Window ID="WindowSubUnit" Title="分包商资质详细信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1024px" Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" Hidden="true"
            runat="server" Text="编辑" Icon="TableEdit">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true" Hidden="true"
            ConfirmText="删除选中行？" ConfirmTarget="Top" runat="server" Text="删除" Icon="Delete">
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
