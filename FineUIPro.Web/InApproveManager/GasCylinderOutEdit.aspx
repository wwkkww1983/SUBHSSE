<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GasCylinderOutEdit.aspx.cs"
    Inherits="FineUIPro.Web.InApproveManager.GasCylinderOutEdit" ValidateRequest="false" %>
    <%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑气瓶出场报批</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .customlabel span
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGasCylinderOutCode" runat="server" Label="气瓶出场编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="单位名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="30% 20% 50%">
                <Items>
                    <f:DatePicker ID="txtOutDate" runat="server" Label="出场时间" LabelAlign="Right" EnableEdit="true"
                        EmptyText="请选择日期">
                    </f:DatePicker>
                    <f:TimePicker ID="txtOutTime" EnableEdit="true" Increment="1" runat="server" EmptyText="请选择时间">
                    </f:TimePicker>
                    <f:TextBox ID="txtDriverName" runat="server" Label="驾驶员姓名" LabelAlign="Right" MaxLength="20">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDriverNum" runat="server" Label="驾驶证号" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtCarNum" runat="server" Label="车牌号" LabelAlign="Right" MaxLength="20">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtLeaderName" runat="server" Label="带车人" LabelAlign="Right" MaxLength="20">
                    </f:TextBox>
                    <f:Label ID="lblE" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="气瓶基本情况" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="GasCylinderOutItemId"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="GasCylinderOutItemId" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True"
                        Height="200px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                                <Items>
                                    <f:Label ID="lblT" runat="server" Text="气瓶基本情况" CssClass="customlabel span">
                                    </f:Label>
                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnNew" Icon="Add" runat="server" ToolTip="新增气瓶基本情况" ValidateForms="SimpleForm1"
                                        OnClick="btnNew_Click">
                                    </f:Button>                
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                            <f:TemplateField ColumnID="tfGasCylinderId" HeaderText="气瓶类型" Width="400px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGasCylinderId" runat="server" Text='<%#ConvertGasCylinder(Eval("GasCylinderId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="300px" ColumnID="GasCylinderNum" DataField="GasCylinderNum"
                                SortField="GasCylinderNum" FieldType="Int" HeaderText="数量" HeaderTextAlign="Center"
                                TextAlign="Left">
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
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="气瓶基本情况" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="200px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除">
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
